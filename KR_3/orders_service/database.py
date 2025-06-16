import os
from sqlalchemy.ext.asyncio import create_async_engine, async_sessionmaker, AsyncSession
from sqlalchemy.orm import declarative_base

DB_URL = os.getenv("ORDERS_DB_URL", "postgresql+asyncpg://orders:orders@orders_db/orders")

engine = create_async_engine(DB_URL, echo=False, pool_size=5, max_overflow=10)
Base = declarative_base()
SessionLocal = async_sessionmaker(engine, expire_on_commit=False, class_=AsyncSession)

async def init_db():
    async with engine.begin() as conn:
        await conn.run_sync(Base.metadata.create_all)

async def get_session() -> AsyncSession:
    async with SessionLocal() as session:
        yield session
