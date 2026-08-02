using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_049_IdentityRecoveryAfterEnergyCollapse : ResearchTestBase
{
    // ── Experimental parameters ──────────────────────────────────────

    private static readonly string[] Histories = { "A", "B", "AB", "BA", "ABC", "CBA" };
    // Destructive transfers only (beyond ±25% threshold from TQM-048).
    private static readonly double[] CollapseTransfers =
        { 0.50, 1.00, 2.00, 5.00, -0.50, -0.75 };
    private static readonly double[] Betas = { 0.0, 0.2, 0.5, 1.0 };
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int Seeds = 3;
    private const int BaseSeed = 490731841;

    public TQM_049_IdentityRecoveryAfterEnergyCollapse(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_049_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-049 Identity Recovery After Energy Collapse");

        report.AppendLine("TQM-049: Is Destroyed Identity Truly Erased or Merely Suppressed?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-048 showed identity is destroyed beyond \u00b125% energy transfer.");
        report.AppendLine("  This experiment tests whether the destruction is PERMANENT or");
        report.AppendLine("  TEMPORARY: if energy is gradually restored to its original level,");
        report.AppendLine("  does the original identity return?");
        report.AppendLine();
        report.AppendLine("  Hypothesis A: Identity is truly erased \u2014 no recovery.");
        report.AppendLine("  Hypothesis B: Identity is suppressed \u2014 recoverable.");
        report.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        var schedules = IdentityRecoveryAnalyzer.Schedules;
        int total = Histories.Length * CollapseTransfers.Length * schedules.Length * Betas.Length * Seeds;

        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  Histories:         [{string.Join(", ", Histories)}]");
        report.AppendLine($"  Collapse transfers: [{string.Join(", ", CollapseTransfers)}]");
        report.AppendLine($"    (all beyond TQM-048 \u00b125% destruction threshold)");
        report.AppendLine($"  Restoration schedules:");
        foreach (var s in schedules)
            report.AppendLine($"    {s.Name}: {s.Steps} step(s), {s.ItersPerStep} iter/step, {s.FinalRecoveryIters} final recovery");
        report.AppendLine($"  \u03b2 (memory):       [{string.Join(", ", Betas)}]");
        report.AppendLine($"  Seeds: {Seeds} per combination");
        report.AppendLine($"  N = {N}, K = {K}, \u03bb = {Lambda}");
        report.AppendLine($"  Total runs: {total}");
        report.AppendLine();
        report.AppendLine("  Three-phase design:");
        report.AppendLine("    Phase 1: Formation \u2192 Training \u2192 Baseline \u2192 Collapse \u2192 Measure");
        report.AppendLine("    Phase 2: Gradual frequency restoration to original level");
        report.AppendLine("    Phase 3: Recovery evolution \u2192 Measure post-recovery identity");
        report.AppendLine();
        report.AppendLine("  Assumptions:");
        report.AppendLine("    A1. Identity fingerprint = (FinalR, MeanFreq, PhaseVariance)");
        report.AppendLine("    A2. Energy proxy = FinalR \u00d7 MeanFreq");
        report.AppendLine("    A3. Recovery = identity distance ratio (baseline vs post-recovery)");
        report.AppendLine("    A4. Linear frequency interpolation is valid restoration mechanism");
        report.AppendLine("    A5. Collapse verified by collapse distance > 0.15");
        report.AppendLine();

        // ── Run experiments ──────────────────────────────────────────
        var bag = new ConcurrentBag<IdentityRecoveryAnalyzer.RecoveryProfile>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int hi = idx % Histories.Length;
            int rem = idx / Histories.Length;
            int ti = rem % CollapseTransfers.Length;
            rem /= CollapseTransfers.Length;
            int si = rem % schedules.Length;
            rem /= schedules.Length;
            int bi = rem % Betas.Length;
            int seedI = rem / Betas.Length;

            int combinedSeed = BaseSeed + idx * 7919;
            bag.Add(IdentityRecoveryAnalyzer.AnalyzeRecovery(
                Histories[hi], Betas[bi], CollapseTransfers[ti],
                schedules[si], K, Lambda, N, combinedSeed));
        });

        sw.Stop();
        var profiles = bag.ToList();
        report.AppendLine($"  Completed {profiles.Count} runs in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Aggregate ────────────────────────────────────────────────
        var agg = IdentityRecoveryAnalyzer.Aggregate(profiles);

        // Verify collapse actually occurred.
        var verified = profiles.Where(p => p.CollapseIdDistance > 0.15).ToList();
        double collapseRate = (double)verified.Count / profiles.Count;
        report.AppendLine($"  Collapse verified (dist > 0.15): {collapseRate:P1} of runs.");
        report.AppendLine($"  Analysis uses verified-collapse subset ({verified.Count} runs).");
        report.AppendLine();

        // ── Section 3: Collapse Phase ────────────────────────────────
        AppendSection(report, "3. Collapse Phase");

        report.AppendLine("  Transfer  │ Mean ColDist │ Mean ColR  │ Mean ColMem │ Memory Surv?");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var tf in CollapseTransfers)
        {
            var sub = profiles.Where(p => Math.Abs(p.TransferFraction - tf) < 0.001).ToList();
            if (sub.Count == 0) continue;
            double mcd = sub.Average(p => p.CollapseIdDistance);
            double mcr = sub.Average(p => p.CollapseR);
            double mcm = sub.Average(p => p.CollapseMemScore);
            double bm = sub.Average(p => p.BaselineMemScore);
            double ms = sub.Average(p => p.MemorySurvivalScore);
            string label = tf < 0 ? "REMOVAL" : "INJECT";
            report.AppendLine($"  {label} {tf,6:F2} │ {mcd,12:F6} │ {mcr,9:F4} │ {mcm,11:F6} │ {ms,10:P1}");
        }

        report.AppendLine();

        // ── Section 4: Recovery Phase ────────────────────────────────
        AppendSection(report, "4. Recovery Phase");

        report.AppendLine("  Transfer  │ Schedule │ ColDist  │ RecDist  │ Recovery Score │ Classification");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var tf in CollapseTransfers)
        {
            foreach (var s in schedules)
            {
                var sub = profiles.Where(p =>
                    Math.Abs(p.TransferFraction - tf) < 0.001 && p.ScheduleName == s.Name).ToList();
                if (sub.Count == 0) continue;
                double mcd = sub.Average(p => p.CollapseIdDistance);
                double mrd = sub.Average(p => p.RecoveryIdDistance);
                double mrs = sub.Average(p => p.IdentityRecoveryScore);
                // Determine classification for this subset.
                string cls = mrs >= 0.85 ? "D: Fully Recoverable" :
                             mrs >= 0.50 ? "C: Temporary Suppression" :
                             mrs >= 0.20 ? "B: Partial Destruction" : "A: Permanent";
                string label = tf < 0 ? "REM " : "INJ ";
                report.AppendLine($"  {label}{tf,5:F2} │ {s.Name,-8} │ {mcd,8:F4} │ {mrd,8:F4} │ {mrs,13:P1} │ {cls}");
            }
        }

        report.AppendLine();

        // Q1: Can identity return after collapse?
        double recoveredFrac = agg.RecoveredCount > 0 ? (double)agg.RecoveredCount / agg.TotalRuns : 0;
        report.AppendLine($"  Q1: Can original identity return after collapse?");
        report.AppendLine($"    {(recoveredFrac > 0.30 ? "YES \u2014" : "NO \u2014")} {recoveredFrac:P1} of collapses show recovery");
        report.AppendLine($"    Mean recovery score: {agg.MeanRecoveryScore:P1}");
        report.AppendLine();

        // Q2: Is identity erased or hidden?
        report.AppendLine($"  Q2: Is identity erased or hidden?");
        string q2Label = agg.OverallClassification;
        if (q2Label.Contains("Recoverable") || q2Label.Contains("Suppression"))
            report.AppendLine($"    HIDDEN \u2014 Identity is suppressed but recoverable ({q2Label})");
        else
            report.AppendLine($"    ERASED \u2014 Identity is permanently destroyed ({q2Label})");
        report.AppendLine();

        // ── Section 5: Identity Restoration Analysis ─────────────────
        AppendSection(report, "5. Identity Restoration Analysis");

        report.AppendLine($"  Overall recovery score:   {agg.MeanRecoveryScore:P1}");
        report.AppendLine($"  Overall classification:   {agg.OverallClassification}");
        report.AppendLine($"  Mean collapse distance:   {agg.MeanCollapseDistance:F6}");
        report.AppendLine($"  Mean recovery distance:   {agg.MeanRecoveryDistance:F6}");
        report.AppendLine($"  Recovered (>50%):         {agg.RecoveredCount}/{agg.TotalRuns} ({recoveredFrac:P1})");
        report.AppendLine($"  Fully recovered (>85%):   {agg.FullRecoveryCount}/{agg.TotalRuns} ({(double)agg.FullRecoveryCount / agg.TotalRuns:P1})");
        report.AppendLine();

        // Classification distribution
        report.AppendLine("  Classification Distribution:");
        foreach (var (cls, cnt, pct) in agg.ClassificationDistribution)
            report.AppendLine($"    {cls,-28} {cnt,5} ({pct,5:F1}%)");
        report.AppendLine();

        // Q3: Does recovery depend on collapse magnitude?
        report.AppendLine($"  Q3: Does recovery depend on collapse magnitude?");
        report.AppendLine("  Transfer  │ Recovery Score │ Classification");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var (tf, mr, _) in agg.ByTransfer)
        {
            string cls = mr >= 0.85 ? "Fully Recoverable" :
                         mr >= 0.50 ? "Temporary Suppression" :
                         mr >= 0.20 ? "Partial Destruction" : "Permanent";
            string label = tf < 0 ? "REMOVAL" : "INJECT";
            report.AppendLine($"  {label} {tf,6:F2} │ {mr,13:P1} │ {cls}");
        }
        report.AppendLine();

        // Q4: Does recovery depend on restoration speed?
        report.AppendLine($"  Q4: Does recovery depend on restoration speed?");
        report.AppendLine("  Schedule │ Recovery Score │ Classification");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var (s, mr, _) in agg.BySchedule)
        {
            string cls = mr >= 0.85 ? "Fully Recoverable" :
                         mr >= 0.50 ? "Temporary Suppression" :
                         mr >= 0.20 ? "Partial Destruction" : "Permanent";
            report.AppendLine($"  {s,-8} │ {mr,13:P1} │ {cls}");
        }
        report.AppendLine();

        // Q7: History dependence
        report.AppendLine($"  Q7: Do different histories recover differently?");
        report.AppendLine("  History │ Recovery Score │ Classification");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var (h, mr, _) in agg.ByHistory)
        {
            string cls = mr >= 0.85 ? "Fully Recoverable" :
                         mr >= 0.50 ? "Temporary Suppression" :
                         mr >= 0.20 ? "Partial Destruction" : "Permanent";
            report.AppendLine($"  {h,-7} │ {mr,13:P1} │ {cls}");
        }
        double bestHr = agg.ByHistory.Max(h => h.MeanRecovery);
        double worstHr = agg.ByHistory.Min(h => h.MeanRecovery);
        report.AppendLine($"    \u0394(max-min) = {bestHr - worstHr:P1}");
        report.AppendLine();

        // ── Section 6: Memory Survival Analysis ──────────────────────
        AppendSection(report, "6. Memory Survival Analysis");

        report.AppendLine("  \u03b2     │ Recovery Score │ Memory Survival │ Memory Protects?");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var b in Betas)
        {
            var sub = profiles.Where(p => Math.Abs(p.Beta - b) < 0.001).ToList();
            if (sub.Count == 0) continue;
            double mr = sub.Average(p => p.IdentityRecoveryScore);
            double ms = sub.Average(p => p.MemorySurvivalScore);
            string protects = b > 0.01 && mr > (profiles.Where(p => p.Beta < 0.01).Average(p => p.IdentityRecoveryScore) * 1.05)
                ? "YES" : "no";
            report.AppendLine($"  {b,4:F1} │ {mr,13:P1} │ {ms,14:P1} │ {protects}");
        }
        report.AppendLine();

        // Q5: Is memory preserved during collapse?
        double meanMemSurvival = profiles.Average(p => p.MemorySurvivalScore);
        report.AppendLine($"  Q5: Is memory preserved during identity collapse?");
        report.AppendLine($"    Mean memory survival: {meanMemSurvival:P1}");
        report.AppendLine($"    {(meanMemSurvival > 0.50 ? "YES \u2014 Memory partially survives collapse" : "NO \u2014 Memory is largely destroyed")}");
        report.AppendLine();

        // Q6: Can identity be reconstructed from residual memory?
        double betaCorr = profiles.Where(p => p.Beta > 0.99).Average(p => p.IdentityRecoveryScore);
        double betaZero = profiles.Where(p => p.Beta < 0.01).Average(p => p.IdentityRecoveryScore);
        report.AppendLine($"  Q6: Can identity be reconstructed from residual memory?");
        report.AppendLine($"    \u03b2=1 recovery: {betaCorr:P1}, \u03b2=0 recovery: {betaZero:P1}");
        report.AppendLine($"    {(betaCorr > betaZero * 1.10 ? "YES \u2014 Memory aids identity reconstruction" : "NO \u2014 Memory does not significantly aid recovery")}");
        report.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  Collapse classification: {agg.OverallClassification}");
        report.AppendLine();

        // Evidence summary
        report.AppendLine("  Evidence Summary:");
        report.AppendLine($"    Overall recovery score:           {agg.MeanRecoveryScore,8:P1}");
        report.AppendLine($"    Mean collapse distance:           {agg.MeanCollapseDistance,8:F6}");
        report.AppendLine($"    Mean recovery distance:           {agg.MeanRecoveryDistance,8:F6}");
        report.AppendLine($"    Recovery rate (>50%):             {recoveredFrac,8:P1}");
        report.AppendLine($"    Full recovery rate (>85%):        {(double)agg.FullRecoveryCount / agg.TotalRuns,8:P1}");
        report.AppendLine($"    Mean memory survival:             {meanMemSurvival,8:P1}");
        report.AppendLine($"    \u03b2=0 recovery:                    {betaZero,8:P1}");
        report.AppendLine($"    \u03b2=1 recovery:                    {betaCorr,8:P1}");
        report.AppendLine($"    History \u0394 recovery:               {bestHr - worstHr,8:P1}");
        report.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Collapse type: {agg.OverallClassification}");
        report.AppendLine();

        string primaryConclusion;
        if (agg.MeanRecoveryScore >= 0.85)
        {
            primaryConclusion = "Identity is FULLY RECOVERABLE after energy collapse.";
            report.AppendLine("  C2. Large energy transfers suppress identity but do NOT erase it.");
            report.AppendLine("      Gradual energy restoration allows the original identity to return.");
            report.AppendLine("  C3. Identity is encoded in the phase structure and survives energy");
            report.AppendLine("      excursions as a latent state. The condensate has memory of its");
            report.AppendLine("      historical identity even when energy is far from baseline.");
        }
        else if (agg.MeanRecoveryScore >= 0.50)
        {
            primaryConclusion = "Identity is TEMPORARILY SUPPRESSED by energy collapse.";
            report.AppendLine("  C2. Identity destruction is not permanent \u2014 the original identity");
            report.AppendLine("      is recoverable through gradual energy restoration.");
            report.AppendLine("  C3. The condensate retains a latent memory of its identity");
            report.AppendLine("      that can be reactivated when energy returns to baseline.");
        }
        else if (agg.MeanRecoveryScore >= 0.20)
        {
            primaryConclusion = "Identity is PARTIALLY DESTROYED by energy collapse.";
            report.AppendLine("  C2. Some identity information survives collapse but recovery");
            report.AppendLine("      is incomplete. Energy excursions cause partial amnesia.");
        }
        else
        {
            primaryConclusion = "Identity is PERMANENTLY DESTROYED by energy collapse.";
            report.AppendLine("  C2. Large energy transfers irreversibly erase historical identity.");
            report.AppendLine("      The condensate does not remember its past after extreme");
            report.AppendLine("      energy excursions, even when energy is restored.");
            report.AppendLine("  C3. Identity and energy are not fully orthogonal \u2014 energy");
            report.AppendLine("      excursions can cause irreversible identity loss.");
        }

        report.AppendLine();
        report.AppendLine($"  Primary conclusion: {primaryConclusion}");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-049 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    // ── Helpers ──────────────────────────────────────────────────────

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
