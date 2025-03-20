using HSEBankFinances.ImportExport;
using System;

namespace HSEBankFinances.Domain
{
    public class Operation
    {
        public int Id { get; }
        public OperationType Type { get; private set; }
        public int BankAccountId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public int CategoryId { get; private set; }

        internal Operation(
            int id,
            OperationType type,
            int bankAccountId,
            decimal amount,
            DateTime date,
            string description,
            int categoryId)
        {
            Id = id;
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            Description = description;
            CategoryId = categoryId;
        }

        public void UpdateDescription(string newDesc)
        {
            Description = newDesc;
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}