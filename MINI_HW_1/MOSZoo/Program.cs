using System;
using Microsoft.Extensions.DependencyInjection;
using MOSZoo.Interfaces;
using MOSZoo.Services;
using MOSZoo.UI;

namespace MOSZoo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Настройка DI-контейнера
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IVeterinaryClinic, VeterinaryClinic>();
            serviceCollection.AddSingleton<Zoo>();
            serviceCollection.AddSingleton<IZooService, ZooService>();
            serviceCollection.AddSingleton<MenuManager>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Получаем и запускаем меню
            Beautiful.PrintBeautifullyW("Добро пожаловать в зоопарк!", ConsoleColor.Yellow);
            var menuManager = serviceProvider.GetService<MenuManager>();
            menuManager.Run();
        }
    }
}

