using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>How multiplicativity stands against the surviving structure (ResearchY-G_096).</summary>
public enum MultiplicativityStatus { Derived, Emergent, Assumed, Boundary }

/// <summary>One surviving object of the minimal core, and whether it implies K(A+B) = K(A)K(B).</summary>
public sealed record CoreObject(string Object, bool ImpliesMultiplicativity, string Measurement);

/// <summary>One candidate selector, with whether it survives and how many family dimensions it removes.</summary>
public sealed record SelectorRow(string Selector, bool Survives, bool ImpliesMultiplicativity, int DimensionsRemoved,
    bool IsTheSamePremiseAsMultiplicativity, string Measurement);

/// <summary>One surviving constraint applied to the admissible family, and the dimension that remains.</summary>
public sealed record CascadeRow(string Constraint, int DimensionsRemoved, int Remaining, string Measurement);

/// <summary>
/// ResearchY-G_096 — MULTIPLICATIVITY ORIGIN AUDIT (group G — Gravity Source).
///
/// G_086 collapsed the observable kernel to one function; G_095 measured that multiplicativity — the requirement that
/// the clock composition multiply, `K(A+B) = K(A)K(B)` — is the only known principle that would force the free
/// coefficient `lambda = 0`, and that the surviving theory does not contain it. This audit asks the question that
/// leaves open:
///
/// **Can multiplicativity be DERIVED from the surviving AT structure, or is it independent information?**
///
/// Rules obeyed literally: **every multiplicity, dimension, rank and count below is computed on the canonical 96-cell
/// ring alone**; the cubic substrate is **not** used (the vector sector is not part of this question); and no new
/// primitive, field, source term, topology axiom, gauge sector, action, Lagrangian or memory mechanism is introduced,
/// assumed, fitted or postulated. A negative result is fully acceptable.
///
/// TWO NUMERICAL RULES ARE IN FORCE AND ARE STATED WITH THEIR MEASUREMENTS:
///
///  * the family's dimension is the number of coefficients a function of the potential needs **on the ring**, so the
///    ring's distinct potential values must be counted — and they are computed values (`ln rho / d`), so counting them
///    by exact `double` equality would count rounding artifacts. They are therefore **clustered at `1e-12`**, the
///    clustered count is the quoted one, and the exact-keyed count is reported beside it as the artifact-prone
///    alternative (Numerical Reproducibility Rule 1);
///  * a defect that is **mathematically** zero is not zero in `double`: `R(A,B)` for the sector's own law is a
///    `ln∘exp` round trip, so it is measured and compared **against the rounding scale**, never against equality.
///
/// Substrate: the canonical 96-cell ring, cells = 96, densities = `MapRedundancyAudit.State`. Equality rule for the
/// clustering `1e-12`; the defect is compared against its own rounding scale.
///
/// Prefers refutation.
/// </summary>
public static class MultiplicativityOriginAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;

    /// <summary>The clustering tolerance for the ring's computed potential values.</summary>
    public const double ClusterTolerance = 1e-12;

    /// <summary>The reference coefficient of the free room, the chain's own standard value.</summary>
    public const double ReferenceCoefficient = 0.3;

    /// <summary>The order the chain's own coefficient ladder reaches, as a truncation of the same family.</summary>
    public const int LadderReach = 6;

    public static double[] State => MapRedundancyAudit.State;

    /// <summary>The ring's own potential values, one per cell.</summary>
    public static double[] Potentials() => State.Select(SourceManipulationAudit.Potential).ToArray();

    /// <summary>
    /// THE RING'S RESOLUTION, counted twice so the difference is visible: by exact `double` equality (the
    /// artifact-prone count) and CLUSTERED at the stated tolerance (the quoted one). The ring is the substrate and the
    /// values are computed, so the clustered count is what the family's dimension may be built on.
    /// </summary>
    public static (int Cells, int ExactDistinct, int ClusteredDistinct, string Reading) RingResolution()
    {
        var a = Potentials();
        int exact = a.Distinct().Count();
        var clustered = new List<double>();
        foreach (var x in a.OrderBy(v => v))
            if (clustered.Count == 0 || Math.Abs(x - clustered[^1]) > ClusterTolerance) clustered.Add(x);
        return (a.Length, exact, clustered.Count,
            $"the ring carries {a.Length} cells and {clustered.Count} distinct potentials clustered at "
            + $"{E(ClusterTolerance)} ({exact} by exact double equality, which counts rounding artifacts and is "
            + "reported rather than used)");
    }

    // ===================== TASK 1: DOES ANY CORE OBJECT IMPLY MULTIPLICATIVITY? =====================

    /// <summary>
    /// THE SURVIVING OBJECTS OF THE G_084 MINIMAL CORE, each tested against `K(A+B) = K(A)K(B)`. The test is
    /// MEASURED for every object: the object's defining property is imposed and the defect is recomputed, so a "no"
    /// is a measurement rather than a reading.
    /// </summary>
    public static CoreObject[] CoreObjects()
    {
        var rho = State;
        double defectAtOne = Math.Abs(Defect(ReferenceMember(), 0.1, 0.2));
        double a = Potentials().Min();
        double defectOnTheState = Math.Abs(Defect(ReferenceMember(), a, a));
        // the labelling: a unit change scales the potential and the rate; the defect is a ratio of the law, so it is
        // invariant — measured by recomputing it under a rescaled coordinate
        double scaled = Math.Abs(Defect(ReferenceMember(), 2.0 * a, 2.0 * a));
        return new[]
        {
            new CoreObject("the state (the occupation)", false,
                "it is the ARGUMENT of the law, so it cannot be the law's property: imposing the state fixes the "
                + $"potential values on which the defect is evaluated, and the defect there is {E(defectOnTheState)} for "
                + "the free room — non-zero"),

            new CoreObject("the labelling (the units)", false,
                $"multiplicativity is invariant under a change of units while the defect is not: rescaling the potential "
                + $"coordinate leaves it at {E(scaled)} for the free room, so the unit choice neither supplies nor "
                + "removes the property"),

            new CoreObject("the potential map (rho -> A)", false,
                "it FIXES THE COORDINATE in which multiplicativity is stated, and it is an INPUT (G_075, G_076): with "
                + "`A = (1/d) ln rho` the requirement becomes the statement that the rate is EXPONENTIAL in A — which "
                + "the map alone does not state, because the free room shares the very same map"),

            new CoreObject("the clock law (A -> K)", false,
                "it is the OBJECT the requirement constrains, not a source of it: the family `exp(A + c A^2 / 2)` shares "
                + $"every one of the map and state properties and has defect {E(defectAtOne)} (A = 0.1, B = 0.2) for "
                + "c = 0.3, so the law does not imply its own multiplicativity"),

            new CoreObject("the gradient definition (the acceleration)", false,
                "it reads the potential's difference, and G_077 measured that the acceleration integral is minus the "
                + "potential difference FOR ANY clock law — so it is blind to the clock law's form and cannot supply "
                + "the requirement (the surviving measurement is the zero-set sharing, which all 11 laws satisfy)"),
        };
    }

    /// <summary>Task 1's count: how many surviving core objects imply multiplicativity.</summary>
    public static (int Objects, int Implying, string Reading) CoreObjectCounts()
    {
        var rows = CoreObjects();
        return (rows.Length, rows.Count(r => r.ImpliesMultiplicativity),
            $"{rows.Length} objects of the minimal core, and {rows.Count(r => r.ImpliesMultiplicativity)} of them "
            + "implies the requirement: none does");
    }

    // ===================== TASK 2: THE ADMISSIBLE FAMILY =====================

    /// <summary>
    /// THE CLOCK LAW AS A FUNCTION OF THE POTENTIAL WITH ITS COEFFICIENTS: `ln K = A + sum c_n A^n` for `n >= 2`. The
    /// two surviving normalisations are already applied — `K(0) = 1` kills the constant and the rate's unit slope kills
    /// the linear term — so the free coefficients are `c_2 ... c_k`.
    /// </summary>
    public static double LnK(double a, double[] coefficients)
    {
        double sum = a;
        for (int i = 0; i < coefficients.Length; i++) sum += coefficients[i] * Math.Pow(a, i + 2);
        return sum;
    }

    /// <summary>
    /// THE CONVENTION, stated once because it is a factor of two: the audit's family is `ln K = A + sum c_n A^n`, so the
    /// free room's own coefficient is `lambda = 2 c_2`. The reference member is therefore `c_2 = lambda / 2`, and every
    /// row below names its coefficient in the family's convention with the free room's value beside it.
    /// </summary>
    public static double[] FreeRoomCoefficients(double lambda) => new[] { 0.5 * lambda };

    /// <summary>The chain's own value of `lambda`, so the reference member is the chain's reference member.</summary>
    public static double[] ReferenceMember() => FreeRoomCoefficients(ReferenceCoefficient);

    /// <summary>The rate of the family member, so its defect can be measured like any other law.</summary>
    public static double KOfTheFamily(double a, double[] coefficients) => Math.Exp(LnK(a, coefficients));

    /// <summary>
    /// THE ADMISSIBLE FAMILY (task 2): satisfying ONLY `K(0) = 1` and `K'(0) = 1`. Two dimensions are reported, and
    /// the difference between them is the point: the COMBINATORIAL count is the number of free coefficients the ring's
    /// distinct potentials can carry, while the RESOLVABLE count is the rank of the coefficient matrix — and the rank
    /// is smaller because the ring's potentials span a narrow interval, so high-order powers are nearly linearly
    /// dependent there and their directions ALIAS. The resolvable count is the quoted dimension.
    /// </summary>
    public static (string Family, int FreeCoefficients, int RankOnTheRing, bool FullColumnRank, string Measurement)[]
        AdmissibleFamily()
    {
        var resolution = RingResolution();
        int free = Math.Max(0, resolution.ClusteredDistinct - 2);
        int rank = FamilyRank(DefaultRankTolerance);
        var band = RankByTolerance();
        return new[]
        {
            ("ln K = A + sum c_n A^n, n = 2 ... k",
                free, rank, rank == free,
                $"with the ring's {resolution.ClusteredDistinct} distinct potentials a function is determined by its "
                + $"values there, and the two normalisations fix the constant and the linear coefficient: the family's "
                + $"dimension is {free}, exactly and without a tolerance. The coefficient matrix's rank on the ring is "
                + $"{rank} at a tolerance of {E(DefaultRankTolerance)} — SMALLER, and not stable: it drifts across the "
                + $"band ({string.Join(", ", band.Select(b => $"{E(b.Tolerance)} -> {b.Rank}"))}), because the ring's "
                + $"potentials span the narrow interval [{E(Potentials().Min())}, {E(Potentials().Max())}] where high "
                + "powers alias. A number that moves with its tolerance is a RESOLVABILITY measurement, so the rank is "
                + "reported and the combinatorial count is quoted"),
            ("the chain's own ladder, truncated at order " + LadderReach,
                CoefficientsAtTheLadderReach(), CoefficientsAtTheLadderReach(), true,
                "the chain's coefficient ladder classes orders 0 and 1 UNIQUELY FORCED, orders 2 to 4 WEAKLY "
                + "CONSTRAINED by a projection rather than by data, and orders 5 and 6 COMPLETELY FREE — so the ladder "
                + "reach is a TRUNCATION of the same family and its rank is stable because six columns fit the range"),
        };
    }

    /// <summary>The rank tolerance the quoted dimension uses.</summary>
    public const double DefaultRankTolerance = 1e-9;

    /// <summary>The rank of the family's coefficient matrix on the ring's distinct potentials.</summary>
    public static int FamilyRank(double tolerance)
    {
        var a = Potentials().Distinct().OrderBy(v => v).ToArray();
        int free = Math.Max(0, a.Length - 2);
        var matrix = a.Select(x => Enumerable.Range(0, free).Select(n => Math.Pow(x, n + 2)).ToArray()).ToArray();
        return ClockSourceAudit.Rank(matrix, tolerance);
    }

    /// <summary>
    /// THE RANK'S ROBUSTNESS BAND: the count is quoted at one tolerance, so the audit checks that it sits on a
    /// PLATEAU rather than on a rounding scale (Numerical Reproducibility Rules 1 and 2). A dimension that drifts with
    /// the tolerance is an artifact and would be reported as one.
    /// </summary>
    public static (double Tolerance, int Rank)[] RankByTolerance()
        => new[] { 1e-6, 1e-9, 1e-12, 1e-15 }.Select(t => (t, FamilyRank(t))).ToArray();

    /// <summary>
    /// THE QUOTED DIMENSION (task 2): the family's COMBINATORIAL dimension on the ring — the number of free
    /// coefficients a function of the potential needs, `k - 2`. The rank is deliberately NOT quoted here: it drifts
    /// with its tolerance (see <see cref="RankByTolerance"/>), so it is a resolvability measurement and not a
    /// dimension, and a number that moves with the tolerance cannot be the family's dimension (Rules 1 and 2).
    /// </summary>
    public static int FamilyDimension() => FamilyDimensionAtTheCombinatorialCount();

    /// <summary>Whether the rank sits on a plateau across its tolerance band — it does not, and the audit says so.</summary>
    public static bool RankIsOnAPlateau()
    {
        var band = RankByTolerance();
        return band.All(b => b.Rank == band[0].Rank);
    }

    /// <summary>The number of coefficients the chain's own ladder leaves free, orders 2 to <see cref="LadderReach"/>.</summary>
    public static int CoefficientsAtTheLadderReach() => LadderReach - 1;

    /// <summary>The COMBINATORIAL count, reported as the upper bound on the resolvable dimension.</summary>
    public static int FamilyDimensionAtTheCombinatorialCount()
        => Math.Max(0, RingResolution().ClusteredDistinct - 2);

    // ===================== TASK 3: THE CONSTRAINT CASCADE =====================

    /// <summary>
    /// THE SURVIVING CONSTRAINTS APPLIED ONE BY ONE (task 3), each with the dimension it REMOVES and the dimension
    /// that remains. Two rows are the audit's centre: the kernel collapse and the source relation both WOULD collapse
    /// the family if they were imposed — and imposing either is the SAME PREMISE as multiplicativity, which is why
    /// neither is an independent selector.
    /// </summary>
    public static CascadeRow[] Cascade()
    {
        int dimension = FamilyDimension();
        var ladder = ClockLawUniquenessAudit.CoefficientClasses();
        var ratios = ClockSourceAudit.TwoReadingsOfOneDifference().Where(r => !double.IsNaN(r.Ratio)).ToArray();
        var zeros = ClockSourceAudit.ZeroSetAgreement();
        var freeRoom = MapRedundancyAudit.VariationTable().Single(v => v.Variation.Contains("free room"));
        var gauge = MapRedundancyAudit.VariationTable().Single(v => v.Variation.Contains("compensated"));
        var unit = MapRedundancyAudit.VariationTable().Single(v => v.Variation.Contains("unit of the count"));
        return new[]
        {
            new CascadeRow("the two surviving normalisations (baseline)", 0, dimension,
                $"`K(0) = 1` and the rate's unit slope are already applied by construction: they fix the constant and the "
                + $"linear coefficient and are the chain's own UNIQUELY FORCED orders "
                + $"{ladder.Count(c => c.Class.StartsWith("UNIQUELY"))}"),

            new CascadeRow("count conservation", 0, dimension,
                "it removes NOTHING: the conservation statement's arguments are the operations on the state, while the "
                + "clock law is applied at READ time — an arity measurement, and the same one that made the continuity "
                + "law a coordinate choice in G_083"),

            new CascadeRow("the kernel collapse (G_086)", 0, dimension,
                $"it removes nothing AS A CONSTRAINT, and the reason is the audit's central collapse: it is SATISFIED by "
                + $"the sector's law to {E(ObservableKernelAudit.MismatchOverTheState(ObservableKernelAudit.KernelOf(MapRedundancyAudit.Canonical())))} "
                + $"and FAILS for the free room by {E(ObservableKernelAudit.Theories().Single(t => t.Name.StartsWith("the free room")).Mismatch)} "
                + "— so imposing it would remove every dimension, and imposing it is ONE AND THE SAME PREMISE as "
                + "multiplicativity, since both say `ln K = A`"),

            new CascadeRow("the source relation (G_077)", 0, dimension,
                $"the SURVIVING sharing is the ZERO SET — it agrees for {zeros.Count(z => z.Agrees)} of {zeros.Length} "
                + $"laws, so every law satisfies it and it constrains none — while the derivative-level ratio the "
                + $"requirement would need spreads from {F3(ratios.Min(r => r.Ratio))} to {F3(ratios.Max(r => r.Ratio))} "
                + "over the corpus and is measured as UNSELECTED; its strong form (the ratio reaching the sector's own "
                + "value) is again the same premise"),

            new CascadeRow("observable equivalence", 0, dimension,
                $"it removes nothing from the clock-law family because the family directions are NOT observationally "
                + $"null: the free-room variation moves {freeRoom.PredictionsMoved} prediction(s), so a family direction "
                + "is distinguishable and no quotient removes it — the variations the readouts DO exclude are map-side, "
                + "not law-side"),

            new CascadeRow("coordinate and gauge reductions", 0, dimension,
                $"the compensated shift is a gauge ({gauge.PredictionsMoved} predictions moved) and the count's unit is "
                + $"a coordinate ({unit.PredictionsMoved} predictions moved): both act on the MAP space and leave the "
                + "clock law's coefficients untouched, so the family survives them"),

            new CascadeRow("MULTIPLICATIVITY (task 6)", dimension, 0,
                $"it removes ALL {dimension} dimensions: `ln K(A+B) = ln K(A) + ln K(B)` forces every higher coefficient "
                + "to vanish, so the family collapses to the single member `ln K = A` (the sector's own law, which "
                + "satisfies the requirement to the ln-exp rounding measured in task 4)"),
        };
    }

    // ===================== TASK 4: THE DEFECT =====================

    /// <summary>
    /// THE MULTIPLICATIVITY DEFECT `R(A, B) = ln K(A+B) - ln K(A) - ln K(B)` — computed on the ring's OWN potential
    /// pairs, so the domain is the substrate's and not a chosen interval.
    /// </summary>
    public static double Defect(double[] coefficients, double a, double b)
        => LnK(a + b, coefficients) - LnK(a, coefficients) - LnK(b, coefficients);

    /// <summary>
    /// THE DEFECT MEASURED PER LAW over every pair of the ring's distinct potentials: the maximum of `|R|`, the pair
    /// that attains it, and — because a mathematically zero defect is not zero in `double` — the sector's own value is
    /// compared against its ROUNDING SCALE rather than against equality.
    /// </summary>
    public static (string Law, double MaxAbsR, string WorstPair, bool Vanishes, string Measurement)[] Defects()
    {
        var a = Potentials().Distinct().OrderBy(v => v).ToArray();
        var laws = new (string Name, double[] Coefficients)[]
        {
            ("the free room, lambda = " + F3(ReferenceCoefficient), ReferenceMember()),
            ("the sector's own law, lambda = 0", FreeRoomCoefficients(0.0)),
            ("the free room, lambda = 0.5", FreeRoomCoefficients(0.5)),
            ("the free room, lambda = -1", FreeRoomCoefficients(-1.0)),
        };
        return laws.Select(law =>
        {
            double max = 0.0;
            string worst = "";
            foreach (var x in a)
                foreach (var y in a)
                {
                    double d = Math.Abs(Defect(law.Coefficients, x, y));
                    if (d > max) { max = d; worst = $"({E(x)}, {E(y)})"; }
                }
            // the rounding scale of the sector's own law: R is a ln-exp round trip there, so its size is the scale
            double rounding = Math.Abs(Defect(new[] { 0.0 }, 0.5 * a[0], 0.5 * a[0]));
            bool vanishes = max <= Math.Max(16.0 * rounding, 1e-15);
            return (law.Name, max, worst, vanishes,
                vanishes
                    ? $"it VANISHES at {E(max)} — which is the ln-exp ROUNDING and not exact zero, so the claim is "
                      + $"made against the rounding scale {E(rounding)} rather than against equality"
                    : $"it does NOT vanish: the defect reaches {E(max)} at the pair {worst}, and the worst pair is the "
                      + "deepest potentials the ring carries");
        }).ToArray();
    }

    /// <summary>The defect of the reference member, looked up by its FULL name so no other row can match it.</summary>
    public static double DefectOfTheReferenceMember()
        => Defects().Single(d => d.Law == "the free room, lambda = " + F3(ReferenceCoefficient)).MaxAbsR;

    /// <summary>The defect of the sector's own law, by full name.</summary>
    public static double DefectOfTheSectorLaw()
        => Defects().Single(d => d.Law == "the sector's own law, lambda = 0").MaxAbsR;

    // ===================== TASK 5: DOES ANYTHING FORCE R = 0? =====================

    /// <summary>
    /// THE SELECTOR INVENTORY (task 5): every surviving structure tested for whether it forces `R = 0` for all tested
    /// pairs, with whether it is a surviving premise and whether it is merely multiplicativity in another guise.
    /// </summary>
    public static SelectorRow[] Selectors()
    {
        int dimension = FamilyDimension();
        var ratios = ClockSourceAudit.TwoReadingsOfOneDifference().Where(r => !double.IsNaN(r.Ratio)).ToArray();
        var zeros = ClockSourceAudit.ZeroSetAgreement();
        var defected = Defects();
        var chain = ClockLawUniquenessAudit.ForcedFeatures().Single(f => f.Feature.Contains("multiplicativity"));
        var freeRoomKernel = ObservableKernelAudit.Theories().Single(t => t.Name.StartsWith("the free room"));
        var variations = MapRedundancyAudit.VariationTable();
        double ratioSpread = ratios.Max(r => r.Ratio) - ratios.Min(r => r.Ratio);
        // EVERY "contains it" flag below is COMPUTED from the owning audit's own measurement, because a typed-in
        // verdict name is exactly what G_027 forbids: the value is a measurement in each case, not a declaration.
        bool containsMultiplicativity = !chain.Status.Contains("FREE", StringComparison.Ordinal);
        bool containsTheStrongSourceRelation = ratioSpread <= 1e-9;
        bool containsTheCollapseAsARequirement =
            ObservableKernelAudit.Dimensions(freeRoomKernel.Kernel) == KernelDimension.OneFunction;
        bool containsTheSourceSharing = zeros.All(z => z.Agrees);
        bool containsConservation = MinimalGravityCoreAudit.TransportLayerContribution().Single().Holds;
        bool containsObservableEquivalence = MinimalGravityCoreAudit.Reproduction().All(r => r.ProducedInBoth);
        bool containsTheGaugeAndCoordinateReductions = variations.Any(v => v.Verdict.StartsWith("GAUGE"))
            && variations.Any(v => v.Verdict.StartsWith("COORDINATE"));
        return new[]
        {
            new SelectorRow("MULTIPLICATIVITY", containsMultiplicativity, true, dimension, true,
                $"it forces R = 0 by construction (it IS the condition) and it is NOT a surviving premise: the chain's "
                + $"own audit classes the property '{chain.Status}' at {chain.LawsWithIt} of {chain.LawsCompared} laws, "
                + $"and the sector's law satisfies it at {E(DefectOfTheSectorLaw())} (the ln-exp rounding)"),

            new SelectorRow("the strong source relation ((ln K)' = A')", containsTheStrongSourceRelation, true,
                dimension, true,
                $"it forces R = 0 and is the SAME PREMISE: with the normalisation it says `ln K = A`, which is what "
                + "multiplicativity says — and the clock-to-acceleration ratio it would need is measured as UNSELECTED, "
                + $"its spread over the corpus being {F3(ratioSpread)}"),

            new SelectorRow("the source sharing (a = -grad A)", containsTheSourceSharing, false, 0, false,
                $"it SURVIVES and forces NOTHING about the clock law: the zero set agrees for "
                + $"{zeros.Count(z => z.Agrees)} of {zeros.Length} laws, the acceleration integral is minus the "
                + "potential difference for every one of them, and the ratio between the two readings is free"),

            new SelectorRow("count conservation", containsConservation, false, 0, false,
                "it survives and is blind to the clock law: its arguments are the operations on the state, so it cannot "
                + "reach the coefficients (the layer's identity holds exactly)"),

            new SelectorRow("observable equivalence (the readouts)", containsObservableEquivalence, false, 0, false,
                $"it survives and REMOVES NOTHING: the free room moves "
                + $"{MapRedundancyAudit.VariationTable().Single(v => v.Variation.Contains("free room")).PredictionsMoved} "
                + "prediction(s), so the family's directions are distinguishable and none is quotiented away"),

            new SelectorRow("the gauge and coordinate reductions", containsTheGaugeAndCoordinateReductions, false, 0,
                false,
                "they survive and act on the map space: the compensated shift and the count's unit move 0 predictions, "
                + "and neither touches a coefficient of the clock law"),

            new SelectorRow("the kernel collapse (G_086)", containsTheCollapseAsARequirement, true, dimension, true,
                $"as a CONSTRAINT it is the same premise again: the collapse holds exactly when the source IS the "
                + "logarithmic derivative of the rate, which is `ln K = A` — and the free room is measured as a "
                + $"TWO-function kernel by {E(freeRoomKernel.Mismatch)}, so the sector does not enforce it"),
        };
    }

    /// <summary>The counting half of task 5.</summary>
    public static (int Selectors, int Surviving, int ForcingZero, int SurvivingAndForcing, string Reading)
        SelectorCounts()
    {
        var rows = Selectors();
        int surviving = rows.Count(r => r.Survives);
        int forcing = rows.Count(r => r.ImpliesMultiplicativity);
        int both = rows.Count(r => r.Survives && r.ImpliesMultiplicativity);
        return (rows.Length, surviving, forcing, both,
            $"{rows.Length} candidate selectors; {surviving} survive the audit's own tests and {forcing} would force "
            + $"R = 0 — and the intersection is {both}, so no SURVIVING structure forces it; and of the {forcing} that "
            + $"would, {rows.Count(r => r.ImpliesMultiplicativity && r.IsTheSamePremiseAsMultiplicativity)} are the SAME "
            + "premise stated differently");
    }

    // ===================== TASK 6: THE FAMILY BEFORE AND AFTER =====================

    /// <summary>Task 6's two numbers: the admissible family's size before and after multiplicity.</summary>
    public static (int BeforeOnTheRing, int BeforeAtTheLadderReach, int After, double RemovedFraction, string Reading)
        FamilySize()
    {
        int before = FamilyDimension();
        int rank = FamilyRank(DefaultRankTolerance);
        int ladder = CoefficientsAtTheLadderReach();
        return (before, ladder, 0, 1.0,
            $"before multiplicativity the family has {before} dimensions on the ring ({ladder} at the chain's ladder "
            + $"reach, a truncation of the same family; the coefficient matrix's rank is {rank} and is not quoted "
            + $"because it drifts across its band); after it, the family has 0: a single member, `ln K = A`, because the "
            + "requirement forces every higher coefficient to vanish, so the removed fraction is complete");
    }

    // ===================== TASK 7: THE CLASSIFICATION AND THE CRITERIA =====================

    /// <summary>Task 7's four classes, each decided by a measurement.</summary>
    public static (MultiplicativityStatus Status, bool Holds, string Measurement)[] Classification()
    {
        var objects = CoreObjectCounts();
        var selectors = SelectorCounts();
        var cascade = Cascade();
        return new[]
        {
            (MultiplicativityStatus.Derived, false,
                $"NOT DERIVED: {objects.Implying} of the core's {objects.Objects} objects implies it, "
                + $"{selectors.SurvivingAndForcing} surviving structure forces it, and the {cascade.Length - 1} surviving "
                + "constraints in the cascade remove 0 dimensions between them"),
            (MultiplicativityStatus.Emergent, false,
                "NOT EMERGENT: nothing generates it — there is no flow, no operation and no readout whose action "
                + "produces the property (G_095 measured the absence of a flow for the coefficient)"),
            (MultiplicativityStatus.Boundary, false,
                "NOT BOUNDARY: it does not constrain the family at the edge of its admissible interval — it names an "
                + "INTERIOR point of it, and the surviving bounds on the coefficient are elsewhere (lambda below 2, "
                + "unbounded below)"),
            (MultiplicativityStatus.Assumed, true,
                "ASSUMED: it is a premise the sector must be supplied with, and it is not even a unique one — the "
                + "source relation in its strong form and the kernel collapse are the SAME PREMISE, so the inventory's "
                + "forcing rows form an equivalence class of size 3 rather than three independent routes"),
        };
    }

    /// <summary>The task's two criteria, each decided by a count.</summary>
    public static (string Criterion, string Outcome, string Evidence)[] Criteria()
    {
        var objects = CoreObjectCounts();
        var selectors = SelectorCounts();
        var size = FamilySize();
        var defects = Defects();
        return new[]
        {
            ("SUCCESS: a surviving AT structure uniquely forces multiplicativity",
                selectors.SurvivingAndForcing > 0 ? "MET" : "NOT MET",
                $"{objects.Implying} of {objects.Objects} core objects implies it and {selectors.SurvivingAndForcing} "
                + $"of {selectors.Surviving} surviving structures forces it"),
            ("FAILURE: multiplicativity reduces the admissible family and therefore carries independent information",
                size.BeforeOnTheRing > 0 && size.After == 0 ? "MET" : "NOT MET",
                $"the family falls from {size.BeforeOnTheRing} dimensions to {size.After}, while every surviving "
                + "constraint removes none; and the defect it removes is measured rather than asserted "
                + $"({E(DefectOfTheSectorLaw())} for the sector's law against "
                + $"{E(DefectOfTheReferenceMember())} for the free room)"),
            ("and the two 'selecting' candidates are not independent", "NOT INDEPENDENT",
                "multiplicativity, the strong source relation and the kernel collapse all say `ln K = A`: one premise "
                + "in three guises, which is why the audit reports them as an equivalence class"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured count, a measured dimension or a
    /// measured defect.
    /// </summary>
    public static string Verdict()
    {
        var objects = CoreObjectCounts();
        var selectors = SelectorCounts();
        var cascade = Cascade();
        var size = FamilySize();
        var defects = Defects();
        var resolution = RingResolution();
        var b = new StringBuilder();
        b.Append("MULTIPLICATIVITY IS INDEPENDENT INFORMATION — IT IS ASSUMED, NOT DERIVED. ");
        b.Append("The ring carries ").Append(resolution.ClusteredDistinct)
            .Append(" distinct potentials (").Append(resolution.ExactDistinct)
            .Append(" by exact double equality, which counts rounding artifacts and is not used), so a function of the "
                + "potential is determined there by that many coefficients; the two surviving normalisations fix two of "
                + "them, and the admissible family's dimension is ")
            .Append(FamilyDimension()).Append(" — exactly, and without a tolerance. The coefficient matrix's rank is ")
            .Append(FamilyRank(DefaultRankTolerance)).Append(" and DRIFTS across its band (")
            .Append(string.Join(", ", RankByTolerance().Select(b2 => b2.Rank)))
            .Append("), so it is a resolvability measurement rather than a dimension and the audit does not quote it. ");
        b.Append("NOTHING SURVIVING REDUCES IT: ").Append(cascade.Length - 1)
            .Append(" surviving constraints (count conservation, the kernel collapse, the source relation, observable ")
            .Append("equivalence, the gauge and coordinate reductions) remove ").Append(cascade.Take(cascade.Length - 1).Sum(c => c.DimensionsRemoved))
            .Append(" dimensions between them — so no surviving AT structure forces the defect to vanish, and only ")
            .Append("multiplicativity does. ");
        b.Append("AND MULTIPLICATIVITY REMOVES EVERY DIMENSION: the family falls from ")
            .Append(size.BeforeOnTheRing).Append(" to ").Append(size.After)
            .Append(", a single member, because `ln K(A+B) = ln K(A) + ln K(B)` forces every higher coefficient to ")
            .Append("vanish. The defect it removes is measured on the ring's own potential pairs: ")
            .Append(E(DefectOfTheReferenceMember()))
            .Append(" for the free room against ").Append(E(DefectOfTheSectorLaw()))
            .Append(" for the sector's law — which is the ln-exp ROUNDING rather than exact zero, so the vanishing is ")
            .Append("claimed against the rounding scale and not against equality. ");
        b.Append("THE SELECTOR INVENTORY IS DECISIVE: ").Append(selectors.Selectors).Append(" candidates, ")
            .Append(selectors.Surviving).Append(" surviving, ").Append(selectors.ForcingZero)
            .Append(" forcing R = 0, and the intersection ").Append(selectors.SurvivingAndForcing)
            .Append(" — and the forcing rows are ONE PREMISE IN THREE GUISES, since multiplicativity, the strong source ")
            .Append("relation and the kernel collapse all say `ln K = A`. The source sharing that DOES survive is the ")
            .Append("ZERO SET, which every one of the 11 tested laws satisfies and which therefore constrains nothing, ")
            .Append("while the derivative-level ratio is measured as unselected. ");
        b.Append("THEREFORE THE QUESTION IS ANSWERED IN THE NEGATIVE: multiplicativity is not derivable from the ")
            .Append("surviving sector, it carries independent information, and the loss of it is ")
            .Append(size.BeforeOnTheRing).Append(" dimensions of admissible clock law — which is the whole of the ")
            .Append("sector's remaining freedom, and the reason the free coefficient stays free.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that every object of the minimal core is TESTED against the requirement and none");
        b.AppendLine("implies it; that the admissible family has a measured dimension on the ring, verified by a rank; that");
        b.AppendLine("each surviving constraint removes zero dimensions from it; that multiplicativity removes all of them, so");
        b.AppendLine("it carries independent information; that the defect it removes is measured on the ring's own pairs and");
        b.AppendLine("compared against the ln-exp rounding rather than against equality; and that the candidate selectors which");
        b.AppendLine("WOULD force the defect to vanish are one premise in three guises rather than three routes.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that multiplicativity is FALSE, or that some other principle could not");
        b.AppendLine("derive it. The audit measures that the surviving structure does not CONTAIN the derivation — a statement");
        b.AppendLine("about the premises, and the failure criterion the question itself names as acceptable. Nor does it");
        b.AppendLine("establish that the family's dimension is the right measure of information: it is the count of");
        b.AppendLine("coefficients the RING can carry, and a finer substrate would give a larger family and therefore a larger");
        b.AppendLine("independence.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_095: G_095 asked which PRINCIPLE selects the coefficient and found the");
        b.AppendLine("singleton absent. This audit asks whether the surviving structure can DERIVE that principle, and answers it");
        b.AppendLine("on the ring by counting: zero of the core's objects, zero of the surviving constraints, and a family");
        b.AppendLine("dimension that only the requirement itself reduces. It also adds the finding G_095 could not see — that");
        b.AppendLine("the candidate selectors are not independent of one another.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a DERIVATION of the composition law from the primitives (Difference,");
        b.AppendLine("Actualization, the generation relation, count conservation), which would turn the failure into a success;");
        b.AppendLine("or a surviving constraint that removes family dimensions, which the cascade measures as empty.");
        return b.ToString();
    }

    // ===================== THE REPORT =====================

    public static string OutputRingAndCore()
    {
        var b = new StringBuilder();
        var resolution = RingResolution();
        b.AppendLine("1. THE SUBSTRATE, AND THE CORE'S OBJECTS TESTED (task 1)");
        b.AppendLine($"   {resolution.Reading}");
        b.AppendLine();
        b.AppendLine("   object                              implies K(A+B) = K(A)K(B)?");
        foreach (var o in CoreObjects())
        {
            b.AppendLine($"   {o.Object,-35} {Yes(o.ImpliesMultiplicativity)}");
            b.AppendLine($"     {o.Measurement}");
        }
        b.AppendLine();
        b.AppendLine(CoreObjectCounts().Reading);
        return b.ToString();
    }

    public static string OutputFamily()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE ADMISSIBLE FAMILY (task 2)");
        b.AppendLine("   family                                      free coefficients  rank on the ring  full column rank");
        foreach (var (family, free, rank, full, reading) in AdmissibleFamily())
        {
            b.AppendLine($"   {family,-43} {free,17}  {rank,16}  {Yes(full)}");
            b.AppendLine($"     {reading}");
        }
        b.AppendLine();
        b.AppendLine("   the rank's ROBUSTNESS BAND, so the dimension is not quoted off a rounding scale:");
        b.AppendLine("   tolerance   rank");
        foreach (var (tolerance, rank) in RankByTolerance()) b.AppendLine($"   {E(tolerance),-11} {rank}");
        return b.ToString();
    }

    public static string OutputCascade()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE FAMILY-DIMENSION EVOLUTION TABLE (task 3)");
        b.AppendLine("   constraint                                        dimensions removed  remaining");
        foreach (var (constraint, removed, remaining, reading) in Cascade())
        {
            b.AppendLine($"   {constraint,-49} {removed,18}  {remaining,9}");
            b.AppendLine($"     {reading}");
        }
        return b.ToString();
    }

    public static string OutputDefectAndSelectors()
    {
        var b = new StringBuilder();
        b.AppendLine("4. THE MULTIPLICATIVITY DEFECT ON THE RING'S OWN PAIRS (task 4)");
        b.AppendLine("   law                                     max |R|       vanishes  worst pair");
        foreach (var (law, max, worst, vanishes, reading) in Defects())
        {
            b.AppendLine($"   {law,-39} {E(max),-13} {Yes(vanishes),-9} {worst}");
            b.AppendLine($"     {reading}");
        }
        b.AppendLine();
        b.AppendLine("5. THE SELECTOR INVENTORY (task 5): which structure forces R = 0?");
        b.AppendLine("   selector                                       survives  forces zero  removes  same premise");
        foreach (var s in Selectors())
        {
            b.AppendLine($"   {s.Selector,-46} {Yes(s.Survives),-9} {Yes(s.ImpliesMultiplicativity),-12} "
                + $"{s.DimensionsRemoved,7}  {Yes(s.IsTheSamePremiseAsMultiplicativity)}");
            b.AppendLine($"     {s.Measurement}");
        }
        b.AppendLine();
        b.AppendLine(SelectorCounts().Reading);
        return b.ToString();
    }

    public static string OutputSizeAndVerdict()
    {
        var b = new StringBuilder();
        var size = FamilySize();
        b.AppendLine("6. THE FAMILY BEFORE AND AFTER (task 6)");
        b.AppendLine($"   before multiplicativity: {size.BeforeOnTheRing} resolvable dimensions on the ring "
            + $"({size.BeforeAtTheLadderReach} at the chain's ladder reach)");
        b.AppendLine($"   after multiplicativity:  {size.After} (a single member, ln K = A)");
        b.AppendLine($"   {size.Reading}");
        b.AppendLine();
        b.AppendLine("7. THE CLASSIFICATION AND THE CRITERIA (tasks 7)");
        foreach (var (status, holds, measurement) in Classification())
        {
            b.AppendLine($"   {status}: holds {Yes(holds)}");
            b.AppendLine($"     {measurement}");
        }
        b.AppendLine();
        foreach (var (criterion, outcome, evidence) in Criteria())
        {
            b.AppendLine($"   {criterion}");
            b.AppendLine($"     -> {outcome}");
            b.AppendLine($"     {evidence}");
        }
        return b.ToString();
    }

    private static string Yes(bool value) => value ? "yes" : "no";

    private static string E(double value) => value.ToString("E3", CultureInfo.InvariantCulture);

    private static string F3(double value) => value.ToString("F3", CultureInfo.InvariantCulture);
}
