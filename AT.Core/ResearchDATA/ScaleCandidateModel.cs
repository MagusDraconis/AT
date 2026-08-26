namespace AT.Core.ResearchDATA;

/// <summary>
/// Comparison of different candidate acceleration scales.
/// Tests: cH₀, cH₀/π, cH₀/(2π), cH₀/(4π) against empirical g†.
/// </summary>
public sealed record ScaleCandidate(
    string Label,
    string Formula,
    double Value_1e10,
    double RatioToEmpirical,
    double DeltaSigma,
    bool Consistent);

/// <summary>
/// Complete scale comparison analysis.
/// </summary>
public sealed record ScaleComparison(
    ScaleCandidate[] Candidates,
    double EmpiricalGDagger_1e10,
    ScaleCandidate BestMatch,
    double Sensitivity2Pi,
    double SensitivityPi,
    double Sensitivity4Pi,
    string Summary);
