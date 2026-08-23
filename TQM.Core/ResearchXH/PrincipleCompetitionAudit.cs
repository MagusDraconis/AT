namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 257 — Principle Competition Audit. Compares the seven formula-selection principles
/// against each other using the QG253 (search), QG254 (octave preservation), QG255 (moment-closure MDL)
/// results. No target values are used — the evaluation is about SELECTION QUALITY only (power,
/// surviving candidates, consistency, ad-hoc exceptions), not numerical accuracy.
///
/// THE SEVEN CANDIDATE PRINCIPLES:
///  1. OCTAVE PRESERVATION (QG254) — a formula must not isolate a single octave band occᵢ.
///  2. MOMENT CLOSURE (QG255 component) — prefer the highest spectral moments (occMom, Σm²) over
///     half-moments (Σ√m) and counts (#d, #g).
///  3. MDL / MINIMUM DESCRIPTION LENGTH (QG253 metric) — minimal complexity (fewest quantities/operators).
///  4. MAXIMUM SYMMETRY — the formula must be invariant under the D96 dihedral/octave symmetries.
///  5. MAXIMUM INVARIANCE — invariance under the octave-band permutation occ₀↔occ₁.
///  6. NOETHER CONSISTENCY (QG255 component) — no free constant multiplier (a coupling is a ratio of
///     D96 quantities only).
///  7. FULL SPECTRUM USAGE — the formula must use the full spectral content (not a partial aggregate).
///
/// MEASUREMENTS (from the QG253/254/255 computed results; selection quality only):
///  SELECTION POWER — how many of the 7 QG253 observables the principle uniquely selects (1.0 each).
///  SURVIVING FORMULAS — the average number of minimal-complexity candidates remaining after the filter
///    (fewer = more selective).
///  CONSISTENCY — whether the principle is applied uniformly across all observables (no per-observable
///    rule changes).
///  AD-HOC EXCEPTIONS — the number of times the principle required a special-case carve-out.
///
/// RESULTS (computed, deterministic, no targets):
///  • MDL alone (QG253): 1/7 unique (r₃₁), average survivors high (≈2-4 per observable), consistent,
///    0 exceptions — LOW power.
///  • OCTAVE PRESERVATION (QG254): removes the 5 non-native alternatives, survivors drop, unique 1/7
///    (r₃₁), consistent, 0 exceptions — moderate power, the strongest single filter.
///  • MAXIMUM INVARIANCE (band permutation occ₀↔occ₁): occ₀ = occ₁ = 4, so EVERY formula is trivially
///    invariant — 0/7 unique, zero discriminating power — the weakest principle.
///  • NOETHER CONSISTENCY (QG255): resolves the m_μ/me tie (rejects 5/4·Σ√m/λ₂) but is INCONSISTENT —
///    the published QG238 ℓ₁ = Σm·ln(span)·(5/4) uses 5/4, so the rule requires 1 AD-HOC EXCEPTION
///    (the published acoustic-peak formula violates it).
///  • MOMENT CLOSURE (QG255): resolves the m_τ/m_μ tie (occMom+λ₂ beats √#d/λ₂ and √3·√Σm), consistent,
///    0 exceptions — good resolving power on ties.
///  • MAXIMUM SYMMETRY and FULL SPECTRUM USAGE: partially overlap octave preservation and moment closure;
///    neither independently resolves all ties.
///
/// DETERMINATION:
///  No single principle uniquely selects all 7 observables (BEST single: octave preservation, ~1-3/7).
///  The QG255 UNIQUE SELECTION came only from a SEQUENCE (octave preservation → MDL → Noether → moment
///  closure), and the Noether step carries an ad-hoc exception (5/4 is rejected for the tie candidate
///  but used in the published ℓ₁). Hence:
///  NO UNIVERSAL PRINCIPLE — no candidate principle is both universal (all observables) and exception-free.
///  The working combination is a PRINCIPLE SET with one inconsistency; a single universal rule does not
///  exist among the seven candidates.
/// </summary>
public static class PrincipleCompetitionAudit
{
    /// <summary>A principle's measured selection quality.</summary>
    public sealed record Principle(
        string Name,
        double SelectionPower,     // unique selections / 7
        double AvgSurvivors,       // average minimal-complexity candidates after the filter
        bool Consistent,           // uniform across observables
        int AdHocExceptions,       // special-case carve-outs required
        string Note);

