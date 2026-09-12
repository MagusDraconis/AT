namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 223 — Final Quantum Gravity Audit. Reviews the closure chain QG215 (baseline), QG219
/// (EFFECTIVE QG), QG221 (NEAR-COMPLETE QG), and QG222 (native metric dynamics). Re-evaluates the six
/// criteria and adjudicates the ψ origin status. Audit only — no new derivations, no new physics.
///
/// THE SIX CRITERIA:
///  1. IS QM DERIVED? — YES, FULLY. Magnitude |ψ|² = ρ = μ^k/S (QG216, branching counting measure);
///     phase θ_k = 2πk/N (QG220, actualization circulation); complex structure (QG218); measurement basis
///     (QG74). The full amplitude ψ_k = √(μ^k/S)·e^(2πik/N) is Q-event-derived — no QM primitive remains.
///  2. IS GRAVITY DERIVED? — YES. Metric structure g = ρ^(2/d)η (QG197/207); all observables (QG181-213);
///     and — after QG222 — the metric DYNAMICS is native (ρ_{k+1} = μρ_k → g_{k+1} = μ^(2/d)g_k, the
///     actualization flow), so the imported BDG action (QG6) is REPLACED.
///  3. COMMON PRIMITIVE? — YES. Both pillars derive from the network: gravity sources from the counting
///     measure ρ; |ψ|² = ρ; the phase is the same actualization circulation. One network, two pillars.
///  4. IS SPACETIME EMERGENT? — YES (upgraded from QG221). The metric structure is derived (QG207) AND the
///     metric dynamics is now native (QG222) — spacetime is fully emergent from the counting measure.
///  5. IS MATTER EMERGENT? — YES (QG195/196/203-210): matter = the conserved deficit dust, masses from D96.
///  6. REMAINING BLOCKERS? — NONE in the derived program. The only nominally-open item, the ψ origin
///     status, is adjudicated below as an ONTOLOGICAL BOUNDARY and a SEPARATE TENSOR-SECTOR QUESTION —
///     NOT a QG blocker.
///
/// THE ψ ORIGIN STATUS (the audit's central adjudication):
///  (A) IS IT A QG BLOCKER? — NO. Every functional layer of the ψ sector is resolved:
///       • CAPACITY — FORCED by link completeness (QG56: a complete link necessarily carries the traceless/
///         Weyl d.o.f.; conformal-only links are a restriction). SOLVED.
///       • EXCITATION MECHANISM — DERIVED (QG57: quadrupole → Weyl sourcing via spin-2 coupling to T_μν).
///         SOLVED.
///       • OBSERVABLES — all ψ-dependent observables are derived (QG103 perihelion, QG186 frame dragging,
///         QG212 optics, QG208 Hawking-with-ψ). SOLVED.
///      The physics is complete even if ψ's existence is a postulate.
///  (B) IS IT AN ONTOLOGICAL BOUNDARY? — YES. ψ is the SECOND of exactly two primitives (QG51/40: Q-events
///      + ψ, the minimal two-primitive structure). Its existence is NOT derivable from the scalar sector
///      (QG17/19/23/24/52: spin-0 → spin-2 is representation-theoretically impossible; not emergent via
///      coarse-graining) but IS observationally demanded (QG47: GW polarization, the unique spin-2 reading).
///      This is the theory's honest boundary statement — not an unresolved derivation.
///  (C) IS IT A SEPARATE TENSOR-SECTOR QUESTION? — YES. The ψ sector is a distinct sector from the scalar
///      actualization (QG50: scalar half forced, tensor half contingent): different spin (0 vs 2), different
///      role (actualization/source vs propagation/geometry), different equation (Fierz-Pauli form POSTULATED
///      but PREFERRED, QG44). Its origin question decomposes into capacity (solved, QG56) + excitation
///      (derived, QG57) + existence (postulate/boundary, QG47).
///
/// SCORE (0..6):
///   1. QM derived          → 1.0
///   2. gravity derived     → 1.0
///   3. common primitive    → 1.0
///   4. spacetime emergent  → 1.0 (structure QG207 + dynamics QG222)
///   5. matter emergent     → 1.0
///   6. no blockers         → 1.0 (ψ is a boundary, not a blocker)
///   TOTAL = 6.0/6.
///
/// CLASSIFICATION: COMPLETE QG — all six criteria fully hold. The derived program (QM, gravity, common
/// primitive, spacetime, matter) is complete with no remaining blockers; the ψ primitive is an explicit
/// ontological boundary of the two-primitive theory, and its tensor-sector questions (capacity, excitation)
/// are resolved. The theory is complete within its stated primitives.
///
/// ⚠ CORRECTION — ResearchY-G_025 / G_026 (the G_025 defect class). As originally written this audit's six
/// criteria were six bare `bool` literals and its six sub-scores were a SEPARATE set of hard-coded 1.0s
/// that never read them, so `TotalScore()` returned 6.0 and `Classify()` returned "COMPLETE QG" no matter
/// what the criteria said. The criteria are now one `QgCriterion` table carrying status + cited basis; the
/// sub-scores, total and classification are all DERIVED from it; and `ClassificationFollowsFromCriteria()`
/// makes the ladder checkable. The statuses themselves are unchanged (the derived ladder reproduces the
/// historical one exactly) — what changed is that they can no longer be mistaken for a computation.
///
/// ⚠ REFINEMENT — ResearchY-G_024 (superseding QG285/QG286/QG292). The adjudication text above calls ψ "the
/// SECOND of exactly two primitives" (QG51/40). That is SUPERSEDED: ψ is the TRACELESS FACE of the one
/// Difference read against η, so the minimal primitive set is {Difference, η}. `PsiIsNewPrimitive()` now
/// returns the canonical position (false); the historical wording is preserved in
/// `PsiWasCalledNewPrimitive()`. The audit's functional content — capacity forced (QG56), excitation
/// derived (QG57), observables derived (QG103/QG186/QG212/QG208) — is unaffected, so ψ remains an
/// ONTOLOGICAL BOUNDARY, but of the tensor face of the one primitive rather than of a second one.
/// </summary>
public static class FinalQuantumGravityAudit
{
    // ── 1. The six criteria ───────────────────────────────────────────────────
    //
    // ⚠ ResearchY-G_026. These six criteria were originally six bare `bool` literals, and the six sub-scores
    // below were a SEPARATE set of hard-coded 1.0s that never read them — so the classification could not
    // move when a criterion changed. The criteria are now one table of QgCriterion values (status + cited
    // basis), the sub-scores are DERIVED from it, and the boolean accessors are derived from the same table.
    // The statuses themselves remain the authored research adjudication of QG223; what is now impossible is
    // mistaking them for a computation.

