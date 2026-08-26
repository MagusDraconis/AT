namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 15 — spacetime fluctuations. Tests whether Poisson fluctuations in the event count δρ/ρ = 1/√N
/// propagate to metric (δg) and curvature (δR) fluctuations, and whether they are graviton-like (tensor) or
/// scalar (conformal). Deterministic (expected values) only. No new primitives.
/// </summary>
public static class SpacetimeFluctuations
{
    /// <summary>Poisson density fluctuation δρ/ρ = 1/√N (N = number of events in the region).</summary>
    public static double DensityFluctuation(double N) => 1.0 / Math.Sqrt(N);

    /// <summary>Metric fluctuation δg/g = (2/d)·(δρ/ρ) (from g = ρ^(2/d)η).</summary>
    public static double MetricFluctuation(double N, int d) => (2.0 / d) * DensityFluctuation(N);

    /// <summary>Curvature fluctuation δR/R ≈ δρ/ρ (to leading order, R is homogeneous in ρ's derivatives).</summary>
    public static double CurvatureFluctuation(double N) => DensityFluctuation(N);

    /// <summary>Trace of the metric fluctuation δg^μ_μ = (d+1)(2/d)(δρ/ρ) — NON-ZERO (pure scalar/trace).</summary>
    public static double MetricFluctuationTrace(double N, int d) => (d + 1.0) * MetricFluctuation(N, d);

    /// <summary>Traceless (graviton) part of the metric fluctuation — ZERO (conformal flatness freezes the tensor modes).</summary>
    public static double MetricFluctuationTraceless(double N, int d)
        => MetricFluctuation(N, d) - MetricFluctuationTrace(N, d) / (d + 1.0);

    /// <summary>Number of events in a region of volume V at density ρ: N = ρ·V.</summary>
    public static double EventCount(double rho, double volume) => rho * volume;
}
