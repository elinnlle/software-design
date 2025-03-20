using System.Linq;
using System.Collections.Generic;
using HSEBankFinances.Domain;

namespace HSEBankFinances.Services
{
    public class AnalyticsService
    {
        public decimal CalculateIncomeExpenseDiff(IEnumerable<Operation> operations)
        {
            decimal income = operations
                .Where(o => o.Type == OperationType.Income)
                .Sum(o => o.Amount);

            decimal expense = operations
                .Where(o => o.Type == OperationType.Expense)
                .Sum(o => o.Amount);

            return income - expense;
        }
    }
}