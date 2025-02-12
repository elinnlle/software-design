using System.Collections.Generic;
using MOSZoo.Interfaces;
using MOSZoo.Domain.Animals;

namespace MOSZoo.Services
{
    /// <summary>
    /// Сервис зоопарка, реализующий бизнес-логику.
    /// </summary>
    public class ZooService : IZooService
    {
        private readonly Zoo _zoo;

        public ZooService(Zoo zoo)
        {
            _zoo = zoo;
        }

        public bool AdmitAnimal(Animal animal)
        {
            return _zoo.AddAnimal(animal);
        }

        public void AddInventoryItem(IInventory item)
        {
            _zoo.AddInventoryItem(item);
        }

        public int GetTotalFoodConsumption()
        {
            return _zoo.TotalFoodConsumption();
        }

        public IEnumerable<Herbo> GetContactZooAnimals()
        {
            return _zoo.GetContactZooAnimals();
        }

        public IEnumerable<IInventory> GetInventoryItems()
        {
            return _zoo.GetInventoryItems();
        }

        public int GetAnimalCount()
        {
            return _zoo.AnimalCount();
        }
        
        public IEnumerable<Animal> GetAnimals()
        {
            return _zoo.GetAnimals();
        }
    }
}
