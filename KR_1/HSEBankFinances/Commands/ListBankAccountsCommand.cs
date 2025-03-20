using System;
using HSEBankFinances.Facades;

namespace HSEBankFinances.Commands
{
    public class ListBankAccountsCommand : ICommand
    {
        private readonly BankAccountFacade _bankAccountFacade;

        public ListBankAccountsCommand(BankAccountFacade bankAccountFacade)
        {
            _bankAccountFacade = bankAccountFacade;
        }

        public void Execute()
        {
            var accounts = _bankAccountFacade.GetAllAccounts();
            Console.WriteLine("Счета:");
            foreach (var acc in accounts)
            {
                Console.WriteLine($"ID={acc.Id}, Name={acc.Name}, Balance={acc.Balance}");
            }
        }
    }
}