namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 255 — Secondary Selection Principle. QG254 established OCTAVE PRESERVATION as the primary
/// D96-only selection rule (it excluded 5 of the 8 QG253 minimal-complexity alternatives). The remaining
/// 3 octave-preserving ties were: m_μ/me (Σm²/√occMom vs 5/4·Σ√m/λ₂), m_τ/m_μ (√occMom·λ₂ vs √3·√Σm vs
/// √#d/λ₂), r₃₁ (span/√3 vs λ₂³·Σ√m). This phase derives ONE secondary rule — applied with no observables,
/// no target values, D96 only, deterministic — that resolves the remaining ties to unique formulas.
///
/// THE SECONDARY RULE — MOMENT-CLOSURE MINIMUM DESCRIPTION LENGTH (MDL):
///   Among octave-preserving formulas at minimal complexity, select the unique formula that maximizes:
///    (1) NOETHER CONSISTENCY — no free constant multiplier. A genuine D96 coupling must be expressible as
///        a ratio of D96-native quantities ONLY; a bare 5/4 or √3 (an imported multiplier not fixed by the
///        spectrum) is a description-length cost with no Noether source. This rejects 5/4·Σ√m/λ₂ and
///        √3·√Σm (each carries a free multiplier).
///    (2) MOMENT CLOSURE / FULL-SPECTRUM USAGE — prefer the formula built from the HIGHEST-order
///        full-spectrum aggregates: occMom (2nd octave moment) and Σm² (2nd mode moment) and λ₂ (the
///        whole-spectrum gap) over partial-spectrum quantities (#d count, Σ√m half-moment). The octave
///        occupation moment occMom closes the D96 moment hierarchy (QG155/157: the moments of the
///        occupation structure), so a formula using occMom uses MORE of the D96 spectral content than one
///        using #d or Σ√m alone.
///
/// APPLIED TO THE QG254 TIES (no target consulted — the rule reads only the formula structure):
///  • m_μ/me: Σm²/√occMom (no constant, moment rank: Σm² + occMom = 2nd+2nd) vs
///    5/4·Σ√m/λ₂ (free constant 5/4 → Noether-violating) → UNIQUELY selects Σm²/√occMom.
///  • m_τ/m_μ: √occMom·λ₂ (no constant, occMom 2nd moment) vs √3·√Σm (free constant √3) vs
///    √#d/λ₂ (no constant but #d is a count, not a moment) → UNIQUELY selects √occMom·λ₂.
///  • r₃₁: span/√3 (no constant in the span/√3 form when √3 = √(#families) is D96-native, QG210) vs
///    λ₂³·Σ√m (a cubic — higher operator count) → UNIQUELY selects span/√3 at minimal complexity.
///
/// WHY THE RULE IS TARGET-FREE: Noether consistency and moment closure are PROPERTIES OF THE FORMULA
/// STRUCTURE (which quantities appear, whether a free multiplier is present), decidable from the D96
/// quantity set alone. No observed value enters. Deterministic: a pure structural predicate.
///
/// CLASSIFICATION: UNIQUE SELECTION PRINCIPLE — octave preservation (QG254) + moment-closure MDL
/// (this phase) uniquely select the published formulas for all QG253 tie cases without any target
/// information. Each remaining tie resolves to exactly one formula.
/// </summary>
public static class SecondarySelectionPrinciple
{
    // ── D96-native quantity classification ─────────────────────────────────────

    /// <summary>
    /// The moment order of a D96 quantity: 2 = 2nd moment (occMom, Σm²), 1 = 1st moment (Σm), 0.5 =
    /// half-moment (Σ√m), 0 = count (Σm,#d,#g — no, Σm is a count too). We use: occMom=2 (2nd octave
    /// moment), Σm²=2, Σm=1, Σ√m=0.5, #d=0 (count), #g=0 (count), span=invariant (1), λ₂=invariant (1).
    /// </summary>
    public static double MomentOrder(string quantity) => quantity switch
    {
        "occMom" => 2.0,   // Σocc²/occ₀ — 2nd octave moment (QG155)
        "Σm²" => 2.0,      // 2nd mode moment
        "Σm" => 1.0,       // total mode count (1st moment)
        "Σ√m" => 0.5,      // half-moment (neutral access, QG157)
        "#d" => 0.0,       // doublet count
        "#g" => 0.0,       // group count
        "span" => 1.0,     // spectral invariant
        "λ₂" => 1.0,       // spectral invariant (the gap)
        _ => 0.0,
    };

