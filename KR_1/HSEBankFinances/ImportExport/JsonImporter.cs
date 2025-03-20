using System;
using System.Collections.Generic;
using HSEBankFinances.Facades;

namespace HSEBankFinances.ImportExport
{
    public class JsonImporter : DataImporter
    {
        public JsonImporter(
            BankAccountFacade bankAccountFacade,
            CategoryFacade categoryFacade,
            OperationFacade operationFacade)
            : base(bankAccountFacade, categoryFacade, operationFacade)
        {
        }

        protected override List<ImportedItem> ParseContent(string content)
        {
            Console.WriteLine("Парсим JSON...");
            // Упростим: возвращаем фиктивные данные
            return new List<ImportedItem>
            {
                new ImportedItem { Type = "BankAccount", Name = "JsonAccount", Amount = 1000 },
                new ImportedItem { Type = "Category",    Name = "JsonCategoryFood", Amount = 0 },
                new ImportedItem { Type = "Operation",   Name = "JsonOperation", Amount = 50 }
            };
        }
    }
}