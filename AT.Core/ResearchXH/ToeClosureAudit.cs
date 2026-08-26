namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 241 — TOE Closure Audit. Re-evaluates all ten TOE criteria from QG226 after the
/// QG227-QG240 derivation era. Each is classified DERIVED / PARTIAL / BOUNDARY / OPEN; the TOE
/// completeness is computed and the remaining true blockers determined. Audit only — no new physics.
///
/// THE TEN CRITERIA, RE-EVALUATED (QG226 status → QG241 status):
///  1. QUANTUM MECHANICS     — DERIVED (unchanged): magnitude |ψ|²=ρ (QG216), phase (QG220), complex
///     structure (QG218), measurement (QG74).
///  2. GRAVITY               — DERIVED (unchanged): structure (QG197/207), observables (QG181-213),
///     native dynamics (QG222).
///  3. MATTER                — DERIVED (unchanged): deficit dust (QG194/195/196), mass laws (QG203-211).
///  4. STANDARD MODEL        — PARTIAL (unchanged): all masses/couplings/mixings DERIVED (QG203-211),
///     but the gauge/fermion/Higgs interaction DYNAMICS remains hosted/compatible (QG60/76/85), not
///     fully derived.
///  5. COSMOLOGY             — PARTIAL (was 2.0/6 open): ALL SIX closure features now derived or partial
///     (expansion QG77, structure formation QG231, dark-matter effect QG206, Λ QG230, Ω_Λ/Ω_m QG234,
///     n_s QG237); the remaining partial item is the acoustic-peak recombination mechanism (QG238).
///  6. INITIAL CONDITIONS    — OPEN → DERIVED (QG227: the uniform critical state ρ_k = 1/K).
///  7. DIMENSIONALITY        — DERIVED (unchanged): QG2/3/5/159/160.
///  8. INFORMATION ORIGIN    — PARTIAL → DERIVED (QG228: information = KL deviation from uniform).
///  9. PRIMITIVE COMPLETENESS — PARTIAL → BOUNDARY (ψ is the second of exactly two primitives; its
///     existence is observationally demanded — an ontological boundary, QG223).
/// 10. PARAMETER COMPLETENESS — PARTIAL → BOUNDARY (every parameter is now DERIVED or a documented
///     boundary: Bekenstein 1/4 needs π, H is an epoch scale input; Ω_Λ/Ω_m derived QG234).
///
/// SCORE: DERIVED 1.0, PARTIAL 0.5, BOUNDARY 0.75, OPEN 0.0.
///   QM 1.0 + Gravity 1.0 + Matter 1.0 + SM 0.5 + Cosmology 0.5 + Initial conditions 1.0
///   + Dimensionality 1.0 + Information 1.0 + Primitive 0.75 + Parameter 0.75 = 8.5/10.
///
/// TOE COMPLETENESS = 8.5/10 (85%). 6 DERIVED, 2 PARTIAL, 2 BOUNDARY, 0 OPEN.
///
/// REMAINING TRUE BLOCKERS — none are OPEN; the two PARTIAL items are derivations-in-progress, not
/// impossibilities:
///  (a) the full SM gauge/fermion/Higgs interaction DYNAMICS (masses/couplings/mixings derived, the
///      interaction dynamics hosted — a derivation, not a boundary);
///  (b) the CMB acoustic-peak recombination mechanism (the peak positions derived, the
///      sound-horizon/recombination physics partial — a derivation, not a boundary).
/// The BOUNDARY items (ψ primitive existence, Bekenstein 1/4, H) are documented, not blockers.
///
/// CLASSIFICATION: NEAR-COMPLETE TOE — 85% complete with 0 OPEN criteria; the only remaining gaps are
/// two partial derivations (SM dynamics, acoustic mechanism) and the documented ontological boundaries.
/// </summary>
public static class ToeClosureAudit
{
    public enum Status { Derived, Partial, Boundary, Open }

    /// <summary>A re-evaluated TOE criterion.</summary>
    public sealed record Criterion(
        int Index,
        string Name,
        Status Status,
        string Qg226Status,
        string Evidence);

