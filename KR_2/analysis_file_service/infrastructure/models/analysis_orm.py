from sqlalchemy.orm import Mapped, mapped_column
from sqlalchemy import INTEGER

from analysis_file_service.infrastructure.models.base_orm import BaseORM


class AnalysisDataORM(BaseORM):
    __tablename__ = "data"
    __table_args__ = {"schema": "analysis"}

    count_paragraphs: Mapped[int] = mapped_column(INTEGER)
    count_words: Mapped[int] = mapped_column(INTEGER)
    count_chars: Mapped[int] = mapped_column(INTEGER)
