namespace ExpenseTracker.Models;

public record Budget
{
    public const string SectionName = "budget";

    public decimal MonthlyExpenses { get; init; }
}