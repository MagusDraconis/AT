namespace AT.App.Models;

/// <summary>
/// A top-level section of the theory book (Introduction, Gravity, Quantum Sector, ...). Carries a
/// summary, the key results of the section, linked chapters, and the AT-QG phase IDs that establish it.
/// Slugs drive both the route (e.g. /theory/gravity) and the navigation tree.
/// </summary>
public sealed record TheorySection(
    string Slug,
    string Title,
    string Subtitle,
    string Summary,
    IReadOnlyList<TheoryResult> KeyResults,
    IReadOnlyList<TheoryChapter> Chapters);
