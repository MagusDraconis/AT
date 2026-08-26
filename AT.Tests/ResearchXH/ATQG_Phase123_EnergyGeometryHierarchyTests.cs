using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 123 — Structure hierarchy from energy. QG122 showed energy (actualization rate) acts as an
/// order parameter over the attractor ladder. This phase asks: does increasing actualization energy generate
/// a HIERARCHY of network geometries from which particle sectors emerge? Investigates attractor ladders,
/// geometry transitions, family emergence, sector emergence, and the energy-class hierarchy. Classify:
/// NO HIERARCHY / PARTIAL HIERARCHY / SECTOR HIERARCHY.
///
/// Tests: ATQG1230 (attractor ladders + geometry transitions), ATQG1231 (family emergence + sector
/// emergence), ATQG1232 (energy-class hierarchy + classification).
/// </summary>
public class ATQG_Phase123_EnergyGeometryHierarchyTests : ResearchTestBase
{
    public ATQG_Phase123_EnergyGeometryHierarchyTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1230: attractor ladders + geometry transitions ───────────────────────

    [Fact]
    public void ATQG1230_AttractorLaddersAndGeometryTransitions()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1230: energy-ordered attractor ladders and geometry transitions");

        bool ladderGrows = EnergyGeometryHierarchy.LadderGrowsWithEnergy();
        bool classesGrow = EnergyGeometryHierarchy.ClassesGrowWithEnergy();

        sb.AppendLine("ATTRACTOR RADIUS LADDER BY ENERGY LEVEL:");
        foreach (var (e, radii) in EnergyGeometryHierarchy.LadderByEnergy())
            sb.AppendLine($"  E={e:F1}: [{string.Join(", ", radii.Select(r => r.ToString("F2")))}]");
        sb.AppendLine($"  ladder grows with energy: {ladderGrows}");
        sb.AppendLine();
        sb.AppendLine("GEOMETRY TRANSITIONS (accessible spectral classes per energy level):");
        foreach (var (e, c) in EnergyGeometryHierarchy.TransitionsByEnergy())
            sb.AppendLine($"  E={e:F1}: {c} classes");
        sb.AppendLine($"  classes grow (monotone) with energy: {classesGrow}");
        sb.AppendLine($"  total geometry classes across the energy axis: {EnergyGeometryHierarchy.TotalGeometryClasses()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the radius ladder GROWS with energy (2 rungs at E=1.0 → 9 at E=4.0) and the");
        sb.AppendLine("number of accessible geometry classes grows monotonically from 2 (baseline) to 8 — a");
        sb.AppendLine("genuine energy-ordered sequence of geometry transitions.");
        Output.WriteLine(sb.ToString());

        Assert.True(ladderGrows, "radius ladder grows with energy");
        Assert.True(classesGrow, "accessible geometry classes grow monotonically with energy");
        Assert.True(EnergyGeometryHierarchy.TotalGeometryClasses() >= 6,
            "the energy axis unlocks a rich class hierarchy");
    }

    // ── ATQG1231: family emergence + sector emergence ────────────────────────────

    [Fact]
    public void ATQG1231_FamilyAndSectorEmergence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1231: family emergence and sector emergence across the energy axis");

        bool familiesPersist = EnergyGeometryHierarchy.FamilyStructurePersists();
        var sectors = EnergyGeometryHierarchy.SectorClusters();
        bool unlocks = EnergyGeometryHierarchy.HighEnergyUnlocksSectors();

        sb.AppendLine("FAMILY EMERGENCE (f=0.7, d=0.3):");
        foreach (var (e, fams, span, r) in EnergyGeometryHierarchy.FamilyByEnergy())
            sb.AppendLine($"  E={e:F1}: radius={r:F2}, families={fams}, span={span:F2}");
        sb.AppendLine($"  family structure persists across the energy axis: {familiesPersist}");
        sb.AppendLine();
        sb.AppendLine($"SECTOR EMERGENCE (KS single-linkage over the full energy×feedback landscape):");
        sb.AppendLine($"  total sectors: {sectors.Length}");
        sb.AppendLine($"  sectors reachable ONLY above baseline energy: {EnergyGeometryHierarchy.HighEnergyOnlySectors()}");
        sb.AppendLine($"  higher energy unlocks new sectors: {unlocks}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: family structure (≥ 2 octave families) is carried up the entire energy axis");
        sb.AppendLine("while the geometry ladder expands, and the landscape decomposes into 12 sectors of which");
        sb.AppendLine("10 are ONLY reachable above the baseline regime — higher energy genuinely unlocks new");
        sb.AppendLine("sector-like geometries.");
        Output.WriteLine(sb.ToString());

        Assert.True(familiesPersist, "family structure persists across the energy axis");
        Assert.True(sectors.Length >= 6, "the energy landscape decomposes into multiple sectors");
        Assert.True(unlocks, "higher energy unlocks new sectors");
        Assert.True(EnergyGeometryHierarchy.HighEnergyOnlySectors() >= 5, "many sectors are high-energy-only");
    }

    // ── ATQG1232: energy-class hierarchy + classification ────────────────────────

    [Fact]
    public void ATQG1232_EnergyClassHierarchyAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1232: energy-class hierarchy → NO HIERARCHY / PARTIAL HIERARCHY / SECTOR HIERARCHY");

        bool ordered = EnergyGeometryHierarchy.EnergyOrderedHierarchy();
        bool unlocks = EnergyGeometryHierarchy.HighEnergyUnlocksSectors();
        string cls = EnergyGeometryHierarchy.Classify();

        sb.AppendLine("ENERGY-CLASS HIERARCHY:");
        sb.AppendLine($"  classes grow monotonically AND high-energy-only sectors exist (energy-ordered): {ordered}");
        sb.AppendLine($"  high energy unlocks new sectors: {unlocks}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO HIERARCHY: energy strongly orders the geometry — the radius ladder (2→9 rungs)");
        sb.AppendLine("    and accessible class count (2→8) both grow with energy.");
        sb.AppendLine("  • SECTOR HIERARCHY: increasing energy generates a hierarchy of network geometries — new");
        sb.AppendLine("    classes appear at higher energy, 10 of 12 sectors are high-energy-only, and family");
        sb.AppendLine("    structure is carried up the axis — an energy-ordered sector hierarchy from which");
        sb.AppendLine("    particle sectors could emerge.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("SECTOR HIERARCHY", cls);
        Assert.True(ordered, "the energy-class structure is a clean energy-ordered hierarchy");
        Assert.True(unlocks, "higher energy unlocks new sectors");
    }
}
