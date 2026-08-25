namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 299 — Remaining Frontier Re-Audit. QG280 produced the definitive frontier list; QG281-
/// QG298 (the resonance reduction, the minimal-theory program, the first-peak origin, and the exception
/// audit) transformed several items. This phase re-audits the frontier: every QG280 item is reclassified
/// OPEN / PARTIAL / BOUNDARY / METHODOLOGY / CLOSED through the QG281-298 lens, and the final
/// post-QG298 frontier is produced.
///
/// THE RE-CLASSIFICATION (through QG281-QG298):
///
/// [R1] ASSIGNMENT FRONTIER — CLOSED (QG283 assignment law; QG284 audit). No longer on the frontier.
///
/// [R2] INDEPENDENT TEMPORAL EVIDENCE — OPEN (the 6.7% binding constraint; P1-P3 pre-registered, P3
///      externally supported). Requires future measurement. UNCHANGED by QG281-298.
///
/// [R3] SELF-CONFIRMATION (F2) — METHODOLOGY (the tests assert the formulas the phases chose; needs
///      external arbitration). UNCHANGED.
///
/// [R4] THE 5/4 EXCEPTION — CLOSED (QG298 FIRST PEAK ORIGIN): 5/4 is the BOUNDARY PROJECTION of the
///      fundamental harmonic — (occ₀ + zero_mode)/occ₀ = (4+1)/4 = 5/4, the first-mode normalization
///      including the background zero-mode transition. The QG297 "fit" is reinterpreted as structural;
///      only the absolute ℓ₁ carries it, the ratios cancel it. The meta-inconsistency (QG238 uses 5/4,
///      QG255 rejects free constants) is resolved: 5/4 is not a free constant.
///
/// [R5] me = 0.511 ANCHOR — CLOSED (reframed by QG289 MINIMAL INVENTORY): me is one of TWO calibration
///      scales (EMPIRICAL), replaceable by any single mass anchor, and only ONE empirical scale is
///      strictly needed. The absolute-scale need remains a calibration choice, not a theory input.
///
/// [R6] ψ PRIMITIVE — BOUNDARY (QG285 PSI REINTERPRETATION + QG286 DIFFERENCE DUALITY): ψ is fully
///      LOCATED as the anisotropic (tensor/traceless) face of Difference — the Weyl content of the
///      connectivity, defined against η (QG292). It is no longer a hand-placed postulate; its
///      fundamental status (spin-2 cannot emerge from scalars, QG52) is a documented boundary.
///
/// [R7] STRUCTURAL IMPORTS (η, π, RG, 3+1) — CLOSED (QG289-292): η = NECESSARY framework reference
///      (the conformal reading structure; needed for the tensor sector only, QG292); π = REDUNDANT
///      (a universal arena constant, QG291 — no derived prediction uses it); RG = REMOVABLE (the
///      running EMERGES from D96, QG204); 3+1 = DERIVED (d≥3 from QG2, the d=3 Einstein structure
///      QG197). The imports are classified and reduced to {η} + one scale.
///
/// [R8] SM REMAINING GAPS (Bekenstein 1/4, Λ, H) — PARTIAL (structure derived: S∝A, Λ∝1/R², redshift
///      law; the exact coefficient/value is a boundary). Bekenstein 1/4 needs the imported 2π quantum
///      factor (QG185/QG259); Λ value needs the R scale (QG230); H is an epoch input. UNCHANGED.
///
/// [R9] DIFFERENCE BOUNDARY — BOUNDARY (the TRUE FUNDAMENTAL BOUNDARY, QG278-279; confirmed the root by
///      the QG292 stress test — removing it collapses all five layers). UNCHANGED.
///
/// [R10] PUBLICATION / EXTERNAL ARBITRATION — METHODOLOGY (external, not derivable within TQM).
///       UNCHANGED.
///
/// THE FINAL POST-QG298 FRONTIER:
///   OPEN (1):        R2 independent temporal evidence (external validation).
///   PARTIAL (1):     R8 SM remaining gaps (structure derived, exact values boundary).
///   BOUNDARY (2):    R6 ψ fundamental status, R9 Difference boundary.
///   METHODOLOGY (2): R3 self-confirmation, R10 publication.
///   CLOSED (3):      R4 5/4 (first-peak boundary projection), R5 me (calibration choice),
///                    R7 structural imports (classified and reduced).
///   The remaining exact issues are: ONE external-validation item (temporal evidence), ONE partial
///   value gap (SM coefficients), the two documented boundaries (ψ, Difference), and the two
///   methodology items. No physics-derivation frontier remains OPEN beyond temporal evidence.
///
/// Classification: the post-QG298 frontier is OPEN 1 / PARTIAL 1 / BOUNDARY 2 / METHODOLOGY 2 /
/// CLOSED 3 — the resonance-minimal program closed the 5/4 exception (R4), reframed the me anchor
/// (R5) and the structural imports (R7); the remaining exact issues are external validation
/// (temporal evidence), the SM value gaps, the documented boundaries (ψ, Difference), and the
/// methodology items.
/// </summary>
public static class RemainingFrontierReaudit
{
    /// <summary>The frontier classification.</summary>
    public enum Status { Open, Partial, Boundary, Methodology, Closed }

