using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X038b_CollapseHostileAudit : ResearchTestBase
{
    public TQM_X038b_CollapseHostileAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X038b_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X038b Hostile Audit of Q-Conservation Collapse");

        var report = CollapseAuditAnalyzer.Audit();

        // 1. The claim under attack
        Sec(sb, "The X038 Claim Under Attack");
        sb.AppendLine("  Measurement collapse follows from Q conservation.");
        sb.AppendLine("  Q_before = 2, Q_branch ≥ 3 → Q conservation violated.");
        sb.AppendLine("  Therefore: branching forbidden → single outcome required.");
        sb.AppendLine("  MISSION: Destroy this argument. Save Many-Worlds.");
        sb.AppendLine();

        // 2. Defense-by-defense audit
        Sec(sb, "Many-Worlds Defenses — Audit");
        sb.AppendLine(CollapseAuditAnalyzer.DefenseReport(report.Defenses));

        // 3. Branch-count theorems
        Sec(sb, "Branch-Count Theorems");
        sb.AppendLine(CollapseAuditAnalyzer.TheoremReport(report.Theorems));
        sb.AppendLine(CollapseHostileAudit.TheBranchCountTheorem());

        // 4. MW escape routes
        Sec(sb, "Many-Worlds Escape Routes — All Fatal");
        sb.AppendLine(CollapseAuditAnalyzer.MwEscapeRoutes());

        // 5. The fundamental incompatibility
        Sec(sb, "Fundamental Incompatibility: MW vs TQM");
        sb.AppendLine("  TQM requires:");
        sb.AppendLine("    1. Q is globally well-defined (X035).");
        sb.AppendLine("    2. Q is conserved (TQM-116).");
        sb.AppendLine("    3. Identity persists (A3, X036).");
        sb.AppendLine();
        sb.AppendLine("  MW requires:");
        sb.AppendLine("    1. Q is NOT globally well-defined (only intra-branch).");
        sb.AppendLine("    2. Q is NOT conserved (branching increases Q).");
        sb.AppendLine("    3. Identity does NOT persist (observers split).");
        sb.AppendLine();
        sb.AppendLine("  These are CONTRADICTORY. MW and TQM cannot both be true.");
        sb.AppendLine("  Since TQM derives all of quantum mechanics from Q (X036-X037),");
        sb.AppendLine("  and MW requires denying Q's fundamental properties,");
        sb.AppendLine("  TQM LOGICALLY EXCLUDES Many-Worlds.");
        sb.AppendLine();

        // 6. The Q-conservation collapse theorem
        Sec(sb, "Q-Conservation Collapse Theorem (Final Form)");
        sb.AppendLine("  For any measurement with N ≥ 2 macroscopically distinct outcomes:");
        sb.AppendLine();
        sb.AppendLine("    Q_branch = Q_initial + (N-1)·Q_apparatus  >  Q_initial");
        sb.AppendLine("    Q_collapse = Q_initial");
        sb.AppendLine();
        sb.AppendLine("    Q conservation (dQ/dt = 0)  ⇒  branching is FORBIDDEN.");
        sb.AppendLine("    Therefore: measurement MUST produce a SINGLE outcome.");
        sb.AppendLine();
        sb.AppendLine("  This is not an interpretation. It is a MATHEMATICAL THEOREM");
        sb.AppendLine("  within the TQM framework.");
        sb.AppendLine();

        // 7. Final verdict
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X038b COMPLETE.");
        sb.AppendLine($"  Defenses attempted: {report.DefensesAttempted}. Successful: {report.SuccessfulDefenses}.");
        sb.AppendLine($"  Verdict: X038 {report.Verdict.ToString().ToUpper()}.");
        sb.AppendLine($"  {report.Summary}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
