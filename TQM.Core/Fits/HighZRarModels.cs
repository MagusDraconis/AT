namespace TQM.Core.FitsAnalysis;

/// <summary>Per-galaxy kinematic result from the high-z RAR pilot pipeline.</summary>
public sealed record GalaxyKinematics(
    string ObjectId,
    double Redshift,
    string EmissionLine,
    double InclinationDeg,
    double Vmax_kms,
    double TurnoverRadius_kpc,
    double VelocitySpan_kms,
    double FitQuality,
    double SNR,
    int GoodPixels,
    double Rms_kms,
    double Vsys_kms,
    double Gobslast_m_s2,
    double Rlast_kpc,
    string Classification);

/// <summary>Result of the whole high-z RAR pilot scan.</summary>
public sealed record HighZRarReport(
    string SA,
    string SB,
    string SC,
    string SD,
    GalaxyKinematics[] Accepted,
    GalaxyKinematics[] All,
    string CsvPath);
