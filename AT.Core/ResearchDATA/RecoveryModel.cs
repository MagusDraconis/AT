namespace AT.Core.ResearchDATA;

/// <summary>
/// Result of recovering injected cosmological parameters from a mock dataset.
/// Tests whether the fitting pipeline can accurately retrieve known signals.
/// </summary>
public sealed record RecoveryResult(
    InjectionModel Injection,
    int RealizationIndex,
    double RecoveredOmegaM_LCDM,
    double RecoveredM_LCDM,
    double ChiSq_LCDM,
    double RecoveredOmegaM_AT,
    double RecoveredM_AT,
    double ChiSq_AT,
    double DeltaChiSq,
    bool PrefersAT,
    double Significance);

/// <summary>
/// Aggregate statistics from an injection-recovery experiment with multiple realizations.
/// </summary>
public sealed record RecoveryStatistics(
    InjectionModel Injection,
    int NRealizations,
    double MeanDeltaChiSq,
    double StdDeltaChiSq,
    double FractionAtPreferred,
    double MeanSignificance,
    double BiasOmegaM_LCDM,
    double BiasOmegaM_AT,
    double RMSE_OmegaM_LCDM,
    double RMSE_OmegaM_AT);
