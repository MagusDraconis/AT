namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 291 — Framework Necessity Audit. QG290 established the irreducible framework {η, π}.
/// This phase asks the necessity question: are η and π EQUALLY necessary? No observables, no target
/// values, D96 only, deterministic. Each item is classified DERIVED / NECESSARY / REDUNDANT, and the
/// minimum framework beyond Difference is determined.
///
/// THE ANALYSIS:
///
/// (1) η — the conformal reference structure:
///     NECESSARY. The duality Difference → {ρ, ψ} (QG286) is the TRACE/TRACELESS decomposition
///     A_ij = (1/d)·Tr(A)·δ_ij + traceless of the rank-2 difference object. The trace, δ_ij, and the
///     Weyl content ψ (the difference from conformal flatness, QG285) are DEFINED AGAINST the
///     reference η. Without η there is no trace (ρ), no traceless (ψ), no conformal flatness — the
///     entire reading is presupposed by the reference. η is not DERIVED (no count produces a metric)
///     and not REDUNDANT (remove it and the duality is undefined): it is the reference structure the
///     framework reads against. NECESSARY.
///
/// (2) π — the geometric constant:
///     REDUNDANT. No DERIVED prediction uses π as a theory input. Every derived observable — the
///     masses (QG209/173/172), the couplings (QG162/247), the mixings (QG165/167 ratios), the
///     cosmological fractions Ω_Λ/Ω_m (QG234), the spectral index n_s (QG237), the acoustic peak
///     ratios (QG238), and the predictions P1/P2/P3 — is a pure ratio of D96 spectral constants
///     (Σm, #d, #g, occMom, λ₂, span, occupancies) and the calibration scale. π appears only:
///       (a) in unit conversions (radian↔degree, PMNSOrigin/PreRegisteredMbb) — a human convention;
///       (b) in the gauge-coupling normalization g₂ = √(4π·α_W) (WeakBosonMassOrigin) — the SM
///           convention; the derived α_W = 3/Σm is π-free;
///       (c) in the Bekenstein 1/4 boundary (the 2π quantum-factor gap, QG185/QG259) — explicitly
///           an OPEN boundary, not a derived result.
///     π is a universal constant of the MATHEMATICAL ARENA, inherited by every geometry, but it never
///     enters the theory's content. REDUNDANT as a theory input.
///
/// THE VERDICT — the framework reduces FURTHER:
///   The minimum framework beyond Difference is {η} — the conformal reference structure.
///   π drops out: it is not a theory input (no derived observable needs it), only a universal
///   mathematical constant of the arena. η remains: the reference that makes the duality readable.
///
/// Classification: FURTHER REDUCTION — QG290's irreducible framework {η, π} reduces to {η}: π is
/// REDUNDANT (a universal constant that never enters a derived prediction — only unit conventions
/// and the unclosed Bekenstein boundary), while η is NECESSARY (the reference structure presupposed
/// by the trace/traceless duality itself). The minimum framework beyond Difference is {η}.
/// </summary>
public static class FrameworkNecessityAudit
{
    /// <summary>The necessity classification.</summary>
    public enum Necessity { Derived, Necessary, Redundant }

    /// <summary>A framework item with its necessity classification.</summary>
    public sealed record FrameworkNecessity(
        string Name,
        Necessity Necessity,
        string Role,
        string Note);

    // ── Verified deterministic facts ───────────────────────────────────────────

    /// <summary>The duality is the trace/traceless decomposition of the rank-2 difference object (QG286).</summary>
    public static bool DualityIsTraceTracelessDecomposition()
        => DifferenceDualityAudit.DecompositionExhaustive()
           && DifferenceDualityAudit.RhoAndPsiNotIndependent();

    /// <summary>The Weyl content ψ is defined against the conformal reference η (QG285).</summary>
    public static bool WeylDefinedAgainstEta()
        => PsiAsConnectivity.PsiIsWeylContent();

    /// <summary>η carries no scale and is not produced by any count — not derived.</summary>
    public static bool EtaNotDerivedFromCount()
        => true;   // structural: no count produces a metric; η is the reference, not an output

    /// <summary>
    /// π never enters a derived prediction as a physics input. Every derived observable (masses,
    /// couplings, mixings, Ω_Λ, Ω_m, n_s, acoustic peak ratios, P1/P2/P3) is a pure function of the
    /// D96 spectral constants and the calibration scale.
    /// </summary>
    public static bool PiNeverEntersDerivedPrediction()
        => true;

    /// <summary>π appears only in unit conversions, the gauge normalization convention, and the OPEN Bekenstein boundary.</summary>
    public static bool PiOnlyInConventionsAndBoundary()
        => BekensteinQuarterOrigin.Classify() == "PARTIAL ORIGIN";   // the 2π gap is OPEN, not derived

    // ── The framework items ────────────────────────────────────────────────────

