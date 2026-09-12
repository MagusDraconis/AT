namespace AT.Core.ResearchXH;

/// <summary>
/// G4-A Phase 0 — audit the metric ansatz g = ρ^(2/d)η. Tests which of five requirements (scale invariance,
/// volume-element consistency, counting-measure preservation, conformal covariance, uniqueness) select the
/// exponent k = 2/d, and whether conformal flatness itself is derived or assumed. No new primitives.
/// </summary>
public static class MetricAnsatzAudit
{
    /// <summary>Standard profile ρ = 1 + a·x².</summary>
    public static double Profile(double x, double a = 1.0) => 1.0 + a * x * x;

    /// <summary>g_00 = −ρ^k for the ansatz g = ρ^k η.</summary>
    public static double G00(double x, double k, double a = 1.0) => -Math.Pow(Profile(x, a), k);

    /// <summary>g_11 = ρ^k for the ansatz g = ρ^k η.</summary>
    public static double G11(double x, double k, double a = 1.0) => Math.Pow(Profile(x, a), k);

    /// <summary>√(−g) = ρ^(kd/2) for the ansatz g = ρ^k η.</summary>
    public static double VolumeElement(double x, double k, int d, double a = 1.0)
        => Math.Pow(Profile(x, a), k * d / 2.0);

    /// <summary>Relative volume-element error |√(−g) − ρ| / ρ for the ansatz g = ρ^k η.</summary>
    public static double VolumeError(double x, double k, int d, double a = 1.0)
        => Math.Abs(VolumeElement(x, k, d, a) - Profile(x, a)) / Profile(x, a);

    /// <summary>Geodesic acceleration a = −(k/2) d(ln ρ)/dx (central difference).</summary>
    public static double Acceleration(double x, double k, double a = 1.0, double h = 1e-6)
    {
        double lp = Math.Log(Profile(x + h, a)) - Math.Log(Profile(x - h, a));
        return -(k / 2.0) * lp / (2.0 * h);
    }

    /// <summary>d(ln ρ)/dx for the profile ρ = 1 + a·x² (central difference).</summary>
    public static double LogDerivative(double x, double a = 1.0, double h = 1e-6)
        => (Math.Log(Profile(x + h, a)) - Math.Log(Profile(x - h, a))) / (2.0 * h);

    /// <summary>d(ln ρ)/dx for an arbitrary profile ρ(x) (central difference).</summary>
    public static double LogDerivativeOf(Func<double, double> rho, double x, double h = 1e-6)
        => (Math.Log(rho(x + h)) - Math.Log(rho(x - h))) / (2.0 * h);

    // ── Non-conformally-flat counterexample ─────────────────────────────────────────
    // g_00 = −ρ^(2/d) e^{2ψ}, g_ii = ρ^(2/d) e^{−2ψ/(d−1)} (ψ = b·x), in 1 time + d space dimensions.
    //
    // ⚠ CORRECTED BY ResearchY-G_025. This block originally read
    //     "det g = −ρ^(2/d)e^{2ψ} · (ρ^(2/d)e^{−2ψ/(d−1)})^(d−1) = −ρ², so √(−g) = ρ (unchanged)"
    // and PerturbedVolumeElement returned ρ by construction. THE EXPONENT WAS WRONG — the spatial block has
    // d factors, not d−1 (there is one g_ii per spatial direction). Correctly,
    //     det g        = −ρ^(2/d)e^{2ψ} · (ρ^(2/d)e^{−2ψ/(d−1)})^d = −ρ^(2(d+1)/d) · e^(−2ψ/(d−1))
    //     √(−det g)    = ρ^((d+1)/d) · e^(−ψ/(d−1))            (4-volume)
    //     √(det g_ij)  = ρ · e^(−dψ/(d−1))                     (spatial — the counting measure)
    // So the ψ-perturbed metric does NOT preserve the counting measure: **ψ = 0 is the only member that does**.
    // The off-by-one had propagated to MetricAnsatzUniqueness.PsiPerturbationPreservesMeasure,
    // ConformalOpticsResolution.ConformalIsRestrictedSector, TRMCompatibilityAudit.MetricOriginPreserved and
    // TRMasUVCompletion; all are corrected in ResearchY-G_025. The ψ ≠ 0 sector is still the optics sector —
    // that conclusion stands — but it is NOT counting-preserving, and the conformal slice is the unique
    // measure-preserving member.

    /// <summary>g_00 for the ψ-perturbed metric.</summary>
    public static double PerturbedG00(double x, int d, double b = 0.3, double a = 1.0)
        => -Math.Pow(Profile(x, a), 2.0 / d) * Math.Exp(2.0 * b * x);

    /// <summary>g_11 for the ψ-perturbed metric (ψ = b·x).</summary>
    public static double PerturbedG11(double x, int d, double b = 0.3, double a = 1.0)
        => Math.Pow(Profile(x, a), 2.0 / d) * Math.Exp(-2.0 * b * x / (d - 1.0));

    /// <summary>det g = −ρ^(2(d+1)/d)·e^(−2ψ/(d−1)) for the ψ-perturbed metric (corrected: d spatial factors).</summary>
    public static double PerturbedDet(double x, int d, double b = 0.3, double a = 1.0)
        => -Math.Pow(Profile(x, a), 2.0 * (d + 1.0) / d) * Math.Exp(-2.0 * b * x / (d - 1.0));

    /// <summary>4-volume element √(−det g) = ρ^((d+1)/d)·e^(−ψ/(d−1)); equals ρ only at ψ = 0.</summary>
    public static double PerturbedVolumeElement4D(double x, int d, double b = 0.3, double a = 1.0)
        => Math.Pow(Profile(x, a), (d + 1.0) / d) * Math.Exp(-b * x / (d - 1.0));

    /// <summary>
    /// Spatial counting-measure element √(det g_ij) = ρ·e^(−dψ/(d−1)) for the ψ-perturbed metric.
    /// Equals the counting measure ρ **only at ψ = 0** (corrected by ResearchY-G_025; the former value was
    /// ρ for every ψ, which was an off-by-one artifact).
    /// </summary>
    public static double PerturbedVolumeElement(double x, int d, double b = 0.3, double a = 1.0)
        => Profile(x, a) * Math.Exp(-d * b * x / (d - 1.0));

    /// <summary>Geodesic acceleration a = +(1/2) g^11 ∂_1 g_00 for the ψ-perturbed metric.</summary>
    public static double PerturbedAcceleration(double x, int d, double b = 0.3, double a = 1.0, double h = 1e-6)
    {
        double g11 = PerturbedG11(x, d, b, a);
        double dg00 = (PerturbedG00(x + h, d, b, a) - PerturbedG00(x - h, d, b, a)) / (2.0 * h);
        return 0.5 * dg00 / g11;
    }

    // ── G4-A Phase 1: selecting η (minimum-curvature reference) ─────────────────────

    /// <summary>
    /// Ricci scalar of the d=2 reference metric h_ψ = diag(−e^{2ψ}, e^{−2ψ}) (det = −1), with ψ = b·x²:
    /// R = (2ψ″ + 4ψ′²)·e^{2ψ} = (4b + 16b²x²)·e^{2bx²}. Zero iff ψ=const (flat η); grows with |b|.
    /// </summary>
    public static double ReferenceRicciScalar(double x, double b)
    {
        double psi = b * x * x;
        double dpsi = 2.0 * b * x;
        double ddpsi = 2.0 * b;
        return (2.0 * ddpsi + 4.0 * dpsi * dpsi) * Math.Exp(2.0 * psi);
    }
}
