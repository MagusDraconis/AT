namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for Θ field memory and information persistence analysis.
///
/// AT-130: Theta Memory and Information Persistence
/// </summary>
public static class ThetaMemoryState
{
    public sealed record MemoryWrite(
        string EncodingType,            // "PhasePattern", "Pulse", "StandingWave", "Sequence", "Texture"
        int BitsWritten,
        double[] InitialPattern,        // Θ(x) at t=0 after write
        double WriteFidelity);          // how accurately pattern was written

    public sealed record PersistenceResult(
        double Density,
        double Time,                    // elapsed time after write
        double PatternOverlap,          // ⟨Θ(t)|Θ(0)⟩ / √(⟨Θ(t)|Θ(t)⟩⟨Θ(0)|Θ(0)⟩)
        double MutualInformation,       // I(write; read)
        double RetentionFraction,       // fraction of bits still recoverable
        double MemoryHalfLife,          // estimated t_1/2 from decay fit
        bool InformationPersists,       // overlap > 0.3?
        string DecayType);              // "Exponential", "PowerLaw", "Stable", "Collapse"

    public sealed record MemoryAttractor(
        string Name,
        double BasinSize,               // fraction of phase space in basin
        double AttractionRate,          // convergence speed
        double StabilityLifetime,       // how long state persists
        bool IsMetastable,              // finite lifetime?
        string Description);

    public sealed record ThetaMemoryReport(
        List<MemoryWrite> Writes,
        List<PersistenceResult> Persistence,
        List<MemoryAttractor> Attractors,
        bool MemoryObserved,
        bool LongTermPersistence,       // t_1/2 > 1000
        double MaxMemoryLifetime,
        double StorageCapacity,         // max bits storable
        double OptimalRetentionDensity,
        string Classification,
        string Verdict);
}
