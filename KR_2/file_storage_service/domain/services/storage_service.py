from typing import Optional
from uuid import UUID

from file_storage_service.domain.entites.file_content import FileContent
from file_storage_service.domain.entites.file_metadata import FileMetadata
from file_storage_service.domain.repositories.file_repository_abc import FileRepositoryABC
from file_storage_service.domain.repositories.file_storage_abc import FileStorageABC


class FileStorageService:

    def __init__(self, file_repository: FileRepositoryABC, file_storage: FileStorageABC):
        self.repository = file_repository
        self.storage = file_storage

    async def insert(self, file_metadata: FileMetadata):
        await self.repository.insert(file_metadata)

    async def save(self, file_content: FileContent) -> str:
        return await self.storage.save(file_content)

    async def delete(self, id: UUID):
        await self.repository.delete(id)

    async def get_metadata(self, id: UUID) -> Optional[FileMetadata]:
        return await self.repository.get(id)

    async def get_content(self, id: UUID) -> Optional[FileContent]:
        try:
            return await self.storage.get(id)
        except KeyError:
            return None

    async def check_exist_by_hash(self, hash_str: str) -> bool:
        return await self.repository.check_exist_by_hash(hash_str)

    async def get_by_hash(self, hash_str: str) -> UUID:
        return await self.repository.get_by_hash(hash_str)
