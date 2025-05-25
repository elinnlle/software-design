from sqlalchemy.orm import Mapped, mapped_column
from sqlalchemy import String

from file_storage_service.infrastructure.models.base_orm import BaseORM


class FileORM(BaseORM):
    __tablename__ = "files"
    __table_args__ = {"schema": "file_storage"}

    name: Mapped[str] = mapped_column(String(255))
    path: Mapped[str] = mapped_column(String(2000))
    hash: Mapped[str] = mapped_column(String(64))
