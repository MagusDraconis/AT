namespace TQM.Core.ResearchDATA;

/// <summary>
/// Analysis of the characteristic acceleration scale a0.
/// Tests whether a0 ≈ cH0 or a0 ≈ cH0/(2π) emerges from the data.
/// </summary>
public sealed record A0Analysis(
    double EmpiricalA0,
    double EmpiricalA0_SI,
    double H0_Value,
    double CH0,
    double CH0_2Pi,
    double Ratio_EmpiricalTo_CH0,
    double Ratio_EmpiricalTo_CH0_2Pi,
    double A0FromFit,
    double A0Uncertainty,
    bool SupportsCH0,
    bool SupportsCH0_2Pi,
    double[] GalaxyTransitionAccelerations,
    double MeanGalaxyA0,
    double StdGalaxyA0,
    double MedianGalaxyA0,
    string ComparisonTable,
    string Verdict);

/// <summary>
/// Mass discrepancy-acceleration relation (MDAR) summary.
/// Tests the prediction: D = f(g_bar) with a characteristic scale.
/// </summary>
public sealed record MDARAnalysis(
    double CharacteristicGbar,
    double Scatter,
    double[] BinnedLogGbar,
    double[] BinnedLogD,
    double[] BinnedLogD_Std,
    string FitFunction,
    string Summary);
