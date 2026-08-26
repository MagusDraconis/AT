namespace AT.Core.Research;

/// <summary>
/// Evaluates candidate formation mechanisms for all 16 carrier classes.
/// AT-X009: Information Carrier Formation Principle
/// </summary>
public static class CarrierFormationMetrics
{
    public static List<FormationPrinciple.FormationMechanism> EvaluateMechanisms()
    {
        return new List<FormationPrinciple.FormationMechanism>
        {
            new("Potential Minima", "Structures settle at local minima of effective potential V(p)",
                true, true, false, false),

            new("Self-Consistency", "Structures that reinforce themselves through feedback persist",
                true, true, true, true),

            new("Topological Protection", "Winding number / Chern number prevents continuous decay",
                false, false, true, false),

            new("Dynamical Stability", "Lyapunov-stable fixed points of the evolution equation",
                true, true, true, true),

            new("Information Compression", "Structures that efficiently encode information resist entropy",
                true, true, false, false),

            new("Entropy Minimization", "Structures occupy low-entropy configurations",
                true, false, false, false),

            new("Critical Coupling", "Persistence requires coupling strength above threshold K > K_c",
                true, true, false, false),

            new("Balanced Flux", "Inflow = outflow of energy/information at the structure",
                true, true, true, false),
        };
    }
}
