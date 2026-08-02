using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_052_ConservedQuantityDiscovery : ResearchTestBase
{
    private static readonly string[] Histories = { "A", "B", "AB", "BA", "ABC", "CBA" };
    private const double Beta = 0.5;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int Seeds = 4;
    private const int BaseSeed = 520183647;

    public TQM_052_ConservedQuantityDiscovery(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_052_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-052 Conserved Quantity Discovery");

        report.AppendLine("TQM-052: Is There a Deeper Invariant Beneath Identity and Energy?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-047 through TQM-051 established identity as a multi-faceted");
        report.AppendLine("  property with stability, latency, exclusion, and resolution limits.");
        report.AppendLine("  This experiment searches for quantities that remain APPROXIMATELY");
        report.AppendLine("  INVARIANT across ALL known TQM transformations.");
        report.AppendLine();
        report.AppendLine("  Hypothesis: A deeper conserved quantity exists beneath identity,");
        report.AppendLine("  energy, memory, and coherence \u2014 perhaps their product or ratio.");
        report.AppendLine();

        // ── Section 2: Candidate Quantities ──────────────────────────
        int totalRuns = Histories.Length * Seeds;

        AppendSection(report, "2. Candidate Quantities & Experimental Setup");
        report.AppendLine($"  Histories: [{string.Join(", ", Histories)}]");
        report.AppendLine($"  Seeds: {Seeds} per history, Total: {totalRuns} condensates");
        report.AppendLine($"  \u03b2 = {Beta}, K = {K}, N = {N}");
        report.AppendLine();
        report.AppendLine("  Transformations (applied sequentially to each condensate):");
        report.AppendLine("    1. Evolve (control \u2014 natural drift)");
        report.AppendLine("    2. Phase Noise (\u00b10.5 rad perturbation)");
        report.AppendLine("    3. Energy Injection (\u00d71.5 frequency scale)");
        report.AppendLine("    4. Energy Removal (\u00d70.5 frequency scale)");
        report.AppendLine("    5. Collapse + Recovery (\u00d73.0 then restore)");
        report.AppendLine("    6. Memory Disruption (strong phase noise)");
        report.AppendLine();
        report.AppendLine("  Candidate invariants (12 total):");
        report.AppendLine("    Q1: Energy \u00d7 Coherence  (R\u00b2 \u00d7 Freq)");
        report.AppendLine("    Q2: Energy \u00d7 Memory     (R \u00d7 Freq \u00d7 Mem)");
        report.AppendLine("    Q3: Coherence \u00d7 Memory  (R \u00d7 Mem)");
        report.AppendLine("    Q4: R (global coherence)");
        report.AppendLine("    Q5: Phase Variance");
        report.AppendLine("    Q6: Mean Phase");
        report.AppendLine("    Q7: Local Coherence");
        report.AppendLine("    Q8: Energy / PhaseVar");
        report.AppendLine("    Q9: R \u00d7 LocalCoherence");
        report.AppendLine("    Q10: Freq \u00d7 Memory");
        report.AppendLine("    Q11: R / PhaseVar");
        report.AppendLine("    Q12: E\u00d7M\u00d7Coh (full product)");
        report.AppendLine();

        // ── Run experiments ──────────────────────────────────────────
        var bag = new ConcurrentBag<EmergentConservationAnalyzer.TransformStep>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, totalRuns, idx =>
        {
            int hi = idx / Seeds, si = idx % Seeds;
            int combinedSeed = BaseSeed + idx * 7919;
            var steps = EmergentConservationAnalyzer.RunSequence(
                Histories[hi], Beta, K, Lambda, N, combinedSeed);
            foreach (var s in steps) bag.Add(s);
        });

        sw.Stop();
        var allSteps = bag.ToList();
        int transformsPerRun = allSteps.Count / totalRuns;
        report.AppendLine($"  Completed {totalRuns} runs × {transformsPerRun} transforms = {allSteps.Count} measurements in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Compute drifts ───────────────────────────────────────────
        var ranked = EmergentConservationAnalyzer.ComputeDrifts(allSteps);
        var agg = EmergentConservationAnalyzer.Aggregate(ranked);

        // ── Section 3: Transformation Analysis ───────────────────────
        AppendSection(report, "3. Invariance Ranking");

        report.AppendLine("  Rank │ Candidate              │ Mean Drift │ Max Drift │ \u00b1Std    │ Class");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        for (int i = 0; i < ranked.Count; i++)
        {
            var r = ranked[i];
            string cls = r.MeanRelativeDrift < 0.05 ? "CONSERVED" :
                         r.MeanRelativeDrift < 0.15 ? "STRONG" :
                         r.MeanRelativeDrift < 0.30 ? "WEAK" : "\u2014";
            string name = r.Name.Length > 22 ? r.Name[..22] : r.Name;
            report.AppendLine($"  {i + 1,3}  │ {name,-22} │ {r.MeanRelativeDrift,9:P1} │ {r.MaxRelativeDrift,8:P1} │ {r.DriftStdDev,6:P1} │ {cls}");
        }
        report.AppendLine();

        // ── Section 4: Transformation Breakdown ──────────────────────
        AppendSection(report, "4. Per-Transformation Drift (Top 5 Candidates)");

        var top5 = ranked.Take(5).ToList();
        var transformNames = new[] { "Evolve", "PhaseNoise", "EnergyInject", "EnergyRemove", "CollapseRecover", "MemoryDisrupt" };

        report.Append("  Candidate          │");
        foreach (var tn in transformNames) report.Append($"{tn,14}");
        report.AppendLine();
        report.Append("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        report.AppendLine(new string('\u2500', 21 + transformNames.Length * 14));

        foreach (var r in top5)
        {
            string name = r.Name.Length > 18 ? r.Name[..18] : r.Name;
            report.Append($"  {name,-18} │");
            foreach (var tn in transformNames)
            {
                var key = Enum.Parse<EmergentConservationAnalyzer.TransformType>(tn);
                double d = r.DriftByTransform.GetValueOrDefault(key, 0);
                report.Append($"{d,13:P1}");
            }
            report.AppendLine();
        }
        report.AppendLine();

        // ── Section 5: Research Questions ────────────────────────────
        AppendSection(report, "5. Research Questions");

        var best = ranked.First();

        report.AppendLine($"  Q1: Does any quantity remain approximately invariant?");
        report.AppendLine($"    {(best.MeanRelativeDrift < 0.15 ? "YES \u2014" : "NO \u2014")} Best candidate: {best.Name} (drift: {best.MeanRelativeDrift:P1})");
        report.AppendLine();

        report.AppendLine($"  Q2: Which quantity changes least under all transformations?");
        report.AppendLine($"    {best.Name} at {best.MeanRelativeDrift:P1} mean drift");
        if (ranked.Count > 1)
            report.AppendLine($"    Runner-up: {ranked[1].Name} at {ranked[1].MeanRelativeDrift:P1}");
        report.AppendLine();

        // Q3: Does invariant survive collapse+recovery?
        var collapseKey = EmergentConservationAnalyzer.TransformType.CollapseRecover;
        double collapseDrift = best.DriftByTransform.GetValueOrDefault(collapseKey, 1);
        report.AppendLine($"  Q3: Does best invariant survive identity collapse+recovery?");
        report.AppendLine($"    {(collapseDrift < 0.30 ? $"YES \u2014 Drift under collapse: {collapseDrift:P1}" : $"NO \u2014 Drift under collapse: {collapseDrift:P1}")}");
        report.AppendLine();

        // Q4: Does invariant survive energy excursions?
        var injKey = EmergentConservationAnalyzer.TransformType.EnergyInject;
        var remKey = EmergentConservationAnalyzer.TransformType.EnergyRemove;
        double injDrift = best.DriftByTransform.GetValueOrDefault(injKey, 1);
        double remDrift = best.DriftByTransform.GetValueOrDefault(remKey, 1);
        report.AppendLine($"  Q4: Does best invariant survive energy excursions?");
        report.AppendLine($"    Injection drift: {injDrift:P1}, Removal drift: {remDrift:P1}");
        report.AppendLine($"    {(Math.Max(injDrift, remDrift) < 0.30 ? "YES \u2014 Energies stable" : "NO \u2014 Energy-sensitive")}");
        report.AppendLine();

        // Q5: Does invariant survive memory disruption?
        var memKey = EmergentConservationAnalyzer.TransformType.MemoryDisrupt;
        double memDrift = best.DriftByTransform.GetValueOrDefault(memKey, 1);
        report.AppendLine($"  Q5: Does best invariant survive memory disruption?");
        report.AppendLine($"    {(memDrift < 0.30 ? $"YES \u2014 Memory drift: {memDrift:P1}" : $"NO \u2014 Memory drift: {memDrift:P1}")}");
        report.AppendLine();

        // Q6: Deeper state variable?
        report.AppendLine($"  Q6: Is there a deeper state variable beneath Identity and Energy?");
        string clsLabel = agg.ConservationClassification;
        if (clsLabel.StartsWith("D:"))
            report.AppendLine("    YES \u2014 An emergent conservation law exists (Class D).");
        else if (clsLabel.StartsWith("C:"))
            report.AppendLine("    POSSIBLY \u2014 A strong invariant exists (Class C) that may be fundamental.");
        else if (clsLabel.StartsWith("B:"))
            report.AppendLine("    WEAKLY \u2014 A weak invariant exists but is not strongly conserved.");
        else
            report.AppendLine("    NO \u2014 No invariant found; identity and energy may be the deepest observables.");
        report.AppendLine();

        // ── Section 6: Full Ranking Table ────────────────────────────
        AppendSection(report, "6. Complete Invariant Rankings");

        report.AppendLine("  Full drift table (all 12 candidates):");
        report.AppendLine("  Candidate                              │ Mean    │ Max     │ STDev   │ Verdict");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var r in ranked)
        {
            string verdict = r.MeanRelativeDrift < 0.05 ? "\u2605 CONSERVATION LAW" :
                             r.MeanRelativeDrift < 0.15 ? "\u25C6 Strong invariant" :
                             r.MeanRelativeDrift < 0.30 ? "\u25CB Weak invariant" : "\u2014 Not conserved";
            string name = r.Name.Length > 37 ? r.Name[..37] : r.Name;
            report.AppendLine($"  {name,-37} │ {r.MeanRelativeDrift,7:P1} │ {r.MaxRelativeDrift,7:P1} │ {r.DriftStdDev,7:P1} │ {verdict}");
        }

        report.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  Conservation classification: {agg.ConservationClassification}");
        report.AppendLine($"  Best invariant: {agg.BestInvariant}");
        report.AppendLine($"  Best mean drift: {agg.BestMeanDrift:P1}");
        report.AppendLine();

        string interpretation;
        if (agg.ConservationClassification.StartsWith("D:"))
            interpretation = "An EMERGENT CONSERVATION LAW has been discovered. " +
                "A compound quantity survives all tested transformations with <5% drift. " +
                "This suggests a deeper organizing principle beneath the observable state variables.";
        else if (agg.ConservationClassification.StartsWith("C:"))
            interpretation = "A STRONG INVARIANT exists but is not perfectly conserved. " +
                "The best candidate shows <15% drift across transformations, suggesting " +
                "an approximate conservation law that may become exact in a limit.";
        else if (agg.ConservationClassification.StartsWith("B:"))
            interpretation = "A WEAK INVARIANT exists but significant drift occurs. " +
                "Identity and energy may be the deepest observable state variables, " +
                "with no deeper conserved quantity beneath them.";
        else
            interpretation = "NO INVARIANT FOUND. Identity and energy appear to be " +
                "the fundamental state variables of a resonance condensate, with " +
                "no deeper conserved quantity beneath.";

        report.AppendLine($"  {interpretation}");
        report.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Classification: {agg.ConservationClassification}");
        report.AppendLine($"  C2. Best invariant: {agg.BestInvariant} ({agg.BestMeanDrift:P1} mean drift)");
        report.AppendLine();

        if (agg.ConservationClassification.StartsWith("D:") || agg.ConservationClassification.StartsWith("C:"))
        {
            report.AppendLine("  C3. A deeper structure exists beneath the observable");
            report.AppendLine("      state variables. The discovered invariant provides");
            report.AppendLine("      a new organizing principle for condensate dynamics.");
            report.AppendLine("  C4. This invariant may be the 'ground state' of the");
            report.AppendLine("      condensate \u2014 the quantity that nature conserves");
            report.AppendLine("      while identity, energy, and coherence fluctuate.");
        }
        else
        {
            report.AppendLine("  C3. Identity and energy are the deepest observable");
            report.AppendLine("      state variables. No compound invariant was found");
            report.AppendLine("      that survives ALL transformations.");
            report.AppendLine("  C4. The condensate's state space may be fully described");
            report.AppendLine("      by identity and energy without a deeper conserved quantity.");
        }

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-052 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
