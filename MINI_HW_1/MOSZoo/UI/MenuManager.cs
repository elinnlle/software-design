using System;
using System.Linq;
using MOSZoo.Interfaces;
using MOSZoo.Domain.Animals;
using MOSZoo.Domain.Inventory;

namespace MOSZoo.UI
{
    /// <summary>
    /// Класс для взаимодействия с пользователем.
    /// </summary>
    public class MenuManager
    {
        private readonly IZooService _zooService;

        public MenuManager(IZooService zooService)
        {
            _zooService = zooService;
        }

        /// <summary>
        /// Запуск главного меню.
        /// </summary>
        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                PrintMainMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();
                HandleMainMenuChoice(choice, ref exit);
            }
        }
        private void HandleMainMenuChoice(string choice, ref bool exit)
        {
            switch (choice)
            {
                case "1": AddAnimalMenu(); break;
                case "2": AddInventoryItemMenu(); break;
                case "3": ShowZooReportMenu(); break;
                case "4": PrintContactZooAnimals(); break;
                case "5": PrintInventoryItems(); break;
                case "0": exit = true; break;
                default: Beautiful.PrintBeautifullyWL("Неверный выбор. Попробуйте снова.", ConsoleColor.Red); break;
            }
        }
        
        /// <summary>
        /// Главное меню.
        /// </summary>
        private void PrintMainMenu()
        {
            Beautiful.PrintBeautifullyWL("\nВыберите действие:", ConsoleColor.White);
            Console.WriteLine("1. Добавить животное");
            Console.WriteLine("2. Добавить инвентарную вещь");
            Console.WriteLine("3. Отчет по зоопарку");
            Console.WriteLine("4. Список животных контактного зоопарка");
            Console.WriteLine("5. Список инвентарных вещей");
            Console.WriteLine("0. Выход");
            Beautiful.PrintBeautifullyW("Ваш выбор: ", ConsoleColor.White);
        }

        /// <summary>
        /// Меню для добавления животного.
        /// </summary>
        private void AddAnimalMenu()
        {
            Beautiful.PrintBeautifullyWL("Выберите тип животного для добавления:", ConsoleColor.White);
            Console.WriteLine("1. Кролик");
            Console.WriteLine("2. Лошадь");
            Console.WriteLine("3. Тигр");
            Console.WriteLine("4. Волк");
            Console.WriteLine("5. Обезьяна");
            Beautiful.PrintBeautifullyW("Ваш выбор: ", ConsoleColor.White);
            
            string typeChoice = Console.ReadLine();
            if (!IsValidAnimalChoice(typeChoice))
            {
                Beautiful.PrintBeautifullyWL("\nНеверный выбор типа животного.", ConsoleColor.Red);
                return;
            }

            Console.Write("\nВведите имя животного: ");
            string name = Console.ReadLine();
            
            int food = ReadFood("Введите количество еды (в кг/сутки): ", name);
            
            Animal animal = CreateAnimal(typeChoice, name, food);
            _zooService.AdmitAnimal(animal);
        }
        
        private Animal CreateAnimal(string typeChoice, string name, int food)
        {
            switch (typeChoice)
            {
                case "1": return new Rabbit(name, food, ReadKindness("Введите уровень доброты (1-10): "));
                case "2": return new Horse(name, food, ReadKindness("Введите уровень доброты (1-10): "));
                case "3": return new Tiger(name, food);
                case "4": return new Wolf(name, food);
                case "5": return new Monkey(name, food);
                default: throw new InvalidOperationException("Неверный тип животного.");
            }
        }

        /// <summary>
        /// Проверяет корректность выбора типа животного.
        /// </summary>
        private bool IsValidAnimalChoice(string choice)
        {
            return choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5";
        }

        /// <summary>
        /// Меню для добавления инвентарной вещи.
        /// </summary>
        private void AddInventoryItemMenu()
        {
            Beautiful.PrintBeautifullyWL("Выберите тип инвентарной вещи для добавления:", ConsoleColor.White);
            Console.WriteLine("1. Стол");
            Console.WriteLine("2. Компьютер");
            Beautiful.PrintBeautifullyW("Ваш выбор: ", ConsoleColor.White);
            
            string typeChoice = Console.ReadLine();
            if (!IsValidInventoryChoice(typeChoice))
            {
                Beautiful.PrintBeautifullyWL("\nНеверный выбор типа вещи.", ConsoleColor.Red);
                return;
            }
            
            Console.Write("\nВведите название вещи: ");
            string name = Console.ReadLine();
            IInventory inventoryItem = CreateInventoryItem(typeChoice, name);
            _zooService.AddInventoryItem(inventoryItem);
        }
        
        private IInventory CreateInventoryItem(string typeChoice, string name)
        {
            switch (typeChoice)
            {
                case "1": return new Table(name, 0);
                case "2": return new Computer(name, 0);
                default: throw new InvalidOperationException("Неверный тип инвентарной вещи.");
            }
        }

        /// <summary>
        /// Проверяет корректность выбора типа инвентарной вещи.
        /// </summary>
        private bool IsValidInventoryChoice(string choice)
        {
            return choice == "1" || choice == "2";
        }

        /// <summary>
        /// Меню отчёта по зоопарку.
        /// </summary>
        private void ShowZooReportMenu()
        {
            bool exitReportMenu = false;
            while (!exitReportMenu)
            {
                Beautiful.PrintBeautifullyWL("Какой отчёт вы хотите посмотреть?", ConsoleColor.White);
                Console.WriteLine("1. Вывести количество животных");
                Console.WriteLine("2. Вывести список животных");
                Console.WriteLine("3. Вывести общее количество потребляемой еды (кг/сутки)");
                Console.WriteLine("0. Вернуться в главное меню");
                Beautiful.PrintBeautifullyW("Ваш выбор: ", ConsoleColor.White);
                
                string reportChoice = Console.ReadLine();
                Console.WriteLine();

                HandleReportMenuChoice(reportChoice, ref exitReportMenu);
            }
        }
        
        private void HandleReportMenuChoice(string reportChoice, ref bool exitReportMenu)
        {
            switch (reportChoice)
            {
                case "1":
                    Console.WriteLine($"Общее количество животных: {_zooService.GetAnimalCount()}\n");
                    break;
                case "2":
                    var animals = _zooService.GetAnimals();
                    if (!animals.Any())
                    {
                        Beautiful.PrintBeautifullyWL("В зоопарке нет животных.\n", ConsoleColor.Yellow);
                    }
                    else
                    {
                        Beautiful.PrintBeautifullyWL("Список животных:", ConsoleColor.White);
                        foreach (var animal in animals)
                        {
                            Console.WriteLine($"Имя: {animal.Name}, Инвентарный номер: {animal.Number}");
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine($"Общее количество потребляемой еды (кг/сутки): {_zooService.GetTotalFoodConsumption()}\n");
                    break;
                case "0":
                    exitReportMenu = true;
                    break;
                default:
                    Beautiful.PrintBeautifullyWL("Неверный выбор пункта меню.\n", ConsoleColor.Red);
                    break;
            }
        }

        /// <summary>
        /// Выводит список животных контактного зоопарка.
        /// </summary>
        private void PrintContactZooAnimals()
        {
            Beautiful.PrintBeautifullyWL("Животные, принятые в контактный зоопарк:", ConsoleColor.White);
            var contactAnimals = _zooService.GetContactZooAnimals();
            if (!contactAnimals.Any())
            {
                Beautiful.PrintBeautifullyWL("Нет животных, подходящих для контактного зоопарка.", ConsoleColor.Yellow);
            }
            else
            {
                foreach (var animal in contactAnimals)
                {
                    Console.WriteLine($"Имя: {animal.Name}. Инвентарный номер: {animal.Number}. Доброта: {animal.Kindness}");
                }
            }
        }

        /// <summary>
        /// Выводит список всех инвентарных объектов.
        /// </summary>
        private void PrintInventoryItems()
        {
            Beautiful.PrintBeautifullyWL("Список инвентарных вещей и животных:", ConsoleColor.White);
            var items = _zooService.GetInventoryItems();
            if (!items.Any())
            {
                Beautiful.PrintBeautifullyWL("В зоопарке нет зарегистрированных инвентарных вещей и животных.", ConsoleColor.Yellow);
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"Наименование: {item.Name}. Инвентарный номер: {item.Number}");
                }
            }
        }

        /// <summary>
        /// Проверяет корректность чтения целого числа.
        /// </summary>
        private int ReadInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out result))
                    return result;
                Beautiful.PrintBeautifullyWL("\nНекорректный ввод, попробуйте снова.\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Метод для чтения количества еды с проверкой на положительное число.
        /// </summary>
        private int ReadFood(string prompt, string animalName)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!int.TryParse(input, out result))
                {
                    Beautiful.PrintBeautifullyWL("Некорректный ввод, попробуйте снова.", ConsoleColor.Red);
                    continue;
                }
                if (result <= 0)
                {
                    Beautiful.PrintBeautifullyWL($"Не издевайтесь! {animalName}, как и вы, тоже хочет кушать хотя бы раз в сутки!", ConsoleColor.Yellow);
                    continue;
                }
                return result;
            }
        }

        /// <summary>
        /// Метод для чтения уровня доброты животного с проверкой диапазона.
        /// </summary>
        private int ReadKindness(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!int.TryParse(input, out result))
                {
                    Beautiful.PrintBeautifullyWL("Некорректный ввод, попробуйте снова.", ConsoleColor.Red);
                    continue;
                }
                if (result > 10)
                {
                    Beautiful.PrintBeautifullyWL("Слишком доброе животное, таких не бывает :)",
                        ConsoleColor.Yellow);
                }
                else if (result < 1)
                {
                    Beautiful.PrintBeautifullyWL("Слишком злое животное, таких не бывает :)", ConsoleColor.Yellow);
                }
                else
                {
                    return result;
                }
            }
        }
    }
}
