namespace AT.Core.ResearchDATA;

/// <summary>
/// Model comparison metrics for RAR fits.
/// Compares MOND, ΛCDM empirical, and AT-derived forms.
/// </summary>
public sealed record ModelComparison(
    string[] ModelNames,
    double[] ChiSqValues,
    double[] RmsScatter,
    double[] AicValues,
    double[] BicValues,
    int[] NFreeParams,
    int BestModelIndex,
    string BestModel,
    string ComparisonTable,
    string Summary);

/// <summary>
/// Individual model entry in the comparison.
/// </summary>
public sealed record ModelEntry(
    string Name,
    string Category,
    string FunctionalForm,
    double ChiSq,
    double RmsScatter,
    double Aic,
    double Bic,
    int NFreeParams,
    int NTotalParams,
    string Notes);
