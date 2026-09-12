using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_020 — Neutron-Star Redshift Audit (group G — Gravity Source).
///
/// QUESTION: can any CURRENTLY MEASURED neutron-star redshift exclude AT's exponential clock law?
/// Use NICER masses and radii, X-ray burst redshifts and published uncertainties.
/// Compare AT (g00 = -e^(2x)) against GR (g00 = -(1 + 2x)).  Compute the sigma separation.
/// Output: EXCLUDED / ALLOWED / PREFERRED.   CRITICAL: what precision on z and M/R is required for 5 sigma?
///
/// THE FRAMEWORK. With x = GM/(R c^2) > 0 the surface redshifts are
///     z_AT = e^x - 1 ,      z_GR = (1 - 2x)^(-1/2) - 1 ,      Dz(x) = z_GR - z_AT > 0 ,
/// and the significance of the separation against a measured z is
///     S = Dz / sqrt( sigma_z^2 + (dz/dx)^2 sigma_x^2 ) ,     dz/dx|_GR = (1 - 2x)^(-3/2) .
/// AT always predicts the SMALLER redshift. The midpoint (z_AT + z_GR)/2 decides which law is *closer* for a
/// given measurement.
///
/// THE DECISIVE NEW RESULT — THE ERROR BUDGET IS BINDING, NOT THE SIGNAL.
/// For every NICER object, the maximum significance achievable with a PERFECT redshift measurement
/// (sigma_z = 0) is:
///   J0030+0451 (Riley 2019)   M = 1.34 +- 0.16, R = 13.02 +- 1.24 km   x = 0.152011   max S = 0.614
///   J0030+0451 (Miller 2019)  M = 1.44 +- 0.15, R = 13.02 +- 1.06 km   x = 0.163355   max S = 0.752
///   J0740+6620 (Riley 2021)   M = 2.072 +- 0.067, R = 12.39 +- 1.30 km  x = 0.247002   max S = 1.334
///   J0740+6620 (Miller 2021)  M = 2.08 +- 0.07, R = 13.70 +- 2.60 km    x = 0.224246   max S = 0.778
///   generic (G_019 object)    M = 1.4 +- 0.05, R = 12.00 +- 1.00 km     x = 0.172317   max S = 1.221
/// NO object reaches 3 sigma, LET ALONE 5 sigma, AT ANY REDSHIFT PRECISION WHATSOEVER: the M/R term
/// (dz/dx) sigma_x already exceeds Dz/k for k = 3 and k = 5. So the binding constraint is the MASS-RADIUS
/// precision, not the redshift.
///
/// THE 5-SIGMA REQUIREMENT (joint: (Dz/5)^2 = sigma_z^2 + (dz/dx)^2 sigma_x^2)
///   best case (J0740+6620, Riley):  sigma_z <= 0.025125 (8.97 % of z_AT, 6.19 % of z_GR) with PERFECT M/R,
///                                   OR sigma_x/x <= 3.661 % with a perfect z,
///                                   OR the equal split sigma_z <= 0.017766 (6.34 % of z_AT) AND
///                                   sigma_x/x <= 2.589 %.
///   generic object:                 sigma_z <= 0.009441 (5.02 % of z_AT) OR sigma_x/x <= 2.907 %; equal split
///                                   sigma_z <= 0.006676 (3.55 %) AND sigma_x/x <= 2.055 %.
/// Measured against the current M/R precision (13.73 % for J0740+6620 Riley), 5 sigma needs a 3.75x (perfect z)
/// to 5.30x (equal split) improvement in sigma_x/x, plus a 3.2x to 7.9x improvement in sigma_z.
///
/// VERDICTS
///   ALLOWED    AT is inside the allowed region for every current measurement, and the reason is structural:
///              the M/R term dominates the error budget, so no z precision however good can reach even
///              3 sigma on today's mass-radius measurements.
///   EXCLUDED   NOT achieved by any current measurement. The single dangerous case is the often-quoted
///              z = 0.35 of EXO 0748-676 (Cottam 2002), which was NOT confirmed in later observations; taken at
///              face value at M = 1.4, R = 11 km it excludes BOTH laws (z_AT = 0.2068, z_GR = 0.2659, against
///              0.35), because z = 0.35 requires R = 6.890 km under AT and 9.164 km under GR.
///   PREFERRED  NOT SUPPORTED. The likelihood ratio LR(AT/GR) for a representative z = 0.30 +- 0.06 against
///              J0740+6620 (Riley) is 4.48 in favour of AT, but the scan over the plausible (R, z_obs) grid
///              FLIPS SIGN: LR = 1.718 at R = 10 km with z_obs = 0.25 (AT-favoured) down to 0.068 at R = 11 km
///              with z_obs = 0.35 (GR-favoured). Because z_AT < z_GR always and the observed redshifts are
///              mostly ABOVE z_AT for R >= 11 km, the data lean toward GR over most of the grid. The apparent
///              AT preference is an artefact of borrowing one object's redshift for another object's x.
///
/// Deterministic: exact algebra on imported published values.  No reclassification (G_019 unchanged); D_040
/// untouched; no canonical claim, value or equation changes; no new primitive.
/// </summary>
public class Y_G_020_Tests : ResearchTestBase
{
    public Y_G_020_Tests(ITestOutputHelper output) : base(output) { }

