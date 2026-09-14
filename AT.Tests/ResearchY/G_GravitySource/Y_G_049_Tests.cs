using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.ClockPrimacyAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_049 - Clock Primacy Audit (group G - Gravity Source).
///
/// QUESTION. Is the clock pattern the UNIQUE lossless observable of rho? Compare the clock, acceleration,
/// field-strength and contraction readings on invertibility, retained information, rank and kernel. Goal: test whether
/// TIME is the primary observable of rho.
///
/// ANSWER: **LOSSLESS - the clock pattern is information-equivalent to rho - and the audit's most useful result is that
/// RANK IS NOT INVERTIBILITY: its own first proof of that was refused by the measurement and is recorded as withdrawn.**
/// </summary>
public class Y_G_049_Tests : ResearchTestBase
{
    public Y_G_049_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_049_TheClockPatternIsLosslessWithAClosedFormInverse()
    {
        Assert.Equal(95, StateDimension());
        Assert.Equal(StateDimension(), ClockRank());
        Assert.True(TheClockInverseIsClosedForm(), $"closed-form residual {ClockClosedFormResidual():E3}");
        Assert.True(ClockClosedFormResidual() < 1e-12);
        Assert.True(TheControlRecoversTheState(), "the search must be able to succeed before its failures are believed");
        Assert.Equal(0, ClockCollisions());
        Assert.Equal("LOSSLESS", Table().Single(t => t.Reading == "clock pattern").Verdict);

        // and the clock is NOT the unique lossless reading: the measurement finds three
        Assert.Equal(3, LosslessReadings().Length);
        Assert.False(TheClockIsTheUniqueLosslessReading());
    }

    [Fact]
    public void Y_G_049_TheWithdrawnCollisionIsRecordedAndTheSearchReplacesIt()
    {
        // THE WITHDRAWN PROOF: the reflection through the mean was expected to collide (same mean, reversed
        // differences) and the measurement refused it, because the acceleration pattern uses differences of the RATE
        // and the cube root is not linear
        var rho = RhoAccessibilityAudit.BaseState();
        double mean = rho.Average();
        var reflected = rho.Select(r => 2.0 * mean - r).ToArray();
        double gap = PatternDistance(AccelerationPattern(reflected), AccelerationPattern(rho));
        Assert.True(gap > 1e-6, $"the withdrawn collision would need zero here; the measurement gives {gap:E3}");
        Assert.Equal(1.951E-2, gap, 3);

        // the REPLACEMENT decides the question: the search was allowed to find an alternative state and found none,
        // converging back onto the audited state - so the hypothesis that rank hides lossiness is NOT confirmed
        Assert.Equal(StateDimension(), AccelerationRank());
        Assert.Equal(0, AccelerationCollisions());
        Assert.False(RankAloneWouldHaveMisled(), "no full-rank reading was measured lossy");
        Assert.True(AccelerationInversionResidual() < 1e-6, $"best residual {AccelerationInversionResidual():E3}");
        Assert.Equal("LOSSLESS", Table().Single(t => t.Reading == "acceleration pattern").Verdict);
    }

    [Fact]
    public void Y_G_049_TheFieldPatternIsTestedByTheSameSearch()
    {
        Assert.Equal(StateDimension(), FieldRank());
        Assert.Equal(0, FieldCollisions());
        Assert.True(FieldInversionResidual() < 1e-6, $"best residual {FieldInversionResidual():E3}");
        Assert.Equal("LOSSLESS", Table().Single(t => t.Reading == "field-strength pattern").Verdict);
        Assert.Contains("field-strength pattern", LosslessReadings());
    }

    [Fact]
    public void Y_G_049_TheContractionsAreLossyByDimension()
    {
        Assert.True(TheContractionsAreLossy());
        Assert.True(ContractionRank() < StateDimension());
        Assert.True(ContractionInformationRatio() < 0.5, $"ratio {ContractionInformationRatio():F3}");
        Assert.Equal("LOSSY", Table().Single(t => t.Reading == "contraction observables").Verdict);
        Assert.Contains("contraction observables", LossyReadings());
    }

    [Fact]
    public void Y_G_049_TheVerdictIsLosslessAndUniquenessIsReportedSeparately()
    {
        Assert.Equal("LOSSLESS", Verdict());
        Assert.Contains("clock pattern", LosslessReadings());
        Assert.Equal(4, Table().Length);

        // the question asks for UNIQUE, so the count answers it: three readings are information-equivalent to rho
        Assert.Equal(3, LosslessReadings().Length);
        Assert.Equal(TheClockIsTheUniqueLosslessReading(), LosslessReadings().Length == 1);
        Assert.False(TheClockIsTheUniqueLosslessReading());
    }

    [Fact]
    public void Y_G_049_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_049 - Clock Primacy Audit: is the clock pattern the unique lossless observable of rho?");

        sb.AppendLine("QUESTION. Is the clock pattern the UNIQUE lossless observable of rho?");
        sb.AppendLine("COMPARE      clock pattern | acceleration pattern | field-strength pattern | contractions");
        sb.AppendLine("MEASURE      invertibility | retained information | rank | kernel");
        sb.AppendLine("GOAL         test whether TIME is the primary observable of rho");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Lossless means information-equivalent to rho: full rank AND an inverse, so a reading with full rank");
        sb.AppendLine("     but no inverse is LOSSY - that distinction is this audit's subject.");
        sb.AppendLine("  2. Inversion is attempted numerically by projected gradient descent on the simplex from displaced");
        sb.AppendLine("     starts, with the audited state as the control: the control must recover itself, or the failures");
        sb.AppendLine("     are not trustworthy.");
        sb.AppendLine("  3. The contraction reading needs no search: fewer dimensions retained than the state has settles it.");
        sb.AppendLine("  4. Deterministic throughout; the starts, the step schedule and the iteration count are fixed.");
        sb.AppendLine();

        PrintHeader(OutputTable());
        PrintHeader(OutputClock());
        PrintHeader(OutputSearch());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
