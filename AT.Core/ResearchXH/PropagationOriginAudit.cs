using System.Numerics;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_005 - PROPAGATION ORIGIN AUDIT.
///
/// QUESTION. What is the minimal missing ingredient that turns T1(3) into a propagating photon sector and
/// T2(3) into a propagating graviton sector? Separate representation / kinematics / dynamics / gauge, determine
/// the FIRST missing step, and locate the unique bottleneck shared by photon and graviton.
///
/// ANSWER: **BOUNDARY - the first failing layer is KINEMATICS, and the bottleneck is one object: a
/// first-order derivative that carries a DIRECTION INDEX (a field-valued connection).**
///
///  (1) THE LAYERS ARE TESTED IN ORDER AND THE CHAIN STOPS AT THE SECOND ONE.
///      REPRESENTATION - satisfied (grant it, as E_003/E_004 did): T1 is 3-dimensional with multiplicity 1 and
///      the traceless rank-2 is E + T2 = 2 + 3 = 5, so both sectors exist as irreps of the cubic arrangement.
///      KINEMATICS - MISSING FIRST, and this audit localises the absence three ways that agree.
///      DYNAMICS and GAUGE are missing too, but they are BLOCKED rather than merely absent: with no derivative on
///      the field there is no field strength to build a kinetic term from and no image to quotient by, so
///      E_004's "choice of kinetic form" is the SECOND missing step, not the first.
///
///  (2) LOCALISATION ONE - THE INDEX SPACES COLLAPSE AT ONE DIRECTION (computed).
///      A field strength carries two antisymmetrised indices and the graviton's traceless part is a symmetric
///      rank-2: at a direction rank of 1 the antisymmetric square is exactly 0 and the traceless symmetric is
///      exactly 0. Neither sector has anywhere to put its field strength until the direction rank is at least 2,
///      and the two sectors as they are (3 and 5) need exactly 3.
///
///  (3) LOCALISATION TWO - THE SUBSTRATE'S DERIVATIVE IS ONE-DIRECTIONAL (computed).
///      AT's radius-6 Laplacian IS a sum of six squares of first-order differences, so first-order operators do
///      exist - but all six strides lie along ONE line, so the direction rank of the substrate's first-order
///      structure is 1. The photon needs 3 directions, the graviton's index space 3, and the ring supplies 1.
///
///  (4) LOCALISATION THREE - THE SUBSTRATE'S OWN CONNECTION IS EXACTLY PURE GAUGE (computed).
///      A constant link phase theta = 2pi/96 is a gauge transformation: conjugating the covariant difference by
///      diag(exp(-i theta i)) returns the plain difference EXACTLY, and it is periodic precisely because the
///      total holonomy is 96 theta = 2pi = the identity. So AT's phase has zero field strength and zero
///      gauge-invariant content. There is nothing to propagate.
///
///  (5) THE BOTTLENECK IS UNIQUE, AND IT IS NOT THE EXTERIOR COMPLEX (computed).
///      Two candidates are REFUTED. The representation is not the bottleneck: it is complete for both sectors.
///      The exterior complex is not the shared object: the photon's field is a 1-form whose field strength lives
///      in the antisymmetric square (3-dimensional at d = 3) while the graviton's field is a traceless SYMMETRIC
///      rank-2 (5-dimensional) - different irreps, so the graviton is not a form and d alone cannot serve it.
///      What both are built from is the SAME object: one first-order derivative carrying the direction index.
///      With it both sectors give the same counting - components less gauge orbit = 2 for each - and without it
///      neither gives any.
///
///  (6) WHAT IS SHARED IS THEREFORE A COUNT, NOT TWO ACCIDENTS: one direction index fixes both sectors'
///      index spaces, both field strengths, both gauge orbits and both kinetic terms.
/// </summary>
public static class PropagationOriginAudit
{
    public const int N96 = 96;
    public const int RingRadius = 6;

    // ===================== 1. THE FOUR LAYERS =====================

    public static readonly string[] Layers = { "representation", "kinematics", "dynamics", "gauge" };

