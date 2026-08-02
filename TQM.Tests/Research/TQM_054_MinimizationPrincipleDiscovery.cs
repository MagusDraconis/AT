using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_054_MinimizationPrincipleDiscovery : ResearchTestBase
{
    private static readonly string[] Histories = { "random", "A", "B", "AB", "BA", "ABC", "CBA" };
    private const double Beta = 0.5;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int Seeds = 3;
    private const int BaseSeed = 540928731;

    public TQM_054_MinimizationPrincipleDiscovery(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_054_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-054 Minimization Principle Discovery");

        report.AppendLine("TQM-054: Is There a Scalar Potential That TQM Dynamics Minimize?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-052 found Local Coherence is conserved.");
        report.AppendLine("  TQM-053 showed coherence is an attractor, not a cause.");
        report.AppendLine("  This experiment searches for a MINIMIZATION PRINCIPLE:");
        report.AppendLine("  a scalar quantity that TQM dynamics consistently drive downward.");
        report.AppendLine();
        report.AppendLine("  If found, this would be analogous to an action principle —");
        report.AppendLine("  a cost function that the system's evolution naturally minimizes.");
        report.AppendLine();

        // ── Section 2: Candidate Potentials ──────────────────────────
        int totalRuns = Histories.Length * Seeds;

        AppendSection(report, "2. Candidate Potentials & Setup");
        report.AppendLine($"  Histories: [{string.Join(", ", Histories)}] ('random' = no training)");
        report.AppendLine($"  Seeds: {Seeds}, Total condensates: {totalRuns}");
        report.AppendLine($"  \u03b2 = {Beta}, K = {K}, N = {N}");
        report.AppendLine($"  Each run: formation(2000) → energy injection(1500) → recovery(1500)");
        report.AppendLine($"  Snapshots every 100 iterations");
        report.AppendLine();
        report.AppendLine("  10 Candidate potentials:");
        report.AppendLine("    P1: Synchronization Deficit (1-R)");
        report.AppendLine("    P2: Phase Variance");
        report.AppendLine("    P3: Neighbor Tension (mean |sin(\u0394\u03b8)|)");
        report.AppendLine("    P4: Frequency StdDev");
        report.AppendLine("    P5: Local Coherence Deficit");
        report.AppendLine("    P6: Phase Energy (\u03a3(1-cos(\u0394\u03b8))/2)");
        report.AppendLine("    P7: Mean |\u0394\u03b8| (raw phase difference)");
        report.AppendLine("    P8: Coupling-Weighted Tension");
        report.AppendLine("    P9: Identity Drift Rate");
        report.AppendLine("    P10: Composite (Sync+Var+Tension average)");
        report.AppendLine();

        // ── Run experiments ──────────────────────────────────────────
        var allTraces = new ConcurrentBag<MinimizationAnalyzer.EvolutionTrace>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, totalRuns, idx =>
        {
            int hi = idx / Seeds, si = idx % Seeds;
            string history = Histories[hi];
            int combinedSeed = BaseSeed + idx * 7919;
            var traces = MinimizationAnalyzer.RunEvolution(
                history, Beta, K, Lambda, N, combinedSeed);
            foreach (var t in traces) allTraces.Add(t);
        });

        sw.Stop();
        var traceList = allTraces.ToList();
        int totalSnapshots = traceList.Sum(t => t.Snapshots.Count);
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine($"  Traces: {traceList.Count}, Total snapshots: {totalSnapshots}");
        report.AppendLine();

        // ── Analyze monotonicity ─────────────────────────────────────
        var ranked = MinimizationAnalyzer.AnalyzeCandidates(traceList);

        // ── Section 3: Evolution Analysis ────────────────────────────
        AppendSection(report, "3. Monotonicity Ranking");

        report.AppendLine("  Rank │ Candidate                        │ Mono ↓ │ Rate   │ Init→Final │ ΔValue  │ Verdict");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        for (int i = 0; i < ranked.Count; i++)
        {
            var r = ranked[i];
            string verdict = r.MeanMonotonicity >= 0.80 ? "\u2605 MINIMIZED" :
                             r.MeanMonotonicity >= 0.65 ? "\u25C6 Strong trend" :
                             r.MeanMonotonicity >= 0.55 ? "\u25CB Weak trend" :
                             "\u2014 No trend";
            string name = r.Name.Length > 33 ? r.Name[..33] : r.Name;
            report.AppendLine($"  {i + 1,3}  │ {name,-33} │ {r.MeanMonotonicity,6:P1} │ {r.MeanRateOfChange,6:F4} │ {r.InitialValue,7:F4}\u2192{r.FinalValue,7:F4} │ {r.TotalDecrease,7:F4} │ {verdict}");
        }
        report.AppendLine();

        // ── Section 4: Phase-Specific Analysis ───────────────────────
        AppendSection(report, "4. Phase-Specific Monotonicity (Top 5)");

        var top5 = ranked.Take(5).ToList();
        var phases = new[] { "formation+training", "energy_inject", "recovery" };

        report.Append("  Candidate                        │");
        foreach (var ph in phases) report.Append($"{ph,18}");
        report.AppendLine(" │ Cross-Seed \u03c3");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var r in top5)
        {
            string name = r.Name.Length > 33 ? r.Name[..33] : r.Name;
            report.Append($"  {name,-33} │");

            // Compute per-phase monotonicity.
            foreach (var ph in phases)
            {
                var monoList = new List<double>();
                foreach (var trace in traceList.Where(t => t.Phase == ph))
                {
                    var snaps = trace.Snapshots;
                    if (snaps.Count < 2) continue;
                    int dec = 0, steps = 0;
                    for (int s = 1; s < snaps.Count; s++)
                    {
                        int ci = Array.FindIndex(MinimizationAnalyzer.Candidates,
                            c => c.Name == r.Name);
                        if (ci >= 0 && snaps[s].Potentials[ci] < snaps[s - 1].Potentials[ci])
                            dec++;
                        steps++;
                    }
                    if (steps > 0) monoList.Add((double)dec / steps);
                }
                double phaseMono = monoList.Count > 0 ? monoList.Average() : 0;
                string label = phaseMono >= 0.80 ? $"{phaseMono,17:P1}\u2605" :
                               $"{phaseMono,17:P1}  ";
                report.Append(label);
            }
            report.AppendLine($" │ {r.CrossSeedRobustness,10:P1}");
        }
        report.AppendLine();

        // ── Section 5: Recovery Analysis ─────────────────────────────
        AppendSection(report, "5. Recovery Analysis (Q3: Does minimization predict recovery?)");

        report.AppendLine("  Candidate                        │ Rec. Mono │ Recovery Consistent?");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var r in ranked)
        {
            string name = r.Name.Length > 33 ? r.Name[..33] : r.Name;
            string consistent = r.RecoveryConsistency >= 0.70 ? "\u2713 YES" :
                                r.RecoveryConsistency >= 0.55 ? "partial" : "no";
            report.AppendLine($"  {name,-33} │ {r.RecoveryConsistency,8:P1} │ {consistent}");
        }
        report.AppendLine();

        // ── Section 6: Research Questions ────────────────────────────
        AppendSection(report, "6. Research Questions");

        var best = ranked.First();

        report.AppendLine($"  Q1: Does any candidate decrease monotonically?");
        report.AppendLine($"    {(best.MeanMonotonicity >= 0.65 ? $"YES \u2014 {best.Name} at {best.MeanMonotonicity:P1} monotonicity" : "NO \u2014 No candidate shows strong monotonic decrease")}");
        report.AppendLine();

        report.AppendLine($"  Q2: What quantity is minimized most consistently?");
        report.AppendLine($"    {best.Name} ({best.MeanMonotonicity:P1} monotonic, \u0394 = {best.TotalDecrease:F4})");
        if (ranked.Count > 1)
            report.AppendLine($"    Runner-up: {ranked[1].Name} ({ranked[1].MeanMonotonicity:P1})");
        report.AppendLine();

        report.AppendLine($"  Q3: Does minimization predict recovery behavior?");
        report.AppendLine($"    {(best.RecoveryConsistency >= 0.65 ? $"YES \u2014 Recovery monotonicity: {best.RecoveryConsistency:P1}" : "NO \u2014 Minimization does not predict recovery")}");
        report.AppendLine();

        report.AppendLine($"  Q4: Does minimization explain identity restoration?");
        string q4;
        if (best.TotalDecrease > 0.1 && best.RecoveryConsistency > 0.6)
            q4 = "YES \u2014 The minimized quantity decreases during recovery, correlating with identity restoration.";
        else if (best.RecoveryConsistency > 0.55)
            q4 = "PARTIALLY \u2014 Minimization correlates with but does not fully explain identity restoration.";
        else
            q4 = "NO \u2014 Identity restoration is not explained by scalar minimization alone.";
        report.AppendLine($"    {q4}");
        report.AppendLine();

        report.AppendLine($"  Q5: Can all TQM dynamics be explained by a single scalar potential?");
        string q5 = best.MeanMonotonicity >= 0.80 ? "YES \u2014 A single potential function captures the dominant dynamics." :
                     best.MeanMonotonicity >= 0.65 ? "PARTIALLY \u2014 A potential explains much but not all behavior." :
                     "NO \u2014 No single scalar potential captures all dynamics.";
        report.AppendLine($"    {q5}");
        report.AppendLine();

        report.AppendLine($"  Q6: Is there a TQM analogue of an action principle?");
        string q6 = best.MeanMonotonicity >= 0.80
            ? "YES \u2014 The system evolves as gradient descent on a scalar potential."
            : best.MeanMonotonicity >= 0.65
            ? "APPROXIMATE \u2014 Gradient-descent-like behavior exists but is not exact."
            : "NO \u2014 No action principle analogue found.";
        report.AppendLine($"    {q6}");
        report.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        AppendSection(report, "7. Interpretation");

        string classification = best.MeanMonotonicity >= 0.80 ? "D: Unified minimization law" :
                                best.MeanMonotonicity >= 0.65 ? "C: Strong potential function" :
                                best.MeanMonotonicity >= 0.55 ? "B: Weak minimization trend" :
                                "A: No minimization principle found";

        report.AppendLine($"  Classification: {classification}");
        report.AppendLine($"  Best candidate: {best.Name} ({best.MeanMonotonicity:P1})");
        report.AppendLine();

        report.AppendLine("  Evidence Summary:");
        report.AppendLine($"    Best monotonicity:              {best.MeanMonotonicity,8:P1}");
        report.AppendLine($"    Best rate of change:            {best.MeanRateOfChange,8:F4}");
        report.AppendLine($"    Best total decrease:            {best.TotalDecrease,8:F4}");
        report.AppendLine($"    Best recovery consistency:      {best.RecoveryConsistency,8:P1}");
        report.AppendLine($"    Best cross-seed \u03c3:              {best.CrossSeedRobustness,8:P1}");

        // Count "minimized" candidates.
        int minimized = ranked.Count(r => r.MeanMonotonicity >= 0.80);
        int strongTrend = ranked.Count(r => r.MeanMonotonicity >= 0.65 && r.MeanMonotonicity < 0.80);
        report.AppendLine($"    Minimized candidates (\u226580%):    {minimized}");
        report.AppendLine($"    Strong trend (65-80%):          {strongTrend}");
        report.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Classification: {classification}");
        report.AppendLine($"  C2. Best potential: {best.Name}");
        report.AppendLine();

        if (best.MeanMonotonicity >= 0.80)
        {
            report.AppendLine("  C3. A UNIFIED MINIMIZATION LAW exists. TQM dynamics can be");
            report.AppendLine("      described as gradient descent on a scalar potential.");
            report.AppendLine("  C4. This is the TQM analogue of an action principle \u2014");
            report.AppendLine("      a cost function the system consistently minimizes.");
        }
        else if (best.MeanMonotonicity >= 0.65)
        {
            report.AppendLine("  C3. A STRONG POTENTIAL FUNCTION exists. TQM dynamics show");
            report.AppendLine("      clear gradient-descent-like behavior toward lower values");
            report.AppendLine("      of this quantity, but the minimization is not exact.");
            report.AppendLine("  C4. This is an APPROXIMATE action principle \u2014 the system");
            report.AppendLine("      trends toward but does not perfectly achieve the minimum.");
        }
        else
        {
            report.AppendLine("  C3. No strong minimization principle was found. The dynamics");
            report.AppendLine("      are not well-described by gradient descent on a single");
            report.AppendLine("      scalar potential. Multiple interacting forces shape");
            report.AppendLine("      the evolution simultaneously.");
        }

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-054 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
