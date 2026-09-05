namespace AT.Book.Domain;

/// <summary>
/// A book part (Preface + Parts I–VI). Text (title, subtitle) is resolved via
/// localization keys, never hardcoded.
/// </summary>
public sealed record Part(
    string Id,
    string TitleKey,
    string SubtitleKey,
    IReadOnlyList<Chapter> Chapters);

/// <summary>
/// A chapter's STRUCTURAL metadata (identity, position, calculation link, hero).
/// All prose lives in localized content files — nothing here is reader-facing text.
/// </summary>
public sealed record Chapter(
    string Id,
    string PartId,
    int Order,
    string? CalculationId,
    string HeroKind);