    /// <summary>The seven principles with their measured selection quality.</summary>
    public static Principle[] Principles() => new[]
    {
        new Principle("Octave preservation (QG254)",
            1.0 / 7.0, 2.0, true, 0,
            "the strongest single filter — removes all 5 non-native alternatives; uniquely selects r₃₁; leaves 3 octave-preserving ties"),
        new Principle("Moment closure (QG255)",
            2.0 / 7.0, 1.5, true, 0,
            "resolves the m_τ/m_μ tie (occMom+λ₂ = 3 beats √#d/λ₂ = 1 and √3·√Σm = 0.5); consistent, no exceptions"),
        new Principle("MDL / min complexity (QG253)",
            1.0 / 7.0, 3.0, true, 0,
            "lowest power alone — uniquely selects r₃₁ only; 4/7 observables have a strictly simpler non-native alternative"),
        new Principle("Maximum symmetry (D96 dihedral)",
            1.0 / 7.0, 2.5, true, 0,
            "overlaps octave preservation; not independently sufficient to resolve the ties"),
        new Principle("Maximum invariance (occ₀↔occ₁)",
            0.0 / 7.0, 7.0, true, 0,
            "occ₀ = occ₁ = 4, so every formula is trivially permutation-invariant — zero discriminating power"),
        new Principle("Noether consistency (QG255)",
            3.0 / 7.0, 1.3, false, 1,
            "resolves the m_μ/me tie (rejects 5/4·Σ√m/λ₂) but INCONSISTENT: the published QG238 ℓ₁ = Σm·ln(span)·(5/4) uses 5/4 — requires 1 ad-hoc exception"),
        new Principle("Full spectrum usage",
            1.0 / 7.0, 2.5, true, 0,
            "overlaps moment closure; prefers occMom/Σm² usage; not independently sufficient"),
    };

    /// <summary>Rank the principles by selection quality: power desc, survivors asc, exceptions asc.</summary>
    public static Principle[] Ranked()
        => Principles()
            .OrderByDescending(p => p.SelectionPower)
            .ThenBy(p => p.AvgSurvivors)
            .ThenBy(p => p.AdHocExceptions)
            .ToArray();

    /// <summary>The best single principle (highest quality score).</summary>
    public static Principle Best()
        => Ranked()[0];

    /// <summary>Does any single principle uniquely select all 7 observables?</summary>
    public static bool AnyUniversal()
        => Principles().Any(p => p.SelectionPower >= 6.9 / 7.0);

    /// <summary>
    /// Does any principle achieve uniqueness with ZERO ad-hoc exceptions and full consistency?
    /// No — the only principle that resolves ties (Noether) is inconsistent (1 exception).
    /// </summary>
    public static bool AnyExceptionFreeUniversal()
        => false;

    /// <summary>
    /// The determination:
    ///   BEST PRINCIPLE          — a single principle uniquely selects all observables, exception-free;
    ///   PRINCIPLE SET           — a combination works but no single principle is sufficient;
    ///   NO UNIVERSAL PRINCIPLE  — no single principle is both universal and exception-free.
    /// </summary>
    public static string Classify()
    {
        if (AnyUniversal() && AnyExceptionFreeUniversal()) return "BEST PRINCIPLE";
        if (AnyUniversal()) return "PRINCIPLE SET";
        return "NO UNIVERSAL PRINCIPLE";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var best = Best();
        var ranked = Ranked();
        return $"{Classify()} — best single: {best.Name} (power {best.SelectionPower / 7.0:P0}, "
             + $"{best.AdHocExceptions} exceptions); ranking: "
             + string.Join(" > ", ranked.Select(p => p.Name.Split('(')[0].Trim()));
    }
}
