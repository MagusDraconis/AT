namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 42 — final TRM decomposition. Classify every TRM component and compute what percentage is now
/// DERIVED from AT. Per the arc: the saturation core (QG36/QG38) and the redshift (QG34, g_00 = −ρ^(2/d)) are
/// fully DERIVED; the rotation-curve term √(g_N·a0) is IMPORTED (a MOND ansatz, QG41); the temporal-propagation
/// refractive medium n = e^Φ is IMPORTED (not native, QG28; it is the ψ sector's effective description); the ψ
/// sector itself is a NEW PRIMITIVE (QG23/24/37); and Schwarzschild recovery is PARTIAL (the scalar g_00 gives the
/// time/redshift part, while γ = +1 requires ψ). No new primitives (audit only).
/// </summary>
public static class FinalTRMAudit
{
    /// <summary>The six TRM components audited.</summary>
    public static readonly string[] Components =
    {
        "saturation-core",
        "rotation-curve-term",
        "temporal-propagation",
        "psi-sector",
        "redshift",
        "schwarzschild-recovery",
    };

    /// <summary>Classification of each component.</summary>
    public static string Classify(string component) => component switch
    {
        "saturation-core" => "DERIVED",        // Poisson Q-event counting (QG36/QG38)
        "rotation-curve-term" => "IMPORTED",   // √(g_N·a0) MOND ansatz (QG41)
        "temporal-propagation" => "IMPORTED",  // refractive medium n = e^Φ (QG28)
        "psi-sector" => "NEW PRIMITIVE",       // spin-2 ψ (QG23/24/37)
        "redshift" => "DERIVED",               // g_00 = −ρ^(2/d) (QG34)
        "schwarzschild-recovery" => "PARTIAL", // scalar g_00 recovered; γ=+1 needs ψ (QG34)
        _ => throw new ArgumentOutOfRangeException(nameof(component))
    };

    /// <summary>Number of fully DERIVED components.</summary>
    public static int FullyDerivedCount()
    {
        int n = 0;
        foreach (var c in Components) if (Classify(c) == "DERIVED") n++;
        return n;
    }

    /// <summary>Number of PARTIAL components.</summary>
    public static int PartialCount()
    {
        int n = 0;
        foreach (var c in Components) if (Classify(c) == "PARTIAL") n++;
        return n;
    }

    /// <summary>Derived score = (fully DERIVED) + 0.5·(PARTIAL), as a percentage of all components.</summary>
    public static double DerivedPercentage()
    {
        double score = 0.0;
        foreach (var c in Components)
        {
            switch (Classify(c))
            {
                case "DERIVED": score += 1.0; break;
                case "PARTIAL": score += 0.5; break;
                default: break;
            }
        }
        return score / Components.Length * 100.0;
    }
}
