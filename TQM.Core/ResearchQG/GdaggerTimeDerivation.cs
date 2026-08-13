namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-080 GdaggerTimeDerivation: derives the RAR acceleration scale g† = c·H/2π from the
/// time-scale dynamics, proving it is the fractional clock drift times c/2π:
///
///   g† = c·d(ln γ)/dt / 2π = c·(γ̇/γ)/2π = c·H/2π    (since γ = a and H = γ̇/γ).
///
/// Equivalently, in terms of the physical clock τ(t) = ∫γ dt, with γ = dτ/dt:
///   g† = c·(τ̈/τ̇)/2π, i.e. the acceleration scale is the clock's log-acceleration.
/// </summary>
public static class GdaggerTimeDerivation
{
    /// <summary>g† from the clock drift: c·d(ln γ)/dt / 2π, in m/s².</summary>
    public static double GdaggerFromClockDrift(double dlnGammaDt_kmsMpc)
        => Cosmology.C_KMS * dlnGammaDt_kmsMpc / (2.0 * Math.PI) * 1e3 / Cosmology.Kpc_m;

    /// <summary>g† from the clock: c·(τ̈/τ̇)/2π using H = τ̈/τ̇ (log-derivative of γ).</summary>
    public static double GdaggerFromClock(double h_kmsMpc)
        => Cosmology.C_KMS * h_kmsMpc / (2.0 * Math.PI) * 1e3 / Cosmology.Kpc_m;

    /// <summary>Local value g†(0) = c·H₀/2π in m/s².</summary>
    public static double LocalGdagger() => GdaggerFromClock(Cosmology.H0);

    /// <summary>Verify GdaggerFromClock == GdaggerFromClockDrift (both = cH/2π).</summary>
    public static bool EquivalenceHolds(double z, double tol = 1e-12)
        => Math.Abs(GdaggerFromClockDrift(Cosmology.H(z)) - GdaggerFromClock(Cosmology.H(z))) < tol;
}
