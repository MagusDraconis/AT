namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-MONO005 — Hostile Referee Audit of the Final Canonical Monograph Structure. Assume submission of
/// the MONO004 17-chapter structure for publication. Search ONLY for theory-architecture defects:
/// logical circularity, dependency violations, hidden assumptions, boundary leakage, and unsupported
/// completeness claims. Style, grammar, and missing citations are ignored. Audit only — no new physics,
/// no theory extension, no speculation.
///
/// METHOD: a hostile referee attacks the ARCHITECTURE of the monograph (its classification, its
/// dependency graph, and the placement of boundary content). Each finding records:
///   id        — A01..A09
///   area      — CIRCULARITY / DEPENDENCY / ASSUMPTION / LEAKAGE / COMPLETENESS
///   severity  — CRITICAL / MAJOR / MINOR
///   challenge — the referee's attack on the architecture
///   target    — the chapter / claim attacked
///   correction — the required architectural correction before Zenodo
///
/// THE FINDINGS (see Catalog):
///   A01 [CRITICAL, DEPENDENCY] — MONO004's canonical foundation {Difference, η} conflicts with the
///     cited QG318(2) Final Theory Architecture, which classified {Difference, Actualization, η} as
///     THREE FOUNDATIONAL primitives. The monograph cites the architecture phase but silently demotes
///     Actualization from primitive to derived step. This is an undeclared dependency change: either
///     Actualization is primitive (as QG318-2 classified it) or the monograph must state the demotion
///     explicitly as a canonical decision.
///   A02 [MAJOR, CIRCULARITY] — Chapter 1 classifies Difference as "Derived — from the Closure
///     Principle" while the canonical foundation declares Difference the fundamental primitive /
///     ontological boundary. A primitive cannot be derived from a principle: if the Closure Principle
///     derives Difference's boundary, the Closure Principle itself is an unexplained prior. The
///     chapter must classify Difference as Boundary/Foundational and present the Closure Principle as a
///     characterization, not a derivation.
///   A03 [MAJOR, COMPLETENESS] — The Executive Summary states "no remaining open physics derivation
///     frontier" unqualified, while the Boundary Layer (hosted SM dynamics QG242/245, Bekenstein 2π,
///     ψ status) qualifies it. A standalone unqualified completeness claim is unsupported; the summary
///     must carry the boundary qualification.
///   A04 [MAJOR, LEAKAGE] — Chapter 10 (Gravity, classified Emergent) contains the Bekenstein 1/4
///     boundary item inside an emergent chapter, while the Boundary Layer is Part VI (ch16). Boundary
///     content must be removed to ch16 or the chapter must be annotated "Emergent with disclosed
///     boundary" — the structure table currently disagrees with its content.
///   A05 [MAJOR, ASSUMPTION] — "No fifth operator" (ch7) is asserted from QG307/308, which searched a
///     finite set of unexplored domains. Absence-of-evidence over a searched space is presented as an
///     existence proof; it must be qualified as "no fifth operator found in any searched domain."
///   A06 [MINOR, DEPENDENCY] — Chapter 3 sources the operator layer (QG260-263) whose canonical home is
///     Chapter 7 (Part III Spectrum). Forward-referencing operators in the dynamics chapter before the
///     spectrum chapter misplaces the dependency order dynamics → spectrum → operators.
///   A07 [MINOR, LEAKAGE] — Chapter 2 (Tensor Reference η, classified Boundary) primarily presents the
///     second PRIMITIVE η (foundational) alongside π (boundary). The classification conflates a
///     primitive with a boundary constant; it should be split or annotated.
///   A08 [MINOR, ASSUMPTION] — The lock-law universality and "organization is a phase transition"
///     claims derive from synthetic deterministic evolving-law cohorts (QG315-317); the Executive
///     Summary presents them as universal without the model-cohort scope disclosure.
///   A09 [MINOR, COMPLETENESS] — Chapter 15 (Validation) cites MONO001 (v1.0 18-chapter structure)
///     without noting that MONO001 is superseded by MONO004 — a mild currency/completeness issue.
///
/// VERDICT: a CRITICAL finding (A01) blocks publication. The verdict is FAIL with required corrections
/// before Zenodo. All corrections are documentation/architecture-level — no physics changes.
/// </summary>
public static class CanonicalMonographRefereeAudit
{
    public enum Area { Circularity, Dependency, Assumption, Leakage, Completeness }
    public enum Severity { Critical, Major, Minor }

    /// <summary>An architecture finding.</summary>
    public sealed record Finding(
        string Id,
        Area Area,
        Severity Severity,
        string Challenge,
        string Target,
        string Correction);

