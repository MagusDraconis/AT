# ResearchY-D_052 — Degeneracy Axis Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_052 (permanent)
**Title:** Degeneracy Axis Audit
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `D_ResonanceStructure/ResearchY-D_052.md`
**Depends on:** D_051 (blind prediction; the ring family and the near-gap baseline), D_050 (spectral predictability; the fitted relations), D_049 (family dependence of the frontier), D_047 (locked entropy $\Delta E_{\text{lock}}$), T_014 (near-gap counting)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_052_Tests.cs` (5 tests)
**Shared machinery:** `AT.Tests/Shared/AdaptabilityAudit.cs`
**Prediction commit:** `e5f9354f` (the test file contains the prediction only — **no measurement code**)
**Measurement commit:** the commit following `e5f9354f`

---

## Question

**Does the degeneracy axis predict adaptability *within* the ring family?**

D_051 ended with a concrete hypothesis: the near-gap density is a **constant** on rings
(near-gap = 2 for every unit-offset ring whose low-$k$ spectrum follows the $k^2$ law), while the
degeneracy count **varies** (38 … 47) — so the collinear pair's *other* member should be the better
ring-family predictor. This audit tests that hypothesis under the same blind protocol.

The family is the **seven 96-node rings**: the canonical D96 plus D_051's six blind rings.

| ring | λ₂ | near-gap | degeneracy | entropy | ΔE_lock | max *m* | multiplicity spectrum |
|---|---|---|---|---|---|---|---|
| D96 | 0.386351 | 2 | 44 | 3.7620 | 0.8023 | 6 | 1×6, 1×5, 42×2, 1×1 |
| S96-123 | 0.059822 | 2 | 44 | 3.7620 | 0.8023 | 6 | 1×6, 1×5, 42×2, 1×1 |
| S96-135 | 0.148851 | 2 | 43 | 3.7180 | 0.8464 | 10 | 1×10, 42×2, 2×1 |
| D96-24 | 2.693016 | 2 | 38 | 3.4591 | 1.1052 | 12 | 1×12, 1×11, 36×2, 1×1 |
| Decay96 | 0.089286 | 2 | 47 | 3.8856 | 0.6787 | 2 | 47×2, 2×1 |
| Boost96 | 1.870529 | 2 | 46 | 3.8307 | 0.7337 | 5 | 1×5, 45×2, 1×1 |
| Ring48 | 5.404246 | 8 | 44 | 3.7620 | 0.8023 | 6 | 1×6, 1×5, 42×2, 1×1 |

---

## Method — the blind protocol

Same enforcement as D_051: **commit order in git**.

| step | content | commit |
|---|---|---|
| **PHASE A** | the four candidate predictors are read from each ring's spectrum; coefficients are fitted on D_048/D_050's **already-published** six cases; the prediction is printed. The test file never constructs the perturbation ensemble. | `e5f9354f` |
| **PHASE B** | the seven rings are measured under the shared ensemble and compared against the frozen numbers | next commit |

The blind points are the **six rings' measured capacity and recovery**. D96's values are already
published (0.9902 / 0.9659) and act as a reproducibility check. Phase A's near-gap rows reproduce
D_050's published fits (−0.00814 x + 0.87153, R² = 0.759; +0.000284 x + 0.95582, R² = 0.525)
exactly — the integrity check that no coefficient was re-tuned.

### The four candidate predictors

| tag | input | frozen slope / intercept, capacity | R² on the D_050 cases |
|---|---|---|---|
| **NG** | near-gap density at $k = 2$ — D_050's pick, the baseline to beat | −0.008136 / 0.871526 | 0.759 |
| **DEG** | degeneracy count (levels with multiplicity > 1) | +0.012234 / 0.370622 | 0.529 |
| **ENT** | degeneracy entropy (Shannon entropy of the multiplicity distribution) | +0.007055 / 0.568361 | 0.001 |
| **LOCK** | D_047's locked entropy $\Delta E_{\text{lock}} = \tfrac{1}{N}\sum_{m>1} m\ln m$ (the "multiplicity spectrum") | −0.007055 / 0.600565 | 0.001 |

Success criterion: **the degeneracy-based predictor beats the near-gap predictor.** Metrics:
R², Spearman ρ, leave-one-out RMSE.

---

## Results

### 1. Resolvability, visible at prediction time

| axis | distinct values across the seven rings | span |
|---|---|---|
| near-gap density | **2** (six rings at 2, Ring48 at 8) | 6 |
| degeneracy count | **5** (38, 43, 44, 46, 47) | 9 |
| entropy | 3.4591 … 3.8856 | 0.427 |
| ΔE_lock | 0.6787 … 1.1052 | 0.427 |

The degeneracy axis does resolve the family better than near-gap — **but it is not injective
either**: D96, S96-123 and Ring48 share *one* multiplicity spectrum.

### 2. The frozen prediction (PHASE A)

| predictor | predicted capacity ranking |
|---|---|
| NG | D96 > S96-123 > S96-135 > D96-24 > Decay96 > Boost96 > Ring48 |
| DEG | Decay96 > Boost96 > D96 = S96-123 = Ring48 > S96-135 > D96-24 |
| ENT | *(identical order to DEG)* |
| LOCK | *(identical order to DEG)* |

| predictor | predicted recovery ranking |
|---|---|
| NG | Ring48 > D96 > S96-123 > S96-135 > D96-24 > Decay96 > Boost96 |
| DEG | D96-24 > S96-135 > D96 = S96-123 = Ring48 > Boost96 > Decay96 |

### 3. The measurement (PHASE B)

| ring | pred cap (NG) | pred cap (DEG) | **obs cap** | \|Δ\|NG | \|Δ\|DEG | pred rec (DEG) | **obs rec** |
|---|---|---|---|---|---|---|---|
| D96 *(check)* | 0.85525 | 0.90894 | **0.99020** | 0.1350 | 0.0813 | 0.95982 | 0.96592 |
| S96-123 | 0.85525 | 0.90894 | **0.96928** | 0.1140 | 0.0603 | 0.95982 | 0.96044 |
| S96-135 | 0.85525 | 0.89670 | **0.93693** | 0.0817 | 0.0402 | 0.96005 | 0.96643 |
| D96-24 | 0.85525 | 0.83553 | **0.97193** | 0.1167 | 0.1364 | 0.96117 | 0.96937 |
| Decay96 | 0.85525 | 0.94564 | **1.00000** | 0.1447 | 0.0544 | 0.95915 | 0.91977 |
| Boost96 | 0.85525 | 0.93341 | **0.99524** | 0.1400 | 0.0618 | 0.95937 | 0.97537 |
| Ring48 | 0.80644 | 0.90894 | **0.99248** | 0.1860 | 0.0835 | 0.95982 | 0.97244 |

Observed spread: capacity 0.93693 … 1.00000 (span 0.06307); recovery 0.91977 … 0.97537 (span 0.05560).

### 4. Metrics — R², Spearman, LOO RMSE

**CAPACITY**

| input | Spearman ρ | LOO RMSE | refit R² | frozen mean \|error\| | R² on D_050 cases |
|---|---|---|---|---|---|
| NG | 0.204 | **0.02437** | 0.068 | 0.13116 | 0.759 |
| **DEG** | **0.815** | 0.03304 | **0.226** | **0.07399** | 0.529 |
| ENT | 0.815 | 0.03903 | 0.187 | 0.38469 | 0.001 |
| LOCK | −0.815 | 0.03903 | 0.187 | 0.38469 | 0.001 |

**RECOVERY**

| input | Spearman ρ | LOO RMSE | refit R² | frozen mean \|error\| | R² on D_050 cases |
|---|---|---|---|---|---|
| **NG** | **0.408** | **0.02095** | 0.066 | 0.01522 | 0.525 |
| DEG | −0.222 | 0.02863 | **0.210** | **0.01275** | 0.102 |
| ENT | −0.222 | 0.03315 | 0.195 | 0.01051 | 0.003 |
| LOCK | 0.222 | 0.03315 | 0.195 | 0.01051 | 0.003 |

**Success criterion, taken literally**

| comparison | result |
|---|---|
| capacity ranking | DEG ρ = **0.815** vs NG 0.204 → **DEG WINS (4×)** |
| capacity absolute | DEG **0.07399** vs NG 0.13116 → **DEG WINS (1.8×)** |
| capacity resolution | DEG R² **0.226** vs NG 0.068 → **DEG WINS** |
| recovery ranking | DEG ρ = **−0.222** vs NG 0.408 → **NG WINS** |
| recovery absolute | DEG **0.01275** vs NG 0.01522 → DEG WINS (marginal) |
| LOO RMSE (both) | **NG WINS** — the degeneracy fit is unstable under refit while NG is nearly constant, i.e. close to predicting the mean |

---

## Classification

### DERIVED

* **The degeneracy axis is not injective on rings — a proof of insufficiency, not a measurement.**
  **D96, S96-123 and Ring48 have the identical multiplicity spectrum 1×6, 1×5, 42×2, 1×1**, hence
  identical degeneracy count (44), entropy (3.7620) and ΔE_lock (0.8023). Every functional of the
  multiplicity spectrum assigns those three rings *one and the same number, by construction*. Yet
  their spectra are far apart (λ₂ = 0.386351, 0.059822, 5.404246 — a factor of 90), and their
  **measured** capacities spread **0.02320 = 37 % of the whole family span** (recovery 0.01200 =
  22 %). *No degeneracy input can ever reduce that error.* The axis is a **coarse invariant**:
  strictly better than near-gap (5 groups vs 2), not sufficient.
* **Near-gap = 2 is forced on rings** (D_051): λ₂/λ₁ = 4 and λ₃/λ₂ = 9/4 > 2, so only the k = ±1
  doublet lies within 2λ₂ whatever the weights. Six of the seven rings sit at that minimum — which is
  exactly why "beating" the near-gap predictor is a **low bar** for any input that varies at all.
* **The bounds survive.** Every prediction of all four predictors × 7 rings × 2 targets lies inside
  [0, 1]; no canonical claim is touched.

### EMERGENT

* **The degeneracy count *is* the better capacity predictor within the ring family** — ρ = 0.815 vs
  0.204 (4×), refit R² 0.226 vs 0.068, frozen mean |error| 0.074 vs 0.131 — and **all seven rings
  adapt strongly** (0.93693 … 1.00000) while differing by a factor of 4 in degree and 90 in λ₂. Its
  sign is **positive**, matching D_050's cross-ensemble +0.886: more degenerate levels ⇒ more
  adaptable, and this now holds *within* one topological family, not only across unrelated graphs.
* **Recovery goes the other way:** DEG ρ = −0.222 vs NG +0.408. The sign is again negative, matching
  D_050's −0.543, but weak. The predictor D_050 chose is not the wrong axis for recovery — it is
  simply not available on rings the way D_050's ensemble used it.
* **ΔE_lock adds nothing.** The multiplicity entropy and ΔE_lock are exactly anti-correlated on this
  family (fitted slopes are exact negatives) and give byte-identical rankings, including identical
  errors. D_047's derived scalar carries the same information as the plain multiplicity entropy here.

### REFUTED

* **"The degeneracy count predicts adaptability within ring families."** REFUTED as a *sufficient*
  claim: three rings share one degeneracy value while their measured capacities differ by 0.0232
  (**37 % of the family span**) — a gap no functional of the multiplicity spectrum can resolve. It
  predicts the **order** of capacity, not the value.
* **"The degeneracy axis replaces the near-gap predictor."** REFUTED: it replaces it for **capacity
  only**. For recovery the near-gap density orders better (ρ 0.408 vs −0.222), and for stability under
  refit (LOO RMSE) near-gap is better for **both** targets.
* **"Adding degeneracy entropy and the locked entropy strengthens the prediction."** REFUTED: they
  are near-constant across the family and their frozen-coefficient mean error is **5×** the degeneracy
  count's (0.38469 vs 0.07399) — a second instance of D_050's overfitting lesson.

---

## Verdict

**DERIVED** — the degeneracy axis is provably non-injective on rings (three rings share one
multiplicity spectrum while their measured capacities span 37 % of the family range); near-gap = 2 is
forced on rings, making the baseline comparison a low bar; the bounds survive.

**EMERGENT** — the degeneracy count is the better *capacity* predictor within the family (ρ 0.815 vs
0.204, absolute error 1.8× lower), with the same sign as D_050's cross-ensemble value; all seven rings
adapt strongly; ΔE_lock is informationally identical to the multiplicity entropy.

**REFUTED** — the degeneracy count as a *sufficient* within-family predictor; the claim that it
replaces the near-gap predictor (recovery goes the other way); and the value of adding degeneracy
entropy / locked entropy.

**Success criterion — "beat the D_051 near-gap predictor": MET FOR CAPACITY, NOT MET FOR RECOVERY.**

**Consequence for the D group.** The degeneracy axis is a real but **coarse family-level
discriminator**: it orders ring adaptability where D_050's predictor cannot, and it takes its sign
from the same structural fact across ensembles. What remains open is not a better *spectral* scalar —
the multiplicity spectrum is the whole spectral-degeneracy data and it is provably not injective —
but a **non-spectral** input: the perturbation-family profile D_049 showed to move the frontier, which
D_050 deliberately withheld and D_051 showed cannot be replaced by the spectrum.

No canonical AT claim, value, equation or registry entry is changed; the D_040
`ClassificationRegistry` is untouched; D_050's and D_051's classifications are reaffirmed.

---

## References

* **D_051** — Blind prediction audit: the ring family, the near-gap constant, the failed prospective test.
* **D_050** — Spectral predictability: the frozen relations and the minimal predictor set.
* **D_049** — Adaptability–robustness frontier: family dependence, the reason for the open question above.
* **D_047** — Degeneracy-lock theorem: $\Delta E_{\text{lock}} = \tfrac{1}{N}\sum_{m>1} m\ln m$.
* **T_014** — Near-gap counting convention.
* **D_040** — `ClassificationRegistry` (canonical classification guard; untouched).
* **Shared machinery** — `AT.Tests/Shared/AdaptabilityAudit.cs` (`BlindRings`, `Ring`, `RingAdjacency`, `RingFamilyNames`, `LockedEntropy`).
* **Test suite** — `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_052_Tests.cs` (5 tests, all passing).