    private const double C2 = C * C;

    private static double GmOf(double mSun) => mSun * MSun * G_CODATA;
    private static double XOf(double mSun, double rKm) => GmOf(mSun) / (rKm * 1e3 * C2);

    private static double ZAt(double x) => Math.Exp(x) - 1.0;
    private static double ZGr(double x) => 1.0 / Math.Sqrt(1.0 - 2.0 * x) - 1.0;
    private static double DZ(double x) => ZGr(x) - ZAt(x);
    private static double SlopeGr(double x) => Math.Pow(1.0 - 2.0 * x, -1.5);
    private static double SlopeAt(double x) => Math.Exp(x);

    // ── 1. the discriminant ──────────────────────────────────────────────────────

    [Fact]
    public void Y_G_020_Discriminant()
    {
        // Both redshifts agree at x = 0 and DZ grows monotonically: AT always predicts the SMALLER redshift.
        Assert.True(Math.Abs(ZAt(0.0)) < 1e-15 && Math.Abs(ZGr(0.0)) < 1e-15);
        double prev = 0.0;
        foreach (double x in new[] { 0.05, 0.10, 0.152011, 0.172317, 0.224246, 0.247002, 0.30 })
        {
            Assert.True(ZAt(x) < ZGr(x), $"x = {x}");
            Assert.True(DZ(x) > prev, $"DZ not monotone at x = {x}");
            prev = DZ(x);
        }

        // The values used throughout.
        Assert.True(Math.Abs(ZAt(0.05) - 0.05127110) < 1e-8);
        Assert.True(Math.Abs(ZGr(0.05) - 0.05409255) < 1e-8);
        Assert.True(Math.Abs(DZ(0.05) - 0.00282146) < 1e-8);
        Assert.True(Math.Abs(DZ(0.247002) - 0.12562778) < 1e-8);
        Assert.True(Math.Abs(SlopeGr(0.247002) - 2.778302) < 1e-6);
        Assert.True(Math.Abs(SlopeAt(0.247002) - 1.280182) < 1e-6);
        Assert.True(SlopeGr(0.247002) > SlopeAt(0.247002));       // GR is steeper everywhere

        // The midpoint decides which law is closer for a given measurement.
        Assert.True(Math.Abs((ZAt(0.20) + ZGr(0.20)) / 2.0 - 0.256199) < 1e-6);
        Assert.True(Math.Abs((ZAt(0.25) + ZGr(0.25)) / 2.0 - 0.349119) < 1e-6);
    }

    // ── 2. the error budget is binding: no object reaches 3 sigma ────────────────

    [Fact]
    public void Y_G_020_NicerMaxSignificance()
    {
        // Imported NICER posteriors (central values and the larger uncertainty of the asymmetric pair).
        var objects = new (string Name, double M, double Dm, double R, double Dr,
                           double X, double Sx, double Slope, double Max)[]
        {
            ("J0030+0451 (Riley 2019)", 1.34, 0.16, 13.02, 1.24, 0.152011, 0.032627, 1.722128, 0.614),
            ("J0030+0451 (Miller 2019)", 1.44, 0.15, 13.02, 1.06, 0.163355, 0.030315, 1.810100, 0.752),
            ("J0740+6620 (Riley 2021)", 2.072, 0.067, 12.39, 1.30, 0.247002, 0.033909, 2.778302, 1.334),
            ("J0740+6620 (Miller 2021)", 2.08, 0.07, 13.70, 2.60, 0.224246, 0.050102, 2.441587, 0.778),
            ("generic (G_019 object)", 1.4, 0.05, 12.00, 1.00, 0.172317, 0.020514, 1.884838, 1.221),
        };

        foreach (var (name, m, dm, r, dr, xExp, sxExp, slopeExp, maxExp) in objects)
        {
            double x = XOf(m, r);
            Assert.True(Math.Abs(x - xExp) < 1e-5, $"{name}: x = {x}");
            double sx = x * (dm / m + dr / r);
            Assert.True(Math.Abs(sx - sxExp) / sxExp < 1e-3, $"{name}: sigma_x = {sx}");
            double slope = SlopeGr(x);
            Assert.True(Math.Abs(slope - slopeExp) / slopeExp < 1e-4, $"{name}: slope = {slope}");

            // MAX significance with a PERFECT z (sigma_z = 0).
            double maxS = DZ(x) / (slope * sx);
            Assert.True(Math.Abs(maxS - maxExp) < 1e-3, $"{name}: max S = {maxS}");
            Assert.True(maxS < 3.0, $"{name} must NOT reach 3 sigma with a perfect z");
            Assert.True(maxS < 1.4, $"{name}: max S = {maxS} is below 1.4 sigma");
        }

        // The strongest single object is J0740+6620 at Riley's radius: 1.334 sigma with a PERFECT z.
        double xBest = XOf(2.072, 12.39);
        double maxBest = DZ(xBest) / (SlopeGr(xBest) * xBest * (0.067 / 2.072 + 1.30 / 12.39));
        Assert.True(Math.Abs(maxBest - 1.334) < 1e-3, $"best max S = {maxBest}");

        // The structural reason: (dz/dx) sigma_x EXCEEDS Dz/3 and Dz/5 for every object, so no sigma_z
        // however small can reach 3 sigma or 5 sigma. The M/R precision is the binding constraint.
        foreach (var (name, m, dm, r, dr, _, _, _, _) in objects)
        {
            double x = XOf(m, r);
            double xTerm = SlopeGr(x) * x * (dm / m + dr / r);
            Assert.True(xTerm > DZ(x) / 3.0, $"{name}: 3 sigma unreachable");
            Assert.True(xTerm > DZ(x) / 5.0, $"{name}: 5 sigma unreachable");
            Assert.True(xTerm > DZ(x) / 2.0, $"{name}: even 2 sigma unreachable");
        }
    }

