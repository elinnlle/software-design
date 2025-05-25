from uuid import UUID
import aiohttp
from typing import Optional

from analysis_file_service.common.env.globals import FILE_STORAGE_SERVICE_URL
from analysis_file_service.domain.entites.file_content import FileContent
from analysis_file_service.domain.factories.file_content_factory_abc import FileContentFactoryABC
from analysis_file_service.domain.services.file_service_client_interface import FileServiceClientInterface


class FileServiceClient(FileServiceClientInterface):

    def __init__(self, session: aiohttp.ClientSession):
        self.session = session
        self.factory = FileContentFactoryABC()

    async def get_file_content(self, id: UUID) -> Optional[FileContent]:
        async with self.session.get(f"{FILE_STORAGE_SERVICE_URL}files/",
                                    params={"id": str(id)}) as response:
            if response.status == 404:
                return None
            response_json = await response.json()
            return await self.factory.create(id=response_json["id"],
                                             content=response_json["content"])
