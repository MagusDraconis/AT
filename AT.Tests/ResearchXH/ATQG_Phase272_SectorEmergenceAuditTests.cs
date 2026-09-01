using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 272 — Sector Emergence Audit. Why do distinct sectors exist? Are they fundamental,
/// emergent, or projection classes? D96 only, no observables.
/// </summary>
public class ATQG_Phase272_SectorEmergenceAuditTests : ResearchTestBase
{
    public ATQG_Phase272_SectorEmergenceAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2720_SharedOperatorBasis()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2720: the sector operator signatures");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - no operator is sector-exclusive (all five shared across sectors);");
        sb.AppendLine("  - every sector uses ≥ 3 of the 5 operators.");
        sb.AppendLine();

        foreach (var p in SectorEmergenceAudit.Sectors())
            sb.AppendLine($"  {p.Name,-11} ({p.Count} obs, {p.OperatorCount} operators): primary {p.PrimaryEmphasis}");
        sb.AppendLine();
        sb.AppendLine($"no operator sector-exclusive: {SectorEmergenceAudit.NoOperatorSectorExclusive()}");
        sb.AppendLine($"distinct operators used: {SectorEmergenceAudit.DistinctOperators()}");
        foreach (var s in Enum.GetValues<OperatorSectorAudit.Sector>())
            sb.AppendLine($"  {s}: {OperatorSectorAudit.OperatorsUsedBy(s).Length} operators");

        Output.WriteLine(sb.ToString());

        Assert.True(SectorEmergenceAudit.NoOperatorSectorExclusive(),
            "no operator is exclusive to a single sector — sectors are not fundamental operators");
        Assert.Equal(5, SectorEmergenceAudit.DistinctOperators());
        foreach (OperatorSectorAudit.Sector s in Enum.GetValues<OperatorSectorAudit.Sector>())
            Assert.True(OperatorSectorAudit.OperatorsUsedBy(s).Length >= 3, $"sector {s} uses ≥3 operators");
    }

    [Fact]
    public void ATQG2721_SectorOverlap()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2721: the sector overlap — high sharing, no exclusive structure");

        sb.AppendLine("HYPOTHESIS: the sector boundaries are not structural — the sectors overlap heavily");
        sb.AppendLine("in operator content (a fundamental partition would be disjoint).");
        sb.AppendLine();

        foreach (var (a, b, shared, max) in SectorEmergenceAudit.SectorOverlaps())
            sb.AppendLine($"  {a,-11} × {b,-11}: shared {shared}/{max}");
        sb.AppendLine();
        sb.AppendLine($"min shared fraction: {SectorEmergenceAudit.MinSectorOverlapFraction():P1}");
        sb.AppendLine($"avg shared fraction: {SectorEmergenceAudit.AvgSectorOverlapFraction():P1}");

        Output.WriteLine(sb.ToString());

        Assert.True(SectorEmergenceAudit.MinSectorOverlapFraction() >= 0.5,
            "every sector pair shares at least half its operators");
        Assert.True(SectorEmergenceAudit.AvgSectorOverlapFraction() > 0.7,
            "average overlap is high — the sectors are not disjoint");
    }

    [Fact]
    public void ATQG2722_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2722: the sector-emergence determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - FUNDAMENTAL SECTORS (score ≤ 2), PARTIAL EMERGENCE (3-4),");
        sb.AppendLine("    SECTOR EMERGENCE (5-6);");
        sb.AppendLine("  - the question: why do distinct sectors exist?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {SectorEmergenceAudit.Summary()}");
        sb.AppendLine($"Emergence score: {SectorEmergenceAudit.EmergenceScore()}/6");
        sb.AppendLine($"projection-class structure: {SectorEmergenceAudit.ProjectionClassStructure()}");
        sb.AppendLine($"no dynamical sector-boundary: {SectorEmergenceAudit.NoDynamicalSectorBoundary()}");
        sb.AppendLine($"CLASSIFICATION = {SectorEmergenceAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - NOT FUNDAMENTAL: no sector has its own operator (all five are shared), and");
        sb.AppendLine("    there is one spectrum, one dynamics, one invariant (QG263/264) — no primitive");
        sb.AppendLine("    sector-entity exists.");
        sb.AppendLine("  - NOT dynamically EMERGENT: there is no sector-forming mechanism in D96; the");
        sb.AppendLine("    boundaries are not drawn by the spectrum.");
        sb.AppendLine("  - PROJECTION CLASSES: the five sectors are the SAME operator basis read at");
        sb.AppendLine("    different theoretical roles — masses = values, couplings = strengths, mixings");
        sb.AppendLine("    = orientations, cosmology = global structure, gravity = geometry.");
        sb.AppendLine("  - The sector structure EMERGES from the operator layer + the question-structure");
        sb.AppendLine("    of the theory. The sector LABELS are the operator→physics assignment (QG271).");

        Output.WriteLine(sb.ToString());

        Assert.Equal("SECTOR EMERGENCE", SectorEmergenceAudit.Classify());
        Assert.True(SectorEmergenceAudit.EmergenceScore() >= 5);
        Assert.Contains("SECTOR EMERGENCE", SectorEmergenceAudit.Summary());
        Assert.Contains("PROJECTION", SectorEmergenceAudit.Summary());
    }
}
