namespace AT.Core.ResearchDATA;

/// <summary>
/// Single point on the Radial Acceleration Relation (RAR).
/// g_obs = Vobs²/R, g_bar = Vbar²/R.
/// </summary>
public sealed record RarPoint(
    string GalaxyId,
    double RadiusKpc,
    double Gobs,
    double Gbar,
    double LogGobs,
    double LogGbar,
    double GobsErr,
    double GbarErr);

/// <summary>
/// Binned RAR data for fitting.
/// </summary>
public sealed record BinnedRarPoint(
    double LogGbarCenter,
    double MeanLogGobs,
    double StdLogGobs,
    double SemLogGobs,
    int NPoints,
    double GbarCenter,
    double MeanGobs);
