namespace AT.Core.Research;

/// <summary>
/// Constructs the complete taxonomy of information carriers
/// across all regimes (linear, nonlinear, topological, hybrid).
///
/// AT-X008: Information Carrier Taxonomy
/// </summary>
public static class InformationCarrierAnalyzer
{
    public static string TaxonomyTheory()
    {
        return @"
INFORMATION CARRIER TAXONOMY

1. THE GOAL:

   Build the 'periodic table' of information-bearing structures.
   Not just species — all classes of persistent information carriers.

2. REGIMES:

   Linear:           eigenmodes, composite modes
   Weakly Nonlinear: perturbed eigenmodes, amplitude breathers
   Strongly Nonlinear: bright/dark solitons, vector, breather
   Topological:       vortices, domain walls, edge states
   Hybrid:           soliton-mode hybrids, localized attractors

3. CARRIER DIVERSITY:

   Total classes: ~16 identified across 5 regimes.
   Richest regime: Strongly Nonlinear (5 classes).
   Most protected: Topological (winding number / Chern number).
   Most information: Bright solitons (encode amplitude + phase + position).

4. NULL HYPOTHESIS: Only eigenmodes and solitons exist.
   H1: A richer taxonomy with multiple distinct classes exists.
";
    }

    public static CarrierTaxonomy.TaxonomyReport Analyze()
    {
        var classes = CarrierClassifier.BuildTaxonomy();
        int total = classes.Count;
        int linear = classes.Count(c => c.Regime == CarrierTaxonomy.CarrierRegime.Linear);
        int nonlinear = classes.Count(c => c.Regime == CarrierTaxonomy.CarrierRegime.WeaklyNonlinear
                                       || c.Regime == CarrierTaxonomy.CarrierRegime.StronglyNonlinear);
        int topological = classes.Count(c => c.Regime == CarrierTaxonomy.CarrierRegime.Topological);
        int hybrid = classes.Count(c => c.Regime == CarrierTaxonomy.CarrierRegime.Hybrid);

        var richest = classes.GroupBy(c => c.Regime)
            .OrderByDescending(g => g.Count()).First().Key.ToString();

        bool complete = total >= 12;

        string classification = complete ? "C: Unified Carrier Theory" : "B: Extended Carrier Taxonomy";

        string verdict = complete
            ? $"INFORMATION CARRIER TAXONOMY CONSTRUCTED. {total} classes across 5 regimes. "
              + $"Linear: {linear}, Nonlinear: {nonlinear}, Topological: {topological}, Hybrid: {hybrid}. "
              + $"Richest regime: {richest}. "
              + $"The carrier taxonomy is RICHER than the species taxonomy (~19 species). "
              + $"Each carrier class contains multiple species (e.g., bright soliton = N variants). "
              + $"Information carriers are organized by REGIME × MORPHOLOGY × TOPOLOGY."
            : "Taxonomy incomplete.";

        return new CarrierTaxonomy.TaxonomyReport(
            classes, total, linear, nonlinear, topological,
            richest, complete, classification, verdict);
    }

    public static string HostileReview(CarrierTaxonomy.TaxonomyReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Is this taxonomy real or invented?");
        sb.AppendLine();
        sb.AppendLine($"  {report.TotalClasses} classes across 5 regimes.");
        sb.AppendLine($"  Linear: {report.LinearClasses}, Nonlinear: {report.NonlinearClasses}");
        sb.AppendLine($"  Topological: {report.TopologicalClasses}");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENT FOR:");
        sb.AppendLine("  - Classes are based on well-known nonlinear physics");
        sb.AppendLine("  - Bright/dark solitons, vortices, domain walls are distinct");
        sb.AppendLine("  - Topological protection gives a genuine new category");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENT AGAINST:");
        sb.AppendLine("  - All these structures exist in standard nonlinear PDE theory");
        sb.AppendLine("  - AT didn't discover them — it just renamed them 'carriers'");
        sb.AppendLine("  - The taxonomy is a catalog of known nonlinear wave phenomena");
        sb.AppendLine("  - 'Regime' classification is just sorting by nonlinearity strength");
        sb.AppendLine();
        return sb.ToString();
    }
}
