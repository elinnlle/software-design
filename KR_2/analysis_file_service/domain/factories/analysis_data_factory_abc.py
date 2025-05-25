from uuid import UUID

from analysis_file_service.domain.entites.analysis_data import AnalysisData
from analysis_file_service.domain.factories.base_factory import BaseFactory


class AnalysisFactory(BaseFactory):

    @staticmethod
    def create(id: UUID,
               count_paragraphs: int,
               count_words: int,
               count_chars: int) -> AnalysisData:
        return AnalysisData(id=id,
                            count_paragraphs=count_paragraphs,
                            count_words=count_words,
                            count_chars=count_chars)
