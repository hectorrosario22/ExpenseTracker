using Ardalis.Result;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services;

public class BudgetService(IStore store) : IBudgetService
{
    public async Task<decimal> GetMonthlyExpensesBudget()
    {
        var budget = await store.Load<Budget>(Budget.SectionName);
        return budget?.MonthlyExpenses ?? 0;
    }

    public async Task<Result> UpdateBudget(Budget budget)
    {
        await store.Save(Budget.SectionName, budget);
        return Result.Success();
    }
}