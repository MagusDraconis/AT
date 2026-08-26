namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 239 — Formula Selection Audit. Audits the uniqueness of the closed-form derivations in
/// QG203-QG238. For each relation records the number of candidate formulas, whether alternatives existed,
/// why the final formula was selected, whether the target value influenced selection, and whether the
/// derivation was preregistered. Classifies each UNIQUE / PREFERRED / UNDERDETERMINED /
/// RETRO-SELECTION RISK. Audit only — no new physics. Deterministic.
///
/// THE SIX RELATIONS AUDITED (QG203-238):
///  1. NEUTRINO MASSES (QG203) — m2 = 1/(Σ√m·√(span/2)), m3 = √#g/(Σm·√2), ratio = 2Σm/(Σ√m·√(span·#g)).
///     Candidate count: several natural D96 scale combinations (N = 1/Σ√m, octave span, #g) existed; the
///     closed forms are the natural normalizations. Alternatives existed but the target (8.72/49.4 meV)
///     was compared after selection — the forms are D96-native (not fitted exponents). NOT preregistered.
///     → PREFERRED (natural D96 normalizations, matched to targets; formula choice among a small candidate set).
///  2. COSMOLOGICAL FRACTIONS (QG234) — Ω_Λ = I_occ/ln K, Ω_m = 1 − Ω_Λ. The normalization by ln K is the
///     natural maximum-information scale; the octave record [4,4,87] fixes I_occ. Alternatives existed
///     (e.g. other normalization scales), the target (0.6847) was a comparison anchor. NOT preregistered.
///     → PREFERRED (natural max-entropy normalization, matched 0.12%; alternative normalizations existed).
///  3. SPECTRAL INDEX n_s (QG237) — 1−n_s = ln(span)/(Σm−#d). The combination (span, Σm−#d) is one of
///     many possible D96 ratios; the factor ln(span) over the independent-mode count was selected to
///     match the observed 0.03503. The target (n_s = 0.9649) is sharp; the specific formula is not forced
///     by an obvious uniqueness principle and was NOT preregistered.
///     → RETRO-SELECTION RISK (a specific D96 combination selected to match the target; no preregistration).
///  4. ACOUSTIC PEAKS (QG238) — ℓ₁ = Σm·ln(span)·5/4, r₂₁ = (Σm−#d)·occ₁/occ₃, r₃₁ = span/√3. The
///     5/4 and √3 factors are selected to match the observed peaks; multiple multiplicative combinations
///     of (Σm, #d, occ, span, √3) could fit; the factors were NOT preregistered and are not forced by a
///     uniqueness principle.
///     → RETRO-SELECTION RISK (multiplicative factors selected to match the peaks; not preregistered).
///  5. LEPTON HIERARCHY (QG209) — m_μ = me·Σm²/√occMom, m_τ = me·Σm²·λ₂. D96-only, no fitted exponents;
///     upgraded from QG142 PARTIAL LAW. The specific moments (Σm², √occMom, λ₂) are natural D96
///     quantities, but the formula choice among moment combinations was compared against the targets.
///     NOT preregistered (post-hoc upgrade).
///     → PREFERRED (D96-only with no fitted exponents; the moment combination was selected among
///       alternatives after comparison).
///  6. LAMBDA ORIGIN (QG230) — Λ ∝ 1/R². The scaling is STRUCTURALLY FORCED: M∝R (QG184) gives
///     ρ̄ ~ M/R³ ~ 1/R² and the single-scale identity Λ ~ ρ̄ ~ H². Existence (growing variance + positive
///     info) and sign (repulsive) are structural. No free multiplicative factor enters the scaling law.
///     → UNIQUE (the 1/R² scaling is forced by the single-scale R structure; no alternative scaling).
///
/// SUMMARY: 1 UNIQUE (Λ scaling), 3 PREFERRED (neutrino masses, fractions, lepton hierarchy), 0
/// UNDERDETERMINED, 2 RETRO-SELECTION RISK (n_s, acoustic peaks).
///
/// The two RETRO-SELECTION RISK items (n_s, acoustic peaks) are the QG237/238 derivations: their closed
/// forms are specific D96 combinations that match sharp observed targets but were neither preregistered
/// nor forced by an independent uniqueness principle. They are the strongest anti-fit criticism of the
/// QG203-238 era and should be flagged for pre-registration or a uniqueness proof.
/// </summary>
public static class FormulaSelectionAudit
{
    public enum Classification { Unique, Preferred, Underdetermined, RetroSelectionRisk }

    /// <summary>An audited closed-form relation.</summary>
    public sealed record Audit(
        string Relation,
        string Formula,
        Classification Classification,
        int CandidateCount,
        bool AlternativesExisted,
        string SelectionReason,
        bool TargetInfluenced,
        bool Preregistered,
        string RiskNote);

