using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_019_UniversalDensityValidation : ResearchTestBase
{
    private static readonly int[] Ns = { 50, 100, 200, 500 };
    private static readonly double[] Ks = { 1, 2, 3, 5 };
    private static readonly double[] Lambdas = { 0.02, 0.05, 0.10, 0.50 };
    private static readonly string[] Placements = { "Uniform", "GaussianBlobs", "MultipleClusters" };
    private const int Iterations = 3000;
    private const int BaseSeed = 121393;

    public TQM_019_UniversalDensityValidation(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void TQM_019_RunUniversalValidation()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-019 Universal Density Threshold Validation");
        report.AppendLine("TQM-019: Is ρc ≈ 0.035 a Universal TQM Constant?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-015 identified ρc ≈ 0.035. This experiment tests whether the");
        report.AppendLine("  critical local density is universal across (N, K, λ, placement).");
        report.AppendLine();

        int total = Ns.Length * Ks.Length * Lambdas.Length * Placements.Length;
        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  N=[{string.Join(",", Ns)}], K=[{string.Join(",", Ks)}]");
        report.AppendLine($"  λ=[{string.Join(",", Lambdas)}], Placements=[{string.Join(",", Placements)}]");
        report.AppendLine($"  Total: {total} combos, {Iterations} iter each");
        report.AppendLine();

        var bag = new ConcurrentBag<DensityThresholdValidationAnalyzer.ThresholdMeasurement>();
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var points = (from n in Ns from k in Ks from lam in Lambdas from p in Placements select (n, k, lam, p)).ToList();

        Parallel.ForEach(points, pt =>
        {
            var (n, k, lam, p) = pt;
            int seed = BaseSeed + n * 1000 + (int)(k * 100) + (int)(lam * 10000) + p.GetHashCode() % 10000;
            var rng = new Random(seed);
            var m = DensityThresholdValidationAnalyzer.Measure(n, k, lam, p, rng, Iterations);
            bag.Add(m);
        });

        sw.Stop();
        var measurements = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms. Condensate-forming combos: {measurements.Count(m => m.CondensateCount > 0)}/{total}");
        report.AppendLine();

        var withCondensates = measurements.Where(m => m.CondensateCount > 0).ToList();
        if (withCondensates.Count == 0) { report.AppendLine("No condensates."); Output.WriteLine(report.ToString()); return; }

        // ── 3. Threshold Measurements ──────────────────────────
        AppendSection(report, "3. Threshold Statistics");
        var rhos = withCondensates.Select(m => m.EstimatedRhoC).OrderBy(r => r).ToList();
        int c = rhos.Count;
        report.AppendLine($"  N={withCondensates.Count} condensate-forming configurations");
        report.AppendLine($"  ρc distribution: min={rhos[0]:F4} p10={rhos[c/10]:F4} median={rhos[c/2]:F4} p90={rhos[c*9/10]:F4} max={rhos[^1]:F4}");
        double globalRhoC = rhos[c / 2];
        report.AppendLine($"  Global median ρc = {globalRhoC:F4}");
        report.AppendLine();

        // ── 4. Scaling Analysis ─────────────────────────────────
        AppendSection(report, "4. ρc vs Parameters");

        report.AppendLine("  Mean ρc by N:");
        foreach (int n in Ns)
        {
            var sub = withCondensates.Where(m => m.N == n).ToList();
            if (sub.Count > 0)
                report.AppendLine($"    N={n,4}: ρc={sub.Average(m => m.EstimatedRhoC):F4} ± {StdDev(sub.Select(m => m.EstimatedRhoC).ToList()):F4} ({sub.Count} pts)");
        }
        report.AppendLine();

        report.AppendLine("  Mean ρc by K:");
        foreach (double k in Ks)
        {
            var sub = withCondensates.Where(m => Math.Abs(m.K - k) < 0.01).ToList();
            if (sub.Count > 0)
                report.AppendLine($"    K={k,3:F0}: ρc={sub.Average(m => m.EstimatedRhoC):F4} ± {StdDev(sub.Select(m => m.EstimatedRhoC).ToList()):F4} ({sub.Count} pts)");
        }
        report.AppendLine();

        report.AppendLine("  Mean ρc by λ:");
        foreach (double lam in Lambdas)
        {
            var sub = withCondensates.Where(m => Math.Abs(m.Lambda - lam) < 0.01).ToList();
            if (sub.Count > 0)
                report.AppendLine($"    λ={lam,4:F2}: ρc={sub.Average(m => m.EstimatedRhoC):F4} ± {StdDev(sub.Select(m => m.EstimatedRhoC).ToList()):F4} ({sub.Count} pts)");
        }
        report.AppendLine();

        report.AppendLine("  Mean ρc by placement:");
        foreach (string p in Placements)
        {
            var sub = withCondensates.Where(m => m.Placement == p).ToList();
            if (sub.Count > 0)
                report.AppendLine($"    {p,-17}: ρc={sub.Average(m => m.EstimatedRhoC):F4} ± {StdDev(sub.Select(m => m.EstimatedRhoC).ToList()):F4} ({sub.Count} pts)");
        }
        report.AppendLine();

        // ── 5. Universality Test ────────────────────────────────
        AppendSection(report, "5. Universality Test");

        double cvN = CV(Group(Ns, withCondensates, m => m.N));
        double cvK = CV(Group(Ks, withCondensates, m => m.K));
        double cvL = CV(Group(Lambdas, withCondensates, m => m.Lambda));
        double cvP = CV(Group(Placements, withCondensates, m => m.Placement));

        report.AppendLine("  Coefficient of variation across parameter groups:");
        report.AppendLine($"    CV(N)  = {cvN:F3}");
        report.AppendLine($"    CV(K)  = {cvK:F3}");
        report.AppendLine($"    CV(λ)  = {cvL:F3}");
        report.AppendLine($"    CV(pl) = {cvP:F3}");
        report.AppendLine();

        double maxCV = new[] { cvN, cvK, cvL, cvP }.Max();
        string classification = maxCV < 0.2 ? "UNIVERSAL CONSTANT" :
                                maxCV < 0.5 ? "WEAKLY UNIVERSAL" : "STRONGLY PARAMETER DEPENDENT";

        report.AppendLine($"  Max CV = {maxCV:F3} → Classification: {classification}");
        report.AppendLine();

        // ── 6. Sensitivity Ranking ─────────────────────────────
        AppendSection(report, "6. Sensitivity Ranking");

        var sensitivities = new (string Param, double CV)[]
        {
            ("N (oscillator count)", cvN),
            ("K (coupling strength)", cvK),
            ("λ (localization)", cvL),
            ("Placement", cvP),
        }.OrderByDescending(s => s.CV).ToList();

        report.AppendLine("  Parameter influence on ρc (most → least):");
        foreach (var (param, cv) in sensitivities)
            report.AppendLine($"    {param,-25}: CV = {cv:F3}");

        report.AppendLine();

        // ── 7. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  H₀ (ρc varies significantly): {(classification == "STRONGLY PARAMETER DEPENDENT" ? "ACCEPTED" : "REJECTED")}");
        report.AppendLine($"  H₁ (ρc is approximately universal): {(classification != "STRONGLY PARAMETER DEPENDENT" ? "ACCEPTED" : "REJECTED")}");
        report.AppendLine();

        report.AppendLine($"  Q1. Converge to common value? {(classification != "STRONGLY PARAMETER DEPENDENT" ? "YES" : "NO")}");
        report.AppendLine($"  Q2. Most influential? {sensitivities.First().Param} (CV={sensitivities.First().CV:F3})");
        report.AppendLine($"  Q3. Independent of N? {(cvN < 0.3 ? "APPROXIMATELY" : "NO")} (CV={cvN:F3})");
        report.AppendLine($"  Q4. Independent of topology? {(cvP < 0.3 ? "APPROXIMATELY" : "NO")} (CV={cvP:F3})");
        report.AppendLine();

        // ── 8. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Classification: {classification}");
        report.AppendLine($"  C2. Global median ρc = {globalRhoC:F4} across {withCondensates.Count} configurations.");
        report.AppendLine($"  C3. ρc is most sensitive to {sensitivities.First().Param.ToLower()} and");
        report.AppendLine($"      least sensitive to {sensitivities.Last().Param.ToLower()}.");
        report.AppendLine("  C4. The critical local density for resonance condensation is");
        report.AppendLine($"      {(classification == "UNIVERSAL CONSTANT" ? "a genuine universal property of the TQM framework." : classification == "WEAKLY UNIVERSAL" ? "approximately universal — weakly dependent on parameters." : "strongly parameter-dependent — not a universal constant.")}");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-019 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static List<double> Group<T>(T[] keys, List<DensityThresholdValidationAnalyzer.ThresholdMeasurement> data,
        Func<DensityThresholdValidationAnalyzer.ThresholdMeasurement, T> selector)
    {
        return keys.Select(k => data.Where(m => EqualityComparer<T>.Default.Equals(selector(m), k))
            .Select(m => m.EstimatedRhoC).DefaultIfEmpty(0).Average()).Where(d => d > 0).ToList();
    }

    private static double CV(List<double> values) =>
        values.Count > 1 && values.Average() > 1e-10 ? StdDev(values) / values.Average() : 0;

    private static double StdDev(List<double> values)
    {
        double mean = values.Average();
        return Math.Sqrt(values.Average(v => (v - mean) * (v - mean)));
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
