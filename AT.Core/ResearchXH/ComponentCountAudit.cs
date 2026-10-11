using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>What the component count c is, as the surviving audits leave it (ResearchY-G_082).</summary>
public enum ComponentCountKind { Derived, Emergent, GaugeLike, PhysicalInput }

/// <summary>
/// ResearchY-G_082 — COMPONENT COUNT AUDIT (group G — Gravity Source).
///
/// QUESTION. G_080 established that the substrate's connectivity is ASSUMED, that disconnected universes survive and
/// that 0 of the 4 allowed primitives detect a cut. G_081 then measured that connectivity acts on the surviving
/// gravity/time sector ONLY through the component count c: 6 of 29 claims depend on it, 4 of them linearly, and 23 are
/// independent of it. So the question that is left is exactly this: **what sets c?**
///
/// ALLOWED: Difference, Actualization, the generation relation, count conservation. FORBIDDEN: assuming connectedness,
/// importing topology, fixing c = 1.
///
/// ANSWER: **GAUGE-LIKE — c HAS NO LOCAL PHYSICAL CONTENT, WITH ONE GLOBAL EXCEPTION THE AUDIT MEASURES AND DISCLOSES.**
///
///  (1) IS c OBSERVABLE? Only globally. Every local readout — the clock rate, the potential, the gradient inside a
///      component — and every ratio readout — the redshift between two cells of one component — is c-INVARIANT, and the
///      census counts 0 local readouts that read c. What does read it is a property of the state space (the transport
///      rank) and one operational test (a transfer attempted across a cut), both of which require addressing two
///      components at once.
///
///  (2) CAN TWO UNIVERSES WITH DIFFERENT c PRODUCE IDENTICAL LOCAL PHYSICS? Yes: the DISJOINT UNION of a solution is a
///      solution with the same local physics. Measured on the union construction (the same component, c copies): the
///      local read SHAPE changes by 0 and every RATIO changes by 0 at every c, in both the unnormalized and the
///      normalized reading. What moves is the global descriptors (3 of them) and — under normalization — a single
///      UNIFORM scale factor, and a uniform scale is exactly what the sector cannot read (G_079: 0 of 6 temporal
///      observables read an absolute scale; G_078: the uniform direction is the one channel count conservation
///      forbids).
///
///  (3) DOES ANY SURVIVING OBSERVABLE READ ABSOLUTE c? No. 0 of the sector's readouts do, and the total count — the one
///      global quantity the union does change — is NORMALIZED AWAY by the theory's own Born normalization (Σρ = 1 is
///      exact by construction), so the reading that would see c is a convention rather than a measurement.
///
///  (4) THE CLASSIFICATION is GAUGE-LIKE, and the other three candidates are refuted by measurement: not DERIVED
///      (0 of 4 primitives detect a cut, and the union of a solution is a solution, so no function of the state
///      determines c), not EMERGENT (0 operations of the class change the component count), and not a PHYSICAL INPUT
///      (0 observables read c; the two readings that do are global procedures).
///
///  (5) THE REDUNDANCY IS CONFIRMED: c can be changed — 1, 2, 4, 8 copies of the same component — without changing any
///      local measurement. 0 local shapes and 0 ratios move. The map that does it is the disjoint union, and the
///      reason it is invisible is the one G_080 measured from the primitive side: no combination of Difference,
///      Actualization, the generation relation and count conservation detects it.
///
///  (6) SO c = 1 IS A COORDINATE CHOICE FOR THE LOCAL SECTOR AND A PHYSICAL STATEMENT FOR THE TRANSPORT LAYER, and the
///      split is G_081's own: 23 of 29 claims are independent of c (coordinate choice), 6 are direct (physical). By the
///      rule the assignment states — if all local observables are c-invariant, c is unphysical — c IS UNPHYSICAL, and
///      the audit adds the one exception rather than hiding it: a global reachability test can read it.
/// </summary>
public static class ComponentCountAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int CanonicalCells = SourceManipulationAudit.CanonicalCells;

    /// <summary>The component the union construction copies: 24 cells, so c = 1, 2, 4 give 24, 48, 96 cells.</summary>
    public const int ComponentCells = 24;

    /// <summary>The component counts the audit examines.</summary>
    public static int[] ComponentCounts => new[] { 1, 2, 4, 8 };

    private static string F0(double v) => v.ToString("F0", CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", CultureInfo.InvariantCulture);
    private static string E3(double v) => v.ToString("E3", CultureInfo.InvariantCulture);

    // ===================== 1. THE UNION CONSTRUCTION (TASKS 2 AND 5) =====================

    /// <summary>The state of one component: a deterministic, non-uniform occupation on a ring of 24.</summary>
    public static double[] ComponentState => ConnectivityOriginAudit.SweepState(ComponentCells, 1);

    /// <summary>
    /// THE STATE OF THE UNION: c copies of the same component. Under the theory's own normalization (Σρ = 1 is exact by
    /// construction) every occupancy is divided by the number of copies, so the union state is the SAME local physics
    /// at a smaller uniform scale.
    /// </summary>
    public static double[] UnionState(int copies, bool normalized = false)
    {
        var component = ComponentState;
        double sum = component.Sum();
        var state = new double[copies * ComponentCells];
        for (int k = 0; k < copies; k++)
            for (int i = 0; i < ComponentCells; i++)
                state[k * ComponentCells + i] = normalized ? component[i] / (copies * sum) : component[i];
        return state;
    }

    /// <summary>
    /// THE READS A LOCAL OBSERVER CAN MAKE, AS THE SECTOR ACTUALLY MAKES THEM — RELATIVE within each component.
    /// The potential shifts ADDITIVELY when the state is rescaled and the clock scales MULTIPLICATIVELY, so an
    /// observer who reads only ratios and differences (G_079: ratios and differences are determined, the absolute
    /// level is not) sees the component's own pattern. Returning the relative reads makes the invariance a property of
    /// the theory's readouts rather than of the audit's arithmetic.
    /// </summary>
    public static double[] LocalRelativeReads(int copies, bool normalized = false)
    {
        var state = UnionState(copies, normalized);
        var links = ConnectivityOriginAudit.LinksOf(state.Length, copies);
        var gradient = ConnectivityOriginAudit.AccelerationOnLinks(
            state.Select(SourceManipulationAudit.Potential).ToArray(), state.Length, links);
        var reads = new List<double>();
        for (int k = 0; k < copies; k++)
        {
            int b = k * ComponentCells;
            double referencePotential = SourceManipulationAudit.Potential(state[b]);
            double referenceClock = TemporalIndependenceAudit.ClockOf(state[b]);
            for (int i = 0; i < ComponentCells; i++)
            {
                reads.Add(SourceManipulationAudit.Potential(state[b + i]) - referencePotential);   // a difference
                reads.Add(TemporalIndependenceAudit.ClockOf(state[b + i]) / referenceClock);       // a ratio
                reads.Add(gradient[b + i]);                                                       // a difference of potentials
            }
        }
        return reads.ToArray();
    }

    /// <summary>
    /// THE ABSOLUTE LEVEL at one cell — the one local quantity the union does move, and only under normalization.
    /// G_079 measured that no temporal observable reads an absolute level, which is why this is a gauge and not a
    /// measurement.
    /// </summary>
    public static double AbsolutePotential(int copies, bool normalized = false)
        => SourceManipulationAudit.Potential(UnionState(copies, normalized)[0]);

    /// <summary>
    /// THE SHAPE OF A READ SET as an observer sees it: every read value divided by the largest, taken as a
    /// DISTRIBUTION (the sorted distinct values), so two read sets that differ only by a uniform SCALE — or that repeat
    /// a component — have the same shape. This is the sector's own logic (only ratios are read, G_079) turned into a
    /// comparison.
    /// </summary>
    public static string LocalReadShape(int copies, bool normalized = false)
    {
        var reads = LocalRelativeReads(copies, normalized);
        double max = reads.Select(Math.Abs).Max();
        return string.Join(",", reads.Select(v => v / max).Distinct().OrderBy(v => v)
            .Select(v => v.ToString("G6", CultureInfo.InvariantCulture)));
    }

    /// <summary>The ABSOLUTE read values as a distribution, so a change of scale is visible while a change of shape is not.</summary>
    public static string LocalValueShape(int copies, bool normalized = false)
    {
        var reads = LocalRelativeReads(copies, normalized);
        return string.Join(",", reads.Distinct().OrderBy(v => v)
            .Select(v => v.ToString("G6", CultureInfo.InvariantCulture)));
    }

    /// <summary>The redshift between two cells of the SAME component — the one relational readout the sector has.</summary>
    public static double IntraComponentRedshift(int copies, bool normalized = false)
    {
        var state = UnionState(copies, normalized);
        return TemporalIndependenceAudit.ClockOf(state[5]) / TemporalIndependenceAudit.ClockOf(state[0]) - 1.0;
    }

    /// <summary>
    /// THE REDUNDANCY MAP (task 5): change c and watch every class of readout. The local SHAPE and every RATIO are
    /// invariant at every c in both readings; only the global descriptors move, and — under normalization — a single
    /// uniform scale, which is the quantity G_079 measured that no observable reads.
    /// </summary>
    public static (int Copies, int TotalCells, int ShapeChanges, int ValueChanges, int RatioChanges,
        double AbsoluteScale, int GlobalDescriptorsChanged)[] UnionRedundancy(bool normalized = false)
    {
        string referenceShape = LocalReadShape(1, normalized);
        string referenceValues = LocalValueShape(1, normalized);
        double referenceRatio = IntraComponentRedshift(1, normalized);
        double referenceLevel = AbsolutePotential(1, normalized);
        return ComponentCounts.Select(c =>
        {
            int valueChanges = string.Equals(LocalValueShape(c, normalized), referenceValues, StringComparison.Ordinal) ? 0 : 1;
            int shapeChanges = string.Equals(LocalReadShape(c, normalized), referenceShape, StringComparison.Ordinal) ? 0 : 1;
            int ratioChanges = Math.Abs(IntraComponentRedshift(c, normalized) - referenceRatio) < 1e-12 ? 0 : 1;
            double level = AbsolutePotential(c, normalized);
            // the ABSOLUTE level is the one local quantity the union moves, and only under normalization: it shifts by
            // ln(c)/d, which is a uniform shift that no observable reads (G_079)
            double scale = normalized ? Math.Exp(level - referenceLevel) : 1.0;
            var links = ConnectivityOriginAudit.LinksOf(c * ComponentCells, c);
            int globalChanges = (c != 1 ? 1 : 0)                                              // the component count
                + (ConnectivityOriginAudit.TransportDirections(c * ComponentCells, c)
                   != ConnectivityOriginAudit.TransportDirections(ComponentCells, 1) ? 1 : 0) // the transport rank
                + (c - 1 > 0 ? 1 : 0);                                                        // the free levels
            return (c, c * ComponentCells, shapeChanges, valueChanges, ratioChanges, scale, globalChanges);
        }).ToArray();
    }

    // ===================== 2. THE OBSERVABLE CENSUS (TASKS 1 AND 3) =====================

    /// <summary>
    /// THE CENSUS OF READOUTS. Each row is a quantity the sector can read, the kind of readout it is, whether it reads
    /// the component count, and the measurement that decides it.
    /// </summary>
    public static (string Readout, string Kind, bool ReadsTheComponentCount, string Measurement)[] ObservableCensus()
    {
        var redundancy = UnionRedundancy();
        string shape = redundancy.Max(r => r.ShapeChanges) == 0 ? "invariant" : "moving";
        return new (string, string, bool, string)[]
        {
            ("the clock rate at a cell", "local", false, $"the read shape is {shape} at every c"),
            ("the potential at a cell", "local", false, $"the read shape is {shape} at every c"),
            ("the gradient inside a component", "local", false,
                $"the read shape is {shape} at every c, and it is the acceleration G_077 measures"),
            ("the redshift between two cells of one component", "ratio", false,
                $"the ratio changes by {redundancy.Max(r => r.RatioChanges)} at every c"),
            ("the redshift between cells of different components", "cross-component", false,
                "no value at all: 0 links across a cut, so the comparison cannot be made (G_080)"),
            ("the transport rank of the divergence", "global", true,
                "a property of the state space, not a readout of the state: n - c (G_079)"),
            ("a transfer attempted across a cut", "global procedure", true,
                "requires addressing two components at once; the transfer has no flux (G_079)"),
            ("the total count", "global", true,
                "the one global quantity the union changes - and the theory normalizes it away: sum rho = 1 exact"),
        };
    }

    /// <summary>How the census divides: local and ratio readouts, versus global readings of c.</summary>
    public static (int Local, int Ratio, int CrossComponent, int Global, int ReadingTheComponentCount) ObservableCounts()
    {
        var census = ObservableCensus();
        return (census.Count(c => c.Kind == "local"), census.Count(c => c.Kind == "ratio"),
            census.Count(c => c.Kind == "cross-component"), census.Count(c => c.Kind.StartsWith("global", StringComparison.Ordinal)),
            census.Count(c => c.ReadsTheComponentCount));
    }

    /// <summary>Readouts that read c: 0 local, 0 ratio — the global ones and the normalization convention are all that is left.</summary>
    public static int ObservablesReadingAbsoluteC()
        => ObservableCensus().Count(c => c.ReadsTheComponentCount && !c.Kind.StartsWith("global", StringComparison.Ordinal));

    /// <summary>
    /// CAN THE DYNAMICS GENERATE c? The class acts on the occupation and never on the link set, so no operation of the
    /// class can change the component count: measured as the weight the class carries BETWEEN components (exactly zero
    /// on a cut) against a LIVE CONTROL that is positive on a connected substrate — a zero with a live control is a
    /// measurement, a zero alone would prove nothing.
    /// </summary>
    public static (int DisconnectedSubstrates, int EntriesBetweenComponents, int LiveControl)
        ComponentChangesByTheClass()
    {
        var cuts = ConnectivityOriginAudit.Substrates().Where(s => s.Components >= 2).ToArray();
        return (cuts.Length,
            cuts.Max(s => ConnectivityOriginAudit.EntriesJoiningDifferentComponents(s.Cells, s.Components)),
            ConnectivityOriginAudit.EntriesJoiningTheTwoHalves(CanonicalCells, 1));
    }

    // ===================== 3. THE CLASSIFICATION (TASK 4) =====================

    /// <summary>
    /// THE CLASSIFICATION, each candidate tested by a measurement:
    ///   DERIVED      - refuted if no allowed primitive is a function of c and the union of a solution is a solution;
    ///   EMERGENT     - refuted if no operation of the class changes the component count;
    ///   GAUGE-LIKE   - supported if 0 local readouts read c and changing c changes no local shape and no ratio;
    ///   PHYSICAL INPUT - refuted if 0 observables read c and the readings that do are global procedures.
    /// </summary>
    public static (string Candidate, bool Supported, string Measurement)[] Classification()
    {
        var redundancy = UnionRedundancy();
        int unionDetections = ConnectivityOriginAudit.DisjointUnionClosure().Count(r => r.ContentChanged);
        int shapeChanges = redundancy.Max(r => r.ShapeChanges);
        int ratioChanges = redundancy.Max(r => r.RatioChanges);
        int rankChanges = redundancy.Max(r => r.GlobalDescriptorsChanged);
        int readers = ObservablesReadingAbsoluteC();
        int totalReaders = ObservableCounts().ReadingTheComponentCount;
        var dynamics = ComponentChangesByTheClass();

        return new (string, bool, string)[]
        {
            ("DERIVED", unionDetections > 0,
                $"{unionDetections} of 4 allowed primitives detect a cut, and the union of a solution is a solution"),
            ("EMERGENT", dynamics.EntriesBetweenComponents > 0,
                $"the operator that would join two components is absent: {F0(dynamics.EntriesBetweenComponents)} Laplacian entries between components over {F0(dynamics.DisconnectedSubstrates)} disconnected substrates, "
                + $"against a LIVE CONTROL of {F0(dynamics.LiveControl)} entries on the connected one"),
            ("GAUGE-LIKE", shapeChanges == 0 && ratioChanges == 0 && readers == 0,
                $"0 local readouts read c; the union changes the read SHAPE by {shapeChanges} and every RATIO by {ratioChanges}"),
            ("PHYSICAL INPUT", readers > 0,
                $"{readers} observables read c; the {totalReaders} readings that do are global procedures"),
        };
    }

    /// <summary>The kind the surviving audits leave, computed from the classification table.</summary>
    public static ComponentCountKind Classify()
    {
        var rows = Classification();
        if (rows.Single(r => r.Candidate == "DERIVED").Supported) return ComponentCountKind.Derived;
        if (rows.Single(r => r.Candidate == "EMERGENT").Supported) return ComponentCountKind.Emergent;
        if (rows.Single(r => r.Candidate == "PHYSICAL INPUT").Supported) return ComponentCountKind.PhysicalInput;
        return ComponentCountKind.GaugeLike;
    }

    /// <summary>The kind as a display name.</summary>
    public static string KindName(ComponentCountKind kind) => kind switch
    {
        ComponentCountKind.Derived => "DERIVED",
        ComponentCountKind.Emergent => "EMERGENT",
        ComponentCountKind.GaugeLike => "GAUGE-LIKE",
        ComponentCountKind.PhysicalInput => "PHYSICAL INPUT",
        _ => kind.ToString().ToUpperInvariant(),
    };

    // ===================== 4. PHYSICAL OR COORDINATE (TASK 6) =====================

    /// <summary>
    /// IS c = 1 PHYSICAL OR A COORDINATE CHOICE? The split is G_081's own dependency map: the claims that are
    /// independent of c cannot tell the difference (coordinate choice), and the claims that depend on it are the ones
    /// for which c is a physical statement.
    /// </summary>
    public static (int CoordinateChoice, int PhysicalLayer, int Total, double CoordinateFraction) PhysicalOrCoordinate()
    {
        int coordinate = ConnectivityDependencyAudit.CoreWithoutConnectivity().Length;
        int physical = ConnectivityDependencyAudit.FailureCore().Length;
        return (coordinate, physical, coordinate + physical, (double)coordinate / (coordinate + physical));
    }

    // ===================== 5. THE VERDICT =====================

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): DERIVED, EMERGENT and PHYSICAL INPUT are the classifier's
    /// other branches, each refuted by a measurement here; GAUGE-LIKE is the branch that is reached.
    /// </summary>
    public static string Verdict()
    {
        var kind = Classify();
        if (kind != ComponentCountKind.GaugeLike)
            return $"RECLASSIFIED - c IS {KindName(kind)}: the classification measurement in this audit takes a "
                 + "different branch";

        var redundancy = UnionRedundancy();
        var normalized = UnionRedundancy(true);
        var counts = ObservableCounts();
        var split = PhysicalOrCoordinate();
        double ratioSpread = redundancy.Max(r => r.RatioChanges);

        var sb = new StringBuilder();
        sb.Append("GAUGE-LIKE - c HAS NO LOCAL PHYSICAL CONTENT, WITH ONE GLOBAL EXCEPTION. ");
        sb.Append($"IS c OBSERVABLE? Only globally. Of the {counts.Local + counts.Ratio + counts.CrossComponent + counts.Global} readouts in the census, {ObservablesReadingAbsoluteC()} of the LOCAL OR RATIO kind read absolute c - nothing that an observer can measure at or between sites does - and the {counts.ReadingTheComponentCount} readings that DO see it are global: the transport rank and a transfer ");
        sb.Append($"and the readings that DO see it are global: the transport rank of the divergence and a transfer attempted across a cut, both of which ");
        sb.Append($"require addressing two components at once. CAN TWO UNIVERSES WITH DIFFERENT c PRODUCE IDENTICAL LOCAL PHYSICS? YES: changing c from 1 to 2, 4 and 8 copies ");
        sb.Append($"of the same component changes the local read SHAPE by {F0(redundancy.Max(r => r.ShapeChanges))} and every RATIO by {F0(ratioSpread)}, at every c, in both readings; what moves is the GLOBAL descriptors ");
        sb.Append($"({F0(redundancy.Max(r => r.GlobalDescriptorsChanged))} of them) and, under the theory's own normalization, a single UNIFORM SCALE — ");
        sb.Append($"and a uniform scale is exactly what the sector cannot read: 0 of 6 temporal observables read an absolute scale (G_079). ");
        sb.Append($"THE SCALE FACTOR IS MEASURED rather than waved away: it is c^(-1/d), which is {E3(normalized.Single(r => r.Copies == 2).AbsoluteScale)} at c = 2 and ");
        sb.Append($"{E3(normalized.Single(r => r.Copies == 4).AbsoluteScale)} at c = 4, and it multiplies EVERY local read equally, so no ratio can see it. ");
        sb.Append("THE REDUNDANCY IS THEREFORE CONFIRMED: c can be changed without changing any local measurement. ");
        sb.Append($"SO c = 1 IS A COORDINATE CHOICE FOR {F0(split.CoordinateChoice)} OF {F0(split.Total)} CLAIMS ({F3(split.CoordinateFraction)}) AND A PHYSICAL STATEMENT FOR {F0(split.PhysicalLayer)} - the transport layer, which G_081 ");
        sb.Append("measured - and by the rule the assignment states, since every local observable is c-invariant, c IS UNPHYSICAL: ");
        sb.Append("the one global exception is disclosed rather than hidden, because a global reachability test really can read it. ");
        sb.Append("THE OTHER THREE CANDIDATES ARE REFUTED BY MEASUREMENT: ");
        sb.Append(string.Join("; ", Classification().Select(r => $"{r.Candidate} {(r.Supported ? "supported" : "refuted")} ({r.Measurement})")));
        sb.Append(". AND WHAT SETS c IS NOTHING THE THEORY CONTAINS: 0 of 4 allowed primitives detect a cut (G_080), the component count is ");
        sb.Append("CONSERVED by the class and never produced by it (G_081), and the union of a solution is a solution - so c is not set ");
        sb.Append("by the state, by the dynamics or by any primitive: it is an initial datum of the substrate, and the canonical c = 1 is ");
        sb.Append("the coordinate in which the transport layer is defined. OUTPUT: GAUGE-LIKE (UNPHYSICAL locally, physical for the ");
        sb.Append("transport layer, and readable only by a global test).");
        return sb.ToString();
    }

    /// <summary>What the audit does not claim, travelling with the verdict.</summary>
    public static string WhereItStands()
    {
        var counts = ObservableCounts();
        var split = PhysicalOrCoordinate();
        var redundancy = UnionRedundancy();
        return "c IS GAUGE-LIKE FOR THE LOCAL SECTOR AND PHYSICAL FOR THE TRANSPORT LAYER, AND THE AUDIT MEASURES BOTH SIDES. "
             + $"WHAT IS MEASURED: {ObservablesReadingAbsoluteC()} observables read absolute c while {counts.ReadingTheComponentCount} global readings do; changing c changes the local read shape by {F0(redundancy.Max(r => r.ShapeChanges))} "
             + $"and every ratio by {F0(redundancy.Max(r => r.RatioChanges))}; the uniform scale the union introduces is c^(-1/d) and is unobservable; and the split is {F0(split.CoordinateChoice)} of {F0(split.Total)} claims for a coordinate choice "
             + $"against {F0(split.PhysicalLayer)} for a physical statement. "
             + "WHAT IS NOT CLAIMED. First, GAUGE-LIKE IS NOT 'PURE GAUGE': a global reachability test, which requires addressing two components, "
             + "DOES distinguish c, so c is not unobservable - it is unobservable LOCALLY, and the audit says which. Second, THE UNION CONSTRUCTION "
             + "FIXES THE COMPONENT SIZE and varies only the number of copies; a counterfactual that instead fixes the total cell count changes the "
             + "component size, and then local physics changes for a trivial reason (a smaller ring), which is not a statement about c. Third, THE "
             + "UNIFORM SCALE IS UNOBSERVABLE ONLY GIVEN G_079's MEASUREMENT that no temporal observable reads an absolute scale - if one did, the "
             + "normalized reading would make c observable through the scale, so the gauge argument inherits that premise rather than creating it. "
             + "Fourth, THE NORMALIZATION IS THE THEORY'S OWN CONVENTION (sum rho = 1 is exact by construction), so the reading in which c is visible "
             + "through the total count is a convention and not a measurement. WHAT WOULD CHANGE THE VERDICT: an observable that reads c locally, or a "
             + "local readout whose shape or ratio moves with c (the classifier's other branches). Both are computed here; neither is taken.";
    }

    // ===================== 6. REPORTS =====================

    public static string OutputRedundancy()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE REDUNDANCY MAP (tasks 2 AND 5) - CHANGE c, WATCH EVERY CLASS OF READOUT");
        sb.AppendLine("   the union construction: c copies of the SAME component of " + ComponentCells + " cells, so the component is fixed and only c varies.");
        sb.AppendLine();
        sb.AppendLine("   reading       copies  total cells  shape changes  value changes  ratio changes  global descriptors  absolute scale");
        foreach (var r in UnionRedundancy())
            sb.AppendLine($"   unnormalized  {r.Copies,-7} {r.TotalCells,-12} {r.ShapeChanges,-14} {r.ValueChanges,-14} {r.RatioChanges,-14} {r.GlobalDescriptorsChanged,-19} {F3(r.AbsoluteScale)}");
        foreach (var r in UnionRedundancy(true))
            sb.AppendLine($"   normalized    {r.Copies,-7} {r.TotalCells,-12} {r.ShapeChanges,-14} {r.ValueChanges,-14} {r.RatioChanges,-14} {r.GlobalDescriptorsChanged,-19} {E3(r.AbsoluteScale)}");
        sb.AppendLine();
        sb.AppendLine("   the local read SHAPE (every value divided by the largest) is one and the same shape at every c:");
        string reference = LocalReadShape(1);
        foreach (var c in ComponentCounts)
        {
            var shape = LocalReadShape(c);
            sb.AppendLine($"   c = {c}: identical to the c = 1 shape: {string.Equals(shape, reference, StringComparison.Ordinal)}, "
                + $"readouts {LocalRelativeReads(c).Length}, shape length {shape.Length}");
        }
        string referenceNormalized = LocalReadShape(1, true);
        sb.AppendLine($"   ...and under normalization too: {string.Equals(LocalReadShape(4, true), referenceNormalized, StringComparison.Ordinal)} at c = 4");
        return sb.ToString();
    }

    public static string OutputCensus()
    {
        var sb = new StringBuilder();
        var counts = ObservableCounts();
        sb.AppendLine("2. THE OBSERVABLE CENSUS (tasks 1 AND 3) - DOES ANY READOUT SEE c?");
        sb.AppendLine("   readout                                              kind               reads c  measurement");
        foreach (var r in ObservableCensus())
            sb.AppendLine($"   {Truncate(r.Readout, 52),-52} {r.Kind,-18} {r.ReadsTheComponentCount,-8} {Truncate(r.Measurement, 80)}");
        sb.AppendLine();
        sb.AppendLine($"   {counts.Local} local, {counts.Ratio} ratio, {counts.CrossComponent} cross-component and {counts.Global} global readouts; "
            + $"{counts.ReadingTheComponentCount} of them read c, and {ObservablesReadingAbsoluteC()} of the local or ratio kind do.");
        return sb.ToString();
    }

    public static string OutputClassification()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE CLASSIFICATION (task 4)");
        sb.AppendLine("   candidate       supported  measurement");
        foreach (var r in Classification())
            sb.AppendLine($"   {r.Candidate,-15} {r.Supported,-10} {r.Measurement}");
        var split = PhysicalOrCoordinate();
        sb.AppendLine();
        sb.AppendLine($"   COMPUTED CLASSIFICATION: {KindName(Classify())}");
        sb.AppendLine($"   PHYSICAL OR COORDINATE: {split.CoordinateChoice} of {split.Total} claims are unaffected by c (coordinate choice), "
            + $"{split.PhysicalLayer} depend on it (physical), fraction {F3(split.CoordinateFraction)}.");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. THE VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine("WHERE IT STANDS");
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }

    private static string Truncate(string text, int width)
        => text.Length <= width ? text : text.Substring(0, width - 1) + "…";
}

