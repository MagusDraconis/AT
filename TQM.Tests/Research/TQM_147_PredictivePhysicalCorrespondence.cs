using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_147_PredictivePhysicalCorrespondence : ResearchTestBase
{
    public TQM_147_PredictivePhysicalCorrespondence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_147_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-147 Predictive Physical Correspondence");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Q → L_Q → spectrum → observables (TQM-145/146).");
        sb.AppendLine("  2. Predictions must be BLIND (computed before comparison).");
        sb.AppendLine("  3. Assume NO predictive power beyond graph theory.");
        sb.AppendLine();

        Sec(sb, "1. Predictive Theory");
        sb.AppendLine(PredictivePhysicsAnalyzer.PredictiveTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = PredictivePhysicsAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Blind Predictions vs Known Results");
        sb.AppendLine("  Geometry         │ Observable           │ Predicted │ Known    │ Error  │ OK?");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var p in report.Predictions)
            sb.AppendLine($"  {p.Geometry,-16} │ {p.Observable,-20} │ {p.PredictedValue,8:F4} │ {p.KnownValue,8:F4} │ {p.Error,6:P1} │ {(p.WithinTolerance ? "✓" : "✗")}");
        sb.AppendLine();

        sb.AppendLine($"  {report.AccuratePredictions}/{report.TotalPredictions} predictions within 5% tolerance.");
        sb.AppendLine($"  Mean error: {report.MeanError:P1}.");
        sb.AppendLine($"  Novel predictions: {report.NovelPredictions}.");
        sb.AppendLine();

        Sec(sb, "3. Key Prediction: m_eff = Q²/π²");
        sb.AppendLine("  This predicts effective mass for ANY Q, derived from L_Q alone.");
        sb.AppendLine("  Q=10 → m_eff ≈ 10.13, Q=100 → m_eff ≈ 1013.");
        sb.AppendLine("  This IS the continuum limit of the discrete Laplacian.");
        sb.AppendLine("  Known from spectral graph theory since at least the 1970s.");
        sb.AppendLine();

        Sec(sb, "4. Hostile Review");
        sb.AppendLine(PredictivePhysicsAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "5. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-147 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
