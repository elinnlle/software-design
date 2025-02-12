namespace MOSZoo.Interfaces
{
    /// <summary>
    /// Интерфейс для объектов, имеющих инвентаризационный номер.
    /// </summary>
    public interface IInventory
    {
        int Number { get; set; }
        string Name { get; }
    }
}
