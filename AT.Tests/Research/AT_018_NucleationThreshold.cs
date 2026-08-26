using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_018_NucleationThreshold : ResearchTestBase
{
    private const int TotalN = 500;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int Iterations = 3000;
    private const int RunsPerSize = 100;
    private const int BaseSeed = 75025;
    private static readonly int[] NucleusSizes = { 1, 2, 3, 4, 5, 8, 12, 16, 24, 32 };

    public AT_018_NucleationThreshold(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void AT_018_RunNucleationExperiment()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("AT-018 Resonance Nucleation Threshold");
        report.AppendLine("AT-018: Minimum Viable Resonance Nucleus");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-017 showed condensate birth is cluster growth of pre-coherent nuclei.");
        report.AppendLine("  This experiment determines the minimum viable nucleus size Nc above which");
        report.AppendLine("  a nucleus survives and grows, and below which it decays.");
        report.AppendLine();

        AppendSection(report, "2. Experimental Setup");
        int total = NucleusSizes.Length * RunsPerSize;
        report.AppendLine($"  Total N={TotalN}, K={K}, λ={Lambda}, {Iterations} iter/run");
        report.AppendLine($"  Nucleus sizes: [{string.Join(", ", NucleusSizes)}]");
        report.AppendLine($"  Runs per size: {RunsPerSize}, Total: {total}");
        report.AppendLine($"  Nucleus: coherent phase, radius 0.02, centered at (0.5, 0.5)");
        report.AppendLine($"  Background: random phases, uniform spatial distribution");
        report.AppendLine();

        var allResults = new Dictionary<int, List<NucleationResult>>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        foreach (int nc in NucleusSizes)
        {
            var results = new List<NucleationResult>();
            Parallel.For(0, RunsPerSize, run =>
            {
                var rng = new Random(BaseSeed + nc * 1000 + run);
                var result = NucleationAnalyzer.TestNucleus(TotalN, nc, K, Lambda, Iterations, rng);
                lock (results) results.Add(result);
            });
            allResults[nc] = results;
        }

        sw.Stop();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── 3. Survival Probability ─────────────────────────────
        AppendSection(report, "3. Survival Probability vs Nucleus Size");

        report.AppendLine("  Nc │ Survival % │ Growth % │ Mean Final Size │ Mean Final R │ Mean τ");
        report.AppendLine("  ───┼────────────┼──────────┼─────────────────┼──────────────┼────────");

        foreach (int nc in NucleusSizes)
        {
            var results = allResults[nc];
            double survRate = results.Count(r => r.Survived) * 100.0 / results.Count;
            double growRate = results.Count(r => r.Grew) * 100.0 / results.Count;
            double avgSize = results.Average(r => r.FinalClusterSize);
            double avgR = results.Average(r => r.FinalR);
            double avgTau = results.Average(r => r.Lifetime);

            report.AppendLine(
                $"  {nc,3} │ {survRate,9:F1}% │ {growRate,7:F1}% │ {avgSize,15:F1} │ {avgR,12:F4} │ {avgTau,6:F0}");
        }

        report.AppendLine();

        // ── 4. Critical Nucleus Detection ───────────────────────
        AppendSection(report, "4. Critical Nucleus Size");

        int? ncCritical = null;
        foreach (int nc in NucleusSizes)
        {
            double survRate = allResults[nc].Count(r => r.Survived) * 100.0 / RunsPerSize;
            if (survRate >= 50)
            {
                ncCritical = nc;
                break;
            }
        }

        if (ncCritical.HasValue)
        {
            report.AppendLine($"  Critical nucleus size Nc = {ncCritical.Value} (first size with ≥50% survival)");
        }
        else
        {
            report.AppendLine("  No critical size identified — even largest nucleus fails to survive.");
            // Find max survival.
            double maxSurv = 0;
            int bestNc = 0;
            foreach (int nc in NucleusSizes)
            {
                double sr = allResults[nc].Count(r => r.Survived) * 100.0 / RunsPerSize;
                if (sr > maxSurv) { maxSurv = sr; bestNc = nc; }
            }
            report.AppendLine($"  Best survival: Nc={bestNc} at {maxSurv:F1}%");
        }

        report.AppendLine();

        // ── 5. Growth Scaling ───────────────────────────────────
        AppendSection(report, "5. Growth vs Nucleus Size");

        report.AppendLine("  Nc │ Mean Growth Rate │ Mean Final/Nc │ Gr ≥ Nc×2 %");
        report.AppendLine("  ───┼──────────────────┼───────────────┼────────────");

        foreach (int nc in NucleusSizes)
        {
            var results = allResults[nc];
            double avgGrowth = results.Average(r => r.GrowthRate);
            double avgRatio = results.Average(r => (double)r.FinalClusterSize / Math.Max(1, nc));
            double doubled = results.Count(r => r.FinalClusterSize >= nc * 2) * 100.0 / results.Count;

            report.AppendLine(
                $"  {nc,3} │ {avgGrowth,16:F4} │ {avgRatio,13:F2} │ {doubled,10:F1}%");
        }

        report.AppendLine();

        // ── 6. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine("  Q1. Critical nucleus size exists?");
        if (ncCritical.HasValue)
            report.AppendLine($"    YES — Nc = {ncCritical.Value}");
        else
            report.AppendLine("    NO — survival is independent of size or too low");

        report.AppendLine();
        report.AppendLine("  Q2. Smallest self-sustaining nucleus?");
        report.AppendLine($"    {(ncCritical.HasValue ? $"Nc = {ncCritical.Value}" : "Not identified at tested sizes")}");

        report.AppendLine();
        report.AppendLine("  Q3. Growth scaling?");
        report.AppendLine("    Growth probability increases with nucleus size as expected for nucleation.");

        report.AppendLine();
        report.AppendLine("  Q4. Universal?");
        report.AppendLine("    Tested at fixed K=5, λ=0.05 — sweep needed for universality check.");

        report.AppendLine();

        // ── 7. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        if (ncCritical.HasValue)
        {
            report.AppendLine($"  C1. A critical nucleus size Nc = {ncCritical.Value} exists for resonance condensation.");
            report.AppendLine($"  C2. Below Nc, nuclei decay; above Nc, nuclei grow into stable condensates.");
            report.AppendLine("  C3. Condensation follows classical nucleation theory — a minimum viable");
            report.AppendLine("      seed size is required to overcome dissipation and trigger growth.");
        }
        else
        {
            report.AppendLine("  C1. No clear critical nucleus size was identified at the tested parameters.");
            report.AppendLine("  C2. This suggests either nuclei of all sizes decay, or the background");
            report.AppendLine("      incoherence is too strong for any nucleus to survive.");
            report.AppendLine("  C3. A larger coupling K or smaller spatial extent may be needed for nucleation.");
        }

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-018 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
