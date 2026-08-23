namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 250 — External Referee Audit. A HOSTILE referee attacks QG0-QG249. The goal is the
/// STRONGEST remaining reasons TQM could still fail — hidden assumptions, unjustified selections,
/// parameter leakage, effective-vs-fundamental derivations, boundary classifications, publication
/// weaknesses. This phase does NOT defend TQM — it attacks only. Each attack is classified
/// FATAL / MAJOR / MINOR / EDITORIAL. Output: the top-25 strongest remaining attacks.
///
/// METHOD: the referee's own perspective. For each attack the referee names the target phases, the
/// failure mode, and why it could sink the theory. No resolution notes are given — the attack stands
/// alone. Severity is the referee's judgment of how much damage the attack does to the claim
/// "TQM is a Theory of Everything" if the referee is right.
///
/// THE TOP-25 ATTACKS, grouped by focus:
///  HIDDEN ASSUMPTIONS — D96 selection, conformal η, uniform initial state, octave grouping, ρ→metric.
///  UNJUSTIFIED SELECTIONS — 5/4 and √3 factors, the anchor me, the [4,4,87] split, dimension 3+1.
///  PARAMETER LEAKAGE — the D96 moment set vs the number of derived targets; effective degrees of freedom.
///  EFFECTIVE VS FUNDAMENTAL — octave laws as effective fits; Yukawa y_f=m_f/v as definitional.
///  BOUNDARY CLASSIFICATIONS — the "boundary" label as a shield for every hard failure (π, H, ψ).
///  PUBLICATION WEAKNESSES — self-authored audits, no peer review, tests that validate their own claims.
///
/// SEVERITY SUMMARY (referee's judgment):
///  FATAL   2 — (1) free-parameter accounting: the D96 moment set + multiplicative factors can fit the
///             target set; (2) no independent confirmation: every "derivation" is validated by a test
///             the phase itself writes. If either holds, the "derivations" are numerical coincidences.
///  MAJOR  14 — self-selection of D96, conformal η import, me anchor, n_s/acoustic retro-selection,
///             y_f definitional, uniform-state assumption, octave grouping circularity, Bekenstein π
///             gap, per-particle mass fits, self-authored audit structure, 3+1 selection, coupling
///             dictionary, ψ primitive, Higgs mechanism-by-construction.
///  MINOR   8 — Λ scaling without coefficient, H epoch input, Poisson-vs-CMB tilt, no QG phenomenology,
///             ρ→metric non-uniqueness, indefinite falsification deadline, no RG program,
///             tests-as-consistency-only.
///  EDITORIAL 1 — publication venue (no peer review, self-authored evidence base).
///
/// The referee's verdict: with two FATAL-level attacks (parameter accounting + self-confirming tests),
/// TQM is NOT protected by its internal audits — the internal audit program is itself part of the
/// attack surface. A hostile referee would not accept the coverage register, the closure audits, or the
/// boundary labels as evidence; each is a self-assessment artifact.
/// </summary>
public static class ExternalRefereeAttack
{
    public enum Severity { Fatal, Major, Minor, Editorial }

    /// <summary>An attack. Focus = the failure mode; TargetPhases = what it hits.</summary>
    public sealed record Attack(
        int Rank,
        Severity Severity,
        string Focus,
        string AttackText,
        string TargetPhases);

