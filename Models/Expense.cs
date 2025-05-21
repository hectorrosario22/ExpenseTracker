namespace ExpenseTracker.Models;

public record Expense
{
    public int Id { get; init; }
    public required string Description { get; init; }
    public required decimal Amount { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}