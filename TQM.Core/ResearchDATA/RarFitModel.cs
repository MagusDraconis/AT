namespace TQM.Core.ResearchDATA;

/// <summary>
/// A fitted RAR function with quality metrics.
/// </summary>
public sealed record RarFitResult(
    string ModelName,
    string FunctionalForm,
    double[] Parameters,
    string[] ParameterNames,
    double ChiSq,
    int Dof,
    double ReducedChiSq,
    double RmsScatter,
    double Aic,
    double Bic,
    string Verdict);

/// <summary>
/// Collection of RAR fits for model comparison.
/// </summary>
public sealed record RarFitCollection(
    RarFitResult[] Fits,
    BinnedRarPoint[] BinnedData,
    RarPoint[] AllPoints);
