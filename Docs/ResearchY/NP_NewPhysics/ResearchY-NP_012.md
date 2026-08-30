# ResearchY-NP_012 — Unique Prediction Search

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_012 (permanent)
**Title:** Unique Prediction Search
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_012.md`
**Depends on:** ResearchY-D_020–D_045, M_001–M_010, NP_003–NP_011 (the measurement,
coupling, synchronization, and field programs)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_012_Tests.cs`

---

## Purpose

**What observable prediction survives after all QM-equivalent interpretations are
removed?** The V2.2 programs established: the measurement chain is mostly
QM-equivalent (M_001–M_010); AT-P043 is QM-standard (M_009); AT-P042 is structurally
unique but observationally in-principle-only (M_010); the coupling network is not a
physical field (NP_011); synchronization is absent (NP_005); no hidden field exists
(NP_007/NP_011). This audit enumerates every surviving AT-specific result, filters out
the QM-equivalent interpretations, and finds the first genuinely unique prediction.

---

## 1. Prediction inventory (every surviving AT-specific result)

| # | Result | Source | Class |
|---|---|---|---|
| 1 | measurement = actualization event | M_001 | interpretation (B) |
| 2 | disturbance = phase pinning | M_002 | interpretation (B) |
| 3 | feedback = pinned phase initial condition | M_003 | interpretation (B) |
| 4 | info per event = log₂(95) | M_004 | QM-standard bound (A) |
| 5 | information conserved (reveal+redistribute) | M_005 | QM-equivalent (A) |
| 6 | observer = epistemic recipient | M_006 | interpretation (B) |
| 7 | measurement chain classified | M_007 | classification |
| 8 | AT-P043 (log₂ 95 bound) | M_008/M_009 | **QM-equivalent — DOWNGRADED** (A) |
| 9 | AT-P042 (discrete tick) | M_008/M_009/M_010 | **structurally unique, observably B** (C-structure) |
| 10 | phase lattice N/gcd(N,k) | M_010 | derived structure (sub-tick observable) |
| 11 | coupling network, κ = 2√(ρ_Aρ_B) | NP_003–NP_007 | not physical (B) |
| 12 | synchronization absent | NP_005 | negative result |
| 13 | no hidden field | NP_007/NP_011 | negative result |
| 14 | no hidden extremum | NP_008/NP_009 | negative result |
| 15 | Network 2 not physical | NP_010/NP_011 | negative result |
| 16 | ω₁ = √91·(2π/N) | D_046 P4 | **uniquely-AT numeric** (C) |
| 17 | families = floor(log₂ span)+1 = 3 | D_046 P8 | **uniquely-AT numeric** (C) |
| 18 | O(2)-type doublet, not SU(2) | D_046 P1 | **uniquely-AT structure** (C) |
| 19 | v = 137·ln(span) = 254.37 GeV | D_046 P6 | AT-derived structure (C) |

---

## 2. Per-result test (A/B/C)

| Result | A) implied by QM | B) alt interpretation | C) genuinely new observable |
|---|---|---|---|
| measurement = event (M_001) | ✓ (measurement is standard) | ✓ | ✗ |
| phase pinning (M_002) | ✓ (projection) | ✓ | ✗ |
| feedback (M_003) | ✓ (state update) | ✓ | ✗ |
| log₂(95) bound (M_004/M_008) | **✓ (standard d-outcome bound)** | — | ✗ |
| info conservation (M_005) | ✓ (unitarity) | ✓ | ✗ |
| observer (M_006) | ✓ (psi-epistemic) | ✓ | ✗ |
| **AT-P042 discrete tick** | ✗ (QM has continuous time) | ✓ (equivalent at samples) | **structural only — sub-tick in-principle** |
| phase lattice | ✗ (QM continuum) | ✓ | sub-tick only |
| coupling network (NP_003–NP_011) | ✓ (Born interference) | ✓ | ✗ |
| **ω₁ = √91·(2π/N)** | **✗ — QM does not predict the fundamental frequency** | — | **✓ numeric** |
| **families = 3** | **✗ — QM does not derive the family count** | — | **✓ numeric (observed)** |
| **O(2) doublet** | **✗ — QM gauge sector is free** | — | **✓ structure** |
| **v = 137·ln(span)** | **✗ — QM does not derive the EW scale structure** | — | **✓ numeric (calibration)** |

---

