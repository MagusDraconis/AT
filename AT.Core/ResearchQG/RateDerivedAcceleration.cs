namespace AT.Core.ResearchQG;

/// <summary>QG-089 rate-derived acceleration: a₀ = c·R. For R = H this gives the 'cH class'
/// a₀ = cH = 6.5e-10 m/s² (no 1/2π); for R = H/2π (the angular-frequency/cycle rate) it gives
/// g† = cH/2π = 1.04e-10 m/s². The rate-first origin therefore spans the same cH vs cH/2π
/// ambiguity already resolved in QG-084/085.</summary>
public static class RateDerivedAcceleration
{
    public static double A0FromRate(double ratePerS) => CosmicRateModel.C * ratePerS;

    public static double CH() => A0FromRate(CosmicRateModel.H0PerS);

    public static double Gdagger() => A0FromRate(CosmicRateModel.H0PerS / (2.0 * Math.PI));
}
