namespace AT.Book.Services.Localization;

/// <summary>A supported language: an ISO code plus its native display name.</summary>
public sealed record Language(string Code, string NativeName);

/// <summary>
/// The catalog of supported languages. To add a language, add a <see cref="Language"/>
/// here and a matching wwwroot/Content/{code}/ directory (shared.json + chapters.json).
/// Nothing else needs to change.
/// </summary>
public static class LanguageCatalog
{
    public static readonly IReadOnlyList<Language> All =
    [
        new("en", "English"),
        new("de", "Deutsch"),
    ];

    public static string DefaultCode => "en";

    public static bool IsSupported(string? code) => code is not null && All.Any(l => l.Code == code);

    public static string NativeName(string? code)
        => All.FirstOrDefault(l => l.Code == code)?.NativeName ?? DefaultCode;
}
