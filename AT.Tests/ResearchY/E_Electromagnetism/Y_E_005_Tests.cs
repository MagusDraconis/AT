using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.PropagationOriginAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_005 - Propagation Origin Audit (group E - Electromagnetism).
///
/// QUESTION. What is the minimal missing ingredient that turns T1(3) into a propagating photon sector and T2(3)
/// into a propagating graviton sector? Separate representation / kinematics / dynamics / gauge, determine the
/// FIRST missing step, and locate the unique bottleneck shared by photon and graviton.
///
/// ANSWER: **BOUNDARY - the first failing layer is KINEMATICS, and the bottleneck is ONE object: a first-order
/// derivative that carries a DIRECTION INDEX (a field-valued connection).**
///
///  (1) THE REPRESENTATION LAYER IS SATISFIED and it is the only one that is: T1 is 3 states with multiplicity
///      1 and the traceless rank-2 is E + T2 = 5, while the single ring tops out at 2.
///  (2) LOCALISATION ONE - THE INDEX SPACES COLLAPSE. A field strength carries two antisymmetrised indices and
///      the graviton's field is a traceless symmetric rank-2: at a direction rank of one the antisymmetric
///      square is exactly 0 and the traceless symmetric is exactly 0, so neither sector has anywhere to put a
///      field strength. The two sectors as they stand need exactly 3 directions.
///  (3) LOCALISATION TWO - THE SUBSTRATE'S DERIVATIVE IS ONE-DIRECTIONAL. AT's radius-6 Laplacian IS a sum of
///      six squares of first-order differences, so first-order operators exist, but all six strides lie along a
///      single line: the direction rank is 1 against the 3 a vector index needs.
///  (4) LOCALISATION THREE - THE SUBSTRATE'S OWN CONNECTION IS EXACTLY PURE GAUGE. Conjugating the covariant
///      difference by the site-dependent phase returns the ordinary difference, and it is periodic precisely
///      because the holonomy is 96 x 2pi/96 = 2pi. So its field strength is identically zero and it has no
///      gauge-invariant content: there is nothing in it to propagate.
///  (5) DYNAMICS AND GAUGE ARE MISSING BUT BLOCKED, not merely absent - which REFINES E_004, whose missing
///      "choice of kinetic form" is the SECOND step, unreachable until a derivative acts on the field.
///  (6) TWO RIVAL BOTTLENECKS ARE REFUTED: the representation (it is complete) and the exterior complex (the
///      photon's field strength is antisymmetric, dim 3, while the graviton's field is symmetric, dim 5, so the
///      graviton is not a form). What serves both is one directional derivative, and the shared count proves it:
///      3 - 1 = 2 for the photon and 5 - 3 = 2 for the graviton.
/// </summary>
public class Y_E_005_Tests : ResearchTestBase
{
    public Y_E_005_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_005_TheRepresentationLayerIsTheOnlySatisfiedOne()
    {
        // the two sectors the question names
        Assert.Equal(3, PhotonComponents());
        Assert.Equal(5, GravitonComponents());

        // T1 is irreducible with multiplicity 1; the traceless rank-2 splits as E + T2
        var photon = SectorContent().Where(r => r.Sector.StartsWith("photon", StringComparison.Ordinal)).ToArray();
        var graviton = SectorContent().Where(r => r.Sector.StartsWith("graviton", StringComparison.Ordinal)).ToArray();
        Assert.Contains(photon, r => r.Irrep == "T1" && r.Dimension == 3 && r.Multiplicity == 1);
        Assert.Equal(5, graviton.Sum(r => r.Dimension * r.Multiplicity));
        Assert.Contains(graviton, r => r.Irrep == "E" && r.Dimension == 2);
        Assert.Contains(graviton, r => r.Irrep == "T2" && r.Dimension == 3);

        // the single ring cannot host either
        Assert.Equal(2, VectorSectorAudit.SingleRingMaxIrrepDimension());
        Assert.True(NeitherExistsOnTheRing());
        Assert.True(RepresentationIsComplete());
    }

    [Fact]
    public void Y_E_005_TheIndexSpacesCollapseAtOneDirection()
    {
        var one = IndexSpaces()[0];
        Assert.Equal(1, one.Directions);
        Assert.Equal(1, one.Vector);
        Assert.Equal(0, one.Antisymmetric);      // no pair of indices: nowhere to put a field strength
        Assert.Equal(0, one.TracelessSymmetric); // no graviton index space either
        Assert.True(AtOneDirectionNoIndexSpaceSurvives());

        // at three directions both sectors' index spaces exist
        var three = IndexSpaces()[2];
        Assert.Equal(3, three.Vector);
        Assert.Equal(3, three.Antisymmetric);
        Assert.Equal(5, three.TracelessSymmetric);

        Assert.True(TheRingHasTooFewDirections());
    }

