namespace TQM.Core.ResearchDATA;

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
    double RecoveredOmegaM_TQM,
    double RecoveredM_TQM,
    double ChiSq_TQM,
    double DeltaChiSq,
    bool PrefersTQM,
    double Significance);

/// <summary>
/// Aggregate statistics from an injection-recovery experiment with multiple realizations.
/// </summary>
public sealed record RecoveryStatistics(
    InjectionModel Injection,
    int NRealizations,
    double MeanDeltaChiSq,
    double StdDeltaChiSq,
    double FractionTqmPreferred,
    double MeanSignificance,
    double BiasOmegaM_LCDM,
    double BiasOmegaM_TQM,
    double RMSE_OmegaM_LCDM,
    double RMSE_OmegaM_TQM);
