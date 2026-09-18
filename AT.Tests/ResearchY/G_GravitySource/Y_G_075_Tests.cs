using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_075 - Clock-Law Uniqueness Audit. Assume ONLY the surviving empirical constraints - the weak-field
/// solar redshift, GPS time dilation, the first-order agreement with GR and the surviving G_035 temporal sector, with
/// no conformal-optics argument and no discarded spatial derivation - and ask which parts of g00 = -exp(2x) are
/// actually forced, what the maximal family g00 = -F(x) is, and whether the surviving sector makes a unique
/// observable prediction.
/// </summary>
public sealed class Y_G_075_Tests : ResearchTestBase
{
    public Y_G_075_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    [Fact]
    public void Y_G_075_TheForcedPartIsAFormAndTwoNumbers()
    {
        PrintHeader("G_075 - which parts of g00 = -exp(2x) are actually forced");

        var sb = new StringBuilder();
        sb.AppendLine("  feature                                                        laws with it   status");
        foreach (var f in ClockLawUniquenessAudit.ForcedFeatures())
            sb.AppendLine($"  {f.Feature,-62} {f.LawsWithIt,3} of {f.LawsCompared,-3}    {f.Status}");
        sb.AppendLine();
        sb.AppendLine("  the classification of each order, computed from the reach tables:");
        foreach (var c in ClockLawUniquenessAudit.CoefficientClasses())
            sb.AppendLine($"    order {c.Order}: {c.Class}");
        Output.WriteLine(sb.ToString());

        var features = ClockLawUniquenessAudit.ForcedFeatures();
        var laws = ClockLawUniquenessAudit.Laws();
        Assert.Equal(laws.Length, features[0].LawsCompared);

        // F(0) = 1 and F'(0) = 2 are carried by EVERY viable law, so no member of the family can differ there
        Assert.Equal(laws.Length, features.Single(f => f.Feature.StartsWith("F(0) = 1")).LawsWithIt);
        Assert.Equal(laws.Length, features.Single(f => f.Feature.StartsWith("F'(0) = 2")).LawsWithIt);
        Assert.All(features.Where(f => f.Feature.StartsWith("F(0) = 1") || f.Feature.StartsWith("F'(0) = 2")),
            f => Assert.Equal("UNIQUELY FORCED", f.Status));

        // AND NOTHING ABOVE THE FIRST ORDER IS FORCED: the second order is shared by three of the laws and not by the
        // others, which is exactly what "free room" means
        var second = features.Single(f => f.Feature.StartsWith("F''(0) = 4"));
        Assert.True(second.LawsWithIt > 1 && second.LawsWithIt < laws.Length,
            $"the second order must be shared by some laws and not others, got {second.LawsWithIt} of {laws.Length}");

        // the classification is computed, and exactly two orders are uniquely forced: zero and one
        var classes = ClockLawUniquenessAudit.CoefficientClasses();
        Assert.Equal(2, classes.Count(c => c.Class == "UNIQUELY FORCED"));
        Assert.Equal(new[] { 0, 1 }, classes.Where(c => c.Class == "UNIQUELY FORCED").Select(c => c.Order).ToArray());
        Assert.DoesNotContain(classes, c => c.Order >= 2 && c.Class == "UNIQUELY FORCED");

        // the multiplicativity row records the structural property the exponential has and the family does not
        var multiplicity = features.Single(f => f.Feature.StartsWith("multiplicativity"));
        Assert.Equal(1, multiplicity.LawsWithIt);
        Assert.Equal("COMPLETELY FREE", multiplicity.Status);

        // THE DECISIVE ROW: a law that is NOT AT's satisfies the whole forced set, and predicts something else
        Assert.True(ClockLawUniquenessAudit.ForcedSetIsSatisfiedByAnotherLaw(),
            "if only AT's law satisfied the forced set, the uniqueness claim would survive");
    }

