namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 284 — Post-Assignment Frontier Audit. QG280 produced the definitive frontier list; QG283
/// CLOSED the assignment frontier (R1, the primary open question). This phase re-audits the remaining
/// frontier items after the assignment closure: which items remain, reclassified as
/// OPEN / PARTIAL / BOUNDARY / METHODOLOGY. Audit only, no new derivations.
///
/// THE RE-CLASSIFICATION (after QG283 ASSIGNMENT CLOSED):
///
/// [R1] ASSIGNMENT FRONTIER — CLOSED by QG283 (the role assignment law: axis position + conservation
///      structure; the relational subclass resolved by unitarity/me-anchor). REMOVED from the frontier.
///
/// [R2] INDEPENDENT TEMPORAL EVIDENCE — OPEN (the 6.7% binding constraint; P1-P3 pre-registered, P3
///      externally supported). Requires future measurement. UNCHANGED.
///
/// [R3] SELF-CONFIRMATION (F2) — METHODOLOGY (not a physics frontier: the tests assert the formulas
///      the phases chose; needs external arbitration, not a derivation). Reclassified from OPEN.
///
/// [R4] THE 5/4 EXCEPTION — OPEN (the meta-level inconsistency: QG238 uses 5/4, QG255 Noether rejects
///      free constants). Not touched by the assignment closure. UNCHANGED.
///
/// [R5] me = 0.511 ANCHOR — BOUNDARY (the only genuinely free empirical input; QG251). The assignment
///      law uses me-anchoring as a structural discriminator but does not derive me itself. UNCHANGED.
///
/// [R6] ψ PRIMITIVE — OPEN (the ψ tensor field remains a hand-placed second primitive; the resonance
///      and assignment reductions are ρ-sector only). UNCHANGED.
///
/// [R7] STRUCTURAL IMPORTS (η, π, RG, 3+1) — BOUNDARY (each a documented import). UNCHANGED.
///
/// [R8] SM REMAINING GAPS (Bekenstein, Λ, H) — PARTIAL (derived structure, exact value a boundary).
///      UNCHANGED.
///
/// [R9] DIFFERENCE BOUNDARY — BOUNDARY (the true fundamental boundary, QG278-279). UNCHANGED.
///
/// [R10] PUBLICATION / EXTERNAL ARBITRATION — METHODOLOGY (external, not derivable within AT).
///       Reclassified from OPEN.
///
/// THE FINAL REMAINING FRONTIER (after assignment closure):
///   PHYSICS OPEN (2): R2 independent temporal evidence, R4 the 5/4 exception, R6 ψ primitive — 3.
///   PARTIAL (1): R8 SM remaining gaps.
///   BOUNDARY (3): R5 me anchor, R7 structural imports, R9 Difference boundary.
///   METHODOLOGY (2): R3 self-confirmation, R10 publication/arbitration.
///   The assignment closure removed the PRIMARY structural question; what remains is the
///   external-validation constraint (temporal evidence), the meta-inconsistency (5/4), the ψ primitive,
///   and the documented boundaries + methodology items.
///
/// CLASSIFICATION: after assignment closure, the frontier is: OPEN 3 / PARTIAL 1 / BOUNDARY 3 /
/// METHODOLOGY 2. The primary structural question (assignment) is CLOSED; the remaining frontier is
/// external validation + the 5/4 exception + ψ + documented boundaries + methodology.
/// </summary>
public static class PostAssignmentFrontierAudit
{
    public enum Status { Open, Partial, Boundary, Methodology }

    /// <summary>A frontier item after assignment closure.</summary>
    public sealed record FrontierItem(
        int Rank,
        string Name,
        Status Status,
        bool ChangedByQG283,
        string Note);

    /// <summary>The re-audited frontier after QG283 assignment closure.</summary>
    public static FrontierItem[] Items() => new[]
    {
        new FrontierItem(1, "Independent temporal evidence", Status.Open, false,
            "the 6.7% binding constraint (P1-P3 pre-registered, P3 externally supported); requires future measurement"),
        new FrontierItem(2, "The 5/4 exception", Status.Open, false,
            "the meta-level inconsistency: QG238 uses 5/4, the QG255 Noether rule rejects free constants"),
        new FrontierItem(3, "ψ primitive", Status.Open, false,
            "the ψ tensor field remains a hand-placed second primitive; the reductions are ρ-sector only"),
        new FrontierItem(4, "SM remaining gaps (Bekenstein, Λ, H)", Status.Partial, false,
            "each has a derived structure (S∝A, Λ∝1/R², redshift law); the exact coefficient/value is a boundary"),
        new FrontierItem(5, "me = 0.511 anchor", Status.Boundary, false,
            "the only genuinely free empirical input (QG251); the assignment law uses me-anchoring but does not derive me"),
        new FrontierItem(6, "Structural imports (η, π, RG, 3+1)", Status.Boundary, false,
            "each a documented import the reductions neither derive nor reframe"),
        new FrontierItem(7, "Difference boundary", Status.Boundary, false,
            "the TRUE FUNDAMENTAL BOUNDARY (QG278-279) — the theory's genuine first concept, not a problem"),
        new FrontierItem(8, "Self-confirmation (F2)", Status.Methodology, false,
            "the tests assert the formulas the phases chose; needs external arbitration — a methodology issue, not physics"),
        new FrontierItem(9, "Publication / external arbitration", Status.Methodology, false,
            "no peer review, self-authored evidence base — external, not derivable within AT"),
    };

    /// <summary>The item CLOSED by QG283 (the assignment frontier).</summary>
    public static FrontierItem AssignmentClosure()
        => new(0, "Assignment frontier (operator → physics labels)", Status.Boundary, true,
            "CLOSED by QG283 — the role assignment law (axis position + conservation structure) resolves the operator → physics labels; the relational subclass resolved by unitarity/me-anchor");

    /// <summary>Count of items by status (after assignment closure).</summary>
    public static IReadOnlyDictionary<Status, int> StatusCounts()
    {
        var d = new Dictionary<Status, int>();
        foreach (Status s in Enum.GetValues<Status>()) d[s] = 0;
        foreach (var i in Items()) d[i.Status]++;
        return d;
    }

    /// <summary>Number of OPEN physics items.</summary>
    public static int OpenCount() => StatusCounts()[Status.Open];

    /// <summary>Number of PARTIAL items.</summary>
    public static int PartialCount() => StatusCounts()[Status.Partial];

    /// <summary>Number of BOUNDARY items.</summary>
    public static int BoundaryCount() => StatusCounts()[Status.Boundary];

    /// <summary>Number of METHODOLOGY items.</summary>
    public static int MethodologyCount() => StatusCounts()[Status.Methodology];

    /// <summary>Total remaining frontier items (excluding the closed assignment).</summary>
    public static int TotalRemaining() => Items().Length;

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var c = StatusCounts();
        return $"Post-assignment frontier: OPEN {c[Status.Open]} / PARTIAL {c[Status.Partial]} / "
             + $"BOUNDARY {c[Status.Boundary]} / METHODOLOGY {c[Status.Methodology]} — the assignment "
             + "frontier (QG280 R1) is CLOSED by QG283; what remains is the external-validation "
             + "constraint (temporal evidence), the 5/4 meta-inconsistency, the ψ primitive, the "
             + "documented boundaries (me, structural imports, Difference), and the methodology items "
             + "(self-confirmation, publication). The primary structural question is resolved; the "
             + "remaining frontier is external + a small physics residue.";
    }
}
