# ResearchY-M_004 — Measurement Information Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** M — Measurement Origin
**ID:** ResearchY-M_004 (permanent)
**Title:** Measurement Information Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `M_Measurement/ResearchY-M_004.md`
**Depends on:** ResearchY-M_001 (measurement event), M_002 (disturbance), M_003
(feedback), D_039 (state identity)
**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_004_Tests.cs`

---

## Purpose

**What is the information-theoretic limit of a measurement event?** M_001–M_003
established that a measurement is an actualization event that pins the phase and feeds
the outcome forward. This audit asks the information-theoretic question: how much
information can one actualization event reveal?

## Accepted (from M_001–M_003, D_039)

- A measurement event = an actualization event that reads both quadratures of a
  distinguishable state (M_001).
- The disturbance is phase-pinning (M_002); the outcome feeds the future (M_003).
- State identity = Difference applied; the complex state space is 95/95 distinct
  (D_039).
- The realized record's information is I_occ = KL(ρ‖uniform) = 0.7513 nats (QG228).

---

## 1. Definitions

| Term | Definition |
|---|---|
| **information** | the reduction of uncertainty about which state is realized |
| **distinguishability** | the state space's distinct points (95/95, D_039) |
| **measurement outcome** | the pinned phase θ₀ and the realized mode (M_002/M_003) |
| **actualization event** | the count realization that reads both quadratures (M_001) |

---

## 2. Information before vs after measurement

| | Before measurement | After measurement |
|---|---|---|
| state | one of 95 (uncertainty log₂ 95) | the realized outcome (uncertainty 0) |
| phase | free (all values available) | pinned (θ₀ fixed, M_002) |
| trajectory | superposition | selected (M_003) |
| information | H = log₂ 95 ≈ 6.57 bits (uniform) | 0 (the outcome is known) |

**The measurement resolves the uncertainty over the state space: information GAIN =
log₂(95) ≈ 6.57 bits (uniform prior).**

---

## 3. What is gained / fixed / lost?

| Quantity | Status |
|---|---|
| **GAINED** | the mode index (log₂ 95 bits of state-space resolution) |
| **FIXED** | the phase (pinned by the read, M_002); the outcome (trajectory selected, M_003) |
| **LOST** | the phase freedom (the superposition collapsed to one trajectory) |

**What is gained: the state's identity is resolved. What is fixed: the phase. What is
lost: the phase freedom.**

---

## 4. Single vs repeated measurement

| | Single | Repeated |
|---|---|---|
| information gain | log₂ 95 (the outcome resolved) | **ZERO additional** (idempotent, M_002) |
| phase | pinned | stays pinned (same read) |
| trajectory | selected | same trajectory (no further change) |

**A repeated measurement yields NO additional information** — the read is idempotent
(M_002). The maximum information content of one event is the state-space
distinguishability.

---

## 5. Prove or refute: measurement creates information

**YES.** Before the measurement, the state is one of 95 (uncertainty log₂ 95); after,
the outcome is known (uncertainty 0). The measurement event CREATES information by
resolving the state-space uncertainty — the gain is log₂(95) ≈ 6.57 bits (uniform).

---

## 6. Maximum information content of one actualization event

**The maximum information content of one actualization event is log₂(95) ≈ 6.57 bits**
— the distinguishability of the state space (D_039). A single event reads both
quadratures of one mode, resolving which of the 95 states is realized. This is the
information-theoretic limit: one event can reveal at most the state-space size.

---

## Theorem

> **Theorem (M_004).** The maximum information content of one actualization event is
> log₂(95) ≈ 6.57 bits — the distinguishability of the state space. A measurement event
> reads both quadratures of one complex mode (M_001), resolving which of the 95
> distinguishable states (D_039) is realized: information before = log₂ 95 (uncertainty
> over the state space), after = 0 (outcome known). GAINED: the mode index (log₂ 95
> bits). FIXED: the phase (pinned, M_002) and the outcome (trajectory selected, M_003).
> LOST: the phase freedom (superposition → one trajectory). Repeated measurements are
> IDEMPOTENT — no additional information (M_002). Prove/refute: measurement creates
> information — YES (it resolves the state-space uncertainty). Hence the measurement
> event is EMERGENT (M_001); information is DERIVED from distinguishability (D_039);
> the max info per event is DERIVED (log₂ 95). No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) The state space has 95 distinct points (D_039). (2) A measurement
> reads both quadratures, resolving the state (M_001). (3) Information before = log₂ 95,
> after = 0 — gain log₂ 95 (Section 2, verified). (4) Repeated reads are idempotent
> (Section 4, M_002). (5) Hence the max info per event = log₂ 95 (Section 6). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability (95/95 distinct, D_039)     [DERIVED]
 → MEASUREMENT (reads both quadratures)           [EMERGENT — M_001]
 → INFORMATION (outcome resolves the state)       [DERIVED]
    → max info per event = log₂ 95 ≈ 6.57 bits   [DERIVED]
    → phase fixed (M_002) / trajectory selected (M_003) [DERIVED]
    → repeated measurement idempotent (no more info) [DERIVED]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is the state space 95/95 distinct? | **YES** (D_039) |
| Does a measurement resolve the state? | **YES** (M_001) |
| Is the information gain log₂ 95? | **YES** (uniform prior, verified) |
| Does measurement create information? | **YES** (uncertainty → 0) |
| Is repeated measurement informative? | **NO** (idempotent, M_002) |
| Is the max info per event log₂ 95? | **YES** (the state-space size) |
| Is information derived? | **YES** (from distinguishability, D_039) |

---

## Falsification Path

**M_004 is falsified if:** a single actualization event reveals MORE than log₂(95) bits
— i.e., an event that distinguishes more states than the state-space size (a read with
> log₂ 95 information content). Such an event would exceed the information-theoretic
limit of the state space.

---

## Counterexamples

1. **Repeated read**: idempotent — NO additional information (M_002).
2. **Real-only space**: 48 states (collapsed) — less distinguishability, less
   information per event.
3. **Phase-pinned state**: the phase freedom is lost — the superposition collapsed to
   one trajectory.
4. **Unmeasured state**: full uncertainty (log₂ 95) — no information yet.

---

## Classification

| Component | Status |
|---|---|
| distinguishability (95/95) | **DERIVED** (D_039) |
| information (outcome resolves the state) | **DERIVED** |
| max info per event (log₂ 95) | **DERIVED** |
| measurement event | **EMERGENT** (M_001) |
| phase-pinning / trajectory selection | **DERIVED** (M_002/M_003) |
| repeated-measurement idempotence | **DERIVED** |

**The information-theoretic limit of one measurement event is log₂(95) ≈ 6.57 bits —
the state-space distinguishability. Measurement creates information by resolving the
state. Information is DERIVED; the measurement event is EMERGENT. No new primitive;
canonical AT unchanged.**

---

## Open Problems

1. **Born-weighted gain (M_004 OP1).** The uniform-prior gain is log₂ 95; the
   Born-weighted gain (Shannon entropy of the realized record, I_occ = 0.7513 nats)
   refines this for non-uniform outcomes — the next information audit.

---

## Next Steps

- **ResearchY-M_005 (or synthesis):** the information audit completes the
  measurement-program core. A synthesis can map the full chain:
  event (M_001) → disturbance (M_002) → feedback (M_003) → information (M_004) →
  observer.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_004_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_M_004_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_M_004_InformationGain` | info before log₂95, after 0 → gain log₂95 | ✅ |
| `Y_M_004_RepeatedMeasurement` | idempotent — no additional information | ✅ |
| `Y_M_004_Distinguishability` | 95/95 distinct complex states | ✅ |
| `Y_M_004_ActualizationInformation` | the read resolves the state (max log₂95) | ✅ |
| `Y_M_004_DependencyTrace` | Difference → distinguishability → measurement → information | ✅ |
| `Y_M_004_Run` | Research report | ✅ |

**Conclusion:** The information-theoretic limit of one measurement event is
log₂(95) ≈ 6.57 bits — the state-space distinguishability. Measurement creates
information by resolving which state is realized; repeated measurements are idempotent
(no additional information). Information is DERIVED; the measurement event is EMERGENT.
No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_004"`

---

## References

- ResearchY-M_001 (measurement event), M_002 (disturbance), M_003 (feedback),
  D_039 (state identity).
- AT-QG: QG216 (Born rule), QG228 (I_occ = 0.7513 nats), QG74 (measurement basis).
- Monograph V2.0: Ch9 (quantum mechanics).
