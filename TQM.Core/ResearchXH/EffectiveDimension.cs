namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 4 — fundamental vs observed dimension. Tests whether TQM can be fundamentally D-dimensional
/// with only an effective d-dimensional observable sector, via dimensional reduction, observable submanifolds,
/// information projection, and causal accessibility. No new primitives.
/// </summary>
public static class EffectiveDimension
{
    /// <summary>Non-trivial Einstein components in the observable d×d block = d(d+1)/2 (the symmetric block
    /// where the counting measure ρ actually varies).</summary>
    public static double ObservableEinsteinComponents(int d) => d * (d + 1.0) / 2.0;

    /// <summary>Total Einstein components in D-dimensional spacetime = D(D+1)/2.</summary>
    public static double TotalEinsteinComponents(int D) => D * (D + 1.0) / 2.0;

    /// <summary>Fraction of the Einstein structure observable from a d-dim submanifold of D-dim geometry.</summary>
    public static double ObservableFraction(int D, int d) => ObservableEinsteinComponents(d) / TotalEinsteinComponents(D);

    /// <summary>Number of frozen (trivial) transverse directions carrying no matter/curvature.</summary>
    public static double TransverseDirections(int D, int d) => D - d;

    /// <summary>
    /// Observable volume-element exponent under reduction: restricting g = ρ^(2/D)η_D to a d-dim submanifold
    /// gives √(−g_eff) = ρ^(d/D). This equals ρ only when d = D (counting-measure consistency is dimension-specific).
    /// </summary>
    public static double EffectiveVolumeExponent(int D, int d) => (double)d / D;

    /// <summary>Metric-origin mismatch |2/D − 2/d| (the fundamental conformal exponent vs the observable one).</summary>
    public static double MetricOriginMismatch(int D, int d) => Math.Abs(2.0 / D - 2.0 / d);
}