    /// <summary>The hostile-referee architecture findings.</summary>
    public static Finding[] Catalog() => new[]
    {
        new Finding("A01", Area.Dependency, Severity.Critical,
            "MONO004's canonical foundation {Difference, eta} conflicts with the cited QG318(2) Final Theory Architecture, which classified {Difference, Actualization, eta} as THREE FOUNDATIONAL primitives. The monograph silently demotes Actualization from primitive to derived step.",
            "Part I Foundation / canonical core chain",
            "State explicitly whether Actualization is primitive or derived; reconcile the primitive count with QG318(2) or declare the demotion a canonical decision with its source."),

        new Finding("A02", Area.Circularity, Severity.Major,
            "Chapter 1 classifies Difference as 'Derived - from the Closure Principle' while Difference is declared the fundamental primitive / ontological boundary. A primitive cannot be derived from a principle: the Closure Principle would be an unexplained prior.",
            "Chapter 1 classification",
            "Reclassify Chapter 1 as Boundary/Foundational; present the Closure Principle as a characterization of the boundary, not a derivation of the primitive."),

        new Finding("A03", Area.Completeness, Severity.Major,
            "The Executive Summary states 'no remaining open physics derivation frontier' unqualified, while the Boundary Layer (hosted SM dynamics QG242/245, Bekenstein 2pi, psi status) qualifies it.",
            "Executive Summary",
            "Carry the boundary qualification into the summary statement; do not state completeness before the boundary disclosure."),

        new Finding("A04", Area.Leakage, Severity.Major,
            "Chapter 10 (Gravity, classified Emergent) contains the Bekenstein 1/4 boundary item inside an emergent chapter while the Boundary Layer is Part VI (ch16).",
            "Chapter 10 classification vs content",
            "Move the Bekenstein boundary item to ch16 or annotate Chapter 10 as 'Emergent with disclosed boundary'; the structure table must agree with its content."),

        new Finding("A05", Area.Assumption, Severity.Major,
            "'No fifth operator' (ch7) is asserted from QG307/308, which searched a finite set of unexplored domains. Absence-of-evidence over a searched space is presented as an existence proof.",
            "Chapter 7 operator basis",
            "Qualify the claim as 'no fifth operator found in any searched domain'; do not assert absolute non-existence."),

        new Finding("A06", Area.Dependency, Severity.Minor,
            "Chapter 3 sources the operator layer (QG260-263) whose canonical home is Chapter 7 (Part III Spectrum), misplacing the dependency order dynamics → spectrum → operators.",
            "Chapter 3 sources",
            "Either move the operator-layer sources to Chapter 7 or explicitly present Chapter 3's use as a forward projection of the spectrum layer."),

        new Finding("A07", Area.Leakage, Severity.Minor,
            "Chapter 2 (Tensor Reference eta, classified Boundary) primarily presents the second PRIMITIVE eta (foundational) alongside pi (boundary); the classification conflates a primitive with a boundary constant.",
            "Chapter 2 classification",
            "Split or annotate Chapter 2: eta is foundational; pi is the boundary constant."),

        new Finding("A08", Area.Assumption, Severity.Minor,
            "The lock-law universality and 'organization is a phase transition' claims derive from synthetic deterministic evolving-law cohorts (QG315-317); the Executive Summary presents them as universal without the model-cohort scope disclosure.",
            "Executive Summary / Chapters 8, 14",
            "Disclose the synthetic-cohort basis in the summary and the lock/phase chapters."),

        new Finding("A09", Area.Completeness, Severity.Minor,
            "Chapter 15 (Validation) cites MONO001 (v1.0 18-chapter structure) without noting it is superseded by MONO004.",
            "Chapter 15 sources",
            "Note MONO001's supersession by MONO004 where it is cited."),
    };

    // ── Tally ─────────────────────────────────────────────────────────────────

    public static int CriticalCount() => Catalog().Count(f => f.Severity == Severity.Critical);
    public static int MajorCount() => Catalog().Count(f => f.Severity == Severity.Major);
    public static int MinorCount() => Catalog().Count(f => f.Severity == Severity.Minor);

    /// <summary>All five required focus areas are represented.</summary>
    public static bool AllAreasCovered()
        => Enum.GetValues<Area>().All(a => Catalog().Any(f => f.Area == a));

    /// <summary>Every finding has a target and a required correction.</summary>
    public static bool AllFindingsActionable()
        => Catalog().All(f => !string.IsNullOrWhiteSpace(f.Target) && !string.IsNullOrWhiteSpace(f.Correction));

    /// <summary>The audit found no circularity that blocks (only A02, a reclassification).</summary>
    public static bool CircularityContained()
        => Catalog().Where(f => f.Area == Area.Circularity).All(f => f.Severity != Severity.Critical);

    // ── The determination ─────────────────────────────────────────────────────

    /// <summary>
    /// Audit score (0..6):
    /// 1. the audit searches all five required focus areas;
    /// 2. every finding is actionable [target + correction];
    /// 3. at least one dependency violation is found [A01/A06];
    /// 4. at least one boundary-leakage finding is found [A04/A07];
    /// 5. at least one unsupported-completeness finding is found [A03/A09];
    /// 6. the verdict is driven by the findings [FAIL if any CRITICAL].
    /// </summary>
    public static int AuditScore()
    {
        int score = 0;
        if (AllAreasCovered()) score++;
        if (AllFindingsActionable()) score++;
        if (Catalog().Any(f => f.Area == Area.Dependency)) score++;
        if (Catalog().Any(f => f.Area == Area.Leakage)) score++;
        if (Catalog().Any(f => f.Area == Area.Completeness)) score++;
        if (CriticalCount() >= 1 || MajorCount() >= 3) score++;
        return score;
    }

    /// <summary>
    /// Data-driven verdict:
    ///   FAIL — a CRITICAL finding (A01 primitive-count conflict) blocks publication; corrections are
    ///     required before Zenodo;
    ///   PASS — no critical and no more than two major findings [not reached here].
    /// </summary>
    public static string Verdict()
    {
        if (CriticalCount() >= 1) return "FAIL";
        if (MajorCount() > 2) return "FAIL";
        return "PASS";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Verdict()} — audit score {AuditScore()}/6. The hostile referee audit of the final " +
               $"17-chapter monograph structure found {CriticalCount()} CRITICAL, {MajorCount()} MAJOR, " +
               $"and {MinorCount()} MINOR architecture issues across all five required focus areas " +
               $"[circularity, dependency, assumption, leakage, completeness]. The blocking finding is " +
               $"A01: the canonical foundation {{Difference, eta}} conflicts with the cited QG318(2) " +
               $"architecture which classified {{Difference, Actualization, eta}} as three primitives. " +
               $"All corrections are documentation/architecture-level — reclassification, qualification, " +
               $"and scope disclosure; no physics changes are required.";
    }
}
