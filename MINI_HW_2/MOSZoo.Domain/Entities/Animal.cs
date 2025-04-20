using MOSZoo.Domain.Common;
using MOSZoo.Domain.Enums;
using MOSZoo.Domain.Events;

namespace MOSZoo.Domain.Entities;

/// <summary>
/// Животное — агрегат.
/// </summary>
public class Animal : Entity
{
    public Species Species { get; private set; }
    public string  Name    { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Gender  Gender  { get; private set; }
    public string  FavouriteFood { get; private set; }
    public bool    IsHealthy     { get; private set; } = true;

    // FK
    public Guid EnclosureId { get; private set; }

    // навигация
    public Enclosure? Enclosure { get; set; }

    public Animal(Species species, string name, DateTime dob, Gender gender, string favouriteFood)
    {
        Species       = species;
        Name          = name;
        DateOfBirth   = dob;
        Gender        = gender;
        FavouriteFood = favouriteFood;
    }

    public void Feed(string food) { }

    public void Treat() => IsHealthy = true;

    /// <summary>
    /// Переместить животное, вернув доменное событие.
    /// </summary>
    public AnimalMovedEvent MoveTo(Guid targetEnclosureId)
    {
        var @event = new AnimalMovedEvent(Id, EnclosureId, targetEnclosureId, DateTimeOffset.UtcNow);
        EnclosureId = targetEnclosureId;
        return @event;
    }
}