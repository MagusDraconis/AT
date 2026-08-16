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

    // ── Discriminating-observable machinery (GR density-source vs TQM curvature-source) ──

    /// <summary>Uniform density profile ρ = ρ₀.</summary>
    public static double Uniform(double x, double rho0 = 1.0) => rho0;

    /// <summary>Gaussian density profile ρ = 1 + A·e^(−(x/σ)²).</summary>
    public static double Gaussian(double x, double A = 0.5, double sigma = 0.3)
        => 1.0 + A * Math.Exp(-(x * x) / (sigma * sigma));

    /// <summary>Shell/ring density profile ρ = 1 + A·e^(−((|x|−r)/σ)²).</summary>
    public static double Shell(double x, double A = 0.5, double r = 0.6, double sigma = 0.15)
    {
        double z = (Math.Abs(x) - r) / sigma;
        return 1.0 + A * Math.Exp(-z * z);
    }

    /// <summary>Double-peak density ρ = 1 + A·(e^(−((x−x₀)/σ)²) + e^(−((x+x₀)/σ)²)).</summary>
    public static double DoublePeak(double x, double A = 0.5, double x0 = 0.4, double sigma = 0.15)
        => 1.0 + A * (Math.Exp(-Math.Pow((x - x0) / sigma, 2)) + Math.Exp(-Math.Pow((x + x0) / sigma, 2)));

    /// <summary>GR Poisson source: S_GR = ρ (the density VALUE, up to 4πG).</summary>
    public static double GrSource(Func<double, double> rho, double x) => rho(x);

    /// <summary>TQM Poisson source: S_TQM = (ln ρ)″ (the log-density curvature).</summary>
    public static double TqmSource(Func<double, double> rho, double x, double h = 1e-5)
    {
        double lp = Math.Log(rho(x));
        double lm = Math.Log(rho(x - h));
        double lpp = Math.Log(rho(x + h));
        return (lpp - 2.0 * lp + lm) / (h * h);
    }

    /// <summary>GR acceleration (1D Poisson enclosed mass): a_GR = −∫₀^x ρ(u) du.</summary>
    public static double GrAcceleration(Func<double, double> rho, double x, int n = 4000)
    {
        double dx = x / n;
        double sum = 0.0;
        for (int i = 0; i < n; i++) sum += rho((i + 0.5) * dx) * dx;
        return -sum;
    }

    /// <summary>TQM acceleration: a_TQM = −(1/d)(ln ρ)′ = −(1/d)ρ′/ρ.</summary>
    public static double TqmAcceleration(Func<double, double> rho, double x, int d, double h = 1e-5)
        => -(rho(x + h) - rho(x - h)) / (2.0 * h * d * rho(x));

    // ── Realistic (stress-test) profiles ───────────────────────────────────────────────

    /// <summary>Cored NFW-like halo ρ = 1 + A/(1 + (x/r_s)²) (peak at origin, ∝ x⁻² tail).</summary>
    public static double Nfw(double x, double A = 0.5, double rs = 0.4) => 1.0 + A / (1.0 + (x * x) / (rs * rs));

    /// <summary>Exponential disk ρ = 1 + A·e^(−|x|/r_d) (peak at origin, exponential tail).</summary>
    public static double Exponential(double x, double A = 0.5, double rd = 0.4)
        => 1.0 + A * Math.Exp(-Math.Abs(x) / rd);

    /// <summary>Uniform sphere ρ = ρ₀ for |x| &lt; R, ρ = 1 outside (compact, uniform core).</summary>
    public static double UniformSphere(double x, double rho0 = 2.0, double R = 0.5)
        => Math.Abs(x) < R ? rho0 : 1.0;
}
