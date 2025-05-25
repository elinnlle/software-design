from typing import AsyncIterator
from fastapi import Depends
from sqlalchemy.ext.asyncio import AsyncSession

from file_storage_service.application.file_use_case import FileUseCase
from file_storage_service.common.env.globals import MINIO_BUCKET_NAME
from file_storage_service.domain.services.hash_service import HashService
from file_storage_service.domain.services.storage_service import FileStorageService
from file_storage_service.infrastructure.config.config import session_async, minio_client
from file_storage_service.infrastructure.factories.hash_factory import HashFactory
from file_storage_service.infrastructure.repositories.file_repository import FileRepository
from file_storage_service.infrastructure.repositories.file_storage import FileStorage


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


async def get_file_repository(session: AsyncSession = Depends(get_async_db_session)) -> FileRepository:
    return FileRepository(session)


async def get_hash_service() -> HashService:
    hash_factory = HashFactory()
    return HashService(hash_factory)


async def get_file_storage() -> FileStorage:
    if not minio_client.bucket_exists(MINIO_BUCKET_NAME):
        minio_client.make_bucket(MINIO_BUCKET_NAME)
    return FileStorage(minio_client, MINIO_BUCKET_NAME)


async def get_file_service(file_repository: FileRepository = Depends(get_file_repository),
                           file_storage: FileStorage = Depends(get_file_storage)) -> FileStorageService:
    return FileStorageService(file_repository, file_storage)


async def get_file_use_case(file_service: FileStorageService = Depends(get_file_service),
                            hash_service: HashService = Depends(get_hash_service)) -> FileUseCase:
    return FileUseCase(file_service, hash_service)
