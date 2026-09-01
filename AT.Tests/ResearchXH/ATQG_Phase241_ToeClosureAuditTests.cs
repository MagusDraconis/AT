using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 241 — TOE Closure Audit. Re-evaluates all ten TOE criteria from QG226 after QG227-QG240.
/// Classify DERIVED/PARTIAL/BOUNDARY/OPEN; compute TOE completeness; determine remaining blockers.
/// Audit only — no new physics.
/// </summary>
public class ATQG_Phase241_ToeClosureAuditTests : ResearchTestBase
{
    public ATQG_Phase241_ToeClosureAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2410_TenCriteriaReEvaluated()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2410: the ten TOE criteria re-evaluated after QG227-QG240");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Re-evaluates the QG226 TOE criteria after the QG227-QG240 derivation era.");
        sb.AppendLine();

        sb.AppendLine("THE TEN CRITERIA (QG226 → QG241):");
        foreach (var c in ToeClosureAudit.Criteria())
            sb.AppendLine($"  {c.Index:00}. {c.Name}: {c.Qg226Status} → {c.Status}");
        sb.AppendLine();

        sb.AppendLine("STATUS COUNTS:");
        foreach (var kv in ToeClosureAudit.StatusCounts())
            sb.AppendLine($"  {kv.Key}: {kv.Value}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(10, ToeClosureAudit.Criteria().Length);
        var sc = ToeClosureAudit.StatusCounts();
        Assert.Equal(6, sc[ToeClosureAudit.Status.Derived]);
        Assert.Equal(2, sc[ToeClosureAudit.Status.Partial]);
        Assert.Equal(2, sc[ToeClosureAudit.Status.Boundary]);
        Assert.Equal(0, sc[ToeClosureAudit.Status.Open]);
    }

    [Fact]
    public void ATQG2411_Completeness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2411: TOE completeness");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Derived=1.0, Partial=0.5, Boundary=0.75, Open=0.0.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        foreach (var c in ToeClosureAudit.Criteria())
            sb.AppendLine($"  {c.Index:00}. {c.Name}: {ToeClosureAudit.SubScore(c.Status):F2}");
        sb.AppendLine($"  TOTAL = {ToeClosureAudit.TotalScore():F1}/10");
        sb.AppendLine($"  Completeness = {ToeClosureAudit.CompletenessFraction():P1}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(8.5, ToeClosureAudit.TotalScore(), 6);
        Assert.Equal(0.85, ToeClosureAudit.CompletenessFraction(), 6);
    }

    [Fact]
    public void ATQG2412_ClassificationNearCompleteToe()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2412: classification — NEAR-COMPLETE TOE");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - No OPEN criteria; the remaining gaps are two partial derivations and documented");
        sb.AppendLine("    boundaries.");
        sb.AppendLine();

        string classification = ToeClosureAudit.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Completeness: {ToeClosureAudit.CompletenessFraction():P1}");
        sb.AppendLine($"  Any OPEN criteria? {ToeClosureAudit.HasOpenCriteria()}");
        sb.AppendLine($"  Remaining blockers (PARTIAL): {string.Join(", ", ToeClosureAudit.RemainingBlockers())}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine($"  Summary: {ToeClosureAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - QG227-QG240 resolved 3 of the QG226 gaps: initial conditions (QG227), information");
        sb.AppendLine("    origin (QG228), and the cosmological sector (QG230/231/234/237); Ω_Λ/Ω_m (QG234).");
        sb.AppendLine("  - The remaining partials are derivations-in-progress, not blockers: the SM");
        sb.AppendLine("    interaction dynamics and the CMB acoustic-peak recombination mechanism.");
        sb.AppendLine("  - The boundaries (ψ primitive existence, Bekenstein 1/4, H) are documented.");
        sb.AppendLine($"  ⇒ {classification} — 85% complete, no OPEN criterion, only partial derivations");
        sb.AppendLine("    and documented boundaries remain.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("NEAR-COMPLETE TOE", classification);
        Assert.False(ToeClosureAudit.HasOpenCriteria(), "no TOE criterion may remain OPEN");
        Assert.Equal(2, ToeClosureAudit.RemainingBlockers().Length);
    }
}
