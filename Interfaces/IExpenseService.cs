using Ardalis.Result;
using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces;

public interface IExpenseService
{
    Task<Result<int>> AddExpense(Expense expense);
    Task<Result> UpdateExpense(Expense expense);
    Task<Result> DeleteExpense(int id);
    Task<Result<List<Expense>>> GetExpenses(int? month = null, IEnumerable<string>? categories = null);
    Task<Result<decimal>> GetTotalExpenses(int? month = null, IEnumerable<string>? categories = null);
}
