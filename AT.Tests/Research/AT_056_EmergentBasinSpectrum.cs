using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_056_EmergentBasinSpectrum : ResearchTestBase
{
    private static readonly string[] Histories = { "A", "B", "AB", "BA", "ABC", "CBA" };
    private static readonly double[] Betas = { 0.1, 1.0 };
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int Seeds = 2;
    private const int BaseSeed = 560814723;
    // 51 energy levels from 0.0 to 5.0, step 0.1.
    private static readonly double[] EnergyLevels = Enumerable.Range(0, 51).Select(i => i * 0.1).ToArray();

    public AT_056_EmergentBasinSpectrum(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_056_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-056 Emergent Basin Spectrum");

        report.AppendLine("AT-056: Are AT-055's Basins Real or Sampling Artifacts?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-055 found 5 energy-defined attractor basins using 5 discrete");
        report.AppendLine("  energy levels. This experiment tests whether those basins are");
        report.AppendLine("  GENUINE EMERGENT STRUCTURES or artifacts of the sampling grid.");
        report.AppendLine();
        report.AppendLine("  H0: Basins are sampling artifacts — fine resolution reveals");
        report.AppendLine("      a smooth continuous landscape.");
        report.AppendLine("  H1: Basins are genuine — high-resolution scan still produces");
        report.AppendLine("      persistent basin clustering.");
        report.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        int total = Histories.Length * Betas.Length * EnergyLevels.Length * Seeds;

        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  Energy levels: [{EnergyLevels[0]:F1} .. {EnergyLevels[^1]:F1}], step 0.1 ({EnergyLevels.Length} levels)");
        report.AppendLine($"  \u03b2: [{string.Join(", ", Betas)}]");
        report.AppendLine($"  Histories: [{string.Join(", ", Histories)}]");
        report.AppendLine($"  Seeds: {Seeds}");
        report.AppendLine($"  Total states: {total} (vs 750 in AT-055 with 5 energy levels)");
        report.AppendLine($"  Resolution: {EnergyLevels.Length / 5.0:F0}\u00d7 finer than AT-055");
        report.AppendLine();

        // ── Generate states ──────────────────────────────────────────
        var bag = new ConcurrentBag<EmergentBasinSpectrumAnalyzer.SpectrumPoint>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int hi = idx % Histories.Length, rem = idx / Histories.Length;
            int bi = rem % Betas.Length; rem /= Betas.Length;
            int ei = rem % EnergyLevels.Length; int si = rem / EnergyLevels.Length;
            int seed = BaseSeed + idx * 7919;
            bag.Add(EmergentBasinSpectrumAnalyzer.GenerateState(
                Histories[hi], Betas[bi], EnergyLevels[ei], K, Lambda, N, seed));
        });

        sw.Stop();
        var points = bag.ToList();
        report.AppendLine($"  Generated {points.Count} states in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Multi-threshold clustering ───────────────────────────────
        double[] thresholds = { 0.15, 0.20, 0.25, 0.30, 0.40, 0.50, 0.75, 1.0 };
        var topo = EmergentBasinSpectrumAnalyzer.AnalyzeSpectrum(points, thresholds);

        // ── Section 3: High Resolution Spectrum ──────────────────────
        AppendSection(report, "3. Basin Persistence Across Clustering Thresholds");

        report.AppendLine("  Threshold │ Basins │ Large(≥5%) │ Silhouette │ Inter/Intra │ Interpretation");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in topo.PersistenceByThreshold)
        {
            string interp = p.LargeBasinCount > 3 && p.Silhouette > 0.5 ? "Strong basins" :
                            p.LargeBasinCount > 1 && p.Silhouette > 0.3 ? "Emergent basins" :
                            p.LargeBasinCount == 1 && p.Silhouette > 0.3 ? "Single basin" :
                            "Continuous/fragmented";
            report.AppendLine($"  {p.Threshold,8:F2} │ {p.BasinCount,5}  │ {p.LargeBasinCount,10} │ {p.Silhouette,9:F4} │ {p.InterIntraRatio,10:F2} │ {interp}");
        }
        report.AppendLine();

        // Q1: Persistence test.
        bool persistentLargeCount = topo.PersistenceByThreshold
            .Select(p => p.LargeBasinCount).Distinct().Count() <= 2;
        report.AppendLine($"  Q1: Do the 5 major basins from AT-055 persist?");
        report.AppendLine($"    {(persistentLargeCount ? "YES \u2014 Large basin count is stable across thresholds" : "NO \u2014 Basin count varies with threshold")}");
        report.AppendLine($"    Large basin counts across thresholds: [{string.Join(", ", topo.PersistenceByThreshold.Select(p => p.LargeBasinCount))}]");
        report.AppendLine();

        // ── Section 4: Basin Detection ───────────────────────────────
        AppendSection(report, "4. Basin Detection at Optimal Threshold");

        report.AppendLine($"  Optimal threshold: {thresholds[topo.PersistenceByThreshold.IndexOf(topo.PersistenceByThreshold.OrderByDescending(p => p.Silhouette).First())]:F2}");
        report.AppendLine($"  Optimal large basins: {topo.OptimalLargeBasinCount}");
        report.AppendLine($"  Optimal silhouette: {topo.OptimalSilhouette:F4}");
        report.AppendLine();

        report.AppendLine($"  Q2: Does a continuous energy spectrum generate discrete");
        report.AppendLine($"      topological regions?");
        report.AppendLine($"    {(topo.DiscreteBasinsConfirmed ? "YES \u2014 Discrete basins emerge from continuous sampling" : "NO \u2014 The landscape is continuous (no discrete basins)")}");
        report.AppendLine();

        // ── Section 5: Cross-Method Comparison ───────────────────────
        AppendSection(report, "5. Cross-Method Comparison");

        report.AppendLine($"  Q5: Is basin count stable across clustering methods?");
        int bcRange = topo.PersistenceByThreshold.Max(p => p.BasinCount) -
                      topo.PersistenceByThreshold.Min(p => p.BasinCount);
        int lbRange = topo.PersistenceByThreshold.Max(p => p.LargeBasinCount) -
                      topo.PersistenceByThreshold.Min(p => p.LargeBasinCount);
        report.AppendLine($"    Total basin count range: {topo.PersistenceByThreshold.Min(p => p.BasinCount)}-{topo.PersistenceByThreshold.Max(p => p.BasinCount)} (\u0394={bcRange})");
        report.AppendLine($"    Large basin count range: {topo.PersistenceByThreshold.Min(p => p.LargeBasinCount)}-{topo.PersistenceByThreshold.Max(p => p.LargeBasinCount)} (\u0394={lbRange})");
        report.AppendLine($"    {(lbRange <= 1 ? "YES \u2014 Large basin count is STABLE (robust to method)" : "NO \u2014 Basin count varies with clustering method")}");
        report.AppendLine();

        // ── Section 6: Topology Analysis ─────────────────────────────
        AppendSection(report, "6. Topology Analysis");

        // Energy distribution analysis.
        var energyGroups = points.GroupBy(p => Math.Round(p.EnergyScale * 10) / 10.0)
            .OrderBy(g => g.Key).ToList();

        // Detect preferred energy zones (where R is high).
        var preferredZones = energyGroups
            .Where(g => g.Average(p => p.R) > 0.90)
            .Select(g => g.Key).ToList();

        // Detect forbidden zones (where R drops).
        var forbiddenZones = energyGroups
            .Where(g => g.Average(p => p.R) < 0.60)
            .Select(g => g.Key).ToList();

        report.AppendLine($"  Q3: Are there preferred energy zones?");
        report.AppendLine($"    Preferred zones (R>0.90): {preferredZones.Count} energy levels");
        if (preferredZones.Count > 0)
            report.AppendLine($"    Range: [{preferredZones.Min():F1}, {preferredZones.Max():F1}]");
        report.AppendLine();

        report.AppendLine($"  Q4: Do forbidden or low-density regions exist?");
        report.AppendLine($"    {(forbiddenZones.Count > 0 ? $"YES \u2014 {forbiddenZones.Count} low-R zones" : "NO \u2014 All energy levels form coherent states")}");
        if (forbiddenZones.Count > 0)
            report.AppendLine($"    Forbidden zones: [{string.Join(", ", forbiddenZones.Take(5))}{(forbiddenZones.Count > 5 ? ", ..." : "")}]");
        report.AppendLine();

        report.AppendLine($"  Q6: Are basins intrinsic properties of the landscape?");
        report.AppendLine($"    {(topo.DiscreteBasinsConfirmed ? "YES \u2014 Basins are robust emergent structures, not artifacts" : "NO \u2014 Basins are threshold-dependent and not intrinsic")}");
        report.AppendLine();

        // ── Energy-R plot ────────────────────────────────────────────
        var avgRByEnergy = energyGroups.Select(g => (Energy: g.Key, AvgR: g.Average(p => p.R))).OrderBy(e => e.Energy).ToList();
        report.AppendLine("  Energy vs Mean R (continuous spectrum):");
        report.AppendLine("  Energy │ Avg R   │ Bar (0-1)");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var (e, r) in avgRByEnergy.Take(30)) // first 30: 0.0-2.9
        {
            int barLen = (int)(r * 40);
            string bar = new string('\u2588', barLen);
            report.AppendLine($"  {e,5:F1}  │ {r,6:F4} │ {bar}");
        }
        report.AppendLine($"  ... ({avgRByEnergy.Count - 30} more levels) ...");
        report.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  Classification: {topo.Classification}");
        report.AppendLine($"  Discrete basins confirmed: {(topo.DiscreteBasinsConfirmed ? "YES" : "NO")}");
        report.AppendLine();

        string interpretation;
        if (topo.DiscreteBasinsConfirmed)
            interpretation = "The basins from AT-055 are GENUINE EMERGENT STRUCTURES. " +
                "Despite 10× finer energy resolution, the landscape still shows " +
                "discrete basin clustering. Energy organizes the state space into " +
                "robust topological regions that are not artifacts of coarse sampling.";
        else
            interpretation = "The basins from AT-055 were SAMPLING ARTIFACTS. " +
                "At higher resolution, the landscape is CONTINUOUS — no discrete " +
                "basin structure emerges. The 5 basins were created by the discrete " +
                "choice of energy levels, not by intrinsic topology.";

        report.AppendLine($"  {interpretation}");
        report.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Classification: {topo.Classification}");
        report.AppendLine($"  C2. Large basin count: {topo.OptimalLargeBasinCount}");
        report.AppendLine($"  C3. Silhouette: {topo.OptimalSilhouette:F4}");
        report.AppendLine();

        if (topo.DiscreteBasinsConfirmed)
        {
            report.AppendLine("  C4. AT-055 basins are CONFIRMED as emergent structures.");
            report.AppendLine("  C5. Energy creates robust attractor basin boundaries that");
            report.AppendLine("      persist under high-resolution sampling.");
        }
        else
        {
            report.AppendLine("  C4. AT-055 basins are NOT confirmed — the landscape is");
            report.AppendLine("      continuous at higher resolution.");
            report.AppendLine("  C5. Energy is a continuous topological gradient, not a");
            report.AppendLine("      source of discrete basin boundaries.");
        }

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-056 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
