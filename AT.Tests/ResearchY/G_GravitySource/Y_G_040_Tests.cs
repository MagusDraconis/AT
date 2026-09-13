using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_040 — Rho Observable Audit.
///
/// QUESTION. Can any measurable quantity retain ALL 95 dimensions of rho?
///
/// Candidates: occupancy patterns, mode populations, detector counts, attractor occupancy, survivor occupancy.
/// Test: dimension retained, information loss, invertibility.
/// Goal: the first observable O such that O &lt;-&gt; rho is approximately invertible.
///
/// ANSWER: **REFUTED — and the ceiling is a symmetry theorem, not a detector limitation.**
///
///  (1) THE LADDER, computed on a recomputed spectrum (96 cells, 45 levels, the histogram {1:1, 2:42, 5:1, 6:1},
///      trace 1152, state space 95):
///        site-addressed cell counts : 95 retained, loss  0  — but this IS rho
///        distance-class contractions: 48 retained, loss 47  — the best non-addressed observable
///        spectral level populations : 44 retained, loss 51  — exactly G_039's free room
///  (2) WHY 47 IS A CEILING. AT's operators must commute with the substrate's dihedral symmetry (order 192), so
///      they live in the centralizer algebra. Its dimension is the number of orbitals, computed FOUR ways, all
///      49: orbital enumeration, Burnside (1/|G|) Sum fix(g)^2 = 9408/192, the multiplicity-free irrep
///      decomposition, and the sum of the algebra's restricted ranks over the 45 levels.
///  (3) WHAT IS LOST. The 47 dimensions are exactly the intra-doublet ORIENTATIONS: the real state is 48
///      magnitudes plus one angle per two-dimensional irrep, and D96 has 47 of those. Schur's lemma makes them
///      unreachable: the computed witnesses move the state by 1.997e-1 while moving every contraction by at
///      most 2.78e-17, and all 192 group images of a generic state report one identical reading.
///  (4) THE REGIME CAVEAT, self-caught. The retained dimension is a GENERIC-STATE statement — a state confined
///      to four channels collapses to rank 4, so sparse states are LESS observable. Both regimes are asserted
///      here rather than the convenient one.
///  (5) A NUMERICAL SLIP, self-caught. The rank threshold must be relative to the whole matrix, not to each row:
///      a per-row tolerance accepted a row of pure roundoff (A_24 restricted to channel 1 is exactly zero),
///      which read each doublet as rank 2 and summed the 45 levels to 143 instead of 49.
/// </summary>
public class Y_G_040_Tests : ResearchTestBase
{
    public Y_G_040_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_040_TheStateSpaceIsRecomputedNotQuoted()
    {
        // The audit stands on its own floor: the spectrum is recomputed before any dimension is counted.
        Assert.Equal(96, RhoObservableAudit.D96Cells);
        Assert.Equal(45, RhoObservableAudit.DistinctLevels());
        Assert.Equal(1152, RhoObservableAudit.LaplacianTrace());
        Assert.Equal(95, RhoObservableAudit.StateDimension());
        Assert.True(RhoObservableAudit.SpectrumReproducesTheRecord());

        var hist = RhoObservableAudit.Levels().GroupBy(l => l.Multiplicity)
            .ToDictionary(g => g.Key, g => g.Count());
        Assert.Equal(1, hist[1]);
        Assert.Equal(42, hist[2]);
        Assert.Equal(1, hist[5]);
        Assert.Equal(1, hist[6]);
        Assert.Equal(96, RhoObservableAudit.Levels().Sum(l => l.Multiplicity));
    }

    [Fact]
    public void Y_G_040_TheSymmetryIsAPermutationGroupOfOrder192()
    {
        Assert.Equal(192, RhoObservableAudit.GroupOrder());
        Assert.Equal(192, RhoObservableAudit.DistinctGroupElements());   // no rotation equals a reflection
        Assert.True(RhoObservableAudit.GroupIsClosedUnderComposition());
        Assert.Equal(1, RhoObservableAudit.OrbitsOnCells());            // transitive: one orbit on the cells

        // the fixed-point structure Burnside needs: 96 once, 2 for the 48 even reflections, 0 for the rest
        var counts = RhoObservableAudit.FixedPointCounts();
        Assert.Single(counts, f => f == 96);
        Assert.Equal(48, counts.Count(f => f == 2));
        Assert.Equal(143, counts.Count(f => f == 0));   // 95 non-trivial rotations + 48 odd reflections
        Assert.Equal(192, counts.Length);
    }

