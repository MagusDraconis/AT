namespace AT.Core.ResearchQG;

/// <summary>
/// QG-090 amount-of-change analysis and the causal-set cosmological-constant prediction.
/// The amount of change C(t) = N(t) = number of distinct events; H, entropy production and
/// information growth are manifestations of d ln C/dt. In Causal Set Theory, the discreteness
/// of spacetime predicts the cosmological constant Λ ~ 1/√N ~ 1e-122 (Planck units), matching
/// the observed dark-energy density — the one genuinely quantitative 'prediction from change'.
/// </summary>
public static class ChangeAnalyzer
{
    public const double PlanckLength = 1.616255e-35; // m
    public const double LambdaObs = 1.1e-52;         // m^-2
    public const double H0 = 67.4, C = 299792458.0;

    public static double HubbleLength => C / (H0 / 3.0857e19); // m

    /// <summary>Number of causet elements in the Hubble 4-volume: N = (R_H/l_P)^4.</summary>
    public static double CausetElementCount()
        => Math.Pow(HubbleLength / PlanckLength, 4.0);

    /// <summary>Predicted Λ in Planck units (Sorkin's ever-present Λ): Λ·l_P² ~ 1/√N.</summary>
    public static double PredictedLambdaPlanck()
        => 1.0 / Math.Sqrt(CausetElementCount());

    /// <summary>Observed Λ in Planck units: Λ_obs × l_P².</summary>
    public static double ObservedLambdaPlanck()
        => LambdaObs * PlanckLength * PlanckLength;

    /// <summary>Ratio predicted/observed (should be O(1)).</summary>
    public static double PredictionRatio()
        => PredictedLambdaPlanck() / ObservedLambdaPlanck();

    /// <summary>a₀ = c × (minimum cosmic rate of change) = cH.</summary>
    public static double A0FromRateOfChange()
        => C * (H0 / 3.0857e19);
}
