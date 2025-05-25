from uuid import UUID

from file_storage_service.domain.entites.entity import Entity


class FileContent(Entity):
    def __init__(self, id: UUID, content: bytes):
        super().__init__(id)
        self.content = content
