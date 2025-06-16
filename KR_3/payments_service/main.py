import asyncio
from fastapi import FastAPI, Depends, HTTPException
from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select

from database import init_db, get_session
from models import Account
from schemas import TopUp
from publisher import publish_outbox_loop
from consumer import consume_orders_loop

app = FastAPI(title="Payments Service")

@app.on_event("startup")
async def on_startup():
    await init_db()
    asyncio.create_task(publish_outbox_loop())
    asyncio.create_task(consume_orders_loop())

def get_user_id_header(user_id: str | None = None):
    return int(user_id) if user_id else 1

@app.post("/account")
async def create_account(
    user_id: int = Depends(get_user_id_header),
    session: AsyncSession = Depends(get_session),
):
    res = await session.execute(select(Account).where(Account.user_id == user_id))
    if (acc := res.scalar_one_or_none()):
        return {"balance": acc.balance}
    acc = Account(user_id=user_id, balance=0)
    session.add(acc)
    await session.commit()
    return {"balance": 0}

@app.post("/account/topup")
async def topup(
    data: TopUp,
    user_id: int = Depends(get_user_id_header),
    session: AsyncSession = Depends(get_session),
):
    res = await session.execute(
        select(Account).where(Account.user_id == user_id).with_for_update()
    )
    acc = res.scalar_one_or_none() or Account(user_id=user_id, balance=0)
    acc.balance += data.amount
    session.add(acc)
    await session.commit()
    return {"balance": acc.balance}

@app.get("/account/balance")
async def balance(
    user_id: int = Depends(get_user_id_header),
    session: AsyncSession = Depends(get_session),
):
    res = await session.execute(select(Account).where(Account.user_id == user_id))
    if (acc := res.scalar_one_or_none()) is None:
        raise HTTPException(status_code=404, detail="Account not found")
    return {"balance": acc.balance}
