namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 258 — Blind Formula Tournament. The decisive test of whether the QG254/QG255 D96
/// selection rules have PREDICTIVE power: can they select a formula BEFORE the target is known?
///
/// THE TOURNAMENT (fully blind):
///  (1) INPUT — the D96 quantities only (Σm, #d, #g, span, λ₂, Σ√m, occ, occMom). No observable value
///      and no target value is used at any point in the selection.
///  (2) GENERATE — all expressions up to complexity 6 from the D96 quantity set (the same candidate
///      pool as QG253, restricted to complexity ≤ 6).
///  (3) STRUCTURE — all seven observables are dimensionless RATIOS, so the pool is restricted to
///      ratio-form expressions (a division operator, ≥ 2 distinct quantities). This is structural
///      knowledge about the observable class, not a target value.
///  (4) APPLY — QG254 octave preservation (a formula must not isolate a single octave band), then
///      QG255 moment-closure MDL (minimal complexity, then Noether consistency — no free constant —
///      then max moment-closure score).
///  (5) SELECT — the TOP formula only.
///  (6) LOCK — the selected formula is frozen.
///  (7) REVEAL — only after locking is the target value consulted.
///  (8) SCORE — success iff the locked formula reproduces the revealed target within 1%.
///
/// THE HONEST FINDING: the target-free rule chain is DEGENERATE — it selects the SAME top formula
/// (λ₂/occMom, the globally minimal-complexity octave-preserving ratio) for every observable, and that
/// formula matches NONE of the seven targets. The rules therefore have NO blind predictive power: a
/// formula cannot be selected for a specific observable without any reference to what that observable
/// is. This is the decisive confirmation of QG256/QG257 — the QG254/QG255 rules are a heuristic
/// narrowing that only "works" when the candidate pool is pre-restricted by the target (as in QG253),
/// not a predictive selection principle.
///
/// CLASSIFICATION: WEAK — the blind success rate is 0/7.
/// </summary>
public static class BlindFormulaTournament
{
    /// <summary>The maximum complexity allowed in the candidate pool.</summary>
    public const int MaxComplexity = 6;

    // ── The observable targets (revealed ONLY after locking) ──────────────────

    /// <summary>An observable with its hidden target and published formula.</summary>
    public sealed record Observable(
        string Name,
        double Target,          // revealed only after selection
        string PublishedFormula,
        int PublishedComplexity);

    /// <summary>The seven observables (targets are hidden during selection).</summary>
    public static Observable[] Observables() => new[]
    {
        new Observable("n_s (1−n_s)", 0.03503, "ln(span)/(Σm−#d)", 7),
        new Observable("r₂₁", 2.4368, "(Σm−#d)·occ₁/occ₃", 6),
        new Observable("r₃₁", 3.6965, "span/√3", 3),
        new Observable("m₂/m₃", 0.1766, "2Σm/(Σ√m·√(span·#g))", 8),
        new Observable("y_t/y_b", 41.26, "mass-law ratio", 8),
        new Observable("m_μ/me", 207.03, "Σm²/√occMom", 5),
        new Observable("m_τ/m_μ", 16.842, "√occMom·λ₂", 4),
    };

    // ── The blind candidate pool (complexity ≤ 6, D96 quantities only) ─────────

    /// <summary>
    /// The candidate pool: the QG253 pool restricted to complexity ≤ MaxComplexity.
    /// Generated from D96 quantities only — no target enters here.
    /// </summary>
    public static (string Name, double Value, int Complexity)[] Pool()
        => FormulaUniquenessAudit.Pool
            .Where(c => c.Complexity <= MaxComplexity)
            .Select(c => (c.Name, c.Value, c.Complexity))
            .ToArray();

    // ── The rule chain (QG254 + QG255), target-free ───────────────────────────

