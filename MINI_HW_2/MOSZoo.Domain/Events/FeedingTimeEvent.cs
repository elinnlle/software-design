using MOSZoo.Domain.Common;

namespace MOSZoo.Domain.Events;

/// <summary>
/// Сигнал о том, что кормление завершено.
/// </summary>
public record FeedingTimeEvent(
    Guid FeedingId,
    Guid AnimalId,
    DateTimeOffset FeedingTime) : IDomainEvent;