    /// <summary>The six audited relations.</summary>
    public static Audit[] Audits() => new[]
    {
        new Audit("Neutrino masses (QG203)",
            "m2 = 1/(Σ√m·√(span/2)), m3 = √#g/(Σm·√2), ratio = 2Σm/(Σ√m·√(span·#g))",
            Classification.Preferred, 3, true,
            "natural D96 scale normalizations (N = 1/Σ√m, octave span, #g); the closed forms are D96-native, no fitted exponents", true, false,
            "several natural normalizations existed; the target was compared after the D96-native form"),
        new Audit("Cosmological fractions (QG234)",
            "Ω_Λ = I_occ/ln K, Ω_m = 1 − Ω_Λ",
            Classification.Preferred, 3, true,
            "the ln K normalization is the natural maximum-information scale; the octave record [4,4,87] fixes I_occ", true, false,
            "alternative normalization scales existed; the max-entropy one matched 0.12%"),
        new Audit("Spectral index n_s (QG237)",
            "1−n_s = ln(span)/(Σm−#d)",
            Classification.RetroSelectionRisk, 5, true,
            "a specific D96 combination (ln(span) over the independent-mode count) that matches the sharp observed 0.03503", true, false,
            "many D96 ratios could fit the tilt; no independent uniqueness principle, no preregistration — the strongest anti-fit risk of the era"),
        new Audit("Acoustic peaks (QG238)",
            "ℓ₁ = Σm·ln(span)·5/4, r₂₁ = (Σm−#d)·occ₁/occ₃, r₃₁ = span/√3",
            Classification.RetroSelectionRisk, 6, true,
            "multiplicative factors (5/4, √3, octave ratios) selected to match the observed peaks", true, false,
            "multiple multiplicative combinations of (Σm, #d, occ, span, √3) could fit; factors not preregistered or forced"),
        new Audit("Lepton hierarchy (QG209)",
            "m_μ = me·Σm²/√occMom, m_τ = me·Σm²·λ₂",
            Classification.Preferred, 4, true,
            "D96-only moments (Σm², √occMom, λ₂) with no fitted exponents; upgraded QG142 PARTIAL to EXACT", true, false,
            "moment combinations compared against the targets; D96-only but not preregistered"),
        new Audit("Lambda origin (QG230)",
            "Λ ∝ 1/R² (existence, sign, scaling)",
            Classification.Unique, 1, false,
            "the 1/R² scaling is STRUCTURALLY FORCED: M∝R (QG184) ⇒ ρ̄ ~ 1/R² and the single-scale identity Λ ~ ρ̄ ~ H²; existence and sign are structural", false, false,
            "no alternative scaling exists — the single-scale R structure forces it"),
    };

    // ── Counts ────────────────────────────────────────────────────────────────

    /// <summary>Classification counts.</summary>
    public static IReadOnlyDictionary<Classification, int> ClassificationCounts()
        => Audits().GroupBy(a => a.Classification).ToDictionary(g => g.Key, g => g.Count());

    /// <summary>Number of RETRO-SELECTION RISK items.</summary>
    public static int RetroSelectionCount()
        => Audits().Count(a => a.Classification == Classification.RetroSelectionRisk);

    /// <summary>Number of items where the target value influenced selection.</summary>
    public static int TargetInfluencedCount()
        => Audits().Count(a => a.TargetInfluenced);

    /// <summary>Number of preregistered items (none in QG203-238).</summary>
    public static int PreregisteredCount()
        => Audits().Count(a => a.Preregistered);

    /// <summary>The retro-selection risk items (the strongest anti-fit criticism).</summary>
    public static string[] RetroSelectionItems()
        => Audits().Where(a => a.Classification == Classification.RetroSelectionRisk).Select(a => a.Relation).ToArray();

    // ── Summary ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Summary: 1 UNIQUE (Λ scaling), 3 PREFERRED (neutrino masses, fractions, lepton hierarchy),
    /// 0 UNDERDETERMINED, 2 RETRO-SELECTION RISK (n_s, acoustic peaks). The two risk items are
    /// QG237/238 — specific D96 combinations matching sharp observed targets without preregistration
    /// or an independent uniqueness proof.
    /// </summary>
    public static string Summary()
    {
        var c = ClassificationCounts();
        return $"{c[Classification.Unique]} UNIQUE / {c[Classification.Preferred]} PREFERRED / "
             + $"{c.GetValueOrDefault(Classification.Underdetermined)} UNDERDETERMINED / "
             + $"{c[Classification.RetroSelectionRisk]} RETRO-SELECTION RISK; "
             + $"target-influenced {TargetInfluencedCount()}, preregistered {PreregisteredCount()}";
    }
}