    [Fact]
    public void Y_G_040_TheCentralizerIs49DimensionalFourWays()
    {
        // The ceiling of the whole audit. Four routes, none of which cites the others.
        Assert.Equal(49, RhoObservableAudit.OrbitalCountBruteForce());
        Assert.Equal(49, RhoObservableAudit.CommutantDimensionByBurnside());
        Assert.Equal(49, RhoObservableAudit.SumOfIrrepsPerLevel());
        Assert.Equal(49, Enumerable.Range(0, RhoObservableAudit.DistinctLevels())
            .Sum(RhoObservableAudit.RestrictedAlgebraRank));
        Assert.True(RhoObservableAudit.TheCountsAgree());

        // ...and it is the irreps, not the multiplicities, that the algebra can reach
        Assert.Equal(230, RhoObservableAudit.SumOfMultiplicitySquares());
        Assert.Equal(181, RhoObservableAudit.ProtectedDimensions());
        Assert.Equal(230 - 49, RhoObservableAudit.ProtectedDimensions());
    }

    [Fact]
    public void Y_G_040_TheIrrepDecompositionIs47DoubletsPlus2Singlets()
    {
        Assert.Equal(49, RhoObservableAudit.IrrepCount());
        Assert.Equal(47, RhoObservableAudit.DoubletCount());
        Assert.Equal(2, RhoObservableAudit.SingletCount());

        // the arithmetic the whole audit rests on: 48 magnitudes + 47 orientations = 95
        Assert.Equal(95, RhoObservableAudit.ContractionRetained() + RhoObservableAudit.ContractionLoss());
        Assert.Equal(RhoObservableAudit.DoubletCount(), RhoObservableAudit.ContractionLoss());

        // only two levels carry more than one irrep, and they are worth 4 dimensions
        Assert.Equal(2, RhoObservableAudit.IrrepsPerLevel().Count(r => r > 1));
        Assert.Equal(4, RhoObservableAudit.DimensionsReclaimedByResolvingLevels());
        Assert.Equal(RhoObservableAudit.SpectralLoss() - RhoObservableAudit.ContractionLoss(),
                     RhoObservableAudit.DimensionsReclaimedByResolvingLevels());
    }

    [Fact]
    public void Y_G_040_TheLadderRetains44Then48Then95()
    {
        Assert.Equal(44, RhoObservableAudit.SpectralRetained());
        Assert.Equal(51, RhoObservableAudit.SpectralLoss());
        Assert.Equal(48, RhoObservableAudit.ContractionRetained());
        Assert.Equal(47, RhoObservableAudit.ContractionLoss());
        Assert.Equal(95, RhoObservableAudit.AddressedRetained());
        Assert.Equal(0, RhoObservableAudit.AddressedLoss());

        // the spectral loss IS G_039's free room — the two audits meet on the same number
        Assert.Equal(51, RhoObservableAudit.FreeRoom());
        Assert.True(RhoObservableAudit.SpectralLossIsTheFreeRoom());

        // and the ordering is strict: resolving levels buys four dimensions, addressing cells buys the rest
        Assert.True(RhoObservableAudit.SpectralRetained() < RhoObservableAudit.ContractionRetained());
        Assert.Equal(RhoObservableAudit.StateDimension(), RhoObservableAudit.AddressedRetained());
    }

    [Fact]
    public void Y_G_040_TheOrientationIsInvisibleAndTheImagesAgree()
    {
        // (a) a rotation inside a two-dimensional irrep is unreachable by every contraction, in BOTH regimes
        var (stateMove, dataMove) = RhoObservableAudit.OrientationWitness(1.1);
        Assert.True(dataMove < 1e-12, $"the orientation moved the data by {dataMove:E3}");
        Assert.True(stateMove > 1e-3, $"the orientation moved the flat state by only {stateMove:E3}");
        var (contrastMove, contrastData) = RhoObservableAudit.OrientationWitness(1.1, contrasty: true);
        Assert.True(contrastData < 1e-12, $"on a contrasty state the orientation moved the data by {contrastData:E3}");
        Assert.True(contrastMove > 1e-1, $"on a contrasty state the orientation moved it by only {contrastMove:E3}");
        Assert.True(RhoObservableAudit.OrientationIsInvisible());

        // (b) the 192 images of a generic state are one reading and 192 states
        var (dataSpread, stateSpread, distinct) = RhoObservableAudit.GroupImageWitness();
        Assert.Equal(1, distinct);
        Assert.True(dataSpread < 1e-12, $"the images disagreed on the data by {dataSpread:E3}");
        Assert.True(stateSpread > 1e-2, $"the images sat only {stateSpread:E3} apart");
        Assert.True(RhoObservableAudit.GroupImagesShareOneReading());
    }

