namespace AT.Core.ResearchDATA;

/// <summary>
/// Statistical power analysis results: false positive rate, false negative rate,
/// and power for detecting a AT signal at a given η.
/// </summary>
public sealed record StatisticalPowerResult(
    double Eta,
    int NRealizations,
    int TruePositives,
    int TrueNegatives,
    int FalsePositives,
    int FalseNegatives,
    double FalsePositiveRate,
    double FalseNegativeRate,
    double Sensitivity,
    double Specificity,
    double StatisticalPower,
    double DetectionThreshold,
    string Summary);

/// <summary>
/// Detection threshold model: η_min required for each confidence level.
/// </summary>
public sealed record DetectionThreshold(
    double ConfidenceLevel,
    double SigmaLevel,
    double RequiredEta,
    double RequiredDeltaW0,
    double RequiredWa,
    bool AchievableWithPantheon);

/// <summary>
/// Power curve: power as a function of η, computed via Monte Carlo.
/// </summary>
public sealed record PowerCurve(
    DetectionThreshold[] Thresholds,
    StatisticalPowerResult[] PowerResults,
    string Summary);
