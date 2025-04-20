using MOSZoo.Domain.Common;
using MOSZoo.Domain.Enums;

namespace MOSZoo.Domain.Entities;

/// <summary>
/// Вольер.
/// </summary>
public class Enclosure : Entity
{
    public string Name { get; private set; }
    public EnclosureType Type { get; private set; }
    public int MaxCapacity { get; private set; }

    private readonly List<Animal> _animals = new();
    public IReadOnlyCollection<Animal> Animals => _animals.AsReadOnly();

    public Enclosure(string name, EnclosureType type, int maxCapacity)
    {
        if (maxCapacity <= 0) throw new ArgumentException("Capacity must be positive");

        Name        = name;
        Type        = type;
        MaxCapacity = maxCapacity;
    }

    public void AddAnimal(Animal animal)
    {
        if (_animals.Count >= MaxCapacity)
            throw new InvalidOperationException("Enclosure is full.");
        _animals.Add(animal);
    }

    public void RemoveAnimal(Animal animal) => _animals.Remove(animal);

    public void Clean() { /* имитация уборки */ }
}