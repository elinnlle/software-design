using System.Collections.Generic;
using HSEBankFinances.Domain;
using HSEBankFinances.Infrastructure;

namespace HSEBankFinances.Facades
{
    public class CategoryFacade
    {
        private readonly DomainFactory _factory;
        private readonly ICategoryRepository _repo;

        public CategoryFacade(DomainFactory factory, ICategoryRepository repo)
        {
            _factory = factory;
            _repo = repo;
        }

        public Category CreateCategory(string name, OperationType type)
        {
            var category = _factory.CreateCategory(name, type);
            _repo.Add(category);
            return category;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _repo.GetAll();
        }

        public Category GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void DeleteCategory(int id)
        {
            _repo.Remove(id);
        }
    }
}