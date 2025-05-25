from abc import ABC, abstractmethod
from uuid import UUID
from typing import Optional

from file_storage_service.domain.entites.entity import Entity


class BaseRepository(ABC):

    @abstractmethod
    async def insert(self, obj: Entity):
        pass

    @abstractmethod
    async def delete(self, id: UUID):
        pass

    @abstractmethod
    async def get(self, id: UUID) -> Optional[Entity]:
        pass
