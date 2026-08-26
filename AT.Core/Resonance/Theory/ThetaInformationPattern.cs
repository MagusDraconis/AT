namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for information-information dynamics in the Θ field.
///
/// AT-132: Information Dynamics in the Θ Field
/// </summary>
public static class ThetaInformationPattern
{
    public sealed record InfoPattern(
        string Name, string Type,
        double[] Pattern,               // Θ(x) encoding
        double Amplitude, double Frequency,
        int BitsEncoded);

    public sealed record PatternInteraction(
        string PatternA, string PatternB,
        double InitialOverlap,          // overlap before evolution
        double FinalOverlap,            // overlap after co-evolution
        double MutualInfoAB,            // I(A; B) after interaction
        double TransferEntropy_AB,      // TE(A→B)
        double TransferEntropy_BA,      // TE(B→A)
        double EntropyChangeA,          // ΔH(A)
        double EntropyChangeB,          // ΔH(B)
        string InteractionType,         // "Merge", "Cancel", "Reinforce", "Independent", "Transform"
        bool InformationTransformed,    // did A or B change?
        string Description);

    public sealed record InfoEntropyProfile(
        string StateLabel,
        double ShannonEntropy,
        double ConditionalEntropy,
        double MutualInfoTotal,
        double InformationProductionRate,  // dI/dt
        double PatternComplexity);        // effective # of independent modes

    public sealed record InfoDynamicsReport(
        List<InfoPattern> Patterns,
        List<PatternInteraction> Interactions,
        List<InfoEntropyProfile> EntropyProfiles,
        bool InteractionsFound,
        bool MergersFound,
        bool CancellationsFound,
        bool CompositeStatesFound,
        bool SelfOrganizationFound,
        string Classification,
        string Verdict);
}
