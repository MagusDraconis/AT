namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 123 — Structure hierarchy from energy. QG122 showed energy (actualization rate) acts as an
/// order parameter over the attractor ladder: raising the energy ceiling opens NEW attractor geometry classes
/// (radius beyond the baseline K=6 cap; spectral class count 2→8). This phase asks: does increasing
/// actualization energy generate a HIERARCHY of network geometries from which particle sectors emerge?
///
/// Method (computational, fully deterministic): sweep the energy ceiling axis (the actualization-rate regime)
/// and the feedback axis, then (1) ATTRACTOR LADDERS — the distinct radius classes realized at each energy
/// level; (2) GEOMETRY TRANSITIONS — the number of distinct spectral classes accessible at each energy level
/// (sharp growth = transitions into new geometries); (3) FAMILY EMERGENCE — octave-family count and hierarchy
/// span evolution with energy; (4) SECTOR EMERGENCE — cluster the whole energy×feedback spectral landscape
/// into sectors (KS single-linkage) and measure how many sectors exist at each energy threshold and how many
/// are only reachable above the baseline regime; (5) ENERGY-CLASS HIERARCHY — is the sector count monotone in
/// energy, with NEW sectors appearing at higher energies (a clean energy-ordered hierarchy)?
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class EnergyGeometryHierarchy
{
    /// <summary>Default dynamics parameters (matching QG115–122).</summary>
    public const double DefaultDamping = 0.2;
    public const double DefaultFeedback = 0.7;
    public const int DefaultK = 6;

    /// <summary>Baseline activity ceiling.</summary>
    public const double BaselineCeiling = 1.0;

    /// <summary>Energy (ceiling) levels swept for the hierarchy study.</summary>
    public static readonly double[] EnergyLevels = { 1.0, 1.5, 2.0, 3.0, 4.0, 6.0, 8.0 };

    // ── 1. Attractor ladders ───────────────────────────────────────────────────────

    /// <summary>Distinct radius classes realized over the feedback sweep at each energy level.</summary>
    public static (double Energy, double[] Radii)[] LadderByEnergy(int n = 96, int K = DefaultK)
    {
        var result = new List<(double, double[])>();
        foreach (double e in EnergyLevels)
        {
            var radii = new List<double>();
            for (double f = 0.2; f <= 1.0 + 1e-9; f += 0.1)
            {
                var net = EnergyDependentAttractors.AdaptiveNetworkWithCeiling(
                    EnergyDependentAttractors.EnergyScaledSeed(n, 1.0), K, 0.3, f, 120, e);
                radii.Add(EnergyDependentAttractors.RadiusOf(net));
            }
            result.Add((e, radii.Distinct().OrderBy(r => r).ToArray()));
        }
        return result.ToArray();
    }

    /// <summary>Does the radius ladder GROW with energy (more rungs at higher energy)?</summary>
    public static bool LadderGrowsWithEnergy(int n = 96, int K = DefaultK)
    {
        var data = LadderByEnergy(n, K);
        return data[^1].Radii.Length > data[0].Radii.Length;
    }

    // ── 2. Geometry transitions ────────────────────────────────────────────────────

    /// <summary>Number of distinct spectral classes accessible at each energy level (f-sweep at that level).</summary>
    public static (double Energy, int Classes)[] TransitionsByEnergy(int n = 96, int K = DefaultK)
    {
        var result = new List<(double, int)>();
        foreach (double e in EnergyLevels)
            result.Add((e, EnergyDependentAttractors.SpectralClassCount(e, n, K)));
        return result.ToArray();
    }

    /// <summary>Does the number of accessible geometry classes GROW monotonically with energy?</summary>
    public static bool ClassesGrowWithEnergy(int n = 96, int K = DefaultK)
    {
        var data = TransitionsByEnergy(n, K);
        for (int i = 1; i < data.Length; i++)
            if (data[i].Classes < data[i - 1].Classes) return false;
        return data[^1].Classes > data[0].Classes;
    }

    /// <summary>Total number of geometry classes unlocked across the whole energy axis (max accessible).</summary>
    public static int TotalGeometryClasses(int n = 96, int K = DefaultK)
        => TransitionsByEnergy(n, K).Max(t => t.Classes);

    // ── 3. Family emergence ────────────────────────────────────────────────────────

    /// <summary>Octave-family count and hierarchy span vs energy level.</summary>
    public static (double Energy, int Families, double Span, double Radius)[] FamilyByEnergy(
        double feedback = 0.7, double damping = 0.3, int n = 96, int K = DefaultK)
    {
        var result = new List<(double, int, double, double)>();
        foreach (double e in EnergyLevels)
        {
            var net = EnergyDependentAttractors.AdaptiveNetworkWithCeiling(
                EnergyDependentAttractors.EnergyScaledSeed(n, 1.0), K, damping, feedback, 120, e);
            result.Add((e, StructureFromContent.FamilyCount(net),
                StructureFromContent.HierarchySpan(net), EnergyDependentAttractors.RadiusOf(net)));
        }
        return result.ToArray();
    }

    /// <summary>Does the octave-family structure persist across the energy axis (families present at every
    /// level, i.e. family structure EMERGES and is carried up the hierarchy)?</summary>
    public static bool FamilyStructurePersists(double feedback = 0.7, double damping = 0.3, int n = 96,
        int K = DefaultK)
        => FamilyByEnergy(feedback, damping, n, K).All(x => x.Families >= 2);

    // ── 4. Sector emergence ────────────────────────────────────────────────────────

    /// <summary>
    /// Sector clustering of the full energy×feedback spectral landscape (KS single-linkage, ε=0.12).
    /// Returns per sector: (id, minEnergy, maxEnergy, memberCount).
    /// </summary>
    public static (int Id, double MinEnergy, double MaxEnergy, int Count)[] SectorClusters(int n = 96,
        int K = DefaultK, double ks = 0.12)
    {
        var shapes = new List<double[]>();
        var meta = new List<(double energy, double f)>();
        foreach (double e in EnergyLevels)
            for (double f = 0.2; f <= 1.0 + 1e-9; f += 0.1)
            {
                var net = EnergyDependentAttractors.AdaptiveNetworkWithCeiling(
                    EnergyDependentAttractors.EnergyScaledSeed(n, 1.0), K, 0.3, f, 120, e);
                var sh = SpectrumRobustness.NormalizedShape(SpectrumRobustness.LaplacianOf(net));
                if (sh.Length > 0) { shapes.Add(sh); meta.Add((e, f)); }
            }
        int m = shapes.Count;
        var labels = new int[m];
        Array.Fill(labels, -1);
        int next = 0;
        for (int i = 0; i < m; i++)
        {
            if (labels[i] != -1) continue;
            labels[i] = next;
            for (int j = 0; j < m; j++)
                if (labels[j] == -1 && SpectralCurvature.KolmogorovSmirnov(shapes[i], shapes[j]) < ks)
                    labels[j] = next;
            next++;
        }
        var result = new List<(int, double, double, int)>();
        for (int s = 0; s < next; s++)
        {
            var members = Enumerable.Range(0, m).Where(i => labels[i] == s).Select(i => meta[i]).ToList();
            result.Add((s, members.Min(x => x.energy), members.Max(x => x.energy), members.Count));
        }
        return result.ToArray();
    }

    /// <summary>Total number of distinct sectors across the whole energy axis.</summary>
    public static int TotalSectors(int n = 96, int K = DefaultK) => SectorClusters(n, K).Length;

    /// <summary>
    /// Number of sectors reachable ONLY above the baseline energy (new sectors unlocked by higher energy).
    /// </summary>
    public static int HighEnergyOnlySectors(int n = 96, int K = DefaultK)
        => SectorClusters(n, K).Count(s => s.MinEnergy > BaselineCeiling + 0.01);

    /// <summary>Does higher energy UNLOCK new sectors (sectors whose minimum energy is above baseline)?</summary>
    public static bool HighEnergyUnlocksSectors(int n = 96, int K = DefaultK)
        => HighEnergyOnlySectors(n, K) > 0;

    // ── 5. Energy-class hierarchy ──────────────────────────────────────────────────

    /// <summary>
    /// Hierarchy check: is the sector structure ENERGY-ORDERED — new geometry classes (and sectors) appear
    /// at successively higher energies, never disappearing as energy grows? Returns true if the accessible
    /// class count is monotone non-decreasing in energy AND high-energy sectors exist.
    /// </summary>
    public static bool EnergyOrderedHierarchy(int n = 96, int K = DefaultK)
        => ClassesGrowWithEnergy(n, K) && HighEnergyUnlocksSectors(n, K);

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   NO HIERARCHY     — energy orders nothing: same classes/sectors at every energy, no new classes,
    ///                      no family/sector emergence;
    ///   PARTIAL HIERARCHY — energy changes the geometry (classes grow) but the structure is not a clean
    ///                       sector hierarchy (no high-energy-only sectors, or sectors not energy-ordered);
    ///   SECTOR HIERARCHY — increasing energy generates a hierarchy of network geometries: more geometry
    ///                       classes at higher energy, NEW sectors unlocked only above the baseline regime,
    ///                       family structure carried up the axis — an energy-ordered sector hierarchy from
    ///                       which particle sectors could emerge — the concrete case.
    /// </summary>
    public static string Classify(int n = 96, int K = DefaultK)
    {
        bool ladderGrows = LadderGrowsWithEnergy(n, K);
        bool classesGrow = ClassesGrowWithEnergy(n, K);
        bool unlocks = HighEnergyUnlocksSectors(n, K);
        bool familiesPersist = FamilyStructurePersists(n: n, K: K);

        if (!ladderGrows && !classesGrow && !unlocks) return "NO HIERARCHY";
        if (classesGrow && unlocks && familiesPersist) return "SECTOR HIERARCHY";
        return "PARTIAL HIERARCHY";
    }
}
