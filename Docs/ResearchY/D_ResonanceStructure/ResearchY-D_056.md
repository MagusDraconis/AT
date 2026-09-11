# ResearchY-D_056 — Multiplicity Distribution Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_056 (permanent)
**Title:** Multiplicity Distribution Audit
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `D_ResonanceStructure/ResearchY-D_056.md`
**Depends on:** D_055 (the rank budget and the anomaly), D_054 (the low-energy edge), D_052 (degeneracy axis, non-injectivity), D_050 (the collinear-input collapse), D_048 (the perturbation ensemble)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_056_Tests.cs` (6 tests)
**Shared machinery:** `AT.Tests/Shared/AdaptabilityAudit.cs`
**Prediction commit:** `fcd81f69` (prediction only — **no measurement code**)
**Measurement commit:** the commit following `fcd81f69`

---

## Question

**Is capacity controlled by the full multiplicity distribution** — max multiplicity, Gini(m),
Entropy(m), Herfindahl(m), largest-level share — and does that beat the near-gap density, the
degeneracy count and λ₂?

D_055 showed the mechanism is a **rank budget**, $\Delta A \le \sum_i \min(m_i-1, r)$, which is a
function of the multiplicity *distribution* rather than of its count. D_056 tests whether the
distribution therefore predicts capacity outright.

**Goal: beat near-gap (ρ 0.204), degeneracy count (ρ 0.815), λ₂ (ρ 0.214).**
**Metrics: Spearman, LOO RMSE, R².**

### Convention, stated because it matters

*m* is the multiplicity list of **every distinct level, singletons included**, so Σ*m* = N = 96
exactly. Dropping singletons would inflate every concentration statistic and would break the Σ = N
normalization that Gini, entropy and Herfindahl require.

### Blind protocol

| step | content | commit |
|---|---|---|
| **PHASE A** | distributions, coefficients (OLS on D_048/D_050's six published sources), decision rule and all predictions — for the required case set **and four new rings**. No measurement code. | `fcd81f69` |
| **PHASE B** | measurement and comparison | next commit |

The required case set's targets are already published, so for it this is a pre-registered replication;
the **four new rings** are the genuinely blind component. The four were chosen to vary the
distribution **shape** rather than its maximum:

| ring | construction | frozen expectation |
|---|---|---|
| P47-16 | ±1, ±16, ±47 (degree 6) | the dominant level should **dissolve** |
| P47-123 | ±1, ±2, ±3, ±47 (degree 8) | dominant level plus small offsets |
| H51123 | ±1, ±5, ±11, ±23 (degree 8) | no N/2 relationship anywhere |
| **P47-48** | ±1, ±47, ±48 (degree 6) | **the antinodal offset should move the dominant level to λ = 8** |

**A pre-registered outlier check** was frozen too: the same metrics recomputed with **Pair1-47
removed**, so a correlation resting on the single extreme ring cannot be mistaken for a law.

---

## Results

### 1. The distributions (PHASE A)

| ring | A₀ | max m | Gini(m) | Entropy(m) | Herfindahl(m) | largest share | pattern |
|---|---|---|---|---|---|---|---|
| D96 | 45 | 6 | 0.0801 | 3.7620 | 0.0250 | 6.2 % | 6×1, 5×1, 2×42, 1×1 |
| **Pair1-47** | 25 | **50** | **0.4992** | **2.2091** | **0.2810** | **52.1 %** | 50×1, 2×22, 1×2 |
| S96-123 | 45 | 6 | 0.0801 | 3.7620 | 0.0250 | 6.2 % | *(identical to D96)* |
| S96-135 | 45 | 10 | 0.1014 | 3.7180 | 0.0293 | 10.4 % | 10×1, 2×42, 1×2 |
| Ring48 | 45 | 6 | 0.0801 | 3.7620 | 0.0250 | 6.2 % | *(identical to D96)* |
| Decay96 | 49 | 2 | 0.0200 | 3.8856 | 0.0206 | 2.1 % | 2×47, 1×2 |
| Boost96 | 47 | 5 | 0.0408 | 3.8307 | 0.0224 | 5.2 % | 5×1, 2×45, 1×1 |
| *new* P47-16 | 25 | 34 | 0.4675 | 2.4960 | 0.1664 | 35.4 % | 34×1, 17×1, 2×22, 1×1 |
| *new* P47-123 | 48 | 4 | 0.0404 | 3.8568 | 0.0215 | 4.2 % | 4×1, 2×45, 1×2 |
| *new* H51123 | 41 | 6 | 0.1611 | 3.6437 | 0.0284 | 6.2 % | 6×1, 4×6, 2×32, 1×2 |
| *new* **P47-48** | 25 | **49** | **0.4800** | **2.2458** | **0.2706** | **51.0 %** | 49×1, 2×23, 1×1 |

**Two things were visible before any simulation.** D96, S96-123 and Ring48 share one distribution
*exactly*, so no functional of it can separate them. And **P47-48's distribution is nearly
indistinguishable from Pair1-47's** (49 vs 50, Gini 0.4800 vs 0.4992, share 51.0 % vs 52.1 %) —
which yields the audit's sharpest forecast.

### 2. The five statistics are one axis

| | max multiplicity | Gini | Entropy | Herfindahl | largest share |
|---|---|---|---|---|---|
| capacity ρ | **−0.964** | **−0.964** | **+0.964** | **−0.964** | **−0.964** |
| recovery ρ | −0.037 | −0.037 | +0.037 | −0.037 | −0.037 |

Max multiplicity, Gini and the largest share are mutually monotone and give **identical ρ to three
decimals**; Entropy and Herfindahl are the exact mirror. **"The full multiplicity distribution" is, as
a predictor space, ONE number** — the same collapse D_050 found among its four spectral inputs.

### 3. Metrics on the required case set

p is the **exact two-sided permutation p-value** (7! = 5040 permutations enumerated).

**CAPACITY**

| input | Spearman ρ | exact p | LOO RMSE | refit R² | frozen mean \|error\| |
|---|---|---|---|---|---|
| max multiplicity | −0.964 | **0.0024** | 0.07781 | 0.995 | 0.29922 |
| Gini(m) | −0.964 | 0.0024 | 0.12010 | 0.987 | 0.34824 |
| Entropy(m) | +0.964 | 0.0024 | **0.04174** | 0.995 | 0.35382 |
| Herfindahl(m) | −0.964 | 0.0024 | 0.48612 | 0.994 | 0.29937 |
| largest share | −0.964 | 0.0024 | 0.07781 | 0.995 | 0.29922 |
| **rank ceiling (D_055)** | +0.906 | 0.0095 | 0.04892 | **0.999** | **0.19963** |

**Baselines recomputed on the same set**

| baseline | ρ | exact p | LOO RMSE | R² |
|---|---|---|---|---|
| near-gap | 0.204 | 0.8571 | **n/a (constant)** | 0.036 |
| degeneracy count | **0.964** | **0.0024** | 0.12029 | 0.987 |
| λ₂ | 0.536 | 0.2357 | 0.27469 | 0.078 |

**Head-to-head:** the distribution statistics tie the degeneracy count on ρ **exactly** (0.964, same
exact p = 0.0024) and beat λ₂ and near-gap. The **p = 0.0024 is the first in the D group to survive
multiple-comparison correction** (α/5 = 0.01 for five declared inputs). The frozen rule
(|ρ| > 0.815 **and** LOO < 0.03304) is not met — **but neither is it met by the degeneracy count
itself on this case set (0.12029)**, because the 0.03304 bar was inherited from D_052's *different*
case set. Relative comparison on the same set is therefore used.

**RECOVERY:** the distribution says nothing — every statistic gives ρ = 0.037, exact p = 0.9786 —
while λ₂ reaches ρ = 0.821 (p = 0.0341).

### 4. The pre-registered outlier check

| input | ρ (7) | ρ (6) | Δρ | R² (7) | **R² (6)** | LOO (6) |
|---|---|---|---|---|---|---|
| max multiplicity | −0.964 | −0.941 | 0.023 | 0.995 | 0.758 | 0.02104 |
| Gini(m) | −0.964 | −0.941 | 0.023 | 0.987 | 0.540 | 0.02346 |
| Entropy(m) | +0.964 | +0.941 | −0.023 | 0.995 | 0.540 | 0.02499 |
| Herfindahl(m) | −0.964 | −0.941 | 0.023 | 0.994 | **0.784** | 0.01794 |
| largest share | −0.964 | −0.941 | 0.023 | 0.995 | 0.758 | 0.02104 |
| **rank ceiling (D_055)** | +0.906 | +0.845 | −0.061 | 0.999 | **0.978** | **0.00511** |
| *degeneracy count* | 0.964 | 0.941 | −0.023 | 0.987 | **0.528** | — |
| *λ₂* | 0.536 | 0.257 | −0.279 | 0.078 | 0.125 | — |
| *near-gap* | 0.204 | 0.131 | −0.073 | 0.036 | 0.058 | — |

**The check passed.** Removing Pair1-47 moves ρ only from 0.964 to 0.941 — the correlation does **not**
rest on the extreme ring. On the six healthy rings the distribution statistics (R² up to **0.784**)
**beat the degeneracy count (0.528)**, and the derived rank ceiling reaches **R² = 0.978, LOO 0.00511**.

**Non-injectivity inherited.** D96, S96-123 and Ring48 share one distribution value whose measured
capacities spread 0.02320 — 4 % of the whole family span (the denominator now includes Pair1-47). No
functional of the distribution can be sufficient, on the very same triple D_052 flagged.

**The near-gap baseline is structurally unusable on rings:** six of seven rings sit at near-gap 2 with
Ring48 alone at 8, so its input becomes **constant** as soon as one ring is held out — its LOO is
*undefined*, not merely poor.

### 5. The blind test (PHASE B)

| ring | observed capacity | observed recovery | max m | rank ceiling | predicted cap (ceiling) |
|---|---|---|---|---|---|
| P47-16 | 0.69765 | 0.96007 | 34 | 0.6845 | 0.52076 |
| P47-123 | 0.99618 | 0.96974 | 4 | 1.0000 | 0.76034 |
| H51123 | 0.97455 | 0.97137 | 6 | 0.9964 | 0.75758 |
| **P47-48** | **0.58920** | 0.94551 | **49** | 0.5718 | 0.43519 |

**The sharp forecast was confirmed exactly.** P47-48 was built by adding the exactly-antipodal ±48
offset. For odd *k* the ±48 term is $2(1-\cos(\pi k)) = 4$, so the odd-mode sum becomes 4 + 4 = 8:

| ring | dominant level | multiplicity | λ₂ | near-gap(2λ₂) | capacity |
|---|---|---|---|---|---|
| Pair1-47 | λ = **4.000000** | 50 | 0.034221 | 2 | 0.42089 |
| **P47-48** | λ = **8.000000** | **49** | **0.034221** | **2** | **0.58920** |

The level **survived, moved from λ = 4 to λ = 8**, and the ring collapsed — the only blind ring to do
so. Its λ₂ is **identical** to Pair1-47's and its near-gap is 2, so the near-gap predictor cannot see
it at all.

**Rank agreement on the four blind rings — perfect for every input** (ρ = ±1.000; with n = 4 and
4! = 24 permutations the smallest attainable two-sided p is 2/24 = 0.0833):

| input | ρ (capacity) | LOO RMSE | refit R² |
|---|---|---|---|
| max multiplicity / Gini / Entropy / Herfindahl / largest share | **±1.000** | 0.0247 … 0.0953 | 0.931 … 0.996 |
| **rank ceiling (D_055)** | **+1.000** | **0.01302** | **0.999** |
| *degeneracy count* | 0.949 | — | — |
| *λ₂* | 0.400 | — | — |
| *near-gap* | 0.258 | — | — |

---

## Classification

### DERIVED

* **The five distribution statistics are one axis, not five.** Max multiplicity, Gini and the largest
  share are mutually monotone with identical ρ; entropy and Herfindahl are its exact mirror. The
  "full distribution" is one number — D_050's collinear-input collapse reproduced on a new input family.
* **The mechanism predictor wins.** D_055's rank ceiling is the only input derived from the
  distribution *by argument rather than fitted from it*, and it leads on every fidelity measure:
  R² = **0.978** on the six healthy rings (against the degeneracy count's 0.528), LOO **0.00511**
  there, R² = **0.999** and ρ = 1.000 on the blind rings, and the smallest frozen-coefficient error.
* **The non-injectivity is inherited.** D96, S96-123 and Ring48 share one distribution exactly, so
  every distribution input assigns them one number while their capacities spread 0.02320 = 4 % of the
  family span.
* **The near-gap baseline is structurally unusable on rings** — its input becomes constant when any
  ring is held out, so its LOO is undefined. It is not a competitor that can be beaten or lose here.
* **The λ = 8 clause, derived in advance and confirmed.** The ±48 offset's odd-mode contribution is
  exactly 4, so the dominant level must move from λ = 4 to λ = 8 and survive; measured λ = 8.000000
  with multiplicity 49.

### EMERGENT

* **The blind forecast succeeded, including its sharpest clause.** All four blind orderings are
  correct: P47-48 collapsed (0.58920) as predicted, P47-16 partially (0.69765, dominant level
  dissolved to multiplicity 34), P47-123 (0.99618) and H51123 (0.97455) healthy.
* **The outlier check passed** — ρ moves only 0.964 → 0.941 without Pair1-47, so the correlation is
  not an artifact of the extreme ring; on the healthy six the distribution beats the degeneracy count.
* **The frozen-coefficient predictions are poor for every input** (mean errors 0.19 … 0.49) — D_051's
  lesson recurring: coefficients fitted on D_048/D_050's *non-ring* sources extrapolate badly onto
  rings. The rank ceiling is least bad (0.19963 required, 0.19593 blind).

### REFUTED

* **"Capacity is controlled by the FULL multiplicity distribution, beyond the degeneracy count."**
  REFUTED as an *ordering* claim: on the required set the five statistics and the degeneracy count give
  the **same ρ (0.964) with the identical exact p (0.0024)**, so the distribution adds no rank
  information the count does not already carry. It does add value fidelity (R² 0.995 vs 0.987; 0.784 vs
  0.528 on the healthy six) — a refinement, not a new instrument.
* **"The multiplicity distribution controls robustness as well as adaptability."** REFUTED: ρ = 0.037,
  exact p = 0.9786 on recovery — indistinguishable from no association.
* **"Each distribution statistic is an independent input."** REFUTED: they collapse onto one axis with
  two signs.
* **"Beating the near-gap baseline demonstrates a superior predictor."** REFUTED as a claim about
  near-gap on rings: its LOO is undefined there, so it is not a competitor.

---

## Verdict

**DERIVED** — the five-statistics collapse, the rank-budget ceiling's lead, the inherited
non-injectivity, the structural unusability of the near-gap baseline on rings, and the λ = 8 clause.

**EMERGENT** — the blind confirmation (four of four orderings, and the collapse of P47-48 at λ = 8),
the outlier check passing, and the poor source-fitted coefficients.

**REFUTED** — distribution control *beyond* the degeneracy count on ordering; any distribution control
of recovery; the independence of the five inputs; and the near-gap baseline as a meaningful competitor
on this family.

**Summary.** The multiplicity distribution predicts ring **capacity** strongly and predicts **recovery**
not at all. Its five specified statistics are one axis; on the required set that axis only **ties** the
degeneracy count on ordering, though it beats it on R² and on the healthy subset, and it reaches
perfect ordering on four brand-new rings whose collapse was predicted in advance from the distribution
alone. The **winner on every fidelity measure is the DERIVED rank-budget ceiling of D_055** — an
argument, not a fit — and its sharpest clause, the λ = 8 level in P47-48, was confirmed exactly.

No canonical AT claim, value, equation or registry entry is changed; the D_040 `ClassificationRegistry`
is untouched. No new simulation primitive is added: the twelve previously audited rings come from the
shared cache and only the four new rings are measured here.

---

## References

* **D_055** — Pair1-47 anomaly: the rank budget $\Delta A \le \sum_i \min(m_i-1, r)$ and the dominant-level cause.
* **D_054** — Low-energy spectral edge: the anomaly and the blind-ring precedent.
* **D_052** — Degeneracy axis: the non-injectivity inherited here.
* **D_050** — Spectral predictability: bounds, and the collinear-input collapse.
* **D_048** — The perturbation ensemble.
* **D_047** — Degeneracy-lock theorem.
* **D_040** — `ClassificationRegistry` (canonical classification guard; untouched).
* **Test suite** — `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_056_Tests.cs` (6 tests, all passing).
