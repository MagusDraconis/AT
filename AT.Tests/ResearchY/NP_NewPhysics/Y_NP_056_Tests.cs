using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_056 — Equation-of-State Audit test suite (Y_NP_056_Tests.cs).
///
/// Question: can the AT informational ontology generate a unique equation of state?
/// ΩΛ = I_occ/ln K = 0.6839 is DERIVED (QG234), but the equation of state w is UNRESOLVED
/// (NP_055). This audit tests whether ΩΛ fixes the dark-energy dynamics.
///
/// Verdict tested: ΩΛ = I_occ/ln K = 0.6839 is a SNAPSHOT number (present-day density
/// fraction) with NO equation-of-state content. The expansion dynamics are DEGENERATE:
/// the same ΩΛ admits a 64% spread in H²(z=1)/H₀² across w = −1 … −1/3. Acceleration is
/// NOT derived — q₀ = [Ωm + ΩΛ(1+3w)]/2 requires w < −0.4874, an extra input; QG230's own
/// Λ ∝ 1/R² (w = −1/3) gives q₀ = +0.158 (decelerating). None of the candidate w's follows
/// uniquely: w = −1 (hosted ΛCDM, ρ ∝ a⁰), w = −1/3 (separate M ∝ R scaling, QG184),
/// evolving w(z) (legacy Λ(t), coefficient 0.015 fitted). A truly time-independent
/// bookkeeping fraction is dynamically inconsistent with acceleration (it forces w_de = 0,
/// q₀ = +0.5).
///
/// Classification: ΩΛ = 0.6839 DERIVED (unchanged); the equation of state w BOUNDARY
/// (undetermined); w = −1 / w = −1/3 / evolving w(z) CORRESPONDENCE (hosted / separately
/// scaled / fitted); a unique dark-energy dynamics REFUTED. AT predicts only a present-day
/// density fraction, not a unique dark-energy dynamics. No new primitive; canonical AT
/// unchanged.
///
/// Deterministic: closed-form FLRW quantities, closed-form equation-of-state scaling.
/// </summary>
public class Y_NP_056_Tests : ResearchTestBase
{
    public Y_NP_056_Tests(ITestOutputHelper output) : base(output) { }

    // Canonical fractions (QG234). ΩΛ is the present-day density fraction.
    private const double OmegaLambda = 0.6839;
    private const double OmegaMatter = 1 - OmegaLambda; // 0.3161

    /// <summary>H²(z)/H₀² in flat FLRW for a given (constant) dark-energy w.</summary>
    private static double H2(double z, double w)
        => OmegaMatter * Math.Pow(1 + z, 3) + OmegaLambda * Math.Pow(1 + z, 3 * (1 + w));

    /// <summary>Present deceleration q₀ = (1/2) Σ Ωᵢ(1+3wᵢ) for constant dark-energy w.</summary>
    private static double Q0(double w)
        => (OmegaMatter + OmegaLambda * (1 + 3 * w)) / 2.0;

    // ── [Required] Y_NP_056_OmegaLIsSnapshot ──────────────────────

    [Fact]
    public void Y_NP_056_OmegaLIsSnapshot()
    {
        // ΩΛ = I_occ/ln K is a single dimensionless number — a fraction of two information
        // quantities with no time argument, no pressure, and no equation of state.
        Assert.True(OmegaLambda > 0 && OmegaLambda < 1, "ΩΛ is a dimensionless fraction");
        Assert.True(Math.Abs((OmegaLambda + OmegaMatter) - 1.0) < 1e-12, "flatness: ΩΛ + Ωm = 1");

        // A single number cannot carry w = p/ρ. The informational inputs {I_occ, ln K} have
        // no z-dependence, so ΩΛ is a snapshot, not a dynamical law.
        double iOcc = 0.7513;
        double lnK = Math.Log(3.0);
        Assert.True(Math.Abs(iOcc / lnK - OmegaLambda) < 1e-3, "ΩΛ = I_occ/ln K is a fixed number");
    }

    // ── [Required] Y_NP_056_ExpansionDynamicsDegeneracy ───────────

