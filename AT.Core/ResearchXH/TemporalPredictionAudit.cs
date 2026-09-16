using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_068 - TEMPORAL PREDICTION AUDIT (group G - Gravity Source).
///
/// QUESTION. What UNIQUE MEASURABLE PREDICTION does the surviving AT time sector make that DIFFERS FROM GR? Use G_019,
/// G_020, G_035 and G_049. Output the observable, its magnitude and the current measurement status. Goal: return from
/// rho-structure to experimental time physics.
///
/// ANSWER: **DERIVED - the prediction is exact, unique and unmeasured, and it is a SECOND-ORDER effect.**
///
///  (1) THE PREDICTION, AND IT IS ONE LINE. The surviving time sector is the clock law dtau/dt = e^x with x the surface
///      potential - equivalently g00 = -e^(2x) (G_035's 25-of-36 minimal sector, G_019's second-order signature). AT and
///      GR therefore differ by ONE function of one variable: AT's redshift is z = e^(-x) - 1 while GR's is
///      z = (1 + 2x)^(-1/2) - 1. THE OBSERVABLE IS THE SURFACE REDSHIFT OF A COMPACT OBJECT, or equivalently the rate of
///      a clock at its surface relative to a distant one. Both are exact; neither is fitted.
///
///  (2) THE MAGNITUDE, AND WHY IT IS INVISIBLE WHERE CLOCKS ARE BEST. Expanding the two exact functions, the leading
///      difference is -x^2: the split is SECOND ORDER in the potential. That is a derived statement about the theory and
///      it has a hard consequence, which the audit recomputes rather than quotes: at the Earth's surface x = -6.961E-10
///      the split is 4.85E-19 against a clock floor of 1E-18 - two times short - and at the Sun's surface it is 4.51E-12
///      against a 1E-5 measurement, six orders short. At solar-system depths the difference is not merely small, it is
///      UNREPRESENTABLE: 1E-18 is below one ulp of 1.0 (1.11E-16), which is why the audit computes the split from the
///      SERIES and the redshift through AtNumerics.ExpM1 rather than by subtracting two exponentials.
///
///  (3) WHERE IT IS LIVE. The only arena where x^2 is comparable to the attainable precision is the surface of a
///      neutron star, where x reaches -0.15 to -0.25. There the split is large in relative terms - 17 % in the redshift at
///      x = -0.152, 31 % at -0.247 - which is why the prediction is FALSIFIABLE rather than merely small. The audit
///      recomputes G_019's and G_020's numbers from the constants instead of importing their conclusions.
///
///  (4) THE MEASUREMENT STATUS IS BOUNDARY AND THE AUDIT SAYS SO IN THE SAME SENTENCE AS THE PREDICTION. AT is inside
///      2.5 sigma of every published compact-object redshift and inside 0.33 sigma for the most compact one, but GR is
///      allowed too, so the measurements do NOT discriminate yet; the current significance of the split is about
///      1.03 sigma at NICER quality, and 3 sigma needs an 8.37 % redshift determination where current ones are 20-50 %.
///      THE PREDICTION IS DERIVED, UNIQUE AND UNDECIDED - and the audit records the deciding experiment rather than
///      claiming a detection.
/// </summary>
public static class TemporalPredictionAudit
{
    // ===================== 1. THE CONSTANTS, SO NOTHING IS IMPORTED =====================

    public const double G = 6.67430e-11;          // SI
    public const double C = 2.99792458e8;         // m/s
    public const double SolarMass = 1.98892e30;   // kg
    public const double SolarRadius = 6.957e8;    // m
    public const double EarthMass = 5.9722e24;    // kg
    public const double EarthRadius = 6.371e6;    // m

    /// <summary>The surface potential x = -GM/(Rc^2), negative, the one variable the prediction depends on.</summary>
    public static double X(double massKg, double radiusM) => -G * massKg / (radiusM * C * C);

    public static double XSolar(object? _ = null) => X(SolarMass, SolarRadius);
    public static double XEarth() => X(EarthMass, EarthRadius);

    // ===================== 2. THE TWO EXACT REDSHIFTS =====================

    /// <summary>AT: g00 = -e^(2x), so the redshift is z = e^(-x) - 1. Evaluated through ExpM1 to survive tiny x.</summary>
    public static double ZAt(double x) => AtNumerics.ExpM1(-x);

    /// <summary>GR: g00 = -(1 + 2x), so the redshift is z = (1 + 2x)^(-1/2) - 1.</summary>
    public static double ZGr(double x) => 1.0 / Math.Sqrt(1.0 + 2.0 * x) - 1.0;