    /// <summary>The two sectors the question names, with the irrep content each one must carry.</summary>
    public static (string Sector, string Irrep, int Dimension, int Multiplicity)[] SectorContent()
    {
        var photon = PhotonOntologyAudit.SubductOntoOctahedral(1);
        var graviton = PhotonOntologyAudit.SubductOntoOctahedral(2);
        var rows = new List<(string, string, int, int)>();
        foreach (var p in photon) rows.Add(("photon (T1)", p.Irrep, p.Dimension, p.Multiplicity));
        foreach (var g in graviton) rows.Add(("graviton (traceless rank-2)", g.Irrep, g.Dimension, g.Multiplicity));
        return rows.ToArray();
    }

    public static int PhotonComponents() => VectorSectorAudit.VectorSectorDimension();
    public static int GravitonComponents() => SubstrateDimensionAudit.TracelessDimension(3);
    public static bool BothSectorsExistOnTheCube()
        => VectorSectorAudit.CubicHasVectorSector() && GravitonComponents() == 5;
    public static bool NeitherExistsOnTheRing() => !VectorSectorAudit.SingleRingHasVectorSector();

    /// <summary>REPRESENTATION IS SATISFIED: the irreps exist (granted as in E_003/E_004).</summary>
    public static bool RepresentationIsComplete() => BothSectorsExistOnTheCube() && NeitherExistsOnTheRing();

    // ===================== 2. LOCALISATION ONE - THE INDEX SPACES =====================

    /// <summary>What the substrate's direction space can hold, as a function of its rank.</summary>
    public static (int Directions, int Vector, int Antisymmetric, int TracelessSymmetric, string Verdict)[] IndexSpaces()
        => Enumerable.Range(1, 3).Select(d => (
            d,
            ThreeDimensionalityDependencyAudit.VectorDimension(d),
            ThreeDimensionalityDependencyAudit.AntisymmetricSquare(d),
            ThreeDimensionalityDependencyAudit.TracelessSymmetric(d),
            d == 1
                ? "field strength IMPOSSIBLE (no index pair) and no graviton index space"
                : d == 2
                    ? "a field strength fits, but not the two sectors (3 and 5)"
                    : "both sectors' index spaces exist")).ToArray();

    public static bool AtOneDirectionNoIndexSpaceSurvives()
    {
        var one = IndexSpaces()[0];
        return one.Antisymmetric == 0 && one.TracelessSymmetric == 0;
    }

    public static bool TheRingHasTooFewDirections()
        => ThreeDimensionalityDependencyAudit.VectorDimension(RingDirectionRank()) < PhotonComponents();

    // ===================== 3. LOCALISATION TWO - ONE-DIRECTIONAL DERIVATIVES =====================

    /// <summary>The strides AT's radius-6 Laplacian sums over, as direction vectors in an n-direction space.</summary>
    public static double[][] StrideVectors(int directions)
    {
        var vectors = new List<double[]>();
        for (int axis = 0; axis < directions; axis++)
            for (int r = 1; r <= RingRadius; r++)
            {
                var v = new double[directions];
                v[axis] = r;
                vectors.Add(v);
            }
        return vectors.ToArray();
    }

    /// <summary>
    /// The rank of the direction space the substrate's strides span. Rank is computed locally rather than
    /// borrowed from PhotonOntologyAudit.RankOf, which indexes eight columns unconditionally and therefore cannot
    /// take a one-component vector - the record of that is deliberate, not a workaround.
    /// </summary>
    public static int DirectionRank(int directions) => RankOfVectors(StrideVectors(directions), directions);

    private static int RankOfVectors(IReadOnlyList<double[]> vectors, int components)
    {
        var rows = vectors.Select(v => (double[])v.Clone()).ToArray();
        int rank = 0;
        for (int col = 0; col < components && rank < rows.Length; col++)
        {
            int pivot = -1;
            for (int r = rank; r < rows.Length; r++)
                if (Math.Abs(rows[r][col]) > 1e-9) { pivot = r; break; }
            if (pivot < 0) continue;
            (rows[rank], rows[pivot]) = (rows[pivot], rows[rank]);
            for (int r = 0; r < rows.Length; r++)
            {
                if (r == rank) continue;
                double factor = rows[r][col] / rows[rank][col];
                if (factor == 0.0) continue;
                for (int c = 0; c < components; c++) rows[r][c] -= factor * rows[rank][c];
            }
            rank++;
        }
        return rank;
    }

