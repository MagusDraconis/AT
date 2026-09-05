namespace AT.Book.Domain;

/// <summary>How strongly a theory object is tied to the canonical primitives.</summary>
public enum TheoryClassification
{
    Derived,
    Emergent,
    Boundary,
    Correspondence,
    NewPrimitive,
    Refuted,
    Postulated,
    Partial,
}

/// <summary>The six theory layers of the AT book, foundation-first.</summary>
public enum TheoryLayer
{
    Foundations = 0,
    Structure = 1,
    Information = 2,
    Cosmology = 3,
    Physics = 4,
    Correspondence = 5,
}

/// <summary>The status of a research audit.</summary>
public enum AuditStatus
{
    Passed,
    Partial,
    Failed,
    Open,
}

/// <summary>The kind of a theory object (drives its rendering).</summary>
public enum TheoryObjectKind
{
    Chapter,
    Definition,
    Theorem,
    Derivation,
    Primitive,
    Boundary,
    Calculation,
}
