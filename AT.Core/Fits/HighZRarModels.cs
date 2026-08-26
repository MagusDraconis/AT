namespace AT.Core.FitsAnalysis;

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

/// <summary>One deprojected rotation-curve point.</summary>
public sealed record RotationCurvePoint(
    double Radius_kpc,
    double Vrot_kms,
    double Vrot_err_kms,
    int Npix);

/// <summary>Full per-galaxy kinematics (velocity field + rotation curve + maps),
/// exposed for the QG-071 RAR extraction audit.</summary>
public sealed record GalaxyFullKinematics(
    string ObjectId,
    double Redshift,
    string EmissionLine,
    double Vsys_kms,
    double Vmax_kms,
    double TurnoverRadius_kpc,
    double InclinationDeg,
    double PA_deg,
    double Chi2,
    double Rms_kms,
    double VelocitySpan_kms,
    double SNR,
    int GoodPixels,
    int FittedPixels,
    double TotalHaFlux,
    double KpcPerPix,
    double ArcsecPerPix,
    double DeltaLambda_um,
    RotationCurvePoint[] RotationCurve,
    double[] FluxMap,
    double[] VelocityMap,
    double[] SnrMap,
    int Ni,
    int Nj);

/// <summary>Result of the whole high-z RAR pilot scan.</summary>
public sealed record HighZRarReport(
    string SA,
    string SB,
    string SC,
    string SD,
    GalaxyKinematics[] Accepted,
    GalaxyKinematics[] All,
    string CsvPath);
