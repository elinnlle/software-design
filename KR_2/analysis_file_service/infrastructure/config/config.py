from sqlalchemy.ext.asyncio import create_async_engine, async_sessionmaker

from analysis_file_service.common.env.globals import POSTGRES_URL

async_engine = create_async_engine(
    url=POSTGRES_URL,
    echo=False,
    pool_size=5,
    max_overflow=10
)

session_async = async_sessionmaker(
    bind=async_engine,
)
