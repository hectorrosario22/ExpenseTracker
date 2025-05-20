using Ardalis.Result;
using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces;

public interface IExpenseService
{
    Task<Result<int>> AddExpense(Expense expense);
    Task<Result<List<Expense>>> GetExpenses();
}