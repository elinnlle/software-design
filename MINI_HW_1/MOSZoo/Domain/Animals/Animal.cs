using MOSZoo.Interfaces;

namespace MOSZoo.Domain.Animals
{
    /// <summary>
    /// Абстрактный класс животных. Реализует IAlive и IInventory.
    /// </summary>
    public abstract class Animal : IAlive, IInventory
    {
        public int Food { get; protected set; }
        public int Number { get; set; }
        public string Name { get; protected set; }

        protected Animal(string name, int food)
        {
            Name = name;
            Food = food;
        }
    }
}
