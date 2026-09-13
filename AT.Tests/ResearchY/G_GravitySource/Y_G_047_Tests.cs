using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.KernelObservableAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_047 - Kernel Observable Audit (group G - Gravity Source).
///
/// QUESTION. Which observable detects the 47 kernel directions DIRECTLY? Given G_040 (95 = 48 + 47) and G_046 (the
/// hidden set is the kernel; a validated hidden step moves the local laws while changing no contraction).
/// Requirements: responds to hidden directions, distinguishes kernel states, independent of the contractions.
///
/// ANSWER: **DERIVED - the first observable is AT's own clock law read at each cell, and the minimal basis is one
/// reading per kernel dimension.** The contractions stay at 5.116E-013 while the clock pattern moves by 3.764E-003,
/// the reading map has full rank on the kernel, and the basis is sized to the MEASURED kernel (53 at the audited
/// state) rather than to G_040's ceiling of 47.
/// </summary>
public class Y_G_047_Tests : ResearchTestBase
{
    public Y_G_047_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_047_TheKernelIsBuiltAndEveryDirectionChangesNoContraction()
    {
        Assert.True(KernelDimension() > 0, $"kernel dimension {KernelDimension()}");
        Assert.Equal(Cells - ContractionRank() - 1, KernelDimension());
        Assert.True(LargestContractionChange() < 1e-9,
            $"contraction change along a kernel direction {LargestContractionChange():E3}");
        Assert.Equal(KernelDimension(), KernelBasis().Length);
    }

    [Fact]
    public void Y_G_047_TheClockRespondsToHiddenDirections()
    {
        Assert.True(LargestClockResponse() > 1e-4, $"clock response {LargestClockResponse():E3}");
        Assert.True(LargestAccelerationResponse() > 1e-4, $"acceleration response {LargestAccelerationResponse():E3}");
        Assert.True(LargestFieldResponse() > 1e-5, $"field response {LargestFieldResponse():E3}");
        Assert.True(TheClockRespondsToHiddenDirections());
    }

    [Fact]
    public void Y_G_047_TheClockResolvesTheKernelWithFullRank()
    {
        Assert.True(TheClockResolvesTheKernel(),
            $"rank {ClockReadingRank()} against kernel dimension {KernelDimension()}");
        Assert.Equal(KernelDimension(), ClockReadingRank());
        Assert.True(TheAccelerationResolvesTheKernel(),
            $"acceleration rank {AccelerationReadingRank()}");
    }

    [Fact]
    public void Y_G_047_TheClockDistinguishesKernelStates()
    {
        Assert.True(TheClockDistinguishesKernelStates());
        Assert.True(DistinguishingResidual() > 1e-6, $"separation {DistinguishingResidual():E3}");
    }

    [Fact]
    public void Y_G_047_ItIsIndependentOfTheContractionObservables()
    {
        // measured both ways: the contractions do not move while the clock does, and the clock resolves more than they retain
        Assert.True(IndependentOfContractionObservables());
        Assert.True(LargestContractionChange() < 1e-9);
        Assert.True(LargestClockResponse() > 1e-4);
        Assert.True(RhoAccessibilityAudit.MeasuredRetainedDimension() < ClockReadingRank());
    }

    [Fact]
    public void Y_G_047_TheFirstObservableIsTheClockPatternAndTheVerdictIsDerived()
    {
        Assert.Equal("the addressed clock-rate pattern", FirstObservable());
        Assert.Contains($"{ClockReadingRank()} independent readings", MinimalObservableBasis());
        Assert.Equal(3, RequirementCheck().Length);
        Assert.Equal("DERIVED", Verdict());
    }

    [Fact]
    public void Y_G_047_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_047 - Kernel Observable Audit: which observable detects the kernel directions directly?");

        sb.AppendLine("QUESTION. Which observable detects the hidden kernel directions DIRECTLY?");
        sb.AppendLine("GIVEN        G_040 (95 = 48 contractions + 47 hidden) and G_046 (the hidden set is the kernel)");
        sb.AppendLine("REQUIREMENTS responds to hidden directions | distinguishes kernel states |");
        sb.AppendLine("             independent of contraction observables");
        sb.AppendLine("MEASURE      clock change | acceleration change | field strength change");
        sb.AppendLine("GOAL         find the first observable that resolves the hidden sector");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The kernel is built at the audited state, so the basis is sized to the MEASURED kernel rather than");
        sb.AppendLine("     to G_040's ceiling; both numbers are reported.");
        sb.AppendLine("  2. A reading is the RESPONSE of an AT quantity to a step along a kernel direction, so a reading that");
        sb.AppendLine("     moved while a contraction moved would be a function of the contractions.");
        sb.AppendLine("  3. The reading map's rank on the kernel is the minimal observable basis, by the gradient argument: one");
        sb.AppendLine("     cell reading resolves one direction.");
        sb.AppendLine("  4. Deterministic throughout; the kernel basis uses a fixed trigonometric sequence, no randomness.");
        sb.AppendLine();

        PrintHeader(OutputKernel());
        PrintHeader(OutputReadings());
        PrintHeader(OutputRequirements());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
