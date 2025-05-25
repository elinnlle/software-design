from uuid import UUID
from abc import ABC, abstractmethod

from ..entites.file_content import FileContent

class FileServiceClientInterface(ABC):

    @abstractmethod
    async def get_file_content(self, id : UUID) -> FileContent:
        pass
