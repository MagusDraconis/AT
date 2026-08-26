using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 311 — Anti-Hierarchy Audit. Try to kill the operator basis using systems with no
/// hierarchy, no power law, no modularity, no scale separation (latin square, regular lattice,
/// balanced tree, round-robin tournament, equal-frequency corpus). Deterministic, D96 only.
/// </summary>
public class ATQG_Phase311_AntiHierarchyAuditTests : ResearchTestBase
{
    public ATQG_Phase311_AntiHierarchyAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3110_FiveAntiHierarchySystems()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3110: the five anti-hierarchy systems");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - latin square, round-robin, equal-frequency corpus are FLAT;");
        sb.AppendLine("  - regular lattice and balanced tree are anti-hierarchy but degenerate + spanned.");
        sb.AppendLine();

        foreach (var s in AntiHierarchyAudit.Systems())
        {
            sb.AppendLine($"  {s.Name.PadRight(26)} — {s.Structure}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, AntiHierarchyAudit.Systems().Length);
        Assert.Equal(5, AntiHierarchyAudit.Systems().Select(s => s.Name).Distinct().Count());
    }

    [Fact]
    public void ATQG3111_OperatorSignatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3111: the four operators on each anti-hierarchy system");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the regular lattice and balanced tree should carry the basis (degenerate + spanned);");
        sb.AppendLine("  - the flat systems (latin square, round-robin K_n, equal-frequency) should lose it.");
        sb.AppendLine();

        foreach (var s in AntiHierarchyAudit.Systems())
        {
            sb.AppendLine($"  {s.Name}: span={s.Span:F2} distinct={s.DistinctValues} octaves={s.OctaveCount}");
            sb.AppendLine($"     CROWDING={s.CrowdingPresent} COMPRESSION={s.CompressionPresent} BEAT={s.BeatPresent} LOCKING={s.LockingPresent} all={s.AllOperatorsPresent}");
        }
        sb.AppendLine();
        sb.AppendLine($"surviving: {AntiHierarchyAudit.SurvivingCount()}/5");
        sb.AppendLine($"anti-hierarchy survives: {AntiHierarchyAudit.AntiHierarchySurvives()}");
        sb.AppendLine($"flat systems lose the basis: {AntiHierarchyAudit.FlatSystemsLoseBasis()}");

        Output.WriteLine(sb.ToString());

        Assert.True(AntiHierarchyAudit.Systems().Any(s => s.Name == "regular lattice" && s.AllOperatorsPresent),
            "the regular lattice must carry all four operators");
        Assert.True(AntiHierarchyAudit.Systems().Any(s => s.Name == "balanced tree" && s.AllOperatorsPresent),
            "the balanced tree must carry all four operators");
        Assert.True(AntiHierarchyAudit.SurvivingCount() >= 2,
            "at least 2 anti-hierarchy systems must carry the basis");
    }

    [Fact]
    public void ATQG3112_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3112: the anti-hierarchy outcome");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the operators require ORGANIZATION (inequality), not hierarchy;");
        sb.AppendLine("  - they survive anti-hierarchy structures (degenerate + spanned) and fail the flat ones.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {AntiHierarchyAudit.Summary()}");
        sb.AppendLine($"Outcome score: {AntiHierarchyAudit.OutcomeScore()}/5");
        sb.AppendLine($"surviving: {AntiHierarchyAudit.SurvivingCount()}/5");
        sb.AppendLine($"CLASSIFICATION = {AntiHierarchyAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the operators SURVIVE anti-hierarchy: the regular lattice [periodic 2D torus —");
        sb.AppendLine("    degenerate + spanned spectrum] and the balanced tree [complete binary tree —");
        sb.AppendLine("    degenerate leaves, log separation] carry all four operators despite having NO");
        sb.AppendLine("    hierarchy, power law, or modularity;");
        sb.AppendLine("  - the operators FAIL on the flat anti-organization systems: the latin-square");
        sb.AppendLine("    frequency [each symbol n times], the round-robin K_n [single positive");
        sb.AppendLine("    eigenvalue], and the equal-frequency corpus [every token equal] collapse to a");
        sb.AppendLine("    single distinct value;");
        sb.AppendLine("  - the operators require ORGANIZATION (inequality), not hierarchy — consistent");
        sb.AppendLine("    with QG309 (zero-difference boundary) and QG310 (anti-organization loses the basis).");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL", AntiHierarchyAudit.Classify());
        Assert.True(AntiHierarchyAudit.PartialOutcome());
        Assert.Contains("PARTIAL", AntiHierarchyAudit.Summary());
    }
}
