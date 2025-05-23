using System.Text.Json;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Services;

public class JsonStore : IStore
{
    private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db.json");
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
            File.WriteAllText(_filePath, "{}");
        }
    }

    public async Task<T?> Load<T>(string sectionName)
    {
        using FileStream stream = File.Open(
            _filePath, FileMode.Open,
            FileAccess.Read, FileShare.Read
        );

        var document = await JsonDocument.ParseAsync(stream);
        var root = document.RootElement;
        if (!root.TryGetProperty(sectionName, out var section))
        {
            return default;
        }

        var sectionJson = root.GetProperty(sectionName).GetRawText();
        var data = JsonSerializer.Deserialize<T>(sectionJson, _jsonOptions);
        return data;
    }

    public async Task Save<T>(string sectionName, T data)
    {
        // Read the existing JSON file
        Dictionary<string, JsonElement> jsonContent;
        using (FileStream readStream = File.OpenRead(_filePath))
        {
            jsonContent = await JsonSerializer.DeserializeAsync<Dictionary<string, JsonElement>>(readStream)
                ?? [];
        }

        // Convert the updated data to JSON
        JsonElement updatedSection;
        using (MemoryStream ms = new())
        {
            await JsonSerializer.SerializeAsync(ms, data, _jsonOptions);
            ms.Position = 0;
            updatedSection = await JsonSerializer.DeserializeAsync<JsonElement>(ms);
        }

        // Update the section in the JSON content
        jsonContent[sectionName] = updatedSection;

        using FileStream writeStream = File.Open(
            _filePath, FileMode.Create,
            FileAccess.Write, FileShare.None
        );
        await JsonSerializer.SerializeAsync(writeStream, jsonContent, _jsonOptions);
    }
}