namespace TQM.Core.ResearchQG;

/// <summary>Falsification attempt against a specific hypothesis.</summary>
public sealed record FalsificationResult(
    string Hypothesis,
    bool Rejected,
    double SignificanceSigma,
    string Verdict);
