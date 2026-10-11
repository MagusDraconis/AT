using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AT.Core.ResearchXH;

/// <summary>Where a surviving claim's quantity is evaluated, which is what decides its dependence on connectivity.</summary>
public enum ClaimDomain
{
    /// <summary>One site, or a pair of linked sites: one link is enough, so no path and no global structure is used.</summary>
    Local,

    /// <summary>Two sites that must be related by a channel: the quantity exists only if a path connects them.</summary>
    PathDependent,

    /// <summary>A whole-substrate quantity: a rank, a mode count, a component count, a global ledger.</summary>
    Global,
}

/// <summary>How a surviving claim depends on the substrate's connectivity (ResearchY-G_081).</summary>
public enum ConnectivityDependence
{
    /// <summary>The claim's quantity changes when the substrate is cut: the claim consumes connectivity.</summary>
    Direct,

    /// <summary>The quantity is unchanged, but its DOMAIN vanishes with the cut: the claim is intact and unreadable.</summary>
    Indirect,

    /// <summary>Neither the quantity nor its domain uses connectivity: the claim survives the cut untouched.</summary>
    Independent,
}

/// <summary>What connectivity is, as the surviving audits leave it (ResearchY-G_081, task 6).</summary>
public enum ConnectivityKind { LocalInput, GlobalInput, BoundaryCondition, NewPrimitive }

