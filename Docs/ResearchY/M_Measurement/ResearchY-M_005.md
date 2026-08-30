# ResearchY-M_005 — Information Conservation Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** M — Measurement Origin
**ID:** ResearchY-M_005 (permanent)
**Title:** Information Conservation Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `M_Measurement/ResearchY-M_005.md`
**Depends on:** ResearchY-M_001 (measurement event), M_002 (disturbance), M_003
(feedback), M_004 (information), D_039 (state identity)
**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_005_Tests.cs`

---

## Purpose

**Does measurement create information or reveal pre-existing information?** M_004
established that a measurement resolves log₂(95) ≈ 6.57 bits of state-space
uncertainty. This audit asks the conservation question: is that information generated
by the event, or was it already present (and merely revealed/redistributed)?

## Accepted (from M_001–M_004, D_039)

- A measurement event = an actualization event that reads both quadratures (M_001).
- The disturbance is phase-pinning (M_002); the outcome feeds the future (M_003).
- The maximum information per event is log₂(95) ≈ 6.57 bits (M_004).
- The state space is 95/95 distinct (D_039); the Born rule Σ|ψ|² = 1 is exact (QG216).

---

## 1. Definitions

| Term | Definition |
|---|---|
| **information** | the distinguishability of the state space (the structure, D_039) |
| **uncertainty** | the observer's lack of knowledge about which state is realized |
| **distinguishability** | the 95 distinct points of the state space |
| **information gain** | the observer's uncertainty reduction (log₂ 95, M_004) |
| **information conservation** | total information is unchanged by the event (no creation/loss) |

---

## 2. Pre-measurement state vs post-measurement state

| | Pre-measurement | Post-measurement |
|---|---|---|
| state space | 95 distinct states (log₂ 95 bits PRE-EXISTING) | the same 95 states (no new states created) |
| observer | uncertainty log₂ 95 | outcome known (uncertainty 0) |
| phase | free | pinned (M_002) |
| trajectory | superposition | selected (M_003) |
| information total | log₂ 95 (in the space) | log₂ 95 (outcome + observer) |

**The 95 states exist BEFORE any measurement — the information is pre-existing in the
state space (D_039). The measurement resolves which one is realized.**

---

## 3. Measure: information in the state / outcome / observer

| Location | Information |
|---|---|
| **state space** | log₂ 95 ≈ 6.57 bits (pre-existing distinguishability, D_039) |
| **outcome** | the realized state (1 of 95) — the distinguishability made ACTUAL |
| **observer** | log₂ 95 bits gained (uncertainty resolved, M_004) |

**The total information is conserved: log₂ 95 (state space) = outcome + observer. The
event does not create the 6.57 bits — it REVEALS which state is realized and
REDISTRIBUTES the phase freedom into the outcome + the observer's knowledge.**

---

## 4. Does measurement create / reveal / redistribute?

| Option | Verdict |
|---|---|
| A) create information | **NO** — the 95 states pre-exist; no new states are created |
| B) reveal information | **YES** — the event resolves which of the 95 states is realized |
| C) redistribute information | **YES** — the phase freedom becomes a pinned outcome + observer knowledge |

**Measurement REVEALS pre-existing distinguishability and REDISTRIBUTES it — it does
NOT create information.**

---

## 5. Entropy, distinguishability, actualization

- **Entropy (observer):** log₂ 95 → 0 (the uncertainty is resolved).
- **Distinguishability (state space):** 95 states, unchanged (conserved).
- **Actualization (the event):** reads both quadratures — a count realization, count-
  conserving (Born rule Σ|ψ|² = 1, QG216).

**The underlying conservation is the count conservation of actualization (QG216): the
event redistributes, it does not create.**

---

## 6. Where do the 6.57 bits come from?

**From the STATE SPACE (pre-existing distinguishability, D_039).** The 95 distinct
states exist before any measurement; the measurement event reveals which one is
realized. The bits are NOT created by the measurement and NOT created by the observer —
they are the structure of the state space, made actual by the event.

---

## 7. Remove measurement — does information still exist?

**YES.** Without a measurement, the 95 states remain distinguishable (D_039) — the
information (log₂ 95 bits) still exists in the state space. What is absent is its
RESOLUTION (the outcome) and the observer's knowledge. **Information exists
independently of measurement; measurement only resolves it.**

---

## 8. Prove or refute: information is conserved through actualization

**YES — information is conserved through actualization.** The state-space information
(log₂ 95, pre-existing) equals the post-measurement total (outcome + observer):
H_before = H_outcome + H_observer. The actualization event reveals and redistributes —
it neither creates nor destroys information. This is the information face of the count
conservation (Born rule Σ|ψ|² = 1, QG216).

---

## Theorem

> **Theorem (M_005).** Measurement REVEALS pre-existing information and REDISTRIBUTES
> it — it does NOT create information. The 6.57 bits are pre-existing in the state
> space (D_039: 95 distinct states exist before any measurement). The measurement event
> reads both quadratures (M_001), resolving WHICH state is realized (reveal), and
> converts the phase freedom into a pinned outcome + observer knowledge (redistribute).
> INFORMATION BALANCE: log₂ 95 (state space) = outcome (realized state) + observer
> (log₂ 95 gained) — total CONSERVED. The underlying conservation is count
> conservation (Born rule Σ|ψ|² = 1, QG216). Test A/B/C: A) create — NO (the states
> pre-exist); B) reveal — YES; C) redistribute — YES. REFINES M_004: "measurement
> creates information" is the OBSERVER's gain; from the conservation view the event
> reveals + redistributes. Remove measurement: the information still exists (the 95
> states remain distinguishable) — only its resolution is absent. Classification:
> distinguishability/information DERIVED (D_039, pre-existing); reveal EMERGENT (the
> resolution event); redistribute DERIVED (phase → outcome + observer); conservation
> DERIVED (count conservation, QG216). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) The 95 states pre-exist (D_039). (2) The event resolves which is
> realized (Sections 2–3, verified). (3) The total information is conserved
> (Section 4, balance). (4) Count conservation underlies it (Section 5, QG216). (5)
> Hence reveal + redistribute, not create (Sections 6–8). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability (95/95, D_039)      [DERIVED — pre-existing]
 → State Identity                         [DERIVED — D_039]
 → MEASUREMENT (reads both quadratures)   [EMERGENT — M_001]
 → INFORMATION (reveal + redistribute)    [DERIVED — not created]
    → outcome (realized state) + observer (log₂ 95) [DERIVED]
    → count conservation (Born rule, QG216)          [DERIVED]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Do the 95 states pre-exist? | **YES** (D_039) |
| Is the information created by measurement? | **NO** (it pre-exists in the space) |
| Is the information revealed? | **YES** (the event resolves the outcome) |
| Is the information redistributed? | **YES** (phase → outcome + observer) |
| Is the total conserved? | **YES** (log₂ 95 = outcome + observer) |
| Does information survive measurement removal? | **YES** (the states remain distinct) |
| Is the conservation the count conservation? | **YES** (Born rule, QG216) |

---

## Information Balance

```
H_state_space (pre-existing) = log₂ 95 ≈ 6.57 bits   [D_039]
  → measurement event (reveals + redistributes)        [M_001]
  → H_outcome (the realized state) + H_observer (log₂ 95) 
  TOTAL: conserved  (H_before = H_after)
