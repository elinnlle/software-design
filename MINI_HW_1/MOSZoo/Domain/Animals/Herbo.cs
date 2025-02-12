namespace MOSZoo.Domain.Animals
{
    /// <summary>
    /// Абстрактный класс для травоядных животных.
    /// </summary>
    public abstract class Herbo : Animal
    {
        public int Kindness { get; protected set; }

        protected Herbo(string name, int food, int kindness)
            : base(name, food)
        {
            Kindness = kindness;
        }
    }
}
