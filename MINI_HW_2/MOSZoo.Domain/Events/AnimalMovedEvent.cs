using MOSZoo.Domain.Common;

namespace MOSZoo.Domain.Events;

/// <summary>
/// Срабатывает, когда животное перемещается между вольерами.
/// </summary>
public record AnimalMovedEvent(
    Guid AnimalId,
    Guid FromEnclosureId,
    Guid ToEnclosureId,
    DateTimeOffset MovedAt) : IDomainEvent;