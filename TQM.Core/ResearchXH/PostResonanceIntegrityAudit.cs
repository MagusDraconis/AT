namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 271 — Post-Resonance Integrity Audit. QG260-270 introduced the resonance hierarchy:
/// Resonance Layer (260) → Operator Layer (261) → Same Operator Sectors (262) → Single Resonance
/// Dynamics (263) → Single Resonance Invariant (264) → Universal Resonance Invariant Σλ=12·96 (265) →
/// Universal Conservation Law Σλ=2E=N·d (266) → Universal Conservation Principle (267) → Universal
/// Self-Consistency (268) → Single Individuation Principle (269) → Universal Difference Principle (270).
/// This phase re-evaluates every remaining criticism through this new hierarchy: for each critique,
/// determine RESOLVED / REFRAMED / STILL OPEN / FALSE PROBLEM. Focus: QG250, QG252, QG253, QG256,
/// QG257, QG258. Deterministic, structure only, no observables.
///
/// THE RE-EVALUATION (each critique through the resonance hierarchy):
///
/// [1] QG250-F1 PARAMETER LEAKAGE ("the 8 D96 numbers + me + factors fit ~25 targets"):
///     RESOLVED — the resonance reduction refutes the premise. QG251 already showed all eight moments
///     descend from ONE object (the D96 spectrum). QG261-266 make it structural: Σm/Σ√m/Σm² are MOMENT
///     reads of the CROWDING projection, occMom is the COMPRESSION read, span is the BEAT read, λ₂ is
///     the LOCKING read — NOT eight free knobs, but the reads of ONE invariant (Σλ = 2E = N·d, QG266,
///     a graph identity). Effective free count = the network + me (QG251). FALSE PREMISE.
///
/// [2] QG250-F2 SELF-CONFIRMATION ("tests validate the formulas they encode"):
///     STILL OPEN — the resonance chain is STRUCTURAL: it shows the operators are derived from the
///     spectrum, not from targets. But it does NOT add independent validation: the tests still assert
///     the formulas the phases chose, and QG258 (blind) confirms the selection rules have no temporal
///     predictive power. The structural reduction strengthens the claim that the quantities are not
///     arbitrary, but the assignment and the validation architecture remain self-authored.
///
/// [3] QG252 INDEPENDENT EVIDENCE (MEDIUM: 42% methodological, 6.7% temporal):
///     REFRAMED — the evidence fractions are unchanged, but the resonance chain adds a NEW class of
///     structural evidence: the operator layer itself (CROWDING/COMPRESSION/BEAT/LOCKING) is DERIVED
///     from the spectrum (QG261), and the invariant Σλ = 12·96 is a graph identity (QG266) — neither
///     was selected against a target. This adds structural derivation units to the "machinery never
///     sees the target" class. The temporal fractions (6.7%) remain the binding constraint.
///
/// [4] QG253 FORMULA UNIQUENESS (6/7 MULTIPLE MATCHES — simpler non-native alternatives exist):
///     REFRAMED — the "simpler alternatives" found by the bare search mostly isolate a single octave
///     band (occ₀, occ₃, √occMom alone). The resonance hierarchy explains WHY the published formulas
///     are correct despite higher complexity: the alternatives project the WRONG operator. The octave
///     structure IS the COMPRESSION projection (QG261-263), so a formula isolating one band breaks the
///     resonance invariance (QG254 octave preservation is now derived, not selected). r₃₁ = span/√3
///     remains UNIQUE. The residual: WHICH operator maps to WHICH observable is still target-informed
///     (QG262 caveat).
///
/// [5] QG256 SELECTION-PRINCIPLE AUDIT (HIGH risk: octave preservation PREFERRED, moment-closure MDL
///     ARBITRARY, 5/4 inconsistency):
///     REFRAMED — octave preservation is UPGRADED from "preferred" to "derived": the octave bands are
///     the COMPRESSION projection of the resonance structure (QG261-263), so isolating a single band is
///     a partial, non-invariant read — a structural error, not merely a selection choice. The
///     moment-closure MDL arbitrariness and the 5/4 exception (QG238 ℓ₁ = Σm·ln(span)·5/4) are NOT
///     fixed by the resonance chain: they remain STILL OPEN as the residual meta-level inconsistency.
///
/// [6] QG257 PRINCIPLE COMPETITION (NO UNIVERSAL PRINCIPLE among the seven):
///     REFRAMED — the seven ad-hoc principles are REPLACED by ONE structural requirement: "project the
///     correct resonance operator" (density vs frequency family, QG263). This is universal (applies to
///     every observable) and exception-free (it is derived from the spectrum). But it does NOT uniquely
///     assign operators to observables without target information — the assignment step (QG262 caveat)
///     remains STILL OPEN. The competition is resolved structurally; the assignment is not.
///
/// [7] QG258 BLIND TOURNAMENT (WEAK: 0/7 — the target-free chain selects the same formula everywhere):
///     REFRAMED — the WEAK result is now EXPLAINED, not merely observed. The operators are UNIVERSAL
///     (QG262 SAME OPERATOR SECTORS): the same operator outputs appear in every sector, so a target-free
///     rule chain CANNOT discriminate between observables — it must select the same formula. This is a
///     structural consequence of operator universality, not a deficiency of the selection rules. The
///     honest conclusion (no blind predictive power without the assignment step) is STILL OPEN.
///
/// [8] QG250 OTHER MAJORS (N=96 selection, η conformal, me anchor, y_f definitional, uniform state,
///     octave grouping, Bekenstein π, ψ primitive, per-particle fits, 3+1, coupling dictionary):
///     REFRAMED — the octave-grouping circularity (attack 9) is resolved by the resonance structure:
///     the [4,4,87] occupancies are the COMPRESSION read of the spectrum, derived not chosen (QG261).
///     The N=96 selection (attack 3) is reframed: the network is the actualization attractor (QG116/
///     159/160), and the invariant Σλ = 12·96 = N·d (QG266) makes the degree-12 regularity structural.
///     The per-particle mass fits (attack 11) are reframed: the mass formulas consume the SAME operator
///     basis in every sector (QG262), so they are not arbitrary per-particle constructions. REMAIN
///     STILL OPEN: the me = 0.511 anchor (attack 5, the only genuinely free input), the conformal η
///     import (attack 4), the Bekenstein 1/4 π gap (attack 10), the ψ primitive (attack 15), the
///     y_f = m_f/v definitional concern (attack 7), and the 3+1 selection (attack 13).
///
/// [9] QG250 MINORS + EDITORIAL (Λ scaling, H epoch, Poisson CMB, no QG quantization, ρ→metric
///     non-uniqueness, no deadline, RG import, 1.08 bits, no peer review):
///     STILL OPEN — the resonance chain does not touch these. They are empirical/architectural issues
///     (imported RG, Λ value, ψ-sector geometry, publication record) that the structural reduction
///     neither resolves nor reframes.
///
/// THE TRUE REMAINING FRONTIER AFTER QG270 (computed):
///   (a) the operator-to-observable ASSIGNMENT (which projection maps to which sector) — the residual
///       target-information step (QG262 caveat, QG258 WEAK, QG257 assignment gap);
///   (b) the 5/4 acoustic-peak factor — the residual meta-level inconsistency with Noether
///       consistency (QG256 STILL OPEN);
///   (c) the me = 0.511 anchor — the only genuinely free empirical input (QG251);
///   (d) independent temporal evidence — the binding constraint (QG252, 6.7% temporal);
///   (e) the structural imports (conformal η, Bekenstein π, ψ primitive, RG, 3+1).
///   The resonance reduction RESOLVED the parameter-leakage premise, DERIVED the selection principles
///   (octave/operator structure), and EXPLAINED the blind-tournament weakness — but the ASSIGNMENT
///   step (structure → physics labels) remains the true frontier.
///
/// CLASSIFICATION: the resonance hierarchy RESOLVED the structural critiques (parameter leakage,
///   octave selection, uniqueness reframing) and REFRAMED the principle critiques (selection rules,
///   competition, blind power) — but the assignment step, the 5/4 exception, the me anchor, and
///   independent temporal evidence remain the true frontier.
/// </summary>
public static class PostResonanceIntegrityAudit
{
    public enum Status { Resolved, Reframed, StillOpen, FalseProblem }

