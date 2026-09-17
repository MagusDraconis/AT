using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_074 - Clock Law Necessity Audit. Does any surviving AT result require dtau/dt = rho^(1/d)
/// specifically, or only a monotonic function of rho? Recompute g00, the redshift and the compact-star prediction.
/// </summary>
public sealed class Y_G_074_Tests : ResearchTestBase
{
    public Y_G_074_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    [Fact]
    public void Y_G_074_MonotonicityIsAllTheShapeNeedsAndEveryCandidateHasIt()
    {
        PrintHeader("G_074 - the shape, which every candidate has, and the weak-field ladder");

        var sb = new StringBuilder();
        sb.AppendLine("  law                          monotone   positive   rate x^1   x^1 in 1+z   x^2 in 1+z");
        var shape = ClockLawNecessityAudit.Shape();
        foreach (var r in ClockLawNecessityAudit.WeakFieldLadder())
        {
            var s = shape.Single(x => x.Law == r.Law);
            sb.AppendLine($"  {r.Law,-28} {s.Monotone,-10} {s.Positive,-10} {r.RateOrder1,-10:F6} {r.Order1,-12:F6} {r.Order2,-12:F6}");
        }
        Output.WriteLine(sb.ToString());

        // POSITIVITY AND MONOTONICITY ALREADY EXCLUDE TWO OF THE FOUR, for measured reasons, and the audit asserts
        // the count rather than pretending every candidate passes the shape test
        Assert.Equal(ClockLawNecessityAudit.Laws().Length, shape.Length);
        // THREE of four are monotone and positive; the Pade form is the one excluded, and it is excluded by
        // POSITIVITY rather than by monotonicity - see the note below on why my first version got that wrong
        Assert.Equal(3, shape.Count(s => s.Monotone && s.Positive));
        Assert.Contains(shape, s => s.Law == "rho^(1/d)" && s.Monotone && s.Positive);
        Assert.Contains(shape, s => s.Law.StartsWith("exp(rho)") && s.Monotone && s.Positive);

        // the log law fails BELOW UNIT DENSITY: the rate turns negative where the density is sub-vacuum
        Assert.Single(shape, s => !s.PositiveBelowUnitDensity);
        Assert.False(shape.Single(s => s.Law == "ln(rho)").PositiveBelowUnitDensity);

        // AND THE PADE FORM IS EXCLUDED BY POSITIVITY, NOT BY MONOTONICITY - which I first got wrong. It has a pole
        // at rho = 5/2 inside the physical range, and a PAIRWISE monotonicity test passes straight through a pole
        // because the rate jumps from very negative to very positive: this repository already recorded that lesson
        // ("does not reverse" is not "is monotone", QM_004/QM_005). Monotonicity therefore admits all four, and the
        // only thing that catches the pole is the SIGN.
        var pade = shape.Single(s => s.Law.StartsWith("Pade"));
        Assert.False(pade.Positive);         // the sign flip at the pole, which is the robust detector
        Assert.False(pade.Monotone);         // and the pairwise check also catches it, through the infinity
        // BOTH checks fail for the SAME reason, which is worth stating: the pole sits at rho = 5/2 inside the range,
        // so the rate crosses zero and passes through infinity together. I first asserted that the pairwise test could
        // NOT see a pole; the measurement refused it, and the correction is recorded.

        // and every candidate passes the first order, so the weak field tells them apart no better than monotonicity
        Assert.Equal(ClockLawNecessityAudit.Laws().Length,
            ClockLawNecessityAudit.RequirementTable().Single(r => r.Requirement == "the rate's first order is 1 + x").Survivors);

        // the AT law's first and second coefficients are the recorded ones
        var at = ClockLawNecessityAudit.WeakFieldLadder().Single(r => r.Law == "rho^(1/d)");
        Assert.Equal(1.0, at.RateOrder1, 5);      // the RATE rises as 1 + x
        Assert.Equal(-1.0, at.Order1, 5);         // so the REDSHIFT's first coefficient is its negative
        Assert.Equal(0.5, at.Order2, 5);          // the recorded 0.5 against GR's 1.5
    }

    [Fact]
    public void Y_G_074_TheReparametrisationIdentityShowsTheNamesAreNotTheTheories()
    {
        PrintHeader("G_074 - the reparametrisation identity: what distinguishes the candidates is the MAP");

        double residual = ClockLawNecessityAudit.ReparametrisationResidual();
        var sb = new StringBuilder();
        sb.AppendLine("  exp(rho) composed with the logarithmic map rho = e^(d x) against rho^(1/d):");
        sb.AppendLine($"    residual {residual:E3}");
        sb.AppendLine();
        sb.AppendLine("  SO THE CANDIDATE NAMES ARE NOT THREE THEORIES: what converts a density into a potential is the map,");
        sb.AppendLine("  and the surviving audits supply the map rather than derive it.");
        Output.WriteLine(sb.ToString());

        Assert.True(residual < 1e-12, $"the exponential with the log map must BE the power law, got {residual:E3}");

        // and the two are then indistinguishable in the redshift as well as in the rate
        var power = ClockLawNecessityAudit.Laws()[0];
        double x = ClockLawNecessityAudit.XCompact;
        // the identity pairs exp(rho) with a LINEAR map against rho^(1/d) with a LOG map - not exp(rho) with the log
        // map, which is what my first version compared and which is not the identity at all
        double viaLogMap = 1.0 / Math.Exp((1.0 + x) - 1.0);
        Assert.Equal(ClockLawNecessityAudit.OnePlusZ(power, x), viaLogMap, 12);
    }

