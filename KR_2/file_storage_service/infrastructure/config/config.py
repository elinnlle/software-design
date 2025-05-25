from sqlalchemy.ext.asyncio import create_async_engine, async_sessionmaker
from minio import Minio

from file_storage_service.common.env.globals import POSTGRES_URL, MINIO_ROOT_USER, MINIO_ROOT_PASSWORD

async_engine = create_async_engine(
    url=POSTGRES_URL,
    echo=False,
    pool_size=5,
    max_overflow=10
)

session_async = async_sessionmaker(
    bind=async_engine,
)

minio_client = Minio(
    "minio:9000",
    access_key=MINIO_ROOT_USER,
    secret_key=MINIO_ROOT_PASSWORD,
    secure=False
)
