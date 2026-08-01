namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// A resonance family — a group of synchronization clusters sharing
/// similar statistical signatures (frequency, energy, lifetime, coherence).
/// Distinct families may represent different stable resonance states.
/// </summary>
public sealed class ResonanceFamily
{
    /// <summary>
    /// Unique family identifier.
    /// </summary>
    public int FamilyId { get; set; }

    /// <summary>
    /// Mean internal synchronization (order parameter) across family members.
    /// </summary>
    public double MeanSynchronization { get; }

    /// <summary>
    /// Mean effective frequency of oscillators in this family.
    /// </summary>
    public double MeanFrequency { get; }

    /// <summary>
    /// Mean accumulated energy across family members.
    /// </summary>
    public double MeanEnergy { get; }

    /// <summary>
    /// Mean cluster lifetime (iterations) across family members.
    /// </summary>
    public double MeanLifetime { get; }

    /// <summary>
    /// Mean cluster size (number of oscillators).
    /// </summary>
    public double MeanClusterSize { get; }

    /// <summary>
    /// Coherence score: how tightly clustered the family members are in feature space.
    /// 1.0 = perfectly identical signatures. Lower = more dispersed.
    /// </summary>
    public double CoherenceScore { get; }

    /// <summary>
    /// All synchronization clusters belonging to this family.
    /// </summary>
    public List<SynchronizationCluster> Members { get; }

    /// <summary>
    /// Number of member clusters.
    /// </summary>
    public int MemberCount => Members.Count;

    public ResonanceFamily(
        int familyId,
        double meanSynchronization,
        double meanFrequency,
        double meanEnergy,
        double meanLifetime,
        double meanClusterSize,
        double coherenceScore,
        List<SynchronizationCluster> members)
    {
        FamilyId = familyId;
        MeanSynchronization = meanSynchronization;
        MeanFrequency = meanFrequency;
        MeanEnergy = meanEnergy;
        MeanLifetime = meanLifetime;
        MeanClusterSize = meanClusterSize;
        CoherenceScore = coherenceScore;
        Members = members ?? new List<SynchronizationCluster>();
    }

    /// <summary>
    /// Returns a compact signature string for report display.
    /// </summary>
    public string Signature() =>
        $"F{FamilyId}: R={MeanSynchronization:F3} ω={MeanFrequency:F3} " +
        $"E={MeanEnergy:F2} τ={MeanLifetime:F0} N={MeanClusterSize:F1} " +
        $"coh={CoherenceScore:F3} [{MemberCount} members]";

    public override string ToString() => Signature();
}
