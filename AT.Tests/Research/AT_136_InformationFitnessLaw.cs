using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_136_InformationFitnessLaw : ResearchTestBase
{
    public AT_136_InformationFitnessLaw(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_136_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-136 Information Fitness Law");

        // ── Section 0: Assumptions ──
        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Theta is an autonomous information layer (AT-128).");
        sb.AppendLine("  2. 4 information species exist (AT-133): A, B, C, D.");
        sb.AppendLine("  3. Selection exists under resource constraints (AT-135).");
        sb.AppendLine("  4. Fitness differences are measurable and reproducible.");
        sb.AppendLine("  5. Assume NO fitness law until a quantitative predictor is found.");
        sb.AppendLine("  6. Require |Spearman ρ| ≥ 0.7 for statistical significance (n=4).");
        sb.AppendLine();

        // ── Section 1: AT-135 Recap ──
        Sec(sb, "1. AT-135 Recap — Selection Dynamics");
        sb.AppendLine("  AT-135 completed the Darwinian triad:");
        sb.AppendLine("    ✓ Reproduction (AT-134)");
        sb.AppendLine("    ✓ Variation (AT-134)");
        sb.AppendLine("    ✓ Selection (AT-135) — 329 extinctions, 8.6× fitness diff");
        sb.AppendLine();
        sb.AppendLine("  Observed fitness hierarchy:");
        sb.AppendLine("    A (Uniform):  s = +0.009, Dominant,  most efficient (0.0138)");
        sb.AppendLine("    B (Standing): s = -0.082, Dominant, moderate efficiency (0.0058)");
        sb.AppendLine("    D (Composite): s = -0.041, Intermediate, moderate efficiency (0.0063)");
        sb.AppendLine("    C (Anti-Phase): s = -0.100, Marginal, least efficient (0.0042)");
        sb.AppendLine();
        sb.AppendLine("  BUT: what IS fitness? Is there a fundamental quantity driving selection?");
        sb.AppendLine();

        // ── Section 2: Fitness Law Theory ──
        Sec(sb, "2. Fitness Law Theory");
        sb.AppendLine(InformationFitnessLawAnalyzer.FitnessLawTheory());
        sb.AppendLine();

        // ── Section 3: Species Measurements ──
        Sec(sb, "3. Species Measurements");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = InformationFitnessLawAnalyzer.Analyze();
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine();
        sb.AppendLine("  Measured species properties:");
        sb.AppendLine("  Sp │ Energy  │ Entropy │ Coherence │ DomFreq │ ZC │ Consumpt │ Repro │ Death │ MutRobust │ Persist");
        sb.AppendLine("  " + new string('─', 95));
        foreach (var m in report.Measurements)
            sb.AppendLine($"  {m.SpeciesName,-2} │ {m.PatternEnergy,7:F2} │ {m.ShannonEntropy,7:F3} │ {m.Coherence,9:F3} │ {m.DominantFrequency,7:F3} │ {m.ZeroCrossings,2} │ {m.ResourceConsumption,8:F1} │ {m.ReproductionRate,5:F3} │ {m.DeathRate,5:F3} │ {m.MutationRobustness,9:F2} │ {m.MemoryPersistence,7:F1}");
        sb.AppendLine();

        // ── Section 4: Candidate Fitness Functions ──
        Sec(sb, "4. Candidate Fitness Functions — Correlation Analysis");
        sb.AppendLine("  Rank │ Candidate                 │ Formula        │ Pearson │ Spearman │ AICc    │ Rank Quality");
        sb.AppendLine("  " + new string('─', 90));
        int rank = 1;
        foreach (var c in report.Candidates.Take(15))
        {
            sb.AppendLine($"  {rank++,3} │ {c.Name,-25} │ {c.Formula,-15} │ {c.PearsonR,7:F3} │ {c.SpearmanRho,8:F3} │ {c.AICC,7:F1} │ {c.PredictiveRank}");
        }
        sb.AppendLine();

        // Highlight the winner.
        if (report.BestSingleVariable != null)
        {
            var best = report.BestSingleVariable;
            sb.AppendLine($"  ★ BEST SINGLE-VARIABLE: {best.Name} ({best.Formula})");
            sb.AppendLine($"    Spearman ρ = {best.SpearmanRho:F3}, Pearson r = {best.PearsonR:F3}");
            sb.AppendLine($"    Predictive rank: {best.PredictiveRank}, Significant: {(best.IsSignificant ? "YES" : "no")}");
            sb.AppendLine();
        }

        // ── Section 5: Multivariate Analysis ──
        Sec(sb, "5. Multivariate Fitness Models");
        if (report.BestMultivariate != null)
        {
            var mv = report.BestMultivariate;
            sb.AppendLine($"  Best multivariate model:");
            sb.AppendLine($"    Formula: {mv.Formula}");
            sb.AppendLine($"    Variables: {string.Join(", ", mv.Variables)}");
            sb.AppendLine($"    R² = {mv.R2:F4}, Adjusted R² = {mv.AdjustedR2:F4}");
            sb.AppendLine($"    AICc = {mv.AICC:F2}");
            sb.AppendLine($"    Parameters: {mv.Variables.Length} + intercept");
            sb.AppendLine();

            if (mv.Variables.Length >= 3)
                sb.AppendLine("  ⚠ WARNING: ≥3 parameters with n=4 → likely overfitting. Prefer simpler model.");
            else
                sb.AppendLine("  ✓ Acceptable: ≤2 parameters with n=4.");
            sb.AppendLine();
        }
        else
        {
            sb.AppendLine("  No valid multivariate model found (likely overfitting prevented).");
            sb.AppendLine();
        }

        // ── Section 6: Fitness Landscape ──
        Sec(sb, "6. Fitness Landscape");
        var landscape = report.Landscape;
        sb.AppendLine($"  Variables: {landscape.VariableX} vs {landscape.VariableY}");
        sb.AppendLine($"  Landscape shape: {landscape.LandscapeShape}");
        sb.AppendLine($"  Optimal point: ({landscape.VariableX} = {landscape.OptimalX:F4},");
        sb.AppendLine($"                   {landscape.VariableY} = {landscape.OptimalY:F4})");
        sb.AppendLine($"  Max fitness at optimum: {landscape.MaxFitness:F6}");
        sb.AppendLine();

        sb.AppendLine("  Species positions on landscape:");
        sb.AppendLine("  Species │ Efficiency (r/c) │ Coherence │ Observed s │ Predicted?");
        sb.AppendLine("  " + new string('─', 60));
        foreach (var m in report.Measurements)
        {
            double efficiency = m.ReproductionRate / Math.Max(m.ResourceConsumption, 0.01);
            double predicted = report.BestSingleVariable?.SpeciesValues.GetValueOrDefault(m.SpeciesName, 0) ?? 0;
            sb.AppendLine($"  {m.SpeciesName,-7} │ {efficiency,17:F5} │ {m.Coherence,9:F3} │ {m.ObservedSelectionCoefficient,10:F4} │ {predicted,9:F4}");
        }
        sb.AppendLine();

        // ── Section 7: Prediction Validation ──
        Sec(sb, "7. Prediction Validation Against AT-135");
        sb.AppendLine($"  Predictive accuracy: {report.PredictionAccuracy:P0}");
        sb.AppendLine($"  Predictive power demonstrated: {(report.PredictivePowerDemonstrated ? "YES" : "NO")}");
        sb.AppendLine();

        if (report.BestSingleVariable != null)
        {
            sb.AppendLine("  Predicted vs observed rankings:");
            var mDict = report.Measurements.ToDictionary(m => m.SpeciesName);
            var obsRank = report.Measurements
                .OrderByDescending(m => m.ObservedSelectionCoefficient)
                .Select(m => m.SpeciesName).ToList();
            var predRank = report.BestSingleVariable.SpeciesValues
                .OrderByDescending(kv => kv.Value)
                .Select(kv => kv.Key).ToList();

            sb.AppendLine($"    Observed:  {string.Join(" > ", obsRank)}");
            sb.AppendLine($"    Predicted: {string.Join(" > ", predRank)}");

            int matches = obsRank.Zip(predRank, (o, p) => o == p ? 1 : 0).Sum();
            sb.AppendLine($"    Exact rank matches: {matches}/4");
        }
        sb.AppendLine();

        // ── Section 8: Evolutionary Law Derivation ──
        Sec(sb, "8. Evolutionary Law Derivation");
        sb.AppendLine("  Attempting to derive dN_i/dt from the fitness law.");
        sb.AppendLine();
        sb.AppendLine("  If w = F(species_properties) is universal, then:");
        sb.AppendLine("    dN_i/dt = r_i · N_i · f(w_i, resource_pressure)");
        sb.AppendLine();
        sb.AppendLine("  The evolutionary dynamics are governed by:");
        sb.AppendLine("    1. Intrinsic growth (r_i)");
        sb.AppendLine("    2. Fitness (w_i = F(properties))");
        sb.AppendLine("    3. Resource pressure (P = Σ(c_i·N_i) / K)");
        sb.AppendLine();
        sb.AppendLine($"  Best fitness function: {report.BestFormula}");
        sb.AppendLine();

        // ── Section 9: Hostile Review ──
        Sec(sb, "9. Hostile Review");
        sb.AppendLine(InformationFitnessLawAnalyzer.HostileReview(report));
        sb.AppendLine();

        // ── Section 10: Research Questions ──
        Sec(sb, "10. Research Questions");
        sb.AppendLine(InformationFitnessLawAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        // ── Section 11: Classification ──
        Sec(sb, "11. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        // ── Final ──
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-136 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Fitness law: {(report.SingleVariableFound ? "DISCOVERED" : "NOT FOUND")}");
        sb.AppendLine($"  Best predictor: {report.BestFormula}");
        sb.AppendLine($"  Predictive accuracy: {report.PredictionAccuracy:P0}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
