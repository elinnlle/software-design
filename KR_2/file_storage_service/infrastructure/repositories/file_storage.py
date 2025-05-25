from minio import Minio, S3Error
from uuid import UUID
import io

from file_storage_service.domain.entites.file_content import FileContent
from file_storage_service.domain.repositories.file_storage_abc import FileStorageABC


class FileStorage(FileStorageABC):
    def __init__(self, client: Minio, bucket_name: str):
        self.client = client
        self.bucket_name = bucket_name

    async def save(self, file: FileContent) -> str:
        try:
            file_stream = io.BytesIO(file.content)
            self.client.put_object(
                self.bucket_name,
                str(file.id) + ".txt",
                data=file_stream,
                length=len(file.content),
                content_type="text/plain"
            )
            return f"{self.bucket_name}/{str(file.id)}.txt"
        except S3Error as exc:
            raise RuntimeError(f"Failed to save file {id}: {exc}") from exc
        except Exception as exc:
            raise RuntimeError(f"Unexpected error saving file {id}: {exc}") from exc

    async def get(self, id: UUID) -> FileContent:
        try:
            response = self.client.get_object(
                bucket_name=self.bucket_name,
                object_name=str(id) + ".txt"
            )
            try:
                file_data = response.read()
                return FileContent(
                    id=id,
                    content=file_data
                )
            finally:
                response.close()
                response.release_conn()
        except Exception as exc:
            if 'Connection aborted' in str(exc):
                raise RuntimeError("Storage connection error") from exc
            raise KeyError(f"Failed to get file {id}: {exc}") from exc
