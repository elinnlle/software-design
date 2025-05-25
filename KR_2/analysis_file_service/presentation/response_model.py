from uuid import UUID
from pydantic import BaseModel


class FileContentResponse(BaseModel):
    id: UUID
    content: bytes


class FileRequest(BaseModel):
    id: UUID


class AnalysisDataResponse(BaseModel):
    id: UUID
    count_paragraphs: int
    count_words: int
    count_chars: int
