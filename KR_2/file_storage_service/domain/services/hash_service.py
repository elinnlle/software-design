from file_storage_service.domain.factories.hash_factory_abc import HashFactoryABC


class HashService:
    def __init__(self, hash_factory: HashFactoryABC):
        self._hash_factory = hash_factory

    async def get_hash(self, file: bytes) -> str:
        return await self._hash_factory.create(file)
