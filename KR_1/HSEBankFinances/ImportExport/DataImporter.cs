using System;
using System.Collections.Generic;
using HSEBankFinances.Facades;

namespace HSEBankFinances.ImportExport
{
    public abstract class DataImporter
    {
        protected BankAccountFacade BankAccountFacade;
        protected CategoryFacade CategoryFacade;
        protected OperationFacade OperationFacade;

        public DataImporter(
            BankAccountFacade bankAccountFacade,
            CategoryFacade categoryFacade,
            OperationFacade operationFacade)
        {
            BankAccountFacade = bankAccountFacade;
            CategoryFacade = categoryFacade;
            OperationFacade = operationFacade;
        }

        public void Import(string filePath)
        {
            // 1. Считать файл (упростим, просто возвращаем строку)
            var content = ReadFile(filePath);

            // 2. Распарсить
            var items = ParseContent(content);

            // 3. Сохранить
            foreach (var item in items)
            {
                SaveItem(item);
            }

            Console.WriteLine($"Импорт из {filePath} завершён.\n");
        }

        protected virtual string ReadFile(string filePath)
        {
            Console.WriteLine($"(Эмуляция чтения файла {filePath})");
            return "Some content";
        }

        protected abstract List<ImportedItem> ParseContent(string content);

        protected virtual void SaveItem(ImportedItem item)
        {
            // Упрощённо – выводим в консоль
            Console.WriteLine($"Сохраняем {item.Type} - {item.Name} - {item.Amount}");
        }
    }

    public class ImportedItem
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}