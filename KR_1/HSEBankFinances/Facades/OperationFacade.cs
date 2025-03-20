using System.Collections.Generic;
using HSEBankFinances.Domain;
using HSEBankFinances.Infrastructure;
using System;

namespace HSEBankFinances.Facades
{
    public class OperationFacade
    {
        private readonly DomainFactory _factory;
        private readonly IOperationRepository _repo;

        public OperationFacade(DomainFactory factory, IOperationRepository repo)
        {
            _factory = factory;
            _repo = repo;
        }

        public Operation CreateOperation(OperationType type, int bankAccountId, decimal amount,
            DateTime date, string description, int categoryId)
        {
            var op = _factory.CreateOperation(type, bankAccountId, amount, date, description, categoryId);
            _repo.Add(op);
            return op;
        }

        public IEnumerable<Operation> GetAllOperations()
        {
            return _repo.GetAll();
        }

        public Operation GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void DeleteOperation(int id)
        {
            _repo.Remove(id);
        }
    }
}