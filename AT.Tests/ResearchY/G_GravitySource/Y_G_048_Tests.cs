using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.ClockCompletenessAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_048 - Clock Completeness Audit (group G - Gravity Source).
///
/// QUESTION. Is the clock pattern the MAXIMAL observable of rho? Compare the clock pattern, the acceleration pattern,
/// the field-strength pattern and the contraction observables; measure kernel rank, information retained and observable
/// dimension; decide whether clock > acceleration > contraction or whether the readings are equivalent.
///
/// ANSWER: **MAXIMAL - the clock pattern resolves the whole state and is invertible - but the top rank is TIED with the
/// acceleration pattern, the field-strength pattern is partial, and the contractions are redundant.**
/// </summary>
public class Y_G_048_Tests : ResearchTestBase
{
    public Y_G_048_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_048_TheClockPatternIsCompleteAndInvertible()
    {
        Assert.Equal(95, StateDimension());
        Assert.Equal(TangentBasis().Length, StateDimension());
        Assert.Equal(StateDimension(), ClockDimension());
        Assert.True(TheClockPatternDeterminesTheState(),
            $"invertibility residual {InvertibilityResidual():E3}");
        Assert.True(InvertibilityResidual() < 1e-12);
        Assert.Equal(1.0, InformationRetained(ClockDimension()), 12);
    }

    [Fact]
    public void Y_G_048_TheAccelerationPatternTiesAtTheTop()
    {
        // the difference map loses the global constant on an arbitrary vector, but the simplex already excludes it
        Assert.Equal(StateDimension(), AccelerationDimension());
        Assert.True(TheTopIsTied(), $"maximal readings: {string.Join(", ", MaximalReadings())}");
        Assert.Equal(3, MaximalReadings().Length);
        Assert.Contains("clock pattern", MaximalReadings());
        Assert.Contains("acceleration pattern", MaximalReadings());
    }

    [Fact]
    public void Y_G_048_TheFieldPatternIsMaximalTooAndTheContractionsAreRedundant()
    {
        // the draft expected a shortfall here and the measurement refused it - recorded rather than deleted
        Assert.Equal(StateDimension(), FieldDimension());
        Assert.Contains("field-strength pattern", MaximalReadings());
        Assert.Empty(PartialReadings());

        Assert.Equal(RhoAccessibilityAudit.MeasuredRetainedDimension(), ContractionDimension());
        Assert.True(ClockDimension() > ContractionDimension());
        Assert.Equal(0, ContractionKernelRank());          // the kernel is what the contractions cannot see
    }

    [Fact]
    public void Y_G_048_AddressabilityIsWhatMakesItComplete()
    {
        // the control: the same law, aggregated, is ONE number
        Assert.Equal(1, AggregateDimension());
        Assert.True(AddressabilityIsRequired());
        Assert.Equal(ClockDimension(), MinimalBasisSize());
    }

    [Fact]
    public void Y_G_048_TheKernelRanksSeparateTheReadings()
    {
        // on the hidden kernel the clock and the acceleration both resolve it fully; the contractions resolve nothing
        Assert.Equal(KernelObservableAudit.KernelDimension(), ClockKernelRank());
        Assert.Equal(ClockKernelRank(), AccelerationKernelRank());
        Assert.True(FieldKernelRank() > 0, $"field kernel rank {FieldKernelRank()}");
        Assert.True(FieldKernelRank() <= ClockKernelRank());
        Assert.Equal(0, ContractionKernelRank());
    }

    [Fact]
    public void Y_G_048_TheVerdictIsMaximalAndTheOrderingIsNotAChain()
    {
        Assert.Equal("MAXIMAL", Verdict());
        Assert.Contains($"clock {ClockDimension()} = acceleration {AccelerationDimension()} = field {FieldDimension()}", Ordering());
        Assert.Contains($"out of a state dimension of {StateDimension()}", Ordering());
        Assert.Equal(4, Table().Length);
    }

    [Fact]
    public void Y_G_048_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_048 - Clock Completeness Audit: is the clock pattern the maximal observable of rho?");

        sb.AppendLine("QUESTION. Is the clock pattern the MAXIMAL observable of rho?");
        sb.AppendLine("COMPARE      clock pattern | acceleration pattern | field-strength pattern | contractions");
        sb.AppendLine("MEASURE      kernel rank | information retained | observable dimension");
        sb.AppendLine("GOAL         decide clock > acceleration > contraction, or equivalence");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Observable dimension is the rank of the reading's response map on the 95-dimensional simplex");
        sb.AppendLine("     tangent space, measured by Gram-Schmidt on a deterministic basis.");
        sb.AppendLine("  2. Information retained is that dimension over the state dimension; a reading of full rank must also");
        sb.AppendLine("     pass an explicit inverse test, so that completeness is not a sampling coincidence.");
        sb.AppendLine("  3. The kernel rank is the same quantity restricted to the kernel directions of G_046/G_047, and the");
        sb.AppendLine("     contractions score zero there by construction.");
        sb.AppendLine("  4. Deterministic throughout; the tangent basis uses a fixed trigonometric sequence.");
        sb.AppendLine();

        PrintHeader(OutputTable());
        PrintHeader(OutputCompleteness());
        PrintHeader(OutputOrdering());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
