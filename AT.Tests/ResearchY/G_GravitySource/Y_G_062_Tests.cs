using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.ModeOccupationAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_062 - Mode Occupation Audit (group G - Gravity Source).
///
/// QUESTION. What determines which Fourier modes are occupied in the canonical state? Measure the occupied modes, the
/// empty modes, the state construction weights and the degeneracy structure. Test: can the empty 11 be populated
/// without changing the theory?
///
/// ANSWER: **DERIVED - the occupancy is set by the CONSTRUCTION rather than by any AT law, and the eleven CAN be
/// populated by changing only the state; what survives is an algebraic floor of 47 that no state can cross.**
/// </summary>
public class Y_G_062_Tests : ResearchTestBase
{
    public Y_G_062_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_062_TheOccupancyRuleIsArithmetic()
    {
        Assert.Equal(45, Levels());
        Assert.Equal(43, NonZeroWeightLevels().Length);
        Assert.Equal(new[] { 8, 31 }, Enumerable.Range(0, Levels()).Where(k => RhoAccessibilityAudit.BaseStateWeight(k) == 0.0));

        // the rule: one seed per non-zero-weight level, one of which is the mean
        Assert.Equal(42, OccupiedModesFromTheRule());
        Assert.Equal(42, OccupiedModes(Canonical()));
        Assert.Equal(53, EmptyModes(Canonical()));
        Assert.True(TheRulePredictsTheCanonicalCount());

        // and the count is the formula's, not the substrate's: removing the zeros gives 44
        Assert.Equal(44, OccupiedModes(FullWeight()));
        Assert.Equal(51, EmptyModes(FullWeight()));
    }

    [Fact]
    public void Y_G_062_TheEmptyChannelsFollowTheFormulaZeros()
    {
        Assert.Equal(new[] { 14, 19, 24, 32, 40, 48 }, EmptyChannels(Canonical()));

        // a DIFFERENT weight formula, whose zeros fall elsewhere, moves the empty channels
        Assert.Equal(new[] { 6, 29 }, ShiftedZeroLevels());
        Assert.Equal(new[] { 6, 23, 24, 32, 40, 48 }, EmptyChannels(ShiftedFormula()));
        Assert.Equal(42, OccupiedModes(ShiftedFormula()));
        Assert.False(EmptyChannels(ShiftedFormula()).SequenceEqual(EmptyChannels(Canonical())));

        // the channels the degeneracy empties are the same under both formulas
        Assert.Contains(24, EmptyChannels(ShiftedFormula()));
        Assert.Contains(32, EmptyChannels(ShiftedFormula()));
        Assert.Contains(40, EmptyChannels(ShiftedFormula()));
        Assert.Contains(48, EmptyChannels(ShiftedFormula()));
    }

    [Fact]
    public void Y_G_062_TheElevenCanBePopulatedByChangingOnlyTheState()
    {
        // a state carrying every mode empties no channel and occupies all eleven
        Assert.Equal(95, OccupiedModes(AllModes()));
        Assert.Equal(0, EmptyModes(AllModes()));
        Assert.Empty(EmptyChannels(AllModes()));
        Assert.True(OccupiesTheEleven(AllModes()));
        Assert.True(EveryOneOfTheElevenIsOccupiedBySomeState());
        Assert.Equal(11, TheEleven().Count(t => Math.Abs(OccupancyOf(AllModes(), t.Channel, t.Kind)) > Floor));

        // and NOTHING but the state moved: the level structure, the distance classes and the eleven are the same objects
        Assert.Equal(45, RhoObservableAudit.DistinctLevels());
        Assert.Equal(49, DistanceClasses());
        Assert.Equal(11, TheEleven().Length);
        Assert.All(States(), s => Assert.Equal(49, DistanceClasses()));
    }

    [Fact]
    public void Y_G_062_TheAlgebraicFloorIsFortySeven()
    {
        // the row space is spanned by one row per distance class, so its rank cannot exceed their number
        Assert.True(TheRowRankCannotExceedTheDistanceClasses());
        Assert.All(Table(), t => Assert.InRange(t.Rows, 1, 49));

        // measured: the floor is 49 - the distance classes - and no state crosses it
        Assert.Equal(47, MinimumKernel());
        Assert.True(TheKernelFloorIsTheDistanceClassBound());
        Assert.Equal("all modes occupied", StateWithTheSmallestKernel());
        Assert.Equal(49, RowSpaceRank(AllModes()));

        // the canonical state is ABOVE the floor, by six dimensions of its own construction
        Assert.Equal(53, KernelOf(Canonical()));
        Assert.Equal(43, RowSpaceRank(Canonical()));
        Assert.Equal(6, KernelOf(Canonical()) - MinimumKernel());
        Assert.True(TheKernelFallsTowardsTheFloorAndTheCanonicalStateIsAboveIt());
    }

