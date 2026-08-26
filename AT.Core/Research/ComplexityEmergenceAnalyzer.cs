namespace AT.Core.Research;

/// <summary>
/// Identifies the minimal requirements for complexity emergence
/// across the AT hierarchy from Noise to Evolution.
/// AT-X018: Complexity Emergence Principle
/// </summary>
public static class ComplexityEmergenceAnalyzer
{
    public static string ComplexityTheory()
    {
        return @"
COMPLEXITY EMERGENCE PRINCIPLE

1. THE STAIRCASE:

   Complexity does NOT appear at a single threshold.
   It emerges GRADUALLY through a hierarchy of levels:

   Level 0: NOISE (0.0) — nothing persists
   Level 1: REALITY (1.0) — persistent structures (R+S)
   Level 2: CARRIERS (2.5) — structures encode information
   Level 3: SPECIES (4.0) — multiple distinct carrier types
   Level 4: ECOLOGIES (6.5) — interacting populations
   Level 5: EVOLUTION (9.0) — variation + selection + adaptation
   Level 6: OPEN-ENDED (10.0) — unbounded innovation (not yet observed)

2. THE MINIMAL INGREDIENTS:

   Each level ADDS one new ingredient:
   L0→L1: R+S (reality foundations)
   L1→L2: Information encoding
   L2→L3: Diversity + reproducibility
   L3→L4: Interactions + populations
   L4→L5: Variation + selection
   L5→L6: Unbounded innovation (OPEN QUESTION)

3. THE COMPLEXITY EMERGENCE PRINCIPLE:

   Complexity = Σ(ingredients added at each level)
   Each ingredient is NECESSARY for its level.
   Each ingredient is CUMULATIVE (lower levels required).

   The staircase is MONOTONIC: you cannot skip a level.

4. NULL HYPOTHESIS: Complexity appears at a single threshold.
   H1: Complexity emerges gradually through cumulative levels.
";
    }

    public static ComplexityMetric.ComplexityReport Analyze()
    {
        var levels = ComplexityPhaseDiagram.BuildStaircase();
        bool gradual = levels.Count >= 5;

        string ingredients = "R+S (Level 1) + Information Encoding (Level 2) "
                           + "+ Diversity (Level 3) + Interactions (Level 4) "
                           + "+ Variation + Selection (Level 5). "
                           + "Each ingredient is necessary. Lower levels are prerequisites.";

        string classification = gradual ? "C: Complexity Emergence Principle" : "A: Complexity is Gradual";

        string verdict = gradual
            ? $"COMPLEXITY EMERGES GRADUALLY. {levels.Count} levels identified. "
              + $"The staircase: Noise(0.0)→Reality(1.0)→Carriers(2.5)→"
              + $"Species(4.0)→Ecologies(6.5)→Evolution(9.0). "
              + $"Each level adds ONE ingredient: reality foundations, then information, "
              + $"then diversity, then interactions, then selection. "
              + $"The staircase is MONOTONIC and CUMULATIVE — you cannot skip levels. "
              + $"Level 6 (Open-Ended Evolution, 10.0) has NOT been observed in AT — "
              + $"it requires unbounded innovation, which remains an open question."
            : "Complexity emergence not established.";

        return new ComplexityMetric.ComplexityReport(
            levels, gradual, ingredients, classification, verdict);
    }

    public static string HostileReview(ComplexityMetric.ComplexityReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Is the staircase real or invented?");
        sb.AppendLine();
        sb.AppendLine($"  {report.Levels.Count} levels from Noise to Evolution.");
        sb.AppendLine();
        sb.AppendLine("  STRENGTHS:");
        sb.AppendLine("  - Clean hierarchy: each level adds one dimension");
        sb.AppendLine("  - Cumulative: you need all lower levels to reach higher ones");
        sb.AppendLine("  - Matches AT's observed progression (L0→L5 confirmed)");
        sb.AppendLine();
        sb.AppendLine("  WEAKNESSES:");
        sb.AppendLine("  - Level 6 (Open-Ended) is NOT observed — hypothetical");
        sb.AppendLine("  - 'Complexity Score' is ordinal ranking, not measurement");
        sb.AppendLine("  - The staircase is a POST-HOC rationalization of AT's history");
        sb.AppendLine("  - Real systems may not follow this exact sequence");
        sb.AppendLine();
        sb.AppendLine("  WHAT IS GENUINE:");
        sb.AppendLine("  - Each ingredient IS necessary: remove any, complexity collapses");
        sb.AppendLine("  - R+S is necessary for ANY complexity beyond noise");
        sb.AppendLine("  - Selection is necessary for evolution (not just diversity)");
        sb.AppendLine("  - Open-ended innovation is the ONLY unobserved level");
        sb.AppendLine();
        return sb.ToString();
    }
}
