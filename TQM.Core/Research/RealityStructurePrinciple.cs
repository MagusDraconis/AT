namespace TQM.Core.Research;

/// <summary>
/// Evaluates all four quadrants (Rev×SC) for persistence, species formation,
/// and evolutionary potential.
/// TQM-X014: Reality Structure Principle
/// </summary>
public static class RealityStructurePrinciple
{
    public static List<RealityStructureMetrics.PersistenceQuadrant> EvaluateQuadrants()
    {
        return new List<RealityStructureMetrics.PersistenceQuadrant>
        {
            // ── Rev∩SC (BOTH) ──
            new(
                "BOTH (Rev∩SC)",
                "Quantum eigenstates, solitons, topological edge states, vortices",
                double.PositiveInfinity, 1.0,
                true, true, true,
                "FULL REALITY: maximal persistence, identity, information, evolution"
            ),

            // ── SC only ──
            new(
                "SELF-CONSISTENT ONLY",
                "Diffusion eigenmodes, Kuramoto sync, damped attractors, Turing patterns",
                100, 0.5,
                true, false, false,
                "PARTIAL REALITY: temporary structure, information degrades, no evolution"
            ),

            // ── Rev only ──
            new(
                "REVERSIBLE ONLY",
                "Free particle, Hamiltonian chaos, degenerate ring modes",
                50, 0.2,
                false, false, false,
                "FLUID REALITY: conserved but shapeless — no persistent forms"
            ),

            // ── Neither ──
            new(
                "NEITHER",
                "Thermal noise, dissipative chaos, transients",
                0, 0.0,
                false, false, false,
                "NO REALITY: nothing persists, no information, no structure"
            ),
        };
    }
}