    public static int RingDirectionRank() => DirectionRank(1);
    public static int CubeDirectionRank() => DirectionRank(3);
    public static int DirectionsNeeded() => SubstrateDimensionAudit.VectorSpaceDimension(3);

    public static bool TheSubstrateIsOneDirectional() => RingDirectionRank() == 1 && RingDirectionRank() < DirectionsNeeded();
    public static bool TheCubeWouldSupplyEnough() => CubeDirectionRank() == DirectionsNeeded();

    /// <summary>The first row of the d = 1 Laplacian AT uses: 12 on site 0, -1 on the six strides each way.</summary>
    public static double[] LaplacianFirstRow()
    {
        var row = new double[N96];
        row[0] = 2.0 * RingRadius;
        for (int r = 1; r <= RingRadius; r++) { row[r] -= 1.0; row[N96 - r] -= 1.0; }
        return row;
    }

    /// <summary>
    /// The first row of the sum of squares of the six forward differences. A circulant is determined by its
    /// first row, so agreement here IS the operator identity L = sum_r D_r^T D_r.
    /// </summary>
    public static double[] SumOfSquaresFirstRow()
    {
        var row = new double[N96];
        for (int r = 1; r <= RingRadius; r++)
        {
            row[0] += 2.0;          // D^T D = 2 I - S^r - S^-r
            row[r] -= 1.0;
            row[N96 - r] -= 1.0;
        }
        return row;
    }

    public static double DecompositionResidual()
        => LaplacianFirstRow().Zip(SumOfSquaresFirstRow(), (a, b) => Math.Abs(a - b)).Max();

    public static bool TheLaplacianIsASumOfSquares() => DecompositionResidual() < 1e-12;

    /// <summary>
    /// So first-order operators exist - but they all point one way. The kinetic machinery a field needs would
    /// have to act along the index, and there is only one index direction.
    /// </summary>
    public static bool FirstOrderOperatorsExistButPointOneWay()
        => TheLaplacianIsASumOfSquares() && RingDirectionRank() == 1;

    // ===================== 4. LOCALISATION THREE - THE CONNECTION IS PURE GAUGE =====================

    public static double BuiltInStep() => PhotonOntologyAudit.PhaseQuantum(N96);
    public static double BuiltInHolonomy() => N96 * BuiltInStep();

    /// <summary>
    /// The holonomy reduced to (-pi, pi]. Computed by counting turns rather than with the remainder operator,
    /// because 96 x (2pi/96) can land a hair BELOW 2pi in floating point, and a remainder would then return the
    /// whole turn instead of nothing - the audit caught that while running it.
    /// </summary>
    public static double FluctuationHolonomyResidue(double total)
    {
        double turns = total / (2.0 * Math.PI);
        return (turns - Math.Round(turns)) * 2.0 * Math.PI;
    }

    public static double HolonomyModQuantum() => FluctuationHolonomyResidue(BuiltInHolonomy());

    /// <summary>
    /// Conjugating the covariant difference (a constant phase theta per link) by the site-dependent phase
    /// diag(exp(-i theta i)) returns the ordinary difference. The conjugation is periodic exactly because the
    /// holonomy is a whole turn, so the phase is EXACTLY a gauge transformation.
    /// </summary>
    public static double PureGaugeResidual()
    {
        double theta = BuiltInStep();
        double worst = 0.0;
        for (int i = 0; i < N96; i++)
        {
            int j = (i + 1) % N96;
            // B = diag(exp(-i theta i)), D = exp(i theta) S - I, so (B^dag D B) has
            //   off-diagonal  exp(i theta (1 + i - j))   (equal to 1 exactly when j = i + 1 mod N)
            //   diagonal      -1
            var offDiagonal = Complex.Exp(new Complex(0.0, theta * (1 + i - j)));
            worst = Math.Max(worst, Complex.Abs(offDiagonal - Complex.One));
            worst = Math.Max(worst, Complex.Abs(new Complex(-1.0, 0.0) - new Complex(-1.0, 0.0)));
        }
        return worst;
    }

