using Ardalis.Result;
using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces;

public interface IBudgetService
{
    Task<Result> UpdateBudget(Budget budget);
    Task<decimal> GetMonthlyExpensesBudget();
}