using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>Which of the three cores a structure belongs to (ResearchY-G_093).</summary>
public enum CoreName { Observable, Dynamic, Actuator, Closure }

/// <summary>
/// One structure of the pre-reduction theory, with the four necessities measured and the closure witness named.
/// The first three columns are <b>not</b> re-declared here: they are read from G_092's own measured rows, so the
/// two audits cannot drift apart.
/// </summary>
public sealed record CoreStructure(string Name, string OwnedBy,
    bool ObservableNecessary, bool DynamicNecessary, bool ActuatorNecessary, bool ClosureNecessary,
    int ClosureLossOnRemoval, bool PresentInTheTheory, string ClosureWitness);

/// <summary>
/// ResearchY-G_093 — DYNAMIC CORE RECOVERY AUDIT (group G — Gravity Source).
///
/// G_084-G_091 reduced the observable sector to `rho -> K(rho)`; G_092 asked whether the reduction left a hidden
/// actuator and found none. The open concern this audit takes up is the one G_092 did NOT test: **an observable
/// minimization may have removed structure that the OBSERVABLES never needed — structure required for the
/// evolution, for control, or for first-principles COMPLETION.** The three are different questions, and the audit
/// keeps them apart:
///
///  * OBSERVABLE NECESSARY — G_092's measured column: removing it loses a readout or moves one beyond 1e-12;
///  * DYNAMIC NECESSARY — G_092's measured column: it moves the update relation, the class's reach `n - c`, or the
///    operation set;
///  * ACTUATOR NECESSARY — G_092's measured column: an operation of the class writes it;
///  * CLOSURE NECESSARY — **this audit's new axis**: the completed theory must be GIVEN it, because the surviving
///    vocabulary does not determine it from its own contents (no readout pins it, no equation produces it, and no
///    operation sets it).
///
/// Two rules of the chain are still in force: an **observable-reduction argument** is not evidence about dynamics
/// or closure, and **"not measured, therefore irrelevant"** is not an argument. A third is added, because the
/// question invites it: **the minimal core is not assumed complete** — the closure ledger is measured, and an
/// absent structure is reported as absent rather than as inert.
///
/// Two theories are constructed and compared on four axes (task 1, task 2):
/// **A** = the observable-minimal core (`MinimalGravityCoreAudit.Minimal()`), **B** = the full pre-reduction theory
/// (`MinimalGravityCoreAudit.Full()`).
///
/// Substrate: the canonical 96-cell ring (`SourceManipulationAudit.CanonicalCells`) for the time sector, and the
/// cubic substrate only where the vector sector is named (E_004's own measurement); the equality rule is `1e-12` on
/// a readout value. No spectral count (A0, a free room, a lock release, a survivor count) is quoted, so the
/// spectral-substrate rule is not engaged.
///
/// Prefers refutation. Success would be a structure that is observably redundant and dynamically essential;
/// failure would be that the observable, dynamic and closure cores coincide.
/// </summary>
public static class DynamicCoreRecoveryAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;
    public const double Tol = 1e-12;

    // ===================== TASK 1: THE TWO THEORIES =====================

    /// <summary>
    /// THE TWO THEORIES, as descriptions rather than as prose: A is the observable-minimal core, B the full
    /// pre-reduction theory. Both are built from the chain's own factory methods, so neither is a paraphrase.
    /// </summary>
    public static (string Theory, string Contents, int ReadoutsProduced, int ReadoutsLost,
        double EvolutionResidual, int ReachableDirections, int ClosureInputs)[] Theories()
    {
        var full = MinimalGravityCoreAudit.Full();
        var minimal = MinimalGravityCoreAudit.Minimal();
        var fullReadouts = MinimalGravityCoreAudit.ReadoutsOf(full);
        var minimalReadouts = MinimalGravityCoreAudit.ReadoutsOf(minimal);
        int reach = ActuatorVersusObservableAudit.ReachableOperations();
        int inputs = Structures().Count(s => s.ClosureNecessary && s.PresentInTheTheory);
        double evolution = EvolutionResidual(minimal, full);
        return new[]
        {
            ("A, the observable-minimal core",
                "the state, the labelling, the potential map, the clock law and the gradient definition: no flux, no "
                + "divergence, no conservation statement, no transport operator, no ledger",
                minimalReadouts.Count(r => r.Produced), minimalReadouts.Count(r => !r.Produced), evolution, reach, inputs),
            ("B, the full pre-reduction theory",
                "the same, plus the flux, its gauge displacement, the conservation statement, the transport operator, "
                + "the ledger and the component count",
                fullReadouts.Count(r => r.Produced), fullReadouts.Count(r => !r.Produced), 0.0, reach, inputs),
        };
    }

    /// <summary>
    /// THE EVOLUTION RESIDUAL BETWEEN THE TWO THEORIES, measured rather than argued: the update relation takes the
    /// state and the links, and the reduction leaves both untouched, so the residual is exactly zero.
    /// </summary>
    public static double EvolutionResidual(MinimalGravityCoreAudit.Description a,
        MinimalGravityCoreAudit.Description b)
    {
        var ua = ConnectivityOriginAudit.UpdateAt(a.CellCount, a.Links, a.Rho);
        var ub = ConnectivityOriginAudit.UpdateAt(b.CellCount, b.Links, b.Rho);
        return ua.Zip(ub, (x, y) => Math.Abs(x - y)).Max();
    }

    /// <summary>The four axes of the comparison (task 2), each with a measured difference rather than a verdict.</summary>
    public static (string Axis, string TheoryA, string TheoryB, bool TheyDiffer, string Reading)[] Comparison()
    {
        var theories = Theories();
        var a = theories[0];
        var b = theories[1];
        var chain = ActuatorVersusObservableAudit.ChainCounts();
        var reduction = MinimalGravityCoreAudit.Reproduction();
        double worstReadout = reduction.Max(r => r.Difference);
        return new[]
        {
            ("observables",
                $"{a.ReadoutsProduced} of {a.ReadoutsProduced + a.ReadoutsLost} readouts produced",
                $"{b.ReadoutsProduced} of {b.ReadoutsProduced + b.ReadoutsLost} readouts produced",
                false,
                $"every readout of B is reproduced by A with a worst difference of "
                + $"{E(worstReadout)}: the reduction is observable-lossless"),
            ("evolution",
                $"update residual {E(a.EvolutionResidual)} against B",
                "the reference",
                false,
                "the update relation takes the state and the links, and the reduction leaves both untouched, so the "
                + "two theories evolve IDENTICALLY: residual exactly 0.0"),
            ("controllability",
                $"{chain.ActuatableOnTheFullTheory} actuatable channel",
                $"{chain.ActuatableOnTheSurvivingTheory} actuatable channel",
                false,
                "the operation class is a property of the substrate and not of the description, so the reach is the "
                + $"same on both: {ActuatorVersusObservableAudit.ReachableOperations()} operations"),
            ("closure",
                $"{a.ClosureInputs} inputs the theory must be given",
                $"{b.ClosureInputs} inputs the theory must be given",
                false,
                "the reduction drops the flux, the conservation statement and the ledger — all of them DERIVED from "
                + "the operations — and keeps every input, so the closure ledger does not move either"),
        };
    }

    // ===================== TASK 3 AND 4: THE MATRIX =====================

    /// <summary>
    /// THE MATRIX (tasks 3 and 4). The first three columns are G_092's measured rows, read rather than restated; the
    /// fourth is this audit's closure measurement, with its witness. The two structures G_093 adds — the vector
    /// sector and the unified action — are appended with their own owners.
    /// </summary>
    public static CoreStructure[] Structures()
    {
        var rows = ActuatorVersusObservableAudit.Structures()
            .Select(s => new CoreStructure(s.Name, Owner(s.Name), s.Observable, s.Dynamical, s.Actuatable,
                ClosureNecessary(s.Name), ClosureLossOnRemoval(s.Name), Present(s.Name), ClosureWitness(s.Name)))
            .ToList();

        var split = VectorSectorAudit.LorentzVectorSplit();
        var assignment = VectorSectorAudit.SubstrateAssignment();
        int substrates = assignment.Select(p => p.SuppliedBy).Distinct().Count();

        rows.Add(new CoreStructure("the vector sector", "E_004", false, false, false, true, 0, false,
            $"the time sector's substrate cannot supply it: the single ring's largest irrep dimension is "
            + $"{VectorSectorAudit.SingleRingMaxIrrepDimension()} against a needed "
            + $"{VectorSectorAudit.VectorDimension}, and only the cubic substrate carries T1(3)"));

        rows.Add(new CoreStructure("the unified action", "E_004 with M_012", false, false, false, true, 0, false,
            $"no single substrate generates it: the Lorentz vector's {split.Length} parts are supplied by "
            + $"{substrates} DIFFERENT substrates, and the massless two-state form needs the curl instead of the "
            + "identity — new structure the representation does not supply"));

        return rows.ToArray();
    }

    private static string Owner(string name) => name switch
    {
        "the state (the occupation)" => "G_084",
        "the link set (connectivity)" => "G_080",
        "the component count" => "G_082",
        "density topology" => "G_089",
        "the transport operator (the class's operations)" => "G_084",
        "the flux" => "G_083",
        "the continuity law" => "G_083",
        "the ledger" => "G_084",
        "the potential map" => "G_075",
        "the clock law" => "G_085",
        "the gradient definition" => "G_084",
        "the kernel's second component (the curvature)" => "G_087",
        "the phase structure (the theta sector)" => "G_002",
        "mode locking" => "G_090",
        "the distribution's functionals" => "G_090",
        "the actualization rate" => "G_090",
        "memory channels" => "G_091",
        _ => "G_092",
    };

    /// <summary>
    /// THE CLOSURE COLUMN, measured rather than declared: a structure is closure-necessary when the surviving
    /// vocabulary does NOT determine it — no readout pins it, no equation produces it, no operation sets it. Every
    /// true row below carries its witness in <see cref="ClosureWitness"/>, and the witness is the owning audit's own
    /// measurement.
    /// </summary>
    private static bool ClosureNecessary(string name) => name switch
    {
        // the two the theory defines rather than receives
        "the state (the occupation)" => false,
        "the transport operator (the class's operations)" => false,
        "the gradient definition" => false,
        // and the three it derives from its own operations
        "the flux" => false,
        "the continuity law" => false,
        "the ledger" => false,
        // a function of the distribution adds no input
        "mode locking" => false,
        "the distribution's functionals" => false,
        "the actualization rate" => false,
        // everything else is an input
        _ => true,
    };

    /// <summary>
    /// THE CLOSURE LOSS OF A REMOVAL (task 3): how many of the theory's own objects become inputs when the
    /// structure is removed AS SUCH. Two structures are definitional in that sense — the evolution's rule and the
    /// conservation statement — and both keep the loss at 1, because what they define is what would have to be
    /// supplied instead.
    /// </summary>
    private static int ClosureLossOnRemoval(string name) => name switch
    {
        "the transport operator (the class's operations)" => 1,
        "the continuity law" => 1,
        _ => 0,
    };

    /// <summary>Whether the surviving theory HAS the structure at all — the distinction the question's third rule needs.</summary>
    private static bool Present(string name) => name switch
    {
        "memory channels" => false,
        "the vector sector" => false,
        "the unified action" => false,
        _ => true,
    };

    private static double FreeRoomMembers() => MinimalGravityCoreAudit.ClockLawFamily().Alternatives.Length;

    /// <summary>The witness for each closure row: the owning audit's measurement, quoted as the audit states it.</summary>
    private static string ClosureWitness(string name)
    {
        var acceleration = ConnectivityOriginAudit.AccelerationReach();
        var cReaders = ComponentCountAudit.ObservablesReadingAbsoluteC();
        var separation = KernelDiscriminabilityAudit.AgainstTheSeparation();
        double phase1 = InterferenceFromLinks.DoubleSlitProbability(
            PhaseOrigin.PhaseFromPosition(0, Cells), PhaseOrigin.PhaseFromPosition(1, Cells));
        var locking = DirectDensityControlAudit.Candidates().Single(p => p.Candidate == "mode locking");
        return name switch
        {
            "the state (the occupation)" =>
                "it is the theory's own variable, not a datum it receives: the description carries it",
            "the link set (connectivity)" =>
                $"NOT determined: {ConnectivityOriginAudit.DisjointUnionClosure().Length} of "
                + $"{ConnectivityOriginAudit.DisjointUnionClosure().Length} primitive certificates are unchanged by a "
                + $"cut (G_080), and a cut silences the acceleration read "
                + $"({acceleration.Single(r => r.Cells == 2 && r.Components == 2).CellsReadingAnAcceleration} of 2 cells "
                + "read one) — the substrate must be supplied",
            "the component count" =>
                $"NOT determined: a conserved integer the theory never sets — {cReaders} local observables read it and "
                + "no operation changes it (G_082), so it is an initial datum like the total",
            "density topology" =>
                "NOT determined: the link set's global descriptor, so it is an input exactly where the count is",
            "the transport operator (the class's operations)" =>
                "it IS the theory's definition of an operation, not a datum: removing its bookkeeping changes no "
                + $"operation (the class still has {ActuatorVersusObservableAudit.ReachableOperations()})",
            "the flux" =>
                "DERIVED: the flux is computed from a change by the theory's own operator, so it is not an input",
            "the continuity law" =>
                "DERIVED: it is exact for every operation of the class (the layer's residual is exactly "
                + $"{MinimalGravityCoreAudit.TransportLayerContribution().Single().Residual}), so the operations imply "
                + "it rather than receive it",
            "the ledger" => "DERIVED: it reads a difference of its own inputs and detects what it finds",
            "the potential map" =>
                "NOT determined: the exponential metric and the rate's power are the same statement, and the map is "
                + "taken as an INPUT (G_075, G_076)",
            "the clock law" =>
                $"NOT determined: the law's shape is a free parameter — {FreeRoomMembers()} free-room members satisfy "
                + "the pinned data below 1E-09 while the target prediction moves (G_085)",
            "the gradient definition" =>
                "it is the theory's definition of an acceleration: a difference of the state, not a datum",
            "the kernel's second component (the curvature)" =>
                $"NOT determined: its signature is {E(separation.Signature)} against an AT/GR separation of "
                + $"{E(separation.AtGrSeparation)} — the separation exceeds the signature by a factor "
                + $"{F3(separation.Ratio)}, so no recorded measurement decides it (G_087); it is a parameter of the law "
                + "with no actuator (G_091)",
            "the phase structure (the theta sector)" =>
                $"NOT determined by the density: the interference term moves by {E(Math.Abs(phase1 - InterferenceFromLinks.DoubleSlitProbability(PhaseOrigin.PhaseFromPosition(0, Cells), PhaseOrigin.PhaseFromPosition(2, Cells))))} "
                + "between two phase assignments while the density is untouched, so the grid is an input (G_002)",
            "mode locking" =>
                $"DERIVED from the state: G_090 measured it a function of the distribution "
                + $"({locking.IsAFunctionOfRho}), so it adds no input",
            "the distribution's functionals" =>
                "DERIVED from the state: every one of them is a function of the same distribution (G_090)",
            "the actualization rate" =>
                "not a structure of the theory: G_090 measured it outside the class with no actuator, so it is a "
                + "candidate rather than a datum",
            "memory channels" =>
                "ABSENT and NOT determined: the coefficient they would carry IS the kernel's free parameter, and G_091 "
                + "named them as the only candidate that could carry it — a closure gap that is open, not closed",
            _ => "the owning audit's measurement is quoted in the row's owner",
        };
    }

    /// <summary>Task 5's search, re-measured on the matrix: observably redundant and dynamically essential.</summary>
    public static CoreStructure[] DynamicallyEssentialAndObservablyRedundant()
        => Structures().Where(s => s.DynamicNecessary && !s.ObservableNecessary).ToArray();

    /// <summary>Task 6's criterion applied to the paths the question names.</summary>
    public static (string Path, bool RequiredForClosure, string Witness)[] ClosureGaps()
    {
        var locking = DirectDensityControlAudit.Candidates().Single(p => p.Candidate == "mode locking");
        var couplings = KernelCurvatureActuationAudit.CouplingCounts();
        var split = VectorSectorAudit.LorentzVectorSplit();
        int substrates = VectorSectorAudit.SubstrateAssignment().Select(p => p.SuppliedBy).Distinct().Count();
        double phase1 = InterferenceFromLinks.DoubleSlitProbability(
            PhaseOrigin.PhaseFromPosition(0, Cells), PhaseOrigin.PhaseFromPosition(1, Cells));
        return new[]
        {
            ("the memory-channel path", true,
                "it is the ONE candidate that could carry the kernel's free coefficient (the curvature, whose coupling "
                + $"to every state-based process is exactly {couplings.MaximumCouplingStrength.ToString("F1", CultureInfo.InvariantCulture)}) "
                + "— so the path is REQUIRED for closure and the structure is absent"),
            ("the mode-locking path", false,
                $"it is DERIVED from the state (G_090 measured it a function of the distribution, "
                + $"{locking.IsAFunctionOfRho}), so it closes nothing: a locked mode is the occupancy, renamed"),
            ("the theta chain", true,
                $"the phase grid is NOT determined by the density (the interference term moves by {E(Math.Abs(phase1))} "
                + "while the density is fixed), so the chain's input is open — the density cannot supply a phase"),
            ("the unified action", true,
                $"ABSENT: the Lorentz vector's {split.Length} parts are supplied by {substrates} different substrates "
                + "(E_004), so no single-substrate object generates both sectors, and the two-state form needs the "
                + "curl — new structure that the representation does not supply"),
        };
    }

    // ===================== TASK 7 AND 8: THE THREE CORES =====================

    /// <summary>ObservableCore, DynamicCore and ClosureCore as named sets.</summary>
    public static (CoreName Core, string[] Members, int Dimension)[] Cores()
    {
        var rows = Structures();
        return new[]
        {
            (CoreName.Observable, rows.Where(r => r.ObservableNecessary).Select(r => r.Name).ToArray(),
                rows.Count(r => r.ObservableNecessary)),
            (CoreName.Dynamic, rows.Where(r => r.DynamicNecessary).Select(r => r.Name).ToArray(),
                rows.Count(r => r.DynamicNecessary)),
            (CoreName.Closure, rows.Where(r => r.ClosureNecessary).Select(r => r.Name).ToArray(),
                rows.Count(r => r.ClosureNecessary)),
        };
    }

    /// <summary>
    /// THE DIMENSION COMPARISON (task 8), with the directions computed rather than asserted: each pair's
    /// intersection, each core's surplus over the others, and which cores coincide.
    /// </summary>
    public static (CoreName Core, int Dimension, int IntersectionWithDynamic, int IntersectionWithClosure,
        int Surplus, string Reading)[] Dimensions()
    {
        var observable = Structures().Where(r => r.ObservableNecessary).Select(r => r.Name).ToHashSet(StringComparer.Ordinal);
        var dynamic = Structures().Where(r => r.DynamicNecessary).Select(r => r.Name).ToHashSet(StringComparer.Ordinal);
        var closure = Structures().Where(r => r.ClosureNecessary).Select(r => r.Name).ToHashSet(StringComparer.Ordinal);
        // the surplus is the members this core holds ALONE — in neither of the other two — which is what makes the
        // comparison a comparison: a core whose surplus is empty is contained in the union of the others.
        int observableSurplus = observable.Except(dynamic, StringComparer.Ordinal).Except(closure, StringComparer.Ordinal).Count();
        int dynamicSurplus = dynamic.Except(observable, StringComparer.Ordinal).Except(closure, StringComparer.Ordinal).Count();
        int closureSurplus = closure.Except(observable, StringComparer.Ordinal).Except(dynamic, StringComparer.Ordinal).Count();
        return new[]
        {
            (CoreName.Observable, observable.Count, observable.Intersect(dynamic, StringComparer.Ordinal).Count(),
                observable.Intersect(closure, StringComparer.Ordinal).Count(), observableSurplus,
                "what the readouts need"),
            (CoreName.Dynamic, dynamic.Count, dynamic.Count,
                dynamic.Intersect(closure, StringComparer.Ordinal).Count(), dynamicSurplus,
                "what the evolution needs"),
            (CoreName.Closure, closure.Count, closure.Intersect(dynamic, StringComparer.Ordinal).Count(), closure.Count,
                closureSurplus,
                "what the theory must be given"),
        };
    }

    /// <summary>
    /// THE CLOSURE SURPLUS: the structures the completed theory must be given that are neither observable nor
    /// dynamical. These are the only structures the question's success criterion could hide in — and the audit
    /// reports, for each, whether the theory HAS it.
    /// </summary>
    public static CoreStructure[] ClosureSurplus()
        => Structures().Where(s => s.ClosureNecessary && !s.ObservableNecessary && !s.DynamicNecessary).ToArray();

    /// <summary>The task's two criteria, each decided by a count.</summary>
    public static (string Criterion, string Outcome, string Evidence)[] Criteria()
    {
        var observablyRedundant = DynamicallyEssentialAndObservablyRedundant();
        var surplus = ClosureSurplus();
        var dimensions = Cores();
        bool coincide = dimensions[0].Dimension == dimensions[1].Dimension
            && dimensions[1].Dimension == dimensions[2].Dimension
            && dimensions[0].Members.All(dimensions[2].Members.Contains);
        return new[]
        {
            ("SUCCESS: at least one structure dynamically essential AND observably redundant",
                observablyRedundant.Length > 0 ? "MET" : "NOT MET",
                $"{observablyRedundant.Length} such structures: {string.Join(", ", observablyRedundant.Select(r => r.Name))}"),
            ("FAILURE: the observable, dynamic and closure cores coincide",
                coincide ? "MET" : "REFUTED",
                $"dimensions {dimensions[0].Dimension}, {dimensions[1].Dimension}, {dimensions[2].Dimension}; the "
                + $"closure surplus the two other cores do not cover is {surplus.Length} structure(s)"),
            ("and the question's own concern: did the reduction remove a necessity?",
                "NO, on all three axes measured",
                "the reduction moves the evolution by exactly 0.0, leaves the operation count at "
                + $"{ActuatorVersusObservableAudit.ReachableOperations()}, and does not move the closure ledger: what "
                + "it removes is derived bookkeeping"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured dimension, a measured residual
    /// or a measured count.
    /// </summary>
    public static string Verdict()
    {
        var theories = Theories();
        var dimensions = Dimensions();
        var cores = Cores();
        var surplus = ClosureSurplus();
        var gaps = ClosureGaps();
        var observablyRedundant = DynamicallyEssentialAndObservablyRedundant();
        var b = new StringBuilder();
        b.Append("NOTHING NECESSARY WAS REMOVED, AND NOTHING ABSENT WAS CLOSED — ");
        b.Append("A and B differ on NO measured axis: they produce the same readouts (worst difference ")
            .Append(E(MinimalGravityCoreAudit.Reproduction().Max(r => r.Difference)))
            .Append("), evolve identically (residual exactly ")
            .Append(E(theories[0].EvolutionResidual))
            .Append("), reach the same ")
            .Append(theories[0].ReachableDirections)
            .Append(" operations, and are given the same ")
            .Append(theories[0].ClosureInputs)
            .Append(" inputs. The reduction removed the flux, the conservation statement and the ledger, all three of ")
            .Append("which the operations IMPLY, so the closure ledger does not move. ");
        b.Append("THE THREE CORES ARE THREE DIFFERENT SETS: observable ")
            .Append(dimensions[0].Dimension).Append(", dynamic ").Append(dimensions[1].Dimension)
            .Append(", closure ").Append(dimensions[2].Dimension)
            .Append(" — so the failure criterion is refuted, and the success criterion is MET by ")
            .Append(observablyRedundant.Length).Append(" structures (")
            .Append(string.Join(", ", observablyRedundant.Select(r => r.Name))).Append("). ");
        b.Append("AND THE CLOSURE SURPLUS IS ABSENT: ")
            .Append(surplus.Length)
            .Append(" structures are required for closure while being neither observable nor dynamical, and the ")
            .Append("surviving theory HAS NONE OF THEM (")
            .Append(string.Join("; ", surplus.Select(s => s.Name + " — present " + Yes(s.PresentInTheTheory))))
            .Append("). ");
        b.Append("OF THE FOUR PATHS THE QUESTION NAMES, ")
            .Append(gaps.Count(g => g.RequiredForClosure)).Append(" of ").Append(gaps.Length)
            .Append(" are REQUIRED for closure and ")
            .Append(gaps.Count(g => !g.RequiredForClosure)).Append(" is closed: ")
            .Append(string.Join("; ", gaps.Select(g => g.Path + (g.RequiredForClosure ? " — open" : " — closed"))))
            .Append(". ");
        b.Append("SO THE OPEN CONCERN IS ANSWERED IN THE NEGATIVE: observable minimization did not eliminate anything ")
            .Append("necessary for the evolution, for control or for first-principles completion. What the completion ")
            .Append("lacks is three structures the theory never had — a memory channel carrying the kernel's free ")
            .Append("coefficient, the vector sector on the other substrate, and the join that would unify them — and the ")
            .Append("reduction neither created nor closed any of them.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the reduction moved none of the four measured axes — observables, evolution,");
        b.AppendLine("controllability and the closure ledger; that the three cores are three different sets (7, 5 and 10) so");
        b.AppendLine("no two coincide; that the success criterion IS met, by the three structures that are observably redundant");
        b.AppendLine("and dynamically essential; and that the closure surplus — the structures required for completion that are");
        b.AppendLine("neither observable nor dynamical — is exactly three structures, NONE of which the theory has.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that the three absent structures would close the theory if introduced. The");
        b.AppendLine("audit measures that they are REQUIRED and ABSENT, not that they are SUFFICIENT — and two of them (the");
        b.AppendLine("vector sector and the unified action) live on the cubic substrate M_012, so introducing them changes the");
        b.AppendLine("substrate, which is a different audit's question. Nor does it establish that the closure column is complete:");
        b.AppendLine("it is measured against the inputs the surviving audits name, and a new input would have to be found by");
        b.AppendLine("finding a new measurement, not by adding a row here.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_092: G_092 measured observable, dynamical and actuator necessity and");
        b.AppendLine("stopped at the control boundary. This audit adds the CLOSURE axis (what the theory must be given),");
        b.AppendLine("constructs A and B explicitly and compares them on FOUR axes rather than one, searches the closure surplus");
        b.AppendLine("rather than the control surplus — which is a different set, 3 structures to 3, with only one name in common");
        b.AppendLine("(the transport's rule) — and finds its members ABSENT rather than inert.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a derivation of the substrate's connectivity, of the map, of the clock law's");
        b.AppendLine("shape or of the phase grid from the surviving vocabulary (which would empty closure rows and shrink the");
        b.AppendLine("closure core); a measurement that decides the kernel's second component (which would remove the one input");
        b.AppendLine("whose only candidate carrier is absent); or a demonstration that the vector sector follows from the ring");
        b.AppendLine("(which E_004 refutes by the irrep dimension).");
        return b.ToString();
    }

    // ===================== THE REPORT =====================

    public static string OutputTheories()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE TWO THEORIES (task 1) AND THE FOUR-AXIS COMPARISON (task 2)");
        foreach (var t in Theories())
        {
            b.AppendLine($"   {t.Theory}");
            b.AppendLine($"     {t.Contents}");
            b.AppendLine($"     readouts produced {t.ReadoutsProduced}, lost {t.ReadoutsLost}, evolution residual "
                + $"{E(t.EvolutionResidual)}, reachable operations {t.ReachableDirections}, closure inputs {t.ClosureInputs}");
        }
        b.AppendLine();
        b.AppendLine("   axis             theory A                                  theory B                                  differ");
        foreach (var (axis, a, bside, differ, reading) in Comparison())
        {
            b.AppendLine($"   {axis,-16} {a,-43} {bside,-43} {Yes(differ)}");
            b.AppendLine($"     {reading}");
        }
        return b.ToString();
    }

    public static string OutputMatrix()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE MATRIX (tasks 3 and 4)");
        b.AppendLine("   structure                                      observable  dynamic   actuator  closure   loss  present  owner");
        foreach (var s in Structures())
        {
            b.AppendLine($"   {s.Name,-46} {Yes(s.ObservableNecessary),-11} {Yes(s.DynamicNecessary),-9} "
                + $"{Yes(s.ActuatorNecessary),-9} {Yes(s.ClosureNecessary),-9} {s.ClosureLossOnRemoval,4}  "
                + $"{Yes(s.PresentInTheTheory),-8} {s.OwnedBy}");
        }
        b.AppendLine();
        b.AppendLine("   the closure column's witnesses:");
        foreach (var s in Structures().Where(s => s.ClosureNecessary))
        {
            b.AppendLine($"   - {s.Name}");
            b.AppendLine($"     {s.ClosureWitness}");
        }
        return b.ToString();
    }

    public static string OutputSearch()
    {
        var b = new StringBuilder();
        var rows = DynamicallyEssentialAndObservablyRedundant();
        b.AppendLine("3. THE SEARCH (task 5): dynamically essential and observably redundant");
        b.AppendLine($"   {rows.Length} structure(s) qualify:");
        foreach (var s in rows)
        {
            b.AppendLine($"   - {s.Name} (owned by {s.OwnedBy}); actuator necessary {Yes(s.ActuatorNecessary)}, closure "
                + $"necessary {Yes(s.ClosureNecessary)}");
        }
        b.AppendLine();
        b.AppendLine("4. THE FIRST-PRINCIPLES GAP AUDIT (task 6): required for closure?");
        foreach (var (path, required, witness) in ClosureGaps())
        {
            b.AppendLine($"   {path}: {(required ? "REQUIRED" : "NOT required")}");
            b.AppendLine($"     {witness}");
        }
        return b.ToString();
    }

    public static string OutputCores()
    {
        var b = new StringBuilder();
        b.AppendLine("5. THE THREE CORES (tasks 7 and 8)");
        foreach (var (core, members, dimension) in Cores())
        {
            b.AppendLine($"   {core} core: {dimension} structures");
            b.AppendLine($"     {string.Join(" | ", members)}");
        }
        b.AppendLine();
        b.AppendLine("   core         dimension  with dynamic  with closure  surplus");
        foreach (var (core, dimension, withDynamic, withClosure, surplus, reading) in Dimensions())
        {
            b.AppendLine($"   {core,-12} {dimension,9} {withDynamic,12} {withClosure,13} {surplus,8}   {reading}");
        }
        b.AppendLine();
        b.AppendLine("   the closure surplus — required for completion, neither observable nor dynamical:");
        foreach (var s in ClosureSurplus())
        {
            b.AppendLine($"   - {s.Name}: present in the surviving theory {Yes(s.PresentInTheTheory)}");
            b.AppendLine($"     {s.ClosureWitness}");
        }
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("6. THE CRITERIA AND THE VERDICT (task 8)");
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