    // ── 3. the published redshift range: ALLOWED ─────────────────────────────────

    [Fact]
    public void Y_G_020_BurstRedshiftsAllowed()
    {
        // X-ray burst / atmosphere-fit neutron-star redshifts cluster at z ~ 0.2-0.35 with 20-50 % systematic
        // uncertainties. Take the representative value z = 0.30 and the pessimistic sigma_z = 0.15 (50 %).
        double zObs = 0.30, sz = 0.15;
        foreach (var (m, r) in new[] { (1.4, 10.0), (1.4, 11.0), (1.4, 12.0), (2.072, 12.39) })
        {
            double x = XOf(m, r);
            double zA = ZAt(x), zG = ZGr(x);
            // AT is inside the 1-sigma band of the measurement for every plausible (M, R).
            Assert.True(Math.Abs(zObs - zA) < 2.0 * sz, $"M = {m}, R = {r}: AT is {Math.Abs(zObs - zA) / sz:F2} sigma away");
            // GR is also still allowed at this precision -> the measurement does NOT discriminate.
            Assert.True(Math.Abs(zObs - zG) < 2.0 * sz, $"M = {m}, R = {r}: GR is {Math.Abs(zObs - zG) / sz:F2} sigma away");
        }

        // With the realistic (not pessimistic) 20 % systematic on z ~ 0.30, i.e. sigma_z = 0.06, the picture
        // sharpens but still leaves AT allowed for every object.
        double sz20 = 0.06;
        foreach (var (m, r) in new[] { (1.4, 10.0), (1.4, 11.0), (1.4, 12.0), (2.072, 12.39) })
        {
            double x = XOf(m, r);
            Assert.True(Math.Abs(zObs - ZAt(x)) / sz20 < 3.0, $"M = {m}, R = {r}: AT beyond 3 sigma");
        }
        // In fact AT is well inside 2.5 sigma everywhere, so AT is ALLOWED.
        Assert.True(Math.Abs(0.30 - ZAt(XOf(2.072, 12.39))) / sz20 < 0.4);
        Assert.True(Math.Abs(0.30 - ZAt(XOf(1.4, 12.0))) / sz20 < 2.0);
    }

    // ── 4. is AT PREFERRED? the likelihood ratio is not robust ───────────────────

