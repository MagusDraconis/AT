using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.TemporalPredictionAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_068 - Temporal Prediction Audit (group G - Gravity Source).
///
/// QUESTION. What unique measurable prediction does the surviving AT time sector make that differs from GR? Output the
/// observable, its magnitude and the current measurement status. Goal: return from rho-structure to experimental time
/// physics.
///
/// ANSWER: **DERIVED - the prediction is exact, unique and unmeasured: AT gives 1 + z = e^(-x) where GR gives
/// 1 + z = (1 + 2x)^(-1/2), so the split is SECOND ORDER in the potential and the live arena is a compact object's
/// surface.**
/// </summary>
public class Y_G_068_Tests : ResearchTestBase
{
    public Y_G_068_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_068_ThePredictionIsTwoExactFunctions()
    {
        // both are exact and fitted by nothing: AT from g00 = -e^(2x), GR from g00 = -(1 + 2x)
        foreach (double x in new[] { -1e-3, -1e-1, -0.152011, -0.247002 })
        {
            Assert.True(ZAt(x) > 0.0);
            Assert.True(ZGr(x) > 0.0);
            Assert.True(ZAt(x) < ZGr(x));
        }

        // at x = -0.15 the audit's own numbers: z_AT = 0.1618342427 against G_019's z_GR = 0.1952286093
        Assert.Equal(0.1618342427, ZAt(-0.15), 9);
        Assert.Equal(0.1952286093, ZGr(-0.15), 9);

        Assert.Contains("e^(-x)", ThePrediction());
        Assert.Contains("(1 + 2x)^(-1/2)", ThePrediction());
        Assert.True(TheSplitIsSecondOrder());
    }

    [Fact]
    public void Y_G_068_TheNaiveRouteFailsWhereTheAuditUsesTheSeries()
    {
        // the split from the series against its leading signature: they agree to 2 parts in 10^6 at x = -1E-6
        // compared RELATIVELY: the next term is (7/3)x^3, so the deviation is (7/3)|x| = 2.333E-6 at x = -1E-6
        Assert.InRange(Math.Abs(SplitFromSeries(-1e-6) / LeadingSignature(-1e-6) - 1.0), 2.3e-6, 2.4e-6);
        Assert.InRange(Math.Abs(LeadingSignature(-1e-6) / -1e-12 - 1.0), 0.0, 1e-12);

        // and the cost of the naive route, MEASURED: at x = -1E-9 the subtraction inflates the split by 82.2x
        Assert.True(TheNaiveRouteInflatesTheWeakFieldSplit());
        Assert.InRange(NaiveSplitInflation(-1e-9), 80.0, 85.0);
        Assert.InRange(SplitNaive(-1e-9), -8.3e-17, -8.1e-17);
        // the SERIES has its own rounding at that depth: representing 1E-9 costs 1E-25 absolutely, so a split of
        // 1E-18 carries a relative error of about 6E-8 - six digits better than the naive route's factor of 82, and
        // reported as a measurement rather than rounded away
        Assert.InRange(Math.Abs(SplitFromSeries(-1e-9) / LeadingSignature(-1e-9) - 1.0), 5e-8, 7e-8);

        // below x ~ 1E-8 the split is not small but UNREPRESENTABLE
        Assert.True(TheWeakFieldSplitIsUnrepresentable());
        Assert.InRange(UlpOfUnity, 2.2e-16, 2.3e-16);
        Assert.True(Math.Abs(LeadingSignature(-1e-9)) < UlpOfUnity);
    }

    [Fact]
    public void Y_G_068_TheConstantsReproduceTheEarlierAudits()
    {
        // the Sun's potential recomputed against G_019's recorded value: agreement to 2.6E-4 relative
        Assert.InRange(SolarConstantAgreement(), 0.0, 1e-3);
        Assert.InRange(XSolar(), -2.13e-6, -2.12e-6);
        Assert.InRange(XEarth(), -6.97e-10, -6.95e-10);

        // and the compact table reproduces G_019's redshifts to the printed digits
        var table = CompactTable();
        var j0030 = table.Single(t => t.Object.StartsWith("J0030"));
        Assert.Equal(-0.152011, j0030.X, 6);
        Assert.InRange(Math.Abs(j0030.ZAt / 0.1641732 - 1.0), 0.0, 1e-5);
        Assert.InRange(Math.Abs(j0030.ZGr / 0.1986775 - 1.0), 0.0, 1e-5);
        var j0740 = table.Single(t => t.Object.StartsWith("J0740+6620 (Riley"));
        Assert.InRange(Math.Abs(j0740.ZAt / 0.2801814 - 1.0), 0.0, 1e-5);
        Assert.InRange(Math.Abs(j0740.ZGr / 0.4058088 - 1.0), 0.0, 2e-5);
    }

