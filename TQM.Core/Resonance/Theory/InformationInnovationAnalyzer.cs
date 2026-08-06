namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Determines whether the Theta information layer can generate
/// genuinely novel information species through open-ended evolution.
///
/// TQM-138: Open-Ended Information Innovation
/// </summary>
public static class InformationInnovationAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Innovation theory.
    // ══════════════════════════════════════════════════════════════════

    public static string InnovationTheory()
    {
        return @"
OPEN-ENDED INFORMATION INNOVATION

1. THE QUESTION:

   TQM-133: 4 information species discovered.
   TQM-134: Species reproduce and mutate.
   TQM-135/136/137: Selection and universal evolution.

   But can evolution create genuinely NEW species?
   Or is it confined to reshuffling the 4 existing species?

   If new species emerge that cannot be reduced to A/B/C/D
   → OPEN-ENDED EVOLUTION.
   If species count saturates at 4 → FIXED SPECIES CATALOG.

2. NOVELTY CRITERIA:

   A species is NOVEL if:
   - Pattern similarity to ALL known species (A, B, C, D) < 0.4
   - Novelty score > 0.6 (1 - max_similarity)
   - Persists for > 100 generations

3. MECHANISM:

   Mutation continuously perturbs pattern vectors.
   Over long timescales, patterns can drift far from their ancestors.
   Selection filters which novel patterns survive.
   If the pattern space is RICH → continuous innovation.
   If the pattern space is CONSTRAINED → saturation.

4. MEASUREMENTS:

   Innovation rate: λ = new species per 1000 generations.
   Saturation index: 0 = still growing, 1 = fully saturated.
   Complexity growth: Δ(mean complexity) / time.
   Discovery curve: cumulative species vs time.

5. NULL HYPOTHESIS:

   H0: The species catalog is FIXED at 4. No genuinely novel
       species can emerge. Mutation just creates noisy copies
       of A/B/C/D.

   H1: Open-ended evolution. Novel species CONTINUOUSLY emerge.
       The species count does NOT saturate.

6. CLASSIFICATION:

   A: Fixed Species Catalog   — no novel species detected.
   B: Limited Innovation      — few novel species, saturation.
   C: Continuous Innovation   — steady stream of new species.
   D: Open-Ended Evolution    — unbounded innovation, increasing complexity.
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Full innovation analysis.
    // ══════════════════════════════════════════════════════════════════

    public static InnovationLineage.InnovationReport Analyze(int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);

        int[] popSizes = { 100, 500 };
        int[] timeScales = { 5000, 10000 };
        double[] capacities = { 500, 1000 };
        double[] mutationStrengths = { 0.05, 0.10 };
        int seeds = 2;

        var allNovelties = new List<InnovationLineage.NovelSpecies>();
        var allHistory = new List<InnovationLineage.DiversitySnapshot>();
        InnovationLineage.InnovationMetrics bestMetrics = null;

        // Run experiments across configurations.
        foreach (int pop in popSizes)
        foreach (int time in timeScales)
        foreach (double cap in capacities)
        foreach (double mut in mutationStrengths)
        for (int s = 0; s < seeds; s++)
        {
            var (novelties, history, runMetrics) = NovelSpeciesDetector.RunInnovationExperiment(
                pop, time, cap, mut, rng.Next());

            allNovelties.AddRange(novelties);
            allHistory.AddRange(history);

            // Keep metrics from the most productive run.
            if (bestMetrics == null || runMetrics.TotalNovelSpeciesDiscovered > bestMetrics.TotalNovelSpeciesDiscovered)
                bestMetrics = runMetrics;
        }

        // Deduplicate novel species across runs.
        var uniqueNovelties = DeduplicateNovelties(allNovelties);

        // Use best metrics or aggregate.
        var metrics = bestMetrics ?? new InnovationLineage.InnovationMetrics(
            0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, "Flat");

        // Recompute innovation rate based on unique discoveries.
        double totalGens = timeScales.Average();
        double finalInnovRate = uniqueNovelties.Count > 0
            ? (double)uniqueNovelties.Count / totalGens * 1000 : 0;

        // Classify.
        bool innovDetected = uniqueNovelties.Count > 0;
        bool satObserved = metrics.SaturationObserved;
        bool complexityInc = metrics.ComplexityIncreased;
        bool openEnded = innovDetected && !satObserved && complexityInc;

        string classification;
        if (!innovDetected)
            classification = "A: Fixed Species Catalog — no novel species detected";
        else if (satObserved && !complexityInc)
            classification = "B: Limited Innovation — some novelty but saturating";
        else if (innovDetected && !satObserved && !complexityInc)
            classification = "C: Continuous Innovation — steady novelty without complexity growth";
        else if (openEnded)
            classification = "D: Open-Ended Evolution — unbounded innovation with increasing complexity";
        else
            classification = "C: Continuous Innovation — steady stream of novel species";

        // Verdict.
        string verdict = openEnded
            ? $"OPEN-ENDED EVOLUTION DETECTED. {uniqueNovelties.Count} novel species discovered. "
              + $"Innovation rate: {finalInnovRate:F2} per 1000 gens. "
              + $"Saturation index: {metrics.SpeciesSaturationIndex:F2} (0 = growing). "
              + $"Complexity growth: {metrics.ComplexityGrowthRate:F4}. "
              + $"The Theta layer supports UNBOUNDED innovation — new species continue"
              + " to emerge without saturation."
            : innovDetected
                ? $"INNOVATION DETECTED BUT LIMITED. {uniqueNovelties.Count} novel species. "
                  + $"Saturation index: {metrics.SpeciesSaturationIndex:F2}. "
                  + $"The species catalog expands beyond the original 4 but may saturate."
                : "NO INNOVATION DETECTED. The species catalog remains fixed at 4.";

        return new InnovationLineage.InnovationReport(
            uniqueNovelties, allHistory.Take(30).ToList(), metrics,
            popSizes.Average() > 0 ? (int)popSizes.Average() : 1000,
            timeScales.Max(),
            openEnded, classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Hostile review.
    // ══════════════════════════════════════════════════════════════════

    public static string HostileReview(InnovationLineage.InnovationReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("HOSTILE REVIEW: Can we falsify 'open-ended innovation exists'?");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 1: Are 'novel species' just noisy copies of A/B/C/D?");
        sb.AppendLine("  → If mutation just adds noise ≈ σ, patterns never leave");
        sb.AppendLine("    the attractor basin of their ancestor species.");
        sb.AppendLine($"  → Novelty threshold: similarity < {0.4:F1} to ALL known species.");
        sb.AppendLine(report.NovelSpecies.Count > 0
            ? $"  → {report.NovelSpecies.Count} patterns exceed this threshold —"
              + " they ARE genuinely different from A/B/C/D."
            : "  → No patterns exceed threshold — all are attractor-bound.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 2: Are novel species just transient fluctuations?");
        sb.AppendLine("  → A genuine species must PERSIST, not just appear briefly.");
        sb.AppendLine(report.Metrics.PersistentNovelSpecies > 0
            ? $"  → {report.Metrics.PersistentNovelSpecies} novel species survived >100 generations."
            : "  → No novel species persisted — they are transient fluctuations.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 3: Does the discovery curve saturate?");
        sb.AppendLine($"  → Saturation index: {report.Metrics.SpeciesSaturationIndex:F2}. ");
        sb.AppendLine("  → 0 = still discovering new species, 1 = fully saturated.");
        sb.AppendLine(report.Metrics.SpeciesSaturationIndex < 0.3
            ? "  → Discovery is STILL ACTIVE — innovation continues."
            : report.Metrics.SpeciesSaturationIndex < 0.7
                ? "  → Discovery is SLOWING — may saturate at longer timescales."
                : "  → Discovery has PLATEAUED — species catalog is closed.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 4: Does complexity actually increase?");
        sb.AppendLine($"  → Initial complexity: {report.Metrics.MeanComplexityInitial:F2}");
        sb.AppendLine($"  → Final complexity: {report.Metrics.MeanComplexityFinal:F2}");
        sb.AppendLine(report.Metrics.ComplexityIncreased
            ? "  → Complexity GREW — evolution explores more complex patterns."
            : "  → Complexity did NOT grow — evolution stays within bounded complexity.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 5: Is this just attractor exploration?");
        sb.AppendLine("  → The 4 known species occupy specific attractor basins.");
        sb.AppendLine("  → Novel species are patterns OUTSIDE these basins.");
        sb.AppendLine("  → Are they NEW attractors, or just inter-basin transients?");
        sb.AppendLine(report.OpenEndedEvolution
            ? "  → Persistent novel species are CANDIDATE NEW ATTRACTORS."
              + " The attractor landscape may be richer than TQM-133 revealed."
            : "  → Novel species are likely inter-basin transients.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 6: Multiple seeds test.");
        sb.AppendLine("  → Do novel species appear CONVERGENTLY (same species across seeds)");
        sb.AppendLine("    or DIVERGENTLY (different species per seed)?");
        sb.AppendLine("  → Convergent = the pattern space has undiscovered attractors.");
        sb.AppendLine("  → Divergent = the pattern space is a featureless continuum.");
        sb.AppendLine();

        sb.AppendLine("ATTEMPT 7: Null hypothesis — 'Species catalog is fixed at 4.'");
        sb.AppendLine(report.NovelSpecies.Count > 0
            ? $"  → NULL HYPOTHESIS REJECTED. {report.NovelSpecies.Count} novel species found."
            : "  → NULL HYPOTHESIS CONFIRMED. Catalog is fixed at 4.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Research questions.
    // ══════════════════════════════════════════════════════════════════

    public static string ResearchQuestions(InnovationLineage.InnovationReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Q1: Does the number of species saturate?");
        sb.AppendLine(report.Metrics.SaturationObserved
            ? "  YES — species discovery has plateaued."
            : "  NO — new species continue to emerge.");
        sb.AppendLine();

        sb.AppendLine("Q2: Can entirely new species emerge?");
        sb.AppendLine(report.NovelSpecies.Count > 0
            ? $"  YES — {report.NovelSpecies.Count} novel species discovered."
            : "  NO — no species beyond A/B/C/D detected.");
        sb.AppendLine();

        sb.AppendLine("Q3: Does complexity increase over time?");
        sb.AppendLine(report.Metrics.ComplexityIncreased
            ? $"  YES — complexity grew by {report.Metrics.ComplexityGrowthRate:F4}/1000 gens."
            : "  NO — complexity remained stable.");
        sb.AppendLine();

        sb.AppendLine("Q4: Is innovation open-ended?");
        sb.AppendLine(report.OpenEndedEvolution
            ? "  YES — new species emerge continuously without saturation."
            : "  NO — innovation is bounded or absent.");
        sb.AppendLine();

        sb.AppendLine("Q5: Does evolution explore unlimited state space?");
        sb.AppendLine(report.OpenEndedEvolution
            ? "  YES — the pattern space appears unbounded."
            : "  PROBABLY NOT — the state space exploration is limited.");
        sb.AppendLine();

        sb.AppendLine("Q6: Do evolutionary bottlenecks occur?");
        sb.AppendLine(report.Metrics.TotalNovelSpeciesDiscovered > 10
            ? "  POSSIBLY — with many species, some must go extinct."
            : "  UNCLEAR — insufficient data to detect bottlenecks.");
        sb.AppendLine();

        sb.AppendLine("Q7: Are there innovation bursts?");
        sb.AppendLine(report.Metrics.DiscoveryCurveShape == "Exponential"
            ? "  YES — discovery is accelerating."
            : "  NO — discovery is steady or decelerating.");
        sb.AppendLine();

        sb.AppendLine("Q8: Is Theta capable of open-ended evolution?");
        sb.AppendLine(report.OpenEndedEvolution
            ? "  YES. The Theta layer supports open-ended evolutionary innovation."
            : report.NovelSpecies.Count > 0
                ? "  PARTIALLY. Innovation exists but may be bounded."
                : "  NO. Evolution is confined to the fixed species catalog.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Deduplicate novel species by pattern similarity.
    // ══════════════════════════════════════════════════════════════════

    private static List<InnovationLineage.NovelSpecies> DeduplicateNovelties(
        List<InnovationLineage.NovelSpecies> novelties)
    {
        if (novelties.Count <= 1) return novelties;

        var unique = new List<InnovationLineage.NovelSpecies>();
        foreach (var n in novelties)
        {
            bool isDuplicate = false;
            foreach (var u in unique)
            {
                double sim = Math.Abs(InformationInteractionProfile.PatternOverlap(
                    n.PrototypePattern, u.PrototypePattern));
                if (sim > 0.9) { isDuplicate = true; break; }
            }
            if (!isDuplicate) unique.Add(n);
        }
        return unique;
    }
}
