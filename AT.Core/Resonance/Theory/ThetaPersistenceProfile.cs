namespace AT.Core.Resonance.Theory;

/// <summary>
/// Persistence analysis of information stored in the Θ field.
/// Computes memory decay laws, half-life, retention fractions,
/// and attractor stability.
///
/// AT-130: Theta Memory and Information Persistence
/// </summary>
public static class ThetaPersistenceProfile
{
    // ══════════════════════════════════════════════════════════════════
    // Simulate memory persistence at a given density.
    // ══════════════════════════════════════════════════════════════════

    public static ThetaMemoryState.PersistenceResult SimulatePersistence(
        double density, double time, int bitsWritten, double damping = 0.1,
        double coherenceLen = 1.0)
    {
        // Pattern overlap decays exponentially due to damping.
        // overlap(t) = exp(−t/τ) where τ = 1/γ (field damping time).
        double tau = 1.0 / Math.Max(damping, 1e-10);
        double overlap = Math.Exp(-time / tau);

        // At high density, coherence protects memory → slower decay.
        double protectionFactor = 1.0 + density * 2.0;
        tau *= protectionFactor;
        overlap = Math.Exp(-time / tau);

        // Mutual information: I ≈ −½log(1−overlap²) for Gaussian channel.
        double mi = overlap > 0.99 ? bitsWritten
                  : overlap > 0.3 ? -0.5 * Math.Log(1.0 - overlap * overlap)
                  : 0;

        // Retention fraction: fraction of bits recoverable at SNR.
        double snr = overlap * overlap * density * 5.0;
        double ber = 0.5 * (1.0 - ErfApprox(Math.Sqrt(snr / 2.0)));
        double retention = 1.0 - Math.Min(ber * 2, 1.0);

        double halfLife = tau * Math.Log(2.0);
        bool persists = overlap > 0.3;

        string decayType = halfLife > 5000 ? "Stable"
                         : halfLife > 1000 ? "PowerLaw" : "Exponential";

        return new ThetaMemoryState.PersistenceResult(
            density, time, overlap, mi, retention, halfLife,
            persists, decayType);
    }

    // ══════════════════════════════════════════════════════════════════
    // Simulate multiple persistence times.
    // ══════════════════════════════════════════════════════════════════

    public static List<ThetaMemoryState.PersistenceResult> SimulateMemoryDecay(
        double density, int bitsWritten,
        double[] times = null, double damping = 0.1)
    {
        times ??= new[] { 10.0, 100.0, 500.0, 1000.0, 5000.0, 10000.0 };
        return times.Select(t => SimulatePersistence(density, t, bitsWritten, damping))
                    .ToList();
    }

    // ══════════════════════════════════════════════════════════════════
    // Estimate storage capacity.
    // ══════════════════════════════════════════════════════════════════

    public static double EstimateStorageCapacity(
        double density, double coherenceLen, double systemSize = 1.0)
    {
        // Number of independent coherence volumes.
        double nVolumes = systemSize / Math.Max(coherenceLen, 0.01);
        // Each volume can store ~1 bit (phase 0/π).
        // Capacity scales with density (more coherence → more reliable).
        return nVolumes * density * 0.5;
    }

    // ══════════════════════════════════════════════════════════════════
    // Memory attractor analysis.
    // ══════════════════════════════════════════════════════════════════

    public static List<ThetaMemoryState.MemoryAttractor> AnalyzeAttractors(
        double density, double damping)
    {
        var attractors = new List<ThetaMemoryState.MemoryAttractor>();
        double tau = 1.0 / Math.Max(damping, 1e-10) * (1.0 + density * 2.0);

        // Uniform phase attractor (R_Q → 1).
        attractors.Add(new ThetaMemoryState.MemoryAttractor(
            "Uniform Phase (R_Q=1)",
            density > 0.3 ? 0.8 : 0.3,
            0.01, tau * 5, tau < 1000,
            "All charges in phase. Global attractor. " +
            "Stable but information-free (all phases equal)."));

        // Anti-phase pattern attractor.
        attractors.Add(new ThetaMemoryState.MemoryAttractor(
            "Anti-Phase Pattern (Δφ=π)",
            density > 0.5 ? 0.4 : 0.1,
            0.02, tau * 2, true,
            "Alternating 0/π phase pattern. Metastable — " +
            "coupling pulls toward uniform. Can store 1 bit."));

        // Standing wave attractor.
        attractors.Add(new ThetaMemoryState.MemoryAttractor(
            "Standing Wave",
            density > 0.7 ? 0.5 : 0.2,
            0.015, tau * 3, true,
            "Spatial standing wave in Θ(x). Multiple nodes. " +
            "Can store multiple bits in nodal structure."));

        return attractors;
    }

    private static double ErfApprox(double x)
    {
        double t = 1.0 / (1.0 + 0.47047 * Math.Abs(x));
        double poly = t * (0.3480242 + t * (-0.0958798 + t * 0.7478556));
        double r = 1.0 - poly * Math.Exp(-x * x);
        return x >= 0 ? r : -r;
    }
}
