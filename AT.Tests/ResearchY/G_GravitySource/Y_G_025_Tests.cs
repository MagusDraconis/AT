using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_025 — Optics Determinant Correction Audit (group G — Gravity Source).
///
/// WHY. G_024 restored the AT-QG optics resolution (QG212) on the strength of its own documentation. This audit
/// INDEPENDENTLY VERIFIES the underlying AT-QG arithmetic — and finds a real defect.
///
/// THE DEFECT. AT.Core/ResearchXH/MetricAnsatzAudit.cs stated, and MetricAnsatzUniqueness used as a premise:
///     "g_00 = −ρ^(2/d)e^(2ψ), g_ii = ρ^(2/d)e^(−2ψ/(d−1)) ... det g = −ρ^(2/d)e^{2ψ}·(ρ^(2/d)e^{−2ψ/(d−1)})^(d−1)
///      = −ρ², so √(−g) = ρ (unchanged)"
/// and PerturbedVolumeElement returned Profile(x) = ρ BY CONSTRUCTION. THE EXPONENT IS AN OFF-BY-ONE: the
/// spatial block has **d** factors (one g_ii per spatial direction), not d−1. Correctly
///     det g       = −ρ^(2/d)e^{2ψ} · (ρ^(2/d)e^{−2ψ/(d−1)})^d = −ρ^(2(d+1)/d)·e^(−2ψ/(d−1))
///     √(−det g)   = ρ^((d+1)/d)·e^(−ψ/(d−1))        4-volume
///     √(det g_ij) = ρ·e^(−dψ/(d−1))                 spatial (the counting measure)
/// so at d = 3, x = 1, b = 0.3: the spatial element is 1.2752563032, NOT ρ = 2.0000000000 (a 36.24 % error);
/// the 4-volume is 2.1688481946, not 2.0000000000 (8.44 %). The error is UNBOUNDED in ψ: at b = −3 the
/// spatial element is 180.0342626010 versus the claimed 2.0000000000 — 8902 % off.
///
/// WHAT IT BROKE (four places, all now corrected):
///   1. MetricAnsatzUniqueness.PsiPerturbationPreservesMeasure() — returned true for EVERY ψ.
///   2. ConformalOpticsResolution.ConformalIsRestrictedSector() — rested on (1).
///   3. TRMCompatibilityAudit.MetricOriginPreserved() and its matrix row ("metric-origin" => "UNCHANGED").
///   4. TRMasUVCompletion.TrmCoreVolumeElement doc ("volume-preserving → regular core unchanged").
///
/// A SECOND, INDEPENDENT DEFECT. ConformalOpticsResolution.GammaPsiZero() => ConformalGamma() (−1.0) and
/// GammaPsiNonZero() => GrGamma() (+1.0) were HARD-CODED CONSTANTS. No code path computed γ from the metric,
/// so the origin-score items 2 and 3 were arithmetic on a constant: (1+1)/2 = 1 and Shapiro(1) = 2. The audit
/// therefore cannot have detected that the coded metric ψ = b·x does NOT have γ = +1 (its computed γ wanders:
/// +0.3352 at x = 0.1, +0.0022 at 0.5, −0.0930 at 1.0, −0.0694 at 2.0 for b = 0.3).
///
/// WHAT IS CORRECTED, AND WHAT SURVIVES.
///   * γ is now DERIVED from the metric: γ = (g_ii − 1)/(g_00 + 1). ψ = 0 gives **−1 exactly**; ψ = −4σ (the
///     derived requirement, PsiForGrOptics) gives **+1 exactly** TO FIRST ORDER. So the two-sector physics
///     SURVIVES — but now on derived rather than asserted grounds.
///   * The measure-preservation premise is REFUTED: ψ ≠ 0 BREAKS the counting measure. This STRENGTHENS the
///     selectivity result — ψ = 0 is the UNIQUE measure-preserving member of the family.
///   * The exact (non-linearised) γ at ψ = −4σ is e^(6σ) = ρ² at d = 3 — it equals +1 only in the weak field.
///     Together with the clock side-effect (√(−g_00) = ρ^(1/d)e^(−ψ) picks up e^(−4σ)) this is the documented
///     strong-field boundary of the ψ sector (G_024's flag, now quantified in AT.Core).
///
/// VERDICTS
///   DERIVED   the corrected determinant identities; √(det g_ij) = ρ ⟺ ψ = 0; γ = −1 at ψ = 0 and γ = +1 (to
///             first order) at ψ = −4σ, both computed from the metric; the exact drift e^(6σ) = ρ²; the clock
///             shift factor e^(−4σ).
///   BOUNDARY  the exact (non-linearised) ψ completion — γ drifts as ρ² and the clock shifts by e^(−4σ), so
///             the strong-field behaviour depends on the O(x²) completion, which is still unspecified.
///   REFUTED   the off-by-one determinant and every claim resting on it: "√(−g) = ρ is unchanged for ANY ψ",
///             "the ψ sector gives counting-preserving alternatives", "metric-origin => UNCHANGED", and the
///             hard-coded γ.
///
/// Deterministic: exact algebra on AT's own constructions. The AT-QG optics conclusion (two sectors; ψ supplies
/// the optics) is RESTORED, not overturned — but its measure-preservation premise and one origin-score basis
/// are withdrawn, and γ is now computed rather than asserted.
/// </summary>
public class Y_G_025_Tests : ResearchTestBase
{
    public Y_G_025_Tests(ITestOutputHelper output) : base(output) { }

