namespace AT.Core.ResearchDATA;

/// <summary>
/// Aggregate detectability result containing all analysis dimensions.
/// </summary>
public sealed record DetectabilityResult(
    RecoveryStatistics Recovery,
    AmplificationExperiment Amplification,
    DetectionThreshold[] Thresholds,
    StatisticalPowerResult[] PowerResults,
    ResidualAnalysis Residuals,
    EuclidComparison Euclid,
    string SectionA_PantheonSensitivity,
    string SectionB_InjectionRecovery,
    string SectionC_SignalAmplification,
    string SectionD_DetectionThresholds,
    string SectionE_StatisticalPower,
    string SectionF_EuclidComparison,
    string SectionG_HostileReview,
    string SectionH_FinalVerdict);

/// <summary>
/// Residual analysis comparing ΛCDM residuals to injected AT residuals.
/// </summary>
public sealed record ResidualAnalysis(
    double MeanResidualLCDM_OnLCDMData,
    double StdResidualLCDM_OnLCDMData,
    double MeanResidualAT_OnATData,
    double StdResidualAT_OnATData,
    double MeanResidualLCDM_OnATData,
    double StdResidualLCDM_OnATData,
    double KolmogorovSmirnovD,
    double KolmogorovSmirnovP,
    string Interpretation);

/// <summary>
/// Comparison between Pantheon sensitivity and Euclid forecast sensitivity.
/// </summary>
public sealed record EuclidComparison(
    double PantheonSigmaW0,
    double PantheonSigmaWa,
    double EuclidSigmaW0,
    double EuclidSigmaWa,
    double SensitivityRatioW0,
    double SensitivityRatioWa,
    double AtSignalW0,
    double AtSignalWa,
    double PantheonSNR,
    double EuclidSNR,
    string Summary);
