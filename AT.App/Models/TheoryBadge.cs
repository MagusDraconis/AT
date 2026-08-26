namespace AT.App.Models;

/// <summary>
/// Status badge for theory results and chapters: how strongly a result is tied to the network primitives.
/// </summary>
public enum TheoryBadge
{
    /// <summary>Derived from the primitives by a theorem / closed argument chain.</summary>
    Derived,

    /// <summary>Consistent with the network; representable without contradiction.</summary>
    Compatible,

    /// <summary>Assumed / empirical input; not derived from the network.</summary>
    Postulated,

    /// <summary>Partially related / partially derived; analogy or organizing structure only.</summary>
    Partial,

    /// <summary>Matched observation / numerical correspondence.</summary>
    Match,

    /// <summary>Uniquely selected among alternatives (preferred form), not fully derived.</summary>
    Preferred,

    /// <summary>Explicitly failed / excluded by the network analysis.</summary>
    Falsified
}