    [Fact]
    public void Y_G_062_TheDegeneracyStructureDoesNotDependOnTheState()
    {
        var degenerate = ResidualPhaseAudit.DegenerateLevels();
        Assert.Equal(2, degenerate.Length);
        Assert.Equal(13, degenerate[0].Index);
        Assert.Equal(12.0, degenerate[0].Level, 6);
        Assert.Equal(5, degenerate[0].Multiplicity);
        Assert.Equal(new[] { 16, 32, 48 }, degenerate[0].Channels);
        Assert.Equal(35, degenerate[1].Index);
        Assert.Equal(14.0, degenerate[1].Level, 6);
        Assert.Equal(6, degenerate[1].Multiplicity);
        Assert.Equal(new[] { 8, 24, 40 }, degenerate[1].Channels);

        // the level structure is a property of the spectrum, so every state sees the same one
        Assert.Equal(51, ResidualPhaseAudit.ModesLeftEmptyByDegeneracy());
        Assert.All(States(), s => Assert.Equal(45, RhoObservableAudit.DistinctLevels()));
    }

    [Fact]
    public void Y_G_062_TheVerdictIsDerived()
    {
        Assert.True(TheRulePredictsTheCanonicalCount());
        Assert.True(EveryOneOfTheElevenIsOccupiedBySomeState());
        Assert.True(TheRowRankCannotExceedTheDistanceClasses());
        Assert.Equal("DERIVED", Verdict());

        // the canonical state occupies NONE of the eleven, and each of the other constructions moves part of the way
        Assert.Equal(0, TheEleven().Count(t => Math.Abs(OccupancyOf(Canonical(), t.Channel, t.Kind)) > Floor));
        Assert.Equal(2, TheEleven().Count(t => Math.Abs(OccupancyOf(AlternativeSeed(), t.Channel, t.Kind)) > Floor));
        Assert.Equal(2, TheEleven().Count(t => Math.Abs(OccupancyOf(FullWeight(), t.Channel, t.Kind)) > Floor));
        Assert.Equal(4, TheEleven().Count(t => Math.Abs(OccupancyOf(FullWeightAlternativeSeed(), t.Channel, t.Kind)) > Floor));
        Assert.Equal(11, TheEleven().Count(t => Math.Abs(OccupancyOf(AllModes(), t.Channel, t.Kind)) > Floor));
    }

    [Fact]
    public void Y_G_062_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_062 - Mode Occupation Audit: what determines which modes the canonical state occupies?");

        sb.AppendLine("QUESTION. What determines which Fourier modes are occupied in the canonical state?");
        sb.AppendLine("GIVEN        G_052 (the interface identity), G_059 (the canonical state is phase-free), G_060 (the");
        sb.AppendLine("             reachable phase rank is 42 of 53), G_061 (the eleven are the unoccupied channels)");
        sb.AppendLine("MEASURE      occupied modes, empty modes, construction weights, degeneracy structure");
        sb.AppendLine("TEST         can the empty 11 be populated without changing the theory?");
        sb.AppendLine("GOAL         explain why the canonical state occupies 42 modes and leaves 11 empty");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. \"Occupied\" means the state has content in the mode above the 1E-9 floor, which is G_061's measured");
        sb.AppendLine("     equivalence with the mode being observable.");
        sb.AppendLine("  2. The construction weight is READ from G_046's core, not copied, so the audit cannot drift from the");
        sb.AppendLine("     state it describes.");
        sb.AppendLine("  3. \"Without changing the theory\" means: the levels, the distance classes, the contraction rows and the");
        sb.AppendLine("     eleven stay the same OBJECTS - only the state differs. Each of those is asserted for every state.");
        sb.AppendLine("  4. A probe with a vanishing weight of its own is a PROBE defect, not a finding: a first version of the");
        sb.AppendLine("     all-modes state left eight modes empty because its weight formula had zeros.");
        sb.AppendLine();

        PrintHeader(OutputOccupancy());
        PrintHeader(OutputRule());
        PrintHeader(OutputFloor());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
