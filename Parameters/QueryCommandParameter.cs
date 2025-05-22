using System.ComponentModel.DataAnnotations;
using Cocona;

namespace ExpenseTracker.Parameters;

public record QueryCommandParameter : ICommandParameterSet
{
    [HasDefaultValue]
    [Option(Description = "Month")]
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
    public int? Month { get; init; }
    
    [HasDefaultValue]
    [Option("category", Description = "Categories")]
    public IEnumerable<string>? Categories { get; init; }
}