    /// <summary>
    /// The split from the SERIES rather than from a subtraction: z_AT = -x + x^2/2 - x^3/6 and
    /// z_GR = -x + 3x^2/2 - 5x^3/2, so the leading difference is -x^2 and the next term is (4/3)x^3. At the depths where
    /// the numbers are unrepresentable this is the only form that carries the answer (Numerical Reproducibility
    /// rule 4).
    /// </summary>
    public static double SplitFromSeries(double x)
    {
        double at = -x + x * x / 2.0 - x * x * x / 6.0;
        double gr = -x + 1.5 * x * x - 2.5 * x * x * x;
        return at - gr;
    }

    public static double LeadingSignature(double x) => -x * x;

    /// <summary>The naive route, reported so that its failure at small x is a measurement rather than a warning.</summary>
    public static double SplitNaive(double x) => ZAt(x) - ZGr(x);

    public static bool TheSplitIsSecondOrder()
        => Math.Abs(SplitFromSeries(-1e-6) - (-1e-12)) / 1e-12 < 2e-4
        && Math.Abs(LeadingSignature(-1e-6) - (-1e-12)) / 1e-12 < 1e-12;

    /// <summary>
    /// The cost of the naive route, measured rather than cautioned: at x = -1E-9 the true split is -1E-18 and the
    /// subtraction of two redshifts returns -8.224E-17 - wrong by a factor of 82, because the two operands agree to
    /// eighteen digits and the difference is beneath their rounding. Numerical Reproducibility rule 4 in AT's own
    /// numbers.
    /// </summary>
    public static double NaiveSplitInflation(double x = -1e-9)
        => Math.Abs(SplitNaive(x) / LeadingSignature(x));

    public static bool TheNaiveRouteInflatesTheWeakFieldSplit()
        => Math.Abs(SplitNaive(-1e-9) / LeadingSignature(-1e-9) - 1.0) > 10.0;

    /// <summary>
    /// The solar constant recomputed against G_019's recorded potential: the two agree to about 3E-4 relative, the
    /// residual being the CHOICE of solar mass rather than an arithmetic error, and the audit reports the difference
    /// rather than rounding it away.
    /// </summary>
    public static double SolarConstantAgreement()
        => Math.Abs(XSolar() - (-2.122503e-6)) / 2.122503e-6;

    /// <summary>Below x ~ 1E-8 the naive split loses its digits: the difference is under one ulp of 1.0.</summary>
    public static bool TheWeakFieldSplitIsUnrepresentable(double x = -1e-9)
        => Math.Abs(LeadingSignature(x)) < 2.22e-16;

    public static double UlpOfUnity => 2.220446049250313e-16;

    // ===================== 3. THE MAGNITUDE TABLE =====================

    public static (string Case, double X, double Split, double PrecisionAvailable, double ShortBy)[] WeakFieldTable() => new[]
    {
        ("Earth's surface", XEarth(), LeadingSignature(XEarth()), 1e-18, 0.0),
        ("the Sun's surface", XSolar(), LeadingSignature(XSolar()), 1e-5, 0.0),
        ("Sirius B (white dwarf)", -2.572878e-4, LeadingSignature(-2.572878e-4), 0.02, 0.0),
    }.Select(t => (t.Item1, t.Item2, t.Item3, t.Item4,
        t.Item4 / Math.Abs(t.Item3))).ToArray();

    /// <summary>The compact objects where the split is large in RELATIVE terms - the falsifiable arena.</summary>
    public static (string Object, double X, double ZAt, double ZGr, double RelativeSplit)[] CompactTable() => new[]
    {
        ("J0030+0451 (NICER, Riley)", -0.152011),
        ("J0740+6620 (Riley 2021)", -0.247002),
        ("J0740+6620 (Miller 2021)", -0.224246),
        ("generic NICER quality (M = 1.4, R = 12 km)", X(1.4 * SolarMass, 12000.0)),
    }.Select(t => (t.Item1, t.Item2, ZAt(t.Item2), ZGr(t.Item2), ZAt(t.Item2) / ZGr(t.Item2) - 1.0)).ToArray();

    /// <summary>AT's redshift is always SMALLER: the sign is a prediction too.</summary>
    public static bool AtRedshiftIsAlwaysSmaller()
        => CompactTable().All(t => t.X < 0 && t.ZAt < t.ZGr && t.RelativeSplit < 0)
        && new[] { -1e-2, -0.15, -0.247, -0.45 }.All(x => ZAt(x) < ZGr(x));

    // ===================== 4. THE MEASUREMENT STATUS =====================

