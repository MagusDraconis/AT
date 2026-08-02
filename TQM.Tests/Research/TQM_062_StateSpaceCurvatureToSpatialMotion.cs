using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_062_StateSpaceCurvatureToSpatialMotion : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerGroup = 50;
    private const int BaseSeed = 620714893;

    public TQM_062_StateSpaceCurvatureToSpatialMotion(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_062_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-062 State-Space Curvature to Spatial Motion");

        report.AppendLine("TQM-062: Can State-Space Curvature Generate Spatial Motion?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-058/059 showed memory creates state-space curvature.");
        report.AppendLine("  This tests whether that curvature produces observable");
        report.AppendLine("  spatial motion via phase-coupling gradients.");
        report.AppendLine();

        // ── Section 2: Setup ─────────────────────────────────────────
        var betas = new[] { 0.0, 0.5, 1.0, 2.0 };
        int seeds = 2;
        var configs = new List<(double, double)>();
        foreach (double ba in betas)
            foreach (double bb in betas.Where(b => b >= ba))
                configs.Add((ba, bb));

        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  β pairs: {configs.Count} combinations from [{string.Join(", ", betas)}]");
        report.AppendLine($"  Seeds: {seeds} per pair, Total: {configs.Count * seeds} simulations");
        report.AppendLine($"  N = {NPerGroup * 2}, 3000 iters, snapshots every 100");
        report.AppendLine($"  Position dynamics: gradient descent on coupling energy");
        report.AppendLine($"  Group A: center (0.3, 0.5), Group B: center (0.7, 0.5)");
        report.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var bag = new ConcurrentBag<SpatialCurvatureAnalyzer.SpatialDriftResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, configs.Count * seeds, idx =>
        {
            int ci = idx / seeds, si = idx % seeds;
            var (ba, bb) = configs[ci];
            bag.Add(SpatialCurvatureAnalyzer.RunSpatialDynamics(
                ba, bb, K, Lambda, NPerGroup, BaseSeed + idx * 7919));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        var drift = SpatialCurvatureAnalyzer.AnalyzeDrift(results);

        // ── Section 3: Spatial Motion ────────────────────────────────
        AppendSection(report, "3. Spatial Motion Results");

        report.AppendLine("  β_A  β_B │ Drift A  │ Drift B  │ ΔSep     │ Converge? │ Vel A   │ Vel B");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var r in results.OrderBy(r => r.BetaA).ThenBy(r => r.BetaB))
        {
            string conv = r.Converges ? "\u25BC YES" : "\u25B2 no";
            double vA = r.History.Average(h => h.VelocityA);
            double vB = r.History.Average(h => h.VelocityB);
            report.AppendLine($"  {r.BetaA,3:F1} {r.BetaB,4:F1} │ {r.MeanDriftA,7:F4} │ {r.MeanDriftB,7:F4} │ {r.SeparationChange,8:F4} │ {conv,8} │ {vA,7:F4} │ {vB,7:F4}");
        }
        report.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        report.AppendLine($"  Q1: Do curvature gradients create motion?");
        report.AppendLine($"    {(results.Any(r => r.MeanDriftA > 0.001) ? "YES \u2014 Measurable spatial drift detected" : "NO \u2014 No significant spatial motion")}");
        report.AppendLine();

        int converging = results.Count(r => r.Converges);
        report.AppendLine($"  Q2: Do condensates move toward high-curvature regions?");
        report.AppendLine($"    Converging pairs: {converging}/{results.Count}");
        report.AppendLine($"    {(converging > results.Count / 2 ? "YES \u2014 Majority converge" : "NO \u2014 No systematic convergence")}");
        report.AppendLine();

        report.AppendLine($"  Q3: Can curvature create effective attraction?");
        report.AppendLine($"    {(drift.SpatialClass.StartsWith("D:") ? "YES \u2014 Effective attraction from geometry alone" : drift.SpatialClass.StartsWith("C:") ? "PARTIALLY \u2014 Directed motion observed" : "NO \u2014 No effective attraction")}");
        report.AppendLine();

        // Sample trajectory.
        var sample = results.FirstOrDefault(r => r.BetaA > 0 && r.BetaB > 0) ?? results.First();
        report.AppendLine("  Sample trajectory (β_A=" + sample.BetaA + ", β_B=" + sample.BetaB + "):");
        report.AppendLine("  Iter │ X_A    Y_A    │ X_B    Y_B    │ Sep     │ R_A     R_B");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var h in sample.History)
            report.AppendLine($"  {h.Iteration,4} │ {h.CenterX_A,6:F3} {h.CenterY_A,6:F3} │ {h.CenterX_B,6:F3} {h.CenterY_B,6:F3} │ {h.Separation,7:F4} │ {h.RA,5:F3}  {h.RB,5:F3}");
        report.AppendLine();

        report.AppendLine($"  Q4: Can stable spatial trajectories emerge?");
        bool stable = results.Any(r => Math.Abs(r.SeparationChange) < 0.02);
        report.AppendLine($"    {(stable ? "YES \u2014 Stable configurations exist" : "NO \u2014 Trajectories are unstable")}");
        report.AppendLine();

        report.AppendLine($"  Q5: Does stronger memory produce stronger motion?");
        report.AppendLine($"    β-drift correlation: r = {drift.BetaDriftCorrelation:F4}");
        report.AppendLine($"    {(Math.Abs(drift.BetaDriftCorrelation) > 0.5 ? "YES \u2014 Memory strength correlates with spatial drift" : "NO \u2014 No significant correlation")}");
        report.AppendLine();

        // ── Interpretation ───────────────────────────────────────────
        AppendSection(report, "4. Interpretation");
        report.AppendLine($"  Classification: {drift.SpatialClass}");
        report.AppendLine($"  Mean separation change: {drift.MeanConvergenceRate:F6}");
        report.AppendLine($"  Convergent fraction: {converging}/{results.Count}");
        report.AppendLine();

        // ── Conclusion ───────────────────────────────────────────────
        AppendSection(report, "5. Conclusion");
        report.AppendLine($"  C1. Classification: {drift.SpatialClass}");
        report.AppendLine($"  C2. β-drift correlation: r = {drift.BetaDriftCorrelation:F4}");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-062 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
