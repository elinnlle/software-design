# Отчёт по проекту «Асинхронное межсервисное взаимодействие»

**Асинхронное межсервисное взаимодействие** — это проект с микросервисной архитектурой, включающий асинхронные взаимодействия, паттерны Outbox/Inbox, WebSocket-уведомления и API Gateway.

---

## Основные возможности

- **API Gateway**  
  - Точка входа: `http://<host>:8000`  
  - Проксирование запросов:  
    - `/orders/*` → Orders Service  
    - `/payments/*` → Payments Service  
  - Поддержка CORS, корректная передача заголовков, query-параметров и JSON-тела

- **Orders Service**  
  - Маршруты:  
    - `POST /orders` — создание заказа  
    - `GET /orders` — список заказов пользователя  
    - `GET /orders/{id}` — детали заказа  
  - WebSocket: `/ws/orders/{id}` для live-обновлений статуса  
  - Async SQLAlchemy + PostgreSQL, Transactional Outbox

- **Payments Service**  
  - Работа с балансом:  
    - `POST /account` — регистрация или возврат баланса  
    - `POST /account/topup` — пополнение счёта  
    - `GET /account/balance` — проверка текущего баланса  
  - Async SQLAlchemy + PostgreSQL, Inbox для дедупликации, Transactional Outbox

- **RabbitMQ**  
  - Поток событий:  
    - Orders → `OrderCreated` → Payments  
    - Payments → `PaymentCompleted` / `PaymentFailed` → Orders

- **Frontend**  
  - HTML/JS + Nginx: `http://<host>:8080`  
  - Форма пополнения баланса, создания заказа и подписка на WebSocket в одном UI  
  - Лог в интерфейсе: вывод действий и обновлений статуса

- **Docker Compose**  
  - Одной командой поднимает все сервисы: rabbitmq, базы, микросервисы, gateway, frontend  
  - Настроены тома и сети

---

## Описание решения

- **API Gateway**: FastAPI + HTTPX  
  - Проксирование HTTP-запросов и заголовков, фильтрация hop-by-hop  
  - Обработка JSON-тела с защитой от пустого контента  
  - CORS для фронтенда

- **Orders Service**:  
  - Модели `orders` и `outbox` в PostgreSQL  
  - При создании заказа: транзакция + запись в outbox  
  - Фоновые задачи на aio-pika: публикация и обработка сообщений  
  - WebSocket-канал для уведомлений

- **Payments Service**:  
  - Модели `accounts`, `transactions`, `inbox`, `outbox`  
  - При получении события `OrderCreated`:  
    - Проверка и дедупликация через `inbox`  
    - Обновление баланса и запись в `transactions` + `outbox`  
  - Публикация результатов платежа и их обработка в Orders Service

- **Frontend**:  
  - `index.html` с формами и скриптом для логирования в `<div id="log">`  
  - Автоматическая подписка на WebSocket после создания заказа  
  - Динамическое обновление UI без консоли

---

## Архитектура микросервисов

- **gateway**  
  - FastAPI-приложение `main.py` с прокси-функцией `proxy(request, base_url, path)`  
  - Middleware CORS

- **orders_service**  
  - FastAPI + SQLAlchemy Async + WebSockets (uvicorn)  
  - Скрипты `publisher.py` и `consumer.py`

- **payments_service**  
  - FastAPI + SQLAlchemy Async + aio-pika для Outbox/Inbox

- **frontend**  
  - Nginx-сервер с копией `index.html`  
  - JS-код для взаимодействия с Gateway

- **infrastructure**  
  - Docker Compose YAML с сервисами:  
    - rabbitmq, orders_db, payments_db  
    - orders_service, payments_service, gateway, frontend

---

## Использованные паттерны и принципы

- **Transactional Outbox/Inbox** — гарантирует надёжную публикацию и обработку событий  
- **CQRS** — разделение команд (write) и запросов (read)  
- **SOLID (SRP, OCP, DIP)** — разделение ответственности и зависимость от абстракций  
- **Asynchronous Architecture** — `async/await` во всех сервисах  
- **Dependency Injection (FastAPI Depends)**

---

## Инструкция по запуску

1. **Клонирование репозитория:**
   ```bash
   git clone <https://github.com/elinnlle/software-design.git>
   cd KR_3
   ```
   
2. **Собрать и запустить всё через Docker Compose:**  
   ```bash
   docker-compose up --build
   ```
   
3. **Доступ к сервисам:**  
   - Swagger Gateway:  
     - http://localhost:8000/orders/docs  
     - http://localhost:8000/payments/docs  
   - Frontend UI: http://localhost:8080  
   - RabbitMQ Management: http://localhost:15672 (логин/пароль: `guest`/`guest`).
   
## Пример проверки Payments Service

1. **Создать аккаунт / получить баланс**
   ```bash
   curl -i -X POST http://localhost:8000/payments/account \
     -H "X-User-ID: 1"
   ```

2. **Пополнить счёт**
   ```bash
   curl -i -X POST http://localhost:8000/payments/account/topup \
     -H "Content-Type: application/json" \
     -H "X-User-ID: 1" \
     -d '{"amount":500}'
   ```

3. **Проверить баланс**
   ```bash
   curl -i http://localhost:8000/payments/account/balance \
     -H "X-User-ID: 1"
   ```
   
## Пример проверки Orders Service

1. **Список заказов**
   ```bash
   curl -i http://localhost:8000/orders/orders \
     -H "X-User-ID: 1"
   ```

2. **Создать новый заказ**
   ```bash
   curl -i -X POST http://localhost:8000/orders/orders \
     -H "Content-Type: application/json" \
     -H "X-User-ID: 1" \
     -d '{"amount":200}'
   ```
