using System;
using HSEBankFinances.Domain;
using HSEBankFinances.Facades;
using HSEBankFinances.Infrastructure;
using HSEBankFinances.Services;
using HSEBankFinances.Commands;
using HSEBankFinances.ImportExport;
using System.Linq;

namespace HSEBankFinances
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------------------
            // Инициализация (без DI)
            // -------------------------------
            var factory = new DomainFactory();

            // In-memory репозитории
            var bankAccountRepo = new InMemoryBankAccountRepository();
            var categoryRepo = new InMemoryCategoryRepository();
            var operationRepo = new InMemoryOperationRepository();

            // Фасады
            var bankAccountFacade = new BankAccountFacade(factory, bankAccountRepo);
            var categoryFacade = new CategoryFacade(factory, categoryRepo);
            var operationFacade = new OperationFacade(factory, operationRepo);

            // Сервис аналитики
            var analyticsService = new AnalyticsService();

            // -------------------------------
            // Консольное меню
            // -------------------------------
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n=== ВШЭ-Банк: Учет финансов ===");
                Console.WriteLine("1. Создать счёт");
                Console.WriteLine("2. Создать категорию");
                Console.WriteLine("3. Создать операцию (доход/расход)");
                Console.WriteLine("4. Показать список счетов");
                Console.WriteLine("5. Показать список категорий");
                Console.WriteLine("6. Показать список операций");
                Console.WriteLine("7. Подсчитать разницу доходов и расходов");
                Console.WriteLine("8. Экспорт данных (Посетитель)");
                Console.WriteLine("9. Импорт данных (Шаблонный метод)");
                Console.WriteLine("0. Выход");
                Console.Write("Введите номер действия: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ExecuteWithTiming(new CreateBankAccountCommand(bankAccountFacade));
                        break;
                    case "2":
                        ExecuteWithTiming(new CreateCategoryCommand(categoryFacade));
                        break;
                    case "3":
                        ExecuteWithTiming(new CreateOperationCommand(operationFacade, bankAccountFacade, categoryFacade));
                        break;
                    case "4":
                        ExecuteWithTiming(new ListBankAccountsCommand(bankAccountFacade));
                        break;
                    case "5":
                        ExecuteWithTiming(new ListCategoriesCommand(categoryFacade));
                        break;
                    case "6":
                        ExecuteWithTiming(new ListOperationsCommand(operationFacade));
                        break;
                    case "7":
                        ExecuteWithTiming(new ShowIncomeExpenseDiffCommand(analyticsService, operationFacade));
                        break;
                    case "8":
                        ExecuteWithTiming(new ExportAllDataCommand(bankAccountFacade, categoryFacade, operationFacade));
                        break;
                    case "9":
                        ExecuteWithTiming(new ImportDemoCommand(bankAccountFacade, categoryFacade, operationFacade));
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }

            Console.WriteLine("Спасибо за использование приложения!");
        }

        /// <summary>
        /// Обёртка для выполнения команд с использованием паттерна «Декоратор»
        /// (замер времени выполнения).
        /// </summary>
        static void ExecuteWithTiming(ICommand command)
        {
            ICommand timedCommand = new TimingCommandDecorator(command);
            timedCommand.Execute();
        }
    }
}
