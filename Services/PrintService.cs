using ExpenseTracker.Interfaces;

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
}