    [Fact]
    public void Y_G_068_NoWeakFieldMeasurementCanEverSeeIt()
    {
        var weak = WeakFieldTable();
        Assert.Equal(3, weak.Length);
        Assert.All(weak, t => Assert.True(t.ShortBy > 1.0));

        // the ordering of the shortfalls is the prediction's shape: terrestrial 2.1x, solar 2.2E6x, white dwarf 3.0E5x
        Assert.InRange(weak[0].ShortBy, 2.0, 2.2);
        Assert.InRange(weak[1].ShortBy, 2.0e6, 2.4e6);
        Assert.InRange(weak[2].ShortBy, 2.8e5, 3.2e5);

        // the Earth's surface split against the clock floor, as G_019 recorded it
        Assert.InRange(Math.Abs(LeadingSignature(XEarth()) / -4.845934e-19 - 1.0), 0.0, 1.5e-3);
    }

    [Fact]
    public void Y_G_068_TheLiveArenaIsTheSurfaceOfACompactObject()
    {
        var compact = CompactTable();
        Assert.Equal(4, compact.Length);
        Assert.True(AtRedshiftIsAlwaysSmaller());

        // the relative splits, to the printed digits
        Assert.Equal(-0.17367, compact.Single(t => t.Object.StartsWith("J0030")).RelativeSplit, 4);
        Assert.Equal(-0.30957, compact.Single(t => t.Object.StartsWith("J0740+6620 (Riley")).RelativeSplit, 4);
        Assert.Equal(-0.27464, compact.Single(t => t.Object.StartsWith("J0740+6620 (Miller")).RelativeSplit, 4);

        // all of them are large: the split is a third of the signal at the most compact object
        Assert.All(compact, t => Assert.True(Math.Abs(t.RelativeSplit) > 0.15));
    }

    [Fact]
    public void Y_G_068_TheMeasurementStatusIsAllowedButUndecided()
    {
        var t = DecidingTest();
        Assert.Equal(0.047205, t.Separation, 6);
        Assert.Equal(0.188055, t.ZAt, 6);
        Assert.Equal(0.235259, t.ZGr, 6);
        Assert.InRange(t.Significance, 1.0, 1.1);

        // the deciding precision: 3 sigma and 5 sigma, as fractions of z_AT
        Assert.Equal(0.015735, SigmaNeededFor(3.0), 6);
        Assert.InRange(SigmaNeededFor(3.0) / t.ZAt, 0.0835, 0.0840);
        Assert.Equal(0.009441, SigmaNeededFor(5.0), 6);
        Assert.InRange(SigmaNeededFor(5.0) / t.ZAt, 0.0501, 0.0504);

        // and the shortfall of the published determinations
        var (atTwenty, atFifty) = RequiredImprovement();
        Assert.InRange(atTwenty, 2.3, 2.5);
        Assert.InRange(atFifty, 5.9, 6.1);

        // the qualitative difference, which needs no precision at all
        Assert.True(AtHasNoClockStoppingSurface());
        Assert.Equal(0.5, GrDivergencePoint(), 9);
        Assert.Contains("ALLOWED", TheMeasurementStatus());
        Assert.Contains("PREFERRED nowhere", TheMeasurementStatus());

        Assert.Equal("DERIVED", Verdict());
    }

    [Fact]
    public void Y_G_068_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_068 - Temporal Prediction Audit: the unique prediction of the surviving AT time sector");

        sb.AppendLine("QUESTION. What unique MEASURABLE prediction does the surviving AT time sector make that differs");
        sb.AppendLine("          from GR? Output the observable, its magnitude and the current measurement status.");
        sb.AppendLine("GIVEN     G_019 (the second-order signature), G_020 (the neutron-star redshift), G_035 (the surviving");
        sb.AppendLine("          minimal time sector, 25 of 36 components, g00 only), G_049 (the clock pattern is lossless)");
        sb.AppendLine("GOAL      return from rho-structure to experimental time physics");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The surviving time sector is the clock law, so the prediction is ONE function of ONE variable: the");
        sb.AppendLine("     surface potential x = -GM/(Rc^2). Nothing here is fitted; both redshifts are exact.");
        sb.AppendLine("  2. The constants are RECOMPUTED rather than imported, and the recomputation is cross-checked against");
        sb.AppendLine("     G_019's recorded potential - with the residual reported rather than rounded away.");
        sb.AppendLine("  3. The split is taken from the SERIES and the redshift through AtNumerics.ExpM1, because at solar-system");
        sb.AppendLine("     depths the difference is beneath the rounding of the operands (Numerical Reproducibility rule 4).");
        sb.AppendLine("  4. The measurement status is reported in the same breath as the prediction: a prediction that is allowed");
        sb.AppendLine("     everywhere and preferred nowhere is stated as exactly that.");
        sb.AppendLine();

        PrintHeader(OutputPrediction());
        PrintHeader(OutputMagnitude());
        PrintHeader(OutputStatus());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