    [Fact]
    public void Y_G_020_PreferenceNotSupported()
    {
        // For a Gaussian measurement with uncertainty sigma_z the log-likelihood difference is
        //   ln LR(AT/GR) = [ (z - z_GR)^2 - (z - z_AT)^2 ] / (2 sigma_z^2).
        double sz = 0.05;
        double LogLr(double zObs, double x, double s) =>
            (Math.Pow(zObs - ZGr(x), 2) - Math.Pow(zObs - ZAt(x), 2)) / (2.0 * s * s);

        // The single AT-favoured corner: the most compact NICER object at a low representative redshift.
        double xBest = XOf(2.072, 12.39);
        double lrBest = Math.Exp(LogLr(0.30, xBest, 0.06));
        Assert.True(Math.Abs(lrBest - 4.483) / 4.483 < 1e-2, $"LR(J0740, z = 0.30) = {lrBest}");
        Assert.True(lrBest > 1.0);

        // BUT the ratio FLIPS SIGN across the plausible grid: because z_AT < z_GR always and the observed
        // redshifts sit mostly ABOVE z_AT for R >= 11 km, the data lean toward GR over most of the grid.
        double lrAtFavoured = Math.Exp(LogLr(0.25, XOf(1.4, 10.0), sz));   // LR = 1.718  (AT favoured)
        double lrGrFavoured = Math.Exp(LogLr(0.35, XOf(1.4, 11.0), sz));   // LR = 0.068  (GR favoured)
        Assert.True(Math.Abs(lrAtFavoured - 1.718) / 1.718 < 1e-2, $"LR(0.25, R = 10) = {lrAtFavoured}");
        Assert.True(Math.Abs(lrGrFavoured - 0.068) / 0.068 < 1e-2, $"LR(0.35, R = 11) = {lrGrFavoured}");
        Assert.True(lrAtFavoured > 1.0 && lrGrFavoured < 1.0);

        // Count how many of the 9 grid points favour AT at sigma_z = 0.05.
        int atFavoured = 0, total = 0;
        foreach (double zObs in new[] { 0.25, 0.30, 0.35 })
            foreach (double r in new[] { 10.0, 11.0, 12.0 })
            {
                total++;
                if (Math.Exp(LogLr(zObs, XOf(1.4, r), sz)) > 1.0) atFavoured++;
            }
        Assert.Equal(9, total);
        Assert.Equal(1, atFavoured);                    // only ONE of nine favours AT
        Assert.True(atFavoured < total / 2);

        // The apparent preference is an artefact of borrowing one object's redshift for another object's x:
        // the redshift measurements are not made on the NICER objects at all.
        Assert.True(ZAt(xBest) < 0.30 && ZGr(xBest) > 0.30);
    }

    // ── 5. the critical question: 5 sigma ────────────────────────────────────────

    [Fact]
    public void Y_G_020_FiveSigmaRequirement()
    {
        // Joint requirement: (Dz/k)^2 = sigma_z^2 + (dz/dx)^2 sigma_x^2.
        var objects = new (string Name, double M, double Dm, double R, double Dr,
                           double SzPerf, double SxPctPerf, double SzEq, double SxPctEq)[]
        {
            ("J0030+0451 (Riley 2019)", 1.34, 0.16, 13.02, 1.24, 0.006901, 2.636, 0.004880, 1.864),
            ("J0030+0451 (Miller 2019)", 1.44, 0.15, 13.02, 1.06, 0.008250, 2.790, 0.005834, 1.973),
            ("J0740+6620 (Riley 2021)", 2.072, 0.067, 12.39, 1.30, 0.025125, 3.661, 0.017766, 2.589),
            ("J0740+6620 (Miller 2021)", 2.08, 0.07, 13.70, 2.60, 0.019035, 3.477, 0.013460, 2.458),
            ("generic (G_019 object)", 1.4, 0.05, 12.00, 1.00, 0.009441, 2.907, 0.006676, 2.055),
        };

        foreach (var (name, m, dm, r, dr, szPerf, sxPctPerf, szEq, sxPctEq) in objects)
        {
            double x = XOf(m, r), dz = DZ(x), slope = SlopeGr(x), need = dz / 5.0;

            // 5 sigma with a PERFECT redshift: the whole budget goes to M/R.
            double sxNeed = need / slope;
            Assert.True(Math.Abs(sxNeed / x * 100.0 - sxPctPerf) < 0.01, $"{name}: sigma_x/x = {sxNeed / x * 100.0}");
            // 5 sigma with a PERFECT M/R: the whole budget goes to the redshift.
            Assert.True(Math.Abs(need - szPerf) < 1e-6, $"{name}: sigma_z = {need}");
            // The equal split.
            double eq = need / Math.Sqrt(2.0);
            Assert.True(Math.Abs(eq - szEq) < 1e-6, $"{name}: equal-split sigma_z = {eq}");
            double sxEqNeed = eq / slope;
            Assert.True(Math.Abs(sxEqNeed / x * 100.0 - sxPctEq) < 0.01, $"{name}: equal-split sigma_x/x = {sxEqNeed / x * 100.0}");

            // The improvement factors required against today's M/R precision.
            double cur = x * (dm / m + dr / r);
            Assert.True(cur / sxNeed > 3.0, $"{name}: needs only {cur / sxNeed:F2}x");
            Assert.True(cur / sxEqNeed > 5.0, $"{name}: equal split needs only {cur / sxEqNeed:F2}x");
        }

        // THE ANSWER for the best object (J0740+6620, Riley 2021):
        double xB = XOf(2.072, 12.39), needB = DZ(xB) / 5.0;
        Assert.True(Math.Abs(needB - 0.025125) < 1e-6);
        Assert.True(Math.Abs(needB / ZAt(xB) * 100.0 - 8.97) < 0.02);        // 8.97 % of z_AT
        Assert.True(Math.Abs(needB / ZGr(xB) * 100.0 - 6.19) < 0.02);        // 6.19 % of z_GR
        Assert.True(Math.Abs(needB / SlopeGr(xB) / xB * 100.0 - 3.661) < 0.01);
        double eqB = needB / Math.Sqrt(2.0);
        Assert.True(Math.Abs(eqB / ZAt(xB) * 100.0 - 6.34) < 0.02);
        Assert.True(Math.Abs(eqB / SlopeGr(xB) / xB * 100.0 - 2.589) < 0.01);
        // Improvement factors: 3.75x (perfect z) and 5.30x (equal split) in sigma_x/x.
        double curB = xB * (0.067 / 2.072 + 1.30 / 12.39);
        Assert.True(Math.Abs(curB / (needB / SlopeGr(xB)) - 3.75) < 0.02, $"factor = {curB / (needB / SlopeGr(xB))}");
        Assert.True(Math.Abs(curB / (eqB / SlopeGr(xB)) - 5.30) < 0.02, $"factor = {curB / (eqB / SlopeGr(xB))}");
        // And the redshift improvement: from 20-50 % of z_AT down to 6.34 %.
        Assert.True(0.20 * ZAt(xB) / eqB > 3.0 && 0.20 * ZAt(xB) / eqB < 3.2);
        Assert.True(0.50 * ZAt(xB) / eqB > 7.8 && 0.50 * ZAt(xB) / eqB < 8.0);

        // 3 sigma for the same object, for the record.
        double need3 = DZ(xB) / 3.0;
        Assert.True(Math.Abs(need3 - 0.041876) < 1e-6);
        Assert.True(Math.Abs(need3 / ZAt(xB) * 100.0 - 14.95) < 0.02);
        Assert.True(Math.Abs(need3 / SlopeGr(xB) / xB * 100.0 - 6.102) < 0.01);
    }

