using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_048_IdentityPreservationUnderEnergyTransfer : ResearchTestBase
{
    // ── Experimental parameters ──────────────────────────────────────

    private static readonly string[] Histories = { "A", "B", "AB", "BA", "ABC", "CBA" };
    // Positive = energy injection, negative = energy removal
    private static readonly double[] Transfers =
        { -0.75, -0.50, -0.25, -0.10, 0.00, 0.10, 0.25, 0.50, 1.00, 2.00, 5.00 };
    private static readonly double[] Betas = { 0.0, 0.1, 0.2, 0.5, 1.0 };
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int Seeds = 4;
    private const int BaseSeed = 480112359;

    public AT_048_IdentityPreservationUnderEnergyTransfer(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_048_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-048 Identity Preservation Under Energy Transfer");

        report.AppendLine("AT-048: Does Resonance Identity Survive Energy Exchange?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-047 demonstrated identity and energy are statistically");
        report.AppendLine("  independent across runs. This experiment tests whether identity");
        report.AppendLine("  survives WITHIN a single condensate when energy is deliberately");
        report.AppendLine("  changed via injection or removal.");
        report.AppendLine();
        report.AppendLine("  Hypothesis: Identity and energy are orthogonal degrees of");
        report.AppendLine("  freedom. Energy transfer should modify energy without");
        report.AppendLine("  necessarily changing identity.");
        report.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        int total = Histories.Length * Transfers.Length * Betas.Length * Seeds;

        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  Histories:      [{string.Join(", ", Histories)}]");
        report.AppendLine($"  Transfers:      [{string.Join(", ", Transfers)}]");
        report.AppendLine($"    Positive = injection (frequency scale-up)");
        report.AppendLine($"    Negative = removal (frequency scale-down)");
        report.AppendLine($"  \u03b2 (memory):    [{string.Join(", ", Betas)}]");
        report.AppendLine($"  Seeds: {Seeds} per combination");
        report.AppendLine($"  N = {N}, K = {K}, \u03bb = {Lambda}");
        report.AppendLine($"  Total runs: {total}");
        report.AppendLine($"  Phases: formation(1500) + history({Histories[^1].Length}\u00d7400) + recovery(1500)");
        report.AppendLine();
        report.AppendLine("  Assumptions:");
        report.AppendLine("    A1. Identity fingerprint = (FinalR, MeanFreq, PhaseVariance)");
        report.AppendLine("    A2. Energy proxy = FinalR \u00d7 MeanFreq");
        report.AppendLine("    A3. Identity preserved if normalized distance < 0.15");
        report.AppendLine("    A4. Energy transfer = multiplicative frequency scaling");
        report.AppendLine("    A5. BEFORE/AFTER comparison captures identity survival");
        report.AppendLine();

        // ── Run experiments ──────────────────────────────────────────
        var bag = new ConcurrentBag<IdentityEnergyTransferAnalyzer.TransferProfile>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int hi = idx % Histories.Length;
            int rem = idx / Histories.Length;
            int ti = rem % Transfers.Length;
            rem /= Transfers.Length;
            int bi = rem % Betas.Length;
            int si = rem / Betas.Length;

            int combinedSeed = BaseSeed + idx * 7919;
            bag.Add(IdentityEnergyTransferAnalyzer.AnalyzeTransfer(
                Histories[hi], Betas[bi], Transfers[ti],
                K, Lambda, N, combinedSeed));
        });

        sw.Stop();
        var profiles = bag.ToList();
        report.AppendLine($"  Completed {profiles.Count} runs in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Compute preservation result ──────────────────────────────
        var result = IdentityEnergyTransferAnalyzer.AnalyzePreservation(profiles);

        // ── Section 3: Energy Transfer Analysis ──────────────────────
        AppendSection(report, "3. Energy Transfer Analysis");

        report.AppendLine("  Transfer │ Mean IdDist │ Mean |\u0394E| │ Pres. Rate │ Identity Survives?");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (tf, mid, med, pr) in result.ByTransfer)
        {
            string label = tf switch
            {
                < -0.01 => "REMOVAL ",
                > 0.01 => "INJECT  ",
                _ => "BASELINE"
            };
            string survives = pr >= 0.70 ? "YES (strong)" :
                              pr >= 0.50 ? "YES (majority)" :
                              pr >= 0.30 ? "partial" : "NO";
            report.AppendLine($"  {label} {tf,6:F2} │ {mid,12:F6} │ {med,10:F4} │ {pr,9:P1} │ {survives}");
        }

        report.AppendLine();

        // Q1 & Q3 combined
        report.AppendLine($"  Q1: Can energy change while identity remains constant?");
        bool q1 = result.PreservationRate >= 0.50;
        report.AppendLine($"    {(q1 ? "YES" : "NO")} \u2014 {result.PreservationRate:P1} of transfers preserve identity");
        report.AppendLine($"    Mean identity distance: {result.MeanIdentityDistance:F6}");
        report.AppendLine();

        // Q2: Can identity change while energy remains constant?
        var baseline = profiles.Where(p => Math.Abs(p.TransferFraction) < 0.001).ToList();
        double baselineIdDist = baseline.Count > 0 ? baseline.Average(p => p.IdentityDistance) : 0;
        report.AppendLine($"  Q2: Can identity change while energy remains constant?");
        report.AppendLine($"    Baseline (0% transfer) identity distance: {baselineIdDist:F6}");
        report.AppendLine($"    {(baselineIdDist > 0.02 ? "YES \u2014 Some identity drift occurs even without energy transfer" : "NO \u2014 Identity stable at fixed energy")}");
        report.AppendLine();

        // Q3: Do large energy transfers erase identity?
        var extremeInj = profiles.Where(p => Math.Abs(p.TransferFraction - 5.0) < 0.01).ToList();
        var extremeRem = profiles.Where(p => Math.Abs(p.TransferFraction + 0.75) < 0.01).ToList();
        double extInjPres = extremeInj.Count > 0 ? (double)extremeInj.Count(p => p.IdentityPreserved) / extremeInj.Count : 0;
        double extRemPres = extremeRem.Count > 0 ? (double)extremeRem.Count(p => p.IdentityPreserved) / extremeRem.Count : 0;

        report.AppendLine($"  Q3: Do large energy transfers erase identity?");
        report.AppendLine($"    500% injection preservation:  {extInjPres:P1}");
        report.AppendLine($"    -75% removal preservation:    {extRemPres:P1}");
        report.AppendLine($"    {(extInjPres < 0.30 || extRemPres < 0.30 ? "YES \u2014 Extreme transfers significantly degrade identity" : "NO \u2014 Identity survives extreme energy transfers")}");
        report.AppendLine();

        // ── Section 4: Identity Preservation ─────────────────────────
        AppendSection(report, "4. Identity Preservation");

        report.AppendLine($"  Overall preservation rate:  {result.PreservationRate:P1}");
        report.AppendLine($"  Preserved: {result.PreservedCount}/{result.TotalTransfers}");
        report.AppendLine($"  Mean identity distance:     {result.MeanIdentityDistance:F6}");
        report.AppendLine($"  Mean |energy change|:       {result.MeanEnergyChange:F4}");
        report.AppendLine();

        report.AppendLine("  History │ Mean IdDist │ Pres. Rate │ Robust?");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (h, mid, pr) in result.ByHistory)
        {
            bool robust = mid < result.MeanIdentityDistance;
            report.AppendLine($"  {h,-7} │ {mid,12:F6} │ {pr,9:P1} │ {(robust ? "YES" : "BELOW AVG")}");
        }
        report.AppendLine();

        report.AppendLine($"  Q5: Are some identities more robust than others?");
        double bestPr = result.ByHistory.Max(h => h.PresRate);
        double worstPr = result.ByHistory.Min(h => h.PresRate);
        report.AppendLine($"    {(bestPr - worstPr > 0.10 ? "YES \u2014 Robustness varies significantly by history" : "NO \u2014 All identities show similar robustness")}");
        report.AppendLine($"    Best: {result.ByHistory.OrderByDescending(h => h.PresRate).First().History} ({bestPr:P1}), Worst: {result.ByHistory.OrderBy(h => h.PresRate).First().History} ({worstPr:P1})");
        report.AppendLine();

        // ── Section 5: Robustness Analysis ───────────────────────────
        AppendSection(report, "5. Robustness Analysis (\u03b2 dependence)");

        report.AppendLine("  \u03b2     │ Mean IdDist │ Pres. Rate │ Memory Protects?");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (b, mid, pr) in result.ByBeta)
        {
            string protects = b > 0.01 && pr > result.ByBeta.First().PresRate ? "YES" :
                              b < 0.01 ? "\u2014" : "no";
            report.AppendLine($"  {b,4:F1} │ {mid,12:F6} │ {pr,9:P1} │ {protects}");
        }

        report.AppendLine();

        double beta0pr = result.ByBeta.Where(b => b.Beta < 0.01).Select(b => b.PresRate).FirstOrDefault(0);
        double beta1pr = result.ByBeta.Where(b => b.Beta > 0.99).Select(b => b.PresRate).FirstOrDefault(0);

        report.AppendLine($"  Q6: Does memory strength \u03b2 protect identity?");
        bool betaProtects = beta1pr > beta0pr * 1.05;
        report.AppendLine($"    {(betaProtects ? $"YES \u2014 Higher \u03b2 improves preservation ({beta0pr:P1} \u2192 {beta1pr:P1})" : $"NO \u2014 \u03b2 does not significantly protect identity ({beta0pr:P1} \u2192 {beta1pr:P1})")}");
        report.AppendLine();

        // ── Section 6: Critical Threshold Search ─────────────────────
        AppendSection(report, "6. Critical Threshold Search");

        report.AppendLine($"  Critical transfer threshold: {result.CriticalTransferThreshold:F2}");
        report.AppendLine($"    (highest transfer where >50% identities preserved)");
        report.AppendLine();

        report.AppendLine($"  Q4: Is there a critical energy threshold for identity destruction?");
        if (result.CriticalTransferThreshold >= 5.0)
            report.AppendLine("    NO \u2014 No critical threshold found within tested range.");
        else if (result.CriticalTransferThreshold >= 2.0)
            report.AppendLine($"    WEAK \u2014 Threshold exists at {result.CriticalTransferThreshold:F2}\u00d7 but is far above normal range.");
        else if (result.CriticalTransferThreshold >= 0.5)
            report.AppendLine($"    YES \u2014 Critical threshold at {result.CriticalTransferThreshold:P0} transfer.");
        else
            report.AppendLine($"    YES \u2014 Low threshold ({result.CriticalTransferThreshold:P0}) \u2014 identity is fragile.");
        report.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  Transfer Classification: {result.TransferClassification}");
        report.AppendLine();

        // Q7: Can the same identity occupy multiple stable energy states?
        report.AppendLine($"  Q7: Can the same identity occupy multiple stable energy states?");
        bool q7 = result.PreservationRate >= 0.50;
        report.AppendLine($"    {(q7 ? "YES \u2014 Identity survives across multiple energy states" : "NO \u2014 Identity is tied to specific energy states")}");
        report.AppendLine($"    Evidence: {result.PreservationRate:P1} preservation rate across energy transfers");
        report.AppendLine();

        // Evidence summary
        report.AppendLine("  Evidence Summary:");
        report.AppendLine($"    Overall preservation rate:        {result.PreservationRate,8:P1}");
        report.AppendLine($"    Mean identity distance:           {result.MeanIdentityDistance,8:F6}");
        report.AppendLine($"    Mean |energy change|:             {result.MeanEnergyChange,8:F4}");
        report.AppendLine($"    Baseline drift (0% transfer):     {baselineIdDist,8:F6}");
        report.AppendLine($"    Critical threshold:               {result.CriticalTransferThreshold,8:F2}");
        report.AppendLine($"    \u03b2=0 preservation:               {beta0pr,8:P1}");
        report.AppendLine($"    \u03b2=1 preservation:               {beta1pr,8:P1}");
        report.AppendLine($"    500% injection preservation:      {extInjPres,8:P1}");
        report.AppendLine($"    -75% removal preservation:        {extRemPres,8:P1}");
        report.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Classification: {result.TransferClassification}");
        report.AppendLine();

        string primaryConclusion;
        if (result.PreservationRate >= 0.90)
        {
            primaryConclusion = "Identity is FULLY INDEPENDENT of energy transfer.";
            report.AppendLine("  C2. Energy can be injected or removed over a wide range without");
            report.AppendLine("      destroying resonance identity. Identity persists as a stable");
            report.AppendLine("      structure across energy-state transitions.");
        }
        else if (result.PreservationRate >= 0.70)
        {
            primaryConclusion = "Identity SURVIVES MODERATE energy changes.";
            report.AppendLine("  C2. Energy transfer does modify identity somewhat but the core");
            report.AppendLine("      identity signature persists through moderate energy changes.");
            report.AppendLine("      Large transfers may degrade identity at the extremes.");
        }
        else if (result.PreservationRate >= 0.40)
        {
            primaryConclusion = "Identity PARTIALLY follows energy.";
            report.AppendLine("  C2. Energy transfer correlates with identity change, but identity");
            report.AppendLine("      is not fully determined by energy. Some degree of independence");
            report.AppendLine("      exists but large transfers destroy identity.");
        }
        else
        {
            primaryConclusion = "Identity FULLY follows energy.";
            report.AppendLine("  C2. Changing energy reliably changes identity. Identity and energy");
            report.AppendLine("      are strongly coupled within a single condensate, and the");
            report.AppendLine("      statistical independence found in AT-047 does not imply");
            report.AppendLine("      within-condensate independence.");
        }

        report.AppendLine();
        report.AppendLine($"  C3. Combined with AT-047's cross-sectional independence finding,");
        report.AppendLine("      this result determines whether identity and energy are");
        report.AppendLine("      orthogonal state dimensions within a single condensate.");
        report.AppendLine();

        report.AppendLine($"  Primary conclusion: {primaryConclusion}");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-048 completed successfully.");
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
