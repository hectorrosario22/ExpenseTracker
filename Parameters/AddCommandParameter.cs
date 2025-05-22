using System.ComponentModel.DataAnnotations;
using Cocona;

namespace ExpenseTracker.Parameters;

public record AddCommandParameter : ICommandParameterSet
{
    [MaxLength(100)]
    [Option(Description = "Expense description")]
    public required string Description { get; init; }

    [Option(Description = "Expense amount")]
    [Range(0, int.MaxValue)]
    public required decimal Amount { get; init; }

    [Option("category", Description = "Expense categories")]
    public required IEnumerable<string> Categories { get; init; }
}
