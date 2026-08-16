namespace TQM.Core.ResearchXH;

/// <summary>
/// G4-RHO Phase 0 — dynamical origin of ρ. Tests whether scale-free actualization, flux conservation, and
/// attractor dynamics determine ρ(x), and whether the α=0 (log-deficit) abundance law arises naturally.
/// No new primitives.
/// </summary>
public static class RhoDynamics
{
    /// <summary>Scale-free (self-similar) density ρ = ρ₀(r/r₀)^s.</summary>
    public static double ScaleFreeDensity(double r, double s, double rho0 = 1.0, double r0 = 1.0)
        => rho0 * Math.Pow(r / r0, s);

    /// <summary>Log density ρ = ρ̄ + c·ln(r/r₀) (the α=0 log-deficit density, rising outward).</summary>
    public static double LogDensity(double r, double c = 0.4, double rhoBar = 1.0, double r0 = 1.0)
        => rhoBar + c * Math.Log(r / r0);

    /// <summary>TQM acceleration a = −(1/d) d(ln ρ)/dr (central difference).</summary>
    public static double Acceleration3D(Func<double, double> rho, double r, int d = 3, double h = 1e-6)
        => -(rho(r + h) - rho(r - h)) / (2.0 * h * d * rho(r));

    /// <summary>Rotation-curve proxy v² = r·|a|.</summary>
    public static double RotationCurve(Func<double, double> rho, double r, int d = 3, double h = 1e-6)
        => r * Math.Abs(Acceleration3D(rho, r, d, h));

    /// <summary>
    /// Actualization flux F = ρ·v·r^(d−1) through radius r, with scale-free velocity v = v₀ r^β.
    /// Conservation (steady state, no sources) means F(r) = const.
    /// </summary>
    public static double Flux(Func<double, double> rho, double r, double beta = 0.0, double v0 = 1.0, int d = 3)
        => rho(r) * v0 * Math.Pow(r, beta + d - 1.0);
}
