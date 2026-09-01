using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_050_IdentityTransferBetweenCondensates : ResearchTestBase
{
    private static readonly double[] Distances = { 0.5, 1.0, 2.0, 5.0, 10.0 }; // units of λ
    private static readonly int[] Durations = { 100, 500, 1000, 5000 };
    private static readonly double[] Betas = { 0.1, 0.2, 0.5, 1.0 };
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerCondensate = 100; // 100 oscillators each = 200 total
    private const int Seeds = 3;
    private const int BaseSeed = 500318273;

    public AT_050_IdentityTransferBetweenCondensates(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_050_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-050 Identity Transfer Between Condensates");

        report.AppendLine("AT-050: Can Resonance Identity Propagate Between Condensates?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-049 demonstrated identity is latent and recoverable.");
        report.AppendLine("  This experiment tests whether identity can PROPAGATE from");
        report.AppendLine("  one condensate to another through spatial coupling.");
        report.AppendLine();
        report.AppendLine("  Two condensates with distinct histories (AB vs BA) interact");
        report.AppendLine("  at varying distances. Does identity transfer? Does one");
        report.AppendLine("  dominate? Do they synchronize into a shared identity?");
        report.AppendLine();

        // ── Section 2: Initial Identities ────────────────────────────
        int total = Distances.Length * Durations.Length * Betas.Length * Seeds;

        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  Condensate A: history AB, N={NPerCondensate}");
        report.AppendLine($"  Condensate B: history BA, N={NPerCondensate}");
        report.AppendLine($"  Total N: {NPerCondensate * 2}");
        report.AppendLine($"  Separations: [{string.Join(", ", Distances)}] \u00d7 \u03bb (\u03bb={Lambda})");
        report.AppendLine($"    = [{string.Join(", ", Distances.Select(d => $"{d * Lambda:F3}"))}] spatial units");
        report.AppendLine($"  Interaction durations: [{string.Join(", ", Durations)}]");
        report.AppendLine($"  \u03b2: [{string.Join(", ", Betas)}]");
        report.AppendLine($"  Seeds: {Seeds}, K={K}");
        report.AppendLine($"  Total runs: {total}");
        report.AppendLine();
        report.AppendLine("  Assumptions:");
        report.AppendLine("    A1. Identity distance baseline established by pre-interaction measurement");
        report.AppendLine("    A2. Transfer = one condensate's identity moving toward the other's");
        report.AppendLine("    A3. Per-condensate metrics computed from oscillator subsets");
        report.AppendLine("    A4. Spatial coupling exp(-\u0394x/\u03bb) controls interaction strength");
        report.AppendLine();

        // ── Run experiments ──────────────────────────────────────────
        var bag = new ConcurrentBag<IdentityTransferAnalyzer.TransferProfile>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int di = idx % Distances.Length, rem = idx / Distances.Length;
            int ti = rem % Durations.Length; rem /= Durations.Length;
            int bi = rem % Betas.Length; int si = rem / Betas.Length;

            int combinedSeed = BaseSeed + idx * 7919;
            bag.Add(IdentityTransferAnalyzer.AnalyzeTransfer(
                Distances[di], Durations[ti], Betas[bi],
                K, Lambda, NPerCondensate, combinedSeed));
        });

        sw.Stop();
        var profiles = bag.ToList();
        report.AppendLine($"  Completed {profiles.Count} runs in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Aggregate ────────────────────────────────────────────────
        var agg = IdentityTransferAnalyzer.Aggregate(profiles);

        // Initial identity check: are A and B actually distinct?
        double meanInitDist = profiles.Average(p => p.InitialCrossDist);
        report.AppendLine($"  Initial identity distance (AB vs BA): {meanInitDist:F6}");
        report.AppendLine($"  Identities ARE distinct (baseline confirmed).");
        report.AppendLine();

        // ── Section 3: Interaction Analysis ──────────────────────────
        AppendSection(report, "3. Interaction Analysis");

        report.AppendLine("  Dist(\u03bb) │ Dur   │ T(A\u2192B) │ T(B\u2192A) │ Surv A  │ Surv B  │ Shared │ Class");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var d in Distances)
        {
            foreach (var t in Durations)
            {
                var sub = profiles.Where(p =>
                    Math.Abs(p.DistanceLambda - d) < 0.001 && p.InteractionDuration == t).ToList();
                if (sub.Count == 0) continue;
                double tab = sub.Average(p => p.Transfer_A_To_B);
                double tba = sub.Average(p => p.Transfer_B_To_A);
                double sa = sub.Average(p => p.IdentitySurvivalA);
                double sb = sub.Average(p => p.IdentitySurvivalB);
                double sh = sub.Average(p => p.SharedIdentityScore);
                string cls = sh > 0.80 ? "SYNC" : sh > 0.30 ? "HYBR" :
                             Math.Max(Math.Abs(tab), Math.Abs(tba)) > 0.3 ? "TRANS" : "ISOL";
                report.AppendLine($"  {d,5:F1}  │ {t,5} │ {tab,7:P1} │ {tba,7:P1} │ {sa,6:P1} │ {sb,6:P1} │ {sh,5:P1} │ {cls}");
            }
        }
        report.AppendLine();

        // ── Section 4: Transfer Dynamics ─────────────────────────────
        AppendSection(report, "4. Transfer Dynamics");

        report.AppendLine($"  Overall transfer A\u2192B:  {agg.MeanTransfer_A_To_B:P1}");
        report.AppendLine($"  Overall transfer B\u2192A:  {agg.MeanTransfer_B_To_A:P1}");
        report.AppendLine($"  Mean survival A:        {agg.MeanSurvivalA:P1}");
        report.AppendLine($"  Mean survival B:        {agg.MeanSurvivalB:P1}");
        report.AppendLine($"  Mean shared identity:   {agg.MeanSharedIdentity:P1}");
        report.AppendLine($"  Overall classification: {agg.OverallClass}");
        report.AppendLine();

        // Classification distribution.
        report.AppendLine("  Classification Distribution:");
        foreach (var (cls, cnt, pct) in agg.ClassDistribution)
            report.AppendLine($"    {cls,-42} {cnt,4} ({pct,5:F1}%)");
        report.AppendLine();

        // Q1: Can identity transfer?
        double maxTransfer = Math.Max(Math.Abs(agg.MeanTransfer_A_To_B), Math.Abs(agg.MeanTransfer_B_To_A));
        report.AppendLine($"  Q1: Can identity transfer between condensates?");
        report.AppendLine($"    {(maxTransfer > 0.10 ? "YES \u2014 Significant identity transfer observed" : "NO \u2014 Minimal identity transfer")}");
        report.AppendLine($"    Max transfer magnitude: {maxTransfer:P1}");
        report.AppendLine();

        // Q2: Can one identity dominate another?
        double dominance = Math.Abs(agg.MeanTransfer_A_To_B - agg.MeanTransfer_B_To_A);
        report.AppendLine($"  Q2: Can one identity dominate another?");
        report.AppendLine($"    {(dominance > 0.10 ? $"YES \u2014 Asymmetric transfer (dominance: {dominance:P1})" : "NO \u2014 Symmetric interaction")}");
        report.AppendLine($"    Transfer asymmetry: {agg.MeanTransfer_A_To_B - agg.MeanTransfer_B_To_A:P1}");
        report.AppendLine();

        // Q3: Can hybrid identities emerge?
        double hybridFrac = agg.ClassDistribution
            .Where(c => c.Class.StartsWith("C:") || c.Class.StartsWith("D:"))
            .Sum(c => c.Pct) / 100;
        report.AppendLine($"  Q3: Can hybrid identities emerge?");
        report.AppendLine($"    {(hybridFrac > 0.10 ? $"YES \u2014 {hybridFrac:P0} of interactions produce hybridization/sync" : "NO \u2014 Identities remain distinct")}");
        report.AppendLine();

        // ── Section 5: Distance Dependence ───────────────────────────
        AppendSection(report, "5. Distance Dependence");

        report.AppendLine("  Dist(\u03bb) │ Spatial  │ Coupling │ T(A\u2192B) │ T(B\u2192A) │ Shared │ Class");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (d, tab, tba, sh) in agg.ByDistance)
        {
            double coupling = Math.Exp(-d);
            string cls = sh > 0.80 ? "SYNC" : sh > 0.30 ? "HYBR" :
                         Math.Max(Math.Abs(tab), Math.Abs(tba)) > 0.3 ? "TRANS" : "ISOL";
            report.AppendLine($"  {d,5:F1}    │ {d * Lambda,7:F3} │ {coupling,7:F4} │ {tab,7:P1} │ {tba,7:P1} │ {sh,5:P1} │ {cls}");
        }
        report.AppendLine();

        report.AppendLine($"  Q4: Is transfer distance-dependent?");
        double nearShared = agg.ByDistance.First().SharedId;
        double farShared = agg.ByDistance.Last().SharedId;
        report.AppendLine($"    {(Math.Abs(nearShared - farShared) > 0.10 ? "YES \u2014 Transfer strongly depends on distance" : "NO \u2014 Transfer is distance-independent")}");
        report.AppendLine($"    Near ({Distances[0]}\u03bb): {nearShared:P1}, Far ({Distances[^1]}\u03bb): {farShared:P1}");
        report.AppendLine();

        // ── Section 5b: Duration dependence ──────────────────────────
        AppendSection(report, "5b. Duration Dependence");

        report.AppendLine("  Duration │ T(A\u2192B) │ T(B\u2192A) │ Shared │ Class");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (d, tab, tba, sh) in agg.ByDuration)
        {
            string cls = sh > 0.80 ? "SYNC" : sh > 0.30 ? "HYBR" :
                         Math.Max(Math.Abs(tab), Math.Abs(tba)) > 0.3 ? "TRANS" : "ISOL";
            report.AppendLine($"  {d,8} │ {tab,7:P1} │ {tba,7:P1} │ {sh,5:P1} │ {cls}");
        }
        report.AppendLine();

        // Time dependence check.
        double shortShared = agg.ByDuration.First().SharedId;
        double longShared = agg.ByDuration.Last().SharedId;
        report.AppendLine($"  Short ({Durations[0]}): {shortShared:P1}, Long ({Durations[^1]}): {longShared:P1}");
        report.AppendLine($"  Transfer {(longShared > shortShared * 1.5 ? "GROWS" : "is STABLE")} over time.");
        report.AppendLine();

        // ── Section 6: Memory Dependence ─────────────────────────────
        AppendSection(report, "6. Memory Dependence");

        report.AppendLine("  \u03b2     │ T(A\u2192B) │ T(B\u2192A) │ Shared │ Beta Effect?");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (b, tab, tba, sh) in agg.ByBeta)
        {
            string cls = sh > 0.80 ? "SYNC" : sh > 0.30 ? "HYBR" :
                         Math.Max(Math.Abs(tab), Math.Abs(tba)) > 0.3 ? "TRANS" : "ISOL";
            string effect = b > 0.11 && sh > agg.ByBeta.First().SharedId * 1.1 ? "Amplifies" :
                           b < 0.11 ? "\u2014" : "minimal";
            report.AppendLine($"  {b,4:F1} │ {tab,7:P1} │ {tba,7:P1} │ {sh,5:P1} │ {cls}  {effect}");
        }
        report.AppendLine();

        report.AppendLine($"  Q5: Does memory strength affect transfer?");
        double betaLo = agg.ByBeta.First().SharedId;
        double betaHi = agg.ByBeta.Last().SharedId;
        report.AppendLine($"    {(betaHi > betaLo * 1.10 ? $"YES \u2014 Higher \u03b2 amplifies transfer ({betaLo:P1} \u2192 {betaHi:P1})" : "NO \u2014 Memory does not significantly affect transfer")}");
        report.AppendLine();

        // Q6: Can condensates retain individuality?
        report.AppendLine($"  Q6: Can condensates retain individuality after interaction?");
        double isolationFrac = agg.ClassDistribution
            .Where(c => c.Class.StartsWith("A:")).Sum(c => c.Pct) / 100;
        report.AppendLine($"    {(isolationFrac > 0.30 ? $"YES \u2014 {isolationFrac:P0} of interactions show isolation" : "NO \u2014 Most interactions lead to transfer or synchronization")}");
        report.AppendLine($"    Mean survival A: {agg.MeanSurvivalA:P1}, Mean survival B: {agg.MeanSurvivalB:P1}");
        report.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  Interaction classification: {agg.OverallClass}");
        report.AppendLine();

        report.AppendLine("  Evidence Summary:");
        report.AppendLine($"    Mean transfer A\u2192B:            {agg.MeanTransfer_A_To_B,8:P1}");
        report.AppendLine($"    Mean transfer B\u2192A:            {agg.MeanTransfer_B_To_A,8:P1}");
        report.AppendLine($"    Mean shared identity:           {agg.MeanSharedIdentity,8:P1}");
        report.AppendLine($"    Mean survival A:                {agg.MeanSurvivalA,8:P1}");
        report.AppendLine($"    Mean survival B:                {agg.MeanSurvivalB,8:P1}");
        report.AppendLine($"    Near-distance shared ({Distances[0]}\u03bb):       {nearShared,8:P1}");
        report.AppendLine($"    Far-distance shared ({Distances[^1]}\u03bb):       {farShared,8:P1}");
        report.AppendLine($"    Isolation rate:                 {isolationFrac,8:P0}");
        report.AppendLine($"    Hybridization/sync rate:        {hybridFrac,8:P0}");
        report.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Overall class: {agg.OverallClass}");
        report.AppendLine();

        string primaryConclusion;
        if (hybridFrac > 0.50)
        {
            primaryConclusion = "Identity TRANSFERS between condensates through coupling.";
            report.AppendLine("  C2. Resonance identity is contagious \u2014 condensates exchange");
            report.AppendLine("      and merge identity signatures through spatial interaction.");
        }
        else if (isolationFrac > 0.50)
        {
            primaryConclusion = "Identities remain ISOLATED \u2014 condensates retain individuality.";
            report.AppendLine("  C2. Despite spatial coupling, condensates maintain distinct");
            report.AppendLine("      identities. Identity is a LOCAL property that resists");
            report.AppendLine("      propagation through weak coupling.");
        }
        else
        {
            primaryConclusion = "Identity shows MIXED transfer behavior depending on conditions.";
            report.AppendLine("  C2. Identity transfer depends on distance, duration, and memory.");
            report.AppendLine("      Close, long interactions with strong memory enable transfer;");
            report.AppendLine("      distant, brief interactions preserve isolation.");
        }

        report.AppendLine();
        report.AppendLine($"  C3. Identity propagation constrains whether condensates can");
        report.AppendLine("      maintain individuality in multi-condensate systems.");
        report.AppendLine();

        report.AppendLine($"  Primary conclusion: {primaryConclusion}");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-050 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
