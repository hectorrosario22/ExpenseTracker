namespace ExpenseTracker.Interfaces;

public interface IStore
{
    Task Save<T>(string sectionName, T data);
    Task<T?> Load<T>(string sectionName);
}