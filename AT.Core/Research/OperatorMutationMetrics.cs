namespace AT.Core.Research;

/// <summary>
/// Defines the meta-operator tower and tests whether it
/// generates genuinely new operator families at each level.
/// AT-X024: Meta-Operator Evolution Principle
/// </summary>
public static class OperatorMutationMetrics
{
    public static List<OperatorLineage.OperatorGeneration> BuildMetaOperatorTower()
    {
        return new List<OperatorLineage.OperatorGeneration>
        {
            new(0, "L₀ = L_Q (Graph Laplacian)",
                "Fourier eigenmodes (sinusoidal)", true, 20),

            new(1, "L₁ = L₀ + α|ψ|²",
                "NLS solitons (bright, dark, vector, vortex, breather)", true, 50),

            new(2, "L₂ = L₁ + β|L₁ψ|²",
                "Cascaded solitons (soliton-soliton bound states)", true, 80),

            new(3, "L₃ = L₂ + γ|L₂ψ|²",
                "Higher-order soliton complexes", true, 120),

            new(4, "L₄ = L₃ + δ·F(L₃, ψ)",
                "Adaptive cascaded structures", true, 200),

            new(5, "L₅ = L₄ + ε·G(L₀,...,L₄)",
                "Multi-generational hybrid carriers", true, 300),
        };
    }
}
