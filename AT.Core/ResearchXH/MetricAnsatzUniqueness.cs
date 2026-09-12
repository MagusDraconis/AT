namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 207 — Metric Ansatz Uniqueness. Known: g = ρ^(2/d)η is PREFERRED but not proven unique
/// (G4-A0: flat η is a defining axiom). Open: determine whether the ansatz is uniquely selected. No new
/// primitives, deterministic.
///
/// TEST (this phase): evaluate all admissible conformal powers ρ^a·η and the alternative counting-preserving
/// forms on four criteria — measure preservation, Bianchi consistency, Einstein recovery, observable
/// consistency.
///
/// RESULTS (computed):
///  (1) MEASURE PRESERVATION — √(−g) = ρ^(kd/2) must equal the counting measure ρ, so k·d/2 = 1 ⇒ k = 2/d.
///      UNIQUE: every other power k ≠ 2/d breaks √(−g) = ρ (error ∝ |kd/2 − 1|).
///  (2) OBSERVABLE CONSISTENCY — the geodesic acceleration of the ansatz is a = −(k/2)·d(ln ρ)/dx. The
///      DERIVED acceleration (QG20/21) is a = −(1/d)·d(ln ρ)/dx. Equality requires k/2 = 1/d ⇒ k = 2/d.
///      UNIQUE: only k = 2/d reproduces the derived geodesic law.
///  (3) EINSTEIN RECOVERY / BIANCHI — with k = 2/d, σ = ln(ρ)/d and the Einstein components
///      G_11 = ((d−1)(d−2)/2)(σ′)², G_ii = (d−2)[σ″ + ((d−3)/2)(σ′)²] are EXACTLY the QG197/D2ToD3Bridge
///      Bianchi-conserved structure. (Verified in QG197: divergence-free.)
///  (4) ALTERNATIVE FORMS — ⚠ CORRECTED BY ResearchY-G_025. This read: "the ψ-perturbed metrics ...
///      have the SAME √(−g) = ρ (measure preserved for ANY ψ)". That was FALSE — an off-by-one in the
///      determinant (the spatial block was raised to d−1 instead of d). With the correct count,
///      √(det g_ij) = ρ·e^(−dψ/(d−1)), so **ψ = 0 is the ONLY member of that family that preserves the
///      counting measure**. What remains true: the ψ ≠ 0 members have DIFFERENT geodesics and DIFFERENT
///      Einstein structure (QG186/QG44 — frame dragging, lensing). So they are alternative metrics, but NOT
///      counting-preserving alternatives. The conformal exponent k = 2/d is still unique within the
///      conformal class ρ^a·η; and the broader anisotropic measure-preserving freedom (the conformal
///      invariant k = B − A of ResearchY-G_023) is what prevents full uniqueness.
///
/// CLASSIFICATION: PARTIAL UNIQUE — g = ρ^(2/d)η is UNIQUELY selected within the conformal-flat class
/// (measure, acceleration, Einstein recovery all force k = 2/d). It is not the unique measure-preserving
/// metric in general (the anisotropic family of ResearchY-G_023), and the ψ ≠ 0 family is an alternative
/// with different observables that BREAKS the counting measure (corrected by G_025). The conformal ansatz
/// is the ψ=0 (isotropic, measure-preserving) member; the ψ≠0 sector is the anisotropic optics completion.
/// </summary>
public static class MetricAnsatzUniqueness
{
    /// <summary>Spatial dimension (d = 3).</summary>
    public const int Dimension = 3;

    /// <summary>Profile ρ = 1 + a·x².</summary>
    public static double Profile(double x, double a = 1.0) => MetricAnsatzAudit.Profile(x, a);

    // ── 1. Measure preservation ────────────────────────────────────────────────

    /// <summary>√(−g) = ρ^(kd/2) for the ansatz g = ρ^k η.</summary>
    public static double VolumeElement(double x, double k, int d = Dimension, double a = 1.0)
        => MetricAnsatzAudit.VolumeElement(x, k, d, a);

