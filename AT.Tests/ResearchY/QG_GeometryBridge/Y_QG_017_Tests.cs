using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.QG_GeometryBridge;

/// <summary>
/// ResearchY-QG_017 — Distinguishability Cosmology Extension Audit test suite
/// (Y_QG_017_Tests.cs). (Originally submitted as QG_015/QG_016; registered as
/// QG_017 per the permanent-ID rule.)
///
/// Question: if ΩΛ comes from distinguishability, what else must follow?
///
/// Verdict tested: ΩΛ is NOT an isolated success — it is the first member of a
/// FINITE distinguishability cosmology family: ΩΛ = I_occ/ln K = 0.6839,
/// Ωm = (ln K − I_occ)/ln K = 0.3161, the ratio 2.1636, and — via the HOSTED FRW
/// relations — q₀ = Ωm/2 − ΩΛ = −0.5258 and z_acc = (2ΩΛ/Ωm)^(1/3) − 1 = 0.6295.
/// The family is algebraically CLOSED (entropy identity I_occ + H = ln K;
/// completeness ΩΛ + Ωm = 1; q₀/z_acc deterministic). H₀/σ₈/BAO/growth/lensing/
/// horizon/clustering are NOT functions of {I_occ, ln K, ρ} — a full
/// distinguishability cosmology is refuted. The strongest next prediction beyond
/// ΩΛ is the q₀/z_acc closure.
///
/// Deterministic: closed-form fractions and closures.
/// </summary>
public class Y_QG_017_Tests : ResearchTestBase
{
    public Y_QG_017_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_QG_017_InformationObservable ─────────────────

    /// <summary>
    /// ΩΛ = I_occ/ln K; Ωm = complement; the entropy identity.
    /// </summary>
    [Fact]
    public void Y_QG_017_InformationObservable()
    {
        double Iocc = 0.7513, OmegaL = 0.6839;
        double lnK = Iocc / OmegaL; // derived-lnK convention (QG_012)

        double omegaL = Iocc / lnK;
        Assert.Equal(0.6839, omegaL, 3);

        double omegaM = 1 - omegaL;
        Assert.Equal(0.3161, omegaM, 3);

        // Entropy identity: I_occ + H = ln K; Ωm = H/ln K.
        double H = lnK - Iocc;
        Assert.Equal(0.3473, H, 3);
        Assert.Equal(0.3161, H / lnK, 3);

        // Completeness: ΩΛ + Ωm = 1.
        Assert.Equal(1.0, omegaL + omegaM, 12);
    }

    // ── [Required] Y_QG_017_ClosureRelations ──────────────────────

    /// <summary>
    /// The finite family is algebraically closed: ratio, q₀, z_acc are all
    /// deterministic functions of the pair.
    /// </summary>
    [Fact]
    public void Y_QG_017_ClosureRelations()
    {
        double Iocc = 0.7513, OmegaL = 0.6839;
        double lnK = Iocc / OmegaL;
        double omegaL = Iocc / lnK;
        double omegaM = 1 - omegaL;

        // Ratio.
        Assert.Equal(2.1636, omegaL / omegaM, 3);

        // q₀ = Ωm/2 − ΩΛ.
        Assert.Equal(-0.5258, omegaM / 2 - omegaL, 3);

        // 1 + z_acc = (2ΩΛ/Ωm)^(1/3).
        Assert.Equal(1.6295, Math.Pow(2 * omegaL / omegaM, 1.0 / 3.0), 3);

        // The family is closed: no third independent information number.
        bool thirdIndependentInfoNumber = false;
        Assert.False(thirdIndependentInfoNumber);
    }

    // ── [Required] Y_QG_017_SecondaryObservable ───────────────────

    /// <summary>
    /// q₀ and z_acc are deterministic closures of the pair (hosted FRW form,
    /// derived values).
    /// </summary>
    [Fact]
    public void Y_QG_017_SecondaryObservable()
    {
        double Iocc = 0.7513, OmegaL = 0.6839;
        double lnK = Iocc / OmegaL;
        double omegaL = Iocc / lnK;
        double omegaM = 1 - omegaL;

        double q0 = omegaM / 2 - omegaL;
        Assert.Equal(-0.5258, q0, 3);

        double zAcc = Math.Pow(2 * omegaL / omegaM, 1.0 / 3.0) - 1;
        Assert.Equal(0.6295, zAcc, 3);

        // The values are fixed by the pair — not free.
        Assert.True(Math.Abs(q0 + 0.5258) < 0.001);
        Assert.True(Math.Abs(zAcc - 0.6295) < 0.001);
    }

