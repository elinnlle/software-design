from file_storage_service.domain.entites.file_metadata import FileMetadata
from file_storage_service.infrastructure.models.file_orm import FileORM


async def to_orm(file_entity: FileMetadata) -> FileORM:
    return FileORM(id=file_entity.id,
                   name=file_entity.name,
                   path=file_entity.path,
                   hash=file_entity.hash)


async def to_entity(file_orm: FileORM) -> FileMetadata:
    return FileMetadata(id=file_orm.id,
                        name=file_orm.name,
                        path=file_orm.path,
                        hash_str=file_orm.hash)
