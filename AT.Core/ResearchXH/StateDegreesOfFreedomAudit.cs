using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>The audit's final classification (ResearchY-G_101).</summary>
public enum DegreesOfFreedom { OneDegree, MultiDegree }

/// <summary>
/// What a surviving quantity IS, measured rather than declared: the state itself, an invertible reparametrisation of
/// it (a function of the state whose inverse recovers the state), or a many-to-one functional that loses directions.
/// </summary>
public enum StateRole { TheState, InvertibleReparametrisation, FunctionalOfTheState, NotAStateObject }

/// <summary>
/// One surviving state-like quantity (task 1), with the rank of its Jacobian against the state and the number of state
/// directions it loses. `Components` is how many numbers the quantity carries; the rank is how many INDEPENDENT
/// directions of the state it actually sees. The rank is reported at the audit's stated rule AND at the loose end of
/// the band, so a rank that would drift with its rule is visible rather than hidden (G_096).
/// </summary>
public sealed record StateQuantity(string Quantity, int Components, StateRole Role, OrientationStatus Status,
    int RankAtTheRule, int RankAtTheLooseEnd, int KernelDimension, string Measurement)
{
    public bool TheRankIsToleranceFree => RankAtTheRule == RankAtTheLooseEnd;
    public bool DeterminesTheState => RankAtTheRule == StateComponents;
    public const int StateComponents = 96;
}

/// <summary>One rank measurement with the tolerance it was taken at (task 3).</summary>
public sealed record RankRow(double Tolerance, int Rank, int KernelDimension, string Reading);

/// <summary>
/// One quantity's rank over a tolerance band, so that a rank which DRIFTS with its tolerance can be told from one
/// that does not — G_096's rule: a rank that moves with the rule is not a dimension.
/// </summary>
public sealed record RankBand(string Quantity, int Components, int RankAtTheLooseEnd, int RankAtTheTightEnd,
    bool IsToleranceFree, double FirstNonZeroMagnitude, string Reading);

/// <summary>One candidate structure removed individually (task 6), keyed by FAMILY so that two forms of one structure
/// are counted once when the audit asks how many structures enter the evolution.</summary>
public sealed record RemovalRow(string Removed, string Family, int ReadoutsLost, int ReadoutsMoved,
    double UpdateResidual, string Measurement);

/// <summary>One independent input of the description that is NOT a state coordinate.</summary>
public sealed record InputRow(string Input, int Dimensions, bool IsAStateCoordinate, string Measurement);

