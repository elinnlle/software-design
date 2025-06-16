import os, json, asyncio, uuid
from aio_pika import connect_robust
from sqlalchemy import select, insert
from sqlalchemy.exc import IntegrityError

from database import SessionLocal
from models import Inbox, Account, Transaction, Outbox

RABBIT_URL = os.getenv("RABBIT_URL", "amqp://guest:guest@rabbitmq/")

async def consume_orders_loop():
    connection = await connect_robust(RABBIT_URL)
    channel = await connection.channel()
    await channel.set_qos(prefetch_count=10)

    exchange = await channel.declare_exchange("orders", durable=True)
    queue = await channel.declare_queue("", exclusive=True)
    await queue.bind(exchange, routing_key="OrderCreated")

    async with queue.iterator() as q:
        async for message in q:
            async with message.process():
                payload = json.loads(message.body.decode())
                message_id = payload.get("id", str(uuid.uuid4()))
                order_id = uuid.UUID(payload["order_id"])
                user_id = payload["user_id"]
                amount = payload["amount"]

                async with SessionLocal() as session:
                    try:
                        await session.execute(insert(Inbox).values(message_id=message_id))
                        await session.commit()
                    except IntegrityError:
                        await session.rollback()
                        continue  # уже обработано

                    res = await session.execute(
                        select(Account).where(Account.user_id == user_id).with_for_update()
                    )
                    acc = res.scalar_one_or_none() or Account(user_id=user_id, balance=0)
                    success = False
                    if acc.balance >= amount:
                        acc.balance -= amount
                        success = True
                    session.add(acc)
                    session.add(
                        Transaction(
                            user_id=user_id,
                            order_id=order_id,
                            amount=amount,
                            success=success,
                        )
                    )
                    event_type = "PaymentCompleted" if success else "PaymentFailed"
                    session.add(
                        Outbox(
                            event_type=event_type,
                            payload={
                                "order_id": str(order_id),
                                "status": "PAID" if success else "FAILED",
                            },
                        )
                    )
                    await session.commit()