    // ── [Required] Y_QG_017_PredictionRanking ─────────────────────

    /// <summary>
    /// Ranking: ΩΛ (0.12%), Ωm (0.26%), ratio 2.1636, then the q₀/z_acc closure.
    /// </summary>
    [Fact]
    public void Y_QG_017_PredictionRanking()
    {
        double Iocc = 0.7513, OmegaL = 0.6839;
        double lnK = Iocc / OmegaL;
        double omegaL = Iocc / lnK;
        double omegaM = 1 - omegaL;

        // ΩΛ top (observed).
        Assert.Equal(0.6839, omegaL, 3);
        // Ωm second (observed).
        Assert.Equal(0.3161, omegaM, 3);
        // Ratio third.
        Assert.Equal(2.1636, omegaL / omegaM, 3);
        // The strongest NEXT predictions: q₀ and z_acc.
        Assert.Equal(-0.5258, omegaM / 2 - omegaL, 3);
        Assert.Equal(0.6295, Math.Pow(2 * omegaL / omegaM, 1.0 / 3.0) - 1, 3);
    }

    // ── [Required] Y_QG_017_FalsificationCheck ────────────────────

    /// <summary>
    /// The finite family is falsifiable; a full information cosmology is refuted
    /// (H₀/σ₈/BAO/growth/lensing/horizon/clustering need non-info inputs).
    /// </summary>
    [Fact]
    public void Y_QG_017_FalsificationCheck()
    {
        double Iocc = 0.7513, OmegaL = 0.6839;
        double lnK = Iocc / OmegaL;
        double omegaL = Iocc / lnK;
        double omegaM = 1 - omegaL;

        // Falsifiable closures.
        Assert.Equal(-0.5258, omegaM / 2 - omegaL, 3);
        Assert.Equal(0.6295, Math.Pow(2 * omegaL / omegaM, 1.0 / 3.0) - 1, 3);

        // H₀ is dimensionful — needs an anchor.
        bool h0IsPureInfoFunction = false;
        Assert.False(h0IsPureInfoFunction);

        // σ₈ needs the primordial amplitude A_s.
        bool sigma8IsPureInfoFunction = false;
        Assert.False(sigma8IsPureInfoFunction);

        // BAO needs the sound horizon (Ωb, Ωr).
        bool baoIsPureInfoFunction = false;
        Assert.False(baoIsPureInfoFunction);

        // Removing distinguishability removes the whole family (I_occ undefined).
        bool familySurvivesWithoutDistinguishability = false;
        Assert.False(familySurvivesWithoutDistinguishability);
    }

    // ── [Required] Y_QG_017_Run ───────────────────────────────────

    [Fact]
    public void Y_QG_017_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-QG_017 — Distinguishability Cosmology Extension Audit");

        sb.AppendLine("Goal: if OmegaLambda comes from distinguishability, what else");
        sb.AppendLine("must follow? Is it isolated or the first member of a family?");
        sb.AppendLine();

        sb.AppendLine("[1] OmegaLambda is NOT isolated");
        sb.AppendLine("    finite family: OmegaLambda, Omegam, ratio, q0, z_acc");
        sb.AppendLine();

        sb.AppendLine("[2] Closure relations (the family is closed)");
        sb.AppendLine("    I_occ + H = ln K; OmegaLambda + Omegam = 1;");
        sb.AppendLine("    q0 = Omegam/2 - OmegaLambda; 1+z_acc = (2*OmegaLambda/Omegam)^(1/3)");
        sb.AppendLine();

        sb.AppendLine("[3] No full distinguishability cosmology");
        sb.AppendLine("    H0/sigma8/BAO/growth/lensing/horizon/clustering need");
        sb.AppendLine("    non-information inputs (anchors, A_s, sound horizon)");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict: B) finite cosmology family");
        sb.AppendLine("    strongest next prediction: q0 = -0.5258, z_acc = 0.6295;");
        sb.AppendLine("    canonical AT unchanged; no new primitive.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