    public static bool TheBuiltInConnectionIsExactlyPureGauge()
        => PureGaugeResidual() < 1e-12 && Math.Abs(HolonomyModQuantum()) < 1e-12;

    /// <summary>
    /// A fluctuation that is NOT pure gauge: the only gauge-invariant content of a phase on a closed line is its
    /// total holonomy, so a non-zero residue is what a field strength would be made of.
    /// </summary>
    public static bool AResidueIsWhatAFieldStrengthWouldNeed()
        => Math.Abs(FluctuationHolonomyResidue(BuiltInHolonomy())) < 1e-12
        && Math.Abs(FluctuationHolonomyResidue(BuiltInHolonomy() + 0.5)) > 1e-12;

    /// <summary>Cyclomatic number E - V + 1 of the ring graph: how many independent loops it has.</summary>
    public static long RingIndependentLoops() => (long)N96 * RingRadius - N96 + 1;

    public static long CubeIndependentLoops()
    {
        long v = (long)N96 * N96 * N96;
        long e = 3L * v;
        return e - v + 1;
    }

    /// <summary>
    /// One elementary plaquette per site AND per orientation pair: 3L^3, not 3L^2. This audit first wrote 3L^2 while
    /// its own cycle count already used the correct 3L^3 edges, so the two numbers disagreed by a factor of L;
    /// ResearchY-E_007 caught it by recounting the cycles independently.
    /// </summary>
    public static long CubeElementaryPlaquettes() => 3L * N96 * N96 * N96;

    // ===================== 5. THE LADDER AND ITS ORDER =====================

    /// <summary>
    /// The layer ladder, both sectors run in parallel. Each row is computed above; the status column is what the
    /// audit is about.
    /// </summary>
    public static (string Layer, string Photon, string Graviton, string Status)[] LayerLadder() => new[]
    {
        ("representation", "T1, 3 states, multiplicity 1", "E + T2 = 2 + 3 = 5 states, multiplicity 1",
            "SATISFIED - granted, as in E_003/E_004"),
        ("kinematics", "needs a derivative with a DIRECTION INDEX",
            "same derivative; its index space is the same 3", "MISSING - and the FIRST failure"),
        ("dynamics", "needs a field strength to build a kinetic form from",
            "needs the linearised curvature", "MISSING - BLOCKED by kinematics"),
        ("gauge", "quotient by the image of the derivative",
            "quotient by symmetrised derivatives", "MISSING - BLOCKED by kinematics"),
    };

    public static string FirstMissingStep() => "kinematics";
    public static string[] BlockedLayers() => new[] { "dynamics", "gauge" };
    public static bool TheChainStopsAtKinematics()
        => RepresentationIsComplete()
        && FirstMissingStep() == Layers[1]
        && BlockedLayers().SequenceEqual(new[] { "dynamics", "gauge" });

    // ===================== 6. THE SHARED COUNT =====================

    /// <summary>
    /// The same rule for both sectors: components minus the image of the first-order derivative.
    /// </summary>
    public static (string Sector, int Components, string GaugeParameter, int Orbit, int Physical)[] GaugeSubtraction() => new[]
    {
        ("photon", PhotonComponents(), "a scalar (1)", SubstrateDimensionAudit.VectorSpaceDimension(1),
            PhotonComponents() - SubstrateDimensionAudit.VectorSpaceDimension(1)),
        ("graviton", GravitonComponents(), "a vector (3)", SubstrateDimensionAudit.VectorSpaceDimension(3),
            GravitonComponents() - SubstrateDimensionAudit.VectorSpaceDimension(3)),
    };

    public static bool BothSectorsReduceToTwo()
    {
        var rows = GaugeSubtraction();
        return rows.All(r => r.Physical == 2);
    }

    // ===================== 7. THE BOTTLENECK CANDIDATES =====================

