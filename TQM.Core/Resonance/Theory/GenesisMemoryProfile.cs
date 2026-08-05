namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for information back-reaction on proto-matter genesis.
///
/// TQM-131: Information Back-Reaction on Proto-Matter Genesis
/// </summary>
public static class GenesisMemoryProfile
{
    public sealed record NucleationBias(
        string MemoryType,              // "PhasePattern", "StandingWave", "AntiPhase", "Random", "None"
        double BiasFactor,              // P(Q|memory) / P(Q|no_memory)
        double SpatialCorrelation,      // correlation between memory peaks and nucleation sites
        double TemporalShift,           // Δt in nucleation time
        double MutualInfo,              // I(memory; future_Q)
        bool SignificantBias,           // bias > 2σ from null?
        string BiasDirection);          // "Enhance", "Suppress", "None"

    public sealed record MemoryGenesisRun(
        double Density, double K, double Lambda, int N,
        string MemoryType,
        int Q_before,                   // initial charge count
        int Q_after,                    // after re-nucleation
        double NucleationRate,          // births per unit time
        double[] NucleationPositionsX,  // x-coordinates of birth sites
        double MemoryOverlapBefore,     // Θ overlap with original
        double MemoryOverlapAfter,      // Θ overlap after re-nucleation
        bool MemorySurvived,            // overlap > 0.3 after re-nucleation
        bool BiasDetected,
        string Interpretation);

    public sealed record InformationGenesisReport(
        List<MemoryGenesisRun> Runs,
        List<NucleationBias> Biases,
        bool BackReactionFound,
        bool MemorySurvivesRenucleation,
        double MaxBiasFactor,
        double MutualInformation,
        string ModifiedNucleationCondition,
        string Classification,
        string Verdict);
}
