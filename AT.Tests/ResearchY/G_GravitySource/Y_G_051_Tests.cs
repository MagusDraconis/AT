using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.PhaseSectorAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_051 - Phase Sector Audit (group G - Gravity Source).
///
/// QUESTION. Do the 53 phase-sector directions carry PHYSICAL information or GAUGE information? Measure clock,
/// acceleration, field and flux-sector responses; separate observable phase directions from pure gauge ones.
///
/// ANSWER: **PHYSICAL - all 53 directions are observable and none is gauge; the flux sector's silence is a DECOUPLING
/// rather than a gauge signature, and the audit excludes it from the criterion on purpose.**
/// </summary>
public class Y_G_051_Tests : ResearchTestBase
{
    public Y_G_051_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_051_EveryPhaseDirectionChangesAMeasurableReading()
    {
        Assert.Equal(53, PhaseDimension());
        Assert.Equal(PhaseDimension(), DirectionTable().Length);
        Assert.True(EveryPhaseDirectionIsObservable(),
            $"smallest clock {MinimumClockResponse():E3}, acceleration {MinimumAccelerationResponse():E3}, " +
            $"field {MinimumFieldResponse():E3}");
        Assert.All(DirectionTable(), r => Assert.True(r.Clock > 1e-9, $"channel {r.Channel} {r.Kind}"));
    }

    [Fact]
    public void Y_G_051_TheResponsesAreFirstOrderNotSecond()
    {
        // a second-order response would be invisible to linear response - nearly gauge
        Assert.True(EveryDirectionIsFirstOrder(), "every direction must scale with the step");
        Assert.All(DirectionTable(), r => Assert.True(Math.Abs(r.Scaling - 0.5) < 0.05,
            $"channel {r.Channel} {r.Kind} scales as {r.Scaling:F4}"));
    }

    [Fact]
    public void Y_G_051_TheTestRecognisesAnActualGaugeDirection()
    {
        // the control: a symmetry-orbit move leaves every multiset untouched
        Assert.True(TheTestRecognisesGaugeDirections(), $"gauge response {GaugeDirectionResponse():E3}");
        Assert.True(GaugeDirectionResponse() < 1e-12);
    }

    [Fact]
    public void Y_G_051_NoPhaseDirectionIsGauge()
    {
        Assert.False(AnyPhaseDirectionIsGauge());
        Assert.Equal(PhaseDimension(), PhysicalDirections().Length);
        Assert.Empty(GaugeDirections());
        Assert.Equal("PHYSICAL", Verdict());
    }

    [Fact]
    public void Y_G_051_TheFluxSilenceIsADecouplingNotGaugeNess()
    {
        // the label reads the link phases, and nothing couples them to the organisation
        Assert.Equal(PhaseDimension(), FluxSilentDirections());
        Assert.Equal(0, CouplingCensus());
        Assert.Equal(0.0, FluxResponse(PhaseDirections()[0].Mode));
        Assert.NotEqual("GAUGE", Verdict());       // the decoupled probe does not decide the verdict
    }

    [Fact]
    public void Y_G_051_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_051 - Phase Sector Audit: do the 53 phase directions carry physical or gauge information?");

        sb.AppendLine("QUESTION. Do the 53 phase-sector directions carry PHYSICAL or GAUGE information?");
        sb.AppendLine("GIVEN        G_050: the kernel is the phase sector (47 quadratures + 5 empty channels x 2 + 1)");
        sb.AppendLine("MEASURE      clock response | acceleration response | field response | flux-sector response");
        sb.AppendLine("GOAL         identify the physical content of the phase sector");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A response is read as the change of a reading's MULTISET - what a law reports about the system,");
        sb.AppendLine("     not which cell carries what - which is the distinction G_048 used to separate relabelling from");
        sb.AppendLine("     change.");
        sb.AppendLine("  2. A direction is GAUGE when the readings that CAN see it report nothing; a decoupled probe's silence");
        sb.AppendLine("     is not evidence of gauge-ness, and the flux sector is exactly such a probe.");
        sb.AppendLine("  3. First-order versus second-order is decided by halving the step: a first-order response halves.");
        sb.AppendLine("  4. The control is the substrate-symmetry orbit, whose moves are gauge by construction.");
        sb.AppendLine("  5. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputResponses());
        PrintHeader(OutputGauge());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
