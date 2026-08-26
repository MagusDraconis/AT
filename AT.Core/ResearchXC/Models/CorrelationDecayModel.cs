namespace AT.Core.ResearchXC.Models;

/// <summary>
/// Models for Correlation Decay Theorem Program (ResearchXC-010).
/// </summary>
public static class CorrelationDecayModel
{
    /// <summary>A source of Q-event correlations.</summary>
    public sealed record CorrelationSource(
        string Name, string Mechanism,
        string Range, string DecayLaw,
        bool IsExponential, string Status);

    /// <summary>A mixing-time estimate for the Q-graph Markov chain.</summary>
    public sealed record MixingEstimate(
        string GraphType, double Degree,
        double SpectralGap, double MixingTime,
        double CorrelationLength,
        string Verdict);

    /// <summary>A candidate decay law.</summary>
    public sealed record DecayLaw(
        string Law, string Formula,
        bool SupportsPoissonLimit,
        bool ObservedInQGraph,
        string Evidence);

    /// <summary>A graph topology that could break mixing.</summary>
    public sealed record WorstCaseTopology(
        string Name, string Description,
        double MixingTime, bool BreaksExponentialDecay,
        bool RealizedInAt,
        string Mitigation);

    /// <summary>The complete correlation decay assessment.</summary>
    public sealed record DecayAssessment(
        string Title,
        List<CorrelationSource> Sources,
        List<MixingEstimate> Estimates,
        List<DecayLaw> Laws,
        List<WorstCaseTopology> Topologies,
        double SpectralGapEstimate,
        double CorrelationLengthEstimate,
        string TheoremStatus,
        string Verdict);
}
