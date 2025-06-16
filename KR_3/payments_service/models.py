import uuid
from datetime import datetime
from sqlalchemy import Integer, DateTime, Boolean, String, JSON
from sqlalchemy.orm import Mapped, mapped_column
from sqlalchemy.dialects.postgresql import UUID

from database import Base

class Account(Base):
    __tablename__ = "accounts"
    user_id: Mapped[int] = mapped_column(Integer, primary_key=True)
    balance: Mapped[int] = mapped_column(Integer, default=0)

class Transaction(Base):
    __tablename__ = "transactions"
    id: Mapped[uuid.UUID] = mapped_column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)
    user_id: Mapped[int] = mapped_column(Integer, nullable=False)
    order_id: Mapped[uuid.UUID] = mapped_column(UUID(as_uuid=True), nullable=False)
    amount: Mapped[int] = mapped_column(Integer, nullable=False)
    success: Mapped[bool] = mapped_column(Boolean, default=False)
    created_at: Mapped[datetime] = mapped_column(DateTime, default=datetime.utcnow)

class Inbox(Base):
    __tablename__ = "inbox"
    message_id: Mapped[str] = mapped_column(String, primary_key=True)
    processed_at: Mapped[datetime] = mapped_column(DateTime, default=datetime.utcnow)

class Outbox(Base):
    __tablename__ = "outbox"
    id: Mapped[int] = mapped_column(Integer, primary_key=True, autoincrement=True)
    event_type: Mapped[str] = mapped_column(String, nullable=False)
    payload: Mapped[dict] = mapped_column(JSON, nullable=False)
    sent: Mapped[bool] = mapped_column(Boolean, default=False)
    created_at: Mapped[datetime] = mapped_column(DateTime, default=datetime.utcnow)
