using MOSZoo.Domain.Entities;
using MOSZoo.Domain.Interfaces;

namespace MOSZoo.Infrastructure.Repositories;

public class InMemoryEnclosureRepository : IEnclosureRepository
{
    private readonly Dictionary<Guid, Enclosure> _store = new();

    public Task AddAsync(Enclosure enc)
    {
        _store[enc.Id] = enc;
        return Task.CompletedTask;
    }

    public Task<Enclosure?> GetAsync(Guid id) =>
        Task.FromResult(_store.TryGetValue(id, out var e) ? e : null);

    public Task RemoveAsync(Guid id)
    {
        _store.Remove(id);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Enclosure enc)
    {
        _store[enc.Id] = enc;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Enclosure>> GetAllAsync() =>
        Task.FromResult<IReadOnlyList<Enclosure>>(_store.Values.ToList());
}