namespace TQM.App.Models;

/// <summary>
/// A single key result of a theory chapter: a headline, a short explanation, a status badge, and the
/// TQM-QG phase IDs that establish it. Phases are attached by ID so future phases can extend a chapter
/// without structural changes.
/// </summary>
public sealed record TheoryResult(
    string Title,
    string Description,
    TheoryBadge Badge,
    IReadOnlyList<string> PhaseIds);
