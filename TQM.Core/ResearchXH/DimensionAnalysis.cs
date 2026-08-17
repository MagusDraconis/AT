namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 2 — origin of spacetime dimension. The gravity chain is derived once d is supplied; here we
/// test whether any dimension is preferred or uniquely selected by actualization statistics, entropy, Einstein
/// consistency, or conformal-flatness cost. No new primitives.
/// </summary>
public static class DimensionAnalysis
{
    /// <summary>Einstein x-component prefactor G_11 = (d−1)(d−2)/2 · (σ′)².</summary>
    public static double Einstein11Prefactor(int d) => 0.5 * (d - 1.0) * (d - 2.0);

    /// <summary>Einstein transverse prefactor G_ii = (d−2)[σ″ + ((d−3)/2)(σ′)²] — leading coefficient (d−2).</summary>
    public static double EinsteinOtherPrefactor(int d) => d - 2.0;

    /// <summary>Trace G^μ_μ = −(d−2)R/2 — prefactor −(d−2)/2.</summary>
    public static double EinsteinTracePrefactor(int d) => -0.5 * (d - 2.0);

    /// <summary>Conformal weight a_d = (d+2)/(2d) (the unique exponent making M^(a_d)=ρ^(−a_d)Lρ^(−a_d) → −cΔ_g).</summary>
    public static double ConformalWeight(int d) => (d + 2.0) / (2.0 * d);

    /// <summary>Metric conformal exponent k = 2/d (from √(−g)=ρ).</summary>
    public static double MetricExponent(int d) => 2.0 / d;

    /// <summary>Flat rotation-curve value v² = |s|/d for a scale-free density ρ ∝ r^s.</summary>
    public static double FlatRotation(int d, double s) => Math.Abs(s) / d;

    /// <summary>
    /// Independent components of the Weyl tensor: 0 for d≤3 (vanishes identically), d(d+1)(d+2)(d−3)/12 for d≥4.
    /// Non-zero Weyl is the "conformal" degree of freedom frozen out by the conformal-flatness assumption.
    /// </summary>
    public static double WeylComponents(int d)
        => d < 4 ? 0.0 : d * (d + 1.0) * (d + 2.0) * (d - 3.0) / 12.0;

    /// <summary>Propagating graviton polarizations d(d−3)/2 (0 for d≤3, 2 for d=4).</summary>
    public static double GravitonPolarizations(int d)
        => d < 4 ? 0.0 : d * (d - 3.0) / 2.0;

    // ── TQM-QG Phase 3: dimension-selection "scores" ─────────────────────────────────

    /// <summary>Einstein-structure richness = independent components of the symmetric Ricci/Einstein tensor in
    /// (d+1)-dimensional spacetime: (d+1)(d+2)/2 — all determined by the single scalar ρ (monotonic in d).</summary>
    public static double EinsteinRichness(int d) => (d + 1.0) * (d + 2.0) / 2.0;

    /// <summary>Frozen fraction = graviton d.o.f. / (graviton + 1 active conformal scalar) — monotonic in d.</summary>
    public static double FrozenFraction(int d)
    {
        double g = GravitonPolarizations(d);
        return g / (g + 1.0);
    }

    /// <summary>Complexity per active degree of freedom = Einstein components per conformal scalar = (d+1)(d+2)/2.</summary>
    public static double ComplexityPerDof(int d) => EinsteinRichness(d);
}
