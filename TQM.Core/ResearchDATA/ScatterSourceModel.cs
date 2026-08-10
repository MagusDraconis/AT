namespace TQM.Core.ResearchDATA;

/// <summary>
/// A stochastic source of RAR scatter within TQM.
/// </summary>
public sealed record ScatterSource(
    string Name,
    string PhysicalMechanism,
    double ExpectedContribution_Dex,
    bool IsDerived,
    bool IsCalibrated,
    int ConfidenceLevel,
    string Assessment);

/// <summary>
/// Catalog of all scatter sources with total budget.
/// </summary>
public sealed record ScatterSourceCatalog(
    ScatterSource[] Sources,
    double TotalPredictedScatter_Dex,
    double ObservedScatter_Dex,
    double RatioPredictedToObserved,
    string Verdict,
    string Summary);
