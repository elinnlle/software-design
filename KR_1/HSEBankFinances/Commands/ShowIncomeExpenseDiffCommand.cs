using System;
using HSEBankFinances.Services;
using HSEBankFinances.Facades;

namespace HSEBankFinances.Commands
{
    public class ShowIncomeExpenseDiffCommand : ICommand
    {
        private readonly AnalyticsService _analyticsService;
        private readonly OperationFacade _operationFacade;

        public ShowIncomeExpenseDiffCommand(AnalyticsService analyticsService, OperationFacade operationFacade)
        {
            _analyticsService = analyticsService;
            _operationFacade = operationFacade;
        }

        public void Execute()
        {
            var operations = _operationFacade.GetAllOperations();
            var diff = _analyticsService.CalculateIncomeExpenseDiff(operations);
            Console.WriteLine($"Разница (доходы - расходы) за всё время: {diff}");
        }
    }
}