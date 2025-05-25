import os

SRV_LOG_LEVEL = os.getenv("LOGGER_LEVEL", "INFO")

POSTGRES_HOST = os.getenv("POSTGRES_HOST", "db")
POSTGRES_PORT = os.getenv("DB_PORT", "5432")
POSTGRES_DB = os.getenv("POSTGRES_DB", "DB")
POSTGRES_USER = os.getenv("POSTGRES_USER", "postrges")
POSTGRES_PASSWORD = os.getenv("POSTGRES_PASSWORD", "postgres")
POSTGRES_URL = os.getenv("POSTGRES_URL")

FILE_STORAGE_SERVICE_URL = os.getenv("FILE_STORAGE_SERVICE_URL", "http://file_storage_service:8000/api/v1/")

SCHEMA = os.getenv("ANALYSIS_SCHEMA", "public")