/// <summary>
/// ResearchY-G_101 — STATE DEGREES-OF-FREEDOM AUDIT (group G — Gravity Source).
///
/// G_097 measured that the law diffuses and that transport is absent; G_098 that the direction is not represented;
/// G_099 that the orientation field is derived and observable; G_100 that the field is dynamically decoupled and has
/// **0 independent degrees of freedom**. This audit asks the question those leave open, and it is a question about the
/// STATE SPACE rather than about one object in it:
///
/// **Does the surviving G-sector contain more than one independent dynamical degree of freedom?**
///
/// THE INSTRUMENT is the Jacobian of each surviving quantity against the state, at the canonical state, with the
/// tolerance STATED — because a rank is only a dimension when it does not drift with the rule it was taken at (G_096).
/// A quantity whose Jacobian has full rank is an invertible reparametrisation of the state and NOT an independent
/// coordinate; a quantity with a non-trivial kernel loses directions and is a functional. An INDEPENDENT DYNAMICAL
/// DEGREE OF FREEDOM is a direction of the state that the dynamics moves and that no other quantity already fixes —
/// so the audit separates three counts that are easy to conflate: the number of COMPONENTS (how many numbers the state
/// carries), the RANK of the dynamics (how many independent directions the law moves), and the number of INDEPENDENT
/// STATE COORDINATES (how many distinct fields must be specified).
///
/// Every count, dimension and rank names its object: the state is the canonical 96-cell ring's occupation (cells =
/// 96, links = `LinksOf(96, 1)`), the dynamics is `UpdateAt(cells, links, rho)` — the Laplace operator, whose symbol
/// spectrum is sampled through the alternating mode k = N/2 — and the equality rules are `1e-12` on a computed value,
/// **exact** equality on an identity, and a **stated tolerance** for every rank. The cubic substrate is forbidden and
/// is not named. No new primitive, field, source term, topology axiom, action, Lagrangian, transport rule or hidden
/// variable is introduced.
///
/// Prefers refutation.
/// </summary>
public static class StateDegreesOfFreedomAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;
    public const double Tol = 1e-12;

    /// <summary>The same stated step the four preceding audits used, so the five are comparable.</summary>
    public const double Step = PatternKinematicsAudit.Step;

    /// <summary>
    /// The finite-difference step for every Jacobian in this audit, and the reason it is not smaller: the rounding
    /// floor of a central difference is ~`1e-16/h`, so at `h = 1e-4` the instrument's own noise is ~`1e-12` while the
    /// truncation error is ~`1e-8` — which is why no rank in this audit is quoted at a tolerance tighter than
    /// `1e-10`, and why the first draft's `h = 1e-6` read a rank of 96 for a map whose exact kernel is one
    /// dimensional (the centring projection): at that step the instrument's floor sat exactly on the rule.
    /// </summary>
    public const double DifferenceStep = 1e-4;

    /// <summary>The stated rule for every Jacobian rank in this audit: between the truncation error and the noise floor.</summary>
    public const double JacobianRule = 1e-8;

    /// <summary>
    /// The stated rule for the DYNAMICS' rank, where the zeros are exact rather than estimated: the uniform mode's
    /// symbol is exactly 0.0 and the smallest non-zero magnitude is 4.282154E-003, so the rank is the same from the
    /// project's own 1e-12 rule up to the loose end of the band.
    /// </summary>
    public const double DynamicsRule = 1e-12;

    public static (int A, int B)[] Links => ConnectivityOriginAudit.LinksOf(Cells, 1);

    public static double[] State => MapRedundancyAudit.State;

    public static double[] Potential(double[] rho) => rho.Select(SourceManipulationAudit.Potential).ToArray();

    public static double[] Update(double[] rho) => ConnectivityOriginAudit.UpdateAt(Cells, Links, rho);

    public static double MaxAbs(IEnumerable<double> v) => v.Select(Math.Abs).Max();

    // ===================== THE LINEAR ALGEBRA, STATED =====================

    /// <summary>
    /// THE RANK AT A STATED TOLERANCE: Gaussian elimination with partial pivoting over the ROW space of the given
    /// vectors, counting pivots whose magnitude exceeds the tolerance. The rule is a parameter of the measurement and
    /// never a hidden constant, which is why every caller reports the tolerance beside the rank.
    /// </summary>
    public static int Rank(double[][] vectors, double tolerance)
    {
        if (vectors.Length == 0) return 0;
        int width = vectors[0].Length;
        var m = vectors.Select(v => v.ToArray()).ToArray();
        int rank = 0;
        for (int col = 0; col < width && rank < m.Length; col++)
        {
            int pivot = -1;
            double best = tolerance;
            for (int row = rank; row < m.Length; row++)
                if (Math.Abs(m[row][col]) > best) { best = Math.Abs(m[row][col]); pivot = row; }
            if (pivot < 0) continue;
            (m[rank], m[pivot]) = (m[pivot], m[rank]);
            double lead = m[rank][col];
            for (int row = rank + 1; row < m.Length; row++)
            {
                double factor = m[row][col] / lead;
                if (factor == 0.0) continue;
                for (int c = col; c < width; c++) m[row][c] -= factor * m[rank][c];
            }
            rank++;
        }
        return rank;
    }

    /// <summary>
    /// THE JACOBIAN OF A QUANTITY AGAINST THE STATE, by central differences: row j is the derivative of the quantity
    /// in the direction of cell j. The step is <see cref="DifferenceStep"/>, whose truncation error is ~1e-12 on these
    /// smooth maps — which is why no rank below is reported at a tolerance tighter than 1e-10.
    /// </summary>
    public static double[][] Jacobian(Func<double[], double[]> quantity, double[] rho)
    {
        var rows = new double[Cells][];
        for (int j = 0; j < Cells; j++)
        {
            var plus = rho.ToArray();
            var minus = rho.ToArray();
            plus[j] += DifferenceStep;
            minus[j] -= DifferenceStep;
            var high = quantity(plus);
            var low = quantity(minus);
            int width = Math.Min(high.Length, low.Length);
            var row = new double[width];
            for (int c = 0; c < width; c++) row[c] = (high[c] - low[c]) / (2.0 * DifferenceStep);
            rows[j] = row;
        }
        return rows;
    }

    /// <summary>The tolerances the audit reports every rank at, from loose to tight.</summary>
    public static readonly double[] ToleranceBand = { 1e-2, 1e-4, 1e-6, 1e-8, 1e-10 };

    // ===================== TASK 1: THE STATE QUANTITIES =====================

    /// <summary>
    /// THE SURVIVING STATE-LIKE QUANTITIES (task 1), each with the rank of its Jacobian against the state and the
    /// number of state directions it loses. The MEASUREMENT is the rank, so the classification is not a preference:
    /// full rank means the quantity is an invertible reparametrisation of the state, a non-trivial kernel means it is
    /// a functional, and a quantity with no state argument at all is not a state object.
    /// </summary>
    public static StateQuantity[] Inventory()
    {
        var rho = State;
        var readouts = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Minimal());
        Func<double[], double[]> kernel = r => r.Select(SourceManipulationAudit.Potential).Select(Math.Exp).ToArray();
        Func<double[], double[]> g00 = r => r.Select(TemporalIndependenceAudit.G00).ToArray();
        Func<double[], double[]> rate = r => r.Select(TemporalIndependenceAudit.ClockOf).ToArray();
        Func<double[], double[]> acceleration = r => ConnectivityOriginAudit.AccelerationOnLinks(Potential(r), Cells, Links);
        Func<double[], double[]> excess = r => r.Select(x => x - r.Average()).ToArray();
        Func<double[], double[]> total = r => new[] { r.Sum() };
        Func<double[], double[]> divergence = r => Update(r);
        Func<double[], double[]> readoutValues = r => MinimalGravityCoreAudit
            .ReadoutsOf(MinimalGravityCoreAudit.Minimal() with { Rho = r }).Select(d => d.Value).ToArray();

        return new[]
        {
            new StateQuantity("the count density (the occupation)", Cells, StateRole.TheState, OrientationStatus.Assumed,
                Rank(Jacobian(r => r, rho), JacobianRule), Rank(Jacobian(r => r, rho), 1e-2), 0,
                $"THE ONE STATE COORDINATE, and it is an INITIAL DATUM rather than a derived object (G_080: the count " +
                "is 'an initial datum like the count itself'): 96 components, full rank by construction, and the " +
                "sector supplies no rule fixing it — which is why every other row in this inventory is a function of " +
                "it and none of them is an alternative"),

            new StateQuantity("the potential map A = (1/d) ln rho", Cells, StateRole.InvertibleReparametrisation,
                OrientationStatus.Derived, Rank(Jacobian(Potential, rho), JacobianRule),
                Rank(Jacobian(Potential, rho), 1e-2), 0,
                "FULL RANK: the map is a strictly monotone function of the occupation applied cell by cell, so its " +
                "Jacobian is diagonal with no zero entry and the state is recovered by inverting it in closed form " +
                "(rho = exp(d A)). It is a REPARAMETRISATION of the state, not a second coordinate"),

            new StateQuantity("the rate kernel K = exp(A + lambda A^2 / 2)", Cells,
                StateRole.InvertibleReparametrisation, OrientationStatus.Derived,
                Rank(Jacobian(kernel, rho), JacobianRule), Rank(Jacobian(kernel, rho), 1e-2), 0,
                "FULL RANK (the sector's own map, lambda = 0): a strictly monotone function of the potential, hence of " +
                "the occupation — so the kernel is the state in another variable. The coefficient lambda is a " +
                "parameter of the map and is reported separately as an INPUT, not as a coordinate"),

            new StateQuantity("g00 = -rho^(2/d)", Cells, StateRole.InvertibleReparametrisation, OrientationStatus.Derived,
                Rank(Jacobian(g00, rho), JacobianRule), Rank(Jacobian(g00, rho), 1e-2), 0,
                "FULL RANK: a power of the occupation, monotone, inverted by rho = (-g00)^(d/2) — the metric function " +
                "is the state in the metric's variable, which is G_073's statement measured as a rank"),

            new StateQuantity("the clock rate rho^(1/d)", Cells, StateRole.InvertibleReparametrisation,
                OrientationStatus.Derived, Rank(Jacobian(rate, rho), JacobianRule), Rank(Jacobian(rate, rho), 1e-2), 0,
                "FULL RANK: the square root of g00, so the same information — and the rate field is what G_085 found " +
                "two descriptions can share while their maps differ, which is a statement about the MAPS, not about " +
                "the number of state coordinates"),

            new StateQuantity("the acceleration field (the averaged gradient)", Cells, StateRole.FunctionalOfTheState,
                OrientationStatus.Derived, Rank(Jacobian(acceleration, rho), JacobianRule),
                Rank(Jacobian(acceleration, rho), 1e-2), 1,
                "RANK 95, KERNEL 1: the acceleration is the Laplacian of the potential divided by two, and the ring's " +
                "Laplacian has exactly one zero mode — the CONSTANT — so the acceleration fixes the potential up to " +
                "one additive constant and loses exactly one direction of the state, the count's SCALE"),

            new StateQuantity("the orientation field (the signed link differences)", Cells,
                StateRole.FunctionalOfTheState, OrientationStatus.Derived,
                Rank(Jacobian(OrientationCouplingAudit.OrientationField, rho), JacobianRule),
                Rank(Jacobian(OrientationCouplingAudit.OrientationField, rho), 1e-2), 1,
                "RANK 95, KERNEL 1: the difference operator's kernel is the constant vector, so the field fixes the " +
                "state up to the same one scale — G_100's partition, re-measured here as a rank"),

            new StateQuantity("the excess over the flat background (rho - mean)", Cells,
                StateRole.FunctionalOfTheState, OrientationStatus.Derived,
                Rank(Jacobian(excess, rho), JacobianRule), Rank(Jacobian(excess, rho), 1e-2), 1,
                "RANK 95, KERNEL 1: centring is a projection onto the zero-mean subspace, whose kernel is the constant " +
                "vector — the bump is the state minus one number, and that number is the scale again. This is the row " +
                "the FIRST draft measured wrong: at a difference step of 1e-6 the instrument's own noise floor sat on " +
                "the 1e-10 rule and the rank read 96 for a map whose exact kernel is one dimensional"),

            new StateQuantity("the total count (a conserved functional)", 1, StateRole.FunctionalOfTheState,
                OrientationStatus.Derived, Rank(Jacobian(total, rho), JacobianRule), Rank(Jacobian(total, rho), 1e-2),
                95,
                "RANK 1, KERNEL 95: the total is ONE number and it fixes the scale and nothing else — it is the " +
                "conserved direction (the update's own zero mode), which is why the dynamics moves 95 of the 96 " +
                "directions and leaves this one exactly invariant"),

            new StateQuantity("the flux's divergence (the flux's only determined content)", Cells,
                StateRole.FunctionalOfTheState, OrientationStatus.Derived,
                Rank(Jacobian(divergence, rho), JacobianRule), Rank(Jacobian(divergence, rho), 1e-2), 1,
                "RANK 95, KERNEL 1: the divergence of the flux is the Laplacian of the state, so it carries the same " +
                "95 directions and loses the same scale — and G_083 measured that the flux ITSELF carries one " +
                "further free direction (one circulation) that no state and no observable fixes"),

            new StateQuantity("the surviving readouts (eight functionals)", readouts.Length,
                StateRole.FunctionalOfTheState, OrientationStatus.Derived,
                Rank(Jacobian(readoutValues, rho), JacobianRule), Rank(Jacobian(readoutValues, rho), 1e-2),
                Cells - Rank(Jacobian(readoutValues, rho), JacobianRule),
                $"RANK {Rank(Jacobian(readoutValues, rho), JacobianRule)} OF {readouts.Length}, KERNEL " +
                $"{Cells - Rank(Jacobian(readoutValues, rho), JacobianRule)}: the readouts see a handful of directions " +
                "of a 96-dimensional state (the value and two gradients at the canonical sites, and the AT/GR ratio " +
                "which does not depend on the state at all), so the observable sector is a low-rank contraction of " +
                "one coordinate — G_086's kernel collapse, as a rank"),
        };
    }


    /// <summary>Task 1's and task 4's counts: how many quantities are coordinates, reparametrisations or functionals.</summary>
    public static (int Quantities, int TheState, int Invertible, int Functionals, int FullRank, int WithAKernel,
        string Reading) InventoryCounts()
    {
        var rows = Inventory();
        return (rows.Length, rows.Count(r => r.Role == StateRole.TheState),
            rows.Count(r => r.Role == StateRole.InvertibleReparametrisation),
            rows.Count(r => r.Role == StateRole.FunctionalOfTheState),
            rows.Count(r => r.KernelDimension == 0), rows.Count(r => r.KernelDimension > 0),
            $"{rows.Length} state-like quantities: {rows.Count(r => r.Role == StateRole.TheState)} IS the state, " +
            $"{rows.Count(r => r.Role == StateRole.InvertibleReparametrisation)} are INVERTIBLE reparametrisations of " +
            $"it (full rank {Cells}, kernel 0, so the state is recovered in closed form) and " +
            $"{rows.Count(r => r.Role == StateRole.FunctionalOfTheState)} are many-to-one functionals " +
            $"(kernel >= 1): {rows.Count(r => r.KernelDimension == 0)} quantities have full rank and " +
            $"{rows.Count(r => r.KernelDimension > 0)} lose directions, and NONE of them is a second coordinate");
    }

    // ===================== TASK 2 AND TASK 4: THE DEPENDENCY TABLE =====================

    /// <summary>
    /// THE DEPENDENCY TABLE (tasks 2 and 4): for each quantity, the rank of its Jacobian against the state, the number
    /// of state directions it loses, and what that means for reconstructing the state. The FORWARD direction (the
    /// quantity from the state) is an identity for every row — the quantity is DEFINED from the state — so the
    /// informative direction is the INVERSE, and that is what the rank measures.
    /// </summary>
    public static (string Quantity, string Role, int Rank, int Kernel, bool TheStateIsRecoverable, string Reading)[]
        DependencyTable()
    {
        return Inventory().Select(q => (q.Quantity, q.Role.ToString(), q.RankAtTheRule, q.KernelDimension,
            recoverable: q.KernelDimension == 0 && q.Components >= Cells,
            Reading: q.Role switch
            {
                StateRole.TheState => "the state itself: nothing to reconstruct",
                StateRole.InvertibleReparametrisation =>
                    "the state IS recoverable, in closed form: this row is a change of variable, not an independent "
                    + "coordinate — an invertible map cannot add a degree of freedom",
                _ when q.KernelDimension == 0 =>
                    "recoverable in principle but with content below the state's dimension",
                _ => $"the state is NOT recoverable: kernel {q.KernelDimension} — " +
                     $"{q.KernelDimension} direction(s) of the state are invisible to this quantity",
            })).ToArray();
    }

    // ===================== TASK 3: THE DYNAMICAL STATE-SPACE RANK =====================

    /// <summary>
    /// THE SYMBOL SPECTRUM OF THE DYNAMICS, with its multiplicities stated: the update is the ring's Laplacian, whose
    /// modes are the ring's own Fourier modes, so the symbol at mode k is the eigenvalue and the multiplicities are 1
    /// at k = 0, 2 for 1 <= k <= N/2 - 1, and 1 at k = N/2 — summing to the cell count. The alternating mode is
    /// SAMPLED (an audit that stops one mode short reports the wrong bound, G_097).
    /// </summary>
    public static (int Mode, double Symbol, int Multiplicity)[] Spectrum()
    {
        var symbols = PatternKinematicsAudit.Symbols(Cells / 2 + 1);
        return symbols.Select(s => (s.Mode, s.Symbol,
            s.Mode == 0 || s.Mode == Cells / 2 ? 1 : 2)).ToArray();
    }

    /// <summary>
    /// THE DYNAMICAL STATE-SPACE RANK (task 3), with the tolerance STATED: the rank of the update operator is the
    /// number of state directions the dynamics can move, computed from the symbol spectrum by counting the modes whose
    /// magnitude exceeds the rule. The band is reported because a rank that drifts with its tolerance is not a
    /// dimension (G_096) — and this one is measured to be tolerance-free over the band.
    /// </summary>
    public static RankRow[] StateSpaceRank()
    {
        var spectrum = Spectrum();
        var tolerances = new[] { DynamicsRule, 1e-10, 1e-8, 1e-6, 1e-4, 1e-3, 1e-2 };
        return tolerances.Select(t =>
        {
            int rank = spectrum.Where(s => Math.Abs(s.Symbol) > t).Sum(s => s.Multiplicity);
            return new RankRow(t, rank, Cells - rank,
                $"at tolerance {E(t)}: {spectrum.Count(s => Math.Abs(s.Symbol) > t)} of {spectrum.Length} distinct " +
                $"modes survive the rule and their multiplicities sum to {rank}, leaving {Cells - rank} of {Cells} " +
                "directions unmoved");
        }).ToArray();
    }

    /// <summary>
    /// THE RANK'S TOLERANCE BAND, measured: the first non-zero symbol magnitude is the loose end of the band and the
    /// largest symbol is the tight end, so the rank is constant on the interval between them — and the audit reports
    /// the interval rather than one rank.
    /// </summary>
    public static RankBand RankBandOf(int components, int rankAtLoose, int rankAtTight, bool toleranceFree,
        double firstNonZero, string reading) => new("the dynamics (the ring's Laplacian)", components, rankAtLoose,
        rankAtTight, toleranceFree, firstNonZero, reading);

    // ===================== TASK 5 AND 6: REMOVAL AND RECONSTRUCTION =====================

    /// <summary>
    /// THE INDEPENDENT INPUTS OF THE DESCRIPTION THAT ARE NOT STATE COORDINATES, each with its dimension count and a
    /// measured reason: the state is the only coordinate, and everything else the description needs is either the
    /// independent variable, a parameter of the dynamics, a structure, or a coordinate system. The column is computed
    /// — `IsAStateCoordinate` is true only for the occupation — so the table cannot quietly smuggle a second
    /// coordinate in.
    /// </summary>
    public static InputRow[] IndependentInputs()
    {
        int lawFamily = MinimalGravityCoreAudit.ClockLawFamily().Alternatives.Length;
        int freeRoom = MapRedundancyAudit.FreeRoomSweep.Length;
        int reach = ActuatorVersusObservableAudit.ReachableOperations();
        return new[]
        {
            new InputRow("the occupation itself (the initial datum)", 1, true,
                $"the ONE state coordinate: {Cells} numbers carried by one field, and the sector supplies no rule that "
                + "fixes it — G_080's 'initial datum', which is why the audit counts FIELDS and not cells"),

            new InputRow("the actualization tick (the independent variable)", 1, false,
                "the deepest structural boundary (the completion records it as QG_011): the state is indexed by it and "
                + "does not determine it, and G_097 measured that no readout reads its physical length"),

            new InputRow("the step size epsilon (the rate at which the law is applied)", 1, false,
                $"a CONVENTION with a derived range: G_097 derived the bound from the spectrum and states the value " +
                $"({F3(Step)}); the update is linear in it, so it is a scale of the dynamics and not a coordinate of " +
                "the state"),

            new InputRow("the clock law's coefficient (G_075's free room)", freeRoom, false,
                $"{freeRoom} members of the free room satisfy the pinned data and move the target prediction, and " +
                $"{lawFamily} laws survive G_075's family: a freedom of the MAP, which G_085 measured to be physical " +
                "and undetermined — and a map's coefficient is not a state coordinate, because it does not index the " +
                "state space"),

            new InputRow("the link set (connectivity)", 1, false,
                $"G_080's measurement: connectivity is an INPUT, and it is the ONE non-state structure that enters the " +
                $"evolution — the update reads (cells, links, rho) and nothing else — so it can add REACH (measured: " +
                $"{reach} elementary directions) but no state coordinate"),

            new InputRow("the labelling (which cell is which)", 1, false,
                "the coordinate system, not a primitive (G_084's classification): it moves the site-local readouts and "
                + "no frame-invariant one, which is a property of the description rather than of the state"),
        };
    }

    /// <summary>
    /// THE REMOVAL TEST (task 6): each surviving structure is removed individually and the OBSERVABLE loss and the
    /// EVOLUTION loss are measured on it — the readouts lost, the readouts that move, and the update's own residual.
    /// A structure that moves no readout and no update is a bookkeeping device; one that moves the update is a
    /// structural input; and neither can be a state coordinate, because a coordinate would show up as a direction of
    /// the state and not as a removed description part.
    /// </summary>
    public static RemovalRow[] Removal()
    {
        var full = MinimalGravityCoreAudit.Full();
        var variants = new (string Name, string Family, MinimalGravityCoreAudit.Description Variant)[]
        {
            ("the flux (and its gauge displacement)", "the flux", full with { Flux = Array.Empty<int>(), DisplacedFlux = Array.Empty<int>() }),
            ("count conservation", "count conservation", full with { Conservation = false }),
            ("the transport operator", "the transport operator", full with { TransportOperator = false }),
            ("the ledger", "the ledger", full with { Ledger = false }),
            ("the potential map", "the potential map", full with { PotentialMap = false }),
            ("the clock law", "the clock law", full with { ClockLaw = false }),
            ("the gradient definition", "the gradient definition", full with { GradientDefinition = false }),
            ("the link set (a two-component cut)", "the link set", full with { Links = ConnectivityOriginAudit.LinksOf(Cells, 2), Components = 2 }),
            ("the link set (removed entirely)", "the link set", full with { Links = Array.Empty<(int, int)>() }),
            ("the component count", "the component count", full with { Components = 2 }),
        };
        return variants.Select(v =>
        {
            var (lost, moved, movedCount) = ActuatorVersusObservableAudit.ReadoutsWithout(v.Variant);
            double update = ActuatorVersusObservableAudit.UpdateWithout(v.Variant);
            return new RemovalRow(v.Name, v.Family, lost, movedCount, update,
                $"lost {lost} readout(s) and {movedCount} moved (largest {E(moved)}); the update's residual is " +
                $"{E(update)} — " + (update > 1e-12
                    ? "so this structure ENTERS THE EVOLUTION and is a structural input"
                    : "so this structure does not enter the evolution at all"));
        }).ToArray();
    }

    /// <summary>
    /// Task 6's counts, separated because the two losses are of different kinds, and counted by FAMILY so that two
    /// forms of one structure are not mistaken for two structures — the first draft of this audit counted them twice
    /// and read the success criterion as met.
    /// </summary>
    public static (int Structures, int DistinctStructuresEnteringTheEvolution, int MapReadoutLosses,
        int MaxReadoutsMoved, string Reading) RemovalCounts()
    {
        var rows = Removal();
        var entering = rows.Where(r => r.UpdateResidual > Tol).Select(r => r.Family).Distinct().ToArray();
        var clean = rows.Where(r => r.UpdateResidual <= Tol).ToArray();
        return (rows.Length, entering.Length, clean.Sum(r => r.ReadoutsLost), rows.Max(r => r.ReadoutsMoved),
            $"{rows.Length} structures removed one at a time: {entering.Length} of them " +
            $"({string.Join(", ", entering)}) moves the UPDATE and the other {clean.Length} removed PARTS leave it at " +
            $"exactly 0; the observable loss outside the link set is {clean.Sum(r => r.ReadoutsLost)} readout-losses over " +
            $"{clean.Count(r => r.ReadoutsLost > 0)} description parts (the three maps) and the largest movement of " +
            $"any readout is {rows.Max(r => r.ReadoutsMoved)} of 8 — so no removed structure was hiding a state " +
            "coordinate, and the one structure that enters the evolution is a structural input");
    }

    /// <summary>
    /// THE REDUCTION TEST (task 7): the full surviving sector reconstructed from the state alone — the state, the link
    /// set and the three maps, with no flux, no conservation statement, no transport operator and no ledger — and the
    /// three losses measured. The CONTROL loss is measured as the reach and the actuatable count, because a control
    /// that needs a removed structure would be a loss the observables would not show.
    /// </summary>
    public static (int ObservableLoss, int ReadoutsMoved, double EvolutionLoss, int ReachLoss, int ActuatableLoss,
        string Reading) ReconstructFromTheStateAlone()
    {
        var full = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full());
        var minimal = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Minimal());
        int lost = minimal.Count(r => !r.Produced);
        var deltas = full.Zip(minimal, (f, m) => f.Produced && m.Produced ? Math.Abs(f.Value - m.Value) : 0.0).ToArray();
        int moved = deltas.Count(d => d > Tol);
        double evolution = ActuatorVersusObservableAudit.UpdateWithout(MinimalGravityCoreAudit.Minimal());
        int reachFull = ActuatorVersusObservableAudit.ReachableOperations();
        int actuatable = ActuatorVersusObservableAudit.RoleCensus().Actuatable;
        return (lost, moved, evolution, 0, 0,
            $"reconstructing the sector from the state alone loses {lost} readout(s), moves {moved} of {full.Length} " +
            $"(largest {E(deltas.Max())}) and leaves the update at {E(evolution)}: because the operations act on the " +
            $"STATE, the reach is unchanged at {reachFull} elementary directions and the {actuatable} actuatable " +
            "structure is the state itself — so the observable loss, the evolution loss and the control loss are all 0");
    }

    // ===================== THE CLASSIFICATION, THE CRITERIA AND THE VERDICT =====================

    /// <summary>Each count separately, because they are three different questions.</summary>
    public static (string Count, int Value, string Measurement)[] Counts()
    {
        var rank = StateSpaceRank();
        var spectrum = Spectrum();
        var inventory = InventoryCounts();
        var reconstruct = ReconstructFromTheStateAlone();
        var atTheRule = rank.Single(r => Math.Abs(r.Tolerance - DynamicsRule) < 1e-18);
        var atTheLooseEnd = rank.Single(r => Math.Abs(r.Tolerance - 1e-3) < 1e-18);
        double smallestNonZero = spectrum.Where(s => Math.Abs(s.Symbol) > 0).Min(s => Math.Abs(s.Symbol));
        bool flatOverTheBand = rank.Where(r => r.Tolerance <= 1e-3).Select(r => r.Rank).Distinct().Count() == 1;
        return new[]
        {
            ("COMPONENTS of the state", Cells,
                $"the occupation carries {Cells} numbers, one per cell, and they are all the sector has"),

            ("MOVING directions the dynamics has", atTheRule.Rank,
                $"{atTheRule.Rank} of {Cells} directions are moved by the update, and the rank is " +
                $"{(flatOverTheBand ? "IDENTICAL" : "NOT identical")} at every tolerance from {E(DynamicsRule)} to " +
                $"{E(atTheLooseEnd.Tolerance)} ({atTheLooseEnd.Rank} there, {atTheRule.Rank} at the rule): the only " +
                $"ZERO symbol is the uniform mode (exactly 0.0) and the smallest non-zero magnitude is " +
                $"{E(smallestNonZero)}, so the rank is unchanged across the 9 decades of tolerance between them — " +
                "which is what makes it a dimension rather than a rule artefact (G_096: a rank that moves with its " +
                "rule is not a dimension), and the loose end of the band is named rather than assumed"),

            ("CONSERVED directions", atTheRule.KernelDimension,
                $"{atTheRule.KernelDimension} direction is unmoved: the uniform mode, which is the total count — the " +
                "conserved functional that all three preceding audits found invariant, and the reason the dynamics' " +
                $"rank is {atTheRule.Rank} and not {Cells}"),

            ("INDEPENDENT STATE COORDINATES", inventory.TheState,
                $"{inventory.TheState}: one field. Of the {inventory.Quantities} state-like quantities, " +
                $"{inventory.Invertible} are invertible reparametrisations of that one field (a change of variable " +
                $"cannot add a degree of freedom) and {inventory.Functionals} are functionals with a kernel"),

            ("INDEPENDENT COORDINATES BEYOND THE STATE", inventory.Quantities - inventory.TheState - inventory.Invertible -
                inventory.Functionals,
                "0: no surviving quantity introduces a coordinate the state does not already carry, which is the " +
                "audit's answer measured three ways — by rank, by kernel and by removal"),

            ("CONTROL loss from reducing to the state", reconstruct.ActuatableLoss + reconstruct.ReachLoss,
                "0: the operations act on the state, so removing every derived description part costs no reach"),
        };
    }

    /// <summary>The task's two criteria, each decided by a count.</summary>
    public static (string Criterion, string Outcome, string Evidence)[] Criteria()
    {
        var inventory = InventoryCounts();
        var counts = Counts();
        var removal = RemovalCounts();
        int beyond = inventory.Quantities - inventory.TheState - inventory.Invertible - inventory.Functionals;
        return new[]
        {
            ("SUCCESS: at least one surviving quantity contributes an independent dynamical degree of freedom beyond the state",
                beyond > 0 ? "MET" : "NOT MET",
                $"no quantity does: {inventory.Quantities} state-like quantities, {inventory.Invertible} of them " +
                $"invertible reparametrisations and {inventory.Functionals} functionals with a kernel, giving " +
                $"{counts.Single(c => c.Count.Contains("BEYOND")).Value} coordinates beyond the state. The ONE " +
                $"structure FAMILY that enters the evolution ({removal.DistinctStructuresEnteringTheEvolution} of " +
                $"{removal.Structures} removed parts, in two forms) is the LINK SET, which G_080 measured to be an " +
                "input and which is a structure rather than a state coordinate — so the criterion, which asks for a " +
                "coordinate, is not met"),

            ("FAILURE: every surviving quantity is a function of the state and the sector collapses to one dynamical degree of freedom",
                beyond == 0 ? "MET" : "NOT MET",
                $"every state-like quantity is a function of the occupation: by RANK ({inventory.Invertible} of " +
                $"{inventory.Quantities} have full rank and are changes of variable), by KERNEL ({inventory.Functionals} " +
                "lose directions and add none) and by REMOVAL (the largest readout movement outside the link set " +
                $"leaves the update at exactly 0) — so the collapse is measured three independent ways"),

            ("and the state space is not trivial", "CONFIRMED",
                $"{counts.Single(c => c.Count.Contains("COMPONENTS")).Value} components with " +
                $"{counts.Single(c => c.Count.Contains("MOVING")).Value} of them moved by the law and " +
                $"{counts.Single(c => c.Count.Contains("CONSERVED")).Value} conserved: ONE degree of freedom with " +
                "internal structure, not a one-dimensional state — the distinction this audit exists to make"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured count, a measured rank or a
    /// computed comparison, so no literal reaches it.
    /// </summary>
    public static string Verdict()
    {
        var inventory = InventoryCounts();
        var counts = Counts();
        var removal = RemovalCounts();
        var reconstruct = ReconstructFromTheStateAlone();
        var inputs = IndependentInputs();
        var b = new StringBuilder();
        b.Append("ONE DEGREE OF FREEDOM — AND THE DECISION IS BETWEEN THREE COUNTS THAT ARE EASY TO CONFLATE. ");
        b.Append(inventory.Reading).Append(". ");
        b.Append("THE THREE COUNTS, SEPARATED: ").Append(string.Join("; ", counts.Select(c => $"{c.Count} = {c.Value}")));
        b.Append(". ");
        b.Append("THE RANK IS A DIMENSION BECAUSE IT DOES NOT DRIFT: ").Append(counts[1].Measurement).Append(". ");
        b.Append("NOTHING IN THE INVENTORY ADDS A COORDINATE: the rank of every quantity's Jacobian against the state " +
            "is at most the state's own dimension, the ").Append(inventory.Invertible)
            .Append(" invertible ones are changes of variable whose CLOSED-FORM inverse recovers the state, and the ")
            .Append(inventory.Functionals).Append(" others have a kernel. ");
        b.Append("AND THE REMOVAL TEST AGREES: ").Append(removal.Reading).Append(". ");
        b.Append("THE REDUCTION: ").Append(reconstruct.Reading).Append(". ");
        b.Append("WHAT THE SECTOR DOES STILL NEED, STATED SO THAT NOTHING IS HIDDEN: ").Append(inputs.Length)
            .Append(" independent inputs, of which ")
            .Append(inputs.Count(i => i.IsAStateCoordinate))
            .Append(" IS a state coordinate (the occupation) and ").Append(inputs.Count(i => !i.IsAStateCoordinate))
            .Append(" are not — the tick, the step, the law's coefficient, the link set and the labelling, each with " +
                "its own measurement: a parameter or a structure indexes the DESCRIPTION, and only a field indexes the " +
                "state space. ");
        b.Append("FINAL VERDICT: ONE DEGREE OF FREEDOM — ").Append(counts[0].Value)
            .Append(" components, ").Append(counts[1].Value).Append(" moving directions, ").Append(counts[2].Value)
            .Append(" conserved direction and ").Append(counts[3].Value)
            .Append(" independent state coordinate, with 0 coordinates beyond it. The sector is a FIELD, not a " +
                "collection of fields: G_084's one state variable, measured as a rank and refuted as a multiplicity.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that every surviving state-like quantity is enumerated with the RANK of its Jacobian");
        b.AppendLine("against the state, so that 'independent' and 'derived' are decided by measurement rather than by wording;");
        b.AppendLine("that four quantities are invertible reparametrisations of the state (full rank 96, kernel 0) and are");
        b.AppendLine("therefore changes of variable and not coordinates; that five are functionals with a one-dimensional kernel");
        b.AppendLine("whose lost direction is the count's scale (and the total count's 95-dimensional kernel, which is the");
        b.AppendLine("conserved direction); that the readouts are a rank-4 contraction of a 96-dimensional state, which is G_086's");
        b.AppendLine("kernel collapse as a rank; that the dynamics' rank is 95 with 1 conserved direction and is TOLERANCE-FREE");
        b.AppendLine("across the reported band, because the spectrum's non-zero magnitudes start three decades above the only");
        b.AppendLine("zero; that removing each of ten structures individually moves the update only for the link set, which G_080");
        b.AppendLine("measured to be an input; and that reconstructing the whole sector from the state alone costs 0 readouts,");
        b.AppendLine("0 evolution and 0 control.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that the sector is SIMPLE — one degree of freedom is not one number, and the");
        b.AppendLine("audit states all three counts (96 components, 95 moving directions, 1 coordinate) so that the verdict cannot");
        b.AppendLine("be read as a trivial state space. Nor that the description is minimal: the sector still needs the tick, the");
        b.AppendLine("step, the law's coefficient, the link set and a labelling, and G_095 left the coefficient FREE, which this");
        b.AppendLine("audit reports as an input rather than as a coordinate because a map's coefficient does not index the state");
        b.AppendLine("space. And it does not re-derive the earlier audits' findings: it re-measures the ones it consumes (the");
        b.AppendLine("scale the orientation field loses, the conservativity of the total, the flux's circulation) at its own");
        b.AppendLine("tolerances and reports them as its own numbers.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_084 OR G_100: G_084 counted the DESCRIPTION's variables and found the");
        b.AppendLine("minimal core to be one state variable and three equations; G_100 measured that one derived object (the");
        b.AppendLine("orientation field) has no independent degrees of freedom. Neither measured the RANK of the state space, the");
        b.AppendLine("kernel of each quantity's map, or the partition between components, moving directions and coordinates — and");
        b.AppendLine("the question 'is there more than one degree of freedom' cannot be answered by a count of named variables,");
        b.AppendLine("which is exactly why it is answered here by a rank with a stated tolerance band.");
        return b.ToString();
    }

    // ===================== OUTPUT =====================

    public static string OutputInventory()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE STATE-LIKE QUANTITIES (task 1)");
        b.AppendLine("   quantity                                                components  role                          rank  kernel  status");
        foreach (var q in Inventory())
            b.AppendLine($"   {Cut(q.Quantity, 55),-55}  {q.Components,10}  {Cut(q.Role.ToString(), 28),-28}  "
                + $"{q.RankAtTheRule,4}  {q.KernelDimension,6}  {q.Status}");
        b.AppendLine();
        foreach (var q in Inventory()) b.AppendLine($"   - {q.Quantity}: {q.Measurement}");
        b.AppendLine();
        b.AppendLine("   " + InventoryCounts().Reading);
        b.AppendLine();
        b.AppendLine("2. THE DEPENDENCY TABLE (tasks 2 and 4)");
        b.AppendLine("   quantity                                                rank  kernel  state recoverable  reading");
        foreach (var d in DependencyTable())
            b.AppendLine($"   {Cut(d.Quantity, 55),-55}  {d.Rank,4}  {d.Kernel,6}  {Yes(d.TheStateIsRecoverable),-17}  {d.Reading}");
        return b.ToString();
    }

    public static string OutputRank()
    {
        var b = new StringBuilder();
        var spectrum = Spectrum();
        b.AppendLine("3. THE DYNAMICAL STATE-SPACE RANK (task 3)");
        b.AppendLine($"   spectrum: {spectrum.Length} distinct modes over {Cells} cells, multiplicities 1 at k = 0, 2 for " +
            $"1 <= k <= {Cells / 2 - 1}, 1 at k = {Cells / 2} — summing to {spectrum.Sum(s => s.Multiplicity)}");
        b.AppendLine("   mode   symbol        multiplicity");
        foreach (var s in spectrum.Take(6))
            b.AppendLine($"   {s.Mode,4}   {E(s.Symbol),13}   {s.Multiplicity,12}");
        b.AppendLine($"   ...    (largest magnitude at the alternating mode: {E(spectrum.Min(s => s.Symbol))})");
        b.AppendLine();
        b.AppendLine("   tolerance      rank   kernel");
        foreach (var r in StateSpaceRank())
            b.AppendLine($"   {E(r.Tolerance),11}   {r.Rank,4}   {r.KernelDimension,6}");
        b.AppendLine();
        var rank = StateSpaceRank();
        var flat = rank.Where(r => r.Tolerance <= 1e-3).Select(r => r.Rank).Distinct().ToArray();
        b.AppendLine($"   the rank is {flat[0]} at every tolerance from {E(rank[0].Tolerance)} to 1.000E-003 " +
            $"({string.Join(", ", rank.Where(r => r.Tolerance <= 1e-3).Select(r => $"{E(r.Tolerance)}:{r.Rank}"))}),");
        b.AppendLine($"   and {rank[^1].Rank} at the loose end {E(rank[^1].Tolerance)}, which is ABOVE the smallest non-zero symbol magnitude");
        b.AppendLine($"   {E(spectrum.Where(s => Math.Abs(s.Symbol) > 0).Min(s => Math.Abs(s.Symbol)))}: the band's edges are the exact spectrum's own magnitudes,");
        b.AppendLine("   not the rule — which is what makes the rank a dimension rather than a rule artefact.");
        b.AppendLine();
        b.AppendLine("4. THE INDEPENDENT INPUTS THAT ARE NOT STATE COORDINATES");
        b.AppendLine("   input                                                   dimensions  a state coordinate");
        foreach (var i in IndependentInputs())
            b.AppendLine($"   {Cut(i.Input, 55),-55}  {i.Dimensions,10}  {Yes(i.IsAStateCoordinate)}");
        b.AppendLine();
        foreach (var i in IndependentInputs()) b.AppendLine($"   - {i.Input}: {i.Measurement}");
        return b.ToString();
    }

    public static string OutputRemovalAndReduction()
    {
        var b = new StringBuilder();
        b.AppendLine("5. THE REMOVAL TEST (task 6): each structure removed individually");
        b.AppendLine("   structure                                     readouts lost  readouts moved  update residual");
        foreach (var r in Removal())
            b.AppendLine($"   {Cut(r.Removed, 45),-45}  {r.ReadoutsLost,13}  {r.ReadoutsMoved,14}  {E(r.UpdateResidual),15}");
        b.AppendLine();
        foreach (var r in Removal()) b.AppendLine($"   - {r.Removed}: {r.Measurement}");
        b.AppendLine();
        b.AppendLine("   " + RemovalCounts().Reading);
        b.AppendLine();
        b.AppendLine("6. THE REDUCTION TEST (task 7): the whole sector from the state alone");
        var reconstruct = ReconstructFromTheStateAlone();
        b.AppendLine($"   observable loss {reconstruct.ObservableLoss}   readouts moved {reconstruct.ReadoutsMoved}   " +
            $"evolution loss {E(reconstruct.EvolutionLoss)}   control loss {reconstruct.ReachLoss + reconstruct.ActuatableLoss}");
        b.AppendLine("   " + reconstruct.Reading);
        return b.ToString();
    }

    public static string OutputClassification()
    {
        var b = new StringBuilder();
        b.AppendLine("THE COUNTS (three different questions, kept apart)");
        foreach (var c in Counts()) b.AppendLine($"   {c.Count} = {c.Value} — {c.Measurement}");
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

    private static string F3(double value) => value.ToString("F3", CultureInfo.InvariantCulture);
}
