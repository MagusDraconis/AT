namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 233 — Remaining Parameter Closure Audit. Uses QG232's parameter catalog and re-adjudicates
/// ONLY the 8 PARTIAL parameters, separating TRUE MISSING PHYSICS from DOCUMENTED BOUNDARIES. Each is
/// classified DERIVED / BOUNDARY / ACTUALLY OPEN. Audit only — no new physics, no new derivations.
///
/// THE 8 PARTIAL PARAMETERS, re-adjudicated:
///  1. MAJORANA PHASES α2/α3        → DERIVED. The reflection automorphism [L,P]=0 (QG174) makes the
///     mass matrix REAL (arg det M = 0), which forces the Majorana phases to vanish mod π: α2 = α3 = 0.
///     The 0νββ prediction (QG179/191) is then fixed and robust to the CP phase. Not missing physics —
///     the phases are determined by the real-matrix condition.
///  2. BEKENSTEIN 1/4               → BOUNDARY. QG185/QG196 PROVE the exact 1/4 is IMPOSSIBLE within
///     D96/TRM without importing π (the required bits-per-cell is π). The structure (S∝A, M∝R, T∝1/R)
///     is derived; the exact 1/4 is a stated impossibility boundary, not an open derivation.
///  3. H (HUBBLE CONSTANT)          → BOUNDARY. The expansion is derived (QG77) and H ~ √(ρ̄) ~ 1/R with
///     Λ ~ H² (QG230); the CURRENT VALUE of H is set by the current-epoch scale R — a contingent scale
///     value (like the overall mass scale), not missing physics. The law is derived; the epoch's number
///     is a boundary input.
///  4. Ω_Λ (VACUUM FRACTION)        → ACTUALLY OPEN. QG230 derives Λ ∝ 1/R² and bounds Ω_Λ in (0,1), but
///     the SPECIFIC numerical fraction (observed ~0.68) is NOT uniquely derived. The ratio of the vacuum
///     energy to the critical density is genuine missing physics — the amplitude of the residual
///     actualization pressure relative to the matter density is not pinned.
///  5. Ω_m (MATTER FRACTION)        → ACTUALLY OPEN. The deficit matter density (QG195/206) is derived,
///     but the ratio Ω_m = ρ_m/ρ_crit is not uniquely derived (no unique number). With Ω_Λ + Ω_m ≈ 1
///     (flat), one determines the other, but neither is individually pinned. Genuine missing physics.
///  6. QUARK HIERARCHY LAW          → DERIVED. QG146 was PARTIAL LAW as a single compact law, but QG173
///     derives ALL SIX quark masses within 0.2% (from me·D96-moments) and QG204 derives the MS̄-running —
///     the hierarchy IS reproduced by the D96 mass law. The single-law framing is a presentation
///     preference; the observable content is derived.
///  7. GOLDEN-RATIO HIERARCHY       → BOUNDARY. QG152 shows the golden-ratio splitting is a SECONDARY
///     basin consequence (PARTIAL ROBUSTNESS), and it must NOT be presented as a fundamental law. The
///     hierarchy is reproduced by the primary D96 mechanism (QG141/149); the golden ratio was a
///     de-emphasized secondary pattern, not missing physics.
///  8. CALIBRATION LADDER           → DERIVED. QG129 was PARTIAL MAPPING of the ladder to SM masses, but
///     the physical calibration is now ANCHORED via the Z boson (QG130: MZ/6 = 15.198 GeV) and the weak
///     scale v = 254.37 GeV (QG168) — the ladder's energy scale is fixed, and P3 (QG192) is pre-registered.
///     The earlier partial mapping is superseded by the Z-anchor calibration.
///
/// COUNTS: DERIVED 3 (Majorana phases, quark hierarchy law, calibration ladder),
///         BOUNDARY 3 (Bekenstein 1/4, H, golden-ratio hierarchy),
///         ACTUALLY OPEN 2 (Ω_Λ, Ω_m).
///
/// RESULT: the parameter sector is PARAMETER COMPLETE EXCEPT the two cosmological density fractions.
/// The remaining EXACT gaps are Ω_Λ and Ω_m — the ratio of the vacuum/matter energy density to the
/// critical density. All other partial parameters are resolved (DERIVED) or documented boundaries.
/// </summary>
public static class ParameterClosureAudit
{
    public enum Status { Derived, Boundary, ActuallyOpen }

