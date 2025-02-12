namespace MOSZoo.Domain.Animals
{
    /// <summary>
    /// Абстрактный класс для хищных животных.
    /// </summary>
    public abstract class Predator : Animal
    {
        protected Predator(string name, int food)
            : base(name, food)
        {
        }
    }
}
