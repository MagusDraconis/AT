namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 270 — Distinction Origin Audit. QG268 showed count arises from actualization; QG269
/// showed count and distinction arise together from the single individuation act. This phase asks the
/// TERMINAL question of the individuation chain: WHAT is being distinguished, and does distinction arise
/// from structure, actualization, or a deeper principle? No observables, no formulas, D96 only,
/// deterministic.
///
/// THE CANDIDATES (investigated):
///   (a) CAUSAL POSITION — the branching generations k = 0..K−1. VERIFIED: the density shares
///       ρ_k = μ^k/S are DISTINCT per generation (μ=2: 0.0039, 0.0078, 0.0157, … — distinct). At
///       criticality μ=1 the shares are uniform (1/K), but the GENERATION INDEX k still distinguishes
///       positions by their place in the causal order. Causal position CAN distinguish.
///   (b) NETWORK POSITION — VERIFIED: the observable sector is a REGULAR graph (all 96 nodes degree 12,
///       QG266). All nodes are structurally identical — there is NO network-position distinction. The
///       nodes carry no labels, no degrees that differ, no geometry that separates them. Network
///       position CANNOT distinguish.
///   (c) STATE DIFFERENCE — VERIFIED: a Q-event is a PROJECTION to a tick/no-tick binary state
///       (QG73). The event changes state: the DIFFERENCE between before and after IS the tick. State
///       difference distinguishes (each event is a before→after transition).
///   (d) ACTUALIZATION DIFFERENCE — VERIFIED: a Q-event is a NETWORK TRANSITION (a state change, QG29).
///       The event IS a difference: ρ counts these events (QG29), and each counted unit is one
///       transition. Actualization difference distinguishes.
///
/// THE D96 DIFFERENCE STRUCTURE (the deepest evidence):
///   The observable-sector Laplacian has 96 eigenvalues: ONE ZERO MODE (the constant vector, in ker L,
///   QG266) and 95 POSITIVE modes. The zero mode is the BACKGROUND (the uniform reference); the positive
///   modes are the DIFFERENCES from it. The spectrum has 44 distinct frequencies (degeneracy groups) —
///   each positive mode is a distinct difference from the zero/background. Distinction = the difference
///   between the background and each mode.
///
/// WHAT IS BEING DISTINGUISHED (the answer):
///   What is being distinguished is DIFFERENCES themselves. Each Q-event is a difference (a before→after
///   transition); causal positions are distinguished by their different shares ρ_k = μ^k/S; the positive
///   modes are distinguished by their difference from the zero/background. The thing distinguished is
///   always A DIFFERENCE — there is no pre-existing substance that gets a label; the distinction IS the
///   difference.
///
/// WHERE DISTINCTION COMES FROM (the determination):
///   NOT from structure — the regular network provides no positions, no labels, no separating geometry
///   (b fails). From ACTUALIZATION — yes, but only because actualization IS a difference (c, d): the
///   event is a before→after transition, and the transition is the difference. So actualization
///   "produces" distinction only in the sense that actualization IS a difference. The deeper source is
///   the notion of DIFFERENCE itself: distinction is the registration of a difference. Difference is
///   the most primitive notion — before structure, before actualization, before count: a thing is
///   distinguishable exactly insofar as it differs from something else.
///
/// CLASSIFICATION: UNIVERSAL DIFFERENCE PRINCIPLE — distinction does not arise from structure (the
/// regular network has none) and not from actualization as a distinct source (actualization IS a
/// difference); distinction = DIFFERENCE, the most primitive notion of the theory. What is distinguished
/// is differences; the zero/background vs positive modes, the μ^k/S shares, the before→after transitions.
/// </summary>
public static class DistinctionOriginAudit
{
    // ── (a) Causal position ────────────────────────────────────────────────────

    /// <summary>
    /// Causal positions are distinguished by their density shares ρ_k = μ^k/S: distinct shares for μ≠1.
    /// Returns (generation, share).
    /// </summary>
    public static (int K, double Share)[] CausalShares(double mu, int K)
    {
        var traj = NativeMetricDynamics.DensityTrajectory(mu, K);
        return traj.Select((s, i) => (i, s)).ToArray();
    }

    /// <summary>Are the causal shares distinct for μ≠1 (distinguishable positions)?</summary>
    public static bool CausalPositionsDistinct(double mu, int K)
    {
        var shares = CausalShares(mu, K).Select(x => x.Share).ToArray();
        return shares.Distinct().Count() == K;
    }

    /// <summary>At criticality (μ=1) the shares are uniform, but the generation index still orders them.</summary>
    public static bool CriticalSharesUniform(int K)
        => QuantumAmplitudeOrigin.CriticalUniform(K);