    [Fact]
    public void Y_E_005_TheDerivativeExistsButPointsOneWay()
    {
        // the operator identity: a circulant is determined by its first row
        Assert.True(TheLaplacianIsASumOfSquares());
        Assert.True(DecompositionResidual() < 1e-12);

        // but the direction rank is one, against the three an index needs
        Assert.Equal(1, RingDirectionRank());
        Assert.Equal(3, CubeDirectionRank());
        Assert.Equal(3, DirectionsNeeded());
        Assert.True(FirstOrderOperatorsExistButPointOneWay());
        Assert.True(TheSubstrateIsOneDirectional());
        Assert.True(TheCubeWouldSupplyEnough());
    }

    [Fact]
    public void Y_E_005_TheBuiltInConnectionIsExactlyPureGauge()
    {
        Assert.Equal(2.0 * Math.PI / 96.0, BuiltInStep(), 9);
        Assert.Equal(2.0 * Math.PI, BuiltInHolonomy(), 9);       // one whole turn
        Assert.True(Math.Abs(HolonomyModQuantum()) < 1e-12);      // ... which is the identity
        Assert.True(TheBuiltInConnectionIsExactlyPureGauge());
        Assert.True(PureGaugeResidual() < 1e-12);

        // and a phase with a residue is what a field strength would be made of
        Assert.True(AResidueIsWhatAFieldStrengthWouldNeed());

        // the ring's loop structure: local field strength needs plaquettes
        Assert.Equal(481, RingIndependentLoops());
        Assert.Equal(1769473, CubeIndependentLoops());
        Assert.Equal(27648, CubeElementaryPlaquettes());
    }

    [Fact]
    public void Y_E_005_TheChainStopsAtKinematics()
    {
        Assert.Equal(4, Layers.Length);
        Assert.Equal(new[] { "representation", "kinematics", "dynamics", "gauge" }, Layers);
        Assert.Equal("kinematics", FirstMissingStep());
        Assert.Equal(new[] { "dynamics", "gauge" }, BlockedLayers());
        Assert.True(TheChainStopsAtKinematics());

        // exactly one layer is satisfied, and it is the first
        var ladder = LayerLadder();
        Assert.Equal(4, ladder.Length);
        Assert.StartsWith("SATISFIED", ladder[0].Status, StringComparison.Ordinal);
        Assert.StartsWith("MISSING", ladder[1].Status, StringComparison.Ordinal);
        Assert.Equal(2, ladder.Count(r => r.Status.Contains("BLOCKED", StringComparison.Ordinal)));
    }

    [Fact]
    public void Y_E_005_TheBottleneckIsSharedAndUnique()
    {
        // the same rule for both sectors
        var rows = GaugeSubtraction();
        Assert.Equal(2, rows.Length);
        Assert.Equal((3, 1, 2), (rows[0].Components, rows[0].Orbit, rows[0].Physical));
        Assert.Equal((5, 3, 2), (rows[1].Components, rows[1].Orbit, rows[1].Physical));
        Assert.True(BothSectorsReduceToTwo());

        // two rivals refuted, one unique bottleneck left
        Assert.Equal(2, RefutedCandidates().Length);
        Assert.Contains("the representation", RefutedCandidates());
        Assert.Contains(RefutedCandidates(), c => c.Contains("exterior complex", StringComparison.Ordinal));
        Assert.Contains("direction index", UniqueBottleneck(), StringComparison.Ordinal);
        Assert.True(TheBottleneckIsSharedAndUnique());

        Assert.Equal("BOUNDARY", Verdict());
    }

    [Fact]
    public void Y_E_005_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_005 - Propagation Origin Audit: what is the FIRST missing step?");

        sb.AppendLine("QUESTION. What is the minimal missing ingredient that turns T1(3) into a propagating");
        sb.AppendLine("photon sector and T2(3) into a propagating graviton sector?");
        sb.AppendLine("SEPARATE  representation / kinematics / dynamics / gauge");
        sb.AppendLine("GOAL      locate the unique bottleneck shared by photon and graviton");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The sectors are taken as E_003/E_004 left them: T1(3) is a genuine irreducible");
        sb.AppendLine("     dimension-3 sector of the cubic arrangement, and the graviton's field is the");
        sb.AppendLine("     traceless symmetric rank-2, E + T2 = 5.");
        sb.AppendLine("  2. The substrate actually instantiated is D96 - a single closed line of 96 cells with a");
        sb.AppendLine("     radius-6 Laplacian - because G_033 found the cube is never instantiated.");
        sb.AppendLine("  3. A field strength needs a pair of antisymmetrised direction indices; a propagating");
        sb.AppendLine("     mode needs a SECOND direction to vary along.");
        sb.AppendLine("  4. The layers are tested IN THE ORDER THE QUESTION NAMES THEM, and a layer is called");
        sb.AppendLine("     MISSING only on a computed witness, never on expectation.");
        sb.AppendLine("  5. Deterministic throughout; no randomness and no fitted constant.");
        sb.AppendLine();

        PrintHeader(OutputLayers());
        PrintHeader(OutputIndexSpaces());
        PrintHeader(OutputBottleneck());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
