# Отчет по проекту "Анализ текстовых файлов"

**Анализ текстовых файлов** — это сервис, предназначенный для обработки загруженных пользователем текстовых файлов. Сервис предоставляет следующие основные возможности:

- **Загрузка и хранение файлов**: уникальные файлы сохраняются в хранилище с метаданными.
- **Анализ содержимого файла**: подсчёт количества абзацев, слов и символов.

Кроме того, проект включает:
- FastAPI — для построения REST API.
- Nginx как API-шлюз, проксирующий запросы к сервисам.
- Два PostgreSQL-контейнера для метаданных и результатов анализа.
- MinIO — S3-хранилище для файлов.
- Docker / Docker Compose — для оркестрации всей инфраструктуры.
- aiohttp — для асинхронного HTTP-взаимодействия между сервисами.

---

## Описание решения

- **Загрузка и хранение файлов**:
    - Приём текстового файла через HTTP POST /api/v1/files/load.
    - Вычисление SHA-256-хэша и дедупликация: если такой файл уже есть, возвращается существующий UUID.
    - Сохранение содержимого в Minio и метаданных о файлах (ID, имя, путь, хэш) в отдельной PostgreSQL-базе filestorage.

- **Получение содержимого файла**:
    - HTTP GET /api/v1/files?id=<UUID> возвращает байты файла по идентификатору.

- **Анализ содержимого файла**:
    - Анализ проводится только при первом запросе статистики для файла — последующие запросы получают сохранённый объект AnalysisData.
    - Результаты анализа (количество абзацев, слов, символов) сохраняются в базе analysis.
    - Поддерживается повторное получение статистики без пересчёта.

- **Интеграция с внешними системами**:
    - Используется MinIO как объектное хранилище для бинарных данных.
    - Данные о файлах и результатах анализа хранятся в PostgreSQL в разных схемах и контейнерах.
    - Взаимодействие между сервисами реализовано через HTTP-запросы.
   
---

## Архитектура микросервисов

- **file_storage_service**
   - Presentation: FastAPI + маршруты `POST /api/v1/files/load` и `GET /api/v1/files` для загрузки и чтения файлов.
   - Use Case: `FileUseCase` содержит логику дедупликации и создания DTO (`FileCreateDTO`).
   - Domain:
     - Сущности `FileMetadata` и `FileContent`.
     - Сервисы: `HashService` (через `HashFactory`), `FileStorageService` (репозиторий + Minio-адаптер).
   - Infrastructure:
     - SQLAlchemy Async для таблицы `file_storage.files`.
     - Minio SDK для сохранения/чтения объектов.

- **file_analysis_service**
   - Presentation: FastAPI + маршрут `GET /api/v1/analyse` для запуска анализа по UUID.
   - Use Case: `AnalyseUseCase` объединяет проверку в `AnalysisService`, вызов `FileServiceClient` и сохранение результата.
   - Domain:
     - Сущность `AnalysisData` с полями `count_paragraphs`, `count_words`, `count_chars`.
     - Сервис `AnalysisService` реализует логику декодирования, подсчёта и фабрику `AnalysisFactory`.
   - Infrastructure:
     - SQLAlchemy Async для таблицы `analysis.data`.
     - HTTP-клиент AioHTTP для общения с file_storage_service.

- **API-шлюз (Nginx)**
   - Проксирует входящий трафик на порт 8080:
     - `/files/...` → file_storage_service (порт 8080).
     - `/analyse/...` → file_analysis_service (порт 8001).
   - Делает rewrite путей, чтобы клиент видел единый API на `http://<host>:8080`.

---

## Использованные паттерны и принципы

### SOLID и GRASP
- **Single Responsibility Principle (SRP)**: Каждый класс отвечает за свою конкретную задачу (например, хранение файлов, вычисление хэшей, взаимодействие с БД).
- **Open/Closed Principle (OCP)**: Возможность расширения функционала (например, добавления новых форматов вывода или способов хранения) без изменения существующего кода.
- **Dependency Inversion Principle (DIP)**: Высокоуровневые модули зависят от абстракций, а не от конкретных реализаций (например, AnalyseUseCase зависит от интерфейсов `AnalysisService` и `FileServiceClient`).
- **High Cohesion и Low Coupling (GRASP)**: Сервисы и адаптеры разделены по ответственности — анализ не знает о Minio, а file_storage не знает о логике анализа.

### Паттерны GoF
- **Repository**: `FileRepository` и `AnalysisDataRepository` инкапсулируют доступ к БД.
- **Factory**: `HashFactory` создаёт хэши файлов. `AnalysisFactory` и `FileContentFactoryABC` строят сущности доменной модели.
- **Adapter**: `FileServiceClient` оборачивает HTTP-запросы к `file_storage_service` в интерфейс, понятный domain-слою анализа.
- **Facade / Use Case**: `FileUseCase` и `AnalyseUseCase` объединяют вызовы сервисов и репозиториев в единые операции.

---

## Инструкция по запуску

1. **Клонирование репозитория:**

   ```bash
   git clone <https://github.com/elinnlle/software-design.git>
   cd KR_2
   ```

2. **Собрать и запустить всё через Docker Compose:**  
   ```bash
   docker-compose up --build
   ```
   
3. **Доступ к сервисам:**  
   - Загрузка файла:
     POST http://localhost:8080/files/load (form-data, поле file).
   - Получение файла:
     GET http://localhost:8080/files?id=<UUID>
   - Анализ файла:
     GET http://localhost:8001/analyse?id=<UUID>

4. **Базы данных:**  
   - metadata_db (schema file_storage) на порту 5433.
   - analysis_data_db (schema analysis) на порту 5434.
   - Minio доступен на http://localhost:9000 (миньо-консоль на :9001, логин/пароль minioadmin).
