using Ardalis.Result;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services;

public class ExpenseService(
    IStore store,
    IBudgetService budgetService) : IExpenseService
{
    public async Task<Result<int>> AddExpense(Expense expense)
    {
        var expenses = await store.Load<List<Expense>>(Expense.SectionName) ?? [];
        var newExpense = expense with
        {
            Id = expenses.Count == 0 ? 1 : expenses.Max(e => e.Id) + 1,
            CreatedAt = DateTime.UtcNow,
        };

        expenses.Add(newExpense);
        await store.Save(Expense.SectionName, expenses);

        var monthlyExpensesBudget = await budgetService.GetMonthlyExpensesBudget();
        var totalExpenses = expenses.Where(d =>
            d.CreatedAt.Month == DateTime.UtcNow.Month
            && d.CreatedAt.Year == DateTime.UtcNow.Year
        ).Sum(e => e.Amount);

        return monthlyExpensesBudget > 0 && totalExpenses > monthlyExpensesBudget
            ? Result.Success(newExpense.Id, $"Be careful! You have exceeded your monthly budget of {monthlyExpensesBudget:C2}.")
            : Result.Success(newExpense.Id);
    }

    public async Task<Result> UpdateExpense(Expense expense)
    {
        var expenses = await store.Load<List<Expense>>(Expense.SectionName) ?? [];
        var index = expenses.FindIndex(e => e.Id == expense.Id);
        if (index == -1)
        {
            return Result.NotFound($"Expense with ID {expense.Id} not found.");
        }

        var updatedExpense = expenses[index] with
        {
            Description = expense.Description,
            Amount = expense.Amount,
            Categories = expense.Categories,
            UpdatedAt = DateTime.UtcNow,
        };

        expenses[index] = updatedExpense;
        await store.Save(Expense.SectionName, expenses);

        var monthlyExpensesBudget = await budgetService.GetMonthlyExpensesBudget();
        var totalExpenses = expenses.Where(d =>
            d.CreatedAt.Month == DateTime.UtcNow.Month
            && d.CreatedAt.Year == DateTime.UtcNow.Year
        ).Sum(e => e.Amount);

        return monthlyExpensesBudget > 0 && totalExpenses > monthlyExpensesBudget
            ? Result.SuccessWithMessage($"Be careful! You have exceeded your monthly budget of {monthlyExpensesBudget:C2}.")
            : Result.Success();
    }

    public async Task<Result> DeleteExpense(int id)
    {
        var expenses = await store.Load<List<Expense>>(Expense.SectionName) ?? [];
        var index = expenses.FindIndex(e => e.Id == id);
        if (index == -1)
        {
            return Result.NotFound($"Expense with ID {id} not found.");
        }

        expenses.RemoveAt(index);
        await store.Save(Expense.SectionName, expenses);
        return Result.Success();
    }

    public async Task<Result<List<Expense>>> GetExpenses(int? month = null, IEnumerable<string>? categories = null)
    {
        IEnumerable<Expense> expenses = await store.Load<List<Expense>>(Expense.SectionName) ?? [];
        if (month.HasValue)
        {
            var currentYear = DateTime.UtcNow.Year;
            expenses = expenses.Where(d =>
                d.CreatedAt.Month == month.Value
                && d.CreatedAt.Year == currentYear
            );
        }

        if (categories is not null && categories.Any())
        {
            var currentYear = DateTime.UtcNow.Year;
            expenses = expenses.Where(d =>
                d.Categories.Overlaps(categories)
            );
        }
        return expenses.ToList();
    }

    public async Task<Result<decimal>> GetTotalExpenses(int? month = null, IEnumerable<string>? categories = null)
    {
        IEnumerable<Expense> expenses = await store.Load<List<Expense>>(Expense.SectionName) ?? [];
        if (month.HasValue)
        {
            var currentYear = DateTime.UtcNow.Year;
            expenses = expenses.Where(d =>
                d.CreatedAt.Month == month.Value
                && d.CreatedAt.Year == currentYear
            );
        }

        if (categories is not null && categories.Any())
        {
            var currentYear = DateTime.UtcNow.Year;
            expenses = expenses.Where(d =>
                d.Categories.Overlaps(categories)
            );
        }

        var totalExpenses = expenses.Sum(d => d.Amount);
        return totalExpenses;
    }
}
