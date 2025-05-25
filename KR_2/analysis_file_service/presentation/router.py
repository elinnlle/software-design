from fastapi import APIRouter, Depends, HTTPException, Query
from uuid import UUID

from analysis_file_service.application.analyse_use_case import AnalyseUseCase
from analysis_file_service.presentation.dependencies import get_analyse_use_case
from analysis_file_service.presentation.response_model import AnalysisDataResponse

analysis_router = APIRouter(prefix='/analyse')


@analysis_router.get(f"/")
async def get_file(id: UUID = Query(),
                   analysis_use_case: AnalyseUseCase = Depends(get_analyse_use_case)) -> AnalysisDataResponse:
    analysis_data = await analysis_use_case.get_statistic(id)
    if not analysis_data:
        raise HTTPException(404, 'File not found')
    return AnalysisDataResponse(id=analysis_data.id,
                                count_paragraphs=analysis_data.count_paragraphs,
                                count_words=analysis_data.count_words,
                                count_chars=analysis_data.count_chars)
