namespace AT.Core.Resonance.Theory;

/// <summary>
/// Compares Q-derived scaling laws against known physical systems.
///
/// AT-146: Physical Scaling Laws from Topological Charge
/// </summary>
public static class ScalingComparison
{
    public static List<ScalingLawCandidate.ScalingCandidate> CompareAll()
    {
        return new List<ScalingLawCandidate.ScalingCandidate>
        {
            new("Effective Mass m_eff ∝ Q²", "Q²", 2.0,
                new[]{"Particle-in-box (E₁∝1/L² → m∝L²)",
                      "Vibrating string fundamental mode",
                      "1D tight-binding gap inverse"},
                new[]{"Particle-in-box: m_eff = Q²/π² (EXACT)"},
                true, "Diffusive (1D)"),

            new("Total Energy E ∝ Q", "Q", 1.0,
                new[]{"Extensive energy (any system)",
                      "Phonon mode count ∝ N",
                      "Tight-binding bandwidth ∝ N"},
                new[]{"E = 2(Q-1) = trace(L) (EXACT identity)"},
                true, "Extensive"),

            new("Spectral Gap Δ ∝ 1/Q²", "1/Q²", -2.0,
                new[]{"Particle-in-box (ΔE∝1/L²)",
                      "Vibrating string overtones",
                      "Finite-size gap in 1D"},
                new[]{"Particle-in-box: Δ = 3π²/Q² (EXACT)"},
                true, "Diffusive (1D)"),

            new("Correlation Length ξ ∝ Q", "Q", 1.0,
                new[]{"System size scaling",
                      "Polymer chain end-to-end distance",
                      "1D correlation length at criticality"},
                Array.Empty<string>(),
                false, "Extensive"),

            new("Transport Coeff D ∝ 1/Q²", "1/Q²", -2.0,
                new[]{"Diffusion constant in finite domain",
                      "Conductivity in 1D (Drude)"},
                Array.Empty<string>(),
                false, "Diffusive"),

            new("Info Capacity C ∝ log(Q)", "log(Q)", 0.0,
                new[]{"Boltzmann entropy S∝log(W)",
                      "Shannon capacity of N symbols",
                      "Information-theoretic bound"},
                Array.Empty<string>(),
                false, "Logarithmic"),

            new("Mode Density ρ = 1", "const", 0.0,
                new[]{"Weyl's law: N(λ)∝λ^(d/2) in d dimensions",
                      "1 mode per degree of freedom"},
                new[]{"ρ = 1 for 1D chain (EXACT)"},
                true, "Constant"),
        };
    }
}