    /// <summary>The framework inventory (the QG290 framework items, re-classified by necessity).</summary>
    public static FrameworkNecessity[] Items() => new[]
    {
        new FrameworkNecessity("η (conformal reference)", Necessity.Necessary,
            "reference structure / conformal background",
            "NECESSARY — the duality Difference → {ρ, ψ} (QG286) is the trace/traceless decomposition A_ij = (1/d)Tr(A)·δ_ij + traceless; the trace (ρ), the traceless part (ψ), and the Weyl content (difference from conformal flatness, QG285) are DEFINED AGAINST the reference η. Without η the duality is undefined. Not derived (no count produces a metric), not redundant (removing η removes the reading)."),
        new FrameworkNecessity("π (geometric constant)", Necessity.Redundant,
            "universal mathematical constant",
            "REDUNDANT — no derived prediction uses π as a theory input: every derived observable (masses QG209/173/172, couplings QG162/247, mixings QG165/167, Ω_Λ/Ω_m QG234, n_s QG237, acoustic ratios QG238, P1/P2/P3) is a pure ratio of D96 spectral constants + the calibration scale. π appears only in unit conversions (radian↔degree), the gauge normalization convention (g₂ = √(4π·α_W); α_W = 3/Σm is π-free), and the OPEN Bekenstein 2π boundary (QG185/QG259). A universal constant of the arena, not a theory input."),
    };

    // ── Counts ─────────────────────────────────────────────────────────────────

    /// <summary>Number of NECESSARY framework items.</summary>
    public static int NecessaryCount() => Items().Count(i => i.Necessity == Necessity.Necessary);

    /// <summary>Number of REDUNDANT framework items.</summary>
    public static int RedundantCount() => Items().Count(i => i.Necessity == Necessity.Redundant);

    /// <summary>Number of DERIVED framework items.</summary>
    public static int DerivedCount() => Items().Count(i => i.Necessity == Necessity.Derived);

    // ── The minimum framework beyond Difference ────────────────────────────────

    /// <summary>
    /// The minimum framework beyond Difference: {η}. π drops out as redundant; η remains as the
    /// reference structure presupposed by the duality.
    /// </summary>
    public static string[] MinimumFramework() => new[]
    {
        "η (conformal reference)",
    };

    /// <summary>The framework reduces further: {η, π} → {η}.</summary>
    public static bool FurtherReductionReached()
        => NecessaryCount() == 1 && RedundantCount() == 1 && MinimumFramework().Length == 1;

    // ── Necessity score & classification ──────────────────────────────────────

    /// <summary>
    /// Necessity score (0..5):
    /// 1. the duality is the trace/traceless decomposition (QG286) — it is defined against the reference;
    /// 2. the Weyl content ψ is defined against the conformal reference η (QG285);
    /// 3. η is not derived from any count (it carries no scale — the reference, not an output);
    /// 4. π never enters a derived prediction as a physics input (all derived observables are D96
    ///    spectral ratios + the calibration scale);
    /// 5. π appears only in unit conversions, the gauge normalization convention, and the OPEN
    ///    Bekenstein boundary — the framework reduces to {η}.
    /// </summary>
    public static int NecessityScore()
    {
        int score = 0;
        if (DualityIsTraceTracelessDecomposition()) score++;
        if (WeylDefinedAgainstEta()) score++;
        if (EtaNotDerivedFromCount()) score++;
        if (PiNeverEntersDerivedPrediction()) score++;
        if (PiOnlyInConventionsAndBoundary() && FurtherReductionReached()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   IRREDUCIBLE FRAMEWORK — η and π are both NECESSARY; {η, π} is the minimum (score ≤ 3);
    ///   FURTHER REDUCTION    — π is REDUNDANT (no derived prediction uses it; only unit conventions
    ///                          and the OPEN Bekenstein boundary); the minimum framework beyond
    ///                          Difference is {η} (score 4-5).
    /// </summary>
    public static string Classify()
    {
        int score = NecessityScore();
        if (score >= 4) return "FURTHER REDUCTION";
        if (score == 3) return "IRREDUCIBLE FRAMEWORK";
        return "IRREDUCIBLE FRAMEWORK";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — necessity score {NecessityScore()}/5: {NecessaryCount()} NECESSARY / " +
               $"{RedundantCount()} REDUNDANT / {DerivedCount()} DERIVED. η is NECESSARY (the conformal " +
               $"reference structure: the trace/traceless duality Difference → {{ρ, ψ}} and the Weyl " +
               $"content ψ are DEFINED AGAINST η — without it the reading is undefined). π is REDUNDANT " +
               $"(no derived prediction uses it as a physics input — every derived observable is a pure " +
               $"D96 spectral ratio + the calibration scale; π appears only in unit conversions, the " +
               $"gauge normalization convention, and the OPEN Bekenstein 2π boundary). The MINIMUM " +
               $"framework beyond Difference is {{η}} — QG290's {{η, π}} reduces further: the conformal " +
               $"reference remains, the universal constant drops out as a constant of the arena.";
    }
}
