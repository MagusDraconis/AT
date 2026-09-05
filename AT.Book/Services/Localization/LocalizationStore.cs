using System.Text.Json;

namespace AT.Book.Services.Localization;

/// <summary>
/// Loads all localized content files (wwwroot/Content/{culture}/*.json) into memory at
/// startup. This is the single store every localizer reads from — chapter prose and UI
/// chrome are both resolved here, never hardcoded.
/// </summary>
public sealed class LocalizationStore
{
    private readonly IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> _byCulture;

    public IReadOnlyList<string> SupportedCultures { get; }

    public LocalizationStore(IWebHostEnvironment env)
    {
        SupportedCultures = new[] { "en", "de" };
        var store = new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var culture in SupportedCultures)
        {
            var dir = Path.Combine(env.WebRootPath, "Content", culture);
            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException($"Localized content directory not found: {dir}");

            var merged = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var file in Directory.GetFiles(dir, "*.json"))
            {
                var text = File.ReadAllText(file);
                var entries = JsonSerializer.Deserialize<Dictionary<string, string>>(text)
                    ?? throw new InvalidOperationException($"Empty or invalid localized content file: {file}");
                foreach (var kv in entries)
                    merged[kv.Key] = kv.Value;
            }
            store[culture] = merged;
        }

        _byCulture = store;
    }

    public string Get(string culture, string key)
    {
        if (_byCulture.TryGetValue(culture, out var entries) && entries.TryGetValue(key, out var value))
            return value;
        if (_byCulture.TryGetValue("en", out var en) && en.TryGetValue(key, out var enValue))
            return enValue;
        return key;
    }
}
