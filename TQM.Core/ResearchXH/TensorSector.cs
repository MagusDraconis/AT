namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 16 — the frozen tensor sector. Tests whether the graviton/tensor (Weyl) sector is ABSENT or
/// merely FROZEN by conformal flatness, by counting the tensor degrees of freedom and activating the ψ
/// (non-conformal) mode of the reference metric. No new primitives.
/// </summary>
public static class TensorSector
{
    /// <summary>Total tensor (Weyl + graviton) degrees of freedom of the (d+1)-dim spacetime (0 for d≤2, &gt;0 for d≥3).</summary>
    public static double TensorDegreesOfFreedom(int d)
        => DimensionAnalysis.WeylComponents(d) + DimensionAnalysis.GravitonPolarizations(d);

    /// <summary>Reference-metric curvature for the ψ-perturbation h_ψ = diag(−e^{2ψ}, e^{−2ψ}) (ψ = b·x²).
    /// Zero for ψ=0 (flat η, frozen); non-zero for ψ≠0 (the non-conformal/tensor mode is active).</summary>
    public static double ReferenceCurvature(double x, double b) => MetricAnsatzAudit.ReferenceRicciScalar(x, b);
}
