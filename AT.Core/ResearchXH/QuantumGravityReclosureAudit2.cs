namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 221 — Quantum Gravity Reclosure Audit (re-run after the phase origin). Reviews the QG closure
/// chain QG215 (baseline audit), QG216 (amplitude magnitude), QG218 (complex structure), QG220 (phase origin).
/// Re-evaluates the six criteria. Audit only — no new physics, no new derivations. Deterministic.
///
/// THE DELTAS SINCE QG219:
///  • QG220 (PHASE ORIGIN): the quantum PHASE θ_k = 2π·k/N is DERIVED from Q-events — the circulation phase of
///    the actualization cycle (causal order → position k, cycle period N → quantum 2π/N, link orientation →
///    sign, connectivity → phase differences). This CLOSES the QG219 gap (a) — the phase ORIGIN.
///
/// RE-EVALUATED CRITERIA:
///  1. IS QM DERIVED? — YES, FULLY. Every component of the amplitude is now derived from Q-events:
///       • the MAGNITUDE |ψ|² = ρ = μ^k/S (QG216, branching counting measure);
///       • the PHASE θ_k = 2π·k/N (QG220, actualization circulation);
///       • the COMPLEX STRUCTURE ψ = |ψ|e^(iθ) → ℂ Hilbert space (QG218);
///       • the MEASUREMENT basis (QG74 MATCH).
///     The full amplitude ψ_k = √(μ^k/S)·e^(2πik/N) is Q-event-derived. No QM primitive remains.
///  2. IS GRAVITY DERIVED? — YES (unchanged; QG181-213).
///  3. SAME PRIMITIVE? — YES: both pillars derive from the network — gravity sources from the counting measure
///     ρ, the QM amplitude magnitude is |ψ|² = ρ, and the phase is the circulation of the same actualization
///     cycle (the rotational structure of the same ring C_N). One network, two pillars.
///  4. IS SPACETIME EMERGENT? — PARTIAL (unchanged): the metric g = ρ^(2/d)η is derived (QG207), but the metric
///     DYNAMICS (the BDG action) remains imported (QG6).
///  5. IS MATTER EMERGENT? — YES (unchanged; QG195/196/203-210).
///  6. ARE ESSENTIAL COMPONENTS OPEN? — PARTIALLY: the phase origin is RESOLVED (QG220). The two remaining
///     gaps are both in the GRAVITY/METRIC sector: (b) the native metric dynamics (BDG action imported, QG6)
///     and (c) the ψ origin status (capacity forced QG56, excitation derived QG57, PARTIAL). No QM gap remains.
///
/// SCORE (0..6, sub-scores allow partials):
///   1. QM derived                → 1.0 (fully derived: magnitude + phase + complex structure + measurement)
///   2. gravity derived           → 1.0
///   3. same primitive            → 1.0
///   4. spacetime emergent        → 0.5 (metric derived QG207, dynamics imported QG6)
///   5. matter emergent           → 1.0
///   6. no essential gaps         → 0.5 (phase origin resolved; metric dynamics + ψ origin remain)
///   TOTAL = 5.0/6.
///
/// CLASSIFICATION: NEAR-COMPLETE QG — both pillars are fully derived from the same network primitive (gravity
/// from ρ; QM magnitude |ψ|² = ρ, phase from the actualization circulation, complex structure forced), matter
/// is emergent, the metric is derived, and the QM pillar is CLOSED (no QM primitive remains). The only open
/// items are two gravity-sector closure issues (native metric dynamics and the ψ status), which make the
/// theory NEAR-COMPLETE rather than COMPLETE.
/// </summary>
public static class QuantumGravityReclosureAudit2
{
    // ── 1. The six criteria (re-evaluated) ────────────────────────────────────
    //
    // ⚠ ResearchY-G_026. These were six bare `bool` literals. `IsSpacetimeEmergent()` returned false while
    // `SpacetimeSubScore()` independently returned 0.5, so the two could never disagree in the tests. The
    // criteria are now one QgCriterion table (section 2) and these accessors are DERIVED from it.