## 3. Examination of the specific candidates

| Candidate | Survival |
|---|---|
| **discrete tick structure** | STRUCTURALLY unique (C) — observationally B at all tick-sampled times (M_010); sub-tick discriminator in-principle only |
| **phase lattice** | DERIVED structure — observable only via sub-tick resolution (in-principle) |
| **finite-state effects** | reproduced by a continuous model at samples (M_010) — no unique observable |
| **complete pairing** | mirror-symmetric doubling — QM-equivalent structure |
| **N=96 consequences** | the SPECTRUM IS the unique content: ω₁, span, family count, v-structure |
| **information conservation** | QM-equivalent (unitarity) |
| **observer coupling** | QM-equivalent (epistemic reading) |

**The measurement and coupling programs produce NO observationally-testable unique
prediction. The unique content lives in the N=96 SPECTRUM (D_046).**

---

## 4. Observable consequences that disappear if AT is replaced by QM

| Consequence | Disappears if AT → QM? |
|---|---|
| fundamental frequency ω₁ = √91·(2π/N) | **YES** — QM has no predicted fundamental |
| family count = floor(log₂ span)+1 = 3 | **YES** — QM leaves the family number free |
| O(2)-type doublet structure | **YES** — QM gauge structure is free |
| v = 137·ln(span) structure | **YES** — QM does not derive the EW scale relation |
| phase lattice discreteness | structural — observable only in-principle |

---

## 5. Falsification paths

| Prediction | Falsification |
|---|---|
| ω₁ = √91·(2π/N) | measure the fundamental excitation; it must equal √91·(2π/N)·(tick scale) |
| families = 3 | find a 4th fermion family (a distinct octave rung) |
| O(2) doublet | find a triplet structure with no mirror pair |
| v = 137·ln(span) | a scale relation inconsistent with 137·ln(span) |
| AT-P042 discrete tick | a continuous (non-lattice) phase at sub-tick resolution |

---

## 6. Ranking (impact + uniqueness + feasibility + testability, /20)

| Rank | Prediction | Impact | Uniqueness | Feasibility | Testability | **Total** |
|---|---|---|---|---|---|---|
| **1** | **ω₁ = √91·(2π/N)** | 4 | 5 | 3 | 4 | **16** |
| **1** | **families = floor(log₂ span)+1 = 3** | 5 | 4 | 4 | 3 | **16** |
| 3 | O(2)-type doublet (not SU(2)) | 3 | 4 | 3 | 3 | 13 |
| 3 | v = 137·ln(span) = 254.37 GeV | 4 | 3 | 3 | 3 | 13 |
| 5 | AT-P042 discrete tick | 4 | 3 | 2 | 2 | 11 |

---

## 7. Top-5 remaining AT predictions

1. **ω₁ = √91·(2π/N)** — the fundamental spectral frequency (uniquely-AT numeric).
2. **families = floor(log₂ span)+1 = 3** — the fermion family count (uniquely-AT numeric, observed).
3. **O(2)-type spectral doublet, not SU(2)** — the gauge-sector structure (uniquely-AT).
4. **v = 137·ln(span) = 254.37 GeV** — the electroweak-scale structure (AT-derived).
5. **AT-P042 discrete tick** — structurally unique, observationally in-principle.

---

## Theorem

