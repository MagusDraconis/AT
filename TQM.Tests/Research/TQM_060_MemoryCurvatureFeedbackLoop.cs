using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_060_MemoryCurvatureFeedbackLoop : ResearchTestBase
{
    private static readonly double[] Betas = { 0.0, 0.05, 0.1, 0.2, 0.5, 1.0, 2.0 };
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int Seeds = 2;
    private const int BaseSeed = 600517293;

    public TQM_060_MemoryCurvatureFeedbackLoop(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_060_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-060 Memory\u2013Curvature Feedback Loop");

        report.AppendLine("TQM-060: Does Memory-Generated Curvature Feed Back Into Memory?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-059 showed memory (β) is the dominant curvature source.");
        report.AppendLine("  This experiment tests the FEEDBACK direction:");
        report.AppendLine("  does curvature then influence future memory formation?");
        report.AppendLine();
        report.AppendLine("  H0: One-way — memory creates curvature, no feedback.");
        report.AppendLine("  H1: Feedback — curvature reshapes trajectories,");
        report.AppendLine("      which create different future memory.");
        report.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  β: [{string.Join(", ", Betas)}]");
        report.AppendLine($"  Seeds: {Seeds} per β");
        report.AppendLine($"  Cycles: 50 experience cycles per condensate");
        report.AppendLine($"  Each cycle: perturbation → recovery → curvature measurement");
        report.AppendLine($"  Measurements every 5 cycles");
        report.AppendLine($"  Total runs: {Betas.Length * Seeds}");
        report.AppendLine();

        // ── Run feedback loops ───────────────────────────────────────
        var bag = new ConcurrentBag<MemoryCurvatureFeedbackAnalyzer.FeedbackProfile>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, Betas.Length * Seeds, idx =>
        {
            int bi = idx / Seeds;
            int seed = BaseSeed + idx * 7919;
            bag.Add(MemoryCurvatureFeedbackAnalyzer.RunFeedbackLoop(
                Betas[bi], K, Lambda, N, seed));
        });

        sw.Stop();
        var profiles = bag.ToList();
        report.AppendLine($"  Completed {profiles.Count} feedback loops in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Analyze ──────────────────────────────────────────────────
        var fb = MemoryCurvatureFeedbackAnalyzer.AnalyzeFeedback(profiles);

        // ── Section 3: Memory Growth Analysis ────────────────────────
        AppendSection(report, "3. Memory Growth Analysis");

        report.AppendLine("  β     │ Mem Start │ Mem End  │ Mem Growth │ MemRate/Cycle");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in profiles.OrderBy(p => p.Beta))
        {
            double memStart = p.History.First().MemoryScore;
            double memEnd = p.History.Last().MemoryScore;
            report.AppendLine($"  {p.Beta,4:F2} │ {memStart,9:F4} │ {memEnd,8:F4} │ {memEnd - memStart,9:F4} │ {p.MemoryGrowthRate,12:F6}");
        }
        report.AppendLine();

        report.AppendLine($"  Q1: Does higher curvature increase future memory formation?");
        report.AppendLine($"    {(profiles.Any(p => p.MemoryGrowthRate > 0.001) ? "YES \u2014 Memory grows over cycles" : "NO \u2014 Memory is stable")}");
        report.AppendLine();

        // ── Section 4: Curvature Evolution ───────────────────────────
        AppendSection(report, "4. Curvature Evolution");

        report.AppendLine("  β     │ Curv Init│ Curv End │ Curv Growth │ CurvRate/Cycle");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in profiles.OrderBy(p => p.Beta))
        {
            double curvStart = p.History.First().Curvature;
            double curvEnd = p.History.Last().Curvature;
            report.AppendLine($"  {p.Beta,4:F2} │ {curvStart,8:F4} │ {curvEnd,8:F4} │ {curvEnd - curvStart,9:F4} │ {p.CurvatureGrowthRate,12:F6}");
        }
        report.AppendLine();

        report.AppendLine($"  Q2: Does memory growth increase curvature?");
        report.AppendLine($"    {(profiles.Any(p => p.CurvatureGrowthRate > 0.001) ? "YES \u2014 Curvature grows over cycles" : "NO \u2014 Curvature is stable")}");
        report.AppendLine();

        // ── Section 5: Feedback Coefficient ──────────────────────────
        AppendSection(report, "5. Feedback Coefficient");

        report.AppendLine("  β     │ Feedback Coeff │ Saturated? │ Sat. Cycle");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in profiles.OrderBy(p => p.Beta))
            report.AppendLine($"  {p.Beta,4:F2} │ {p.FeedbackCoefficient,13:F4} │ {(p.Saturated ? "YES" : "no"),9} │ {(p.Saturated ? p.SaturationCycle.ToString() : "\u2014"),9}");

        report.AppendLine();
        report.AppendLine($"  Mean feedback coefficient: {fb.MeanFeedbackCoefficient:F4}");
        report.AppendLine($"  β-feedback correlation:    {fb.BetaFeedbackCorrelation:F4}");
        report.AppendLine();

        report.AppendLine($"  Q3: Does a positive feedback loop exist?");
        report.AppendLine($"    {(fb.MeanFeedbackCoefficient > 0.3 ? $"YES \u2014 Mean feedback r = {fb.MeanFeedbackCoefficient:F3}" : "NO \u2014 No significant feedback")}");
        report.AppendLine();

        // ── Section 6: Saturation Analysis ───────────────────────────
        AppendSection(report, "6. Saturation Analysis");

        int saturatedCount = profiles.Count(p => p.Saturated);
        report.AppendLine($"  Saturated profiles: {saturatedCount}/{profiles.Count}");
        report.AppendLine($"  Mean saturation cycle: {(saturatedCount > 0 ? profiles.Where(p => p.Saturated).Average(p => p.SaturationCycle) : 0):F0}");
        report.AppendLine();

        report.AppendLine($"  Q4: Does feedback saturate?");
        report.AppendLine($"    {(saturatedCount > profiles.Count / 2 ? "YES \u2014 Majority of profiles reach saturation" : saturatedCount > 0 ? "PARTIALLY \u2014 Some saturation observed" : "NO \u2014 No saturation detected")}");
        report.AppendLine();

        // Sample evolution trace.
        var sample = profiles.FirstOrDefault(p => p.Beta > 0.4) ?? profiles.First();
        report.AppendLine("  Sample evolution (β=" + sample.Beta + "):");
        report.AppendLine("  Cycle │ Memory   │ Curvature");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var m in sample.History)
            report.AppendLine($"  {m.Cycle,5} │ {m.MemoryScore,8:F4} │ {m.Curvature,8:F4}");
        report.AppendLine();

        // ── Section 7: Geometric Self-Organization ────────────────────
        AppendSection(report, "7. Geometric Self-Organization");

        report.AppendLine($"  Q5: Is there a critical β for self-reinforcement?");
        double? criticalBeta = profiles.Where(p => p.FeedbackCoefficient > 0.3).Select(p => (double?)p.Beta).Min();
        report.AppendLine($"    {(criticalBeta.HasValue ? $"YES \u2014 Critical β ≈ {criticalBeta:F2}" : "NO \u2014 No clear threshold")}");
        report.AppendLine();

        report.AppendLine($"  Q6: Can repeated experience permanently reshape geometry?");
        bool permanent = profiles.Any(p => Math.Abs(p.CurvatureGrowthRate) > 0.0005);
        report.AppendLine($"    {(permanent ? "YES \u2014 Curvature grows with repeated experience" : "NO \u2014 Geometry is stable across cycles")}");
        report.AppendLine();

        report.AppendLine($"  Q7: Does identity become stronger after multiple cycles?");
        bool stronger = profiles.Any(p => p.MemoryGrowthRate > 0.0005);
        report.AppendLine($"    {(stronger ? "YES \u2014 Memory score increases with repeated experience" : "NO \u2014 Identity strength is stable")}");
        report.AppendLine();

        // ── Section 8: Interpretation ────────────────────────────────
        AppendSection(report, "8. Interpretation");

        report.AppendLine($"  Classification: {fb.FeedbackClass}");
        report.AppendLine($"  {fb.Description}");
        report.AppendLine();

        // ── Section 9: Conclusion ────────────────────────────────────
        AppendSection(report, "9. Conclusion");

        report.AppendLine($"  C1. Classification: {fb.FeedbackClass}");
        report.AppendLine($"  C2. Mean feedback coefficient: {fb.MeanFeedbackCoefficient:F4}");
        report.AppendLine($"  C3. β-feedback correlation: {fb.BetaFeedbackCorrelation:F4}");
        report.AppendLine($"  C4. Saturated profiles: {saturatedCount}/{profiles.Count}");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-060 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
