using System;
using HSEBankFinances.Facades;
using HSEBankFinances.ImportExport;

namespace HSEBankFinances.Commands
{
    public class ImportDemoCommand : ICommand
    {
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly CategoryFacade _categoryFacade;
        private readonly OperationFacade _operationFacade;

        public ImportDemoCommand(
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
            Console.WriteLine("Демо импорта. Показать, как работает шаблонный метод (JSON, CSV, YAML).");

            var jsonImporter = new JsonImporter(_bankAccountFacade, _categoryFacade, _operationFacade);
            jsonImporter.Import("data.json");

            var csvImporter = new CsvImporter(_bankAccountFacade, _categoryFacade, _operationFacade);
            csvImporter.Import("data.csv");

            var yamlImporter = new YamlImporter(_bankAccountFacade, _categoryFacade, _operationFacade);
            yamlImporter.Import("data.yaml");
        }
    }
}