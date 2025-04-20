using MOSZoo.Domain.Entities;

namespace MOSZoo.Domain.Interfaces;

public interface IFeedingScheduleRepository
{
    Task AddAsync(FeedingSchedule schedule);
    Task<FeedingSchedule?> GetAsync(Guid id);
    Task UpdateAsync(FeedingSchedule schedule);
    Task<IReadOnlyList<FeedingSchedule>> GetAllAsync();
}