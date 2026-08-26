namespace AT.Core.ResearchXH;

/// <summary>
/// AT-F Phase 3 — derive the metric origin √(−g)=ρ. Tests whether the identification of the counting measure
/// with the metric volume element emerges uniquely from counting-measure consistency (invariant counting, volume
/// preservation, causal-set "number = volume"). No new primitives.
/// </summary>
public static class MetricOrigin
{
    /// <summary>Standard profile ρ = 1 + x².</summary>
    public static double Profile(double x) => 1.0 + x * x;

    /// <summary>Counting measure N[a,b] = ∫_a^b ρ(x) dx (a coordinate-invariant count).</summary>
    public static double Count(Func<double, double> rho, double a, double b, int n = 20000)
    {
        double dx = (b - a) / n;
        double s = 0.0;
        for (int i = 0; i < n; i++) s += rho(a + (i + 0.5) * dx) * dx;
        return s;
    }

    /// <summary>Metric volume V[a,b] = ∫_a^b √(−g)(x) dx.</summary>
    public static double Volume(Func<double, double> sqrtMinusG, double a, double b, int n = 20000)
    {
        double dx = (b - a) / n;
        double s = 0.0;
        for (int i = 0; i < n; i++) s += sqrtMinusG(a + (i + 0.5) * dx) * dx;
        return s;
    }

    // Volume-element candidates (all give the same volume measure only if equal to ρ):
    /// <summary>√(−g) = ρ (the metric origin).</summary>
    public static double SqrtMinusG_Rho(double x) => Profile(x);

    /// <summary>√(−g) = ρ² (alternative).</summary>
    public static double SqrtMinusG_RhoSq(double x) { double p = Profile(x); return p * p; }

    /// <summary>√(−g) = √ρ (alternative).</summary>
    public static double SqrtMinusG_SqrtRho(double x) => Math.Sqrt(Profile(x));

    /// <summary>√(−g) = 1 (constant volume element).</summary>
    public static double SqrtMinusG_Const(double x) => 1.0;

    /// <summary>Maximum relative mismatch max_L |V[0,L] − N[0,L]| / N[0,L] between the metric volume and the count.</summary>
    public static double Mismatch(Func<double, double> rho, Func<double, double> sqrtMinusG, double[] Ls)
    {
        double worst = 0.0;
        foreach (double L in Ls)
        {
            double nCount = Count(rho, 0.0, L);
            double v = Volume(sqrtMinusG, 0.0, L);
            double rel = Math.Abs(v - nCount) / Math.Max(nCount, 1e-12);
            if (rel > worst) worst = rel;
        }
        return worst;
    }
}
