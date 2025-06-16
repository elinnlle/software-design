from typing import Dict, List
from fastapi import WebSocket, WebSocketDisconnect

class ConnectionManager:
    def __init__(self):
        self.active_orders: Dict[str, List[WebSocket]] = {}

    async def connect(self, order_id: str, websocket: WebSocket):
        await websocket.accept()
        self.active_orders.setdefault(order_id, []).append(websocket)

    def _cleanup(self, order_id: str, websocket: WebSocket):
        self.active_orders.get(order_id, []).remove(websocket)
        if not self.active_orders[order_id]:
            del self.active_orders[order_id]

    async def disconnect(self, order_id: str, websocket: WebSocket):
        self._cleanup(order_id, websocket)

    async def send_update(self, order_id: str, message: dict):
        if order_id not in self.active_orders:
            return
        living = []
        for ws in self.active_orders[order_id]:
            try:
                await ws.send_json(message)
                living.append(ws)
            except Exception:
                pass
        self.active_orders[order_id] = living