    /// <summary>
    /// Apply octave preservation (QG254) to the pool: keep only formulas that do not isolate a single
    /// octave band and are genuine RATIOS (all seven observables are dimensionless ratios, so the
    /// candidate pool is restricted to ratio-form expressions — a structural fact, not a target value).
    /// Then rank by QG255 moment-closure MDL. Returns the top-ranked formula.
    /// </summary>
    public static string SelectTopFormula((string Name, double Value, int Complexity)[] pool)
    {
        string[] tokens = { "Σm", "#d", "#g", "span", "λ₂", "Σ√m", "occMom" };
        int DistinctCount(string name) => tokens.Count(t => name.Contains(t));

        // Structural: all observables are dimensionless RATIOS — keep ratio-form expressions
        // (a division operator) with ≥ 2 distinct quantities.
        var ratioPool = pool
            .Where(c => c.Name.Contains('/') && DistinctCount(c.Name) >= 2)
            .ToArray();

        // QG254: octave preservation.
        var octavePreserved = ratioPool
            .Where(c => FormulaSelectionPrinciple.IsOctavePreserving(c.Name))
            .ToArray();

        // QG255: minimal complexity.
        int min = octavePreserved.Min(c => c.Complexity);
        var minimal = octavePreserved.Where(c => c.Complexity == min).ToArray();

        // QG255: Noether consistency — drop free-constant formulas (5/4, √2, leading digits).
        var noether = minimal.Where(c => !SecondarySelectionPrinciple.HasFreeConstant(c.Name)).ToArray();
        var afterNoether = noether.Length > 0 ? noether : minimal;

        // QG255: moment closure — max total moment order.
        double best = afterNoether.Max(c => SecondarySelectionPrinciple.MomentClosureScore(c.Name));
        var top = afterNoether
            .Where(c => Math.Abs(SecondarySelectionPrinciple.MomentClosureScore(c.Name) - best) < 1e-9)
            .ToArray();

        // Select the top formula: the unique survivor, or the first (lowest complexity, then value
        // sorted deterministically) if a tie remains after all rules.
        return top.OrderBy(c => c.Complexity).ThenBy(c => c.Value).First().Name;
    }

    // ── The locked-then-revealed scoring ───────────────────────────────────────

    /// <summary>
    /// Run the tournament for one observable: select the top formula BLIND, then reveal the target and
    /// score. Success iff the selected formula reproduces the target within 1%.
    /// </summary>
    public static (string Observable, string Selected, double SelectedValue, double Target, double Deviation, bool Success) Run(Observable obs)
    {
        string selected = SelectTopFormula(Pool());
        double value = FormulaUniquenessAudit.Pool.First(c => c.Name == selected).Value;
        double dev = Math.Abs(value / obs.Target - 1.0);
        return (obs.Name, selected, value, obs.Target, dev, dev < 0.01);
    }

    /// <summary>Run the tournament for all seven observables.</summary>
    public static (string Observable, string Selected, double SelectedValue, double Target, double Deviation, bool Success)[] RunAll()
        => Observables().Select(Run).ToArray();

    /// <summary>The selection success rate (fraction of observables the blind rule chain hit within 1%).</summary>
    public static double SuccessRate()
    {
        var results = RunAll();
        return (double)results.Count(r => r.Success) / results.Length;
    }

    /// <summary>
    /// Classification by success rate:
    ///   WEAK       &lt; 30%
    ///   MODERATE   30–59%
    ///   STRONG     60–84%
    ///   PREDICTIVE ≥ 85%
    /// </summary>
    public static string Classify()
    {
        double rate = SuccessRate();
        if (rate >= 0.85) return "PREDICTIVE";
        if (rate >= 0.60) return "STRONG";
        if (rate >= 0.30) return "MODERATE";
        return "WEAK";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var results = RunAll();
        var parts = string.Join("; ", results.Select(r => $"{r.Observable} → {r.Selected} (dev {r.Deviation * 100:F2}%, {(r.Success ? "HIT" : "MISS")})"));
        return $"{Classify()} — success {SuccessRate():P0} ({results.Count(r => r.Success)}/{results.Length}); {parts}";
    }
}
