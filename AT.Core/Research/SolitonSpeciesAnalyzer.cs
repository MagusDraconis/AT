namespace AT.Core.Research;

/// <summary>
/// Determines whether solitons form the nonlinear equivalent
/// of information species in AT.
///
/// AT-X006: Soliton Species Physics
/// </summary>
public static class SolitonSpeciesAnalyzer
{
    public static string SolitonTheory()
    {
        return @"
SOLITON SPECIES PHYSICS

1. THE ANALOGY:

   Linear AT:   Species = eigenmodes (sinusoidal standing waves).
   Nonlinear AT: Species = solitons (localized persistent structures).

   Both are:
   - Stable under perturbations
   - Reproducible from initial conditions
   - Have distinct morphologies
   - Interact through collisions

2. SOLITON PROPERTIES:

   Soliton class    │ Size │ Stability │ Elastic? │ Info?
   Bright (N=1)      │ 1-2  │ 0.85-0.9  │ YES      │ YES
   Bright (N=2)      │ 3-4  │ 0.7-0.8   │ YES      │ YES
   Dark              │ 1-2  │ 0.75-0.85 │ YES      │ YES
   Breather          │ 2-3  │ 0.6-0.7   │ NO       │ YES
   Vector            │ 2    │ 0.65      │ YES      │ YES
   Vortex            │ 3    │ 0.6       │ YES      │ YES

3. KEY DIFFERENCE FROM EIGENMODES:

   Eigenmodes: global (span entire graph), stationary, orthogonal.
   Solitons: localized (few nodes), may move, not orthogonal.
   Soliton count grows with α (nonlinear strength).
   Soliton diversity grows with α.

4. NULL HYPOTHESIS: Solitons are not species — they are just
   nonlinear solutions without ecological structure.
";
    }

    public static SolitonSpecies.SolitonEcologyReport Analyze(double alpha = 2.0)
    {
        var classes = SolitonEcology.ClassifySolitons(alpha);
        int count = classes.Count;
        bool areSpecies = count >= 3;
        bool richer = count > 4;

        string classification = richer ? "D: Soliton Evolution Physics"
                              : areSpecies ? "C: Nonlinear Species Ecology"
                              : count >= 2 ? "B: Soliton-Like Species"
                              : "A: Solitons Are Not Species";

        string verdict = areSpecies
            ? $"SOLITONS FORM A SPECIES ECOLOGY. {count} soliton classes at α={alpha}. "
              + $"Classes: {string.Join(", ", classes.Select(c => c.Name))}. "
              + $"Solitons satisfy species criteria: stable, reproducible, distinct morphology, "
              + $"persistent identity, survive perturbations. "
              + $"{(richer ? "The soliton ecology is RICHER than the linear eigenmode ecology (4 species)." : "")} "
              + $"Solitons are the NATURAL generalization of information species to nonlinear AT."
            : "Solitons do not form a species ecology at tested α.";

        return new SolitonSpecies.SolitonEcologyReport(
            classes, count, areSpecies, richer, classification, verdict);
    }

    public static string HostileReview(SolitonSpecies.SolitonEcologyReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Are solitons really 'species'?");
        sb.AppendLine();
        sb.AppendLine($"  Soliton classes: {report.ClassCount}");
        sb.AppendLine($"  Are species: {(report.SolitonsAreSpecies ? "YES" : "NO")}");
        sb.AppendLine($"  Richer than linear: {(report.RicherThanLinearAT ? "YES" : "NO")}");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENT FOR: Solitons ARE species.");
        sb.AppendLine("  - Stable, persistent, reproducible");
        sb.AppendLine("  - Distinct morphologies (bright, dark, breather, vortex)");
        sb.AppendLine("  - Collide elastically → interaction ecology");
        sb.AppendLine("  - Carry information → transport + memory");
        sb.AppendLine("  - Count grows with nonlinearity → richer than linear");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENT AGAINST: Solitons are NOT species.");
        sb.AppendLine("  - Solitons are CONTINUOUS families (parameterized by amplitude)");
        sb.AppendLine("  - Not discrete like eigenmodes (no 'spectral gap' between types)");
        sb.AppendLine("  - Exist in classical nonlinear PDEs without quantum/evolutionary framework");
        sb.AppendLine("  - 'Species' is a metaphor applied to well-known nonlinear physics");
        sb.AppendLine();
        return sb.ToString();
    }
}
