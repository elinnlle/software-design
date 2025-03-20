using System;
using HSEBankFinances.Domain;

namespace HSEBankFinances.ImportExport
{
    public class CsvExportVisitor : IExportVisitor
    {
        public void Visit(BankAccount account)
        {
            Console.WriteLine($"BankAccount;{account.Id};{account.Name};{account.Balance}");
        }

        public void Visit(Category category)
        {
            Console.WriteLine($"Category;{category.Id};{category.Name};{category.Type}");
        }

        public void Visit(Operation operation)
        {
            Console.WriteLine($"Operation;{operation.Id};{operation.Type};{operation.Amount};{operation.Date};{operation.Description}");
        }
    }
}