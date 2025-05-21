using Cocona;

namespace ExpenseTracker.Parameters;

public record DeleteCommandParameter : ICommandParameterSet
{
    [Option(Description = "Expense ID")]
    public required int Id { get; init; }
}
