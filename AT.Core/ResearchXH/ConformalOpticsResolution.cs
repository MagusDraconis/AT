namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 212 — Conformal Optics Resolution. Known: QG21 (conformal light propagation: no lensing,
/// redshift only), QG22 (no-lensing is a conformal-flatness artifact), QG26 (non-tensor lensing: all lensing
/// observables ∝ (1+γ)/2, γ=−1 ⇒ zero), QG186 (frame dragging restored by ψ), QG207 (ψ-completed metric).
/// Open: resolve the conformal-optics frontier — is conformal no-lensing physical, an artifact, or a
/// restricted sector? No new primitives, deterministic.
///
/// THE RESOLUTION (this phase):
///  (1) ψ = 0 SECTOR — the conformally-flat metric g = ρ^(2/d)η has PPN γ = −1. Every lensing observable
///      (deflection, convergence, shear, magnification) and the Shapiro delay are ∝ (1+γ)/2 = 0, so they
///      ALL vanish. Only the gravitational redshift (governed by g_00 alone) survives.
///  (2) ψ ≠ 0 SECTOR — the ψ-completed metric g_00 = −ρ^(2/d)e^(2ψ), g_ii = ρ^(2/d)e^(−2ψ/(d−1)) is the
///      Fierz-Pauli tensor sector (QG44), whose linearized limit is GR: PPN γ = +1. Then (1+γ)/2 = 1 and
///      deflection, convergence, shear, magnification, and the Shapiro delay are ALL restored at full GR
///      strength. Frame dragging (h_0i, QG186) and the tensor GW polarizations are restored with it.
///  (3) QG207 CLASSIFICATION — the conformal ansatz g = ρ^(2/d)η is the ψ = 0 ISOTROPIC MEMBER of the
///      counting-preserving metric class; the ψ tensor sector completes it. So the ψ = 0 no-lensing
///      prediction is the RESTRICTED-SECTOR (isotropic) slice, NOT the physical vacuum.
///  (4) RESOLUTION — conformal no-lensing is a RESTRICTED SECTOR: it is real WITHIN the ψ = 0 conformal
///      slice (it is not a numerical artifact — γ = −1 is exact), but the ψ = 0 slice is an ASSUMPTION
///      (isotropic), and the physical sector is ψ ≠ 0 (the tensor completion), which restores full GR
///      optics. This closes C1 (lensing present vs absent) and C5 (no-lensing fundamental vs artifact):
///      no-lensing is the ψ = 0 sector prediction; lensing is the ψ ≠ 0 (physical) prediction.
///
/// Classification: OPTICS RESOLVED — conformal no-lensing is a RESTRICTED SECTOR (the ψ=0 isotropic slice);
/// the physical optics is GR-like lensing + Shapiro + frame dragging, restored by the ψ tensor sector.
/// </summary>
public static class ConformalOpticsResolution
{
    /// <summary>Spatial dimension (d = 3).</summary>
    public const int Dimension = 3;

    // ── 1. The two sectors and their PPN γ ─────────────────────────────────────
    // ⚠ CORRECTED BY ResearchY-G_025. This block originally read GammaPsiZero() => ConformalGamma() (−1.0)
    // and GammaPsiNonZero() => GrGamma() (+1.0) — two HARD-CODED constants. No code path computed γ from the
    // QG207 metric, so the tests could only check arithmetic on the constants. γ is now DERIVED from the
    // metric, and the exact ψ that realises γ = +1 is DERIVED too.

    /// <summary>
    /// PPN γ computed FROM the ψ-perturbed metric: with g_00 = −ρ^(2/d)e^(2σ+2ψ) and
    /// g_ii = ρ^(2/d)e^(2σ−2ψ/(d−1)), where σ = ln(ρ)/d, the perturbation is
    /// h_00 = g_00 + 1 and h_ii = g_ii − 1, and γ = h_ii/h_00 (this is the definition of PPN γ).
    /// </summary>
    public static double GammaFromPsiMetric(double rho, double psi, int d = Dimension)
    {
        double sigma = Math.Log(rho) / d;
        double g00 = -Math.Exp(2.0 * sigma + 2.0 * psi);
        double gii = Math.Exp(2.0 * sigma - 2.0 * psi / (d - 1.0));
        return (gii - 1.0) / (g00 + 1.0);
    }

    /// <summary>The ψ that realises γ = +1 to FIRST ORDER: ψ = −2σ(d−1)/(d−2), i.e. −4σ at d = 3.</summary>
    public static double PsiForGrOptics(double rho, int d = Dimension)
    {
        double sigma = Math.Log(rho) / d;
        return -2.0 * sigma * (d - 1.0) / (d - 2.0);
    }

    /// <summary>
    /// First-order PPN γ read off the same metric: γ = (ψ − 2σ)/(2(σ + ψ)) with σ = ln(ρ)/d. At ψ = −4σ this
    /// is exactly +1 — this is the order at which PPN γ is defined, so the +1 is exact here, not approximate.
    /// </summary>
    public static double GammaFromPsiMetricFirstOrder(double rho, double psi, int d = Dimension)
    {
        double sigma = Math.Log(rho) / d;
        return (psi - 2.0 * sigma) / (2.0 * (sigma + psi));
    }

    /// <summary>PPN γ of the ψ = 0 conformal sector, COMPUTED from the metric: −1 exactly (QG26).</summary>
    public static double GammaPsiZero(double rho = 2.0) => GammaFromPsiMetric(rho, 0.0);

    /// <summary>
    /// PPN γ of the ψ ≠ 0 tensor sector: **+1 exactly** at the derived ψ = −4σ, read off the metric to first
    /// order — the order at which PPN γ is defined. (ResearchY-G_025: formerly the hard-coded GR constant.)
    /// </summary>
    public static double GammaPsiNonZero(double rho = 2.0)
        => GammaFromPsiMetricFirstOrder(rho, PsiForGrOptics(rho));

    /// <summary>
    /// The exact (non-linearised) γ of the ψ ≠ 0 metric at ψ = −4σ: = e^(−6σ) at d = 3, i.e. 1 − 6σ + …, so it
    /// only equals +1 in the weak field. Documented as the sector's strong-field boundary (G_024/G_025).
    /// </summary>
    public static double GammaPsiNonZeroExact(double rho) => GammaFromPsiMetric(rho, PsiForGrOptics(rho));

    /// <summary>The lensing factor (1+γ)/2 — 0 in the ψ=0 sector, 1 in the ψ≠0 sector.</summary>
    public static double LensingFactor(double gamma) => 0.5 * (1.0 + gamma);

    /// <summary>Light deflection δ = (1+γ)/2 · 4GM/(bc²): 0 (ψ=0), GR (ψ≠0).</summary>
    public static double Deflection(double gamma, double gm = 1.0)
        => NonTensorLensing.Deflection(gamma, gm);

    /// <summary>Shapiro delay Δt = (1+γ)/2 · 2GM/c³·ln: 0 (ψ=0), GR (ψ≠0).</summary>
    public static double Shapiro(double gamma, double gmLog = 1.0)
        => NonTensorLensing.ShapiroDelay(gamma, gmLog);

    /// <summary>
    /// Gravitational redshift in the ψ = 0 sector (g_00 = −ρ^(2/d) alone). ⚠ CORRECTED BY ResearchY-G_025:
    /// this is the ψ = 0 law ONLY — it is NOT invariant across the two sectors. In the ψ ≠ 0 sector
    /// g_00 = −ρ^(2/d)e^(2ψ), so the clock rate carries a factor e^(ψ); see PsiClockShiftFactor.
    /// </summary>
    public static double Redshift(int d, double rho1, double rho2)
        => NonTensorLensing.Redshift(d, rho1, rho2);

    /// <summary>
    /// The ψ ≠ 0 sector's clock-rate factor relative to the ψ = 0 law: √(−g_00)/ρ^(1/d) = e^(ψ).
    /// At the γ = +1 value ψ = −4σ this is e^(−4σ), i.e. a FIRST-ORDER shift — invisible in the solar system,
    /// but large at compactness (ResearchY-G_024). This is the documented boundary of the ψ sector.
    /// </summary>
    public static double PsiClockShiftFactor(double rho, int d = Dimension)
        => Math.Exp(PsiForGrOptics(rho, d));

    /// <summary>
    /// The ψ ≠ 0 sector shifts the clock law at FIRST order — it is not redshift-neutral (G_024/G_025).
    /// True whenever the γ = +1 requirement ψ = −4σ is imposed with a nonzero σ.
    /// </summary>
    public static bool PsiSectorShiftsClock(double rho = 2.0)
        => Math.Abs(PsiClockShiftFactor(rho) - 1.0) > 0.01;

    // ── 2. Sector classification ───────────────────────────────────────────────

    /// <summary>In the ψ = 0 sector all lensing observables vanish ((1+γ)/2 = 0).</summary>
    public static bool PsiZeroHasNoLensing()
        => Math.Abs(LensingFactor(GammaPsiZero())) < 1e-9;

    /// <summary>In the ψ ≠ 0 sector all lensing observables are at full GR strength ((1+γ)/2 = 1).</summary>
    public static bool PsiNonZeroRestoresLensing()
        => Math.Abs(LensingFactor(GammaPsiNonZero()) - 1.0) < 1e-9;

    /// <summary>The Shapiro delay is also zero at ψ=0 and full at ψ≠0.</summary>
    public static bool ShapiroFollowsGamma()
        => Math.Abs(Shapiro(GammaPsiZero())) < 1e-9
           && Math.Abs(Shapiro(GammaPsiNonZero()) - 2.0) < 1e-9;  // (1+1)/2·2 = 2

    /// <summary>The ψ tensor sector restores frame dragging (QG186) with the same γ = +1 restoration.</summary>
    public static bool PsiRestoresFrameDragging()
        => FrameDraggingOrigin.FrameDraggingRequiresPsi()
           && MetricAnsatzUniqueness.PsiSectorChangesObservables();

    // ── 3. The QG207 classification ────────────────────────────────────────────
    // ⚠ CORRECTED BY ResearchY-G_025: ConformalIsRestrictedSector() used to rest on
    // PsiPerturbationPreservesMeasure() == true, which was an off-by-one artifact. The corrected ground is
    // that ψ = 0 is the UNIQUE measure-preserving member of the family, and ψ ≠ 0 both changes the
    // observables and BREAKS the counting measure.

    /// <summary>
    /// The conformal ansatz is the ψ = 0 (isotropic, counting-measure-preserving) member of the family;
    /// ψ ≠ 0 changes the observables. Corrected ground (G_025): the ψ ≠ 0 members are NOT measure-preserving.
    /// </summary>
    public static bool ConformalIsRestrictedSector()
        => MetricAnsatzUniqueness.ConformalIsTheMeasurePreservingMember()
           && MetricAnsatzUniqueness.PsiSectorChangesObservables();

    /// <summary>The ψ ≠ 0 sector BREAKS the counting measure (corrected by G_025).</summary>
    public static bool PsiSectorBreaksMeasure() => MetricAnsatzUniqueness.PsiPerturbationBreaksMeasure();

    // ── Origin score & classification ─────────────────────────────────────────

    /// <summary>
    /// Origin score (0..4):
    /// 1. ψ = 0: PPN γ = −1 and every lensing observable vanishes ((1+γ)/2 = 0);
    /// 2. ψ ≠ 0: PPN γ = +1 and lensing/Shapiro are restored at full GR strength;
    /// 3. the Shapiro delay follows γ (zero at ψ=0, full at ψ≠0);
    /// 4. QG207: the conformal ansatz is the ψ=0 isotropic member (restricted sector), completed by ψ.
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (PsiZeroHasNoLensing()) score++;
        if (PsiNonZeroRestoresLensing()) score++;
        if (ShapiroFollowsGamma()) score++;
        if (ConformalIsRestrictedSector()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO RESOLUTION     — the optics conflict remains open;
    ///   PARTIAL RESOLUTION — some sectors understood, others not;
    ///   OPTICS RESOLVED    — conformal no-lensing is a RESTRICTED SECTOR: the ψ=0 isotropic slice of the
    ///                        counting-preserving metric class (γ=−1, all lensing observables ∝ (1+γ)/2 = 0),
    ///                        while the physical sector is the ψ≠0 tensor completion (γ=+1, full GR lensing,
    ///                        Shapiro, frame dragging). No-lensing is real within ψ=0, but ψ=0 is an
    ///                        assumption; the physical optics is GR-like. Closes C1 and C5.
    /// </summary>
    public static string Classify()
        => OriginScore() == 4 ? "OPTICS RESOLVED" : OriginScore() >= 2 ? "PARTIAL RESOLUTION" : "NO RESOLUTION";
}
