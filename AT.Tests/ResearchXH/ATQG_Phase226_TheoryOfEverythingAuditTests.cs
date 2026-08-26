using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 226 — Theory Of Everything Audit. Determine whether AT satisfies TOE requirements.
/// Ten criteria classified DERIVED/PARTIAL/OPEN. Audit only — no new physics.
/// </summary>
public class ATQG_Phase226_TheoryOfEverythingAuditTests : ResearchTestBase
{
    public ATQG_Phase226_TheoryOfEverythingAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2260_TenCriteria()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2260: the ten TOE criteria and their derivation status");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Each criterion is classified DERIVED / PARTIAL / OPEN based on QG0-QG223.");
        sb.AppendLine();

        sb.AppendLine("THE TEN CRITERIA:");
        foreach (var c in TheoryOfEverythingAudit.Criteria())
            sb.AppendLine($"  {c.Index:00}. {c.Name}: {c.Status}  [{c.SourcePhases}]");
        sb.AppendLine();

        sb.AppendLine("STATUS COUNTS:");
        foreach (var kv in TheoryOfEverythingAudit.StatusCounts())
            sb.AppendLine($"  {kv.Key}: {kv.Value}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(10, TheoryOfEverythingAudit.Criteria().Length);
        var sc = TheoryOfEverythingAudit.StatusCounts();
        Assert.Equal(4, sc[TheoryOfEverythingAudit.Status.Derived]);
        Assert.Equal(5, sc[TheoryOfEverythingAudit.Status.Partial]);
        Assert.Equal(1, sc[TheoryOfEverythingAudit.Status.Open]);
    }

    [Fact]
    public void ATQG2261_MissingRequirements()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2261: the missing (not fully derived) TOE requirements");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - 'Missing' = a criterion that is PARTIAL or OPEN, or a specific gap within one.");
        sb.AppendLine();

        sb.AppendLine("MISSING REQUIREMENTS:");
        foreach (var m in TheoryOfEverythingAudit.MissingRequirements())
            sb.AppendLine($"  • {m}");

        Output.WriteLine(sb.ToString());

        Assert.True(TheoryOfEverythingAudit.MissingRequirements().Length >= 5,
            "a PARTIAL TOE must have at least 5 missing requirements");
    }

    [Fact]
    public void ATQG2262_ClassificationPartialToe()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2262: classification — PARTIAL TOE");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - score < 5.0 NOT TOE; 5.0-7.4 PARTIAL TOE; 7.5-8.9 EFFECTIVE TOE; 9.0+ COMPLETE TOE.");
        sb.AppendLine();

        double score = TheoryOfEverythingAudit.TotalScore();
        string classification = TheoryOfEverythingAudit.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        foreach (var c in TheoryOfEverythingAudit.Criteria())
            sb.AppendLine($"  {c.Index:00}. {c.Name}: {TheoryOfEverythingAudit.SubScore(c.Status):F1}");
        sb.AppendLine($"  TOTAL = {score:F1}/10");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine($"  Summary: {TheoryOfEverythingAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The theory is a COMPLETE QUANTUM GRAVITY (QG223) and MONOGRAPH READY (QG224), with");
        sb.AppendLine("    QM + gravity + matter + the SM mass sector + dimensionality all DERIVED.");
        sb.AppendLine("  - As a THEORY OF EVERYTHING it is PARTIAL: cosmology (structure formation, dark");
        sb.AppendLine("    energy), initial conditions, and full parameter/primitive completeness remain");
        sb.AppendLine("    partial or open.");
        sb.AppendLine($"  ⇒ {classification} — the honest verdict is a complete QG within a partial TOE.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL TOE", classification);
        Assert.Equal(6.5, score, 6);
        Assert.Equal(1, TheoryOfEverythingAudit.StatusCounts()[TheoryOfEverythingAudit.Status.Open]); // exactly one criterion (initial conditions) is OPEN
    }
}