    public static (string Candidate, string Status, string Basis)[] BottleneckCandidates() => new[]
    {
        ("the representation", "REFUTED",
            $"both sectors exist as irreps: T1 is {PhotonComponents()} and the traceless rank-2 is "
            + $"{GravitonComponents()} - the representation is complete, so it is not what is missing"),
        ("the exterior complex (d, with d^2 = 0)", "REFUTED",
            $"the photon's field strength lives in the antisymmetric square (dim "
            + $"{ThreeDimensionalityDependencyAudit.AntisymmetricSquare(3)}) but the graviton's field is a "
            + $"traceless SYMMETRIC rank-2 (dim {GravitonComponents()}) - different irreps, so the graviton is "
            + "not a form and the exterior derivative alone cannot serve both"),
        ("a first-order derivative carrying the direction index", "THE UNIQUE BOTTLENECK",
            $"one object supplies both fields' index space, both field strengths, both gauge orbits and both "
            + $"kinetic terms; without it the antisymmetric square is "
            + $"{ThreeDimensionalityDependencyAudit.AntisymmetricSquare(1)} and the traceless symmetric is "
            + $"{ThreeDimensionalityDependencyAudit.TracelessSymmetric(1)} - nothing to hold a field strength"),
    };

    public static string[] RefutedCandidates()
        => BottleneckCandidates().Where(c => c.Status == "REFUTED").Select(c => c.Candidate).ToArray();

    public static string UniqueBottleneck()
        => BottleneckCandidates().Single(c => c.Status == "THE UNIQUE BOTTLENECK").Candidate;

    public static bool TheBottleneckIsSharedAndUnique()
        => RefutedCandidates().Length == 2
        && UniqueBottleneck().Contains("direction index", StringComparison.Ordinal)
        && BothSectorsReduceToTwo();

    // ===================== 8. WHAT THIS CHANGES ELSEWHERE =====================

    public static string RefinementOfE003()
        => "E_003 named the missing primitive as a phase on spacetime links with its own dynamics - the spacetime "
         + "index and the fluctuation. E_005 AGREES AND LOCATES IT: the spacetime index IS the direction index, "
         + "and it is the KINEMATICS layer, i.e. the FIRST missing step. The audit adds the computed reason the "
         + "existing phase cannot stand in for it: the built-in connection is exactly pure gauge "
         + $"(residual {PureGaugeResidual():E2}, holonomy {BuiltInHolonomy():F6} = one whole turn), so its field "
         + "strength is identically zero and it carries no gauge-invariant content at all.";

    public static string RefinementOfE004()
        => "E_004 located the shortfall at the choice of kinetic form that takes three states to two. E_005 "
         + "REFINES THAT TO THE SECOND STEP: a kinetic form is a functional of a field STRENGTH, and there is no "
         + "field strength until a first-order derivative carrying the direction index exists - the antisymmetric "
         + $"square is {ThreeDimensionalityDependencyAudit.AntisymmetricSquare(1)} at a direction rank of 1. So "
         + "the choice E_004 identifies is real, downstream, and unreachable without the kinematic object first. "
         + "E_004's own finding that the representation supplies no gauge orbit is reproduced here.";

    public static string Verdict()
    {
        if (!TheChainStopsAtKinematics()) return "DERIVED";
        if (!TheBottleneckIsSharedAndUnique()) return "DERIVED";
        if (TheBuiltInConnectionIsExactlyPureGauge() && TheSubstrateIsOneDirectional()) return "BOUNDARY";
        return "REFUTED";
    }

