from abc import ABC, abstractmethod
from uuid import UUID
from typing import Optional

from file_storage_service.domain.entites.file_metadata import FileMetadata
from file_storage_service.domain.repositories.base_repository_abc import BaseRepository


class FileRepositoryABC(BaseRepository, ABC):

    @abstractmethod
    async def insert(self, obj: FileMetadata):
        pass

    @abstractmethod
    async def delete(self, id: UUID):
        pass

    @abstractmethod
    async def get(self, id: UUID) -> Optional[FileMetadata]:
        pass

    @abstractmethod
    async def check_exist_by_hash(self, hash_str: str) -> bool:
        pass

    @abstractmethod
    async def get_by_hash(self, hash_str: str) -> UUID:
        pass