    /// <summary>The top-25 strongest attacks (attack only, no defense).</summary>
    public static Attack[] Top25() => new[]
    {
        new Attack(1, Severity.Fatal, "Parameter leakage",
            "The D96 moment set is not fixed before the derivations: Σm=95, #d=42, #g=44, occMom=1900.25, λ₂=0.386, span=6.40, Σ√m=64.08, occ=[4,4,87] — at least eight numbers — plus the electron anchor me and multiplicative factors (5/4, √3, 1/2, 2). Reproducing ~25 fermion/cosmological quantities with this many available knobs is not a derivation; it is an over-parameterized fit. The referee demands the effective free-parameter count exceed the number of derived targets before any 'derivation' is credited.",
            "QG140-249 (all closed-form laws)"),
        new Attack(2, Severity.Fatal, "Effective vs fundamental / self-confirmation",
            "Every derivation is validated by a test that the same phase writes and asserts. Passing means only that the code matches the formula the phase chose. There is no independent, pre-committed falsification of the derivations themselves — only of three collider/neutrino predictions (P1-P3). If the formulas are effective numerology, the test suite cannot detect it, because the suite encodes the formulas.",
            "QG0-249 (the whole validation architecture)"),
        new Attack(3, Severity.Major, "Unjustified selection",
            "N=96 is SELECTED from candidates (64, 96, 128, 192) by criteria — 3 families, Z2 pairing, span — that are themselves the physics the theory then 'derives'. The selection is tuned so the chosen network yields the desired observables. The referee asks: which observables were known when N=96 was picked, and were the selection criteria written before or after seeing the target values?",
            "QG159/160 (D96 selection)"),
        new Attack(4, Severity.Major, "Hidden assumption",
            "The metric ansatz g = ρ^(2/d)·η imports the flat Minkowski background η. The conformal class is ASSUMED (Malament gives topology, not geometry); the conformal factor ρ^(2/d) is one member of an infinite conformal class. 'PARTIAL UNIQUE within the conformal class' concedes that the class itself is assumed — the theory does not derive flatness, it imports it.",
            "QG207 (metric ansatz), QG2/3 (dimension)"),
        new Attack(5, Severity.Major, "Hidden assumption",
            "The electron mass me = 0.511 MeV is an INPUT anchor for the entire fermion hierarchy (QG140/173/209). The 'derived' masses are all me times spectral ratios. A parameter-complete TOE cannot have one fermion mass as a free input; the referee asks why me is special and what in D96 fixes its value.",
            "QG140/173/209/203 (mass laws)"),
        new Attack(6, Severity.Major, "Unjustified selection / retro-selection",
            "The spectral index n_s = 1 − ln(span)/(Σm−#d) and the acoustic peaks ℓ₁ = Σm·ln(span)·5/4, r₃₁ = span/√3 use multiplicative factors (5/4, √3) and specific D96 ratios that were selected to match sharp observed values. QG239 itself classifies these as RETRO-SELECTION RISK: 5-6 candidate formulas were tried, the target influenced selection, and nothing was preregistered. A hostile referee concludes the 'derivations' are post-hoc curve-fits of the CMB.",
            "QG237/238 (CMB spectrum, acoustic peaks)"),
        new Attack(7, Severity.Major, "Effective vs fundamental / tautology",
            "The Yukawa couplings are DEFINED as y_f = m_f/v (QG247): the mass is divided by the VEV to 'derive' the coupling. This is a restatement, not a derivation — the coupling inherits every assumption of the mass law and the VEV construction. 'SM DYNAMICS COMPLETE' (QG248) rests on a definitional identity masquerading as a derivation.",
            "QG247 (Yukawa origin), QG248 (SM closure)"),
        new Attack(8, Severity.Major, "Hidden assumption",
            "The initial state is the UNIFORM critical state ρ_k = 1/K, justified as 'least committal' (QG227). Maximum-entropy reasoning is a modeling choice, not a derivation: the uniform state is assumed because it is convenient, and the theory then derives cosmology FROM that assumption. A hostile referee calls this the initial-condition postulate in disguise.",
            "QG227 (initial conditions), QG228 (information)"),
        new Attack(9, Severity.Major, "Unjustified selection / circularity",
            "The octave occupancies [4,4,87] split 95 modes into three groups, and the three-family structure, the mass hierarchy, the gauge couplings, and Ω_Λ all read numbers off that split. The 3-family structure was known before the octave grouping; the grouping is chosen to reproduce it. The referee demands a first-principles rule for WHY the spectrum groups into 4, 4, 87.",
            "QG155/210 (octave families), QG161/162 (gauge)"),
        new Attack(10, Severity.Major, "Boundary classification / real gap",
            "Bekenstein S = A/4 is classified a BOUNDARY because the exact 1/4 'requires π'. A TOE that cannot derive a fundamental black-hole coefficient — and instead declares it impossible — has a real, named gap in its gravity sector. Labeling the gap a 'boundary' does not make it derived.",
            "QG185/196 (Bekenstein 1/4)"),
        new Attack(11, Severity.Major, "Effective vs fundamental",
            "The quark and lepton mass laws reproduce values within 0.2-3% using different combinations of the same few spectral moments (Σm, occMom, λ₂, Σ√m, #d, #g) for each particle. Six quarks, three leptons, three neutrinos: each with its own chosen combination. This is not one law; it is a per-particle fit. QG209's 'EXACT LAW' for leptons and QG173's quark law are separate constructions, not a unified derivation.",
            "QG173/209/203 (mass laws)"),
        new Attack(12, Severity.Major, "Boundary classification / audit methodology",
            "The theory resolves its own gaps by writing more audits that reclassify open items as 'BOUNDARY' or 'RESOLVED'. The referee is asked to accept the resolution of objections by the object of the objections. The RefereeObjectionAudit, the FormulaSelectionAudit, and the FinalToeAudit are all self-authored; none is external arbitration.",
            "QG215/221/223/241/249 (audit program)"),
        new Attack(13, Severity.Major, "Unjustified selection",
            "Dimension 3+1 is 'derived' by constructing five constraints that jointly select 3+1 (QG2/3). The referee asks whether the constraints were chosen because they yield 3+1 — in which case the derivation is a selection criterion dressed as a theorem. The same applies to the D96 degree 12 giving 1+3+8 generators.",
            "QG2/3 (dimension), QG161 (gauge count)"),
        new Attack(14, Severity.Major, "Parameter leakage",
            "The gauge coupling 1/α_em = 137 is written as Σm + #d = 95 + 42. The referee notes the 'coincidence' that the fine-structure denominator equals a mode count is asserted, not explained: why should an electromagnetic coupling denominator be a combinatoric count of network modes? The mapping from counts to couplings is a dictionary the theory writes, then treats as a derivation.",
            "QG162 (coupling origin)"),
        new Attack(15, Severity.Major, "Hidden assumption / gravity",
            "The ψ tensor field is the second primitive. Its EXISTENCE is observationally demanded, its capacity 'forced', its excitation 'derived' — but it is not derived from Q-events. Any theory claiming completeness from two primitives that needs a hand-placed field sector has an undisclosed assumption at its core.",
            "QG23/24/47/51/56/57 (ψ sector)"),
        new Attack(16, Severity.Major, "Effective vs fundamental / mechanism",
            "The Higgs mechanism is now declared DERIVED, but the theory's mass generation is m_f = y_f·v where both factors come from the SAME octave data. The referee sees no independent mechanism: the VEV and the coupling are two readings of the same spectrum, so their product reproducing a mass is guaranteed by construction, not by physics.",
            "QG168/169/246/247 (mass mechanism)"),
        new Attack(17, Severity.Minor, "Effective vs fundamental / cosmology",
            "Λ ∝ 1/R² derives a SCALING, not a value. The cosmological constant's magnitude today is never derived — it is an epoch-dependent boundary. A TOE that derives the scaling but not the number has not derived the cosmological constant; it has derived a proportionality.",
            "QG230 (Λ origin)"),
        new Attack(18, Severity.Minor, "Hidden assumption / cosmology",
            "The Hubble constant H is an epoch-scale INPUT (QG233). The expansion is derived from scale-free ρ, but the current expansion rate — one of the most precisely measured numbers in cosmology — is not a prediction of the theory.",
            "QG77/233 (expansion, H)"),
        new Attack(19, Severity.Minor, "Contradiction",
            "The CMB seeds are Poisson noise δ_i = 1/√⟨N⟩ (QG231), which is white/scale-free; the observed CMB is near-scale-invariant with a tilt, and QG237 'derives' n_s from the octave span. A hostile referee sees the white-noise seed and the tilted spectrum as inconsistent pictures of the same initial field, reconciled only by a separate octave formula.",
            "QG231/237/238 (CMB)"),
        new Attack(20, Severity.Minor, "Effective vs fundamental / QG",
            "There is no quantization of gravity: no graviton, no quantum-gravitational corrections. The theory is a classical-geometry + quantum-matter hybrid. The 'Quantum Gravity' claim rests on deriving QM and GR from the same primitive, not on quantizing gravity — a distinction a hostile referee will exploit.",
            "QG14/216-224 (QG claims)"),
        new Attack(21, Severity.Minor, "Boundary classification / metric",
            "The ρ → metric map is non-unique: QG207 admits the ψ tensor sector provides alternative counting-preserving metrics with the same √(−g)=ρ but different observables. A theory whose spacetime metric is only 'PARTIAL UNIQUE' has an unresolved geometric ambiguity in its core gravity sector.",
            "QG207 (metric ansatz)"),
        new Attack(22, Severity.Minor, "Falsification",
            "The three pre-registered predictions have no deadline: P1 (106 GeV) and P2 (0νββ) can remain PENDING indefinitely; the theory never falsifies itself. The prediction registry is append-only, but 'append-only' is not 'testable'. A referee demands a time horizon after which the theory is declared falsified.",
            "QG190-193 (prediction registry)"),
        new Attack(23, Severity.Minor, "Effective vs fundamental / RG",
            "The mass/coupling laws are stated at a 'natural scale', and running is imported from the standard MS̄ scheme (QG204). The theory does not derive the renormalization group; it borrows it. All 'derived' running quantities inherit the imported RG framework.",
            "QG163/164/204 (running couplings)"),
        new Attack(24, Severity.Minor, "Hidden assumption / information",
            "The 'information content of the universe' is 1.08 bits (the D96 octave record, QG228). A hostile referee notes that 1.08 bits cannot account for the observed complexity; the identification of information with a KL divergence of a mode count is a choice, not a measure of physical information.",
            "QG228 (information origin)"),
        new Attack(25, Severity.Editorial, "Publication weakness",
            "The theory's evidence base is internal: self-published reports, an internal test suite, and self-authored audits. There is no peer-reviewed derivation of the core claims, no external replication, and no neutral party validating the coverage register. In the absence of external arbitration, the referee cannot distinguish the program from a large, internally-consistent fitting exercise.",
            "QG0-249 (publication/validation record)"),
    };

    /// <summary>Severity counts.</summary>
    public static IReadOnlyDictionary<Severity, int> SeverityCounts()
        => Top25().GroupBy(a => a.Severity).ToDictionary(g => g.Key, g => g.Count());

    /// <summary>Attacks by focus area (first token of the Focus string).</summary>
    public static IReadOnlyDictionary<string, int> FocusCounts()
        => Top25().GroupBy(a => a.Focus).ToDictionary(g => g.Key, g => g.Count());

    /// <summary>The referee's verdict (attack-only framing).</summary>
    public static string Verdict()
    {
        var sc = SeverityCounts();
        return $"Referee verdict: {sc[Severity.Fatal]} FATAL / {sc[Severity.Major]} MAJOR / "
             + $"{sc[Severity.Minor]} MINOR / {sc[Severity.Editorial]} EDITORIAL — the internal audit "
             + "program is part of the attack surface; no boundary label is accepted as evidence.";
    }
}
