namespace AT.Core.ResearchDATA;

/// <summary>
/// Velocity component decomposition for a single galaxy.
/// Baryonic velocity: Vbar² = Vgas² + Υ_disk·Vdisk² + Υ_bulge·Vbulge²
/// where Υ_disk, Υ_bulge are mass-to-light ratios determined by fitting.
/// </summary>
public sealed record VelocityComponents(
    string GalaxyId,
    double BestUpsilonDisk,
    double BestUpsilonBulge,
    double ChiSq,
    double ReducedChiSq,
    double[] RadiusKpc,
    double[] Vobs,
    double[] VobsErr,
    double[] Vgas,
    double[] Vdisk,
    double[] Vbulge,
    double[] Vbar,
    double[] Vdm,
    bool DarkMatterNeeded,
    double MeanMassDiscrepancy,
    string Verdict);

/// <summary>
/// Aggregate velocity decomposition statistics across all galaxies.
/// </summary>
public sealed record VelocityStatistics(
    int NGalaxies,
    int NPointsTotal,
    int NGalaxiesNeedDM,
    double FractionNeedDM,
    double MeanUpsilonDisk,
    double MedianUpsilonDisk,
    double StdUpsilonDisk,
    double MeanUpsilonBulge,
    double MeanMassDiscrepancy,
    string Summary);
