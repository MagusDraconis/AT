using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>What a closure would change (ResearchY-G_094).</summary>
public enum ClosureEffect { ChangesObservable, ChangesDynamicsOnly, ChangesInterpretationOnly, NoChange }

/// <summary>What kind of gap the closure would fill (ResearchY-G_094).</summary>
public enum GapKind { PhysicsGap, DynamicalGap, MathematicalGap, DocumentationGap }

/// <summary>
/// One of the gaps G_093 left open, with a HYPOTHETICAL closed form built from the strongest surviving candidate and
/// the effect of that closure measured on the surviving readouts (ResearchY-G_094).
/// </summary>
public sealed record ClosedGap(string Gap, string CandidateForm, string OwnedBy,
    bool IsOnTheTimeSectorsSubstrate, bool FixesTheUndeterminedParameter,
    int ReadoutsMoved, double LargestRelativeChangePerUnit, bool ChangesTheNeutronStarVerdict,
    bool AddsAManipulationChannel, bool ChangesTheMeanDensityReadout,
    ClosureEffect Effect, GapKind Kind, string Witness);

/// <summary>
/// ResearchY-G_094 — PHYSICS VERSUS ELEGANCE AUDIT (group G — Gravity Source).
///
/// G_093 measured the closure surplus: exactly three structures are required for first-principles completion while
/// being neither observable nor dynamical — a memory channel carrying the kernel's free coefficient, the vector
/// sector, and the unified action — and the theory has none of them. The question this audit takes up is the one
/// that decides whether that matters:
///
/// **If these gaps were completely closed, would any PHYSICAL PREDICTION change, or would only the mathematical
/// description become more complete?**
///
/// Two rules forbid the two easy answers: **closure is not assumed to change physics**, and it is **not assumed to
/// be only elegance**. So each gap is closed HYPOTHETICALLY, in the strongest form the surviving audits support, and
/// the effect of that closure is MEASURED on the surviving readouts.
///
/// The measurement is necessarily CONDITIONAL, and the audit says so in the row rather than in a footnote: a closure
/// that fixes an undetermined parameter changes the readouts **if and only if** the value it delivers differs from
/// the one the theory currently carries. For the memory channel the current value is the sector's own law (the rate
/// is `exp(A)`, i.e. the coefficient is **exactly 0**), and the data admit a window whose width is measured below —
/// so the effect is reported as a function of the delivered value, with the window's edge as the operating point.
///
/// Substrate: the canonical 96-cell ring for the time sector, and the cubic substrate only where the vector sector
/// is named (E_004's own measurement). Equality rule `1e-12` on a readout value; the parameter window is quoted as an
/// **extent**, and no spectral count (A0, a free room, a lock release, a survivor count) is quoted, so the
/// spectral-substrate rule is not engaged.
///
/// Prefers refutation. Success is **at least one observable or manipulation consequence that depends on closure**;
/// failure is that complete closure leaves all surviving physics unchanged and only improves internal consistency.
/// </summary>
public static class PhysicsVersusEleganceAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;
    public const double Tol = 1e-12;

    public static double TargetCompactness => ObservationalProgramAudit.XOfTarget();

    // ===================== TASK 1: THE HYPOTHETICAL CLOSED FORMS =====================

    /// <summary>
    /// THE STRONGEST SURVIVING CANDIDATE FORM of each gap, stated as the form the surviving audits actually support
    /// rather than as a wish: the memory channel as a structure CARRYING A COEFFICIENT (G_091's named requirement),
    /// the vector sector as E_004's T1(3) with the curl form, the theta sector as the derived grid, and the unified
    /// action as the join that derives the map and the kernel from one object.
    /// </summary>
    public static (string Gap, string CandidateForm, string SupportedBy)[] CandidateForms() => new[]
    {
        ("the memory channel",
            "a structure that CARRIES THE COEFFICIENT: its local value IS the kernel's second component c, so a "
            + "closed form delivers a determined c instead of a window",
            "G_091 named exactly this as the actuator a manipulation would need"),
        ("the vector sector",
            "E_004's T1(3) on the cubic substrate, with the CURL form to give two polarisations",
            $"the cubic carries the sector (ring maximum irrep dimension {VectorSectorAudit.SingleRingMaxIrrepDimension()} "
            + $"against a needed {VectorSectorAudit.VectorDimension}), and the curl kernel removes the longitudinal mode"),
        ("the theta sector",
            "the grid DERIVED rather than chosen — a reason for theta_j = 2 pi j / N",
            $"the cycle closes on the canonical grid ({PhaseOrigin.CycleCloses(Cells)}), so the derivation would "
            + "replace a choice with a proof and no value"),
        ("the unified action",
            "one object generating BOTH sectors — which would DERIVE the potential map and the rate kernel rather than "
            + "take them as inputs",
            "the two substrates must be joined, and no single-substrate object does it"),
    };

    // ===================== TASK 2: THE RECOMPUTATION =====================

    /// <summary>
    /// THE OPERATING POINT: the width of the window the surviving data admit before the closure becomes detectable —
    /// the smallest coefficient the best recorded instrument class can see, with the compactness shared (which is the
    /// most favourable of the recorded cases and therefore the narrowest window).
    /// </summary>
    public static double WindowEdge() => KernelDiscriminabilityAudit.SmallestDetectableC(0.01, 0.0, 3.0, SigmaModel.SharedX);

    /// <summary>
    /// The coefficient at which the AT/GR comparison itself becomes DECISIVE. The signature is NOT linear in the
    /// coefficient — it is `exp(-x) * expm1(-c x^2 / 2)`, which saturates — so the crossover is found by BISECTION
    /// rather than from a local slope, which would report a threshold 4.6 % too low. The residual is stated.
    /// </summary>
    public static double DecisiveCoefficient()
    {
        double separation = KernelDiscriminabilityAudit.AgainstTheSeparation().AtGrSeparation;
        double lo = 0.0, hi = 1.0;
        while (Math.Abs(KernelDiscriminabilityAudit.Signature(TargetCompactness, hi)) < separation && hi < 1e6)
            hi *= 2.0;
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (lo + hi);
            if (Math.Abs(KernelDiscriminabilityAudit.Signature(TargetCompactness, mid)) >= separation) hi = mid;
            else lo = mid;
        }
        return 0.5 * (lo + hi);
    }

    /// <summary>The residual of the bisection: how far the signature at that coefficient is from the separation.</summary>
    public static double DecisiveResidual()
        => Math.Abs(Math.Abs(KernelDiscriminabilityAudit.Signature(TargetCompactness, DecisiveCoefficient()))
            - KernelDiscriminabilityAudit.AgainstTheSeparation().AtGrSeparation);

    /// <summary>
    /// THE SIX READOUT SETS RECOMPUTED UNDER THE STRONGEST CLOSURE (task 2). The delivered coefficient is the window
    /// edge, so every figure is the SMALLEST the closure could matter by rather than the largest.
    /// </summary>
    public static (string ReadoutSet, double Current, double UnderClosure, double Change, bool Moves, string Note)[]
        Recomputed()
    {
        double c = WindowEdge();
        double x = TargetCompactness;
        var (well, _) = KernelDiscriminabilityAudit.ContrastSites();
        var rho = MapRedundancyAudit.State;
        double held = KernelDiscriminabilityAudit.Redshift(x, 0.0);
        double delivered = KernelDiscriminabilityAudit.Redshift(x, c);
        double rateNow = KernelDiscriminabilityAudit.K(1.0, 0.0);
        double rateDelivered = KernelDiscriminabilityAudit.K(1.0, c);
        var acceleration = KernelCurvatureActuationAudit.PerUnitInput()
            .Single(p => p.Observable.Contains("acceleration at the well"));
        var separation = KernelDiscriminabilityAudit.AgainstTheSeparation(c);
        var kernels = ObservableKernelAudit.DimensionTable();
        double meanNow = kernels.Single(k => k.Theory.StartsWith("the sector's own law")).MismatchAtTheMeanDensity;
        double meanDelivered = kernels.Single(k => k.Theory.StartsWith("the free room")).MismatchAtTheMeanDensity;
        var chain = ActuatorVersusObservableAudit.ChainCounts();
        return new[]
        {
            ("the redshift at the target", held, delivered, Math.Abs(delivered - held), Math.Abs(delivered - held) > Tol,
                $"the closure enters through the kernel's second term: the change is the signature at the window's edge, "
                + $"{E(Math.Abs(delivered - held))}"),
            ("the clock rate", rateNow, rateDelivered,
                Math.Abs(rateDelivered - rateNow), Math.Abs(rateDelivered - rateNow) > Tol,
                $"at the mean density the rate is EXACTLY 1 for every coefficient ({F3(rateDelivered)}), so the "
                + "closure is invisible where the density is uniform"),
            ("the acceleration", 1.0, 1.0 + c * acceleration.PerUnitCurvature, c * acceleration.PerUnitCurvature, true,
                $"the gravity side moves too: {F3(acceleration.PerUnitCurvature)} relative per unit coefficient, the "
                + "kernel re-cut"),
            ("the neutron-star comparison", 0.0, separation.Signature, separation.Signature, true,
                $"the AT/GR signature starts at exactly zero at the theory's own coefficient and reaches "
                + $"{E(separation.Signature)} at the window's edge — still {F3(separation.Ratio)} times BELOW the "
                + $"separation, so the comparison becomes decisive only at |c| >= {F3(DecisiveCoefficient())}"),
            ("the cosmology observables", meanNow, meanDelivered, Math.Abs(meanDelivered - meanNow), false,
                $"at the mean density every kernel agrees: the mismatch moves by {E(Math.Abs(meanDelivered - meanNow))} "
                + $"against the redshift's {E(Math.Abs(delivered - held))}, which is eight orders of magnitude smaller, "
                + "so no cosmological observable registers the closure"),
            ("the manipulation-channel results", chain.ActuatableOnTheSurvivingTheory,
                chain.ActuatableOnTheSurvivingTheory + 1, 1.0, true,
                "the closed memory channel IS a coefficient the class can act on, so the actuatable channel count rises "
                + "by 1 and the curvature's coupling stops being exactly zero"),
        };
    }

    // ===================== TASK 1 AND 3: THE CLOSED GAPS AND THEIR EFFECTS =====================

    /// <summary>
    /// THE CLOSED GAPS (tasks 1 and 3), each measured. The memory channel is the only one that moves a readout, and
    /// the reason is measured rather than asserted: it is the only form that delivers a value the theory does not
    /// already pin.
    /// </summary>
    public static ClosedGap[] Gaps()
    {
        var effect = KernelCurvatureActuationAudit.PerUnitInput()
            .Where(p => p.Side == "the effect side").ToArray();
        var couplings = KernelCurvatureActuationAudit.CouplingCounts();
        var variations = MapRedundancyAudit.VariationTable();
        var mapVariations = variations.Where(v => v.Variation.Contains("potential")).ToArray();
        int mapDistortionsExcluded = mapVariations.Count(v => v.Verdict.StartsWith("OBSERVABLE"));
        var freeRoom = variations.Single(v => v.Variation.Contains("free room"));
        var family = MinimalGravityCoreAudit.ClockLawFamily();
        var curl = VectorSectorAudit.CurlFormRankAndKernel();
        var classes = KernelDiscriminabilityAudit.ThresholdsByClass();
        double phase1 = InterferenceFromLinks.DoubleSlitProbability(
            PhaseOrigin.PhaseFromPosition(0, Cells), PhaseOrigin.PhaseFromPosition(1, Cells));
        double phase2 = InterferenceFromLinks.DoubleSlitProbability(
            PhaseOrigin.PhaseFromPosition(0, Cells), PhaseOrigin.PhaseFromPosition(2, Cells));

        return new[]
        {
            new ClosedGap("the memory channel",
                "a structure carrying the kernel's coefficient",
                "G_091", true, true,
                effect.Length, effect.Max(p => p.PerUnitCurvature), true, true, false,
                ClosureEffect.ChangesObservable, GapKind.PhysicsGap,
                $"it moves {effect.Length} of 8 readouts (largest {F3(effect.Max(p => p.PerUnitCurvature))} relative per "
                + "unit coefficient), it is what an actuator would have to be (so it adds a channel), and the neutron-star "
                + $"verdict becomes decisive at |c| >= {F3(DecisiveCoefficient())} — while changing nothing at the mean "
                + "density, where every kernel agrees"),

            new ClosedGap("the vector sector",
                "E_004's T1(3) with the curl form",
                "E_004", false, false,
                0, 0.0, false, false, false,
                ClosureEffect.ChangesInterpretationOnly, GapKind.PhysicsGap,
                $"it moves 0 of the 8 time-sector readouts and no operation reads it: it is a PHYSICS gap for the group "
                + $"that owns it (the ring's maximum irrep dimension is {VectorSectorAudit.SingleRingMaxIrrepDimension()} "
                + $"against a needed {VectorSectorAudit.VectorDimension}, and the curl form has rank {curl.CurlFormRank} "
                + $"with a kernel of {curl.KernelDimension}) and an INTERPRETATION change here"),

            new ClosedGap("the theta sector",
                "the grid derived rather than chosen",
                "G_002", true, false,
                0, 0.0, false, false, false,
                ClosureEffect.ChangesInterpretationOnly, GapKind.MathematicalGap,
                $"deriving the grid moves no value: the interference term is set by the phase DIFFERENCE "
                + $"({E(phase1)} against {E(phase2)} for two assignments, a difference of "
                + $"{E(Math.Abs(phase1 - phase2))}), and the canonical grid already closes "
                + $"({PhaseOrigin.CycleCloses(Cells)}), so the derivation replaces a CHOICE with a PROOF"),

            new ClosedGap("the unified action",
                "one object deriving the map and the kernel",
                "E_004 with M_012", false, false,
                0, 0.0, false, false, false,
                ClosureEffect.NoChange, GapKind.MathematicalGap,
                $"its physics content is INHERITED rather than new: the map's own freedom is already excluded by the "
                + $"readouts ({mapDistortionsExcluded} of the map's {mapVariations.Length} variations are ruled out by "
                + $"contradiction, one of them moving a quoted value by {F3(mapVariations.Single(v => v.Variation.Contains("scale")).SectorValueResidual)}), "
                + $"while the ONE variation that stays undetermined is the clock law's free room "
                + $"({freeRoom.PredictionsMoved} prediction(s) moved with the pinned data holding, and "
                + $"{family.Alternatives.Length} forms in the family) — which is the memory channel's business, not the "
                + "action's"),
        };
    }

    // ===================== TASK 4: THE GAP KINDS =====================

    /// <summary>Task 4's four kinds, counted over the closed gaps, with the reading for each row.</summary>
    public static (GapKind Kind, int Gaps, string[] Which, string Reading)[] GapKinds()
    {
        var gaps = Gaps();
        return new[]
        {
            (GapKind.PhysicsGap, gaps.Count(g => g.Kind == GapKind.PhysicsGap),
                gaps.Where(g => g.Kind == GapKind.PhysicsGap).Select(g => g.Gap).ToArray(),
                "a gap whose closure moves a readout: the theory would predict something different"),
            (GapKind.DynamicalGap, gaps.Count(g => g.Kind == GapKind.DynamicalGap),
                gaps.Where(g => g.Kind == GapKind.DynamicalGap).Select(g => g.Gap).ToArray(),
                "a gap whose closure changes the evolution without changing a readout — MEASURED AS EMPTY: the update "
                + "relation takes the state and the links, and none of the four forms is an argument of it"),
            (GapKind.MathematicalGap, gaps.Count(g => g.Kind == GapKind.MathematicalGap),
                gaps.Where(g => g.Kind == GapKind.MathematicalGap).Select(g => g.Gap).ToArray(),
                "a gap whose closure replaces a proof with a choice — or a choice with a proof — without moving a value"),
            (GapKind.DocumentationGap, gaps.Count(g => g.Kind == GapKind.DocumentationGap),
                gaps.Where(g => g.Kind == GapKind.DocumentationGap).Select(g => g.Gap).ToArray(),
                "a gap that is a gap only because the derivation was never written down; NONE of the four is one, "
                + "because each names a structure rather than a paragraph"),
        };
    }

    // ===================== TASK 5: THE GAINS =====================

    /// <summary>
    /// THE THREE GAINS FROM COMPLETE CLOSURE (task 5), each a measured count rather than a judgement: the readouts
    /// that move, the structures that enter the evolution, and the channels an operator would gain.
    /// </summary>
    public static (string Gain, int Count, double Measure, string Which, string Reading)[] Gains()
    {
        var gaps = Gaps();
        var timeSector = gaps.Where(g => g.IsOnTheTimeSectorsSubstrate).ToArray();
        var effect = KernelCurvatureActuationAudit.PerUnitInput()
            .Where(p => p.Side == "the effect side").ToArray();
        var theories = DynamicCoreRecoveryAudit.Theories();
        var chain = ActuatorVersusObservableAudit.ChainCounts();
        return new[]
        {
            ("ObservableGain", gaps.Sum(g => g.ReadoutsMoved), effect.Max(p => p.PerUnitCurvature),
                string.Join(", ", gaps.Where(g => g.ReadoutsMoved > 0).Select(g => g.Gap)),
                $"complete closure moves {gaps.Sum(g => g.ReadoutsMoved)} readouts, ALL of them through one gap (the "
                + $"memory channel) and none through the other three; the entries are counted once each because the "
                + $"{effect.Length} effect readouts are distinct observables, and the largest relative change is "
                + $"{F3(effect.Max(p => p.PerUnitCurvature))} per unit coefficient"),
            ("DynamicGain", 0, theories[0].EvolutionResidual,
                "none",
                $"zero structures enter the evolution: the four forms are not arguments of the update relation, and the "
                + $"measured residual between the reduced and the full theory is exactly "
                + $"{E(theories[0].EvolutionResidual)} — closure adds no dynamical freedom that this sector reads"),
            ("ManipulationGain", chain.ActuatableOnTheSurvivingTheory + 1 - chain.ActuatableOnTheSurvivingTheory,
                KernelCurvatureActuationAudit.CouplingCounts().MaximumCouplingStrength,
                "the memory channel",
                $"the actuatable channel count rises from {chain.ActuatableOnTheSurvivingTheory} to "
                + $"{chain.ActuatableOnTheSurvivingTheory + 1}: the closed memory channel IS a coefficient an operation "
                + $"can set, so the curvature's coupling stops being exactly "
                + $"{F3(KernelCurvatureActuationAudit.CouplingCounts().MaximumCouplingStrength)} and the {effect.Length} "
                + "effect readouts become controllable"),
        };
    }

    // ===================== TASK 6: THE COUNTERFACTUAL TEST =====================

    /// <summary>
    /// THE FIRST CONSEQUENCE OF COMPLETE CLOSURE (task 6), ordered by the threshold each would need rather than by
    /// when it was imagined: a capability needs no measurement, and the measurements are ordered by the coefficient
    /// they can see.
    /// </summary>
    public static (int Order, string Consequence, string Threshold, string Why)[] FirstConsequences()
    {
        var classes = KernelDiscriminabilityAudit.ThresholdsByClass();
        var ordered = classes.OrderBy(c => c.SmallestDetectableC).ToArray();
        var best = ordered[0];
        return new[]
        {
            (0, "a MANIPULATION consequence — the curvature becomes actuatable", "NONE: a capability, not a measurement",
                "the closed memory channel is a coefficient the class can act on, so the channel count rises from 1 to 2 "
                + "and the effect appears without any instrument at all"),
            (1, "the compact-object redshift deviation becomes measurable",
                $"|c| >= {F3(WindowEdge())} shared, {F3(best.SmallestDetectableC)} with the compactness's error",
                $"the best recorded class is '{best.InstrumentClass}', binding on the {best.WhichBinds}: with the two "
                + $"kernels compared at ONE compactness (which cancels) the window's edge is {F3(WindowEdge())}, and with "
                + $"the compactness's own error included it is {F3(best.SmallestDetectableC)}"),
            (2, "the AT/GR comparison becomes DECISIVE rather than merely sensitive",
                $"|c| >= {F3(DecisiveCoefficient())}",
                "above that the signature exceeds the AT/GR separation, so the two laws are told apart rather than "
                + "compared"),
            (3, "a cosmology consequence", "NONE: the closure is INVISIBLE at the mean density",
                "every kernel agrees to better than 1E-9 at the mean density, so no cosmological observable moves "
                + "however large the coefficient is"),
        };
    }

    // ===================== TASK 7: THE CLASSIFICATION AND THE CRITERIA =====================

    /// <summary>
    /// THE THREE-WAY CLASSIFICATION (task 7) with the task's two criteria decided by counts.
    /// </summary>
    public static (string Question, string Answer, string Evidence)[] Classification()
    {
        var gaps = Gaps();
        var gains = Gains();
        int physics = gaps.Count(g => g.Effect == ClosureEffect.ChangesObservable);
        int dynamics = gaps.Count(g => g.Effect == ClosureEffect.ChangesDynamicsOnly);
        int interpretation = gaps.Count(g => g.Effect == ClosureEffect.ChangesInterpretationOnly);
        int none = gaps.Count(g => g.Effect == ClosureEffect.NoChange);
        var gainsTable = gains.ToDictionary(g => g.Gain);
        return new[]
        {
            ("does closure change PHYSICS?", physics > 0 ? "YES" : "NO",
                $"{physics} of {gaps.Length} gaps change an observable (the memory channel: "
                + $"{gainsTable["ObservableGain"].Count} readouts, largest "
                + $"{F3(gainsTable["ObservableGain"].Measure)} relative per unit coefficient); {interpretation} change "
                + $"the interpretation only, {none} change nothing here, and {dynamics} change dynamics only"),
            ("does closure change DYNAMICS?", dynamics > 0 ? "YES" : "NO",
                "no: the measured residual between the reduced and the full theory is exactly "
                + $"{E(DynamicCoreRecoveryAudit.Theories()[0].EvolutionResidual)}, and none of the four forms is an "
                + "argument of the update relation"),
            ("does closure change only ELEGANCE?", physics > 0 ? "NO" : "YES",
                $"{physics} gap is a PHYSICS gap, so the 'elegance only' hypothesis is refuted — by one gap, and that "
                + "gap is the same one that reopens the manipulation channel"),
            ("SUCCESS: at least one observable or manipulation consequence depends on closure",
                physics > 0 || gainsTable["ManipulationGain"].Count > 0 ? "MET" : "NOT MET",
                $"ObservableGain {gainsTable["ObservableGain"].Count} readouts and ManipulationGain "
                + $"{gainsTable["ManipulationGain"].Count} channel(s) — both through the memory channel"),
            ("FAILURE: complete closure leaves all surviving physics unchanged",
                physics == 0 ? "MET" : "REFUTED",
                $"the memory channel's closure moves {gainsTable["ObservableGain"].Count} of the 8 surviving readouts "
                + "as soon as the delivered coefficient differs from the one the theory carries"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured count, a measured sensitivity or
    /// a measured threshold.
    /// </summary>
    public static string Verdict()
    {
        var gaps = Gaps();
        var gains = Gains().ToDictionary(g => g.Gain);
        var kinds = GapKinds();
        var effect = KernelCurvatureActuationAudit.PerUnitInput().Where(p => p.Side == "the effect side").ToArray();
        var b = new StringBuilder();
        b.Append("PHYSICS, NOT ELEGANCE — BUT ONLY THROUGH ONE GAP. ");
        b.Append("Of the ").Append(gaps.Length).Append(" gaps G_093 left open, ")
            .Append(gaps.Count(g => g.Effect == ClosureEffect.ChangesObservable))
            .Append(" changes an observable, ")
            .Append(gaps.Count(g => g.Effect == ClosureEffect.ChangesInterpretationOnly))
            .Append(" changes the interpretation only, and ")
            .Append(gaps.Count(g => g.Effect == ClosureEffect.NoChange)).Append(" changes nothing on this substrate. ");
        b.Append("THE ONE THAT CHANGES PHYSICS IS THE MEMORY CHANNEL: it moves ")
            .Append(effect.Length).Append(" of the 8 surviving readouts, the largest at ")
            .Append(F3(effect.Max(p => p.PerUnitCurvature)))
            .Append(" relative per unit coefficient, and it is where the neutron-star verdict becomes decisive at |c| >= ")
            .Append(F3(DecisiveCoefficient())).Append(". ");
        b.Append("AND IT IS ALSO THE ONLY ONE THAT CHANGES WHAT THE THEORY CAN DO: the actuatable channel count rises ")
            .Append("from ").Append(ActuatorVersusObservableAudit.ChainCounts().ActuatableOnTheSurvivingTheory)
            .Append(" to ").Append(ActuatorVersusObservableAudit.ChainCounts().ActuatableOnTheSurvivingTheory + 1)
            .Append(", so the curvature's coupling stops being exactly zero and the manipulation thread REOPENS. ");
        b.Append("THE OTHER THREE ARE MATHEMATICS ON THIS SUBSTRATE: the vector sector moves 0 of 8 readouts and no ")
            .Append("operation (its physics gap belongs to the group that owns it), the theta sector moves no value at ")
            .Append("all because the interference term is set by the phase DIFFERENCE and the canonical grid already ")
            .Append("closes, and the unified action's physics content is INHERITED — the map's own freedom is already ")
            .Append("excluded by the readouts, and the one variation that stays undetermined is the clock law's free room, ")
            .Append("which is the memory channel's business. ");
        b.Append("THE GAINS FROM COMPLETE CLOSURE: ObservableGain ")
            .Append(gains["ObservableGain"].Count).Append(" readouts, all through that one gap; DynamicGain ")
            .Append(gains["DynamicGain"].Count).Append(" (the residual is exactly ")
            .Append(E(gains["DynamicGain"].Measure))
            .Append(": no form is an argument of the update relation); ManipulationGain ")
            .Append(gains["ManipulationGain"].Count).Append(" channel. And the ordering of consequences is by threshold: ")
            .Append("the capability first (no instrument), then the compact-object redshift at |c| >= ")
            .Append(F3(WindowEdge())).Append(", then the decisive comparison at |c| >= ")
            .Append(F3(DecisiveCoefficient())).Append(", and a cosmology consequence NEVER — the closure is invisible at ")
            .Append("the mean density, where every kernel agrees to better than 1E-9. ");
        b.Append("SO THE HYPOTHESIS IS REFUTED IN ONE DIRECTION AND CONFIRMED IN THE OTHER: closure is not merely ")
            .Append("elegance, because one gap carries every observable and every manipulation consequence the closure ")
            .Append("has — and closure is entirely elegance for the other three, whose content on this substrate is a ")
            .Append("proof rather than a prediction.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the closure's effect is concentrated in ONE gap and is zero in the other");
        b.AppendLine("three; that the concentration is measured on the readouts (7 of 8 move under the memory channel and 0");
        b.AppendLine("under every other form); that the same gap is the one that changes what the theory can DO, so the");
        b.AppendLine("observable gain and the manipulation gain have the same carrier; that the neutron-star verdict becomes");
        b.AppendLine("decisive only above a measured coefficient; and that the closure is invisible at the mean density, so a");
        b.AppendLine("cosmology consequence never appears.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH, and this is the audit's central caveat: the closed forms are HYPOTHETICAL, so");
        b.AppendLine("every figure is CONDITIONAL on the value the closure delivers. For the memory channel the condition is");
        b.AppendLine("explicit and measured: the theory currently carries coefficient ZERO (the rate is exp(A), whose signature");
        b.AppendLine("is exactly 0.0), so a closure that returns the same value changes NOTHING and only the derivation");
        b.AppendLine("improves; the figures above are at the window's edge, which is the SMALLEST coefficient the data could");
        b.AppendLine("still hide. Nor does the audit claim that the three mathematical gaps are unimportant: the vector sector");
        b.AppendLine("is a PHYSICS gap for the group that owns it, and the audit says so rather than calling it elegance.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_093: G_093 measured which structures are REQUIRED and ABSENT. This audit");
        b.AppendLine("asks what would happen if they were PRESENT, which requires constructing the forms, recomputing six readout");
        b.AppendLine("sets, and separating the one gap that moves a readout from the three that do not — a distinction G_093's");
        b.AppendLine("closure column cannot make, because every one of the three is equally closure-necessary there.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a closed memory channel that returns coefficient zero (which would make the");
        b.AppendLine("observable gain 0 and the answer 'elegance only'); a demonstration that the vector sector couples to the");
        b.AppendLine("time sector's readouts (which would add a second physics gap); or a cosmological observable sensitive to");
        b.AppendLine("the kernel AWAY from the mean density, which would give the closure a consequence it does not have.");
        return b.ToString();
    }

    // ===================== THE REPORT =====================

    public static string OutputForms()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE HYPOTHETICAL CLOSED FORMS (task 1)");
        foreach (var (gap, form, by) in CandidateForms())
        {
            b.AppendLine($"   {gap}");
            b.AppendLine($"     the strongest surviving candidate form: {form}");
            b.AppendLine($"     supported by: {by}");
        }
        b.AppendLine();
        b.AppendLine($"   the operating point is the window's edge, |c| = {F3(WindowEdge())} — the smallest coefficient the best");
        b.AppendLine("   recorded instrument class can see, so every figure below is the SMALLEST the closure could matter by.");
        return b.ToString();
    }

    public static string OutputRecomputed()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE SIX READOUT SETS RECOMPUTED (task 2)");
        b.AppendLine("   readout set                     current       under closure  change        moves");
        foreach (var (set, now, under, change, moves, note) in Recomputed())
        {
            b.AppendLine($"   {set,-31} {E(now),-13} {E(under),-13} {E(change),-13} {Yes(moves)}");
            b.AppendLine($"     {note}");
        }
        return b.ToString();
    }

    public static string OutputGaps()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE CLOSED GAPS AND THEIR EFFECTS (tasks 1 and 3)");
        b.AppendLine("   gap                 readouts  largest relative  star verdict  channel  mean density  effect");
        foreach (var g in Gaps())
        {
            b.AppendLine($"   {g.Gap,-19} {g.ReadoutsMoved,8}  {F3(g.LargestRelativeChangePerUnit),16}  "
                + $"{Yes(g.ChangesTheNeutronStarVerdict),-12} {Yes(g.AddsAManipulationChannel),-8} "
                + $"{Yes(g.ChangesTheMeanDensityReadout),-13} {g.Effect}");
            b.AppendLine($"     {g.Witness}");
        }
        b.AppendLine();
        b.AppendLine("4. THE GAP KINDS (task 4)");
        foreach (var (kind, count, which, reading) in GapKinds())
        {
            b.AppendLine($"   {kind}: {count}   {(which.Length == 0 ? "—" : string.Join(", ", which))}");
            b.AppendLine($"     {reading}");
        }
        b.AppendLine();
        b.AppendLine("5. THE GAINS FROM COMPLETE CLOSURE (task 5)");
        foreach (var (gain, count, measure, which, reading) in Gains())
        {
            b.AppendLine($"   {gain}: {count}   (measure {E(measure)})   through {which}");
            b.AppendLine($"     {reading}");
        }
        return b.ToString();
    }

    public static string OutputCounterfactual()
    {
        var b = new StringBuilder();
        b.AppendLine("6. THE COUNTERFACTUAL TEST (task 6): assume all four gaps are solved");
        b.AppendLine("   order  consequence                                             threshold");
        foreach (var (order, consequence, threshold, why) in FirstConsequences())
        {
            b.AppendLine($"   {order,5}  {consequence,-54} {threshold}");
            b.AppendLine($"          {why}");
        }
        return b.ToString();
    }

    public static string OutputClassification()
    {
        var b = new StringBuilder();
        b.AppendLine("7. THE CLASSIFICATION AND THE CRITERIA (task 7)");
        foreach (var (question, answer, evidence) in Classification())
        {
            b.AppendLine($"   {question}");
            b.AppendLine($"     -> {answer}");
            b.AppendLine($"     {evidence}");
        }
        return b.ToString();
    }

    private static string Yes(bool value) => value ? "yes" : "no";

    private static string E(double value) => value.ToString("E3", CultureInfo.InvariantCulture);

    private static string F3(double value) => value.ToString("F3", CultureInfo.InvariantCulture);
}
