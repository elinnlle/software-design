from abc import ABC, abstractmethod
from uuid import UUID

from file_storage_service.domain.entites.file_content import FileContent


class FileStorageABC(ABC):

    @abstractmethod
    async def save(self, file: FileContent) -> str:
        pass

    @abstractmethod
    async def get(self, id: UUID) -> FileContent:
        pass
