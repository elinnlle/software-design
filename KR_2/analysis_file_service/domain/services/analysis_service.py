import re
from uuid import UUID

from analysis_file_service.domain.entites.analysis_data import AnalysisData
from analysis_file_service.domain.entites.file_content import FileContent
from analysis_file_service.domain.factories.analysis_data_factory_abc import AnalysisFactory
from analysis_file_service.domain.repositories.analysis_data_repository_abc import AnalysisDataRepositoryABC


class AnalysisService:

    def __init__(self,
                 repository: AnalysisDataRepositoryABC,
                 analysis_factory: AnalysisFactory):
        self._repository = repository
        self._analysis_factory = analysis_factory

    async def insert(self, data: AnalysisData):
        await self._repository.insert(data)

    async def get(self, id: UUID) -> AnalysisData:
        return await self._repository.get(id)

    async def check_exist(self, id: UUID) -> bool:
        return await self._repository.check_exist(id)

    def _decode(self, data: bytes) -> str:
        return data.decode('untf-8')

    def _count_paragraphs(self, string: str) -> int:
        return len(re.split(r'\n\s*\n', string))

    def _count_words(self, string: str) -> int:
        return len(re.findall(r'\b\w+\b', string))

    def _count_chars(self, string: str) -> int:
        return len(string)

    async def analyse(self, file: FileContent) -> AnalysisData:
        content_string = str(file.content)
        return self._analysis_factory.create(id=file.id,
                                             count_paragraphs=self._count_paragraphs(content_string),
                                             count_words=self._count_words(content_string),
                                             count_chars=self._count_chars(content_string))
