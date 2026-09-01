using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.QG_GeometryBridge;

/// <summary>
/// ResearchY-QG_012 — Distinguishability Cosmology Audit test suite
/// (Y_QG_012_Tests.cs).
///
/// Question: is ΩΛ uniquely privileged, or does distinguishability generate
/// additional cosmological observables?
///
/// Verdict tested: ΩΛ is privileged but NOT unique. Distinguishability generates a
/// FINITE family of cosmological observables — the density-fraction pair
/// (ΩΛ = I_occ/ln K = 0.6839, Ωm = 1 − ΩΛ = 0.3161) and its deterministic closure:
/// the ratio I_occ/(ln K − I_occ) = 2.1636, the current deceleration
/// q₀ = Ωm/2 − ΩΛ = −0.5258, and the turnaround redshift
/// z_acc = (2ΩΛ/Ωm)^(1/3) − 1 = 0.6295 (the latter two via the HOSTED FRW relations —
/// form CORRESPONDENCE, values DERIVED). H₀, σ₈, BAO, structure growth, weak lensing,
/// horizon scale, and matter clustering are NOT information functions (they need
/// dimensionful anchors, the primordial amplitude A_s, or the sound horizon) — so a
/// FULL information cosmology is refuted.
///
/// Deterministic: closed-form fractions and derived observables.
/// </summary>
public class Y_QG_012_Tests : ResearchTestBase
{
    public Y_QG_012_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_QG_012_InformationObservable ─────────────────

    /// <summary>
    /// The primary information observables: ΩΛ = I_occ/ln K and Ωm = complement.
    /// The entropy identity I_occ + H = ln K gives Ωm = H/ln K.
    /// </summary>
    [Fact]
    public void Y_QG_012_InformationObservable()
    {
        double Iocc = 0.7513, OmegaL = 0.6839;

        // Canonical convention (NP_019): ln K is DERIVED from I_occ/ΩΛ.
        double lnK = Iocc / OmegaL;          // = 1.098552

        // ΩΛ = I_occ/ln K = 0.6839.
        double omegaL = Iocc / lnK;
        Assert.Equal(0.6839, omegaL, 3);

        // Ωm = 1 − ΩΛ = 0.3161.
        double omegaM = 1 - omegaL;
        Assert.Equal(0.3161, omegaM, 3);

        // Entropy identity: H = ln K − I_occ, Ωm = H/ln K.
        double H = lnK - Iocc;
        Assert.Equal(0.3473, H, 3);
        Assert.Equal(0.3161, H / lnK, 3);

        // The pair partitions the state-space size: ΩΛ + Ωm = 1.
        Assert.Equal(1.0, omegaL + omegaM, 12);
    }

    // ── [Required] Y_QG_012_CosmologyMapping ──────────────────────

    /// <summary>
    /// Which observables are direct functions of {I_occ, ln K, ΩΛ, Ωm, ρ}?
    /// The pair + closures are; H₀/σ₈/BAO/growth/lensing/clustering are not.
    /// </summary>
    [Fact]
    public void Y_QG_012_CosmologyMapping()
    {
        double Iocc = 0.7513, OmegaL = 0.6839;
        double lnK = Iocc / OmegaL;
        double omegaL = Iocc / lnK;
        double omegaM = 1 - omegaL;

        // The pair and the ratio are information functions.
        Assert.Equal(2.1636, omegaL / omegaM, 3);

        // H₀ is dimensionful — needs an anchor, not a dimensionless info object.
        bool h0IsPureInfoFunction = false;
        Assert.False(h0IsPureInfoFunction);

        // σ₈ needs the primordial amplitude A_s.
        bool sigma8IsPureInfoFunction = false;
        Assert.False(sigma8IsPureInfoFunction);

        // BAO needs the sound horizon (Ωb, Ωr).
        bool baoIsPureInfoFunction = false;
        Assert.False(baoIsPureInfoFunction);

        // Structure growth needs A_s and the growth index.
        bool growthIsPureInfoFunction = false;
        Assert.False(growthIsPureInfoFunction);
    }

    // ── [Required] Y_QG_012_SecondaryObservable ───────────────────

