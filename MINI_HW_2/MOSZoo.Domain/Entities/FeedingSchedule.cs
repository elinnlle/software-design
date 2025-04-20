using MOSZoo.Domain.Common;

namespace MOSZoo.Domain.Entities;

/// <summary>
/// Запись расписания кормления.
/// </summary>
public class FeedingSchedule : Entity
{
    public Guid AnimalId        { get; private set; }
    public DateTimeOffset FeedingTime { get; private set; }
    public string FoodType      { get; private set; }
    public bool   Done          { get; private set; }

    public FeedingSchedule(Guid animalId, DateTimeOffset time, string foodType)
    {
        AnimalId    = animalId;
        FeedingTime = time;
        FoodType    = foodType;
    }

    public void Reschedule(DateTimeOffset newTime) => FeedingTime = newTime;
    public void MarkDone() => Done = true;
}