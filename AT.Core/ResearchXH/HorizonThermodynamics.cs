namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 13 — horizon thermodynamics. Tests whether a Hawking-like temperature T ∝ 1/R emerges from the
/// first law T = dE/dS, using the counting-measure entropy S ∝ R^(d−1) (area, QG12) and the deficit energy
/// E ∝ R^d (volume) vs the Schwarzschild E ∝ R. d = spatial dimension. No new primitives.
/// </summary>
public static class HorizonThermodynamics
{
    /// <summary>Entropy S ∝ R^(d−1) (horizon area).</summary>
    public static double Entropy(int d, double R) => Math.Pow(R, d - 1.0);

    /// <summary>Entropy gradient dS/dR = (d−1) R^(d−2).</summary>
    public static double EntropyGradient(int d, double R) => (d - 1.0) * Math.Pow(R, d - 2.0);

    /// <summary>Deficit energy (enclosed deficit mass) E ∝ R^d (volume).</summary>
    public static double DeficitEnergy(int d, double R) => Math.Pow(R, d);

    /// <summary>Schwarzschild-like energy E ∝ R (mass linear in radius).</summary>
    public static double SchwarzschildEnergy(double R) => R;

    /// <summary>AT temperature T = dE/dS with E ∝ R^d: T = d/(d−1) · R  (GROWS with R — anti-Hawking).</summary>
    public static double TemperatureDeficit(int d, double R)
        => (d * Math.Pow(R, d - 1.0)) / ((d - 1.0) * Math.Pow(R, d - 2.0));

    /// <summary>Hawking-like temperature with E ∝ R and S ∝ R^(d−1): T = 1/((d−1) R^(d−2)) (∝ 1/R for d=3).</summary>
    public static double TemperatureHawking(int d, double R)
        => 1.0 / ((d - 1.0) * Math.Pow(R, d - 2.0));
}
