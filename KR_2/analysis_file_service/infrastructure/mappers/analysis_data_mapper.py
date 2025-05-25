from analysis_file_service.domain.entites.analysis_data import AnalysisData
from analysis_file_service.infrastructure.models.analysis_orm import AnalysisDataORM


async def to_orm(data: AnalysisData) -> AnalysisDataORM:
    return AnalysisDataORM(id=data.id,
                           count_paragraphs=data.count_paragraphs,
                           count_words=data.count_words,
                           count_chars=data.count_chars)


async def to_entity(data: AnalysisDataORM) -> AnalysisData:
    return AnalysisData(id=data.id,
                        count_paragraphs=data.count_paragraphs,
                        count_words=data.count_words,
                        count_chars=data.count_chars)
