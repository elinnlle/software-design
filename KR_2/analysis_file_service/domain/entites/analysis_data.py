from uuid import UUID

from analysis_file_service.domain.entites.entity import Entity


class AnalysisData(Entity):

    def __init__(self,
                 id: UUID,
                 count_paragraphs: int,
                 count_words: int,
                 count_chars: int):
        super().__init__(id)
        self.count_paragraphs = count_paragraphs
        self.count_words = count_words
        self.count_chars = count_chars
