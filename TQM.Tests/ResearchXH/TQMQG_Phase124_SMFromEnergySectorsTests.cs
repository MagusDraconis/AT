using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 124 — Standard Model sectors from energy hierarchy. QG123 found an
/// energy-ordered sector hierarchy. This phase asks whether observed particle-sector structure
/// can correspond to specific energy-defined attractor sectors.
///
/// Tests: TQMQG1240 (sector ordering), TQMQG1241 (family emergence + hierarchy formation),
/// TQMQG1242 (sector transitions + observable-sector selection + classification).
/// </summary>
public class TQMQG_Phase124_SMFromEnergySectorsTests : ResearchTestBase
{
    public TQMQG_Phase124_SMFromEnergySectorsTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1240_SectorOrderingAndEmergence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1240: sector ordering along the energy hierarchy");

        var sectors = SMFromEnergySectors.OrderedSectors();
        int observable = SMFromEnergySectors.ObservableSectorCount();
        int total = SMFromEnergySectors.TotalSectorCount();
        bool hidden = SMFromEnergySectors.HasHiddenHighEnergySectors();

        sb.AppendLine("ORDERED SECTORS (minEnergy→maxEnergy):");
        foreach (var s in sectors)
            sb.AppendLine($"  sector {s.SectorId}: E[{s.MinEnergy:F1},{s.MaxEnergy:F1}] members={s.Count}");
        sb.AppendLine();
        sb.AppendLine($"observable sectors (E<=1.0): {observable}");
        sb.AppendLine($"total sectors (full hierarchy): {total}");
        sb.AppendLine($"hidden high-energy sectors exist: {hidden}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: sectors are ordered by energy and observable sectors are a strict");
        sb.AppendLine("subset of the full hierarchy, with additional sectors unlocked only at higher");
        sb.AppendLine("actualization energy.");
        Output.WriteLine(sb.ToString());

        Assert.True(sectors.Length >= 8, "hierarchy should contain many sectors");
        Assert.True(hidden, "high-energy-only sectors should exist");
        Assert.True(total > observable, "observable sector set should be a strict subset");
        for (int i = 1; i < sectors.Length; i++)
            Assert.True(sectors[i].MinEnergy >= sectors[i - 1].MinEnergy, "sectors must be energy-ordered");
    }

    [Fact]
    public void TQMQG1241_FamilyEmergenceAndHierarchyFormation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1241: family emergence and hierarchy formation");

        bool classesGrow = EnergyGeometryHierarchy.ClassesGrowWithEnergy();
        bool threeFamily = SMFromEnergySectors.ObservableThreeFamilyStructure();
        var fam = SMFromEnergySectors.FamilyTrajectory();

        sb.AppendLine("FAMILY TRAJECTORY (fixed f=0.7,d=0.3):");
        foreach (var x in fam)
            sb.AppendLine($"  E={x.Energy:F1}: families={x.Families}");
        sb.AppendLine();
        sb.AppendLine($"geometry class count grows with energy: {classesGrow}");
        sb.AppendLine($"observable 3-family structure present: {threeFamily}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: energy builds a geometry hierarchy and includes a 3-family");
        sb.AppendLine("observable class at baseline energy, supporting a sector-to-family mapping.");
        Output.WriteLine(sb.ToString());

        Assert.True(classesGrow, "geometry classes should grow with energy");
        Assert.True(threeFamily, "baseline regime should include a 3-family class");
        Assert.True(fam.All(x => x.Families >= 2), "family structure should persist along hierarchy");
    }

    [Fact]
    public void TQMQG1242_SectorTransitionsSelectionAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1242: sector transitions, observable selection, classification");

        bool discrete = SMFromEnergySectors.SectorTransitionsDiscrete();
        int score = SMFromEnergySectors.MappingScore();
        string cls = SMFromEnergySectors.Classify();

        sb.AppendLine($"discrete sector transitions: {discrete}");
        sb.AppendLine($"mapping score (0..5): {score}");
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO RELATION rejected: multiple correspondence conditions hold.");
        sb.AppendLine("  • SECTOR ORIGIN accepted: ordered hierarchy, discrete transitions,");
        sb.AppendLine("    observable 3-family class, and observable subset selection from");
        sb.AppendLine("    a larger high-energy sector space.");
        Output.WriteLine(sb.ToString());

        Assert.True(discrete, "sector transitions should be discrete");
        Assert.True(score >= 4, "mapping score should be strong");
        Assert.Equal("SECTOR ORIGIN", cls);
    }
}

