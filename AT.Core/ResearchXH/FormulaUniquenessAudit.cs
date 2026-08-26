namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 253 — Formula Uniqueness Audit. QG239 and QG250 flagged RETRO-SELECTION RISK for the
/// closed-form relations (the formula was chosen with the target visible; no uniqueness principle).
/// This phase REPLACES empirical formula choice with a derivation-choice rule: generate ALL dimensionless
/// combinations of the D96 quantities, search for the minimal-complexity expression reproducing each
/// observable, and check whether the published formula is the SIMPLEST. No new physics — methodology only.
/// Deterministic.
///
/// THE SEARCH (the derivation-choice rule):
///  (1) D96 quantities: Σm=95, #d=42, #g=44, span=6.4025, λ₂=0.38635, occ₀=4, occ₁=4, occ₃=87,
///      occMom=1900.25, Σ√m=64.08.
///  (2) Generate candidate expressions: block forms (q, q², q³, √q, 1/q, ln q), affine differences
///      (q_i − q_j), products/ratios of two blocks, (affine × block), (affine / block), 1/(affine),
///      each with a small constant multiplier set {2, 3, 4, 5, 1/2, 1/3, 5/4, 4/5, √2, 1/√2, √3, 1/√3}.
///  (3) Complexity = (# distinct D96 quantities used) + (# binary operators) + (# unary functions
///      √, ln, ^2, ^3, 1/x) + (1 if a non-trivial constant multiplier was used, else 0).
///  (4) For each observable: find every candidate within 0.5% of the target; compute the MINIMAL
///      complexity among matches; count matches at that minimal complexity; check whether the published
///      formula is among them.
///
/// THE FINDING (honest, from the generated data):
///  • r₃₁ (span/√3) — UNIQUE: the sole minimal-complexity match (c=3) within 0.5% (λ₂³·Σ√m matches only at
///    c=4).
///  • m_μ/me (Σm²/√occMom) — NON-UNIQUE: published is minimal (c=5) but #g²/√occ₃ (dev 0.26%) and
///    5/4·Σ√m/λ₂ (dev 0.15%) tie at c=5.
///  • m_τ/m_μ (√occMom·λ₂) — NON-UNIQUE: published is minimal (c=4) but √3·√Σm (dev 0.24%) and √#d/λ₂
///    (dev 0.40%) tie at c=4.
///  • 1−n_s (ln(span)/(Σm−#d)) — MULTIPLE MATCHES: a SIMPLER expression 1/(span·ln occ₃) (c=5, dev 0.16%)
///    matches; the published form (c=7) is NOT the simplest.
///  • r₂₁ ((Σm−#d)·occ₁/occ₃) — MULTIPLE MATCHES: √Σm/occ₀ (c=4, dev 0.004%) is SIMPLER and more accurate
///    than the published (c=6).
///  • m₂/m₃ (2Σm/(Σ√m·√(span·#g))) — MULTIPLE MATCHES: 1/(occ₀·√2) (c=4, dev 0.100%) is SIMPLER.
///  • y_t/y_b (mass-law ratio) — MULTIPLE MATCHES: occ₀²/λ₂ (c=4, dev 0.37%) is SIMPLER.
///
///  CONCLUSION: only ONE of seven audited formulas (r₃₁) is the unique minimal-complexity expression.
///  Six of seven are NON-UNIQUE or MULTIPLE MATCHES; in four cases a STRICTLY SIMPLER expression
///  reproduces the target at least as well. The published formulas are therefore mostly NOT forced by a
///  minimal-complexity derivation-choice rule — the choice was empirical (target-informed), confirming
///  the QG239/QG250 RETRO-SELECTION RISK for all but r₃₁.
/// </summary>
public static class FormulaUniquenessAudit
{
    // ── D96 quantities ─────────────────────────────────────────────────────────

    public const double SigmaM = 95.0;
    public const double Doublets = 42.0;
    public const double Groups = 44.0;
    public const double Span = 6.4025;
    public const double Lambda2 = 0.386351;
    public const double Occ0 = 4.0;
    public const double Occ1 = 4.0;
    public const double Occ3 = 87.0;
    public const double OccMom = 1900.25;
    public const double SqrtSumM = 64.0825;

    /// <summary>The D96 quantity dictionary (name → value).</summary>
    public static IReadOnlyDictionary<string, double> Quantities() => new Dictionary<string, double>
    {
        ["Σm"] = SigmaM, ["#d"] = Doublets, ["#g"] = Groups, ["span"] = Span,
        ["λ₂"] = Lambda2, ["occ₀"] = Occ0, ["occ₁"] = Occ1, ["occ₃"] = Occ3,
        ["occMom"] = OccMom, ["Σ√m"] = SqrtSumM,
    };

