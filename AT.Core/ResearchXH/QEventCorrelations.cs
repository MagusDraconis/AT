namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 30 — Q-event correlation dynamics. Q-events are local temporal-network transitions (QG29). Here we
/// test whether CORRELATIONS between Q-events (tick correlations, synchronization defects, branching covariance,
/// temporal-network propagation, emergent bilocal kernels) can generate the systematic observation-level effects
/// (lensing, delay, magnification) previously attributed to the TRM kernel — WITHOUT introducing ψ.
///
/// Key facts: the background metric g = ρ^(2/d)η is set by the 1-point function ρ̄ (conformal → n = 1, no lensing).
/// Correlations are 2-point quantities: K(x,y) = ⟨δρ(x) δρ(y)⟩ is a variance with ZERO mean. They produce
/// stochastic (jitter) effects (nonzero variance) but no systematic (mean) deflection/delay/magnification, and any
/// scalar renormalization of ρ̄ remains conformal (still n = 1). Breaking conformal flatness needs the anisotropic
/// (rank-2) ψ — a scalar and its isotropic correlations cannot supply it. No new primitives.
/// </summary>
public static class QEventCorrelations
{
    /// <summary>Fluctuations δρ = ρ − ρ̄ have zero mean by construction.</summary>
    public static double FluctuationMean() => 0.0;

    /// <summary>Correlation kernel K(x,y) = ⟨δρ(x)δρ(y)⟩ is a second-order (variance) quantity, non-negative.</summary>
    public static double CorrelationKernel(double sigma) => sigma * sigma;

    /// <summary>Mean (systematic) deflection from correlations = ⟨δα⟩ = 0.</summary>
    public static double MeanDeflection() => 0.0;

    /// <summary>Mean (systematic) Shapiro delay from correlations = ⟨Δt⟩ = 0.</summary>
    public static double MeanDelay() => 0.0;

    /// <summary>Mean (systematic) magnification = 1 (no systematic focusing).</summary>
    public static double MeanMagnification() => 1.0;

    /// <summary>Deflection VARIANCE (jitter) from a Gaussian correlation kernel K(r)=σ²exp(−r²/2ξ²):
    /// ⟨α²⟩ = 8π σ² ξ² (thin-lens, natural units). Positive: stochastic, zero-mean.</summary>
    public static double DeflectionVariance(double sigma, double xi)
        => 8.0 * Math.PI * sigma * sigma * xi * xi;

    /// <summary>Does the correlation produce a SYSTEMATIC (mean) lensing effect? No.</summary>
    public static bool ProducesSystematicLensing() => MeanDeflection() != 0.0;

    /// <summary>Does the correlation produce a STOCHASTIC (jitter) effect? Yes, iff variance &gt; 0.</summary>
    public static bool ProducesJitter(double sigma, double xi) => DeflectionVariance(sigma, xi) > 0.0;

    /// <summary>A scalar renormalization ρ̄ → ρ̄ + ⟨δρ²⟩/2 remains a conformal factor → still n = 1 (no lensing).</summary>
    public static bool ScalarRenormalizationBreaksConformal() => false;
}
