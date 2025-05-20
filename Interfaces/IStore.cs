namespace ExpenseTracker.Interfaces;

public interface IStore
{
    Task Save<T>(T data);
    Task<List<T>> Load<T>();
}