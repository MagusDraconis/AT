namespace AT.Core.ResearchXH;

/// <summary>
/// G4-G Phase 0 — native Einstein-structure investigation. In the conformally-flat 2D geometry
/// g = ρ·η (ρ the native counting measure), constructs the native Ricci tensor, scalar curvature,
/// and the Einstein-tensor candidate G = R_μν − (R/2)g_μν from ρ and its derivatives alone.
///
/// In d=2 the Einstein tensor vanishes IDENTICALLY: R_μν = (R/2)g_μν. This class both builds the
/// tensors natively and exposes that 2D degeneracy (non-trivial Einstein structure requires d ≥ 3).
/// No GR field equations are imported — only the intrinsic curvature of the metric g = ρ·η.
/// </summary>
public static class EinsteinStructure
{
    /// <summary>Native conformal factor ρ(x) = 1 + a·x².</summary>
    public static double Rho(double x, double a) => 1.0 + a * x * x;

    /// <summary>ln ρ (the conformal exponent's source).</summary>
    public static double LnRho(double x, double a) => Math.Log(1.0 + a * x * x);

    /// <summary>Second derivative (ln ρ)″ = 2a(1−a x²)/(1+a x²)².</summary>
    public static double LnRhoSecond(double x, double a)
    {
        double f = 1.0 + a * x * x;
        return 2.0 * a * (1.0 - a * x * x) / (f * f);
    }

    /// <summary>Scalar curvature R = −(1/ρ)(ln ρ)″ = −2e^(−2σ)Δσ (d=2, g = ρ·η).</summary>
    public static double ScalarCurvature(double x, double a) => -LnRhoSecond(x, a) / Rho(x, a);

    /// <summary>Ricci tensor diagonal component R_μν = −Δσ·δ_μν = −(1/2)(ln ρ)″·δ_μν.</summary>
    public static double RicciDiag(double x, double a) => -0.5 * LnRhoSecond(x, a);

    /// <summary>Einstein-tensor diagonal component G_μν = R_μν − (R/2)g_μν (≡ 0 in d=2).</summary>
    public static double EinsteinDiag(double x, double a)
        => RicciDiag(x, a) - 0.5 * ScalarCurvature(x, a) * Rho(x, a);

    /// <summary>Trace g^μν R_μν = ρ⁻¹·2·RicciDiag (must equal the scalar curvature R).</summary>
    public static double TraceRicci(double x, double a) => 2.0 * RicciDiag(x, a) / Rho(x, a);

    /// <summary>Gauss–Bonnet integrand R·√g = −(ln ρ)″ (√g = ρ in d=2).</summary>
    public static double GaussBonnetIntegrand(double x, double a) => -LnRhoSecond(x, a);

    /// <summary>Analytic total curvature content ∫R√g dA over [−1,1]² = −8a/(1+a) (a boundary term).</summary>
    public static double TotalCurvature(double a) => -8.0 * a / (1.0 + a);
}
