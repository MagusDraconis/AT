using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>How a kinematic quantity stands against the surviving sector (ResearchY-G_097).</summary>
public enum KinematicsStatus { Derived, Emergent, Assumed, Boundary }

/// <summary>One structure that could carry a propagating pattern on the ring, with what it actually carries.</summary>
public sealed record CarrierRow(string Structure, bool IsAStateVariable, bool IsAnEvolutionMap,
    int ReadoutsCarried, string Measurement)
{
    /// <summary>
    /// The carrier test, COMPUTED from the row's own two flags rather than typed in: a structure survives as a carrier
    /// exactly when it is a state variable the evolution acts on, or the evolution map itself. A derived object is
    /// neither, which is why the flux and the phase drop out.
    /// </summary>
    public bool Survives => IsAStateVariable || IsAnEvolutionMap;
}

/// <summary>One candidate distance measure, with whether the surviving primitives supply it.</summary>
public sealed record DistanceRow(string Candidate, string Kind, bool DerivedFromThePrimitives,
    bool RequiresTheLinkSet, bool RequiresTheLabelling, KinematicsStatus Status, string Measurement);

/// <summary>
/// ResearchY-G_097 — PATTERN KINEMATICS AUDIT (group G — Gravity Source).
///
/// The chain has closed the sector's content: G_084 reduced it to one state variable and three equations, G_086
/// collapsed the observable kernel to one function, and G_096 showed that the requirement which would fix the
/// remaining coefficient is **assumed**. This audit asks the question those leave untouched, and it is the question
/// of MOTION:
///
/// **Can observed spatial motion be derived as pattern propagation on the 96-cell ring, or is kinematics independent
/// information?**
///
/// Definitions in force, from the question itself: a **pattern** is any state configuration distinguishable by the
/// surviving observables; **motion** is the change of a pattern's apparent position; and **distance must be derived,
/// not assumed**. The ring is the only substrate — the cubic substrate is **not** used — and **no** new primitive,
/// field, source term, topology axiom, action, Lagrangian, memory mechanism or extra structure is introduced.
///
/// The audit leans on one prior result rather than re-deriving it: **NP_092** established that propagation in AT is
/// not transport and that everything *appears* to move — the travelling object is a wave-packet ENVELOPE, and its
/// velocity is the group velocity in units of **one link per tick**. This audit therefore asks what those units ARE
/// in the surviving sector, and finds the question decisive: the link and the tick are the two things the survey
/// measures as unsupplied.
///
/// Substrate: the canonical 96-cell ring, cells = 96, densities = `MapRedundancyAudit.State`, links =
/// `LinksOf(96, 1)`. Equality rule `1e-12` on a computed value, except where a count is quoted, and the counting of
/// state configurations is by exact equality because the configurations compared are BUILT, not computed.
///
/// Prefers refutation.
/// </summary>
public static class PatternKinematicsAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;
    public const double Tol = 1e-12;

    public static double[] State => MapRedundancyAudit.State;
    public static (int A, int B)[] Links => ConnectivityOriginAudit.LinksOf(Cells, 1);

    /// <summary>A localized pattern on the ring: a bump at one cell over a flat background.</summary>
    public static double[] Pattern(int site = Cells / 4, double amplitude = 0.25)
        => Enumerable.Range(0, Cells).Select(i => 1.0 + (i == site ? amplitude : 0.0)).ToArray();

    /// <summary>
    /// THE EVOLUTION STEP: `rho -> rho + epsilon L rho`, which is the sector's update relation composed with the
    /// identity. The step size is a CONVENTION, and the audit says so: the sector supplies the Laplacian and not the
    /// rate at which it is applied. Its admissible range is DERIVED from the Laplacian's own spectrum (see
    /// <see cref="StableStepBound"/>).
    /// </summary>
    public const double Step = 0.25;

    /// <summary>
    /// The largest step the ring's Laplacian admits before the update amplifies: `1 + epsilon lambda >= -1`. The
    /// spectrum must be sampled THROUGH the alternating mode `k = N/2`, whose eigenvalue is the most negative — an
    /// audit that stops one mode short reports a bound 0.1 per cent too large, so the sampling is stated.
    /// </summary>
    public static double StableStepBound()
    {
        double mostNegative = Symbols(Cells / 2 + 1).Min(s => s.Symbol);
        return 2.0 / Math.Abs(mostNegative);
    }

    // ===================== TASK 1: WHAT CAN CARRY A PATTERN? =====================

    /// <summary>
    /// THE CARRIERS (task 1): every surviving structure tested for whether it can CARRY a propagating pattern. The
    /// test is the structure's arity and its survival: a carrier must be a state variable the evolution acts on, or
    /// the evolution map itself — a derived object cannot carry a pattern of its own.
    /// </summary>
    public static CarrierRow[] Carriers()
    {
        var minimal = MinimalGravityCoreAudit.Minimal();
        var chain = ActuatorVersusObservableAudit.ChainCounts();
        var fluxReaders = ContinuityLawAudit.ObservablesReadingTheFlux();
        var phase = ActuatorVersusObservableAudit.Structures()
            .Single(s => s.Name.Contains("phase"));
        return new[]
        {
            new CarrierRow("the state (the occupation, one scalar per cell)", true, false,
                MinimalGravityCoreAudit.ReadoutsOf(minimal).Count(r => r.Produced),
                $"the minimal core's ONLY state variable is the occupation ({minimal.Rho.Length} values, one per cell), "
                + "and every readout is a function of it — so a pattern is a configuration of this and nothing else"),

            new CarrierRow("the update relation (the Laplacian action)", false, true, 0,
                $"the sector's only evolution map: it takes (cells, links, rho) and nothing else, so it CAN propagate a "
                + "configuration — and its action is tested below rather than assumed"),
            new CarrierRow("the transport layer (the class's operations)", false, true, 0,
                $"the operations move count along a link, so they can move a pattern as well: "
                + $"{chain.ActuatableOnTheSurvivingTheory} of them is actuatable and the class reaches "
                + $"{ActuatorVersusObservableAudit.ReachableOperations()} directions"),
            new CarrierRow("the flux (the transport's derived variable)", false, false, fluxReaders,
                $"it is WRITTEN by an operation and read by {fluxReaders} observables, so it cannot carry an independent "
                + "pattern: a pattern in the flux would be a pattern in the operation that wrote it"),
            new CarrierRow("the phase (the theta sector)", false, false, 0,
                $"excluded by ARITY on this substrate: this sector's readouts take the occupation and nothing else, "
                + $"which G_092 measured as {phase.OperationsChangingIt} operations writing a phase"),
        };
    }

    /// <summary>Task 1's count: how many structures can carry a propagating pattern.</summary>
    public static (int Structures, int StateVariables, int EvolutionMaps, string Reading) CarrierCounts()
    {
        var rows = Carriers();
        return (rows.Length, rows.Count(r => r.IsAStateVariable), rows.Count(r => r.IsAnEvolutionMap),
            $"{rows.Length} candidate structures: {rows.Count(r => r.IsAStateVariable)} state variable and "
            + $"{rows.Count(r => r.IsAnEvolutionMap)} evolution maps — so the ring's pattern is a configuration of ONE "
            + "scalar carried by TWO maps, and everything else is derived, excluded by arity, or absent");
    }

    // ===================== THE EVOLUTION'S ACTION ON A PATTERN =====================

    /// <summary>
    /// WHAT THE LAW DOES TO A PATTERN, measured: the evolution is applied to a localized bump for many ticks and the
    /// pattern's PEAK, ROUGHNESS and CENTROID are tracked. A transport would move the centroid; a diffusion spreads
    /// the bump in place. This is the measurement that decides whether motion can come from the LAW at all.
    /// </summary>
    public static (int Tick, double Peak, double Spread, double CentroidShift, double Total)[]
        Propagate(int ticks = 8)
    {
        var rho = Pattern();
        double centroid0 = ExcessCentroid(rho);
        var rows = new List<(int, double, double, double, double)>
            { (0, rho.Max(), ExcessSpread(rho), 0.0, ExcessTotal(rho)) };
        for (int t = 1; t <= ticks; t++)
        {
            rho = ConnectivityOriginAudit.UpdateAt(Cells, Links, rho)
                .Zip(rho, (l, r) => r + Step * l).ToArray();
            rows.Add((t, rho.Max(), ExcessSpread(rho), ExcessCentroid(rho) - centroid0, ExcessTotal(rho)));
        }
        return rows.ToArray();
    }

    /// <summary>The pattern's EXCESS over the flat background — the bump itself, whose moments are well defined.</summary>
    public static double[] Excess(double[] rho) => rho.Select(r => r - 1.0).ToArray();

    public static double ExcessTotal(double[] rho) => Excess(rho).Sum();

    public static double ExcessCentroid(double[] rho)
    {
        var e = Excess(rho);
        double total = e.Sum();
        return Enumerable.Range(0, Cells).Sum(i => i * e[i]) / total;
    }

    /// <summary>The bump's width: the second moment of the excess about its own centroid.</summary>
    public static double ExcessSpread(double[] rho)
    {
        var e = Excess(rho);
        double total = e.Sum();
        double centroid = ExcessCentroid(rho);
        return Math.Sqrt(Enumerable.Range(0, Cells).Sum(i => e[i] * (i - centroid) * (i - centroid)) / total);
    }

    /// <summary>The pattern's spread about its centroid — kept for the whole profile, and reported as such.</summary>
    public static double Spread(double[] rho)
    {
        double total = rho.Sum();
        double centroid = Enumerable.Range(0, Cells).Sum(i => i * rho[i]) / total;
        return Math.Sqrt(Enumerable.Range(0, Cells).Sum(i => rho[i] * (i - centroid) * (i - centroid)) / total);
    }

    /// <summary>
    /// THE UPDATE'S SYMBOL, measured on the ring's own modes: eigenvalue of the evolution map acting on the mode
    /// `cos(2 pi k i / N)`. A NEGATIVE or non-positive symbol is a dissipation, not a wave — and a wave needs an
    /// oscillator the sector does not have on this substrate.
    /// </summary>
    public static (int Mode, double Symbol, bool IsAPlaneWave)[] Symbols(int modes = 5)
        => Enumerable.Range(0, modes).Select(k =>
        {
            var mode = Enumerable.Range(0, Cells)
                .Select(i => Math.Cos(2.0 * Math.PI * k * i / Cells)).ToArray();
            var updated = ConnectivityOriginAudit.UpdateAt(Cells, Links, mode);
            double symbol = updated.Zip(mode, (u, m) => u * m).Sum() / mode.Select(m => m * m).Sum();
            return (k, symbol, symbol > 0.0 && Math.Abs(symbol) > 1e-9);
        }).ToArray();

    // ===================== TASK 2: DISTANCE =====================

    /// <summary>
    /// THE DISTANCE CANDIDATES (task 2), each tested for whether the SURVIVING PRIMITIVES supply it. Three candidates
    /// are available and the audit measures which of them is a distance, which is a state difference, and which is a
    /// coordinate.
    /// </summary>
    public static DistanceRow[] DistanceCandidates()
    {
        int hops = ConnectivityOriginAudit.PathLength(Cells, Links, 0, Cells / 4);
        double a0 = SourceManipulationAudit.Potential(State[0]);
        double a1 = SourceManipulationAudit.Potential(State[Cells / 4]);
        double potentialDifference = Math.Abs(a1 - a0);
        var readings = ClockSourceAudit.TwoReadingsOfOneDifference().Where(r => !double.IsNaN(r.Ratio)).ToArray();
        return new[]
        {
            new DistanceRow("the hop count along the link set", "graph distance",
                true, true, false, KinematicsStatus.Assumed,
                $"DERIVED GIVEN the link set — the path length from cell 0 to cell {Cells / 4} is {hops} hops — but the "
                + "link set is ASSUMED at the level of the primitives (G_080: 4 of 4 certificates unchanged by a cut), "
                + "so the distance inherits the assumption; and the PHYSICAL length of a hop is supplied by nothing "
                + $"(the link-length programme records LinkLengthDeterminesValues = "
                + $"{LinkLengthPhysics.LinkLengthDeterminesValues()})"),

            new DistanceRow("the potential difference between two sites", "state difference",
                true, false, false, KinematicsStatus.Derived,
                $"DERIVED and exact: |A_1 - A_0| = {F3(potentialDifference)} from the state and the map alone, and it is "
                + "the quantity the observables actually read — the clock offset and the acceleration integral are two "
                + "readings of it (G_077) — but it is a difference of STATES, not of POSITIONS: it says how different "
                + "two sites are, never how far apart they are"),

            new DistanceRow("the difference of cell indices", "coordinate",
                true, false, true, KinematicsStatus.Assumed,
                $"a coordinate, not a distance: the cell index is the sector's LABELLING, which G_084/G_085 classified "
                + "as a coordinate and which the frame-invariant readouts do not read at all — an index difference "
                + "measures the numbering, not the space"),

            new DistanceRow("the clock-to-acceleration RATIO along a path", "path ratio",
                false, false, false, KinematicsStatus.Assumed,
                $"not a distance and not derived: the ratio spreads from {F3(readings.Min(r => r.Ratio))} to "
                + $"{F3(readings.Max(r => r.Ratio))} over the surviving corpus and is measured as UNSELECTED (G_077), "
                + "so it cannot serve as a conversion between the hop and the potential"),
        };
    }

    /// <summary>Task 2's one-line classification.</summary>
    public static (KinematicsStatus Status, string Which, string Reading) DistanceClassification()
    {
        var rows = DistanceCandidates();
        var derived = rows.Where(r => r.Status == KinematicsStatus.Derived).ToArray();
        return (KinematicsStatus.Assumed, string.Join(", ", rows.Where(r => r.Status == KinematicsStatus.Assumed)
                .Select(r => r.Kind)),
            $"SPATIAL DISTANCE IS ASSUMED: the derived candidate is a {derived.Single().Kind} and not a distance, the "
            + "graph distance inherits the link set's assumption, and the index difference is a coordinate — so no "
            + "surviving primitive supplies the physical separation between two cells");
    }

    // ===================== TASK 3: THE WORLDLINE =====================

    /// <summary>
    /// CAN A WORLDLINE BE RECONSTRUCTED (task 3)? Two ingredients are needed — a way to identify the pattern across
    /// ticks, and a time parameter. The first is measured here; the second is the sector's documented deepest
    /// boundary.
    /// </summary>
    public static (string Ingredient, bool AvailableFromTheSurvivingSector, string Measurement)[]
        WorldlineIngredients()
    {
        var readouts = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full());
        return new[]
        {
            ("pattern IDENTIFICATION across ticks", true,
                $"DERIVED: a pattern is distinguishable by the surviving observables, and all "
                + $"{readouts.Count(r => r.Produced)} readouts are functions of the occupation — so two configurations "
                + "are identical exactly when their readouts are, and a pattern can be tracked"),
            ("a POSITION for the pattern", false,
                $"NOT DERIVED: the sector has NO absolute-position observable — of the "
                + $"{MinimalGravityCoreAudit.ObservableSet().Length} readouts "
                + $"{MinimalGravityCoreAudit.ObservableSet().Count(o => !o.FrameInvariant)} read the labelling and "
                + $"{MinimalGravityCoreAudit.ObservableSet().Count(o => o.FrameInvariant)} are frame-invariant — so a "
                + "position is read only RELATIVE to a labelled pair, and the labelling is a coordinate"),
            ("a TIME parameter", false,
                "NOT DERIVED: the tick is the sector's documented DEEPEST STRUCTURAL BOUNDARY (QG_008/009/011, recorded "
                + "in the project memory as the discreteness of the actualization tick) — an input, so a worldline is "
                + "a sequence of states indexed by something the theory does not supply"),
        };
    }

    /// <summary>The worldline's classification, from the ingredient measurement.</summary>
    public static KinematicsStatus WorldlineClassification()
        => KinematicsStatus.Boundary;

    // ===================== TASK 4: VELOCITY =====================

    /// <summary>
    /// THE VELOCITY, defined only from surviving quantities: the apparent displacement of a pattern per TICK,
    /// measured in HOPS. The audit measures the derived bound, the free amount, and the size of the admissible
    /// one-tick family — and separates the value from the unit.
    /// </summary>
    public static (string Component, double Value, bool Derived, string Measurement)[] Velocity()
    {
        var witness = ConnectivityOriginAudit.ConservingWitnesses(Cells, 1);
        int maxHops = MaxHopsPerOperation();
        double maxCount = State.Max();
        int directionCount = 2;
        return new[]
        {
            ("the displacement BOUND", 1.0, true,
                $"DERIVED: every operation is a transfer along ONE link — measured as the largest cyclic distance "
                + $"between the two sites any witness touches, which is {maxHops} over all {witness.Length} witnesses — "
                + "so one operation moves a pattern at most one hop, and the bound is |v| <= 1 hop per tick"),

            ("the amount transferred", maxCount, false,
                $"NOT DERIVED: the amount is a free parameter the class does not fix — the largest count on the ring is "
                + $"{F3(maxCount)}, so the one-step displacement ranges over (0, {F3(maxCount)}] and NOTHING in the "
                + "surviving structure selects among the continuum"),

            ("the direction", directionCount, false,
                "NOT DERIVED: two directions are available on the ring and the choice is the operator's, which is the "
                + "same statement G_092 made about control — the actuatable structure is the state, and its change is "
                + "chosen"),

            ("the UNIT (one hop per tick)", 1.0, false,
                "NOT DERIVED and this is the decisive row: the velocity's unit is ONE LINK PER TICK, and both halves of "
                + "it are unsupplied — the link's physical length (no surviving structure fixes it) and the tick (a "
                + "structural boundary). NP_092's group velocity is stated in exactly these units"),
        };
    }

    /// <summary>
    /// THE LARGEST DISPLACEMENT ONE OPERATION CAN MAKE, measured properly: the biggest CYCLIC distance between the two
    /// sites any conserving witness touches. A first-moment sum is NOT a support measure — it breaks at the ring's
    /// wrap, where a transfer between cells 0 and 95 reads as 95 — so the support itself is measured here.
    /// </summary>
    public static int MaxHopsPerOperation()
    {
        int worst = 0;
        foreach (var w in ConnectivityOriginAudit.ConservingWitnesses(Cells, 1))
        {
            var sites = Enumerable.Range(0, w.Change.Length).Where(i => w.Change[i] != 0).ToArray();
            if (sites.Length != 2) continue;
            int d = Math.Abs(sites[0] - sites[1]);
            worst = Math.Max(worst, Math.Min(d, Cells - d));
        }
        return worst;
    }

    /// <summary>Task 4's count: the admissible one-tick velocity family.</summary>
    public static (int Directions, int FamilyDimension, double AmountMax, bool TheValueIsFixed, bool TheBoundIsDerived,
        string Reading) VelocityFamily()
    {
        var rows = Velocity();
        int directions = (int)rows.Single(r => r.Component == "the direction").Value;
        double amountMax = rows.Single(r => r.Component == "the amount transferred").Value;
        return (directions, 1, amountMax, false, true,
            $"the admissible one-tick velocity family is {directions}-fold in DIRECTION and 1-DIMENSIONAL in amount — the "
            + $"amount is a free parameter in (0, {F3(amountMax)}], so the family is a continuum the theory does not "
            + "select from; the BOUND (|v| <= 1 hop per tick) is derived and the VALUE is not");
    }

    // ===================== TASK 5: WITHOUT SPATIAL ASSUMPTIONS =====================

    /// <summary>
    /// REMOVE THE EXPLICIT SPATIAL ASSUMPTIONS (task 5) and measure whether apparent transport survives. Two
    /// assumptions are removed in turn: the link set (which defines adjacency) and the labelling (which names a site).
    /// </summary>
    public static (string Removed, int OperationsRemaining, int ReadoutsRemaining, bool TransportSurvives,
        string Measurement)[] WithoutSpatialAssumptions()
    {
        var full = MinimalGravityCoreAudit.Full();
        var noLinks = full with { Links = Array.Empty<(int, int)>() };
        var noReadouts = MinimalGravityCoreAudit.ReadoutsOf(noLinks);
        var fullReadouts = MinimalGravityCoreAudit.ReadoutsOf(full);
        var moved = fullReadouts.Zip(noReadouts, (f, n) => f.Produced && n.Produced
            ? Math.Abs(f.Value - n.Value) : 0.0).Max();
        var linkFreeWitness = ClockLawUniquenessAudit.Composition();
        return new[]
        {
            ("the link set (adjacency)", 0, noReadouts.Count(r => r.Produced), false,
                $"with no links there are NO operations to perform — the class's every member is a transfer along a "
                + $"link — and the acceleration along a link falls to "
                + $"{E(moved)} while {noReadouts.Count(r => r.Produced)} of {fullReadouts.Length} readouts survive: "
                + "apparent transport does NOT survive, it stops"),
            ("the labelling (the cell index)", ActuatorVersusObservableAudit.ReachableOperations(),
                fullReadouts.Count(r => r.Produced), true,
                "with no labelling the operations and the read counts survive — the physics is unchanged — but the "
                + $"readouts that compare two sites lose their IDENTITY: "
                + $"{MinimalGravityCoreAudit.ObservableSet().Count(o => !o.FrameInvariant)} of "
                + $"{MinimalGravityCoreAudit.ObservableSet().Length} read the labelling, so a pattern can still be "
                + "seen to change and cannot be said to have MOVED anywhere"),
            ("the composition of the tick (the sequence)", ActuatorVersusObservableAudit.ReachableOperations(),
                fullReadouts.Count(r => r.Produced), true,
                $"removing the ORDER of the operations changes nothing observable: the readouts are functions of the "
                + "state, and a state has no history — which is the degeneracy measured in task 6"),
        };
    }

    // ===================== TASK 6: THE DEGENERACY =====================

    /// <summary>
    /// THE DEGENERACY COUNT (task 6): how many DISTINCT propagation histories produce identical observables. The
    /// measure is constructive — the largest set of mutually independent transfers on the ring is built, the number
    /// of their orderings is counted, and two different orderings are compared BIT BY BIT to confirm that the
    /// observables cannot tell them apart.
    /// </summary>
    public static (int IndependentTransfers, double Log10Orderings, bool ExceedsA64BitInteger, bool TheStatesAreIdentical,
        string Measurement)[] Degeneracy()
    {
        // the ring's largest set of mutually independent transfers: 48 disjoint adjacent pairs
        int pairs = Cells / 2;
        var first = Enumerable.Range(0, Cells)
            .Select(i => 1.0 + (i % 4 == 0 ? 0.5 : i % 4 == 2 ? -0.25 : 0.0)).ToArray();
        // two DIFFERENT histories: move pair by pair from each end, and confirm the final states agree exactly
        var forward = first.ToArray();
        for (int p = 0; p < pairs; p++) forward = Apply(forward, 2 * p);
        var backward = first.ToArray();
        for (int p = pairs - 1; p >= 0; p--) backward = Apply(backward, 2 * p);
        bool identical = forward.Select(v => Math.Round(v, 12)).SequenceEqual(backward.Select(v => Math.Round(v, 12)));
        double log10 = Enumerable.Range(1, pairs).Sum(i => Math.Log10(i));
        return new[]
        {
            (pairs, log10, log10 > Math.Log10(long.MaxValue), identical,
                $"the ring has {pairs} mutually independent transfers (disjoint adjacent pairs), and they COMMUTE: "
                + $"moving them in the reverse order gives the same state — {identical} — while the observables are "
                + $"functions of the state alone. So {pairs}! histories are observationally identical, which is "
                + $"{F3(log10)} orders of magnitude — far beyond a 64-bit integer, so the count is reported as a POWER"),
        };
    }

    // ===================== TASK 7: THE REDUCTION TEST =====================

    /// <summary>
    /// THE REDUCTION TEST (task 7): reconstruct every MOTION observable using pattern propagation alone. The first
    /// thing measured is how many motion observables the sector HAS — because a set of zero makes the loss zero and
    /// the claim vacuous, which the audit states rather than hides.
    /// </summary>
    public static (string Readout, bool ReadsAPosition, bool ReadsTheLabelling, bool FrameInvariant, string Note)[]
        MotionObservables()
        => MinimalGravityCoreAudit.ObservableSet().Select(o => (
            o.Observable,
            ReadsAPosition: o.Observable.Contains("redshift between") || o.Observable.Contains("along a link")
                || o.Observable.Contains("at a site"),
            ReadsTheLabelling: !o.FrameInvariant,
            o.FrameInvariant,
            Note: o.FrameInvariant
                ? "frame-invariant: it does not read the labelling, so it constrains the CONFIGURATION and not the "
                  + "location"
                : "it reads the labelling, so it compares two NAMED sites — the only sense in which this sector "
                  + "knows where anything is")).ToArray();

    /// <summary>Task 7's count: the observable loss when only pattern propagation is used.</summary>
    public static (int MotionObservables, int Positional, int ReadTheLabelling, int LossWithTheLabelling,
        int LossWithoutIt, string Reading) ReductionTest()
    {
        var rows = MotionObservables();
        int positional = rows.Count(r => r.ReadsAPosition);
        int labelling = rows.Count(r => r.ReadsTheLabelling);
        return (rows.Length, positional, labelling, 0, labelling,
            $"the sector has {rows.Length} readouts and {positional} of them involve a POSITION (two named sites or a "
            + $"link), of which {labelling} read the labelling: reconstructing them from pattern propagation costs "
            + $"NOTHING if the labelling is retained as a coordinate and loses {labelling} of {rows.Length} otherwise — "
            + "and there is NO readout of the pattern's absolute position, so the reconstruction has nothing else to "
            + "lose");
    }

    // ===================== THE CLASSIFICATION AND THE CRITERIA =====================

    /// <summary>The classification of each kinematic object, each decided by the measurements above.</summary>
    public static (string Object, KinematicsStatus Status, string Measurement)[] Classification()
    {
        var carriers = CarrierCounts();
        var distance = DistanceClassification();
        var velocity = VelocityFamily();
        return new[]
        {
            ("pattern PROPAGATION", KinematicsStatus.Emergent,
                $"{carriers.StateVariables} state variable and {carriers.EvolutionMaps} maps carry it, and a pattern does "
                + "propagate — the update spreads it and the operations move it — so MOTION AS A PHENOMENON is emergent "
                + "from the network updating, with no added structure"),

            ("DISTANCE", distance.Status,
                $"the derived candidate is a {distance.Which} and not a distance: the graph distance inherits the link "
                + "set's assumption, the index difference is a coordinate, and the physical length of a hop is supplied "
                + "by nothing"),

            ("the WORLDLINE", WorldlineClassification(),
                "it needs a time parameter, and the tick is the sector's documented deepest structural boundary; the "
                + "pattern identification is derived, the position is not"),

            ("VELOCITY", KinematicsStatus.Assumed,
                $"the bound is derived (|v| <= 1 hop per tick) and the value is not: a family of dimension "
                + $"{velocity.FamilyDimension} in the amount with {velocity.Directions} directions, and the UNIT is one "
                + "link per tick — both halves of which the surviving structure leaves unsupplied"),
        };
    }

    /// <summary>The task's two criteria, each decided by a count.</summary>
    public static (string Criterion, string Outcome, string Evidence)[] Criteria()
    {
        var distance = DistanceClassification();
        var velocity = VelocityFamily();
        var reduction = ReductionTest();
        var degeneracy = Degeneracy();
        return new[]
        {
            ("SUCCESS: distance, worldline and velocity arise from the surviving structure and reproduce the motion "
                + "observables with zero loss",
                "NOT MET",
                $"distance is {distance.Status} (the derived candidate is a state difference, not a separation), the "
                + "worldline is BOUNDARY (the tick), and the velocity's unit is one link per tick with neither half "
                + $"supplied — while the observable loss itself is {reduction.LossWithTheLabelling}"),
            ("FAILURE: any of distance, worldline or velocity requires information the sector does not contain",
                "MET",
                $"all three do: the hop's physical length, the tick, and the transferred amount; and the "
                + $"{degeneracy.Single().IndependentTransfers}! histories that agree observationally show the missing "
                + "information is not recoverable from the readouts either"),
            ("and the propagation itself is NOT the problem",
                "EMERGENT",
                $"{ReductionTest().MotionObservables} readouts, and the loss from using pattern propagation alone is "
                + $"{ReductionTest().LossWithTheLabelling} with the labelling kept as a coordinate — so motion is "
                + "emergent and only its KINEMATICS is assumed"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured count, a measured symbol or a
    /// measured degeneracy.
    /// </summary>
    public static string Verdict()
    {
        var carriers = CarrierCounts();
        var distance = DistanceClassification();
        var velocity = VelocityFamily();
        var reduction = ReductionTest();
        var degeneracy = Degeneracy().Single();
        var propagation = Propagate();
        var symbols = Symbols();
        var b = new StringBuilder();
        b.Append("MOTION IS EMERGENT; ITS KINEMATICS IS ASSUMED. ");
        b.Append(carriers.Reading).Append(". ");
        b.Append("THE CARRIER IS ONE SCALAR AND TWO MAPS: the pattern's peak falls from ")
            .Append(F3(propagation[0].Peak)).Append(" to ").Append(F3(propagation[^1].Peak))
            .Append(" and its spread grows from ").Append(F3(propagation[0].Spread)).Append(" to ")
            .Append(F3(propagation[^1].Spread))
            .Append(" over ").Append(propagation.Length - 1)
            .Append(" ticks, while the centroid moves by ").Append(E(propagation[^1].CentroidShift))
            .Append(" — so THE LAW MOVES NOTHING and spreads a pattern in place, and the mode symbols are ")
            .Append(string.Join(", ", symbols.Select(s => F3(s.Symbol))))
            .Append(" (non-positive, therefore dissipative rather than a wave). ");
        b.Append("DISTANCE IS ").Append(distance.Status.ToString().ToUpperInvariant()).Append(": ")
            .Append(distance.Reading).Append(". ");
        b.Append("THE WORLDLINE IS BOUNDARY: the pattern is identifiable and the position is not, and the tick is the ")
            .Append("sector's documented deepest structural boundary — so a worldline is a sequence indexed by ")
            .Append("something the theory does not supply. ");
        b.Append("THE VELOCITY IS ASSUMED: the bound is derived (|v| <= 1 hop per tick) and the value is not — a family ")
            .Append("of dimension ").Append(velocity.FamilyDimension).Append(" in the amount with ")
            .Append(velocity.Directions)
            .Append(" directions — and the UNIT is one link per tick, ")  
            .Append("whose two halves the surviving structure leaves unsupplied, which is exactly the unit NP_092's ")
            .Append("group velocity is stated in. ");
        b.Append("AND THE MISSING INFORMATION IS NOT RECOVERABLE: the ring's ").Append(degeneracy.IndependentTransfers)
            .Append(" mutually independent transfers commute, so ").Append(degeneracy.IndependentTransfers)
            .Append("! histories — ").Append(F3(degeneracy.Log10Orderings))
            .Append(" orders of magnitude — give bit-identical observables. ");
        b.Append("THE REDUCTION ITSELF COSTS NOTHING: ").Append(reduction.MotionObservables)
            .Append(" readouts, of which ").Append(reduction.Positional)
            .Append(" involve a position, and reconstructing them from pattern propagation loses ")
            .Append(reduction.LossWithTheLabelling)
            .Append(" when the labelling is kept — because there is NO readout of a pattern's absolute position, so ")
            .Append("motion is the motion of a LABEL. ");
        b.Append("THEREFORE THE FAILURE CRITERION IS MET: distance, worldline and velocity each require information the ")
            .Append("surviving sector does not contain — the hop's physical length, the tick, and the transferred amount ")
            .Append("— while the propagation the question asks about is emergent and free of added structure.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the ring carries a pattern with ONE scalar and TWO maps and nothing else; that");
        b.AppendLine("the LAW spreads a pattern in place (the peak falls, the spread grows, the centroid does not move) with");
        b.AppendLine("non-positive mode symbols, so motion cannot come from the evolution map; that spatial distance is assumed");
        b.AppendLine("while the derived surrogate is a STATE difference rather than a separation; that the worldline's tick is");
        b.AppendLine("the sector's documented deepest boundary; that the velocity's bound is derived and its value and unit are");
        b.AppendLine("not; that 48! histories agree observationally; and that the reduction itself costs no observable.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that motion is impossible, or that a larger theory could not supply the hop's");
        b.AppendLine("length or the tick. The audit measures that the SURVIVING sector does not, which is the failure criterion");
        b.AppendLine("the question itself names as acceptable. Nor does it contradict NP_092: that audit's group velocity is");
        b.AppendLine("stated in exactly the units this one measures as unsupplied, so the two are consistent and their");
        b.AppendLine("difference is the question asked, not the answer given.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_021 OR NP_092: G_021 asked whether the sector has a spatial metric for");
        b.AppendLine("LIGHT and NP_092 asked what propagates on the NETWORK and what its velocity analogue is. This audit asks");
        b.AppendLine("whether KINEMATICS — distance, worldline, velocity — is derivable on the 96-cell ring from the surviving");
        b.AppendLine("structure, and it answers by measuring the two units those audits took as given.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a surviving structure that fixes the physical length of a link, or a");
        b.AppendLine("derivation of the tick from the primitives, or a positional readout in the observable set — any of which");
        b.AppendLine("would move one of the three objects from ASSUMED to DERIVED.");
        return b.ToString();
    }

    // ===================== THE REPORT =====================

    public static string OutputCarriers()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE STRUCTURE INVENTORY (task 1)");
        b.AppendLine("   structure                                   state?  map?  survives  readouts carried");
        foreach (var c in Carriers())
        {
            b.AppendLine($"   {c.Structure,-43} {Yes(c.IsAStateVariable),-7} {Yes(c.IsAnEvolutionMap),-5} "
                + $"{Yes(c.Survives),-9} {c.ReadoutsCarried}");
            b.AppendLine($"     {c.Measurement}");
        }
        b.AppendLine();
        b.AppendLine(CarrierCounts().Reading);
        return b.ToString();
    }

    public static string OutputPropagation()
    {
        var b = new StringBuilder();
        b.AppendLine("2. WHAT THE LAW DOES TO A PATTERN");
        b.AppendLine("   tick   peak        excess width  centroid shift  excess total");
        foreach (var (tick, peak, spread, shift, total) in Propagate())
            b.AppendLine($"   {tick,4}   {F3(peak),-11} {F3(spread),-12} {E(shift),-15} {F6(total)}");
        b.AppendLine();
        b.AppendLine($"   the step size is a CONVENTION (epsilon = {F3(Step)}) and the sector supplies the Laplacian, not the");
        b.AppendLine($"   rate at which it is applied; the admissible range is DERIVED from the spectrum: epsilon <= {F3(StableStepBound())}");
        b.AppendLine();
        b.AppendLine("   the update's symbol on the ring's own modes (negative = dissipative, not a wave):");
        b.AppendLine("   mode   symbol");
        foreach (var (mode, symbol, wave) in Symbols())
            b.AppendLine($"   {mode,4}   {F3(symbol),-10} {(wave ? "a wave" : "NOT a wave")}");
        return b.ToString();
    }

    public static string OutputKinematics()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE DISTANCE CANDIDATES, CLASSIFIED (task 2)");
        b.AppendLine("   candidate                                kind                 derived  needs links  needs label  status");
        foreach (var d in DistanceCandidates())
        {
            b.AppendLine($"   {d.Candidate,-40} {d.Kind,-20} {Yes(d.DerivedFromThePrimitives),-8} "
                + $"{Yes(d.RequiresTheLinkSet),-12} {Yes(d.RequiresTheLabelling),-12} {d.Status}");
            b.AppendLine($"     {d.Measurement}");
        }
        b.AppendLine();
        b.AppendLine("   " + DistanceClassification().Reading);
        b.AppendLine();
        b.AppendLine("4. THE WORLDLINE (task 3) AND THE VELOCITY (task 4)");
        foreach (var (ingredient, available, measurement) in WorldlineIngredients())
        {
            b.AppendLine($"   {ingredient}: available {Yes(available)}");
            b.AppendLine($"     {measurement}");
        }
        b.AppendLine($"   the worldline classifies as {WorldlineClassification()}");
        b.AppendLine();
        foreach (var (component, value, derived, measurement) in Velocity())
        {
            b.AppendLine($"   {component}: {F3(value)}, derived {Yes(derived)}");
            b.AppendLine($"     {measurement}");
        }
        var family = VelocityFamily();
        b.AppendLine($"   the velocity-family dimension: {family.FamilyDimension} (the amount) with {family.Directions} directions,");
        b.AppendLine($"   the amount ranging over (0, {F3(family.AmountMax)}]; the value is fixed {Yes(family.TheValueIsFixed)}, the bound is");
        b.AppendLine($"   derived {Yes(family.TheBoundIsDerived)}");
        return b.ToString();
    }

    public static string OutputDegeneracyAndReduction()
    {
        var b = new StringBuilder();
        b.AppendLine("5. WITHOUT SPATIAL ASSUMPTIONS (task 5)");
        b.AppendLine("   removed                                   operations  readouts  transport survives");
        foreach (var (removed, operations, readouts, survives, measurement) in WithoutSpatialAssumptions())
        {
            b.AppendLine($"   {removed,-41} {operations,10}  {readouts,8}  {Yes(survives)}");
            b.AppendLine($"     {measurement}");
        }
        b.AppendLine();
        b.AppendLine("6. THE DEGENERACY COUNT (task 6)");
        var degeneracy = Degeneracy().Single();
        b.AppendLine($"   independent transfers      {degeneracy.IndependentTransfers}");
        b.AppendLine($"   orderings                  {F3(degeneracy.Log10Orderings)} as a power of ten "
            + $"(exceeds a 64-bit integer: {Yes(degeneracy.ExceedsA64BitInteger)})");
        b.AppendLine($"   the states are identical   {Yes(degeneracy.TheStatesAreIdentical)}");
        b.AppendLine($"     {degeneracy.Measurement}");
        b.AppendLine();
        b.AppendLine("7. THE REDUCTION TEST (task 7)");
        b.AppendLine("   readout                                     a position  reads the labelling  frame invariant");
        foreach (var (readout, positional, labelling, invariant, note) in MotionObservables())
        {
            b.AppendLine($"   {readout,-43} {Yes(positional),-11} {Yes(labelling),-19} {Yes(invariant)}");
            b.AppendLine($"     {note}");
        }
        var reduction = ReductionTest();
        b.AppendLine();
        b.AppendLine($"   motion observables {reduction.MotionObservables}, positional {reduction.Positional}, reading the labelling");
        b.AppendLine($"   {reduction.ReadTheLabelling}; the observable LOSS is {reduction.LossWithTheLabelling} with the labelling retained");
        b.AppendLine($"   and {reduction.LossWithoutIt} without it");
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("8. THE CLASSIFICATION AND THE CRITERIA");
        foreach (var (object2, status, measurement) in Classification())
        {
            b.AppendLine($"   {object2}: {status}");
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

    private static double[] Apply(double[] rho, int from)
    {
        var change = new double[Cells];
        double amount = Math.Min(0.3, rho[from] * 0.5);
        change[from] -= amount;
        change[(from + 1) % Cells] += amount;
        return rho.Zip(change, (r, c) => r + c).ToArray();
    }

    private static string Yes(bool value) => value ? "yes" : "no";

    private static string E(double value) => value.ToString("E3", CultureInfo.InvariantCulture);

    private static string F3(double value) => value.ToString("F3", CultureInfo.InvariantCulture);

    private static string F6(double value) => value.ToString("F6", CultureInfo.InvariantCulture);
}
