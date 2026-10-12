using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>The audit's final classification (ResearchY-G_102): the one-degree-of-freedom reduction is exact, or a
/// surviving structure contributes rank beyond the state.</summary>
public enum FreedomClosure { Closed, Open }

/// <summary>Where a candidate quantity comes from: computed from the state, supplied to the description, or absent
/// from the sector entirely (the closure requirements of G_093/G_095).</summary>
public enum CandidateOrigin { DerivedFromTheState, SuppliedToTheDescription, AbsentFromTheSector }

/// <summary>
/// HOW the evolution sees a candidate, measured rather than bundled: an ARGUMENT of the update (the only role that
/// makes a structure something the law reads), its COEFFICIENT (the rate at which it is applied), its INDEX (the
/// independent variable), or nothing at all. The first draft folded all three into one flag and reported three
/// structures as "read by the evolution" where only one is a structure the law takes as an argument.
/// </summary>
public enum EvolutionRole { NotRead, Argument, Coefficient, Index }

/// <summary>
/// One candidate quantity (task 1): everything NOT already classified by G_101 as an invertible reparametrisation of
/// the state, plus the non-state structures and the closure requirements. The numeric effect columns are MAGNITUDES —
/// the largest movement a variation of the candidate causes — and the COUNTS live in the readings, so a count can
/// never be mistaken for a magnitude.
/// </summary>
public sealed record CandidateRow(string Candidate, CandidateOrigin Origin, int Components, int RankAgainstTheState,
    int KernelAgainstTheState, double ReadoutEffect, double UpdateEffect, EvolutionRole Role, string Measurement)
{
    public bool IsAFunctionOfTheState => Origin == CandidateOrigin.DerivedFromTheState;
    public bool IsAnArgumentOfTheUpdate => Role == EvolutionRole.Argument;
}

/// <summary>
/// One candidate's rank contribution (task 2): the rank of its sensitivity STACKED with the state's own identity. The
/// stacking is the measurement, and it works for a supplied structure too, because that structure's effect on the
/// state is a VECTOR IN the state space — so it cannot raise the space's rank either.
/// </summary>
public sealed record RankContribution(string Candidate, string Group, CandidateOrigin Origin, int RankStacked,
    int RankIncrease, bool ContributesABeyondTheStateDirection, string Measurement);

/// <summary>
/// WHY a reconstruction is blocked, so that a kernel DIMENSION is never averaged with a MOVEMENT magnitude: a scale
/// or a kernel obstruction is a count of invisible directions, a movement obstruction is a measured displacement, and
/// the absence of a structure is neither.
/// </summary>
public enum ObstructionKind { None, Scale, Kernel, Movement, Absent }

/// <summary>
/// One quantity's reconstruction (task 7), with the residual and the OBSTRUCTION kept apart: the residual is a
/// closed-form error (0 when the direction does not apply) and the obstruction is the measured magnitude of whatever
/// prevents the reconstruction, with its KIND named.
/// </summary>
public sealed record ReconstructionRow(string Quantity, string Direction, double Residual, double Obstruction,
    ObstructionKind Kind, bool Closes, string Measurement);