    private const int D = 3;
    private const double B = 0.3;

    // ── 1. the corrected determinant identities ──────────────────────────────────

    [Fact]
    public void Y_G_025_DeterminantCorrection()
    {
        PrintHeader("1. The determinant correction: d spatial factors, not d − 1");

        foreach (double x in new[] { -1.0, -0.5, 0.5, 1.0, 2.0 })
        {
            double psi = B * x, rho = MetricAnsatzAudit.Profile(x);

            // closed forms
            double spatial = rho * Math.Exp(-D * psi / (D - 1.0));
            double four = Math.Pow(rho, (D + 1.0) / D) * Math.Exp(-psi / (D - 1.0));
            double det = -Math.Pow(rho, 2.0 * (D + 1.0) / D) * Math.Exp(-2.0 * psi / (D - 1.0));

            Assert.True(Math.Abs(MetricAnsatzAudit.PerturbedVolumeElement(x, D, B) - spatial) < 1e-12);
            Assert.True(Math.Abs(MetricAnsatzAudit.PerturbedVolumeElement4D(x, D, B) - four) < 1e-12);
            Assert.True(Math.Abs(MetricAnsatzAudit.PerturbedDet(x, D, B) - det) < 1e-12);
            Assert.True(Math.Abs(Math.Sqrt(-det) - four) < 1e-12);

            // and the determinant built directly from the metric components, with d spatial factors
            double g00 = MetricAnsatzAudit.PerturbedG00(x, D, B);
            double g11 = MetricAnsatzAudit.PerturbedG11(x, D, B);
            double direct = g00 * Math.Pow(g11, D);
            Assert.True(Math.Abs(direct - det) < 1e-12, $"x = {x}: direct det {direct} vs {det}");
        }

        double xs = 1.0, ps = B * xs, rhos = MetricAnsatzAudit.Profile(xs);
        Output.WriteLine($"x = {xs}, ψ = {ps}, ρ = {rhos}");
        Output.WriteLine($"  CORRECTED spatial √(det g_ij) = ρ·e^(−dψ/(d−1)) = {MetricAnsatzAudit.PerturbedVolumeElement(xs, D, B):F10}");
        Output.WriteLine($"  CORRECTED 4-volume √(−det g)  = ρ^((d+1)/d)e^(−ψ/(d−1)) = {MetricAnsatzAudit.PerturbedVolumeElement4D(xs, D, B):F10}");
        Output.WriteLine($"  former claim                  = ρ = {rhos:F10}   (spatial error {(MetricAnsatzAudit.PerturbedVolumeElement(xs, D, B) - rhos) / rhos:P2})");
        Output.WriteLine("");
        Output.WriteLine("The former derivation raised the spatial block to (d−1) = 2; there are d = 3 spatial factors.");
        Assert.True(Math.Abs(MetricAnsatzAudit.PerturbedVolumeElement(xs, D, B) - rhos) / rhos > 0.3);
    }

    // ── 2. the measure is NOT preserved — ψ = 0 is the unique member ─────────────

