using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_064_UniversalityOfEffectiveAttraction : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const double Beta = 0.5;
    private const int NPerGroup = 50;
    private const int BaseSeed = 640817239;

    public TQM_064_UniversalityOfEffectiveAttraction(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_064_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-064 Universality of Effective Attraction");

        report.AppendLine("TQM-064: Is Spatial Attraction Universal or Coupling-Specific?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-062 showed spatial convergence with cos(Δθ) coupling.");
        report.AppendLine("  This tests whether attraction persists across 8 different");
        report.AppendLine("  coupling force laws — or is specific to cos(Δθ).");
        report.AppendLine();

        // ── Section 2: Setup ─────────────────────────────────────────
        string[] lawNames = {
            "K1: cos(Δθ)", "K2: sin(Δθ)", "K3: cos²(Δθ)", "K4: exp(-|Δθ|)",
            "K5: 1/(1+|Δθ|)", "K6: cos*exp(-|Δθ|)", "K7: sign(cos(Δθ))", "K8: 1-|Δθ|/π"
        };
        double[] separations = { 0.5, 2.0 };
        int seeds = 2;

        AppendSection(report, "2. Coupling Laws & Setup");
        report.AppendLine($"  {lawNames.Length} force laws for position dynamics");
        report.AppendLine($"  Separations: [{string.Join(", ", separations)}]λ");
        report.AppendLine($"  Seeds: {seeds}, β = {Beta}, N = {NPerGroup * 2}");
        report.AppendLine($"  Phase coupling: always sin(Δθ) (standard Kuramoto)");
        report.AppendLine($"  Position coupling: varies by law");
        report.AppendLine($"  Total: {lawNames.Length * separations.Length * seeds} runs");
        report.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var bag = new ConcurrentBag<CouplingUniversalityAnalyzer.LawProfile>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        int total = lawNames.Length * separations.Length * seeds;
        Parallel.For(0, total, idx =>
        {
            int li = idx % lawNames.Length, rem = idx / lawNames.Length;
            int si = rem % separations.Length; int seedI = rem / separations.Length;
            bag.Add(CouplingUniversalityAnalyzer.RunCouplingLaw(
                lawNames[li], separations[si], Beta, K, Lambda, NPerGroup,
                BaseSeed + idx * 7919));
        });

        sw.Stop();
        var profiles = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        var univ = CouplingUniversalityAnalyzer.AnalyzeUniversality(profiles);

        // ── Section 3: Synchronization Results ───────────────────────
        AppendSection(report, "3. Synchronization & Attraction Results");

        report.AppendLine("  Coupling Law          │ Sepλ │ ΔSep     │ Converge?│ Sync? │ R_A   R_B");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in profiles.OrderBy(p => p.LawName).ThenBy(p => p.SeparationLambda))
        {
            string conv = p.Converges ? "\u25BC" : "\u25B2";
            string sync = p.Synchronizes ? "\u2713" : " ";
            string name = p.LawName.Length > 20 ? p.LawName[..20] : p.LawName;
            report.AppendLine($"  {name,-20} │ {p.SeparationLambda,4:F1} │ {p.SeparationChange,8:F4} │ {conv,7}    │ {sync,4}  │ {p.FinalRA,5:F3} {p.FinalRB,5:F3}");
        }
        report.AppendLine();

        // ── Section 4: Per-Law Summary ───────────────────────────────
        AppendSection(report, "4. Per-Law Attraction & Sync Scores");

        report.AppendLine("  Coupling Law          │ AttrScore │ SyncScore │ Converge% │ Sync%");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (string law in lawNames)
        {
            var sub = profiles.Where(p => p.LawName == law).ToList();
            if (sub.Count == 0) continue;
            double attr = sub.Average(p => p.AttractionScore);
            double sync = sub.Average(p => p.SyncScore);
            double convPct = (double)sub.Count(p => p.Converges) / sub.Count;
            double syncPct = (double)sub.Count(p => p.Synchronizes) / sub.Count;
            string name = law.Length > 20 ? law[..20] : law;
            report.AppendLine($"  {name,-20} │ {attr,8:P1} │ {sync,8:P1} │ {convPct,8:P0} │ {syncPct,7:P0}");
        }
        report.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        report.AppendLine($"  Q1: Do all coupling laws produce attraction?");
        report.AppendLine($"    Convergence: {univ.ConvergenceFraction:P0} of all runs");
        report.AppendLine($"    {(univ.ConvergenceFraction > 0.8 ? "YES \u2014 Nearly universal" : univ.ConvergenceFraction > 0.5 ? "PARTIALLY \u2014 Most laws produce attraction" : "NO \u2014 Attraction is law-specific")}");
        report.AppendLine();

        // Count laws where sync happens without attraction.
        var byLaw = lawNames.Select(l => profiles.Where(p => p.LawName == l).ToList()).ToList();
        int syncNoAttr = byLaw.Count(sub => sub.Average(p => p.SyncScore) > 0.8 && sub.Average(p => p.AttractionScore) < 0.3);
        report.AppendLine($"  Q2: Which laws synchronize without attraction?");
        report.AppendLine($"    {syncNoAttr} law(s) synchronize without attracting");
        report.AppendLine();

        report.AppendLine($"  Q3: Can attraction emerge from synchronization alone?");
        report.AppendLine($"    {(univ.ConvergenceFraction > 0.5 ? "YES \u2014 Synchronization produces attraction in most laws" : "NO \u2014 Synchronization does not guarantee attraction")}");
        report.AppendLine();

        report.AppendLine($"  Q4: Can synchronization occur with no spatial convergence?");
        int syncFrac = profiles.Count(p => p.Synchronizes);
        int syncNoConv = profiles.Count(p => p.Synchronizes && !p.Converges);
        report.AppendLine($"    {(syncNoConv > 0 ? $"YES \u2014 {syncNoConv}/{syncFrac} synchronized pairs do NOT converge" : "NO \u2014 All synchronized pairs converge")}");
        report.AppendLine();

        report.AppendLine($"  Q5: Is attraction proportional to sync strength?");
        double corr = Correlation(profiles.Select(p => p.SyncScore).ToList(),
                                  profiles.Select(p => p.AttractionScore).ToList());
        report.AppendLine($"    r = {corr:F4} — {(Math.Abs(corr) > 0.5 ? "YES" : "NO")}");
        report.AppendLine();

        report.AppendLine($"  Q6: Which effects are universal?");
        report.AppendLine($"    Convergence: {univ.ConvergenceFraction:P0} universal");
        report.AppendLine($"    Synchronization: {univ.SyncFraction:P0} universal");
        report.AppendLine();

        // ── Interpretation ───────────────────────────────────────────
        AppendSection(report, "5. Interpretation");
        report.AppendLine($"  Classification: {univ.Classification}");
        report.AppendLine($"  Mean attraction score: {univ.MeanAttractionScore:P1}");
        report.AppendLine();

        // ── Conclusion ───────────────────────────────────────────────
        AppendSection(report, "6. Conclusion");
        report.AppendLine($"  C1. Classification: {univ.Classification}");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-064 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static double Correlation(List<double> x, List<double> y)
    {
        double mx = x.Average(), my = y.Average();
        double cov = 0, vx = 0, vy = 0;
        for (int i = 0; i < x.Count; i++)
        { cov += (x[i] - mx) * (y[i] - my); vx += (x[i] - mx) * (x[i] - mx); vy += (y[i] - my) * (y[i] - my); }
        return cov / Math.Sqrt(Math.Max(vx, 1e-15) * Math.Max(vy, 1e-15));
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
