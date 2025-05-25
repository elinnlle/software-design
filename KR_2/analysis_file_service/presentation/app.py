import logging
import sys
from pathlib import Path

from fastapi import FastAPI

DIR = Path(__file__).absolute().parent.parent
sys.path.append(str(DIR))

from analysis_file_service.presentation.router import analysis_router

app = FastAPI(title="File Processing Service")

app.include_router(analysis_router, prefix='/api/v1')
