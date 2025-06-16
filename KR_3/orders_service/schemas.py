import uuid
from pydantic import BaseModel, Field

class OrderCreate(BaseModel):
    amount: int = Field(..., gt=0)

class OrderOut(BaseModel):
    id: uuid.UUID
    amount: int
    status: str
