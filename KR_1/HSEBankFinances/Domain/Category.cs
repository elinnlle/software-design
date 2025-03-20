using HSEBankFinances.ImportExport;

namespace HSEBankFinances.Domain
{
    public class Category
    {
        public int Id { get; }
        public string Name { get; private set; }
        public OperationType Type { get; private set; }

        internal Category(int id, string name, OperationType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public void UpdateName(string newName)
        {
            Name = newName;
        }

        public void UpdateType(OperationType newType)
        {
            Type = newType;
        }
        
        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}