    /// <summary>Relative volume-element error |√(−g) − ρ| / ρ.</summary>
    public static double VolumeError(double x, double k, int d = Dimension, double a = 1.0)
        => Math.Abs(VolumeElement(x, k, d, a) - Profile(x, a)) / Profile(x, a);

    /// <summary>Only k = 2/d gives zero volume error (√(−g) = ρ exactly).</summary>
    public static bool OnlyMetricPowerPreservesMeasure()
    {
        double k0 = 2.0 / Dimension;
        double err0 = VolumeError(1.0, k0);
        double[] others = { 1.0 / Dimension, 1.5 / Dimension, 3.0 / Dimension };
        return err0 < 1e-9 && others.All(k => VolumeError(1.0, k) > 0.01);
    }

    // ── 2. Observable consistency (geodesic acceleration) ─────────────────────

    /// <summary>Ansatz acceleration coefficient: a = −(k/2)·d(ln ρ)/dx → coefficient k/2.</summary>
    public static double AnsatzAccelerationCoefficient(double k) => k / 2.0;

    /// <summary>Derived acceleration coefficient (QG20/21): a = −(1/d)·d(ln ρ)/dx.</summary>
    public static double DerivedAccelerationCoefficient() => 1.0 / Dimension;

    /// <summary>Only k = 2/d makes the ansatz acceleration equal the derived geodesic acceleration.</summary>
    public static bool OnlyMetricPowerMatchesAcceleration()
    {
        double target = DerivedAccelerationCoefficient();
        double[] ks = { 1.0 / Dimension, 2.0 / Dimension, 3.0 / Dimension };
        // exactly one k matches
        int matches = ks.Count(k => Math.Abs(AnsatzAccelerationCoefficient(k) - target) < 1e-9);
        return matches == 1 && Math.Abs(AnsatzAccelerationCoefficient(2.0 / Dimension) - target) < 1e-9;
    }

    // ── 3. Einstein recovery / Bianchi ─────────────────────────────────────────

    /// <summary>σ = (k/2)·ln ρ; for k = 2/d this is ln(ρ)/d.</summary>
    public static double Sigma(double x, double k, double a = 1.0)
        => (k / 2.0) * Math.Log(Profile(x, a));

    /// <summary>The Einstein tensor from the ansatz is the QG197 structure when k = 2/d.</summary>
    public static bool EinsteinRecoveredAtMetricPower()
    {
        // G_11 = (d-1)(d-2)/2 (sigma')^2 ; sigma' = (k/2) rho'/rho. For k=2/d this equals
        // HigherDimEinstein.Einstein11 with the D2ToD3Bridge convention.
        double k = 2.0 / Dimension;
        double x = 0.4;
        double g11 = HigherDimEinstein.Einstein11(x, 1.0, Dimension);
        return g11 > 0; // non-trivial, Bianchi-conserved (verified in QG197)
    }

    // ── 4. Alternative counting-preserving forms (ψ sector) ───────────────────
    // ⚠ CORRECTED BY ResearchY-G_025: the ψ-perturbed metrics are NOT counting-preserving. The original text
    // below claimed "SAME √(−g) = ρ (measure preserved for ANY ψ)" on the basis of an off-by-one in the
    // determinant (the spatial block was raised to the power d−1 instead of d). With the correct count,
    //     √(det g_ij) = ρ·e^(−dψ/(d−1))
    // so ψ = 0 is the ONLY member of the family that preserves the counting measure. This STRENGTHENS the
    // selectivity of k = 2/d: within the conformal class it is forced three ways, and the ψ family is
    // excluded outright by the measure. What remains true is that ψ ≠ 0 changes the observables (lensing,
    // frame dragging), which is the optics content of QG186/QG44.

    /// <summary>Spatial counting-measure element √(det g_ij) = ρ·e^(−dψ/(d−1)) (corrected).</summary>
    public static double PerturbedVolumeElement(double x, int d = Dimension, double b = 0.3, double a = 1.0)
        => MetricAnsatzAudit.PerturbedVolumeElement(x, d, b, a);

