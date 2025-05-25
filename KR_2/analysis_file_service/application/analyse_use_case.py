from uuid import UUID
from typing import Optional

from analysis_file_service.domain.entites.analysis_data import AnalysisData
from analysis_file_service.domain.services.analysis_service import AnalysisService
from analysis_file_service.infrastructure.services.file_service_client import FileServiceClient


class AnalyseUseCase:
    def __init__(self, analysis_service: AnalysisService,
                 file_service_client: FileServiceClient):
        self.analysis_service = analysis_service
        self.file_service_client = file_service_client

    async def get_statistic(self, id: UUID) -> Optional[AnalysisData]:
        if await self.analysis_service.check_exist(id):
            return await self.analysis_service.get(id)
        file_content = await self.file_service_client.get_file_content(id)
        if not file_content:
            return None
        data_analyst = await self.analysis_service.analyse(file_content)
        await self.analysis_service.insert(data_analyst)
        return data_analyst