    [Fact]
    public void Y_G_025_MeasureIsNotPreserved()
    {
        PrintHeader("2. REFUTED: '√(−g) = ρ is preserved for ANY ψ' — ψ = 0 is the unique member");

        Assert.False(MetricAnsatzUniqueness.PsiPerturbationPreservesMeasure());
        Assert.True(MetricAnsatzUniqueness.PsiPerturbationBreaksMeasure());
        Assert.True(MetricAnsatzUniqueness.ConformalIsTheMeasurePreservingMember());

        // ψ = 0 preserves exactly at every sample point; ψ ≠ 0 fails everywhere ψ ≠ 0.
        var sb = new StringBuilder();
        sb.AppendLine("   x       √(det g_ij) at ψ≠0      ρ          error        √(det g_ij) at ψ=0");
        foreach (double x in new[] { -1.0, -0.5, 0.0, 0.5, 1.0 })
        {
            double rho = MetricAnsatzAudit.Profile(x);
            double p = MetricAnsatzAudit.PerturbedVolumeElement(x, D, B);
            double p0 = MetricAnsatzAudit.PerturbedVolumeElement(x, D, 0.0);
            sb.AppendLine($"   {x,5:F2} {p,20:F10} {rho,10:F6} {(p - rho) / rho,12:P2} {p0,20:F10}");
            Assert.True(Math.Abs(p0 - rho) < 1e-12, "ψ = 0 must preserve the measure");
            if (x != 0.0) Assert.True(Math.Abs(p - rho) / rho > 0.1, $"ψ≠0 must break the measure at x = {x}");
        }
        Output.WriteLine(sb.ToString().TrimEnd());
        Output.WriteLine("");
        Output.WriteLine("The error is UNBOUNDED in ψ (at b = −3 it reaches 8902 %), which is why the hard-coded");
        Output.WriteLine("return value could never have been caught by the shipped tests.");
    }

    // ── 3. γ is now DERIVED from the metric ──────────────────────────────────────

    [Fact]
    public void Y_G_025_GammaIsDerivedNotAsserted()
    {
        PrintHeader("3. γ is now COMPUTED from the metric, not returned as a constant");

        foreach (double rho in new[] { 1.000001, 1.5, 2.0, 3.0 })
        {
            // ψ = 0 -> γ = −1 EXACTLY
            Assert.Equal(-1.0, ConformalOpticsResolution.GammaPsiZero(rho), 12);
            // ψ = −4σ -> γ = +1 EXACTLY at first order (the order at which PPN γ is defined)
            Assert.Equal(1.0, ConformalOpticsResolution.GammaPsiNonZero(rho), 12);
            // the exact (non-linearised) value drifts as e^(6σ) = ρ² at d = 3
            Assert.True(Math.Abs(ConformalOpticsResolution.GammaPsiNonZeroExact(rho) - Math.Pow(rho, 2.0)) < 1e-9);
        }
        Output.WriteLine("ρ          γ(ψ=0, exact)   γ(ψ=−4σ, 1st order)   γ(ψ=−4σ, exact)   ρ² check");
        foreach (double rho in new[] { 1.000001, 1.5, 2.0 })
            Output.WriteLine($"{rho,-10:F6} {ConformalOpticsResolution.GammaPsiZero(rho),+17:F10} " +
                             $"{ConformalOpticsResolution.GammaPsiNonZero(rho),+21:F10} " +
                             $"{ConformalOpticsResolution.GammaPsiNonZeroExact(rho),+17:F10} {Math.Pow(rho, 2.0),+10:F6}");
        Output.WriteLine("");
        Output.WriteLine("So the old GR constant (+1) was CORRECT — but only at ψ = −4σ, a ψ the old code never imposed.");
        Output.WriteLine("The exact value drifts as ρ²: γ = +1 holds only in the weak field, which is the BOUNDARY.");

        // the ψ that γ = +1 requires, derived
        foreach (double rho in new[] { 1.5, 2.0 })
        {
            double sigma = Math.Log(rho) / D;
            Assert.Equal(-4.0, ConformalOpticsResolution.PsiForGrOptics(rho) / sigma, 12);
        }
        Output.WriteLine("PsiForGrOptics(ρ) = −4σ at d = 3 (derived): the requirement the old code never stated.");
    }

    // ── 4. the coded ψ = b·x form cannot realise γ = +1 ──────────────────────────

    [Fact]
    public void Y_G_025_CodedPsiFormCannotGiveGammaPlusOne()
    {
        PrintHeader("4. The coded perturbation ψ = b·x does NOT have γ = +1");

        var sb = new StringBuilder();
        sb.AppendLine("   b      γ at x=0.1     x=0.5      x=1.0      x=2.0");
        foreach (double b in new[] { 0.0, 0.3, 0.5 })
        {
            var cells = new List<string>();
            foreach (double x in new[] { 0.1, 0.5, 1.0, 2.0 })
            {
                double rho = MetricAnsatzAudit.Profile(x);
                double g = ConformalOpticsResolution.GammaFromPsiMetric(rho, b * x, D);
                cells.Add($"{g,+10:F4}");
                if (b != 0.0) Assert.True(Math.Abs(g - 1.0) > 1e-3, $"b = {b}, x = {x}: γ should not be +1");
            }
            sb.AppendLine($"   {b,-5:F1} " + string.Join(" ", cells));
        }
        Output.WriteLine(sb.ToString().TrimEnd());
        Output.WriteLine("");
        Output.WriteLine("γ ≠ +1 anywhere for the coded ψ — it wanders (+0.3352, +0.0022, −0.0930, −0.0694 at b = 0.3).");
        Output.WriteLine("γ = +1 needs ψ = −(4/3)ln(1+ax²), which is QUADRATIC at small x; the code uses ψ = b·x, LINEAR.");
        Output.WriteLine("Nothing detected this because γ was returned as a constant.");
        Assert.True(true);
    }

