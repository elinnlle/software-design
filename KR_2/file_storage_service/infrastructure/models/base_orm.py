from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column
from sqlalchemy.dialects.postgresql import UUID as UUIDps
from uuid import UUID
from sqlalchemy import MetaData


class BaseORM(DeclarativeBase):
    metadata = MetaData()
    __table_args__ = {'info': {'is_async': True}}

    id: Mapped[UUID] = mapped_column(UUIDps(as_uuid=True),
                                     primary_key=True)
