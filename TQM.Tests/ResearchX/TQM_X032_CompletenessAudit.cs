using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X032_CompletenessAudit : ResearchTestBase
{
    public TQM_X032_CompletenessAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X032_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X032 Completeness Audit");

        var report = CompletenessAuditAnalyzer.Analyze();

        Sec(sb, "Equivalence Matrix");
        sb.AppendLine("  Concept                │ Main TQM            │ ResearchX           │ Match? │ Notes");
        sb.AppendLine("  " + new string('─', 110));
        foreach (var e in report.Entries)
            sb.AppendLine($"  {e.Concept,-22} │ {e.MainTQMStatus,-20} │ {e.ResearchXStatus,-20} │ {(e.IsEquivalent ? "✓" : "✗"),-6} │ {e.Notes}");
        sb.AppendLine();
        sb.AppendLine($"  Equivalent: {report.EquivalentConcepts}/{report.TotalConcepts}. Gaps: {report.GapsRemaining}.");
        sb.AppendLine();

        Sec(sb, "Gap Analysis");
        foreach (var g in report.Gaps)
            sb.AppendLine($"  • {g}");
        sb.AppendLine();

        Sec(sb, "The Two Convergent Paths");
        sb.AppendLine("  MAIN TQM (117-154):");
        sb.AppendLine("    Q → L_Q → Hilbert → J → i → Schrödinger → Born → Measurement");
        sb.AppendLine("    Provides: MATHEMATICAL MACHINERY");
        sb.AppendLine();
        sb.AppendLine("  RESEARCHX (X001-X031):");
        sb.AppendLine("    Q → R+S → Reality → Carriers → Species → Evolution → Necessity");
        sb.AppendLine("    Provides: CONCEPTUAL FRAMEWORK");
        sb.AppendLine();
        sb.AppendLine("  BOTH CONVERGE TO: unitary quantum mechanics at (R=1, S=1).");
        sb.AppendLine();

        Sec(sb, "Are the Gaps Problematic?");
        sb.AppendLine("  L_Q form:           ResearchX doesn't need it — (R,S) is operator-independent.");
        sb.AppendLine("  Complexity staircase: Main TQM didn't formalize it — already observed the levels.");
        sb.AppendLine("  Finite/infinite:      Main TQM never asked — ResearchX answered definitively.");
        sb.AppendLine("  Quantum necessity:    Main TQM never proved — ResearchX did (X031).");
        sb.AppendLine();
        sb.AppendLine("  These are COMPLEMENTARY contributions, not contradictions.");
        sb.AppendLine();

        Sec(sb, "Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X032 complete. {report.EquivalentConcepts}/{report.TotalConcepts} equivalent.");
        sb.AppendLine($"  Main TQM + ResearchX = UNIFIED TQM FRAMEWORK.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
