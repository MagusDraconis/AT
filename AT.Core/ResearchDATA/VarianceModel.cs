namespace AT.Core.ResearchDATA;

/// <summary>
/// Variance propagation model: Q-event fluctuations → halo variance → RAR scatter.
/// </summary>
public sealed record VarianceStep(
    int Step,
    string Level,
    string Variable,
    double InputVariance,
    double PropagationFactor,
    double OutputVariance,
    string Equation);

/// <summary>
/// Complete variance propagation chain.
/// </summary>
public sealed record VarianceModel(
    VarianceStep[] Steps,
    double InitialVariance,
    double FinalVariance_Dex,
    double ObservedScatter_Dex,
    bool ReproducesObservation,
    string Summary,
    string DetailedReport);
