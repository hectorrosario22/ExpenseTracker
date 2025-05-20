using System.ComponentModel.DataAnnotations;
using Cocona;

namespace ExpenseTracker.Parameters;

public record AddCommandParameter(
    [Required]
    [MaxLength(100)]
    [Option(Description = "Descripción del gasto")]
    string Description,

    [Option(Description = "Monto gastado")]
    [Range(0, int.MaxValue)]
    decimal Amount
) : ICommandParameterSet;