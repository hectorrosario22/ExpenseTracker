using System.ComponentModel.DataAnnotations;
using Cocona;

namespace ExpenseTracker.Parameters;

public record SummaryCommandParameter : ICommandParameterSet
{
    [HasDefaultValue]
    [Option(Description = "Month")]
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
    public int? Month { get; init; }
}