```

---

## Falsification Path

**M_005 is falsified if:** a measurement that CREATES a new distinguishable state (the
post-measurement state space is LARGER than 95) is demonstrated, or if the total
information changes across an event (H_before ≠ H_outcome + H_observer). Both would
violate information conservation.

---

## Counterexamples

1. **Pre-existing space**: 95 distinct states exist before any measurement (D_039) —
   the information is already there.
2. **Observer gain**: log₂ 95 bits are gained by the observer, but this is the
   REDISTRIBUTION of pre-existing distinguishability, not creation.
3. **Phase-pinning**: the phase freedom is converted into the pinned outcome (M_002) —
   redistribution.
4. **Remove measurement**: the 95 states remain — information still exists.

---

## Classification

| Component | Status |
|---|---|
| distinguishability / information | **DERIVED** (D_039 — pre-existing) |
| reveal (the resolution event) | **EMERGENT** (M_001) |
| redistribute (phase → outcome + observer) | **DERIVED** |
| information conservation | **DERIVED** (count conservation, QG216) |
| observer knowledge | **DERIVED** (the redistribution's receiving face) |

**Information is CONSERVED through actualization: the measurement event reveals
pre-existing distinguishability and redistributes it (phase freedom → outcome +
observer). It does NOT create information. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Born-weighted balance (M_005 OP1).** The uniform balance is log₂ 95 =
   outcome + observer; the Born-weighted refinement (I_occ = 0.7513 nats, QG228)
   refines the observer's average gain — the next conservation audit.

---

## Next Steps

- **ResearchY-M_006 (or synthesis):** the conservation audit completes the
  information chain. A synthesis can map the full measurement program:
  event (M_001) → disturbance (M_002) → feedback (M_003) → information (M_004) →
  conservation (M_005) → observer.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_005_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_M_005_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_M_005_InformationSource` | the 6.57 bits are pre-existing (state space, D_039) | ✅ |
| `Y_M_005_InformationGain` | observer gains log₂ 95 (the reveal) | ✅ |
| `Y_M_005_InformationConservation` | log₂ 95 = outcome + observer (conserved) | ✅ |
| `Y_M_005_ObserverInformation` | the observer's knowledge is the redistribution | ✅ |
| `Y_M_005_PrePostComparison` | 95 states before and after (no creation) | ✅ |
| `Y_M_005_DependencyTrace` | Difference → distinguishability → identity → measurement → info | ✅ |
| `Y_M_005_Run` | Research report | ✅ |

**Conclusion:** Measurement REVEALS pre-existing information and REDISTRIBUTES it —
it does NOT create information. The 6.57 bits pre-exist in the state space (D_039);
the event resolves which state is realized and converts the phase freedom into the
outcome + observer knowledge. Information is CONSERVED (count conservation, QG216).
No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_005"`

---

## References

- ResearchY-M_001 (measurement event), M_002 (disturbance), M_003 (feedback),
  M_004 (information), D_039 (state identity).
- AT-QG: QG216 (Born rule — count conservation), QG228 (I_occ = 0.7513 nats).
- Monograph V2.0: Ch9 (quantum mechanics).
