namespace AT.Book.Domain;

/// <summary>A single labelled step in an executable calculation.</summary>
public sealed record CalculationStep(string Label, string Value, string? Note = null);

/// <summary>
/// The output of an executable derivation: an id, a formula, and labelled intermediate
/// values (mirroring the ResearchTestBase report convention: assumptions → intermediate
/// values → conclusions).
/// </summary>
public sealed record CalculationResult(
    string Id,
    string Title,
    string Formula,
    IReadOnlyList<CalculationStep> Steps,
    string? Conclusion = null);
