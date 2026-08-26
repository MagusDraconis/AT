namespace AT.Core.ResearchDATA;

/// <summary>
/// Result of signal amplification test for a single η value.
/// Tests whether doubling/tripling/etc. the AT signal would make it detectable.
/// </summary>
public sealed record SignalAmplificationResult(
    double Eta,
    string Label,
    int NRealizations,
    double MeanDeltaChiSq,
    double StdDeltaChiSq,
    double FractionDetected,
    double MeanSignificance,
    double MaxSignificance,
    double MinSignificance,
    string Verdict);

/// <summary>
/// Amplification experiment comparing multiple η values.
/// </summary>
public sealed record AmplificationExperiment(
    SignalAmplificationResult[] Results,
    double EtaThreshold1Sigma,
    double EtaThreshold2Sigma,
    double EtaThreshold3Sigma,
    double EtaThreshold5Sigma,
    string Summary);
