# ResearchY-QG_016 — Tick Discreteness Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** QG — Geometry Bridge
**ID:** ResearchY-QG_016 (permanent)
**Title:** Tick Discreteness Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-31
**File:** `QG_GeometryBridge/ResearchY-QG_016.md`
**Depends on:** ResearchY-QG_011 (finite event principle), D_041 (time origin),
M_008/M_009 (measurement predictions), QG_015 (observable world)
**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_016_Tests.cs`

---

## Purpose

**Why must the actualization tick be discrete?** QG_011 identified the discreteness of
the actualization tick as the deepest remaining boundary; QG_015 showed observability
is EMERGENT under the observable-sector input. This audit asks the final question:
is tick discreteness a PRIMITIVE boundary, or does it follow from Difference itself?
The answer determines whether the theory's fundamental clock is derived or input.

---

## 1. The two discretenesses

| Discreteness | Object | Source |
|---|---|---|
| **state-space discreteness** | the 95 distinct states (a SET) | Difference → distinguishability (D_039) |
| **dynamics discreteness** | the TICK (Δθ = 2πk/N per step) | the phase advance (D_041) |

**These are different.** Difference produces a discrete SET of states (membership
discreteness — the elements are enumerated). The tick is a DYNAMIC parameter — the
step of the phase advance. Nothing in "distinguishing states" forces the ADVANCE to
be stepwise vs continuous.

---

## 2. Compare: discrete tick vs continuous actualization

| Property | Discrete tick | Continuous actualization |
|---|---|---|
| phase evolution | θ_m = θ₀ + m·2πk/N (lattice) | θ(t) continuous |
| phase lattice | N/gcd(N,k) values (k=1→96, k=16→6, k=48→2) | continuum of values |
| one event = one step | YES (M_001, one outcome) | no fundamental step |
| information per event | finite: log₂(95) = 6.57 bits (M_004) | finite IF sampled discretely |
| normalization | Σρ = 1 over discrete outcomes | still Σρ = 1 (count is discrete) |
| count conservation | Σρ = 1 (normalizer S) | still holds (count structure) |
| AT-P042 (discrete lattice) | fundamental clock | an artifact of sampling |
| phase-advance derivation | Δθ = 2πk/N FROM the spectrum (D_041) | decoupled from the spectrum (free) |

---

## 3. The critical fact: continuous actualization is NOT observationally inconsistent

**M_010 established the decisive equivalence:** continuous actualization with the
matching rate ω = 2πk/(N·τ) reproduces AT-P042 EXACTLY at every tick-sampled time —
phase, recurrence (N/gcd(N,k)), interference, and finite-state orbits are identical
at samples. The ONLY difference is the SUB-TICK phase.

**Therefore observability does NOT force discrete dynamics.** A continuous dynamics
SAMPLED at discrete times gives the same observable physics. The discreteness of the
DYNAMICS (the tick as fundamental) is not implied by observability — finite events
(QG_010/QG_011) require discrete READS, but those reads can sample a continuous
evolution.

---

## 4. First inconsistency of continuous actualization

| Candidate | Continuous actualization | First to fail |
|---|---|---|
| observability | survives (discrete sampling, M_010) | — |
| information gain | finite per event (discrete read) | — |
| normalization | Σρ = 1 (count discrete) | — |
| count conservation | Σρ = 1 (normalizer S) | — |
| phase evolution | continuous — works | — |
| **phase-advance derivation** | **Δθ decouples from the spectrum — becomes a free continuous parameter** | **FIRST** |
| **AT-P042** | **the discrete lattice becomes a sampling artifact, not the fundamental clock** | **FIRST** |

**The first inconsistency of continuous actualization is STRUCTURAL, not
observational:** the phase advance loses its spectral derivation (Δθ = 2πk/N derived
from the mode index k and state count N, D_041) and becomes a free continuous
parameter — a new boundary — while AT-P042 (the discrete lattice) is demoted from a
fundamental clock to an artifact. This contradicts the theory's own derivation chain.

---

## 5. Test effects

| Effect | Discrete tick | Continuous actualization |
|---|---|---|
| observability | finite events (QG_011) | survives via discrete sampling (M_010) |
| information gain | log₂(95) per event (M_004) | same (discrete read) |
| normalization | Σρ = 1 | same (count is discrete) |
| count conservation | Σρ = 1 (normalizer S) | same (count structure) |
| phase evolution | lattice, derived step | continuum, free step |

**Continuous actualization breaks nothing observable — it breaks the DERIVED
structure of the phase advance and the fundamental status of the tick.**

---

## 6. Search: minimal principle forcing a discrete event

**There is no principle forcing discrete DYNAMICS:**

- Difference → distinguishability forces a discrete SET of states (membership), not a
  discrete advance.
- Observability forces finite EVENTS (discrete reads, QG_010/QG_011) but NOT discrete
  dynamics — continuous evolution sampled discretely is observationally equivalent
  (M_010).
- The count structure (ρ_k = μ^k/S, QG194) is discrete, but it is the count over
  STATES, not the advance over TIME.

**The minimal principle forcing the discrete event is the observable-sector
requirement (D_020) TOGETHER with the canonical phase advance (D_041):** the states
are discrete (Difference), and the phase advances in the derived spectral step
Δθ = 2πk/N (D_041). The DISCRETENESS of the dynamics is the input; the STEP SIZE is
derived.

---

## 7. Prove or refute: Difference implies discrete events

**REFUTED — as a claim about dynamics.** Difference implies DISCRETE STATES (the
95-state set, D_039) but NOT discrete EVENTS:

1. Difference → distinguishability produces the state space (a discrete set of
   members).
2. The tick (Δθ = 2πk/N) is the DYNAMIC step of the phase advance — a property of
   HOW actualization evolves, not of WHAT the states are.
3. A continuous actualization is observationally equivalent at all tick-sampled
   times (M_010) — so observability does not force the discrete dynamics either.
4. Therefore Difference implies discrete STATES (derived) but the discrete TICK
   (the stepwise dynamics) is an additional input.

**The discreteness of the DYNAMICS is BOUNDARY; the discreteness of the STATE SPACE
is DERIVED; the STEP VALUE Δθ = 2πk/N is DERIVED from the spectrum (D_041).**

---

## Theorem

> **Theorem (QG_016).** Tick discreteness is a BOUNDARY: Difference implies discrete
> STATES but not discrete DYNAMICS, and observability does not force it either —
> but GIVEN a discrete step, its value is DERIVED from the spectrum. Proof: (1)
> Distinguish the state-space discreteness (a discrete SET of 95 states, D_039,
> DERIVED from Difference) from the dynamics discreteness (the tick Δθ = 2πk/N,
> D_041). (2) Compare discrete vs continuous actualization (Section 2): continuous
> evolution preserves observability (discrete sampling), finite information per
> event (log₂(95), M_004), normalization (Σρ = 1), and count conservation (the count
> is over discrete states). (3) The decisive fact (M_010): continuous actualization
> with rate ω = 2πk/(N·τ) reproduces AT-P042 EXACTLY at every tick-sampled time —
> phase, recurrence N/gcd(N,k), interference, and finite-state orbits are identical.
> Observability therefore does NOT force discrete dynamics (finite events, QG_011,
> require discrete READS, which can sample a continuous evolution). (4) The first
> inconsistency of continuous actualization is STRUCTURAL, not observational: the
> phase advance Δθ = 2πk/N loses its spectral derivation (the step was derived from
> the mode index k and state count N, D_041) and becomes a free continuous parameter,
> while AT-P042 is demoted from the fundamental clock to a sampling artifact. (5)
> Therefore Difference implies discrete STATES (DERIVED) but not discrete EVENTS —
> the tick's DISCRETENESS is a BOUNDARY (the stepwise dynamics is an input), while
> the STEP VALUE Δθ = 2πk/N is DERIVED from the spectrum (D_041). (6) Prove/refute:
> Difference implies discrete events — REFUTED (as dynamics; the states are discrete,
> the advance could be continuous, and continuous sampling is observationally
> equivalent). Classification: state-space discreteness DERIVED (Difference → set);
> finite events EMERGENT (observable-sector requirement, QG_010/QG_011); the tick's
> DISCRETENESS BOUNDARY (canonical, D_041); the step VALUE Δθ = 2πk/N DERIVED (from
> the spectrum); AT-P042 as the fundamental clock BOUNDARY-supported (structural
> prediction, M_009). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Distinguish the two discretenesses (Section 1). (2) Compare
> discrete/continuous (Section 2). (3) Establish the M_010 equivalence (Section 3).
> (4) Locate the first structural inconsistency (Section 4). (5) Test the effects
> (Section 5). (6) Search the minimal principle (Section 6). (7) Refute the
> implication (Section 7). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability (D_039)
    ├── discrete STATE SPACE (95 states) — DERIVED (membership discreteness)
    └── observability (finite events, QG_010/QG_011) — EMERGENT
 → Actualization
    ├── tick discreteness (the stepwise dynamics) — BOUNDARY (canonical D_041)
    └── step value Δθ = 2πk/N — DERIVED (from the spectrum, D_041)
 → Finite Event (QG_011) — EMERGENT (consistency with finite observation)
 → Observable Physics (AT-P042: discrete lattice — structural prediction)
```

