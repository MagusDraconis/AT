namespace TQM.Core.Research;

/// <summary>
/// Classifies structures at the intersection of reversibility
/// and self-consistency — the quantum information carriers.
/// TQM-X012: Quantum Information Carrier Principle
/// </summary>
public static class QuantumCarrierMetrics
{
    public static List<QuantumCarrier.QuantumCarrierClass> ClassifyIntersection()
    {
        return new List<QuantumCarrier.QuantumCarrierClass>
        {
            // ── QUANTUM INFORMATION CARRIERS (Rev + SC) ──
            new("Schrödinger Eigenstate", "ψ_k = exp(-iλ_k t)·v_k",
                true, true, true, 1.0, double.PositiveInfinity, "Linear Quantum"),

            new("Harmonic Oscillator State", "|n⟩ with E_n = (n+½)ℏω",
                true, true, true, 1.0, double.PositiveInfinity, "Linear Quantum"),

            new("Bright Soliton (NLS)", "sech(x) profile, focusing NLS",
                true, true, true, 0.95, 1000, "Nonlinear Quantum"),

            new("Dark Soliton", "tanh(x) profile, defocusing NLS",
                true, true, true, 0.95, 1000, "Nonlinear Quantum"),

            new("Topological Edge State", "Boundary-localized, Chern-protected",
                true, true, true, 1.0, double.PositiveInfinity, "Topological Quantum"),

            new("Quantum Vortex", "Quantized circulation, phase singularity",
                true, true, true, 0.90, 500, "Topological Quantum"),

            new("Coherent Breather", "Periodic localized oscillation",
                true, true, true, 0.85, 200, "Hybrid Quantum"),

            // ── REVERSIBLE ONLY (no self-consistency) ──
            new("Free Particle Wavepacket", "i∂ψ/∂t = -∇²ψ, dispersing",
                true, false, false, 0.3, 50, "Linear — NOT quantum carrier"),

            new("Hamiltonian Chaos", "Ergodic trajectory, no fixed point",
                true, false, false, 0.1, 1, "Chaotic — NOT quantum carrier"),

            // ── SELF-CONSISTENT ONLY (no reversibility) ──
            new("Diffusion Eigenmode", "∂u/∂t = -L_Q u, decaying norm",
                false, true, false, 0.6, 100, "Dissipative — NOT quantum carrier"),

            new("Kuramoto Sync State", "Phase-locked, dissipative",
                false, true, false, 0.5, 50, "Dissipative — NOT quantum carrier"),

            // ── NEITHER ──
            new("Thermal Noise", "Random fluctuations, no structure",
                false, false, false, 0.0, 0, "Neither — noise"),
        };
    }
}
