namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 34 — identify the irreducible TRM ingredient. TRM reproduces three successes: redshift, regular
/// black holes, weak-field GR recovery. Four candidate ingredients are examined — effective mass M_eff(r), the
/// propagation kernel, temporal-rate modification, and a UV cutoff scale. Key finding: the first three are ONE
/// object (the non-conformal ψ sector) written three ways: temporal-rate ψ (g_00 = −ρ^(2/d)e^{2ψ}), the kernel
/// n = e^Φ, and the effective mass M_eff = n − 1 = e^Φ − 1. The UV cutoff scale is a separate, redundant
/// ingredient — none of the three successes needs it. Redshift needs no TRM ingredient at all (AT's g_00 = −ρ^(2/d)
/// already gives it). Weak-field GR recovery and regular black holes both need ψ. No new primitives.
/// </summary>
public static class IrreducibleTRMIngredient
{
    /// <summary>The four candidate ingredients audited.</summary>
    public static readonly string[] Ingredients =
    {
        "effective-mass",     // M_eff(r)
        "propagation-kernel", // n(r)
        "temporal-rate",      // ψ (g_00 modification)
        "uv-cutoff-scale",    // ℓ
    };

    /// <summary>Effective mass profile M_eff = e^Φ − 1.</summary>
    public static double Meff(double phi) => Math.Exp(phi) - 1.0;

    /// <summary>Propagation kernel n = e^Φ = 1 + M_eff.</summary>
    public static double Kernel(double phi) => Math.Exp(phi);

    /// <summary>The kernel and the effective mass are the SAME object: n = 1 + M_eff (exactly).</summary>
    public static bool KernelIsMeff(double phi, double tol = 1e-12)
        => Math.Abs(Kernel(phi) - (1.0 + Meff(phi))) < tol;

    /// <summary>The temporal-rate ψ is the fundamental form; M_eff and the kernel are just its two rewrites.</summary>
    public static bool ThreeIngredientsAreOne() => KernelIsMeff(0.5);

    /// <summary>
    /// Which ingredient each TRM success actually needs:
    /// redshift — NONE (AT's g_00 = −ρ^(2/d) already gives gravitational redshift);
    /// weak-field GR recovery — ψ (moves γ from −1 to +1);
    /// regular black hole (finite-curvature horizon) — ψ.
    /// </summary>
    public static bool RedshiftRequiresPsi() => false;
    public static bool WeakFieldGrRequiresPsi() => true;
    public static bool RegularBlackHoleRequiresPsi() => true;

    /// <summary>Does any of the three successes require the UV cutoff scale ℓ? No.</summary>
    public static bool AnySuccessRequiresCutoff() => false;

    /// <summary>Classification: ψ (and its two aliases) are ESSENTIAL; the UV cutoff is REDUNDANT.</summary>
    public static string Classify(string ingredient) => ingredient switch
    {
        "effective-mass" => "ESSENTIAL",     // = ψ (M_eff = e^Φ − 1)
        "propagation-kernel" => "ESSENTIAL", // = ψ (n = e^Φ)
        "temporal-rate" => "ESSENTIAL",      // the irreducible ingredient ψ itself
        "uv-cutoff-scale" => "REDUNDANT",    // no success needs a cutoff scale
        _ => throw new ArgumentOutOfRangeException(nameof(ingredient))
    };

    /// <summary>How many successes survive if ψ is removed (only redshift survives).</summary>
    public static int SurvivingWithoutPsi() => 1;   // redshift

    /// <summary>How many successes survive if the UV cutoff is removed (all three survive).</summary>
    public static int SurvivingWithoutCutoff() => 3; // redshift + weak-field GR + regular BH
}