    // ── 5. the clock side-effect (the boundary this exposes) ─────────────────────

    [Fact]
    public void Y_G_025_PsiShiftsTheClock()
    {
        PrintHeader("5. The ψ completion is NOT redshift-neutral — the sector's boundary");

        // √(−g_00) = ρ^(1/d)e^(ψ); at the γ = +1 value ψ = −4σ the clock factor is e^(−4σ) = ρ^(−4/3).
        foreach (double rho in new[] { 1.5, 2.0 })
        {
            double expected = Math.Pow(rho, -4.0 / 3.0);
            Assert.True(Math.Abs(ConformalOpticsResolution.PsiClockShiftFactor(rho) - expected) < 1e-12);
        }
        Assert.True(ConformalOpticsResolution.PsiSectorShiftsClock(2.0));
        Assert.True(ConformalOpticsResolution.PsiSectorBreaksMeasure());

        Output.WriteLine("ρ = 2: the γ = +1 requirement ψ = −4σ gives a clock-rate factor e^(ψ) = 2^(−4/3) = " +
                         $"{ConformalOpticsResolution.PsiClockShiftFactor(2.0):F6}");
        Output.WriteLine("");
        Output.WriteLine("So the sector that supplies the optics also shifts the clock law at FIRST order — invisible in the");
        Output.WriteLine("solar system (G_024: 2.78e−9 at the Earth's surface) but large at compactness. This is the");
        Output.WriteLine("documented BOUNDARY: the exact (non-linearised) completion is still unspecified.");
    }

    // ── 6. the correction propagated to the dependent results ────────────────────

    [Fact]
    public void Y_G_025_PropagationCorrected()
    {
        PrintHeader("6. The dependent results, corrected");

        // TRM compatibility: metric-origin is MODIFIED, not UNCHANGED
        Assert.False(TRMCompatibilityAudit.MetricOriginPreserved(D, 1.0, B));
        Assert.True(TRMCompatibilityAudit.MetricOriginPreserved(D, 1.0, 0.0));
        Assert.Equal("MODIFIED", TRMCompatibilityAudit.Classify("metric-origin"));
        Assert.Equal("MODIFIED", TRMCompatibilityAudit.Classify("einstein-structure"));

        // the optics classification is preserved, now on corrected grounds
        Assert.True(ConformalOpticsResolution.PsiZeroHasNoLensing());
        Assert.True(ConformalOpticsResolution.PsiNonZeroRestoresLensing());
        Assert.True(ConformalOpticsResolution.ShapiroFollowsGamma());
        Assert.True(ConformalOpticsResolution.ConformalIsRestrictedSector());
        Assert.Equal(4, ConformalOpticsResolution.OriginScore());
        Assert.Equal("OPTICS RESOLVED", ConformalOpticsResolution.Classify());

        // and the ansatz classification keeps PARTIAL UNIQUE, with the corrected reason
        Assert.Equal("PARTIAL UNIQUE", MetricAnsatzUniqueness.Classify());

        Output.WriteLine("TRMCompatibilityAudit   : metric-origin -> MODIFIED (was UNCHANGED)");
        Output.WriteLine("ConformalOpticsResolution: OPTICS RESOLVED — preserved, but now on corrected grounds:");
        Output.WriteLine("    the conformal slice is the UNIQUE counting-measure-preserving member, and ψ ≠ 0 (the optics");
        Output.WriteLine("    sector) BREAKS the measure. The former ground was 'ψ preserves the measure', which was false.");
        Output.WriteLine("MetricAnsatzUniqueness  : PARTIAL UNIQUE — restored with the corrected reason.");
    }

