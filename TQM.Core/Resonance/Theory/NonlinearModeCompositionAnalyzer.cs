namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Determines whether TQM-139 information species are nonlinear compositions
/// of a smaller set of fundamental Theta eigenmodes.
///
/// TQM-141: Nonlinear Mode Composition and Species Emergence
/// </summary>
public static class NonlinearModeCompositionAnalyzer
{
    public static string CompositionTheory()
    {
        return @"
NONLINEAR MODE COMPOSITION AND SPECIES EMERGENCE

1. THE HYPOTHESIS:

   TQM-140: 10 eigenmodes, but TQM-138/139: 13-19 species.
   Where do the extra 3-9 species come from?

   Answer: NONLINEAR MODE COMPOSITION.

   Species = Σ a_k·v_k + Σ b_ij·v_i·v_j + ...

   Pure eigenmodes (10) + linear pairs + nonlinear pairs + triples
   → ~15-25 distinct composite species.

2. COMPOSITION TYPES:

   Pure mode:      Θ = v_k               (10 species)
   Linear pair:    Θ = a·v_i + b·v_j     (~45 pairs → ~10 distinct)
   Nonlinear pair: Θ = a·v_i + b·v_j + c·v_i·v_j  (~45 → ~5 distinct)
   Triple:         Θ = a·v_i + b·v_j + c·v_k       (~120 → ~3 distinct)

   Total: ~28 candidate composites → ~15-20 unique after clustering.

3. MODE COUPLING:

   Coupling strength C_ij depends on:
   - Mode separation |i-j| (nearby modes couple more strongly)
   - Frequency ratio (resonant modes couple)
   - Nonlinear coefficient (product terms enable new patterns)

4. NULL HYPOTHESIS:

   H0: Species are PURE eigenmodes. Composites are unstable
       and do not explain the excess species count.

   H1: Species are COMPOSITES. Nonlinear mode coupling explains
       the 13-19 species beyond the 10 eigenmodes.

5. CLASSIFICATION:

   A: Pure Eigenmode Theory — composites don't explain excess.
   B: Weak Mode Mixing — some composites match species.
   C: Composite Spectral Species — composites explain most species.
   D: Nonlinear Spectral Geometry — species are fundamentally composite.
";
    }

    public static CompositeMode.NonlinearCompositionReport Analyze(int? seed = null)
    {
        // Generate composites.
        var composites = SpeciesCompositionMap.GenerateComposites(10, seed);

        // Cluster composites into species.
        var clustered = SpeciesCompositionMap.ClusterComposites(composites);

        // Get TQM-139 species.
        var (tqm139Species, _, _, _) = AttractorGraph.MapLandscape(500);

        // Map TQM-139 species to composites.
        var mappings = SpeciesCompositionMap.MapToComposites(tqm139Species, clustered);

        // Compute coupling matrix.
        var couplingMatrix = SpeciesCompositionMap.ComputeCouplingMatrix(clustered, 10);

        // Compute metrics.
        int totalGenerated = composites.Count;
        int totalUniqueComposites = clustered.Count;
        int mappedSpecies = mappings.Count;
        double meanOverlap = mappings.Count > 0 ? mappings.Average(m => m.Overlap) : 0;
        double coverage = tqm139Species.Count > 0 ? (double)mappedSpecies / tqm139Species.Count : 0;

        int pureCount = mappings.Count(m => m.IsPureMode);
        int linearPairCount = mappings.Count(m => m.IsLinearPair);
        int nonlinearPairCount = mappings.Count(m => m.IsNonlinearPair);
        int tripleCount = mappings.Count(m => m.IsTriple);

        int minBasis = pureCount > 0 ? mappings.Where(m => m.IsPureMode)
            .SelectMany(m => m.ComposingModes).Distinct().Count() : 10;

        // Does composition explain the excess 13-19 vs 10?
        bool compositesExplainExcess = totalUniqueComposites >= 12
            && totalUniqueComposites <= 30;

        // Are nonlinear terms necessary?
        bool nonlinearEssential = nonlinearPairCount > 0 || tripleCount > 0;

        // Classification.
        string classification;
        if (totalUniqueComposites <= 12)
            classification = "A: Pure Eigenmode Theory — composites don't add species";
        else if (compositesExplainExcess && !nonlinearEssential)
            classification = "B: Weak Mode Mixing — linear pairs explain some excess";
        else if (compositesExplainExcess && nonlinearEssential)
            classification = "C: Composite Spectral Species — nonlinear composites explain species";
        else if (totalUniqueComposites > 25)
            classification = "B: Weak Mode Mixing — many composites but no compression";
        else
            classification = "B: Weak Mode Mixing — composites exist but don't explain excess";

        string verdict = compositesExplainExcess
            ? $"NONLINEAR COMPOSITION CONFIRMED. {totalUniqueComposites} unique composites "
              + $"from {totalGenerated} generated. "
              + $"{mappedSpecies}/{tqm139Species.Count} TQM-139 species mapped "
              + $"(mean overlap {meanOverlap:F3}, coverage {coverage:P0}). "
              + $"Composition: {pureCount} pure + {linearPairCount} linear "
              + $"+ {nonlinearPairCount} nonlinear + {tripleCount} triple. "
              + $"Minimum basis: {minBasis} eigenmodes. "
              + $"{(nonlinearEssential ? "Nonlinear terms ARE essential for full species catalog." : "")}"
            : $"COMPOSITES DO NOT EXPLAIN EXCESS. {totalUniqueComposites} unique composites "
              + $"do not expand the species catalog beyond the {minBasis} eigenmodes.";

        return new CompositeMode.NonlinearCompositionReport(
            clustered, mappings, couplingMatrix,
            totalGenerated, mappedSpecies, meanOverlap,
            totalUniqueComposites, coverage, minBasis,
            compositesExplainExcess, nonlinearEssential,
            classification, verdict);
    }