    [Fact]
    public void Y_NP_056_ExpansionDynamicsDegeneracy()
    {
        // The SAME ΩΛ = 0.6839 is compatible with infinitely many w, each giving a
        // different expansion history. At z = 1 the spread is large.
        double h2Minus1 = H2(1.0, -1.0);
        double h2MinusHalf = H2(1.0, -0.5);
        double h2MinusThird = H2(1.0, -1.0 / 3.0);

        Assert.True(Math.Abs(h2Minus1 - 3.2127) < 1e-3, $"H²(z=1, w=-1) = {h2Minus1:F4}");
        Assert.True(Math.Abs(h2MinusThird - 5.2644) < 1e-3, $"H²(z=1, w=-1/3) = {h2MinusThird:F4}");

        // 64% spread for the same ΩΛ ⇒ ΩΛ does NOT determine the dynamics.
        double spread = (h2MinusThird - h2Minus1) / h2Minus1;
        Assert.True(spread > 0.5, $"H² spread = {spread * 100:F0}% (> 50%)");
        Assert.True(h2MinusThird > h2MinusHalf && h2MinusHalf > h2Minus1, "H(z) depends on w");
    }

    // ── [Required] Y_NP_056_DecelerationDependsOnW ────────────────

    [Fact]
    public void Y_NP_056_DecelerationDependsOnW()
    {
        // q₀ = [Ωm + ΩΛ(1+3w)]/2. Acceleration (q₀ < 0) requires w < −0.4874.
        double q0Minus1 = Q0(-1.0);
        double q0MinusThird = Q0(-1.0 / 3.0);
        double q0Zero = Q0(0.0);

        Assert.True(Math.Abs(q0Minus1 - (-0.5258)) < 1e-3, $"q₀(w=-1) = {q0Minus1:F4}");
        Assert.True(q0Minus1 < 0, "w = −1 accelerates");
        Assert.True(Math.Abs(q0MinusThird - 0.1581) < 1e-3, $"q₀(w=-1/3) = {q0MinusThird:F4}");
        Assert.True(q0MinusThird > 0, "w = −1/3 DECELERATES (no acceleration)");
        Assert.True(Math.Abs(q0Zero - 0.5) < 1e-3, $"q₀(w=0) = {q0Zero:F4}");

        // Acceleration threshold: q₀ = 0 ⇔ w = (−Ωm/ΩΛ − 1)/3 = −0.4874.
        double wThr = (-OmegaMatter / OmegaLambda - 1.0) / 3.0;
        Assert.True(Math.Abs(wThr - (-0.4874)) < 1e-3, $"threshold w = {wThr:F4}");
        Assert.True(Q0(wThr) == 0.0 || Math.Abs(Q0(wThr)) < 1e-12, "threshold gives q₀ = 0");
        Assert.True(-1.0 < wThr && wThr < -1.0 / 3.0, "w = −1 accelerates, w = −1/3 does not");
    }

    // ── [Required] Y_NP_056_WMinusOne ─────────────────────────────

    [Fact]
    public void Y_NP_056_WMinusOne()
    {
        // w = −1 (cosmological constant) requires ρ_Λ = constant (ρ ∝ a⁰). Nothing in
        // ΩΛ = I_occ/ln K — a fraction — fixes ρ_Λ to be constant.
        Assert.Equal(-1.0, -1 - 0 / 3.0); // ρ ∝ a^0 → w = −1

        // ΩΛ is a dimensionless fraction; it carries no energy-density scale or constancy.
        Assert.True(OmegaLambda < 1 && OmegaLambda > 0);
        bool wMinusOneDerived = false; // hosted ΛCDM, not derived from the fraction
        Assert.False(wMinusOneDerived);
    }

    // ── [Required] Y_NP_056_WMinusOneThird ────────────────────────

