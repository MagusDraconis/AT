namespace TQM.Core.ResearchQG;

/// <summary>Per-galaxy residual and its candidate explanatory observables for the
/// QG-077 hidden-systematics decomposition. The residual is the log g† deviation from
/// the best-fit constant (the null), so its variance is the pure scatter to decompose.</summary>
public sealed record GalaxyResidual(
    string Object,
    double Z,
    double Inclination,
    double LogMStar,
    double LogSFR,
    double GasFraction,
    double LogRe,
    double VelocitySpan,
    double Vmax,
    double RcExtentRe,
    double DiskChi2,
    double VelRms,
    double KinematicScore,
    double LogGdagger,
    double LogGdaggerErr,
    double ResidualConst);

/// <summary>Univariate correlation of the residual with one observable.</summary>
public sealed record ResidualCorrelation(
    string Observable,
    double PearsonR,
    double R2,
    double SlopeDexPerDex,
    int Nvalid);

/// <summary>Hierarchical (greedy) variance-decomposition step.</summary>
public sealed record VarianceComponent(
    string Observable,
    double R2,
    double IncrementalR2,
    double CumulativeR2);

/// <summary>Aggregate report for QG-077.</summary>
public sealed record HiddenSystematicsReport(
    string SA, string SB, string SC, string SD, string SE, string SF,
    ResidualCorrelation[] Correlations,
    VarianceComponent[] Decomposition,
    GalaxyResidual[] Galaxies,
    double ResidualStdDex,
    double ExplainedVarianceFraction,
    double RemainingScatterDex,
    string CsvDir);