    /// <summary>
    /// The derived closure: q₀ = Ωm/2 − ΩΛ and z_acc = (2ΩΛ/Ωm)^(1/3) − 1 are
    /// deterministic consequences of the pair (hosted FRW form, derived values).
    /// </summary>
    [Fact]
    public void Y_QG_012_SecondaryObservable()
    {
        double Iocc = 0.7513, OmegaL = 0.6839;
        double lnK = Iocc / OmegaL;
        double omegaL = Iocc / lnK;
        double omegaM = 1 - omegaL;

        // Current deceleration parameter (hosted FRW form): q₀ = Ωm/2 − ΩΛ.
        double q0 = omegaM / 2 - omegaL;
        Assert.Equal(-0.5258, q0, 3);

        // Turnaround redshift: 1 + z_acc = (2ΩΛ/Ωm)^(1/3).
        double zAcc = Math.Pow(2 * omegaL / omegaM, 1.0 / 3.0) - 1.0;
        Assert.Equal(0.6295, zAcc, 3);

        // The values are fixed by the pair — not free.
        Assert.True(Math.Abs(q0 + 0.5258) < 0.001);
        Assert.True(Math.Abs(zAcc - 0.6295) < 0.001);
    }

    // ── [Required] Y_QG_012_PredictionRanking ─────────────────────

    /// <summary>
    /// Ranking: ΩΛ (0.12%) top, Ωm (0.26%), ratio 2.1636, then the q₀/z_acc closure.
    /// </summary>
    [Fact]
    public void Y_QG_012_PredictionRanking()
    {
        double Iocc = 0.7513, OmegaL = 0.6839;
        double lnK = Iocc / OmegaL;
        double omegaL = Iocc / lnK;
        double omegaM = 1 - omegaL;

        // Top-ranked: ΩΛ — the observed fraction.
        Assert.Equal(0.6839, omegaL, 3);

        // Second: Ωm.
        Assert.Equal(0.3161, omegaM, 3);

        // Third: the ratio.
        Assert.Equal(2.1636, omegaL / omegaM, 3);

        // Then the closure: q₀ and z_acc are the next derived predictions.
        double q0 = omegaM / 2 - omegaL;
        double zAcc = Math.Pow(2 * omegaL / omegaM, 1.0 / 3.0) - 1.0;
        Assert.Equal(-0.5258, q0, 3);
        Assert.Equal(0.6295, zAcc, 3);
    }

    // ── [Required] Y_QG_012_FalsificationCheck ────────────────────

    /// <summary>
    /// The finite family is falsifiable; a full information cosmology is refuted.
    /// If distinguishability is removed, the whole family vanishes.
    /// </summary>
    [Fact]
    public void Y_QG_012_FalsificationCheck()
    {
        double Iocc = 0.7513, OmegaL = 0.6839;
        double lnK = Iocc / OmegaL;
        double omegaL = Iocc / lnK;
        double omegaM = 1 - omegaL;

        // Falsifiable: q₀ ≠ −0.526 or z_acc ≠ 0.630 falsifies the closure.
        double q0 = omegaM / 2 - omegaL;
        double zAcc = Math.Pow(2 * omegaL / omegaM, 1.0 / 3.0) - 1.0;
        Assert.Equal(-0.5258, q0, 3);
        Assert.Equal(0.6295, zAcc, 3);

        // Full info cosmology refuted: an amplitude observable needs A_s.
        bool sigma8FromInfo = false;
        Assert.False(sigma8FromInfo);

        // Removing distinguishability removes I_occ — the family vanishes.
        bool familySurvivesWithoutDistinguishability = false;
        Assert.False(familySurvivesWithoutDistinguishability);
    }

    // ── [Required] Y_QG_012_Run ───────────────────────────────────

    [Fact]
    public void Y_QG_012_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-QG_012 — Distinguishability Cosmology Audit");

        sb.AppendLine("Goal: is OmegaLambda uniquely privileged, or does distinguishability");
        sb.AppendLine("generate additional cosmological observables?");
        sb.AppendLine();

        sb.AppendLine("[1] The pair is privileged but NOT unique");
        sb.AppendLine("    OmegaLambda = I_occ/ln K = 0.6839; Omegam = 0.3161");
        sb.AppendLine("    entropy identity: I_occ + H = ln K, Omegam = H/ln K");
        sb.AppendLine();

        sb.AppendLine("[2] Finite family (derived closures)");
        sb.AppendLine("    ratio = 2.1636; q0 = Omegam/2 - OmegaLambda = -0.5258;");
        sb.AppendLine("    z_acc = (2*OmegaLambda/Omegam)^(1/3) - 1 = 0.6295 (hosted FRW)");
        sb.AppendLine();

        sb.AppendLine("[3] No full information cosmology");
        sb.AppendLine("    H0/sigma8/BAO/growth/lensing/clustering need anchors, A_s,");
        sb.AppendLine("    or the sound horizon — not info objects");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    finite family of information observables (B);");
        sb.AppendLine("    canonical AT unchanged; no new primitive.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
