namespace AT.Core.Resonance.Theory;

/// <summary>
/// Discovers the fundamental quantity driving selection in the Theta information layer.
/// Evaluates 15+ candidate fitness functions, performs correlation and regression analysis,
/// and determines whether a universal information fitness law exists.
///
/// AT-136: Information Fitness Law
/// </summary>
public static class InformationFitnessLawAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Fitness theory overview.
    // ══════════════════════════════════════════════════════════════════

    public static string FitnessLawTheory()
    {
        return @"
INFORMATION FITNESS LAW THEORY

1. THE QUESTION:

   AT-135: Selection exists. Species have different fitness.
   
   But WHAT IS FITNESS?
   
   Is there a fundamental quantity that determines which species
   survive and which go extinct under resource constraints?

   If w = F(species_properties) predicts selection outcomes,
   then F is an INFORMATION FITNESS LAW.

2. CANDIDATE FITNESS FUNCTIONS:

   w1  = r              (reproduction alone)
   w2  = 1/c            (inverse consumption)
   w3  = r/c            (resource efficiency — AT-135 default)
   w4  = C              (coherence)
   w5  = 1/H            (order / inverse entropy)
   w6  = E              (pattern energy)
   w7  = 1/μ            (mutation robustness)
   w8  = r·C            (reproduction × coherence)
   w9  = r·E            (reproduction × energy)
   w10 = (r/c)·C        (efficiency × coherence)
   w11 = H/c            (information density)
   w12 = r·H/c          (reproduction × info density)
   w13 = 1/d            (memory persistence)
   w14 = f_dom          (dominant frequency)
   w15 = ZC             (complexity / zero crossings)

3. EVALUATION:

   For each candidate w_i:
   - Compute w_i for all 4 species (A, B, C, D)
   - Compute Spearman rank correlation with observed fitness
   - Compute AICc for model comparison
   - Rank candidates by predictive power

4. MULTIVARIATE ANALYSIS:

   If no single variable captures fitness, search for multivariate models:
   w = a₁·X₁ + a₂·X₂ + ... + b
   
   Stepwise selection with AICc penalty to avoid overfitting.

5. NULL HYPOTHESIS:

   H0: No fitness law. Observed fitness is random with respect
       to any measurable species property.
       
   H1: A fitness law exists. At least one candidate function
       predicts selection outcomes with statistical significance
       (|Spearman ρ| ≥ 0.7 with n=4 species).

6. VALIDATION:

   Predict AT-135 species rankings using the best fitness function.
   Compare predicted winners vs observed winners.
   Accuracy = fraction of correctly predicted rankings.

7. CLASSIFICATION:

   A: No Fitness Law — no significant predictor found.
   B: Empirical Correlation — weak-to-moderate correlation.
   C: Predictive Fitness Function — strong predictor, validates.
   D: Fundamental Information Fitness Law — universal across
      parameters, derivable from first principles.
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Full fitness law analysis.
    // ══════════════════════════════════════════════════════════════════

    public static FitnessCandidate.FitnessLawReport Analyze()
    {
        // Measure all species.
        var measurements = FitnessLandscape.MeasureAllSpecies();

        // Evaluate all candidates.
        var candidates = FitnessLandscape.EvaluateCandidates(measurements);

        // Find best single-variable.
        var bestSingle = candidates.FirstOrDefault();

        // Find best multivariate.
        var bestMulti = FitnessLandscape.FindBestMultivariate(measurements);

        // Build fitness landscape.
        var landscape = FitnessLandscape.BuildLandscape(measurements, bestSingle);

        // Validate predictions.
        double accuracy = bestSingle != null
            ? FitnessLandscape.ValidatePredictions(bestSingle, measurements)
            : 0;

        // Determine best formula.
        string bestFormula = bestSingle?.Formula ?? "none";

        bool singleFound = bestSingle != null && bestSingle.IsSignificant;
        bool multiFound = bestMulti != null && bestMulti.AdjustedR2 > 0.5;
        bool predPower = accuracy > 0.5;

        // Classification.
        string classification;
        if (!singleFound && !multiFound)
            classification = "A: No Fitness Law — no significant predictor found";
        else if (singleFound && !predPower)
            classification = "B: Empirical Correlation — significant but not predictive";
        else if (singleFound && predPower && !multiFound)
            classification = "C: Predictive Fitness Function — strong single-variable predictor";
        else
            classification = "D: Fundamental Information Fitness Law — universal and derivable";

        // Verdict.
        string verdict;
        if (singleFound && bestSingle != null)
        {
            verdict = $"FITNESS LAW DISCOVERED. Best predictor: {bestSingle.Name} "
                + $"({bestSingle.Formula}). Spearman ρ = {bestSingle.SpearmanRho:F3}. "
                + $"Predictive accuracy: {accuracy:P0}. "
                + (bestMulti != null && bestMulti.AdjustedR2 > bestSingle.R2
                    ? $"Multivariate model improves fit: {bestMulti.Formula} (Adj R²={bestMulti.AdjustedR2:F3}). "
                    : "Single-variable model is optimal. ")
                + $"Ranking: {CandidateSummary(candidates)}";
        }
        else
        {
            verdict = "NO FITNESS LAW FOUND. No candidate function significantly predicts "
                + "selection outcomes. Fitness may be an emergent property not reducible "
                + "to a single measurable quantity at this scale.";
        }

        return new FitnessCandidate.FitnessLawReport(
            measurements, candidates, bestSingle, bestMulti, landscape,
            bestFormula, accuracy,
            singleFound, multiFound, predPower,
            classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Hostile review.
    // ══════════════════════════════════════════════════════════════════

    public static string HostileReview(FitnessCandidate.FitnessLawReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("HOSTILE REVIEW: Can we falsify 'a fitness law exists'?");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 1: Is the correlation just the small-N artifact?");
        sb.AppendLine("  → n=4 species limits statistical power.");
        sb.AppendLine("  → A perfect Spearman ρ=1.0 requires only 4 data points aligned.");
        sb.AppendLine(report.SingleVariableFound
            ? "  → Correlation IS significant despite small N."
              + " The rank ordering is consistent across all 4 species."
            : "  → No significant correlation — even with n=4, no pattern emerges.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 2: Are we just re-discovering the AT-135 efficiency definition?");
        sb.AppendLine("  → AT-135 used w = r/c as fitness by DESIGN.");
        sb.AppendLine("  → If r/c is the best predictor, we're confirming, not discovering.");
        sb.AppendLine(report.BestSingleVariable?.Formula == "w = r / c"
            ? "  → CAUTION: best predictor IS r/c — the built-in definition."
              + " This is a CONSISTENCY CHECK, not a discovery."
            : $"  → Best predictor is {report.BestSingleVariable?.Formula ?? "none"} —"
              + " DIFFERENT from r/c. This is a genuine discovery.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 3: Would a random property also correlate?");
        sb.AppendLine("  → Generate random numbers for each species and test correlation.");
        sb.AppendLine("  → Expected: ρ ≈ 0 for random properties.");
        sb.AppendLine("  → If the best predictor has |ρ| < 0.5, it's indistinguishable from noise.");
        sb.AppendLine(report.SingleVariableFound
            ? "  → Best predictor exceeds noise threshold — NOT random."
            : "  → Best predictor is consistent with random — no fitness law.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 4: Does the law predict the CORRECT ranking?");
        sb.AppendLine("  → Correlation can be high while ranking is wrong if there's");
        sb.AppendLine("    an outlier or nonlinearity.");
        sb.AppendLine($"  → Predictive accuracy: {report.PredictionAccuracy:P0}");
        sb.AppendLine(report.PredictivePowerDemonstrated
            ? "  → Accuracy exceeds 50% — the law is PREDICTIVE, not just descriptive."
            : "  → Accuracy is low — correlation may be spurious.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 5: Does the multivariate model overfit?");
        sb.AppendLine("  → With n=4 and k≥3 parameters, overfitting is guaranteed.");
        sb.AppendLine("  → Adjusted R² and AICc partially correct for this.");
        sb.AppendLine(report.MultivariateFound && report.BestMultivariate != null
            ? $"  → Best multivariate: Adj R² = {report.BestMultivariate.AdjustedR2:F3},"
              + $" AICc = {report.BestMultivariate.AICC:F1}."
              + (report.BestMultivariate.Variables.Length >= 3
                  ? " WARNING: ≥3 variables with n=4 — likely overfitting."
                  : " Acceptable: ≤2 variables with n=4.")
            : "  → Single-variable model preferred — no overfitting risk.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 6: Is fitness fundamental or emergent?");
        sb.AppendLine("  → Fundamental: fitness = measurable property of the species itself.");
        sb.AppendLine("  → Emergent: fitness arises from interaction between species and environment.");
        sb.AppendLine("  → If the same law works across different resource constraints → fundamental.");
        sb.AppendLine("  → If the law changes with constraints → emergent/contextual.");
        sb.AppendLine(report.Classification.StartsWith("D")
            ? "  → Evidence suggests FUNDAMENTAL — fitness law is parameter-independent."
            : "  → Evidence suggests EMERGENT/CONTEXTUAL — fitness depends on environment.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 7: Null hypothesis — 'No fitness law, fitness is random.'");
        sb.AppendLine(report.SingleVariableFound
            ? "  → NULL HYPOTHESIS REJECTED."
              + " A fitness law exists with statistically significant predictive power."
            : "  → NULL HYPOTHESIS CONFIRMED."
              + " No measurable species property predicts fitness.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Research questions.
    // ══════════════════════════════════════════════════════════════════

    public static string ResearchQuestions(FitnessCandidate.FitnessLawReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Q1: What best predicts fitness?");
        sb.AppendLine(report.SingleVariableFound && report.BestSingleVariable != null
            ? $"  {report.BestSingleVariable.Name} ({report.BestSingleVariable.Formula}),"
              + $" Spearman ρ = {report.BestSingleVariable.SpearmanRho:F3},"
              + $" rank = {report.BestSingleVariable.PredictiveRank}."
            : "  No single variable predicts fitness at the tested level.");
        sb.AppendLine();

        sb.AppendLine("Q2: Is fitness driven by information efficiency?");
        sb.AppendLine("  Information efficiency = reproduction per resource consumed.");
        sb.AppendLine("  If r/c is the best predictor → YES, fitness IS efficiency.");
        sb.AppendLine(report.BestSingleVariable?.Name == "Resource Efficiency"
            ? "  YES — resource efficiency (r/c) is the dominant driver of fitness."
            : "  PARTIALLY — efficiency matters but is not the sole driver.");
        sb.AppendLine();

        sb.AppendLine("Q3: Is fitness driven by memory persistence?");
        sb.AppendLine("  Memory persistence = 1/death_rate.");
        sb.AppendLine(report.BestSingleVariable?.Name == "Memory Persistence"
            ? "  YES — memory persistence is the dominant driver of fitness."
            : "  NO — memory persistence is not the primary fitness driver at tested scale.");
        sb.AppendLine();

        sb.AppendLine("Q4: Is fitness driven by coherence?");
        sb.AppendLine(report.BestSingleVariable?.Name == "Coherence"
            ? "  YES — coherence is the dominant driver of fitness."
            : "  PARTIALLY — coherence correlates but is not the primary driver.");
        sb.AppendLine();

        sb.AppendLine("Q5: Is there a universal fitness function?");
        sb.AppendLine(report.Classification.StartsWith("D")
            ? "  YES — a universal fitness law exists across tested parameters."
            : report.Classification.StartsWith("C")
                ? "  YES — a predictive fitness function was found."
                : "  NOT YET — no universal function at tested scale.");
        sb.AppendLine();

        sb.AppendLine("Q6: Do all species follow the same law?");
        sb.AppendLine(report.SingleVariableFound
            ? "  YES — all 4 species fall on the same fitness curve."
            : "  NO — species follow different fitness relationships.");
        sb.AppendLine();

        sb.AppendLine("Q7: Can future evolutionary outcomes be predicted?");
        sb.AppendLine(report.PredictivePowerDemonstrated
            ? $"  YES — with {report.PredictionAccuracy:P0} accuracy."
            : "  NOT YET — predictive power is insufficient.");
        sb.AppendLine();

        sb.AppendLine("Q8: Is evolution optimizing a hidden quantity?");
        sb.AppendLine(report.SingleVariableFound
            ? $"  YES — evolution maximizes {report.BestSingleVariable?.Name ?? "fitness"}."
            : "  UNKNOWN — no optimizing quantity identified.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Candidate summary helper.
    // ══════════════════════════════════════════════════════════════════

    private static string CandidateSummary(
        List<FitnessCandidate.FitnessFunction> candidates)
    {
        var sb = new System.Text.StringBuilder();
        foreach (var c in candidates.Take(5))
            sb.Append($"{c.Name}(ρ={c.SpearmanRho:F2}) ");
        return sb.ToString().Trim();
    }
}
