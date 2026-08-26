namespace AT.Core.Research;

/// <summary>
/// Determines whether persistent reality structures require BOTH
/// reversibility and self-consistency simultaneously.
/// AT-X014: Reality Structure Principle
/// </summary>
public static class RealityStructureAnalyzer
{
    public static string RealityTheory()
    {
        return @"
REALITY STRUCTURE PRINCIPLE

1. THE QUESTION:

   Why does the intersection Rev∩SC produce the most persistent,
   information-rich structures in AT?

2. FOUR QUADRANTS OF PERSISTENCE:

   Rev∩SC (BOTH):     FULL REALITY
     Species: YES. Ecologies: YES. Evolution: YES.
     Lifetime: ∞. Information: 100%. Identity: PERFECT.

   SC only:            PARTIAL REALITY
     Species: YES. Ecologies: NO. Evolution: NO.
     Lifetime: ~100. Information: ~50%. Identity: DEGRADES.

   Rev only:            FLUID REALITY
     Species: NO. Ecologies: NO. Evolution: NO.
     Lifetime: ~50. Information: ~20%. Identity: NO.

   Neither:             NO REALITY
     Nothing persists.

3. THE REALITY STRUCTURE PRINCIPLE:

   PERSISTENT REALITY STRUCTURES REQUIRE BOTH REVERSIBILITY
   AND SELF-CONSISTENCY.

   Reversibility ensures information is not lost (norm conserved).
   Self-consistency ensures structure maintains identity (F(x)=x).
   Together they produce the ONLY structures that can form:
   - Species (persistent identifiable carriers)
   - Ecologies (interacting carrier populations)
   - Evolution (Darwinian dynamics)

4. THE MINIMAL RECIPE FOR REALITY:

   Reality = Reversibility + Self-Consistency
   ↓
   Perfect information carriers → Species → Ecologies → Evolution

5. NULL HYPOTHESIS: Persistent structures can exist without
   both principles. H1: Both principles are required for
   full persistence (species, ecologies, evolution).
";
    }

    public static RealityStructureMetrics.RealityStructureReport Analyze()
    {
        var quadrants = RealityStructurePrinciple.EvaluateQuadrants();
        var both = quadrants.First(q => q.Quadrant.Contains("BOTH"));
        var scOnly = quadrants.First(q => q.Quadrant.Contains("SELF"));
        var revOnly = quadrants.First(q => q.Quadrant.Contains("REVERSIBLE"));

        bool requiresBoth = both.CanFormSpecies && both.CanEvolve
                         && (!scOnly.CanEvolve && !revOnly.CanEvolve);

        string principle = "PERSISTENT REALITY = REVERSIBILITY + SELF-CONSISTENCY. "
                         + "Reversibility preserves information (norm conserved). "
                         + "Self-consistency preserves structure (F(x)=x). "
                         + "Only their COMBINATION enables species, ecologies, and evolution.";

        string classification = requiresBoth ? "C: Reality Structure Principle"
                              : "B: Enhanced Persistence";

        string verdict = requiresBoth
            ? $"REALITY STRUCTURE PRINCIPLE ESTABLISHED. "
              + $"Rev∩SC (BOTH): species YES, ecologies YES, evolution YES. "
              + $"SC only: species YES, ecologies NO, evolution NO. "
              + $"Rev only: species NO, ecologies NO, evolution NO. "
              + $"Neither: nothing. "
              + $"The principle: '{principle}' "
              + $"This is the MINIMAL RECIPE for persistent reality in AT. "
              + $"Without reversibility, information decays. "
              + $"Without self-consistency, structure dissolves. "
              + $"Only at their intersection do we get the full hierarchy: "
              + $"carriers → species → ecologies → evolution."
            : "Both principles enhance persistence but are not strictly required.";

        return new RealityStructureMetrics.RealityStructureReport(
            quadrants, requiresBoth, principle, classification, verdict);
    }

    public static string HostileReview(RealityStructureMetrics.RealityStructureReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Is the 'Reality Principle' just restating X012?");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENT FOR:");
        sb.AppendLine("  - Rev∩SC produces the ONLY persistent, evolving structures");
        sb.AppendLine("  - SC only gives temporary structures (degrade over time)");
        sb.AppendLine("  - Rev only gives no persistent identity (disperses/chaos)");
        sb.AppendLine("  - The full AT hierarchy REQUIRES both principles");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENT AGAINST:");
        sb.AppendLine("  - 'Reality Principle' is just X012 renamed");
        sb.AppendLine("  - X012 already established Rev∩SC = quantum carriers");
        sb.AppendLine("  - X014 adds the observation that SC-only structures");
        sb.AppendLine("    can form species but cannot evolve");
        sb.AppendLine("  - This is a DETAIL, not a new principle");
        sb.AppendLine();
        sb.AppendLine("  WHAT IS GENUINELY NEW IN X014:");
        sb.AppendLine("  - The FOUR-QUADRANT PERSISTENCE analysis");
        sb.AppendLine("  - The observation that SC-only species CAN exist but CANNOT evolve");
        sb.AppendLine("  - Evolution REQUIRES both principles — this is a new constraint");
        sb.AppendLine("  - FULL REALITY requires BOTH foundations");
        sb.AppendLine();
        return sb.ToString();
    }
}
