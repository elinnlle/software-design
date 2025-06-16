from pydantic import BaseModel, Field

class TopUp(BaseModel):
    amount: int = Field(..., gt=0)
