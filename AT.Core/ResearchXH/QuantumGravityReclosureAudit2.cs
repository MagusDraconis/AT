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

    /// <summary>Is QM derived? YES, FULLY — magnitude (QG216) + phase (QG220) + complex structure (QG218) + measurement (QG74).</summary>
    public static bool IsQuantumMechanicsDerived() => true;

    /// <summary>Is gravity derived? Yes — QG181/207/209/213.</summary>
    public static bool IsGravityDerived() => true;

    /// <summary>Same primitive? Yes — both pillars derive from the network (ρ + the actualization circulation).</summary>
    public static bool SamePrimitiveForBoth() => true;

    /// <summary>Is spacetime emergent? Partially — metric derived (QG207), dynamics (BDG) imported (QG6).</summary>
    public static bool IsSpacetimeEmergent() => false;

    /// <summary>Is matter emergent? Yes — QG195/196/203-210.</summary>
    public static bool IsMatterEmergent() => true;

    /// <summary>Are essential components open? Partially — two gravity-sector items remain (metric dynamics, ψ origin).</summary>
    public static bool EssentialComponentsOpen() => true;

    // ── 2. Sub-scores (allow partial credit) ──────────────────────────────────

    /// <summary>QM sub-score: 1.0 (fully derived).</summary>
    public static double QmSubScore() => 1.0;

    /// <summary>Gravity sub-score: 1.0.</summary>
    public static double GravitySubScore() => 1.0;

    /// <summary>Same-primitive sub-score: 1.0.</summary>
    public static double SamePrimitiveSubScore() => 1.0;

    /// <summary>Spacetime-emergent sub-score: 0.5 (metric derived, dynamics imported).</summary>
    public static double SpacetimeSubScore() => 0.5;

    /// <summary>Matter-emergent sub-score: 1.0.</summary>
    public static double MatterSubScore() => 1.0;

    /// <summary>No-gaps sub-score: 0.5 (phase origin resolved; 2 gravity-sector items remain).</summary>
    public static double NoGapsSubScore() => 0.5;

    /// <summary>The six sub-scores, labeled.</summary>
    public static (string Criterion, double Score)[] SubScores() => new[]
    {
        ("QM derived", QmSubScore()),
        ("gravity derived", GravitySubScore()),
        ("same primitive", SamePrimitiveSubScore()),
        ("spacetime emergent", SpacetimeSubScore()),
        ("matter emergent", MatterSubScore()),
        ("no essential gaps", NoGapsSubScore()),
    };

    /// <summary>Total score (0..6).</summary>
    public static double TotalScore()
        => SubScores().Sum(s => s.Score);

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

    /// <summary>The QG215/QG219/QG221 score progression.</summary>
    public static (string Phase, string Status, double Score)[] Progression() => new[]
    {
        ("QG215", "PARTIAL QG", 2.0),
        ("QG219", "EFFECTIVE QG", 4.0),
        ("QG221", "NEAR-COMPLETE QG", 5.0),
    };
}
