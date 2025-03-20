using System;
using System.Collections.Generic;
using HSEBankFinances.Facades;

namespace HSEBankFinances.ImportExport
{
    public class CsvImporter : DataImporter
    {
        public CsvImporter(
            BankAccountFacade bankAccountFacade,
            CategoryFacade categoryFacade,
            OperationFacade operationFacade)
            : base(bankAccountFacade, categoryFacade, operationFacade)
        {
        }

        protected override List<ImportedItem> ParseContent(string content)
        {
            Console.WriteLine("Парсим CSV...");
            return new List<ImportedItem>
            {
                new ImportedItem { Type = "BankAccount", Name = "CsvAccount", Amount = 2000 },
                new ImportedItem { Type = "Category",    Name = "CsvCategoryHealth", Amount = 0 },
                new ImportedItem { Type = "Operation",   Name = "CsvOperation", Amount = 123 }
            };
        }
    }
}