    /// <summary>The constant multiplier set.</summary>
    public static IReadOnlyList<(string Name, double Value)> Constants() => new (string, double)[]
    {
        ("2", 2.0), ("3", 3.0), ("4", 4.0), ("5", 5.0),
        ("1/2", 0.5), ("1/3", 1.0 / 3.0), ("5/4", 1.25), ("4/5", 0.8),
        ("√2", Math.Sqrt(2.0)), ("1/√2", 1.0 / Math.Sqrt(2.0)),
        ("√3", Math.Sqrt(3.0)), ("1/√3", 1.0 / Math.Sqrt(3.0)),
    };

    // ── A candidate expression ─────────────────────────────────────────────────

    /// <summary>A generated expression: value, name, distinct-quantity count, operator count, constant flag.</summary>
    public sealed record Candidate(
        double Value,
        string Name,
        int Quantities,
        int Operators,
        bool ConstantUsed)
    {
        /// <summary>Complexity = distinct quantities + operators + (1 if a non-trivial constant was used).</summary>
        public int Complexity => Quantities + Operators + (ConstantUsed ? 1 : 0);
    }

    // ── The candidate generator (deterministic, bounded) ───────────────────────

    /// <summary>Generate all candidate expressions.</summary>
    public static Candidate[] Generate()
    {
        var qs = Quantities();
        var blocks = new List<Candidate>();
        foreach (var (qn, qv) in qs)
        {
            blocks.Add(new Candidate(qv, qn, 1, 0, false));
            blocks.Add(new Candidate(qv * qv, $"{qn}²", 1, 1, false));
            blocks.Add(new Candidate(qv * qv * qv, $"{qn}³", 1, 1, false));
            blocks.Add(new Candidate(Math.Sqrt(qv), $"√{qn}", 1, 1, false));
            blocks.Add(new Candidate(1.0 / qv, $"1/{qn}", 1, 1, false));
            if (qv > 0)
                blocks.Add(new Candidate(Math.Log(qv), $"ln {qn}", 1, 1, false));
        }

        // Affine differences (q_i − q_j).
        var affines = new List<Candidate>();
        foreach (var (a, av) in qs)
            foreach (var (b, bv) in qs)
            {
                if (a == b) continue;
                affines.Add(new Candidate(av - bv, $"({a}−{b})", 2, 1, false));
            }

        // Block × block and block / block (bounded complexity ≤ 6).
        var combos = new List<Candidate>();
        foreach (var a in blocks)
            foreach (var b in blocks)
            {
                if (a.Quantities + b.Quantities > 6) continue;
                combos.Add(new Candidate(a.Value * b.Value, $"{a.Name}·{b.Name}", a.Quantities + b.Quantities, a.Operators + b.Operators + 1, false));
                if (Math.Abs(b.Value) > 1e-9)
                    combos.Add(new Candidate(a.Value / b.Value, $"{a.Name}/{b.Name}", a.Quantities + b.Quantities, a.Operators + b.Operators + 1, false));
            }

        // (affine) × block and (affine) / block.
        var triples = new List<Candidate>();
        foreach (var af in affines)
            foreach (var b in blocks)
            {
                if (af.Quantities + b.Quantities > 6) continue;
                triples.Add(new Candidate(af.Value * b.Value, $"{af.Name}·{b.Name}", af.Quantities + b.Quantities, af.Operators + b.Operators + 1, false));
                if (Math.Abs(b.Value) > 1e-9)
                    triples.Add(new Candidate(af.Value / b.Value, $"{af.Name}/{b.Name}", af.Quantities + b.Quantities, af.Operators + b.Operators + 1, false));
            }

        // 1/(affine).
        var invAff = affines
            .Where(a => Math.Abs(a.Value) > 1e-9)
            .Select(a => new Candidate(1.0 / a.Value, $"1/{a.Name}", a.Quantities, a.Operators + 1, false))
            .ToList();

        var all = new List<Candidate>();
        all.AddRange(blocks);
        all.AddRange(affines);
        all.AddRange(combos);
        all.AddRange(triples);
        all.AddRange(invAff);

        // Apply constant multipliers.
        var withConstants = new List<Candidate>();
        foreach (var c in all)
            foreach (var (cn, cv) in Constants())
            {
                if (Math.Abs(cv - 1.0) < 1e-12) continue;
                withConstants.Add(new Candidate(c.Value * cv, $"{cn}·{c.Name}", c.Quantities, c.Operators + 1, true));
                if (Math.Abs(cv) > 1e-9)
                    withConstants.Add(new Candidate(c.Value / cv, $"{c.Name}/{cn}", c.Quantities, c.Operators + 1, true));
            }
        all.AddRange(withConstants);

        return all.ToArray();
    }

    /// <summary>The generated candidate pool (cached).</summary>
    public static Candidate[] Pool { get; } = Generate();

    // ── The observables ────────────────────────────────────────────────────────

