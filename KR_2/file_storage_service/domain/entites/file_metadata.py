from uuid import UUID
from typing import Optional

from file_storage_service.domain.entites.entity import Entity


class FileMetadata(Entity):
    def __init__(self,
                 id: UUID,
                 name: Optional[str] = None,
                 path: Optional[str] = None,
                 hash_str: Optional[str] = None):
        super().__init__(id)
        self.name = name
        self.path = path
        self.hash = hash_str
