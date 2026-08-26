namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 32 — TRM compatibility audit. The TRM kernel is the ψ (non-conformal) sector (QG31). Here we audit
/// which existing AT derivations survive if that kernel is added. The ψ-perturbation was chosen (MetricAnsatzAudit)
/// to preserve det g = −ρ², i.e. √(−g) = ρ, so the metric-origin derivation is UNCHANGED; the Einstein tensor gains
/// ψ/Weyl terms (MODIFIED); everything built on the scalar ρ alone (counting measure, matter = deficit, the α=0
/// attractor, critical branching) is UNCHANGED because ψ does not touch the 1-point scalar dynamics. No new primitives.
/// </summary>
public static class TRMCompatibilityAudit
{
    /// <summary>The six existing derivations audited for compatibility with the added TRM (ψ) kernel.</summary>
    public static readonly string[] Derivations =
    {
        "counting-measure",       // rho counts Q-events
        "metric-origin",          // sqrt(-g) = rho
        "matter-deficit",         // m = rho-bar - rho
        "einstein-structure",     // G_mu-nu from sigma = (1/d) ln rho
        "alpha-zero-attractor",   // scale-free rho dynamics
        "critical-branching",     // branching statistics generate rho
    };

    /// <summary>Classification of each derivation under the added ψ kernel.</summary>
    public static string Classify(string derivation) => derivation switch
    {
        "counting-measure" => "UNCHANGED",       // rho counts ticks; psi doesn't change the 1-point rho
        "metric-origin" => "UNCHANGED",          // det g = -rho^2 is independent of psi (MetricAnsatzAudit)
        "matter-deficit" => "UNCHANGED",         // m = rho-bar - rho is scalar; psi doesn't touch it
        "einstein-structure" => "MODIFIED",      // G_mu-nu gains psi/Weyl terms (extra curvature)
        "alpha-zero-attractor" => "UNCHANGED",   // scale-space diffusion of rho is independent of psi
        "critical-branching" => "UNCHANGED",     // branching statistics generate rho, not psi
        _ => throw new ArgumentOutOfRangeException(nameof(derivation))
    };

    /// <summary>√(−g) for the ψ-perturbed metric (same √(−g) = ρ as the conformally-flat ansatz).</summary>
    public static double PerturbedVolumeElement(int d, double x, double b = 0.3, double a = 1.0)
        => MetricAnsatzAudit.PerturbedVolumeElement(x, d, b, a);

    /// <summary>Standard profile ρ = 1 + a·x².</summary>
    public static double Profile(double x, double a = 1.0) => MetricAnsatzAudit.Profile(x, a);

    /// <summary>Relative volume-element error |√(−g) − ρ| / ρ under the ψ-perturbation: must be 0.</summary>
    public static double PerturbedVolumeError(int d, double x, double b = 0.3, double a = 1.0)
        => Math.Abs(PerturbedVolumeElement(d, x, b, a) - Profile(x, a)) / Profile(x, a);

    /// <summary>Is √(−g) = ρ preserved by ψ? Yes (the ψ-perturbation is volume-preserving).</summary>
    public static bool MetricOriginPreserved(int d, double x, double b = 0.3, double a = 1.0, double tol = 1e-12)
        => PerturbedVolumeError(d, x, b, a) < tol;
}
