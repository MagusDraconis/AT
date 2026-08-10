namespace TQM.Core.ResearchDATA;

/// <summary>
/// Mass discrepancy D(R) = Vobs² / Vbar² for all points.
/// D > 1 indicates dark matter is needed at that radius.
/// </summary>
public sealed record MassDiscrepancyPoint(
    string GalaxyId,
    double RadiusKpc,
    double Vobs,
    double Vbar,
    double Discrepancy,
    double AccelObs,
    double AccelBar);

/// <summary>
/// Binned mass discrepancy statistics.
/// </summary>
public sealed record BinnedDiscrepancy(
    double BinCenter,
    double BinLow,
    double BinHigh,
    int NPoints,
    double MeanDiscrepancy,
    double StdDiscrepancy,
    double MedianDiscrepancy,
    double MeanAccelBar,
    string Regime);

/// <summary>
/// Full mass discrepancy analysis results.
/// </summary>
public sealed record MassDiscrepancyAnalysis(
    MassDiscrepancyPoint[] AllPoints,
    BinnedDiscrepancy[] BinnedByRadius,
    BinnedDiscrepancy[] BinnedByAcceleration,
    double TransitionAcceleration,
    double TransitionRadius,
    string TransitionDescription,
    string Summary);
