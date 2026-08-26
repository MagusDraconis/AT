namespace AT.Core.Research;

/// <summary>
/// Defines the complexity staircase from Noise to Evolution.
/// AT-X018: Complexity Emergence Principle
/// </summary>
public static class ComplexityPhaseDiagram
{
    public static List<ComplexityMetric.ComplexityLevel> BuildStaircase()
    {
        return new List<ComplexityMetric.ComplexityLevel>
        {
            new(
                "Level 0: NOISE",
                "None",
                0.0, false, false, false, false,
                "Thermal fluctuations, random noise, turbulence"
            ),

            new(
                "Level 1: REALITY STRUCTURES",
                "R+S (Reversibility + Self-Consistency)",
                1.0, false, false, false, false,
                "Quantum eigenstates, solitons, topological defects"
            ),

            new(
                "Level 2: INFORMATION CARRIERS",
                "Reality + Information Encoding",
                2.5, false, false, false, false,
                "Encoded eigenmodes, memory states, qubits"
            ),

            new(
                "Level 3: SPECIES",
                "Carriers + Identity + Reproducibility",
                4.0, true, false, false, false,
                "Multiple distinct carrier types, species catalog (~19)"
            ),

            new(
                "Level 4: ECOLOGIES",
                "Species + Interactions + Populations",
                6.5, true, true, false, false,
                "Interacting species, competition, coexistence"
            ),

            new(
                "Level 5: EVOLUTION",
                "Ecologies + Variation + Selection",
                9.0, true, true, true, false,
                "Darwinian dynamics, fitness landscapes, adaptation"
            ),

            new(
                "Level 6: OPEN-ENDED EVOLUTION",
                "Evolution + Innovation + Unbounded Novelty",
                10.0, true, true, true, true,
                "Continuous new species, expanding complexity (NOT YET OBSERVED in AT)"
            ),
        };
    }
}