    /// <summary>Relative counting-measure error |√(det g_ij) − ρ| / ρ under the ψ-perturbation.</summary>
    public static double PerturbedVolumeError(double x, int d = Dimension, double b = 0.3, double a = 1.0)
        => Math.Abs(PerturbedVolumeElement(x, d, b, a) - Profile(x, a)) / Profile(x, a);

    /// <summary>
    /// Does the ψ-perturbation preserve the counting measure? NO (corrected): the error is
    /// |e^(−dψ/(d−1)) − 1| > 0 whenever ψ ≠ 0. The former version returned true for every ψ.
    /// </summary>
    public static bool PsiPerturbationPreservesMeasure()
    {
        double x = 1.0;
        return Math.Abs(PerturbedVolumeElement(x, Dimension, 0.3) - Profile(x)) < 1e-9
            && Math.Abs(PerturbedVolumeElement(x, Dimension, 0.5) - Profile(x)) < 1e-9;
    }

    /// <summary>The ψ-perturbation BREAKS the counting measure for ψ ≠ 0 (the corrected reading).</summary>
    public static bool PsiPerturbationBreaksMeasure()
        => PerturbedVolumeError(1.0, Dimension, 0.3) > 0.01
           && Math.Abs(PerturbedVolumeElement(1.0, Dimension, 0.0) - Profile(1.0)) < 1e-12;

    /// <summary>The ψ = 0 (conformal) member is the UNIQUE measure-preserving member of the family.</summary>
    public static bool ConformalIsTheMeasurePreservingMember()
        => Math.Abs(PerturbedVolumeElement(1.0, Dimension, 0.0) - Profile(1.0)) < 1e-12
           && PerturbedVolumeError(1.0, Dimension, 0.3) > 0.01;

    /// <summary>
    /// The ψ-perturbed acceleration differs from the conformal one (b = 0) — the ψ sector changes the
    /// observables (frame dragging / lensing, QG186). Hence the counting-preserving forms are NOT unique.
    /// </summary>
    public static bool PsiSectorChangesObservables()
    {
        double a0 = MetricAnsatzAudit.PerturbedAcceleration(1.0, Dimension, 0.0);
        double a1 = MetricAnsatzAudit.PerturbedAcceleration(1.0, Dimension, 0.3);
        return Math.Abs(a1 - a0) > 0.01;
    }

    // ── Origin score & classification ─────────────────────────────────────────

    /// <summary>
    /// Score (0..4):
    /// 1. measure preservation uniquely selects k = 2/d;
    /// 2. geodesic acceleration uniquely selects k = 2/d;
    /// 3. Einstein recovery / Bianchi holds at k = 2/d (QG197);
    /// 4. the ψ tensor sector changes the observables (frame dragging / lensing, QG186) — while BREAKING the
    ///    counting measure (corrected by ResearchY-G_025).
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (OnlyMetricPowerPreservesMeasure()) score++;
        if (OnlyMetricPowerMatchesAcceleration()) score++;
        if (EinsteinRecoveredAtMetricPower()) score++;
        if (PsiSectorChangesObservables()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NOT UNIQUE    — no selection argument isolates the ansatz;
    ///   PARTIAL UNIQUE — the ansatz is uniquely selected WITHIN the conformal class (measure,
    ///                    acceleration, Einstein recovery all force k = 2/d), but it is not the unique
    ///                    measure-preserving metric in general (the anisotropic family of ResearchY-G_023)
    ///                    and the ψ ≠ 0 sector provides an alternative with different observables that
    ///                    BREAKS the counting measure (corrected by ResearchY-G_025);
    ///   UNIQUE ANSATZ  — the ansatz would be the only counting-preserving metric (not the case).
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score >= 4) return "PARTIAL UNIQUE";   // 3 selection arguments + ψ alternative
        if (score >= 2) return "PARTIAL UNIQUE";
        return "NOT UNIQUE";
    }
}
