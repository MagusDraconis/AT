using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Core.Temporal;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_016_CondensationDynamics : ResearchTestBase
{
    private const int N = 500;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int Iterations = 4000;
    private const int SampleInterval = 50; // dense sampling every 50 iterations
    private const int GridSize = 20;
    private const int BaseSeed = 28657;
    private const int NumRuns = 8;

    public AT_016_CondensationDynamics(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void AT_016_RunCondensationDynamics()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("AT-016 Condensation Dynamics");
        report.AppendLine("AT-016: Birth Process of Resonance Condensates");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-015 identified ρc ≈ 0.035. This experiment investigates HOW");
        report.AppendLine("  condensates form — gradually, via critical transition, or through");
        report.AppendLine("  cascade synchronization — by tracking high-resolution timelines.");
        report.AppendLine();

        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  N={N}, K={K}, λ={Lambda}, Multiple Clusters placement");
        report.AppendLine($"  Runs: {NumRuns}, Iterations: {Iterations}, Sampling: every {SampleInterval} iter");
        report.AppendLine($"  Grid: {GridSize}×{GridSize}, Condensate threshold: R ≥ 0.80");
        report.AppendLine();

        var allTimelines = new List<CondensationTimeline>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        for (int run = 0; run < NumRuns; run++)
        {
            var timelines = RunOne(run);
            allTimelines.AddRange(timelines);
        }

        sw.Stop();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine($"  Total condensate births tracked: {allTimelines.Count}");
        report.AppendLine();

        if (allTimelines.Count == 0)
        {
            report.AppendLine("  No condensates formed — cannot analyze dynamics.");
            Output.WriteLine(report.ToString());
            return;
        }

        // Classify all timelines.
        foreach (var t in allTimelines) t.Classify();

        // ── 3. Condensation Timelines ───────────────────────────
        AppendSection(report, "3. Condensation Timelines");

        report.AppendLine("  Birth mechanism distribution:");
        var byMechanism = allTimelines.GroupBy(t => t.BirthMechanism)
            .ToDictionary(g => g.Key, g => g.Count());
        foreach (var (mech, count) in byMechanism.OrderByDescending(kv => kv.Value))
            report.AppendLine($"    {mech,-25}: {count} ({count * 100.0 / allTimelines.Count:F0}%)");

        report.AppendLine();
        report.AppendLine("  Individual condensate timelines:");
        report.AppendLine("  ID │ Birth Iter │ Mechanism            │ Precursor │ Start R │ Peak R  │ End R   │ Stage");
        report.AppendLine("  ───┼────────────┼──────────────────────┼───────────┼─────────┼─────────┼─────────┼───────");

        foreach (var t in allTimelines.OrderBy(t => t.BirthIteration))
        {
            double startR = t.Snapshots.Count > 0 ? t.Snapshots[0].LocalR : 0;
            double peakR = t.Snapshots.Count > 0 ? t.Snapshots.Max(s => s.LocalR) : 0;
            double endR = t.Snapshots.Count > 0 ? t.Snapshots[^1].LocalR : 0;
            int precursor = t.PrecursorIteration > 0 ? t.PrecursorIteration - t.BirthIteration : 0;

            report.AppendLine(
                $"  {t.CondensateId,2} │ {t.BirthIteration,10} │ {t.BirthMechanism,-20} │ {precursor,7}  │ {startR,7:F4} │ {peakR,7:F4} │ {endR,7:F4} │ {t.FinalStage}");
        }

        report.AppendLine();

        // ── 4. Growth Analysis ──────────────────────────────────
        AppendSection(report, "4. Growth Analysis");

        report.AppendLine("  Average growth trajectory (aligned to birth iteration):");
        report.AppendLine("  Offset │  R_local  │ Density │ Size  │ ΔR/step");
        report.AppendLine("  ───────┼───────────┼─────────┼───────┼─────────");

        int maxSnapshots = allTimelines.Max(t => t.Snapshots.Count);
        for (int i = 0; i < Math.Min(maxSnapshots, 20); i++)
        {
            var aligned = allTimelines
                .Where(t => i < t.Snapshots.Count)
                .Select(t => t.Snapshots[i])
                .ToList();

            if (aligned.Count > 0)
            {
                double avgR = aligned.Average(s => s.LocalR);
                double avgDensity = aligned.Average(s => s.LocalDensity);
                double avgSize = aligned.Average(s => s.ClusterSize);
                double avgGrowth = i > 0
                    ? aligned.Average(s => s.GrowthRate)
                    : 0;

                report.AppendLine(
                    $"  {i * SampleInterval - allTimelines.Average(t => t.BirthIteration),6:F0} │ {avgR,9:F4} │ {avgDensity,7:F4} │ {avgSize,5:F1} │ {avgGrowth,7:F4}");
            }
        }

        report.AppendLine();

        // ── 5. Precursor Detection ──────────────────────────────
        AppendSection(report, "5. Precursor Detection");

        int withPrecursor = allTimelines.Count(t => t.PrecursorIteration > 0);
        report.AppendLine($"  Condensates with detectable precursor: {withPrecursor}/{allTimelines.Count}");
        report.AppendLine();

        if (withPrecursor > 0)
        {
            double avgLeadTime = allTimelines
                .Where(t => t.PrecursorIteration > 0)
                .Average(t => t.BirthIteration - t.PrecursorIteration);
            report.AppendLine($"  Average precursor lead time: {avgLeadTime:F0} iterations before birth");
            report.AppendLine($"  Precursor signal: R_local exceeds 0.3 before formal condensate detection (R≥0.80)");
        }

        report.AppendLine();

        // ── 6. Birth Mechanisms ─────────────────────────────────
        AppendSection(report, "6. Birth Mechanisms");

        string dominantMech = byMechanism.OrderByDescending(kv => kv.Value).First().Key;
        report.AppendLine($"  Dominant mechanism: {dominantMech} ({byMechanism[dominantMech]} cases)");
        report.AppendLine();

        report.AppendLine("  Mechanism descriptions:");
        report.AppendLine("    Critical Transition    : sharp jump in R (ΔR > 0.5 in one sampling interval)");
        report.AppendLine("    Gradual Accumulation   : steady increase in coherence over time");
        report.AppendLine("    Cascade Synchronization: rapid growth via oscillator recruitment");
        report.AppendLine();

        report.AppendLine($"  Q1. Sudden or gradual?");
        int critical = allTimelines.Count(t => t.BirthMechanism == "Critical Transition");
        int gradual = allTimelines.Count(t => t.BirthMechanism == "Gradual Accumulation");
        report.AppendLine($"    Critical: {critical}, Gradual: {gradual}");
        report.AppendLine($"    Dominant mode: {(critical > gradual ? "SUDDEN (critical transition)" : "GRADUAL (coherence accumulation)")}");

        report.AppendLine();
        report.AppendLine($"  Q2. Precursor? {(withPrecursor > 0 ? $"YES — detectable {allTimelines.Where(t => t.PrecursorIteration > 0).Average(t => t.BirthIteration - t.PrecursorIteration):F0} iterations before birth" : "NO")}");

        report.AppendLine();
        report.AppendLine($"  Q3. Predictable? {(withPrecursor > allTimelines.Count * 0.7 ? "Likely — strong precursor signal" : "Uncertain — precursor not always present")}");

        report.AppendLine();
        report.AppendLine("  Q4. Universal pattern?");
        int mechTypes = byMechanism.Count;
        report.AppendLine($"    {(mechTypes == 1 ? "YES — single universal pattern" : $"NO — {mechTypes} distinct birth mechanisms")}");

        report.AppendLine();

        // ── 7. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine("  Condensation is a dynamical process with distinct stages:");
        report.AppendLine("    1. Pre-condensation: low local R, diffuse phases");
        report.AppendLine("    2. Seed formation: first region exceeds R > 0.3");
        report.AppendLine("    3. Growth: cluster expands, coherence increases");
        report.AppendLine($"    4. {(critical > gradual ? "Critical transition: sharp jump to R > 0.8" : "Maturation: gradual approach to R > 0.8")}");
        report.AppendLine("    5. Stable condensate: persistent R > 0.8, τ > 1000");
        report.AppendLine();

        // ── 8. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Condensates form via {dominantMech.ToLower()} ({byMechanism[dominantMech]}/{allTimelines.Count} cases).");
        report.AppendLine();
        report.AppendLine($"  C2. {(withPrecursor > allTimelines.Count * 0.5 ? "A PRECURSOR SIGNAL exists" : "No reliable precursor signal")} —");
        report.AppendLine($"      {(withPrecursor > allTimelines.Count * 0.5 ? "condensation can be anticipated by rising local R." : "condensation occurs without clear advance warning.")}");
        report.AppendLine();
        report.AppendLine("  C3. The condensation birth process is a fundamental dynamical");
        report.AppendLine("      phenomenon in resonator networks, analogous to nucleation");
        report.AppendLine("      in phase transitions and self-organization in complex systems.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-016 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private List<CondensationTimeline> RunOne(int runIdx)
    {
        int seed = BaseSeed + runIdx * 1000;
        var rng = new Random(seed);
        var network = new TemporalNetwork(N);

        for (int i = 0; i < N; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 0.5 + rng.NextDouble() * 1.5;
            var node = new TemporalNode(i, phase, freq);
            PlaceInCluster(node, rng, i);
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, K, Lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = N };
        var densityField = new LocalDensityField(GridSize);

        var snapshots = new List<(int Iter, double Density, double R, int Size, double PhaseVar)>();
        var seenIds = new HashSet<int>();
        var timelines = new List<CondensationTimeline>();
        var activeTimelines = new Dictionary<int, CondensationTimeline>();

        for (int iter = 0; iter < Iterations; iter++)
        {
            sim.Step();

            if (iter % SampleInterval == 0)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                double maxR = densityField.MaxLocalR();
                double meanDensity = densityField.MeanLocalR();
                int cellsAbove = densityField.CellsAboveThreshold(0.80);
                double phaseVar = 1.0 - SynchronizationMetrics.FromNetwork(network, iter + 1).OrderParameterR;

                snapshots.Add((iter, meanDensity, maxR, cellsAbove, phaseVar));

                // Check for new condensates via flood-fill detection.
                var tempAnalyzer = new ResonanceCondensationAnalyzer
                {
                    CondensationThreshold = 0.80, MinCondensateCells = 2, OverlapThreshold = 0.3
                };
                var condensates = tempAnalyzer.DetectAndTrack(densityField, iter + 1);

                foreach (var c in condensates)
                {
                    if (!seenIds.Contains(c.Id) && c.BirthIteration == iter + 1)
                    {
                        seenIds.Add(c.Id);
                        var timeline = new CondensationTimeline(c.Id, c.BirthIteration);
                        activeTimelines[c.Id] = timeline;
                        timelines.Add(timeline);
                    }
                }

                // Add snapshot to active timelines.
                foreach (var (cid, timeline) in activeTimelines)
                {
                    timeline.Snapshots.Add(new CondensationSnapshot(
                        iter, meanDensity, maxR, cellsAbove, phaseVar, 0));
                }
            }
        }

        return timelines;
    }

    private static void PlaceInCluster(TemporalNode node, Random rng, int idx)
    {
        var centers = new[] { (0.1, 0.1), (0.9, 0.1), (0.5, 0.5), (0.1, 0.9), (0.9, 0.9) };
        var (cx, cy) = centers[idx % 5];
        node.X = Math.Clamp(cx + NextGaussian(rng) * 0.02, 0, 1);
        node.Y = Math.Clamp(cy + NextGaussian(rng) * 0.02, 0, 1);
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble(), u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