    [Fact]
    public void Y_G_075_NoSurvivingMeasurementReachesTheSecondOrder()
    {
        PrintHeader("G_075 - the reach of each surviving constraint, which is what decides the verdict");

        var sb = new StringBuilder();
        sb.AppendLine("  realised constraint              x             precision   orders beyond the first   bound on |delta b2|");
        foreach (var r in ClockLawUniquenessAudit.ConstraintReach())
            sb.AppendLine($"  {r.Constraint,-32} {r.X,-13:E3} {r.RelativePrecision,-11:E3} {r.OrdersBeyondTheFirst,24}   {r.BoundOnSecondOrderCoefficient,16:E3}");
        sb.AppendLine();
        sb.AppendLine("  the programme's rows, kept separate from the data:");
        foreach (var r in ClockLawUniquenessAudit.ProgrammeReach())
            sb.AppendLine($"  {r.Row,-55} {r.OrdersBeyondTheFirst,3}   {r.BoundOnSecondOrderCoefficient,16:E3}   {r.Status}");
        Output.WriteLine(sb.ToString());

        var reach = ClockLawUniquenessAudit.ConstraintReach();

        // THE MEASUREMENT: neither realised constraint sees past the first order, so neither can pin a second-order
        // coefficient however strong it sounds
        Assert.All(reach.Where(r => r.X != 0.0), r => Assert.Equal(0, r.OrdersBeyondTheFirst));

        // the solar row is the SHARPEST of the two, and it still misses the AT-vs-GR difference by four orders
        double solarBound = reach.Single(r => r.X == ClockLawUniquenessAudit.XSolar).BoundOnSecondOrderCoefficient;
        // F''(0) = 2 * b2, so the AT-vs-GR difference at the second order is 2 * (b2_AT - b2_GR) = 4, the same
        // difference the recorded PPN split carries
        double atSecondOrder = 2.0 * ClockLawUniquenessAudit.Coefficient(ClockLawUniquenessAudit.At(), 2);
        double grSecondOrder = 2.0 * ClockLawUniquenessAudit.Coefficient(ClockLawUniquenessAudit.Gr(), 2);
        double difference = Math.Abs(atSecondOrder - grSecondOrder);
        Assert.True(difference > 3.9 && difference < 4.1, $"the forced-set difference must be the recorded 4, got {difference}");
        Assert.True(solarBound / difference > 1000.0,
            $"the solar constraint must be far too coarse to see the difference, got {solarBound / difference:F1}x");

        // and the GPS row is weaker still, so the ordering of the two constraints is measured rather than assumed
        double gpsBound = reach.Single(r => r.X == ClockLawUniquenessAudit.XEarth).BoundOnSecondOrderCoefficient;
        Assert.True(gpsBound > solarBound);

        // the compact object is the ONLY row that reaches the second order - and it is a projection, not a measurement
        var programme = ClockLawUniquenessAudit.ProgrammeReach();
        Assert.All(programme, r => Assert.True(r.OrdersBeyondTheFirst >= 1));
        Assert.All(programme, r => Assert.Contains("PROJECTION", r.Status, StringComparison.Ordinal));
        // and NEITHER row is a measurement: the status text says so in its own words, which the audit asserts
        Assert.All(programme, r => Assert.True(
            r.Status.Contains("rather than a measurement", StringComparison.OrdinalIgnoreCase)
            || r.Status.Contains("not a measured", StringComparison.OrdinalIgnoreCase)
            || r.Status.Contains("capability", StringComparison.OrdinalIgnoreCase), r.Status));
    }

