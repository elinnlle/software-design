using System;
using System.Collections.Generic;
using HSEBankFinances.Facades;

namespace HSEBankFinances.ImportExport
{
    public class YamlImporter : DataImporter
    {
        public YamlImporter(
            BankAccountFacade bankAccountFacade,
            CategoryFacade categoryFacade,
            OperationFacade operationFacade)
            : base(bankAccountFacade, categoryFacade, operationFacade)
        {
        }

        protected override List<ImportedItem> ParseContent(string content)
        {
            Console.WriteLine("Парсим YAML...");
            return new List<ImportedItem>
            {
                new ImportedItem { Type = "BankAccount", Name = "YamlAccount", Amount = 3000 },
                new ImportedItem { Type = "Category",    Name = "YamlCategoryTravel", Amount = 0 },
                new ImportedItem { Type = "Operation",   Name = "YamlOperation", Amount = 999 }
            };
        }
    }
}