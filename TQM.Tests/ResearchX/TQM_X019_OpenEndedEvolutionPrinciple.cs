using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X019_OpenEndedEvolutionPrinciple : ResearchTestBase
{
    public TQM_X019_OpenEndedEvolutionPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X019_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X019 Open-Ended Evolution Principle");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. TQM-X018: 6-level staircase, L6 NOT observed.");
        sb.AppendLine("  2. X002-X004 all failed to achieve L6.");
        sb.AppendLine("  3. Hypothesis: L6 is CONDITIONAL, not impossible.");
        sb.AppendLine();

        Sec(sb, "1. Open-Ended Evolution Theory");
        sb.AppendLine(OpenEndedEvolutionAnalyzer.OpenEndedTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = OpenEndedEvolutionAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. L6 Requirement Audit");
        sb.AppendLine("  Requirement                          │ Satisfied? │ Bottleneck? │ Why");
        sb.AppendLine("  " + new string('─', 100));
        foreach (var r in report.Requirements)
            sb.AppendLine($"  {r.Requirement,-37} │ {(r.SatisfiedInTQM ? "YES" : "NO"),-10} │ {(r.IsBottleneck ? "YES" : "NO"),-11} │ {r.Why}");
        sb.AppendLine();
        sb.AppendLine($"  Satisfied: {report.Requirements.Count(r=>r.SatisfiedInTQM)}/{report.Requirements.Count}");
        sb.AppendLine($"  Bottlenecks: {report.Requirements.Count(r=>r.IsBottleneck)}");
        sb.AppendLine();

        Sec(sb, "3. Failure Root Cause Analysis");
        sb.AppendLine($"  Missing ingredient: {report.MissingIngredient}");
        sb.AppendLine();
        sb.AppendLine($"  Root cause: {report.FailureRootCause}");
        sb.AppendLine();
        sb.AppendLine("  The fundamental limit:");
        sb.AppendLine("    Hilbert space dim = N → N orthogonal eigenmodes");
        sb.AppendLine("    Fixed operator type → fixed carrier classes");
        sb.AppendLine("    Static graph → fixed fitness landscape");
        sb.AppendLine("    Closed system → finite resources");
        sb.AppendLine();
        sb.AppendLine("  ALL of these must be RELAXED for L6 to be possible.");
        sb.AppendLine();

        Sec(sb, "4. The Level 5 → Level 6 Gap");
        sb.AppendLine("  L5 (Evolution):");
        sb.AppendLine("    ✓ Operates WITHIN fixed carrier classes (eigenmodes, solitons)");
        sb.AppendLine("    ✓ Species diversify but within the same Fourier/soliton families");
        sb.AppendLine("    ✓ Innovation saturates at ~19 species");
        sb.AppendLine();
        sb.AppendLine("  L6 (Open-Ended):");
        sb.AppendLine("    ✗ Requires NEW carrier classes to emerge");
        sb.AppendLine("    ✗ Requires non-saturating species AND class diversity");
        sb.AppendLine("    ✗ No mechanism exists in current TQM");
        sb.AppendLine();
        sb.AppendLine("  The L5→L6 jump is QUALITATIVELY different from L4→L5.");
        sb.AppendLine("  It may require a different KIND of system entirely.");
        sb.AppendLine();

        Sec(sb, "5. Possible Routes to L6");
        sb.AppendLine("  Route 1: DYNAMIC GRAPH TOPOLOGY");
        sb.AppendLine("    Q charges move → graph changes → new eigenmode FAMILIES");
        sb.AppendLine("    Requires: graph rewiring, not just node motion");
        sb.AppendLine();
        sb.AppendLine("  Route 2: NICHE CONSTRUCTION");
        sb.AppendLine("    Species modify graph → new eigenmodes → new species → ...");
        sb.AppendLine("    Requires: species→graph feedback loop");
        sb.AppendLine();
        sb.AppendLine("  Route 3: CO-EVOLUTION");
        sb.AppendLine("    Interacting species create new selective pressures");
        sb.AppendLine("    Requires: mutualistic/antagonistic dynamics");
        sb.AppendLine();
        sb.AppendLine("  Route 4: OPEN SYSTEMS");
        sb.AppendLine("    External energy/matter input → expanding Hilbert space");
        sb.AppendLine("    Requires: coupling to an infinite reservoir");
        sb.AppendLine();
        sb.AppendLine("  Route 5: NONLINEAR OPERATOR SPACE");
        sb.AppendLine("    Nonlinearity creates new soliton families continuously");
        sb.AppendLine("    Requires: state-dependent operators L(ψ)");
        sb.AppendLine();
        sb.AppendLine("  NONE of these routes have been tested in TQM.");
        sb.AppendLine();

        Sec(sb, "6. Hostile Review");
        sb.AppendLine(OpenEndedEvolutionAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "7. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X019 complete. Classification: {report.Classification}");
        sb.AppendLine($"  L6 is CONDITIONAL — not achieved. Missing: carrier class generation.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