    // ── 2. Sub-scores (DERIVED from Criteria — never typed) ───────────────────
    //
    // ⚠ ResearchY-G_026. These were hard-coded literals (QmSubScore() => 1.0, SpacetimeSubScore() => 0.5,
    // NoGapsSubScore() => 0.5) which did NOT read the criteria — so `IsSpacetimeEmergent()` could return
    // false while `SpacetimeSubScore()` still returned 0.5, and the two could never disagree in the tests.
    // Partial credit is now a declared QgStatus.Partial on the criterion itself, and the score is DERIVED.

    /// <summary>The six QG closure criteria of this phase, each with its status and cited evidence.</summary>
    public static QgCriterion[] Criteria() => new[]
    {
        new QgCriterion("QM derived", QgStatus.Full,
            "QG216 amplitude + QG218 complex structure + QG220 phase θ_k = 2πk/N"),
        new QgCriterion("gravity derived", QgStatus.Full,
            "QG181/207/209/213"),
        new QgCriterion("same primitive", QgStatus.Full,
            "gravity and |ψ|² both derive from ρ; the phase is the actualization circulation"),
        new QgCriterion("spacetime emergent", QgStatus.Partial,
            "metric structure derived (QG207) but the metric dynamics (BDG) is still imported (QG6) — partial"),
        new QgCriterion("matter emergent", QgStatus.Full,
            "QG195/196/203-210"),
        new QgCriterion("no essential gaps", QgStatus.Partial,
            "QG220 resolved the phase origin; two gravity-sector items remain (native metric dynamics, ψ origin) — partial"),
    };

    /// <summary>Does a named criterion hold outright? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool HoldsOf(string name)
        => Criteria().Any(c => c.Name == name && c.Holds);

    /// <summary>Is QM derived? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool IsQuantumMechanicsDerived() => HoldsOf("QM derived");

    /// <summary>Is gravity derived? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool IsGravityDerived() => HoldsOf("gravity derived");

    /// <summary>Same primitive? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool SamePrimitiveForBoth() => HoldsOf("same primitive");

    /// <summary>Is spacetime emergent? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool IsSpacetimeEmergent() => HoldsOf("spacetime emergent");

    /// <summary>Is matter emergent? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool IsMatterEmergent() => HoldsOf("matter emergent");

    /// <summary>Are essential components still open? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool EssentialComponentsOpen() => !HoldsOf("no essential gaps");

    /// <summary>QM sub-score — DERIVED from the criterion (Full = 1.0).</summary>
    public static double QmSubScore() => ScoreOf("QM derived");

    /// <summary>Gravity sub-score — DERIVED from the criterion.</summary>
    public static double GravitySubScore() => ScoreOf("gravity derived");

    /// <summary>Same-primitive sub-score — DERIVED from the criterion.</summary>
    public static double SamePrimitiveSubScore() => ScoreOf("same primitive");

    /// <summary>Spacetime-emergent sub-score — DERIVED: Partial (0.5), because the dynamics is imported.</summary>
    public static double SpacetimeSubScore() => ScoreOf("spacetime emergent");

    /// <summary>Matter-emergent sub-score — DERIVED from the criterion.</summary>
    public static double MatterSubScore() => ScoreOf("matter emergent");

    /// <summary>No-gaps sub-score — DERIVED: Partial (0.5), two gravity-sector items remain.</summary>
    public static double NoGapsSubScore() => ScoreOf("no essential gaps");

    /// <summary>The derived score of a named criterion (0 if unknown).</summary>
    public static double ScoreOf(string name)
        => Criteria().FirstOrDefault(c => c.Name == name)?.Score ?? 0.0;

    /// <summary>The six sub-scores, labelled. Derived from <see cref="Criteria"/>.</summary>
    public static (string Criterion, double Score)[] SubScores()
        => Criteria().Select(c => (c.Name, c.Score)).ToArray();

