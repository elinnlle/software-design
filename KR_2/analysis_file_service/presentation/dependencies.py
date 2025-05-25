from typing import AsyncIterator
import aiohttp
from fastapi import Depends
from sqlalchemy.ext.asyncio import AsyncSession

from analysis_file_service.application.analyse_use_case import AnalyseUseCase
from analysis_file_service.domain.factories.analysis_data_factory_abc import AnalysisFactory
from analysis_file_service.domain.repositories.analysis_data_repository_abc import AnalysisDataRepositoryABC
from analysis_file_service.domain.services.analysis_service import AnalysisService
from analysis_file_service.infrastructure.config.config import session_async
from analysis_file_service.infrastructure.repositories.analysis_data_repository import AnalysisDataRepository
from analysis_file_service.infrastructure.services.file_service_client import FileServiceClient


async def get_async_db_session() -> AsyncIterator[AsyncSession]:
    async with session_async() as session:
        try:
            yield session
            await session.commit()
        except Exception:
            await session.rollback()
            raise
        finally:
            await session.close()


async def get_async_client_session() -> aiohttp.ClientSession:
    return aiohttp.ClientSession()


async def get_analysis_data_repository(session: AsyncSession = Depends(get_async_db_session)) -> AnalysisDataRepositoryABC:
    return AnalysisDataRepository(session)


async def get_analysis_data_factory() -> AnalysisFactory:
    return AnalysisFactory()


async def get_analysis_service(analysis_data_repository: AnalysisDataRepository = Depends(get_analysis_data_repository),
                               analysis_factory: AnalysisFactory = Depends(get_analysis_data_factory)) -> AnalysisService:
    return AnalysisService(repository=analysis_data_repository,
                           analysis_factory=analysis_factory)


async def get_file_service_client(session: aiohttp.ClientSession = Depends(get_async_client_session)) -> FileServiceClient:
    return FileServiceClient(session)


async def get_analyse_use_case(analysis_service: AnalysisService = Depends(get_analysis_service),
                               file_service_client: FileServiceClient = Depends(get_file_service_client)) -> AnalyseUseCase:
    return AnalyseUseCase(analysis_service=analysis_service,
                          file_service_client=file_service_client)
