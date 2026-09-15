using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.ResidualPhaseAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_061 - Residual Phase Audit (group G - Gravity Source).
///
/// QUESTION. What is special about the 11 unreachable phase directions of G_060? Identify the alternating mode, the empty
/// channels, the kernel relation and the symmetry properties; test whether any existing AT operator can reach them;
/// explain the 53 = 42 + 11 split.
///
/// ANSWER: **DERIVED - and they are NOT special directions of the substrate at all: they are the modes the canonical
/// state does not occupy, and the split is a property of SHIFT-INVARIANCE rather than of the directions.**
/// </summary>
public class Y_G_061_Tests : ResearchTestBase
{
    public Y_G_061_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_061_TheElevenAreIdentified()
    {
        Assert.Equal(11, TheCountIsEleven());
        Assert.Equal(new[] { 14, 19, 24, 32, 40, 48 }, EmptyChannels());
        Assert.Equal(new[] { 14, 19, 24, 32, 40 }, EmptyDoubletChannels());
        Assert.Equal(AlternatingChannel(), 48);

        // every one of the eleven is empty to the floating-point floor
        Assert.All(UnreachableDirections(), t => Assert.InRange(Math.Abs(Occupancy(t.Channel, t.Kind)), 0.0, 1e-11));

        // the symmetry: order 192, and the two most symmetric directions are measurable
        Assert.Equal(192, GroupOrder());
        Assert.Equal(3, OrbitSize(KernelStructureAudit.ChannelBasis(32)[0]));
        Assert.Equal(2, OrbitSize(KernelStructureAudit.ChannelBasis(48)[0]));
        Assert.True(TheElevenFormASymmetryInvariantSet());
        Assert.All(OrbitTable(), t => Assert.InRange(t.OrbitSize, 2, 192));
    }

    [Fact]
    public void Y_G_061_TheKernelIsTheStatesOrthogonalComplement()
    {
        // hidden IF AND ONLY IF the state has no content in the mode - measured over all 95 non-constant modes
        Assert.True(HiddenIsExactlyZeroOccupancy());
        Assert.True(TheKernelIsTheStatesOrthogonalComplement());

        Assert.Equal(42, OccupiedModes().Length);
        Assert.Equal(43, OccupiedSpanDimension());
        Assert.Equal(53, KernelDimension());
        Assert.Equal(96, OccupiedSpanDimension() + KernelDimension());
        Assert.Equal(42, OccupiedChannels().Length);
    }

    [Fact]
    public void Y_G_061_TheAccountingOfElevenCloses()
    {
        // the two degenerate levels, identified by measurement
        var degenerate = DegenerateLevels();
        Assert.Equal(2, degenerate.Length);
        Assert.Equal(13, degenerate[0].Index);
        Assert.Equal(12.0, degenerate[0].Level, 6);
        Assert.Equal(5, degenerate[0].Multiplicity);
        Assert.Equal(new[] { 16, 32, 48 }, degenerate[0].Channels);
        Assert.Equal(35, degenerate[1].Index);
        Assert.Equal(14.0, degenerate[1].Level, 6);
        Assert.Equal(6, degenerate[1].Multiplicity);
        Assert.Equal(new[] { 8, 24, 40 }, degenerate[1].Channels);

        // the count any one-vector-per-level state must leave empty is the free room
        Assert.Equal(51, ModesLeftEmptyByDegeneracy());
        Assert.Equal(96 - 45, ModesLeftEmptyByDegeneracy());

        // and two levels carry a vanishing construction weight
        Assert.Equal(new[] { 8, 31 }, ZeroWeightLevels());
        Assert.Equal(new[] { 14, 19 }, ZeroWeightChannels());

        // 7 + 4 = 11, the parts disjoint
        Assert.Equal(7, DirectionsFromDegeneracy());
        Assert.Equal(4, DirectionsFromZeroWeightLevels());
        Assert.Equal(11, TheElevenAccountedFor().Total);
        Assert.True(TheElevenClose());
    }

    [Fact]
    public void Y_G_061_CirculantAtOperatorsCannotReachThem()
    {
        var table = ReachTable();
        Assert.Equal(4, table.Length);
        Assert.All(table.Where(t => t.Operator.Contains("circulant")),
            t => { Assert.Equal(0, t.Rank); Assert.InRange(t.Projection, 0.0, 1e-12); });
        Assert.True(NoCirculantAtOperatorReachesThem());
    }

    [Fact]
    public void Y_G_061_StateDependentAtOperatorsReachAllEleven()
    {
        var connection = ReachTable().Single(t => t.Operator.StartsWith("connection"));
        var coupling = ReachTable().Single(t => t.Operator.StartsWith("T1/T2"));
        Assert.Equal(11, connection.Rank);
        Assert.Equal(11, coupling.Rank);
        Assert.InRange(connection.Projection, 2.9e-3, 3.1e-3);
        Assert.InRange(coupling.Projection, 1.4e-1, 1.5e-1);
        Assert.True(StateDependentAtOperatorsReachAllOfThem());
    }

    [Fact]
    public void Y_G_061_TheElevenAreNotSubstrateInvariantButTheirCountIsConstrained()
    {
        // rebuilding from the LAST basis vector of each level MOVES the unreachable set
        Assert.Equal(new[] { 8, 14, 16, 19, 24, 32 }, AlternativeEmptyChannels());
        Assert.Equal(12, AlternativeUnreachableCount());
        Assert.Equal(11, CanonicalUnreachableCount());
        Assert.True(TheElevenAreNotSubstrateInvariant());
        Assert.False(AlternativeEmptyChannels().Contains(AlternatingChannel()));
        Assert.True(AlternativeOccupiedAlternating());

        // what does NOT move is the count the degeneracy forces
        Assert.True(TheDegeneracyCountIsInvariant());

        Assert.Equal("DERIVED", Verdict());
    }

    [Fact]
    public void Y_G_061_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_061 - Residual Phase Audit: what is special about the 11 unreachable phase directions?");

        sb.AppendLine("QUESTION. What is special about the 11 unreachable phase directions of G_060?");
        sb.AppendLine("GIVEN        G_060 - the reachable phase rank is 42 of 53 by two independent routes, and the eleven");
        sb.AppendLine("             unreachable directions are the five doubly-hidden channels plus the alternating mode");
        sb.AppendLine("IDENTIFY     the alternating mode, the empty channels, the kernel relation, the symmetry properties");
        sb.AppendLine("TEST         can any existing AT operator reach these directions?");
        sb.AppendLine("GOAL         explain the 53 = 42 + 11 split");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. \"Hidden\" keeps G_040's definition: a direction that changes NO contraction.");
        sb.AppendLine("  2. The construction weight of the canonical state is READ from G_046's core rather than copied: the");
        sb.AppendLine("     audit asks which levels carry no weight, and a copied formula could drift from the state it describes.");
        sb.AppendLine("  3. Reachability is measured as the projection of an operator's response onto the eleven, with the");
        sb.AppendLine("     CIRCULANT operators given their exact action and the STATE-DEPENDENT ones given their exact Jacobian.");
        sb.AppendLine("  4. Deterministic throughout, on the canonical state unless an alternative is named.");
        sb.AppendLine();

        PrintHeader(OutputTheEleven());
        PrintHeader(OutputLevels());
        PrintHeader(OutputReach());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
