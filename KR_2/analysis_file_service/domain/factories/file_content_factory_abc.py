from abc import ABC
from uuid import UUID

from analysis_file_service.domain.entites.file_content import FileContent
from analysis_file_service.domain.factories.base_factory import BaseFactory


class FileContentFactoryABC(BaseFactory, ABC):

    async def create(self, id: UUID, content : bytes) -> FileContent:
        return FileContent(
            id = id,
            content= content
        )