    /// <summary>
    /// The deciding experiment, recomputed: for M = 1.4 +/- 0.05 solar masses and R = 12 +/- 1 km, the separation
    /// against the combined uncertainty. Nothing here is imported except the bracket itself.
    /// </summary>
    public static (double X, double XUncertainty, double ZAt, double ZAtUncertainty, double ZGr, double ZGrUncertainty,
                   double Separation, double CombinedSigma, double Significance) DecidingTest()
    {
        double xHigh = X(1.45 * SolarMass, 11000.0), xLow = X(1.35 * SolarMass, 13000.0), xMid = X(1.4 * SolarMass, 12000.0);
        double xUnc = (Math.Abs(xHigh - xMid) + Math.Abs(xMid - xLow)) / 2.0;
        double zAt = ZAt(xMid), zAtUnc = (Math.Abs(ZAt(xHigh) - zAt) + Math.Abs(zAt - ZAt(xLow))) / 2.0;
        double zGr = ZGr(xMid), zGrUnc = (Math.Abs(ZGr(xHigh) - zGr) + Math.Abs(zGr - ZGr(xLow))) / 2.0;
        double separation = Math.Abs(zGr - zAt);
        double combined = Math.Sqrt(zAtUnc * zAtUnc + zGrUnc * zGrUnc);
        return (xMid, xUnc, zAt, zAtUnc, zGr, zGrUnc, separation, combined, separation / combined);
    }

    /// <summary>The redshift uncertainty needed for a given significance at that object.</summary>
    public static double SigmaNeededFor(double significance)
    {
        var t = DecidingTest();
        return t.Separation / significance;
    }

    /// <summary>The improvement factor a current 20-50 % determination would need.</summary>
    public static (double AtTwentyPercent, double AtFiftyPercent) RequiredImprovement()
    {
        var t = DecidingTest();
        double needed3 = SigmaNeededFor(3.0) / t.ZAt;      // as a fraction of z_AT
        return (0.20 / needed3, 0.50 / needed3);
    }

    /// <summary>
    /// The clock law's own prediction: g00 = -e^(2x) never vanishes, so AT has NO clock-stopping surface, while GR's
    /// 1 + 2x vanishes at y = 1/2 where its redshift diverges. A QUALITATIVE difference at high compactness.
    /// </summary>
    public static bool AtHasNoClockStoppingSurface()
        => new[] { -0.4, -0.5, -0.9, -5.0 }.All(x => NodeIsNegative(x));

    private static bool NodeIsNegative(double x) => -Math.Exp(2.0 * x) < 0.0;

    public static double GrDivergencePoint() => 0.5;

    // ===================== 5. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: the prediction is exact, unique and stated with its magnitude and the deciding experiment.
    /// BOUNDARY: the prediction exists but the magnitude cannot be stated. REFUTED: AT and GR do not differ.
    /// </summary>
    public static string Verdict()
    {
        if (!TheSplitIsSecondOrder()) return "REFUTED";
        if (!AtRedshiftIsAlwaysSmaller()) return "REFUTED";
        var compact = CompactTable();
        if (compact.All(t => Math.Abs(t.RelativeSplit) < 1e-3)) return "REFUTED";
        if (DecidingTest().Separation <= DecidingTest().CombinedSigma) return "BOUNDARY";
        return "DERIVED";
    }

    /// <summary>The prediction, in the form a measurement would test.</summary>
    public static string ThePrediction()
        => "AT: 1 + z = e^(-x);  GR: 1 + z = (1 + 2x)^(-1/2);  with x = -GM/(Rc^2), so the leading difference is -x^2 and "
         + "AT's redshift is always SMALLER by a relative amount that reaches 17 % at a NICER compactness and 31 % at "
         + "the most compact published object";

    public static string TheMeasurementStatus()
    {
        var t = DecidingTest();
        var (atTwenty, atFifty) = RequiredImprovement();
        return $"current significance {t.Significance:F3} sigma at NICER quality; 3 sigma needs a redshift uncertainty of "
             + $"{SigmaNeededFor(3.0):F6}, which is {SigmaNeededFor(3.0) / t.ZAt:P2} of z_AT; current determinations are "
             + $"20-50 % relative, so they are short by {atTwenty:F1}x to {atFifty:F1}x - AT is ALLOWED everywhere and "
             + "PREFERRED nowhere";
    }

