namespace AT.Core.ResearchXH;

/// <summary>
/// G4-G Phase 1 — non-trivial Einstein structure in d ≥ 3. For the conformally-flat metric
/// g = ρ^(2/d)·η (ρ = 1 + a·x², x-only profile, σ = (1/d) ln ρ), computes the native Ricci tensor,
/// scalar curvature, and Einstein tensor G = R − (R/2)g in ARBITRARY dimension d from ρ alone.
///
/// In d=2 the Einstein tensor vanishes (G4-G0); for d ≥ 3 it is NON-TRIVIAL. The components
/// (x-only profile, so all off-diagonal components vanish) are:
///   G_11 = ((d−1)(d−2)/2)·(σ′)²
///   G_ii = (d−2)·[σ″ + ((d−3)/2)(σ′)²]   (i ≠ 1)
/// whose trace is G^μ_μ = −(d−2)R/2 and whose covariant divergence ∇^μ G_μν = 0 (Bianchi).
/// No Einstein equations imported — only the intrinsic curvature of the native metric.
/// </summary>
public static class HigherDimEinstein
{
    /// <summary>ρ = 1 + a·x².</summary>
    public static double Rho(double x, double a) => 1.0 + a * x * x;

    /// <summary>σ = (1/d) ln ρ.</summary>
    public static double Sigma(double x, double a, int d) => Math.Log(1.0 + a * x * x) / d;

    /// <summary>σ′ = (1/d)(ln ρ)′ = 2ax/(d(1+a x²)).</summary>
    public static double SigmaPrime(double x, double a, int d) => 2.0 * a * x / (d * (1.0 + a * x * x));

    /// <summary>σ″ = (1/d)(ln ρ)″ = 2a(1−a x²)/(d(1+a x²)²).</summary>
    public static double SigmaSecond(double x, double a, int d)
    {
        double f = 1.0 + a * x * x;
        return 2.0 * a * (1.0 - a * x * x) / (d * f * f);
    }

    /// <summary>Scalar curvature R = −2(d−1)ρ^(−2/d)[σ″ + ((d−2)/2)(σ′)²].</summary>
    public static double ScalarCurvature(double x, double a, int d)
    {
        double sp = SigmaPrime(x, a, d), s2 = SigmaSecond(x, a, d);
        return -2.0 * (d - 1.0) * Math.Pow(Rho(x, a), -2.0 / d) * (s2 + 0.5 * (d - 2.0) * sp * sp);
    }

    /// <summary>Einstein tensor x-component G_11 = ((d−1)(d−2)/2)(σ′)².</summary>
    public static double Einstein11(double x, double a, int d)
    {
        double sp = SigmaPrime(x, a, d);
        return 0.5 * (d - 1.0) * (d - 2.0) * sp * sp;
    }

    /// <summary>Einstein tensor transverse component G_ii = (d−2)[σ″ + ((d−3)/2)(σ′)²].</summary>
    public static double EinsteinOther(double x, double a, int d)
    {
        double sp = SigmaPrime(x, a, d), s2 = SigmaSecond(x, a, d);
        return (d - 2.0) * (s2 + 0.5 * (d - 3.0) * sp * sp);
    }

    /// <summary>Trace G^μ_μ = ρ^(−2/d)[G_11 + (d−1)G_ii] (equals −(d−2)R/2).</summary>
    public static double TraceEinstein(double x, double a, int d)
        => Math.Pow(Rho(x, a), -2.0 / d) * (Einstein11(x, a, d) + (d - 1.0) * EinsteinOther(x, a, d));

    /// <summary>Bianchi (divergence-free) residual ∇^μ G_μ1 (x-component) — must equal 0.</summary>
    public static double BianchiResidual(double x, double a, int d, double h = 1e-6)
    {
        // ∇^μ G_μ1 = ρ^(−2/d)[G_11′ + (d−3)σ′ G_11 − (d−1)σ′ G_ii]
        double g11p = (Einstein11(x + h, a, d) - Einstein11(x - h, a, d)) / (2.0 * h);
        double sp = SigmaPrime(x, a, d);
        double g11 = Einstein11(x, a, d);
        double go = EinsteinOther(x, a, d);
        return Math.Pow(Rho(x, a), -2.0 / d) * (g11p + (d - 3.0) * sp * g11 - (d - 1.0) * sp * go);
    }