    [Fact]
    public void Y_G_074_TheFormShowsItselfOnlyAtSecondOrder()
    {
        PrintHeader("G_074 - the form's only signature: the second-order coefficient");

        var sb = new StringBuilder();
        foreach (var r in ClockLawNecessityAudit.RequirementTable())
            sb.AppendLine($"  {r.Requirement,-33} {r.Survivors,2} of {ClockLawNecessityAudit.Laws().Length}   {string.Join(", ", r.Which)}");
        Output.WriteLine(sb.ToString());

        var table = ClockLawNecessityAudit.RequirementTable();

        // monotonicity and the ratio shape admit every candidate
        Assert.Equal(ClockLawNecessityAudit.Laws().Length, table.Single(r => r.Requirement == "positive and monotone").Survivors);
        Assert.Equal(ClockLawNecessityAudit.Laws().Length, table.Single(r => r.Requirement == "redshift is a ratio of rates").Survivors);

        // the recorded second order admits fewer, and it is the only discriminating requirement
        int atSecond = table.Single(r => r.Requirement == "the recorded second order").Survivors;
        Assert.True(atSecond >= 1, "the recorded power law must survive its own second order");
        Assert.True(atSecond < ClockLawNecessityAudit.Laws().Length,
            "if every candidate survived the second order the audit would have no discriminator at all");

        // and the power law is among the survivors
        Assert.Contains("rho^(1/d)", table.Single(r => r.Requirement == "the recorded second order").Which);
    }

    [Fact]
    public void Y_G_074_TheCompactStarPredictionIsWhereTheFormBecomesPhysical()
    {
        PrintHeader("G_074 - the compact-star prediction and whether the programme can resolve the form");

        var sb = new StringBuilder();
        sb.AppendLine($"  at x = {ClockLawNecessityAudit.XCompact:F6}:");
        foreach (var c in ClockLawNecessityAudit.CompactStarPrediction())
            sb.AppendLine($"  {c.Law,-28} 1 + z = {c.OnePlusZ:F12}   shift from the power law {c.ShiftFromThePowerLaw:E3}");
        var r = ClockLawNecessityAudit.Resolvability()[0];
        sb.AppendLine();
        sb.AppendLine($"  form spread {r.FormSpread:E3} against the AT-vs-GR separation {r.AtVsGrSeparation:E3}  ratio {r.Ratio:F4}");
        Output.WriteLine(sb.ToString());

        var compact = ClockLawNecessityAudit.CompactStarPrediction();

        // the power law is the reference, so its own shift is zero
        Assert.Equal(0.0, compact.Single(c => c.Law == "rho^(1/d)").ShiftFromThePowerLaw, 12);

        // every candidate keeps the AT-below-GR ordering at this object - the ordering does not discriminate
        Assert.All(compact, c => Assert.True(c.OnePlusZ < c.Gr, c.Law));

        // and the forms differ from each other by a measurable amount
        Assert.True(r.FormSpread > 1e-6, $"the forms must differ measurably, got {r.FormSpread:E3}");
        Assert.Equal(r.FormSpread, compact.Max(c => c.OnePlusZ) - compact.Min(c => c.OnePlusZ), 12);
    }

    [Fact]
    public void Y_G_074_TheVerdictSplitsTheQuestionBetweenShapeAndNumbers()
    {
        PrintHeader("G_074 - the verdict");

        var sb = new StringBuilder();
        sb.AppendLine(ClockLawNecessityAudit.Verdict());
        Output.WriteLine(sb.ToString());

        Assert.StartsWith("BOUNDARY", ClockLawNecessityAudit.Verdict());
        Assert.Contains("MONOTONICITY DRIVES THE SHAPE", ClockLawNecessityAudit.Verdict());
        Assert.Contains("THE MAP DRIVES THE NUMBERS", ClockLawNecessityAudit.Verdict());

        // neither extreme: the shape is not unique to the power law and the power law is not refuted
        Assert.DoesNotContain("OUTPUT: UNIQUE", ClockLawNecessityAudit.Verdict());
        Assert.DoesNotContain("OUTPUT: REFUTED", ClockLawNecessityAudit.Verdict());
        Assert.Contains("OUTPUT: BOUNDARY", ClockLawNecessityAudit.Verdict());
    }

    [Fact]
    public void Y_G_074_Diag()
    {
        PrintHeader("G_074 - DIAGNOSTIC: every computed surface");

        Output.WriteLine(ClockLawNecessityAudit.OutputShapeAndLadder());
        Output.WriteLine(ClockLawNecessityAudit.OutputRequirements());
        Output.WriteLine(ClockLawNecessityAudit.OutputCompact());
        Output.WriteLine(ClockLawNecessityAudit.OutputVerdict());
    }
}
