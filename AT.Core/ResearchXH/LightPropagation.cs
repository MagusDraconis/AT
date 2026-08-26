namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 21 — derive light propagation from AT. Tests whether light must follow null geodesics of the
/// conformally-flat metric g = ρ^(2/d)η. Null geodesics are conformally invariant, so light propagates at c and is
/// NOT bent (no lensing), but IS redshifted (g_00 = −ρ^(2/d) varies). No new primitives.
/// </summary>
public static class LightPropagation
{
    /// <summary>Effective light speed in g = ρ^(2/d)η: c = 1, INDEPENDENT of ρ (null geodesics conformally invariant).</summary>
    public static double LightSpeed(double rho) => 1.0;

    /// <summary>Gravitational redshift of a photon from ρ1 to ρ2: z = ν2/ν1 − 1 = (ρ1/ρ2)^(1/d) − 1 ≈ (1/d)ln(ρ1/ρ2).</summary>
    public static double GravitationalRedshift(double rho1, double rho2, int d)
        => Math.Pow(rho1 / rho2, 1.0 / d) - 1.0;

    /// <summary>Gravitational light BENDING (lensing deflection): ZERO — null geodesics of a conformally-flat metric
    /// are the straight lines of flat space.</summary>
    public static double LightBending() => 0.0;
}
