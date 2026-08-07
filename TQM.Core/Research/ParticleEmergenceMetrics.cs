namespace TQM.Core.Research;

/// <summary>
/// Data types for X047 Particle Emergence.
/// </summary>
public static class ParticleEmergenceMetrics
{
    public enum ParticleStatus { NoParticles, WeakCandidates, StableStructures, FullyDerived }

    public sealed record ParticleCandidate(
        string Name, string TopologicalOrigin,
        string Invariant, bool IsStable,
        bool IsLocalized, bool HasConservedCharge,
        string Notes);

    public sealed record TopologicalProperty(
        string Property, string PhysicalInterpretation,
        string TopologicalOrigin, bool NaturallyQuantized);

    public sealed record ParticleReport(
        List<ParticleCandidate> Candidates,
        List<TopologicalProperty> Properties,
        int StableCount, ParticleStatus Status,
        string Derivation, string Verdict);
}
