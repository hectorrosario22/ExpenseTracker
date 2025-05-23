using System.ComponentModel.DataAnnotations;
using Cocona;

namespace ExpenseTracker.Parameters;

public record BudgetCommandParameter : ICommandParameterSet
{
    [Option("monthly-expenses", Description = "Monthly expenses")]
    [Range(0, int.MaxValue)]
    public required decimal MonthlyExpenses { get; init; }
}
