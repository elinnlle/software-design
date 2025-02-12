using System;
using MOSZoo.Interfaces;
using MOSZoo.Domain.Animals;

namespace MOSZoo.Services
{
    /// <summary>
    /// Класс ветеринарной клиники – проверяет здоровье животного.
    /// </summary>
    public class VeterinaryClinic : IVeterinaryClinic
    {
        public bool CheckHealth(Animal animal)
        {
            Console.WriteLine($"Проверка здоровья {animal.Name}. Нажмите пробел, если животное здорово, или любую другую клавишу, если не здорово.");
            var keyInfo = Console.ReadKey(intercept: true);
            return keyInfo.Key == ConsoleKey.Spacebar;
        }
    }
}
