namespace TQM.Core.ResearchDATA;

/// <summary>
/// Acceleration data point: g_obs vs g_bar.
/// g_obs = Vobs²/R, g_bar = Vbar²/R.
/// The RAR (Radial Acceleration Relation) connects these.
/// </summary>
public sealed record AccelerationPoint(
    string GalaxyId,
    double RadiusKpc,
    double Gobs,
    double Gbar,
    double LogGobs,
    double LogGbar,
    double Discrepancy);

/// <summary>
/// Binned acceleration statistics for the RAR.
/// </summary>
public sealed record BinnedAcceleration(
    double LogGbarCenter,
    double LogGbarLow,
    double LogGbarHigh,
    double GbarCenter,
    int NPoints,
    double MeanLogGobs,
    double StdLogGobs,
    double MedianLogGobs,
    double MeanDiscrepancy,
    string Regime);

/// <summary>
/// Full acceleration analysis, including the empirical RAR fit.
/// RAR functional form: g_obs = g_bar / (1 - exp(-sqrt(g_bar/g†)))
/// where g† is the characteristic acceleration scale.
/// </summary>
public sealed record AccelerationAnalysis(
    AccelerationPoint[] AllPoints,
    BinnedAcceleration[] Binned,
    double CharacteristicAcceleration,
    double CharacteristicAccelerationM2S2,
    double RmsScatter,
    double PearsonR,
    double SpearmanRho,
    bool RarConfirmed,
    string RarFitDescription,
    string Summary);
