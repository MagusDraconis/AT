# ResearchY-NP_099 — Classicality Emergence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_099 (permanent)
**Title:** Classicality Emergence Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_099.md`
**Depends on:** ResearchY-NP_093 (selection = Born rule), NP_094 (inertia), NP_095 (friction),
NP_096 (heat / entropy), NP_097 (temperature), NP_098 (localization), NP_071 (matter = deficit),
NP_006/007 (interference cross-term), AT-QG QG216 (Born rule), QG228 (I_occ), ResearchY-D_041
(tick / phase advance)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_099_Tests.cs`

---

## Purpose

NP_098 made localization = the wave-packet peak; NP_096 made heat = decohered motion and entropy =
mode multiplicity. NP_099 asks the synthesis question: **how does classical reality emerge from
actualization — how does a localized resonance become an apparently classical object?** Program:
(1) trace single / repeated / many-body realizations; (2) determine when quantum localization
becomes classical persistence; (3) test whether classicality is decoherence / repeated
actualization / stable resonance hierarchy / entropy dominance; (4) determine why rocks, planets,
and macroscopic bodies appear classical; (5) identify the AT analogue of the classical limit.
**Success criterion:** explain how a localized resonance becomes apparently classical. No new
primitives; canonical AT unchanged.

---

## 1. Trace single, repeated, many-body realizations

| Realization | What happens | Quantum or classical? |
|---|---|---|
| **single** (one tick) | one Born selection realizes ONE node with probability \|ψ\|² = ρ (NP_093) | quantum — a single stochastic outcome |
| **repeated** (many ticks) | the Born selections accumulate into the \|ψ\|² envelope (NP_098) | **still quantum** — the interference fringes survive (double-slit buildup) |
| **many-body** (scattering against the environment) | friction (NP_095) randomizes the phase; the interference term averages out | **classical** — no fringes; the envelope follows the worldline |

**The classical transition is the third stage:** when the environment's scattering (decoherence)
destroys the phase coherence, the interference vanishes and only the classical probability
(diagonal) survives.

---

## 2. When does quantum localization become classical persistence?

The interference cross-term is the quantum signature (NP_006/007):

```
I = ρ_A + ρ_B + 2√(ρ_A ρ_B)·cos(θ_A − θ_B)
            └── the OFF-DIAGONAL (interference) term ──┘
```

| Regime | The interference term | Result |
|---|---|---|
| **coherent** (Δθ fixed) | 2√(ρ_Aρ_B)·cos Δθ ≠ 0 | FRINGES (quantum) |
| **decohered** (Δθ random over [0,2π]) | ⟨cos Δθ⟩ → 0 | NO fringes — I → ρ_A + ρ_B (classical) |

**Quantum localization becomes classical persistence when the decoherence is complete** — i.e.,
when the scattering rate γ (friction, NP_095) satisfies γ·t ≫ 1. At that point the interference
term averages to zero, the wave packet stops interfering, and its envelope follows the classical
worldline (v_g, NP_094/098) deterministically.

---

## 3. A / B / C / D — what is classicality?

| Interpretation | Verdict |
|---|---|
| **A) decoherence** | **YES — the mechanism.** The loss of phase coherence (scattering, NP_095) kills the interference term. |
| **B) repeated actualization** | **PARTIAL — necessary, not sufficient.** Repeated Born selection (NP_093) realizes the trajectory, but alone it still shows fringes (the double-slit buildup). |
| **C) stable resonance hierarchy** | **PARTIAL — the substrate.** Macroscopic bodies are composites of stable resonances (octave bands, NP_072); stability (inertia, NP_094) gives persistence. |
| **D) entropy dominance** | **PARTIAL — the signature.** High mode multiplicity (NP_096) makes decoherence fast and complete. |

**Determination: A (decoherence) is the mechanism, realized through B (repeated actualization) on
C (stable resonance hierarchy), with D (entropy dominance) as the emergent signature.** Classicality
is not a separate ingredient — it is decohered localization.

---

## 4. Why do rocks, planets, macroscopic bodies appear classical?

| Property | Macroscopic body |
|---|---|
| number of modes | astronomically large (a rock ≈ 10²³ deficit excitations) |
| entropy (mode multiplicity, NP_096) | enormous — the interference terms span ~10²³ phases |
| decoherence rate γ (NP_095) | effectively infinite — the environment scatters every mode constantly |
| interference term | ⟨cos Δθ⟩ → 0 essentially instantaneously |

**A rock appears classical because its decoherence is effectively instantaneous.** The astronomically
many modes, each scattering against the rest, randomize the phases so fast that the interference
terms vanish before they can be observed — leaving a single, non-interfering, deterministic worldline.

---

## 5. The AT analogue of the classical limit

| Quantum mechanics | AT |
|---|---|
| the classical limit ℏ → 0 | **the decoherence-dominant limit γ·t ≫ 1** |
| interference terms vanish as ℏ → 0 | the off-diagonal terms average out as γ·t → ∞ |
| a point particle follows a worldline | the wave-packet envelope follows the classical worldline (v_g) |

The classical limit is **the large-N / high-entropy limit** — where the mode multiplicity is so
large that the random-phase averaging (decoherence) is complete and the interference term is
identically zero. Classical reality is the *decohered* face of the resonance ontology.

---

## Theorem

