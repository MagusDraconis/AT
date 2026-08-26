namespace AT.Core.Research;

/// <summary>
/// Registers all known operator families in AT.
/// AT-X021: Operator Evolution Principle
/// </summary>
public static class OperatorFamily
{
    public static List<OperatorEvolutionMetrics.OperatorFamily> RegisterFamilies()
    {
        return new List<OperatorEvolutionMetrics.OperatorFamily>
        {
            new("Graph Laplacian", "L = D - A",
                "Fourier eigenmodes (sinusoidal)", 20, true),

            new("Normalized Laplacian", "L_norm = I - D^(-1/2)AD^(-1/2)",
                "Scale-invariant eigenmodes", 20, true),

            new("Magnetic Laplacian", "L_mag = D - A∘exp(iθ_ij)",
                "Landau levels, Hofstadter butterfly", 30, false),

            new("Nonlinear Schrödinger", "L_NLS = L_Q + α|ψ|²",
                "Solitons (bright, dark, vector, vortex)", 50, true),

            new("Hypergraph Laplacian", "L_hyper (3-body interactions)",
                "Multi-body eigenmodes", 40, false),

            new("Fractional Laplacian", "L^α, α∈(0,1)",
                "Lévy-flight modes, anomalous diffusion", 30, false),

            new("Adaptive Operator", "L(ψ) = L_Q + β·F(ψ)",
                "State-dependent modes", 100, false),
        };
    }
}
