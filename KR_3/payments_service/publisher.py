import os, json, asyncio
from aio_pika import connect_robust, Message
from sqlalchemy import select, update

from database import SessionLocal
from models import Outbox

RABBIT_URL = os.getenv("RABBIT_URL", "amqp://guest:guest@rabbitmq/")

async def publish_outbox_loop():
    connection = await connect_robust(RABBIT_URL)
    channel = await connection.channel()
    exchange = await channel.declare_exchange("payments", durable=True)

    while True:
        async with SessionLocal() as session:
            result = await session.execute(select(Outbox).where(Outbox.sent == False))
            for msg in result.scalars():
                body = json.dumps(msg.payload).encode()
                await exchange.publish(
                    Message(body, content_type="application/json"),
                    routing_key=msg.event_type,
                )
                await session.execute(
                    update(Outbox).where(Outbox.id == msg.id).values(sent=True)
                )
            await session.commit()
        await asyncio.sleep(1)
