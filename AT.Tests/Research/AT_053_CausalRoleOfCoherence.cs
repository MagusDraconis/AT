using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_053_CausalRoleOfCoherence : ResearchTestBase
{
    private static readonly string[] Histories = { "A", "B", "AB", "BA", "ABC", "CBA" };
    private static readonly double[] CoherenceTargets = { 1.0, 0.8, 0.6, 0.4, 0.2, 0.1, 0.0 };
    private const double Beta = 0.5;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int Seeds = 4;
    private const int BaseSeed = 530476129;

    public AT_053_CausalRoleOfCoherence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_053_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-053 Causal Role of Coherence");

        report.AppendLine("AT-053: Is Coherence the CAUSE or a Correlate of Condensate Identity?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-052 discovered Local Coherence is perfectly conserved across");
        report.AppendLine("  all transformations. This experiment tests CAUSALITY:");
        report.AppendLine("  if we DELIBERATELY DESTROY coherence, does identity collapse?");
        report.AppendLine();
        report.AppendLine("  Correlation ≠ Causation. AT-053 performs a causal intervention:");
        report.AppendLine("  disrupt coherence → measure impact on identity, memory, recovery.");
        report.AppendLine();

        // ── Section 2: Coherence Manipulation ────────────────────────
        int total = Histories.Length * CoherenceTargets.Length * Seeds;

        AppendSection(report, "2. Coherence Manipulation Design");
        report.AppendLine($"  Histories: [{string.Join(", ", Histories)}]");
        report.AppendLine($"  Coherence targets: [{string.Join(", ", CoherenceTargets)}]");
        report.AppendLine($"    (1.0 = control/no disruption, 0.0 = complete decoherence)");
        report.AppendLine($"  Disruption: Gaussian phase noise, σ = \u221a(-2·ln(R_target))");
        report.AppendLine($"  Seeds: {Seeds}, \u03b2 = {Beta}, N = {N}");
        report.AppendLine($"  Total runs: {total}");
        report.AppendLine();
        report.AppendLine("  Three-phase per run:");
        report.AppendLine("    Phase 1: Formation → Training → Baseline measurement");
        report.AppendLine("    Phase 2: Coherence disruption to target R → Post-disruption measurement");
        report.AppendLine("    Phase 3: Recovery evolution (1500 iters) → Recovery measurement");
        report.AppendLine();

        // ── Run experiments ──────────────────────────────────────────
        var bag = new ConcurrentBag<CoherenceDisruptionAnalyzer.DisruptionProfile>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int hi = idx % Histories.Length, rem = idx / Histories.Length;
            int ci = rem % CoherenceTargets.Length, si = rem / CoherenceTargets.Length;
            int combinedSeed = BaseSeed + idx * 7919;
            bag.Add(CoherenceDisruptionAnalyzer.AnalyzeDisruption(
                Histories[hi], CoherenceTargets[ci], Beta, K, Lambda, N, combinedSeed));
        });

        sw.Stop();
        var profiles = bag.ToList();
        report.AppendLine($"  Completed {profiles.Count} runs in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Aggregate ────────────────────────────────────────────────
        var agg = CoherenceDisruptionAnalyzer.Aggregate(profiles);

        // ── Section 3: Identity Impact ───────────────────────────────
        AppendSection(report, "3. Identity Impact (Q1: Can identity survive coherence destruction?)");

        report.AppendLine("  Target R │ Achieved R │ Id Pres. │ Mem Pres. │ Rec. Score │ Identity?");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (t, ip, mp, rs) in agg.ByTarget)
        {
            var sub = profiles.Where(p => Math.Abs(p.TargetR - t) < 0.001).ToList();
            if (sub.Count == 0) continue;
            double achievedR = sub.Average(p => p.DisruptedR);
            string idLabel = ip >= 0.70 ? "SURVIVES" : ip >= 0.40 ? "damaged" : "DESTROYED";
            report.AppendLine($"  {t,7:F1}  │ {achievedR,9:F4} │ {ip,7:P1} │ {mp,8:P1} │ {rs,9:P1} │ {idLabel}");
        }

        report.AppendLine();
        report.AppendLine($"  Q1 ANSWER: Identity collapse threshold R ≈ {agg.IdentityCollapseThreshold:F1}");
        report.AppendLine($"    {(agg.IdentityCollapseThreshold > 0.6 ? "Identity is FRAGILE \u2014 collapses at high coherence" : agg.IdentityCollapseThreshold > 0.3 ? "Identity is MODERATELY robust to coherence loss" : "Identity is ROBUST \u2014 survives low coherence")}");
        report.AppendLine();

        // ── Section 4: Memory Impact ─────────────────────────────────
        AppendSection(report, "4. Memory Impact (Q2: Can memory survive coherence destruction?)");

        report.AppendLine($"  Memory collapse threshold: R ≈ {agg.MemoryCollapseThreshold:F1}");
        report.AppendLine();

        report.AppendLine($"  Q2 ANSWER:");
        report.AppendLine($"    {(agg.MemoryCollapseThreshold > 0.6 ? "Memory is FRAGILE \u2014 destroyed by small coherence loss" : agg.MemoryCollapseThreshold > 0.3 ? "Memory is MODERATELY robust" : "Memory is ROBUST \u2014 survives low coherence")}");
        report.AppendLine();

        // ── Section 5: Recovery Analysis ─────────────────────────────
        AppendSection(report, "5. Recovery Analysis (Q4: Does recovery require coherence?)");

        report.AppendLine($"  Recovery threshold: R ≈ {agg.RecoveryThreshold:F1}");
        report.AppendLine();

        report.AppendLine($"  Q4 ANSWER: Recovery requires R \u2265 {agg.RecoveryThreshold:F1}");
        report.AppendLine($"    {(agg.RecoveryThreshold > 0.6 ? "Recovery is FRAGILE" : agg.RecoveryThreshold > 0.3 ? "Recovery is MODERATE" : "Recovery is ROBUST")}");
        report.AppendLine();

        // ── Section 6: Threshold Detection ───────────────────────────
        AppendSection(report, "6. Threshold Detection (Q3: Critical coherence threshold?)");

        report.AppendLine("  Property          │ Threshold R │ Interpretation");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        report.AppendLine($"  Identity survival   │ {agg.IdentityCollapseThreshold,9:F1} │ {(agg.IdentityCollapseThreshold < 0.3 ? "Very robust" : agg.IdentityCollapseThreshold < 0.5 ? "Moderate" : "Fragile")}");
        report.AppendLine($"  Memory survival     │ {agg.MemoryCollapseThreshold,9:F1} │ {(agg.MemoryCollapseThreshold < 0.3 ? "Very robust" : agg.MemoryCollapseThreshold < 0.5 ? "Moderate" : "Fragile")}");
        report.AppendLine($"  Recovery ability    │ {agg.RecoveryThreshold,9:F1} │ {(agg.RecoveryThreshold < 0.3 ? "Very robust" : agg.RecoveryThreshold < 0.5 ? "Moderate" : "Fragile")}");
        report.AppendLine();

        report.AppendLine($"  Q3 ANSWER: Critical coherence threshold is R ≈ {agg.IdentityCollapseThreshold:F1}");
        report.AppendLine($"    Above this: condensate properties survive");
        report.AppendLine($"    Below this: identity, memory, and recovery collapse");
        report.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  Causal classification: {agg.CausalClassification}");
        report.AppendLine();
        report.AppendLine("  Evidence Summary:");
        report.AppendLine($"    Mean identity preservation:      {agg.MeanIdentityPreservation,8:P1}");
        report.AppendLine($"    Mean memory preservation:        {agg.MeanMemoryPreservation,8:P1}");
        report.AppendLine($"    Mean recovery score:             {agg.MeanRecoveryScore,8:P1}");
        report.AppendLine($"    Identity collapse threshold:     R \u2248 {agg.IdentityCollapseThreshold,5:F1}");
        report.AppendLine($"    Memory collapse threshold:       R \u2248 {agg.MemoryCollapseThreshold,5:F1}");
        report.AppendLine($"    Recovery threshold:              R \u2248 {agg.RecoveryThreshold,5:F1}");
        report.AppendLine();

        // Q5: Is coherence the causal driver?
        report.AppendLine($"  Q5: Is coherence the causal driver of condensate stability?");
        if (agg.CausalClassification.StartsWith("A:"))
            report.AppendLine("    YES \u2014 Coherence is the FUNDAMENTAL CAUSE.");
        else if (agg.CausalClassification.StartsWith("B:"))
            report.AppendLine("    PARTIALLY \u2014 Coherence is necessary but not sufficient.");
        else if (agg.CausalClassification.StartsWith("C:"))
            report.AppendLine("    NO \u2014 Coherence is an emergent consequence, not the cause.");
        else
            report.AppendLine("    NO \u2014 Coherence is merely correlated with identity/memory.");
        report.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Causal classification: {agg.CausalClassification}");
        report.AppendLine($"  C2. Identity collapse at R \u2248 {agg.IdentityCollapseThreshold:F1}");
        report.AppendLine($"  C3. Memory collapse at R \u2248 {agg.MemoryCollapseThreshold:F1}");
        report.AppendLine($"  C4. Recovery collapse at R \u2248 {agg.RecoveryThreshold:F1}");
        report.AppendLine();

        if (agg.CausalClassification.StartsWith("A:"))
        {
            report.AppendLine("  C5. Coherence IS the root variable. Destroying coherence");
            report.AppendLine("      destroys identity, memory, and recovery. This confirms");
            report.AppendLine("      AT-052's finding: coherence is the conserved foundation");
            report.AppendLine("      from which all condensate properties emerge.");
        }
        else if (agg.CausalClassification.StartsWith("B:"))
        {
            report.AppendLine("  C5. Coherence is NECESSARY but not SUFFICIENT for identity.");
            report.AppendLine("      Below a threshold, identity collapses; but above it,");
            report.AppendLine("      other factors (history, memory) also determine identity.");
        }
        else
        {
            report.AppendLine("  C5. Coherence is NOT the causal root. Identity survives");
            report.AppendLine("      coherence disruption, suggesting a deeper or parallel");
            report.AppendLine("      organizing principle beyond phase alignment.");
        }

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-053 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
