using MOSZoo.Domain.Entities;

namespace MOSZoo.Domain.Interfaces;

public interface IEnclosureRepository
{
    Task AddAsync(Enclosure enclosure);
    Task<Enclosure?> GetAsync(Guid id);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(Enclosure enclosure);
    Task<IReadOnlyList<Enclosure>> GetAllAsync();
}