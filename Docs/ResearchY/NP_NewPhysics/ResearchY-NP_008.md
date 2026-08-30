# ResearchY-NP_008 — Interference Extremum Principle Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_008 (permanent)
**Title:** Interference Extremum Principle Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_008.md`
**Depends on:** ResearchY-NP_006 (phase-locking origin), NP_007 (coupling network),
M_003 (feedback), M_005 (information conservation)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_008_Tests.cs`

---

## Purpose

**Does Actualization extremize the interference functional I?** NP_007 established the
coupling network (I = ρ_A+ρ_B+2√(ρ_Aρ_B)·cos(θ_A−θ_B)) with Born-derived link weights.
This audit asks whether Actualization follows max(I), min(I), or stationary(I) — and
whether the missing synchronization dynamics already exists implicitly as a gradient
descent/ascent on I.

---

## 1. The functional and its derivatives

```
I = ρ_A + ρ_B + 2√(ρ_Aρ_B)·cos(θ_A − θ_B)         (rel = θ_A − θ_B)
∂I/∂θ_A = +2√(ρ_Aρ_B)·sin(θ_B − θ_A)
∂I/∂θ_B = −2√(ρ_Aρ_B)·sin(θ_B − θ_A)
```

Verified: ρ_A=0.25, ρ_B=0.75 gives ∂I/∂θ_A = −0.4152·sign(sin(rel)).

---

## 2. Maxima, minima, stationary points

| rel | I | Type |
|---|---|---|
| 0 (in-phase) | (√ρ_A+√ρ_B)² = 1.866 | **MAXIMUM** |
| π (anti-phase) | (√ρ_A−√ρ_B)² = 0.134 | **MINIMUM** |
| π/2, 3π/2 | ρ_A+ρ_B = 1.000 | stationary in |∂I/∂rel|² sense? NO — these are inflection points of the cos, not extrema |

The extrema of I w.r.t. the relative phase are: **maximum at rel = 0 (in-phase),
minimum at rel = π (anti-phase)**. The gradient ∂I/∂θ_A vanishes at both.

---

## 3. Actualization evolution vs gradient evolution

| Evolution | Update | Effect on I |
|---|---|---|
| **Actualization** | θ(t+1) = θ(t) + Δθ (self-rate) | I CHANGES (drifts), no extremization, no conservation |
| **Gradient** | θ(t+1) = θ(t) + η·∂I/∂θ | I increases (ascent) or decreases (descent) monotonically → locks at an extremum |

**Verified:** under actualization drift (k_A=16, k_B=32, rel₀=0.5), I goes 1.760 →
1.740 → 0.980 — changing, not extremized, not conserved. The gradient update would
drive the relative phase to rel = 0 (max I) or rel = π (min I).

---

## 4. Does actualization increase/decrease/conserve I?

| Property | Result |
|---|---|
| increase I | NO — I drifts down and up as rel sweeps |
| decrease I | NO — not monotone |
| conserve I | NO — I changes under the drift (1.760 → 0.980 in 2 ticks) |

**Actualization neither extremizes nor conserves I.** It follows the fixed self-rates
(D_041), sweeping the relative phase through the full circle.

---

## 5. Determination

| Option | Verdict |
|---|---|
| A) max(I) | **NO** — canonical evolution does not seek the in-phase maximum |
| B) min(I) | **NO** — canonical evolution does not seek the anti-phase minimum |
| C) stationary(I) | **NO** — canonical evolution does not stop at an extremum |
| D) none | **YES — canonical Actualization extremizes NOTHING** |

**Canonical Actualization follows NONE of the extremum options** — it is the fixed
self-rate evolution (D_041). BUT the gradient evolution θ(t+1) = θ(t) + η·∂I/∂θ is the
missing synchronization dynamics (NP_005/NP_006) — it would drive the relative phase
to an extremum of I.

---

## 6. Hidden variational principle

The gradient of I is EXACTLY the missing locking term (NP_006):

```
∂I/∂θ_A = 2√(ρ_Aρ_B)·sin(θ_B − θ_A) = κ·sin(θ_B − θ_A),  κ = 2√(ρ_Aρ_B)
```

**A variational principle on I — phase advance ∝ ∂I/∂θ — IS the missing
synchronization dynamics.** It is not present in canonical AT (the update is the
self-rate only), but it is IMPLICIT in the interference functional: the gradient
descent/ascent on I reproduces the Kuramoto locking term with the Born-derived
coefficient.

---

## 7. Evaluation: synchronization, coherence, collective modes

| Observable | Under canonical actualization | Under gradient (variational) evolution |
|---|---|---|
| **synchronization** | absent (unequal modes drift, NP_005) | present — rel → 0 or π (extremum of I) |
| **coherence** | drifts (interference fringes move) | sustained (locked relative phase) |
| **collective modes** | transient (rel sweeps through 0, π) | STABLE (in-phase/anti-phase are attractors) |

**The interference extremum principle, if actualized, would make the collective modes
stable and the synchronization explicit.**

---

## Theorem

