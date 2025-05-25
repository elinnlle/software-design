from uuid import UUID, uuid4
from typing import Optional

from file_storage_service.application.file_create_dto import FileCreateDTO
from file_storage_service.domain.entites.file import File
from file_storage_service.domain.factories.file_content_factory_abc import FileContentFactoryABC
from file_storage_service.domain.factories.file_metadata_factory_abc import FileMetadataFactoryABC
from file_storage_service.domain.services.storage_service import FileStorageService
from file_storage_service.domain.services.hash_service import HashService


class FileUseCase:
    def __init__(self,
                 file_storage_service: FileStorageService,
                 hash_service: HashService):
        self.file_storage_service = file_storage_service
        self.hash_service = hash_service
        self.file_metadata_factory = FileMetadataFactoryABC()
        self.file_content_factory = FileContentFactoryABC()

    async def add_file(self, dto: FileCreateDTO) -> UUID:
        hash_str = await self.hash_service.get_hash(dto.content)
        if await self.file_storage_service.check_exist_by_hash(hash_str):
            return await self.file_storage_service.get_by_hash(hash_str)
        id = uuid4()
        file_content = await self.file_content_factory.create(id=id,
                                                              content=dto.content)
        path = await self.file_storage_service.save(file_content)
        file_metadata = await self.file_metadata_factory.create(id=id,
                                                                name=dto.name,
                                                                path=path,
                                                                hash_str=hash_str)
        await self.file_storage_service.insert(file_metadata)
        return id

    async def get_file(self, id: UUID) -> Optional[File]:
        file_metadata = await self.file_storage_service.get_metadata(id)
        file_content = await self.file_storage_service.get_content(id)
        if not file_metadata or not file_content:
            return None
        return File(metadata=file_metadata,
                    content=file_content)
