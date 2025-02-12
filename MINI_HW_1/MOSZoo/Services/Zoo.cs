using System;
using System.Collections.Generic;
using System.Linq;
using MOSZoo.Interfaces;
using MOSZoo.Domain.Animals;
using MOSZoo.UI;

namespace MOSZoo.Services
{
    /// <summary>
    /// Класс зоопарка, хранящий животных и инвентарные вещи.
    /// </summary>
    public class Zoo
    {
        private readonly List<Animal> _animals;
        private readonly List<IInventory> _inventories;
        private readonly IVeterinaryClinic _veterinaryClinic;
        
        // Счетчик для присвоения инвентарных номеров
        private int _nextInventoryNumber = 1;

        public Zoo(IVeterinaryClinic veterinaryClinic)
        {
            _veterinaryClinic = veterinaryClinic;
            _animals = new List<Animal>();
            _inventories = new List<IInventory>();
        }

        /// <summary>
        /// Принимает животное, проверив его здоровье.
        /// </summary>
        public bool AddAnimal(Animal animal)
        {
            if (_veterinaryClinic.CheckHealth(animal))
            {
                if (animal.Number == 0)
                {
                    animal.Number = _nextInventoryNumber++;
                }
                _animals.Add(animal);
                _inventories.Add(animal);
                Console.WriteLine();
                Beautiful.PrintBeautifullyWL($"Животное {animal.Name} успешно принято в зоопарк с инвентарным номером {animal.Number}.", ConsoleColor.Green);
                return true;
            }
            else
            {
                Beautiful.PrintBeautifullyWL($"\nЖивотное {animal.Name} не прошло проверку здоровья и не принято в зоопарк.", ConsoleColor.Red);
                return false;
            }
        }

        /// <summary>
        /// Добавляет инвентарную вещь.
        /// </summary>
        public void AddInventoryItem(IInventory item)
        {
            if (item.Number == 0)
            {
                item.Number = _nextInventoryNumber++;
            }
            _inventories.Add(item);
            Beautiful.PrintBeautifullyWL($"\nВещь {item.Name} успешно добавлена с инвентарным номером {item.Number}.", ConsoleColor.Green);
        }

        public int TotalFoodConsumption()
        {
            return _animals.Sum(a => a.Food);
        }

        public int AnimalCount()
        {
            return _animals.Count;
        }

        /// <summary>
        /// Возвращает травоядных животных с уровнем доброты больше 5.
        /// </summary>
        public IEnumerable<Herbo> GetContactZooAnimals()
        {
            return _animals.OfType<Herbo>().Where(a => a.Kindness > 5);
        }

        /// <summary>
        /// Возвращает список всех инвентарных вещей.
        /// </summary>
        public IEnumerable<IInventory> GetInventoryItems()
        {
            return _inventories;
        }
        
        /// <summary>
        /// Возвращает список всех животных.
        /// </summary>
        public IEnumerable<Animal> GetAnimals()
        {
            return _animals;
        }
    }
}