**The two discretenesses split:** the state space is discrete by Difference
(DERIVED); the dynamics tick is discrete by input (BOUNDARY); the step size is
derived (DERIVED).

---

## 8. Boundary Reduction Test

| Proposed reduction | Verdict |
|---|---|
| tick discreteness → Difference | **NO** — Difference produces a discrete SET, not a discrete advance |
| tick discreteness → observability | **NO** — continuous dynamics with discrete sampling is observationally equivalent (M_010) |
| tick discreteness → count structure | **NO** — the count (ρ_k = μ^k/S) is over states, not over time |
| step VALUE Δθ = 2πk/N → spectrum | **YES — DERIVED** (D_041: the step is the spectral phase quantum) |

**The discreteness of the dynamics is NOT reducible — it is a BOUNDARY. The step
value IS reducible to the spectrum.**

---

## 9. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Difference implies discrete events" | Difference produces a discrete SET of states; the ADVANCE could be continuous (M_010: sampling equivalent) |
| "Observability forces discrete dynamics" | finite events need discrete READS, not discrete evolution (continuous + sampling works) |
| "Continuous actualization breaks measurement" | discrete sampling of continuous evolution gives the same observable physics |
| "The tick is derived from Difference" | the tick is a dynamic parameter; Difference fixes the states, not the advance |
| "The step value is boundary too" | Δθ = 2πk/N IS derived from the spectrum (D_041) — only the discreteness is input |

