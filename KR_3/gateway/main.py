import os
import httpx
from fastapi import FastAPI, Request, HTTPException, Response
from fastapi.middleware.cors import CORSMiddleware

ORDERS_URL = os.getenv("ORDERS_URL", "http://orders_service:8001")
PAYMENTS_URL = os.getenv("PAYMENTS_URL", "http://payments_service:8002")

app = FastAPI(title="API Gateway")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["http://localhost:8080"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

async def proxy(request: Request, base_url: str, path: str):
    url = f"{base_url}{path}"

    body = None
    if request.method in ("POST", "PUT", "PATCH"):
        try:
            body = await request.json()
        except Exception:
            body = None

    async with httpx.AsyncClient() as client:
        try:
            resp = await client.request(
                request.method,
                url,
                params=request.query_params,
                json=body,
                timeout=30.0,
            )
        except httpx.RequestError as exc:
            raise HTTPException(status_code=502, detail=f"Upstream error: {exc}") from exc

    excluded = {
        "connection", "keep-alive", "proxy-authenticate", "proxy-authorization",
        "te", "trailer", "transfer-encoding", "upgrade"
    }
    headers = {k: v for k, v in resp.headers.items() if k.lower() not in excluded}

    return Response(
        content=resp.content,
        status_code=resp.status_code,
        headers=headers,
        media_type=resp.headers.get("content-type"),
    )

@app.api_route("/orders{path:path}", methods=["GET", "POST", "PUT", "PATCH", "DELETE"])
async def orders_proxy(path: str, request: Request):
    return await proxy(request, ORDERS_URL, path)

@app.api_route("/payments{path:path}", methods=["GET", "POST", "PUT", "PATCH", "DELETE"])
async def payments_proxy(path: str, request: Request):
    return await proxy(request, PAYMENTS_URL, path)

@app.get("/")
def read_root():
    return {"message": "Gateway is live. Use /orders and /payments endpoints."}