    // ── 7. report ────────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_025_Run()
    {
        PrintHeader("ResearchY-G_025 — Optics Determinant Correction Audit (verifies the AT-QG optics arithmetic)");

        var sb = new StringBuilder();
        sb.AppendLine("WHAT WAS VERIFIED. G_024 restored the AT-QG optics resolution on the strength of its own docs.");
        sb.AppendLine("G_025 verifies the underlying arithmetic INDEPENDENTLY — and finds a real defect.");
        sb.AppendLine("");
        sb.AppendLine("DEFECT 1 — OFF-BY-ONE IN THE DETERMINANT. MetricAnsatzAudit stated");
        sb.AppendLine("    det g = −ρ^(2/d)e^(2ψ)·(ρ^(2/d)e^(−2ψ/(d−1)})^(d−1) = −ρ², so √(−g) = ρ (unchanged)");
        sb.AppendLine("and PerturbedVolumeElement returned ρ BY CONSTRUCTION. The spatial block has d factors, not d−1.");
        sb.AppendLine("    det g       = −ρ^(2(d+1)/d)·e^(−2ψ/(d−1))");
        sb.AppendLine("    √(−det g)   = ρ^((d+1)/d)·e^(−ψ/(d−1))        4-volume   (2.1688481946 vs the claimed 2.0)");
        sb.AppendLine("    √(det g_ij) = ρ·e^(−dψ/(d−1))                spatial    (1.2752563032 vs the claimed 2.0)");
        sb.AppendLine("    at b = −3 the spatial element is 180.0342626 vs the claimed 2.0 — 8902 % off (UNBOUNDED in ψ).");
        sb.AppendLine("");
        sb.AppendLine("DEFECT 2 — HARD-CODED γ. GammaPsiZero() => ConformalGamma() (−1.0) and GammaPsiNonZero() =>");
        sb.AppendLine("GrGamma() (+1.0). No path computed γ from the metric, so origin-score items 2 and 3 were arithmetic");
        sb.AppendLine("on a constant — and the audit could not detect that the CODED metric ψ = b·x has γ ≠ +1 anywhere");
        sb.AppendLine("(+0.3352, +0.0022, −0.0930, −0.0694 for b = 0.3 at x = 0.1, 0.5, 1.0, 2.0).");
        sb.AppendLine("");
        sb.AppendLine("PROPAGATION FIXED (4 places):");
        sb.AppendLine("  1. MetricAnsatzUniqueness.PsiPerturbationPreservesMeasure  — returned true for EVERY ψ");
        sb.AppendLine("  2. ConformalOpticsResolution.ConformalIsRestrictedSector   — rested on (1)");
        sb.AppendLine("  3. TRMCompatibilityAudit.MetricOriginPreserved + matrix row — 'metric-origin' => UNCHANGED");
        sb.AppendLine("  4. TRMasUVCompletion.TrmCoreVolumeElement doc               — 'volume-preserving → core unchanged'");
        sb.AppendLine("  (HawkingTemperatureWithPsi cited the same false premise; its own ψ exponent d/(d−1) is correct.)");
        sb.AppendLine("");
        sb.AppendLine("WHAT SURVIVES — the physics is RESTORED on derived grounds:");
        sb.AppendLine("  * γ = −1 at ψ = 0 EXACTLY, and γ = +1 EXACTLY (to first order) at the DERIVED ψ = −4σ — both now");
        sb.AppendLine("    computed from the metric. The two-sector optics conclusion (QG212) stands.");
        sb.AppendLine("  * the measure-preservation premise is REFUTED, which STRENGTHENS the selectivity result:");
        sb.AppendLine("    ψ = 0 is the UNIQUE counting-measure-preserving member of the family.");
        sb.AppendLine("  * the TRM matrix moves one row: metric-origin UNCHANGED → MODIFIED (4 unchanged, 2 modified).");
        sb.AppendLine("");
        sb.AppendLine("THE BOUNDARY THIS EXPOSES:");
        sb.AppendLine("  * the exact (non-linearised) γ at ψ = −4σ is e^(6σ) = ρ² at d = 3 — +1 only in the weak field;");
        sb.AppendLine("  * the same ψ shifts the clock by e^(−4σ) = ρ^(−4/3) — invisible in the solar system, large at");
        sb.AppendLine("    compactness. So the O(x²) form of the completion is load-bearing and still unspecified.");
        sb.AppendLine("");
        sb.AppendLine("VERDICTS: DERIVED / BOUNDARY / REFUTED as recorded in ResearchY-G_025.md.");
        sb.AppendLine("The AT-QG optics conclusion is amply RESTORED; its measure-preservation premise and one origin-score");
        sb.AppendLine("basis are withdrawn; and γ is now computed rather than asserted.");

        Output.WriteLine(sb.ToString().TrimEnd());
        Assert.True(sb.Length > 0);
    }
}
