using System;
using HSEBankFinances.Facades;
using HSEBankFinances.Domain;

namespace HSEBankFinances.Commands
{
    public class CreateCategoryCommand : ICommand
    {
        private readonly CategoryFacade _categoryFacade;

        public CreateCategoryCommand(CategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        public void Execute()
        {
            Console.WriteLine("Введите название категории:");
            var name = Console.ReadLine();

            Console.WriteLine("Это доход (1) или расход (2)?");
            var choice = Console.ReadLine();

            var type = OperationType.Income;
            if (choice == "2")
                type = OperationType.Expense;

            var category = _categoryFacade.CreateCategory(name, type);
            Console.WriteLine($"Категория создана: ID={category.Id}, {category.Name}, {category.Type}");
        }
    }
}