> **Theorem (NP_008).** Canonical Actualization does NOT extremize, maximize, minimize,
> or conserve the interference functional I — it follows the fixed self-rate update
> θ(t+1) = θ(t) + Δθ (D_041), sweeping the relative phase through the full circle, so
> I changes non-monotonically (verified: rel₀=0.5, I: 1.760 → 1.740 → 0.980). The
> extrema of I are the in-phase maximum (rel=0, I=(√ρ_A+√ρ_B)² = 1.866) and the
> anti-phase minimum (rel=π, I=(√ρ_A−√ρ_B)² = 0.134); ∂I/∂θ_A vanishes at both.
> HOWEVER, the gradient ∂I/∂θ_A = κ·sin(θ_B−θ_A) with κ = 2√(ρ_Aρ_B) is EXACTLY the
> missing synchronization term (NP_005/NP_006): a variational phase update θ(t+1) =
> θ(t) + η·∂I/∂θ would drive the relative phase to an extremum of I, making
> synchronization explicit, coherence sustained, and collective modes stable.
> Therefore: canonical Actualization extremizes NOTHING (D — none); the interference
> EXTREMUM PRINCIPLE is a hidden variational structure that, if actualized, IS the
> missing synchronization dynamics. Classification: the functional I and its gradient
> are DERIVED (complex state + Born); the extrema (max/min) are DERIVED (algebra of
> I); canonical drift is DERIVED (fixed Δθ); the extremum PRINCIPLE (phase follows
> ∂I/∂θ) is EMERGENT under a variational requirement — absent (BOUNDARY) in canonical
> AT. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Compute ∂I/∂θ_A, ∂I/∂θ_B (Section 1, verified). (2) Identify
> extrema: max at rel=0, min at rel=π (Section 2, verified: 1.866 / 0.134). (3)
> Compare actualization vs gradient evolution (Section 3, verified: I drifts under
> actualization, monotone under gradient). (4) Show canonical actualization extremizes
> nothing (Sections 4–5). (5) Show the gradient is the missing locking term (Section
> 6) and evaluate consequences (Section 7). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → Interference functional I (Born, complex state) — DERIVED
 → Extremum Principle?
    → canonical: NONE (D — self-rate drift)
    → gradient on I: max/min (EMERGENT under variational requirement)
 → Synchronization
    → canonical: absent (NP_005)
    → variational: rel → extremum of I (in-phase/anti-phase attractors)
```

---

## 8. Necessity Proof

The gradient ∂I/∂θ_A is NECESSARY for unequal-mode synchronization (NP_005: a stable
fixed point requires the κ·sin(θ_B−θ_A) term; NP_006: its natural form IS ∂I/∂θ_A).
The extremum principle is the CLEANEST statement of that necessity: synchronizing
equalizes the phase advance at an extremum of I. Without a phase update that follows
∂I/∂θ, no synchronization mechanism exists.

---

## 9. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Actualization maximizes I" | the drift sweeps rel through the circle; I goes 1.760 → 0.980 (down), not up |
| "Actualization conserves I" | I changes under the drift — no conservation |
| "The stationary point is π/2" | π/2 is not an extremum of the cos-functional; ∂I/∂θ_A ≠ 0 there (=−0.866) |
| "The extremum principle exists canonically" | canonical update has only the self-rate (D_041) — no gradient term |

---

## 10. Falsification Path

1. **Canonical no-extremum claim** — falsified if a two-mode system's relative phase
   is observed to SEEK an extremum of I (rel → 0 or π) with NO added interaction:
   that would require an extremum principle absent from the canonical update.
2. **Gradient-is-locking claim** — falsified if the phase advance does NOT follow
   ∂I/∂θ (coefficient ≠ 2√(ρ_Aρ_B)) in a synchronizing system.

---

## Classification

| Component | Status |
|---|---|
| interference functional I | **DERIVED** (complex state D_036 + Born QG216) |
| extrema (max rel=0, min rel=π) | **DERIVED** (algebra of I) |
| gradient ∂I/∂θ_A = κ·sin(θ_B−θ_A) | **DERIVED** (algebra of I) |
| canonical drift (no extremization) | **DERIVED** (fixed Δθ, D_041) |
| extremum principle (phase ∝ ∂I/∂θ) | **EMERGENT** (under variational requirement) / **BOUNDARY** in canonical AT |

**Canonical Actualization extremizes nothing; the interference EXTREMUM PRINCIPLE is a
hidden variational structure that would BE the missing synchronization dynamics. No
new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Variational actualization (NP_008 OP1).** Whether the extremum principle on I
   should be adopted (phase advances along ∂I/∂θ) — the single requirement that would
   make synchronization self-generated (extends NP_006 OP1).

---

## Next Steps

- **Registry note:** canonical Actualization extremizes nothing; the gradient on the
  interference functional is the hidden synchronization dynamics (emerges under a
  variational requirement).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_008_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_008_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_008_InterferenceGradient` | ∂I/∂θ_A = 2√(ρ_Aρ_B)·sin(θ_B−θ_A) | ✅ |
| `Y_NP_008_Maxima` | max at rel=0 (in-phase, 1.866) | ✅ |
| `Y_NP_008_Minima` | min at rel=π (anti-phase, 0.134) | ✅ |
| `Y_NP_008_StationaryPoints` | ∂I/∂θ vanishes at max/min only | ✅ |
| `Y_NP_008_ActualizationEvolution` | I drifts (no extremization, no conservation) | ✅ |
| `Y_NP_008_SynchronizationCriterion` | gradient evolution locks at an extremum | ✅ |
| `Y_NP_008_Run` | research report | ✅ |

**Conclusion:** Canonical Actualization extremizes NOTHING (option D) — it follows the
fixed self-rates and I drifts. The interference EXTREMUM PRINCIPLE (phase ∝ ∂I/∂θ) is
a hidden variational structure that is exactly the missing synchronization dynamics:
it would lock the relative phase at an extremum of I. No new primitive; canonical AT
unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_008"`

---

## References

- ResearchY-NP_005 (missing sync mechanism, threshold), NP_006 (phase-locking origin,
  κ = 2√(ρ_Aρ_B)), NP_007 (coupling network), M_003 (feedback), M_005 (information
  conservation), D_036 (complex state), D_041 (tick rate).
- AT-QG: QG216 (Born rule).