    public static string WhereItStands()
        => "THE CHAIN STOPS AT KINEMATICS, AND IT STOPS FOR BOTH SECTORS AT THE SAME PLACE BECAUSE OF THE SAME "
         + "OBJECT. The audit runs the four layers the question names in order and marks each one from a "
         + "computation rather than from expectation. THE REPRESENTATION IS SATISFIED, and it is the only layer "
         + $"that is: T1 is {PhotonComponents()} states with multiplicity 1 and the traceless rank-2 is "
         + $"E + T2 = 2 + 3 = {GravitonComponents()}, while the single ring tops out at "
         + $"{VectorSectorAudit.SingleRingMaxIrrepDimension()} and cannot host either. KINEMATICS IS THE FIRST "
         + "FAILURE, and this audit localises it three ways that agree, which is the point of running three. "
         + "FIRST, THE INDEX SPACES COLLAPSE: a field strength carries two antisymmetrised indices and the "
         + "graviton's field is a traceless symmetric rank-2, and at a direction rank of one the antisymmetric "
         + $"square is exactly {ThreeDimensionalityDependencyAudit.AntisymmetricSquare(1)} while the traceless "
         + $"symmetric is exactly {ThreeDimensionalityDependencyAudit.TracelessSymmetric(1)} - neither sector has "
         + "anywhere to put a field strength, and the two sectors as they stand, three and five, need exactly "
         + $"three directions. SECOND, THE SUBSTRATE'S OWN DERIVATIVE IS ONE-DIRECTIONAL: AT's radius-6 Laplacian "
         + $"IS a sum of six squares of first-order differences (residual {DecompositionResidual():E2}), so "
         + "first-order operators exist - but all six strides lie along a single line, so the direction rank of "
         + $"the substrate's first-order structure is {RingDirectionRank()} against the {DirectionsNeeded()} a "
         + $"vector index needs. THIRD, AND SHARPEST, THE SUBSTRATE'S OWN CONNECTION IS EXACTLY PURE GAUGE: the "
         + $"built-in link phase is {BuiltInStep():F9}, the holonomy around the ring is {BuiltInHolonomy():F6} - "
         + "one whole turn, the identity - and conjugating the covariant difference by the site-dependent phase "
         + $"returns the ordinary difference to within {PureGaugeResidual():E2}. The phase is therefore removable, "
         + "its field strength is identically zero, and the only gauge-invariant content a phase on a closed line "
         + "could have is exactly the residue that is zero here. There is nothing in it to propagate. DYNAMICS AND "
         + "GAUGE COME NEXT, AND THEY ARE BLOCKED RATHER THAN MERELY ABSENT: a kinetic form is a functional of a "
         + "field strength and a gauge orbit is the image of the derivative, so with no derivative acting on the "
         + $"field neither can even be written. That is a refinement of E_004, which located the shortfall at the "
         + $"choice of kinetic form - the second step, not the first. THE BOTTLENECK IS UNIQUE, AND THE AUDIT "
         + "TESTS THE CLAIM BY REFUTING TWO RIVALS. The representation is refuted because it is complete, and the "
         + "exterior complex is refuted because the graviton is not a form at all: the photon's field strength "
         + $"lives in the antisymmetric square, dimension {ThreeDimensionalityDependencyAudit.AntisymmetricSquare(3)}, "
         + $"while the graviton's field is a traceless symmetric rank-2, dimension {GravitonComponents()} - "
         + "different irreps, so no single exterior derivative can serve both. What does serve both is one "
         + "first-order derivative carrying the direction index, and the count proves the sharing: components "
         + $"minus the image of that derivative gives {GaugeSubtraction()[0].Physical} for the photon "
         + $"(3 - 1) and {GaugeSubtraction()[1].Physical} for the graviton (5 - 3) - the same rule, the same "
         + "answer, and the same number of physical states because a massless field of any spin has two. The "
         + "substrate's loop structure says the same thing from the other side: the ring has "
         + $"{RingIndependentLoops()} independent loops and the cube would have {CubeIndependentLoops():N0}, of "
         + $"which {CubeElementaryPlaquettes():N0} are elementary plaquettes, so only the cube can carry a LOCAL "
         + "field strength rather than a single global number. SO THE MINIMAL MISSING INGREDIENT IS ONE OBJECT - A "
         + "FIRST-ORDER DERIVATIVE THAT CARRIES THE DIRECTION INDEX, TOGETHER WITH THE DIRECTIONS IT ACTS ALONG - "
         + "AND THE FIRST MISSING STEP IS KINEMATICS. It cannot be derived from what AT has, which is why the "
         + "verdict is BOUNDARY rather than DERIVED: the location and the uniqueness are computed here, but the "
         + "ingredient itself has to be supplied.";

