from file_storage_service.domain.entites.entity import Entity
from file_storage_service.domain.entites.file_content import FileContent
from file_storage_service.domain.entites.file_metadata import FileMetadata


class File(Entity):
    def __init__(self, metadata: FileMetadata, content: FileContent):
        if content.id != metadata.id:
            raise ValueError(f"{content.id=} != {metadata.id=}")
        super().__init__(metadata.id)
        self.content = content
        self.metadata = metadata
