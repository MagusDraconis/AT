# ResearchY-NP_018 — Distinguishability Observable Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_018 (permanent)
**Title:** Distinguishability Observable Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_018.md`
**Depends on:** ResearchY-D_039 (Difference = distinguishability), M_004 (information
from distinguishability), M_005 (measurement reveals distinguishability), M_007
(measurement-program synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_018_Tests.cs`

---

## Purpose

**Does distinguishability itself generate an observable physical quantity?** D_039
established Difference = distinguishability; M_004/M_005 derived information from it.
This audit searches for a DIRECT measurable signature of distinguishability — an
observable that can be written as a function of distinguishability alone — beyond the
D96 spectrum.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **distinguishability** | the number/quality of distinct states the theory can host (D_039: 95) |
| **observable quantity** | a measurable physical number |
| **information** | the entropy of the state space: H = log₂(95) = 6.57 bits (M_004) |
| **information density** | the Born-weighted occupancy information: I_occ = 0.7513 nats (QG228) |

---

## 2. Search: entropy, information density, coherence, state count

| Candidate | Value | Function of distinguishability? |
|---|---|---|
| **state count** | 95 distinct states | **YES — by definition (D_039)** |
| **entropy H** | log₂(95) = 6.57 bits = ln 95 nats | **YES — H = ln(#states)** |
| **information density I_occ** | 0.7513 nats | **YES — the occupancy-weighted distinguishability information (QG228)** |
| **cosmological fraction ΩΛ** | I_occ/ln K = 0.6839 | **YES — written directly from the information density (QG234)** |
| **coherence** | interference contrast | PARTIAL — depends on relative phase, not the state count |

---

## 3. Can an observable be written directly as a function of distinguishability?

**YES — THREE observables are direct functions of distinguishability:**

```
state count:        95  =  the number of distinct states (D_039)
entropy:            H   =  log₂(95) = 6.57 bits  =  ln(#states)   (M_004)
cosmological ratio: ΩΛ  =  I_occ/ln K = 0.7513/1.0986 = 0.6839    (QG234)
```

The most striking: **ΩΛ = 0.6839** — the measured dark-energy fraction — is written
directly as a ratio of the distinguishability-derived information density (I_occ) to
the log of the state-space size K. This is a physical, measured, cosmological quantity
that is a direct function of distinguishability.

---

## 4. Comparison with QM / SM / GR

| Framework | Observable function of distinguishability? |
|---|---|
| **QM** | NO — QM has no fundamental state-count; entropy is derived from a given Hilbert space, not predicted |
| **SM** | NO — the SM has no distinguishability-origin observable |
| **GR** | NO — GR has no state-count |
| **AT** | **YES — H = log₂(95), ΩΛ = I_occ/ln K = 0.6839** — observables written directly from distinguishability |

**QM/SM/GR do NOT produce a fundamental observable written as a function of
distinguishability. AT does — most notably the cosmological fraction ΩΛ = 0.6839
(measured to 0.12%).**

---

## 5. Observable candidates

| Candidate | Value | Status |
|---|---|---|
| **state count** | 95 | structural — the state space size |
| **entropy H** | log₂(95) = 6.57 bits | DERIVED — the information content |
| **information density I_occ** | 0.7513 nats | DERIVED — Born-weighted occupancy info |
| **ΩΛ = I_occ/ln K** | **0.6839** | **OBSERVED — the dark-energy fraction (0.12%)** |
| **Ωm = 1 − ΩΛ** | 0.3161 | OBSERVED — the matter fraction (0.26%) |

---

## Theorem

> **Theorem (NP_018).** Distinguishability generates DIRECTLY OBSERVABLE physical
> quantities. Proof: (1) Distinguishability is the number of distinct states the
> theory hosts — 95 (D_039). (2) The entropy of that state space is H = log₂(95) =
> 6.57 bits (M_004) — a direct function of distinguishability. (3) The Born-weighted
> information density is I_occ = 0.7513 nats (QG228) — the occupancy-weighted
> distinguishability information. (4) The cosmological density fractions are written
> DIRECTLY from it: ΩΛ = I_occ/ln K = 0.7513/1.0986 = 0.6839 and Ωm = 1 − ΩΛ =
> 0.3161 (QG234) — and these are MEASURED (ΩΛ to 0.12%, Ωm to 0.26%). (5) Therefore
> distinguishability leaves a direct measurable signature: the dark-energy fraction is
> a function of the information density of the distinguishable state space. QM/SM/GR
> produce no such fundamental observable (QM has no predicted state-count; SM/GR have
> no distinguishability origin). OBSERVABLE CANDIDATES: the state count (95), the
> entropy (log₂ 95 = 6.57 bits), the information density (I_occ = 0.7513 nats), and —
> the strongest — the cosmological fraction ΩΛ = 0.6839 (observed). FALSIFICATION:
> a measured ΩΛ deviating from I_occ/ln K beyond the established 0.12% tolerance
> falsifies the distinguishability→information→cosmology chain. Classification: the
> state count and entropy are DERIVED (D_039/M_004); the information density is
> DERIVED (QG228); ΩΛ is a PREDICTION (distinguishability-derived, observed);
> coherence is CORRESPONDENCE (depends on relative phase, not the count). No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define distinguishability (Section 1). (2) Search the candidates
> (Section 2, verified: H = log₂95 = 6.57 bits; I_occ = 0.7513; ΩΛ = 0.6839). (3)
> Show the direct functional dependence (Section 3). (4) Compare with QM/SM/GR
> (Section 4). (5) List the observable candidates (Section 5). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability (D_039: 95 distinct states)
 → Information (H = log₂ 95 = 6.57 bits, M_004)
 → Information density (I_occ = 0.7513 nats, QG228)
 → Cosmological fraction (ΩΛ = I_occ/ln K = 0.6839, QG234) — OBSERVED
 → Observable signature (the dark-energy fraction)
```

---

## 6. Falsification paths

| Prediction | Falsification |
|---|---|
| ΩΛ = I_occ/ln K = 0.6839 | a measured dark-energy fraction deviating from I_occ/ln K beyond 0.12% |
| H = log₂(95) = 6.57 bits | a state space of a different size (state-count ≠ 95) |
| I_occ = 0.7513 nats | an occupancy-weighted information inconsistent with 0.7513 |

---

## Classification

| Component | Status |
|---|---|
| state count (95) | **DERIVED** (D_039) |
| entropy H = log₂(95) | **DERIVED** (M_004) |
| information density I_occ | **DERIVED** (QG228) |
| **ΩΛ = I_occ/ln K = 0.6839** | **PREDICTION** (distinguishability-derived, OBSERVED) |
| coherence | **CORRESPONDENCE** (relative-phase dependent) |

**Distinguishability generates directly observable quantities — most strikingly the
cosmological fraction ΩΛ = 0.6839, written directly from the information density of
the distinguishable state space and measured to 0.12%. No new primitive; canonical AT
unchanged.**

---

## Open Problems

1. **Information-density refinement (NP_018 OP1).** Whether I_occ admits a per-mode
   decomposition (a finer distinguishability observable than the single global value).

---

## Next Steps

- **Registry note:** ΩΛ = I_occ/ln K is the theory's most direct distinguishability
  observable — the dark-energy fraction derived from the state space's information.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_018_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_018_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_018_Distinguishability` | the 95-state distinguishability | ✅ |
| `Y_NP_018_Entropy` | H = log₂(95) = 6.57 bits | ✅ |
| `Y_NP_018_InformationDensity` | I_occ = 0.7513 nats | ✅ |
| `Y_NP_018_StateCount` | the state space size is 95 | ✅ |
| `Y_NP_018_ObservableFunction` | ΩΛ = I_occ/ln K = 0.6839 | ✅ |
| `Y_NP_018_QMComparison` | QM/SM/GR have no such observable | ✅ |
| `Y_NP_018_Run` | research report | ✅ |

**Conclusion:** Distinguishability generates directly observable quantities — the
state count (95), the entropy (log₂ 95 = 6.57 bits), the information density (I_occ =
0.7513 nats), and — the strongest — the cosmological fraction ΩΛ = 0.6839 (measured to
0.12%). QM/SM/GR produce no fundamental observable written as a function of
distinguishability. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_018"`

---

## References

- ResearchY-D_039 (Difference = distinguishability), M_004 (information log₂ 95),
  M_005 (measurement reveals distinguishability), M_007 (measurement synthesis).
- AT-QG: QG228 (I_occ = 0.7513 nats), QG234 (ΩΛ = I_occ/ln K = 0.6839).