    /// <summary>Total score (0..6) — the sum of the derived per-criterion scores.</summary>
    public static double TotalScore()
        => QgClosure.TotalScore(Criteria());

    /// <summary>Do all criteria cite the phase audit carrying their evidence?</summary>
    public static bool AllBasesCited() => QgClosure.AllBasesCited(Criteria());

    /// <summary>Consistency self-check: the classification must FOLLOW from the criteria table.</summary>
    public static bool ClassificationFollowsFromCriteria()
        => QgClosure.ClassificationFollowsFromCriteria(Classify(), Criteria());

    /// <summary>Are the sub-scores consistent with the criteria? (Asserts the decoupling is gone.)</summary>
    public static bool SubScoresMatchCriteria()
    {
        var crit = Criteria();
        var subs = SubScores();
        return crit.Length == subs.Length
               && crit.Zip(subs).All(p => p.First.Name == p.Second.Criterion
                                          && Math.Abs(p.First.Score - p.Second.Score) < 1e-12);
    }

    // ── 3. The deltas since QG215 / QG219 ─────────────────────────────────────

    /// <summary>The closure chain (QG215 → QG219 → QG221).</summary>
    public static string[] ReclosureDeltas() => new[]
    {
        "QG215: PARTIAL QG — QM not derived (amplitude/phase was a new primitive)",
        "QG216 AMPLITUDE ORIGIN: |ψ|² = ρ = μ^k/S derived from Q-events — the magnitude is no longer a primitive",
        "QG218 HILBERT ORIGIN: complex structure derived — magnitude + phase = complex number, Hilbert space over ℂ",
        "QG220 PHASE ORIGIN: θ_k = 2π·k/N derived from the actualization circulation — the phase is no longer a primitive",
    };

    /// <summary>The QG219 gaps and their QG221 status.</summary>
    public static (string Gap, string Status)[] GapStatuses() => new[]
    {
        ("(a) phase origin — located (QG63) but value/mechanism not derived", "RESOLVED by QG220 (PHASE ORIGIN: θ_k = 2πk/N)"),
        ("(b) native metric dynamics — BDG action imported (QG6), not derived", "OPEN (gravity/metric sector)"),
        ("(c) ψ origin status — capacity forced (QG56), excitation derived (QG57), PARTIAL", "PARTIAL (gravity/metric sector)"),
    };

    /// <summary>Number of remaining (non-resolved) gaps.</summary>
    public static int RemainingGapCount()
        => GapStatuses().Count(g => !g.Status.StartsWith("RESOLVED"));

    // ── 4. Classification ─────────────────────────────────────────────────────

    /// <summary>
    /// QG classification by total score (0..6):
    ///   &lt; 3.0        → PARTIAL QG — a pillar is missing or the pillars use different primitives;
    ///   3.0 – 4.5     → EFFECTIVE QG — both pillars derived from a common primitive, substantial gaps remain;
    ///   5.0 – 5.5     → NEAR-COMPLETE QG — both pillars fully derived from the same primitive, QM closed,
    ///                   only gravity-sector closure items remain;
    ///   6.0          → COMPLETE QG — all six criteria fully hold.
    /// </summary>
    public static string Classify()
    {
        double score = TotalScore();
        if (score >= 6.0) return "COMPLETE QG";
        if (score >= 5.0) return "NEAR-COMPLETE QG";
        if (score >= 3.0) return "EFFECTIVE QG";
        return "PARTIAL QG";
    }

    /// <summary>The QG215/QG219/QG221 progression, each phase reporting its OWN derived total.</summary>
    public static (string Phase, string Status, double Score)[] Progression() => new[]
    {
        ("QG215", QuantumGravityClosureAudit.Classify(), QuantumGravityClosureAudit.TotalScore()),
        ("QG219", QuantumGravityReclosureAudit.Classify(), QuantumGravityReclosureAudit.TotalScore()),
        ("QG221", Classify(), TotalScore()),
    };
}
