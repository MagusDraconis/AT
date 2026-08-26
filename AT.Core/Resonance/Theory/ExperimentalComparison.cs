namespace AT.Core.Resonance.Theory;

/// <summary>
/// Generates predictions for external physical systems and compares
/// against known results. Tests whether AT works beyond its construction set.
///
/// AT-148: External Physical Prediction Test
/// </summary>
public static class ExperimentalComparison
{
    public static List<PredictionCandidate.ExternalPrediction> RunExternalTests()
    {
        var tests = new List<PredictionCandidate.ExternalPrediction>();
        int N = 20;

        // ══════════════════════════════════════════════════════════════
        // SYSTEMS WHERE AT SHOULD WORK (graph Laplacian governs dynamics)
        // ══════════════════════════════════════════════════════════════

        // 1. 1D Coupled Harmonic Oscillators
        // AT: λ_k = 2-2cos(πk/(N+1)) → ω_k ∝ sin(πk/(2(N+1)))
        // Known: ω_k = 2√(K/m)·sin(πk/(2(N+1))) [EXACT MATCH]
        tests.Add(new PredictionCandidate.ExternalPrediction(
            "1D Harmonic Chain", "Frequency spectrum ω_k",
            "ω_k ∝ sin(πk/(2(N+1)))", "ω_k = 2√(K/m)·sin(πk/(2(N+1)))",
            true, "Graph Laplacian ≡ Dynamical Matrix"));

        // 2. 1D Tight-Binding Electrons
        // AT: E_k = -2t·cos(ka) with k=πn/(N+1)
        // Known: E_k = -2t·cos(ka) [EXACT MATCH]
        tests.Add(new PredictionCandidate.ExternalPrediction(
            "1D Tight-Binding Chain", "Band dispersion E(k)",
            "E_k = -2t·cos(πk/(N+1))", "E_k = -2t·cos(ka)",
            true, "Identity — graph Laplacian = Hamiltonian"));

        // 3. 1D Diffusion on a Lattice
        // AT: λ₁ = 2-2cos(π/(N+1)) ≈ π²/N²
        // Known: slowest mode ∝ 1/N² [EXACT]
        tests.Add(new PredictionCandidate.ExternalPrediction(
            "1D Diffusion", "Slowest mode scaling",
            "λ₁ ∝ 1/N²", "λ₁ = π²/N² + O(1/N⁴)",
            true, "Diffusion operator = graph Laplacian"));

        // 4. Spin-wave spectrum (1D ferromagnet)
        // AT: ω_k ∝ 1-cos(ka) = 2sin²(ka/2)
        // Known: ω_k = 2JS(1-cos(ka)) [EXACT]
        tests.Add(new PredictionCandidate.ExternalPrediction(
            "1D Ferromagnetic Spin Chain", "Magnon dispersion",
            "ω_k ∝ 1-cos(πk/(N+1))", "ω_k = 2JS·(1-cos(ka))",
            true, "Graph Laplacian modes = magnon modes"));

        // ══════════════════════════════════════════════════════════════
        // SYSTEMS WHERE AT PARTIALLY WORKS
        // ══════════════════════════════════════════════════════════════

        // 5. 1D Ising Chain — T=0 gap
        // AT: λ₁ ∝ 1/N² (from graph Laplacian)
        // Known: Δ ∝ 1/N (domain wall energy = 2J, gap = 2J/N)
        tests.Add(new PredictionCandidate.ExternalPrediction(
            "1D Ising Chain (T=0)", "Finite-size gap Δ(N)",
            "Δ ∝ 1/N²", "Δ = 2J/N",
            false, "AT fails — Ising gap ∝ 1/N, not 1/N²"));

        // 6. 1D Heisenberg Chain — spinon gap
        // AT: λ₁ ∝ 1/N²
        // Known: Δ ∝ 1/N (gapless in thermodynamic limit)
        tests.Add(new PredictionCandidate.ExternalPrediction(
            "1D Heisenberg Chain", "Spin gap Δ(N)",
            "Δ ∝ 1/N²", "Δ ∝ 1/N (Bethe ansatz)",
            false, "AT fails — Heisenberg gap ∝ 1/N"));

        // ══════════════════════════════════════════════════════════════
        // SYSTEMS WHERE AT FAILS (different physics)
        // ══════════════════════════════════════════════════════════════

        // 7. Percolation cluster — conductivity
        // AT: predicts nothing (graph Laplacian doesn't capture percolation)
        tests.Add(new PredictionCandidate.ExternalPrediction(
            "2D Percolation (p=p_c)", "Conductivity exponent t",
            "Not predictable from L_Q", "t ≈ 1.3 (numerical)",
            false, "AT has no prediction — outside graph Laplacian domain"));

        // 8. Random resistor network — resistance scaling
        tests.Add(new PredictionCandidate.ExternalPrediction(
            "Random Resistor Network", "Resistance scaling",
            "R ∝ L (Ohm's law for uniform)", "R ∝ L^ζ (ζ≠1 for random)",
            false, "AT predicts uniform case only"));

        return tests;
    }
}
