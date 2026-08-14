namespace TQM.Core.ResearchQG;

/// <summary>QG-092 causality hierarchy: the full dependency graph from primitive → causality →
/// time → change → geometry → cosmology.</summary>
public static class CausalityHierarchy
{
    public static string[] DependencyGraph() => new[]
    {
        "primitive (consistency / mathematical relation)",
        "  └─► causality (partial order ≺)",
        "        ├─► time (causal depth)",
        "        ├─► change (differences along chains)",
        "        └─► geometry (conformal metric + volume)",
        "              └─► cosmology (H = depth growth; Λ ~ 1/√N)",
    };

    /// <summary>The deepest surviving primitive after QG-092.</summary>
    public static string DeepestPrimitive =>
        "causality (the partial order) — irreducible; 'consistency' only forbids cycles";
}
