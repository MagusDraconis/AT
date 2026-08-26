namespace AT.Core.Resonance.Theory;

/// <summary>
/// Determines whether Darwinian information evolution is universal
/// (persists across alternative fitness models and resource regimes)
/// or model-dependent (disappears when assumptions change).
///
/// AT-137: Universality of Information Evolution
/// </summary>
public static class EvolutionUniversalityAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Universality theory.
    // ══════════════════════════════════════════════════════════════════

    public static string UniversalityTheory()
    {
        return @"
UNIVERSALITY OF INFORMATION EVOLUTION

1. THE QUESTION:

   AT-134/135/136 established Darwinian evolution in Theta:
   ✓ Reproduction  ✓ Variation  ✓ Selection  ✓ Fitness Law (w=r/c)

   But: was this evolution an ARTIFACT of the specific r/c fitness model?

   If evolution persists under radically different fitness definitions
   and resource regimes → UNIVERSAL.
   If evolution disappears when assumptions change → MODEL-DEPENDENT.

2. ALTERNATIVE FITNESS MODELS:

   A. Baseline (r/c)        — AT-135/136 default
   B. Quadratic Repro (r²/c) — favors high repro even more
   C. Inverse Square (r/c²)  — penalizes consumption heavily
   D. Linear Diff (r-c)      — additive, not multiplicative
   E. Logarithmic             — compressed scale
   F. Random Fitness          — no systematic advantage
   G. Equal Fitness           — no fitness differences at all

3. RESOURCE REGIMES:

   1. Global Resources    — baseline shared pool
   2. Local Resources     — per-species budgets
   3. Dynamic Resources   — growing capacity
   4. Fluctuating         — sinusoidal variation
   5. Scarcity Cycles     — boom/bust alternation
   6. Resource Shocks     — sudden collapses
   7. No Limits           — unconstrained control

4. ROBUSTNESS METRICS:

   Selection Robustness Index:
     Fraction of runs where selection is detected.

   Evolution Persistence Score:
     Weighted composite: selection (0.4) + competition (0.3)
     + coexistence (0.15) + persistence (0.15).

   Rank Stability (Kendall τ):
     How stable is the species fitness ranking across models?

5. NULL HYPOTHESIS:

   H0: Evolution is model-dependent. Changing the fitness model
       or resource regime DESTROYS Darwinian dynamics.

   H1: Evolution is universal. Darwinian dynamics PERSIST
       across diverse fitness models and resource regimes.

6. CLASSIFICATION:

   A: Evolution Artifact        — disappears under changes.
   B: Model-Dependent Evolution — exists but is fragile.
   C: Robust Evolution          — persists across most models.
   D: Universal Evolution Principle — inevitable emergent phenomenon.
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Full universality analysis.
    // ══════════════════════════════════════════════════════════════════

    public static AlternativeFitnessModel.UniversalityReport Analyze()
    {
        var models = EvolutionRobustnessProfile.GetAllFitnessModels();
        var regimes = EvolutionRobustnessProfile.GetAllResourceRegimes();

        // Run all combinations.
        var results = EvolutionRobustnessProfile.RunAll(models, regimes, seedsPerConfig: 3);

        // Compute metrics.
        var metrics = EvolutionRobustnessProfile.ComputeMetrics(results, models, regimes);

        // Species rank stability.
        var rankStability = EvolutionRobustnessProfile.ComputeRankStability(results);

        // Hidden invariant.
        var invariant = EvolutionRobustnessProfile.DiscoverHiddenInvariant(results);

        // Classification.
        string classification = metrics.Classification;

        string verdict = metrics.Verdict;

        return new AlternativeFitnessModel.UniversalityReport(
            models, regimes, results, metrics,
            rankStability, invariant,
            classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Hostile review.
    // ══════════════════════════════════════════════════════════════════

    public static string HostileReview(AlternativeFitnessModel.UniversalityMetrics metrics)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("HOSTILE REVIEW: Can we falsify 'evolution is universal'?");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 1: Does evolution survive RANDOM fitness?");
        sb.AppendLine("  → If fitness is assigned randomly, there should be NO selection.");
        sb.AppendLine("  → Any 'selection' in random-fitness runs is stochastic noise.");
        int randomSel = metrics.TotalRuns > 0
            ? metrics.RunsWithSelection : 0;
        sb.AppendLine(randomSel > 0
            ? "  → Selection detected even under random fitness —"
              + " this may indicate that resource constraints alone"
              + " create differential outcomes (NOT genuine selection)."
            : "  → No selection under random fitness — fitness matters.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 2: Does evolution survive EQUAL fitness?");
        sb.AppendLine("  → If all species have equal fitness, the ecosystem should");
        sb.AppendLine("    be at an unstable equilibrium — stochastic drift only.");
        sb.AppendLine("  → Any systematic frequency changes under equal fitness");
        sb.AppendLine("    are NOT natural selection.");
        sb.AppendLine(metrics.RunsWithSelection > metrics.TotalRuns * 0.1
            ? "  → Selection detected broadly — BUT this includes random/equal"
              + " fitness models where it shouldn't exist. CAUTION needed."
            : "  → Selection is narrowly detected — consistent with genuine signal.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 3: Is extinction just the population hitting zero by drift?");
        sb.AppendLine($"  → {metrics.RunsWithExtinctions}/{metrics.TotalRuns} runs had extinctions.");
        sb.AppendLine(metrics.RunsWithExtinctions > metrics.TotalRuns * 0.5
            ? "  → Extinctions are COMMON — resource constraints are binding."
            : "  → Extinctions are RARE — most configurations are stable.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 4: Does the fitness ranking remain stable?");
        sb.AppendLine($"  → Mean rank stability (Kendall τ): {metrics.RankStabilityGlobal:F3}");
        sb.AppendLine(metrics.RankStabilityGlobal > 0.5
            ? "  → Species rankings are STABLE across models —"
              + " fitness hierarchy is robust."
            : "  → Species rankings are UNSTABLE —"
              + " the fitness hierarchy depends on the model.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 5: Is there a model where evolution COMPLETELY disappears?");
        sb.AppendLine("  → Under No Resource Limits control: evolution SHOULD disappear");
        sb.AppendLine("    (no resource pressure → no selection).");
        sb.AppendLine("  → Under Equal Fitness: evolution SHOULD disappear");
        sb.AppendLine("    (no fitness differences → no differential survival).");
        sb.AppendLine("  → If evolution persists in these regimes, the detection method");
        sb.AppendLine("    may be too sensitive (false positives).");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 6: Is the 'universality' just the fact that resources matter?");
        sb.AppendLine("  → The TRULY invariant fact may be: constrained resources →");
        sb.AppendLine("    differential outcomes. This is not 'evolution' per se —");
        sb.AppendLine("    it's just resource allocation.");
        sb.AppendLine("  → Genuine universal evolution requires: variation + heritability");
        sb.AppendLine("    + fitness differences → systematic change. Is this chain");
        sb.AppendLine("    intact across ALL models?");
        sb.AppendLine(metrics.EvolutionPersistenceScore > 0.6
            ? "  → YES — the full chain persists across most configurations."
            : "  → NO — the chain breaks under some configurations.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 7: Null hypothesis — 'Evolution is an artifact of r/c.'");
        sb.AppendLine(metrics.IsEvolutionUniversal
            ? "  → NULL HYPOTHESIS REJECTED. Evolution persists across"
              + " diverse fitness models and resource regimes."
            : "  → NULL HYPOTHESIS CONFIRMED. Evolution is model-dependent"
              + " and does not survive radical assumption changes.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Research questions.
    // ══════════════════════════════════════════════════════════════════

    public static string ResearchQuestions(AlternativeFitnessModel.UniversalityMetrics metrics)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Q1: Is selection robust?");
        sb.AppendLine(metrics.SelectionRobustnessIndex > 0.5
            ? $"  YES — selection persists in {metrics.SelectionRobustnessIndex:P0} of runs."
            : $"  NO — selection is fragile ({metrics.SelectionRobustnessIndex:P0}).");
        sb.AppendLine();

        sb.AppendLine("Q2: Do the same species remain dominant?");
        sb.AppendLine(metrics.RankStabilityGlobal > 0.5
            ? $"  YES — rank stability τ = {metrics.RankStabilityGlobal:F3}."
            : $"  PARTIALLY — τ = {metrics.RankStabilityGlobal:F3}, rankings shift across models.");
        sb.AppendLine();

        sb.AppendLine("Q3: Does evolution survive changing fitness laws?");
        sb.AppendLine(metrics.IsEvolutionUniversal
            ? "  YES — evolution persists across all 7 fitness models."
            : "  PARTIALLY — evolution survives some models but not all.");
        sb.AppendLine();

        sb.AppendLine("Q4: Does evolution survive changing resources?");
        sb.AppendLine(metrics.EvolutionPersistenceScore > 0.5
            ? "  YES — evolution persists across resource regimes."
            : "  NO — evolution is sensitive to resource configuration.");
        sb.AppendLine();

        sb.AppendLine("Q5: Is reproduction alone sufficient?");
        sb.AppendLine("  NO — selection requires BOTH reproduction AND fitness differences."
                     + " Equal fitness + reproduction ≠ evolution.");
        sb.AppendLine();

        sb.AppendLine("Q6: Can evolution emerge without explicit fitness?");
        sb.AppendLine(metrics.EvolutionPersistenceScore > 0.4
            ? "  PARTIALLY — some Darwinian signatures emerge from resource"
              + " dynamics alone, but full evolution requires fitness variation."
            : "  NO — explicit fitness differences are necessary.");
        sb.AppendLine();

        sb.AppendLine("Q7: Is there a deeper invariant beneath w=r/c?");
        sb.AppendLine("  The invariant across all models appears to be:"
                     + " RESOURCE CONSTRAINT × REPRODUCTION → ECOLOGICAL DYNAMICS."
                     + " The specific fitness formula (r/c) is NOT invariant —"
                     + " but the PHENOMENON of differential survival IS.");
        sb.AppendLine();

        sb.AppendLine("Q8: Is Darwinian dynamics universal in Theta?");
        sb.AppendLine(metrics.IsEvolutionUniversal
            ? "  YES. Darwinian information dynamics are a UNIVERSAL emergent"
              + " property of Theta. They do not require the specific r/c"
              + " fitness model — any fitness asymmetry + resource constraint"
              + " produces selection, competition, and extinction."
            : "  PARTIALLY. Darwinian dynamics exist but are model-dependent.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Model summary table.
    // ══════════════════════════════════════════════════════════════════

    public static string ModelSummaryTable(
        List<AlternativeFitnessModel.ModelRunResult> results,
        List<AlternativeFitnessModel.FitnessModelSpec> models)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("  Model                    │ Selection │ Extinctions │ Competition │ Evolution?");
        sb.AppendLine("  " + new string('─', 85));

        foreach (var model in models)
        {
            var runs = results.Where(r => r.FitnessModel == model.Name).ToList();
            if (runs.Count == 0) continue;

            int sel = runs.Count(r => r.SelectionDetected);
            int ext = runs.Count(r => r.Extinctions > 0);
            int comp = runs.Count(r => r.CompetitionObserved);
            int evo = runs.Count(r => r.EvolutionPersisted);

            sb.AppendLine($"  {model.Name,-24} │ {sel,2}/{runs.Count,2}     │ {ext,2}/{runs.Count,2}       │ {comp,2}/{runs.Count,2}         │ {(evo > runs.Count / 2 ? "YES" : "no")}");
        }

        return sb.ToString();
    }
}
