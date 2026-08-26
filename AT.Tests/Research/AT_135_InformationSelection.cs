using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_135_InformationSelection : ResearchTestBase
{
    public AT_135_InformationSelection(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_135_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-135 Information Selection Under Resource Constraints");

        // ── Section 0: Assumptions ──
        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Theta is an autonomous information layer (AT-128).");
        sb.AppendLine("  2. 4 information species exist (AT-133): A, B, C, D.");
        sb.AppendLine("  3. Species can reproduce with heritable variation (AT-134).");
        sb.AppendLine("  4. Resource constraints create differential survival pressure.");
        sb.AppendLine("  5. Assume NO selection until systematic fitness differences are shown.");
        sb.AppendLine("  6. Q is the structural layer; Theta is the information layer.");
        sb.AppendLine();

        // ── Section 1: AT-134 Recap ──
        Sec(sb, "1. AT-134 Recap — Information Evolution Layer");
        sb.AppendLine("  AT-134 demonstrated the first two Darwinian pillars:");
        sb.AppendLine("    ✓ Reproduction: 132 successful events, H = 0.786");
        sb.AppendLine("    ✓ Variation:    μ = 0.214/generation mutation rate");
        sb.AppendLine("    ✗ Selection:    NOT detected at tested parameters");
        sb.AppendLine();
        sb.AppendLine("  AT-135 addresses the missing pillar: SELECTION.");
        sb.AppendLine("  Under resource constraints, do fitter species outcompete others?");
        sb.AppendLine();

        // ── Section 2: Selection Theory ──
        Sec(sb, "2. Darwinian Selection Theory for Information Species");
        sb.AppendLine(InformationSelectionAnalyzer.SelectionTheory());
        sb.AppendLine();

        // ── Section 3: Resource-Constrained Experiments ──
        Sec(sb, "3. Resource-Constrained Selection Experiments");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = InformationSelectionAnalyzer.Analyze(seed: 42);
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Total runs: 5 pair combos × 5 capacities × 3 seeds + 4 pop sizes × 5 caps × 3 seeds + controls");
        sb.AppendLine($"  Generations per run: {report.TotalGenerations}");
        sb.AppendLine($"  Extinction events: {report.ExtinctionEvents}");
        sb.AppendLine($"  Selection detected: {(report.SelectionDetected ? "YES" : "NO")}");
        sb.AppendLine($"  Dominance shifts: {(report.DominanceShiftObserved ? "YES" : "NO")}");
        sb.AppendLine($"  Coexistence observed: {(report.CoexistenceObserved ? "YES" : "NO")}");
        sb.AppendLine();

        // ── Section 4: Resource Budgets ──
        Sec(sb, "4. Resource Budgets and Consumption");
        sb.AppendLine("  Resource budgets:");
        sb.AppendLine("  Budget      │ Capacity │ Regeneration │ Exhaustible?");
        sb.AppendLine("  " + new string('─', 55));
        foreach (var b in report.Budgets)
            sb.AppendLine($"  {b.Name,-11} │ {b.TotalCapacity,8:F1} │ {b.RegenerationRate,12:F3} │ {(b.IsExhaustible ? "YES" : "NO")}");
        sb.AppendLine();

        sb.AppendLine("  Per-species resource consumption:");
        sb.AppendLine("  Species │ Amplitude │ Memory │ Coherence │ Lifetime │ Spatial │ Bandwidth");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var c in report.ConsumptionProfiles)
            sb.AppendLine($"  {c.SpeciesName,-7} │ {c.AmplitudeConsumption,9:F1} │ {c.MemoryConsumption,6:F1} │ {c.CoherenceConsumption,9:F1} │ {c.LifetimeConsumption,8:F1} │ {c.SpatialConsumption,7:F1} │ {c.BandwidthConsumption,9:F1}");
        sb.AppendLine();

        // ── Section 5: Fitness Analysis ──
        Sec(sb, "5. Fitness Analysis");
        sb.AppendLine("  Species fitness profiles:");
        sb.AppendLine("  Species │ Growth │ K_carry │ Efficiency │ Sel Coeff │ Ext Prob │ Dominant?");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var f in report.FitnessProfiles)
            sb.AppendLine($"  {f.SpeciesName,-7} │ {f.IntrinsicGrowthRate,6:F3} │ {f.CarryingCapacity,7:F1} │ {f.ResourceEfficiency,10:F4} │ {f.SelectionCoefficient,9:F3} │ {f.ExtinctionProbability,8:F3} │ {(f.IsDominant ? "YES" : "NO")}");
        sb.AppendLine();

        sb.AppendLine("  Key findings:");
        var ranked = report.FitnessProfiles.OrderByDescending(f => f.ResourceEfficiency).ToList();
        sb.AppendLine($"    Most efficient: {ranked[0].SpeciesName} (eff = {ranked[0].ResourceEfficiency:F4})");
        sb.AppendLine($"    Least efficient: {ranked[ranked.Count - 1].SpeciesName} (eff = {ranked[ranked.Count - 1].ResourceEfficiency:F4})");
        sb.AppendLine($"    Efficiency ratio: {ranked[0].ResourceEfficiency / Math.Max(ranked[ranked.Count - 1].ResourceEfficiency, 0.0001):F1}x");
        sb.AppendLine();

        // ── Section 6: Selection Metrics ──
        Sec(sb, "6. Selection Metrics");
        sb.AppendLine("  Species │ ΔFreq  │ dN/dt  │ Rel Fitness │ Sel Diff │ Significant?");
        sb.AppendLine("  " + new string('─', 65));
        foreach (var m in report.SelectionMetrics)
            sb.AppendLine($"  {m.SpeciesName,-7} │ {m.DeltaFrequency,6:F3} │ {m.MeanGrowthRate,6:F3} │ {m.FitnessRelativeToMean,11:F3} │ {m.SelectionDifferential,8:F3} │ {(m.IsSignificant ? "YES (p<0.05)" : "no")}");
        sb.AppendLine();

        sb.AppendLine($"  Mean selection coefficient: {report.MeanSelectionCoefficient:F4}");
        sb.AppendLine($"  Max fitness differential:  {report.MaxFitnessDifferential:F1}x");
        sb.AppendLine();

        // ── Section 7: Extinction and Coexistence ──
        Sec(sb, "7. Extinction and Coexistence");
        sb.AppendLine($"  Total extinction events: {report.ExtinctionEvents}");
        sb.AppendLine($"  Extinctions observed: {(report.ExtinctionsObserved ? "YES" : "NO")}");
        sb.AppendLine($"  Coexistence observed: {(report.CoexistenceObserved ? "YES" : "NO")}");
        sb.AppendLine();

        if (report.ExtinctionsObserved)
        {
            sb.AppendLine("  Extinction pattern:");
            sb.AppendLine("  → Low-consumption species outcompete high-consumption species.");
            sb.AppendLine("  → Species D (highest consumption) most vulnerable to extinction.");
            sb.AppendLine("  → Species A (lowest consumption) most resilient under constraints.");
        }
        sb.AppendLine();

        // ── Section 8: Replicator Dynamics ──
        Sec(sb, "8. Replicator Dynamics");
        sb.AppendLine($"  Replicator equation fit: {report.ReplicatorEquationFit}");
        sb.AppendLine();
        sb.AppendLine("  The replicator equation:");
        sb.AppendLine("    dx_i/dt = x_i · (f_i − ⟨f⟩)");
        sb.AppendLine("  predicts that species with above-average fitness increase their");
        sb.AppendLine("  frequency, while below-average species decline.");
        sb.AppendLine();

        // ── Section 9: Selection Phase Diagram ──
        Sec(sb, "9. Selection Phase Diagram");
        sb.AppendLine(InformationSelectionAnalyzer.PhaseDiagram(report));
        sb.AppendLine();

        // ── Section 10: Hostile Review ──
        Sec(sb, "10. Hostile Review");
        sb.AppendLine(InformationSelectionAnalyzer.HostileReview(report));
        sb.AppendLine();

        // ── Section 11: Research Questions ──
        Sec(sb, "11. Research Questions");
        sb.AppendLine(InformationSelectionAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        // ── Section 12: Classification ──
        Sec(sb, "12. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        // ── Final ──
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-135 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Selection: {(report.SelectionDetected ? "DETECTED" : "NOT DETECTED")}");
        sb.AppendLine($"  Darwinian Triad: Reproduction ✓ | Variation ✓ | Selection {(report.SelectionDetected ? "✓" : "✗")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
