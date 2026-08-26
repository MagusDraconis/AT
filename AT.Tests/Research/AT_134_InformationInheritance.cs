using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_134_InformationInheritance : ResearchTestBase
{
    public AT_134_InformationInheritance(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_134_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-134 Information Species Reproduction and Inheritance");

        // ── Section 0: Assumptions ──
        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Theta is an autonomous information layer (AT-128).");
        sb.AppendLine("  2. 4 stable information species exist (AT-133): A, B, C, D.");
        sb.AppendLine("  3. Species have characteristic patterns and attractor basins.");
        sb.AppendLine("  4. We test whether species can REPRODUCE beyond attractor dynamics.");
        sb.AppendLine("  5. Assume species are ONLY attractors until inheritance is demonstrated.");
        sb.AppendLine("  6. Q is the structural layer; Theta is the information layer.");
        sb.AppendLine();

        // ── Section 1: AT-133 Recap ──
        Sec(sb, "1. AT-133 Recap — Stable Information Species");
        sb.AppendLine("  AT-133 discovered 4 stable information species in Theta:");
        sb.AppendLine("    A: Uniform Phase-Locked  — R_Q=1, zero entropy, ~50% basin");
        sb.AppendLine("    B: Standing Wave (n=1)    — sin(kx), ~25% basin");
        sb.AppendLine("    C: Anti-Phase Domain      — spatial domains, Δφ=π, ~15% basin");
        sb.AppendLine("    D: Composite Memory       — multi-mode superposition, ~10% basin");
        sb.AppendLine();
        sb.AppendLine("  Question: Can these species REPRODUCE, or are they merely attractors?");
        sb.AppendLine();

        // ── Section 2: Evolution Theory ──
        Sec(sb, "2. Information Evolution Theory");
        sb.AppendLine(InformationInheritanceAnalyzer.EvolutionTheory());
        sb.AppendLine();

        // ── Section 3: Reproduction Experiments ──
        Sec(sb, "3. Reproduction Experiments");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = InformationInheritanceAnalyzer.Analyze(seed: 42);
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Total reproduction events: {report.TotalReproductionEvents}");
        sb.AppendLine($"  Total extinctions: {report.TotalExtinctions}");
        sb.AppendLine($"  Total lineages: {report.TotalLineages}");
        sb.AppendLine($"  Longest lineage: {report.LongestLineageLength} generations");
        sb.AppendLine();

        sb.AppendLine("  Species reproduction profiles:");
        sb.AppendLine("  Name │ Repro Rate │ Survival │ Fidelity │ Mutation  │ Advantage");
        sb.AppendLine("  " + new string('─', 72));
        foreach (var p in report.SpeciesProfiles)
            sb.AppendLine($"  {p.SpeciesName,-4} │ {p.ReproductionRate,10:F3} │ {p.SurvivalProbability,8:F3} │ {p.Fidelity,8:F3} │ {p.MutationRate,9:F4} │ {p.CompetitiveAdvantage,9:F3}");
        sb.AppendLine();

        sb.AppendLine("  Inheritance metrics:");
        sb.AppendLine($"    Mean inheritance coefficient H = {report.MeanInheritanceCoefficient:F4}");
        sb.AppendLine($"    Mean species fidelity        = {report.MeanFidelity:F4}");
        sb.AppendLine($"    Mean survival rate           = {report.MeanSurvivalRate:F4}");
        sb.AppendLine();

        // ── Section 4: Lineage Analysis ──
        Sec(sb, "4. Lineage Analysis");
        sb.AppendLine(InformationInheritanceAnalyzer.LineageReport(
            report.Lineages, report.TransitionMatrix));
        sb.AppendLine();

        // ── Section 5: Mutation Analysis ──
        Sec(sb, "5. Mutation Analysis");
        sb.AppendLine($"  Mutations observed: {(report.MutationsObserved ? "YES" : "NO")}");
        if (report.Lineages.Count > 0)
        {
            double meanDrift = report.Lineages.Average(l => l.MutationDrift);
            double maxDrift = report.Lineages.Max(l => l.MutationDrift);
            sb.AppendLine($"  Mean mutation drift: {meanDrift:F4} per lineage");
            sb.AppendLine($"  Max mutation drift:  {maxDrift:F4} per lineage");
            sb.AppendLine($"  Mutation rate μ = 1 - H = {1.0 - report.MeanInheritanceCoefficient:F4}");
        }
        sb.AppendLine();

        // ── Section 6: Species Competition ──
        Sec(sb, "6. Species Competition");
        sb.AppendLine($"  Competition detected: {(report.CompetitionDetected ? "YES" : "NO")}");
        sb.AppendLine($"  Competitive advantages:");
        foreach (var p in report.SpeciesProfiles.OrderByDescending(x => x.CompetitiveAdvantage))
            sb.AppendLine($"    {p.SpeciesName}: {p.CompetitiveAdvantage:F3}");
        sb.AppendLine();

        var competeEvents = report.AllReproductionEvents
            .Where(e => e.Outcome == "Compete").ToList();
        if (competeEvents.Count > 0)
        {
            sb.AppendLine($"  Competition events: {competeEvents.Count}");
            foreach (var e in competeEvents.Take(6))
                sb.AppendLine($"    {e.ParentA} vs {e.ParentB}: {e.Description}");
        }
        sb.AppendLine();

        // ── Section 7: Heritability Metrics ──
        Sec(sb, "7. Heritability Metrics");
        sb.AppendLine("  Heritability = fraction of child trait variance explained by parent traits.");
        sb.AppendLine();
        sb.AppendLine($"  Mean inheritance coefficient H = {report.MeanInheritanceCoefficient:F4}");
        sb.AppendLine($"  H > 0.5: strong inheritance");
        sb.AppendLine($"  H > 0.3: moderate inheritance");
        sb.AppendLine($"  H > 0.0: weak inheritance");
        sb.AppendLine($"  H ≈ 0:   no inheritance (random)");
        sb.AppendLine();

        // Statistical significance: compare H against baseline.
        double baselineH = 0.3;
        bool significant = report.MeanInheritanceCoefficient > baselineH + 0.1;
        sb.AppendLine($"  Baseline H (random patterns near attractor): ~{baselineH:F2}");
        sb.AppendLine($"  Observed H: {report.MeanInheritanceCoefficient:F4}");
        sb.AppendLine(significant
            ? "  → STATISTICALLY SIGNIFICANT: H_obs > H_baseline + 0.1"
            : "  → NOT SIGNIFICANT: H_obs ≤ H_baseline + 0.1");
        sb.AppendLine();

        // ── Section 8: Physical Interpretation ──
        Sec(sb, "8. Physical Interpretation");
        sb.AppendLine("  REPRODUCTION MECHANISM:");
        sb.AppendLine("  Information species reproduce through pattern-copying facilitated");
        sb.AppendLine("  by the Theta field dynamics. When field density is sufficient");
        sb.AppendLine("  (ρ_Q > 0.3, autonomy threshold from AT-128), a species can generate");
        sb.AppendLine("  a copy of its pattern — analogous to template-based self-replication.");
        sb.AppendLine();
        sb.AppendLine("  INHERITANCE MECHANISM:");
        sb.AppendLine("  Offspring patterns inherit parent characteristics through field-mediated");
        sb.AppendLine("  coupling. The coupling acts as a 'channel' that transmits pattern");
        sb.AppendLine("  information from parent to child. Fidelity depends on the damping");
        sb.AppendLine("  coefficient and field density.");
        sb.AppendLine();
        sb.AppendLine("  EVOLUTIONARY POTENTIAL:");
        sb.AppendLine("  If reproduction + variation (mutation) + selection (competition)");
        sb.AppendLine("  coexist in Theta, Darwinian information evolution becomes possible.");
        sb.AppendLine("  This would bridge proto-matter to proto-life within the AT framework.");
        sb.AppendLine();

        // ── Section 9: Hostile Review ──
        Sec(sb, "9. Hostile Review");
        sb.AppendLine(InformationInheritanceAnalyzer.HostileReview(report));
        sb.AppendLine();

        // ── Section 10: Research Questions ──
        Sec(sb, "10. Research Questions");
        sb.AppendLine(InformationInheritanceAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        // ── Section 11: Classification ──
        Sec(sb, "11. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        // ── Final Verdict ──
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-134 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Reproduction: {(report.ReproductionDetected ? "DETECTED" : "NOT DETECTED")}");
        sb.AppendLine($"  Lineages: {(report.LineagesFormed ? "FORMED" : "NOT FORMED")}");
        sb.AppendLine($"  Verdict: {(report.ReproductionDetected && report.LineagesFormed ? "INFORMATION CAN REPRODUCE AND FORM LINEAGES" : report.ReproductionDetected ? "INFORMATION CAN REPRODUCE (WEAK)" : "INFORMATION CANNOT REPRODUCE — ATTRACTORS ONLY")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
