using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 249 — Final TOE Audit. Re-evaluate the TOE status after QG223-248 using the external
/// TOE checklist, QG226, QG241, QG248. Classify every remaining item; answer the four determination
/// questions; list the top-10 strongest remaining criticisms. Audit only.
/// </summary>
public class TQMQG_Phase249_FinalToeAuditTests : ResearchTestBase
{
    public TQMQG_Phase249_FinalToeAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2490_TenCriteriaFinal()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2490: the ten TOE criteria (final)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Reviews QG223-248, using the QG226/241 ten criteria and QG248 SM closure.");
        sb.AppendLine("  - Each criterion: DERIVED / PARTIAL / BOUNDARY / OPEN.");
        sb.AppendLine();

        sb.AppendLine("THE TEN CRITERIA (QG241 status → QG249 status):");
        foreach (var c in FinalToeAudit.Criteria())
        {
            sb.AppendLine($"  {c.Index}. {c.Name}: {c.Status} (was {c.Qg241Status})");
            sb.AppendLine($"      {c.Evidence}");
        }
        sb.AppendLine();
        sb.AppendLine($"Completeness: {FinalToeAudit.TotalScore():F1}/10 ({FinalToeAudit.CompletenessFraction():P1})");
        sb.AppendLine($"By status: {string.Join(", ", FinalToeAudit.StatusCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(10, FinalToeAudit.Criteria().Length);
        var sc = FinalToeAudit.StatusCounts();
        Assert.Equal(7, sc[FinalToeAudit.Status.Derived]);
        Assert.Equal(1, sc[FinalToeAudit.Status.Partial]);
        Assert.Equal(2, sc[FinalToeAudit.Status.Boundary]);
        Assert.Equal(0, sc[FinalToeAudit.Status.Open]);
        // The SM criterion moved PARTIAL → DERIVED after QG248.
        Assert.Equal(FinalToeAudit.Status.Derived, FinalToeAudit.Criteria()[3].Status);
        Assert.Equal("PARTIAL", FinalToeAudit.Criteria()[3].Qg241Status);
        Assert.True(FinalToeAudit.TotalScore() > 8.5, "the QG248 SM completion must lift the score above QG241's 8.5");
    }

    [Fact]
    public void TQMQG2491_FourDeterminations()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2491: the four determination questions");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The four questions ask for true missing physics, hosted core dynamics, unresolved");
        sb.AppendLine("    contradictions, and remaining TOE blockers.");
        sb.AppendLine();

        sb.AppendLine("DETERMINATIONS:");
        foreach (var (q, a) in FinalToeAudit.Determinations())
        {
            sb.AppendLine($"  {q}");
            sb.AppendLine($"      {a}");
        }

        Output.WriteLine(sb.ToString());

        Assert.False(FinalToeAudit.AnyTrueMissingPhysics(), "no OPEN criterion → no true missing physics");
        Assert.False(FinalToeAudit.AnyHostedCoreDynamics(), "QG248 closed the last hosted core");
        Assert.True(FinalToeAudit.AnyUnresolvedContradiction(), "C4 remains PARTIALLY RESOLVED (documentation)");
        Assert.False(FinalToeAudit.AnyRemainingBlocker(), "no OPEN criterion → no blocker");
    }

    [Fact]
    public void TQMQG2492_ClassificationAndCriticisms()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2492: classification and the top-10 criticisms");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - NEAR-COMPLETE TOE: 70-95% with no OPEN; COMPLETE TOE requires no OPEN and no PARTIAL.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FinalToeAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("TOP-10 STRONGEST REMAINING CRITICISMS:");
        foreach (var c in FinalToeAudit.TopCriticisms())
            sb.AppendLine($"  {c.Rank}. [{c.Area} / {c.Status}] {c.Statement}");
        sb.AppendLine();
        sb.AppendLine($"Criticism status: {string.Join(", ", FinalToeAudit.CriticismStatusCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("NEAR-COMPLETE TOE", FinalToeAudit.Classify());
        Assert.Equal(10, FinalToeAudit.TopCriticisms().Length);
        Assert.Contains("NEAR-COMPLETE TOE", FinalToeAudit.Summary());
    }
}
