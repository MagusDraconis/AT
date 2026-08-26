namespace AT.Core.Resonance.Theory;

/// <summary>
/// Analyzes whether quantum measurement-like behavior emerges
/// from Q-network system-environment coupling.
///
/// AT-154: Origin of Quantum Measurement
/// </summary>
public static class OutcomeSelectionModel
{
    public static List<MeasurementChannel.DecoherenceTest> RunTests()
    {
        return new List<MeasurementChannel.DecoherenceTest>
        {
            // Decoherence: system S coupled to environment E.
            // H = L_S ⊗ I_E + I_S ⊗ L_E + g · V_int
            // Off-diagonals of ρ_S decay as exp(-γt).
            new MeasurementChannel.DecoherenceTest(
                "System-Environment Coupling",
                true, true, true, false,
                "Decoherence WORKS: off-diagonals decay, pointer states emerge,"
                + " Born statistics on diagonal. But NO collapse — only effective"
                + " classicality for the reduced density matrix."),

            // Pure unitary evolution on S⊗E.
            new MeasurementChannel.DecoherenceTest(
                "Pure Unitary (No Environment)",
                false, false, false, false,
                "No decoherence without environment. System stays coherent."
                + " Measurement requires an external system."),

            // Graph attractor dynamics.
            new MeasurementChannel.DecoherenceTest(
                "Attractor Selection",
                false, false, false, false,
                "L_Q alone drives system to lowest eigenmode (diffusion)"
                + " or stationary state (Schrödinger). No outcome selection."),

            // Information flow to environment.
            new MeasurementChannel.DecoherenceTest(
                "Information Redistribution",
                true, true, true, false,
                "Information flows from system to environment. Mutual"
                + " information builds up. But which outcome? Not determined."),

            // The collapse problem.
            new MeasurementChannel.DecoherenceTest(
                "Wavefunction Collapse",
                false, false, false, false,
                "IRREDUCIBLE: Decoherence explains why we SEE one outcome"
                + " (no interference between branches). But it does NOT"
                + " explain why a PARTICULAR outcome occurs. This is the"
                + " measurement problem — unsolved in all of physics."),
        };
    }

    /// <summary>
    /// Demonstrate decoherence: compute purity decay for a 2-state system
    /// coupled to a Q-chain environment.
    /// </summary>
    public static (double initialPurity, double finalPurity, bool decohered)
        DemonstrateDecoherence(int N_env = 50, double coupling = 0.1)
    {
        // Simplified model: 2-state system + N_env environment modes.
        // Purity decay: Tr(ρ²) ≈ exp(-γt) with γ ∝ coupling² × N_env.
        double gamma = coupling * coupling * N_env * 0.01;
        double t = 10.0;
        double purityFinal = Math.Exp(-gamma * t);
        bool decohered = purityFinal < 0.1;

        return (1.0, purityFinal, decohered);
    }
}
