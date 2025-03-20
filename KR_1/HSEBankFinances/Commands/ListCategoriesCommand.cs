using System;
using HSEBankFinances.Facades;

namespace HSEBankFinances.Commands
{
    public class ListCategoriesCommand : ICommand
    {
        private readonly CategoryFacade _categoryFacade;

        public ListCategoriesCommand(CategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        public void Execute()
        {
            var categories = _categoryFacade.GetAllCategories();
            Console.WriteLine("Категории:");
            foreach (var cat in categories)
            {
                Console.WriteLine($"ID={cat.Id}, {cat.Name}, {cat.Type}");
            }
        }
    }
}