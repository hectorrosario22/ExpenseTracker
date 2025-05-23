using Ardalis.Result;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services;

public class BudgetService(IStore store) : IBudgetService
{
    public async Task<Result> UpdateBudget(Budget budget)
    {
        await store.Save(Budget.SectionName, budget);
        return Result.Success();
    }
}