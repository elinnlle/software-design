using HSEBankFinances.ImportExport;

namespace HSEBankFinances.Domain
{
    public class BankAccount
    {
        public int Id { get; }
        public string Name { get; private set; }
        public decimal Balance { get; private set; }

        internal BankAccount(int id, string name, decimal balance)
        {
            Id = id;
            Name = name;
            Balance = balance;
        }

        public void UpdateName(string newName)
        {
            Name = newName;
        }

        public void UpdateBalance(decimal newBalance)
        {
            Balance = newBalance;
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}