namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 249 — Final TOE Audit. Re-evaluates the theory-of-everything status after the
/// QG223-QG248 era, using the external TOE checklist (QG235), the ten TOE criteria (QG226/241), and
/// the SM-dynamics closure (QG248). Every remaining item is classified DERIVED / PARTIAL / BOUNDARY /
/// OPEN; the four determination questions are answered; the output is PARTIAL TOE / NEAR-COMPLETE TOE /
/// COMPLETE TOE, plus the top-10 strongest remaining criticisms. Audit only — no new physics.
///
/// THE TEN TOE CRITERIA, FINAL (QG241 status → QG249 status):
///  1. QUANTUM MECHANICS     — DERIVED (unchanged): magnitude |ψ|²=ρ (QG216), phase (QG220), complex
///     structure (QG218), measurement (QG74).
///  2. GRAVITY               — DERIVED (unchanged): structure (QG197/207), observables (QG181-213),
///     native dynamics (QG222).
///  3. MATTER                — DERIVED (unchanged): deficit dust (QG194/195/196), mass laws (QG203-211).
///  4. STANDARD MODEL        — PARTIAL → DERIVED (QG248: SM DYNAMICS COMPLETE — gauge dynamics QG243/244,
///     Higgs potential + SSB QG246, Yukawa + mass mechanism QG247; 8 DERIVED / 1 framework-partial /
///     1 boundary in the ten-component audit).
///  5. COSMOLOGY             — PARTIAL (unchanged): all six features derived or partial (expansion QG77,
///     structure QG231, dark matter QG206, Λ QG230, Ω_Λ/Ω_m QG234, n_s QG237); the remaining partial is
///     the acoustic-peak recombination mechanism (QG238: peak positions derived, sound-horizon/
///     recombination physics partial).
///  6. INITIAL CONDITIONS    — DERIVED (unchanged): QG227 uniform critical state.
///  7. DIMENSIONALITY        — DERIVED (unchanged): QG2/3/5/159/160.
///  8. INFORMATION ORIGIN    — DERIVED (unchanged): QG228 KL deviation.
///  9. PRIMITIVE COMPLETENESS — BOUNDARY (unchanged): ψ is the second of exactly two primitives;
///     observational boundary (QG223).
/// 10. PARAMETER COMPLETENESS — BOUNDARY (unchanged): Bekenstein 1/4 needs π (QG196), H is an epoch
///     scale; Ω_Λ/Ω_m derived (QG234).
///
/// SCORE: DERIVED 1.0, PARTIAL 0.5, BOUNDARY 0.75, OPEN 0.0.
///   QM 1.0 + Gravity 1.0 + Matter 1.0 + SM 1.0 + Cosmology 0.5 + Initial 1.0
///   + Dimensionality 1.0 + Information 1.0 + Primitive 0.75 + Parameter 0.75 = 9.0/10.
///
/// TOE COMPLETENESS = 9.0/10 (90%). 7 DERIVED, 1 PARTIAL, 2 BOUNDARY, 0 OPEN.
/// (QG241 was 8.5/10; QG248's SM completion adds the +0.5.)
///
/// THE FOUR DETERMINATION QUESTIONS:
///  (1) ANY TRUE MISSING PHYSICS? — NO OPEN criterion. The single PARTIAL (acoustic-peak recombination
///      mechanism) is a derivation-in-progress with its target values already derived (QG238: ℓ₁ 0.008%,
///      r₂₁ 0.035%, r₃₁ 0.058%); the BOUNDARY items (ψ, Bekenstein 1/4, H, SU(3) color-count) are
///      documented. No true missing physics remains — only a partial derivation and stated boundaries.
///  (2) ANY HOSTED CORE DYNAMICS? — NO. QG248 closed the last hosted core (SM dynamics): the gauge
///      dynamics, Higgs sector, and Yukawa sector are all DERIVED from D96. The only remaining hosted
///      item is the propagator/quantization MACHINERY (QG248 PARTIAL, framework-completeness — the
///      quadratic operator content is derived, the momentum-space Feynman formalism is the standard host).
///      Core dynamics: none hosted.
///  (3) ANY UNRESOLVED CONTRADICTION? — ONE partially-resolved internal tension: C4 (perihelion tensor-vs-
///      scalar) remains PARTIALLY RESOLVED in the coverage register — QG212 clarifies the sectors (scalar
///      ψ restores γ = +1; GW is the spin-2 observable; QG103 perihelion derived), but the register entry
///      has not been re-adjudicated to RESOLVED. C1-C3, C5-C7 are RESOLVED. This is a documentation
///      re-adjudication item, not a physics blocker.
///  (4) ANY REMAINING TOE BLOCKER? — NO OPEN blocker. The path to COMPLETE TOE requires only: (a) the
///      acoustic-peak recombination mechanism (Cosmology partial), (b) re-adjudicating C4 to RESOLVED
///      (documentation), and (c) accepting the documented boundaries (ψ primitive, Bekenstein π, H epoch
///      scale, SU(3) color-count). None is an impossibility.
///
/// CLASSIFICATION: NEAR-COMPLETE TOE — 90% complete, 0 OPEN, 1 PARTIAL, 2 BOUNDARY. The SM dynamics
/// criterion is now DERIVED (QG248); the single remaining partial derivation is the CMB acoustic-peak
/// recombination mechanism. COMPLETE TOE would require closing that partial (and the documented C4
/// re-adjudication) — at which point only the stated ontological/π/epoch boundaries remain.
/// </summary>
public static class FinalToeAudit
{
    public enum Status { Derived, Partial, Boundary, Open }