    /// <summary>A frontier item after the QG281-298 re-audit.</summary>
    public sealed record FrontierItem(
        int Rank,
        string Name,
        Status Status,
        bool ChangedByQG281to298,
        string ReauditNote);

    /// <summary>The re-audited frontier after QG281-298.</summary>
    public static FrontierItem[] Items() => new[]
    {
        new FrontierItem(1, "Independent temporal evidence", Status.Open, false,
            "the 6.7% binding constraint (P1-P3 pre-registered, P3 externally supported); requires future measurement — UNCHANGED"),
        new FrontierItem(2, "SM remaining gaps (Bekenstein 1/4, Λ value, H)", Status.Partial, false,
            "structure derived (S∝A, Λ∝1/R², redshift law); the exact coefficient/value is a boundary: Bekenstein 1/4 needs the imported 2π factor (QG185/259), Λ value needs the R scale (QG230), H is an epoch input — UNCHANGED"),
        new FrontierItem(3, "ψ fundamental status", Status.Boundary, true,
            "CLOSED as a hand-placed postulate → reclassified BOUNDARY: QG285 PSI REINTERPRETATION + QG286 DIFFERENCE DUALITY locate ψ as the anisotropic (tensor/traceless) face of Difference — the Weyl content of the connectivity, defined against η (QG292); its fundamental status (spin-2 cannot emerge from scalars, QG52) is a documented boundary"),
        new FrontierItem(4, "Difference boundary", Status.Boundary, false,
            "the TRUE FUNDAMENTAL BOUNDARY (QG278-279) — confirmed the ROOT by the QG292 stress test (removing it collapses all five layers) — UNCHANGED"),
        new FrontierItem(5, "Self-confirmation (F2)", Status.Methodology, false,
            "the tests assert the formulas the phases chose; needs external arbitration — a methodology issue, not physics — UNCHANGED"),
        new FrontierItem(6, "Publication / external arbitration", Status.Methodology, false,
            "no peer review, self-authored evidence base — external, not derivable within TQM — UNCHANGED"),
        new FrontierItem(7, "The 5/4 exception", Status.Closed, true,
            "CLOSED by QG298 FIRST PEAK ORIGIN: 5/4 is the BOUNDARY PROJECTION of the fundamental harmonic — (occ₀ + zero_mode)/occ₀ = (4+1)/4 = 5/4, the first-mode normalization including the background zero-mode transition; only the absolute ℓ₁ carries it, the ratios cancel it — the QG297 'fit' is structural, the meta-inconsistency resolved"),
        new FrontierItem(8, "me = 0.511 anchor", Status.Closed, true,
            "CLOSED (reframed by QG289 MINIMAL INVENTORY): me is one of TWO calibration scales (EMPIRICAL), replaceable by any single mass anchor, and only ONE empirical scale is strictly needed — the absolute-scale need is a calibration choice, not a theory input"),
        new FrontierItem(9, "Structural imports (η, π, RG, 3+1)", Status.Closed, true,
            "CLOSED (QG289-292): η = NECESSARY framework reference (the conformal reading structure; needed for the tensor sector only, QG292); π = REDUNDANT (a universal arena constant — no derived prediction uses it, QG291); RG = REMOVABLE (the running EMERGES from D96, QG204); 3+1 = DERIVED (d≥3 from QG2, the d=3 Einstein structure QG197) — the imports are classified and reduced to {η} + one scale"),
    };

    /// <summary>Count of items by status.</summary>
    public static IReadOnlyDictionary<Status, int> StatusCounts()
    {
        var d = new Dictionary<Status, int>();
        foreach (Status s in Enum.GetValues<Status>()) d[s] = 0;
        foreach (var i in Items()) d[i.Status]++;
        return d;
    }

