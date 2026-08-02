using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_057_ResonanceStateSpaceGeodesics : ResearchTestBase
{
    private static readonly string[] Histories = { "A", "B", "AB", "BA" };
    private const double Beta = 0.5;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int Seeds = 3;
    private const int BaseSeed = 570296831;

    public TQM_057_ResonanceStateSpaceGeodesics(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_057_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-057 Resonance State-Space Geodesics");

        report.AppendLine("TQM-057: Do State Transitions Follow Preferred Geodesic Paths?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-056 revealed a single continuous attractor landscape.");
        report.AppendLine("  This experiment tests HOW states move through that landscape:");
        report.AppendLine("  do recovery trajectories follow preferred paths (geodesics)");
        report.AppendLine("  or wander randomly through state space?");
        report.AppendLine();

        // ── Section 2: Trajectory Collection ─────────────────────────
        int totalRuns = Histories.Length * Seeds;
        int pertTypes = 3;

        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  Histories: [{string.Join(", ", Histories)}]");
        report.AppendLine($"  Seeds: {Seeds}, \u03b2 = {Beta}");
        report.AppendLine($"  Total condensates: {totalRuns}");
        report.AppendLine($"  Perturbations: EnergyCollapse, PhaseNoise, MemoryDisrupt");
        report.AppendLine($"  Recovery tracking: 2000 iterations, snapshots every 50 iters");
        report.AppendLine($"  Total trajectories: {totalRuns * pertTypes}");
        report.AppendLine();

        // ── Collect trajectories ─────────────────────────────────────
        var allTrajs = new ConcurrentBag<StateSpaceGeodesicAnalyzer.Trajectory>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, totalRuns, idx =>
        {
            int hi = idx / Seeds, si = idx % Seeds;
            int seed = BaseSeed + idx * 7919;
            var trajs = StateSpaceGeodesicAnalyzer.CollectTrajectories(
                Histories[hi], Beta, K, Lambda, N, seed);
            foreach (var t in trajs) allTrajs.Add(t);
        });

        sw.Stop();
        var trajectories = allTrajs.ToList();
        report.AppendLine($"  Collected {trajectories.Count} trajectories in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Analyze ──────────────────────────────────────────────────
        var geo = StateSpaceGeodesicAnalyzer.Analyze(trajectories);

        // ── Section 3: Trajectory Metrics ────────────────────────────
        AppendSection(report, "3. Trajectory Metrics");

        report.AppendLine("  Perturbation       │ Path Len │ Curvature │ Converge(iter) │ Shortest Ratio");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var pt in Enum.GetValues<StateSpaceGeodesicAnalyzer.PerturbType>())
        {
            var sub = trajectories.Where(t => t.Perturbation == pt).ToList();
            if (sub.Count == 0) continue;
            double pl = sub.Average(t => t.PathLength);
            double cv = sub.Average(t => t.Curvature);
            double cs = sub.Average(t => t.ConvergenceSpeed);
            string name = pt.ToString().Length > 14 ? pt.ToString()[..14] : pt.ToString();
            report.AppendLine($"  {name,-18} │ {pl,8:F4} │ {cv,8:F3} │ {cs,13:F0} │ {geo.ShortestPathRatio,13:F2}\u00d7");
        }
        report.AppendLine();

        // ── Section 4: Repeatability ─────────────────────────────────
        AppendSection(report, "4. Path Repeatability");

        report.AppendLine($"  Overall metrics:");
        report.AppendLine($"    Mean path length:       {geo.MeanPathLength:F4}");
        report.AppendLine($"    Mean curvature:         {geo.MeanCurvature:F3} rad");
        report.AppendLine($"    Mean convergence:       {geo.ConvergenceScore:F0} iterations");
        report.AppendLine($"    Repeatability score:    {geo.RepeatabilityScore:P1}");
        report.AppendLine($"    Shortest path ratio:    {geo.ShortestPathRatio:F2}\u00d7");
        report.AppendLine();

        report.AppendLine($"  Q1: Do repeated transitions follow the same path?");
        report.AppendLine($"    {(geo.RepeatabilityScore > 0.7 ? "YES \u2014 Paths are highly repeatable (deterministic)" : geo.RepeatabilityScore > 0.4 ? "PARTIALLY \u2014 Paths show moderate consistency" : "NO \u2014 Paths vary significantly between runs")}");
        report.AppendLine($"    Repeatability: {geo.RepeatabilityScore:P1}");
        report.AppendLine();

        report.AppendLine($"  Q2: Are recovery trajectories unique?");
        var pertPaths = new Dictionary<StateSpaceGeodesicAnalyzer.PerturbType, double>();
        foreach (var pt in Enum.GetValues<StateSpaceGeodesicAnalyzer.PerturbType>())
        {
            var sub = trajectories.Where(t => t.Perturbation == pt).ToList();
            pertPaths[pt] = sub.Average(t => t.PathLength);
        }
        double pathSpread = pertPaths.Values.Max() - pertPaths.Values.Min();
        report.AppendLine($"    {(pathSpread < 0.5 ? "NO \u2014 All perturbations produce similar paths" : "YES \u2014 Different perturbations follow different paths")}");
        report.AppendLine($"    Path length range: {pertPaths.Values.Min():F4} - {pertPaths.Values.Max():F4}");
        report.AppendLine();

        report.AppendLine($"  Q3: Does state space contain preferred corridors?");
        report.AppendLine($"    {(geo.RepeatabilityScore > 0.5 ? "YES \u2014 High repeatability indicates preferred corridors" : "NO \u2014 Low repeatability suggests unconstrained wandering")}");
        report.AppendLine();

        report.AppendLine($"  Q4: Can transitions be predicted from starting state?");
        report.AppendLine($"    {(geo.RepeatabilityScore > 0.7 ? "YES \u2014 Trajectories are deterministic from initial state" : geo.RepeatabilityScore > 0.4 ? "PARTIALLY \u2014 Some predictability" : "NO \u2014 Trajectories are not predictable")}");
        report.AppendLine();

        report.AppendLine($"  Q5: Does a geometry emerge from trajectory structure?");
        string q5 = geo.ShortestPathRatio < 1.5
            ? "YES \u2014 Paths are near-geodesic (ratio < 1.5\u00d7)"
            : geo.ShortestPathRatio < 3.0
            ? "PARTIALLY \u2014 Paths are somewhat direct"
            : "NO \u2014 Paths are far from geodesic";
        report.AppendLine($"    {q5}");
        report.AppendLine();

        // ── Section 5: Per-Perturbation Breakdown ────────────────────
        AppendSection(report, "5. Per-Perturbation Trajectory Analysis");

        foreach (var pt in Enum.GetValues<StateSpaceGeodesicAnalyzer.PerturbType>())
        {
            var sub = trajectories.Where(t => t.Perturbation == pt).ToList();
            if (sub.Count == 0) continue;

            report.AppendLine($"  {pt}:");
            report.AppendLine($"    Trajectories: {sub.Count}");
            report.AppendLine($"    Mean path length: {sub.Average(t => t.PathLength):F4}");
            report.AppendLine($"    Mean curvature: {sub.Average(t => t.Curvature):F3} rad");
            report.AppendLine($"    Mean convergence: {sub.Average(t => t.ConvergenceSpeed):F0} iters");

            // Show trajectory snapshots for first 5 points.
            var first = sub.First();
            report.AppendLine($"    Sample trajectory (first run):");
            report.AppendLine($"    Iter │ R       │ Freq    │ PhaseVar │ Energy   │ MemScore");
            report.AppendLine($"    \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
            foreach (var p in first.Points.Take(8))
                report.AppendLine($"    {p.Iteration,4} │ {p.R,6:F4} │ {p.Freq,7:F4} │ {p.PhaseVar,8:F4} │ {p.Energy,8:F4} │ {p.MemScore,8:F4}");
            report.AppendLine($"    ... ({first.Points.Count - 8} more snapshots) ...");
            report.AppendLine();
        }

        // ── Section 6: Interpretation ────────────────────────────────
        AppendSection(report, "6. Interpretation");

        report.AppendLine($"  Classification: {geo.Classification}");
        report.AppendLine();

        string interp;
        if (geo.RepeatabilityScore > 0.7)
            interp = "State transitions follow DETERMINISTIC GEODESICS. The resonance " +
                "landscape has a well-defined geometry where recovery follows " +
                "preferred paths that are predictable and repeatable.";
        else if (geo.RepeatabilityScore > 0.4)
            interp = "State transitions follow PREFERRED CORRIDORS with moderate " +
                "predictability. The landscape channels trajectories but allows " +
                "some variation between runs.";
        else
            interp = "State transitions are WEAKLY STRUCTURED. While not purely " +
                "random, trajectories show significant variation and lack " +
                "well-defined geodesic paths.";
        report.AppendLine($"  {interp}");
        report.AppendLine();

        // ── Section 7: Conclusion ────────────────────────────────────
        AppendSection(report, "7. Conclusion");

        report.AppendLine($"  C1. Classification: {geo.Classification}");
        report.AppendLine($"  C2. Repeatability: {geo.RepeatabilityScore:P1}");
        report.AppendLine($"  C3. Shortest path ratio: {geo.ShortestPathRatio:F2}\u00d7");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-057 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