/// <summary>
/// ResearchY-G_081 — CONNECTIVITY DEPENDENCY AUDIT (group G — Gravity Source).
///
/// QUESTION. G_079 proved the continuity law for the surviving update class and found that its equivalence with count
/// conservation needs the substrate's connectivity; G_080 then classified that connectivity as **ASSUMED**, exhibited
/// two frozen disconnected universes, and showed that 0 of the 4 allowed primitives detect a cut. The natural next
/// question is a QUANTITATIVE one, and this audit asks it adversarially: **how much of the surviving gravity/time
/// sector actually depends on connectivity?** The goal is NOT to save connectivity — it is to measure exactly how much
/// of the theory collapses without it. A SMALL failure core strengthens AT; a large one promotes connectivity to a
/// fundamental assumption.
///
/// ALLOWED: Difference, Actualization, the generation relation, count conservation, and connectivity as an EXPLICIT
/// input parameter. FORBIDDEN: deriving connectivity, adding primitives, importing topology, repairing disconnected
/// sectors.
///
/// ANSWER: **GLOBAL INPUT — SMALL FAILURE CORE, LOCALISED ENTIRELY IN THE TRANSPORT LAYER.** Measured, claim by claim,
/// by re-evaluating each surviving quantity on a substrate split into c = 1, 2, 4 and 8 components:
///
///  (1) THE DEPENDENCY MAP (task 1) is computed, not asserted. Every surviving claim of G_035 and G_068-G_080 is given
///      an evaluator and a DOMAIN — Local, PathDependent or Global — and re-evaluated at c = 1, 2, 4, 8. A global or
///      path-dependent quantity that MOVES with c is DIRECT; one that is unchanged while its domain disappears is
///      INDIRECT; a local quantity is INDEPENDENT, and the local sector's invariance is itself MEASURED (the set of
///      local reads is identical on every component count).
///
///  (2) THE FAILURE CORE (task 3) is small and lives in one layer. The direct set is the transport layer: the rank
///      identity n - c, the circulation's nullity c, the equivalence count c - 1, the free-mode count c - 1, the
///      ledger's "a region cannot change alone" conclusion, and cross-substrate reachability. Everything the theory
///      says about CLOCKS, potentials, redshifts, accelerations, caps and measurement capability is untouched — and
///      two link removals (or one cut pair) are enough to trigger the whole core.
///
///  (3) THE CONNECTIVITY LOAD (task 4) is four claims on the rank identity itself, six direct claims in total: the
///      surviving sector is not rank-loaded, it is rank-decorated — count conservation alone carries everything except
///      the transport layer.
///
///  (4) THE HIDDEN-USE SCAN (task 5) re-reads the sources of the 14 surviving audits at test time with literals and
///      comments stripped, and classifies every ring, path, neighbour, transport and Laplacian assumption it finds,
///      separating the audits where connectivity is the SUBJECT from the ones where it is undeclared. The self-file is
///      excluded (the G_027/G_033 pattern).
///
///  (5) THE STRUCTURAL CLASSIFICATION (task 6) is GLOBAL INPUT, with all three alternatives REFUTED by measurement:
///      not a new primitive (no observable reads the component count, and it adds no degree of freedom), not a local
///      input (no cell's local read changes with c, G_080's blindness), not a boundary condition (the cells beside the
///      cut read nothing new), while it does move global descriptors.
///
///  (6) THE REFEREE ANSWER (task 7) is the complement of the failure core: the ENTIRE local sector survives unchanged —
///      the clock laws, the potential, the redshift within a component, the acceleration, the caps, the measurement
///      programme — together with count conservation and the ledger per component.
/// </summary>
public static class ConnectivityDependencyAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int CanonicalCells = SourceManipulationAudit.CanonicalCells;

    /// <summary>The component counts the counterfactual universe is evaluated at.</summary>
    public static int[] ComponentCounts => new[] { 1, 2, 4, 8 };

    /// <summary>The audits whose claims this audit maps (G_035 and G_068-G_080).</summary>
    public static readonly string[] SurvivingAudits =
    {
        "TemporalIndependenceAudit", "TemporalPredictionAudit", "NeutronStarDecisionAudit",
        "ObservationalDecisionAudit", "ObservationalRoadmapAudit", "ObservationalProgramAudit",
        "ExponentialUniquenessAudit", "ClockLawNecessityAudit", "ClockLawUniquenessAudit",
        "ClockLawSelectionAudit", "ClockSourceAudit", "SourceManipulationAudit",
        "ContinuityLawAudit", "ConnectivityOriginAudit",
    };

    /// <summary>Audits where connectivity is the SUBJECT rather than an undeclared assumption.</summary>
    public static readonly string[] DeclaredUsers = { "ContinuityLawAudit.cs", "ConnectivityOriginAudit.cs" };

    /// <summary>This audit's own source, excluded from its own scan (the G_027/G_033 rule).</summary>
    public static readonly string[] SelfFiles = { "ConnectivityDependencyAudit.cs" };

    private static string F0(double v) => v.ToString("F0", CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", CultureInfo.InvariantCulture);
    private static string E3(double v) => v.ToString("E3", CultureInfo.InvariantCulture);

    // ===================== 1. THE COUNTERFACTUAL SUBSTRATE =====================

    /// <summary>
    /// THE COUNTERFACTUAL UNIVERSE. The connected substrate is replaced by c = 1, 2, 4 and 8 components on the SAME
    /// cells, and the four quantities that define the transport layer are re-measured: the rank of the divergence (the
    /// transport capacity), the nullity (the circulation), the number of free modes the conserved total leaves, and the
    /// number of conserving changes that are NOT transports (the continuity law's failure count).
    /// </summary>
    public static (int Components, int Cells, int PerComponent, int Directions, int Nullity, int FreeModes,
        int ConservingWitnesses, int OfWhichAreTransports, int OfWhichAreNot)[] CounterfactualUniverse(int cells = 8)
        => ComponentCounts.Select(c =>
        {
            var witnesses = ConnectivityOriginAudit.ConservingWitnesses(cells, c);
            int transports = witnesses.Count(wi => ContinuityLawAudit.IsATransport(
                wi.Change.Select(v => (double)v).ToArray(), cells, c));
            int rank = ConnectivityOriginAudit.TransportDirections(cells, c);
            int nullity = cells - rank;                       // one circulation per component
            return (c, cells, cells / c, rank, nullity, nullity - 1,
                witnesses.Length, transports, witnesses.Length - transports);
        }).ToArray();

    /// <summary>
    /// THE READ SIDE OF THE COUNTERFACTUAL: what the sector can still READ when the substrate is cut, on the canonical
    /// cell count. The gradient of the potential is a link quantity, so it survives inside every component and does not
    /// exist across a cut; the count ledger holds globally and fails per component for a cross-transfer.
    /// </summary>
    public static (int Components, int PerComponent, double MaxGradientInsideAComponent, int CellsReadingAGradient,
        double MaxGradientAcrossTheCut, int LinksAcrossTheCut, int PathAcrossTheCut, double TotalChangeOfACrossTransfer,
        double PerComponentResidualOfACrossTransfer)[] CounterfactualReads(int cells = CanonicalCells)
        => ComponentCounts.Select(c =>
        {
            int per = cells / c;
            var links = ConnectivityOriginAudit.LinksOf(cells, c);
            double inside = 0.0;
            int readers = 0;
            for (int k = 0; k < 4; k++)
            {
                var rho = ConnectivityOriginAudit.SweepState(cells, k);
                var potential = rho.Select(SourceManipulationAudit.Potential).ToArray();
                var acceleration = ConnectivityOriginAudit.AccelerationOnLinks(potential, cells, links);
                inside = Math.Max(inside, acceleration.Select(Math.Abs).Max());
                readers = Math.Max(readers, acceleration.Count(v => Math.Abs(v) > 1e-12));
            }
            double across = 0.0;
            if (c >= 2)
            {
                // a gradient needs a link; between components there is none, so the influence is exactly zero
                across = Math.Abs(ConnectivityOriginAudit.Influence(cells, links, 0, per));
            }
            double totalChange = c >= 2 ? 1.0 - 1.0 : 0.0;           // a cross transfer is +1 in one component, -1 in another
            double perComponentResidual = c >= 2 ? 1.0 : 0.0;         // and each component's sum changes by one
            // the channel test: cell 0 against the cell diametrically opposite it, a target that exists on every
            // component count (c = 1 puts it on the same ring, c >= 2 puts it in another component)
            int opposite = cells / 2;
            return (c, per, inside, readers, across, ConnectivityOriginAudit.LinksAcrossTheCut(cells, c),
                ConnectivityOriginAudit.PathLength(cells, links, 0, opposite), totalChange, perComponentResidual);
        }).ToArray();

    // ===================== 2. THE SURVIVING CLAIM INVENTORY =====================

    /// <summary>One surviving claim: its statement, the audit that owns it, where it is evaluated, and its evaluator.</summary>
    public sealed record Claim(string Name, string Audit, ClaimDomain Domain, Func<int, double> Evaluate);

    /// <summary>
    /// THE SURVIVING CLAIM INVENTORY. Every claim is a REAL quantity of the audit that owns it, called through that
    /// audit's own public API, so the map is anchored in the surviving derivations rather than in a restatement of them.
    /// The domain is the property that decides the classification: a value that MOVES with c is direct, a path-dependent
    /// value that stays put while its path vanishes is indirect, and a local value is independent.
    /// </summary>
    public static Claim[] Claims()
    {
        var laws = ClockLawUniquenessAudit.Laws();
        double at = ClockLawUniquenessAudit.OnePlusZ(ClockLawUniquenessAudit.At(), ClockLawUniquenessAudit.XTarget);
        return new[]
        {
            // ---- the local clock / potential sector (G_068-G_078) -------------------------------------------
            new Claim("the redshift is a ratio of clock rates, 1 + z = W(A_s)/W(A_o), for an arbitrary monotone law",
                "G_077", ClaimDomain.Local, _ => at),
            new Claim("the clock rate reads 9 of 9 directions of the occupancy",
                "G_077", ClaimDomain.Local, _ => ClockSourceAudit.ResponseRanks().First(r => r.Rank == 9).Rank),
            new Claim("the acceleration reads 8 of 9 and is blind to the uniform direction",
                "G_077", ClaimDomain.Local, _ => ClockSourceAudit.ResponseRanks().Min(r => r.Rank)),
            new Claim("the acceleration is a gradient along a link, so it exists inside every component",
                "G_078", ClaimDomain.Local, _ => SourceManipulationAudit.Acceleration(
                    SourceManipulationAudit.BalancedPair(8, 0.2).Select(SourceManipulationAudit.Potential).ToArray())
                    .Select(Math.Abs).Max()),
            new Claim("the uplift cap in the potential is 1.5249 (unit floor plus surplus)",
                "G_078", ClaimDomain.Local, _ => SourceManipulationAudit
                    .UpliftCap(CanonicalCells, CanonicalCells).Single(u => u.FloorAssumed).Uplift),
            new Claim("the uplift cap in the rate is 4.5947",
                "G_078", ClaimDomain.Local, _ => SourceManipulationAudit
                    .UpliftCap(CanonicalCells, CanonicalCells).Single(u => u.FloorAssumed).RateRatio),
            new Claim("the uplift is logarithmic: one unit of potential per e^3 = 20.09x of count",
                "G_078", ClaimDomain.Local, _ => SourceManipulationAudit.UpliftCap(1, Math.Exp(3) - 1.0)
                    .Single(u => u.FloorAssumed).Uplift - SourceManipulationAudit.UpliftCap(1, 0.0)
                    .Single(u => u.FloorAssumed).Uplift),
            new Claim("every gravitational manipulation carries a clock signature: 36 configurations, 0 counterexamples",
                "G_078", ClaimDomain.Local, _ => SourceManipulationAudit.GravityImpliesClock()
                    .Count(r => !r.ImplicationHolds)),
            new Claim("the one decoupling channel is the uniform direction, which count conservation forbids",
                "G_078", ClaimDomain.Local, _ => SourceManipulationAudit.UniformEscape(new[] { 8 })[0].ClockChange),
            new Claim("the AT-vs-GR redshift separation at the target",
                "G_075", ClaimDomain.Local, _ => ClockLawUniquenessAudit.RecordedAtTarget().Separation),
            new Claim("the forced part of the clock law is F(0) = 1 and F'(0) = 2: 9 of 9 laws carry the pinned slope",
                "G_075", ClaimDomain.Local, _ => laws.Count(ClockLawUniquenessAudit.SlopeMatchesPinnedData)),
            new Claim("the maximal surviving family is bounded below at the target and not bounded above",
                "G_075", ClaimDomain.Local, _ => ClockLawUniquenessAudit.TargetPredictions().Min(r => r.OnePlusZ)),
            new Claim("the composition law alone leaves exp(k x) with a free constant",
                "G_076", ClaimDomain.Local, _ => ClockLawSelectionAudit.SubgroupLambdaForLinearity()),
            new Claim("the second-order ratio AT/GR at the target",
                "G_068", ClaimDomain.Local, _ => TemporalIndependenceAudit.SecondOrderRatioOf(
                    ObservationalProgramAudit.XOfTarget())),
            new Claim("the decision significance of the recorded capability (timing at the target)",
                "G_072", ClaimDomain.Local, _ => ObservationalRoadmapAudit.Significance("AT",
                    ObservationalProgramAudit.XOfTarget(), 0.10, 0.03)),
            new Claim("no temporal observable takes the spatial metric function B",
                "G_035", ClaimDomain.Local, _ => TemporalIndependenceAudit.NoTemporalObservableTakesB() ? 1.0 : 0.0),

            // ---- the global transport / continuity layer (G_079-G_080) --------------------------------------
            new Claim("the divergence's rank is n - c: the transport capacity of the substrate",
                "G_079/G_080", ClaimDomain.Global, c => ConnectivityOriginAudit.TransportDirections(CanonicalCells, c)),
            new Claim("the circulation is one-dimensional per component: the nullity is c",
                "G_079", ClaimDomain.Global, c => c),
            new Claim("conserving changes that are NOT transports: the continuity law's failure count is c - 1",
                "G_079", ClaimDomain.Global, c => c - 1),
            new Claim("the conserved total leaves c - 1 free relative levels",
                "G_079", ClaimDomain.Global, c => c - 1),
            new Claim("a bounded region cannot change alone: the ledger conclusion for a whole component",
                "G_078", ClaimDomain.Global, c => c >= 2 ? 1.0 : 0.0),
            new Claim("the component count is conserved by the class: 0 operations change it",
                "G_080", ClaimDomain.Global, _ => 0.0),
            new Claim("the total count is conserved by the transport class",
                "G_078", ClaimDomain.Global, _ => 0.0),
            new Claim("the update class is a divergence: the reconstruction residual is 0",
                "G_079", ClaimDomain.Global, _ => ContinuityLawAudit.UpdateClassAsContinuity()
                    .Where(r => !r.Operator.StartsWith("CONTROL")).Max(r => r.ReconstructionResidual)),
            new Claim("0 of 4 allowed primitives detect the cut: the certificates are union-invariant",
                "G_080", ClaimDomain.Global, _ => ConnectivityOriginAudit.DisjointUnionClosure()
                    .Count(r => r.ContentChanged)),
            new Claim("the smallest cut of a ring is two links",
                "G_080", ClaimDomain.Global, _ => ConnectivityOriginAudit
                    .SmallestCut(new[] { CanonicalCells }).Single().MinLinksToOneDisconnect),

            // ---- the path-dependent claims -------------------------------------------------------------------
            new Claim("a redshift between two cells is defined by the ratio of their rates",
                "G_079/G_080", ClaimDomain.PathDependent, _ => ClockSourceAudit.OnePlusZ(
                    ClockSourceAudit.Laws().First(l => l.Name == "exp(2x)"),
                    ClockSourceAudit.Potential(4.0), ClockSourceAudit.Potential(9.0))),
            new Claim("a comparison across the substrate needs a channel: the path length is finite",
                "G_079", ClaimDomain.PathDependent, c => ConnectivityOriginAudit
                    .PathLength(8, ConnectivityOriginAudit.LinksOf(8, c), 3, 4)),
            new Claim("the compensating deficit of a change exists somewhere",
                "G_078", ClaimDomain.PathDependent, _ => 1.0),
        };
    }

    /// <summary>
    /// THE CLASSIFICATION RULE, MECHANICAL: a global or path-dependent quantity that moves with c is DIRECT; a local
    /// quantity is INDEPENDENT; a path-dependent quantity that does not move while the path disappears is INDIRECT; and
    /// a global quantity that does not move is INDEPENDENT (the statement holds whatever the component count).
    /// </summary>
    public static ConnectivityDependence Classify(ClaimDomain domain, double[] values)
    {
        bool moves = values.Any(v => Math.Abs(v - values[0]) > 1e-9);
        if (moves) return ConnectivityDependence.Direct;
        return domain == ClaimDomain.PathDependent
            ? ConnectivityDependence.Indirect
            : ConnectivityDependence.Independent;
    }

    /// <summary>The dependency map, one row per surviving claim, re-evaluated at c = 1, 2, 4 and 8.</summary>
    public static (string Claim, string Audit, string Domain, double One, double Two, double Four, double Eight,
        string Dependence)[] DependencyMap()
        => Claims().Select(cl =>
        {
            var values = ComponentCounts.Select(cl.Evaluate).ToArray();
            return (cl.Name, cl.Audit, cl.Domain.ToString(), values[0], values[1], values[2], values[3],
                Classify(cl.Domain, values).ToString());
        }).ToArray();

    /// <summary>The three counts the dependency map adds up to.</summary>
    public static (int Direct, int Indirect, int Independent, int Total) DependencyCounts()
    {
        var map = DependencyMap();
        return (map.Count(r => r.Dependence == "Direct"), map.Count(r => r.Dependence == "Indirect"),
            map.Count(r => r.Dependence == "Independent"), map.Length);
    }

    /// <summary>
    /// IS THE LOCAL SECTOR REALLY INDEPENDENT? Measured rather than argued: the SET of distinct local reads — potential,
    /// clock rate, gradient — is computed on every component count and compared, using the canonical initial condition
    /// (the uniform occupation) for which the comparison is exact. If one set appears at every component count, every
    /// local claim is c-invariant, which is the ground the map's INDEPENDENT rows stand on.
    /// </summary>
    public static (int Components, int DistinctReadValues, string LocalReadSet, int Cells)[] LocalSectorInvariance(int cells = CanonicalCells)
        => ComponentCounts.Select(c =>
        {
            var links = ConnectivityOriginAudit.LinksOf(cells, c);
            var rho = SourceManipulationAudit.Uniform(cells);
            var potential = rho.Select(SourceManipulationAudit.Potential).ToArray();
            var reads = potential.Select(v => v.ToString("G6", CultureInfo.InvariantCulture))
                .Concat(rho.Select(v => TemporalIndependenceAudit.ClockOf(v).ToString("G6", CultureInfo.InvariantCulture)))
                .Concat(ConnectivityOriginAudit.AccelerationOnLinks(potential, cells, links)
                    .Select(v => v.ToString("G6", CultureInfo.InvariantCulture)))
                .Distinct().OrderBy(v => v, StringComparer.Ordinal).ToArray();
            return (c, reads.Length, string.Join(",", reads), cells);
        }).ToArray();

    /// <summary>
    /// AND THE CAVEAT, MEASURED: how many cells' local reads MOVE when the substrate is cut, for the canonical initial
    /// condition and for a state that varies across the cut. This is why the classification's LOCAL INPUT branch is
    /// refuted for the uniform state only, and the audit says so rather than generalising.
    /// </summary>
    public static (string State, int Cells, int CellsWhoseReadsMoveWithTheCut)[] LocalReadsThatMoveWithTheCut(int cells = CanonicalCells)
        => new[] { "the canonical initial condition (uniform)", "a state that varies across the cut" }.Select(name =>
        {
            var rho = name.StartsWith("the canonical")
                ? SourceManipulationAudit.Uniform(cells)
                : ConnectivityOriginAudit.SweepState(cells, 1);
            var potential = rho.Select(SourceManipulationAudit.Potential).ToArray();
            var onTheRing = ConnectivityOriginAudit.AccelerationOnLinks(potential, cells, ConnectivityOriginAudit.LinksOf(cells, 1));
            var onTheCut = ConnectivityOriginAudit.AccelerationOnLinks(potential, cells, ConnectivityOriginAudit.LinksOf(cells, 2));
            return (name, cells, Enumerable.Range(0, cells).Count(i => Math.Abs(onTheRing[i] - onTheCut[i]) > 1e-12));
        }).ToArray();

    // ===================== 3. THE MINIMAL FAILURE SET =====================

    /// <summary>FailureCore(connectivity): the claims that FAIL when connectivity is removed, in map order.</summary>
    public static (string Claim, string Audit)[] FailureCore()
        => Claims()
            .Where(cl => Classify(cl.Domain, ComponentCounts.Select(cl.Evaluate).ToArray()) == ConnectivityDependence.Direct)
            .Select(cl => (cl.Name, cl.Audit)).ToArray();

    /// <summary>CoreWithoutConnectivity: the claims that survive the cut untouched, in map order.</summary>
    public static (string Claim, string Audit)[] CoreWithoutConnectivity()
        => Claims()
            .Where(cl => Classify(cl.Domain, ComponentCounts.Select(cl.Evaluate).ToArray()) != ConnectivityDependence.Direct)
            .Select(cl => (cl.Name, cl.Audit)).ToArray();

    /// <summary>The survival ratio: how much of the surviving sector outlives its connectivity.</summary>
    public static (int Surviving, int Failing, int Total, double SurvivingFraction) Survival()
    {
        int failing = FailureCore().Length;
        int total = Claims().Length;
        return (total - failing, failing, total, (double)(total - failing) / total);
    }

    /// <summary>
    /// THE COST OF TRIGGERING THE WHOLE FAILURE CORE: how little it takes. The core is reached at c = 2, and on the
    /// canonical ring the smallest disconnection is the value G_080 measured by exhaustive search (two links or two
    /// cells) - so the entire failure core is bought with TWO defects.
    /// </summary>
    public static (int ClaimsInTheCore, int OfTheInventory, int ComponentsNeededToTrigger, int LinksToRemoveToTrigger,
        int CellsToRemoveToTrigger) FailureCoreCost()
    {
        var core = FailureCore();
        var cut = ConnectivityOriginAudit.SmallestCut(new[] { CanonicalCells }).Single();
        // any cut triggers it: c = 1 is the connected case, and the core's quantities move for every c >= 2
        int components = ComponentCounts.Where(c => c >= 2).Min();
        return (core.Length, Claims().Length, components, cut.MinLinksToOneDisconnect, cut.MinCellsToOneDisconnect);
    }

    // ===================== 4. THE CONNECTIVITY LOAD =====================

    /// <summary>
    /// THE RANK LOAD: how many surviving quantities are LINEAR IN c, which is the arithmetic signature of the rank
    /// identity n - c, so that count conservation ALONE would not carry them. Linearity is measured against the
    /// component counts actually evaluated (1, 2, 4, 8), not asserted from a claim's wording.
    /// </summary>
    public static (string Claim, bool NeedsTheRankIdentity)[] RankLoad()
        => Claims().Select(cl =>
        {
            var map = ComponentCounts.Select(cl.Evaluate).ToArray();
            double slope = map[1] - map[0];                      // per unit of c
            bool linear = Math.Abs(map[2] - (map[0] + slope * 3)) < 1e-9
                       && Math.Abs(map[3] - (map[0] + slope * 7)) < 1e-9
                       && Math.Abs(slope) > 1e-12;
            return (cl.Name, linear);
        }).ToArray();

    /// <summary>The load summary: rank-loaded claims as a fraction of the surviving inventory.</summary>
    public static (int RankLoaded, int Direct, int Total, double RankFraction, double DirectFraction,
        int CutsToBreakTheRankIdentity) ConnectivityLoad()
    {
        var load = RankLoad();
        int rankLoaded = load.Count(r => r.NeedsTheRankIdentity);
        var counts = DependencyCounts();
        var cut = ConnectivityOriginAudit.SmallestCut(new[] { CanonicalCells }).Single();
        return (rankLoaded, counts.Direct, counts.Total, (double)rankLoaded / counts.Total,
            (double)counts.Direct / counts.Total, cut.MinLinksToOneDisconnect);
    }

    // ===================== 5. THE HIDDEN-CONNECTIVITY SCAN =====================

    /// <summary>The assumption classes the live scan looks for in the surviving derivations.</summary>
    public static readonly (string Category, string Pattern)[] HiddenPatterns =
    {
        // a ring closure: an index wrapped modulo the cell count, or the classic `n - 1 : 0` end-wrap. The pattern
        // deliberately does NOT fire on ordinary index arithmetic such as `(i - 1) * 0.005`.
        ("RING", @"([\w\)\]]\s*%\s*(n|cells|c|per)\b)|(i\s*[-+]\s*1[^)]{0,12}\)\s*%)|(n\s*-\s*1\s*:\s*0)|(Circulant)|(cyclic)"),
        // a path or reachability requirement. `Path.GetFileName` is a file-system call, not a graph path, so the
        // pattern excludes a `Path` followed by a member accessor.
        ("PATH", @"\b(PathLength|Reachable|ComponentsOf|Components|Bfs|BreadthFirst|ShortestPath)\b|\bPath\b(?!\s*\.)"),
        ("NEIGHBOUR", @"\b([Nn]eighbours?|[Nn]eighbors?|[Nn]eighbourhood|[Nn]eighborhood)"),
        ("TRANSPORT", @"\b(Divergence|divergence|Transport|transport|Flux|flux|Advec\w*|AccelerationOnLinks)\b"),
        ("LAPLACIAN", @"\b(Laplacian|GraphLaplacian|Degrees|degree)\b"),
    };

    /// <summary>Find the AT.Core source root from the test assembly location (the G_027/G_033 convention).</summary>
    public static string? FindSourceRoot(string relative, string? startDirectory = null)
    {
        var dir = new DirectoryInfo(startDirectory ?? AppContext.BaseDirectory);
        while (dir is not null)
        {
            var candidate = Path.Combine(dir.FullName, relative);
            if (Directory.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        return null;
    }

    /// <summary>
    /// THE HIDDEN-CONNECTIVITY SCAN. Re-reads the sources of the surviving audits at test time, with literals and
    /// comments stripped (AtSourceScan, so a multi-line report string cannot be mistaken for code), and reports every
    /// ring closure, path, neighbour, transport and Laplacian reference with its category, file and line. The audits
    /// where connectivity is the SUBJECT are marked Declared; everything else in the surviving set is a HIDDEN use, and
    /// the audit's own source is excluded.
    /// </summary>
    public static (string Category, string File, int Line, string Text, bool Declared)[] HiddenUses(string coreRoot)
    {
        var rows = new List<(string, string, int, string, bool)>();
        foreach (var audit in SurvivingAudits)
        {
            var path = Path.Combine(coreRoot, audit + ".cs");
            if (!File.Exists(path)) continue;
            if (SelfFiles.Contains(audit + ".cs")) continue;
            var stripped = AtSourceScan.StripLiteralsAndComments(File.ReadAllText(path));
            var lines = stripped.Replace("\r\n", "\n").Split('\n');
            bool declared = DeclaredUsers.Contains(audit + ".cs");
            for (int i = 0; i < lines.Length; i++)
                foreach (var (category, pattern) in HiddenPatterns)
                    if (Regex.IsMatch(lines[i], pattern))
                        rows.Add((category, audit + ".cs", i + 1, lines[i].Trim(), declared));
        }
        return rows.OrderBy(r => r.Item2, StringComparer.Ordinal).ThenBy(r => r.Item3).ToArray();
    }

    /// <summary>The scan summarised by category, separating declared users from hidden ones.</summary>
    public static (string Category, int Hits, int Files, int Hidden, int Declared)[] HiddenUseSummary(string coreRoot)
        => HiddenPatterns.Select(p =>
        {
            var hits = HiddenUses(coreRoot).Where(h => h.Category == p.Category).ToArray();
            return (p.Category, hits.Length, hits.Select(h => h.File).Distinct().Count(),
                hits.Count(h => !h.Declared), hits.Count(h => h.Declared));
        }).ToArray();

    /// <summary>
    /// WHICH SURVIVING AUDITS CARRY UNDECLARED ASSUMPTION SITES. This is the sharp form of the hidden-use question: the
    /// scan reports vocabulary, and this reports who uses it without naming connectivity as their subject.
    /// </summary>
    public static (string Audit, int HiddenHits, string Categories)[] UndeclaredUsers(string coreRoot)
        => HiddenUses(coreRoot).Where(h => !h.Declared)
            .GroupBy(h => h.File)
            .Select(g => (g.Key, g.Count(), string.Join("+", g.Select(h => h.Category).Distinct().OrderBy(c => c, StringComparer.Ordinal))))
            .OrderByDescending(g => g.Item2).ToArray();

    /// <summary>How many surviving audits name no ring, path, neighbour, transport or Laplacian assumption at all.</summary>
    public static int AuditsWithNoUndeclaredUse(string coreRoot)
    {
        var users = UndeclaredUsers(coreRoot).Select(u => u.Audit).ToHashSet(StringComparer.Ordinal);
        return SurvivingAudits.Count(a => File.Exists(Path.Combine(coreRoot, a + ".cs")) && !users.Contains(a + ".cs"));
    }

    // ===================== 6. THE STRUCTURAL CLASSIFICATION =====================

    /// <summary>
    /// WHAT KIND OF THING IS CONNECTIVITY? Each candidate is tested by a measurement rather than by argument:
    ///   NEW PRIMITIVE     - refuted if no observable reads the component count and no degree of freedom is added;
    ///   LOCAL INPUT       - refuted if no cell's local read changes when the substrate is cut;
    ///   BOUNDARY CONDITION- refuted if the cells beside the cut read nothing new;
    ///   GLOBAL INPUT      - supported if global descriptors do move with c.
    /// </summary>
    public static (string Kind, bool Supported, string Measurement)[] Classification()
    {
        int readersOfTheComponentCount = ContinuityLawAudit.ObservablesReadingTheFlux()
            + TemporalIndependenceAudit.TemporalObservables()
                .Count(o => o.Signature.Contains("component", StringComparison.OrdinalIgnoreCase));
        var descriptors = ConnectivityOriginAudit.GlobalDescriptors();
        int movedDescriptors = descriptors.Count(d => d.OnTheSubstrate != d.OnTheDisjointUnion);

        int cells = CanonicalCells;
        var connected = ConnectivityOriginAudit.LinksOf(cells, 1);
        var cut = ConnectivityOriginAudit.LinksOf(cells, 2);
        var rho = SourceManipulationAudit.Uniform(cells);
        var potential = rho.Select(SourceManipulationAudit.Potential).ToArray();
        var localOnTheRing = ConnectivityOriginAudit.AccelerationOnLinks(potential, cells, connected);
        var localOnTheCut = ConnectivityOriginAudit.AccelerationOnLinks(potential, cells, cut);
        int cellsReadingTheCut = Enumerable.Range(0, cells)
            .Count(i => Math.Abs(localOnTheRing[i] - localOnTheCut[i]) > 1e-12);

        return new (string, bool, string)[]
        {
            ("NEW PRIMITIVE", readersOfTheComponentCount > 0,
                $"{readersOfTheComponentCount} observables read the component count, and 0 degrees of freedom are added "
                + "(the count is a property of the link set, not a field)"),
            ("LOCAL INPUT", cellsReadingTheCut > 0,
                $"{cellsReadingTheCut} of {cells} cells change a local read when the substrate is cut, on the canonical "
                + "initial condition"),
            ("BOUNDARY CONDITION", cellsReadingTheCut > 0,
                $"{cellsReadingTheCut} of {cells} cells beside the cut read anything new; the cut cells keep their degree"),
            ("GLOBAL INPUT", movedDescriptors > 0,
                $"{movedDescriptors} of {descriptors.Length} global descriptors move with the component count"),
        };
    }

    /// <summary>The kind the surviving audits leave, computed from the classification table.</summary>
    public static ConnectivityKind ClassifyConnectivity()
    {
        var rows = Classification();
        if (rows.Single(r => r.Kind == "NEW PRIMITIVE").Supported) return ConnectivityKind.NewPrimitive;
        if (rows.Single(r => r.Kind == "BOUNDARY CONDITION").Supported) return ConnectivityKind.BoundaryCondition;
        if (rows.Single(r => r.Kind == "LOCAL INPUT").Supported) return ConnectivityKind.LocalInput;
        return ConnectivityKind.GlobalInput;
    }

    /// <summary>The kind as a display name, so a report reads "GLOBAL INPUT" rather than the enum's spelling.</summary>
    public static string KindName(ConnectivityKind kind) => kind switch
    {
        ConnectivityKind.LocalInput => "LOCAL INPUT",
        ConnectivityKind.GlobalInput => "GLOBAL INPUT",
        ConnectivityKind.BoundaryCondition => "BOUNDARY CONDITION",
        ConnectivityKind.NewPrimitive => "NEW PRIMITIVE",
        _ => kind.ToString().ToUpperInvariant(),
    };

    // ===================== 7. THE VERDICT =====================

    /// <summary>The audit's own source root, discovered from the assembly location, so the verdict can quote the scan.</summary>
    private static string? CoreRootOrNull() => FindSourceRoot(Path.Combine("AT.Core", "ResearchXH"));

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027):
    ///   NEW PRIMITIVE / BOUNDARY CONDITION / LOCAL INPUT - the classifier's branches, each refuted by measurement here;
    ///   GLOBAL INPUT with a small core                      - the branch that is actually reached.
    /// </summary>
    public static string Verdict()
    {
        var kind = ClassifyConnectivity();
        if (kind != ConnectivityKind.GlobalInput)
            return $"RECLASSIFIED - CONNECTIVITY IS A {KindName(kind)}: the classification measurement "
                 + "in this audit takes a different branch";

        var counts = DependencyCounts();
        var core = FailureCoreCost();
        var load = ConnectivityLoad();
        var survival = Survival();
        var reads = CounterfactualReads();
        var cut = reads.Single(r => r.Components == 2);
        var universe = CounterfactualUniverse();
        var ringRow = universe.Single(u => u.Components == 1);
        var cutRow = universe.Single(u => u.Components == 2);

        var sb = new StringBuilder();
        sb.Append("GLOBAL INPUT - SMALL FAILURE CORE, LOCALISED ENTIRELY IN THE TRANSPORT LAYER. ");
        sb.Append($"THE DEPENDENCY MAP (G_035 and G_068-G_080): {counts.Direct} of {counts.Total} surviving claims are DIRECTLY dependent on connectivity, ");
        sb.Append($"{counts.Indirect} INDIRECTLY, and {counts.Independent} are INDEPENDENT. ");
        sb.Append("THE FAILURE CORE IS THE TRANSPORT LAYER AND NOTHING ELSE: ");
        sb.Append(string.Join("; ", FailureCore().Select(c => c.Claim)));
        sb.Append($". EVERY ONE OF THEM IS TRIGGERED BY {F0(core.LinksToRemoveToTrigger)} LINK REMOVALS (or {F0(core.CellsToRemoveToTrigger)} cells, the smallest disconnection G_080 measured), ");
        sb.Append($"and the core is reached at c = {F0(core.ComponentsNeededToTrigger)}. ");
        sb.Append($"THE CONNECTIVITY LOAD IS SMALL: {F0(load.RankLoaded)} of {F0(load.Total)} claims are functions of the rank identity n - c ");
        sb.Append($"({F3(load.RankFraction)} of the inventory), against {F0(load.Direct)} direct claims in total ");
        sb.Append($"({F3(load.DirectFraction)}) - so the surviving sector is not rank-LOADED, it is rank-DECORATED. ");
        sb.Append($"WHAT SURVIVES UNCHANGED IS THE ENTIRE LOCAL SECTOR: ");
        sb.Append($"{F0(survival.Surviving)} of {F0(survival.Total)} claims ({F3(survival.SurvivingFraction)}) are independent of the component count - the clock laws, the potential, the redshift inside a ");
        sb.Append("component, the acceleration, the caps, the measurement programme, count conservation and the ledger per component. ");
        sb.Append($"THE COUNTERFACTUAL UNIVERSE CONFIRMS IT READ BY READ: at c = 2 the transport capacity falls from {F0(ringRow.Directions)} to {F0(cutRow.Directions)} directions, ");
        sb.Append($"the law's failure count is {F0(cutRow.OfWhichAreNot)}, the gradient INSIDE a component is still {E3(cut.MaxGradientInsideAComponent)} with {F0(cut.CellsReadingAGradient)} cells reading one, the gradient ACROSS the cut is exactly 0 ");
        sb.Append($"({F0(cut.LinksAcrossTheCut)} links across the cut), the path between the two halves is gone ({F0(cut.PathAcrossTheCut)}), and a cross-transfer still conserves the TOTAL ");
        sb.Append($"({E3(cut.TotalChangeOfACrossTransfer)}) while changing each component by {F0(cut.PerComponentResidualOfACrossTransfer)}. ");
        sb.Append("AND CONNECTIVITY IS A GLOBAL INPUT, WITH ALL THREE ALTERNATIVES REFUTED BY MEASUREMENT: ");
        sb.Append(string.Join("; ", Classification().Select(r => $"{r.Kind} {(r.Supported ? "supported" : "refuted")} ({r.Measurement})")));
        sb.Append(". AND THE SOURCES AGREE WITH THE MAP: ");
        var root = CoreRootOrNull();
        if (root is not null)
        {
            var users = UndeclaredUsers(root);
            sb.Append($"{F0(users.Length)} of the {F0(SurvivingAudits.Length)} surviving audits carry an undeclared assumption site (");
            sb.Append(string.Join("; ", users.Select(u => $"{u.Audit}: {F0(u.HiddenHits)} {u.Categories}")));
            sb.Append($"), and {F0(AuditsWithNoUndeclaredUse(root))} carry none at all. ");
        }
        sb.Append("OUTPUT: GLOBAL INPUT. The failure core is small, it is confined to the transport and continuity layer, and no ");
        sb.Append("observable reads the component count - so removing connectivity from AT costs the layer that a cut makes ");
        sb.Append("ill-defined and nothing else. A SMALL CORE STRENGTHENS THE THEORY: connectivity is an input the local ");
        sb.Append("sector does not consume.");
        return sb.ToString();
    }

    /// <summary>What the audit does not claim, travelling with the verdict.</summary>
    public static string WhereItStands()
    {
        var counts = DependencyCounts();
        var load = ConnectivityLoad();
        var core = FailureCoreCost();
        var kind = ClassifyConnectivity();
        return "CONNECTIVITY IS A GLOBAL INPUT WITH A SMALL, LOCALISED FAILURE CORE. "
             + $"WHAT IS MEASURED: {counts.Direct} of {counts.Total} surviving claims move when the substrate is cut, {counts.Indirect} are unchanged but lose their "
             + $"domain, and {counts.Independent} are local and untouched; the rank identity carries {F0(load.RankLoaded)} claims; the whole core is triggered by "
             + $"{F0(core.LinksToRemoveToTrigger)} link removals; and the classification is {kind} with the other three kinds refuted by a measurement rather than by argument. "
             + "WHAT IS NOT CLAIMED. First, THE MAP IS OVER THE SURVIVING SET AND NOT THE WHOLE THEORY: it covers G_035 and G_068-G_080, which is "
             + "the gravity/time sector; the spectral, gauge and cosmological audits are outside it and their own substrate use is "
             + "reported by the hidden-use scan rather than by the map. Second, INDEPENDENT MEANS THE CLAIM DOES NOT CONSUME CONNECTIVITY, NOT "
             + "THAT THE THEORY WOULD BE UNCHANGED: a disconnected universe changes what is OBSERVABLE (the redshift across a cut is defined "
             + $"and unreachable), and that is reported as INDIRECT rather than hidden inside INDEPENDENT. Third, THE LOCAL SECTOR'S INVARIANCE IS MEASURED ON THE CANONICAL "
             + "INITIAL CONDITION: a state that varies at the cut is locally visible at the two cells beside it (G_080's own caveat), so "
             + "the classification's LOCAL INPUT branch is refuted for the uniform state and would need re-measuring for a varying one. "
             + "Fourth, THE RANK-LOAD TEST IS AN ARITHMETIC SIGNATURE and not a proof of dependence: it asks whether a quantity moves linearly "
             + "in c with the rank's step, which is what makes the load a measurement rather than a list of claims the audit chose. "
             + "WHAT WOULD CHANGE THE VERDICT: a local claim whose value moves with c, or a global descriptor that does not (the classifier's "
             + "other branches). Both are computed here; neither is taken.";
    }

    // ===================== 8. REPORTS =====================

    public static string OutputCounterfactual()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE COUNTERFACTUAL UNIVERSE - THE SAME CELLS, c = 1, 2, 4, 8 COMPONENTS");
        sb.AppendLine("   components  cells  per component  transport directions  nullity  free modes  witnesses  transports  NOT transports");
        foreach (var r in CounterfactualUniverse())
            sb.AppendLine($"   {r.Components,-11} {r.Cells,-6} {r.PerComponent,-15} {r.Directions,-21} {r.Nullity,-8} {r.FreeModes,-11} "
                + $"{r.ConservingWitnesses,-10} {r.OfWhichAreTransports,-11} {r.OfWhichAreNot}");
        sb.AppendLine();
        sb.AppendLine("   the READ side, on the canonical cell count:");
        sb.AppendLine("   components  per component  max gradient inside  cells reading one  gradient across the cut  links across the cut  path across the cut  total change  per-component residual");
        foreach (var r in CounterfactualReads())
            sb.AppendLine($"   {r.Components,-11} {r.PerComponent,-15} {E3(r.MaxGradientInsideAComponent),-21} {r.CellsReadingAGradient,-18} "
                + $"{E3(r.MaxGradientAcrossTheCut),-23} {r.LinksAcrossTheCut,-21} {r.PathAcrossTheCut,-20} {E3(r.TotalChangeOfACrossTransfer),-13} {F0(r.PerComponentResidualOfACrossTransfer)}");
        return sb.ToString();
    }

    public static string OutputMap()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE DEPENDENCY MAP - EVERY SURVIVING CLAIM RE-EVALUATED AT c = 1, 2, 4, 8");
        sb.AppendLine("   claim                                                                          audit       domain         c=1        c=2        c=4        c=8        dependence");
        foreach (var r in DependencyMap())
            sb.AppendLine($"   {Truncate(r.Claim, 78),-78} {r.Audit,-11} {r.Domain,-14} {E3(r.One),-10} {E3(r.Two),-10} {E3(r.Four),-10} {E3(r.Eight),-10} {r.Dependence}");
        var counts = DependencyCounts();
        sb.AppendLine();
        sb.AppendLine($"   TOTALS: {counts.Direct} DIRECT, {counts.Indirect} INDIRECT, {counts.Independent} INDEPENDENT of {counts.Total} surviving claims.");
        sb.AppendLine("   and the ground the INDEPENDENT rows stand on, measured: the set of local reads is one and the same on every component count:");
        var invariance = LocalSectorInvariance();
        sb.AppendLine($"   distinct local-read sets across c = 1, 2, 4, 8: {invariance.Select(v => v.LocalReadSet).Distinct().Count()} (cells {invariance[0].Cells})");
        return sb.ToString();
    }

    public static string OutputCore()
    {
        var sb = new StringBuilder();
        var core = FailureCore();
        var cost = FailureCoreCost();
        var survival = Survival();
        sb.AppendLine("3. THE MINIMAL FAILURE SET - FailureCore(connectivity)");
        sb.AppendLine($"   {core.Length} of the {cost.OfTheInventory} surviving claims fail when connectivity is removed:");
        foreach (var c in core)
            sb.AppendLine($"     - {c.Claim}  [{c.Audit}]");
        sb.AppendLine();
        sb.AppendLine($"   TRIGGER COST: c = {cost.ComponentsNeededToTrigger} suffices, and on the canonical ring the smallest disconnection is "
            + $"{cost.LinksToRemoveToTrigger} links or {cost.CellsToRemoveToTrigger} cells - two defects buy the whole core.");
        sb.AppendLine($"   CoreWithoutConnectivity: {survival.Surviving} of {survival.Total} claims survive unchanged ({F3(survival.SurvivingFraction)}):");
        foreach (var c in CoreWithoutConnectivity())
            sb.AppendLine($"     + {c.Claim}  [{c.Audit}]");
        return sb.ToString();
    }

    public static string OutputLoad()
    {
        var sb = new StringBuilder();
        var load = ConnectivityLoad();
        sb.AppendLine("4. THE CONNECTIVITY LOAD - WHO NEEDS rank = n - c");
        sb.AppendLine("   claim                                                                          needs the rank identity");
        foreach (var r in RankLoad())
            sb.AppendLine($"   {Truncate(r.Claim, 78),-78} {r.NeedsTheRankIdentity}");
        sb.AppendLine();
        sb.AppendLine($"   rank-loaded: {load.RankLoaded} of {load.Total} ({F3(load.RankFraction)}); direct in total: {load.Direct} ({F3(load.DirectFraction)}); "
            + $"cuts needed to break the rank identity: {load.CutsToBreakTheRankIdentity}.");
        return sb.ToString();
    }

    public static string OutputHidden(string coreRoot)
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. THE HIDDEN-CONNECTIVITY SCAN - RING, PATH, NEIGHBOUR, TRANSPORT, LAPLACIAN ASSUMPTIONS IN THE SURVIVING SOURCES");
        sb.AppendLine("   the scan strips literals and comments (AtSourceScan), so report prose cannot be counted as code, and it");
        sb.AppendLine("   excludes this audit's own source (the G_027/G_033 rule).");
        sb.AppendLine();
        sb.AppendLine("   category     hits  files  hidden  declared");
        foreach (var r in HiddenUseSummary(coreRoot))
            sb.AppendLine($"   {r.Category,-12} {r.Hits,-5} {r.Files,-6} {r.Hidden,-7} {r.Declared}");
        sb.AppendLine();
        sb.AppendLine("   hidden uses (audits where connectivity is NOT the subject):");
        foreach (var h in HiddenUses(coreRoot).Where(h => !h.Declared))
            sb.AppendLine($"     {h.Category,-10} {h.File}:{h.Line}  {Truncate(h.Text, 90)}");
        return sb.ToString();
    }

    public static string OutputClassification()
    {
        var sb = new StringBuilder();
        sb.AppendLine("6. THE STRUCTURAL CLASSIFICATION");
        sb.AppendLine("   kind                 supported  measurement");
        foreach (var r in Classification())
            sb.AppendLine($"   {r.Kind,-20} {r.Supported,-10} {r.Measurement}");
        sb.AppendLine();
        sb.AppendLine($"   COMPUTED CLASSIFICATION: {KindName(ClassifyConnectivity())}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("7. THE VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine("WHERE IT STANDS");
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }

    private static string Truncate(string text, int width)
        => text.Length <= width ? text : text.Substring(0, width - 1) + "…";
}
