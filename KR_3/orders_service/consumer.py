import os, json, asyncio
from aio_pika import connect_robust
from sqlalchemy import select

from database import SessionLocal
from models import Order
from websocket_manager import ConnectionManager

RABBIT_URL = os.getenv("RABBIT_URL", "amqp://guest:guest@rabbitmq/")

async def consume_payments_loop(manager: ConnectionManager):
    connection = await connect_robust(RABBIT_URL)
    channel = await connection.channel()
    await channel.set_qos(prefetch_count=10)

    exchange = await channel.declare_exchange("payments", durable=True)
    queue = await channel.declare_queue("", exclusive=True)
    await queue.bind(exchange, routing_key="Payment*")

    async with queue.iterator() as q:
        async for message in q:
            async with message.process():
                payload = json.loads(message.body.decode())
                order_id = payload["order_id"]
                status = payload["status"]
                async with SessionLocal() as session:
                    res = await session.execute(select(Order).where(Order.id == order_id))
                    if (order := res.scalar_one_or_none()):
                        order.status = status
                        await session.commit()
                        await manager.send_update(str(order_id), {"status": status})
