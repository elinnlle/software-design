using System.Collections.Generic;
using HSEBankFinances.Domain;

namespace HSEBankFinances.Infrastructure
{
    public interface IOperationRepository
    {
        Operation Add(Operation operation);
        Operation GetById(int id);
        IEnumerable<Operation> GetAll();
        void Update(Operation operation);
        void Remove(int id);
    }
}