    [Fact]
    public void Y_G_075_TheMaximalFamilyIsTheFreeRoomAndItNeverCloses()
    {
        PrintHeader("G_075 - the maximal family F = 1 + 2x + x^2 G(x), and the free room order by order");

        var sb = new StringBuilder();
        sb.AppendLine("  the free-room ladder: two VIABLE laws equal below order k and different at order k");
        sb.AppendLine("  order   law A            law B                       difference at order k   both viable");
        foreach (var r in ClockLawUniquenessAudit.FreeRoomLadder())
            sb.AppendLine($"  {r.Order,-7} {r.LawA,-16} {r.LawB,-27} {r.FirstDifference,22:F6}   {r.BothViable}");
        sb.AppendLine();
        sb.AppendLine("  the free part G at the target, per law:");
        foreach (var l in ClockLawUniquenessAudit.Laws())
            sb.AppendLine($"    {l.Name,-35} G(x_target) = {ClockLawUniquenessAudit.FreePart(l, ClockLawUniquenessAudit.XTarget),16:F4}");
        Output.WriteLine(sb.ToString());

        var ladder = ClockLawUniquenessAudit.FreeRoomLadder();

        // THE FREE ROOM NEVER CLOSES: for every order the audit tests there are two viable laws that a constraint at
        // every lower order cannot separate
        Assert.Equal(5, ladder.Length);
        Assert.All(ladder, r => Assert.True(r.BothViable, $"both witnesses must be viable at order {r.Order}"));
        Assert.All(ladder, r => Assert.True(Math.Abs(r.FirstDifference) > 1e-3,
            $"the two laws must actually differ at order {r.Order}, got {r.FirstDifference:E3}"));

        // and the family's own AT member has the free part of the exponential at the target - measured, not typed
        double freePart = ClockLawUniquenessAudit.FreePart(ClockLawUniquenessAudit.At(), ClockLawUniquenessAudit.XTarget);
        double expected = (Math.Exp(2.0 * ClockLawUniquenessAudit.XTarget) - 1.0 - 2.0 * ClockLawUniquenessAudit.XTarget)
                        / (ClockLawUniquenessAudit.XTarget * ClockLawUniquenessAudit.XTarget);
        Assert.Equal(expected, freePart, 10);
        Assert.True(freePart > 0.0, "the exponential's free part is its own x^2 coefficient and above");

        // the second-order coefficient is free, and the two languages agree about it - the metric ladder and the
        // redshift ladder are one statement, not two
        var quadratic = ClockLawUniquenessAudit.QuadraticTable();
        var classical = new[] { "exp(2x)", "1 + 2x", "(1+x)^2", "1/(1-2x)", "Pade [1/1]", "Pade [2/2]" };
        Assert.All(quadratic.Where(q => classical.Contains(q.Law)), q => Assert.Equal(q.Derived, q.RedshiftOrder2, 6));
        Assert.Equal(0.5, quadratic.Single(q => q.Law == "exp(2x)").RedshiftOrder2, 6);
        Assert.Equal(1.5, quadratic.Single(q => q.Law == "1 + 2x").RedshiftOrder2, 6);
        Assert.All(quadratic, q => Assert.Equal(-1.0, q.RedshiftOrder1, 6));
        // the stiff free-room witnesses sit at the stencil's own truncation limit, which the audit REPORTS rather than
        // hides: the residual is a property of the differencing and not of the law, which is why every coefficient the
        // verdict uses is quoted from direct evaluation of F
        Assert.All(quadratic, q => Assert.True(Math.Abs(q.Residual) <= 1e-3 * Math.Max(1.0, Math.Abs(q.Derived)),
            $"{q.Law}: the stencil residual {q.Residual:E3} exceeds the truncation budget against {q.Derived:E3}"));
    }

    [Fact]
    public void Y_G_075_EveryViableLawIsPositiveMonotoneAndCarriesThePinnedData()
    {
        PrintHeader("G_075 - viability, measured on the physical range rather than read off the formula");

        var sb = new StringBuilder();
        sb.AppendLine("  law                                 F(0)       F'(0)      drift      positive   monotone   viable");
        foreach (var v in ClockLawUniquenessAudit.ViabilityTable())
            sb.AppendLine($"  {v.Law,-35} {v.FAtZero,-10:F6} {v.SlopeAtZero,-10:F6} {v.SlopeDrift,-10:E2} {v.Positive,-10} {v.Monotone,-10} {v.Viable}");
        Output.WriteLine(sb.ToString());

        var viability = ClockLawUniquenessAudit.ViabilityTable();
        Assert.All(viability, v => Assert.True(v.Positive && v.Monotone,
            $"{v.Law} must be positive and monotone on the physical range"));
        Assert.All(viability, v => Assert.True(v.Viable, $"{v.Law} must carry the pinned data"));
        Assert.All(viability, v => Assert.Equal(1.0, v.FAtZero, 12));
        // THE PINNED SLOPE, held to the criterion the viability test uses: the classical laws are exact to six
        // decimals, and a law that misses that must be one where the stencil's own drift is large enough to explain it
        var classical = new[] { "exp(2x)", "1 + 2x", "(1+x)^2", "1/(1-2x)", "Pade [1/1]", "Pade [2/2]" };
        Assert.All(viability.Where(v => classical.Contains(v.Law)), v => Assert.Equal(2.0, v.SlopeAtZero, 6));
        Assert.All(viability, v => Assert.True(Math.Abs(v.SlopeAtZero - 2.0) <= Math.Max(1e-6, 8.0 * v.SlopeDrift),
            $"{v.Law}: the measured slope {v.SlopeAtZero:F9} misses the pinned 2 by more than the drift {v.SlopeDrift:E2} allows"));
        Assert.All(viability.Where(v => Math.Abs(v.SlopeAtZero - 2.0) > 1e-6), v => Assert.True(v.SlopeDrift > 1e-7,
            $"{v.Law}: a law that misses 1e-6 must be one the stencil cannot resolve better, drift {v.SlopeDrift:E2}"));
        Assert.Contains(viability, v => Math.Abs(v.SlopeAtZero - 2.0) > 1e-6);   // the stiff witness is really present

        // the free-room family's viability limit is computed by bisection rather than typed: monotone up to c = 2,
        // with the last digits set by the grid's own resolution rather than by the law
        double low = -10.0, high = 2.0;
        for (int i = 0; i < 60; i++)
        {
            double mid = 0.5 * (low + high);
            if (ClockLawUniquenessAudit.IsViable(ClockLawUniquenessAudit.FreeRoom(mid))) low = mid; else high = mid;
        }
        Assert.True(high > 1.99 && high < 2.01, $"the free room's monotonicity limit must be 2, got {high:F6}");
        Assert.True(ClockLawUniquenessAudit.IsViable(ClockLawUniquenessAudit.FreeRoom(0.0)));
        Assert.False(ClockLawUniquenessAudit.IsViable(ClockLawUniquenessAudit.FreeRoom(2.1)));

        // and the family is infinite: a grid of c parameters gives distinct viable laws with distinct predictions
        var predictions = new[] { -4.0, -3.0, -2.0, -1.0, 0.0, 1.0, 1.9 }
            .Select(c => ClockLawUniquenessAudit.OnePlusZ(ClockLawUniquenessAudit.FreeRoom(c), ClockLawUniquenessAudit.XTarget))
            .ToArray();
        Assert.Equal(predictions.Length, predictions.Distinct().Count());
        // a RISING c lowers the prediction, i.e. a falling c raises it - which is the direction the witness family uses
        Assert.True(predictions.Zip(predictions.Skip(1), (a, b) => b - a).All(d => d < 0.0),
            "the prediction must fall strictly as c rises, or the witness family would not be a family");
    }