    // ── Direct reconstruction from ρ, ρ′, ρ″ (no intermediate metric/σ objects) ────────

    /// <summary>
    /// Direct Einstein x-component from ρ and ρ′ alone: G_11 = (d−1)(d−2)/(2d²)·(ρ′/ρ)².
    /// Uses only the counting measure and its first derivative — no metric, no conformal exponent.
    /// </summary>
    public static double DirectEinstein11(double rho, double rhoPrime, int d)
    {
        double r = rhoPrime / rho;
        return 0.5 * (d - 1.0) * (d - 2.0) * r * r / (d * d);
    }

    /// <summary>
    /// Direct Einstein transverse component from ρ, ρ′, ρ″:
    /// G_ii = (d−2)/d·(ρ″/ρ) − (d−2)(d+3)/(2d²)·(ρ′/ρ)².
    /// </summary>
    public static double DirectEinsteinOther(double rho, double rhoPrime, double rhoSecond, int d)
    {
        double r = rhoPrime / rho;
        return (d - 2.0) / d * (rhoSecond / rho) - 0.5 * (d - 2.0) * (d + 3.0) * r * r / (d * d);
    }

    /// <summary>Analytic ρ′ = 2ax.</summary>
    public static double RhoPrime(double x, double a) => 2.0 * a * x;

    /// <summary>Analytic ρ″ = 2a.</summary>
    public static double RhoSecond(double x, double a) => 2.0 * a;

    // ── Stress-energy candidates (the native "matter" sector) ──────────────────────────

    /// <summary>
    /// Kinetic (gradient) stress-energy of the conformal field σ from ∇σ alone:
    /// T^kin_μν = ∂_μσ ∂_νσ − (1/2)η_μν(∂σ)². x-component = (1/2)(σ′)².
    /// </summary>
    public static double KineticStress11(double x, double a, int d)
    {
        double sp = SigmaPrime(x, a, d);
        return 0.5 * sp * sp;
    }

    /// <summary>Kinetic stress-energy transverse component = −(1/2)(σ′)².</summary>
    public static double KineticStressOther(double x, double a, int d)
    {
        double sp = SigmaPrime(x, a, d);
        return -0.5 * sp * sp;
    }

    /// <summary>
    /// Native conserved stress-energy T_μν = G_μν/κ (κ the gravitational coupling). Symmetric and
    /// divergence-free (Bianchi), fully determined by ρ — the stress-energy that sources g = ρ^(2/d)η.
    /// </summary>
    public static double NativeStress11(double x, double a, int d, double kappa = 1.0)
        => Einstein11(x, a, d) / kappa;

    /// <summary>Native conserved stress-energy transverse component.</summary>
    public static double NativeStressOther(double x, double a, int d, double kappa = 1.0)
        => EinsteinOther(x, a, d) / kappa;

    /// <summary>Trace of the native stress-energy T^μ_μ = −(d−2)R/(2κ).</summary>
    public static double TraceNativeStress(double x, double a, int d, double kappa = 1.0)
        => TraceEinstein(x, a, d) / kappa;

    /// <summary>
    /// Covariant divergence ∇^μ T_kin_μ1 of the KINETIC stress-energy (∇ρ only). For the diagonal
    /// tensor T_11=(1/2)(σ′)², T_ii=−(1/2)(σ′)² this equals
    /// ρ^(−2/d)[σ′σ″ + (d−1)(σ′)³] — non-zero unless σ′ = 0 or σ is harmonic.
    /// </summary>
    public static double KineticDivergence(double x, double a, int d)
    {
        double sp = SigmaPrime(x, a, d), s2 = SigmaSecond(x, a, d);
        double t11p = sp * s2;              // d/dx[(1/2)(σ′)²] = σ′σ″
        double t11 = 0.5 * sp * sp;
        double tii = -0.5 * sp * sp;
        return Math.Pow(Rho(x, a), -2.0 / d) * (t11p + (d - 3.0) * sp * t11 - (d - 1.0) * sp * tii);
    }
}