---

## 10. Falsification paths

| Claim | Falsification |
|---|---|
| tick discreteness is boundary | a derivation of stepwise dynamics from Difference alone |
| observability does not force discreteness | an observable that requires a continuous evolution to exist |
| continuous actualization is observationally equivalent | an observable differing between discrete and continuous evolution at sampled times |
| the step value is derived | a phase advance not equal to 2πk/N from the spectrum |

---

## Classification

| Component | Status |
|---|---|
| state-space discreteness (the set) | **DERIVED** (Difference → distinguishability, D_039) |
| finite events | **EMERGENT** (observable-sector requirement, QG_010/QG_011) |
| **tick DISCRETENESS (stepwise dynamics)** | **BOUNDARY** (canonical input, D_041) |
| step VALUE Δθ = 2πk/N | **DERIVED** (from the spectrum, D_041) |
| AT-P042 (discrete lattice) | **STRUCTURAL PREDICTION** (M_009), boundary-supported |

**Tick discreteness is a BOUNDARY: Difference implies discrete STATES (derived) but
not discrete EVENTS — the stepwise dynamics is an input, not a consequence. The step
VALUE is derived from the spectrum. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **None new.** The discreteness question is resolved: state discreteness DERIVED
   (Difference → set), dynamics discreteness BOUNDARY (the stepwise advance is the
   canonical input), step value DERIVED (spectral). The tick remains the deepest
   structural boundary — but it is now precisely located: its DISCRETENESS is input,
   its SIZE is derived.

---

## Next Steps

- **Registry note:** tick discreteness is BOUNDARY (dynamics input); state-space
   discreteness is DERIVED (Difference → set); the step value Δθ = 2πk/N is DERIVED
   (spectral); continuous actualization is observationally equivalent at sampled
   times (M_010) — its inconsistency is structural (the phase advance loses its
   spectral derivation).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_016_Tests.cs`
**Run:** 2026-08-31 · **Result:** see `Tests/Results/Y_QG_016_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_016_DiscreteTick` | the lattice structure; Δθ = 2πk/N derived | ✅ |
| `Y_QG_016_ContinuousActualization` | continuous sampling is observationally equivalent | ✅ |
| `Y_QG_016_PhaseLattice` | N/gcd(N,k) cardinalities; AT-P042 | ✅ |
| `Y_QG_016_InformationGain` | finite info per event; continuous breaks derivation | ✅ |
| `Y_QG_016_BoundaryReduction` | discreteness not reducible to Difference/observability | ✅ |
| `Y_QG_016_Run` | research report | ✅ |

**Conclusion:** Tick discreteness is a BOUNDARY — Difference implies discrete STATES
(DERIVED) but not discrete EVENTS. Continuous actualization is observationally
equivalent at sampled times (M_010), so observability does not force discreteness;
its inconsistency is structural (the phase advance loses its spectral derivation).
The step VALUE Δθ = 2πk/N is DERIVED from the spectrum (D_041); the stepwise DYNAMICS
is the canonical input. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_016"`

---

## References

- ResearchY-QG_011 (finite event principle), QG_015 (observable world), D_041 (time
  origin), M_008/M_009 (measurement predictions).
- AT-QG: QG194 (normalizer S), QG220 (phase).
