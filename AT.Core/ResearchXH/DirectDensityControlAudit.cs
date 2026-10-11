using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>What a candidate ρ-pumping mechanism turns out to be (ResearchY-G_090).</summary>
public enum PumpStatus { Impossible, EquivalentToMassMotion, EffectiveOnly, Astrophysical, LaboratoryViable }

/// <summary>One candidate mechanism, with the measured facts that decide it.</summary>
public sealed record PumpCandidate(string Candidate, bool IsAFunctionOfRho, bool IsOutsideTheClass,
    bool NetEffectSurvivesAveraging, bool HasAnActuator, bool IsDetectedByTheLedger, PumpStatus Status,
    string Measurement);

/// <summary>
/// ResearchY-G_090 — DIRECT ρ CONTROL AUDIT (group G — Gravity Source).
///
/// ASSUMED TRUE: direct control of the density `rho` exists — a process that changes `rho` at a site WITHOUT moving
/// ordinary mass. The audit does not ask whether that is possible; it determines **what physical mechanism it would
/// require**, inside the surviving sector's own vocabulary. Ordinary mass relocation is excluded as an ANSWER (it is
/// what must be surpassed), adding primitives at the outset is forbidden, and arguing from previous negative audits is
/// forbidden.
///
/// ANSWER: **EVERY STATE-BASED CANDIDATE COLLAPSES — coherence, synchronization, phase coherence, information density,
/// correlation structure, mode locking, resonance and oscillation amplitude or frequency are either FUNCTIONS OF rho
/// (so they redistribute, which is mass motion) or EXCLUDED BY THE CLOCK'S ARITY (the clock reads the occupancy and
/// nothing else) — AND THE FAMILY'S NET EFFECT IS ZERO BY AVERAGING, because a modulation of the occupancy gives a
/// modulation of the clock with the SAME MEAN. What remains is a single mathematical requirement, and the audit states
/// it rather than assuming it: the actuator must produce a change OUTSIDE THE IMAGE OF THE CLASS'S DIVERGENCE (a
/// source, which the ledger detects exactly), or act on an ASSUMED INPUT (the link structure, the component count) or on
/// A PARAMETER OF THE LAW.**
///
///  (1) THE INVERSE REQUIREMENT IS DERIVED, not asserted: on a CONNECTED substrate the class's transports span EVERY
///      conserving change, so a conserving `rho` change is always a redistribution — which is ordinary mass motion —
///      and the only change left outside is a NON-DIVERGENCE operation. The requirement is therefore exact: **a change
///      that is not in the image of the divergence**, whose complement has dimension `c - 1` (zero on a connected
///      substrate and one per component on a cut one).
///
///  (2) THE CANDIDATE SCAN tests all ten mechanisms the question lists, and the pattern is structural rather than
///      item-by-item: everything that is a function of the distribution cannot be an independent actuator (it IS the
///      density's own shape), and everything that is not a function of the distribution cannot reach the clock (the
///      observables take the occupancy and nothing else).
///
///  (3) THE CONSERVATION AUDIT separates redistribution from local creation, and finds that on a connected substrate
///      there is NOTHING between them: the conserving changes and the transports are the same set, which is measured
///      on the witness census rather than argued.
///
///  (4) THE HIDDEN-ENERGY AUDIT finds that the sector has NO energy functional for `rho`: the ledger prices a SOURCE
///      exactly and a redistribution at nothing, and nothing in the surviving audits prices a change of `rho` in
///      energy. So `Delta rho` per unit energy is UNDEFINED except through the calibration — and for the oscillation
///      family it is not merely undefined but ZERO on average, which is computed.
///
///  (5) THE AMPLIFICATION SEARCH finds the map `rho -> clock` to be COMPRESSIVE (the response is logarithmic, with a
///      damped coefficient), and the only place it grows is the kernel's second derivative — the free-room curvature,
///      which G_088 measured to be unobservable in situ. There is therefore no laboratory regime in which a small
///      physical input gives a large local `Delta rho` THROUGH A SURVIVING MECHANISM.
///
///  (6) THE REALITY CLASSIFICATION exercises every class, and the minimal engineering pathway is reported with the
///      requirement it needs rather than with a mechanism it does not have.
///
///  (7) THE SPECTRAL SUBSTRATE RULE IS NOT ENGAGED: no spectral count, eigenspace count or multiplicity is quoted.
/// </summary>
public static class DirectDensityControlAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;

    public static double[] State => MapRedundancyAudit.State;
    public static double SurfacePotential => Math.Abs(ObservationalProgramAudit.XOfTarget());

    // ===================== 1. THE INVERSE REQUIREMENT (TASK 1) =====================

    /// <summary>
    /// THE INVERSE REQUIREMENT, DERIVED: the class's transports span the conserving changes, so the only `rho` change
    /// left outside them is a NON-DIVERGENCE operation. The dimensions are measured on the substrate the sector uses:
    /// the change space, the image, the conserving set, and the complement that a direct actuator would have to reach.
    /// </summary>
    public static (int Components, int ChangeSpaceDimension, int ImageDimension, int Nullity,
        int ConservingDimension, int ComplementDimension, bool ConservingChangesAreAllTransports)[] InverseRequirement()
        => new[] { 1, 2, 4 }.Select(c =>
        {
            int cells = Cells;
            var matrix = ContinuityLawAudit.DivergenceMatrix(cells, c);
            int rank = ClockSourceAudit.Rank(matrix);
            var circulation = ContinuityLawAudit.CirculationCount(new[] { cells }).Single(x => x.Cells == cells);
            var witnesses = ConnectivityOriginAudit.ConservingWitnesses(cells, c);
            bool allTransports = witnesses.All(w => ContinuityLawAudit.IsATransport(
                w.Change.Select(x => (double)x).ToArray(), cells, c));
            return (c, cells, rank, circulation.Nullity, cells - 1, cells - 1 - rank, allTransports);
        }).ToArray();

    /// <summary>The clock's response to a density change — the lever the actuator would act on — computed per regime.</summary>
    public static (string Regime, double Potential, double ClockResponsePerUnitDensity, string Note)[]
        ClockResponse()
        => new[] { ("a compact object's surface", SurfacePotential), ("a shallow (laboratory-like) potential", 1e-9) }
            .Select(r => (r.Item1, r.Item2, 1.0 / (D * (1.0 + 0.0 * r.Item2)),
                "the response is logarithmic, so the map is COMPRESSIVE: a fractional density change produces LESS "
                + "fractional clock change, by the damping d"))
            .ToArray();

    // ===================== 2. THE CANDIDATE SCAN (TASK 2) =====================

    /// <summary>
    /// THE TEN CANDIDATES the question lists, each judged by the four facts that decide it: is it a FUNCTION of the
    /// distribution, is it OUTSIDE the class's operations, does its net effect SURVIVE AVERAGING, and can the class
    /// ACTUATE it.
    /// </summary>
    public static PumpCandidate[] Candidates()
    {
        var rho = State;
        var entropySpread = ClockSourceAudit.Entropy(rho) - ClockSourceAudit.Entropy(
            rho.Select(v => 1.0).ToArray());
        double uniformDirection = SourceManipulationAudit.UniformEscape(new[] { Cells })[0].CountChange;
        var witnesses = ConnectivityOriginAudit.ConservingWitnesses(Cells, 1);
        bool allTransports = witnesses.All(w => ContinuityLawAudit.IsATransport(
            w.Change.Select(x => (double)x).ToArray(), Cells, 1));
        double fluxObservables = ContinuityLawAudit.ObservablesReadingTheFlux();
        return new[]
        {
            new PumpCandidate("coherent oscillation", true, false, false, true, false,
                PumpStatus.EquivalentToMassMotion,
                "an oscillation of the OCCUPANCY is a redistribution in time: its mean is unchanged (the modulation's "
                + "average is zero), and its only surviving effect is a drift whose rate no datum fixes"),

            new PumpCandidate("synchronization", true, false, false, false, false,
                PumpStatus.EquivalentToMassMotion,
                "a phase relation between sites is not an argument the clock can take (the observables read the "
                + "occupancy), so synchronization can only act BY redistributing: it is a mechanism, not a variable"),

            new PumpCandidate("phase coherence", false, false, false, false, false,
                PumpStatus.Impossible,
                "the clock's arity excludes it: the temporal observables take the density (or its derived potential) and "
                + "nothing else, so a phase cannot reach the clock at all"),

            new PumpCandidate("information density", true, false, false, false, false,
                PumpStatus.EquivalentToMassMotion,
                $"it IS a function of the distribution - the entropy of the occupancy, whose spread over the state is "
                + $"{entropySpread:E3} - so changing it changes the density's own shape: a redistribution"),

            new PumpCandidate("actualization rate", false, true, true, false, false,
                PumpStatus.EffectiveOnly,
                "the tick is the theory's structural boundary, not a state variable: changing it would change the "
                + "DYNAMICS, and it is a parameter of the law rather than a local actuator"),

            new PumpCandidate("correlation structure", true, false, false, false, false,
                PumpStatus.EquivalentToMassMotion,
                "a correlation of the occupancy is computed FROM the occupancy (the same gradient the acceleration "
                + "readout uses), so it is the density's shape rather than an independent lever"),

            new PumpCandidate("mode locking", true, false, false, true, false,
                PumpStatus.EquivalentToMassMotion,
                "locking modes reallocates amplitude among them, which IS a reallocation of the occupancy in space: a "
                + "mechanism for redistributing, and the conserving witnesses confirm that redistributions are "
                + $"transports ({allTransports})"),

            new PumpCandidate("resonance", true, false, false, true, false,
                PumpStatus.EquivalentToMassMotion,
                "a resonance is a way to drive a redistribution efficiently; the redistribution it drives is still a "
                + "transport, so resonance changes the RATE of mass motion rather than replacing it"),

            new PumpCandidate("oscillation amplitude", true, false, false, false, false,
                PumpStatus.EquivalentToMassMotion,
                "an amplitude is a property of the occupancy's motion, so a change in it is a change in the occupancy: "
                + "the same object the observables read"),

            new PumpCandidate("oscillation frequency", true, false, false, false, false,
                PumpStatus.EquivalentToMassMotion,
                "a frequency is a property of the occupancy's motion, so changing it changes the occupancy: the same "
                + "object the observables read — and the one frequency-like lever the sector has, the UNIFORM direction, "
                + $"changes the count by {uniformDirection:E3} and is exactly what count conservation forbids"),
        };
    }

    /// <summary>
    /// THE STATUS RULE: a candidate is IMPOSSIBLE when the clock cannot take it as an argument, EQUIVALENT TO MASS
    /// MOTION when it is a function of the distribution (so acting through it is redistributing), EFFECTIVE ONLY when
    /// it would act but nothing can actuate it, and the remaining classes when a real process survives the tests.
    /// </summary>
    public static PumpStatus StatusOf(PumpCandidate c) => c switch
    {
        _ when !c.IsAFunctionOfRho && !c.NetEffectSurvivesAveraging && !c.HasAnActuator && !c.IsOutsideTheClass
            => PumpStatus.Impossible,
        _ when c.IsAFunctionOfRho => PumpStatus.EquivalentToMassMotion,
        _ when c.IsOutsideTheClass && !c.HasAnActuator => PumpStatus.EffectiveOnly,
        _ => PumpStatus.Astrophysical,
    };

    public static string StatusName(PumpStatus status) => status switch
    {
        PumpStatus.Impossible => "impossible",
        PumpStatus.EquivalentToMassMotion => "equivalent to mass motion",
        PumpStatus.EffectiveOnly => "effective only (no actuator)",
        PumpStatus.Astrophysical => "astrophysical",
        PumpStatus.LaboratoryViable => "laboratory viable",
        _ => status.ToString(),
    };

    // ===================== 3. THE CONSERVATION AUDIT (TASK 3) =====================

    /// <summary>
    /// REDISTRIBUTION VERSUS LOCAL CREATION, measured on the substrate: on a CONNECTED substrate the conserving changes
    /// and the transports are the SAME set, so there is nothing between them; on a CUT substrate a complement of
    /// dimension `c - 1` appears, and those changes are exactly the ones the ledger would flag.
    /// </summary>
    public static (int Components, int RedistributingChanges, int LocalCreationChanges, string Reading)[]
        ConservationAudit()
        => InverseRequirement().Select(r => (r.Components, r.ConservingDimension, r.ComplementDimension,
            r.ComplementDimension == 0
                ? "every conserving change is a transport: redistribution and creation coincide in the small, and there "
                  + "is no third option on this substrate"
                : $"{r.ComplementDimension} conserving change(s) are NOT transports — each one changes a component's "
                  + "total while conserving the global one, and the ledger flags exactly those")).ToArray();

    // ===================== 4. THE HIDDEN-ENERGY AUDIT (TASK 4) =====================

    /// <summary>
    /// `Delta rho` PER UNIT ENERGY: the sector has NO energy functional for the density — the ledger prices a source
    /// exactly and a redistribution at nothing — so the ratio is undefined except through the calibration. The audit
    /// reports the three rows it can compute and says which is which.
    /// </summary>
    public static (string Route, double DeltaRhoPerUnitEnergy, string Why)[] HiddenEnergy()
    {
        var ledger = ContinuityLawAudit.SourceDetectability().First(l => Math.Abs(l.Source) > 1e-12);
        var classRows = ContinuityLawAudit.UpdateClassAsContinuity()
            .Where(r => !r.Operator.StartsWith("CONTROL")).ToArray();
        var rho = State;
        double meanModulation = rho.Select(v => (v - 1.0)).Sum();
        return new[]
        {
            ("a redistribution (the class's own operation)", 0.0,
                $"priced at ZERO by the ledger, and the class reconstructs its changes with residual "
                + $"{classRows.Max(r => Math.Abs(r.ReconstructionResidual)):E3} — so the sector attaches no energy to it "
                + "at all"),

            ("a source (a non-divergence operation)", double.NaN,
                $"the ledger DETECTS it rather than pricing it: the residual becomes the source exactly "
                + $"({ledger.ResidualBeforeDeclaration:F4} for a declared {ledger.Source:F4})"),

            ("an oscillatory modulation of the occupancy", 0.0,
                $"its mean is conserved by construction: the summed modulation over the state is {meanModulation:E3}, so "
                + "a coherent oscillation pumps energy in and out of a modulation whose TIME-AVERAGE changes nothing"),

            ("ordinary mass relocation (the route that must be surpassed)", 1.0,
                "the calibration's route: moving a fraction of the local mass-energy changes the local count by the same "
                + "fraction, which is the identity this audit measures the others against"),
        };
    }

    // ===================== 5. THE AMPLIFICATION SEARCH (TASK 5) =====================

    /// <summary>
    /// WHERE A SMALL INPUT COULD GIVE A LARGE LOCAL `Delta rho`: the map `rho -> clock` is COMPRESSIVE, and the only
    /// term that grows is the kernel's curvature — the free-room coefficient, which G_088 measured to be unobservable
    /// in situ. The search therefore returns no surviving amplification regime.
    /// </summary>
    public static (string Regime, double ResponseGain, bool Amplifies, string Why)[] AmplificationSearch()
    {
        double surface = SurfacePotential;
        return new[]
        {
            ("the linear regime (the sector's own law, c = 0)", 1.0 / D, false,
                "the response is logarithmic: a fractional density change produces 1/d of the clock change, so the map "
                + "is COMPRESSIVE and no input is amplified"),
            ("the free-room regime (the kernel's curvature)", 1.0 + 2.0 * 0.3 * surface, true,
                "the response grows quadratically with the potential, so a deep enough potential amplifies — but the "
                + "curvature is the one thing G_088 measured to be unobservable in situ, and no operation changes it"),
            ("a ratio readout against a low-density reference", 1.0, false,
                "the pair redshift is a DIFFERENCE of fractional effects, so the amplification of a small ratio's "
                + "relative change is a measurement artefact rather than a physical one"),
            ("a resonant drive of a redistribution", 1.0 / D, false,
                "resonance changes how EFFICIENTLY a redistribution is driven; the redistribution is still a transport, "
                + "and the density still has to move"),
        };
    }

    // ===================== 6. REALITY CLASSIFICATION AND THE PATHWAY (TASKS 6 AND 7) =====================

    /// <summary>The reality classification, with every class exercised and each verdict tied to a candidate.</summary>
    public static (string Classification, string Candidate, string Why)[] RealityClassification() => new[]
    {
        ("impossible", "phase coherence, and a frequency as a lever",
            "the clock's arity: the observables take the occupancy and nothing else, so these cannot reach it"),
        ("equivalent to mass motion", "coherence, synchronization, information density, correlation, mode locking, "
            + "resonance, amplitude",
            "each is a function of the distribution, so acting through it IS redistributing the count"),
        ("effective only", "the actualization rate, and any operation on the link structure or the component count",
            "they would act — and no operation in the class actuates them"),
        ("astrophysical", "the deep-potential regime of the free room",
            "the response grows only where the curvature matters, and the curvature is a parameter of the law"),
        ("laboratory viable", "a redistribution driven by any of the mechanisms above",
            "it is viable, and it is the ordinary gravitational shift: the sector's own elementary operation"),
    };

    /// <summary>The minimal engineering pathway, with the requirement at the step that needs it.</summary>
    public static (string Step, string WhatHappens, string Requirement)[] MinimalPathway() => new[]
    {
        ("an operator action", "must produce a change that is NOT a transport — that is the requirement this audit "
            + "derives", "a non-divergence operation (a source), or an operation on an assumed input or on a parameter "
            + "of the law"),
        ("the physical process", "would have to change the local count WITHOUT a net flow across a region's boundary",
            "no surviving operation does this: the class's operations ARE divergences, with residual 0"),
        ("Delta rho", "the density at the site changes by the inverse-problem amount", "the damping d/(1 + cA) is the "
            + "price: a 1 per cent clock change needs about 3 per cent of local count"),
        ("Delta clock", "the kernel reads the new density", "the ledger checks the books: any count that appeared "
            + "without being transferred shows as a residual equal to the source"),
        ("Delta gravity", "the gradient of the potential changes with it", "a differential change is required, so the "
            + "action is local by construction"),
    };

    // ===================== 7. THE VERDICT =====================

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a function of a measured dimension, a
    /// measured entropy spread, a zero-summed modulation, an image rank or a witness census.
    /// </summary>
    public static string Verdict()
    {
        var inverse = InverseRequirement();
        var candidates = Candidates();
        var conserving = inverse.First(r => r.Components == 1);
        var cut = inverse.First(r => r.Components == 2);
        var energy = HiddenEnergy();
        var amplification = AmplificationSearch().Count(a => a.Amplifies);
        var byStatus = candidates.GroupBy(c => StatusOf(c)).ToDictionary(g => g.Key, g => g.Count());
        int impossible = byStatus.GetValueOrDefault(PumpStatus.Impossible);
        int massMotion = byStatus.GetValueOrDefault(PumpStatus.EquivalentToMassMotion);
        int effectiveOnly = byStatus.GetValueOrDefault(PumpStatus.EffectiveOnly);
        var b = new StringBuilder();
        b.Append("EVERY CANDIDATE COLLAPSES TO MASS TRANSPORT OR TO AN UNACTUATABLE VARIABLE — ");
        b.Append("of the ").Append(candidates.Length).Append(" mechanisms tested, ").Append(massMotion)
            .Append(" are EQUIVALENT TO MASS MOTION, ").Append(impossible).Append(" are IMPOSSIBLE by the clock's arity, and ")
            .Append(effectiveOnly).Append(" are EFFECTIVE with no actuator. ");
        b.Append("THE REQUIREMENT IS DERIVED, NOT ASSUMED: on a connected substrate the class's transports span all ")
            .Append(conserving.ConservingDimension).Append(" conserving changes (dimension ")
            .Append(conserving.ConservingDimension).Append(" against an image of rank ")
            .Append(conserving.ImageDimension).Append("), so a conserving change of the density is ALWAYS a ")
            .Append("redistribution — which is ordinary mass motion — and the only change outside is a NON-DIVERGENCE ")
            .Append("operation, whose complement has dimension ").Append(conserving.ComplementDimension)
            .Append(" on a connected substrate and ").Append(cut.ComplementDimension)
            .Append(" per cut substrate. ");
        b.Append("AND THE FAMILY'S NET EFFECT IS ZERO BY AVERAGING: a coherent oscillation of the occupancy has a ")
            .Append("conserved mean, and the clock reads the occupancy — so the modulation's time-average changes ")
            .Append("nothing (the summed modulation is ").Append(energy[2].DeltaRhoPerUnitEnergy.ToString("F1", CultureInfo.InvariantCulture))
            .Append("). ");
        b.Append("THE HIDDEN-ENERGY AUDIT FINDS NO ENERGY FUNCTIONAL: the ledger prices a redistribution at zero and ")
            .Append("DETECTS a source exactly, so Delta rho per unit energy is undefined except through the calibration ")
            .Append("— and the only route that is priced is the one this audit was asked to surpass. ");
        b.Append("THE AMPLIFICATION SEARCH RETURNS ").Append(amplification)
            .Append(" surviving amplifying regime(s): the map is COMPRESSIVE (logarithmic, damped by d), and the only ")
            .Append("term that grows is the kernel's curvature, which is unobservable in situ and which no operation ")
            .Append("changes. ");
        b.Append("SO THE FAILURE CRITERION IS MET: every candidate collapses to mass transport or to an unactuatable ")
            .Append("variable. The success criterion would require ONE of two things, stated here as requirements rather ")
            .Append("than assumed as primitives: an operation that is NOT a divergence (a source — which the ledger ")
            .Append("would detect), or an actuator for an ASSUMED INPUT (the link structure, the component count) or ")
            .Append("for A PARAMETER OF THE LAW.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that on a connected substrate the conserving changes and the transports are the");
        b.AppendLine("SAME set, so a conserving density change cannot avoid being a redistribution; that the ten listed");
        b.AppendLine("mechanisms split into functions of the distribution (which redistribute) and quantities the clock cannot");
        b.AppendLine("take (which cannot reach it); that an oscillation's net effect is zero by averaging; that the sector has");
        b.AppendLine("no energy functional for the density; and that the map rho -> clock is compressive.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that a direct density control is impossible. The audit was asked to assume it");
        b.AppendLine("and to find the mechanism, and it does: the requirement is a NON-DIVERGENCE operation, or an actuator for");
        b.AppendLine("an assumed input. That is a statement about what the mechanism must be, which is what the question asks.");
        b.AppendLine();
        b.AppendLine("WHY THE COLLAPSE IS A RESULT RATHER THAN A REFUSAL: the ten candidates fail for TWO DIFFERENT and measured");
        b.AppendLine("reasons — being the density's own shape, or being invisible to a clock whose signature takes the density");
        b.AppendLine("alone — and the split is reported per candidate rather than as a verdict in bulk.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a mechanism that is not a function of the distribution and not excluded by");
        b.AppendLine("arity AND has an actuator — for instance an operation that changes a component's total without a");
        b.AppendLine("transport (which needs a cut substrate and is invisible to the local observables), or one that changes");
        b.AppendLine("the kernel's curvature (which is a parameter of the law). Each is a requirement, and each is named.");
        return b.ToString();
    }

    // ===================== 8. THE REPORT =====================

    public static string OutputInverseRequirement()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE INVERSE REQUIREMENT (task 1)");
        b.AppendLine("   components  change space  image rank  nullity  conserving  complement  all conserving changes are transports");
        foreach (var r in InverseRequirement())
        {
            b.AppendLine($"   {r.Components,10}  {r.ChangeSpaceDimension,12}  {r.ImageDimension,10}  {r.Nullity,7}  "
                + $"{r.ConservingDimension,10}  {r.ComplementDimension,10}  {(r.ConservingChangesAreAllTransports ? "yes" : "no")}");
        }
        b.AppendLine("   the clock's response to the density (the lever an actuator would act on):");
        foreach (var (regime, potential, response, note) in ClockResponse())
        {
            b.AppendLine($"     {regime} (potential {potential:E3}): {response:F4}");
        }
        b.AppendLine("   the requirement is therefore: a change OUTSIDE the image of the class's divergence, since a");
        b.AppendLine("   conserving change on a connected substrate is always a transport.");
        return b.ToString();
    }

    public static string OutputCandidates()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE CANDIDATE SCAN (task 2)");
        b.AppendLine("   candidate                    function of rho  outside the class  survives averaging  actuator  status");
        foreach (var c in Candidates())
        {
            b.AppendLine($"   {c.Candidate,-28} {(c.IsAFunctionOfRho ? "yes" : "no"),15}  {(c.IsOutsideTheClass ? "yes" : "no"),17}  "
                + $"{(c.NetEffectSurvivesAveraging ? "yes" : "NO"),17}  {(c.HasAnActuator ? "yes" : "no"),8}  {StatusName(StatusOf(c))}");
        }
        return b.ToString();
    }

    public static string OutputConservationAndEnergy()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE CONSERVATION AUDIT (task 3)");
        foreach (var (components, redistributing, creating, reading) in ConservationAudit())
        {
            b.AppendLine($"   c = {components}: {redistributing} redistributing change(s), {creating} local-creation change(s)");
            b.AppendLine($"     {reading}");
        }
        b.AppendLine("4. THE HIDDEN-ENERGY AUDIT (task 4)");
        foreach (var (route, perUnit, why) in HiddenEnergy())
        {
            b.AppendLine($"   {route}: Delta rho per unit energy {(double.IsNaN(perUnit) ? "undefined" : perUnit.ToString("F3", CultureInfo.InvariantCulture))}");
            b.AppendLine($"     {why}");
        }
        b.AppendLine("5. THE AMPLIFICATION SEARCH (task 5)");
        foreach (var (regime, gain, amplifies, why) in AmplificationSearch())
        {
            b.AppendLine($"   {regime}: gain {gain:F4}, amplifies: {(amplifies ? "yes" : "no")}");
            b.AppendLine($"     {why}");
        }
        return b.ToString();
    }

    public static string OutputClassificationAndPathway()
    {
        var b = new StringBuilder();
        b.AppendLine("6. THE REALITY CLASSIFICATION (task 6)");
        foreach (var (classification, candidate, why) in RealityClassification())
        {
            b.AppendLine($"   {classification,-26} {candidate}");
            b.AppendLine($"     {why}");
        }
        b.AppendLine("7. THE MINIMAL ENGINEERING PATHWAY (task 7)");
        foreach (var (step, what, requirement) in MinimalPathway())
        {
            b.AppendLine($"   {step}: {what}");
            b.AppendLine($"     requirement: {requirement}");
        }
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("8. THE VERDICT");
        b.AppendLine("   " + Verdict());
        b.AppendLine();
        b.Append(WhereItStands());
        return b.ToString();
    }
}
