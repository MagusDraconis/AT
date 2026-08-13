namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-080 TimeDrivenRedshift: cosmological redshift and time dilation derived purely
/// from the evolving physical clock γ(t), with no metric expansion of space (static
/// comoving coordinates, da/dτ = 0 in physical time).
///
/// Derivation: an atomic transition has a universal physical frequency ν₀. Its
/// coordinate frequency at emission is ν₀·γ_emit; propagating through static space the
/// coordinate frequency is conserved; the observer measures ν₀·γ_emit/γ_obs in physical
/// time. Hence 1+z = ν₀/(ν₀ γ_emit/γ_obs) = γ_obs/γ_emit.
/// </summary>
public static class TimeDrivenRedshift
{
    /// <summary>Redshift from clock rates: 1+z = γ_obs/γ_emit.</summary>
    public static double Redshift(double gammaEmit, double gammaObs) => gammaObs / gammaEmit - 1.0;

    /// <summary>Time dilation factor Δτ_obs/Δτ_emit for a static-space evolving clock.</summary>
    public static double TimeDilationFactor(double gammaEmit, double gammaObs) => gammaObs / gammaEmit;

    /// <summary>TSC redshift with γ = a (weak TSC), must equal 1+z.</summary>
    public static double WeakTscRedshift(double zEmit) => Redshift(Cosmology.ScaleFactor(zEmit), 1.0);

    /// <summary>
    /// "Strong" (naive) TSC: static space with clocks FASTER in the past, i.e. the
    /// alternative sign convention γ_emit > γ_obs. This yields the inverse time dilation
    /// 1/(1+z), which SN Ia observations falsify.
    /// </summary>
    public static double StrongTscTimeDilation(double zEmit)
        => Redshift(1.0, Cosmology.ScaleFactor(zEmit)); // γ_emit=1, γ_obs=a → factor a=1/(1+z)
}