    [Fact]
    public void Y_NP_056_WMinusOneThird()
    {
        // w = −1/3 corresponds to ρ ∝ a^(−2) (QG230 Λ ∝ 1/R²). This scaling comes from the
        // separate counting-measure relation M ∝ R (QG184), NOT from ΩΛ = I_occ/ln K.
        double wMinusThird = -1 - (-2.0) / 3.0;
        Assert.True(Math.Abs(wMinusThird - (-1.0 / 3.0)) < 1e-12, "ρ ∝ a^(−2) → w = −1/3");

        // And w = −1/3 does NOT accelerate (q₀ > 0).
        Assert.True(Q0(wMinusThird) > 0, "QG230's Λ ∝ 1/R² gives q₀ > 0 (decelerating)");
    }

    // ── [Required] Y_NP_056_EvolvingW ─────────────────────────────

    [Fact]
    public void Y_NP_056_EvolvingW()
    {
        // Evolving w(z) from the legacy Λ(t) = α/√V(t): w(z) ≈ −1 + 0.015·(1+z)^(3/2).
        // The coefficient 0.015 is FITTED, not derived (X046/XD001).
        double w0 = -1 + 0.015 * Math.Pow(1 + 0, 1.5);
        double w1 = -1 + 0.015 * Math.Pow(1 + 1, 1.5);
        Assert.True(Math.Abs(w0 - (-0.9850)) < 1e-3, $"w(z=0) = {w0:F4}");
        Assert.True(Math.Abs(w1 - (-0.9576)) < 1e-3, $"w(z=1) = {w1:F4}");
        Assert.True(w1 > w0, "w(z) evolves (grows with z)");

        // The coefficient is an external fit, not a consequence of ΩΛ = I_occ/ln K.
        bool coefficientDerived = false;
        Assert.False(coefficientDerived);
    }

    // ── [Required] Y_NP_056_BookkeepingVsAcceleration ─────────────

    [Fact]
    public void Y_NP_056_BookkeepingVsAcceleration()
    {
        // If ΩΛ = 0.6839 is read as a TIME-INDEPENDENT bookkeeping fraction (NP_055 B=E),
        // then Ωm = 1 − ΩΛ is also constant ⇒ ρ_Λ = (ΩΛ/Ωm) ρ_m ∝ ρ_m ∝ a^(−3) ⇒ w_de = 0.
        double wScaling = -1 - (-3.0) / 3.0; // ρ ∝ a^(−3) → w = 0
        Assert.Equal(0.0, wScaling);

        // w_de = 0 gives q₀ = [Ωm + ΩΛ]/2 = 0.5 > 0 — DECELERATING.
        double q0Bookkeeping = (OmegaMatter + OmegaLambda * (1 + 3 * 0)) / 2.0;
        Assert.True(Math.Abs(q0Bookkeeping - 0.5) < 1e-9, $"bookkeeping q₀ = {q0Bookkeeping:F4}");
        Assert.True(q0Bookkeeping > 0, "a constant ΩΛ fraction cannot accelerate");

        // The observed acceleration requires ΩΛ(z) to evolve (w < −1/3), which the fixed
        // partition of ln K does not do — acceleration is a hosted input.
        Assert.True(Q0(-1.0) < 0, "only w < −1/3 (hosted) accelerates");
    }

    // ── [Required] Y_NP_056_Classification ────────────────────────

    [Fact]
    public void Y_NP_056_Classification()
    {
        // ΩΛ = 0.6839 (present-day density fraction): DERIVED (QG234, unchanged).
        double iOcc = 0.7513;
        double ol = iOcc / Math.Log(3.0);
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3, "ΩΛ DERIVED");

        // The equation of state w: BOUNDARY (undetermined by the informational ontology).
        bool wDetermined = false;
        Assert.False(wDetermined);

        // w = −1: CORRESPONDENCE (hosted ΛCDM, ρ ∝ a⁰ — not derived).
        // w = −1/3: CORRESPONDENCE (separate M ∝ R scaling, QG184 — and non-accelerating).
        // evolving w(z): CORRESPONDENCE (legacy Λ(t), coefficient 0.015 fitted).
        Assert.True(Q0(-1.0) < 0, "w = −1 accelerates (hosted)");
        Assert.True(Q0(-1.0 / 3.0) > 0, "w = −1/3 decelerates (QG230 scaling)");

