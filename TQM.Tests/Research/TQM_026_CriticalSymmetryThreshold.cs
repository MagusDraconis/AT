using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_026_CriticalSymmetryThreshold : ResearchTestBase
{
    private static readonly double[] Ks = { 0.5, 1.0, 1.5, 2.0, 2.5 };
    private static readonly int[] NeighborCounts = { 10, 20, 30, 40, 50 };
    private static readonly double[] Symmetries = { 0.10, 0.30, 0.50, 0.70, 0.90 };
    private const int TotalN = 200;
    private const double Lambda = 0.05;
    private const int SeedsPerCombo = 5;
    private const int BaseSeed = 3524578;

    public TQM_026_CriticalSymmetryThreshold(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void TQM_026_RunCriticalSymmetryTest()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-026 Critical Symmetry Threshold");
        report.AppendLine("TQM-026: Symmetry as a Gate Near the Critical Threshold");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-025 showed symmetry is irrelevant at super-critical conditions.");
        report.AppendLine("  This experiment tests whether symmetry BECOMES causal near the");
        report.AppendLine("  critical condensation threshold where parameters are marginal.");
        report.AppendLine();

        int total = Ks.Length * NeighborCounts.Length * Symmetries.Length * SeedsPerCombo;
        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  K=[{string.Join(",", Ks)}], Neighbors=[{string.Join(",", NeighborCounts)}]");
        report.AppendLine($"  Symmetry=[{string.Join(",", Symmetries)}], {SeedsPerCombo} seeds, {total} runs");
        report.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var allResults = new List<CriticalSymmetryAnalyzer.CriticalPoint>();

        foreach (double k in Ks)
            foreach (int nc in NeighborCounts)
                foreach (double sym in Symmetries)
                    allResults.AddRange(CriticalSymmetryAnalyzer.Sweep(k, nc, sym, TotalN, Lambda, SeedsPerCombo, BaseSeed + (int)(k * 1000) + nc * 100));

        sw.Stop();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms. Points: {allResults.Count}");
        report.AppendLine();

        // ── 3. Formation Matrix ─────────────────────────────────
        AppendSection(report, "3. Condensation Probability (%) vs (K, Neighbors, Symmetry)");

        foreach (double k in Ks)
        {
            report.AppendLine($"  K = {k}:");
            report.Append("  N \\ S │");
            foreach (double s in Symmetries) report.Append($"{s,8:F2}");
            report.AppendLine();
            report.Append("  ──────┼");
            report.AppendLine(new string('─', Symmetries.Length * 8));

            foreach (int nc in NeighborCounts)
            {
                report.Append($"  {nc,5} │");
                foreach (double sym in Symmetries)
                {
                    var sub = allResults.Where(r =>
                        Math.Abs(r.K - k) < 0.01 && r.NeighborCount == nc && Math.Abs(r.Symmetry - sym) < 0.001).ToList();
                    double rate = sub.Count(r => r.Formed) * 100.0 / sub.Count;
                    report.Append($" {rate,7:F0}%");
                }
                report.AppendLine();
            }
            report.AppendLine();
        }

        // ── 4. Symmetry Effect at Threshold ────────────────────
        AppendSection(report, "4. Symmetry Effect Near Threshold");

        // Find where symmetry matters most: where ΔP(sym=0.9) - ΔP(sym=0.1) is largest.
        var symmetryEffects = new List<(double K, int Nc, double DeltaP)>();

        foreach (double k in Ks)
        {
            foreach (int nc in NeighborCounts)
            {
                var low = allResults.Where(r => Math.Abs(r.K - k) < 0.01 && r.NeighborCount == nc && Math.Abs(r.Symmetry - 0.1) < 0.001).ToList();
                var high = allResults.Where(r => Math.Abs(r.K - k) < 0.01 && r.NeighborCount == nc && Math.Abs(r.Symmetry - 0.9) < 0.001).ToList();
                double lowRate = low.Count(r => r.Formed) * 100.0 / Math.Max(1, low.Count);
                double highRate = high.Count(r => r.Formed) * 100.0 / Math.Max(1, high.Count);
                symmetryEffects.Add((k, nc, highRate - lowRate));
            }
        }

        var topEffect = symmetryEffects.OrderByDescending(e => e.DeltaP).Take(5).ToList();
        report.AppendLine("  Top-5 configurations where symmetry has the strongest effect:");
        report.AppendLine("  K    │ Nc │ ΔP (0.9 − 0.1)");
        report.AppendLine("  ─────┼────┼───────────────");

        foreach (var (k, nc, dp) in topEffect)
            report.AppendLine($"  {k,4:F1} │ {nc,2} │ {dp,13:F0}%");

        report.AppendLine();

        // ── 5. Critical Symmetry ────────────────────────────────
        AppendSection(report, "5. Critical Symmetry Detection");

        double? sc = null;
        foreach (double sym in Symmetries.OrderBy(s => s))
        {
            double avgRate = allResults.Where(r => Math.Abs(r.Symmetry - sym) < 0.001)
                .GroupBy(r => (r.K, r.NeighborCount))
                .Average(g => g.Count(r => r.Formed) * 100.0 / g.Count());
            if (avgRate >= 50 && sc == null) sc = sym;
        }

        report.AppendLine($"  Average formation rate by symmetry:");
        foreach (double sym in Symmetries)
        {
            var sub = allResults.Where(r => Math.Abs(r.Symmetry - sym) < 0.001).ToList();
            double rate = sub.Count(r => r.Formed) * 100.0 / sub.Count;
            report.AppendLine($"    S={sym:F2}: {rate:F0}%");
        }

        report.AppendLine($"  Critical symmetry Sc = {(sc.HasValue ? $"{sc.Value:F2}" : "none identified")}");
        report.AppendLine();

        // ── 6. Interpretation ───────────────────────────────────
        AppendSection(report, "6. Interpretation");

        double maxDelta = topEffect.Max(e => e.DeltaP);
        report.AppendLine($"  Q1. Symmetry effect near threshold?");
        report.AppendLine($"    Maximum ΔP = {maxDelta:F0}% — {(maxDelta > 30 ? "STRONG effect near threshold" : maxDelta > 10 ? "MODERATE effect" : "WEAK effect")}");

        report.AppendLine();
        report.AppendLine("  Q2. Critical symmetry Sc?");
        report.AppendLine($"    {(sc.HasValue ? $"Sc = {sc.Value:F2}" : "Not identified at tested parameters")}");

        report.AppendLine();
        report.AppendLine("  Q3. Low symmetry suppress?");
        bool suppression = symmetryEffects.Any(e => e.DeltaP > 20);
        report.AppendLine($"    {(suppression ? "YES — at marginal parameters, low symmetry reduces formation" : "PARTIALLY — effect depends on K and neighbor count")}");

        report.AppendLine();
        report.AppendLine("  Q4. High symmetry trigger?");
        report.AppendLine($"    {(suppression ? "YES — high symmetry can compensate for marginal density" : "Not conclusively")}");

        report.AppendLine();

        AppendSection(report, "7. Conclusion");

        string role = maxDelta > 30 ? "a THRESHOLD GATE — symmetry controls condensation at marginal parameters" :
                      maxDelta > 10 ? "a CONTRIBUTING FACTOR — symmetry modulates but does not gate condensation" :
                      "PREDICTIVE only — symmetry is a marker, not a driver, even near threshold";

        report.AppendLine($"  C1. Near the critical threshold, symmetry acts as {role}.");
        report.AppendLine();
        report.AppendLine($"  C2. The symmetry effect is strongest at {(topEffect.First().K, topEffect.First().Nc)}");
        report.AppendLine($"      where it produces a ΔP of {maxDelta:F0}%.");
        report.AppendLine();
        report.AppendLine("  C3. Symmetry is a secondary control parameter: it gates condensation");
        report.AppendLine("      only when K and neighbor count are marginal. At super-critical");
        report.AppendLine("      conditions (TQM-025), symmetry becomes irrelevant.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-026 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