    // ── 6. the dangerous case: z = 0.35 and the radius inversion ─────────────────

    [Fact]
    public void Y_G_020_RadiusInversionAndDangerousCase()
    {
        // For a measured z, AT demands a smaller radius than GR (M = 1.4 Msun).
        double gm = GmOf(1.4);
        var rows = new (double Z, double RAt, double RGr)[]
        {
            (0.20, 11.342, 13.535), (0.25, 9.267, 11.488),
            (0.30, 7.881, 10.129), (0.35, 6.890, 9.164),
        };
        foreach (var (z, rAt, rGr) in rows)
        {
            double xA = Math.Log(1.0 + z);
            double xG = (1.0 - Math.Pow(1.0 + z, -2.0)) / 2.0;
            Assert.True(Math.Abs(gm / (xA * C2) / 1e3 - rAt) < 0.01, $"z = {z}: R_AT = {gm / (xA * C2) / 1e3}");
            Assert.True(Math.Abs(gm / (xG * C2) / 1e3 - rGr) < 0.01, $"z = {z}: R_GR = {gm / (xG * C2) / 1e3}");
            Assert.True(rAt < rGr);
            Assert.True(Math.Abs((rAt - rGr) / rGr * 100.0 + 24.81) < 0.02 || z != 0.35);
        }
        Assert.True(Math.Abs((6.890 - 9.164) / 9.164 * 100.0 + 24.81) < 0.02);

        // THE SINGLE DANGEROUS CASE: the often-quoted z = 0.35 of EXO 0748-676 (Cottam et al. 2002), which was
        // NOT confirmed in later observations. Taken at face value at M = 1.4, R = 11 km it excludes BOTH laws.
        double x11 = XOf(1.4, 11.0);
        Assert.True(Math.Abs(x11 - 0.187982) < 1e-5, $"x = {x11}");
        Assert.True(Math.Abs(ZAt(x11) - 0.206812) < 1e-5);
        Assert.True(Math.Abs(ZGr(x11) - 0.265888) < 1e-5);
        Assert.True(0.35 - ZAt(x11) > 0.14);                 // 0.143188 above AT
        Assert.True(0.35 - ZGr(x11) > 0.08);                 // 0.084112 above GR
        // So z = 0.35 is inconsistent with BOTH laws at R = 11 km; it merely demands a smaller radius.
        Assert.True(ZAt(x11) < 0.35 && ZGr(x11) < 0.35);
        // AT-consistency would require 6.890 km, i.e. 4.110 km below 11 km.
        double rNeed = GmOf(1.4) / (Math.Log(1.35) * C2) / 1e3;
        Assert.True(Math.Abs(rNeed - 6.890) < 0.01);
        Assert.True(Math.Abs(11.0 - rNeed - 4.110) < 0.01);
        // For contrast: GR would need only 9.164 km — 1.837 km below 11 km.
        double rNeedGr = GmOf(1.4) / (((1.0 - Math.Pow(1.35, -2.0)) / 2.0) * C2) / 1e3;
        Assert.True(Math.Abs(rNeedGr - 9.164) < 0.01);
        Assert.True(rNeedGr - rNeed > 2.2);
    }