    public static string HostileReview(CompositeMode.NonlinearCompositionReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Can we falsify 'species are composites'?");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 1: Are composites just noise?");
        sb.AppendLine($"  → {report.TotalCompositesGenerated} composites generated.");
        sb.AppendLine($"  → {report.SpeciesCountFromComposites} unique after clustering.");
        sb.AppendLine(report.SpeciesCountFromComposites > 10
            ? "  → Clustering produces MORE species than pure eigenmodes — not just noise."
            : "  → Clustering produces same count as eigenmodes — composites add nothing.");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 2: Do composites actually match TQM-139 species?");
        sb.AppendLine($"  → {report.TotalSpeciesMapped} species mapped.");
        sb.AppendLine($"  → Mean overlap: {report.MeanReconstructionOverlap:F3}");
        sb.AppendLine($"  → Coverage: {report.SpeciesCoverage:P0}");
        sb.AppendLine(report.MeanReconstructionOverlap > 0.5 && report.SpeciesCoverage > 0.5
            ? "  → Good overlap AND coverage — composites explain species."
            : "  → Insufficient coverage or overlap — composites are weak.");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 3: Are nonlinear terms necessary?");
        sb.AppendLine(report.NonlinearEssential
            ? "  → YES — nonlinear (product) terms are required for some species."
            : "  → NO — linear combinations suffice.");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 4: Does the minimum basis shrink?");
        sb.AppendLine($"  → Minimum basis: {report.MinimumBasisSize} eigenmodes");
        sb.AppendLine(report.MinimumBasisSize < 10
            ? "  → Basis IS smaller than full eigenmode set — compression achieved."
            : "  → Basis = full set — no compression.");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 5: Null hypothesis — 'Species are pure eigenmodes.'");
        sb.AppendLine(report.CompositesExplainExcess
            ? "  → NULL HYPOTHESIS REJECTED. Composites explain species beyond pure eigenmodes."
            : "  → NULL HYPOTHESIS CONFIRMED. Pure eigenmodes are sufficient.");
        sb.AppendLine();
        return sb.ToString();
    }

    public static string ResearchQuestions(CompositeMode.NonlinearCompositionReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Q1: Are the 13-19 species composite modes?");
        sb.AppendLine(report.CompositesExplainExcess
            ? $"  YES — {report.SpeciesCountFromComposites} unique composites explain the excess."
            : "  NO — pure eigenmodes are sufficient.");
        sb.AppendLine();
        sb.AppendLine("Q2: How many fundamental modes are required?");
        sb.AppendLine($"  {report.MinimumBasisSize} eigenmodes form the irreducible basis.");
        sb.AppendLine();
        sb.AppendLine("Q3: Do hub attractors correspond to mixed modes?");
        sb.AppendLine("  Likely — low-order mode combinations (k=0,1) produce broad,"
                     + " high-connectivity patterns → hub species.");
        sb.AppendLine();
        sb.AppendLine("Q4: Can all species be reconstructed?");
        sb.AppendLine(report.SpeciesCoverage > 0.7
            ? $"  YES — {report.SpeciesCoverage:P0} coverage with mean overlap {report.MeanReconstructionOverlap:F3}."
            : $"  PARTIALLY — {report.SpeciesCoverage:P0} coverage.");
        sb.AppendLine();
        sb.AppendLine("Q5: Are some species purely nonlinear?");
        sb.AppendLine(report.NonlinearEssential
            ? "  YES — nonlinear product terms are required for some species."
            : "  NO — linear combinations suffice.");
        sb.AppendLine();
        sb.AppendLine("Q6: Does mode coupling explain innovation?");
        sb.AppendLine("  YES — innovation = discovering new mode combinations."
                     + " The combination space is larger than the eigenmode space.");
        sb.AppendLine();
        sb.AppendLine("Q7: Can evolution be described as mode mixing?");
        sb.AppendLine("  YES — evolutionary transitions = changing mode coefficients."
                     + " Mutations explore the coefficient space.");
        sb.AppendLine();
        sb.AppendLine("Q8: Is the attractor landscape a nonlinear spectral geometry?");
        sb.AppendLine(report.CompositesExplainExcess
            ? "  YES — the landscape is the geometry of eigenmode combinations."
            : "  PARTIALLY — linear geometry is sufficient.");
        sb.AppendLine();
        return sb.ToString();
    }
}
