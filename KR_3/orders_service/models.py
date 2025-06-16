import uuid
from datetime import datetime
from sqlalchemy import String, Integer, DateTime, Enum, JSON, Boolean
from sqlalchemy.orm import Mapped, mapped_column
from sqlalchemy.dialects.postgresql import UUID

from database import Base
import enum

class OrderStatus(str, enum.Enum):
    PENDING = "PENDING"
    PAID = "PAID"
    FAILED = "FAILED"

class Order(Base):
    __tablename__ = "orders"
    id: Mapped[uuid.UUID] = mapped_column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)
    user_id: Mapped[int] = mapped_column(Integer, nullable=False)
    amount: Mapped[int] = mapped_column(Integer, nullable=False)
    status: Mapped[OrderStatus] = mapped_column(Enum(OrderStatus), default=OrderStatus.PENDING)
    created_at: Mapped[datetime] = mapped_column(DateTime, default=datetime.utcnow)

class Outbox(Base):
    __tablename__ = "outbox"
    id: Mapped[int] = mapped_column(Integer, primary_key=True, autoincrement=True)
    aggregate_id: Mapped[uuid.UUID] = mapped_column(UUID(as_uuid=True))
    event_type: Mapped[str] = mapped_column(String, nullable=False)
    payload: Mapped[dict] = mapped_column(JSON, nullable=False)
    sent: Mapped[bool] = mapped_column(Boolean, default=False)
    created_at: Mapped[datetime] = mapped_column(DateTime, default=datetime.utcnow)
