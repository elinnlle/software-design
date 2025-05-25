from uuid import UUID
from typing import Optional
from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select

from file_storage_service.domain.entites.file_metadata import FileMetadata
from file_storage_service.domain.repositories.file_repository_abc import FileRepositoryABC
from file_storage_service.infrastructure.factories.file_factory import to_orm, to_entity
from file_storage_service.infrastructure.models.file_orm import FileORM


class FileRepository(FileRepositoryABC):

    def __init__(self, session: AsyncSession):
        self._session = session

    async def insert(self, obj: FileMetadata):
        orm_file = await to_orm(obj)
        self._session.add(orm_file)
        await self._session.commit()

    async def delete(self, id: UUID):
        pass

    async def get(self, id: UUID) -> Optional[FileMetadata]:
        file_orm = await self._session.get(FileORM, id)
        if not file_orm:
            return None
        return await to_entity(file_orm)

    async def check_exist_by_hash(self, hash_str: str) -> bool:
        query = select(FileORM).where(FileORM.hash == hash_str).limit(1)
        result = await self._session.execute(query)
        return result.scalar_one_or_none() is not None

    async def get_by_hash(self, hash_str: str) -> Optional[UUID]:
        query = select(FileORM).where(FileORM.hash == hash_str)
        result = await self._session.execute(query)
        orm_file = result.scalars().first()
        if not orm_file:
            return None
        return orm_file.id
