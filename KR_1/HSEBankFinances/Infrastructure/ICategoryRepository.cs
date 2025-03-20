using System.Collections.Generic;
using HSEBankFinances.Domain;

namespace HSEBankFinances.Infrastructure
{
    public interface ICategoryRepository
    {
        Category Add(Category category);
        Category GetById(int id);
        IEnumerable<Category> GetAll();
        void Update(Category category);
        void Remove(int id);
    }
}