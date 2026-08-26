namespace AT.Core.ResearchDATA;

/// <summary>
/// Candidate origin for the factor 2π in g† = cH₀/(2π).
/// Evaluates natural occurrences of 2π within AT's existing structure.
/// </summary>
public sealed record PiFactorCandidate(
    string Origin,
    string Mechanism,
    string Equation,
    double Factor,
    bool IsInevitable,
    bool RequiresAssumption,
    int StrengthScore, // 1-5, higher = stronger
    string Assessment);

/// <summary>
/// Complete 2π origin audit.
/// </summary>
public sealed record PiFactorAudit(
    PiFactorCandidate[] Candidates,
    PiFactorCandidate BestCandidate,
    string SyntheticAnswer,
    string Verdict,
    string Classification);
