namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for the proto-matter collective dynamics analysis.
/// Defines multi-charge ensemble states, correlation structures,
/// collective phases, and the charge phase diagram.
///
/// AT-123: Proto-Matter Collective Dynamics
/// </summary>
public static class CollectiveStateProfile
{
    // ══════════════════════════════════════════════════════════════════
    // Core types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>Result of a single multi-charge ensemble simulation.</summary>
    public sealed record ChargeEnsembleRun(
        double K, double Lambda, int N, int Seed,
        int InitialQ,                   // target number of condensates
        string Layout,                  // "random", "clustered", "lattice", "dense", "sparse"
        int FinalQ,
        int Births, int Mergers,
        double[] Q_history,
        double FinalGlobalR,
        double MeanSeparation,          // mean distance between condensate centers
        double PairCorrelationPeak,     // peak of g(r)
        double CorrelationLength,       // e-folding distance of g(r)
        int LargestCluster,             // largest cluster of nearby condensates
        double ChargeDensity,           // Q / system area
        string PhaseClassification);    // "Vacuum", "DiluteGas", "CorrelatedGas", "Cluster", "Percolating", "Dense"

    /// <summary>Pair correlation function g(r) for charge centers.</summary>
    public sealed record ChargeCorrelation(
        double[] Distances,             // r bins
        double[] g_r,                   // pair correlation function
        double CorrelationLength,       // decay length
        double NearestNeighborMean,     // mean NN distance
        double NearestNeighborStd,      // std of NN distance
        bool IsOrdered,                 // ordered (crystal-like) or disordered
        string StructureType);          // "Gas", "Liquid", "Crystal", "Clustered"

    /// <summary>A collective phase of proto-matter.</summary>
    public sealed record CollectivePhase(
        string Name,
        string Description,
        double ChargeDensityMin, double ChargeDensityMax,
        double CouplingMin, double CouplingMax,
        bool HasLongRangeOrder,
        bool IsPercolated,
        string PairCorrelationSignature,
        string TransportProperty);

    /// <summary>The charge phase diagram: density × coupling.</summary>
    public sealed record ChargePhaseDiagram(
        List<CollectivePhase> Phases,
        double[,] Q_density_grid,       // measured Q density at each (ρ, K) point
        string[,] Phase_grid,          // phase label at each point
        double[] DensityAxis,           // ρ values
        double[] CouplingAxis,          // K values
        int CriticalDensityIndex,       // where percolation first occurs
        int CriticalCouplingIndex,      // where gas→cluster transition occurs
        string PhaseDiagramDescription);

    /// <summary>Complete collective dynamics report.</summary>
    public sealed record ProtoMatterCollectiveReport(
        List<ChargeEnsembleRun> Runs,
        List<ChargeCorrelation> Correlations,
        ChargePhaseDiagram PhaseDiagram,
        List<CollectivePhase> IdentifiedPhases,
        bool CollectivePhasesFound,
        bool PhaseTransitionFound,
        string ContinuumChargeEquation,
        string Classification,
        string Verdict);

    // ══════════════════════════════════════════════════════════════════
    // Collective phases catalog
    // ══════════════════════════════════════════════════════════════════

    public static List<CollectivePhase> GetKnownPhases()
    {
        return new List<CollectivePhase>
        {
            new("Vacuum",
                "Q ≈ 0. No condensates. R<0.5 globally. The PDE ground state.",
                0, 0.01, 0, 100, false, false,
                "g(r) ≈ 1 (uncorrelated, no charges present)",
                "No transport — nothing to transport."),

            new("Dilute Gas",
                "Q ≥ 1 but charges are widely separated (d ≫ 5λ). " +
                "Each condensate evolves independently. No collective effects.",
                0.01, 0.05, 0.1, 20, false, false,
                "g(r) ≈ 1 for r > 0 (uncorrelated Poisson gas)",
                "Diffusive: charges evolve independently."),

            new("Correlated Gas",
                "Charges within coupling range (d ~ 3-10λ) feel mutual " +
                "interaction via phase coupling gradient. Weak correlations.",
                0.03, 0.12, 1.0, 20, false, false,
                "g(r) > 1 at r ~ 5λ (weak attraction peak)",
                "Weakly interacting. Coherence-mediated forces."),

            new("Cluster Phase",
                "Charges form bound clusters of 2-5 condensates. " +
                "Within clusters: frequent mergers. Between clusters: weak interaction.",
                0.08, 0.25, 2.0, 20, false, false,
                "g(r) peaked at short range, decays to 1 at long range",
                "Cluster-internal: coherent. Cluster-external: diffusive."),

            new("Percolating Phase",
                "Charges are so dense that the entire system is connected " +
                "via coupling paths. Global coherence emerges.",
                0.15, 0.50, 3.0, 20, true, true,
                "g(r) > 1 at all r (system-spanning correlations)",
                "Transport: coherent across entire system. Superfluid-like."),

            new("Dense Matter",
                "Extremely dense charge configuration. Most oscillators " +
                "belong to condensates. Global R → 1.",
                0.30, 1.0, 0.5, 20, true, true,
                "g(r) → crystalline peaks at high density",
                "Highly coherent. Proto-matter 'solid' or 'liquid.'"),
        };
    }
}