> **Theorem (NP_012).** After removing all QM-equivalent interpretations, the V2.2
> measurement and coupling programs (M_001–M_010, NP_003–NP_011) contribute NO
> observationally-testable uniquely-AT prediction. Every M-series observable is
> QM-equivalent (CORRESPONDENCE): the event, pinning, feedback, log₂(95) bound
> (AT-P043, DOWNGRADED in M_009), conservation, and observer are all reproduced by
> standard QM (projection, unitarity, psi-epistemic readings). AT-P042 (discrete tick)
> is the SOLE structurally-unique measurement prediction, but it is observationally
> equivalent to continuous QM at every tick-sampled time (M_010) — its discriminator is
> sub-tick, in-principle only. The NP program (coupling, synchronization, network,
> field, extremum) yields negative results: coupling is not physical (NP_011),
> synchronization is absent (NP_005), no hidden field exists. THE SURVIVING
> UNIQUELY-AT PREDICTIONS are the N=96 SPECTRAL values: ω₁ = √91·(2π/N) (a fundamental
> frequency QM cannot predict), families = floor(log₂ span)+1 = 3 (a family count QM
> leaves free), the O(2)-type doublet (gauge structure QM leaves free), and
> v = 137·ln(span) = 254.37 GeV (EW-scale structure). THE FIRST uniquely-AT prediction
> is the fundamental spectral frequency ω₁ = √91·(2π/N) — numerically testable and
> falsifiable (a measured fundamental excitation must match √91·(2π/N)·(tick scale)).
> Ranking: ω₁ and the family count tie at 16/20; the O(2) doublet and v-structure at
> 13/20; AT-P042 (observationally in-principle) last at 11/20. Classification:
> measurement observables CORRESPONDENCE; AT-P042 PREDICTION (structural); ω₁, family
> count, O(2) doublet, v-structure PREDICTION (uniquely-AT); absent structures
> (field, sync, network-2) BOUNDARY. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Enumerate the inventory (Section 1). (2) Test each as A/B/C
> (Section 2). (3) Examine the candidates (Section 3). (4) Identify the
> disappear-if-replaced observables (Section 4). (5) Rank and falsify (Sections 5–6,
> verified numerically: ω₁=0.624, families=3, v=254.37). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → Spectrum (N=96)
 → Measurement (M-series) — mostly QM-equivalent
 → Coupling (NP-series) — not physical, no field
 → Prediction
    → M/NP observables: CORRESPONDENCE (QM-equivalent)
    → AT-P042: structural only (sub-tick in-principle)
    → ω₁ = √91·(2π/N): UNIQUELY-AT (numeric)
    → families = 3: UNIQUELY-AT (numeric)
 → Experiment
```

---

## 8. Falsification table

| Prediction | Falsification path | Status |
|---|---|---|
| ω₁ = √91·(2π/N) | measured fundamental ≠ √91·(2π/N)·scale | PENDING — testable |
| families = 3 | a 4th family discovered | CONSISTENT (observed) |
| O(2) doublet | a triplet with no mirror pair | PENDING |
| v = 137·ln(span) | scale relation deviates from 137·ln(span) | CONSISTENT (calibration) |
| AT-P042 discrete tick | continuous sub-tick phase | PENDING — in-principle |

---

## Classification

| Component | Status |
|---|---|
| measurement observables (M_001–M_010) | **CORRESPONDENCE** (QM-equivalent) |
| AT-P043 (log₂ 95 bound) | **CORRESPONDENCE** (downgraded, M_009) |
| AT-P042 (discrete tick) | **PREDICTION** (structural — in-principle observable) |
| ω₁, family count, O(2) doublet, v-structure | **PREDICTION** (uniquely-AT, from N=96 spectrum) |
| hidden field / sync / network-2 | **BOUNDARY** (absent) |

**The unique predictions of the theory live in the N=96 SPECTRUM, not the measurement
program. The first uniquely-AT prediction is the fundamental spectral frequency
ω₁ = √91·(2π/N). No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Tick-scale calibration (NP_012 OP1).** Fixing the tick period τ would turn
   ω₁ = √91·(2π/N) into an absolute-frequency prediction (currently a ratio structure).

---

## Next Steps

- **Registry note:** the uniquely-AT predictions are spectral (ω₁, family count, O(2)
  doublet, v-structure); the measurement program is QM-equivalent.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_012_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_012_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_012_PredictionInventory` | enumerate surviving AT-specific results | ✅ |
| `Y_NP_012_QMComparison` | test each as A/B/C | ✅ |
| `Y_NP_012_UniquenessFilter` | filter to C-only survivors | ✅ |
| `Y_NP_012_FalsificationCheck` | each survivor has a falsification path | ✅ |
| `Y_NP_012_Ranking` | 4-axis ranking (ω₁ & families top) | ✅ |
| `Y_NP_012_Run` | research report | ✅ |

**Conclusion:** The measurement and coupling programs contribute NO observationally-
testable unique prediction (all QM-equivalent or negative results). The surviving
uniquely-AT predictions are the N=96 spectral values — led by ω₁ = √91·(2π/N), the
first uniquely-AT prediction. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_012"`

---

## References

- ResearchY-M_001–M_010 (measurement program), NP_003–NP_011 (lever, coupling, sync,
  field, extremum, network programs), D_046 (eight new predictions P1–P8).
- AT-QG: QG216 (Born rule), QG234 (cosmology fractions).