    /// <summary>A critique re-evaluated through the resonance hierarchy.</summary>
    public sealed record Critique(
        string Name,
        string Source,
        Status Status,
        string ResonanceInterpretation);

    /// <summary>The critiques re-evaluated through QG260-270.</summary>
    public static Critique[] Critiques() => new[]
    {
        new Critique("QG250-F1 parameter leakage", "ExternalRefereeAttack",
            Status.Resolved,
            "the 8 moments are reads of ONE invariant (Σλ = 2E = N·d, QG266) via CROWDING/COMPRESSION/BEAT/LOCKING (QG261) — not 8 free knobs; FALSE PREMISE (QG251 already showed 2 effective params)"),
        new Critique("QG250-F2 self-confirmation", "ExternalRefereeAttack",
            Status.StillOpen,
            "structural reduction does not add independent validation; tests still assert the chosen formulas; QG258 confirms no temporal blind power"),
        new Critique("QG252 independent evidence", "IndependentPredictionAudit",
            Status.Reframed,
            "fractions unchanged, but the operator layer (QG261) and invariant Σλ (QG266) add STRUCTURAL derivation units that never see a target; temporal 6.7% remains the binding constraint"),
        new Critique("QG253 formula uniqueness (6/7 multiple)", "FormulaUniquenessAudit",
            Status.Reframed,
            "the 'simpler' alternatives isolate a single octave band — they project the WRONG operator; the octave structure IS the COMPRESSION projection (QG261-263), so octave preservation is now DERIVED; r₃₁ remains UNIQUE; the operator→observable assignment is the residual"),
        new Critique("QG256 selection-principle audit", "SelectionPrincipleAudit",
            Status.Reframed,
            "octave preservation UPGRADED to derived (the octave bands are the COMPRESSION read, QG261); the moment-closure MDL arbitrariness and the 5/4 exception (QG238) remain STILL OPEN as the residual meta-inconsistency"),
        new Critique("QG257 principle competition", "PrincipleCompetitionAudit",
            Status.Reframed,
            "the seven ad-hoc principles are replaced by ONE structural requirement: project the correct resonance operator family (density/frequency, QG263) — universal and exception-free; but the operator→observable ASSIGNMENT remains target-informed (QG262)"),
        new Critique("QG258 blind tournament (WEAK 0/7)", "BlindFormulaTournament",
            Status.Reframed,
            "the WEAK result is EXPLAINED: the operators are UNIVERSAL (QG262), so a target-free chain CANNOT discriminate observables — it must pick the same formula; a structural consequence, not a selection defect; no blind predictive power without the assignment step (STILL OPEN)"),
        new Critique("QG250 octave-grouping circularity (attack 9)", "ExternalRefereeAttack",
            Status.Resolved,
            "the [4,4,87] occupancies are the COMPRESSION read of the spectrum (QG261), derived not chosen — the grouping is a projection of the resonance structure"),
        new Critique("QG250 N=96 selection (attack 3)", "ExternalRefereeAttack",
            Status.Reframed,
            "the network is the actualization attractor (QG116/159/160); the invariant Σλ = 12·96 = N·d (QG266) makes the degree-12 regularity structural — the selection is the attractor, not a tuned choice"),
        new Critique("QG250 per-particle mass fits (attack 11)", "ExternalRefereeAttack",
            Status.Reframed,
            "the mass formulas consume the SAME operator basis in every sector (QG262 SAME OPERATOR SECTORS) — they are projections of one operator layer, not arbitrary per-particle constructions"),
        new Critique("QG250 me = 0.511 anchor (attack 5)", "ExternalRefereeAttack",
            Status.StillOpen,
            "the only genuinely free empirical input (QG251); the resonance chain does not derive it — it is the one external value in the theory"),
        new Critique("QG250 conformal η import (attack 4)", "ExternalRefereeAttack",
            Status.StillOpen,
            "the conformal class remains assumed; the resonance structure (spectrum → ρ) does not derive flatness"),
        new Critique("QG250 Bekenstein π gap (attack 10)", "ExternalRefereeAttack",
            Status.StillOpen,
            "S = A/4 still requires the imported 2π factor (QG185/196); the resonance invariant does not supply π"),
        new Critique("QG250 ψ primitive (attack 15)", "ExternalRefereeAttack",
            Status.StillOpen,
            "the ψ tensor field remains a hand-placed second primitive; the resonance reduction is ρ-sector only"),
        new Critique("QG250 y_f = m_f/v definitional (attack 7)", "ExternalRefereeAttack",
            Status.StillOpen,
            "the Yukawa coupling remains the mass/VEV ratio; the resonance structure explains both factors from the same spectrum but the definitional identity is unchanged"),
        new Critique("QG250 3+1 selection (attack 13)", "ExternalRefereeAttack",
            Status.StillOpen,
            "the dimension derivation remains a constraint-selection; the resonance chain does not touch d"),
        new Critique("QG250 minors + editorial (Λ value, H, RG, ψ-metric, publication)", "ExternalRefereeAttack",
            Status.StillOpen,
            "empirical/architectural issues (imported RG, Λ magnitude, ρ→metric non-uniqueness, no peer review) that the structural reduction neither resolves nor reframes"),
    };

