using MOSZoo.Interfaces;

namespace MOSZoo.Domain.Inventory
{
    /// <summary>
    /// Абстрактный класс для инвентарных вещей.
    /// </summary>
    public abstract class Thing : IInventory
    {
        public int Number { get; set; }
        public string Name { get; protected set; }

        protected Thing(string name, int number)
        {
            Name = name;
            Number = number;
        }
    }
}
