namespace AT.Core.ResearchQG;

/// <summary>QG-089 rate-driven redshift: 1+z = exp(∫ R dt) between emission and observation.
/// This is exactly the FLRW redshift 1+z = a_obs/a_emit (since R = d ln a/dt), so redshift
/// expressed through integrated rate evolution is EQUIVALENT to metric-expansion redshift.</summary>
public static class RateDrivenRedshift
{
    /// <summary>Redshift from integrated rate: 1+z = a_obs/a_emit = a(0)/a(z) = 1/a(z).</summary>
    public static double RedshiftFromRate(double zEmit)
        => 1.0 / CosmicRateModel.ScaleFactor(zEmit) - 1.0;

    /// <summary>True iff the rate-derived redshift equals the observed (always, by construction).</summary>
    public static bool EquivalentToFlrw(double z, double tol = 1e-6)
        => Math.Abs(RedshiftFromRate(z) - z) < tol;
}
