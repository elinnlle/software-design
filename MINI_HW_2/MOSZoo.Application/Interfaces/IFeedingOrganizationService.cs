using MOSZoo.Domain.Entities;

namespace MOSZoo.Application.Interfaces;

public interface IFeedingOrganizationService
{
    Task<FeedingSchedule> AddScheduleAsync(Guid animalId, DateTimeOffset time, string foodType);
    Task MarkDoneAsync(Guid feedingId);
}