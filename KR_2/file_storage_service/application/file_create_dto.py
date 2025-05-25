class FileCreateDTO:
    def __init__(self,
                 name: str,
                 content: bytes):
        self.name = name
        self.content = content
