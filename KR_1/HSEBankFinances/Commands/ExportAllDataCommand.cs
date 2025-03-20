using System;
using System.Linq;
using HSEBankFinances.Facades;
using HSEBankFinances.ImportExport;
using HSEBankFinances.Commands;

namespace HSEBankFinances.Commands
{
    public class ExportAllDataCommand : ICommand
    {
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly CategoryFacade _categoryFacade;
        private readonly OperationFacade _operationFacade;

        public ExportAllDataCommand(
            BankAccountFacade bankAccountFacade,
            CategoryFacade categoryFacade,
            OperationFacade operationFacade)
        {
            _bankAccountFacade = bankAccountFacade;
            _categoryFacade = categoryFacade;
            _operationFacade = operationFacade;
        }

        public void Execute()
        {
            var accounts = _bankAccountFacade.GetAllAccounts().ToList();
            var categories = _categoryFacade.GetAllCategories().ToList();
            var operations = _operationFacade.GetAllOperations().ToList();

            // Экспорт в JSON
            var jsonVisitor = new JsonExportVisitor();
            Console.WriteLine("\n=== Экспорт в JSON (Посетитель) ===");
            accounts.ForEach(a => a.Accept(jsonVisitor));
            categories.ForEach(c => c.Accept(jsonVisitor));
            operations.ForEach(o => o.Accept(jsonVisitor));

            // Экспорт в CSV
            var csvVisitor = new CsvExportVisitor();
            Console.WriteLine("\n=== Экспорт в CSV (Посетитель) ===");
            accounts.ForEach(a => a.Accept(csvVisitor));
            categories.ForEach(c => c.Accept(csvVisitor));
            operations.ForEach(o => o.Accept(csvVisitor));
        }
    }
}