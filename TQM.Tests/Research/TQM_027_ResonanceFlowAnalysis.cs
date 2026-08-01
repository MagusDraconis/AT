using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_027_ResonanceFlowAnalysis : ResearchTestBase
{
    private static readonly int[] Ns = { 100, 500 };
    private static readonly double[] Ks = { 2, 5 };
    private static readonly double[] Lambdas = { 0.05, 0.10, 0.50 };
    private static readonly string[] Placements = { "Uniform", "MultipleClusters" };
    private const int SeedsPerCombo = 5;
    private const int BaseSeed = 5702887;

    public TQM_027_ResonanceFlowAnalysis(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void TQM_027_RunFlowAnalysis()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-027 Resonance Flow Analysis");
        report.AppendLine("TQM-027: Does Dynamic Resonance Flow Drive Condensation?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  Static metrics (density, symmetry, connectivity) predict but don't");
        report.AppendLine("  gate condensation. This experiment tests whether DYNAMIC resonance");
        report.AppendLine("  flow — phase velocity convergence — triggers condensate formation.");
        report.AppendLine();

        int total = Ns.Length * Ks.Length * Lambdas.Length * Placements.Length * SeedsPerCombo;
        AppendSection(report, "2. Flow Definitions");
        report.AppendLine($"  {total} runs. Flow metrics: convergence (-∇·ω), dR/dt, frequency gradient.");
        report.AppendLine();

        var bag = new ConcurrentBag<ResonanceFlowAnalyzer.FlowPoint>();
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var combos = (from n in Ns from k in Ks from lam in Lambdas from p in Placements select (n, k, lam, p)).ToList();

        Parallel.ForEach(combos, combo =>
        {
            var (n, k, lam, p) = combo;
            for (int s = 0; s < SeedsPerCombo; s++)
            {
                var rng = new Random(BaseSeed + n * 10000 + (int)(k * 1000) + (int)(lam * 100000) + p.GetHashCode() % 10000 + s * 7919);
                var pts = ResonanceFlowAnalyzer.Analyze(n, k, lam, p, rng);
                foreach (var pt in pts) bag.Add(pt);
            }
        });

        sw.Stop();
        var points = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms. Points: {points.Count}");
        report.AppendLine();

        if (points.Count < 50) { report.AppendLine("Insufficient data."); Output.WriteLine(report.ToString()); return; }

        // ── 3. Flow Analysis ────────────────────────────────────
        AppendSection(report, "3. Flow Metric Distributions at Condensate Birth");

        report.AppendLine("  Metric            │ Mean     │ CV     │ Range");
        report.AppendLine("  ──────────────────┼──────────┼────────┼──────────");

        double meanConv = points.Average(p => p.FlowConvergence);
        double cvConv = CV(points.Select(p => p.FlowConvergence).ToList());
        report.AppendLine($"  Flow Convergence  │ {meanConv,8:F4} │ {cvConv,6:F3} │ [{points.Min(p => p.FlowConvergence):F3}, {points.Max(p => p.FlowConvergence):F3}]");

        double meanDRDT = points.Average(p => p.DRDT);
        double cvDRDT = CV(points.Select(p => p.DRDT).ToList());
        report.AppendLine($"  dR/dt             │ {meanDRDT,8:F4} │ {cvDRDT,6:F3} │ [{points.Min(p => p.DRDT):F3}, {points.Max(p => p.DRDT):F3}]");

        double meanFG = points.Average(p => p.FreqGradient);
        double cvFG = CV(points.Select(p => p.FreqGradient).ToList());
        report.AppendLine($"  Freq Gradient     │ {meanFG,8:F4} │ {cvFG,6:F3} │ [{points.Min(p => p.FreqGradient):F3}, {points.Max(p => p.FreqGradient):F3}]");

        double meanDens = points.Average(p => p.Density);
        double cvDens = CV(points.Select(p => p.Density).ToList());
        report.AppendLine($"  Density           │ {meanDens,8:F4} │ {cvDens,6:F3} │ [{points.Min(p => p.Density):F3}, {points.Max(p => p.Density):F3}]");

        report.AppendLine();

        // ── 4. Birth Events ─────────────────────────────────────
        AppendSection(report, "4. Flow Sign at Condensate Birth");

        int positiveConv = points.Count(p => p.FlowConvergence > 0);
        int negativeConv = points.Count(p => p.FlowConvergence < 0);
        int positiveDRDT = points.Count(p => p.DRDT > 0);

        report.AppendLine($"  Convergent flow (>0) : {positiveConv}/{points.Count} ({positiveConv * 100.0 / points.Count:F0}%)");
        report.AppendLine($"  Divergent flow  (<0) : {negativeConv}/{points.Count} ({negativeConv * 100.0 / points.Count:F0}%)");
        report.AppendLine($"  Positive dR/dt  (>0) : {positiveDRDT}/{points.Count} ({positiveDRDT * 100.0 / points.Count:F0}%)");
        report.AppendLine();

        // ── 5. Flow Correlations ────────────────────────────────
        AppendSection(report, "5. Comparison: Flow vs Density vs Geometry");

        // Placeholder for ranking — compute cross-λ CV for each.
        var flowCV = GroupCV(points, p => p.FlowConvergence, p => p.Density > 0.05 ? "High" : "Low");
        var drdtCV = GroupCV(points, p => p.DRDT, p => p.Density > 0.05 ? "High" : "Low");

        report.AppendLine($"  Flow convergence consistency: {flowCV:F3}");
        report.AppendLine($"  dR/dt consistency         : {drdtCV:F3}");
        report.AppendLine();
        report.AppendLine("  Relative to density (CV=0.144 across λ from TQM-023):");
        report.AppendLine($"    Flow CV = {flowCV:F3} vs Density CV = 0.144");
        report.AppendLine($"    Flow is {(flowCV < 0.144 ? "MORE" : "LESS")} universal than density");
        report.AppendLine();

        // ── 6. Interpretation ───────────────────────────────────
        AppendSection(report, "6. Interpretation");

        report.AppendLine("  Q1. Form at flow convergence points?");
        report.AppendLine($"    {positiveConv} of {points.Count} births at convergent flow —");
        report.AppendLine($"    {(positiveConv > points.Count * 0.6 ? "YES — flow convergence predicts birth" : "INCONCLUSIVE — births occur at both convergent and divergent flow")}");

        report.AppendLine();
        report.AppendLine("  Q2. Critical flow intensity?");
        report.AppendLine($"    Mean flow convergence at birth: {meanConv:F4}");

        report.AppendLine();
        report.AppendLine("  Q3. Better than density?");
        report.AppendLine($"    {(cvConv < cvDens ? "Potentially — lower CV" : "Not conclusively")}");

        report.AppendLine();

        AppendSection(report, "7. Conclusion");
        report.AppendLine($"  C1. Dynamic resonance flow {(positiveConv > points.Count * 0.6 ? "IS" : "is not conclusively")} a predictor of condensate birth.");
        report.AppendLine();
        report.AppendLine("  C2. Flow analysis opens a new dimension of TQM investigation:");
        report.AppendLine("      where static metrics describe structure, flow describes dynamics.");
        report.AppendLine();
        report.AppendLine("  C3. Future work: track flow throughout condensate lifetime, not just at birth.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-027 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static double CV(List<double> values)
    {
        if (values.Count < 2) return 0; double m = values.Average();
        return m > 1e-10 ? Math.Sqrt(values.Average(v => (v - m) * (v - m))) / m : 0;
    }

    private static double GroupCV<T>(List<ResonanceFlowAnalyzer.FlowPoint> data,
        Func<ResonanceFlowAnalyzer.FlowPoint, double> sel,
        Func<ResonanceFlowAnalyzer.FlowPoint, T> key)
    {
        var g = data.GroupBy(key).Select(grp => grp.Select(sel).Average()).Where(v => Math.Abs(v) > 1e-10).ToList();
        if (g.Count < 2) return 0; double m = g.Average();
        return Math.Sqrt(g.Average(v => (v - m) * (v - m))) / m;
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
