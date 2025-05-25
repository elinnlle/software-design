from abc import ABC
from uuid import UUID


class Entity(ABC):
    def __init__(self, id: UUID):
        self.id = id
