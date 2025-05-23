using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces;

public interface IPrintService
{
    void Success(string message);
    void Failed(string message);
    void Table(List<Expense> expenses);
    void Default(string message);
    void Warning(string message);
}
