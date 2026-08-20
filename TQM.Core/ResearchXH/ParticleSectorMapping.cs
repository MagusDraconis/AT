namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 126 — Particle interpretation of attractor sectors. QG123-125 established that energy creates
/// a hierarchy of METASTABLE attractor sectors (QG123 SECTOR HIERARCHY, QG124 SECTOR ORIGIN, QG125
/// METASTABLE: high sectors decay down the ladder into the observable 3-family sector). This phase asks: can
/// the OBSERVED particle-sector structure be MAPPED onto these attractor sectors?
///
/// Method (computational, fully deterministic): within the de-actualizing (link-decay) dynamics of QG125,
/// build the attractor sector realized at each energy level and characterize it by radius, link count and
/// octave-family count (the sector inventory). Then (1) LOW-ENERGY SECTOR — characterize the observable
/// E=1 sector (radius, families) and identify it as the observable particle sector; (2) HIGH-ENERGY SECTORS —
/// enumerate the distinct higher-energy sectors and their family content (candidate heavier/particle-sector
/// analogs); (3) FAMILY CORRESPONDENCE — do distinct sectors carry distinct family structures (a
/// sector→generation map); (4) SECTOR DECAY CHAINS — from each high-energy sector, the downward multi-rung
/// cascade (QG125) is a candidate particle decay chain; (5) OBSERVABLE REMNANTS — does every decay chain
/// terminate in the observable sector (all decays end in the stable observable remnant)?
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here (reuses the
/// QG125 link-decay dynamics).
/// </summary>
public static class ParticleSectorMapping
{
    /// <summary>Default dynamics parameters (matching QG115–125).</summary>
    public const double DefaultDamping = 0.3;
    public const double DefaultFeedback = 0.9;
    public const int DefaultK = 6;

    /// <summary>High-energy regime used to build high sectors.</summary>
    public const double HighCeiling = HighEnergySectorStability.HighCeiling;

    /// <summary>Baseline (observable) energy regime.</summary>
    public const double BaselineCeiling = HighEnergySectorStability.BaselineCeiling;

    /// <summary>Energy levels of the sector hierarchy (QG123).</summary>
    public static readonly double[] EnergyLevels = EnergyGeometryHierarchy.EnergyLevels;

    // ── Sector inventory ─────────────────────────────────────────────────────────

    /// <summary>
    /// Attractor sector realized at each energy level (decay dynamics): (energy, radius, linkCount, families).
    /// </summary>
    public static (double Energy, double Radius, int Links, int Families)[] SectorInventory(int n = 96,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var result = new List<(double, double, int, int)>();
        foreach (double e in EnergyLevels)
        {
            var (_, adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
                EnergyDependentAttractors.EnergyScaledSeed(n, 1.0), K, damping, feedback, 200, e,
                HighEnergySectorStability.DecayThreshold);
            result.Add((e, HighEnergySectorStability.RadiusOf(adj),
                StructureFromContent.LinkCount(adj), StructureFromContent.FamilyCount(adj)));
        }
        return result.ToArray();
    }

    /// <summary>Distinct radius classes across the energy axis (the sector geometry classes).</summary>
    public static double[] DistinctRadiusClasses(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => SectorInventory(n, K, feedback, damping).Select(s => s.Radius).Distinct().OrderBy(r => r).ToArray();

    /// <summary>Number of distinct sector geometry classes (distinct radii) across the hierarchy.</summary>
    public static int SectorClassCount(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping) => DistinctRadiusClasses(n, K, feedback, damping).Length;

    // ── 1. Low-energy sector ─────────────────────────────────────────────────────

    /// <summary>The observable (low-energy) sector at E = 1.0.</summary>
    public static (double Energy, double Radius, int Links, int Families) LowEnergySector(int n = 96,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var inv = SectorInventory(n, K, feedback, damping);
        return inv[0];
    }

    // ── 2. High-energy sectors ───────────────────────────────────────────────────

    /// <summary>Sectors at energy ABOVE baseline (E &gt; 1.0).</summary>
    public static (double Energy, double Radius, int Links, int Families)[] HighEnergySectors(int n = 96,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping)
        => SectorInventory(n, K, feedback, damping)
            .Where(s => s.Energy > BaselineCeiling + 1e-9).ToArray();

    /// <summary>Number of distinct high-energy sector classes (distinct radii above baseline).</summary>
    public static int HighEnergyClassCount(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => HighEnergySectors(n, K, feedback, damping).Select(s => s.Radius).Distinct().Count();

    // ── 3. Family correspondence ─────────────────────────────────────────────────

    /// <summary>Distinct family counts appearing across the sector hierarchy.</summary>
    public static int[] FamilyCountsAcrossSectors(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => SectorInventory(n, K, feedback, damping).Select(s => s.Families).Distinct().OrderBy(f => f).ToArray();

    /// <summary>Does the low-energy (observable) sector carry the 3-family structure?</summary>
    public static bool ObservableThreeFamilies(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => LowEnergySector(n, K, feedback, damping).Families == 3;

    /// <summary>
    /// Family correspondence: distinct sectors carry distinct family structure — the number of distinct
    /// family counts across the hierarchy is &gt;= 2 (more than one generation-structure class exists).
    /// </summary>
    public static bool DistinctFamilyStructure(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => FamilyCountsAcrossSectors(n, K, feedback, damping).Length >= 2;

    // ── 4. Sector decay chains ───────────────────────────────────────────────────

    /// <summary>
    /// Decay chain from the highest-energy sector down to baseline (QG125 downward ramp). Returns the
    /// distinct radius plateaus (rungs) the chain passes through.
    /// </summary>
    public static double[] DecayChainRungs(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping, int rampSteps = 30)
    {
        var ladder = HighEnergySectorStability.DownwardLadder(n, K, feedback, damping, rampSteps);
        var distinct = new List<double>();
        foreach (var (_, r) in ladder)
            if (distinct.Count == 0 || Math.Abs(r - distinct[^1]) > 1e-6) distinct.Add(r);
        return distinct.ToArray();
    }

    /// <summary>Chain length (number of rungs) of the downward decay cascade.</summary>
    public static int DecayChainLength(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => DecayChainRungs(n, K, feedback, damping).Length;

    /// <summary>
    /// Does the decay chain TERMINATE at the observable sector radius? The final ramp rung may be near-
    /// observable; to confirm true termination, settle the decayed state at the baseline ceiling for
    /// extended evolution and require the settled radius to equal the observable sector radius.
    /// </summary>
    public static bool DecayChainEndsAtObservable(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping, int rampSteps = 30)
    {
        var rungs = DecayChainRungs(n, K, feedback, damping, rampSteps);
        double obsRadius = LowEnergySector(n, K, feedback, damping).Radius;

        // settle the fully decayed state at baseline and require it to land on the observable radius
        var (a, _) = HighEnergySectorStability.HighEnergySector(n, K, feedback, damping);
        for (int i = 0; i <= rampSteps; i++)
        {
            double ceil = HighEnergySectorStability.HighCeiling
                - (HighEnergySectorStability.HighCeiling - HighEnergySectorStability.BaselineCeiling)
                * i / (double)rampSteps;
            (a, _) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(a, K, damping, feedback, 3, ceil,
                HighEnergySectorStability.DecayThreshold);
        }
        for (int t = 0; t < 120; t++)
            (a, var adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(a, K, damping, feedback, 1,
                HighEnergySectorStability.BaselineCeiling, HighEnergySectorStability.DecayThreshold);
        var settled = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(a, K, damping, feedback, 1,
            HighEnergySectorStability.BaselineCeiling, HighEnergySectorStability.DecayThreshold).Adjacency;
        return Math.Abs(HighEnergySectorStability.RadiusOf(settled) - obsRadius) < 1e-6
            && Math.Abs(rungs[^1] - obsRadius) < 1.0 + 1e-6;   // final ramp rung is at most one rung above observable
    }

    // ── 5. Observable remnants ───────────────────────────────────────────────────

    /// <summary>
    /// Observable-remnant consistency: after a high-energy sector has fully decayed at baseline, its family
    /// count must equal the observable sector family count (all decays settle in the observable sector).
    /// </summary>
    public static bool RemnantMatchesObservable(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var remnant = HighEnergySectorStability.ObservableRemnant(n, K, feedback, damping);
        return remnant.RemnantFamilies == remnant.ObservableFamilies;
    }

    // ── Mapping score & classification ───────────────────────────────────────────

    /// <summary>
    /// Sector→particle mapping score (0..5):
    /// 1. observable (E=1) sector carries the 3-family structure;
    /// 2. multiple distinct high-energy sector classes exist;
    /// 3. distinct family structures appear across sectors;
    /// 4. decay chains pass through multiple rungs (decay cascade exists);
    /// 5. decay chains terminate at the observable sector (all decays settle there).
    /// </summary>
    public static int MappingScore(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        int score = 0;
        if (ObservableThreeFamilies(n, K, feedback, damping)) score++;
        if (HighEnergyClassCount(n, K, feedback, damping) >= 2) score++;
        if (DistinctFamilyStructure(n, K, feedback, damping)) score++;
        if (DecayChainLength(n, K, feedback, damping) >= 3) score++;
        if (RemnantMatchesObservable(n, K, feedback, damping) && DecayChainEndsAtObservable(n, K, feedback, damping))
            score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO MAPPING            — the attractor sectors carry no structure that corresponds to observed particle
    ///                           sectors (no 3-family observable sector, single sector, no decay structure);
    ///   PARTIAL MAPPING       — some correspondences hold (observable sector exists, multiple sectors) but the
    ///                           map is incomplete (no distinct family structure, or decay chains do not settle);
    ///   SECTOR-PARTICLE MAPPING — strong correspondence: the observable 3-family sector maps to observed
    ///                           particle families, distinct high-energy sectors form heavier particle-sector
    ///                           analogs, sector decay chains map to particle decay chains, and all decays
    ///                           terminate in the observable remnant — the concrete case.
    /// </summary>
    public static string Classify(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        int score = MappingScore(n, K, feedback, damping);
        if (score <= 2) return "NO MAPPING";
        if (score == 5) return "SECTOR-PARTICLE MAPPING";
        return "PARTIAL MAPPING";
    }
}
