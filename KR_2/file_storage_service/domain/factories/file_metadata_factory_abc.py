from abc import ABC
from uuid import UUID

from file_storage_service.domain.entites.file_metadata import FileMetadata
from file_storage_service.domain.factories.base_factory import BaseFactory


class FileMetadataFactoryABC(BaseFactory, ABC):

    async def create(self,
                     id: UUID,
                     name: str,
                     path: str,
                     hash_str: str) -> FileMetadata:
        return FileMetadata(id=id,
                            name=name,
                            path=path,
                            hash_str=hash_str)
