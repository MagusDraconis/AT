namespace AT.Core.ResearchDATA;

/// <summary>
/// Single row from the SPARC mass models table (Lelli+2016c).
/// Each row is one radial point in one galaxy's rotation curve.
/// </summary>
public sealed record GalaxyMassPoint(
    string GalaxyId,
    double DistanceMpc,
    double RadiusKpc,
    double Vobs,
    double EVobs,
    double Vgas,
    double Vdisk,
    double Vbulge,
    double SBdisk,
    double SBbulge);

/// <summary>
/// Aggregate statistics for a single galaxy.
/// </summary>
public sealed record GalaxySummary(
    string GalaxyId,
    double DistanceMpc,
    int NPoints,
    double RminKpc,
    double RmaxKpc,
    double VobsMax,
    double VobsMin,
    double VgasMax,
    double VdiskMax,
    double VbulgeMax,
    bool HasBulge,
    double MeanSBdisk);
