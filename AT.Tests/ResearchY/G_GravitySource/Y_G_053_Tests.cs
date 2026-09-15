using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.PhaseSectorDynamicsAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_053 - Phase Sector Dynamics Audit (group G - Gravity Source).
///
/// QUESTION. Do phase modes have INDEPENDENT physical effects beyond amplitudes? Construct a pure amplitude
/// perturbation and a pure phase perturbation, measure the clock, acceleration and field responses, and determine which
/// observables are uniquely phase-sensitive.
///
/// ANSWER: **DERIVED - the phase effect is not reproducible by any amplitude move, the phase sector ROTATES the
/// Fourier content where the amplitude sector RESIZES it, and the first uniquely phase-sensitive observable is the
/// quadrature functional.**
/// </summary>
public class Y_G_053_Tests : ResearchTestBase
{
    public Y_G_053_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_053_TheTwoPerturbationsAreOrthogonalAndBothReal()
    {
        Assert.Equal(42, AmplitudeBasis().Length);
        Assert.Equal(53, PhaseBasis().Length);
        Assert.True(ThePerturbationsAreOrthogonal(), $"overlap {PerturbationOverlap():E3}");
        Assert.True(BothPerturbationsAreReal());
        Assert.All(ResponseTable(), r => Assert.True(r.Amplitude > 1e-6 && r.Phase > 1e-6,
            $"{r.Reading}: amplitude {r.Amplitude:E3}, phase {r.Phase:E3}"));
    }

    [Fact]
    public void Y_G_053_NoAmplitudeMoveReproducesThePhaseEffect()
    {
        // the amplitude-response span is built from all 42 directions before the projection is believed
        Assert.Equal(AmplitudeBasis().Length, AmplitudeResponseSpanRank());
        Assert.True(ClockNonReproducible() > 0.5, $"clock non-reproducible {ClockNonReproducible():F4}");
        Assert.True(AccelerationNonReproducible() > 0.5, $"acceleration non-reproducible {AccelerationNonReproducible():F4}");
        Assert.True(FieldNonReproducible() > 0.5, $"field non-reproducible {FieldNonReproducible():F4}");
        Assert.True(ThePhaseEffectIsNotReproducible());
    }

    [Fact]
    public void Y_G_053_ThePhaseSectorRotatesWhatTheAmplitudeSectorResizes()
    {
        var a = AmplitudeEffect();
        var p = PhaseEffect();
        // the sharpest statement the measurement supports: an amplitude move NEVER rotates the channel
        Assert.True(a.AngleChange < 1e-12, $"the amplitude move rotated the channel by {a.AngleChange:E3}");
        Assert.True(p.AngleChange > 1e-3, $"the phase move changed the angle by {p.AngleChange:E3}");
        Assert.True(a.MagnitudeChange > 5.0 * p.MagnitudeChange,
            $"amplitude magnitude {a.MagnitudeChange:E3} vs phase {p.MagnitudeChange:E3}");
        Assert.True(AmplitudeResizesAndPhaseRotates(), TheGeometry());
    }

    [Fact]
    public void Y_G_053_TheQuadratureFunctionalIsUniquelyPhaseSensitive()
    {
        // non-zero on the phase move, exactly zero on every one of the 42 amplitude moves
        Assert.True(QuadraturePhaseResponse() > 1e-6, $"phase response {QuadraturePhaseResponse():E3}");
        Assert.True(QuadratureAmplitudeResponse() < 1e-14, $"amplitude response {QuadratureAmplitudeResponse():E3}");
        Assert.True(TheQuadratureIsUniquelyPhaseSensitive());
        Assert.Contains("quadrature functional", TheFirstPhaseObservable());
    }

    [Fact]
    public void Y_G_053_ATsOwnLawsAreMixedNotUniquelyPhaseSensitive()
    {
        // each of the three readings takes from BOTH sectors - so the uniquely phase-sensitive observable is the
        // quadrature functional rather than one of the theory's existing laws
        Assert.Equal(3, MixedReadings().Length);
        Assert.Contains("clock", MixedReadings());
        Assert.Contains("acceleration", MixedReadings());
        Assert.Contains("field strength", MixedReadings());
        Assert.Equal("DERIVED", Verdict());
    }

    [Fact]
    public void Y_G_053_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_053 - Phase Sector Dynamics Audit: do phase modes have independent effects beyond amplitudes?");

        sb.AppendLine("QUESTION. Do phase modes have INDEPENDENT physical effects beyond amplitudes?");
        sb.AppendLine("GIVEN        G_050 (the phase sector is the hidden Fourier modes), G_052 (the split is an identity)");
        sb.AppendLine("CONSTRUCT    pure amplitude perturbation | pure phase perturbation");
        sb.AppendLine("MEASURE      clock response | acceleration response | field response");
        sb.AppendLine("GOAL         the first observable that distinguishes amplitude from phase");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A pure amplitude perturbation is a unit direction in the visible-mode span and a pure phase");
        sb.AppendLine("     perturbation one in the hidden-mode span; G_052's identity makes them exact complements.");
        sb.AppendLine("  2. Since both perturbations move every reading, the question is whether the phase effect can be");
        sb.AppendLine("     MIMICKED: independence is tested by projecting the phase response onto the span of the responses");
        sb.AppendLine("     to all amplitude directions and measuring the residual.");
        sb.AppendLine("  3. The geometric reading is taken in a channel's two-quadrature plane, where a resize and a rotation");
        sb.AppendLine("     are different operations.");
        sb.AppendLine("  4. A uniquely phase-sensitive observable is one that is zero on every amplitude move and non-zero on");
        sb.AppendLine("     the phase move.");
        sb.AppendLine("  5. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputPerturbations());
        PrintHeader(OutputIndependence());
        PrintHeader(OutputPhaseObservable());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
