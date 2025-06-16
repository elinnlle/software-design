import asyncio, uuid
from fastapi import FastAPI, Depends, HTTPException, WebSocket, WebSocketDisconnect
from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select, insert

from database import init_db, get_session
from models import Order, Outbox, OrderStatus
from schemas import OrderCreate, OrderOut
from publisher import publish_outbox_loop
from consumer import consume_payments_loop
from websocket_manager import ConnectionManager

app = FastAPI(title="Orders Service")
manager = ConnectionManager()

@app.on_event("startup")
async def on_startup():
    await init_db()
    asyncio.create_task(publish_outbox_loop())
    asyncio.create_task(consume_payments_loop(manager))

def get_user_id_header(user_id: str | None = None):
    return int(user_id) if user_id else 1

@app.post("/orders", response_model=OrderOut)
async def create_order(
    data: OrderCreate,
    user_id: int = Depends(get_user_id_header),
    session: AsyncSession = Depends(get_session),
):
    order = Order(user_id=user_id, amount=data.amount)
    session.add(order)
    await session.flush()

    outbox = Outbox(
        aggregate_id=order.id,
        event_type="OrderCreated",
        payload={"order_id": str(order.id), "user_id": user_id, "amount": data.amount},
    )
    session.add(outbox)
    await session.commit()
    return OrderOut(id=order.id, amount=order.amount, status=order.status)

@app.get("/orders", response_model=list[OrderOut])
async def list_orders(
    user_id: int = Depends(get_user_id_header),
    session: AsyncSession = Depends(get_session),
):
    result = await session.execute(select(Order).where(Order.user_id == user_id))
    orders = result.scalars().all()
    return [OrderOut(id=o.id, amount=o.amount, status=o.status) for o in orders]

@app.get("/orders/{order_id}", response_model=OrderOut)
async def get_order(
    order_id: uuid.UUID,
    user_id: int = Depends(get_user_id_header),
    session: AsyncSession = Depends(get_session),
):
    result = await session.execute(select(Order).where(Order.id == order_id))
    order = result.scalar_one_or_none()
    if not order:
        raise HTTPException(status_code=404, detail="Order not found")
    if order.user_id != user_id:
        raise HTTPException(status_code=403, detail="Forbidden")
    return OrderOut(id=order.id, amount=order.amount, status=order.status)

@app.websocket("/ws/orders/{order_id}")
async def ws_orders(order_id: str, websocket: WebSocket):
    await manager.connect(order_id, websocket)
    try:
        while True:
            await websocket.receive_text()
    except WebSocketDisconnect:
        await manager.disconnect(order_id, websocket)
