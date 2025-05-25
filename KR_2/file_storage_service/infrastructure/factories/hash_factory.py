import hashlib

from file_storage_service.domain.factories.hash_factory_abc import HashFactoryABC


class HashFactory(HashFactoryABC):
    def __init__(self):
        self.algorithm = "sha256"

    async def create(self, file: bytes) -> str:
        hasher = self._get_hasher()
        hasher.update(file)
        return hasher.hexdigest()

    def _get_hasher(self):
        return hashlib.new(self.algorithm)
