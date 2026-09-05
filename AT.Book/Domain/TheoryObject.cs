namespace AT.Book.Domain;

/// <summary>
/// A concrete theory object: chapter, definition, theorem, derivation, primitive,
/// boundary, or calculation. Narrative and formula are optional; a CalculationId links
/// the object to an executable derivation in a <see cref="Services.Calculations.ICalculationService"/>.
/// </summary>
public sealed record TheoryObject(
    string Id,
    string Title,
    string Summary,
    TheoryLayer Layer,
    TheoryClassification Classification,
    TheoryObjectKind Kind,
    IReadOnlyList<string> Dependencies,
    string? Narrative = null,
    string? Formula = null,
    string? CalculationId = null,
    IReadOnlyList<string>? References = null,
    IReadOnlyList<string>? AuditIds = null) : ITheoryObject;