    /// <summary>The ten TOE criteria re-evaluated.</summary>
    public static Criterion[] Criteria() => new[]
    {
        new Criterion(1, "Quantum Mechanics", Status.Derived, "DERIVED",
            "magnitude |ψ|²=ρ (QG216), phase (QG220), complex structure (QG218), measurement (QG74)"),
        new Criterion(2, "Gravity", Status.Derived, "DERIVED",
            "structure (QG197/207), observables (QG181-213), native dynamics (QG222)"),
        new Criterion(3, "Matter", Status.Derived, "DERIVED",
            "deficit dust (QG194/195/196), mass laws (QG203-211)"),
        new Criterion(4, "Standard Model", Status.Partial, "PARTIAL",
            "all masses/couplings/mixings DERIVED (QG203-211); gauge/fermion/Higgs interaction DYNAMICS hosted/compatible (QG60/76/85), not fully derived"),
        new Criterion(5, "Cosmology", Status.Partial, "PARTIAL",
            "all six features now derived or partial (expansion QG77, structure QG231, dark matter QG206, Λ QG230, Ω_Λ/Ω_m QG234, n_s QG237); the acoustic-peak recombination mechanism remains partial (QG238)"),
        new Criterion(6, "Initial conditions", Status.Derived, "OPEN",
            "QG227: the uniform critical state ρ_k = 1/K (the unique minimum-information fixed point)"),
        new Criterion(7, "Dimensionality", Status.Derived, "DERIVED",
            "QG2/3/5/159/160 (dimension from network structure; 3+1; D96 selection)"),
        new Criterion(8, "Information origin", Status.Derived, "PARTIAL",
            "QG228: information = KL(ρ‖uniform) > 0 from the mandatory Poisson fluctuations"),
        new Criterion(9, "Primitive completeness", Status.Boundary, "PARTIAL",
            "ψ is the second of exactly two primitives; its existence is observationally demanded — an ontological boundary (QG223), not a gap"),
        new Criterion(10, "Parameter completeness", Status.Boundary, "PARTIAL",
            "every parameter is DERIVED or a documented boundary: Bekenstein 1/4 needs π (QG196), H is an epoch scale; Ω_Λ/Ω_m derived (QG234)"),
    };

    /// <summary>Sub-score: Derived=1, Partial=0.5, Boundary=0.75, Open=0.</summary>
    public static double SubScore(Status s) => s switch
    {
        Status.Derived => 1.0,
        Status.Partial => 0.5,
        Status.Boundary => 0.75,
        _ => 0.0,
    };

    /// <summary>Total TOE completeness (0..10).</summary>
    public static double TotalScore()
        => Criteria().Sum(c => SubScore(c.Status));

    /// <summary>Completeness fraction (0..1).</summary>
    public static double CompletenessFraction()
        => TotalScore() / 10.0;

    /// <summary>Status counts.</summary>
    public static IReadOnlyDictionary<Status, int> StatusCounts()
    {
        var dict = Criteria().GroupBy(c => c.Status).ToDictionary(g => g.Key, g => g.Count());
        foreach (Status s in Enum.GetValues<Status>())
            if (!dict.ContainsKey(s)) dict[s] = 0;
        return dict;
    }

    /// <summary>The remaining true blockers (PARTIAL derivations; none OPEN).</summary>
    public static string[] RemainingBlockers()
        => Criteria().Where(c => c.Status == Status.Partial).Select(c => c.Name).ToArray();

    /// <summary>Are there any OPEN criteria? No.</summary>
    public static bool HasOpenCriteria()
        => StatusCounts()[Status.Open] > 0;

    /// <summary>
    /// Classification: PARTIAL TOE (&lt; 70% or any OPEN), NEAR-COMPLETE TOE (70-95% with no OPEN),
    /// COMPLETE TOE (≥ 95% with no OPEN and no PARTIAL).
    /// </summary>
    public static string Classify()
    {
        double score = CompletenessFraction();
        int partial = StatusCounts()[Status.Partial];
        int open = StatusCounts()[Status.Open];
        if (open > 0 || score < 0.70) return "PARTIAL TOE";
        if (score >= 0.95 && partial == 0) return "COMPLETE TOE";
        return "NEAR-COMPLETE TOE";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var sc = StatusCounts();
        return $"{Classify()} — completeness {CompletenessFraction():P1} ({TotalScore():F1}/10); "
             + $"{sc[Status.Derived]} DERIVED / {sc[Status.Partial]} PARTIAL / "
             + $"{sc[Status.Boundary]} BOUNDARY / {sc[Status.Open]} OPEN";
    }
}
