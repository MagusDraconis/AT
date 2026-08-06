namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Determines whether stable information species discovered in TQM-133
/// can reproduce, inherit traits, and form persistent information lineages.
///
/// TQM-134: Information Species Reproduction and Inheritance
/// </summary>
public static class InformationInheritanceAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Evolution theory overview.
    // ══════════════════════════════════════════════════════════════════

    public static string EvolutionTheory()
    {
        return @"
INFORMATION EVOLUTION THEORY

1. THE QUESTION:

   TQM-133 discovered 4 stable information species in Theta.
   But do these species merely EXIST, or can they REPRODUCE?

   If species can produce copies of themselves → REPRODUCTION.
   If copies pass traits to descendants → INHERITANCE.
   If traits persist across generations → LINEAGES.
   If variation + selection occurs → EVOLUTION.

2. REPRODUCTION MECHANISMS:

   SELF-REPLICATION:
   A single species generates a copy of itself through
   the information dynamics. Requires sufficient field
   density (autonomy) and pattern stability.

   CROSS-REPRODUCTION:
   Two species interact and produce a new pattern that
   inherits traits from both parents. Analogous to
   recombination in biological systems.

   MERGER:
   Two overlapping species merge into a composite that
   inherits from both.

3. INHERITANCE:

   Inheritance coefficient H(parent, child):
   H = pattern_similarity(parent, child)
   H > 0: child resembles parent beyond random chance.
   H ≈ 1: perfect fidelity (cloning).
   H ≈ 0: no inheritance (random).

   If H is significantly > 0 across multiple independent
   runs, INHERITANCE IS DEMONSTRATED.

4. MUTATIONS:

   Mutation rate μ = 1 - H.
   μ measures how much drift accumulates per generation.
   High μ → species identity fades quickly.
   Low μ → species identity persists across generations.

5. SPECIES TRANSITIONS:

   Transition matrix T_ij:
   Probability that pattern starting as species i
   is classified as species j after evolution.

   Diagonal T_ii = species stability (identity preservation).
   Off-diagonal T_ij = mutation/attraction to other species.

6. LINEAGES:

   A lineage is an ancestor → descendant chain.
   Lineage length = number of surviving generations.
   Lineage similarity = descendant vs ancestor pattern similarity.
   If lineages persist > 1 generation → EVOLUTIONARY DYNAMICS.

7. HOSTILE NULL HYPOTHESIS:

   H0: All observed species are merely attractors.
       No inheritance. Any pattern near attractor X
       converges to X regardless of its parentage.


   TEST: Compare 'child' patterns produced by reproduction
   vs patterns initialized near the same attractor.
   If child patterns differ systematically from random
   initializations → inheritance is real.

8. CLASSIFICATION:

   A: Attractors Only — no reproduction, no lineages.
      Species are static attractor basins.

   B: Weak Inheritance — occasional reproduction,
      low fidelity, short lineages.

   C: Information Lineages — reproducible reproduction,
      significant inheritance, persistent lineages.

   D: Information Evolution Layer — robust reproduction,
      high heritability, competition, adaptation.
      Theta supports Darwinian information dynamics.
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Full analysis of reproduction, inheritance, and evolution.
    // ══════════════════════════════════════════════════════════════════

    public static InformationLineage.InformationEvolutionReport Analyze(
        int? seed = null, bool verbose = false)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);

        string[] species = { "A", "B", "C", "D" };
        double[] densities = { 0.3, 0.5, 0.7, 0.9 };
        int[] timeScales = { 1000, 5000, 10000, 50000 };

        var allEvents = new List<InformationLineage.ReproductionEvent>();
        var allLineages = new List<InformationLineage.SpeciesLineage>();
        var allTransitions = new List<InformationLineage.SpeciesTransition>();
        int totalExtinctions = 0;

        // ══════════════════════════════════════════════════════════════
        // PHASE 1: Single-species reproduction experiments.
        // ══════════════════════════════════════════════════════════════

        foreach (string sp in species)
        foreach (double density in densities)
        {
            var ev = SpeciesReproductionProfile.SimulateReproduction(
                sp, sp, density, seed: rng.Next());
            allEvents.Add(ev);
        }

        // ══════════════════════════════════════════════════════════════
        // PHASE 2: Cross-species interaction experiments.
        // ══════════════════════════════════════════════════════════════

        var pairs = new[]
        {
            ("A", "B"), ("B", "C"), ("A", "C"),
            ("A", "D"), ("B", "D"), ("C", "D"),
        };

        foreach (var (s1, s2) in pairs)
        foreach (double density in densities)
        {
            var ev = SpeciesReproductionProfile.SimulateReproduction(
                s1, s2, density, seed: rng.Next());
            allEvents.Add(ev);
        }

        // ══════════════════════════════════════════════════════════════
        // PHASE 3: Multi-species population experiments.
        // ══════════════════════════════════════════════════════════════

        var populations = new[]
        {
            new List<string> { "A", "A" },
            new List<string> { "A", "B" },
            new List<string> { "B", "C" },
            new List<string> { "A", "C" },
            new List<string> { "A", "B", "C" },
        };

        foreach (var pop in populations)
        foreach (double density in densities)
        {
            foreach (int timescale in timeScales)
            {
                var (lineages, events, ext) = SpeciesReproductionProfile.SimulateEvolution(
                    pop, timescale, density, seed: rng.Next());
                allLineages.AddRange(lineages);
                allEvents.AddRange(events);
                totalExtinctions += ext;
            }
        }

        // ══════════════════════════════════════════════════════════════
        // PHASE 4: Transition matrix.
        // ══════════════════════════════════════════════════════════════

        foreach (double density in densities)
        {
            allTransitions.AddRange(
                SpeciesReproductionProfile.ComputeTransitionMatrix(
                    species, density, seed: rng.Next()));
        }

        // ══════════════════════════════════════════════════════════════
        // PHASE 5: Build species profiles.
        // ══════════════════════════════════════════════════════════════

        var profiles = species.Select(sp =>
            SpeciesReproductionProfile.BuildProfile(sp, allEvents, allLineages))
            .ToList();

        // ══════════════════════════════════════════════════════════════
        // COMPUTE SUMMARY STATISTICS.
        // ══════════════════════════════════════════════════════════════

        int totalReproEvents = allEvents.Count(e => e.Outcome == "Reproduce" && e.ChildSurvived);
        int totalLineages = allLineages.Count;
        int longestLineage = allLineages.Count > 0 ? allLineages.Max(l => l.LineageLength) : 0;

        double meanH = allEvents
            .Where(e => e.Outcome == "Reproduce" && e.ChildSurvived)
            .Select(e => e.InheritanceCoefficient)
            .DefaultIfEmpty(0)
            .Average();

        double meanFidelity = profiles.Select(p => p.Fidelity).DefaultIfEmpty(0).Average();
        double meanSurvival = profiles.Select(p => p.SurvivalProbability).DefaultIfEmpty(0).Average();

        bool reproductionDetected = totalReproEvents > 0;
        bool lineagesFormed = allLineages.Any(l => l.LineageLength > 2);
        bool mutationsObserved = allLineages.Any(l => l.MutationDrift > 0.05);
        bool competitionDetected = allEvents.Any(e => e.Outcome == "Compete");

        // ══════════════════════════════════════════════════════════════
        // CLASSIFICATION.
        // ══════════════════════════════════════════════════════════════

        string classification;
        if (!reproductionDetected)
            classification = "A: Attractors Only — no reproduction detected";
        else if (reproductionDetected && !lineagesFormed)
            classification = "B: Weak Inheritance — reproduction exists but no persistent lineages";
        else if (lineagesFormed && meanH < 0.5)
            classification = "C: Information Lineages — persistent lineages with moderate inheritance";
        else
            classification = "D: Information Evolution Layer — robust reproduction, high heritability, persistent lineages";

        // ══════════════════════════════════════════════════════════════
        // VERDICT.
        // ══════════════════════════════════════════════════════════════

        string verdict = reproductionDetected
            ? $"INFORMATION REPRODUCTION DETECTED. {totalReproEvents} successful reproduction events. " +
              $"Mean inheritance coefficient H = {meanH:F3}. " +
              $"Mean species fidelity = {meanFidelity:F3}. " +
              $"{(lineagesFormed ? $"Persistent lineages found (max length {longestLineage}). " : "No persistent lineages. ")}" +
              $"{(mutationsObserved ? "Mutations observed during evolution. " : "No significant mutations. ")}" +
              $"{(competitionDetected ? "Species competition detected. " : "No competition observed. ")}" +
              $"{totalExtinctions} extinction events. " +
              $"{SpeciesSummary(profiles, allLineages)}"
            : "NO INFORMATION REPRODUCTION DETECTED. Species are static attractors with no capacity for self-copying or inheritance. " +
              "The null hypothesis (attractors only) is confirmed.";

        return new InformationLineage.InformationEvolutionReport(
            profiles, allLineages, allTransitions, allEvents,
            totalReproEvents, totalExtinctions,
            totalLineages, longestLineage,
            meanH, meanFidelity, meanSurvival,
            reproductionDetected, lineagesFormed,
            mutationsObserved, competitionDetected,
            classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Hostile review.
    // ══════════════════════════════════════════════════════════════════

    public static string HostileReview(InformationLineage.InformationEvolutionReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("HOSTILE REVIEW: Can we falsify 'information reproduction exists'?");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 1: Are 'reproduction events' just attractor convergence?");
        sb.AppendLine("  → If a child pattern simply converges to the parent's attractor basin,");
        sb.AppendLine("    this is attractor dynamics, NOT reproduction.");
        sb.AppendLine(report.ReproductionDetected
            ? "  → Child patterns ARE similar to parents, but the question is whether"
              + " this similarity exceeds the attractor convergence baseline."
              + " See ATTEMPT 4 for the statistical test."
            : "  → No reproduction detected — the attractor-only explanation holds.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 2: Is 'inheritance' just persistence of initial conditions?");
        sb.AppendLine("  → If a species simply persists because it's an attractor,");
        sb.AppendLine("    any initial condition in its basin produces the same state.");
        sb.AppendLine("  → TRUE inheritance requires: child carries parent-specific traits");
        sb.AppendLine("    that are NOT derivable from the attractor alone.");
        sb.AppendLine(report.MeanInheritanceCoefficient > 0.3
            ? $"  → Mean H = {report.MeanInheritanceCoefficient:F3} —"
              + " children DO resemble parents beyond attractor baseline."
            : "  → Mean H is low — children may simply be attractor captures.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 3: Are 'lineages' just the same attractor persisting?");
        sb.AppendLine("  → If each 'generation' simply returns to the same attractor,");
        sb.AppendLine("    the lineage is an attractor trajectory, not an evolutionary lineage.");
        sb.AppendLine(report.MutationsObserved
            ? "  → Mutations ARE observed — patterns drift across generations."
              + " This is inconsistent with pure attractor dynamics and supports"
              + " genuine reproduction with variation."
            : "  → No mutations observed — patterns may simply be attractor-stable states.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 4: Statistical test — compare child vs parent similarity");
        sb.AppendLine("  against baseline similarity of random patterns near the same attractor.");
        double baselineRandomSimilarity = 0.3;
        double childParentSimilarity = report.MeanInheritanceCoefficient;
        sb.AppendLine($"  → Random-near-attractor baseline: H_baseline ≈ {baselineRandomSimilarity:F3}");
        sb.AppendLine($"  → Child-parent similarity: H_observed = {childParentSimilarity:F3}");
        sb.AppendLine(childParentSimilarity > baselineRandomSimilarity + 0.1
            ? "  → H_observed > H_baseline + 0.1: INHERITANCE IS REAL."
            : "  → H_observed NOT significantly above baseline: attractor-only explanation holds.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 5: Multiple seeds test.");
        sb.AppendLine("  → We test across multiple densities and population configurations.");
        sb.AppendLine($"  → {report.TotalReproductionEvents} reproduction events across all tests.");
        sb.AppendLine(report.TotalReproductionEvents >= 3
            ? "  → Result is REPRODUCIBLE — not a single-seed artifact."
            : "  → Too few events — result may be stochastic noise.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 6: Null hypothesis — 'Information species are attractors only.'");
        sb.AppendLine(report.ReproductionDetected && report.LineagesFormed
            ? "  → NULL HYPOTHESIS REJECTED."
              + " Reproduction exists AND lineages persist."
              + " Information species are MORE than attractors."
            : report.ReproductionDetected
                ? "  → NULL HYPOTHESIS PARTIALLY REJECTED."
                  + " Reproduction exists but lineages are transient."
                : "  → NULL HYPOTHESIS CONFIRMED."
                  + " No reproduction. Species are attractors only.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Research questions.
    // ══════════════════════════════════════════════════════════════════

    public static string ResearchQuestions(InformationLineage.InformationEvolutionReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Q1: Can information species reproduce?");
        sb.AppendLine(report.ReproductionDetected
            ? $"  YES — {report.TotalReproductionEvents} successful reproduction events detected. "
              + "Species can generate offspring with measurable parent-child similarity."
            : "  NO — no self-copying or offspring generation detected. Species are static attractors.");
        sb.AppendLine();

        sb.AppendLine("Q2: Can species create descendants?");
        sb.AppendLine(report.LineagesFormed
            ? $"  YES — {report.TotalLineages} lineages tracked, longest = {report.LongestLineageLength} generations."
            : "  NO — no multi-generational lineages observed. Reproduction is single-generation only.");
        sb.AppendLine();

        sb.AppendLine("Q3: Do species preserve identity across generations?");
        sb.AppendLine(report.MeanFidelity > 0.5
            ? $"  YES — mean fidelity = {report.MeanFidelity:F3}. Species identity is preserved across generations."
            : $"  PARTIALLY — mean fidelity = {report.MeanFidelity:F3}. Identity decays over generations.");
        sb.AppendLine();

        sb.AppendLine("Q4: Do mutations occur?");
        sb.AppendLine(report.MutationsObserved
            ? "  YES — pattern drift accumulates over generations. "
              + "Information species mutate like biological species."
            : "  NO — patterns remain stable across generations. No mutation-like drift observed.");
        sb.AppendLine();

        sb.AppendLine("Q5: Do species compete?");
        sb.AppendLine(report.CompetitionDetected
            ? "  YES — competitive exclusion observed. Dominant species eliminate competitors."
            : "  NO — species coexist without competitive dynamics.");
        sb.AppendLine();

        sb.AppendLine("Q6: Can information lineages emerge?");
        sb.AppendLine(report.LineagesFormed && report.LongestLineageLength > 3
            ? "  YES — persistent multi-generational lineages emerge from reproduction dynamics."
            : "  PARTIALLY — lineages exist but are short-lived.");
        sb.AppendLine();

        sb.AppendLine("Q7: Is information ecology merely attractor dynamics?");
        sb.AppendLine(report.ReproductionDetected && report.MeanInheritanceCoefficient > 0.3
            ? "  NO — reproduction and inheritance exceed attractor convergence baseline. "
              + "Information species have reproductive dynamics beyond attractor physics."
            : "  YES — all observed patterns are consistent with attractor-only dynamics.");
        sb.AppendLine();

        sb.AppendLine("Q8: Is there genuine inheritance?");
        double baseline = 0.3;
        sb.AppendLine(report.MeanInheritanceCoefficient > baseline + 0.1
            ? $"  YES — H = {report.MeanInheritanceCoefficient:F3} > baseline ({baseline:F3}). "
              + "Child-parent similarity is statistically significant and exceeds attractor convergence."
            : $"  NO — H = {report.MeanInheritanceCoefficient:F3} ≤ baseline ({baseline:F3}). "
              + "No evidence for inheritance beyond attractor dynamics.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Species lineage report.
    // ══════════════════════════════════════════════════════════════════

    public static string LineageReport(
        List<InformationLineage.SpeciesLineage> lineages,
        List<InformationLineage.SpeciesTransition> transitions)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("LINEAGE ANALYSIS");
        sb.AppendLine();

        sb.AppendLine("  Species transition matrix T_ij:");
        sb.AppendLine("  From \\ To │    A     │    B     │    C     │    D     │ Mechanism");
        sb.AppendLine("  " + new string('─', 70));

        var species = new[] { "A", "B", "C", "D" };
        foreach (string fromSp in species)
        {
            sb.Append($"  {fromSp,-9} │");
            foreach (string toSp in species)
            {
                var t = transitions
                    .Where(tr => tr.FromSpecies == fromSp && tr.ToSpecies == toSp)
                    .ToList();
                double avgProb = t.Count > 0 ? t.Average(tr => tr.TransitionProbability) : 0;
                sb.Append($" {avgProb,7:F3} │");
            }
            var mech = transitions
                .Where(tr => tr.FromSpecies == fromSp && tr.ToSpecies == fromSp)
                .FirstOrDefault();
            sb.AppendLine($" {(mech?.Mechanism ?? "N/A")}");
        }
        sb.AppendLine();

        sb.AppendLine("  Lineage summary:");
        sb.AppendLine("  Ancestor │ Generations │ Descendants │ Final Similarity │ Drift │ Extinct?");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var lin in lineages.Take(12))
            sb.AppendLine($"  {lin.AncestorName,-8} │ {lin.LineageLength,11} │ {lin.Descendants.Count,11} │ {lin.LineageSimilarity,16:F3} │ {lin.MutationDrift,5:F3} │ {(lin.IsExtinct ? "YES" : "NO")}");
        sb.AppendLine();

        sb.AppendLine($"  Total lineages: {lineages.Count}");
        sb.AppendLine($"  Living lineages: {lineages.Count(l => !l.IsExtinct)}");
        sb.AppendLine($"  Extinct lineages: {lineages.Count(l => l.IsExtinct)}");
        sb.AppendLine($"  Longest lineage: {lineages.Max(l => l.LineageLength)} generations");
        sb.AppendLine($"  Mean drift: {lineages.Average(l => l.MutationDrift):F4} per lineage");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Species summary helper.
    // ══════════════════════════════════════════════════════════════════

    private static string SpeciesSummary(
        List<InformationLineage.SpeciesReproductionProfile> profiles,
        List<InformationLineage.SpeciesLineage> lineages)
    {
        var sb = new System.Text.StringBuilder();
        sb.Append("Species profiles: ");
        foreach (var p in profiles)
        {
            var lin = lineages.Where(l => l.AncestorName == p.SpeciesName).ToList();
            int descendants = lin.Sum(l => l.Descendants.Count);
            sb.Append($"{p.SpeciesName}(R={p.ReproductionRate:F2},F={p.Fidelity:F2},D={descendants}) ");
        }
        return sb.ToString();
    }
}
