using System.Collections.Generic;
using HSEBankFinances.Domain;
using HSEBankFinances.Infrastructure;

namespace HSEBankFinances.Facades
{
    public class BankAccountFacade
    {
        private readonly DomainFactory _factory;
        private readonly IBankAccountRepository _repo;

        public BankAccountFacade(DomainFactory factory, IBankAccountRepository repo)
        {
            _factory = factory;
            _repo = repo;
        }

        public BankAccount CreateBankAccount(string name, decimal initialBalance)
        {
            var account = _factory.CreateBankAccount(name, initialBalance);
            _repo.Add(account);
            return account;
        }

        public IEnumerable<BankAccount> GetAllAccounts()
        {
            return _repo.GetAll();
        }

        public BankAccount GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void DeleteAccount(int id)
        {
            _repo.Remove(id);
        }
    }
}