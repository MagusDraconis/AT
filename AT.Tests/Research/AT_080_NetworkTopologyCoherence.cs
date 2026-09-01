using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_080_NetworkTopologyCoherence : ResearchTestBase
{
    private const double K = 2.0;
    private const double Lambda = 0.05;
    private const int N = 100;
    private const int BaseSeed = 800731952;
    private const int NumConfigs = 120;

    public AT_080_NetworkTopologyCoherence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_080_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-080 Network Topology and Coherence Evolution");

        sb.AppendLine("AT-080: Does Network Topology Explain the Unexplained dR/dt Variance?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-077-079: ~46% of dR/dt variance explained by R.");
        sb.AppendLine("  ~54% remains unexplained — not captured by hidden state");
        sb.AppendLine("  variables (H1-H8 all gained < 0.01).");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: The missing variance is encoded in network");
        sb.AppendLine("  topology — the spatial arrangement of oscillators");
        sb.AppendLine("  determines coupling structure, which affects evolution.");
        sb.AppendLine();

        // ── Section 2: Setup ─────────────────────────────────────────
        Sec(sb, "2. Experimental Setup");
        sb.AppendLine($"  {NumConfigs} configurations, N={N}, K={K}, λ={Lambda}");
        sb.AppendLine($"  6 topology types: uniform, clustered, linear, circular,");
        sb.AppendLine($"    dense-sparse, random-clusters");
        sb.AppendLine($"  10-step evolution, measure dR/dt");
        sb.AppendLine();
        sb.AppendLine("  7 topology metrics: MeanCoupling, CouplingVar, MeanDegree,");
        sb.AppendLine("    DegreeVar, SpectralGap, CouplingEntropy, SpatialClustering");
        sb.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var states = TopologyEvolutionAnalyzer.GenerateTopologyEnsemble(
            K, Lambda, N, NumConfigs, BaseSeed);
        var report = TopologyEvolutionAnalyzer.AnalyzeTopology(states);
        sw.Stop();

        sb.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // ── Section 3: Network Architectures ─────────────────────────
        Sec(sb, "3. Topology Types and Evolution");

        var byType = states.GroupBy(s => s.TopologyType).ToList();
        sb.AppendLine("  Topology Type       │ Count │ Mean R  │ Mean dR/dt │ MeanCoupling");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var g in byType)
            sb.AppendLine($"  {g.Key,-19} │ {g.Count(),4} │ {g.Average(s => s.R),6:F4} │ {g.Average(s => s.dRdt),9:F5} │ {g.Average(s => s.MeanCoupling),10:F4}");
        sb.AppendLine();

        // ── Section 4: Topology Metrics ──────────────────────────────
        Sec(sb, "4. Topology Metrics vs dR/dt");

        double r2Base = R2Linear(states.Select(s => s.R).ToArray(),
                                  states.Select(s => s.dRdt).ToArray());
        sb.AppendLine($"  Baseline R² (R only): {r2Base:F4}");
        sb.AppendLine();
        sb.AppendLine("  Rank │ Topology Metric    │ R²(R+T)  │ Gain     ");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        int rank = 0;
        foreach (var g in report.Gains)
        {
            rank++;
            string star = rank == 1 ? " \u2605" : "  ";
            string sign = g.Gain >= 0 ? "+" : "";
            sb.AppendLine($"  {rank,3}{star} │ {g.Metric,-18} │ {g.R2_With,6:F4} │ {sign}{g.Gain,7:F4}");
        }
        sb.AppendLine();

        // ── Section 5: Variance Decomposition ────────────────────────
        Sec(sb, "5. Variance Decomposition");

        double explainedByR = r2Base;
        double explainedByTopo = report.BestGain;
        double unexplained = 1.0 - report.TotalR2;
        sb.AppendLine($"  Variance explained by R:        {explainedByR * 100:F0}%");
        sb.AppendLine($"  Additional from topology:       {explainedByTopo * 100:F1}%");
        sb.AppendLine($"  Total explained:                {report.TotalR2 * 100:F0}%");
        sb.AppendLine($"  Unexplained:                    {unexplained * 100:F0}%");
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        sb.AppendLine("  Q1: Can identical-R states evolve differently due to topology?");
        double topoVar = states.GroupBy(s => Math.Round(s.R, 2))
            .Select(g => g.Select(s => s.dRdt).ToList())
            .Where(l => l.Count > 1)
            .Average(l => l.Count > 1 ? l.Max() - l.Min() : 0);
        sb.AppendLine($"    Mean dR/dt range within R-bins: {topoVar:F5}");
        sb.AppendLine($"    {(topoVar > 0.001 ? "YES — Topology creates different futures" : "NO — Topology doesn't cause divergence")}");
        sb.AppendLine();

        sb.AppendLine("  Q2: How much dR/dt variance is explained by network structure?");
        sb.AppendLine($"    {report.BestMetric}: ΔR² = {report.BestGain:+0.0000}");
        sb.AppendLine();

        sb.AppendLine("  Q3: Which topology metric is most predictive?");
        sb.AppendLine($"    {report.BestMetric} (gain = {report.BestGain:+0.0000})");
        sb.AppendLine();

        sb.AppendLine("  Q4: Does topology outperform hidden state variables?");
        sb.AppendLine($"    Topology gain: {report.BestGain:+0.0000}");
        sb.AppendLine($"    Hidden state gain (AT-079): +0.0071");
        sb.AppendLine($"    {(report.BestGain > 0.007 ? "YES — Topology is more important" : "NO — Hidden states are comparable or better")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Can topology close the gap from AT-079?");
        sb.AppendLine($"    Gap closed: {report.BestGain / (1 - r2Base) * 100:F1}% of unexplained variance");
        sb.AppendLine();

        sb.AppendLine("  Q6: Is coherence evolution fundamentally a network phenomenon?");
        sb.AppendLine($"    Classification: {report.Classification}");
        sb.AppendLine();

        // ── Section 6: Interpretation ────────────────────────────────
        Sec(sb, "6. Interpretation");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        // ── Section 7: Conclusion ────────────────────────────────────
        Sec(sb, "7. Conclusion");
        sb.AppendLine($"  C1. Classification: {report.Classification}");
        sb.AppendLine($"  C2. Best topology metric: {report.BestMetric}");
        sb.AppendLine($"  C3. Topology gain: {report.BestGain:+0.0000}");
        sb.AppendLine($"  C4. Total R²: {report.TotalR2:F4}");
        sb.AppendLine($"  C5. Configurations tested: {states.Count}");
        sb.AppendLine();
        sb.AppendLine($"  C6. {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-080 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static double R2Linear(double[] X, double[] Y)
    {
        double sxy = 0, sx2 = 0;
        for (int i = 0; i < X.Length; i++) { sxy += X[i] * Y[i]; sx2 += X[i] * X[i]; }
        double a = sx2 > 1e-15 ? sxy / sx2 : 0;
        double ssRes = 0, ssTot = 0, m = Y.Average();
        for (int i = 0; i < Y.Length; i++)
        { double p = a * X[i]; ssRes += (Y[i] - p) * (Y[i] - p); ssTot += (Y[i] - m) * (Y[i] - m); }
        return ssTot > 1e-15 ? 1 - ssRes / ssTot : 0;
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
