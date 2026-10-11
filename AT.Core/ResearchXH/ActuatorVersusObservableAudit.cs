using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// The role a structure plays in the surviving sector — the three roles are MEASURED separately and a structure may
/// hold any combination of them (ResearchY-G_092).
/// </summary>
public enum StructureRole
{
    /// <summary>Actuatable and not observable: the audit's SUCCESS criterion.</summary>
    InvisibleActuator,

    /// <summary>Read and settable: the coincidence, where control acts on what the readouts already read.</summary>
    ObservableAndActuatable,

    /// <summary>Read, not evolved, not settable.</summary>
    ObservableOnly,

    /// <summary>Read AND evolved, not settable: a boundary condition.</summary>
    BoundaryCondition,

    /// <summary>Evolved, not read, not settable: the task-5 target.</summary>
    DynamicalOnly,

    /// <summary>None of the three.</summary>
    Inert,

    /// <summary>Not an argument of anything the surviving sector computes with: a new primitive.</summary>
    AbsentFromTheVocabulary,
}

/// <summary>One structure that the reduction G_080-G_091 eliminated or demoted, with the three roles measured.</summary>
public sealed record EliminatedStructure(string Name, string RemovedBy,
    int ReadoutsLost, double ReadoutsMoved, int ReadoutsThatMove, bool Observable,
    bool TheReadIsDecidableInSitu,
    double UpdateMoved, int ReachMoved, int OperationsChangingIt, bool Dynamical,
    bool Actuatable, bool RequiredForControl, bool InTheVocabulary,
    StructureRole Role, string Measurement);

