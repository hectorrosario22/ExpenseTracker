using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services;

public class PrintService : IPrintService
{
    public void Success(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Failed(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Table(List<Expense> expenses)
    {
        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses found.");
            return;
        }

        string[] headers = ["ID", "Date", "Description", "Amount", "Category"];

        int widthID = Math.Max(headers[0].Length, expenses.Max(t => t.Id.ToString().Length));
        int widthDescription = Math.Max(headers[2].Length, expenses.Max(t => t.Description.Length));
        int widthAmount = Math.Max(headers[3].Length, expenses.Max(t => t.Amount.ToString("C2").Length));

        string format = $"{{0,-{widthID}}} | {{1,-10}} | {{2,-{widthDescription}}} | {{3,-{widthAmount}}} | {{4}}";
        Console.WriteLine(format, headers[0], headers[1], headers[2], headers[3], headers[4]);

        foreach (var task in expenses)
        {
            Console.WriteLine(
                format,
                task.Id,
                task.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd"),
                task.Description,
                task.Amount.ToString("C2"),
                string.Join(", ", task.Categories)
            );
        }
    }

    public void Default(string message)
    {
        Console.WriteLine(message);
    }

    public void Warning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
