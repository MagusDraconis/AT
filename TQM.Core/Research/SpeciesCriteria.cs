namespace TQM.Core.Research;

/// <summary>
/// Evaluates species criteria against eigenmodes and solitons
/// to find the universal principle defining an information species.
///
/// TQM-X007: Universal Species Principle
/// </summary>
public static class SpeciesCriteria
{
    public static List<SpeciesPrinciple.SpeciesCriterion> EvaluateAll()
    {
        return new List<SpeciesPrinciple.SpeciesCriterion>
        {
            new("Persistence", "Structure survives indefinitely",
                true, true, true, false),

            new("Reproducibility", "Same IC → same structure",
                true, true, true, false),

            new("Distinct Morphology", "Recognizable pattern/shape",
                true, true, true, false),

            new("Identity Preservation", "Survives perturbations unchanged",
                true, true, true, false),

            new("Interaction Capability", "Can collide/exchange with others",
                true, true, true, false),

            new("Information Storage", "Encodes information in structure",
                true, true, true, false),

            new("Information Transmission", "Moves/transfers information",
                true, true, false, false),

            new("Perturbation Resistance", "Topologically or dynamically protected",
                true, true, false, false),

            new("Population Formation", "Multiple copies can coexist",
                true, true, false, false),

            new("Evolutionary Participation", "Subject to selection/fitness",
                true, true, false, false),
        };
    }
}
