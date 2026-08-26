namespace AT.Core.Research;

/// <summary>
/// Expresses every carrier class as a fixed-point equation F(x)=x
/// and evaluates whether a deeper invariant exists beneath self-consistency.
/// AT-X010: Self-Consistency Principle
/// </summary>
public static class FeedbackInvariant
{
    public static List<SelfConsistencyMetric.DeeperCandidate> SearchDeeper()
    {
        return new List<SelfConsistencyMetric.DeeperCandidate>
        {
            // Fixed-point dynamics: F(x) = x.
            // This IS self-consistency. Not deeper — it's the same thing.
            new("Fixed-Point Dynamics",
                "F(x) = x (Banach/Brouwer fixed point)",
                true, false,
                "EQUIVALENT. 'Self-consistency' and 'fixed point' are two names for the same mathematical structure. F(x)=x IS the minimal form."),

            // Feedback loops: output → input.
            // This IS self-consistency operationalized.
            new("Feedback Loops",
                "x_{n+1} = G(x_n), limit x* = G(x*)",
                true, false,
                "EQUIVALENT. A feedback loop that converges to a fixed point IS self-consistency in algorithmic form."),

            // Constraint satisfaction.
            new("Constraint Satisfaction",
                "Dynamics constrained by boundary conditions + conservation laws",
                true, false,
                "PARTIAL. Constraints determine WHICH fixed points exist, but don't explain WHY fixed points exist at all."),

            // Attractor existence theorems.
            new("Attractor Existence",
                "Dissipative systems → contracting phase space volume → attractors",
                true, true,
                "DEEPER. This explains WHY attractors exist in dissipative systems. But the proof depends on the specific dynamics (e.g., L_Q is positive semi-definite). Not a universal deeper layer — it's system-specific."),

            // Energy minimization.
            new("Energy Minimization",
                "Systems evolve to minimize E(x); minima are self-consistent",
                true, false,
                "NOT UNIVERSAL. Solitons are NOT energy minima (they're saddle points of the Hamiltonian). Only works for gradient systems."),

            // Information-theoretic: max entropy / min free energy.
            new("Information Optimization",
                "Max entropy subject to constraints → equilibrium distribution",
                false, false,
                "FAILS. Information carriers are NON-EQUILIBRIUM structures. They persist despite entropy production, not because of entropy maximization."),

            // Structural stability (Andronov-Pontryagin).
            new("Structural Stability",
                "Small perturbations of dynamics don't change qualitative behavior",
                false, false,
                "ORTHOGONAL. Structural stability is about robustness of the dynamics, not existence of fixed points."),

            // Nothing deeper.
            new("Self-Consistency IS Fundamental",
                "No deeper invariant exists across all carrier classes. Below self-consistency lies the specific mathematical structure of each regime (L_Q for linear, NLS for nonlinear, winding number for topological).",
                true, true,
                "FUNDAMENTAL. Self-consistency = fixed-point condition. This IS the deepest universal invariant. What varies between regimes is the specific form of the fixed-point equation, not the existence of fixed points."),
        };
    }
}