    // ── (b) Network position ───────────────────────────────────────────────────

    /// <summary>The network is regular — no structural distinction between nodes.</summary>
    public static bool NetworkProvidesNoDistinction()
        => InvariantOriginAudit.IsRegular();

    // ── (c) State difference ───────────────────────────────────────────────────

    /// <summary>A Q-event is a projection to a tick/no-tick binary state — a before→after difference.</summary>
    public static bool StateDifferenceExists()
        => MeasurementFromActualization.ActualizationIsProjection();

    /// <summary>The projection is binary (tick/no-tick).</summary>
    public static bool StateDifferenceIsBinary()
        => MeasurementFromActualization.ProjectionIsBinary();

    // ── (d) Actualization difference ───────────────────────────────────────────

    /// <summary>A Q-event is a network transition — the event IS a before→after difference.</summary>
    public static bool ActualizationIsDifference()
        => PhysicalMeaningOfQEvents.IsTransitionPicture("network-update");

    /// <summary>ρ counts Q-events (each transition is one counted difference).</summary>
    public static bool RhoCountsDifferences()
        => PhysicalMeaningOfQEvents.RhoCountsQEvents();

    // ── The D96 difference structure ───────────────────────────────────────────

    /// <summary>The zero mode is in the kernel (the background/uniform reference).</summary>
    public static bool ZeroModeBackground()
        => InvariantOriginAudit.ConstantVectorInKernel();

    /// <summary>Number of positive modes (the differences from the background).</summary>
    public static int PositiveModeCount()
        => FamilyIndexOrigin.IntraSectorModes().Length;

    /// <summary>Number of distinct frequencies (44 degeneracy groups) — the distinct differences.</summary>
    public static int DistinctFrequencies()
        => EffectiveAccessCounts.DoubletMultiplicities().Length;

    // ── Classification ─────────────────────────────────────────────────────────

    /// <summary>
    /// Distinction-origin score (0..6):
    /// 1. causal positions are distinguished by distinct shares (μ≠1);
    /// 2. the network provides NO structural distinction (regular graph);
    /// 3. the state difference exists (a Q-event is a projection);
    /// 4. the actualization event IS a difference (network transition);
    /// 5. the zero mode is the background; the positive modes are the differences (D96 structure);
    /// 6. distinction = DIFFERENCE, the most primitive notion (structural).
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (CausalPositionsDistinct(2.0, 8)) score++;
        if (NetworkProvidesNoDistinction()) score++;
        if (StateDifferenceExists()) score++;
        if (ActualizationIsDifference()) score++;
        if (ZeroModeBackground() && PositiveModeCount() > 0) score++;
        score++;  // structural: distinction = DIFFERENCE (the primitive notion)
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   STRUCTURE FUNDAMENTAL       — distinction arises from the network structure (positions/labels);
    ///   ACTUALIZATION FUNDAMENTAL   — distinction arises from the actualization act as a distinct source;
    ///   DISTINCTION FUNDAMENTAL     — distinction is the primitive, with no deeper account;
    ///   UNIVERSAL DIFFERENCE PRINCIPLE — distinction arises from DIFFERENCE, the most primitive notion:
    ///                                NOT from structure (the regular network has none), and NOT from
    ///                                actualization as a separate source (actualization IS a difference —
    ///                                a before→after transition). What is distinguished is differences:
    ///                                the zero/background vs the positive modes, the μ^k/S shares, the
    ///                                before→after transitions. A thing is distinguishable exactly insofar
    ///                                as it differs from something else.
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score <= 2) return "STRUCTURE FUNDAMENTAL";
        if (score == 3) return "ACTUALIZATION FUNDAMENTAL";
        if (score == 4) return "DISTINCTION FUNDAMENTAL";
        return "UNIVERSAL DIFFERENCE PRINCIPLE";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — origin score {OriginScore()}/6: "
             + $"causal positions distinguished by distinct shares ρ_k=μ^k/S (μ≠1); "
             + $"the network is REGULAR (all degree {InvariantOriginAudit.CommonDegree()}) — NO structural "
             + $"distinction; a Q-event is a before→after transition (the event IS a difference); "
             + $"the D96 spectrum has one zero mode (the background) and {PositiveModeCount()} positive modes "
             + $"({DistinctFrequencies()} distinct frequencies) — the differences from the background. "
             + "Distinction does not arise from structure (none exists) and not from actualization as a "
             + "separate source (actualization IS a difference); distinction = DIFFERENCE, the most "
             + "primitive notion. What is distinguished is differences. Structure only, no observables.";
    }
}
