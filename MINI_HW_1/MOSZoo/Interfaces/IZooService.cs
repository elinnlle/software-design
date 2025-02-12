using System.Collections.Generic;
using MOSZoo.Domain.Animals;

namespace MOSZoo.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса зоопарка для управления операциями, связанными с животными и инвентарём.
    /// </summary>
    public interface IZooService
    {
        bool AdmitAnimal(Animal animal);
        void AddInventoryItem(IInventory item);
        int GetTotalFoodConsumption();
        IEnumerable<Herbo> GetContactZooAnimals();
        IEnumerable<IInventory> GetInventoryItems();
        IEnumerable<Animal> GetAnimals();
        int GetAnimalCount();
    }
}
