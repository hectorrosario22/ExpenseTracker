using System.Text.Json;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Services;

public class JsonStore : IStore
{
    private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "expenses.json");
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        AllowTrailingCommas = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };

    public JsonStore()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    public async Task<List<T>> Load<T>()
    {
        using FileStream stream = File.Open(
            _filePath, FileMode.Open,
            FileAccess.Read, FileShare.Read
        );
        var items = await JsonSerializer.DeserializeAsync<List<T>>(stream, _jsonOptions);
        return items ?? [];
    }

    public async Task Save<T>(T data)
    {
        using FileStream stream = File.Open(
            _filePath, FileMode.Create,
            FileAccess.Write, FileShare.None
        );
        await JsonSerializer.SerializeAsync(stream, data, _jsonOptions);
    }
}