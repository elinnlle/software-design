using System;
using HSEBankFinances.Facades;
using HSEBankFinances.Domain;

namespace HSEBankFinances.Commands
{
    public class CreateOperationCommand : ICommand
    {
        private readonly OperationFacade _operationFacade;
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly CategoryFacade _categoryFacade;

        public CreateOperationCommand(
            OperationFacade operationFacade,
            BankAccountFacade bankAccountFacade,
            CategoryFacade categoryFacade)
        {
            _operationFacade = operationFacade;
            _bankAccountFacade = bankAccountFacade;
            _categoryFacade = categoryFacade;
        }

        public void Execute()
        {
            Console.WriteLine("Введите ID счёта:");
            if (!int.TryParse(Console.ReadLine(), out var accountId))
            {
                Console.WriteLine("Некорректный ID счёта.");
                return;
            }

            var account = _bankAccountFacade.GetById(accountId);
            if (account == null)
            {
                Console.WriteLine("Счёт с таким ID не найден.");
                return;
            }

            Console.WriteLine("Введите сумму операции:");
            if (!decimal.TryParse(Console.ReadLine(), out var amount))
            {
                Console.WriteLine("Некорректная сумма.");
                return;
            }

            Console.WriteLine("Введите описание (необязательно):");
            var description = Console.ReadLine();

            Console.WriteLine("Введите ID категории:");
            if (!int.TryParse(Console.ReadLine(), out var categoryId))
            {
                Console.WriteLine("Некорректный ID категории.");
                return;
            }

            var category = _categoryFacade.GetById(categoryId);
            if (category == null)
            {
                Console.WriteLine("Категория с таким ID не найдена.");
                return;
            }

            // Определяем тип операции по типу категории (Income/Expense)
            var opType = category.Type;

            var operation = _operationFacade.CreateOperation(
                opType,
                accountId,
                amount,
                DateTime.Now,
                description,
                categoryId
            );

            // Пересчёт баланса счёта
            if (opType == OperationType.Income)
                account.UpdateBalance(account.Balance + amount);
            else
                account.UpdateBalance(account.Balance - amount);

            // Сохраняем изменения счёта – т.к. у нас InMemory, достаточно репо-обновления:
            // но обычно Facade должен делать это сам. Для упрощения "достанем" репо:
            // Или сделаем метод в фасаде (условно)...

            // (Упростим: напрямую вызовем "Update" в репо)
            // P.S. Лучше иметь метод UpdateBankAccount в фасаде.
            var repoField = typeof(BankAccountFacade).GetField("_repo",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var repo = repoField?.GetValue(_bankAccountFacade) as Infrastructure.IBankAccountRepository;
            repo?.Update(account);

            Console.WriteLine($"Операция создана: ID={operation.Id}, Тип={operation.Type}, Сумма={operation.Amount}");
            Console.WriteLine($"Текущий баланс счёта {account.Id}: {account.Balance}");
        }
    }
}
