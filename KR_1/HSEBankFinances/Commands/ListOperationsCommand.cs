using System;
using HSEBankFinances.Facades;

namespace HSEBankFinances.Commands
{
    public class ListOperationsCommand : ICommand
    {
        private readonly OperationFacade _operationFacade;

        public ListOperationsCommand(OperationFacade operationFacade)
        {
            _operationFacade = operationFacade;
        }

        public void Execute()
        {
            var ops = _operationFacade.GetAllOperations();
            Console.WriteLine("Операции:");
            foreach (var o in ops)
            {
                Console.WriteLine($"ID={o.Id}, Type={o.Type}, Amount={o.Amount}, Date={o.Date}, Desc={o.Description}");
            }
        }
    }
}