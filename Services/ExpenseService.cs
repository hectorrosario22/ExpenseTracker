using Ardalis.Result;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services;

public class ExpenseService(IStore store) : IExpenseService
{
    public async Task<Result<int>> AddExpense(Expense expense)
    {
        var expenses = await store.Load<Expense>();
        var newExpense = expense with
        {
            Id = expenses.Count == 0 ? 1 : expenses.Max(e => e.Id) + 1
        };

        expenses.Add(newExpense);
        await store.Save(expenses);
        return Result.Success(newExpense.Id);
    }
}
