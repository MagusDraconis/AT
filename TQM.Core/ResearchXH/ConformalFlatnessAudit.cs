namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 22 — audit conformal-flatness consequences. Tests whether the failures (no lensing, no tensor GWs,
/// no Hawking T) are consequences of the conformal-flatness assumption (ψ=0) or fundamental TQM results. The
/// reference-metric curvature (ψ) is the single knob: ψ=0 → Weyl=0 (no lensing/tensor), ψ≠0 → non-zero. No new primitives.
/// </summary>
public static class ConformalFlatnessAudit
{
    /// <summary>Light bending (the reference-metric curvature that deflects null geodesics): 0 at ψ=0 (conformal
    /// flatness → straight light), non-zero at ψ≠0 (weakly non-conformal → lensing).</summary>
    public static double LightBending(double x, double psi) => MetricAnsatzAudit.ReferenceRicciScalar(x, psi);

    /// <summary>Tensor (Weyl + graviton) degrees of freedom — frozen to 0 by conformal flatness, present for d≥3.</summary>
    public static double TensorModes(int d) => TensorSector.TensorDegreesOfFreedom(d);
}
