# MOSZoo - Управление зоопарком

**MOSZoo** — это веб‑приложение для управления зоопарком, которое автоматизирует учёт животных, вольеров и расписание кормлений. Приложение предоставляет REST‑API для:

- Добавления и удаления животных.
- Добавления и удаления вольеров.
- Перемещения животных между вольерами.
- Просмотра и управления расписанием кормлений.
- Получения статистики по зоопарку (общее число животных, свободные и занятые вольеры).

---

## Описание решения

### Архитектура и принципы Clean Architecture / DDD

Приложение построено по принципам **Clean Architecture** с выделением слоёв Domain, Application, Infrastructure и Presentation, а также элементами **Domain‑Driven Design** (агрегаты, доменные события, репозитории).

#### Основные компоненты:

1. **Доменная модель (Domain)**  
   Расположена в проекте `MOSZoo.Domain`:
   - **Сущности (Entity)**  
     - `Animal` — агрегат для животного (вид, кличка, дата рождения, пол, любимая еда, статус здоровья, ссылка на вольер).  
     - `Enclosure` — агрегат для вольера (тип, название, вместимость, список животных).  
     - `FeedingSchedule` — запись о кормлении (животное, время, тип еды, статус выполнения).  
   - **Доменные события**  
     - `AnimalMovedEvent` — при перемещении животного.  
     - `FeedingTimeEvent` — при отметке выполнения кормления.  
   - **Общие абстракции**  
     - `Entity` (базовый класс с `Id`) и `IDomainEvent` (маркер).

2. **Интерфейсы (Interfaces)**  
   Расположены в `MOSZoo.Domain.Interfaces` и `MOSZoo.Application.Interfaces`:
   - `IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository` — контракты доступа для репозиториев.  
   - `IAnimalTransferService` — перенос животных между вольерами.  
   - `IFeedingOrganizationService` — организация расписания кормлений.  
   - `IZooStatisticsService` — получение статистики зоопарка.

3. **Сервисы (Application / Services)**  
   Расположены в `MOSZoo.Application.Services`:
   - `AnimalTransferService` — реализует бизнес‑правила перемещения животных, публикует `AnimalMovedEvent`.  
   - `FeedingOrganizationService` — добавление/отметка кормлений, публикация `FeedingTimeEvent`.  
   - `ZooStatisticsService` — вычисление количества животных и свободных вольеров, возвращает DTO `ZooStatisticsDto`.  
   - **DTO**  
     - `ZooStatisticsDto` — переносит сводные данные в Presentation.

4. **Инфраструктура (Infrastructure / Repositories)**  
   Расположена в `MOSZoo.Infrastructure.Repositories`:
   - `InMemoryAnimalRepository`  
   - `InMemoryEnclosureRepository`  
   - `InMemoryFeedingScheduleRepository`  
   Каждая — потокобезопасное in‑memory хранилище для быстрого старта и тестирования.

5. **Presentation (Controllers / Web API)**  
   Расположена в `MOSZoo.Presentation.Controllers`:
   - `AnimalsController` — CRUD животных и endpoint `/move` для перемещения.  
   - `EnclosuresController` — CRUD вольеров.  
   - `FeedingController` — просмотр и управление расписанием кормлений.  
   - `StatisticsController` — получение статистики зоопарка.  
   - Все контроллеры возвращают JSON, используют DI для подключения сервисов и репозиториев.

---

### Dependency Injection (DI)

Регистрация сервисов и репозиториев происходит через расширения:

```csharp
// В Program.cs
builder.Services.AddApplication();      // IAnimalTransferService, IFeedingOrganizationService, IZooStatisticsService
builder.Services.AddInfrastructure();   // InMemory-репозитории для IAnimalRepository, IEnclosureRepository, IFeedingScheduleRepository
builder.Services.AddMediatR(...);       // для публикации доменных событий
```
---

## Инструкция по запуску

1. **Клонирование репозитория:**

   ```bash
   git clone <https://github.com/elinnlle/software-design.git>
   ```

2. **Запуск приложения:**  
Откройте проект в вашей среде разработки и запустите MOSZoo.Presentation

3. **Тестирование через Swagger:**  
Перейдите в браузере по адресу http://localhost:5051/swagger/index.html
Добавьте сущности (Animal, Enclosure, FeedingSchedule).
Выполните операции перемещения, кормления и получите статистику.