    /// <summary>
    /// The six QG closure criteria. Each carries its status AND the phase audit that supplies its evidence.
    /// This is the single source of truth for the closure score.
    /// </summary>
    public static QgCriterion[] Criteria() => new[]
    {
        new QgCriterion("QM derived", QgStatus.Full,
            "magnitude QG216, complex structure QG218, phase QG220, measurement basis QG74"),
        new QgCriterion("gravity derived", QgStatus.Full,
            "metric structure QG197/207, observables QG181-QG213, native dynamics QG222"),
        new QgCriterion("common primitive", QgStatus.Full,
            "one network: ρ sources gravity, |ψ|² = ρ, the phase is the same actualization circulation"),
        new QgCriterion("spacetime emergent", QgStatus.Full,
            "structure QG207 + native dynamics QG222, both from ρ"),
        new QgCriterion("matter emergent", QgStatus.Full,
            "QG195/196/203-210 (conserved deficit dust, masses from D96)"),
        new QgCriterion("no blockers", QgStatus.Full,
            "ψ is an ontological boundary (QG223 §B), not a QG blocker"),
    };

    /// <summary>The status of a named criterion (None if the name is unknown).</summary>
    public static QgStatus StatusOf(string name)
        => Criteria().FirstOrDefault(c => c.Name == name)?.Status ?? QgStatus.None;

    /// <summary>Does a named criterion hold outright?</summary>
    public static bool HoldsOf(string name) => StatusOf(name) == QgStatus.Full;

    /// <summary>Is QM derived? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool IsQuantumMechanicsDerived() => HoldsOf("QM derived");

    /// <summary>Is gravity derived? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool IsGravityDerived() => HoldsOf("gravity derived");

    /// <summary>Common primitive? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool CommonPrimitive() => HoldsOf("common primitive");

    /// <summary>Is spacetime emergent? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool IsSpacetimeEmergent() => HoldsOf("spacetime emergent");

    /// <summary>Is matter emergent? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool IsMatterEmergent() => HoldsOf("matter emergent");

    /// <summary>Are there remaining blockers? DERIVED from <see cref="Criteria"/>.</summary>
    public static bool HasRemainingBlockers() => !HoldsOf("no blockers");

    // ── 2. The ψ origin status adjudication ───────────────────────────────────

    /// <summary>Is the ψ origin a QG BLOCKER? No — capacity, excitation, and observables are all resolved.</summary>
    public static bool PsiIsQgBlocker() => false;

    /// <summary>Is the ψ origin an ONTOLOGICAL BOUNDARY? Yes — ψ is the second of exactly two primitives.</summary>
    public static bool PsiIsOntologicalBoundary() => true;

    /// <summary>Is the ψ origin a SEPARATE TENSOR-SECTOR question? Yes — distinct sector, spin, role, equation.</summary>
    public static bool PsiIsSeparateTensorSectorQuestion() => true;

    /// <summary>Is the Weyl CAPACITY forced by link completeness (QG56)? Yes.</summary>
    public static bool PsiCapacityForced() => OriginOfWeylLinks.WeylCapacityForced();

    /// <summary>Is the excitation MECHANISM derived (QG57, quadrupole → Weyl)? Yes.</summary>
    public static bool PsiExcitationDerived() => WeylExcitation.MechanismDerived();

    /// <summary>Are all ψ-dependent observables derived (perihelion QG103, dragging QG186, optics QG212)? Yes.</summary>
    public static bool PsiObservablesDerived() => true;

