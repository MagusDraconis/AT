namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 229 — Cosmology Closure Audit. Determine the exact cosmology gap. Reviews QG77 (the
/// original cosmology compatibility audit) and the QG194-228 derivations that touch the cosmology sector.
/// Six cosmology features are classified DERIVED / PARTIAL / OPEN, and the single highest-impact remaining
/// blocker is identified. Audit only — no new physics, no new derivations. Deterministic.
///
/// THE SIX FEATURES:
///  1. EXPANSION — DERIVED (QG77): expansion = the gravitational redshift (QG26) + the scale-free ρ
///     evolution (G4-RHO). The native metric g = ρ^(2/d)η gives FRW geometry a = ρ^(1/d). Closed.
///  2. STRUCTURE FORMATION — OPEN: density-perturbation growth and galaxy clustering are NOT derived
///     (QG77 UNKNOWN). No phase in QG194-228 derives the growth of the deficit-density perturbations into
///     bound structures. The initial-condition (QG227) and information-content (QG228) derivations provide
///     the SEED spectrum (fluctuations with non-zero variance) but not the growth law.
///  3. DARK MATTER — PARTIAL: the dark-matter EFFECT (flat rotation curves) is DERIVED — matter = the
///     deficit ρ̄−ρ (QG194/195) with α=0 (QG206: flat rotation) and M∝R (QG184). But this is a field/effect
///     picture (the deficit), not a particle; the CMB / structure implications are not derived.
///  4. DARK ENERGY — OPEN: no phase derives cosmic acceleration / the dark-energy sector (QG77 UNKNOWN).
///     Nothing in QG194-228 addresses Λ or the accelerating expansion.
///  5. Λ (cosmological constant) — OPEN: no origin for Λ; QG88 (parameter value selection) is PARTIAL
///     CONSTRAINT and does not select Λ; QG77 marks it UNKNOWN.
///  6. CMB-COMPATIBLE STRUCTURE — PARTIAL: the conformal metric hosts FRW geometry and CMB ISOTROPY is
///     COMPATIBLE (QG77), but the CMB ANISOTROPY SPECTRUM (the perturbation imprint) is not derived —
///     it requires structure formation (feature 2).
///
/// THE SINGLE HIGHEST-IMPACT BLOCKER: DARK ENERGY / Λ. It constitutes the majority of the universe's energy
/// budget (the accelerated expansion), it is completely underived (no candidate mechanism exists in
/// QG194-228), and it is the largest single cosmological feature. Structure formation is the runner-up
/// (needed for all observed structure), but dark energy is the highest-impact because it dominates the
/// energy budget and has zero derivation.
///
/// SCORE (0..6): expansion 1.0, structure formation 0.0, dark matter 0.5, dark energy 0.0, Λ 0.0,
/// CMB structure 0.5 → TOTAL 2.0/6.
///
/// CLASSIFICATION: PARTIAL COSMOLOGY — expansion and the dark-matter effect are derived (QG77 + QG206),
/// CMB isotropy is compatible, but structure formation, dark energy, and Λ remain OPEN. The cosmology
/// sector is substantially closer than QG77's "UNKNOWN" but not closed.
/// </summary>
public static class CosmologyClosureAudit
{
    public enum Status { Derived, Partial, Open }

    /// <summary>A cosmology feature with its status and evidence.</summary>
    public sealed record Feature(
        int Index,
        string Name,
        Status Status,
        string Evidence);

    /// <summary>The six cosmology features.</summary>
    public static Feature[] Features() => new[]
    {
        new Feature(1, "Expansion", Status.Derived,
            "QG77: expansion = redshift (QG26) + scale-free ρ evolution; FRW geometry a = ρ^(1/d)"),
        new Feature(2, "Structure formation", Status.Open,
            "QG77 UNKNOWN; no growth law for the deficit perturbations — the QG227/228 seeds lack dynamics"),
        new Feature(3, "Dark matter", Status.Partial,
            "DERIVED as an effect: matter = deficit (QG194/195), α=0 flat rotation (QG206), M∝R (QG184) — not a particle, no CMB/structure implications"),
        new Feature(4, "Dark energy", Status.Open,
            "QG77 UNKNOWN; no mechanism for cosmic acceleration in QG194-228"),
        new Feature(5, "Λ (cosmological constant)", Status.Open,
            "no origin for Λ; QG88 value selection PARTIAL CONSTRAINT does not select it; QG77 UNKNOWN"),
        new Feature(6, "CMB-compatible structure", Status.Partial,
            "conformal metric hosts FRW + CMB ISOTROPY compatible (QG77); the anisotropy spectrum requires structure formation (feature 2)"),
    };

    /// <summary>Sub-score: Derived=1.0, Partial=0.5, Open=0.0.</summary>
    public static double SubScore(Status s) => s switch
    {
        Status.Derived => 1.0,
        Status.Partial => 0.5,
        _ => 0.0,
    };

    /// <summary>Total cosmology closure score (0..6).</summary>
    public static double TotalScore()
        => Features().Sum(f => SubScore(f.Status));

    /// <summary>Count of features in each status.</summary>
    public static IReadOnlyDictionary<Status, int> StatusCounts()
        => Features().GroupBy(f => f.Status).ToDictionary(g => g.Key, g => g.Count());

    // ── The single highest-impact blocker ─────────────────────────────────────

    /// <summary>The highest-impact remaining cosmology blocker: dark energy / Λ.</summary>
    public static (string Name, string Why) HighestImpactBlocker() =>
        ("Dark energy / Λ",
         "constitutes the majority of the universe's energy budget (accelerated expansion), is completely "
         + "underived (no candidate mechanism in QG194-228), and is the largest single cosmological feature; "
         + "structure formation is the runner-up (needed for all observed structure), but dark energy "
         + "dominates the budget with zero derivation");

    // ── Classification ────────────────────────────────────────────────────────

    /// <summary>
    /// Cosmology closure classification:
    ///   NOT CLOSED         — the cosmology sector is essentially open (score &lt; 2.0);
    ///   PARTIAL COSMOLOGY  — expansion and the dark-matter effect are derived, but structure formation,
    ///                        dark energy, and Λ remain open (score 2.0-4.4);
    ///   COSMOLOGY COMPLETE — every feature is derived or the remaining gaps are minor (score ≥ 4.5).
    /// </summary>
    public static string Classify()
    {
        double score = TotalScore();
        if (score >= 4.5) return "COSMOLOGY COMPLETE";
        if (score >= 2.0) return "PARTIAL COSMOLOGY";
        return "NOT CLOSED";
    }

    /// <summary>Summary string (e.g., "PARTIAL COSMOLOGY (2.0/6); derived 1, partial 2, open 3").</summary>
    public static string Summary()
    {
        var sc = StatusCounts();
        return $"{Classify()} ({TotalScore():F1}/6); derived {sc[Status.Derived]}, partial {sc[Status.Partial]}, open {sc[Status.Open]}";
    }
}
