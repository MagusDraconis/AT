namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 27 — TRM/AT observable bridge. QG21/26 used GR null-geodesic optics and found NO lensing (γ=−1).
/// TRM instead reproduced lensing-like effects through EFFECTIVE PROPAGATION (the time-rate field acting as a
/// refractive medium). This audit compares three light-propagation prescriptions for the SAME weak-field ρ with
/// potential Φ = (1/d)ln ρ, via the "temporal fraction" t ∈ [0,1]:
///   t = 0 → full conformal metric (g_00 AND g_ii) → index n = 1 (the conformal factor cancels) → NO lensing
///   t = 1 → temporal-only (g_00 alone, ignoring g_ii) → index n = e^Φ → FULL GR lensing (TRM effective medium)
/// Every lensing observable scales linearly in t. No new primitives — t is a diagnostic interpolator, not physics.
/// </summary>
public static class TRMObservableBridge
{
    /// <summary>AT geometry: full conformal metric (t=0) — the conformal factor cancels in null propagation.</summary>
    public static double AtGeometryFraction() => 0.0;

    /// <summary>TRM effective propagation: temporal-only optics (t=1) — the time-rate field is the refractive medium.</summary>
    public static double TrmEffectiveFraction() => 1.0;

    /// <summary>Effective refractive index n = e^(t·Φ) (temporal fraction t of the weak-field potential Φ).</summary>
    public static double EffectiveIndex(double phi, double t) => Math.Exp(t * phi);

    /// <summary>Light deflection α = 4GM/b · t (units GM/bc²); equals the Einstein deflection 4GM/bc² at t=1.</summary>
    public static double Deflection(double gm, double t) => 4.0 * gm * t;

    /// <summary>Shapiro time delay Δt = 2GM/c³ ln(...) · t (units GM/c³ · ln); equals the GR value at t=1.</summary>
    public static double ShapiroDelay(double gmLog, double t) => 2.0 * gmLog * t;

    /// <summary>Lensing convergence κ = Σ · t (surface density Σ in units of Σ_crit).</summary>
    public static double Convergence(double surfaceDensity, double t) => surfaceDensity * t;

    /// <summary>Lensing shear γ_s = Σ_shear · t.</summary>
    public static double Shear(double shearDensity, double t) => shearDensity * t;

    /// <summary>Magnification μ = 1/[(1−κ)² − γ_s²].</summary>
    public static double Magnification(double kappa, double shear)
        => 1.0 / ((1.0 - kappa) * (1.0 - kappa) - shear * shear);
}
