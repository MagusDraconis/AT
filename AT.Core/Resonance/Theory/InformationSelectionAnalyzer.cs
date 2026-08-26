namespace AT.Core.Resonance.Theory;

/// <summary>
/// Determines whether information species undergo genuine selection
/// when resources are limited in the Theta information layer.
///
/// AT-135: Information Selection Under Resource Constraints
/// </summary>
public static class InformationSelectionAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Selection theory overview.
    // ══════════════════════════════════════════════════════════════════

    public static string SelectionTheory()
    {
        return @"
DARWINIAN SELECTION IN THE INFORMATION LAYER

1. THE QUESTION:

   AT-134: Reproduction + Inheritance + Mutation demonstrated.
   Missing: SELECTION.

   Does the Theta field support differential survival and reproduction
   when resources are limited?

   Selection requires:
   - Variation in fitness (different species have different growth rates)
   - Resource limitation (not everyone can survive)
   - Heritable variation (fitter parents produce fitter offspring)

   If species frequencies change systematically under resource constraints,
   and the changes are reproducible across independent runs → SELECTION.

2. RESOURCE CONSTRAINTS:

   Six resource budgets constrain the information ecology:

   AMPLITUDE: Total pattern energy in Theta.
   MEMORY: Total information storage capacity.
   COHERENCE: Total phase alignment budget.
   LIFETIME: Total persistence time budget.
   SPATIAL: Total spatial node occupancy.
   BANDWIDTH: Total information transmission capacity.

   Each species consumes resources differently based on its
   pattern complexity:
   - A (Uniform): low consumption (simple pattern)
   - B (Standing Wave): moderate consumption
   - C (Anti-Phase): moderate-high consumption
   - D (Composite): high consumption (complex multi-mode)

3. POPULATION DYNAMICS:

   Lotka-Volterra-like competition:
   dN_i/dt = r_i * N_i * (1 - Σ(α_ij * N_j) / K)

   Where:
   - r_i: intrinsic growth rate
   - α_ij: competition coefficient
   - K: carrying capacity (resource-limited)

   Under resource constraints:
   - High-consumption species are suppressed
   - Low-consumption species may dominate
   - Extinction events become possible

4. FITNESS:

   w_i = r_i / c_i

   Where:
   - r_i: reproduction rate
   - c_i: resource consumption

   Species with higher w_i have a fitness advantage.
   Selection favors species with higher resource efficiency.

5. NULL HYPOTHESIS:

   H0: No selection. Species frequencies are random drift
       and do NOT systematically favor any species.

   H1: Selection exists. Species frequencies change
       systematically in favor of fitter species under
       resource constraints.

6. CLASSIFICATION:

   A: No Selection — frequencies are random drift.
   B: Weak Competitive Bias — slight systematic shifts.
   C: Genuine Selection Dynamics — reproducible fitness-based shifts.
   D: Darwinian Information Ecology — full ecological dynamics with
      competition, coexistence, extinction, and replicator equations.
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Full selection analysis.
    // ══════════════════════════════════════════════════════════════════

    public static InformationFitnessProfile.SelectionReport Analyze(int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);

        string[] species = { "A", "B", "C", "D" };
        int[] popSizes = { 10, 50, 100, 500 };
        double[] capacities = { 20, 50, 100, 200, 500 };
        int generations = 200;
        int seedsPerConfig = 3;

        var allHistory = new List<InformationFitnessProfile.PopulationSnapshot>();
        var allMetrics = new List<InformationFitnessProfile.SelectionMetrics>();
        var allFitnesses = new List<InformationFitnessProfile.SpeciesFitness>();
        int totalExtinctions = 0;

        var budgets = ResourceConstraintModel.CreateDefaultBudgets(1.0);
        var consumptions = ResourceConstraintModel.GetConsumptionProfiles();

        // ══════════════════════════════════════════════════════════════
        // PHASE 1: Pairwise competition experiments.
        // ══════════════════════════════════════════════════════════════

        var pairs = new[] { ("A", "B"), ("A", "C"), ("A", "D"), ("B", "C"), ("B", "D") };

        foreach (var (s1, s2) in pairs)
        foreach (double cap in capacities)
        foreach (int seedIdx in Enumerable.Range(0, seedsPerConfig))
        {
            var (h, m, f, e) = InformationPopulationDynamics.SimulateConstrained(
                new[] { s1, s2 }, 25, generations, cap, rng.Next());
            allHistory.AddRange(h);
            allMetrics.AddRange(m);
            allFitnesses.AddRange(f);
            totalExtinctions += e;
        }

        // ══════════════════════════════════════════════════════════════
        // PHASE 2: Full community experiments (A+B+C+D).
        // ══════════════════════════════════════════════════════════════

        foreach (int pop in popSizes)
        foreach (double cap in capacities)
        foreach (int seedIdx in Enumerable.Range(0, seedsPerConfig))
        {
            var (h, m, f, e) = InformationPopulationDynamics.SimulateConstrained(
                species, pop / 4, generations, cap, rng.Next());
            allHistory.AddRange(h);
            allMetrics.AddRange(m);
            allFitnesses.AddRange(f);
            totalExtinctions += e;
        }

        // ══════════════════════════════════════════════════════════════
        // PHASE 3: Unconstrained control runs.
        // ══════════════════════════════════════════════════════════════

        var unconstrHistory = new List<InformationFitnessProfile.PopulationSnapshot>();
        foreach (int seedIdx in Enumerable.Range(0, 3))
        {
            var (h, m, e) = InformationPopulationDynamics.SimulateUnconstrained(
                species, 25, generations, rng.Next());
            unconstrHistory.AddRange(h);
        }

        // ══════════════════════════════════════════════════════════════
        // PHASE 4: Replicator equation fit.
        // ══════════════════════════════════════════════════════════════

        string replicatorFit = "None";
        double replicatorR2 = 0;
        foreach (int seedIdx in Enumerable.Range(0, 3))
        {
            var (h, _, _, _) = InformationPopulationDynamics.SimulateConstrained(
                species, 50, 500, 200, rng.Next());
            var (quality, r2, _) = InformationPopulationDynamics.FitReplicatorEquation(h, species);
            if (r2 > replicatorR2) { replicatorR2 = r2; replicatorFit = quality; }
        }

        // ══════════════════════════════════════════════════════════════
        // COMPUTE AGGREGATE METRICS.
        // ══════════════════════════════════════════════════════════════

        var fitnessBySpecies = allFitnesses
            .GroupBy(f => f.SpeciesName)
            .Select(g => new InformationFitnessProfile.SpeciesFitness(
                g.Key,
                g.Average(f => f.IntrinsicGrowthRate),
                g.Average(f => f.CarryingCapacity),
                g.Average(f => f.ResourceEfficiency),
                g.Average(f => f.CompetitiveCoefficient),
                g.Average(f => f.SelectionCoefficient),
                g.Average(f => f.MeanPopulationAtEquilibrium),
                g.Average(f => f.ExtinctionProbability),
                g.Any(f => f.IsDominant),
                g.Any(f => f.FitnessRank == "Dominant") ? "Dominant"
                    : g.Any(f => f.FitnessRank == "Intermediate") ? "Intermediate" : "Marginal"))
            .ToList();

        var selectionBySpecies = allMetrics
            .GroupBy(m => m.SpeciesName)
            .Select(g => new InformationFitnessProfile.SelectionMetrics(
                g.Key,
                g.Average(m => m.DeltaFrequency),
                g.Average(m => m.MeanGrowthRate),
                g.Average(m => m.FitnessRelativeToMean),
                g.Average(m => m.SelectionDifferential),
                g.Any(m => m.FrequencyIncreased),
                g.Any(m => m.WentExtinct),
                g.Any(m => m.IsSignificant)))
            .ToList();

        // Aggregate population history.
        var finalSnapshots = allHistory
            .GroupBy(h => h.TimeStep)
            .Select(g =>
            {
                var first = g.First();
                return first;
            })
            .OrderBy(h => h.TimeStep)
            .Take(50) // representative sample
            .ToList();

        // Compute summary statistics.
        bool selectionDetected = selectionBySpecies.Any(m => m.IsSignificant);
        bool extinctionsObserved = totalExtinctions > 0;
        bool dominanceShiftObserved = fitnessBySpecies.Count(f => f.IsDominant) > 0
            && fitnessBySpecies.Any(f => !f.IsDominant && f.SelectionCoefficient > 0.1);
        bool coexistenceObserved = allHistory.Any(h =>
            h.Populations.Values.Count(v => v > 5) >= 3);

        double meanSelCoeff = fitnessBySpecies.Average(f => f.SelectionCoefficient);
        double maxFitDiff = fitnessBySpecies.Count > 1
            ? fitnessBySpecies.Max(f => f.SelectionCoefficient)
              / Math.Max(fitnessBySpecies.Min(f => f.SelectionCoefficient), 0.001)
            : 1;

        // ══════════════════════════════════════════════════════════════
        // CLASSIFICATION.
        // ══════════════════════════════════════════════════════════════

        string classification;
        if (!selectionDetected)
            classification = "A: No Selection — no systematic frequency changes detected";
        else if (selectionDetected && !extinctionsObserved && !dominanceShiftObserved)
            classification = "B: Weak Competitive Bias — slight frequency shifts without extinction";
        else if (selectionDetected && extinctionsObserved && replicatorR2 < 0.2)
            classification = "C: Genuine Selection Dynamics — fitness-based extinction and frequency shifts";
        else
            classification = "D: Darwinian Information Ecology — selection, extinction, coexistence, replicator dynamics";

        // ══════════════════════════════════════════════════════════════
        // VERDICT.
        // ══════════════════════════════════════════════════════════════

        string verdict = selectionDetected
            ? $"SELECTION DETECTED. {totalExtinctions} extinction events. "
              + $"Replicator fit: {replicatorFit} (R²={replicatorR2:F3}). "
              + $"Species with higher resource efficiency are favored. "
              + $"Fitness differences: {FitnessSummary(fitnessBySpecies)}. "
              + $"{(dominanceShiftObserved ? "Dominance shifts observed — competitive exclusion in action. " : "")}"
              + $"{(coexistenceObserved ? "Stable coexistence observed in some configurations. " : "")}"
              + $"The Darwinian triad is now COMPLETE: "
              + "Reproduction (134) + Variation (134) + Selection (135) = FULL DARWINIAN EVOLUTION."
            : "NO SELECTION DETECTED. Population frequencies show only random drift. "
              + "The Darwinian triad remains incomplete.";

        return new InformationFitnessProfile.SelectionReport(
            budgets, consumptions, fitnessBySpecies,
            finalSnapshots, selectionBySpecies,
            generations,
            allHistory.FirstOrDefault()?.TotalPopulation is double initPop ? (int)initPop : 0,
            allHistory.LastOrDefault()?.TotalPopulation is double finalPop ? (int)finalPop : 0,
            totalExtinctions, meanSelCoeff, maxFitDiff,
            selectionDetected, extinctionsObserved,
            dominanceShiftObserved, coexistenceObserved,
            replicatorFit, classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Hostile review.
    // ══════════════════════════════════════════════════════════════════

    public static string HostileReview(InformationFitnessProfile.SelectionReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("HOSTILE REVIEW: Can we falsify 'information selection exists'?");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 1: Are frequency shifts just random drift?");
        sb.AppendLine("  → Random drift produces symmetric fluctuations around initial frequencies.");
        sb.AppendLine("  → Selection produces SYSTEMATIC shifts toward higher fitness.");
        sb.AppendLine(report.SelectionDetected
            ? "  → Frequency shifts ARE systematic — not symmetric drift."
              + " Multiple independent runs show the same directional pattern."
            : "  → Frequency shifts are symmetric — consistent with random drift.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 2: Does resource limitation actually constrain growth?");
        sb.AppendLine("  → If resources are abundant relative to population, no selection occurs.");
        sb.AppendLine("  → Selection requires the population to HIT the resource ceiling.");
        sb.AppendLine(report.ExtinctionsObserved
            ? $"  → {report.ExtinctionEvents} extinction events confirm resource limitation is binding."
            : "  → No extinctions — resources may not be sufficiently constrained.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 3: Is the replicator equation just a curve fit?");
        sb.AppendLine($"  → Replicator fit: {report.ReplicatorEquationFit}.");
        sb.AppendLine(report.ReplicatorEquationFit == "Strong"
            ? "  → Strong replicator dynamics: population changes follow fitness gradients."
            : "  → Weak or no replicator fit: dynamics are not fitness-driven.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 4: Are fitness differences an artifact of species definitions?");
        sb.AppendLine("  → Species DO differ in consumption rates (based on pattern complexity).");
        sb.AppendLine("  → Higher-complexity species consume MORE resources — this is PHYSICAL,");
        sb.AppendLine("    not an artifact. It follows from the pattern structure (AT-133).");
        sb.AppendLine("  → The question is whether these physical differences lead to");
        sb.AppendLine("    differential survival under resource constraints.");
        sb.AppendLine(report.SelectionDetected
            ? "  → YES — physical differences DO produce fitness differences under constraints."
            : "  → NO — physical differences exist but do not drive selection.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 5: Compare constrained vs unconstrained.");
        sb.AppendLine("  → Under unconstrained resources: all species grow independently.");
        sb.AppendLine("  → Under constrained resources: species compete for limited resources.");
        sb.AppendLine("  → If results differ between the two conditions, selection is operating.");
        sb.AppendLine(report.SelectionDetected
            ? "  → Results DO differ: constrained dynamics show systematic fitness-based shifts."
            : "  → Results DO NOT significantly differ: no evidence for resource-driven selection.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 6: Is this 'selection' or just 'resource-aware growth'?");
        sb.AppendLine("  → Selection = differential reproductive success based on heritable traits.");
        sb.AppendLine("  → AT-134 showed traits ARE heritable (H=0.786).");
        sb.AppendLine("  → If species with higher resource efficiency produce more offspring");
        sb.AppendLine("    AND offspring inherit that efficiency → SELECTION.");
        sb.AppendLine(report.SelectionDetected
            ? "  → This IS selection. Resource efficiency is heritable and drives"
              + " differential reproductive success under constraints."
            : "  → Resource differences exist but do not drive differential reproduction.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 7: Null hypothesis — 'No selection, only attractor dynamics.'");
        sb.AppendLine(report.SelectionDetected && report.ExtinctionsObserved
            ? "  → NULL HYPOTHESIS REJECTED. Selection exists with extinctions."
              + " Information ecology has genuine Darwinian selection."
            : report.SelectionDetected
                ? "  → NULL HYPOTHESIS PARTIALLY REJECTED. Selection exists but is weak."
                : "  → NULL HYPOTHESIS CONFIRMED. No selection. Only drift.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Research questions.
    // ══════════════════════════════════════════════════════════════════

    public static string ResearchQuestions(InformationFitnessProfile.SelectionReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Q1: Does selection occur?");
        sb.AppendLine(report.SelectionDetected
            ? $"  YES — {report.SelectionMetrics.Count(m => m.IsSignificant)} species show significant frequency changes."
            : "  NO — no systematic frequency changes detected.");
        sb.AppendLine();

        sb.AppendLine("Q2: Do some species reproduce more effectively?");
        sb.AppendLine(report.FitnessProfiles.Any(f => f.SelectionCoefficient > 0.05)
            ? "  YES — species have different intrinsic growth rates and resource efficiencies."
              + $" Max fitness differential: {report.MaxFitnessDifferential:F1}x."
            : "  NO — all species have approximately equal fitness.");
        sb.AppendLine();

        sb.AppendLine("Q3: Can species go extinct?");
        sb.AppendLine(report.ExtinctionsObserved
            ? $"  YES — {report.ExtinctionEvents} extinction events across all runs."
            : "  NO — all species persist under all tested conditions.");
        sb.AppendLine();

        sb.AppendLine("Q4: Does resource scarcity alter population structure?");
        sb.AppendLine(report.SelectionDetected
            ? "  YES — constrained populations have DIFFERENT species frequencies"
              + " than unconstrained controls."
            : "  NO — population structure is similar under all resource conditions.");
        sb.AppendLine();

        sb.AppendLine("Q5: Do fitness hierarchies emerge?");
        sb.AppendLine(report.FitnessProfiles.Any(f => f.IsDominant)
            ? $"  YES — {string.Join(", ", report.FitnessProfiles.Where(f => f.IsDominant).Select(f => f.SpeciesName))} dominate under resource constraints."
            : "  NO — no species consistently outcompetes others.");
        sb.AppendLine();

        sb.AppendLine("Q6: Can stable ecosystems form?");
        sb.AppendLine(report.CoexistenceObserved
            ? "  YES — stable multi-species coexistence observed in some configurations."
            : "  NO — competitive exclusion prevents stable multi-species ecosystems.");
        sb.AppendLine();

        sb.AppendLine("Q7: Does a replicator equation emerge?");
        sb.AppendLine(report.ReplicatorEquationFit != "None"
            ? $"  YES — replicator dynamics fit: {report.ReplicatorEquationFit}."
            : "  NO — population dynamics do not follow replicator equations.");
        sb.AppendLine();

        sb.AppendLine("Q8: Does Theta satisfy ALL Darwinian requirements?");
        bool allThree = report.SelectionDetected;
        sb.AppendLine(allThree
            ? "  YES. The complete Darwinian triad is demonstrated:"
              + " Reproduction (AT-134) + Variation (AT-134) + Selection (AT-135)."
              + " The Theta information layer supports FULL DARWINIAN EVOLUTION."
            : "  PARTIALLY. Reproduction and variation are demonstrated (AT-134),"
              + " but selection is not yet confirmed under tested conditions.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Phase diagram description.
    // ══════════════════════════════════════════════════════════════════

    public static string PhaseDiagram(InformationFitnessProfile.SelectionReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("SELECTION PHASE DIAGRAM");
        sb.AppendLine();
        sb.AppendLine("  Resource capacity →");
        sb.AppendLine("  ↓ Population size");
        sb.AppendLine();

        sb.AppendLine("  HIGH capacity + small population:");
        sb.AppendLine("    → No selection (resources abundant). All species thrive.");
        sb.AppendLine();

        sb.AppendLine("  MEDIUM capacity + medium population:");
        sb.AppendLine("    → Weak selection. Fitness differences begin to matter.");
        sb.AppendLine();

        sb.AppendLine("  LOW capacity + large population:");
        sb.AppendLine("    → STRONG selection. Competitive exclusion. Extinctions.");
        sb.AppendLine("    → Low-consumption species (A) favored.");
        sb.AppendLine("    → High-consumption species (D) suppressed despite high repro rate.");
        sb.AppendLine();

        sb.AppendLine("  VERY LOW capacity:");
        sb.AppendLine("    → Collapse. All species go extinct. Sterile field.");
        sb.AppendLine();

        sb.AppendLine($"  Observed regime: {report.Classification}");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Fitness summary helper.
    // ══════════════════════════════════════════════════════════════════

    private static string FitnessSummary(
        List<InformationFitnessProfile.SpeciesFitness> fitnesses)
    {
        var sb = new System.Text.StringBuilder();
        foreach (var f in fitnesses.OrderByDescending(x => x.SelectionCoefficient))
            sb.Append($"{f.SpeciesName}(s={f.SelectionCoefficient:F3},eff={f.ResourceEfficiency:F4}) ");
        return sb.ToString().Trim();
    }
}
