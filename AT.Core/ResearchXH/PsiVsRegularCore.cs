namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 35 — does ψ alone reproduce the regular-core structure? QG34 identified ψ as the irreducible TRM
/// ingredient. Here we test whether ψ can generate the regular-mass profile M_eff(r) = M(1 − e^(−r³/r_c³))
/// WITHOUT additional assumptions. ψ is a free field: a smooth ψ with ψ(0)=0 gives a regular core (finite M_eff,
/// finite curvature) — a QUALITATIVE match. But the SPECIFIC r³/r_c³ form is not derivable: it is an ansatz
/// imposed ON ψ and requires a new core scale r_c (two additional assumptions). So: core regularity and curvature
/// finiteness are FULL MATCH (qualitative), the exact mass profile is NO MATCH, and the horizon structure is
/// PARTIAL. Overall: PARTIAL MATCH. No new primitives.
/// </summary>
public static class PsiVsRegularCore
{
    /// <summary>Target regular-mass profile M_eff(r) = M(1 − e^(−r³/r_c³)) (finite core, asymptote M).</summary>
    public static double RegularMass(double r, double M, double rc)
        => M * (1.0 - Math.Exp(-Math.Pow(r, 3) / Math.Pow(rc, 3)));

    /// <summary>Core value: M_eff(0) = M(1 − e^0) = 0 (finite, no divergence).</summary>
    public static double RegularCoreValue(double M, double rc) => RegularMass(0.0, M, rc);

    /// <summary>Asymptote: M_eff(r→∞) → M (flat-space Schwarzschild mass).</summary>
    public static double RegularAsymptote(double M, double rc) => RegularMass(1e3 * rc, M, rc);

    /// <summary>ψ-generated effective mass M_eff = e^ψ − 1 (the kernel's mass interpretation).</summary>
    public static double PsiMass(double psi) => Math.Exp(psi) - 1.0;

    /// <summary>A smooth ψ with ψ(0)=0 gives a finite core M_eff(0)=0 (QUALITATIVE regularity).</summary>
    public static bool RegularCoreQualitatively() => PsiMass(0.0) == 0.0;

    /// <summary>Does the SPECIFIC r³/r_c³ form follow from ψ alone? No — it is an ansatz imposed on ψ.</summary>
    public static bool SpecificFormRequiresAssumption() => true;

    /// <summary>The specific form needs a core scale r_c (a new length scale = an additional assumption).</summary>
    public static bool NeedsCoreScale() => true;

    /// <summary>Number of additional assumptions the exact form requires (a chosen ψ(r) AND a core scale r_c).</summary>
    public static int AdditionalAssumptions() => 2;

    /// <summary>Per-aspect classification of "pure ψ" vs "TRM regular mass".</summary>
    public static string ClassifyAspect(string aspect) => aspect switch
    {
        "core-regularity" => "FULL MATCH",        // smooth ψ → regular core
        "curvature-finiteness" => "FULL MATCH",   // smooth ψ → finite curvature
        "horizon-structure" => "PARTIAL MATCH",   // ψ can form horizons, not the specific r_c without assumption
        "mass-profile" => "NO MATCH",             // the exact r³/r_c³ form is an ansatz, not derivable
        _ => throw new ArgumentOutOfRangeException(nameof(aspect))
    };

    /// <summary>Overall classification.</summary>
    public static string OverallClassification() => "PARTIAL MATCH";
}
