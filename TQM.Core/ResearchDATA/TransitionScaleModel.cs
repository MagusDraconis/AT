namespace TQM.Core.ResearchDATA;

/// <summary>
/// Analysis of the transition acceleration scale g†.
/// Tests multiple possible origins:
/// - cH₀ (cosmological)
/// - cH₀/(2π) (circular frequency form)
/// - TQM Λ(t) scale
/// - Causal-set spacing scale
/// - Defect abundance scale
/// </summary>
public sealed record TransitionScaleCandidate(
    string Name,
    string PhysicalOrigin,
    double PredictedValue,
    double PredictedValue_1e10,
    double RatioToEmpirical,
    bool Consistent);

/// <summary>
/// Complete transition scale analysis.
/// </summary>
public sealed record TransitionScaleAnalysis(
    double EmpiricalGDagger,
    double EmpiricalGDagger_1e10,
    TransitionScaleCandidate[] Candidates,
    string BestCandidate,
    string DerivationSummary,
    bool TqmDerivesScale,
    string Verdict);
