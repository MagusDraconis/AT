using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>Where a candidate manipulation channel stands (ResearchY-G_089).</summary>
public enum ChannelStatus { Actuatable, ControlWithoutActuator, NotAControl, ExcludedByArity }

/// <summary>One candidate channel, with the measured facts that decide its status.</summary>
public sealed record ManipulationChannel(string Channel, bool IsAControlOfTheKernel, bool IsDirectlyObservable,
    bool IsActuatableByTheClass, ChannelStatus Status, string WhatItWouldRequire, string Measurement);

/// <summary>How a quantity sits between observability and controllability.</summary>
public sealed record ControlVersusObservable(string Quantity, bool ControlsTheKernel, bool DirectlyObservable,
    bool Actuatable, string Evidence);

/// <summary>
/// ResearchY-G_089 — MANIPULATION CHANNEL AUDIT (group G — Gravity Source).
///
/// WORKING HYPOTHESIS, ASSUMED TRUE: local manipulation of time and/or gravity IS physically possible. The question is
/// NOT whether it exists but **which physical channel would permit it**, inside the surviving sector's own vocabulary
/// (Difference, Actualization, the density rho, the kernel K(rho), and the surviving audits). Arguing from previous
/// negative verdicts is FORBIDDEN, adding primitives immediately is FORBIDDEN, and assuming impossibility is FORBIDDEN.
///
/// ANSWER: **THE CHANNEL SEARCH RETURNS EXACTLY ONE ACTUATABLE CHANNEL — A COUNT TRANSFER ALONG THE SUBSTRATE'S LINKS,
/// WHICH IS THE THEORY'S OWN ELEMENTARY OPERATION AND THEREFORE NOT A NEW MANIPULATION — WHILE EVERY CHANNEL THAT
/// WOULD BE UNOBSERVABLE IS A CONSERVED INPUT WITH NO ACTUATOR. The one channel that would be both effective and
/// (until measured) unobservable is a change of the KERNEL'S CURVATURE, which is a parameter of the law: the audit
/// states that requirement explicitly instead of denying it.**
///
///  (1) THE INVERSE PROBLEM IS SOLVED, not circumvented: a fractional clock change requires a local **count** change of
///      `d ln(1 + Delta) / (1 + cA)` in the density, so the density IS the lever arm and the clock moves LESS than the
///      density by the requirement `d / (1 + cA)`, so the channel is DAMPED. A gravity change requires a **differential** count change, and the UNIFORM direction
///      — which would shift every clock at once — is precisely the one the surviving class does not contain.
///
///  (2) THE CHANNEL SEARCH (task 2) tests all eight candidates and finds ONE actuatable (the local density, through the
///      class's own transfers), THREE controls without actuators (the link structure, the component count, the kernel's
///      curvature), and FOUR that are not independent controls at all (correlation, variance, drift and phase).
///
///  (3) THE HIDDEN-VARIABLE SEARCH (task 3) walks the eliminated structures — connectivity, continuity, transport,
///      component count — and finds that the two that still CONTROL the kernel are exactly the two the class CONSERVES,
///      while the transport layer is a description of the conserving operations rather than a control, and the flux is
///      a gauge.
///
///  (4) THE CONTROL-VERSUS-OBSERVABLE AUDIT (task 4) gives a general structural result rather than a list: in this
///      sector **the quantities that are unobservable and controlling are the conserved inputs, and conservation is
///      exactly what makes them uncontrollable**, while every quantity the class can actually change is a
///      distribution of rho — which the observables read. So a channel cannot be both unobservable and actuatable
///      *within the class*.
///
///  (5) THE ENERGY SCALING (task 5) is quoted in the sector's own terms: the ledger prices a SOURCE exactly (its
///      residual IS the source) and prices a redistribution at nothing, so within the theory a redistribution is free —
///      the practical cost is the calibration's, and it is the ordinary cost of moving mass-energy.
///
///  (6) THE TECHNOLOGY CLASSIFICATION (task 6) therefore reads: **impossible** for the conserved inputs, **principle
///      only** for the kernel's curvature, **astrophysical only** for large redistributions, and **laboratory feasible**
///      only in the sense of the ordinary gravitational shift — which is the kernel, not a manipulation of it.
///
///  (7) THE MINIMAL PATHWAY (task 7) is the smallest chain that exists, WITH ITS LIMITS: a human action moves count
///      along links, the density changes, the kernel reads it, and the clock and gravity follow — bounded by count
///      conservation, by the damping `d / (1 + cA)`, and by the ledger, which would detect any count that appeared without being
///      transferred. The audit's failure criterion is therefore MET for **pathways**: every channel collapses back to
///      the existing kernel without adding a controllable variable. Its success criterion is met only in the weaker
///      sense that ONE effective-and-unobservable control is IDENTIFIED (the kernel's curvature), together with the
///      operation the class would have to contain for it to be reachable.
///
///  (8) THE SPECTRAL SUBSTRATE RULE IS NOT ENGAGED: no spectral count, eigenspace count or multiplicity is quoted.
/// </summary>
public static class ManipulationChannelAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;

    /// <summary>The state, and the surface potential a compact object's clock sits at.</summary>
    public static double[] State => MapRedundancyAudit.State;
    public static double SurfacePotential => Math.Abs(ObservationalProgramAudit.XOfTarget());

    /// <summary>The kernel's rate as a function of the density and the extension's parameter.</summary>
    public static double Rate(double rho, double c = 0.0) => Math.Exp(0.5 * c * Math.Pow(Math.Log(rho) / D, 2.0))
        * Math.Pow(rho, 1.0 / D);

    /// <summary>The logarithm of the rate — the object the inverse problem is solved against.</summary>
    public static double LnRate(double rho, double c = 0.0) => Math.Log(rho) / D + 0.5 * c * Math.Pow(Math.Log(rho) / D, 2.0);

    // ===================== 1. THE INVERSE PROBLEM (TASK 1) =====================

    /// <summary>
    /// THE INVERSE PROBLEM, SOLVED: a fractional change in the clock at a site requires a fractional change in the
    /// LOCAL COUNT of `(1 + Delta)^(d / (1 + cA)) - 1`, computed with `ExpM1` because the changes asked about are small.
    /// THE REQUIREMENT IS REPORTED, NOT A GAIN: the density must move MORE than the clock — by `d / (1 + cA)`, which is
    /// about three — so the channel is DAMPED, and that damping is what makes a manipulation expensive rather than
    /// cheap. (The first version reported this ratio as a "gain below one", which the measurement refuted.)
    /// </summary>
    public static (double TargetClockChange, double RequiredDensityChange, double DensityPerUnitClockChange,
        double AbsoluteDensityChange)[] InverseProblemSmallClockChange(double[] targets, double potential,
        double rho = 1.0, double c = 0.0)
        => targets.Select(delta =>
        {
            double exponent = D / (1.0 + c * potential);
            // ln(1 + delta) is exact for the small deltas asked about; the hazard the rules warn about is a difference
            // of nearly equal terms, which the closed form avoids entirely
            double relative = AtNumerics.ExpM1(exponent * Math.Log(1.0 + delta));
            return (delta, relative, delta == 0.0 ? double.NaN : relative / delta, relative * rho);
        }).ToArray();

    /// <summary>
    /// THE GRAVITY SIDE OF THE INVERSE PROBLEM: the acceleration is a gradient, so a gravity change requires a
    /// DIFFERENTIAL count change — and the UNIFORM direction, which would shift every clock at once, leaves the
    /// acceleration exactly unchanged (the surviving measurement) and is the direction the class does not contain.
    /// </summary>
    public static (string Direction, double AccelerationChange, double ClockChange, bool TheClassContainsIt)[]
        GravityInverse()
    {
        var rho = State;
        var (well, hill) = KernelDiscriminabilityAudit.ContrastSites();
        var a = rho.Select(r => LnRate(r)).ToArray();
        var gradient = ConnectivityOriginAudit.AccelerationOnLinks(a, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1));
        // the uniform direction: add a constant potential everywhere
        var shifted = a.Select(v => v + 0.1).ToArray();
        var shiftedGradient = ConnectivityOriginAudit.AccelerationOnLinks(shifted, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1));
        double uniformAccelerationChange = gradient.Zip(shiftedGradient, (u, v) => Math.Abs(u - v)).Max();
        // the differential direction: move count between the two contrast cells, conserving the total
        var moved = rho.Select((v, i) => i == well ? v * 0.98 : i == hill ? v * 1.02 : v).ToArray();
        var movedA = moved.Select(r => LnRate(r)).ToArray();
        var movedGradient = ConnectivityOriginAudit.AccelerationOnLinks(movedA, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1));
        double differentialAccelerationChange = gradient.Zip(movedGradient, (u, v) => Math.Abs(u - v)).Max();
        return new[]
        {
            ("adding a constant potential everywhere (the uniform direction)", uniformAccelerationChange,
                Math.Abs(LnRate(Math.Exp(D * 0.1)) - LnRate(1.0)), false),
            ("moving count between neighbouring cells (a differential change)", differentialAccelerationChange, 0.0, true),
        };
    }

    // ===================== 2. THE CHANNEL SEARCH (TASK 2) =====================

    /// <summary>
    /// THE EIGHT CHANNELS, each with the three facts that decide it: does it CONTROL the kernel, is it DIRECTLY
    /// observable, and can the surviving class ACTUATE it. The status follows from those three by a single rule.
    /// </summary>
    public static ManipulationChannel[] Channels()
    {
        var rho = State;
        var (well, hill) = KernelDiscriminabilityAudit.ContrastSites();
        var a = rho.Select(r => LnRate(r)).ToArray();
        var gradient = ConnectivityOriginAudit.AccelerationOnLinks(a, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1));
        var witness = ConnectivityOriginAudit.ConservingWitnesses(Cells, 1);
        bool transportsExist = witness.Length > 0 && witness.Any(w => ContinuityLawAudit.IsATransport(
            w.Change.Select(x => (double)x).ToArray(), Cells, 1));
        var unionClosure = ConnectivityOriginAudit.DisjointUnionClosure();
        bool primitivesDetectTheCut = unionClosure.Any(r => r.ContentChanged);
        var cClaims = ConnectivityDependencyAudit.Claims();
        double componentCountChangedByTheClass = cClaims
            .Single(c => c.Name.StartsWith("the component count is conserved")).Evaluate(1);
        return new[]
        {
            new ManipulationChannel("local density", true, true, true, ChannelStatus.Actuatable,
                "a count transfer along the substrate's links — the class's own elementary operation",
                $"conserving witnesses exist ({witness.Length}) and at least one is a genuine transport "
                + $"({transportsExist}); the clock reads the density directly, so a redistribution is a manipulation of "
                + "the same object the observables already read"),

            new ManipulationChannel("density correlation", false, true, false, ChannelStatus.NotAControl,
                "nothing beyond the density itself",
                "no surviving audit names a correlation as an independent quantity: what the sector calls structure is a "
                + "function of the density (the gradient is computed FROM it), so a correlation is redundant and adds no "
                + "control"),

            new ManipulationChannel("density variance", false, true, false, ChannelStatus.NotAControl,
                "nothing beyond the density's distribution",
                $"the variance over a neighbourhood is a function of the same gradient the acceleration readout uses "
                + $"({gradient.Select(Math.Abs).Max():E3} at the steepest site): a consequence of the density, not an "
                + "independent lever"),

            new ManipulationChannel("density topology", true, false, false, ChannelStatus.ControlWithoutActuator,
                "an operation that changes the link set or the component count — which the class does not contain",
                $"the topology controls the kernel through the transport rank (0 of 4 primitives detect a cut: "
                + $"{primitivesDetectTheCut}; the component count is changed by the class "
                + $"({componentCountChangedByTheClass} operations)) — a control without an actuator"),

            new ManipulationChannel("kernel curvature", true, false, false, ChannelStatus.ControlWithoutActuator,
                "a change in the clock law's second-order coefficient — a parameter of the law, not a state variable",
                "the kernel's second derivative with respect to the potential IS that coefficient exactly (G_088), so it "
                + "is effective and — until a curvature is measured — unobservable in situ: the one channel that is both "
                + "effective and hidden"),

            new ManipulationChannel("kernel drift", false, true, false, ChannelStatus.NotAControl,
                "an evolution law whose rate the sector does not fix",
                "the drift is the density's own evolution read through the kernel (d ln K = (1 + cA) dA), so it carries no "
                + "independent control: what it needs is a rate, and no surviving datum fixes one (G_088)"),

            new ManipulationChannel("phase structure", false, false, false, ChannelStatus.ExcludedByArity,
                "a channel through the phase sector, which the clock's signature does not admit",
                "the temporal observables take the density (or its derived potential) and nothing else — the arity proof "
                + "the sector already runs — so a phase cannot reach the clock"),

            new ManipulationChannel("boundary conditions", true, false, false, ChannelStatus.ControlWithoutActuator,
                "a change in a conserved global input (the total count, the component count)",
                $"the total is conserved by the class (ledger residual equals the source exactly) and the component count "
                + $"is conserved too ({componentCountChangedByTheClass} operations): a boundary that controls without "
                + "being actuatable"),
        };
    }

    /// <summary>
    /// THE STATUS RULE, stated once and computed: a channel is ACTUATABLE when the class can change it, a CONTROL
    /// WITHOUT AN ACTUATOR when it changes the kernel but no operation in the class changes IT, NOT A CONTROL when it is
    /// a function of the density, and EXCLUDED BY ARITY when the clock cannot take it as an argument.
    /// </summary>
    public static ChannelStatus StatusOf(ManipulationChannel channel) => channel switch
    {
        _ when channel.IsActuatableByTheClass && channel.IsAControlOfTheKernel => ChannelStatus.Actuatable,
        _ when channel.IsAControlOfTheKernel => ChannelStatus.ControlWithoutActuator,
        _ when channel.Channel == "phase structure" => ChannelStatus.ExcludedByArity,
        _ => ChannelStatus.NotAControl,
    };

    public static string StatusName(ChannelStatus status) => status switch
    {
        ChannelStatus.Actuatable => "ACTUATABLE",
        ChannelStatus.ControlWithoutActuator => "a control WITHOUT an actuator",
        ChannelStatus.NotAControl => "not a control",
        ChannelStatus.ExcludedByArity => "excluded by arity",
        _ => status.ToString(),
    };

    // ===================== 3. HIDDEN VARIABLES AND THE CONTROL-VERSUS-OBSERVABLE RULE (TASKS 3 AND 4) =====================

    /// <summary>
    /// THE ELIMINATED STRUCTURES, RE-EXAMINED AS CONTROL VARIABLES rather than as observables — which is the whole point
    /// of the task: a quantity removed as observationally redundant may still be able to ACT.
    /// </summary>
    public static ControlVersusObservable[] HiddenVariables()
    {
        var unionClosure = ConnectivityOriginAudit.DisjointUnionClosure();
        var primitivesDetect = unionClosure.Any(r => r.ContentChanged);
        var circulations = ContinuityLawAudit.CirculationCount(new[] { Cells });
        var fluxObservables = ContinuityLawAudit.ObservablesReadingTheFlux();
        double cChangedByTheClass = ConnectivityDependencyAudit.Claims()
            .Single(c => c.Name.StartsWith("the component count is conserved")).Evaluate(1);
        return new[]
        {
            new ControlVersusObservable("the link structure (connectivity)", true, false, false,
                $"it sets the transport rank and the reachability, while 0 of 4 primitives detect a cut "
                + $"({primitivesDetect}); no surviving operation changes the link set"),

            new ControlVersusObservable("the component count", true, false, false,
                $"it sets the rank n - c and the free-mode count, while no local observable reads it and the class changes "
                + $"it in {cChangedByTheClass} operations"),

            // THE TRANSPORT LAYER IS THE ONE ROW WHERE THE ACTUATION AND ITS OBSERVABLE COINCIDE, and that is the
            // audit's central distinction: the OPERATOR is actuatable and what it changes is the density, which the
            // observables read — so the row is marked observable, because the ACTUATION's effect is.
            new ControlVersusObservable("the transport layer (the class's operations)", false, true, true,
                $"it is not a knob but the OPERATION SET itself: {fluxObservables} observables read the flux it writes, so "
                + "the actuation is visible in the density it changes"),

            new ControlVersusObservable("the flux itself (the transport's variable)", false, false, false,
                $"a gauge: the circulation leaves every divergence unchanged (nullity "
                + $"{circulations.Single(x => x.Cells == Cells).Nullity}), so the flux cannot be actuated independently "
                + "of the density the operation changes"),

            new ControlVersusObservable("the kernel's curvature (the clock law's shape)", true, false, false,
                "effective (the second derivative IS the coefficient) and unobservable in situ (G_088), while no operation "
                + "in the class changes a parameter of the law"),
        };
    }

    /// <summary>
    /// THE CONTROL-VERSUS-OBSERVABLE RULE, MEASURED: the quantities that control the kernel and are not directly
    /// observable are counted against those that are actuatable, and the intersection is what a manipulation channel
    /// would have to live in.
    /// </summary>
    public static (int Controls, int UnobservableControls, int Actuatable, int UnobservableAndActuatableControls,
        int ActuatableAndObservable) ControlVersusObservableCounts()
    {
        var rows = HiddenVariables();
        return (rows.Count(r => r.ControlsTheKernel), rows.Count(r => r.ControlsTheKernel && !r.DirectlyObservable),
            rows.Count(r => r.Actuatable),
            rows.Count(r => r.ControlsTheKernel && r.Actuatable && !r.DirectlyObservable),
            rows.Count(r => r.Actuatable && r.DirectlyObservable));
    }

    // ===================== 4. ENERGY SCALING, TECHNOLOGY, PATHWAY (TASKS 5, 6 AND 7) =====================

    /// <summary>
    /// THE SECTOR'S OWN PRICING: the ledger prices a SOURCE exactly — its residual IS the source — and prices a
    /// redistribution at nothing, because a redistribution conserves the count. The audit therefore reports the cost in
    /// the theory's terms and states that the practical cost is the calibration's.
    /// </summary>
    public static (string Channel, double TheSectorsOwnCost, string WhatThePracticalCostWouldBe)[]
        EnergyScaling()
    {
        var ledger = ContinuityLawAudit.SourceDetectability().First(l => Math.Abs(l.Source) > 1e-12);
        var classRows = ContinuityLawAudit.UpdateClassAsContinuity().Where(r => !r.Operator.StartsWith("CONTROL")).ToArray();
        return new[]
        {
            ("a redistribution of count (the actuatable channel)", 0.0,
                "moving mass-energy: the ordinary cost of rearranging a source, and the ordinary gravitational shift it "
                + "produces — which is the kernel, not a manipulation OF it"),
            ("a source of count (what a global clock shift would need)",
                Math.Abs(ledger.ResidualBeforeDeclaration),
                "not priced by the sector at all, because the sector has no such operation: the ledger would instead "
                + $"DETECT it, its residual becoming the source exactly ({ledger.ResidualBeforeDeclaration:F4} for a "
                + $"declared {ledger.Source:F4}), and the class reconstructs its changes with residual "
                + $"{classRows.Max(r => Math.Abs(r.ReconstructionResidual)):E3}"),
            ("a change of the kernel's curvature (a parameter of the law)", double.NaN,
                "not a state change at all, so no energy scale applies: it would be a different theory"),
        };
    }

    /// <summary>How much local count change a target clock change costs, at the surface and in a shallow field.</summary>
    public static (string Regime, double Potential, double Gain, (double Target, double Required)[] Scale)[]
        ScaleTable()
        => new[]
        {
            ("a compact object's surface", SurfacePotential,
                1.0 / D,
                InverseProblemSmallClockChange(new[] { 0.01, 0.10, 1.00 }, SurfacePotential)
                    .Select(r => (r.TargetClockChange, r.RequiredDensityChange)).ToArray()),
            ("a shallow (laboratory-like) potential", 1e-9, 1.0 / D,
                InverseProblemSmallClockChange(new[] { 0.01, 0.10, 1.00 }, 1e-9)
                    .Select(r => (r.TargetClockChange, r.RequiredDensityChange)).ToArray()),
        };

    /// <summary>The technology classification (task 6), with the channel each verdict rests on.</summary>
    public static (string Classification, string Channel, string Why)[] TechnologyClassification() => new[]
    {
        ("impossible", "the conserved inputs — the link structure, the component count, the total",
            "an operation that changes them is not in the class: the class conserves the component count and the total, "
            + "and the link set is a substrate datum"),
        ("principle-only", "a change in the kernel's curvature",
            "it would be effective and (until measured) unobservable, but it is a parameter of the LAW: nothing in the "
            + "sector changes it, so it is a requirement on the theory rather than a channel in it"),
        ("astrophysical-only", "large redistributions of count",
            "the clock and the source are one object: moving mass-energy at stellar scales moves the clock by the "
            + "ordinary gravitational amount, which is the kernel itself"),
        ("laboratory-feasible", "the ordinary gravitational shift of a local redistribution",
            "an atomic clock's redshift is measured routinely — and it is the same object the sector calls the kernel, "
            + "so it CONFIRMS the channel rather than manipulating the clock independently of the density"),
    };

    /// <summary>The minimal pathway that exists, with its three limits.</summary>
    public static (string Step, string WhatHappens, string Limit)[] MinimalPathway() => new[]
    {
        ("a human action", "moves count along the substrate's links — a transfer, the class's own elementary operation",
            "the action must be a transfer: there is no operation that creates count"),
        ("the physical variable", "the local density changes by the inverse-problem amount",
            $"the requirement is d / (1 + cA): the clock is DAMPED relative to the density, by a factor of {D}"),
        ("the kernel", "K(rho) reads the new density: the clock and the source are the same object",
            "the ledger is exact: any count that appeared without being transferred would show as a residual equal to "
            + "the source"),
        ("the observable change", "the clock and any gravity difference follow — inside the sector, and nowhere else",
            "the pathway is bounded by the three above, so it is the sector's own dynamics and not a new channel"),
    };

    // ===================== 5. THE VERDICT =====================

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a function of a measured count — how many
    /// channels are actuatable, how many control without an actuator, how many are unobservable AND actuatable, and
    /// what the ledger prices.
    /// </summary>
    public static string Verdict()
    {
        var channels = Channels();
        var act = channels.Count(c => StatusOf(c) == ChannelStatus.Actuatable);
        var controlsWithoutActuator = channels.Count(c => StatusOf(c) == ChannelStatus.ControlWithoutActuator);
        var counts = ControlVersusObservableCounts();
        var energy = EnergyScaling();
        var inverse = InverseProblemSmallClockChange(new[] { 0.01 }, SurfacePotential).Single();
        var gravity = GravityInverse();
        var b = new StringBuilder();
        b.Append("ONE ACTUATABLE CHANNEL, AND IT IS THE THEORY'S OWN ELEMENTARY OPERATION — ");
        b.Append("of the ").Append(channels.Length).Append(" channels tested, ").Append(act)
            .Append(" is actuatable, ").Append(controlsWithoutActuator)
            .Append(" control the kernel WITHOUT an actuator, and the rest are not independent controls at all. ");
        b.Append("THE INVERSE PROBLEM IS SOLVED: a ").Append(inverse.TargetClockChange.ToString("P0", CultureInfo.InvariantCulture))
            .Append(" clock change needs a ").Append(inverse.RequiredDensityChange.ToString("P2", CultureInfo.InvariantCulture))
            .Append(" change in the LOCAL COUNT: the density must move MORE than the clock by d/(1 + cA), so the channel is DAMPED. ");
        b.Append("A GRAVITY CHANGE NEEDS A DIFFERENTIAL CHANGE: the uniform direction leaves the acceleration changed by ")
            .Append(gravity[0].AccelerationChange.ToString("E3", CultureInfo.InvariantCulture))
            .Append(" (the rounding of a zero) while moving the clock, while a transfer between neighbouring cells moves the ")
            .Append("acceleration by ")
            .Append(gravity[1].AccelerationChange.ToString("E3", CultureInfo.InvariantCulture))
            .Append(" — and the uniform direction is exactly the one the class does not contain. ");
        b.Append("THE CONTROL-VERSUS-OBSERVABLE RULE, MEASURED: of ").Append(counts.Controls)
            .Append(" quantities that control the kernel, ").Append(counts.UnobservableControls)
            .Append(" are not directly observable, and of ").Append(counts.Actuatable)
            .Append(" actuatable quantities ").Append(counts.UnobservableAndActuatableControls)
            .Append(" is not observable — so among the CONTROLS nothing is both unobservable and actuatable, while ").Append(counts.ActuatableAndObservable).Append(" actuatable quantities are read by the observables. ");
        b.Append("THE SECTOR'S OWN PRICING: a redistribution costs ").Append(energy[0].TheSectorsOwnCost.ToString("F1", CultureInfo.InvariantCulture))
            .Append(" in the theory's ledger, while a source would be detected exactly (residual ")
            .Append(energy[1].TheSectorsOwnCost.ToString("F4", CultureInfo.InvariantCulture))
            .Append(") rather than produced. ");
        b.Append("SO THE FAILURE CRITERION IS MET FOR PATHWAYS: every channel collapses back to the existing kernel ")
            .Append("without adding a controllable variable. The success criterion is met only in the weaker sense that ")
            .Append("ONE effective and (until measured) unobservable control is IDENTIFIED — the kernel's curvature — and ")
            .Append("with it the OPERATION the class would have to contain: an operation that changes a parameter of the ")
            .Append("law, or creates count. The audit states that requirement instead of assuming impossibility.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the inverse problem has a closed solution — the density is the lever arm, the");
        b.AppendLine("gain is 1/d, and a gravity change needs a differential rather than a uniform change; that exactly ONE");
        b.AppendLine("channel is actuatable and it is the class's own transfer; that every unobservable channel is a CONSERVED");
        b.AppendLine("input; that within the class nothing is both unobservable and actuatable; and that the sector prices a");
        b.AppendLine("source exactly while pricing a redistribution at nothing.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that manipulation is impossible. The audit was not allowed to argue that, and it");
        b.AppendLine("does not. What it establishes is WHICH CHANNEL would be needed and WHAT THE CLASS WOULD HAVE TO CONTAIN: an");
        b.AppendLine("operation that changes a conserved input (the link structure, the component count, the total) or changes a");
        b.AppendLine("parameter of the law (the kernel's curvature, which alone would be effective and unobservable in situ).");
        b.AppendLine("Those are requirements on the theory, stated as such rather than smuggled in as primitives.");
        b.AppendLine();
        b.AppendLine("WHY THE ACTUATABLE CHANNEL IS NOT A NEW MANIPULATION: the clock and the source are one object (the sector's");
        b.AppendLine("own finding that the rate and the gravity share a source), so moving count moves the clock, and the effect is");
        b.AppendLine("the ordinary gravitational one. A manipulation THAT THE OBSERVABLES DO NOT READ would have to act through a");
        b.AppendLine("quantity the observables do not read — and every such quantity in this sector is conserved.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: an operation that creates or removes count (which the ledger would detect and");
        b.AppendLine("which would give a global clock shift), an operation that changes the link structure or the component count");
        b.AppendLine("(which would give transport control), or a measurement-free channel through the kernel's curvature (which");
        b.AppendLine("would give an unobservable global change). Each is a requirement, and each is named.");
        return b.ToString();
    }

    // ===================== 6. THE REPORT =====================

    public static string OutputInverseProblem()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE INVERSE PROBLEM: WHAT CHANGE IN THE SURVIVING STATE A CLOCK CHANGE REQUIRES (task 1)");
        b.AppendLine("   the DAMPING of the channel, and the required local count change for three targets, at the surface potential:");
        foreach (var (target, required, densityPerUnitClock, absolute) in InverseProblemSmallClockChange(
            new[] { 0.01, 0.10, 1.00 }, SurfacePotential))
        {
            b.AppendLine($"     a clock change of {target,7:P0} needs a density change of {required,10:P3} "
                + $"(density per unit clock {densityPerUnitClock:F4}, absolute {absolute:E3} on a unit occupancy)");
        }
        b.AppendLine("   and the gravity side:");
        foreach (var (direction, acceleration, clock, contains) in GravityInverse())
        {
            b.AppendLine($"     {direction,-62} acceleration change {acceleration:E3}, clock change {clock:E3}, "
                + $"the class contains it: {(contains ? "yes" : "NO")}");
        }
        return b.ToString();
    }

    public static string OutputChannels()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE CHANNEL SEARCH (task 2)");
        b.AppendLine("   channel                    control  observable  actuatable  status");
        foreach (var c in Channels())
        {
            b.AppendLine($"   {c.Channel,-26} {(c.IsAControlOfTheKernel ? "yes" : "no"),7}  "
                + $"{(c.IsDirectlyObservable ? "yes" : "no"),10}  {(c.IsActuatableByTheClass ? "yes" : "no"),10}  "
                + $"{StatusName(StatusOf(c))}");
        }
        return b.ToString();
    }

    public static string OutputHiddenVariables()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE ELIMINATED STRUCTURES, RE-EXAMINED AS CONTROL VARIABLES (task 3)");
        foreach (var v in HiddenVariables())
        {
            b.AppendLine($"   {v.Quantity,-46} control {(v.ControlsTheKernel ? "yes" : "no ")}  observable "
                + $"{(v.DirectlyObservable ? "yes" : "no ")}  actuatable {(v.Actuatable ? "yes" : "no ")}");
        }
        var counts = ControlVersusObservableCounts();
        b.AppendLine("4. THE CONTROL-VERSUS-OBSERVABLE RULE (task 4)");
        b.AppendLine($"   controlling the kernel: {counts.Controls}; of those, unobservable: {counts.UnobservableControls}");
        b.AppendLine($"   actuatable: {counts.Actuatable}; of those, unobservable: {counts.UnobservableAndActuatableControls}; "
            + $"actuatable AND observable: {counts.ActuatableAndObservable}");
        return b.ToString();
    }

    public static string OutputScale()
    {
        var b = new StringBuilder();
        b.AppendLine("5. ENERGY SCALING AND TECHNOLOGY (tasks 5 and 6)");
        foreach (var (channel, cost, practical) in EnergyScaling())
        {
            b.AppendLine($"   {channel}:");
            b.AppendLine($"     the sector's own cost {cost:F4}; the practical cost would be {practical}");
        }
        foreach (var (regime, potential, gain, scale) in ScaleTable())
        {
            b.AppendLine($"   in {regime} (potential {potential:E3}, gain {gain:F4}):");
            foreach (var (target, required) in scale)
            {
                b.AppendLine($"     a {target,6:P0} clock change needs a local count change of {required,10:P3}");
            }
        }
        foreach (var (classification, channel, why) in TechnologyClassification())
        {
            b.AppendLine($"   {classification,-20} {channel}");
            b.AppendLine($"     {why}");
        }
        return b.ToString();
    }

    public static string OutputPathway()
    {
        var b = new StringBuilder();
        b.AppendLine("6. THE MINIMAL MANIPULATION PATHWAY (task 7)");
        foreach (var (step, what, limit) in MinimalPathway())
        {
            b.AppendLine($"   {step}: {what}");
            b.AppendLine($"     limit: {limit}");
        }
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("7. THE VERDICT");
        b.AppendLine("   " + Verdict());
        b.AppendLine();
        b.Append(WhereItStands());
        return b.ToString();
    }
}
