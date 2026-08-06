using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_137_UniversalityOfInformationEvolution : ResearchTestBase
{
    public TQM_137_UniversalityOfInformationEvolution(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_137_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-137 Universality of Information Evolution");

        // ── Section 0: Assumptions ──
        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Theta is an autonomous information layer (TQM-128).");
        sb.AppendLine("  2. Darwinian evolution exists under r/c fitness (TQM-134/135/136).");
        sb.AppendLine("  3. Evolution may be model-dependent — an artifact of r/c.");
        sb.AppendLine("  4. We test whether evolution persists under alternative assumptions.");
        sb.AppendLine("  5. Assume evolution is NOT universal until demonstrated across models.");
        sb.AppendLine();

        // ── Section 1: TQM-135/136 Recap ──
        Sec(sb, "1. TQM-135/136 Recap — Darwinian Evolution in Theta");
        sb.AppendLine("  TQM-135: Selection exists — 329 extinctions, 8.6× fitness differential.");
        sb.AppendLine("  TQM-136: Fitness law w = r/c — Spearman ρ = 1.000, perfect prediction.");
        sb.AppendLine();
        sb.AppendLine("  CRITICAL QUESTION: Was this evolution an artifact of the r/c model?");
        sb.AppendLine("  If evolution disappears under different fitness definitions → artifact.");
        sb.AppendLine("  If evolution persists → universal emergent phenomenon.");
        sb.AppendLine();

        // ── Section 2: Universality Theory ──
        Sec(sb, "2. Universality Theory");
        sb.AppendLine(EvolutionUniversalityAnalyzer.UniversalityTheory());
        sb.AppendLine();

        // ── Section 3: Alternative Fitness Models ──
        Sec(sb, "3. Alternative Fitness Models");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = EvolutionUniversalityAnalyzer.Analyze();
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Total runs: {report.Metrics.TotalRuns}");
        sb.AppendLine($"  Models tested: {report.Models.Count}");
        sb.AppendLine($"  Resource regimes tested: {report.Regimes.Count}");
        sb.AppendLine($"  Seeds per configuration: 3");
        sb.AppendLine();

        sb.AppendLine("  Fitness models:");
        sb.AppendLine("  # │ Model                  │ Formula           │ Category");
        sb.AppendLine("  " + new string('─', 60));
        for (int i = 0; i < report.Models.Count; i++)
        {
            var m = report.Models[i];
            sb.AppendLine($"  {i + 1} │ {m.Name,-22} │ {m.Formula,-17} │ {m.Category}");
        }
        sb.AppendLine();

        // ── Section 4: Model × Regime Results ──
        Sec(sb, "4. Model × Regime Results Summary");
        sb.AppendLine(EvolutionUniversalityAnalyzer.ModelSummaryTable(
            report.RunResults, report.Models));
        sb.AppendLine();

        // ── Section 5: Detailed Per-Model Analysis ──
        Sec(sb, "5. Per-Model Universality Analysis");
        sb.AppendLine("  Model                  │ Runs │ Sel% │ Ext% │ Evol% │ Dominant Species");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var model in report.Models)
        {
            var runs = report.RunResults.Where(r => r.FitnessModel == model.Name).ToList();
            if (runs.Count == 0) continue;

            int sel = runs.Count(r => r.SelectionDetected);
            int ext = runs.Count(r => r.Extinctions > 0);
            int evo = runs.Count(r => r.EvolutionPersisted);
            int dom = runs.Count(r => r.EvolutionPersisted && r.DominantSpecies == "A");

            var domMap = runs.Where(r => r.EvolutionPersisted)
                .GroupBy(r => r.DominantSpecies)
                .OrderByDescending(g => g.Count())
                .Select(g => $"{g.Key}:{g.Count()}")
                .ToList();

            sb.AppendLine($"  {model.Name,-22} │ {runs.Count,4} │ {sel * 100.0 / runs.Count,4:F0}% │ {ext * 100.0 / runs.Count,4:F0}% │ {evo * 100.0 / runs.Count,4:F0}% │ {string.Join(", ", domMap)}");
        }
        sb.AppendLine();

        // ── Section 6: Universality Metrics ──
        Sec(sb, "6. Universality Metrics");
        var mtx = report.Metrics;
        sb.AppendLine($"  Total runs:              {mtx.TotalRuns}");
        sb.AppendLine($"  Runs with selection:     {mtx.RunsWithSelection} ({mtx.SelectionRobustnessIndex:P0})");
        sb.AppendLine($"  Runs with extinctions:   {mtx.RunsWithExtinctions}");
        sb.AppendLine($"  Runs with competition:   {mtx.RunsWithCompetition}");
        sb.AppendLine($"  Runs with coexistence:   {mtx.RunsWithCoexistence}");
        sb.AppendLine();
        sb.AppendLine($"  Selection Robustness Index:  {mtx.SelectionRobustnessIndex:P0}");
        sb.AppendLine($"  Evolution Persistence Score: {mtx.EvolutionPersistenceScore:P0}");
        sb.AppendLine($"  Global Rank Stability (τ):  {mtx.RankStabilityGlobal:F3}");
        sb.AppendLine();
        sb.AppendLine($"  Most universal fitness model: {mtx.MostUniversalFitnessModel}");
        sb.AppendLine($"  Most robust resource regime:  {mtx.MostRobustResourceRegime}");
        sb.AppendLine($"  Evolution is universal: {(mtx.IsEvolutionUniversal ? "YES" : "NO")}");
        sb.AppendLine();

        // ── Section 7: Species Rank Stability ──
        Sec(sb, "7. Species Rank Stability Across Models");
        sb.AppendLine("  Species │ Dominant Fraction │ Stability Assessment");
        sb.AppendLine("  " + new string('─', 50));
        foreach (var (sp, frac) in report.SpeciesRankStability.OrderByDescending(x => x.Value))
        {
            string assess = frac > 0.4 ? "Highly stable — dominates broadly"
                          : frac > 0.2 ? "Moderately stable"
                          : frac > 0.05 ? "Weak — context-dependent"
                          : "Unstable — rarely dominates";
            sb.AppendLine($"  {sp,-7} │ {frac,17:P0} │ {assess}");
        }
        sb.AppendLine();

        // ── Section 8: Hidden Invariant ──
        Sec(sb, "8. Hidden Invariant — What Survives All Modifications?");
        sb.AppendLine($"  {report.HiddenInvariant}");
        sb.AppendLine();

        // ── Section 9: Hostile Review ──
        Sec(sb, "9. Hostile Review");
        sb.AppendLine(EvolutionUniversalityAnalyzer.HostileReview(report.Metrics));
        sb.AppendLine();

        // ── Section 10: Research Questions ──
        Sec(sb, "10. Research Questions");
        sb.AppendLine(EvolutionUniversalityAnalyzer.ResearchQuestions(report.Metrics));
        sb.AppendLine();

        // ── Section 11: Classification ──
        Sec(sb, "11. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        // ── Final ──
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment TQM-137 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Evolution universality: {(report.Metrics.IsEvolutionUniversal ? "UNIVERSAL" : "MODEL-DEPENDENT")}");
        sb.AppendLine($"  Selection robustness: {report.Metrics.SelectionRobustnessIndex:P0}");
        sb.AppendLine($"  Evolution persistence: {report.Metrics.EvolutionPersistenceScore:P0}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
