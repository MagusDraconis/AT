# ResearchY-D_054 — Low-Energy Spectral Edge Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_054 (permanent)
**Title:** Low-Energy Spectral Edge Audit
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `D_ResonanceStructure/ResearchY-D_054.md`
**Depends on:** D_053, D_052 (the baselines to beat), D_051 (ring family), D_050 (bounds, the minimal-set lesson), D_048, D_047
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_054_Tests.cs` (6 tests)
**Shared machinery:** `AT.Tests/Shared/AdaptabilityAudit.cs`
**Prediction commit:** `ae09d917` (prediction only — **no measurement code**)
**Measurement commit:** the commit following `ae09d917`

---

## Question

**Is capacity controlled by the shape of the low-energy spectral edge?**

D_052's degeneracy count beat D_050's near-gap density for ring capacity, and D_053 showed the ring
factor dominates the family factor. D_054 asks whether the *edge geometry* — λ₂, λ₃, λ₄, λ₅ and the
shape ratios λ₃/λ₂, λ₄/λ₂, λ₅/λ₂ — is a stronger control than any of them.

**Goal: beat the near-gap, degeneracy and multiplicity-spectrum predictors.**

### The frozen baselines (D_052, published on this same case set)

| predictor | Spearman ρ | refit R² | LOO RMSE | frozen mean \|error\| |
|---|---|---|---|---|
| degeneracy count | **0.815** | 0.226 | 0.03304 | 0.07399 |
| near-gap density | 0.204 | 0.068 | 0.02437 | 0.13116 |
| multiplicity entropy / ΔE_lock | 0.815 | 0.187 | 0.03903 | 0.38469 |

### Decision rule, frozen before any metric was computed

An edge-shape predictor **beats the field** iff **both** hold on the required case set:
**(i)** Spearman ρ > 0.815, and **(ii)** within-family LOO RMSE < 0.03304. Corroboration is then
required on a held-out set.

### Blind protocol — stated honestly

| step | content | commit |
|---|---|---|
| **PHASE A** | edge geometry of every ring, coefficient table, and the predicted capacity and recovery for the required case set **and** for six new edge-shape rings. No measurement code. | `ae09d917` |
| **PHASE B** | measurement and comparison against the frozen numbers | next commit |

The required case set is exactly the seven rings whose targets **D_051–D_053 already published**, so
for them this audit is a **pre-registered replication**, not a blind test. To give the protocol a
genuinely blind component, **six new edge-shape rings** were declared in `ae09d917` and measured for
the first time in PHASE B. All coefficients are OLS fits on D_048/D_050's six already-published cases.

---

## Results

### 1. The edge geometry — and it settles most of the question before any simulation

| ring | λ₂ | λ₃ | λ₄ | λ₅ | r3 | r4 | r5 |
|---|---|---|---|---|---|---|---|
| D96 | 0.386351 | 0.386351 | 1.504529 | 1.504529 | 1.000000 | 3.894202 | 3.894202 |
| S96-123 | 0.059822 | 0.059822 | 0.237500 | 0.237500 | 1.000000 | 3.970113 | 3.970113 |
| S96-135 | 0.148851 | 0.148851 | 0.582645 | 0.582645 | 1.000000 | 3.914271 | 3.914271 |
| D96-24 | 2.693016 | 2.693016 | 9.742948 | 9.742948 | 1.000000 | 3.617858 | 3.617858 |
| Decay96 | 0.089286 | 0.089286 | 0.349208 | 0.349208 | 1.000000 | 3.911139 | 3.911139 |
| Boost96 | 1.870529 | 1.870529 | 7.263112 | 7.263112 | 1.000000 | 3.882919 | 3.882919 |
| Ring48 | 5.404246 | 5.404246 | 5.504529 | 5.504529 | 1.000000 | 1.018556 | 1.018556 |
| E1 | 0.004282 | 0.004282 | 0.017110 | 0.017110 | 1.000000 | 3.995718 | 3.995718 |
| E1-16-32 | 0.152241 | 0.152241 | 0.585786 | 0.585786 | 1.000000 | 3.847759 | 3.847759 |
| **G1248** | **4.000000** | **4.357490** | **4.357490** | **6.000000** | **1.089372** | **1.089372** | **1.500000** |
| Wscale | 0.017006 | 0.017006 | 0.066519 | 0.066519 | 1.000000 | 3.911582 | 3.911582 |
| Two1-12 | 0.267949 | 0.267949 | 1.000000 | 1.000000 | 1.000000 | 3.732051 | 3.732051 |
| Pair1-47 | 0.034221 | 0.034221 | 0.136297 | 0.136297 | 1.000000 | 3.982890 | 3.982890 |

**The four inputs collapse to one.** On a circulant ring the eigenvalue at wavevector k equals the one
at N − k, so the bottom of the spectrum is a **doublet**: **λ₂ = λ₃ and λ₄ = λ₅ in 12 of the 13 rings**.
Hence **r3 = λ₃/λ₂ is exactly 1** (only two distinct values in the whole family: 1.000000 and 1.089372,
the latter only on G1248) and **r5 ≡ r4**. Only **one** scale-free number survives: **r4 = λ₄/λ₂**, the
second folded level over the first, which is **4.000000 for a pure parabolic edge λ_k ∝ k²**. It
measures **edge bending**, running 3.995718 (E1, barely bent) → 3.894202 (D96) → 3.617858 (D96-24) →
1.018556 (Ring48, whose ±48 offset flips the sign of cos almost every step).

### 2. The derived bounds disqualify an input family

Of **182** cells (7 inputs × 13 rings × 2 targets):

| input | cells outside [0,1] | worst |
|---|---|---|
| λ₂, λ₃, λ₄, λ₅ | **0** | — |
| r3 | 1 | G1248 capacity = **−0.49307** |
| r4 | 6 | E1 capacity = 1.09533 |
| r5 | 6 | E1 capacity = 1.09112 |

The pattern is diagnostic and inverts the naive expectation: λ₂ … λ₅ never leave the range because the
source set spans λ₂ from 0.386 to 96 — a far wider envelope than any ring — so those lines are shallow;
the ratios **do** leave it because on the source set they are pulled toward the non-ring cases (K96
gives r4 = r5 = 1 exactly). **The input that is admissible in linear form is the one that does not
resolve rings, and the shape descriptor that does resolve rings is inadmissible in linear form.**

### 3. Metrics on the required case set

p is the **exact two-sided permutation p-value** (all 7! = 5040 rank permutations enumerated).

**CAPACITY**

| input | Spearman ρ | exact p | LOO RMSE | refit R² | frozen mean \|error\| | bounds viol. |
|---|---|---|---|---|---|---|
| λ₂ | **0.214** | 0.6615 | 0.02487 | 0.089 | 0.29402 | 0 |
| λ₃ | 0.214 | 0.6615 | 0.02487 | 0.089 | 0.29176 | 0 |
| λ₄ | 0.179 | 0.7131 | 0.02847 | 0.046 | 0.29942 | 0 |
| λ₅ | 0.179 | 0.7131 | 0.02847 | 0.046 | 0.29800 | 0 |
| r3 | 0.000 | 1.0000 | 0.02415 | 0.529 | 0.27532 | 0 |
| r4 | **−0.429** | 0.3536 | 0.02551 | 0.066 | 0.16005 | 6 |
| r5 | −0.429 | 0.3536 | 0.02551 | 0.066 | 0.15663 | 6 |

**RECOVERY**

| input | Spearman ρ | exact p | LOO RMSE | refit R² | frozen mean \|error\| |
|---|---|---|---|---|---|
| λ₂, λ₃, λ₄, λ₅ | **0.821** | **0.0341** | 0.02220 / 0.02220 / **0.02069** / 0.02069 | 0.218 / 0.218 / 0.284 / 0.284 | 0.01264 … 0.01230 |
| r3 | 0.000 | 1.0000 | n/a (constant) | 0.044 | 0.01145 |
| r4, r5 | −0.714 | 0.0881 | 0.04626 | 0.080 | 0.01109 |

**Baselines recomputed on this set** (so the comparison is apples-to-apples): near-gap capacity
ρ = 0.204 (p = 0.8571, LOO 0.02437), degeneracy capacity ρ = **0.815** (p = 0.0310, LOO 0.03304);
near-gap recovery ρ = 0.408 (LOO 0.02095 → **undefined under leave-one-out**: its entire signal is the
single ring at near-gap 8, so dropping Ring48 makes the input constant), degeneracy recovery
ρ = −0.222.

**Head-to-head — the frozen rule (ρ > 0.815 AND LOO < 0.03304):**

| target | best edge-shape input | ρ | LOO | beats? |
|---|---|---|---|---|
| capacity | λ₂ | 0.214 | 0.02487 | **NO** (fails ρ by 4×) |
| recovery | λ₂ | **0.821** | 0.02220 | **YES** |

**Per-target field:** capacity — λ₂ "beats on LOO only"; recovery — λ₂ **beats on both**
(ρ 0.821 vs 0.408, LOO 0.02220 vs 0.02863).

**Nothing survives the Bonferroni correction** for seven declared inputs (α/7 = 0.00714): every
capacity p ≥ 0.3536 and the recovery p = 0.0341.

### 4. The held-out set — the genuinely blind component

| ring | description | capacity | recovery | excluded |
|---|---|---|---|---|
| E1 | ±1 only (degree 2) — the pure parabolic edge | 0.98598 | 0.96257 | 16 |
| E1-16-32 | ±1, ±16, ±32 (degree 6) | 0.98440 | 0.97067 | 0 |
| G1248 | geometric ±1…±32 (degree 12) | 0.99787 | 0.97354 | 0 |
| Wscale | ±1(1), ±2(¼), ±4(1/16), ±8(1/64) (degree 8) | 0.99929 | 0.89800 | 0 |
| Two1-12 | ±1(1), ±12(4) (degree 4) | 0.99574 | 0.96973 | 0 |
| **Pair1-47** | ±1 and ±47 (degree 4) — the near-antipodal pair | **0.42089** | 0.95081 | 0 |

Held-out capacity span **0.57840** against the required set's **0.06307** — a nine-fold wider test
range, carried by a single point.

**Input resolution on the held-out set** (how many of the six rings each predictor distinguishes):

| predictor | distinct of 6 |
|---|---|
| near-gap | **2** (2, 2, 13, 2, 2, 2) |
| degeneracy | **2** (47, 47, 47, 47, 47, 23) |
| λ₂, λ₃, λ₄, λ₅ | **6** |
| r3 | 2 |
| r4, r5 | 6 |

**This inverts the resolution story, and it is the sharpest result of the audit.** On the held-out set
both D_052 predictors separate exactly one ring (Pair1-47) while the edge inputs are the **only**
predictors that resolve all six — and the edge inputs still **lose**: capacity ρ = 0.143 (p = 0.8028)
against the degeneracy count's 0.655, and they lose on LOO as well (0.35984 vs the field's 0.23353).
**Resolution is not predictiveness.**

---

## Classification

### DERIVED

* **The edge shape of a ring family is essentially one number.** The k ↔ N−k coincidence makes the
  spectrum bottom a doublet, so λ₂ = λ₃ and λ₄ = λ₅ in 12 of 13 rings, r3 ≡ 1 (two values in the whole
  family) and r5 ≡ r4. What remains is **r4's departure from 4** — the edge-bending measure.
* **One declared input varies on the calibration set and is constant on the target set.** r3 spans a
  wide range across D_048/D_050's six source cases but takes two values on the ring family. Its
  capacity fit extrapolates to a **negative** prediction (−0.49307 on G1248) and yields ρ = 0.000 with
  p = 1.0000. This is **D_051's failure mode in mirror image**, and it is visible structurally, without
  any measurement.
* **The derived bounds disqualify an input family.** 0 ≤ capacity, recovery ≤ 1 are identities, so a
  predictor that leaves the range is wrong by construction: 12 cells violate for r4/r5, 1 for r3, none
  for λ₂ … λ₅. Admissibility and resolution pull in opposite directions here.
* **λ₂ is an order-preserving relabelling, not a new instrument.** On the required set λ₂ … λ₅ give
  **identical ρ to three decimals** on both targets (capacity 0.214 for λ₂ and λ₃, 0.179 for λ₄ and λ₅;
  recovery 0.821 for all four). Adding λ₃, λ₄, λ₅ buys nothing — D_050's lesson reproduced on a
  different input family.

### EMERGENT

* **The recovery signal is real but uncorrected.** λ₂ reaches ρ = 0.821 (exact p = 0.0341) on the
  required set — the first predictor in the D group to beat the D_052 field on **both** ρ and LOO —
  and it repeats directionally on the held-out set (ρ = 0.771, p = 0.1028), but it does **not** survive
  Bonferroni correction for seven declared inputs, and the required set's near-gap LOO is not even
  defined, so only the ρ comparison is available.
* **The held-out edge-shape rings all adapt strongly** (0.98440 … 0.99929) **except Pair1-47**
  (0.42089). Twelve of thirteen rings sit at capacity ≈ 0.94 … 1.00, so the required case set has
  almost no variance left to explain.
* **Resolution is not predictiveness.** The only predictors that resolve all six held-out rings are the
  ones that predict worst on capacity.

### REFUTED

* **"Capacity is controlled by the shape of the low-energy spectral edge."** REFUTED: the best
  edge-shape input reaches ρ = **0.214** (p = 0.6615) against the degeneracy count's **0.815**
  (p = 0.0310), and the shape ratios r4, r5 are **negatively** correlated with capacity (ρ = −0.429).
* **"The edge shape beats the D_052 field."** REFUTED for capacity on every criterion and on both case
  sets; on the held-out set the edge inputs are the *worse* predictors, with the degeneracy count
  reaching refit R² = 0.999 there.
* **"λ₃, λ₄ and λ₅ add information beyond λ₂."** REFUTED: identical ρ to three decimals, because the
  doublet structure ties them together.
* **"The shape ratios are usable predictors."** REFUTED in linear form: r4 and r5 violate the derived
  bounds in 12 cells (up to capacity 1.09533) and r3 is constant on the target family.

---

## Verdict

**Goal — "beat the near-gap, degeneracy and multiplicity-spectrum predictors":**
**NOT MET for capacity; MET on the required case set for recovery, but not confirmed and not surviving
multiple-comparison correction.**

**DERIVED** — the doublet collapse (r3 ≡ 1, r5 ≡ r4, one surviving shape number r4); the
calibration/target mismatch of r3; the derived bounds disqualifying r4/r5 in linear form; and λ₂ … λ₅
being one predictor relabelled.

**EMERGENT** — the recovery ρ = 0.821 signal and its directional held-out echo; the near-total
uniformity of ring capacity; and the resolution-versus-predictiveness inversion.

**REFUTED** — edge-shape control of capacity; the edge shape beating the D_052 field; λ₃/λ₄/λ₅ adding
information; and the shape ratios being usable predictors.

**What the audit does establish.** The low-energy edge does not control capacity within this ring
family — the family has almost no capacity variance left to control, and the edge descriptors that
*do* resolve it geometrically are either degenerate (r3, r5) or inadmissible in linear form (r4). But
the edge **geometry** does identify the one ring that breaks the family: **Pair1-47 (±1 and ±47)** is
the only audited ring whose spectrum bottom is not the folded doublet pair — λ₂ at k = 2 rather than
k = 1 — and it is the only one whose capacity falls to **0.42089**. That is an **existence statement
about the ring family, not a predictor**: the family is not uniform, and the outlier is found by its
geometry rather than by any of the numeric fits tried here.

No canonical AT claim, value, equation or registry entry is changed; the D_040 `ClassificationRegistry`
is untouched. No new simulation primitive: the required case set reuses the shared cache; only the six
held-out rings are measured here.

---

## References

* **D_053** — Perturbation-family dominance: the spectrum factor's margin.
* **D_052** — Degeneracy axis: the baselines (ρ 0.815 / LOO 0.03304) and the resolution framing tested here.
* **D_051** — Blind prediction audit: the ring family and the "varies on the calibration set, constant on the target set" failure mode.
* **D_050** — Spectral predictability: the derived bounds and the minimal-set lesson.
* **D_048** — The perturbation ensemble.
* **D_047** — Degeneracy-lock theorem.
* **D_040** — `ClassificationRegistry` (canonical classification guard; untouched).
* **Test suite** — `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_054_Tests.cs` (6 tests, all passing).
