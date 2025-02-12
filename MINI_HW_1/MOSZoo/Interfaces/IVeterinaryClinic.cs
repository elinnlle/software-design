using MOSZoo.Domain.Animals;

namespace MOSZoo.Interfaces
{
    /// <summary>
    /// Интерфейс ветеринарной клиники для проверки состояния здоровья животных.
    /// </summary>
    public interface IVeterinaryClinic
    {
        bool CheckHealth(Animal animal);
    }
}
