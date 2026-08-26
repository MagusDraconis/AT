namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 28 — derive the light-propagation law. QG27 showed null geodesics give NO lensing while the TRM
/// kernel gives GR-like lensing. Here we ask which rule follows from ACTUALIZATION DYNAMICS. Key fact: the causal
/// order determines the CONFORMAL CLASS (the light cone), and the counting measure ρ supplies only the conformal
/// factor ρ^(2/d) — a conformal rescaling that leaves the light cone invariant. Hence light propagates along the
/// causal-order light cone (null geodesics, n = 1), independent of ρ. The TRM effective index n = e^Φ = ρ^(1/d)
/// requires ignoring the spatial g_ii — a temporal-only assumption NOT in AT's primitives (it is the ψ ≠ 0
/// non-conformal sector in disguise). No new primitives.
/// </summary>
public static class PropagationLaw
{
    /// <summary>Conformal invariance of null geodesics: g → Ω²g leaves the light cone unchanged (factor 1).</summary>
    public static double ConformalInvariance() => 1.0;

    /// <summary>Null-geodesic effective index from the FULL conformal metric g = ρ^(2/d)η:
    /// n = √(g_ii/(−g_00)) = √(ρ^(2/d)/ρ^(2/d)) = 1, independent of ρ.</summary>
    public static double NullGeodesicIndex() => 1.0;

    /// <summary>TRM temporal-only effective index n = e^Φ = ρ^(1/d) (ignores the spatial g_ii).</summary>
    public static double TrmEffectiveIndex(int d, double rho) => Math.Pow(rho, 1.0 / d);

    /// <summary>Candidate mechanisms that ARE native to actualization (each yields the conformal class only → n = 1).</summary>
    public static readonly string[] NativeMechanisms =
    {
        "event-to-event",      // signal propagates along the causal order → light cone = conformal class
        "branching-path",      // branching statistics give ρ (the conformal factor), not a refractive index
        "correlation-kernel",  // event correlations give ⟨ρρ⟩, not a medium for light
        "null-geodesic-limit", // the conformal factor cancels exactly (n = 1)
    };

    /// <summary>Candidate mechanism that is IMPORTED (the temporal-only effective-medium assumption → n = e^Φ).</summary>
    public static readonly string[] ImportedMechanisms =
    {
        "effective-refractive-index",
    };

    public static bool IsNative(string mechanism) => Array.IndexOf(NativeMechanisms, mechanism) >= 0;
    public static bool IsImported(string mechanism) => Array.IndexOf(ImportedMechanisms, mechanism) >= 0;

    /// <summary>Lensing requires a spatially-varying index n(x) ≠ const.</summary>
    public static bool ProducesLensing(double nOverdensity, double nVacuum)
        => Math.Abs(nOverdensity - nVacuum) > 1e-12;
}
