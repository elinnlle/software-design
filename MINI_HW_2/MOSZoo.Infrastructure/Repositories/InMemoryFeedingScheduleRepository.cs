using MOSZoo.Domain.Entities;
using MOSZoo.Domain.Interfaces;

namespace MOSZoo.Infrastructure.Repositories;

public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly Dictionary<Guid, FeedingSchedule> _store = new();

    public Task AddAsync(FeedingSchedule s)
    {
        _store[s.Id] = s;
        return Task.CompletedTask;
    }

    public Task<FeedingSchedule?> GetAsync(Guid id) =>
        Task.FromResult(_store.TryGetValue(id, out var s) ? s : null);

    public Task UpdateAsync(FeedingSchedule s)
    {
        _store[s.Id] = s;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<FeedingSchedule>> GetAllAsync() =>
        Task.FromResult<IReadOnlyList<FeedingSchedule>>(_store.Values.ToList());
}