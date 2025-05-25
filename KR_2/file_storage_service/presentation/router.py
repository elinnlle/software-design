from uuid import UUID

from fastapi import APIRouter, UploadFile, Depends, Query, HTTPException

from file_storage_service.application.file_create_dto import FileCreateDTO
from file_storage_service.application.file_use_case import FileUseCase
from file_storage_service.presentation.dependencies import get_file_use_case
from file_storage_service.presentation.response_model import FileLoadResponse, FileContentResponse

file_router = APIRouter(prefix='/files')


@file_router.post("/load",
                  summary="Загрузка текстовых файлов",
                  response_description="Информация о загруженном файле")
async def upload_text_file(file: UploadFile,
                           file_use_case: FileUseCase = Depends(get_file_use_case)) -> FileLoadResponse:
    content = await file.read()
    dto = FileCreateDTO(name=file.filename if file.filename else str(),
                        content=content)
    file_id = await file_use_case.add_file(dto)
    return FileLoadResponse(id=file_id)


@file_router.get(f"/")
async def get_file(id: UUID = Query(),
                   file_use_case: FileUseCase = Depends(get_file_use_case)) -> FileContentResponse:
    file = await file_use_case.get_file(id)
    if not file:
        raise HTTPException(404, "File not found")
    return FileContentResponse(id=file.id,
                               content=file.content.content)
