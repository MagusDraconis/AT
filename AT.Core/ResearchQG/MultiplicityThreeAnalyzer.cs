using System.Globalization;
using System.Text;

namespace AT.Core.ResearchQG;

/// <summary>
/// Hostile audit of the recurring integer 3 (spatial dims, generations, color, defect hierarchy,
/// dim(G)) treated as one multiplicity variable N. Determines whether N=3 is derived, selected,
/// emergent, or contingent. Rejects topology-only, anthropics, numerology, hidden parameters,
/// new primitives.
/// </summary>
public static class MultiplicityThreeAnalyzer
{
    // Occurrences of "3" and their known status.
    public static (string Occurrence, string Space, string Status, string Basis)[] Occurrences() => new[]
    {
        ("spatial dimensions (3+1)", "spacetime", "DERIVED",
            "complexity maximization peaks at M²≈5 → d=3+1 (X042, XE009)"),
        ("generations (3)", "generation space G", "SELECTED",
            "stability cutoff → 3 observable excitations (X051; 5/6 models)"),
        ("color (SU(3), n=3)", "color SU(3)", "CONTINGENT/SELECTED",
            "tri-winding n=3; 8-gluon algebra borrowed (QG-038)"),
        ("defect hierarchy (n=3 confinement)", "defect moduli", "CONTINGENT",
            "n=3 bound vortex substructure (QG-034)"),
        ("dim(G) = 3", "generation space G", "SELECTED",
            "CP lower bound N≥3 ∩ Z-width/Higgs upper N≤3 (QG-053/067)"),
    };

    /// <summary>Number of CP-violating phases in an N×N unitary mixing matrix: (N-1)(N-2)/2.</summary>
    public static double CPPhases(int n) => (n - 1.0) * (n - 2.0) / 2.0;

    /// <summary>The derived lower bound: CP violation requires N≥3 (the first N with a complex phase).</summary>
    public static int DerivedLowerBound()
    {
        for (int n = 1; n <= 6; n++)
            if (CPPhases(n) >= 1.0) return n;
        return -1;
    }

    /// <summary>Bifurcation/catastrophe counts of stable branches (none gives exactly 3 stable).</summary>
    public static (string Catastrophe, int StableBranches)[] Bifurcations() => new[]
    {
        ("fold", 1),
        ("pitchfork", 2),
        ("cusp", 2),        // 2 stable + 1 unstable
        ("butterfly", 3),   // 3 stable + 2 unstable — but requires 2 parameters (codim-2)
    };

    /// <summary>The five rejected fallacies.</summary>
    public static string[] RejectedFallacies() => new[]
    {
        "TOPOLOGY-ONLY — π₁(S¹)=ℤ gives INFINITE winding; no topological principle fixes N=3. REJECTED.",
        "ANTHROPICS — 'N=3 for stable atoms/nuclei' is selection, not derivation. REJECTED.",
        "NUMEROLOGY — the 1-2-3 rank=winding and 3=3=3=3 coincidences lack a mechanism. REJECTED.",
        "HIDDEN PARAMETERS — a 'multiplicity parameter' to force N=3 is forbidden. REJECTED.",
        "NEW PRIMITIVES — N=3 as a primitive is forbidden by the accepted hierarchy. REJECTED.",
    };

    /// <summary>Does the internal multiplicity inherit the spatial 3?</summary>
    public static bool InternalInheritsSpatial => false; // no known mechanism links spacetime 3 to internal 3
}
