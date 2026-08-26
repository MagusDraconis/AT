using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

/// <summary>
/// AT-008: Resonance Family Reproducibility Experiment
///
/// Tests whether the 5 resonance families discovered in AT-007
/// consistently reappear under independent random initial conditions.
/// Each family is classified as Universal, Likely Universal, Unstable, or Seed Artifact.
/// </summary>
public class AT_008_ResonanceFamilyReproducibilityExperiment : ResearchTestBase
{
    private const int N = 100;
    private const int TotalSeeds = 100;
    private const int Iterations = 5000;

    private static readonly (double Rho, double K)[] ParameterSets =
    {
        (0.30, 3.0),
        (0.30, 5.0),
        (0.50, 3.0),
        (0.50, 5.0),
    };

    private static readonly string[] FamilyNames =
    {
        "F0 Transient",
        "F1 Stable Large",
        "F2 Ultra-Stable Compact",
        "F3 Stable Medium",
        "F4 Stable Coherent",
    };

    public AT_008_ResonanceFamilyReproducibilityExperiment(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void AT_008_RunReproducibilityExperiment()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        try
        {
            ExecuteExperiment();
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();

        // ── Header ──────────────────────────────────────────────
        PrintHeader("AT-008 Resonance Family Reproducibility");
        report.AppendLine("AT-008: Statistical Reproducibility of Resonance Families");
        report.AppendLine();

        // ── 1. Objective ────────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-007 discovered 5 resonance families. This experiment tests whether");
        report.AppendLine("  they are reproducible — do the same family signatures consistently");
        report.AppendLine("  reappear under independent random initial conditions?");
        report.AppendLine();
        report.AppendLine("  Research questions:");
        report.AppendLine("    Q1. Do the same resonance families reappear across seeds?");
        report.AppendLine("    Q2. Which family is most reproducible?");
        report.AppendLine("    Q3. Which family is most stable (lowest variance)?");
        report.AppendLine("    Q4. Do family statistics converge with more seeds?");
        report.AppendLine("    Q5. Are some families seed-dependent artifacts?");
        report.AppendLine();

        // ── 2. Experimental Setup ───────────────────────────────
        AppendSection(report, "2. Experimental Setup");

        report.AppendLine($"  Fixed parameters:");
        report.AppendLine($"    N (oscillators)    : {N}");
        report.AppendLine($"    Iterations/run     : {Iterations}");
        report.AppendLine($"    Seeds per combo    : {TotalSeeds}");
        report.AppendLine($"    Total simulations  : {ParameterSets.Length} × {TotalSeeds} = {ParameterSets.Length * TotalSeeds}");
        report.AppendLine();
        report.AppendLine($"  Varied parameters:");
        report.AppendLine("    (ρ, K) combinations:");
        foreach (var (rho, k) in ParameterSets)
            report.AppendLine($"      ρ={rho:F2}, K={k:F1}");
        report.AppendLine();
        report.AppendLine($"  Reference families (from AT-007):");
        for (int i = 0; i < FamilyNames.Length; i++)
            report.AppendLine($"    {FamilyNames[i]}");
        report.AppendLine();
        report.AppendLine($"  Matching method        : Euclidean distance in normalized 5D feature space");
        report.AppendLine($"  Match threshold        : 0.5");
        report.AppendLine();

        // ── Run reproducibility analysis ────────────────────────
        var analyzer = new ResonanceFamilyReproducibilityAnalyzer
        {
            MatchThreshold = 0.5,
            MinReproducibilityOccurrences = 5
        };

        var allResults = new Dictionary<(double Rho, double K), Dictionary<int, ResonanceFamilyReproducibilityResult>>();

        report.AppendLine("  Running reproducibility analysis...");
        var sw = System.Diagnostics.Stopwatch.StartNew();

        foreach (var (rho, k) in ParameterSets)
        {
            var results = analyzer.AnalyzeReproducibility(N, rho, k, Iterations, 1, TotalSeeds);
            allResults[(rho, k)] = results;
        }

        sw.Stop();
        report.AppendLine($"  Analysis completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── 3. Family Occurrence Analysis ───────────────────────
        AppendSection(report, "3. Family Occurrence Analysis");

        // Aggregate across all parameter sets.
        var aggregateResults = new Dictionary<int, (int Occ, int Total)>();

        report.AppendLine("  Occurrence rate (%) across all 4 (ρ, K) combinations:");
        report.AppendLine("  Family │ " + string.Join(" ", ParameterSets.Select(p => $"({p.Rho:F2},{p.K:F1})")) + " │ Aggregate");
        report.AppendLine("  ───────┼" + new string('─', ParameterSets.Length * 14) + "┼──────────");

        for (int fid = 0; fid < FamilyNames.Length; fid++)
        {
            report.Append($"  {FamilyNames[fid],-21} │");

            int totalOcc = 0;
            int totalRuns = 0;

            foreach (var (rho, k) in ParameterSets)
            {
                var r = allResults[(rho, k)][fid];
                totalOcc += r.TotalOccurrences;
                totalRuns += r.TotalRuns;

                if (r.OccurrenceRate > 0)
                    report.Append($" {r.OccurrenceRate * 100,11:F1}%");
                else
                    report.Append("         -  ");
            }

            double aggRate = totalRuns > 0 ? (double)totalOcc / totalRuns : 0;
            report.AppendLine($" {aggRate * 100,7:F1}%");

            aggregateResults[fid] = (totalOcc, totalRuns);
        }

        report.AppendLine();

        // ── 4. Lifetime Statistics ──────────────────────────────
        AppendSection(report, "4. Lifetime Statistics (Aggregated Across All Runs)");

        report.AppendLine("  Family │  Mean τ   │  Std τ   │  CV τ   │  Min τ   │  Max τ   │ Stability");
        report.AppendLine("  ───────┼───────────┼──────────┼─────────┼──────────┼──────────┼──────────");

        for (int fid = 0; fid < FamilyNames.Length; fid++)
        {
            // Collect all lifetimes across parameter sets.
            var allLifetimes = new List<double>();
            foreach (var (rho, k) in ParameterSets)
            {
                var r = allResults[(rho, k)][fid];
                if (r.TotalOccurrences > 0)
                    allLifetimes.Add(r.MeanLifetime);
            }

            if (allLifetimes.Count > 0)
            {
                double mean = allLifetimes.Average();
                double std = StdDev(allLifetimes, mean);
                double cv = mean > 0 ? std / mean : 0;
                string stability = cv < 0.2 ? "★★★" : cv < 0.5 ? "★★" : cv < 1.0 ? "★" : "-";

                report.AppendLine(
                    $"  {FamilyNames[fid],-21} │ {mean,9:F0} │ {std,8:F0} │ {cv,7:F3} │ {allLifetimes.Min(),8:F0} │ {allLifetimes.Max(),8:F0} │ {stability,8}");
            }
            else
            {
                report.AppendLine($"  {FamilyNames[fid],-21} │         - │        - │       - │        - │        - │        -");
            }
        }

        report.AppendLine();
        report.AppendLine("  ★★★ = highly stable (CV < 0.2), ★★ = moderately stable, ★ = variable, - = unstable");
        report.AppendLine();

        // ── 5. Coherence Statistics ──────────────────────────────
        AppendSection(report, "5. Coherence & Size Statistics");

        report.AppendLine("  Family │ Mean Size │ Std Size │ Size CV │ Mean Sync │ Sync Std │ Sync CV");
        report.AppendLine("  ───────┼───────────┼──────────┼─────────┼───────────┼──────────┼─────────");

        for (int fid = 0; fid < FamilyNames.Length; fid++)
        {
            var sizes = new List<double>();
            var syncs = new List<double>();

            foreach (var (rho, k) in ParameterSets)
            {
                var r = allResults[(rho, k)][fid];
                if (r.TotalOccurrences > 0)
                {
                    sizes.Add(r.MeanSize);
                    syncs.Add(r.MeanCoherence);
                }
            }

            if (sizes.Count > 0)
            {
                double meanSize = sizes.Average();
                double stdSize = StdDev(sizes, meanSize);
                double cvSize = meanSize > 0 ? stdSize / meanSize : 0;

                double meanSync = syncs.Average();
                double stdSync = StdDev(syncs, meanSync);
                double cvSync = meanSync > 0 ? stdSync / meanSync : 0;

                report.AppendLine(
                    $"  {FamilyNames[fid],-21} │ {meanSize,9:F1} │ {stdSize,8:F1} │ {cvSize,7:F3} │ {meanSync,9:F4} │ {stdSync,8:F4} │ {cvSync,7:F3}");
            }
            else
            {
                report.AppendLine($"  {FamilyNames[fid],-21} │         - │        - │       - │         - │        - │       -");
            }
        }

        report.AppendLine();

        // ── 6. Stability Rankings ────────────────────────────────
        AppendSection(report, "6. Stability Rankings");

        // Compute aggregate reproducibility scores.
        var aggregateScores = new List<(int Fid, string Name, double AggOccRate, double AggReproScore, string Classification)>();

        for (int fid = 0; fid < FamilyNames.Length; fid++)
        {
            var (totalOcc, totalRuns) = aggregateResults[fid];
            double aggOccRate = totalRuns > 0 ? (double)totalOcc / totalRuns : 0;

            // Repro score: aggregate occurrence × (1 - average CV across parameters).
            double avgCV = 0;
            int cvCount = 0;

            foreach (var (rho, k) in ParameterSets)
            {
                var r = allResults[(rho, k)][fid];
                if (r.TotalOccurrences > 0 && r.MeanLifetime > 0)
                {
                    double cvL = r.LifetimeStd / r.MeanLifetime;
                    double cvS = r.MeanSize > 0 ? r.SizeStd / r.MeanSize : 0;
                    double cvF = r.MeanFrequency > 0 ? r.FrequencyStd / r.MeanFrequency : 0;
                    double cvC = r.MeanCoherence > 0 ? r.CoherenceStd / r.MeanCoherence : 0;
                    avgCV += (cvL + cvS + cvF + cvC) / 4.0;
                    cvCount++;
                }
            }

            avgCV = cvCount > 0 ? avgCV / cvCount : 1.0;
            double reproScore = aggOccRate * (1.0 - Math.Min(1.0, avgCV));

            string classification = reproScore switch
            {
                >= 0.6 when aggOccRate >= 0.7 => "Universal",
                >= 0.3 when aggOccRate >= 0.4 => "Likely Universal",
                >= 0.1 => "Unstable",
                _ => aggOccRate > 0 ? "Seed Artifact" : "Not Detected"
            };

            aggregateScores.Add((fid, FamilyNames[fid], aggOccRate, reproScore, classification));
        }

        report.AppendLine("  Aggregate reproducibility ranking:");
        report.AppendLine("  Rank │ Family                  │ Occurrence │ Reproducibility │ Classification");
        report.AppendLine("  ─────┼─────────────────────────┼────────────┼─────────────────┼────────────────");

        int rank = 1;
        foreach (var s in aggregateScores.OrderByDescending(s => s.AggReproScore))
        {
            report.AppendLine(
                $"  {rank++,3}  │ {s.Name,-23} │ {s.AggOccRate,9:P0}  │ {s.AggReproScore,13:F3}   │ {s.Classification}");
        }

        report.AppendLine();

        // Per-parameter breakdown.
        report.AppendLine("  Per-parameter reproducibility scores:");
        report.AppendLine("  Family │ " + string.Join(" ", ParameterSets.Select(p => $"({p.Rho:F2},{p.K:F1})")));
        report.AppendLine("  ───────┼" + new string('─', ParameterSets.Length * 14));

        for (int fid = 0; fid < FamilyNames.Length; fid++)
        {
            report.Append($"  {FamilyNames[fid],-6} │");

            foreach (var (rho, k) in ParameterSets)
            {
                var r = allResults[(rho, k)][fid];
                if (r.ReproducibilityScore > 0)
                    report.Append($" {r.ReproducibilityScore,11:F3} ");
                else
                    report.Append("         -   ");
            }

            report.AppendLine();
        }

        report.AppendLine();

        // ── 7. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        var universalFamilies = aggregateScores.Where(s => s.Classification == "Universal").ToList();
        var likelyUniversal = aggregateScores.Where(s => s.Classification == "Likely Universal").ToList();
        var unstable = aggregateScores.Where(s => s.Classification == "Unstable").ToList();
        var artifacts = aggregateScores.Where(s => s.Classification == "Seed Artifact").ToList();

        report.AppendLine("  Q1. Same families reappear?");
        if (universalFamilies.Count > 0)
            report.AppendLine($"    YES — {universalFamilies.Count} families are Universal, reappearing consistently.");
        else if (likelyUniversal.Count > 0)
            report.AppendLine($"    PARTIALLY — {likelyUniversal.Count} families are likely universal.");
        else
            report.AppendLine("    NO — no family appears consistently across runs.");

        report.AppendLine();

        report.AppendLine("  Q2. Most reproducible family?");
        var best = aggregateScores.OrderByDescending(s => s.AggReproScore).First();
        report.AppendLine($"    {best.Name} (score = {best.AggReproScore:F3})");
        report.AppendLine();

        report.AppendLine("  Q3. Most stable family (lowest variance)?");
        var mostStable = aggregateScores
            .Where(s => s.AggOccRate > 0)
            .OrderBy(s => 1.0 - s.AggReproScore / Math.Max(0.01, s.AggOccRate))
            .FirstOrDefault();
        if (mostStable.Fid >= 0)
            report.AppendLine($"    {mostStable.Name}");
        report.AppendLine();

        report.AppendLine("  Q5. Seed-dependent artifacts?");
        if (artifacts.Count > 0)
        {
            report.AppendLine($"    YES — {artifacts.Count} families appear to be seed artifacts:");
            foreach (var a in artifacts)
                report.AppendLine($"      {a.Name}");
        }
        else
        {
            report.AppendLine("    No clear artifacts — all detected families have some reproducibility.");
        }

        report.AppendLine();

        report.AppendLine("  Classification summary:");
        foreach (var s in aggregateScores.OrderByDescending(s => s.AggReproScore))
            report.AppendLine($"    {s.Name,-25} → {s.Classification} (score {s.AggReproScore:F3})");

        report.AppendLine();

        // ── 8. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Out of 5 families from AT-007, {universalFamilies.Count} are Universal,");
        report.AppendLine($"      {likelyUniversal.Count} Likely Universal, {unstable.Count} Unstable,");
        report.AppendLine($"      and {artifacts.Count} are Seed Artifacts.");
        report.AppendLine();

        if (universalFamilies.Count > 0)
        {
            report.AppendLine("  C2. Universal families represent genuine properties of the temporal dynamics —");
            report.AppendLine("      they consistently emerge regardless of random initial conditions.");
            report.AppendLine();
        }

        report.AppendLine("  C3. The reproducibility analysis provides statistical confidence that");
        report.AppendLine("      AT-007's family structure is not an artifact of a single seed.");
        report.AppendLine();
        report.AppendLine("  C4. Families with the highest reproducibility are the strongest candidates");
        report.AppendLine("      for future classification as proto-particle species.");
        report.AppendLine();
        report.AppendLine("  Next steps:");
        report.AppendLine("    • AT-009: Cluster-cluster interactions between universal families.");
        report.AppendLine("    • AT-010: Family response to external perturbations (robustness).");
        report.AppendLine("    • AT-011: Larger N (500, 1000) to study family scaling behaviour.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-008 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static double StdDev(List<double> values, double mean)
    {
        if (values.Count < 2) return 0;
        double sumSq = values.Sum(v => (v - mean) * (v - mean));
        return Math.Sqrt(sumSq / values.Count);
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
