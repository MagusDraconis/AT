namespace TQM.Core.FitsAnalysis;

/// <summary>One row of the KMOS3D kinematic candidate catalog (main CSV).</summary>
public sealed record CatalogEntry(
    string ObjectId,
    double Redshift,
    string Band,
    double ExposureMinutes,
    string EmissionLine,
    double EstimatedZ,
    double SNR,
    double AxisRatio,
    double InclinationDeg,
    double KinematicScore);

/// <summary>Extended per-cube metrics (Top-20 CSV and report).</summary>
public sealed record CatalogDetails(
    string ObjectId,
    double RAdeg,
    double DECdeg,
    string CubeDims,
    double SizeArcsec,
    int GoodPixels,
    int FittedPixels,
    double TotalFlux,
    double VelocitySpanKms,
    double SnrScore,
    double IncScore,
    double FluxScore,
    string Classification,
    string LineObservedA,
    string LambdaRangeA);

/// <summary>Result of the whole catalog scan.</summary>
public sealed record CatalogReport(
    string Summary,
    CatalogEntry[] Entries,
    CatalogDetails[] Details,
    string Top20Table,
    string CsvPath,
    string Top20CsvPath);
