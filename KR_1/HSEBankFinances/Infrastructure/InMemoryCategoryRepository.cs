using System.Collections.Generic;
using HSEBankFinances.Domain;
using System.Linq;

namespace HSEBankFinances.Infrastructure
{
    public class InMemoryCategoryRepository : ICategoryRepository
    {
        private readonly Dictionary<int, Category> _storage = new Dictionary<int, Category>();

        public Category Add(Category category)
        {
            _storage[category.Id] = category;
            return category;
        }

        public Category GetById(int id)
        {
            _storage.TryGetValue(id, out var cat);
            return cat;
        }

        public IEnumerable<Category> GetAll()
        {
            return _storage.Values.ToList();
        }

        public void Update(Category category)
        {
            if (_storage.ContainsKey(category.Id))
            {
                _storage[category.Id] = category;
            }
        }

        public void Remove(int id)
        {
            _storage.Remove(id);
        }
    }
}