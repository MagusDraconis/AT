namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 14 — Planck-regime audit. Tests whether actualization implies a natural minimum length or
/// maximum density, via maximal event density, minimum spacing, branching saturation, curvature divergence, and
/// entropy bounds. No new primitives.
/// </summary>
public static class PlanckRegime
{
    /// <summary>Curvature divergence factor ρ^(−2/d): the scalar curvature R ∝ ρ^(−2/d) diverges as ρ → 0
    /// (the metric √(−g)=ρ degenerates at the horizon).</summary>
    public static double CurvatureDivergence(double rho, int d) => Math.Pow(rho, -2.0 / d);

    /// <summary>Branching density after k generations: μ^k. Critical μ=1 is constant; μ&gt;1 diverges (runaway).</summary>
    public static double BranchingDensity(double mu, int k) => Math.Pow(mu, k);

    /// <summary>Minimum cell size for a maximum density ρ_max: ℓ = ρ_max^(−1/d) (the cell size is set by the
    /// free maximum density, not by any native constant).</summary>
    public static double MinimumCellSize(double rhoMax, int d) => Math.Pow(rhoMax, -1.0 / d);
}