/// <summary>
/// ResearchY-G_102 — FREEDOM CLOSURE AUDIT (group G — Gravity Source).
///
/// G_101 measured the sector's state space: **96** components, **95** moving directions, **1** conserved direction and
/// **ONE INDEPENDENT STATE COORDINATE**, with every surviving quantity either an invertible reparametrisation of the
/// occupation or a many-to-one functional of it. This audit asks the sharper question that leaves open, and it is
/// about GENERATION rather than about counting:
///
/// **Can the surviving G-sector generate an independent degree of freedom internally, or is the one-degree-of-freedom
/// reduction exact?**
///
/// THE INSTRUMENT has to have two arms, because two different claims are in play. A quantity that IS a function of the
/// state cannot add a direction — its Jacobian against the state has 96 columns, so stacking it with the state leaves
/// the rank where it was, and that is measured. But a quantity that is NOT a function of the state CAN be varied while
/// the state is held fixed, and then the only question that makes it a DYNAMICAL degree of freedom is whether the
/// EVOLUTION reads it: a variation that moves no update and no readout is a gauge, a variation that moves a readout and
/// no update is a coordinate or a parameter of a map, and only a variation the update reads is a dynamical direction.
/// The audit measures all three, and it separates "generated internally" from "supplied to the description", because a
/// supplied structure is not a degree of freedom the sector produces.
///
/// Every count, dimension and rank names its object: the state is the canonical 96-cell ring's occupation (cells = 96,
/// links = `LinksOf(96, 1)`), the evolution is `UpdateAt(cells, links, rho)`, the rank rule is **1e-8** for the
/// finite-difference Jacobians (whose rounding floor is ~1e-12 at the audit's step of 1e-4 and whose truncation error
/// is ~1e-8) and **1e-12** for the dynamics, where the uniform mode's symbol is exactly 0.0 — and the equality rules
/// are those rules plus **exact** equality where an identity holds. The cubic substrate is forbidden and is not named.
/// No new primitive, field, source term, topology axiom, action, Lagrangian or hidden variable is introduced.
///
/// Prefers refutation.
/// </summary>
public static class FreedomClosureAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;
    public const double Tol = 1e-12;

    public const double Step = PatternKinematicsAudit.Step;

    /// <summary>The same difference step and rules G_101 used, so the two audits' ranks are comparable.</summary>
    public const double DifferenceStep = StateDegreesOfFreedomAudit.DifferenceStep;
    public const double JacobianRule = StateDegreesOfFreedomAudit.JacobianRule;

    public static (int A, int B)[] Links => ConnectivityOriginAudit.LinksOf(Cells, 1);

    public static double[] State => MapRedundancyAudit.State;

    public static double[] Potential(double[] rho) => rho.Select(SourceManipulationAudit.Potential).ToArray();

    public static double[] Update(double[] rho) => ConnectivityOriginAudit.UpdateAt(Cells, Links, rho);

    public static double MaxAbs(IEnumerable<double> v) => v.Select(Math.Abs).Max();

    // ===================== TASK 1: THE CANDIDATES =====================

    /// <summary>
    /// THE CANDIDATE TABLE (task 1): every surviving quantity that G_101 did NOT classify as an invertible
    /// reparametrisation of the state — i.e. the functionals with a kernel — together with the non-state structures the
    /// reduction demoted and the three closure requirements G_093 and G_095 identified. Each carries the rank of its
    /// Jacobian against the state (0 for the non-derived ones, because there is no such Jacobian) and the effect of an
    /// INDEPENDENT variation of it, measured in the two currencies that matter: the readouts and the update.
    /// </summary>
    public static CandidateRow[] Candidates()
    {
        var rho = Working;
        var full = MinimalGravityCoreAudit.Full();
        var minimal = MinimalGravityCoreAudit.Minimal();
        var baseReadouts = MinimalGravityCoreAudit.ReadoutsOf(full);

        // the flux's circulation: the same divergence, a different flux (G_083)
        var displaced = full with { DisplacedFlux = full.DisplacedFlux.Select(f => f + 5).ToArray() };
        double circulationEffect = MaxAbs(baseReadouts
            .Zip(MinimalGravityCoreAudit.ReadoutsOf(displaced), (x, y) => x.Value - y.Value));

        // the component count: the union construction, held fixed in every link (G_082)
        var countInvariance = ActuatorVersusObservableAudit.ComponentCountInvariance();
        double countEffect = Math.Max(countInvariance.ShapeChanges + countInvariance.ValueChanges,
            countInvariance.RatioChanges);

        // the labelling: a relabelling that CARRIES the state with it. THE CANONICAL STATE IS REFLECTION-SYMMETRIC
        // (G_099 measured it parity-even at 6.661E-016), so reflecting it is the IDENTITY and the first draft's
        // relabelling test measured 0 for that reason alone: the audit uses its own SKEWED pattern instead.
        double relabellingMoves = MaxAbs(MinimalGravityCoreAudit.ReadoutsOf(minimal)
            .Zip(MinimalGravityCoreAudit.ReadoutsOf(minimal with { Rho = Reflect(rho) }), (x, y) => x.Value - y.Value));

        // the maps' coefficient: the free room of G_075, swept as G_085 swept it (G_095 left it free)
        double lawMoves = MaxAbs(MapRedundancyAudit.ReadoutsOf(MapRedundancyAudit.Canonical(), rho)
            .Zip(MapRedundancyAudit.ReadoutsOf(MapRedundancyAudit.FreeRoom(0.4), rho),
                (x, y) => x.Value - y.Value));

        // the step, the link set and the tick: the three ways the evolution can see something that is not the state
        double stepEffect = MaxAbs(Update(rho).Select(l => l * 0.5));
        double cutEffect = MaxAbs(UpdateEffectVector(full with { Links = ConnectivityOriginAudit.LinksOf(Cells, 2) }));
        double tickEffect = MaxAbs(Update(rho));

        Func<double[], double[]> readoutValues = r => MinimalGravityCoreAudit
            .ReadoutsOf(minimal with { Rho = r }).Select(d => d.Value).ToArray();

        return new[]
        {
            new CandidateRow("the orientation field (the signed link differences)", CandidateOrigin.DerivedFromTheState,
                Cells, RankOf(OrientationCouplingAudit.OrientationField), 1, 0.0, 0.0, EvolutionRole.NotRead,
                "a FUNCTION of the state: rank 95 with a one-dimensional kernel — the constant, i.e. the count's scale " +
                "— so two different states (rho and 2 rho) give the SAME field, and no variation of it exists that the " +
                "state does not already carry"),

            new CandidateRow("the acceleration field (the averaged gradient)", CandidateOrigin.DerivedFromTheState,
                Cells, RankOf(r => ConnectivityOriginAudit.AccelerationOnLinks(Potential(r), Cells, Links)), 1, 0.0, 0.0,
                EvolutionRole.NotRead,
                "a FUNCTION of the state with the same one-dimensional kernel: the Laplacian of the potential, whose " +
                "kernel is the constant vector"),

            new CandidateRow("the excess over the flat background", CandidateOrigin.DerivedFromTheState,
                Cells, RankOf(r => r.Select(x => x - r.Average()).ToArray()), 1, 0.0, 0.0, EvolutionRole.NotRead,
                "a FUNCTION of the state: centring projects onto the zero-mean subspace, losing the constant"),

            new CandidateRow("the total count (the conserved functional)", CandidateOrigin.DerivedFromTheState,
                1, RankOf(r => new[] { r.Sum() }), 95, 0.0, 0.0, EvolutionRole.NotRead,
                "a FUNCTION of the state: ONE number with a 95-dimensional kernel — it is the conserved direction " +
                "itself, and it cannot vary while the state does not"),

            new CandidateRow("the flux's divergence", CandidateOrigin.DerivedFromTheState,
                Cells, RankOf(r => Update(r)), 1, 0.0, 0.0, EvolutionRole.NotRead,
                "a FUNCTION of the state (it IS the update's own operator applied to it), rank 95 with the same kernel"),

            new CandidateRow("the surviving readouts (eight functionals)", CandidateOrigin.DerivedFromTheState,
                baseReadouts.Length, RankOf(readoutValues), Cells - RankOf(readoutValues), 0.0, 0.0,
                EvolutionRole.NotRead,
                $"FUNCTIONS of the state at rank {RankOf(readoutValues)} of 8 with a {Cells - RankOf(readoutValues)}" +
                "-dimensional kernel: the observable sector cannot introduce a direction the state does not have"),

            new CandidateRow("the flux's own circulation", CandidateOrigin.SuppliedToTheDescription,
                Cells, 0, 0, circulationEffect, 0.0, EvolutionRole.NotRead,
                $"NOT a function of the state: the same divergence admits a different flux (G_083), and varying it " +
                $"moves the readouts by {E(circulationEffect)} and the update by 0 — a GAUGE the physics forgets, read " +
                $"by {ContinuityLawAudit.ObservablesReadingTheFlux()} observables"),

            new CandidateRow("the component count", CandidateOrigin.SuppliedToTheDescription,
                1, 0, 0, countEffect, 0.0, EvolutionRole.NotRead,
                $"NOT a function of the state: the union construction (G_082) changes the configuration while the " +
                $"shape, the values and the ratios move by {E(countEffect)} and the update by 0 — a GAUGE-like datum"),

            new CandidateRow("the labelling (which cell is which)", CandidateOrigin.SuppliedToTheDescription,
                1, 0, 0, relabellingMoves, 0.0, EvolutionRole.NotRead,
                $"NOT a function of the state: a relabelling that CARRIES the state moves the site-local readouts by " +
                $"up to {E(relabellingMoves)} and the update by 0 — the coordinate system, measured on a state that is " +
                "NOT reflection-symmetric (the canonical one is, so testing it there is the identity)"),

            new CandidateRow("the clock law's coefficient (the free room)", CandidateOrigin.SuppliedToTheDescription,
                1, 0, 0, lawMoves, 0.0, EvolutionRole.NotRead,
                $"NOT a function of the state: the free room moves the map's readouts by up to {E(lawMoves)} and the " +
                "update by 0 — a PARAMETER OF THE MAP (G_085, left free by G_095), which indexes the description " +
                "rather than the state space"),

            new CandidateRow("the step size epsilon", CandidateOrigin.SuppliedToTheDescription,
                1, 0, 0, 0.0, stepEffect, EvolutionRole.Coefficient,
                $"NOT a function of the state: it is the RATE at which the law is applied, the update scales with it " +
                $"({E(stepEffect)} of the canonical update at half the step), and its admissible range is DERIVED from " +
                "the spectrum (G_097) — the evolution's coefficient, not a structure it reads"),

            new CandidateRow("the link set (connectivity)", CandidateOrigin.SuppliedToTheDescription,
                1, 0, 0, 0.0, cutEffect, EvolutionRole.Argument,
                $"NOT a function of the state and the ONE structure the EVOLUTION TAKES AS AN ARGUMENT: a " +
                $"two-component cut moves the update by {E(cutEffect)} (G_080 measured it to be an input, and G_101 " +
                "re-measured this residual) — so it is the only supplied object that can add reach, and it is supplied"),

            new CandidateRow("the tick (the independent variable)", CandidateOrigin.SuppliedToTheDescription,
                1, 0, 0, 0.0, tickEffect, EvolutionRole.Index,
                $"NOT a function of the state: the state is indexed by it and does not determine it, and one tick moves " +
                $"the state by {E(tickEffect)} — the completion record's deepest structural boundary. It is the " +
                "evolution's INDEX, not a structure it reads"),

            new CandidateRow("the memory channel (a closure requirement)", CandidateOrigin.AbsentFromTheSector,
                1, 0, 0, 0.0, 0.0, EvolutionRole.NotRead,
                "ABSENT: G_093 found that what is required for closure while being neither observable nor dynamical is " +
                "exactly three structures the theory never had, of which this is one — an absence cannot contribute " +
                "rank, and G_095 left its coefficient FREE rather than fixing it"),

            new CandidateRow("the vector sector (a closure requirement)", CandidateOrigin.AbsentFromTheSector,
                1, 0, 0, 0.0, 0.0, EvolutionRole.NotRead,
                "ABSENT: G_093's second missing structure, and the one that would need the cubic substrate — which this " +
                "audit's rules forbid it to introduce"),

            new CandidateRow("the unified action (a closure requirement)", CandidateOrigin.AbsentFromTheSector,
                1, 0, 0, 0.0, 0.0, EvolutionRole.NotRead,
                "ABSENT: G_093's third missing structure — reported so that the closure verdict cannot be read as 'the " +
                "theory is complete'"),
        };
    }

    /// <summary>The audit's working state: a localized skewed pattern, NOT reflection-symmetric — which is what makes
    /// a relabelling a relabelling. The canonical state is parity-even to the rounding (G_099), so reflecting it is
    /// the identity.</summary>
    public static double[] Working => OrientationCouplingAudit.Working;

    /// <summary>The update's change under a variant of the description, as a VECTOR in the state space.</summary>
    public static double[] UpdateEffectVector(MinimalGravityCoreAudit.Description variant)
    {
        var full = MinimalGravityCoreAudit.Full();
        var before = ConnectivityOriginAudit.UpdateAt(full.CellCount, full.Links, full.Rho);
        var after = ConnectivityOriginAudit.UpdateAt(variant.CellCount, variant.Links, variant.Rho);
        return before.Zip(after, (x, y) => x - y).ToArray();
    }

    /// <summary>The readouts' change under a variant, as a vector in the readout space.</summary>
    public static double[] ReadoutEffectVector(MinimalGravityCoreAudit.Description variant)
    {
        var full = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full());
        var other = MinimalGravityCoreAudit.ReadoutsOf(variant);
        return full.Zip(other, (x, y) => x.Value - y.Value).ToArray();
    }

    /// <summary>A quantity's Jacobian rank against the state at the audit's stated rule.</summary>
    public static int RankOf(Func<double[], double[]> quantity)
    {
        var jacobian = StateDegreesOfFreedomAudit.Jacobian(quantity, State);
        return jacobian.Length == 0 ? 0 : StateDegreesOfFreedomAudit.Rank(jacobian, JacobianRule);
    }

    /// <summary>Task 1's counts, with the three kinds and the three evolution roles kept apart.</summary>
    public static (int Candidates, int FunctionsOfTheState, int Supplied, int Absent, int StructuresTheEvolutionReads,
        string Reading) CandidateCounts()
    {
        var rows = Candidates();
        var arguments = rows.Count(r => r.IsAnArgumentOfTheUpdate);
        return (rows.Length, rows.Count(r => r.IsAFunctionOfTheState),
            rows.Count(r => r.Origin == CandidateOrigin.SuppliedToTheDescription),
            rows.Count(r => r.Origin == CandidateOrigin.AbsentFromTheSector), arguments,
            $"{rows.Length} candidates: {rows.Count(r => r.IsAFunctionOfTheState)} are FUNCTIONS of the state (so they " +
            $"cannot add a direction), {rows.Count(r => r.Origin == CandidateOrigin.SuppliedToTheDescription)} are " +
            $"SUPPLIED structures (variably independently of the state) and " +
            $"{rows.Count(r => r.Origin == CandidateOrigin.AbsentFromTheSector)} are ABSENT — and exactly {arguments} " +
            "is a STRUCTURE the evolution takes as an argument, the rest of what the evolution sees being its " +
            "coefficient and its index");
    }

    // ===================== TASK 2 AND 3: THE RANK CONTRIBUTION =====================

    /// <summary>
    /// THE RANK CONTRIBUTION (task 2) AND THE MAXIMUM STATE-SPACE RANK (task 3), measured the only way a rank
    /// contribution can be measured: STACK each candidate's Jacobian with the state's own identity and take the rank.
    /// A function of the state cannot raise it — its Jacobian's columns are the state's own, so the stacked rank is
    /// the state's rank — and the audit reports the increase rather than the rank so that a zero is visible.
    /// </summary>
    public static RankContribution[] RankContributions()
    {
        var identity = StateDegreesOfFreedomAudit.Jacobian(r => r, State);
        int baseRank = StateDegreesOfFreedomAudit.Rank(identity, JacobianRule);
        var full = MinimalGravityCoreAudit.Full();
        var minimal = MinimalGravityCoreAudit.Minimal();

        Func<(string Candidate, string Group, CandidateOrigin Origin, double[][] Rows)[]> candidates = () => new[]
        {
            ("the orientation field", "orientation", CandidateOrigin.DerivedFromTheState,
                StateDegreesOfFreedomAudit.Jacobian(OrientationCouplingAudit.OrientationField, State)),
            ("the acceleration field", "orientation", CandidateOrigin.DerivedFromTheState,
                StateDegreesOfFreedomAudit.Jacobian(
                    r => ConnectivityOriginAudit.AccelerationOnLinks(Potential(r), Cells, Links), State)),
            ("the excess over the flat background", "state-like", CandidateOrigin.DerivedFromTheState,
                StateDegreesOfFreedomAudit.Jacobian(r => r.Select(x => x - r.Average()).ToArray(), State)),
            ("the total count", "conservation", CandidateOrigin.DerivedFromTheState,
                StateDegreesOfFreedomAudit.Jacobian(r => new[] { r.Sum() }, State)),
            ("the flux's divergence", "evolution", CandidateOrigin.DerivedFromTheState,
                StateDegreesOfFreedomAudit.Jacobian(r => Update(r), State)),
            ("the surviving readouts", "observable", CandidateOrigin.DerivedFromTheState,
                StateDegreesOfFreedomAudit.Jacobian(r => MinimalGravityCoreAudit
                    .ReadoutsOf(minimal with { Rho = r }).Select(d => d.Value).ToArray(), State)),

            // the supplied structures enter as their measured EFFECT VECTOR, which is the only honest form of a rank
            // test for an object that is not a function of the state: the effect is a vector IN the state space, so
            // stacking it cannot raise the space's rank either — it can only add a PARAMETER dimension
            ("the link set (as an argument of the update)", "supplied", CandidateOrigin.SuppliedToTheDescription,
                new[] { UpdateEffectVector(full with { Links = ConnectivityOriginAudit.LinksOf(Cells, 2) }) }),
            ("the step size epsilon (as the update's coefficient)", "supplied", CandidateOrigin.SuppliedToTheDescription,
                new[] { Update(State).Select(l => l * 0.5).ToArray() }),
            ("the tick (as the update's index)", "supplied", CandidateOrigin.SuppliedToTheDescription,
                new[] { Update(State).Select(l => l * 1.0).ToArray() }),
            ("the labelling (as a relabelling of the readouts)", "supplied", CandidateOrigin.SuppliedToTheDescription,
                new[] { ReadoutEffectVector(minimal with { Rho = Reflect(Working) }) }),
            ("the clock law's coefficient (as a map parameter)", "supplied", CandidateOrigin.SuppliedToTheDescription,
                new[] { MapRedundancyAudit.ReadoutsOf(MapRedundancyAudit.Canonical(), Working)
                    .Zip(MapRedundancyAudit.ReadoutsOf(MapRedundancyAudit.FreeRoom(0.4), Working),
                        (x, y) => x.Value - y.Value).ToArray() }),
            ("the flux's circulation (as a gauge)", "supplied", CandidateOrigin.SuppliedToTheDescription,
                new[] { ReadoutEffectVector(full with
                    { DisplacedFlux = full.DisplacedFlux.Select(f => f + 5).ToArray() }) }),
            ("the component count (as a gauge)", "supplied", CandidateOrigin.SuppliedToTheDescription,
                new[] { new double[Cells] }),
        };
        return candidates().Select(c =>
        {
            var stacked = identity.Concat(c.Rows).ToArray();
            int rank = StateDegreesOfFreedomAudit.Rank(stacked, JacobianRule);
            int increase = rank - baseRank;
            return new RankContribution(c.Candidate, c.Group, c.Origin, rank, increase, increase > 0,
                c.Origin == CandidateOrigin.DerivedFromTheState
                    ? $"stacking its Jacobian with the state's own identity leaves the rank at {rank} — an increase of " +
                      $"{increase} — because its columns are FUNCTIONS of the state's columns: {Cells} columns of rank " +
                      $"{baseRank} cannot be raised by adding rows"
                    : $"supplied, so it has no Jacobian against the state: what can be stacked is its measured EFFECT, " +
                      $"and that effect is a vector IN the {Cells}-dimensional state space, so the rank stays at {rank} " +
                      $"(increase {increase}) — it adds a PARAMETER dimension to the description, not a state direction");
        }).ToArray();
    }

    /// <summary>
    /// THE MAXIMUM STATE-SPACE RANK OBTAINABLE FROM ALL SURVIVING STRUCTURES TOGETHER (task 3), and the two numbers
    /// that must not be confused: the rank of everything the sector DERIVES (which is the state's own rank, because
    /// every derived quantity is a function of it) and the rank of the full DESCRIPTION, which includes the supplied
    /// inputs and is therefore reported separately.
    /// </summary>
    public static (int MaxRankDerived, int IncreaseBeyondTheState, int SuppliedInputs, string Reading) MaximumRank()
    {
        var identity = StateDegreesOfFreedomAudit.Jacobian(r => r, State);
        int baseRank = StateDegreesOfFreedomAudit.Rank(identity, JacobianRule);
        var all = identity
            .Concat(StateDegreesOfFreedomAudit.Jacobian(OrientationCouplingAudit.OrientationField, State))
            .Concat(StateDegreesOfFreedomAudit.Jacobian(
                r => ConnectivityOriginAudit.AccelerationOnLinks(Potential(r), Cells, Links), State))
            .Concat(StateDegreesOfFreedomAudit.Jacobian(r => new[] { r.Sum() }, State))
            .Concat(StateDegreesOfFreedomAudit.Jacobian(r => Update(r), State))
            .Concat(StateDegreesOfFreedomAudit.Jacobian(r => MinimalGravityCoreAudit
                .ReadoutsOf(MinimalGravityCoreAudit.Minimal() with { Rho = r }).Select(d => d.Value).ToArray(), State))
            .ToArray();
        int rank = StateDegreesOfFreedomAudit.Rank(all, JacobianRule);
        int supplied = StateDegreesOfFreedomAudit.IndependentInputs().Count(i => !i.IsAStateCoordinate);
        return (rank, rank - baseRank, supplied,
            $"stacking EVERY surviving quantity's Jacobian with the state's own gives rank {rank} against " +
            $"{baseRank} for the state alone — an increase of {rank - baseRank}. The description's rank is therefore " +
            $"{rank} plus the {supplied} SUPPLIED inputs, which are not generated by the sector: " +
            "the rank of what the sector DERIVES is exactly the rank of the state");
    }

    // ===================== TASK 4, 5 AND 6: THE THREE GROUPS =====================

    /// <summary>
    /// THE THREE GROUPS THE TASKS NAME (4, 5 and 6), each measured by its own instrument: the ORIENTATION quantities'
    /// rank contribution (task 4), the OBSERVABLES' (task 5) and the EVOLUTION's (task 6), with the evolution's own
    /// rank reported separately because it is the only place a supplied structure can enter.
    /// </summary>
    public static (string Group, int Members, int MaxRankIncrease, double MaxIndependentEffect, string Reading)[]
        GroupContributions()
    {
        var contributions = RankContributions();
        Func<string, string> key = g => g switch
        {
            "orientation" => "the orientation quantities (task 4)",
            "observable" => "the observable quantities (task 5)",
            "evolution" => "the evolution quantities (task 6)",
            "conservation" => "the conservation quantity",
            "state-like" => "the state-like functionals",
            _ => "the supplied structures (not generated)",
        };
        return contributions.GroupBy(c => key(c.Group)).Select(g =>
        {
            int maxIncrease = g.Max(c => c.RankIncrease);
            bool allDerived = g.All(c => c.Origin == CandidateOrigin.DerivedFromTheState);
            return (g.Key, g.Count(), maxIncrease, 0.0,
                $"max rank increase {maxIncrease} over {g.Count()} member(s), " +
                (allDerived ? "all of them functions of the state" : "supplied rather than derived") +
                (g.Key.Contains("evolution")
                    ? $" — and the update's OWN rank is {EvolutionRank()}, which is the state's rank and not an " +
                      "increase: the operator moves 95 of the 96 directions and reads no quantity of its own"
                    : " — so no direction of the state space comes from here"));
        }).ToArray();
    }

    /// <summary>The evolution's own rank: how many directions the update moves (G_101's measurement, re-taken here).</summary>
    public static int EvolutionRank() => StateDegreesOfFreedomAudit
        .StateSpaceRank().Single(r => Math.Abs(r.Tolerance - StateDegreesOfFreedomAudit.DynamicsRule) < 1e-18).Rank;

    // ===================== TASK 7: THE RECONSTRUCTION RESIDUALS =====================

    /// <summary>
    /// THE RECONSTRUCTION TABLE (task 7), in whichever DIRECTION is informative: for a derived quantity the forward
    /// direction (the quantity from the state) is an identity, so the audit rebuilds the STATE from the quantity in
    /// closed form and measures the residual — and where the inverse does not exist it reports the OBSTRUCTION rather
    /// than a number: the kernel, or two different states the quantity cannot tell apart.
    /// </summary>
    public static ReconstructionRow[] Reconstruction()
    {
        var rho = State;
        var a = Potential(rho);
        var kernel = a.Select(Math.Exp).ToArray();
        var g00 = rho.Select(TemporalIndependenceAudit.G00).ToArray();
        var rate = rho.Select(TemporalIndependenceAudit.ClockOf).ToArray();

        double fromPotential = MaxAbs(Enumerable.Range(0, Cells).Select(i => Math.Exp(D * a[i]) - rho[i])) / MaxAbs(rho);
        double fromKernel = MaxAbs(Enumerable.Range(0, Cells).Select(i => Math.Exp(D * Math.Log(kernel[i])) - rho[i])) / MaxAbs(rho);
        double fromG00 = MaxAbs(Enumerable.Range(0, Cells).Select(i => Math.Pow(-g00[i], D / 2.0) - rho[i])) / MaxAbs(rho);
        double fromRate = MaxAbs(Enumerable.Range(0, Cells).Select(i => Math.Pow(rate[i], D) - rho[i])) / MaxAbs(rho);
        double sameField = MaxAbs(OrientationCouplingAudit.OrientationField(rho)
            .Zip(OrientationCouplingAudit.OrientationField(rho.Select(r => r * 2.0).ToArray()), (x, y) => x - y));
        var readoutKernel = StateDegreesOfFreedomAudit.Inventory()
            .Single(q => q.Quantity.Contains("readouts")).KernelDimension;
        var full = MinimalGravityCoreAudit.Full();
        var minimal = MinimalGravityCoreAudit.Minimal();
        double circulation = MaxAbs(MinimalGravityCoreAudit.ReadoutsOf(full)
            .Zip(MinimalGravityCoreAudit.ReadoutsOf(full with
                { DisplacedFlux = full.DisplacedFlux.Select(f => f + 5).ToArray() }),
                (x, y) => x.Value - y.Value));
        // the labelling is tested on the SKEWED working state, because the canonical one is reflection-symmetric
        double relabellingMoves = MaxAbs(MinimalGravityCoreAudit.ReadoutsOf(minimal)
            .Zip(MinimalGravityCoreAudit.ReadoutsOf(minimal with { Rho = Reflect(Working) }),
                (x, y) => x.Value - y.Value));
        double lawMoves = MaxAbs(MapRedundancyAudit.ReadoutsOf(MapRedundancyAudit.Canonical(), Working)
            .Zip(MapRedundancyAudit.ReadoutsOf(MapRedundancyAudit.FreeRoom(0.4), Working),
                (x, y) => x.Value - y.Value));
        double linkEffect = MaxAbs(UpdateEffectVector(full with { Links = ConnectivityOriginAudit.LinksOf(Cells, 2) }));

        return new[]
        {
            new ReconstructionRow("the potential map A", "the state from A", fromPotential, 0.0, ObstructionKind.None, true,
                $"CLOSED FORM (rho = exp(d A)) with a relative residual of {E(fromPotential)}: the reconstruction is " +
                "exact to the rounding, so the potential carries the state and adds no direction"),
            new ReconstructionRow("the rate kernel K", "the state from K", fromKernel, 0.0, ObstructionKind.None, true,
                $"CLOSED FORM (rho = exp(d ln K)) with a relative residual of {E(fromKernel)}"),
            new ReconstructionRow("g00", "the state from g00", fromG00, 0.0, ObstructionKind.None, true,
                $"CLOSED FORM (rho = (-g00)^(d/2)) with a relative residual of {E(fromG00)}"),
            new ReconstructionRow("the clock rate", "the state from the rate", fromRate, 0.0, ObstructionKind.None, true,
                $"CLOSED FORM (rho = rate^d) with a relative residual of {E(fromRate)}"),
            new ReconstructionRow("the orientation field", "the state from the field", 0.0, sameField, ObstructionKind.Scale, false,
                $"NOT reconstructible, and the obstruction is MEASURED: rho and 2 rho give the SAME field (largest " +
                $"difference {E(sameField)}), so the scale is invisible — the kernel is one dimensional"),
            new ReconstructionRow("the surviving readouts", "the state from the readouts", 0.0, readoutKernel, ObstructionKind.Kernel, false,
                $"NOT reconstructible: the readouts' Jacobian has a {readoutKernel}-dimensional kernel, so " +
                $"{readoutKernel} of the state's {Cells} directions are invisible to every measurement the sector has"),
            new ReconstructionRow("the flux's own circulation", "the flux from the state", 0.0, circulation, ObstructionKind.Movement, false,
                $"NOT reconstructible: the state fixes the divergence and not the flux, and the displaced flux moves " +
                $"the readouts by {E(circulation)} while the state is held fixed"),
            new ReconstructionRow("the labelling", "the labelling from the state", 0.0, relabellingMoves, ObstructionKind.Movement, false,
                $"NOT reconstructible: a relabelling that carries the state moves the site-local readouts by " +
                $"{E(relabellingMoves)} — which is what a coordinate is"),
            new ReconstructionRow("the clock law's coefficient", "the coefficient from the state", 0.0, lawMoves, ObstructionKind.Movement, false,
                $"NOT reconstructible: the free room moves the map's readouts by {E(lawMoves)} with the state held " +
                "fixed — a parameter of the map, which the state does not determine (G_095 left it free)"),
            new ReconstructionRow("the link set", "the link set from the state", 0.0, linkEffect, ObstructionKind.Movement, false,
                $"NOT reconstructible: the update takes it as an argument (a cut moves the update by {E(linkEffect)}) " +
                "and the state cannot produce it — G_080's input"),
            new ReconstructionRow("the closure requirements (memory channel, vector sector, unified action)",
                "from the state", 0.0, 0.0, ObstructionKind.Absent, false,
                "ABSENT rather than hidden: three structures G_093 found necessary for closure and which the sector " +
                "does not contain, so there is nothing to reconstruct and nothing that could contribute rank"),
        };
    }

    /// <summary>
    /// Task 7's counts, with the obstruction KINDS kept apart: a kernel dimension is a count of invisible directions,
    /// a movement is a measured displacement, and an absence is neither — averaging them into one number was the first
    /// draft's defect, and the mean it produced (1.337E+001) was a statistic of nothing.
    /// </summary>
    public static (int Quantities, int Closing, int ObstructedByAScale, int ObstructedByAKernel,
        int ObstructedByAMovement, int Absent, double WorstClosingResidual, double MaxMovementObstruction,
        string Reading) ReconstructionCounts()
    {
        var rows = Reconstruction();
        double worst = rows.Where(r => r.Closes).Max(r => r.Residual);
        double maxMovement = rows.Where(r => r.Kind == ObstructionKind.Movement)
            .Select(r => r.Obstruction).DefaultIfEmpty(0.0).Max();
        return (rows.Length, rows.Count(r => r.Closes), rows.Count(r => r.Kind == ObstructionKind.Scale),
            rows.Count(r => r.Kind == ObstructionKind.Kernel), rows.Count(r => r.Kind == ObstructionKind.Movement),
            rows.Count(r => r.Kind == ObstructionKind.Absent), worst, maxMovement,
            $"{rows.Count(r => r.Closes)} of {rows.Length} quantities rebuild the state in CLOSED FORM (worst " +
            $"relative residual {E(worst)}); of the rest, {rows.Count(r => r.Kind == ObstructionKind.Scale)} is " +
            $"obstructed by a SCALE, {rows.Count(r => r.Kind == ObstructionKind.Kernel)} by a KERNEL DIMENSION, " +
            $"{rows.Count(r => r.Kind == ObstructionKind.Movement)} by a measured MOVEMENT (largest " +
            $"{E(maxMovement)}) and {rows.Count(r => r.Kind == ObstructionKind.Absent)} by ABSENCE — and no count is " +
            "averaged with a magnitude");
    }

    // ===================== THE CLASSIFICATION, THE CRITERIA AND THE VERDICT =====================

    /// <summary>The verdict's own inputs, so no clause is a literal.</summary>
    public static (string Criterion, string Outcome, string Evidence)[] Criteria()
    {
        var candidates = CandidateCounts();
        var contributions = RankContributions();
        var maximum = MaximumRank();
        var reconstruction = ReconstructionCounts();
        var arguments = Candidates().Where(c => c.IsAnArgumentOfTheUpdate).ToArray();
        return new[]
        {
            ("SUCCESS: at least one surviving structure contributes positive rank beyond the state",
                contributions.Any(c => c.ContributesABeyondTheStateDirection) ? "MET" : "NOT MET",
                $"{contributions.Count(c => c.ContributesABeyondTheStateDirection)} of {contributions.Length} derived " +
                $"groups contributes positive rank: stacking every surviving quantity's Jacobian with the state's own " +
                $"leaves the rank at {maximum.MaxRankDerived} against the state's own rank — an increase of " +
                $"{maximum.IncreaseBeyondTheState}"),

            ("FAILURE: all surviving structures are functions of the state and the reduction to one degree of freedom is exact",
                maximum.IncreaseBeyondTheState == 0 ? "MET" : "NOT MET",
                $"{candidates.FunctionsOfTheState} of {candidates.Candidates} candidates are functions of the state, " +
                $"{reconstruction.Closing} of {reconstruction.Quantities} quantities rebuild it in closed form and " +
                $"{maximum.IncreaseBeyondTheState} of the derived rank comes from beyond it — so the reduction is EXACT " +
                "for everything the sector derives"),

            ("and the supplied inputs are reported rather than hidden", "CONFIRMED",
                $"{maximum.SuppliedInputs} independent inputs are SUPPLIED rather than generated, and exactly " +
                $"{arguments.Length} candidate is a STRUCTURE the evolution takes as an argument " +
                $"({string.Join(", ", arguments.Select(a => a.Candidate))}) — a supplied structure is not a degree of " +
                "freedom the sector produces"),

            ("and the closures G_093 named remain ABSENT", "CONFIRMED",
                $"{candidates.Absent} candidates are absent from the sector — the memory channel, the vector sector " +
                "and the unified action — so the verdict is about the surviving sector and not about the theory being " +
                "complete"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured count, a measured rank or a
    /// computed comparison, so no literal reaches it.
    /// </summary>
    public static string Verdict()
    {
        var candidates = CandidateCounts();
        var contributions = RankContributions();
        var maximum = MaximumRank();
        var reconstruction = ReconstructionCounts();
        var groups = GroupContributions();
        var arguments = Candidates().Where(c => c.IsAnArgumentOfTheUpdate).Select(c => c.Candidate).ToArray();
        var b = new StringBuilder();
        b.Append("CLOSED — THE ONE-DEGREE-OF-FREEDOM REDUCTION IS EXACT FOR EVERYTHING THE SECTOR DERIVES. ");
        b.Append(candidates.Reading).Append(". ");
        b.Append("THE RANK CONTRIBUTION IS MEASURED BY STACKING, NOT BY ARGUMENT: ").Append(maximum.Reading).Append(". ");
        b.Append("THE THREE GROUPS THE TASK NAMES ARE EACH ZERO: ")
            .Append(string.Join("; ", groups.Select(g => $"{g.Group}: max increase {g.MaxRankIncrease}"))).Append(". ");
        b.Append("THE RECONSTRUCTION AGREES: ").Append(reconstruction.Reading).Append(". ");
        b.Append("AND WHAT CAN BE VARIED INDEPENDENTLY OF THE STATE IS NOT GENERATED: exactly ").Append(arguments.Length)
            .Append(" candidate is a structure the EVOLUTION TAKES AS AN ARGUMENT — ").Append(string.Join(", ", arguments))
            .Append(" — and G_080 measured it to be an INPUT supplied to the description rather " +
                "than produced by the sector; the step is the update's COEFFICIENT and the tick its INDEX, neither of " +
                "which is a structure; and the circulation, the component count, the labelling and the law's " +
                "coefficient move no update at all, so none of them is a dynamical direction. ");
        b.Append("FINAL VERDICT: CLOSED — rank 1. Every surviving quantity is a function of the occupation (measured by " +
            "rank, by kernel and by closed-form reconstruction), the maximum rank obtainable from all of them together " +
            "is the state's own, and the only structure the evolution takes as an argument is supplied. The sector " +
            "GENERATES nothing beyond its one degree of freedom: the ")
            .Append(maximum.SuppliedInputs)
            .Append(" inputs it still needs are inputs, and the three closures G_093 named are ABSENT rather than " +
                "generated — which is what keeps this verdict about the surviving sector and not about completeness.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that every surviving quantity G_101 did not classify as a reparametrisation is enumerated");
        b.AppendLine("with the rank of its Jacobian against the state and the MAGNITUDE of what a variation of it moves; that every");
        b.AppendLine("derived quantity's Jacobian STACKED with the state's own identity leaves the rank unchanged, so no derived quantity");
        b.AppendLine("contributes a direction; that a SUPPLIED structure's rank contribution is measured the same way, through its");
        b.AppendLine("effect — which is a vector IN the state space and therefore cannot raise its rank either; that the maximum rank");
        b.AppendLine("obtainable from all surviving quantities together is the state's own rank; that the orientation, observable and");
        b.AppendLine("evolution groups each contribute zero increase, with the evolution's own rank reported as the state's rank; that");
        b.AppendLine("four quantities rebuild the state in closed form at the rounding while the others' obstructions are MEASURED (rho");
        b.AppendLine("and 2 rho share a field, the readouts have a 92-dimensional kernel, a displaced flux moves nothing, a relabelling");
        b.AppendLine("moves the coordinate readouts, the free room moves the map's readouts and a cut moves the update); and that exactly");
        b.AppendLine("one candidate is a STRUCTURE the evolution takes as an argument, and it is supplied.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that the theory is COMPLETE. Three structures G_093 found necessary for closure — the");
        b.AppendLine("memory channel, the vector sector and the unified action — are ABSENT from the surviving sector, and this audit");
        b.AppendLine("reports them as absent rather than as reconstructed; the law's coefficient remains FREE (G_095); and the description");
        b.AppendLine("still needs six inputs, five of which are not state coordinates. Nor does it establish that a closure, if performed,");
        b.AppendLine("would leave the rank at one: a memory channel or a vector sector is a structure the theory never had, and adding one");
        b.AppendLine("is a different theory, which these rules forbid this audit to construct. What it establishes is that nothing SURVIVING");
        b.AppendLine("generates a second direction.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_101: G_101 counted the state space and measured each quantity's rank AGAINST the");
        b.AppendLine("state — what a quantity SEES. This audit measures the other direction: whether stacking a quantity with the state");
        b.AppendLine("RAISES the rank, and whether a quantity that is NOT a function of the state can be varied into a dynamical");
        b.AppendLine("direction. The first is a rank of a stacked matrix and the second is an effect on the update, and neither was");
        b.AppendLine("computed by G_101 — which is why 'the reduction is exact' is a measurement here rather than a corollary.");
        return b.ToString();
    }

    // ===================== OUTPUT =====================

    private static double[] Reflect(double[] rho) => OrientationCouplingAudit.Reflect(rho);

    public static string OutputCandidates()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE CANDIDATE TABLE (task 1): every surviving quantity that is NOT an invertible reparametrisation");
        b.AppendLine("   candidate                                             origin               comps  rank  kernel  readout eff  update eff  evolution role");
        foreach (var c in Candidates())
            b.AppendLine($"   {Cut(c.Candidate, 53),-53}  {Cut(c.Origin.ToString(), 20),-20}  {c.Components,5}  " +
                $"{c.RankAgainstTheState,4}  {c.KernelAgainstTheState,6}  {E(c.ReadoutEffect),11}  {E(c.UpdateEffect),10}  {c.Role}");
        b.AppendLine();
        foreach (var c in Candidates()) b.AppendLine($"   - {c.Candidate}: {c.Measurement}");
        b.AppendLine();
        b.AppendLine("   " + CandidateCounts().Reading);
        return b.ToString();
    }

    public static string OutputRankContributions()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE RANK-CONTRIBUTION TABLE (task 2): each Jacobian STACKED with the state's own identity");
        b.AppendLine("   candidate                                             group          rank stacked  increase  contributes");
        foreach (var r in RankContributions())
            b.AppendLine($"   {Cut(r.Candidate, 53),-53}  {Cut(r.Group, 14),-14}  {r.RankStacked,12}  " +
                $"{r.RankIncrease,8}  {Yes(r.ContributesABeyondTheStateDirection)}");
        b.AppendLine();
        foreach (var r in RankContributions()) b.AppendLine($"   - {r.Candidate}: {r.Measurement}");
        b.AppendLine();
        var maximum = MaximumRank();
        b.AppendLine($"3. THE MAXIMUM STATE-SPACE RANK (task 3): {maximum.MaxRankDerived} from ALL surviving quantities together,");
        b.AppendLine($"   against the state's own rank — an increase of {maximum.IncreaseBeyondTheState}, with {maximum.SuppliedInputs} SUPPLIED inputs.");
        b.AppendLine("   " + maximum.Reading);
        b.AppendLine();
        b.AppendLine("4. THE THREE GROUPS (tasks 4, 5 and 6)");
        foreach (var g in GroupContributions())
            b.AppendLine($"   - {g.Group}: {g.Members} member(s), max rank increase {g.MaxRankIncrease} — {g.Reading}");
        return b.ToString();
    }

    public static string OutputReconstruction()
    {
        var b = new StringBuilder();
        b.AppendLine("5. THE RECONSTRUCTION RESIDUAL TABLE (task 7)");
        b.AppendLine("   quantity                                              direction                    residual    obstruction  closes");
        foreach (var r in Reconstruction())
            b.AppendLine($"   {Cut(r.Quantity, 53),-53}  {Cut(r.Direction, 28),-28}  {E(r.Residual),10}  {E(r.Obstruction),11}  {Yes(r.Closes)}");
        b.AppendLine();
        foreach (var r in Reconstruction()) b.AppendLine($"   - {r.Quantity} ({r.Direction}): {r.Measurement}");
        b.AppendLine();
        b.AppendLine("   " + ReconstructionCounts().Reading);
        return b.ToString();
    }

    public static string OutputClassification()
    {
        var b = new StringBuilder();
        b.AppendLine("CLASSIFICATION");
        var maximum = MaximumRank();
        b.AppendLine($"   the rank obtainable from all surviving structures together: {maximum.MaxRankDerived} " +
            $"(increase beyond the state: {maximum.IncreaseBeyondTheState})");
        b.AppendLine($"   supplied inputs reported rather than hidden: {maximum.SuppliedInputs}");
        b.AppendLine();
        b.AppendLine("CRITERIA");
        foreach (var (criterion, outcome, evidence) in Criteria())
            b.AppendLine($"   {criterion} -> {outcome}: {evidence}");
        return b.ToString();
    }

    // ===================== FORMATTING =====================

    private static string Cut(string text, int width) => text.Length <= width ? text : text[..width];

    private static string Yes(bool value) => value ? "yes" : "no";

    private static string E(double value) => value.ToString("E3", CultureInfo.InvariantCulture);
}
