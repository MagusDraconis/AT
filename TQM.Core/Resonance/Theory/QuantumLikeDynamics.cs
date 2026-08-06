namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Analyzes whether L_Q supports Schrödinger-like, wave, or diffusive dynamics.
///
/// TQM-149: Emergence of Schrödinger Dynamics from Q Networks
/// </summary>
public static class QuantumLikeDynamics
{
    public static List<SchrodingerMapping.DynamicsComparison> CompareDynamics(int N = 20)
    {
        return new List<SchrodingerMapping.DynamicsComparison>
        {
            // Model A: Diffusion ∂u/∂t = -L_Q u
            // Solution: u(t) = exp(-L_Q·t) u(0)
            // Eigenmodes decay: u_k(t) = exp(-λ_k·t) v_k
            // No oscillations, no phase, no interference
            new SchrodingerMapping.DynamicsComparison(
                "A: Diffusion", "∂u/∂t = -L_Q u",
                false, false, false, true, "Dissipative"),

            // Model B: Classical wave ∂²u/∂t² = -L_Q u
            // Solution: u(t) = cos(√L_Q·t) u(0) + sin(√L_Q·t)/√L_Q · u'(0)
            // Real oscillations, no complex phase
            new SchrodingerMapping.DynamicsComparison(
                "B: Classical Wave", "∂²u/∂t² = -L_Q u",
                false, false, true, true, "Wave (Real)"),

            // Model C: Schrödinger i∂ψ/∂t = L_Q ψ
            // Solution: ψ(t) = exp(-i·L_Q·t) ψ(0)
            // Unitary evolution, norm conserved, complex phase
            // Stationary states: ψ_k(t) = exp(-i·λ_k·t) v_k
            // Interference: superposition of eigenmodes
            new SchrodingerMapping.DynamicsComparison(
                "C: Schrödinger", "i∂ψ/∂t = L_Q ψ",
                true, true, true, true, "Quantum-like"),

            // Model D: Generalized phase dynamics
            // ∂θ/∂t = -L_Q θ (Kuramoto on graph)
            new SchrodingerMapping.DynamicsComparison(
                "D: Phase (Kuramoto)", "∂θ/∂t = -L_Q θ",
                false, true, false, true, "Synchronization"),
        };
    }

    /// <summary>
    /// Demonstrate unitary evolution on a 1D chain.
    /// </summary>
    public static (double normInitial, double normFinal, bool conserved)
        DemonstrateUnitaryEvolution(int Q = 20, int steps = 100)
    {
        // ψ(0) = random complex vector.
        var rng = new Random(42);
        var psi = new System.Numerics.Complex[Q];
        for (int i = 0; i < Q; i++)
            psi[i] = new System.Numerics.Complex(
                rng.NextDouble() - 0.5, rng.NextDouble() - 0.5);

        double normInit = psi.Sum(c => c.Magnitude * c.Magnitude);

        // Build L_Q.
        var L = new double[Q, Q];
        for (int i = 0; i < Q; i++)
        {
            L[i, i] = 2;
            if (i > 0) L[i, i - 1] = -1;
            if (i < Q - 1) L[i, i + 1] = -1;
        }

        // Evolve: ψ(t+dt) = exp(-i·L·dt) ψ(t) ≈ (I - i·L·dt) ψ(t)
        double dt = 0.01;
        for (int t = 0; t < steps; t++)
        {
            var newPsi = new System.Numerics.Complex[Q];
            for (int i = 0; i < Q; i++)
            {
                var sum = System.Numerics.Complex.Zero;
                for (int j = 0; j < Q; j++)
                    sum += L[i, j] * psi[j];
                newPsi[i] = psi[i] - System.Numerics.Complex.ImaginaryOne * dt * sum;
            }
            psi = newPsi;
        }

        double normFinal = psi.Sum(c => c.Magnitude * c.Magnitude);
        bool conserved = Math.Abs(normFinal - normInit) / normInit < 0.01;

        return (normInit, normFinal, conserved);
    }
}
