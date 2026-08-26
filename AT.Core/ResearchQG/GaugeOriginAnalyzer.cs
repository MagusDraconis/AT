using System.Globalization;

namespace AT.Core.ResearchQG;

/// <summary>
/// Hostile audit of the gauge-group origin: why SU(3)×SU(2)×U(1)? Works from the accepted chain
/// Q + Random Actualization + (ℓ,τ,ħ) + M² → Oscillation → Phase → U(1) → Particles/Defects.
/// Classifies each subgroup as Derived / Selected / Emergent / Contingent / Assumed. Rejects
/// anthropic, ecological ranking, post-selection, numerology, hidden dimensions, new primitives.
/// </summary>
public static class GaugeOriginAnalyzer
{
    // Lie-group facts (dimension, rank).
    public static (string Group, int Dim, int Rank)[] Subgroups() => new[]
    {
        ("U(1)", 1, 1),
        ("SU(2)", 3, 2),
        ("SU(3)", 8, 3),
    };

    // The derived/emergent structure behind each subgroup (from the established chain).
    public static (string Group, string Mechanism, string Classification, double Success)[] Classification() => new[]
    {
        ("U(1)", "Phase θ on S¹; Aut(S¹)=SO(2)≅U(1); π₁(S¹)=ℤ winding → integer charge",
            "DERIVED", 1.00),
        ("SU(2)", "Binary winding {n=+1,n=−1} → 2-level doublet; Bloch sphere S² → SO(3)=SU(2)/Z₂ → spinor double-cover SU(2)",
            "EMERGENT (the 2 is the minimal winding pair)", 0.70),
        ("SU(3)", "Tri-winding n=3 → 3 bound vortex substructures; Aut(C³/S₃) ⊇ U(3) ⊃ SU(3)",
            "CONTINGENT (the 3 is the underived count; full algebra borrowed)", 0.10),
    };

    /// <summary>Key facts for the report.</summary>
    public static string Facts()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("  π₁(S¹) = ℤ  (infinite winding — topology gives NO specific n)");
        sb.AppendLine("  Aut(S¹) = O(2) ⊃ SO(2) ≅ U(1)  (theorem)");
        sb.AppendLine("  SO(3) ≅ SU(2)/Z₂  (spinor double cover, theorem)");
        sb.AppendLine("  Aut(Cⁿ/Sₙ) ⊇ U(n) ⊃ SU(n)  (defect-moduli automorphism, structure only)");
        sb.AppendLine("  rank pattern: U(1)=1, SU(2)=2, SU(3)=3  (= winding sectors 1,2,3 — 'numerology'?)");
        return sb.ToString();
    }

    /// <summary>The four derivation searches and their verdicts.</summary>
    public static (string Route, string Result, string Verdict)[] DerivationRoutes() => new[]
    {
        ("Topology-only", "π₁(S¹)=ℤ gives INFINITE winding; no topological principle fixes n=2 or n=3",
            "FAILS for SU(2)/SU(3) dimensions; only U(1) is topology-derived"),
        ("Attractor-space", "gauge group = Aut(attractor space); but attractor landscape CONTENT is contingent (flavor audit)",
            "FAILS to fix n; the specific n is contingent"),
        ("Defect-moduli", "Aut(moduli of n defects) ⊇ SU(n); U(1)=Aut(S¹), SU(2)=Aut(2-vortex)→SO(3)→SU(2), SU(3)=Aut(3-vortex)→U(3)",
            "STRONGEST: derives the group STRUCTURE from defect count n, but n is input"),
        ("Persistence/symmetry", "classical groups are all persistent; persistence does not SELECT which n",
            "FAILS to select; gives no preference among SU(n)"),
    };

    /// <summary>Six rejected fallacies.</summary>
    public static string[] RejectedFallacies() => new[]
    {
        "ANTHROPIC — 'n=3 for stable nuclei' explains nothing; no observer requires SU(3) specifically. REJECTED.",
        "ECOLOGICAL RANKING — 'SM ranks #1 by niche fitness' (X056) is selection, not derivation. REJECTED.",
        "POST-SELECTION — 'we observe the group we live in' is unfalsifiable conditioning. REJECTED.",
        "NUMEROLOGY — the 1-2-3 rank=winding pattern is a coincidence without a mechanism. REJECTED as derivation.",
        "HIDDEN DIMENSIONS — extra compact dims to house SU(3) is a new primitive in disguise. REJECTED.",
        "NEW PRIMITIVES — a 'gauge-group primitive' is forbidden by the accepted hierarchy. REJECTED.",
    };
}