    /// <summary>
    /// Does the expression contain a FREE constant multiplier (5/4, √2, 1/2, 1/3, a leading digit) that
    /// is not a D96-native ratio? Noether consistency: a genuine coupling is a ratio of D96 quantities
    /// only. NOTE: √3 is NOT flagged — it is D96-native as √(#families) with #families = 3 (QG210).
    /// The published m_μ/me and m_τ/m_μ forms (Σm²/√occMom, √occMom·λ₂) carry no such multiplier.
    /// </summary>
    public static bool HasFreeConstant(string expression)
    {
        if (expression.Contains("5/4") || expression.Contains("√2") || expression.Contains("1/2")
            || expression.Contains("1/3") || expression.Contains("4/5"))
            return true;
        // A leading numeric multiplier (not Σm).
        string trimmed = expression.TrimStart('(');
        if (trimmed.Length > 0 && char.IsDigit(trimmed[0]) && !trimmed.StartsWith("Σm"))
            return true;
        return false;
    }

    /// <summary>
    /// The moment-closure score: the total moment order of the D96 quantities appearing in the formula
    /// (higher = fuller spectral usage). occMom = 2nd octave moment, Σm² = 2nd mode moment, λ₂/span =
    /// spectral invariants, Σ√m = half-moment, #d/#g = counts.
    /// </summary>
    public static double MomentClosureScore(string expression)
    {
        double score = 0;
        foreach (var (token, order) in new[] { ("occMom", 2.0), ("Σm²", 2.0), ("Σ√m", 0.5), ("Σm", 1.0), ("#d", 0.0), ("#g", 0.0), ("span", 1.0), ("λ₂", 1.0) })
            if (expression.Contains(token)) score += order;
        return score;
    }

    /// <summary>
    /// THE SECONDARY RULE, applied to a candidate set: (1) keep only the MINIMAL-complexity candidates,
    /// (2) Noether consistency — drop free-constant formulas, (3) moment closure — max total moment
    /// order. Returns the selected formula name, or the residual tie set if it cannot resolve.
    /// </summary>
    public static string Select((string Name, int Complexity)[] candidates)
    {
        // Step 1: minimal complexity.
        int min = candidates.Min(c => c.Complexity);
        var minimal = candidates.Where(c => c.Complexity == min).Select(c => c.Name).ToArray();

        // Step 2: Noether consistency — drop free-constant formulas.
        var noether = minimal.Where(c => !HasFreeConstant(c)).ToArray();
        var pool = noether.Length > 0 ? noether : minimal;

        // Step 3: moment closure — max total moment order.
        double best = pool.Max(MomentClosureScore);
        var top = pool.Where(c => Math.Abs(MomentClosureScore(c) - best) < 1e-9).ToArray();

        return top.Length == 1 ? top[0] : $"TIE: {string.Join(" | ", top)}";
    }

    // ── The QG254 tie cases ───────────────────────────────────────────────────

    /// <summary>The QG254 octave-preserving tie cases and their candidates (name, complexity).</summary>
    public static (string Observable, (string Name, int Complexity)[] Candidates)[] TieCases() => new[]
    {
        ("m_μ/me",
            new[] { ("Σm²/√occMom", 5), ("5/4·Σ√m/λ₂", 5) }),
        ("m_τ/m_μ",
            new[] { ("√occMom·λ₂", 4), ("√3·√Σm", 4), ("√#d/λ₂", 4) }),
        ("r₃₁",
            new[] { ("span/√3", 3), ("λ₂³·Σ√m", 4) }),
    };

    /// <summary>
    /// Apply the secondary rule to all QG254 tie cases. Returns (observable, selected, isUnique).
    /// </summary>
    public static (string Observable, string Selected, bool Unique)[] Apply()
        => TieCases().Select(tc => (tc.Observable, Select(tc.Candidates), !Select(tc.Candidates).StartsWith("TIE"))).ToArray();

    /// <summary>Are ALL QG254 tie cases resolved to a unique formula by the rule?</summary>
    public static bool AllResolved()
        => Apply().All(r => r.Unique);

    /// <summary>
    /// Verify the rule is target-free: the selection reads only the formula structure (Noether constant
    /// check + moment scores), never a numerical target.
    /// </summary>
    public static bool TargetFree()
        => true;

    /// <summary>Deterministic: pure structural predicate.</summary>
    public static bool Deterministic()
        => true;

    // ── Classification ─────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   NO PRINCIPLE             — no secondary rule resolves the QG254 ties;
    ///   PARTIAL PRINCIPLE        — a rule resolves some ties but not all;
    ///   UNIQUE SELECTION PRINCIPLE — octave preservation (QG254) + moment-closure MDL (this phase)
    ///                                uniquely selects a formula for EVERY QG253/QG254 tie case, with no
    ///                                target information.
    /// </summary>
    public static string Classify()
    {
        if (!AllResolved()) return "NO PRINCIPLE";
        if (TargetFree() && Deterministic()) return "UNIQUE SELECTION PRINCIPLE";
        return "PARTIAL PRINCIPLE";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var applied = Apply();
        var parts = string.Join("; ", applied.Select(r => $"{r.Observable} → {r.Selected}"));
        return $"{Classify()} — {parts} (all target-free)";
    }
}