> **Theorem (NP_099).** Classicality in AT is DECOHERED LOCALIZATION (A) — the emergence of a
> non-interfering, persistent, trajectory-following object from a wave packet whose phase coherence
> has been destroyed by scattering. A localized resonance (a wave packet, NP_098) is quantum while
> its interference term survives: I = ρ_A + ρ_B + 2√(ρ_Aρ_B)·cos(θ_A−θ_B) shows fringes. Friction
> (NP_095) scatters it against the environment's deficit excitations, randomizing the phases; when
> the decoherence rate satisfies γ·t ≫ 1, ⟨cos Δθ⟩ → 0 and the fringes vanish — I → ρ_A + ρ_B, the
> classical probabilities. The transition quantum → classical is therefore decoherence (A), realized
> through repeated actualization (B, the Born selections that trace the trajectory), on a stable
> resonance hierarchy (C, the composite modes of matter), with entropy dominance (D, the high mode
> multiplicity that makes decoherence instantaneous). Macroscopic bodies are classical because their
> ~10²³ modes decohere essentially instantly. The classical limit (ℏ → 0) is the decoherence-dominant
> limit (γ·t ≫ 1), equivalently the large-N / high-entropy limit. Proof: (1) Trace single/repeated/
> many-body realizations (Section 1). (2) The interference term (Section 2, verified — coherent
> fringes amplitude 2√(ρ_Aρ_B) = 0.866 for ρ=(0.25,0.75); decohered ⟨cos Δθ⟩ → 0). (3) Test A–D
> (Section 3, verified — A the mechanism). (4) Macroscopic bodies (Section 4, verified). (5) The
> classical limit (Section 5). **Success criterion: a localized resonance becomes an apparently
> classical object when decoherence (γ·t ≫ 1) destroys its interference term, leaving a persistent
> non-interfering worldline.** Classification: classicality EMERGENT (decoherence + repeated
> actualization on the stable resonance hierarchy); the interference term DERIVED (Born, QG216); the
> decoherence mechanism DERIVED (friction, NP_095); "classicality as a separate primitive" REFUTED;
> "classicality as a fundamental (non-emergent) regime" REFUTED. No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Trace realizations. (2) Interference → decoherence. (3) Test A–D.
> (4) Macroscopic bodies. (5) Classical limit. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "classicality is a separate primitive" | classicality is decohered localization — no new ingredient |
| "repeated actualization alone gives classicality" | the double-slit buildup still shows fringes; only decoherence removes them |
| "a single mode is classical" | a single mode is delocalized and interfering (quantum, NP_098) |
| "entropy dominance is the mechanism" | entropy dominance is the signature; decoherence is the mechanism |
| "macroscopic bodies are fundamentally classical" | they are composites of quantum resonances, decohered by their own many modes |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| classicality = decoherence | a classical (non-interfering) object with a surviving interference term (no decoherence) |
| the transition is at γ·t ≫ 1 | a decohered object that still shows fringes, or a coherent object with no fringes |
| macroscopic bodies decohere instantly | a macroscopic body that exhibits quantum interference |
| the classical limit is decoherence-dominant | a classical limit attained without phase randomization |

---

## 8. Classification

| Component | Status |
|---|---|
| classicality (the non-interfering worldline) | **EMERGENT** (decoherence + repeated actualization) |
| the interference term (Born, QG216) | **DERIVED** |
| the decoherence mechanism (friction, NP_095) | **DERIVED** |
| the stable resonance hierarchy (NP_072) | **DERIVED** |
| classicality as a separate primitive | **REFUTED** |
| classicality as a fundamental (non-emergent) regime | **REFUTED** |

**Conclusion.** Classical reality emerges from actualization as **decohered localization**: a
localized resonance (a wave packet) is quantum while its interference term survives, but scattering
against the environment (friction, NP_095) randomizes its phase until — at γ·t ≫ 1 — the interference
term averages to zero and only the classical probabilities remain. The object then follows a single,
non-interfering worldline. Macroscopic bodies are classical because their enormous mode multiplicity
makes decoherence instantaneous; the classical limit is the decoherence-dominant (large-N /
high-entropy) limit. Classicality is EMERGENT, built on the DERIVED resonance ontology — no new
primitive. No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_099_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_099_SingleRealization` | one tick = one Born-selected node (quantum) | ✅ |
| `Y_NP_099_RepeatedRealizations` | buildup still shows fringes (quantum) | ✅ |
| `Y_NP_099_ManyBodyRealizations` | decoherence removes fringes (classical) | ✅ |
| `Y_NP_099_InterferenceTerm` | coherent fringes; decohered ⟨cos Δθ⟩ → 0 | ✅ |
| `Y_NP_099_ABCD` | A mechanism; B/C/D partial | ✅ |
| `Y_NP_099_Transition` | classical at γ·t ≫ 1 | ✅ |
| `Y_NP_099_MacroscopicClassical` | huge modes → instant decoherence | ✅ |
| `Y_NP_099_ClassicalLimit` | ℏ→0 = decoherence-dominant | ✅ |
| `Y_NP_099_Classification` | classicality EMERGENT; primitive REFUTED | ✅ |
| `Y_NP_099_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_099"`

---

## References

- ResearchY-NP_093 (selection = Born rule), NP_094 (inertia), NP_095 (friction), NP_096
  (heat/entropy), NP_097 (temperature), NP_098 (localization), NP_071 (matter = deficit),
  NP_006/007 (interference cross-term), NP_072 (particle = resonance class).
- AT-QG: QG216 (Born rule), QG228 (I_occ).
- ResearchY-D_041 (tick / phase advance).
