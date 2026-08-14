namespace TQM.Core.ResearchQG;

/// <summary>QG-095 unified cosmological scale: the dimensionless relations between the two
/// surviving anchors Λ and g†, and their common origin in the cosmic rate H.
/// Key fact: Λ ~ H²/c² (causal-set, via N=(R_H/l_P)⁴ ⇒ Λ·l_P² = (H l_P/c)²) and g† ~ cH/2π
/// (time-scale) — BOTH are powers of the single cosmic rate H.</summary>
public static class UnifiedScaleAnalyzer
{
    public const double C = 299792458.0;
    public const double H0 = 67.4;
    public const double Lambda = 1.1e-52;      // m^-2
    public const double A0 = 1.2e-10;          // m/s²
    public const double Kpc_m = 3.0857e19;
    public const double PlanckLength = 1.616255e-35;

    public static double H0PerS => H0 / Kpc_m;
    public static double CH => C * H0PerS;                        // 6.55e-10
    public static double Gdagger => CH / (2.0 * Math.PI);         // 1.04e-10
    public static double C2SqrtLambda => C * C * Math.Sqrt(Lambda); // 9.4e-10

    /// <summary>H²/Λ = H²/(Λ c²) — the 'why now' ratio (≈ 1 = cosmic coincidence).</summary>
    public static double H2OverLambda => (H0PerS * H0PerS) / (Lambda * C * C);

    /// <summary>H²/c² in m^-2 (≈ Λ, the causal-set identification Λ ~ H²/c²).</summary>
    public static double H2OverC2 => H0PerS * H0PerS / (C * C);

    /// <summary>Causal-set Λ from N: Λ·l_P² = (H l_P/c)², so Λ_pred = H²/c².</summary>
    public static double LambdaPredictedFromH => H2OverC2;

    /// <summary>Ratio Λ_pred/Λ_obs (should be O(1) = the causal-set result).</summary>
    public static double LambdaPredictionRatio => LambdaPredictedFromH / Lambda;

    /// <summary>Dimensionless combinations.</summary>
    public static (string Name, double Value, string Interpretation)[] DimensionlessCombinations() => new[]
    {
        ("g†/(cH)", Gdagger / CH, "= 1/(2π) — the 2π factor (QG-085)"),
        ("a₀/(cH)", A0 / CH, "= 0.183 — near 1/5, 1/6, 1/(2π)"),
        ("g†/(c²√Λ)", Gdagger / C2SqrtLambda, "≈ 0.11"),
        ("a₀/(c²√Λ)", A0 / C2SqrtLambda, "≈ 0.13"),
        ("H²/Λc²", H2OverLambda, "≈ 1 — the 'why now' coincidence"),
        ("Λ·l_P²", Lambda * PlanckLength * PlanckLength, "= 2.9e-122"),
        ("(H l_P/c)²", Math.Pow(H0PerS * PlanckLength / C, 2.0), "= 1.4e-122 (causal-set Λ)"),
    };
}
