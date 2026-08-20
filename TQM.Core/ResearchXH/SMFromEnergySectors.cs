namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 124 — Standard Model sectors from energy hierarchy. QG123 established an
/// energy-ordered sector hierarchy (12 sectors total, 10 high-energy-only). This phase asks:
/// can observed particle-sector structure (families, charges, interactions) correspond to
/// specific energy-defined attractor sectors?
///
/// Method (computational, fully deterministic): treat each energy-defined attractor sector as a
/// candidate "particle sector" and evaluate five mappings: (1) SECTOR ORDERING — low-energy to
/// high-energy sector ordering from the energy hierarchy; (2) FAMILY EMERGENCE — whether the
/// three-family structure appears in baseline/low-energy observable sectors; (3) HIERARCHY
/// FORMATION — whether the sector count and class count grow with energy; (4) SECTOR
/// TRANSITIONS — whether transitions between sectors are discrete (KS-separated) rather than a
/// continuum; (5) OBSERVABLE-SECTOR SELECTION — whether local/low-energy observation selects a
/// strict subset of the total sectors, analogous to observed SM sector accessibility.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class SMFromEnergySectors
{
    /// <summary>Baseline (observable) energy level.</summary>
    public const double ObservableEnergy = 1.0;

    /// <summary>
    /// Sector ordering by minimum energy of appearance. Each entry is
    /// (sectorId, minEnergy, maxEnergy, memberCount).
    /// </summary>
    public static (int SectorId, double MinEnergy, double MaxEnergy, int Count)[] OrderedSectors(
        int n = 96, int K = 6)
        => EnergyGeometryHierarchy.SectorClusters(n, K)
            .OrderBy(s => s.MinEnergy)
            .ThenBy(s => s.Id)
            .Select(s => (s.Id, s.MinEnergy, s.MaxEnergy, s.Count))
            .ToArray();

    /// <summary>Number of sectors visible at or below the observable-energy regime.</summary>
    public static int ObservableSectorCount(int n = 96, int K = 6)
        => OrderedSectors(n, K).Count(s => s.MinEnergy <= ObservableEnergy + 1e-9);

    /// <summary>Total number of sectors across the full energy hierarchy.</summary>
    public static int TotalSectorCount(int n = 96, int K = 6)
        => OrderedSectors(n, K).Length;

    /// <summary>Are there hidden sectors beyond the observable-energy regime?</summary>
    public static bool HasHiddenHighEnergySectors(int n = 96, int K = 6)
        => TotalSectorCount(n, K) > ObservableSectorCount(n, K);

    /// <summary>
    /// Family emergence in observable sectors: at baseline energy (E=1), does the model produce
    /// a 3-family structure for some attractor class?
    /// </summary>
    public static bool ObservableThreeFamilyStructure(int n = 96, int K = 6)
    {
        // At E=1 and f-sweep, QG117/122 produce baseline classes; check if any has 3 families.
        for (double f = 0.2; f <= 1.0 + 1e-9; f += 0.1)
        {
            var net = EnergyDependentAttractors.AdaptiveNetworkWithCeiling(
                EnergyDependentAttractors.EnergyScaledSeed(n, 1.0), K, 0.3, f, 120, ObservableEnergy);
            if (StructureFromContent.FamilyCount(net) == 3) return true;
        }
        return false;
    }

    /// <summary>
    /// Family-count trajectory at fixed (f,d) across the energy hierarchy. Used to quantify whether
    /// higher-energy sectors merge/split family content.
    /// </summary>
    public static (double Energy, int Families)[] FamilyTrajectory(
        double feedback = 0.7, double damping = 0.3, int n = 96, int K = 6)
        => EnergyGeometryHierarchy.FamilyByEnergy(feedback, damping, n, K)
            .Select(x => (x.Energy, x.Families))
            .ToArray();

    /// <summary>
    /// Sector transitions are discrete if accessible class counts form a staircase over energy
    /// (monotone non-decreasing with at least two jumps).
    /// </summary>
    public static bool SectorTransitionsDiscrete(int n = 96, int K = 6)
    {
        var transitions = EnergyGeometryHierarchy.TransitionsByEnergy(n, K);
        int jumps = 0;
        for (int i = 1; i < transitions.Length; i++)
        {
            if (transitions[i].Classes < transitions[i - 1].Classes) return false;
            if (transitions[i].Classes > transitions[i - 1].Classes) jumps++;
        }
        return jumps >= 2;
    }

    /// <summary>
    /// Mapping score for sector-origin correspondence (0..5):
    /// 1. ordered sector hierarchy exists;
    /// 2. class count grows with energy;
    /// 3. observable (E=1) includes a 3-family class;
    /// 4. sector transitions are discrete;
    /// 5. observable sector selection is a strict subset of total sectors.
    /// </summary>
    public static int MappingScore(int n = 96, int K = 6)
    {
        int score = 0;
        if (OrderedSectors(n, K).Length > 0) score++;
        if (EnergyGeometryHierarchy.ClassesGrowWithEnergy(n, K)) score++;
        if (ObservableThreeFamilyStructure(n, K)) score++;
        if (SectorTransitionsDiscrete(n, K)) score++;
        if (HasHiddenHighEnergySectors(n, K)) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO RELATION      — sector hierarchy does not map to observed structure (poor score, no
    ///                      3-family observable class, no subset selection);
    ///   PARTIAL RELATION — some correspondences hold (ordering, discrete transitions, subset
    ///                      selection), but mapping is not uniquely fixed to SM sectors;
    ///   SECTOR ORIGIN    — strong correspondence: ordered hierarchy, discrete transitions, observable
    ///                      3-family structure, and observable subset selection from a larger total sector
    ///                      space — suggesting observed sectors originate from energy-defined attractor
    ///                      sectors.
    /// </summary>
    public static string Classify(int n = 96, int K = 6)
    {
        int score = MappingScore(n, K);
        if (score <= 2) return "NO RELATION";
        if (score == 5) return "SECTOR ORIGIN";
        return "PARTIAL RELATION";
    }
}