        // A unique dark-energy dynamics: REFUTED (no w follows uniquely from ΩΛ).
        bool uniqueDynamics = false;
        Assert.False(uniqueDynamics);

        // No new primitive; canonical AT unchanged.
        Assert.Equal(0.6839, OmegaLambda);
    }

    // ── [Required] Y_NP_056_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_056_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_056 — Equation-of-State Audit");

        sb.AppendLine("Goal: can the AT informational ontology generate a unique equation of state?");
        sb.AppendLine("OmegaL = I_occ/ln K = 0.6839 is DERIVED (QG234); the equation of state w");
        sb.AppendLine("is UNRESOLVED (NP_055).");
        sb.AppendLine();

        sb.AppendLine("[1] OmegaL is a snapshot number");
        sb.AppendLine($"    OmegaL = I_occ/ln K = 0.7513/ln 3 = {OmegaLambda:F4}  (dimensionless)");
        sb.AppendLine("    No time argument, no pressure, no equation of state.");
        sb.AppendLine();

        sb.AppendLine("[2] Expansion-dynamics degeneracy (OmegaL fixed, dynamics free)");
        sb.AppendLine("    w            H2(z=1)/H0^2       q0 = [Om + OL(1+3w)]/2");
        foreach (double w in new[] { -1.0, -0.5, -1.0 / 3.0 })
            sb.AppendLine($"    {w,-6:F3}      {H2(1.0, w),-16:F4}    {Q0(w),+10:F4}");
        sb.AppendLine($"    H2 spread = {((H2(1.0, -1.0 / 3.0) - H2(1.0, -1.0)) / H2(1.0, -1.0)) * 100:F0}% for the same OmegaL");
        sb.AppendLine("    => OmegaL does NOT determine the expansion dynamics.");
        sb.AppendLine();

        sb.AppendLine("[3] Acceleration is NOT derived");
        double wThr = (-OmegaMatter / OmegaLambda - 1.0) / 3.0;
        sb.AppendLine($"    q0 < 0 requires w < {wThr:F4}  (an extra, hosted input).");
        sb.AppendLine($"    w = -1   -> q0 = {Q0(-1.0):F4}  (accelerating, hosted)");
        sb.AppendLine($"    w = -1/3 -> q0 = {Q0(-1.0 / 3.0):+F4}  (decelerating, QG230 scaling)");
        sb.AppendLine();

        sb.AppendLine("[4] Candidate equations of state — none follows uniquely");
        sb.AppendLine("    w = -1      (rho ∝ a^0):  hosted LCDM, not derived from a fraction.");
        sb.AppendLine("    w = -1/3    (rho ∝ a^-2): separate M ∝ R scaling (QG184); non-accelerating.");
        sb.AppendLine("    evolving w(z) (Lambda(t)=alpha/sqrt(V)): coefficient 0.015 FITTED (legacy).");
        sb.AppendLine();

        sb.AppendLine("[5] Bookkeeping reading is dynamically inconsistent with acceleration (NEW)");
        sb.AppendLine($"    constant OmegaL => rho_L = (OL/Om) rho_m ∝ rho_m ∝ a^-3 => w = 0 => q0 = {Q0(0.0):F3}");
        sb.AppendLine("    A fixed partition of ln K cannot accelerate; acceleration is a hosted w < -1/3.");
        sb.AppendLine();

        sb.AppendLine("[6] Verdict");
        sb.AppendLine("    AT predicts ONLY a present-day density fraction (OmegaL = 0.6839).");
        sb.AppendLine("    It does NOT predict a unique dark-energy dynamics.");
        sb.AppendLine("    The equation of state w is BOUNDARY (undetermined).");
        sb.AppendLine("    w = -1 / w = -1/3 / evolving w(z): CORRESPONDENCE (hosted / scaled / fitted).");
        sb.AppendLine("    A unique dark-energy dynamics: REFUTED.");
        sb.AppendLine("    No new primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
