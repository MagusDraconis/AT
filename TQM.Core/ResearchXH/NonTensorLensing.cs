namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 26 — non-tensor explanation of lensing. Given QG25 (lensing is OBSERVABLE AMBIGUITY), test whether
/// apparent lensing can emerge from the scalar machinery — actualization-density gradients, time-delay statistics,
/// path-selection effects, conformal optical depth, horizon-counting geometry — using only observable quantities
/// (image shift, magnification, time delay) and ignoring GR's curvature interpretation.
///
/// Core fact: the conformally-flat metric g = ρ^(2/d)η has PPN parameter γ = −1. Every lensing observable is
/// proportional to the factor (1+γ)/2 (deflection, convergence, shear) or to (1+γ)/2 directly (Shapiro delay), so
/// γ = −1 makes them ALL vanish. Only the gravitational redshift — governed by g_00 alone — survives. No new primitives.
/// </summary>
public static class NonTensorLensing
{
    /// <summary>PPN parameter γ of the conformally-flat metric g = ρ^(2/d)η: γ = −1.</summary>
    public static double ConformalGamma() => -1.0;

    /// <summary>PPN parameter γ of General Relativity (Schwarzschild weak field): γ = +1.</summary>
    public static double GrGamma() => 1.0;

    /// <summary>
    /// Light deflection angle (weak field): δ = (1+γ)/2 · 4GM/(b c²). The argument gm is GM/(b c²) in natural units.
    /// </summary>
    public static double Deflection(double gamma, double gm) => 0.5 * (1.0 + gamma) * 4.0 * gm;

    /// <summary>
    /// Shapiro time delay: Δt = (1+γ)/2 · 2GM/c³ · ln(...). The argument gmLog is GM/c³ · ln(...) in natural units.
    /// </summary>
    public static double ShapiroDelay(double gamma, double gmLog) => 0.5 * (1.0 + gamma) * 2.0 * gmLog;

    /// <summary>Lensing convergence prefactor: κ ∝ (1+γ)/2 · Σ/Σ_crit.</summary>
    public static double ConvergenceFactor(double gamma) => 0.5 * (1.0 + gamma);

    /// <summary>Lensing shear prefactor: γ_s ∝ (1+γ)/2 · (surface-density structure).</summary>
    public static double ShearFactor(double gamma) => 0.5 * (1.0 + gamma);

    /// <summary>Magnification μ = 1/[(1−κ)² − γ_s²]. With κ = γ_s = 0 (conformal), μ = 1.</summary>
    public static double Magnification(double kappa, double shear)
        => 1.0 / ((1.0 - kappa) * (1.0 - kappa) - shear * shear);

    /// <summary>
    /// Gravitational redshift between two potentials (g_00 = −ρ^(2/d)): z = (ρ2/ρ1)^(1/d) − 1. This is the ONE
    /// conformal observable that survives γ = −1 — it is governed by g_00 alone, not by the null-geodesic factor.
    /// </summary>
    public static double Redshift(int d, double rho1, double rho2)
        => Math.Pow(rho2 / rho1, 1.0 / d) - 1.0;
}
