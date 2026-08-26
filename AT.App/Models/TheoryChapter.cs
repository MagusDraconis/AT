namespace AT.App.Models;

/// <summary>
/// A chapter of the theory book: a self-contained topic with a summary, key results, and the AT-QG
/// phase IDs that establish it. Chapters are attached to a section by slug.
/// </summary>
public sealed record TheoryChapter(
    string Slug,
    string Title,
    string Summary,
    IReadOnlyList<TheoryResult> Results);
