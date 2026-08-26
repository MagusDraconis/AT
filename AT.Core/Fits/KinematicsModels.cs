namespace AT.Core.FitsAnalysis;

public sealed record PixelKinematics(
    int RAIndex, int DECIndex,
    double RA_deg, double DEC_deg,
    double Velocity_kms, double Dispersion_kms, double Flux, double SNR);

public sealed record DiskFit(
    double Vsys_kms, double Inclination_deg, double PA_deg,
    double Vmax_kms, double TurnoverRadius_kpc, double Chi2);

public sealed record RotationPoint(
    double Radius_kpc, double Vrot_kms, double Vrot_err_kms, int Npix);

public sealed record KinematicsReport(
    string SA, string SB, string SC, string SD, string SE,
    double HaObserved_A, double HaSystemic_A, double Redshift,
    double PixelScale_arcsec, double KpcPerArcsec,
    int RA_N, int DEC_N, int NpixelsFitted, int NpixelsGood,
    double[] FluxMap, double[] VelocityMap, double[] DispersionMap,
    double[] SnrMap,
    DiskFit Disk,
    RotationPoint[] RotationCurve,
    string Classification,
    string VelocityMapPath, string FluxMapPath, string RotationCurvePath);