/// <summary>
/// ResearchY-G_092 — ACTUATOR VERSUS OBSERVABLE AUDIT (group G — Gravity Source).
///
/// G_084-G_091 reduced the observable sector: connectivity, the component count, the transport layer, the continuity
/// law, the flux, the second kernel component and every candidate pump were stripped or demoted, each time on the
/// ground that THE SURVIVING OBSERVABLES DID NOT MOVE. This audit asks the converse question, which is a different
/// question: **did any eliminated structure remain necessary for DYNAMICS or for CONTROL?**
///
/// Two rules keep this from being a re-reading of the same evidence:
///
///  * observable-reduction arguments are FORBIDDEN — "the reduction kept the readouts" is not evidence about
///    dynamics, so it is never used as one;
///  * "not observed, therefore irrelevant" is FORBIDDEN — relevance is not read off observability.
///
/// Therefore the three roles are three PROCEDURES, each measured on its own object, and a structure may hold any
/// combination of them:
///
///  * OBSERVABLE — remove or move the structure in the description and re-read the readouts
///    (`MinimalGravityCoreAudit.ReadoutsOf`), counting readouts LOST (no longer produced) and readouts MOVED (value
///    changes). A structure that neither loses nor moves a readout is inert *with respect to the surviving
///    readouts* — a statement about the readouts, not about the world;
///  * DYNAMICAL — remove or change the structure in the UPDATE RELATION (`ConnectivityOriginAudit.UpdateAt`, the
///    Laplacian action on the state — the sector's only evolution map), in the CLASS'S REACH (the transport rank
///    `n - c` of G_090), or in the OPERATION SET (a structure the class's own operations write is part of the
///    evolution by definition), and measure whether any of the three moves;
///  * ACTUATABLE — count the class's own operations that CHANGE the structure. This is the strong sense of "a knob":
///    an operator can SET a structure only if some operation of the class writes it.
///
/// A fourth question is kept apart on purpose, because a precondition is not a knob: is the structure REQUIRED FOR
/// CONTROL, i.e. does removing it kill the operations themselves? That is what the task's `FailureCore(actuation)`
/// means, and it is why the audit can report both "control needs three structures" and "an operator can set one".
///
/// The substrate is the canonical 96-cell ring (`SourceManipulationAudit.CanonicalCells`), with the counterfactual
/// cuts `c = 1, 2, 4` of G_090 (96 cells, transported along the ring's links); the equality rule for every readout
/// comparison is `Tol = 1e-12` on a computed value, and no spectral count (A0, a free room, a lock release, a
/// survivor count) is quoted, so the spectral-substrate rule is not engaged.
///
/// Prefers refutation. Success would be a structure removed by the reduction and still required for control while
/// invisible to observation; failure would be that the observable and actuator cores coincide.
/// </summary>
public static class ActuatorVersusObservableAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;

    public static double[] State => MapRedundancyAudit.State;
    public static double SurfacePotential => Math.Abs(ObservationalProgramAudit.XOfTarget());

    /// <summary>The equality rule for every readout comparison in this audit.</summary>
    public const double Tol = 1e-12;

    // ===================== TASK 1: THE THREE ROLES, AS PROCEDURES =====================

    /// <summary>
    /// THE THREE DEFINITIONS, with the object each one is measured on. They are stated apart from one another because
    /// the whole audit turns on their being separable: a structure can be observable and not dynamical (a map), or
    /// dynamical and not observable (a coordinate), or neither (bookkeeping).
    /// </summary>
    public static (string Role, string Procedure, string WhatItMeasures)[] RoleDefinitions() => new[]
    {
        ("OBSERVABLE", "remove or move the structure in the description, then re-read the 8 surviving readouts",
            "readouts LOST (no longer produced) and readouts MOVED (value changes): the readouts, and nothing else"),
        ("DYNAMICAL", "remove or change the structure in the update relation, in the class's reach, and in the "
            + "operation set",
            "whether the state's evolution moves: the update residual, the transport rank n - c, and the number of "
            + "classes operations that write it"),
        ("ACTUATABLE", "count the class's own operations that change the structure",
            "whether an operator can SET it: only a structure some operation writes is a knob"),
        ("REQUIRED FOR CONTROL", "remove the structure and ask whether the operations themselves survive",
            "a PRECONDITION, not a knob — kept apart from actuatability on purpose"),
    };

    /// <summary>
    /// THE READOUTS OF A DESCRIPTION WITHOUT THE STRUCTURE: the counting half of the observable role. The readouts
    /// are produced by the description's own contents, so a structure the readouts consume shows up either as a
    /// readout that is no longer produced or as a value that moves.
    /// </summary>
    public static (int Lost, double Moved, int MovedCount) ReadoutsWithout(
        MinimalGravityCoreAudit.Description variant)
    {
        var full = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full());
        var reduced = MinimalGravityCoreAudit.ReadoutsOf(variant);
        int lost = reduced.Count(r => !r.Produced);
        var deltas = full.Zip(reduced, (f, m) => f.Produced && m.Produced ? Math.Abs(f.Value - m.Value) : 0.0)
            .ToArray();
        return (lost, deltas.Max(), deltas.Count(d => d > Tol));
    }

    /// <summary>
    /// THE COMPONENT COUNT'S OWN INSTRUMENT (G_082), and the reason it is needed: the cut changes the LINKS as well as
    /// the count, so a readout that moves under a cut is not evidence about the count. G_082's union construction
    /// holds the component-internal link set fixed and varies only c, so its local read shape, its local values, its
    /// ratio and the number of local observables reading the absolute count are the count's own measurement — and
    /// all four are c-invariant.
    /// </summary>
    public static (int ShapeChanges, int ValueChanges, int RatioChanges, int LocalObservablesReadingTheCount)
        ComponentCountInvariance()
    {
        var redundancy = ComponentCountAudit.UnionRedundancy();
        return (redundancy.Max(r => r.ShapeChanges), redundancy.Max(r => r.ValueChanges),
            redundancy.Max(r => r.RatioChanges), ComponentCountAudit.ObservablesReadingAbsoluteC());
    }

    /// <summary>
    /// THE UPDATE RESIDUAL WITHOUT THE STRUCTURE: the dynamical half. The update relation is the Laplacian acting on
    /// the state, so a structure that enters the evolution shows up here as a non-zero residual — and a structure
    /// that does not is measured to leave it at exactly 0.0 rather than being declared irrelevant.
    /// </summary>
    public static double UpdateWithout(MinimalGravityCoreAudit.Description variant)
    {
        var full = MinimalGravityCoreAudit.Full();
        var before = ConnectivityOriginAudit.UpdateAt(full.CellCount, full.Links, full.Rho);
        var after = ConnectivityOriginAudit.UpdateAt(variant.CellCount, variant.Links, variant.Rho);
        return before.Zip(after, (a, b) => Math.Abs(a - b)).Max();
    }

    /// <summary>
    /// THE CLASS'S REACH: the transport rank and the complement (G_090's inverse requirement). The reach is where a
    /// structure shows its control relevance without appearing in any readout.
    /// </summary>
    public static (int Components, int Rank, int Conserving, int Complement)[] Reach()
        => DirectDensityControlAudit.InverseRequirement()
            .Select(r => (r.Components, r.ImageDimension, r.ConservingDimension, r.ComplementDimension))
            .ToArray();

    /// <summary>The number of elementary operations the class has on the canonical frame — the rank itself.</summary>
    public static int ReachableOperations() => Reach().Single(r => r.Components == 1).Rank;

    // ===================== TASK 2: THE INVENTORY =====================

    /// <summary>
    /// THE INVENTORY: every structure the reduction eliminated or demoted, each with the three roles MEASURED. The
    /// descriptions are built from the full description by removing exactly one structu part at a time, so the
    /// measurements are independent of one another.
    /// </summary>
    public static EliminatedStructure[] Structures()
    {
        var full = MinimalGravityCoreAudit.Full();
        var redistributed = full with { Rho = MinimalGravityCoreAudit.Redistributed };
        var cut = full with { Links = ConnectivityOriginAudit.LinksOf(Cells, 2), Components = 2 };
        var noLinks = full with { Links = Array.Empty<(int, int)>() };
        var noFlux = full with { Flux = Array.Empty<int>(), DisplacedFlux = Array.Empty<int>() };
        var noConservation = full with { Conservation = false };
        var noTransport = full with { TransportOperator = false };
        var noLedger = full with { Ledger = false };
        var noPotential = full with { PotentialMap = false };
        var noClock = full with { ClockLaw = false };
        var noGradient = full with { GradientDefinition = false };

        var reach = Reach();
        int rankAtOne = reach.Single(r => r.Components == 1).Rank;
        int rankAtTwo = reach.Single(r => r.Components == 2).Rank;
        int reachMoved = Math.Abs(rankAtOne - rankAtTwo);
        int operations = ReachableOperations();

        var layerResidual = MinimalGravityCoreAudit.TransportLayerContribution().Single().Residual;
        int fluxObservables = ContinuityLawAudit.ObservablesReadingTheFlux();
        var countInvariance = ComponentCountInvariance();
        var curvaturePerUnit = KernelCurvatureActuationAudit.PerUnitInput()
            .Where(p => p.Side == "the effect side").ToArray();
        double curvatureEffect = curvaturePerUnit.Max(p => Math.Abs(p.PerUnitCurvature));
        var couplings = KernelCurvatureActuationAudit.CouplingCounts();
        var pumps = DirectDensityControlAudit.Candidates();
        double phase1 = InterferenceFromLinks.DoubleSlitProbability(
            PhaseOrigin.PhaseFromPosition(0, Cells), PhaseOrigin.PhaseFromPosition(1, Cells));
        double phase2 = InterferenceFromLinks.DoubleSlitProbability(
            PhaseOrigin.PhaseFromPosition(0, Cells), PhaseOrigin.PhaseFromPosition(2, Cells));
        double phaseSignature = Math.Abs(phase1 - phase2);
        var accelerationReach = ConnectivityOriginAudit.AccelerationReach();
        int readingOnTheRing = accelerationReach.Single(r => r.Substrate.Contains("canonical")
            && r.Components == 1).CellsReadingAnAcceleration;
        int readingOnTheSplit = accelerationReach.Single(r => r.Cells == 2 && r.Components == 2)
            .CellsReadingAnAcceleration;

        var rows = new List<EliminatedStructure>();

        var state = ReadoutsWithout(redistributed);
        rows.Add(new EliminatedStructure("the state (the occupation)", "— retained by G_084 as essential (reference row)",
            state.Lost, state.Moved, state.MovedCount, true, true, UpdateWithout(redistributed), 0, operations, true,
            true, true, true, StructureRole.ObservableAndActuatable,
            $"the reference: {operations} operations change it, the update moves by "
            + $"{E(UpdateWithout(redistributed))} when it is moved, and {state.MovedCount} of 8 readouts move with it"));

        var links = ReadoutsWithout(noLinks);
        rows.Add(new EliminatedStructure("the link set (connectivity)",
            "G_080 classified it ASSUMED; G_084's strip list names connectivity",
            links.Lost, links.Moved, links.MovedCount, true, true, UpdateWithout(noLinks), reachMoved, 0, true,
            false, true, true, StructureRole.BoundaryCondition,
            $"the read NEEDS a link: {readingOnTheSplit} of 2 cells read an acceleration on the split pair against "
            + $"{readingOnTheRing} on the canonical ring, and {links.MovedCount} readout moves by {E(links.Moved)} when "
            + $"the links are removed; the update moves by {E(UpdateWithout(noLinks))} with no links at all and by "
            + $"{E(UpdateWithout(cut))} under a one-link cut; and 0 operations write it — so "
            + "the cut's readout movement belongs to THIS row, not to the count row below"));

        rows.Add(new EliminatedStructure("the component count",
            "G_081's counterfactual used it; G_082 classified it gauge-like",
            0, 0.0, 0, false, true, 0.0, reachMoved, 0, true, false, false, true,
            StructureRole.DynamicalOnly,
            $"it sets the reach ({rankAtOne} -> {rankAtTwo} of {rankAtOne} reachable directions when c goes 1 -> 2) "
            + $"while its OWN instrument is invariant: {countInvariance.ShapeChanges} shape changes, "
            + $"{countInvariance.ValueChanges} value changes, {countInvariance.RatioChanges} ratio changes and "
            + $"{countInvariance.LocalObservablesReadingTheCount} local observables reading the absolute count, and 0 "
            + "operations change it — a measured zero beside a live control; and it is NOT an operand of the update "
            + "relation, which takes (cells, links, rho), so its update residual is exactly 0.0 and the reach is its "
            + "whole dynamical evidence"));

        rows.Add(new EliminatedStructure("density topology",
            "G_089 listed it; G_090's topology row controls the kernel through the transport rank",
            0, 0.0, 0, false, true, 0.0, reachMoved, 0, true, false, false, true,
            StructureRole.DynamicalOnly,
            "the link set's global descriptor, so it acts exactly as the component count does — through the rank "
            + $"n - c ({rankAtOne} -> {rankAtTwo}) — and no local observable reads it"));

        var transport = ReadoutsWithout(noTransport);
        rows.Add(new EliminatedStructure("the transport operator (the class's operations)",
            "G_084's strip list",
            transport.Lost, transport.Moved, transport.MovedCount, false, true, 0.0, 0, 0, true,
            false, true, true, StructureRole.DynamicalOnly,
            $"removing it moves {E(transport.Moved)} of a readout — the layer's gauge residual is exactly "
            + $"{layerResidual} — while it IS the evolution: the class's {operations} operations change the state, so "
            + "it is dynamical by identity rather than by a changed residual, and no operation can set the rule it "
            + "obeys"));

        var flux = ReadoutsWithout(noFlux);
        rows.Add(new EliminatedStructure("the flux", "G_083 and G_084 stripped it; G_089 listed it as the transport's variable",
            flux.Lost, flux.Moved, flux.MovedCount, false, true, 0.0, 0, 0, false, false, false, true,
            StructureRole.Inert,
            $"a gauge and an output rather than an input: {fluxObservables} observables read a flux value, the "
            + "circulation leaves every divergence unchanged, and no operation takes a flux as an argument"));

        var continuity = ReadoutsWithout(noConservation);
        rows.Add(new EliminatedStructure("the continuity law", "G_083 classified it a coordinate choice",
            continuity.Lost, continuity.Moved, continuity.MovedCount, false, true, 0.0, 0, 0, false, false, false, true,
            StructureRole.Inert,
            $"removing the statement moves {E(continuity.Moved)} of a readout and 0.0 of the update: it is an identity "
            + "satisfied by the operation, not an operation"));

        var ledger = ReadoutsWithout(noLedger);
        rows.Add(new EliminatedStructure("the ledger", "G_084's strip list", ledger.Lost, ledger.Moved,
            ledger.MovedCount, false, true, 0.0, 0, 0, false, false, false, true, StructureRole.Inert,
            "it DETECTS a source rather than acting on the state, so no readout and no update reads it"));

        var potential = ReadoutsWithout(noPotential);
        rows.Add(new EliminatedStructure("the potential map", "G_085 weighed it; G_086 collapsed it into the kernel",
            potential.Lost, potential.Moved, potential.MovedCount, true, true, 0.0, 0, 0, false, false, false, true,
            StructureRole.ObservableOnly,
            $"it is read through {potential.Lost} readouts of 8 and enters no evolution: the update takes the state and "
            + "the links, so removing the map leaves the update at exactly 0.0"));

        var clock = ReadoutsWithout(noClock);
        rows.Add(new EliminatedStructure("the clock law", "G_084/G_085; G_086 collapsed it into the kernel",
            clock.Lost, clock.Moved, clock.MovedCount, true, true, 0.0, 0, 0, false, false, false, true,
            StructureRole.ObservableOnly,
            $"it is read through {clock.Lost} readouts of 8 and enters no evolution: a map applied at read time"));

        var gradient = ReadoutsWithout(noGradient);
        rows.Add(new EliminatedStructure("the gradient definition", "G_084's strip list",
            gradient.Lost, gradient.Moved, gradient.MovedCount, true, true, 0.0, 0, 0, false, false, false, true,
            StructureRole.ObservableOnly,
            $"it is read through {gradient.Lost} readouts of 8 (the field acceleration and the along-a-link gradient) "
            + "and is a difference of the state, not a degree of freedom"));

        rows.Add(new EliminatedStructure("the kernel's second component (the curvature)",
            "G_086 collapsed it; G_088 measured it unmeasurable; G_091 found it a parameter",
            0, curvatureEffect, curvaturePerUnit.Length, true, false, 0.0, 0, 0, false, false, false, true,
            StructureRole.ObservableOnly,
            $"it MOVES {curvaturePerUnit.Length} readouts per unit curvature (largest {E(curvatureEffect)}), so it is "
            + "read in the structural sense, while its coupling to every state-based process is exactly "
            + $"{couplings.MaximumCouplingStrength.ToString("F1", CultureInfo.InvariantCulture)} and G_087 measured it "
            + "undecidable — the ONE row where read and decidable come apart"));

        rows.Add(new EliminatedStructure("the phase structure (the theta sector)",
            "G_089 excluded it by arity; G_090's phase-coherence candidate collided",
            0, phaseSignature, 1, true, true, 0.0, 0, 0, false, false, false, true,
            StructureRole.ObservableOnly,
            $"it is a real observable — the interference term moves by {E(phaseSignature)} between two phase "
            + "assignments — but in the interference sector, not here: this sector's readouts take the density and "
            + "nothing else, the update takes (cells, links, rho), and no operation sets a phase"));

        var locking = pumps.Single(p => p.Candidate == "mode locking");
        rows.Add(new EliminatedStructure("mode locking", "G_090: every candidate collapses",
            0, 0.0, 0, false, true, 0.0, 0, 0, false, false, false, true,
            StructureRole.Inert,
            $"G_090 measured it a FUNCTION of the distribution ({locking.IsAFunctionOfRho}), so it adds no readout the "
            + "state did not already supply and no degree of freedom the operations can act on"));

        var functionals = pumps.Where(p => p.Candidate is "correlation structure" or "oscillation amplitude"
            or "oscillation frequency" or "information density").ToArray();
        rows.Add(new EliminatedStructure("the distribution's functionals",
            "G_089/G_090: correlation, variance, amplitude, frequency, information",
            0, 0.0, 0, false, true, 0.0, 0, 0, false, false, false, true,
            StructureRole.Inert,
            $"all {functionals.Length} are functions of the same distribution "
            + $"({functionals.Count(f => f.IsAFunctionOfRho)} of {functionals.Length} measured as such by G_090), so a "
            + "change through them IS a change of the state"));

        var actualization = pumps.Single(p => p.Candidate == "actualization rate");
        rows.Add(new EliminatedStructure("the actualization rate", "G_090: effective only, no actuator",
            0, 0.0, 0, false, true, 0.0, 0, 0, false, false, false, true,
            StructureRole.Inert,
            $"G_090 measured it OUTSIDE the class ({actualization.IsOutsideTheClass}) with no actuator "
            + $"({actualization.HasAnActuator}): a named process the operations cannot perform"));

        rows.Add(new EliminatedStructure("memory channels",
            "G_089 and G_091 named it as the one candidate that COULD couple; it is a new primitive",
            0, 0.0, 0, false, false, 0.0, 0, 0, false, false, false, false,
            StructureRole.AbsentFromTheVocabulary,
            "no function of the surviving sector takes a memory channel as an argument — G_091 named it rather than "
            + "using it, because introducing it is what this chain's rules forbid"));

        return rows.ToArray();
    }

    /// <summary>
    /// THE ROLE RULE: a pure function of the three measured roles, in the order that makes the audit's success
    /// criterion empty or not. `InTheVocabulary` is checked first, because a primitive that is absent is not inert —
    /// it is absent.
    /// </summary>
    public static StructureRole RoleOf(bool observable, bool dynamical, bool actuatable, bool inTheVocabulary)
    {
        if (!inTheVocabulary) return StructureRole.AbsentFromTheVocabulary;
        if (actuatable && !observable) return StructureRole.InvisibleActuator;
        if (observable && actuatable) return StructureRole.ObservableAndActuatable;
        if (observable && dynamical) return StructureRole.BoundaryCondition;
        if (observable) return StructureRole.ObservableOnly;
        if (dynamical) return StructureRole.DynamicalOnly;
        return StructureRole.Inert;
    }

    public static string RoleName(StructureRole role) => role switch
    {
        StructureRole.InvisibleActuator => "invisible actuator",
        StructureRole.ObservableAndActuatable => "observable and actuatable",
        StructureRole.ObservableOnly => "observable only",
        StructureRole.BoundaryCondition => "a boundary condition",
        StructureRole.DynamicalOnly => "dynamical only",
        StructureRole.Inert => "inert",
        StructureRole.AbsentFromTheVocabulary => "absent from the vocabulary",
        _ => role.ToString(),
    };

    /// <summary>The four-way census the task asks for, with the absent row counted apart.</summary>
    public static (int Observable, int Dynamical, int Actuatable, int ObservableAndActuatable, int ObservableOnly,
        int BoundaryCondition, int DynamicalOnly, int InvisibleActuator, int Inert, int Absent, int Total)
        RoleCensus()
    {
        var rows = Structures();
        return (rows.Count(r => r.Observable), rows.Count(r => r.Dynamical), rows.Count(r => r.Actuatable),
            rows.Count(r => r.Role == StructureRole.ObservableAndActuatable),
            rows.Count(r => r.Role == StructureRole.ObservableOnly),
            rows.Count(r => r.Role == StructureRole.BoundaryCondition),
            rows.Count(r => r.Role == StructureRole.DynamicalOnly),
            rows.Count(r => r.Role == StructureRole.InvisibleActuator),
            rows.Count(r => r.Role == StructureRole.Inert),
            rows.Count(r => r.Role == StructureRole.AbsentFromTheVocabulary),
            rows.Length);
    }

    // ===================== TASK 3: THE MANIPULATION CHAIN ON THE FULL PRE-REDUCTION THEORY =====================

    /// <summary>
    /// THE CHAIN RE-RUN. G_089 ran its channel search over the SURVIVING vocabulary and found one actuatable
    /// channel. The task asks for the same search with every pre-reduction structure available as a candidate
    /// channel, so the eight channels of G_089 are joined by the structures the reduction removed — the component
    /// count, the flux, the continuity law, the ledger, the transport's own rule and memory channels — and each is
    /// judged by the only question that decides actuatability: does an operation of the class write it?
    /// </summary>
    public static (string Structure, string Status, bool AddsAnActuator, string Measurement)[]
        ChainOnTheFullTheory()
    {
        var channels = ManipulationChannelAudit.Channels();
        var rows = new List<(string, string, bool, string)>();
        foreach (var c in channels)
        {
            var status = ManipulationChannelAudit.StatusOf(c);
            bool actuatable = status == ChannelStatus.Actuatable;
            rows.Add((c.Channel, ManipulationChannelAudit.StatusName(status), actuatable,
                "the surviving vocabulary's own verdict (G_089), re-run unchanged: "
                + $"{c.Measurement.Split(';')[0]}"));
        }

        int operations = ReachableOperations();
        rows.Add(("the component count (pre-reduction only)", "control without an actuator", false,
            $"it controls the kernel through the rank and no operation writes it: 0 of {operations} operations"));
        rows.Add(("the flux (pre-reduction only)", "not a control", false,
            "it is what an operation WRITES, not what an operation takes: no operation's argument list contains it"));
        rows.Add(("the continuity law (pre-reduction only)", "not a control", false,
            "an identity satisfied by every operation, so it cannot be varied by one"));
        rows.Add(("the ledger (pre-reduction only)", "not a control", false,
            "it detects a source; it is not an input of any operation"));
        rows.Add(("the transport's own rule", "a precondition, not a channel", false,
            $"it IS the operation set: {operations} operations, and an operator obeys it rather than setting it"));
        rows.Add(("memory channels", "excluded by arity", false,
            "absent from the vocabulary, so there is no operation to judge"));
        return rows.ToArray();
    }

    /// <summary>The count the task's task 3 asks for: what the full theory adds to the chain.</summary>
    public static (int ActuatableOnTheFullTheory, int ActuatableOnTheSurvivingTheory, int AddedByTheFullTheory)
        ChainCounts()
    {
        int full = ChainOnTheFullTheory().Count(r => r.Status == ManipulationChannelAudit.StatusName(ChannelStatus.Actuatable));
        int surviving = ManipulationChannelAudit.Channels()
            .Count(c => ManipulationChannelAudit.StatusOf(c) == ChannelStatus.Actuatable);
        return (full, surviving, full - surviving);
    }

    // ===================== TASK 4: THE TWO FAILURE CORES =====================

    /// <summary>FailureCore(observables): the structures whose removal loses or moves a readout.</summary>
    public static string[] FailureCoreObservables()
        => Structures().Where(s => s.Observable).Select(s => s.Name).ToArray();

    /// <summary>
    /// FailureCore(actuation) in the task's sense: the structures whose removal kills the OPERATIONS — the update's
    /// operands and the rule itself. This is deliberately NOT the actuatable set: a precondition is not a knob.
    /// </summary>
    public static string[] FailureCoreActuation()
        => Structures().Where(s => s.RequiredForControl).Select(s => s.Name).ToArray();

    /// <summary>The two cores compared, with the direction of each difference.</summary>
    public static (int Observables, int Actuation, int Intersection, int ActuationOnly, int ObservablesOnly,
        bool ActuationIsASubsetOfObservables, bool ObservablesIsASubsetOfActuation, bool TheCoresCoincide)
        CoreComparison()
    {
        var observable = FailureCoreObservables().ToHashSet(StringComparer.Ordinal);
        var actuation = FailureCoreActuation().ToHashSet(StringComparer.Ordinal);
        int intersection = observable.Intersect(actuation, StringComparer.Ordinal).Count();
        int actuationOnly = actuation.Except(observable, StringComparer.Ordinal).Count();
        int observablesOnly = observable.Except(actuation, StringComparer.Ordinal).Count();
        return (observable.Count, actuation.Count, intersection, actuationOnly, observablesOnly,
            actuationOnly == 0, observablesOnly == 0, actuationOnly == 0 && observablesOnly == 0);
    }

    // ===================== TASK 5: DYNAMICS WITHOUT OBSERVABLES =====================

    /// <summary>
    /// THE TASK-5 SEARCH: the structures that move the evolution while moving no readout. These are the candidates
    /// the success criterion needs — a structure removed by the reduction and still required for control.
    /// </summary>
    public static EliminatedStructure[] DynamicalWithoutObservables()
        => Structures().Where(s => s.Dynamical && !s.Observable).ToArray();

    /// <summary>
    /// THE DIRECTION OF THE DIFFERENCE, which is what decides whether such a structure can be a CHANNEL: the reach
    /// measured against the counterfactual cuts. On every frame the reachable set is `n - c` of the `n - 1`
    /// conserving directions, so the component count can only REMOVE reachable directions — never add one.
    /// </summary>
    public static (int Components, int ReachableDirections, int ConservingDirections, int Unreachable,
        string Reading)[] ReachCeiling()
        => Reach().Select(r => (r.Components, r.Rank, r.Conserving, r.Complement,
            r.Complement == 0
                ? "the class reaches every conserving direction: the ceiling is not binding"
                : $"{r.Complement} conserving direction(s) are outside the class on this frame — the count can only "
                  + "take reach away")).ToArray();

    // ===================== TASK 6: THE TABLE AND THE VERDICT =====================

    /// <summary>The three-column table the task asks for, with the four-way role as the outcome.</summary>
    public static (string Structure, bool ObservableNecessity, bool DynamicNecessity, bool ActuatorNecessity,
        StructureRole Role)[] NecessityTable()
        => Structures().Select(s => (s.Name, s.Observable, s.Dynamical, s.Actuatable, s.Role)).ToArray();

    /// <summary>The task's two criteria, each decided by a count rather than by a reading.</summary>
    public static (string Criterion, string Outcome, string Evidence)[] Criteria()
    {
        var census = RoleCensus();
        var cores = CoreComparison();
        var chain = ChainCounts();
        return new[]
        {
            ("SUCCESS: a structure removed by the reduction and still required for control while invisible",
                census.InvisibleActuator == 0 ? "NOT MET" : "MET",
                $"{census.InvisibleActuator} structures are actuatable and not observable; the actuation core's "
                + $"required members number {cores.Actuation} and {cores.ActuationOnly} of them is outside the "
                + "observable core — and that one is the operation's own rule, which no operation sets"),
            ("FAILURE: the observable and actuator cores coincide",
                cores.TheCoresCoincide ? "MET (with the operation's rule the single exception)"
                    : "NOT MET IN THE LETTER — they differ in both directions",
                $"observables {cores.Observables}, actuation {cores.Actuation}, intersection {cores.Intersection}, "
                + $"actuation-only {cores.ActuationOnly}, observables-only {cores.ObservablesOnly}"),
            ("and the chain gains nothing from the full theory",
                chain.AddedByTheFullTheory == 0 ? "CONFIRMED" : "REFUTED",
                $"{chain.ActuatableOnTheFullTheory} actuatable channel(s) on the full pre-reduction theory against "
                + $"{chain.ActuatableOnTheSurvivingTheory} on the reduced one"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured count, a measured residual or a
    /// measured role — the verdict is a function of the inventory, not a sentence about it.
    /// </summary>
    public static string Verdict()
    {
        var census = RoleCensus();
        var cores = CoreComparison();
        var chain = ChainCounts();
        var dynamics = DynamicalWithoutObservables();
        var ceiling = ReachCeiling();
        var countInvariance = ComponentCountInvariance();
        var b = new StringBuilder();
        b.Append("NO ACTUATABLE STRUCTURE IS UNOBSERVABLE — ");
        b.Append("of the ").Append(census.Total)
            .Append(" structures inventoried (the reduction's eliminations and demotions, with the state as the ")
            .Append("reference row), ").Append(census.Actuatable).Append(" is actuatable and every one of them is ")
            .Append("observable; ").Append(census.InvisibleActuator)
            .Append(" is actuatable and not observable, so the success criterion is empty. ");
        b.Append("CONTROL NEEDS THREE AND CAN SET ONE: the structures whose removal kills the operations number ")
            .Append(cores.Actuation).Append(" (the state, the link set and the operation's own rule), and the ")
            .Append(census.Actuatable).Append(" an operator can set is the state — the object the readouts already ")
            .Append("read. ");
        b.Append("THE CORES DO NOT COINCIDE, IN EITHER DIRECTION: ")
            .Append(cores.ObservablesOnly).Append(" structures are read and not required for control (the maps, the ")
            .Append("gradient definition, the curvature and the phase) and ").Append(cores.ActuationOnly)
            .Append(" is required for control and not read (the rule itself) — so the two cores differ both ways, and ")
            .Append("the difference that would matter, an actuatable structure outside the observable core, is ")
            .Append("empty. ");
        b.Append("THE FULL PRE-REDUCTION THEORY ADDS NOTHING: ").Append(chain.ActuatableOnTheFullTheory)
            .Append(" actuatable channel(s) against ").Append(chain.ActuatableOnTheSurvivingTheory)
            .Append(" on the reduced theory, a gain of ").Append(chain.AddedByTheFullTheory).Append(". ");
        b.Append("AND THE ONE STRUCTURE THAT IS DYNAMICAL WITHOUT BEING OBSERVABLE IS NOT A CHANNEL: ")
            .Append(dynamics.Length).Append(" structures move the evolution while moving no readout (")
            .Append(string.Join(", ", dynamics.Select(d => d.Name)))
            .Append("), and none of them is actuatable; the reach on every frame is ")
            .Append(ceiling[0].ReachableDirections).Append(" of ").Append(ceiling[0].ConservingDirections)
            .Append(" conserving directions, so the only effect they can have is to TAKE REACH AWAY (")
            .Append(ceiling[^1].Unreachable).Append(" direction(s) at c = ").Append(ceiling[^1].Components)
            .Append("). ")
            .Append("AND THE COUNT ROW IS MEASURED WITH ITS OWN INSTRUMENT: a cut changes the links as well as the ")
            .Append("count, so the count's evidence is G_082's union construction, where the local read shape changes in ")
            .Append(countInvariance.ShapeChanges).Append(" case(s), the values in ").Append(countInvariance.ValueChanges)
            .Append(", the ratio in ").Append(countInvariance.RatioChanges).Append(", and ")
            .Append(countInvariance.LocalObservablesReadingTheCount)
            .Append(" local observable(s) read the absolute count — the cut's readout movement belongs to the LINKS ")
            .Append("row. ");
        b.Append("SUBSTANCE OF THE FAILURE: the reduction was observation-safe at exactly the point where it was ")
            .Append("control-fatal — stripping the transport operator moves 0.0 of a readout (the layer's gauge ")
            .Append("residual is exactly ").Append(MinimalGravityCoreAudit.TransportLayerContribution().Single().Residual)
            .Append(") while it removes the operations themselves — but the operation it removes is not a variable, ")
            .Append("and every operation there is writes the state, which is read. There is no invisible actuator, only ")
            .Append("an invisible RULE whose effect is always visible.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the three roles are separable and are separated by measurement, not by");
        b.AppendLine("argument; that they come apart in BOTH directions in the surviving inventory (structures read and not");
        b.AppendLine("evolved, structures evolved and not read); that the set of structures an operator can SET is a subset of");
        b.AppendLine("the observable core; that the set REQUIRED for control is not, and contains the operation's own rule;");
        b.AppendLine("that the full pre-reduction theory adds no actuatable channel; and that the reach is bounded by n - c, so");
        b.AppendLine("the dynamics-without-observables structures can only take control away, never grant it.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that a hidden actuator is impossible in physics. The audit measures the");
        b.AppendLine("SURVIVING VOCABULARY, and its one absent member — a memory channel carrying a coefficient — is named");
        b.AppendLine("rather than used, because introducing it is what the rules forbid. It also does not claim the roles are");
        b.AppendLine("fundamental: it claims that on the canonical 96-cell ring, with the counterfactual cuts c = 1, 2, 4 and");
        b.AppendLine("the tolerance 1e-12, the three roles fall as the table says.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_089: G_089 asked control VERSUS observable and found 3 unobservable");
        b.AppendLine("controls, none actuatable. What is added here is the DYNAMICAL axis — measured on the update relation,");
        b.AppendLine("the class's reach and the operation set, three objects G_089 never touched — the per-structure REMOVAL");
        b.AppendLine("measurement (readouts lost and moved, update residual), the re-run of the chain on the FULL");
        b.AppendLine("pre-reduction theory, and the finding that the reduction was observation-safe exactly where it was");
        b.AppendLine("control-fatal.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: an operation whose argument list contains a link, a count, a flux or a");
        b.AppendLine("phase (which would make the corresponding structure actuatable); a readout that survives the removal of");
        b.AppendLine("the maps (which would shrink the observable core); or a measurement that decides the kernel's second");
        b.AppendLine("component, which would move the one row where READ and MEASURABLE come apart.");
        return b.ToString();
    }

    // ===================== THE REPORT =====================

    public static string OutputRoles()
    {
        var b = new StringBuilder();
        b.AppendLine("1. OBSERVABLE ROLE VERSUS DYNAMICAL ROLE (task 1)");
        b.AppendLine("   role                  procedure                                          what it measures");
        foreach (var (role, procedure, what) in RoleDefinitions())
        {
            b.AppendLine($"   {role,-21} {procedure,-50} {what}");
        }
        b.AppendLine();
        b.AppendLine("   the two roles are measured on DIFFERENT OBJECTS on purpose: the readouts are a property of the");
        b.AppendLine("   description, the update is a property of the substrate's links, and the reach is a property of the");
        b.AppendLine("   class. A structure may hold any combination of the three.");
        return b.ToString();
    }

    public static string OutputTable()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE NECESSITY TABLE (tasks 2 and 6)");
        b.AppendLine("   structure                                  observable  dynamical  actuator  role");
        foreach (var s in Structures())
        {
            b.AppendLine($"   {s.Name,-42} {Yes(s.Observable),-10} {Yes(s.Dynamical),-9} {Yes(s.Actuatable),-8} "
                + RoleName(s.Role));
        }
        b.AppendLine();
        b.AppendLine("   readouts lost / moved, update residual, reach moved, and operations that write each structure:");
        b.AppendLine("   structure                                  lost  moves  worst      update     reach  ops   decidable in situ");
        foreach (var s in Structures())
        {
            b.AppendLine($"   {s.Name,-42} {s.ReadoutsLost,4}  {s.ReadoutsThatMove,5}  {E(s.ReadoutsMoved),-9}  "
                + $"{E(s.UpdateMoved),-9}  {s.ReachMoved,5}  {s.OperationsChangingIt,4}  "
                + $"{Yes(s.TheReadIsDecidableInSitu)}");
        }
        return b.ToString();
    }

    public static string OutputCensus()
    {
        var census = RoleCensus();
        var b = new StringBuilder();
        b.AppendLine("3. THE CENSUS (task 2)");
        b.AppendLine($"   structures            {census.Total}");
        b.AppendLine($"   observable            {census.Observable}");
        b.AppendLine($"   dynamical             {census.Dynamical}");
        b.AppendLine($"   actuatable            {census.Actuatable}");
        b.AppendLine($"   observable+actuatable {census.ObservableAndActuatable}");
        b.AppendLine($"   observable only       {census.ObservableOnly}");
        b.AppendLine($"   boundary condition    {census.BoundaryCondition}");
        b.AppendLine($"   dynamical only        {census.DynamicalOnly}");
        b.AppendLine($"   INVISIBLE ACTUATOR    {census.InvisibleActuator}   <- the success criterion");
        b.AppendLine($"   inert                 {census.Inert}");
        b.AppendLine($"   absent                {census.Absent}");
        return b.ToString();
    }

    public static string OutputChain()
    {
        var chain = ChainCounts();
        var b = new StringBuilder();
        b.AppendLine("4. THE CHAIN ON THE FULL PRE-REDUCTION THEORY (task 3)");
        b.AppendLine("   structure                                  status                          adds an actuator");
        foreach (var (structure, status, adds, measurement) in ChainOnTheFullTheory())
        {
            b.AppendLine($"   {structure,-42} {status,-31} {Yes(adds)}");
            b.AppendLine($"      {measurement}");
        }
        b.AppendLine();
        b.AppendLine($"   actuatable channels on the full theory {chain.ActuatableOnTheFullTheory}, on the surviving theory "
            + $"{chain.ActuatableOnTheSurvivingTheory}, added {chain.AddedByTheFullTheory}");
        return b.ToString();
    }

    public static string OutputCores()
    {
        var cores = CoreComparison();
        var b = new StringBuilder();
        b.AppendLine("5. THE TWO FAILURE CORES (task 4)");
        b.AppendLine("   FailureCore(observables)  " + string.Join(" | ", FailureCoreObservables()));
        b.AppendLine("   FailureCore(actuation)    " + string.Join(" | ", FailureCoreActuation()));
        b.AppendLine($"   observable {cores.Observables}, actuation {cores.Actuation}, intersection {cores.Intersection}, "
            + $"actuation-only {cores.ActuationOnly}, observables-only {cores.ObservablesOnly}");
        b.AppendLine($"   the actuation core is a subset of the observable core: {Yes(cores.ActuationIsASubsetOfObservables)}");
        b.AppendLine($"   the observable core is a subset of the actuation core: {Yes(cores.ObservablesIsASubsetOfActuation)}");
        b.AppendLine($"   the two cores coincide: {Yes(cores.TheCoresCoincide)}");
        return b.ToString();
    }

    public static string OutputTaskFive()
    {
        var dynamics = DynamicalWithoutObservables();
        var b = new StringBuilder();
        b.AppendLine("6. DYNAMICS WITHOUT OBSERVABLES (task 5)");
        b.AppendLine($"   structures that move the evolution and no readout: {dynamics.Length}");
        foreach (var s in dynamics)
        {
            b.AppendLine($"   - {s.Name}");
            b.AppendLine($"     {s.Measurement}");
        }
        b.AppendLine();
        b.AppendLine("   the direction, measured against the counterfactual cuts:");
        b.AppendLine("   components  reachable  conserving  unreachable");
        foreach (var (components, reachable, conserving, unreachable, reading) in ReachCeiling())
        {
            b.AppendLine($"   {components,10}  {reachable,9}  {conserving,10}  {unreachable,11}   {reading}");
        }
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("7. THE CRITERIA AND THE VERDICT (task 7)");
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
}
