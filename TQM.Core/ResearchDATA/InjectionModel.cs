namespace TQM.Core.ResearchDATA;

/// <summary>
/// Parameters for generating mock Pantheon datasets with injected cosmological signals.
/// Used to test whether the analysis pipeline can recover known deviations from ΛCDM.
/// </summary>
public sealed record InjectionModel(
    double Eta,
    double OmegaMTrue,
    double MTrue,
    int NRealizations,
    string Label);

/// <summary>
/// A single mock dataset generated from a known cosmological model.
/// </summary>
public sealed record MockDataset(
    InjectionModel Injection,
    int RealizationIndex,
    double[] ZValues,
    double[] Errors,
    double[] TrueDistanceModuli,
    double[] ObservedDistanceModuli);
