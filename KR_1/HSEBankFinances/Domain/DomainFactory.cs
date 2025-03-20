using System;

namespace HSEBankFinances.Domain
{
    /// <summary>
    /// Фабрика для создания доменных объектов (порождающий паттерн «Фабрика»).
    /// </summary>
    public class DomainFactory
    {
        private int _bankAccountCounter = 1;
        private int _categoryCounter = 1;
        private int _operationCounter = 1;

        public BankAccount CreateBankAccount(string name, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название счёта не может быть пустым.");
            if (initialBalance < 0)
                throw new ArgumentException("Баланс не может быть отрицательным.");

            var account = new BankAccount(_bankAccountCounter++, name, initialBalance);
            return account;
        }

        public Category CreateCategory(string name, OperationType type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название категории не может быть пустым.");

            var cat = new Category(_categoryCounter++, name, type);
            return cat;
        }

        public Operation CreateOperation(OperationType type, int bankAccountId, decimal amount,
            DateTime date, string description, int categoryId)
        {
            if (amount < 0)
                throw new ArgumentException("Сумма операции не может быть отрицательной.");

            var op = new Operation(_operationCounter++, type, bankAccountId, amount, date, description, categoryId);
            return op;
        }
    }
}