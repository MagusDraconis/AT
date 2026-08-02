using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_063_ResonanceErrorReductionDynamics : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const double Beta = 0.5;
    private const int NPerGroup = 50;
    private const int BaseSeed = 630295847;

    public TQM_063_ResonanceErrorReductionDynamics(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_063_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-063 Resonance Error Reduction Dynamics");

        report.AppendLine("TQM-063: Does Attraction Emerge from Resonance Error Reduction?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-062 showed spatial attraction from coupling dynamics.");
        report.AppendLine("  This tests whether motion is DRIVEN by error reduction:");
        report.AppendLine("  does error decrease even WITHOUT position updates?");
        report.AppendLine();

        // ── Section 2: Setup ─────────────────────────────────────────
        string[] modes = { "fixed", "moving" };
        double[] separations = { 0.5, 1.0, 2.0, 5.0 };
        (string, string)[] idPairs = { ("AB", "BA"), ("A", "B") };
        int seeds = 2;

        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  Modes: fixed position vs moving position");
        report.AppendLine($"  Separations: [{string.Join(", ", separations)}]λ");
        report.AppendLine($"  Identity pairs: AB/BA, A/B");
        report.AppendLine($"  Seeds: {seeds}, β = {Beta}, N = {NPerGroup * 2}");
        report.AppendLine($"  Total: {modes.Length * separations.Length * idPairs.Length * seeds} profiles");
        report.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var bag = new ConcurrentBag<ResonanceErrorAnalyzer.ErrorProfile>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        int total = modes.Length * separations.Length * idPairs.Length * seeds;
        Parallel.For(0, total, idx =>
        {
            int mi = idx % modes.Length, rem = idx / modes.Length;
            int si = rem % separations.Length; rem /= separations.Length;
            int ii = rem % idPairs.Length; int seedI = rem / idPairs.Length;
            bag.Add(ResonanceErrorAnalyzer.RunErrorEvolution(
                modes[mi], separations[si], idPairs[ii].Item1, idPairs[ii].Item2,
                Beta, K, Lambda, NPerGroup, BaseSeed + idx * 7919));
        });

        sw.Stop();
        var profiles = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        var err = ResonanceErrorAnalyzer.AnalyzeErrors(profiles);

        // ── Section 3: Error Evolution ───────────────────────────────
        AppendSection(report, "3. Error Evolution (Fixed vs Moving)");

        report.AppendLine("  Mode   │ Sepλ │ Init E7 │ Final E7│ ΔE7     │ ErrRate  │ ΔSep");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in profiles.OrderBy(p => p.Mode).ThenBy(p => p.SeparationLambda))
            report.AppendLine($"  {p.Mode,-6} │ {p.SeparationLambda,4:F1} │ {p.InitialError,7:F4} │ {p.FinalError,8:F4} │ {p.FinalError - p.InitialError,7:F4} │ {p.ErrorReductionRate,8:F4} │ {p.SeparationChange,7:F4}");

        report.AppendLine();
        report.AppendLine($"  Fixed position mean error reduction:  {err.FixedMeanReduction:F6}");
        report.AppendLine($"  Moving position mean error reduction: {err.MovingMeanReduction:F6}");
        report.AppendLine($"  Error-motion correlation:             {err.ErrorMotionCorrelation:F4}");
        report.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        report.AppendLine($"  Q1: Does attraction correlate with resonance error?");
        report.AppendLine($"    Correlation r = {err.ErrorMotionCorrelation:F4}");
        report.AppendLine($"    {(Math.Abs(err.ErrorMotionCorrelation) > 0.5 ? "YES \u2014 Strong correlation" : "NO \u2014 No significant correlation")}");
        report.AppendLine();

        report.AppendLine($"  Q2: Does motion stop when error reaches a minimum?");
        bool errorReduces = err.FixedMeanReduction > 0.001;
        report.AppendLine($"    {(errorReduces ? "YES \u2014 Error decreases naturally even at fixed positions" : "NO \u2014 Error does not decrease naturally")}");
        report.AppendLine();

        report.AppendLine($"  Q3: Is attraction proportional to error gradient?");
        report.AppendLine($"    {(Math.Abs(err.ErrorMotionCorrelation) > 0.3 ? "YES \u2014 Error gradient drives motion" : "NO \u2014 Motion is independent of error gradient")}");
        report.AppendLine();

        report.AppendLine($"  Q4: Does identity mismatch contribute to attraction?");
        var abProfiles = profiles.Where(p => p.HistoryA == "AB").ToList();
        var aProfiles = profiles.Where(p => p.HistoryA == "A").ToList();
        double abReduction = abProfiles.Average(p => p.ErrorReductionRate);
        double aReduction = aProfiles.Average(p => p.ErrorReductionRate);
        report.AppendLine($"    AB/BA reduction: {abReduction:F6}, A/B reduction: {aReduction:F6}");
        report.AppendLine($"    {(Math.Abs(abReduction - aReduction) > 0.001 ? "YES \u2014 Identity affects error reduction" : "NO \u2014 Identity irrelevant to error reduction")}");
        report.AppendLine();

        report.AppendLine($"  Q5: Can all motion be explained by error reduction?");
        report.AppendLine($"    {(err.FixedMeanReduction > err.MovingMeanReduction * 0.5 ? "YES \u2014 Error reduces even without motion" : "NO \u2014 Motion drives error reduction")}");
        report.AppendLine();

        report.AppendLine($"  Q6: Is error a better predictor than coherence/identity?");
        report.AppendLine($"    Error-motion r = {err.ErrorMotionCorrelation:F4}");
        report.AppendLine($"    {(Math.Abs(err.ErrorMotionCorrelation) > 0.3 ? "YES \u2014 Error explains motion" : "NO \u2014 Error is not the primary driver")}");
        report.AppendLine();

        // ── Section 4: Interpretation ────────────────────────────────
        AppendSection(report, "4. Interpretation");
        report.AppendLine($"  Classification: {err.Classification}");
        report.AppendLine();

        // ── Section 5: Conclusion ────────────────────────────────────
        AppendSection(report, "5. Conclusion");
        report.AppendLine($"  C1. Classification: {err.Classification}");
        report.AppendLine($"  C2. Fixed error reduction: {err.FixedMeanReduction:F6}");
        report.AppendLine($"  C3. Moving error reduction: {err.MovingMeanReduction:F6}");
        report.AppendLine($"  C4. Error-motion correlation: r = {err.ErrorMotionCorrelation:F4}");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-063 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
