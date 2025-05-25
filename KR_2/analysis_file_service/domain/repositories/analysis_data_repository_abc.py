from abc import ABC, abstractmethod
from uuid import UUID

from analysis_file_service.domain.entites.analysis_data import AnalysisData


class AnalysisDataRepositoryABC(ABC):

    @abstractmethod
    async def check_exist(self, id: UUID) -> bool:
        pass

    @abstractmethod
    async def insert(self, obj: AnalysisData):
        pass

    @abstractmethod
    async def get(self, id: UUID) -> AnalysisData:
        pass