    /// <summary>Number of OPEN items.</summary>
    public static int OpenCount() => StatusCounts()[Status.Open];

    /// <summary>Number of PARTIAL items.</summary>
    public static int PartialCount() => StatusCounts()[Status.Partial];

    /// <summary>Number of BOUNDARY items.</summary>
    public static int BoundaryCount() => StatusCounts()[Status.Boundary];

    /// <summary>Number of METHODOLOGY items.</summary>
    public static int MethodologyCount() => StatusCounts()[Status.Methodology];

    /// <summary>Number of CLOSED items (closed/reframed by QG281-298).</summary>
    public static int ClosedCount() => StatusCounts()[Status.Closed];

    /// <summary>Number of items changed by QG281-298.</summary>
    public static int ChangedCount() => Items().Count(i => i.ChangedByQG281to298);

    /// <summary>
    /// The re-audit result: 3 items closed/reframed by QG281-298 (5/4, me, structural imports),
    /// 1 item reclassified (ψ → BOUNDARY); the remaining exact issues are the external validation,
    /// the SM value gaps, the documented boundaries, and the methodology items.
    /// </summary>
    public static bool FrontierClosureVerified()
        => ClosedCount() == 3 && ChangedCount() == 4 && OpenCount() == 1;

    /// <summary>The remaining exact issues (the final frontier).</summary>
    public static string[] RemainingExactIssues() => new[]
    {
        "OPEN: independent temporal evidence (the 6.7% binding constraint — needs future measurement)",
        "PARTIAL: SM value gaps (Bekenstein 1/4 needs the 2π factor; Λ value needs the R scale; H epoch)",
        "BOUNDARY: ψ fundamental status (documented — spin-2 cannot emerge from scalars, QG52)",
        "BOUNDARY: Difference (the true fundamental boundary, QG278-279)",
        "METHODOLOGY: self-confirmation (F2) — external arbitration",
        "METHODOLOGY: publication / external arbitration",
    };

    // ── Re-audit score & classification ────────────────────────────────────────

    /// <summary>
    /// Re-audit score (0..5):
    /// 1. the 5/4 exception is CLOSED (QG298 first-peak boundary projection — structural);
    /// 2. the me anchor is CLOSED/reframed (QG289: one of two calibration scales, replaceable);
    /// 3. the structural imports are CLOSED/classified (QG289-292: η framework, π redundant, RG
    ///    removable, 3+1 derived);
    /// 4. ψ is reclassified BOUNDARY (QG285/286 located it as the anisotropic face of Difference);
    /// 5. the remaining frontier is exactly external validation + SM value gaps + documented
    ///    boundaries + methodology (no physics-derivation frontier beyond temporal evidence).
    /// </summary>
    public static int ReauditScore()
    {
        int score = 0;
        if (Items().Any(i => i.Name == "The 5/4 exception" && i.Status == Status.Closed)) score++;
        if (Items().Any(i => i.Name == "me = 0.511 anchor" && i.Status == Status.Closed)) score++;
        if (Items().Any(i => i.Name == "Structural imports (η, π, RG, 3+1)" && i.Status == Status.Closed)) score++;
        if (Items().Any(i => i.Name == "ψ fundamental status" && i.Status == Status.Boundary)) score++;
        if (FrontierClosureVerified()) score++;
        return score;
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var c = StatusCounts();
        return $"Post-QG298 frontier: OPEN {c[Status.Open]} / PARTIAL {c[Status.Partial]} / " +
               $"BOUNDARY {c[Status.Boundary]} / METHODOLOGY {c[Status.Methodology]} / CLOSED " +
               $"{c[Status.Closed]} — the QG281-298 program closed the 5/4 exception (R4: first-peak " +
               $"boundary projection, QG298), reframed the me anchor (R5: one of two calibration scales, " +
               $"QG289) and the structural imports (R7: η framework, π redundant, RG removable, 3+1 " +
               $"derived), and reclassified ψ (R6: the anisotropic face of Difference — a documented " +
               $"boundary). The remaining exact issues are: ONE OPEN (independent temporal evidence — " +
               $"external), ONE PARTIAL (SM value gaps — Bekenstein 2π, Λ value, H epoch), TWO BOUNDARY " +
               $"(ψ fundamental status, Difference), and TWO METHODOLOGY (self-confirmation, " +
               $"publication). No physics-derivation frontier remains beyond temporal evidence.";
    }
}
