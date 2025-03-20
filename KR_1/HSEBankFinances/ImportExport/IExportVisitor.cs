using HSEBankFinances.Domain;

namespace HSEBankFinances.ImportExport
{
    public interface IExportVisitor
    {
        void Visit(BankAccount account);
        void Visit(Category category);
        void Visit(Operation operation);
    }
}