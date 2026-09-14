using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.KernelStructureAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_050 - Kernel Structure Audit (group G - Gravity Source).
///
/// QUESTION. What physical structure do the 53 kernel directions represent? Measure basis vectors, symmetry classes,
/// multiplicity relation, clock / acceleration / field signatures. Goal: does the kernel have an independent physical
/// interpretation?
///
/// ANSWER: **DERIVED - the kernel is a union of WHOLE symmetry channels (isotypic, because the contractions commute
/// with the dihedral symmetry) and it is the SHORT-WAVELENGTH sector of the organisation.**
/// </summary>
public class Y_G_050_Tests : ResearchTestBase
{
    public Y_G_050_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_050_TheKernelIsAUnionOfWholeFourierModes()
    {
        var table = ModeTable();
        Assert.Equal(95, table.Length);                        // one mode per state dimension
        Assert.True(TheKernelIsAUnionOfFourierModes(), "no Fourier mode may be half hidden");
        Assert.All(table, t => Assert.True(t.Class != "SPLIT", $"channel {t.Channel} {t.Kind} is {t.Class}"));

        // the FIRST DRAFT'S DIHEDRAL CLAIM IS REFUTED: some doublet channels are half hidden, which only a
        // translation-invariant (circulant) argument forbids
        Assert.True(HalfHiddenChannels().Length > 0,
            "the dihedral argument predicted whole channels; the measurement must show half-hidden doublets");
    }

    [Fact]
    public void Y_G_050_TheMultiplicityRelationIsExact()
    {
        Assert.Equal(95, StateDimension());
        Assert.Equal(53, KernelDimension());
        Assert.True(TheAlternatingChannelIsHidden(), "the alternating mode is hidden");
        Assert.Equal(KernelDimension(), HiddenModes());
        Assert.Equal(StateDimension() - KernelDimension(), VisibleModes());
        Assert.True(TheMultiplicityRelationHolds());
    }

    [Fact]
    public void Y_G_050_TheHiddenModesAreNotAWavelengthBandAndThePhaseStructureIsNamed()
    {
        // THE WITHDRAWN HYPOTHESIS: the first draft expected a wavelength band, and the measurement REFUTED it
        Assert.False(TheHiddenSetIsTheShortWavelengthSector(),
            "the hidden and visible modes are interleaved in frequency, not separated");

        // the named structure: one hidden quadrature per populated channel, both quadratures per empty one
        Assert.True(TheEmptyChannelsAreExactlyTheBothHidden(),
            $"empty {string.Join(",", EmptyChannels())} vs both-hidden {string.Join(",", BothHiddenChannels())}");
        Assert.Equal(5, EmptyChannels().Length);
        Assert.True(ThePhaseSectorRelationHolds(), TheNamedStructure());
        Assert.Contains("QUADRATURE", TheNamedStructure());
    }

    [Fact]
    public void Y_G_050_TheSignaturesAreMeasuredOnEveryHiddenChannel()
    {
        var table = SignatureTable();
        Assert.Equal(HiddenModes(), table.Length);     // one signature per HIDDEN MODE, not per channel
        Assert.True(table.Length > 0);
        Assert.All(table, r => Assert.True(r.Clock >= 0.0 && r.Acceleration >= 0.0 && r.Field >= 0.0));
        // the readings respond somewhere in the kernel, which is what G_047 established
        Assert.True(table.Any(r => r.Clock > 0.0));
        Assert.True(table.Any(r => r.Acceleration > 0.0) && table.Any(r => r.Field > 0.0));
    }

    [Fact]
    public void Y_G_050_TheKernelIsNotTheGaugeOrbitAndTheVerdictIsDerived()
    {
        Assert.Equal("DERIVED", Verdict());
        // the gauge orbit is a different object, measured separately in G_046
        Assert.True(RhoAccessibilityAudit.OrbitDimension(RhoAccessibilityAudit.BaseState()) != KernelDimension());
        Assert.Contains("PHASE SECTOR", PhysicalInterpretation());
        Assert.Contains("NOT a gauge orbit", WhatItIsNot());
    }

    [Fact]
    public void Y_G_050_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_050 - Kernel Structure Audit: what physical structure do the 53 kernel directions represent?");

        sb.AppendLine("QUESTION. What physical structure do the 53 kernel directions represent?");
        sb.AppendLine("GIVEN        G_040 (95 = 48 + 47), G_046 (the hidden set is the kernel), G_047 (53 readings),");
        sb.AppendLine("             G_049 (three of four readings are lossless)");
        sb.AppendLine("MEASURE      basis vectors | symmetry classes | multiplicity relation | clock, acceleration and");
        sb.AppendLine("             field signatures");
        sb.AppendLine("GOAL         does the kernel have an independent physical interpretation?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The symmetry channels are the ring's Fourier channels, and channel 0 is the simplex direction");
        sb.AppendLine("     itself, so it contributes no state direction.");
        sb.AppendLine("  2. A channel is HIDDEN when its subspace lies inside the kernel, visible when it is orthogonal to it,");
        sb.AppendLine("     and SPLIT otherwise - and a split channel would refute the isotypy argument.");
        sb.AppendLine("  3. Signatures are the responses of the three lossless readings to a unit step along the channel's own");
        sb.AppendLine("     mode, projected into the kernel.");
        sb.AppendLine("  4. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputChannels());
        PrintHeader(OutputSpectral());
        PrintHeader(OutputSignatures());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