    [Fact]
    public void Y_G_075_TheTargetPredictionIsUnbounded()
    {
        PrintHeader("G_075 - the predicted surface redshift of the target, for every law the audit carries");

        var recorded = ClockLawUniquenessAudit.RecordedAtTarget();
        var sb = new StringBuilder();
        sb.AppendLine($"  the target is {ClockLawUniquenessAudit.TargetName}, at x = {ClockLawUniquenessAudit.XTarget:F6}");
        sb.AppendLine("  law                                1 + z              shift from AT      viable");
        foreach (var p in ClockLawUniquenessAudit.TargetPredictions())
            sb.AppendLine($"  {p.Law,-35} {p.OnePlusZ,-17:F12} {p.ShiftFromAt,17:E3}      {p.Viable}");
        sb.AppendLine();
        sb.AppendLine($"  recorded AT {recorded.At:F12}, recorded GR {recorded.Gr:F12}, separation {recorded.Separation:E3}");
        sb.AppendLine();
        sb.AppendLine("  the maximal family's envelope, from its two witness families:");
        sb.AppendLine("  witness                          parameter      1 + z              log(1+z)     viable");
        foreach (var w in ClockLawUniquenessAudit.EnvelopeWitnesses())
            sb.AppendLine($"  {w.Witness,-32} {w.Parameter,-14:G6} {w.OnePlusZ,-17:E6} {w.LogOnePlusZ,13:F6}    {w.Viable}");
        Output.WriteLine(sb.ToString());

        // the recorded values are reproduced by the family's own members rather than imported
        Assert.Equal(recorded.At, ClockLawUniquenessAudit.OnePlusZ(ClockLawUniquenessAudit.At(), ClockLawUniquenessAudit.XTarget), 12);
        Assert.Equal(recorded.Gr, ClockLawUniquenessAudit.OnePlusZGr(ClockLawUniquenessAudit.XTarget), 12);

        var witnesses = ClockLawUniquenessAudit.EnvelopeWitnesses();
        Assert.All(witnesses, w => Assert.True(w.Viable, $"{w.Witness} must be viable to bind the envelope"));

        var envelope = ClockLawUniquenessAudit.Envelope()[0];
        Assert.True(envelope.BoundedBelow, "the clock slows, so 1 + z is bounded below by 1");
        Assert.False(envelope.BoundedAbove, "the family must be unbounded above for the verdict to be a refutation");

        // BOTH extremes are explicit: the suppression witness reaches the lowest prediction the grid can RESOLVE, and
        // the free-room witness passes any bound a finite parameter can state
        Assert.True(envelope.LowestWitness < 1.15, $"the suppression witness must fall to ~1.14, got {envelope.LowestWitness:F6}");
        Assert.True(envelope.HighestWitness > 1.0e3, $"the free-room witness must rise past 1e3, got {envelope.HighestWitness:E3}");
        Assert.True(ClockLawUniquenessAudit.FamilyBracketsGr(),
            "the family must contain members on BOTH sides of GR's prediction at the target");

        // the finite part alone - the named laws - is already wider than the effect it was built to measure
        var spread = ClockLawUniquenessAudit.NamedSpread()[0];
        Assert.True(spread.Spread > spread.AtVsGrSeparation,
            $"the named laws' spread {spread.Spread:E3} must exceed the AT-vs-GR separation {spread.AtVsGrSeparation:E3}");
        Assert.True(spread.Ratio > 1.0);
    }

