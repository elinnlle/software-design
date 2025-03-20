using System.Collections.Generic;
using HSEBankFinances.Domain;
using System.Linq;

namespace HSEBankFinances.Infrastructure
{
    public class InMemoryBankAccountRepository : IBankAccountRepository
    {
        private readonly Dictionary<int, BankAccount> _storage = new Dictionary<int, BankAccount>();

        public BankAccount Add(BankAccount account)
        {
            _storage[account.Id] = account;
            return account;
        }

        public BankAccount GetById(int id)
        {
            _storage.TryGetValue(id, out var acc);
            return acc;
        }

        public IEnumerable<BankAccount> GetAll()
        {
            return _storage.Values.ToList();
        }

        public void Update(BankAccount account)
        {
            if (_storage.ContainsKey(account.Id))
            {
                _storage[account.Id] = account;
            }
        }

        public void Remove(int id)
        {
            _storage.Remove(id);
        }
    }
}