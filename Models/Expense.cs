namespace ExpenseTracker.Models;

public record Expense
{
    public int Id { get; init; }
    public required string Description { get; init; }
    public required decimal Amount { get; init; }
}