using MOSZoo.Domain.Entities;

namespace MOSZoo.Domain.Interfaces;

public interface IAnimalRepository
{
    Task AddAsync(Animal animal);
    Task<Animal?> GetAsync(Guid id);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(Animal animal);
    Task<IReadOnlyList<Animal>> GetAllAsync();
}