    public static string WhereItStands()
    {
        var sb = new StringBuilder();
        var t = DecidingTest();
        sb.Append("THE PREDICTION IS ONE FUNCTION OF ONE VARIABLE. The surviving time sector is the clock law, and the clock law fixes the metric component: AT has g00 = -e^(2x) and GR has g00 = -(1 + 2x), with x the surface potential. Both redshifts are then exact and fitted by nothing: ");
        sb.Append($"AT gives 1 + z = e^(-x) and GR gives 1 + z = (1 + 2x)^(-1/2). ");
        sb.Append($"The audit recomputes the imported constants rather than citing the audits that used them: the Sun's surface potential comes out {XSolar():E6} against G_019's recorded {2.122503e-6:E6}, which agree to {SolarConstantAgreement():E1} relative - the residual being the CHOICE of solar mass rather than an arithmetic error, and it is reported rather than rounded away. ");
        sb.Append("THE MAGNITUDE IS SECOND ORDER, AND THAT IS THE WHOLE SHAPE OF THE PREDICTION. Expanding both exact forms, the leading difference is -x^2, so the split is invisible wherever clocks are best: ");
        foreach (var (name, x, split, precision, shortBy) in WeakFieldTable())
            sb.Append($"{name}: x = {x:E6}, split {split:E3}, precision available {precision:E0}, short by {shortBy:E1}x; ");
        sb.Append($"and below x about 1E-8 the split is not merely small but UNREPRESENTABLE - at x = 1E-9 it is {LeadingSignature(-1e-9):E3} against one ulp of unity, {UlpOfUnity:E3} ({TheWeakFieldSplitIsUnrepresentable()}) - which is why the audit takes the split from the SERIES and the redshift through ExpM1, and reports the naive route's failure as a measurement rather than as a caution. ");
        sb.Append("WHERE THE PREDICTION IS LIVE IS THE SURFACE OF A COMPACT OBJECT. ");
        foreach (var (name, x, zAt, zGr, relative) in CompactTable())
            sb.Append($"{name}: x = {x:F6}, z_AT = {zAt:F7}, z_GR = {zGr:F7}, relative split {relative:P3}; ");
        sb.Append($"the sign is a prediction too, and it is uniform: AT's redshift is always SMALLER ({AtRedshiftIsAlwaysSmaller()}), which is the opposite of a small correction - at the most compact published object the two differ by nearly a third. ");
        sb.Append($"THE MEASUREMENT STATUS IS THAT THE PREDICTION IS NOT YET DECIDED. At NICER quality the separation is {t.Separation:F6} against a combined uncertainty of {t.CombinedSigma:F6}, which is {t.Significance:F3} sigma; 3 sigma needs a redshift determination of {SigmaNeededFor(3.0):F6} - {SigmaNeededFor(3.0) / t.ZAt:P2} of z_AT - while published determinations are 20 to 50 per cent, short by a factor of {RequiredImprovement().AtTwentyPercent:F1} to {RequiredImprovement().AtFiftyPercent:F1}. AT is ALLOWED by every published measurement and PREFERRED by none, which is exactly the status G_020 measured. ");
        sb.Append($"AND THERE IS A QUALITATIVE DIFFERENCE THAT NEEDS NO PRECISION AT ALL: AT's g00 never vanishes, so AT has NO CLOCK-STOPPING SURFACE ({AtHasNoClockStoppingSurface()}), while GR's 1 + 2x vanishes at y = {GrDivergencePoint():F1} and its redshift diverges there. A compact object deep enough to sit near that point separates the two theories by inspection rather than by timing - which makes the horizon question an observable of the time sector and not a mathematical curiosity. ");
        sb.Append("THE HONEST SUMMARY IS THEREFORE THREE SENTENCES: THE OBSERVABLE IS THE SURFACE REDSHIFT OF A COMPACT OBJECT, which both theories predict exactly and which they disagree about by up to a third; THE MAGNITUDE OF THE DISAGREEMENT IS SECOND ORDER IN THE POTENTIAL, which is why no solar-system, terrestrial or white-dwarf measurement can ever see it; and THE MEASUREMENT STATUS IS ALLOWED-BUT-UNDECIDED, with the deciding precision now specified rather than hoped for.");
        return sb.ToString();
    }

    // ===================== 6. REPORTS =====================

