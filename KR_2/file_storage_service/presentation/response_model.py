from uuid import UUID
from pydantic import BaseModel


class FileLoadResponse(BaseModel):
    id: UUID


class FileRequest(BaseModel):
    id: UUID


class FileContentResponse(BaseModel):
    id: UUID
    content: bytes