    [Fact]
    public void Y_G_075_TheFamilyContainsGrSoTheForcedSetIsNotAts()
    {
        PrintHeader("G_075 - the decisive row: GR's own clock law is a member of the family");

        var at = ClockLawUniquenessAudit.At();
        var gr = ClockLawUniquenessAudit.Gr();
        double x = ClockLawUniquenessAudit.XTarget;

        var sb = new StringBuilder();
        sb.AppendLine($"  AT's law : F(0) = {at.F(0.0):F12}, F'(0) = {ClockLawUniquenessAudit.Coefficient(at, 1):F6}, 1 + z = {ClockLawUniquenessAudit.OnePlusZ(at, x):F12}");
        sb.AppendLine($"  GR's law : F(0) = {gr.F(0.0):F12}, F'(0) = {ClockLawUniquenessAudit.Coefficient(gr, 1):F6}, 1 + z = {ClockLawUniquenessAudit.OnePlusZ(gr, x):F12}");
        sb.AppendLine();
        sb.AppendLine("  multiplicativity residuals:");
        foreach (var c in ClockLawUniquenessAudit.Composition())
            sb.AppendLine($"    {c.Law,-35} {c.Residual:E3}");
        Output.WriteLine(sb.ToString());

        // GR's clock law is viable under every criterion the audit applies, so the surviving constraints admit it
        Assert.True(ClockLawUniquenessAudit.IsViable(gr));
        Assert.Equal(at.F(0.0), gr.F(0.0), 12);
        Assert.Equal(ClockLawUniquenessAudit.Coefficient(at, 1), ClockLawUniquenessAudit.Coefficient(gr, 1), 6);

        // ...and it predicts something else at the target, so the forced set cannot be AT's
        Assert.NotEqual(ClockLawUniquenessAudit.OnePlusZ(at, x), ClockLawUniquenessAudit.OnePlusZ(gr, x), 6);

        // and the composition test separates the exponential from every other member of the family
        var composition = ClockLawUniquenessAudit.Composition();
        Assert.True(composition.Single(c => c.Law == "exp(2x)").Residual < 1e-12);
        Assert.All(composition.Where(c => c.Law != "exp(2x)"), c => Assert.True(c.Residual > 1e-6,
            $"{c.Law} must fail multiplicativity, which is what makes it a different law"));

        // the single structural constraint therefore admits exactly one member - the repair priced in the verdict
        Assert.Equal(1, composition.Count(c => c.Residual < 1e-12));
    }

    [Fact]
    public void Y_G_075_TheVerdictIsARefutationOfUniqueness()
    {
        PrintHeader("G_075 - the verdict");

        string verdict = ClockLawUniquenessAudit.Verdict();
        Output.WriteLine(verdict);
        Output.WriteLine(ClockLawUniquenessAudit.WhereItStands());

        Assert.StartsWith("REFUTED", verdict);
        Assert.Contains("TWO NUMBERS AND A FORM", verdict);
        Assert.Contains("UNBOUNDED", verdict);
        Assert.Contains("multiplicativity", verdict);
        Assert.Contains("OUTPUT: REFUTED", verdict);
        Assert.DoesNotContain("OUTPUT: UNIQUE", verdict);

        // the verdict is computed from live branches: the bounded branch and the single-law branch are both real code
        // paths, and the audit asserts the branch that fired rather than a string
        var envelope = ClockLawUniquenessAudit.Envelope()[0];
        Assert.False(envelope.Bounded);
        Assert.True(ClockLawUniquenessAudit.ForcedSetIsSatisfiedByAnotherLaw());
        // ITEM SEVEN OF THE QUESTION, AS A COMPUTED PREDICATE RATHER THAN A SENTENCE
        Assert.False(ClockLawUniquenessAudit.MakesAUniqueObservablePrediction());
    }

    [Fact]
    public void Y_G_075_Diag()
    {
        PrintHeader("G_075 - DIAGNOSTIC: every computed surface");

        Output.WriteLine(ClockLawUniquenessAudit.OutputForced());
        Output.WriteLine(ClockLawUniquenessAudit.OutputLadder());
        Output.WriteLine(ClockLawUniquenessAudit.OutputReach());
        Output.WriteLine(ClockLawUniquenessAudit.OutputTarget());
        Output.WriteLine(ClockLawUniquenessAudit.OutputEnvelope());
        Output.WriteLine(ClockLawUniquenessAudit.OutputVerdict());
    }
}
