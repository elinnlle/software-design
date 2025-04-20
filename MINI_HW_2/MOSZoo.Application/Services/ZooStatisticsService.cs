using MOSZoo.Application.DTO;
using MOSZoo.Application.Interfaces;
using MOSZoo.Domain.Interfaces;

namespace MOSZoo.Application.Services;

public class ZooStatisticsService : IZooStatisticsService
{
    private readonly IAnimalRepository    _animals;
    private readonly IEnclosureRepository _enclosures;

    public ZooStatisticsService(IAnimalRepository animals, IEnclosureRepository enclosures)
    {
        _animals    = animals;
        _enclosures = enclosures;
    }

    public async Task<ZooStatisticsDto> GetAsync()
    {
        var allEnclosures = await _enclosures.GetAllAsync();
        var allAnimals    = await _animals.GetAllAsync();

        int free = allEnclosures.Count(e => e.Animals.Count < e.MaxCapacity);

        return new ZooStatisticsDto(allAnimals.Count, free, allEnclosures.Count);
    }
}