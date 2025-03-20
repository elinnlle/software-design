using System.Collections.Generic;
using HSEBankFinances.Domain;
using System.Linq;

namespace HSEBankFinances.Infrastructure
{
    public class InMemoryOperationRepository : IOperationRepository
    {
        private readonly Dictionary<int, Operation> _storage = new Dictionary<int, Operation>();

        public Operation Add(Operation operation)
        {
            _storage[operation.Id] = operation;
            return operation;
        }

        public Operation GetById(int id)
        {
            _storage.TryGetValue(id, out var op);
            return op;
        }

        public IEnumerable<Operation> GetAll()
        {
            return _storage.Values.ToList();
        }

        public void Update(Operation operation)
        {
            if (_storage.ContainsKey(operation.Id))
            {
                _storage[operation.Id] = operation;
            }
        }

        public void Remove(int id)
        {
            _storage.Remove(id);
        }
    }
}