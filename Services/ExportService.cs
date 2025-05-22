using System.Text;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services;

public class ExportService : IExportService
{
    public async Task<string> ToCsv(List<Expense> expenses)
    {
        StringBuilder csvBuilder = new();
        csvBuilder.AppendLine("ID,Date,Description,Amount,Categories");

        foreach (var expense in expenses)
        {
            var categories = string.Join(";", expense.Categories);
            csvBuilder.AppendLine($"{expense.Id},{expense.CreatedAt:yyyy-MM-dd},{expense.Description},{expense.Amount},{categories}");
        }

        var csv = csvBuilder.ToString();
        var filename = $"expenses_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.csv";
        var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

        await File.WriteAllTextAsync(filepath, csv);
        return filepath;
    }
}
