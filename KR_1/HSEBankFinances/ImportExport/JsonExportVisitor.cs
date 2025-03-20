using System;
using HSEBankFinances.Domain;

namespace HSEBankFinances.ImportExport
{
    public class JsonExportVisitor : IExportVisitor
    {
        public void Visit(BankAccount account)
        {
            // Упрощённо выводим в консоль "JSON"
            Console.WriteLine($"{{ \"type\": \"BankAccount\", \"id\": {account.Id}, \"name\": \"{account.Name}\", \"balance\": {account.Balance} }}");
        }

        public void Visit(Category category)
        {
            Console.WriteLine($"{{ \"type\": \"Category\", \"id\": {category.Id}, \"name\": \"{category.Name}\", \"opType\": \"{category.Type}\" }}");
        }

        public void Visit(Operation operation)
        {
            Console.WriteLine($"{{ \"type\": \"Operation\", \"id\": {operation.Id}, \"opType\": \"{operation.Type}\", \"amount\": {operation.Amount}, \"date\": \"{operation.Date}\" }}");
        }
    }
}