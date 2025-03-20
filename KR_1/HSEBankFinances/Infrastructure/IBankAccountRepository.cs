using System.Collections.Generic;
using HSEBankFinances.Domain;

namespace HSEBankFinances.Infrastructure
{
    public interface IBankAccountRepository
    {
        BankAccount Add(BankAccount account);
        BankAccount GetById(int id);
        IEnumerable<BankAccount> GetAll();
        void Update(BankAccount account);
        void Remove(int id);
    }
}