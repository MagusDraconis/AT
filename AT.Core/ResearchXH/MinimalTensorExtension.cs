namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 24 — minimal tensor extension audit. Given QG23 (ψ cannot emerge from the scalar actualization),
/// determine the SMALLEST extra primitive that restores lensing, tensor GWs, and Hawking thermodynamics. Four
/// candidates are ranked by the additional degrees of freedom they introduce and by whether they can source a
/// helicity-2 (spin-2) mode. No new primitives are added — this audit only measures what a primitive would cost.
///
/// Conventions: d = spatial dimension, spacetime D = d+1. The propagating graviton count is
/// GravitonPolarizations(d) = (d+1)(d−2)/2 = 2 at d=3. A symmetric spatial rank-2 tensor has d(d+1)/2 components
/// (= 6 at d=3), whose helicity decomposition contains the 2 transverse-traceless graviton d.o.f. plus 3 vector
/// and 1 scalar. A vector has d components (= 3 at d=3) and is spin-1 — it cannot source helicity-2.
/// </summary>
public static class MinimalTensorExtension
{
    // ── Candidate primitives: additional degrees of freedom they introduce ─────────────

    /// <summary>Tensor counting measure = symmetric spatial rank-2 (d(d+1)/2 = 6 at d=3). Spin-2-capable but over-complete.</summary>
    public static double TensorCountingMeasureDof(int d) => d * (d + 1.0) / 2.0;

    /// <summary>Directional actualization = spatial vector (d components = 3 at d=3). Spin-1: cannot source helicity-2.</summary>
    public static double DirectionalActualizationDof(int d) => d;

    /// <summary>Anisotropic causal structure = rank-2 tensor on the causal cone (d(d+1)/2 = 6 at d=3). Over-complete.</summary>
    public static double AnisotropicCausalDof(int d) => d * (d + 1.0) / 2.0;

    /// <summary>ψ-field as an independent spin-2 primitive = the propagating graviton polarizations (2 at d=3).</summary>
    public static double PsiFieldDof(int d) => DimensionAnalysis.GravitonPolarizations(d);

    // ── Helicity content of each candidate ────────────────────────────────────────────

    /// <summary>Maximum spin (helicity) carried by a candidate: scalar=0, vector=1, rank-2=2.</summary>
    public static double MaxHelicity(string candidate) => candidate switch
    {
        "scalar" => 0.0,
        "vector" => 1.0,
        "rank2" => 2.0,
        "psi" => 2.0,
        _ => throw new ArgumentOutOfRangeException(nameof(candidate))
    };

    /// <summary>Tensor GWs need helicity 2; a candidate suffices iff its max helicity is 2.</summary>
    public static bool CanSourceTensorGWs(string candidate) => MaxHelicity(candidate) >= 2.0;

    // ── Observable requirements: minimal additional degrees of freedom ─────────────────

    /// <summary>Lensing (Weyl ≠ 0) needs only 1 d.o.f. — a scalar ψ breaks conformal flatness (cf. MetricAnsatzAudit).</summary>
    public static double LensingDofRequired() => 1.0;

    /// <summary>Tensor GWs need the 2 helicity-2 polarizations: GravitonPolarizations(d) (2 at d=3).</summary>
    public static double TensorGWDofRequired(int d) => DimensionAnalysis.GravitonPolarizations(d);

    /// <summary>Hawking temperature T = κ/2π is derived from the horizon profile — 0 independent d.o.f. beyond the metric.</summary>
    public static double HawkingDofRequired() => 0.0;

    /// <summary>
    /// Minimal additional degrees of freedom to restore ALL THREE observables = the largest single requirement,
    /// i.e. the 2 graviton polarizations (which also cover lensing's 1 and Hawking's 0).
    /// </summary>
    public static double MinimalAdditionalDof(int d)
        => Math.Max(Math.Max(LensingDofRequired(), TensorGWDofRequired(d)), HawkingDofRequired());
}
