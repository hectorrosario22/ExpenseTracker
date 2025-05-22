using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces;

public interface IExportService
{
    Task<string> ToCsv(List<Expense> expenses);
}