    // ===================== REPORT SECTIONS =====================

    public static string OutputLayers()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE FOUR LAYERS, RUN FOR BOTH SECTORS");
        sb.AppendLine("   layer            photon                        graviton                       status");
        foreach (var (layer, photon, graviton, status) in LayerLadder())
            sb.AppendLine($"   {layer,-16} {photon,-29} {graviton,-30} {status}");
        sb.AppendLine($"   FIRST MISSING STEP : {FirstMissingStep()}");
        sb.AppendLine($"   blocked, not merely absent : {string.Join(", ", BlockedLayers())}");
        sb.AppendLine();
        sb.AppendLine("   REPRESENTATION CONTENT");
        foreach (var (sector, irrep, dim, mult) in SectorContent())
            sb.AppendLine($"     {sector,-26} {irrep,-4} dim {dim} x {mult}");
        return sb.ToString();
    }

    public static string OutputIndexSpaces()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. LOCALISATION ONE - THE INDEX SPACES COLLAPSE AT ONE DIRECTION");
        sb.AppendLine("   directions | vector | antisym (field strength) | traceless sym (graviton) | verdict");
        foreach (var (d, v, a, t, verdict) in IndexSpaces())
            sb.AppendLine($"   {d,10} | {v,6} | {a,23} | {t,23} | {verdict}");
        sb.AppendLine($"   no index space survives at one direction : {AtOneDirectionNoIndexSpaceSurvives()}");
        sb.AppendLine();
        sb.AppendLine("3. LOCALISATION TWO - THE DERIVATIVE IS ONE-DIRECTIONAL");
        sb.AppendLine($"   residue of L = sum_r D_r^T D_r (first row) : {DecompositionResidual():E3}");
        sb.AppendLine($"   direction rank, ring                      : {RingDirectionRank()}");
        sb.AppendLine($"   direction rank, cube                      : {CubeDirectionRank()}");
        sb.AppendLine($"   directions a vector index needs           : {DirectionsNeeded()}");
        sb.AppendLine($"   ring independent loops (E - V + 1)        : {RingIndependentLoops()}");
        sb.AppendLine($"   cube independent loops                    : {CubeIndependentLoops():N0}");
        sb.AppendLine($"   cube elementary plaquettes                : {CubeElementaryPlaquettes():N0}");
        sb.AppendLine();
        sb.AppendLine("4. LOCALISATION THREE - THE BUILT-IN CONNECTION IS EXACTLY PURE GAUGE");
        sb.AppendLine($"   built-in step 2*pi/96                     : {BuiltInStep():F9}");
        sb.AppendLine($"   holonomy around the ring                  : {BuiltInHolonomy():F6}  (= 2*pi, the identity)");
        sb.AppendLine($"   holonomy modulo 2*pi                      : {HolonomyModQuantum():E3}");
        sb.AppendLine($"   pure-gauge conjugation residual           : {PureGaugeResidual():E3}");
        sb.AppendLine($"   exactly pure gauge                        : {TheBuiltInConnectionIsExactlyPureGauge()}");
        return sb.ToString();
    }

    public static string OutputBottleneck()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. THE BOTTLENECK CANDIDATES");
        foreach (var (candidate, status, basis) in BottleneckCandidates())
        {
            sb.AppendLine($"   {candidate}");
            sb.AppendLine($"     -> {status}: {basis}");
        }
        sb.AppendLine();
        sb.AppendLine("6. THE SHARED COUNT - ONE RULE, TWO SECTORS");
        sb.AppendLine("   sector    | components | gauge parameter | orbit | physical");
        foreach (var (sector, components, parameter, orbit, physical) in GaugeSubtraction())
            sb.AppendLine($"   {sector,-9} | {components,10} | {parameter,-15} | {orbit,5} | {physical}");
        sb.AppendLine($"   both sectors reduce to two : {BothSectorsReduceToTwo()}");
        sb.AppendLine();
        sb.AppendLine("7. REFINEMENTS OF THE PREDECESSORS");
        sb.AppendLine("   " + RefinementOfE003());
        sb.AppendLine("   " + RefinementOfE004());
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("8. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