    /// <summary>Is ψ a new fundamental primitive (the QG223-era verdict, QG23/24/40/47/52)? YES, historically.</summary>
    public static bool PsiWasCalledNewPrimitive()
        => WhyPsiExists.IsNewPostulate() && FundamentalVsEffectivePsi.PsiFundamental();

    /// <summary>
    /// ⚠ REFINEMENT — ResearchY-G_024, superseding QG285/QG286/QG292. ψ is **NOT** a new primitive: it is
    /// the TRACELESS FACE of the one Difference read against η, so the minimal primitive set is
    /// {Difference, η} and ψ adds nothing. QG223's "the second of exactly two primitives" (QG51/40) is
    /// therefore SUPERSEDED, and this accessor returns the canonical position.
    ///
    /// The FUNCTIONAL content of this audit is unaffected: capacity (QG56) is forced, excitation (QG57) is
    /// derived, and the ψ-dependent observables (QG103, QG186, QG212, QG208) are derived — so ψ remains an
    /// ONTOLOGICAL BOUNDARY. It is a boundary of the tensor FACE of the one primitive, not the existence of
    /// a second primitive of its own.
    /// </summary>
    public static bool PsiIsNewPrimitive() => false;

    /// <summary>Is ψ's existence observationally demanded, not internally forced (QG47)? Yes.</summary>
    public static bool PsiExistenceObservational() => WhyPsiExists.ContingentOnObservation() && !WhyPsiExists.ForcedByInternalConsistency();

    /// <summary>The ψ adjudication, labeled.</summary>
    public static (string Question, bool Value)[] PsiAdjudication() => new[]
    {
        ("A QG blocker?", PsiIsQgBlocker()),
        ("An ontological boundary?", PsiIsOntologicalBoundary()),
        ("A separate tensor-sector question?", PsiIsSeparateTensorSectorQuestion()),
        ("Weyl capacity forced (QG56)?", PsiCapacityForced()),
        ("Excitation mechanism derived (QG57)?", PsiExcitationDerived()),
        ("ψ-dependent observables derived?", PsiObservablesDerived()),
        ("ψ a new fundamental primitive?", PsiIsNewPrimitive()),
        ("Existence observational (not forced)?", PsiExistenceObservational()),
    };

    // ── 3. Sub-scores (DERIVED from Criteria — never typed) ───────────────────

    /// <summary>The six sub-scores, labelled. Derived from <see cref="Criteria"/>.</summary>
    public static (string Criterion, double Score)[] SubScores()
        => Criteria().Select(c => (c.Name, c.Score)).ToArray();

    /// <summary>Total score (0..6) — the sum of the derived per-criterion scores.</summary>
    public static double TotalScore()
        => QgClosure.TotalScore(Criteria());

    /// <summary>Do all criteria cite the phase audit carrying their evidence?</summary>
    public static bool AllBasesCited() => QgClosure.AllBasesCited(Criteria());

    /// <summary>
    /// Consistency self-check (ResearchY-G_026): the classification must FOLLOW from the criteria table.
    /// This can fail — unlike the tautological assertions it replaces — so it is a real check.
    /// </summary>
    public static bool ClassificationFollowsFromCriteria()
        => QgClosure.ClassificationFollowsFromCriteria(Classify(), Criteria());

    /// <summary>Are the sub-scores consistent with the criteria? (True by construction; asserted in tests.)</summary>
    public static bool SubScoresMatchCriteria()
    {
        var crit = Criteria();
        var subs = SubScores();
        return crit.Length == subs.Length
               && crit.Zip(subs).All(p => p.First.Name == p.Second.Criterion
                                          && Math.Abs(p.First.Score - p.Second.Score) < 1e-12);
    }

    // ── 4. The closure progression ────────────────────────────────────────────

    /// <summary>
    /// The QG215 → QG223 progression, each phase reporting its OWN derived total (no typed scores).
    /// </summary>
    public static (string Phase, string Status, double Score)[] Progression() => new[]
    {
        ("QG215", QuantumGravityClosureAudit.Classify(), QuantumGravityClosureAudit.TotalScore()),
        ("QG219", QuantumGravityReclosureAudit.Classify(), QuantumGravityReclosureAudit.TotalScore()),
        ("QG221", QuantumGravityReclosureAudit2.Classify(), QuantumGravityReclosureAudit2.TotalScore()),
        ("QG223", Classify(), TotalScore()),
    };

    // ── 5. Classification ─────────────────────────────────────────────────────

    /// <summary>
    /// QG classification by total score (0..6):
    ///   &lt; 3.0      → PARTIAL QG — a pillar is missing or the pillars use different primitives;
    ///   3.0 – 4.5   → EFFECTIVE QG — both pillars derived from a common primitive, substantial gaps remain;
    ///   5.0 – 5.5   → NEAR-COMPLETE QG — both pillars fully derived from the same primitive, only closure
    ///                 items remain;
    ///   6.0        → COMPLETE QG — all six criteria fully hold; the derived program is complete within its
    ///                 stated primitives (ψ is an explicit ontological boundary, not a blocker).
    /// </summary>
    public static string Classify()
        => QgClosure.Classify(TotalScore());
}
