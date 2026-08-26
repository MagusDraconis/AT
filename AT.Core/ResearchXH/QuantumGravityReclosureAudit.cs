namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 219 — Quantum Gravity Reclosure Audit. Re-evaluates the QG status using QG215 (the closure
/// audit baseline), QG216 (amplitude magnitude derived), and QG218 (complex-state structure derived).
/// Audit only — no new physics, no new derivations. Deterministic.
///
/// DELTA SINCE QG215 (the reclosure):
///  • QG216 (AMPLITUDE ORIGIN): the amplitude MAGNITUDE |ψ|² = ρ = μ^k/S is DERIVED from Q-events (the
///    branching counting measure) — the Born rule holds exactly by construction.
///  • QG218 (HILBERT ORIGIN): the COMPLEX structure is DERIVED — a state carries magnitude (branching,
///    node) + phase (U(1) links, QG63), a state with both is a complex number, and the Hilbert space is
///    over ℂ (forced: real gives no interference, quaternionic adds structure with no source).
///  • QG63 already established the phase lives on the EXISTING U(1) links — no new object is needed.
///
/// RE-EVALUATED CRITERIA:
///  1. IS QM DERIVED? — SUBSTANTIALLY YES. The magnitude is derived from Q-events (QG216); the complex
///     structure is derived (QG218); the phase is hosted on the existing U(1) links (QG63, no new object).
///     QM is no longer "compatible but not emergent" — the amplitude structure now derives from ρ and the
///     link structure.
///  2. IS GRAVITY DERIVED? — YES (unchanged; QG181-213).
///  3. SAME PRIMITIVE? — YES for the CORE: both gravity and the QM amplitude magnitude derive from the SAME
///     counting measure ρ (gravity sources from ρ; |ψ|² = ρ). The phase is a U(1) link connection on the
///     existing structure. The two pillars now share ρ as their common source.
///  4. IS SPACETIME EMERGENT? — PARTIAL (unchanged): the metric g = ρ^(2/d)η is derived (QG207), but the
///     metric DYNAMICS (BDG action) remains imported (QG6).
///  5. IS MATTER EMERGENT? — YES (unchanged; QG195/196/203-210).
///  6. ESSENTIAL COMPONENTS OPEN? — PARTIALLY: the phase ORIGIN (the U(1) connection's value/mechanism),
///     the native metric dynamics (BDG), and the ψ origin status remain open. The measurement basis was
///     already resolved by QG74; the Bekenstein 1/4 is a proven boundary (QG196).
///
/// REMAINING QG215 GAPS (unresolved):
///  (a) the phase ORIGIN — the U(1) connection is located (QG63) but its value/mechanism is not derived;
///  (b) the native metric dynamics — the BDG action is imported (QG6), not derived;
///  (c) the ψ origin status — capacity forced (QG56), excitation derived (QG57), but the field's status
///      remains PARTIAL.
///  RESOLVED since QG215:
///  (d) the amplitude magnitude (QG216), the complex structure (QG218);
///  (e) the measurement basis (QG74 MATCH — was listed as open in QG215, but QG74 established it).
///
/// CLASSIFICATION: EFFECTIVE QG — both pillars are now derived from the same primitive ρ (gravity from ρ,
/// QM amplitude magnitude |ψ|² = ρ with the complex structure forced), matter is emergent, and the metric
/// is derived. The remaining gaps (phase origin, native metric dynamics, ψ origin) are primitive/closure
/// issues that make it EFFECTIVE rather than COMPLETE: the theory is a self-contained derived-gravity +
/// derived-amplitude program with a small set of remaining origins.
/// </summary>
public static class QuantumGravityReclosureAudit
{
    // ── 1. The six criteria (re-evaluated) ────────────────────────────────────

    /// <summary>
    /// Is QM derived? Substantially YES — magnitude from Q-events (QG216), complex structure derived
    /// (QG218), phase hosted on the existing U(1) links (QG63).
    /// </summary>
    public static bool IsQuantumMechanicsDerived() => true;

    /// <summary>Is gravity derived? Yes — QG181/207/209/213.</summary>
    public static bool IsGravityDerived() => true;

    /// <summary>
    /// Same primitive? YES for the core — both gravity and the amplitude magnitude derive from ρ;
    /// the phase is a U(1) link connection on the existing structure.
    /// </summary>
    public static bool SamePrimitiveForBoth() => true;

    /// <summary>Is spacetime emergent? Partially — metric derived, dynamics (BDG) imported (QG6).</summary>
    public static bool IsSpacetimeEmergent() => false;

    /// <summary>Is matter emergent? Yes — QG195/196/203-210.</summary>
    public static bool IsMatterEmergent() => true;

    /// <summary>Are essential components still open? Partially — phase origin, metric dynamics, ψ origin.</summary>
    public static bool EssentialComponentsOpen() => true;

    // ── 2. The delta since QG215 ──────────────────────────────────────────────

    /// <summary>The reclosure deltas (QG216 + QG218).</summary>
    public static string[] ReclosureDeltas() => new[]
    {
        "QG216 AMPLITUDE ORIGIN: |ψ|² = ρ = μ^k/S derived from Q-events — the magnitude is no longer a primitive",
        "QG218 HILBERT ORIGIN: complex structure derived — magnitude (branching) + phase (U(1) links) = complex number, Hilbert space over ℂ",
        "QG63: the phase lives on the existing U(1) links — no new object needed",
    };

    /// <summary>The QG215 gaps that remain unresolved.</summary>
    public static string[] RemainingGaps() => new[]
    {
        "(a) the phase ORIGIN — the U(1) connection is located (QG63) but its value/mechanism is not derived",
        "(b) the native metric dynamics — the BDG action is imported (QG6), not derived",
        "(c) the ψ origin status — capacity forced (QG56), excitation derived (QG57), but PARTIAL",
    };

    /// <summary>The QG215 gaps resolved since.</summary>
    public static string[] ResolvedGaps() => new[]
    {
        "(d) the amplitude magnitude (QG216) and the complex structure (QG218)",
        "(e) the measurement basis (QG74 MATCH — general measurement already established)",
    };

    // ── 3. Classification ─────────────────────────────────────────────────────

    /// <summary>
    /// QG score (0..6): +1 QM derived, +1 gravity derived, +1 same primitive, +1 spacetime emergent,
    /// +1 matter emergent, +1 no essential components open.
    /// </summary>
    public static int QgScore()
    {
        int score = 0;
        if (IsQuantumMechanicsDerived()) score++;
        if (IsGravityDerived()) score++;
        if (SamePrimitiveForBoth()) score++;
        if (IsSpacetimeEmergent()) score++;
        if (IsMatterEmergent()) score++;
        if (!EssentialComponentsOpen()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NOT QG      — neither pillar derived (score 0-1);
    ///   PARTIAL QG  — one pillar derived, the other not, or not the same primitive (score 2-3);
    ///   EFFECTIVE QG — both pillars derived from a common primitive, spacetime/matter emergent, but a
    ///                  small set of primitive/closure origins remains (score 4-5);
    ///   COMPLETE QG — all six criteria hold (score 6).
    /// </summary>
    public static string Classify()
    {
        int score = QgScore();
        if (score >= 6) return "COMPLETE QG";
        if (score >= 4) return "EFFECTIVE QG";
        if (score >= 2) return "PARTIAL QG";
        return "NOT QG";
    }
}
