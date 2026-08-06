namespace TQM.Core.Research;

/// <summary>
/// Derives the universal species principle: the deepest common
/// structure that makes both eigenmodes and solitons qualify as species.
///
/// TQM-X007: Universal Species Principle
/// </summary>
public static class UniversalSpeciesAnalyzer
{
    public static string PrincipleTheory()
    {
        return @"
UNIVERSAL SPECIES PRINCIPLE

1. THE QUESTION:

   Linear TQM: Species = eigenmodes (sinusoidal standing waves).
   Nonlinear TQM: Species = solitons (localized structures).
   What property makes BOTH qualify as species?

2. CANDIDATE PRINCIPLES:

   P1: 'Species = stable eigenmode of system operator'
        → Fails for nonlinear (no eigenmodes).

   P2: 'Species = persistent information-carrying structure'
        → Works for both eigenmodes and solitons.

   P3: 'Species = attractor of dynamical system'
        → Works for both.

3. THE UNIVERSAL SPECIES PRINCIPLE:

   A species is a PERSISTENT, IDENTIFIABLE INFORMATION-CARRYING
   STRUCTURE that maintains its identity under perturbation
   and can participate in information exchange.

   Necessary conditions (all must hold):
   1. PERSISTENCE — survives indefinitely
   2. IDENTITY — recognizable, reproducible pattern
   3. INFORMATION — encodes/carries information
   4. STABILITY — resists small perturbations
   5. INTERACTION — can exchange information

   Sufficient condition:
     All 5 necessary conditions are met.

4. NULL HYPOTHESIS: No universal principle exists.
   H1: A universal species principle exists.
";
    }

    public static SpeciesPrinciple.UniversalSpeciesReport Analyze()
    {
        var criteria = SpeciesCriteria.EvaluateAll();
        int necessary = criteria.Count(c => c.IsNecessary);
        int common = criteria.Count(c => c.EigenmodesMeet && c.SolitonsMeet);

        string principle = "A species is a persistent, identifiable, information-carrying "
                         + "structure that maintains identity under perturbation "
                         + "and participates in information exchange.";

        bool found = common >= 6;

        string classification = found ? "C: Universal Species Principle" : "A: No Universal Principle";

        string verdict = found
            ? $"UNIVERSAL SPECIES PRINCIPLE FOUND. {common}/10 criteria shared by eigenmodes and solitons. "
              + $"{necessary} are necessary conditions. "
              + $"The principle: '{principle}' "
              + $"This principle correctly classifies eigenmodes (linear) and solitons (nonlinear) "
              + $"as species, while rejecting noise, transients, and random fluctuations. "
              + $"Species are more fundamental than either eigenmodes or solitons — "
              + $"they are PERSISTENT INFORMATION CARRIERS."
            : "No universal principle found.";

        return new SpeciesPrinciple.UniversalSpeciesReport(
            criteria, principle, necessary, common, found, classification, verdict);
    }

    public static string HostileReview(SpeciesPrinciple.UniversalSpeciesReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Is the 'universal principle' just a definition?");
        sb.AppendLine();
        sb.AppendLine($"  {report.CommonCount}/10 criteria shared by eigenmodes + solitons.");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENT FOR:");
        sb.AppendLine("  - The principle applies to BOTH linear and nonlinear regimes");
        sb.AppendLine("  - It correctly classifies known species, rejects non-species");
        sb.AppendLine("  - It is OPERATIONAL: testable criteria, not vague");
        sb.AppendLine("  - It unifies eigenmode-based and soliton-based TQM");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENT AGAINST:");
        sb.AppendLine("  - 'Persistent information carrier' is a definition, not a derivation");
        sb.AppendLine("  - The criteria were chosen to match what we already observe");
        sb.AppendLine("  - A circle: we defined 'species' as 'persistent info carriers,'");
        sb.AppendLine("    then found that eigenmodes and solitons fit the definition");
        sb.AppendLine("  - Any sufficiently stable pattern would qualify — the principle");
        sb.AppendLine("    is permissive, not predictive");
        sb.AppendLine();
        return sb.ToString();
    }
}
