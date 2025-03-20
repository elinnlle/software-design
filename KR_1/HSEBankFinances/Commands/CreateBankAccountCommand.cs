using System;
using HSEBankFinances.Facades;

namespace HSEBankFinances.Commands
{
    public class CreateBankAccountCommand : ICommand
    {
        private readonly BankAccountFacade _bankAccountFacade;

        public CreateBankAccountCommand(BankAccountFacade bankAccountFacade)
        {
            _bankAccountFacade = bankAccountFacade;
        }

        public void Execute()
        {
            Console.WriteLine("Введите название счёта:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите начальный баланс:");
            if (!decimal.TryParse(Console.ReadLine(), out var initialBalance))
            {
                Console.WriteLine("Некорректный ввод баланса.");
                return;
            }

            var account = _bankAccountFacade.CreateBankAccount(name, initialBalance);
            Console.WriteLine($"Счёт создан: ID={account.Id}, Name={account.Name}, Balance={account.Balance}");
        }
    }
}