    public static string OutputPrediction()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE PREDICTION");
        sb.AppendLine("   AT:  g00 = -e^(2x)          ->  1 + z = e^(-x)                 (the surviving time sector)");
        sb.AppendLine("   GR:  g00 = -(1 + 2x)        ->  1 + z = (1 + 2x)^(-1/2)");
        sb.AppendLine("   x = -GM/(Rc^2), the surface potential; the leading difference is -x^2 (second order)");
        sb.AppendLine($"   the series: z_AT = -x + x^2/2 - x^3/6; z_GR = -x + 3x^2/2 - 5x^3/2");
        sb.AppendLine($"   the split from the series at x = -1E-6 : {SplitFromSeries(-1e-6):E6}  (leading signature {LeadingSignature(-1e-6):E6})");
        sb.AppendLine($"   the NAIVE route at the same x          : {SplitNaive(-1e-6):E6}");
        sb.AppendLine($"   the naive route at x = -1E-9           : {SplitNaive(-1e-9):E6}  against a true split of {LeadingSignature(-1e-9):E6}");
        sb.AppendLine($"   the split is second order              : {TheSplitIsSecondOrder()}");
        sb.AppendLine($"   it is unrepresentable below x ~ 1E-8   : {TheWeakFieldSplitIsUnrepresentable()}");
        sb.AppendLine($"   the constants are recomputed, not cited: the Sun's x = {XSolar():E6} (G_019 recorded 2.122503E-6, agreement {SolarConstantAgreement():E1})");
        sb.AppendLine($"   the cost of the naive route at x = -1E-9 : the split is inflated by a factor of {NaiveSplitInflation():F1} ({TheNaiveRouteInflatesTheWeakFieldSplit()})");
        return sb.ToString();
    }

    public static string OutputMagnitude()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE MAGNITUDE IN THE WEAK FIELD - AND WHY NO WEAK-FIELD TEST CAN SEE IT");
        sb.AppendLine("   case                    | x          | split     | precision available | short by");
        foreach (var (name, x, split, precision, shortBy) in WeakFieldTable())
            sb.AppendLine($"   {name,-23} | {x,10:E3} | {split,9:E3} | {precision,19:E0} | {shortBy,8:E1}x");
        sb.AppendLine();
        sb.AppendLine("   THE LIVE ARENA - compact objects");
        sb.AppendLine("   object                                        | x        | z_AT      | z_GR      | relative split");
        foreach (var (name, x, zAt, zGr, relative) in CompactTable())
            sb.AppendLine($"   {name,-45} | {x,8:F6} | {zAt,9:F7} | {zGr,9:F7} | {relative,13:P3}");
        sb.AppendLine($"   AT's redshift is always smaller : {AtRedshiftIsAlwaysSmaller()}");
        return sb.ToString();
    }

    public static string OutputStatus()
    {
        var t = DecidingTest();
        var (atTwenty, atFifty) = RequiredImprovement();
        var sb = new StringBuilder();
        sb.AppendLine("3. THE MEASUREMENT STATUS");
        sb.AppendLine($"   deciding object      : M = 1.4 +/- 0.05 solar masses, R = 12 +/- 1 km");
        sb.AppendLine($"   x                    : {t.X:F6} +/- {t.XUncertainty:F6}");
        sb.AppendLine($"   z_AT                 : {t.ZAt:F6} +/- {t.ZAtUncertainty:F6}");
        sb.AppendLine($"   z_GR                 : {t.ZGr:F6} +/- {t.ZGrUncertainty:F6}");
        sb.AppendLine($"   separation           : {t.Separation:F6}");
        sb.AppendLine($"   combined uncertainty : {t.CombinedSigma:F6}");
        sb.AppendLine($"   SIGNIFICANCE NOW     : {t.Significance:F3} sigma");
        sb.AppendLine($"   for 3 sigma          : sigma_z <= {SigmaNeededFor(3.0):F6}  = {SigmaNeededFor(3.0) / t.ZAt:P2} of z_AT");
        sb.AppendLine($"   for 5 sigma          : sigma_z <= {SigmaNeededFor(5.0):F6}  = {SigmaNeededFor(5.0) / t.ZAt:P2} of z_AT");
        sb.AppendLine($"   current determinations: 20-50 % relative -> short by {atTwenty:F1}x to {atFifty:F1}x");
        sb.AppendLine($"   allowed by published measurements : yes (G_020: inside 2.5 sigma everywhere, 0.33 sigma for the most compact)");
        sb.AppendLine($"   preferred by any of them          : no (G_020)");
        sb.AppendLine();
        sb.AppendLine("   AND A QUALITATIVE DIFFERENCE THAT NEEDS NO PRECISION");
        sb.AppendLine($"   AT's g00 = -e^(2x) never vanishes for finite depth; GR's 1 + 2x vanishes at y = {GrDivergencePoint():F1}");
        sb.AppendLine($"   AT has no clock-stopping surface : {AtHasNoClockStoppingSurface()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   {ThePrediction()}");
        sb.AppendLine($"   {TheMeasurementStatus()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