    /// <summary>A final TOE criterion.</summary>
    public sealed record Criterion(
        int Index,
        string Name,
        Status Status,
        string Qg241Status,
        string Evidence);

    /// <summary>A remaining criticism (ranked).</summary>
    public sealed record Criticism(
        int Rank,
        string Area,
        string Statement,
        string Status,
        string Response);

    /// <summary>The ten TOE criteria, final.</summary>
    public static Criterion[] Criteria() => new[]
    {
        new Criterion(1, "Quantum Mechanics", Status.Derived, "DERIVED",
            "magnitude |ψ|²=ρ (QG216), phase (QG220), complex structure (QG218), measurement (QG74)"),
        new Criterion(2, "Gravity", Status.Derived, "DERIVED",
            "structure (QG197/207), observables (QG181-213), native dynamics (QG222)"),
        new Criterion(3, "Matter", Status.Derived, "DERIVED",
            "deficit dust (QG194/195/196), mass laws (QG203-211)"),
        new Criterion(4, "Standard Model", Status.Derived, "PARTIAL",
            "QG248 SM DYNAMICS COMPLETE: gauge dynamics (QG243/244), Higgs potential + SSB (QG246), Yukawa + mass mechanism (QG247); the ten-component audit is 8 DERIVED / 1 framework-partial (propagator machinery) / 1 boundary (SU(3) color-count)"),
        new Criterion(5, "Cosmology", Status.Partial, "PARTIAL",
            "expansion QG77, structure QG231, dark matter QG206, Λ QG230, Ω_Λ/Ω_m QG234, n_s QG237 all derived or partial; the remaining partial is the acoustic-peak recombination mechanism (QG238: ℓ₁ 0.008%, r₂₁ 0.035%, r₃₁ 0.058% derived; the sound-horizon/recombination mechanism not)"),
        new Criterion(6, "Initial conditions", Status.Derived, "DERIVED",
            "QG227: the uniform critical state ρ_k = 1/K"),
        new Criterion(7, "Dimensionality", Status.Derived, "DERIVED",
            "QG2/3/5/159/160"),
        new Criterion(8, "Information origin", Status.Derived, "DERIVED",
            "QG228: information = KL(ρ‖uniform) > 0"),
        new Criterion(9, "Primitive completeness", Status.Boundary, "BOUNDARY",
            "ψ is the second of exactly two primitives; observational boundary (QG223)"),
        new Criterion(10, "Parameter completeness", Status.Boundary, "BOUNDARY",
            "Bekenstein 1/4 needs π (QG196), H is an epoch scale; Ω_Λ/Ω_m derived (QG234)"),
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

    // ── The four determination questions ──────────────────────────────────────

    /// <summary>(1) Any true missing physics? No OPEN criterion; the one PARTIAL is a derivation-in-progress.</summary>
    public static bool AnyTrueMissingPhysics()
        => StatusCounts()[Status.Open] > 0;

    /// <summary>
    /// (2) Any hosted CORE dynamics? No — QG248 closed the last hosted core (SM dynamics). The only
    /// remaining hosted item is the propagator/quantization machinery (framework-completeness).
    /// </summary>
    public static bool AnyHostedCoreDynamics()
        => false;

    /// <summary>
    /// (3) Any unresolved contradiction? C4 (perihelion tensor-vs-scalar) remains PARTIALLY RESOLVED in
    /// the coverage register (a documentation re-adjudication item); C1-C3, C5-C7 are RESOLVED.
    /// </summary>
    public static bool AnyUnresolvedContradiction()
        => true;   // C4: PARTIALLY RESOLVED — the register has not been re-adjudicated

    /// <summary>(4) Any remaining TOE blocker? No OPEN blocker.</summary>
    public static bool AnyRemainingBlocker()
        => StatusCounts()[Status.Open] > 0;

    /// <summary>The answers to the four questions.</summary>
    public static (string Question, string Answer)[] Determinations() => new[]
    {
        ("Any true missing physics?", "NO — no OPEN criterion; the single PARTIAL (acoustic-peak recombination mechanism QG238) is a derivation-in-progress whose target values are already derived (ℓ₁ 0.008%, r₂₁ 0.035%, r₃₁ 0.058%); the boundaries (ψ, Bekenstein π, H, SU(3) color-count) are documented"),
        ("Any hosted core dynamics?", "NO — QG248 closed the last hosted core (SM dynamics): gauge dynamics (QG243/244), Higgs sector (QG246), and Yukawa sector (QG247) are all DERIVED; only the propagator/quantization MACHINERY remains a framework-completeness partial"),
        ("Any unresolved contradiction?", "ONE — C4 (perihelion tensor-vs-scalar) is PARTIALLY RESOLVED in the coverage register (QG212 clarifies the sectors; the register needs re-adjudication to RESOLVED); C1-C3, C5-C7 are RESOLVED — a documentation item, not a physics blocker"),
        ("Any remaining TOE blocker?", "NO — no OPEN criterion; the path to COMPLETE TOE needs the acoustic mechanism (Cosmology partial), the C4 re-adjudication, and the accepted documented boundaries"),
    };

    // ── Classification ────────────────────────────────────────────────────────

    /// <summary>
    /// PARTIAL TOE (&lt; 70% or any OPEN), NEAR-COMPLETE TOE (70-95% with no OPEN), COMPLETE TOE
    /// (≥ 95% with no OPEN and no PARTIAL).
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

    // ── Top-10 strongest remaining criticisms ────────────────────────────────

    /// <summary>The top-10 strongest remaining criticisms (from the referee catalog + QG248 boundary).</summary>
    public static Criticism[] TopCriticisms() => new[]
    {
        new Criticism(1, "Imported physics",
            "ψ is a NEW PRIMITIVE — a 'complete' theory must derive its field content from Q-events, not posit it.",
            "BOUNDARY", "ψ is the second of exactly two primitives (QG51); its capacity is FORCED (QG56), excitation DERIVED (QG57); an ONTOLOGICAL boundary (QG223) — the strongest remaining criticism"),
        new Criticism(2, "Imported physics",
            "The Bekenstein-Hawking S = A/4 requires the imported 2π quantum factor; the native coefficient is off by 2π.",
            "BOUNDARY", "QG185/QG196 PROVE the exact 1/4 impossible within D96 without importing π — a stated impossibility boundary, not a gap"),
        new Criticism(3, "Falsification",
            "The CMB acoustic-peak recombination mechanism is not derived — the sound-horizon/recombination physics is missing.",
            "PARTIAL", "QG238 derives the peak positions (ℓ₁ 0.008%, r₂₁ 0.035%, r₃₁ 0.058%) from the octave hierarchy; the recombination mechanism is the single remaining Cosmology partial"),
        new Criticism(4, "Imported physics",
            "The propagator/quantization machinery is hosted, not derived — the momentum-space Feynman formalism is the standard host.",
            "PARTIAL", "QG248: the quadratic operator content is DERIVED (QG244 determines i/(p²−m²)); the explicit Feynman quantization machinery is a documented framework-completeness item"),
        new Criticism(5, "Assumption",
            "The SU(3) color-COUNT identification (3 families = 3 colors) retains a postulate trace.",
            "BOUNDARY", "QG161 derives the su(3) STRUCTURE (3²−1 = 8 from the 3 octave families); the color-count identification retains the QG79 pre-D96 postulate trace — documented"),
        new Criticism(6, "Ambiguity",
            "Inflation is not derived — only replaced by Poisson seeds for structure formation.",
            "BOUNDARY", "QG236: all five inflation-motive problems are SOLVED BY AT; the epoch is REPLACED; the CMB spectrum content is the partial item (criticism 3)"),
        new Criticism(7, "Assumption",
            "The golden-ratio hierarchy is presented as a law but is a secondary basin consequence.",
            "BOUNDARY", "QG152: explicitly a SECONDARY basin consequence, not a fundamental law — documented and not used as a primary derivation"),
        new Criticism(8, "Ambiguity",
            "The Hubble constant H is an epoch-scale input, not derived from the primitives.",
            "BOUNDARY", "QG233: expansion and H ~ √ρ̄ ~ 1/R are DERIVED (QG77/230); the CURRENT value is a contingent epoch scale input — documented"),
        new Criticism(9, "Falsification",
            "The theory offers no quantum-gravity phenomenology comparable to LQG/string (no quantization-of-gravity predictions).",
            "PARTIAL", "QG235: the Planck regime is derived (QG14); a full QG phenomenology is not — a framework-completeness item, not a falsification gap"),
        new Criticism(10, "Assumption",
            "The flat background metric η in the ansatz g = ρ^(2/d)η is imported; the conformal class is assumed, not derived.",
            "BOUNDARY", "QG207 determines k = 2/d uniquely within the conformal class (PARTIAL UNIQUE); the flat background and the ψ tensor completion are documented structure choices"),
    };

    /// <summary>The top-10 criticism status counts.</summary>
    public static IReadOnlyDictionary<string, int> CriticismStatusCounts()
        => TopCriticisms().GroupBy(c => c.Status).ToDictionary(g => g.Key, g => g.Count());
}