    // ── 7. research report ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_020_Run()
    {
        var sb = new StringBuilder();

        PrintHeader(sb, "ResearchY-G_020 — NEUTRON-STAR REDSHIFT AUDIT");
        sb.AppendLine("Question: can any CURRENTLY MEASURED neutron-star redshift exclude AT's exponential clock law?");
        sb.AppendLine("Sources: NICER masses and radii, X-ray burst redshifts, published uncertainties.");
        sb.AppendLine("Compare: AT (g00 = -e^(2x)) against GR (g00 = -(1 + 2x)); compute the sigma separation.");
        sb.AppendLine("Output: EXCLUDED / ALLOWED / PREFERRED.   Critical: the precision for 5 sigma.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS");
        sb.AppendLine("  A1  x = GM/(R c^2) > 0;  z_AT = e^x - 1  and  z_GR = (1 - 2x)^(-1/2) - 1;  Dz = z_GR - z_AT > 0.");
        sb.AppendLine("  A2  Significance  S = Dz / sqrt(sigma_z^2 + (dz/dx)^2 sigma_x^2),  dz/dx|_GR = (1 - 2x)^(-3/2).");
        sb.AppendLine("  A3  sigma_x/x = sigma_M/M + sigma_R/R (the conservative larger branch of each asymmetric error).");
        sb.AppendLine("  A4  Imported: NICER J0030+0451 and J0740+6620 posteriors; burst/atmosphere redshifts 0.2-0.35 with");
        sb.AppendLine("      20-50 % systematics.");
        sb.AppendLine();

        PrintHeader(sb, "1. THE DISCRIMINANT");
        sb.AppendLine("      x        z_AT        z_GR        Dz         midpoint    dz/dx (GR)");
        foreach (double x in new[] { 0.05, 0.10, 0.152011, 0.172317, 0.224246, 0.247002, 0.30 })
            sb.AppendLine($"   {x:F6}   {ZAt(x):.8f}  {ZGr(x):.8f}  {DZ(x):.8f}   {(ZAt(x) + ZGr(x)) / 2.0:.8f}   {SlopeGr(x):F6}");
        sb.AppendLine("  AT always predicts the SMALLER redshift; at x = 0 they coincide and Dz grows monotonically.");

        PrintHeader(sb, "2. MAX ACHIEVABLE SIGNIFICANCE WITH A *PERFECT* REDSHIFT");
        sb.AppendLine("  object                     M +- dM     R +- dR km      x        sigx/x    dz/dx   (dz/dx)sx  max S");
        foreach (var (name, m, dm, r, dr) in new (string, double, double, double, double)[]
                 { ("J0030+0451 (Riley 2019)", 1.34, 0.16, 13.02, 1.24),
                   ("J0030+0451 (Miller 2019)", 1.44, 0.15, 13.02, 1.06),
                   ("J0740+6620 (Riley 2021)", 2.072, 0.067, 12.39, 1.30),
                   ("J0740+6620 (Miller 2021)", 2.08, 0.07, 13.70, 2.60),
                   ("generic (G_019 object)", 1.4, 0.05, 12.00, 1.00) })
        {
            double x = XOf(m, r), sx = x * (dm / m + dr / r), sl = SlopeGr(x);
            sb.AppendLine($"  {name,-26} {m:F3}+-{dm,-6:F3} {r:F2}+-{dr,-5:F2}  {x:F6}  {sx / x * 100,6:F2}%  {sl:F4}  {sl * sx:F6}  {DZ(x) / (sl * sx):F3}");
        }
        sb.AppendLine("  NO object reaches 3 sigma, let alone 5 sigma, at ANY redshift precision: the M/R term already");
        sb.AppendLine("  exceeds Dz/3 and Dz/5. The binding constraint is the MASS-RADIUS precision, not the redshift.");

        PrintHeader(sb, "3. THE PUBLISHED REDSHIFT RANGE — ALLOWED");
        sb.AppendLine("  A representative burst/atmosphere redshift is z = 0.30 with a 20 % systematic (sigma_z = 0.06).");
        sb.AppendLine("   M      R km      z_AT       z_GR      |0.30 - z_AT|   |0.30 - z_GR|");
        foreach (var (m, r) in new[] { (1.4, 10.0), (1.4, 11.0), (1.4, 12.0), (2.072, 12.39) })
        {
            double x = XOf(m, r);
            sb.AppendLine($"   {m:F3}  {r,6:F2}   {ZAt(x):.6f}   {ZGr(x):.6f}      {Math.Abs(0.30 - ZAt(x)) / 0.06:F3} sigma      {Math.Abs(0.30 - ZGr(x)) / 0.06:F3} sigma");
        }
        sb.AppendLine("  AT is inside 2.5 sigma everywhere -> ALLOWED. (GR is also allowed, so the measurement does not");
        sb.AppendLine("  discriminate at this precision.)");

        PrintHeader(sb, "4. IS AT *PREFERRED*? — NOT SUPPORTED");
        sb.AppendLine("  ln LR(AT/GR) = [ (z - z_GR)^2 - (z - z_AT)^2 ] / (2 sigma_z^2),  sigma_z = 0.05:");
        sb.AppendLine("   z_obs   R km     x        z_AT     z_GR     |dz| AT     |dz| GR     LR(AT/GR)");
        int atFav = 0;
        foreach (double zObs in new[] { 0.25, 0.30, 0.35 })
            foreach (double r in new[] { 10.0, 11.0, 12.0 })
            {
                double x = XOf(1.4, r);
                double dA = Math.Abs(zObs - ZAt(x)), dG = Math.Abs(zObs - ZGr(x));
                double lr = Math.Exp((dG * dG - dA * dA) / (2.0 * 0.05 * 0.05));
                if (lr > 1.0) atFav++;
                sb.AppendLine($"   {zObs:F2}   {r,5:F1}  {x:F6}  {ZAt(x):.4f}  {ZGr(x):.4f}   {dA:F4}     {dG:F4}     {lr,8:F3}");
            }
        double xB = XOf(2.072, 12.39);
        double lrB = Math.Exp((Math.Pow(0.30 - ZGr(xB), 2) - Math.Pow(0.30 - ZAt(xB), 2)) / (2.0 * 0.06 * 0.06));
        sb.AppendLine($"  Against J0740+6620 (Riley) with z = 0.30 +- 0.06 the ratio is LR = {lrB:F3} — nominally in AT's favour.");
        sb.AppendLine($"  BUT only {atFav} of the 9 grid points favour AT, and the ratio FLIPS SIGN (1.718 at R = 10 km with");
        sb.AppendLine("  z_obs = 0.25, down to 0.068 at R = 11 km with z_obs = 0.35). Because z_AT < z_GR always and the");
        sb.AppendLine("  observed redshifts sit mostly ABOVE z_AT for R >= 11 km, the data lean toward GR over most of the");
        sb.AppendLine("  grid. The apparent preference is an artefact of borrowing one object's redshift for another's x.");

        PrintHeader(sb, "5. THE CRITICAL QUESTION — 5 SIGMA");
        sb.AppendLine("  Joint requirement: (Dz/5)^2 = sigma_z^2 + (dz/dx)^2 sigma_x^2.");
        sb.AppendLine("  object                     perfect z: sigx/x    perfect M/R: sigma_z        equal split: sigma_z   sigx/x");
        foreach (var (name, m, dm, r, dr) in new (string, double, double, double, double)[]
                 { ("J0030+0451 (Riley 2019)", 1.34, 0.16, 13.02, 1.24),
                   ("J0030+0451 (Miller 2019)", 1.44, 0.15, 13.02, 1.06),
                   ("J0740+6620 (Riley 2021)", 2.072, 0.067, 12.39, 1.30),
                   ("J0740+6620 (Miller 2021)", 2.08, 0.07, 13.70, 2.60),
                   ("generic (G_019 object)", 1.4, 0.05, 12.00, 1.00) })
        {
            double x = XOf(m, r), need = DZ(x) / 5.0, sl = SlopeGr(x), eq = need / Math.Sqrt(2.0);
            sb.AppendLine($"  {name,-26} {need / sl / x * 100,8:F3} %        {need:F6} ({need / ZAt(x) * 100:F2} % z_AT)   {eq:F6} ({eq / ZAt(x) * 100:F2} %)  {eq / sl / x * 100:F3} %");
        }
        double needB = DZ(xB) / 5.0, eqB = needB / Math.Sqrt(2.0);
        double curB = xB * (0.067 / 2.072 + 1.30 / 12.39);
        double need3 = DZ(xB) / 3.0;
        sb.AppendLine("  THE BEST CURRENT TARGET (J0740+6620, Riley 2021):");
        sb.AppendLine($"    5 sigma with a perfect z   :  sigma_x/x <= {needB / SlopeGr(xB) / xB * 100:F3} %   (today {curB / xB * 100:F2} % -> {curB / (needB / SlopeGr(xB)):F2}x better)");
        sb.AppendLine($"    5 sigma with a perfect M/R :  sigma_z   <= {needB:F6} = {needB / ZAt(xB) * 100:F2} % of z_AT ({needB / ZGr(xB) * 100:F2} % of z_GR)");
        sb.AppendLine($"    5 sigma, equal split       :  sigma_z   <= {eqB:F6} = {eqB / ZAt(xB) * 100:F2} % of z_AT  AND  sigma_x/x <= {eqB / SlopeGr(xB) / xB * 100:F3} %");
        sb.AppendLine($"      -> {curB / (eqB / SlopeGr(xB)):F2}x better M/R and a {0.20 * ZAt(xB) / eqB:F1}x to {0.50 * ZAt(xB) / eqB:F1}x better redshift");
        sb.AppendLine($"    (3 sigma, for the record: sigma_z <= {need3:F6} = {need3 / ZAt(xB) * 100:F2} % of z_AT, or sigma_x/x <= {need3 / SlopeGr(xB) / xB * 100:F3} %)");

        PrintHeader(sb, "6. THE DANGEROUS CASE AND THE RADIUS INVERSION");
        sb.AppendLine("  For a measured z, AT demands a SMALLER radius than GR (M = 1.4 Msun):");
        sb.AppendLine("     z      R_AT        R_GR       dR/R");
        foreach (double z in new[] { 0.20, 0.25, 0.30, 0.35 })
        {
            double xA = Math.Log(1.0 + z), xG = (1.0 - Math.Pow(1.0 + z, -2.0)) / 2.0;
            double rA = GmOf(1.4) / (xA * C2) / 1e3, rG = GmOf(1.4) / (xG * C2) / 1e3;
            sb.AppendLine($"   {z:F2}   {rA,7:F3} km  {rG,7:F3} km   {(rA - rG) / rG * 100,+7:F2} %");
        }
        double x11 = XOf(1.4, 11.0);
        sb.AppendLine($"  THE SINGLE DANGEROUS CASE: the often-quoted z = 0.35 of EXO 0748-676 (Cottam et al. 2002), NOT");
        sb.AppendLine($"  confirmed later. At M = 1.4, R = 11 km (x = {x11:F6}) the predictions are z_AT = {ZAt(x11):F6} and");
        sb.AppendLine($"  z_GR = {ZGr(x11):F6} — so z = 0.35 excludes BOTH laws; it merely demands a smaller radius.");
        sb.AppendLine($"  AT-consistency for z = 0.35 requires R = 6.890 km (4.110 km below 11 km); GR requires 9.164 km.");

        PrintHeader(sb, "7. CONCLUSIONS");
        sb.AppendLine("  C1  ALLOWED — no current neutron-star redshift excludes AT. AT is inside 2.5 sigma of a");
        sb.AppendLine("      representative z = 0.30 +- 0.06 for every plausible (M, R), and inside 1 sigma for the most");
        sb.AppendLine("      compact objects.");
        sb.AppendLine("  C2  EXCLUDED — not achieved, and NOT ACHIEVABLE with current data: with the present mass-radius");
        sb.AppendLine("      precision the maximum significance with a PERFECT redshift is 1.334 sigma (J0740+6620, Riley),");
        sb.AppendLine("      and the (dz/dx) sigma_x term exceeds Dz/2 for every object. No z precision whatsoever can reach");
        sb.AppendLine("      3 sigma, let alone 5 sigma, on today's M/R.");
        sb.AppendLine("  C3  PREFERRED — NOT SUPPORTED. The nominal AT-favoured ratio (4.483 against J0740+6620 with");
        sb.AppendLine($"      z = 0.30 +- 0.06) flips sign across the plausible grid (1.718 down to 0.068), and only {atFav} of 9");
        sb.AppendLine("      grid points favour AT; because z_AT < z_GR and the observed redshifts sit mostly above z_AT for");
        sb.AppendLine("      R >= 11 km, the data lean toward GR over most of the grid.");
        sb.AppendLine("  C4  CRITICAL — 5 SIGMA REQUIRES BOTH. For the best current object: sigma_x/x <= 3.661 % with a");
        sb.AppendLine($"      perfect z, or sigma_z <= 0.025125 = 8.97 % of z_AT with a perfect M/R, or the equal split");
        sb.AppendLine($"      sigma_x/x <= 2.589 % AND sigma_z <= 0.017766 = 6.34 % of z_AT. Against today's 13.73 % M/R that");
        sb.AppendLine("      is 3.75x to 5.30x better in sigma_x/x, plus 3.2x to 7.9x better in sigma_z — the two must be");
        sb.AppendLine("      won TOGETHER, because the error budget is joint.");
        sb.AppendLine("  C5  The structural lesson: the deficit is in the ERROR BUDGET, not in the signal. The signal is");
        sb.AppendLine("      0.1256 in z for the most compact object; it is sigma_x that keeps the test out of reach.");

        PrintHeader(sb, "8. CLASSIFICATION");
        sb.AppendLine("  ALLOWED    AT survives every current neutron-star redshift measurement.");
        sb.AppendLine("  EXCLUDED   NOT achieved — and unreachable at ANY z precision on current M/R (max 1.334 sigma).");
        sb.AppendLine("  PREFERRED  NOT supported — the likelihood ratio flips sign and mostly leans GR.");
        sb.AppendLine("  No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new");
        sb.AppendLine("  primitive; deterministic (exact algebra on imported published values).");

        Output.WriteLine(sb.ToString());
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
