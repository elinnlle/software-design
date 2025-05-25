import logging
import sys
from pathlib import Path

from fastapi import FastAPI

DIR = Path(__file__).absolute().parent.parent
sys.path.append(str(DIR))

from file_storage_service.presentation.router import file_router

app = FastAPI(title="File Processing Service")

app.include_router(file_router, prefix='/api/v1')
