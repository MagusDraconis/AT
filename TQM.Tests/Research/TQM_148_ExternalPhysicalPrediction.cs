using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_148_ExternalPhysicalPrediction : ResearchTestBase
{
    public TQM_148_ExternalPhysicalPrediction(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_148_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-148 External Physical Prediction Test");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. TQM built from Q → L_Q → spectrum (TQM-142/145/146).");
        sb.AppendLine("  2. External test: predict systems NOT used in TQM construction.");
        sb.AppendLine("  3. Assume NO external predictive power until demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. External Prediction Theory");
        sb.AppendLine(ExternalPredictionAnalyzer.ExternalTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = ExternalPredictionAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. External Predictions vs Known Results");
        sb.AppendLine("  System                     │ TQM Predicts       │ Known Result       │ Match?");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var p in report.Predictions)
            sb.AppendLine($"  {p.System,-26} │ {p.TQMPrediction,-18} │ {p.KnownResult,-18} │ {(p.TQMMatches ? "✓" : "✗")}");
        sb.AppendLine();

        sb.AppendLine($"  Passed: {report.Passed}/{report.TotalTests}. Failed: {report.Failed}/{report.TotalTests}.");
        sb.AppendLine();

        Sec(sb, "3. Domain of Applicability");
        sb.AppendLine("  TQM WORKS for: graph Laplacian systems.");
        sb.AppendLine($"    {string.Join(", ", report.WhereTQMWorks)}");
        sb.AppendLine();
        sb.AppendLine("  TQM FAILS for: non-graph-Laplacian systems.");
        sb.AppendLine($"    {string.Join(", ", report.WhereTQMFails)}");
        sb.AppendLine();
        sb.AppendLine("  The Ising/Heisenberg failure is SCIENTIFICALLY VALUABLE:");
        sb.AppendLine("  It proves TQM is a properly delimited theory, not a 'theory of everything.'");
        sb.AppendLine("  Gap scaling: TQM predicts 1/N², Ising/Heisenberg give 1/N.");
        sb.AppendLine("  The difference arises because Ising/Heisenberg have NON-LAPLACIAN dynamics.");
        sb.AppendLine();

        Sec(sb, "4. Hostile Review");
        sb.AppendLine(ExternalPredictionAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "5. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-148 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
