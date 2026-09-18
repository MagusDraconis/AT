using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_077 - Clock Source Audit. Starting only from the surviving AT primitives (Difference, Actualization,
/// the density, causal structure, count conservation, the G_035 temporal sector), what produces LOCAL clock-rate
/// changes - and can the source be operated independently of gravity? The GR field equations, a specific clock law,
/// multiplicativity, neutron-star fitting and any aether reading are not used.
/// </summary>
public sealed class Y_G_077_Tests : ResearchTestBase
{
    public Y_G_077_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    [Fact]
    public void Y_G_077_TheClockIsAReadoutOfOneFieldAndAddsNoField()
    {
        PrintHeader("G_077 - the minimal object: one scalar, and the clock as a readout of it");

        var fields = ClockSourceAudit.FieldCount()[0];
        var sb = new StringBuilder();
        sb.AppendLine($"  temporal observables {fields.Observations}, taking the occupancy {fields.TakingTheOccupancy}, the potential {fields.TakingThePotential}, anything else {fields.TakingAnythingElse}");
        sb.AppendLine($"  the sector's content (metric functions, scalar fields, exponents): {TemporalIndependenceAudit.MinimalTimeSectorContent()}");
        sb.AppendLine("  response ranks:");
        sb.AppendLine("  observable                     sites   rank   directions missed");
        foreach (var r in ClockSourceAudit.ResponseRanks())
            sb.AppendLine($"  {r.Observable,-30} {r.Sites,5} {r.Rank,6} {r.DirectionsMissed,18}");
        Output.WriteLine(sb.ToString());

        // NO observable takes anything beyond the occupancy or its derived potential - G_035's arity proof, re-run in
        // the form this audit needs: a second driver would have no argument to enter through
        Assert.Equal(0, fields.TakingAnythingElse);
        Assert.Equal(fields.Observations, fields.TakingTheOccupancy + fields.TakingThePotential);
        Assert.Equal((1, 1, 1), TemporalIndependenceAudit.MinimalTimeSectorContent());

        // THE LOCAL READOUT: two configurations agreeing at the probe and differing elsewhere give the same rate there
        Assert.All(ClockSourceAudit.LocalReadout(), r => Assert.Equal(0.0, r.Residual, 12));

        // AND THE DECISIVE RANK MEASUREMENT: the clock reads EVERY direction of the occupancy while the acceleration is
        // blind to exactly the uniform one - and the uniform direction is the one count conservation forbids
        var ranks = ClockSourceAudit.ResponseRanks();
        int sites = ranks[0].Sites;
        Assert.Equal(sites, ranks.Single(r => r.Observable.StartsWith("clock")).Rank);
        Assert.Equal(sites - 1, ranks.Single(r => r.Observable.StartsWith("acceleration")).Rank);
        Assert.Equal(0, ranks.Single(r => r.Observable.StartsWith("clock")).DirectionsMissed);
        Assert.Equal(1, ranks.Single(r => r.Observable.StartsWith("acceleration")).DirectionsMissed);

        var uniform = ClockSourceAudit.UniformDirection();
        Assert.True(uniform.Single(u => u.Quantity.StartsWith("clock")).Change > 1e-6);
        Assert.True(uniform.Single(u => u.Quantity.StartsWith("acceleration")).Change < 1e-14);
        Assert.True(uniform.Single(u => u.Quantity.StartsWith("every pairwise")).Change < 1e-14);
        Assert.True(uniform.Single(u => u.Quantity.StartsWith("the total")).Change > 1e-6,
            "the uniform direction must change the TOTAL count, which is what makes count conservation remove it");
    }

    [Fact]
    public void Y_G_077_TheDriverIsTheDensityAndEveryAlternativeIsMeasured()
    {
        PrintHeader("G_077 - which driver actually moves the clock");

        var sb = new StringBuilder();
        sb.AppendLine("  the actualization rate as a second driver - inverted rather than asserted:");
        sb.AppendLine($"    largest reconstruction residual {ClockSourceAudit.ActualizationIsTheSameInformation().Max(r => r.Residual):E3}");
        sb.AppendLine();
        sb.AppendLine("  connectivity: two graphs, the same occupancy");
        foreach (var c in ClockSourceAudit.ConnectivityTest())
            sb.AppendLine($"    {c.Connectivity,-32} clock {c.ClockAtProbe:F12}   acceleration {c.AccelerationAtProbe:E3}");
        sb.AppendLine();
        sb.AppendLine("  information density: same local occupancy, different entropy");
        foreach (var c in ClockSourceAudit.InformationDensityTest())
            sb.AppendLine($"    {c.Configuration,-32} rho {c.LocalOccupancy:F6}   entropy {c.Entropy:F6}   clock {c.ClockAtProbe:F12}");
        Output.WriteLine(sb.ToString());

        // the actualization rate carries the SAME information as the occupancy: rho is recovered from the rate exactly
        Assert.All(ClockSourceAudit.ActualizationIsTheSameInformation(), r => Assert.True(r.Residual < 1e-6,
            $"{r.Law}: rho must be recoverable from the rate, residual {r.Residual:E3}"));

        // connectivity is not a driver: the same occupancy on two different graphs reads the same
        var graphs = ClockSourceAudit.ConnectivityTest();
        Assert.Equal(graphs[0].ClockAtProbe, graphs[1].ClockAtProbe, 12);
        Assert.Equal(graphs[0].AccelerationAtProbe, graphs[1].AccelerationAtProbe, 12);

        // information density is not a driver: the entropy changes by a large fraction while the local clock does not
        var info = ClockSourceAudit.InformationDensityTest();
        Assert.Equal(info[0].LocalOccupancy, info[1].LocalOccupancy, 12);
        Assert.Equal(info[0].ClockAtProbe, info[1].ClockAtProbe, 12);
        Assert.True(Math.Abs(info[0].Entropy - info[1].Entropy) > 0.5,
            $"the entropies must differ substantially, got {info[0].Entropy:F6} against {info[1].Entropy:F6}");
    }

    [Fact]
    public void Y_G_077_TheRedshiftIsARatioOfRatesForEveryLaw()
    {
        PrintHeader("G_077 - clock change to redshift, with no clock law assumed");

        var corpus = ClockSourceAudit.RedshiftCorpus();
        var sb = new StringBuilder();
        sb.AppendLine($"  the ratio form over {corpus.Length} law/pair combinations, every law carrying the pinned data: {corpus.All(r => r.PinnedAtZero)}");
        sb.AppendLine();
        sb.AppendLine("  the local response is the logarithmic derivative, at two steps:");
        sb.AppendLine("  law                                 g'(A)         residual (h = 1e-4)   residual (h = 1e-5)   fall");
        foreach (var r in ClockSourceAudit.LocalResponse())
            sb.AppendLine($"  {r.Law,-35} {r.LogDerivative,-13:F6} {r.ResidualCoarse,-23:E3} {r.ResidualFine,-21:E3} {r.Ratio:F2}");
        Output.WriteLine(sb.ToString());

        // the corpus carries the exponential as ONE member among others, and non-analytic members too
        Assert.True(corpus.Length >= 30);
        Assert.Contains(corpus, r => r.Law.StartsWith("mesh interpolant"));
        Assert.Contains(corpus, r => r.Law == "exp(2x)");
        Assert.All(corpus, r => Assert.True(r.PinnedAtZero, $"{r.Law} must carry the pinned data"));

        // the redshift is a ratio of rates: measured by reproducing it independently for each entry
        var law = ClockSourceAudit.Laws().First(l => l.Name == "1/(1-2x)");
        double direct = law.W(-0.20) / law.W(-0.05);
        Assert.Equal(direct, ClockSourceAudit.OnePlusZ(law, -0.20, -0.05), 12);

        // A LOCAL MEASUREMENT FIXES ONLY THE LOGARITHMIC DERIVATIVE: the residual of the first-order form either FALLS
        // in proportion to the step - a truncation, which is what the stiff non-analytic member shows - or it is
        // already at the round-off floor, which is what the exponential shows. Both outcomes are the same statement,
        // and the test asserts the disjunction rather than a tolerance.
        Assert.All(ClockSourceAudit.LocalResponse(), r => Assert.True(
            (r.Ratio > 5.0 && r.Ratio < 20.0) || r.ResidualFine < 1e-10,
            $"{r.Law}: the residual must fall with the step or sit at the round-off floor - ratio {r.Ratio:F2}, "
            + $"fine residual {r.ResidualFine:E3}"));
        // ...and the derivative itself is law-dependent, which is what the observable measures
        var derivatives = ClockSourceAudit.LocalResponse().Select(r => r.LogDerivative).ToArray();
        Assert.True(derivatives.Max() - derivatives.Min() > 0.05,
            "the logarithmic derivative must differ across the corpus, or the observable would measure nothing");
    }

    [Fact]
    public void Y_G_077_SuccessiveRedshiftFactorsTelescopeAndThatIsNotAConstraint()
    {
        PrintHeader("G_077 - why composing redshifts is empty and composing potentials is not");

        var telescoping = ClockSourceAudit.Telescoping();
        var sb = new StringBuilder();
        sb.AppendLine("  law                                  product       direct        residual");
        foreach (var t in telescoping)
            sb.AppendLine($"  {t.Law,-36} {t.Product,-13:F12} {t.Direct,-13:F12} {t.Residual:E3}");
        Output.WriteLine(sb.ToString());

        // the product of the per-step factors IS the total factor, for EVERY law - so no law is selected by it
        Assert.All(telescoping, t => Assert.True(t.Residual < 1e-12,
            $"{t.Law}: the factors must telescope, residual {t.Residual:E3}"));
        Assert.True(telescoping.Length >= 10);
    }

    [Fact]
    public void Y_G_077_TwoReadingsOfOnePotentialDifferenceShareTheirZeroSet()
    {
        PrintHeader("G_077 - the decisive measurement: one potential difference, two readings");

        var agreement = ClockSourceAudit.ZeroSetAgreement();
        var sb = new StringBuilder();
        sb.AppendLine("  configuration                                     law                     clock offset   accel. integral");
        foreach (var r in ClockSourceAudit.TwoReadingsOfOneDifference().Take(16))
            sb.AppendLine($"  {r.Configuration,-49} {r.Law,-23} {r.ClockOffset,-14:E3} {r.AccelerationIntegral:E3}");
        sb.AppendLine();
        sb.AppendLine($"  every law's zero set agrees: {agreement.All(a => a.Agrees)} over {agreement.Length} laws");
        Output.WriteLine(sb.ToString());

        // F-FREE: the zero-set agreement holds for EVERY law in the corpus, the exponential being one member
        Assert.All(agreement, a => Assert.True(a.Agrees, $"{a.Law}: the two readings must vanish together"));
        Assert.Equal(ClockSourceAudit.Laws().Length, agreement.Length);

        // and the degenerate direction is exactly the one neither observable can read
        var vacuum = ClockSourceAudit.TwoReadingsOfOneDifference()
            .Where(r => r.Configuration.StartsWith("vacuum")).ToArray();
        Assert.All(vacuum, r => Assert.Equal(0.0, r.ClockOffset, 14));
        Assert.All(vacuum, r => Assert.Equal(0.0, r.AccelerationIntegral, 14));

        // WHAT IS NOT FIXED IS THE PRICE: the ratio of the two readings is law-dependent
        var spread = ClockSourceAudit.RatioSpread();
        Assert.All(spread, s => Assert.True(s.Spread > 0.01,
            $"{s.Configuration}: the ratio must be law-dependent, spread {s.Spread:E3}"));
    }

    [Fact]
    public void Y_G_077_TheClockIsManipulableOnlyThroughTheDensity()
    {
        PrintHeader("G_077 - passive observable or manipulable state?");

        var tests = ClockSourceAudit.Manipulability();
        foreach (var t in tests)
        {
            Output.WriteLine($"  [{(t.Result ? "YES" : "NO ")}] {t.Test}");
            Output.WriteLine($"         {t.Measurement}");
        }

        // the clock CAN be changed, by the occupancy
        Assert.True(tests.Single(t => t.Test.Contains("LOCAL OCCUPANCY changes")).Result,
            "a clock change must be sourceable at all, or the audit would conclude that time control is impossible");
        // the change is LOCAL: nothing happens at sites whose occupancy did not move
        Assert.False(tests.Single(t => t.Test.Contains("did NOT change")).Result);
        // the same perturbation moves the acceleration profile
        Assert.True(tests.Single(t => t.Test.Contains("MOVES the acceleration profile")).Result);
        // and there is NO knob other than the occupancy
        Assert.False(tests.Single(t => t.Test.Contains("NO occupancy change")).Result);
    }

    [Fact]
    public void Y_G_077_TheSourceTermIsSharedWithTheAcceleration()
    {
        PrintHeader("G_077 - the required source term, and count conservation");

        var census = ClockSourceAudit.SourceTermCensus();
        var conservation = ClockSourceAudit.CountConservation();
        var sb = new StringBuilder();
        sb.AppendLine("  the source term census:");
        foreach (var c in census)
            sb.AppendLine($"    {c.Quantity,-25} | {c.Role}");
        sb.AppendLine();
        sb.AppendLine("  count conservation - a transport keeps the total and moves BOTH readings:");
        foreach (var c in conservation)
            sb.AppendLine($"    {c.Step,-49} total {c.TotalOccupancy:F6}   clock {c.ClockAtProbe:F9}   accel {c.AccelerationAtSource:E3}");
        Output.WriteLine(sb.ToString());

        // ONE field, TWO readouts: the census says so in its own rows, and the audit asserts the count
        Assert.Equal(4, census.Length);
        Assert.Contains(census, c => c.Quantity == "rho, the occupancy" && c.Role.Contains("ONLY field"));
        Assert.Contains(census, c => c.Quantity.Contains("clock rate") && c.Role.Contains("READOUT"));
        Assert.Contains(census, c => c.Quantity.Contains("acceleration") && c.Role.Contains("other READOUT"));

        // the transport conserves the total, moves the clock, and moves the acceleration at the source
        Assert.All(conservation, c => Assert.Equal(7.0, c.TotalOccupancy, 10));
        Assert.True(conservation.Zip(conservation.Skip(1), (a, b) => b.ClockAtProbe - a.ClockAtProbe).All(d => d > 0.0),
            "the clock at the probe must rise as occupancy is transported in");
        Assert.True(conservation.Zip(conservation.Skip(1), (a, b) => Math.Abs(b.AccelerationAtSource) - Math.Abs(a.AccelerationAtSource)).All(d => d > 0.0),
            "the acceleration at the source must rise with the same transport");
    }

    [Fact]
    public void Y_G_077_TheBoundarySplitsAndTheVerdictRefusesToCollapseIt()
    {
        PrintHeader("G_077 - the boundary: confine or remove?");

        var trade = ClockSourceAudit.TransitionWidthTrade();
        var boundary = ClockSourceAudit.BoundaryAnalysis();
        var shell = ClockSourceAudit.ShellAnalysis();
        var sb = new StringBuilder();
        sb.AppendLine($"  the shell: interior acceleration {shell.InteriorAcceleration:E3}, wall link gradient "
                    + $"{shell.WallLinkGradient:E3}, clock offset {shell.ClockOffset:E3}");
        sb.AppendLine("  the width-versus-strength trade (the gradient the LATTICE link carries):");
        sb.AppendLine("  wall cells   peak gradient     integral      clock offset");
        foreach (var t in trade)
            sb.AppendLine($"  {t.WallCells,10} {t.PeakGradient,16:E3} {t.Integral,13:E3} {t.ClockOffset:E3}");
        sb.AppendLine();
        foreach (var b in boundary)
        {
            sb.AppendLine($"  [{(b.Answer ? "YES" : "NO ")}] {b.Question}");
            sb.AppendLine($"         {b.Measurement}");
        }
        Output.WriteLine(sb.ToString());

        // THE INTEGRAL IS FIXED AND THE SUPPORT IS FREE: the peak FALLS as the wall widens, so narrowing the transition
        // raises the peak in proportion - the gradient can be confined but not removed
        Assert.All(trade, t => Assert.Equal(trade[0].Integral, t.Integral, 10));
        Assert.True(trade.Zip(trade.Skip(1), (a, b) => b.PeakGradient - a.PeakGradient).All(d => d < 0.0),
            "widening the wall must LOWER the peak gradient, i.e. narrowing it raises the peak");

        // the boundary answers split, which is the audit's honest form of the answer
        Assert.True(boundary.Single(b => b.Question.Contains("NO acceleration inside")).Answer,
            "a clock offset with no LOCAL acceleration must be permitted, or the audit would be overstating");
        Assert.True(boundary.Single(b => b.Question.Contains("if the total count may change")).Answer,
            "the uniform direction must be shown to exist, with its count cost, before count conservation can remove it");
        Assert.False(boundary.Single(b => b.Question.Contains("under COUNT CONSERVATION")).Answer);
        Assert.False(boundary.Single(b => b.Question.Contains("on ANY path")).Answer);

        // ...and the verdict is the refutation, with the two-level statement inside it
        string verdict = ClockSourceAudit.Verdict();
        Output.WriteLine(verdict);
        Assert.StartsWith("REFUTED", verdict);
        Assert.Contains("NO INDEPENDENT CLOCK SOURCE EXISTS", verdict);
        Assert.Contains("TIME CONTROL IS GRAVITY CONTROL", verdict);
        Assert.Contains("OUTPUT: REFUTED", verdict);
        Assert.DoesNotContain("OUTPUT: BOUNDARY", verdict);
    }

    [Fact]
    public void Y_G_077_Diag()
    {
        PrintHeader("G_077 - DIAGNOSTIC: every computed surface");

        Output.WriteLine(ClockSourceAudit.OutputMinimal());
        Output.WriteLine(ClockSourceAudit.OutputRedshift());
        Output.WriteLine(ClockSourceAudit.OutputTwoReadings());
        Output.WriteLine(ClockSourceAudit.OutputBoundary());
        Output.WriteLine(ClockSourceAudit.OutputVerdict());
    }
}
