from uuid import UUID
from typing import Optional
from sqlalchemy.ext.asyncio import AsyncSession

from analysis_file_service.domain.entites.analysis_data import AnalysisData
from analysis_file_service.domain.repositories.analysis_data_repository_abc import AnalysisDataRepositoryABC
from analysis_file_service.infrastructure.mappers.analysis_data_mapper import to_orm, to_entity
from analysis_file_service.infrastructure.models.analysis_orm import AnalysisDataORM


class AnalysisDataRepository(AnalysisDataRepositoryABC):
    def __init__(self, session: AsyncSession):
        self._session = session

    async def insert(self, obj: AnalysisData):
        orm_analysis_data = await to_orm(obj)
        self._session.add(orm_analysis_data)
        await self._session.commit()

    async def get(self, id: UUID) -> Optional[AnalysisData]:
        analysis_data_orm = await self._session.get(AnalysisDataORM, id)
        if not analysis_data_orm:
            return None
        return await to_entity(analysis_data_orm)

    async def check_exist(self, id: UUID) -> bool:
        return await self._session.get(AnalysisDataORM, id) is not None