    /// <summary>An audited observable: target value, published formula, and its evaluation/complexity.</summary>
    public sealed record Observable(
        string Name,
        double Target,
        string PublishedFormula,
        double PublishedValue,
        int PublishedComplexity)
    {
        /// <summary>Published deviation from the target.</summary>
        public double PublishedDeviation => Math.Abs(PublishedValue / Target - 1.0);
    }

    public static Observable[] Observables() => new[]
    {
        new Observable("m_μ/me", 207.03, "Σm²/√occMom", SigmaM * SigmaM / Math.Sqrt(OccMom), 5),
        new Observable("m_τ/m_μ", 16.842, "√occMom·λ₂", Math.Sqrt(OccMom) * Lambda2, 4),
        new Observable("1−n_s", 0.03503, "ln(span)/(Σm−#d)", Math.Log(Span) / (SigmaM - Doublets), 7),
        new Observable("r₂₁", 2.4368, "(Σm−#d)·occ₁/occ₃", (SigmaM - Doublets) * Occ1 / Occ3, 6),
        new Observable("r₃₁", 3.6965, "span/√3", Span / Math.Sqrt(3.0), 3),
        new Observable("m₂/m₃", 0.1766, "2Σm/(Σ√m·√(span·#g))", 2.0 * SigmaM / (SqrtSumM * Math.Sqrt(Span * Groups)), 8),
        new Observable("y_t/y_b", 41.26, "mass-law ratio m_t/m_b", 41.262, 8),
    };

    /// <summary>Tolerance for a match (0.5%).</summary>
    public const double Tolerance = 0.005;

    /// <summary>
    /// The analysis for one observable: minimal complexity among matches, count at minimal complexity,
    /// whether a strictly simpler match exists, and the classification.
    /// </summary>
    public sealed record Analysis(
        Observable Obs,
        int MinComplexity,
        int MatchesAtMin,
        bool SimplerExists,
        string Classification,
        (string Name, double Dev, int Complexity)[] TopMatches);

    /// <summary>
    /// Analyze one observable: find all candidates within tolerance, dedupe algebraic variants (same
    /// value and complexity), determine the minimal complexity, and classify:
    ///   UNIQUE            — the published formula is the ONLY formula at the minimal complexity
    ///                       (up to algebraic variants);
    ///   NON-UNIQUE        — the published formula ties with OTHER formulas at the same complexity
    ///                       (no strictly simpler formula exists);
    ///   MULTIPLE MATCHES  — a STRICTLY SIMPLER formula reproduces the target, so the published
    ///                       formula is not the simplest expression.
    /// </summary>
    public static Analysis Analyze(Observable obs)
    {
        var matches = Pool
            .Where(c => c.Value > 0 && Math.Abs(c.Value / obs.Target - 1.0) < Tolerance)
            .Select(c => (Name: c.Name, Dev: Math.Abs(c.Value / obs.Target - 1.0), c.Complexity, Val: c.Value))
            // Dedupe algebraic variants: same value (6 s.f.) and same complexity = one formula.
            .GroupBy(m => (Sig: Math.Round(m.Val, 6), m.Complexity))
            .Select(g => (Name: g.First().Name, Dev: g.First().Dev, Complexity: g.First().Complexity))
            .OrderBy(m => m.Complexity).ThenBy(m => m.Dev)
            .ToArray();

        // The published formula itself counts as a match if its own value is within tolerance.
        bool publishedMatches = obs.PublishedDeviation < Tolerance;
        int atMin = matches.Length > 0 ? matches.Count(m => m.Complexity == matches[0].Complexity) : 0;
        bool simplerExists = matches.Length > 0 && matches[0].Complexity < obs.PublishedComplexity;

        string cls;
        if (matches.Length == 0) cls = "NO MATCH";
        else if (simplerExists) cls = "MULTIPLE MATCHES";
        else if (publishedMatches && atMin == 1) cls = "UNIQUE";
        else cls = "NON-UNIQUE";

        return new Analysis(obs, matches.Length > 0 ? matches[0].Complexity : int.MaxValue, atMin, simplerExists, cls, matches.Take(5).ToArray());
    }

    /// <summary>Analyze all observables.</summary>
    public static Analysis[] AllAnalyses()
        => Observables().Select(Analyze).ToArray();

    /// <summary>Count of each classification across all observables.</summary>
    public static IReadOnlyDictionary<string, int> ClassificationCounts()
        => AllAnalyses().GroupBy(a => a.Classification).ToDictionary(g => g.Key, g => g.Count());

    /// <summary>Is any audited formula UNIQUE (the unique minimal-complexity expression)?</summary>
    public static bool AnyUnique()
        => AllAnalyses().Any(a => a.Classification == "UNIQUE");

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var counts = ClassificationCounts();
        var parts = string.Join(" / ", counts.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key} {kv.Value}"));
        return $"Formula uniqueness: {parts} of {Observables().Length} — "
             + (AnyUnique() ? "at least one UNIQUE" : "NO audited formula is the unique minimal-complexity expression");
    }
}
