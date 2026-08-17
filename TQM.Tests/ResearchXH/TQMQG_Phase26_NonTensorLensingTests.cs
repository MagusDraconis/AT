using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 26 — non-tensor explanation of lensing. Tests whether apparent lensing (image shift, magnification,
/// time delay) can emerge from the scalar machinery, using only observable quantities. Classify: MATCH / PARTIAL
/// MATCH / NO MATCH.
///
/// Tests: TQMQG260 (deflection & magnification), TQMQG261 (time delay & the redshift survivor), TQMQG262
///        (mechanism census → classification).
/// </summary>
public class TQMQG_Phase26_NonTensorLensingTests : ResearchTestBase
{
    public TQMQG_Phase26_NonTensorLensingTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG260: image shift & magnification ─────────────────────────────────────────

    [Fact]
    public void TQMQG260_ImageShiftAndMagnification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG260: image shift & magnification (γ = −1 vs GR γ = +1)");

        double gc = NonTensorLensing.ConformalGamma();
        double gr = NonTensorLensing.GrGamma();
        double gm = 1.0;   // GM/(b c²) in natural units for a grazing ray

        double dConf = NonTensorLensing.Deflection(gc, gm);
        double dGr = NonTensorLensing.Deflection(gr, gm);

        double kConf = NonTensorLensing.ConvergenceFactor(gc);
        double sConf = NonTensorLensing.ShearFactor(gc);
        double muConf = NonTensorLensing.Magnification(kConf, sConf);
        double kGr = NonTensorLensing.ConvergenceFactor(gr);
        double muGr = NonTensorLensing.Magnification(kGr, kGr);

        sb.AppendLine($"deflection  conformal γ=−1: {dConf:F6}   vs GR γ=+1: {dGr:F6}  (units 4GM/bc²)");
        sb.AppendLine($"convergence conformal κ  : {kConf:F6}   vs GR κ ∝ (1+γ)/2: {kGr:F6}");
        sb.AppendLine($"magnification conformal μ : {muConf:F6}   vs GR μ: {muGr:F6}");

        bool noShift = dConf == 0.0;
        bool noMag = Math.Abs(muConf - 1.0) < 1e-12;

        sb.AppendLine();
        sb.AppendLine($"image shift vanishes in conformal TQM: {noShift}");
        sb.AppendLine($"magnification is exactly 1 (no focusing): {noMag}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: actualization-density gradients and path-selection effects cannot deflect light — the");
        sb.AppendLine("conformal factor ρ^(2/d) multiplies g_00 and g_ii equally, so null geodesics are unchanged (δ = 0, μ = 1).");
        sb.AppendLine("IMAGE SHIFT: NO MATCH.  MAGNIFICATION: NO MATCH.");
        Output.WriteLine(sb.ToString());

        Assert.True(noShift, "conformal deflection should vanish");
        Assert.True(noMag, "conformal magnification should be 1");
    }

    // ── TQMQG261: time delay & the redshift survivor ─────────────────────────────────

    [Fact]
    public void TQMQG261_TimeDelayAndRedshiftSurvivor()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG261: Shapiro time delay (vanishes) vs gravitational redshift (survives)");

        double gc = NonTensorLensing.ConformalGamma();
        double gr = NonTensorLensing.GrGamma();
        double gmLog = 1.0;   // GM/c³ · ln(...) in natural units

        double tConf = NonTensorLensing.ShapiroDelay(gc, gmLog);
        double tGr = NonTensorLensing.ShapiroDelay(gr, gmLog);

        int d = 3;
        double rho1 = 1.0, rho2 = 1.0 + 2e-3;   // weak overdensity
        double z = NonTensorLensing.Redshift(d, rho1, rho2);

        sb.AppendLine($"Shapiro delay conformal γ=−1: {tConf:F6}   vs GR γ=+1: {tGr:F6}  (units 2GM/c³·ln)");
        sb.AppendLine($"gravitational redshift (d=3, ρ2/ρ1=1.002): z = {z:F6}");

        bool noDelay = tConf == 0.0;
        bool redshiftSurvives = z > 0.0;

        sb.AppendLine();
        sb.AppendLine($"arrival-time delay vanishes in conformal TQM: {noDelay}");
        sb.AppendLine($"gravitational redshift is nonzero (survives γ=−1): {redshiftSurvives}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the conformal factor cancels in the null condition (dt = dx), so light arrives with NO extra");
        sb.AppendLine("coordinate delay — time-delay statistics cannot produce a Shapiro delay. Only the g_00-governed redshift");
        sb.AppendLine("(a frequency shift, not an arrival-time shift) survives. TIME DELAY: NO MATCH (redshift is a separate MATCH).");
        Output.WriteLine(sb.ToString());

        Assert.True(noDelay, "conformal Shapiro delay should vanish");
        Assert.True(redshiftSurvives, "gravitational redshift should be nonzero");
    }

    // ── TQMQG262: mechanism census → classification ───────────────────────────────────

    [Fact]
    public void TQMQG262_MechanismCensus()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG262: can any scalar mechanism produce apparent lensing?");

        double gc = NonTensorLensing.ConformalGamma();
        double kConf = NonTensorLensing.ConvergenceFactor(gc);
        double sConf = NonTensorLensing.ShearFactor(gc);
        double mu = NonTensorLensing.Magnification(kConf, sConf);
        double d = NonTensorLensing.Deflection(gc, 1.0);
        double t = NonTensorLensing.ShapiroDelay(gc, 1.0);

        string[] mechanisms =
        {
            "actualization-density gradients",
            "time-delay statistics",
            "path-selection effects",
            "conformal optical depth",
            "horizon-counting geometry",
        };

        foreach (var m in mechanisms)
        {
            // every mechanism operates through the SAME conformal geometry (γ=−1):
            string shift = d == 0.0 ? "NO MATCH" : "?";
            string mag = mu == 1.0 ? "NO MATCH" : "?";
            string delay = t == 0.0 ? "NO MATCH" : "?";
            sb.AppendLine($"{m,-32} image shift: {shift,-9} magnification: {mag,-9} time delay: {delay}");
        }

        sb.AppendLine();
        sb.AppendLine("All five mechanisms reduce to the same conformal factor ρ^(2/d) (γ=−1), which cancels deflection,");
        sb.AppendLine("focusing, and delay. No non-tensor mechanism produces apparent lensing.");
        sb.AppendLine();
        sb.AppendLine("OVERALL CLASSIFICATION: NO MATCH — image shift, magnification, and time delay all vanish;");
        sb.AppendLine("only the gravitational redshift (a frequency effect) survives as a genuine TQM prediction.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(0.0, d);
        Assert.Equal(1.0, mu);
        Assert.Equal(0.0, t);
    }
}
