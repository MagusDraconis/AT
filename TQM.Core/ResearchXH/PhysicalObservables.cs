namespace TQM.Core.ResearchXH;

/// <summary>
/// G4-O Phase 0 — physical observables implied by the ρ-only Einstein structure (Q-events → ρ → G_μν).
/// For the conformally-flat metric g = ρ^(2/d)η with σ = (1/d)ln ρ, the effective gravitational
/// potential is Φ = σ, from which the standard weak-field observables follow: acceleration a = −∇Φ,
/// redshift Δν/ν = −ΔΦ, lensing deflection ∝ ΔΦ, expansion H = ρ̇/ρ. The native Poisson relation is
/// ΔΦ + ((d−2)/2)|∇Φ|² = −(1/(2(d−1)))ρ^(2/d) R, whose source is the CURVATURE (not the density).
/// No imported matter sector, no Einstein equations.
/// </summary>
public static class PhysicalObservables
{
    /// <summary>Effective gravitational potential Φ = σ = (1/d) ln ρ.</summary>
    public static double EffectivePotential(double x, double a, int d) => Math.Log(1.0 + a * x * x) / d;

    /// <summary>Geodesic-like acceleration a = −∇Φ = −(1/d)(ln ρ)′ = −2ax/(d(1+a x²)).</summary>
    public static double Acceleration(double x, double a, int d) => -2.0 * a * x / (d * (1.0 + a * x * x));

    /// <summary>Laplacian of Φ: ΔΦ = (1/d)(ln ρ)″ = 2a(1−a x²)/(d(1+a x²)²).</summary>
    public static double PotentialLaplacian(double x, double a, int d)
    {
        double f = 1.0 + a * x * x;
        return 2.0 * a * (1.0 - a * x * x) / (d * f * f);
    }

    /// <summary>|∇Φ|² = (1/d²)((ln ρ)′)².</summary>
    public static double PotentialGradientSquared(double x, double a, int d)
    {
        double lp = 2.0 * a * x / (1.0 + a * x * x);
        return lp * lp / (d * d);
    }

    /// <summary>Gravitational redshift Δν/ν = −ΔΦ = −[Φ(x2) − Φ(x1)].</summary>
    public static double Redshift(double x1, double x2, double a, int d)
        => -(EffectivePotential(x2, a, d) - EffectivePotential(x1, a, d));

    /// <summary>Lensing deflection ∝ ΔΦ = Φ(x2) − Φ(x1).</summary>
    public static double LensingDeflection(double x1, double x2, double a, int d)
        => EffectivePotential(x2, a, d) - EffectivePotential(x1, a, d);

    /// <summary>Expansion (Hubble-like) H = ρ̇/ρ (0 for the static profile ρ = 1+a x²).</summary>
    public static double Expansion(double rhoDot, double rho) => rhoDot / rho;

    /// <summary>
    /// Native Poisson relation residual: ΔΦ + ((d−2)/2)|∇Φ|² + (1/(2(d−1)))ρ^(2/d) R  (must equal 0).
    /// </summary>
    public static double PoissonResidual(double x, double a, int d)
    {
        double lap = PotentialLaplacian(x, a, d);
        double g2 = PotentialGradientSquared(x, a, d);
        double r = HigherDimEinstein.ScalarCurvature(x, a, d);
        double rho = 1.0 + a * x * x;
        double term = Math.Pow(rho, 2.0 / d) * r / (2.0 * (d - 1.0));
        return lap + 0.5 * (d - 2.0) * g2 + term;
    }
}
