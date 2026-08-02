using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_055_ResonanceLandscapeMapping : ResearchTestBase
{
    private static readonly string[] Histories = { "A", "B", "AB", "BA", "ABC", "CBA" };
    private static readonly double[] Betas = { 0.0, 0.2, 0.5, 1.0, 2.0 };
    private static readonly double[] EnergyScales = { 0.5, 0.75, 1.0, 1.5, 2.0 };
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int Seeds = 5;
    private const int BaseSeed = 550371942;

    public TQM_055_ResonanceLandscapeMapping(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_055_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-055 Resonance Landscape Mapping");

        report.AppendLine("TQM-055: Mapping the Global Topology of the TQM State Space");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-047-054 established: identity ≠ energy, coherence is");
        report.AppendLine("  conserved, dynamics are not gradient descent, but recovery");
        report.AppendLine("  follows minimization. This experiment maps the GLOBAL");
        report.AppendLine("  TOPOLOGY of the resonance state space.");
        report.AppendLine();
        report.AppendLine("  Questions: How many attractor basins? Are identities separate");
        report.AppendLine("  basins or regions within one? Where do recovery paths travel?");
        report.AppendLine();

        // ── Section 2: State Space Definition ────────────────────────
        int total = Histories.Length * Betas.Length * EnergyScales.Length * Seeds;

        AppendSection(report, "2. State Space Sampling");
        report.AppendLine($"  Histories:  [{string.Join(", ", Histories)}]");
        report.AppendLine($"  \u03b2:         [{string.Join(", ", Betas)}]");
        report.AppendLine($"  Energy:     [{string.Join(", ", EnergyScales)}]");
        report.AppendLine($"  Seeds: {Seeds} per combination");
        report.AppendLine($"  Total states: {total}");
        report.AppendLine($"  State vector: 6D (R, Freq, PhaseVar, Energy, Mem, LocalCoh)");
        report.AppendLine();

        // ── Generate states ──────────────────────────────────────────
        var bag = new ConcurrentBag<ResonanceLandscapeAnalyzer.LandscapePoint>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int hi = idx % Histories.Length, rem = idx / Histories.Length;
            int bi = rem % Betas.Length; rem /= Betas.Length;
            int ei = rem % EnergyScales.Length; int si = rem / EnergyScales.Length;
            int seed = BaseSeed + idx * 7919;
            bag.Add(ResonanceLandscapeAnalyzer.GenerateState(
                Histories[hi], Betas[bi], EnergyScales[ei], K, Lambda, N, seed));
        });

        sw.Stop();
        var points = bag.ToList();
        report.AppendLine($"  Generated {points.Count} states in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Topology analysis ────────────────────────────────────────
        var topo = ResonanceLandscapeAnalyzer.AnalyzeTopology(points);

        // ── Section 3: Basin Detection ───────────────────────────────
        AppendSection(report, "3. Attractor Basin Detection");

        report.AppendLine($"  Total basins detected:     {topo.BasinCount}");
        report.AppendLine($"  Silhouette score:          {topo.SilhouetteScore:F4}");
        report.AppendLine($"  Mean intra-basin distance: {topo.MeanIntraBasinDistance:F4}");
        report.AppendLine($"  Mean inter-basin distance: {topo.MeanInterBasinDistance:F4}");
        report.AppendLine($"  Separation ratio (inter/intra): {(topo.MeanIntraBasinDistance > 0 ? topo.MeanInterBasinDistance / topo.MeanIntraBasinDistance : 0):F2}\u00d7");
        report.AppendLine();

        report.AppendLine("  Basin │ Points │  Pct  │ Density │ Stability(R) │ Dominant Histories");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var b in topo.Basins.OrderByDescending(b => b.PointCount))
        {
            double pct = 100.0 * b.PointCount / topo.TotalPoints;
            string hists = string.Join(", ", b.DominantHistories);
            report.AppendLine($"  {b.Id,4}  │ {b.PointCount,5}  │ {pct,4:F1}% │ {b.Density,6:F3} │ {b.StabilityMean,11:F4} │ {hists}");
        }

        report.AppendLine();

        // Q1
        report.AppendLine($"  Q1: How many attractor basins exist?");
        report.AppendLine($"    {topo.BasinCount} basin(s) detected at threshold 0.3");
        report.AppendLine();

        // Q2
        var basinHistories = topo.Basins.SelectMany(b => b.DominantHistories).Distinct().ToList();
        report.AppendLine($"  Q2: Are identities separate basins?");
        report.AppendLine($"    {(topo.BasinCount > 1 && basinHistories.Count > 1 ? "YES \u2014 Different histories occupy different basins" : "NO \u2014 All histories share the same basin")}");
        report.AppendLine($"    Distinct basin histories: {basinHistories.Count}");
        report.AppendLine();

        // ── Section 4: Transition Analysis ───────────────────────────
        AppendSection(report, "4. Transition Analysis");

        if (topo.BasinCount > 1)
        {
            report.AppendLine("  Inter-Basin Distance Matrix:");
            report.Append("  Basin │");
            for (int i = 0; i < Math.Min(topo.BasinCount, 8); i++) report.Append($"{i,8}");
            report.AppendLine();
            report.Append("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
            report.AppendLine(new string('\u2500', 9 + Math.Min(topo.BasinCount, 8) * 8));

            for (int i = 0; i < Math.Min(topo.BasinCount, 8); i++)
            {
                report.Append($"  {i,4}  │");
                for (int j = 0; j < Math.Min(topo.BasinCount, 8); j++)
                    report.Append($" {topo.BasinDistances[i, j],7:F3}");
                report.AppendLine();
            }
            report.AppendLine();
        }

        // Q3: Energy changes and basin transitions.
        var basinByEnergy = new Dictionary<int, Dictionary<double, int>>();
        foreach (var p in points)
        {
            if (!basinByEnergy.ContainsKey(p.Label))
                basinByEnergy[p.Label] = new Dictionary<double, int>();
            var d = basinByEnergy[p.Label];
            d[p.EnergyScale] = d.GetValueOrDefault(p.EnergyScale) + 1;
        }

        report.AppendLine($"  Q3: Do energy changes move states across basin boundaries?");
        int basinsWithMultipleEnergy = basinByEnergy.Count(kv => kv.Value.Count > 1);
        report.AppendLine($"    {(basinsWithMultipleEnergy > 0 ? $"YES \u2014 {basinsWithMultipleEnergy} basin(s) contain multiple energy levels" : "NO \u2014 Each energy level produces a distinct basin")}");
        report.AppendLine();

        // ── Section 5: Recovery Corridors ────────────────────────────
        AppendSection(report, "5. Recovery Corridor Analysis");

        // States with energy=1.0 are "baseline", others are "perturbed".
        var baseline = points.Where(p => Math.Abs(p.EnergyScale - 1.0) < 0.01).ToList();
        var perturbed = points.Where(p => Math.Abs(p.EnergyScale - 1.0) > 0.01).ToList();

        int sameBasinRecovery = 0;
        foreach (var pert in perturbed)
        {
            var matching = baseline.FirstOrDefault(b => b.History == pert.History && Math.Abs(b.Beta - pert.Beta) < 0.01 && b.Seed == pert.Seed);
            if (matching.Label == pert.Label) sameBasinRecovery++;
        }

        double recoveryFraction = perturbed.Count > 0 ? (double)sameBasinRecovery / perturbed.Count : 0;

        report.AppendLine($"  Perturbed states: {perturbed.Count}, Baseline states: {baseline.Count}");
        report.AppendLine($"  Same-basin (energy perturbed → baseline): {sameBasinRecovery}/{perturbed.Count} ({recoveryFraction:P1})");
        report.AppendLine();

        report.AppendLine($"  Q4: Where do recovery trajectories travel?");
        report.AppendLine($"    {(recoveryFraction > 0.70 ? "WITHIN BASIN \u2014 Recovery stays in the same attractor basin" : recoveryFraction > 0.30 ? "PARTIAL \u2014 Recovery sometimes crosses basin boundaries" : "ACROSS BASINS \u2014 Recovery often crosses basin boundaries")}");
        report.AppendLine();

        // ── Section 6: Landscape Topology ────────────────────────────
        AppendSection(report, "6. Landscape Topology");

        report.AppendLine($"  Topology classification: {topo.TopologyClassification}");
        report.AppendLine();

        // Q5: Topology barriers and identity stability.
        report.AppendLine($"  Q5: Do topology barriers explain identity stability?");
        if (topo.SilhouetteScore > 0.5)
        {
            report.AppendLine($"    YES \u2014 Well-separated basins (silhouette {topo.SilhouetteScore:F3}) act as");
            report.AppendLine($"    topology barriers that confine identity within attractor regions.");
        }
        else if (topo.BasinCount > 1)
        {
            report.AppendLine($"    WEAKLY \u2014 Multiple basins exist but overlap (silhouette {topo.SilhouetteScore:F3}),");
            report.AppendLine($"    suggesting soft rather than hard topology barriers.");
        }
        else
        {
            report.AppendLine($"    NO \u2014 Single basin (no topology barriers) — identity stability");
            report.AppendLine($"    comes from internal attractor dynamics, not topology.");
        }
        report.AppendLine();

        // Q6: Hidden structure.
        report.AppendLine($"  Q6: Does the landscape contain hidden structure?");
        if (topo.BasinCount > 1)
            report.AppendLine($"    YES \u2014 {topo.BasinCount} basins with {topo.MeanInterBasinDistance / Math.Max(topo.MeanIntraBasinDistance, 1e-10):F1}\u00d7 separation reveal structure");
        else
            report.AppendLine($"    NO \u2014 Single basin suggests a smooth, connected state space.");
        report.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  Topology: {topo.TopologyClassification}");
        report.AppendLine($"  Basins: {topo.BasinCount}");
        report.AppendLine($"  Silhouette: {topo.SilhouetteScore:F4}");
        report.AppendLine();

        report.AppendLine("  Evidence Summary:");
        report.AppendLine($"    Total states:                    {topo.TotalPoints,8}");
        report.AppendLine($"    Attractor basins:                {topo.BasinCount,8}");
        report.AppendLine($"    Silhouette score:                {topo.SilhouetteScore,8:F4}");
        report.AppendLine($"    Intra-basin distance:            {topo.MeanIntraBasinDistance,8:F4}");
        report.AppendLine($"    Inter-basin distance:            {topo.MeanInterBasinDistance,8:F4}");
        report.AppendLine($"    Recovery within basin:           {recoveryFraction,8:P1}");

        // PCA-like: identify most variable dimensions.
        report.AppendLine();
        report.AppendLine("  Dominant state-space dimensions (variance ranking):");
        var allVecs = points.Select(p => new[] { p.R, p.MeanFreq, p.PhaseVar, p.Energy, p.MemScore, p.LocalCoh }).ToList();
        string[] dimNames = { "R", "Freq", "PhaseVar", "Energy", "MemScore", "LocalCoh" };
        var variances = new List<(string Name, double Var)>();
        for (int d = 0; d < 6; d++)
        {
            double mean = allVecs.Average(v => v[d]);
            double var = allVecs.Average(v => (v[d] - mean) * (v[d] - mean));
            variances.Add((dimNames[d], var));
        }
        foreach (var (name, v) in variances.OrderByDescending(v => v.Var))
            report.AppendLine($"    {name,-10} \u03c3\u00b2 = {v,10:F6}");
        report.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Topology: {topo.TopologyClassification}");
        report.AppendLine($"  C2. {topo.BasinCount} attractor basin(s) detected");
        report.AppendLine();

        if (topo.BasinCount == 1)
        {
            report.AppendLine("  C3. The TQM state space is a SINGLE CONNECTED ATTRACTOR BASIN.");
            report.AppendLine("      Identities, energy levels, and memory states are all");
            report.AppendLine("      regions within this single basin rather than separate basins.");
            report.AppendLine("  C4. Identity stability (TQM-047-051) is explained by the");
            report.AppendLine("      smooth, connected topology — the system has one global");
            report.AppendLine("      attractor with internal structure rather than barriers.");
        }
        else
        {
            report.AppendLine($"  C3. The TQM state space contains {topo.BasinCount} attractor basins.");
            report.AppendLine("  C4. Topology barriers (inter/intra = {topo.MeanInterBasinDistance / Math.Max(topo.MeanIntraBasinDistance, 1e-10):F1}\u00d7)");
            report.AppendLine("      constrain state transitions and explain identity stability.");
        }

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-055 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