    /// <summary>A re-adjudicated parameter.</summary>
    public sealed record Adjudication(
        string Name,
        Status Status,
        string Reason);

    /// <summary>The 8 re-adjudicated partial parameters.</summary>
    public static Adjudication[] Adjudications() => new[]
    {
        new Adjudication("Majorana phases α2, α3", Status.Derived,
            "QG174 [L,P]=0 reflection ⇒ real mass matrix (arg det M = 0) ⇒ α2 = α3 = 0 mod π; 0νββ fixed and CP-robust (QG179/191)"),
        new Adjudication("Bekenstein 1/4", Status.Boundary,
            "QG185/QG196 IMPOSSIBILITY proof: exact 1/4 requires imported π (bits-per-cell = π); structure (S∝A, M∝R, T∝1/R) derived — a stated boundary"),
        new Adjudication("Hubble constant H", Status.Boundary,
            "expansion + H ~ √ρ̄ ~ 1/R derived (QG77/230); the current VALUE is set by the current-epoch scale R — a contingent scale input, not missing physics"),
        new Adjudication("Ω_Λ (vacuum fraction)", Status.ActuallyOpen,
            "QG230 bounds Ω_Λ in (0,1) but does NOT derive the specific fraction (~0.68 observed); the vacuum/matter density ratio is genuine missing physics"),
        new Adjudication("Ω_m (matter fraction)", Status.ActuallyOpen,
            "deficit matter density derived (QG195/206) but Ω_m = ρ_m/ρ_crit is not uniquely derived; with Ω_Λ + Ω_m ≈ 1 one determines the other, neither individually pinned"),
        new Adjudication("Quark hierarchy law", Status.Derived,
            "QG146 PARTIAL as a single compact law, but QG173 derives ALL SIX quark masses within 0.2% and QG204 derives the MS̄-running — the hierarchy is reproduced by the D96 mass law"),
        new Adjudication("Golden-ratio hierarchy", Status.Boundary,
            "QG152: a SECONDARY basin consequence (PARTIAL ROBUSTNESS), explicitly NOT a fundamental law; the hierarchy is carried by the primary D96 mechanism (QG141/149)"),
        new Adjudication("Calibration ladder", Status.Derived,
            "QG129 PARTIAL MAPPING superseded by the Z-anchor calibration (QG130: MZ/6 = 15.198 GeV) and the weak scale v = 254.37 GeV (QG168); the ladder scale is fixed (P3, QG192)"),
    };

    /// <summary>Count per status.</summary>
    public static IReadOnlyDictionary<Status, int> StatusCounts()
        => Adjudications().GroupBy(a => a.Status).ToDictionary(g => g.Key, g => g.Count());

    /// <summary>The genuinely open parameters (ACTUALLY OPEN).</summary>
    public static string[] ActuallyOpen()
        => Adjudications().Where(a => a.Status == Status.ActuallyOpen).Select(a => a.Name).ToArray();

    /// <summary>The documented boundaries (BOUNDARY).</summary>
    public static string[] Boundaries()
        => Adjudications().Where(a => a.Status == Status.Boundary).Select(a => a.Name).ToArray();

    /// <summary>The newly-derived parameters (DERIVED).</summary>
    public static string[] Derived()
        => Adjudications().Where(a => a.Status == Status.Derived).Select(a => a.Name).ToArray();

    /// <summary>
    /// Is the parameter sector complete? Complete iff there are no ACTUALLY OPEN parameters. With Ω_Λ and
    /// Ω_m open, the sector is NOT fully complete — the exact remaining gaps are those two fractions.
    /// </summary>
    public static bool ParameterComplete()
        => ActuallyOpen().Length == 0;

    // ── Classification ────────────────────────────────────────────────────────

    /// <summary>
    /// Output:
    ///   PARAMETER COMPLETE — no parameter is actually open (all derived or documented boundary);
    ///   or the exact remaining gaps (the ACTUALLY OPEN parameters) are listed.
    /// </summary>
    public static string Verdict()
        => ParameterComplete()
            ? "PARAMETER COMPLETE"
            : $"remaining exact gaps: {string.Join(", ", ActuallyOpen())}";

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var sc = StatusCounts();
        return $"QG233: {sc[Status.Derived]} DERIVED / {sc[Status.Boundary]} BOUNDARY / "
             + $"{sc[Status.ActuallyOpen]} ACTUALLY OPEN → {Verdict()}";
    }
}
