using MOSZoo.Domain.Entities;
using MOSZoo.Domain.Interfaces;

namespace MOSZoo.Infrastructure.Repositories;

public class InMemoryAnimalRepository : IAnimalRepository
{
    private readonly Dictionary<Guid, Animal> _store = new();

    public Task AddAsync(Animal animal)
    {
        _store[animal.Id] = animal;
        return Task.CompletedTask;
    }

    public Task<Animal?> GetAsync(Guid id) =>
        Task.FromResult(_store.TryGetValue(id, out var a) ? a : null);

    public Task RemoveAsync(Guid id)
    {
        _store.Remove(id);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Animal animal)
    {
        _store[animal.Id] = animal;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Animal>> GetAllAsync() =>
        Task.FromResult<IReadOnlyList<Animal>>(_store.Values.ToList());
}