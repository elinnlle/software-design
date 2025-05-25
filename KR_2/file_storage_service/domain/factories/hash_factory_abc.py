from abc import ABC, abstractmethod

from file_storage_service.domain.factories.base_factory import BaseFactory


class HashFactoryABC(BaseFactory, ABC):

    @abstractmethod
    async def create(self, file: bytes) -> str:
        pass