    [Fact]
    public void Y_G_040_AChannelConfinedStateIsEvenLessObservable()
    {
        // The regime caveat: the ceiling is a generic-state statement, and the audit says so.
        Assert.Equal(49, RhoObservableAudit.ChannelCount(RhoObservableAudit.GenericState()));
        Assert.Equal(5, RhoObservableAudit.ChannelCount(RhoObservableAudit.ChannelConfinedState()));
        int confined = RhoObservableAudit.ChannelConfinedRank();
        Assert.True(confined < RhoObservableAudit.ContractionRetained(),
            $"a confined state retained {confined}, which is not below the generic {RhoObservableAudit.ContractionRetained()}");
        Assert.True(confined > 0);
    }

    [Fact]
    public void Y_G_040_TheVerdictIsRefutedForAll95Dimensions()
    {
        Assert.Equal("REFUTED", RhoObservableAudit.Verdict());

        // three candidates reach the whole space — and all three reach it by BEING rho
        var whole = RhoObservableAudit.CandidatesThatReachTheWholeSpace();
        Assert.Equal(3, whole.Length);
        Assert.Contains("occupancy patterns", whole);
        Assert.Contains("attractor occupancy", whole);
        Assert.Contains("survivor occupancy", whole);

        // the closest distinct rung is the contraction profile, at 48 of 95
        Assert.Equal("distance-class contractions", RhoObservableAudit.ClosestCandidate());
        Assert.Equal(48, RhoObservableAudit.Candidates()
            .Single(c => c.Candidate == "distance-class contractions").Retained);
        Assert.All(RhoObservableAudit.Candidates(), c => Assert.Contains(c.Verdict, new[] { "REFUTED", "CORRELATED" }));
    }

    [Fact]
    public void Y_G_040_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_040 — Rho Observable Audit: can any measurable quantity retain all 95 dimensions?");

        sb.AppendLine("QUESTION. Can any measurable quantity retain ALL 95 dimensions of rho?");
        sb.AppendLine("CANDIDATES  occupancy patterns, mode populations, detector counts, attractor occupancy,");
        sb.AppendLine("            survivor occupancy");
        sb.AppendLine("TEST        dimension retained, information loss, invertibility");
        sb.AppendLine("GOAL        the first observable O such that O <-> rho is approximately invertible");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The substrate is the 96-cell circulant C96(1..6) of G_016/G_018/G_039.");
        sb.AppendLine("  2. An observable is a linear datum of the occupancy; AT's own observables (counts, level");
        sb.AppendLine("     populations, energy = <lambda,rho>) are all of this form, and all are built from the");
        sb.AppendLine("     substrate, so all commute with the substrate's symmetry group.");
        sb.AppendLine("  3. A datum is CONSTRUCTIBLE iff it is equivariant under that group. The alternative is");
        sb.AppendLine("     naming cells one by one, which is exactly the identification G_017 excluded.");
        sb.AppendLine("  4. Retained dimension = the rank of the datum's Jacobian restricted to the simplex");
        sb.AppendLine("     tangent space (dimension 95); the loss is what the datum cannot see.");
        sb.AppendLine("  5. Deterministic throughout: fixed coefficients, closed-form spectra, no randomness.");
        sb.AppendLine();

        PrintHeader(RhoObservableAudit.OutputSpace());
        PrintHeader(RhoObservableAudit.OutputSymmetry());
        PrintHeader(RhoObservableAudit.OutputLadder());
        PrintHeader(RhoObservableAudit.OutputWitnesses());
        PrintHeader(RhoObservableAudit.OutputCandidates());

        Output.WriteLine(sb.ToString());
    }
}