    /// <summary>Count of critiques by re-evaluation status.</summary>
    public static IReadOnlyDictionary<Status, int> StatusCounts()
    {
        var d = new Dictionary<Status, int>();
        foreach (Status s in Enum.GetValues<Status>()) d[s] = 0;
        foreach (var c in Critiques()) d[c.Status]++;
        return d;
    }

    /// <summary>The resonance hierarchy's effect on the focus critiques.</summary>
    public static int ResolvedCount() => StatusCounts()[Status.Resolved];

    /// <summary>Number of critiques reframed (interpretation changed).</summary>
    public static int ReframedCount() => StatusCounts()[Status.Reframed];

    /// <summary>Number of critiques still open after QG270.</summary>
    public static int StillOpenCount() => StatusCounts()[Status.StillOpen];

    /// <summary>Number of false problems (none found).</summary>
    public static int FalseProblemCount() => StatusCounts()[Status.FalseProblem];

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var c = StatusCounts();
        return $"Post-resonance integrity: RESOLVED {c[Status.Resolved]} / REFRAMED {c[Status.Reframed]} / "
             + $"STILL OPEN {c[Status.StillOpen]} / FALSE PROBLEM {c[Status.FalseProblem]} — the resonance "
             + "reduction (QG260-270) RESOLVED the parameter-leakage premise and octave-grouping "
             + "circularity, DERIVED the selection principles (octave/operator structure), and EXPLAINED "
             + "the blind-tournament weakness — but the operator→observable ASSIGNMENT, the 5/4 "
             + "exception, the me anchor, and independent temporal evidence remain the TRUE frontier.";
    }
}
