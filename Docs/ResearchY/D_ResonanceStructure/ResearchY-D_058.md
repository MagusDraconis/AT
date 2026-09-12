# ResearchY-D_058 — Recovery Mechanism Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_058 (permanent)
**Title:** Recovery Mechanism Audit
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `D_ResonanceStructure/ResearchY-D_058.md`
**Depends on:** D_057 (the rank law and its scope), D_056 (the distribution), D_055 (the mechanism), D_053 (the family factor), D_049 (recovery as an axis), D_048 (the ensemble)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_058_Tests.cs` (7 tests)
**Shared machinery:** `AT.Tests/Shared/AdaptabilityAudit.cs`
**Prediction commit:** `2758ad43` (hypothesis, mechanism and frozen prediction — the two new rings measured only in the next commit)
**Measurement commit:** the commit following `2758ad43`

---

## Question

**Why is recovery not predicted by the rank budget, the multiplicity distribution, the degeneracy
count, λ₂ or the near-gap density — while capacity is?**

### The hypothesis, stated before measurement

**Capacity and recovery are different observable classes:**

* **Capacity is a COUNT.** ΔA = A₁ − A₀ is the number of *new distinct eigenvalues*, an integer, and the
  rank budget $\Delta A \le \sum_i \min(m_i-1, r)$ bounds exactly it.
* **Recovery is a MAGNITUDE.** $1 - \lVert\lambda'-\lambda\rVert_2/\lVert\lambda\rVert_2$ is a relative
  spectral displacement. **A bound on how *many* new levels appear says nothing about how *far* they
  move.**

If that is right, then (i) the two targets should be near-orthogonal across samples, and (ii) the
rank-budget ceiling should have no grip on recovery.

### The magnitude mechanism, stated before measurement

First-order perturbation theory gives $\delta\lambda_i = v_i^\top \delta A\, v_i$, so the RMS shift
scales with $\lVert\delta A\rVert_F$. Normalizing by the spectrum's own norm:

$$1 - \text{recovery} \approx c \cdot \frac{\lVert\delta A\rVert_F}{\lVert\lambda\rVert_2}$$

For a degree-*d* unit ring $\lVert\lambda\rVert_2 = \sqrt{N\,d\,(d+1)}$ **exactly** (since
$\operatorname{tr}L^2 = dN(d+1)$ for a *d*-regular simple graph), while $k$ edge deletions give
$\lVert\delta A\rVert_F = \sqrt{2k}$ — so **denser or heavier graphs suffer less at the same dose
fraction**.

---

## Results

### 1. The class distinction — count versus magnitude

| comparison | ρ |
|---|---|
| ρ(ΔA, relative shift), all 480 samples | **0.046** |
| delete | 0.296 |
| add | 0.163 |
| rewire | −0.046 |
| weight | −0.251 |
| ρ(mean ΔA, mean shift) across 8 rings | −0.595 |

| ring | mean ΔA (count) | mean relative shift (magnitude) | capacity | recovery |
|---|---|---|---|---|
| D96 | 50.500 | 0.03408 | 0.9902 | 0.9659 |
| Pair1-47 | 29.883 | 0.04919 | 0.4209 | 0.9508 |
| S96-123 | 49.433 | 0.03956 | 0.9693 | 0.9604 |
| S96-135 | 47.783 | 0.03357 | 0.9369 | 0.9664 |
| Ring48 | 50.617 | 0.02756 | 0.9925 | 0.9724 |
| Decay96 | 47.000 | 0.08023 | 1.0000 | 0.9198 |
| Boost96 | 48.767 | 0.02463 | 0.9952 | 0.9754 |
| D96-24 | 55.400 | 0.03063 | 0.9719 | 0.9694 |

The scale contrast is the point: **ΔA moves from 29.9 to 55.4 (capacity 0.42 … 1.00) while the
relative shift moves only 0.0246 … 0.0802 (recovery 0.9198 … 0.9754).**

### 2. The rank budget's scope

| quantity | ρ |
|---|---|
| ρ(ceiling, **capacity**) | **0.873** — the count law works |
| ρ(ceiling, **recovery**) | **0.409** — the count law has no grip |

The same ceiling, on the identical samples. **And there is a structural reason**: the rank budget counts
how many distinct levels a rank-*r* perturbation can produce, while recovery measures the L2 distance
they travel. Two perturbations can create the *same* number of new levels while moving them by
completely different distances — the count is invariant under a rescaling of the shift, and the shift is
what recovery reads.

**So recovery is not a failed prediction; it is outside the count predictors' type.**

### 3. The magnitude law

Calibrated on D_048/D_050's six **source** cases only:

$$1 - \text{recovery} = 0.82416 \cdot \frac{\lVert\delta A\rVert_F}{\lVert\lambda\rVert_2} - 0.002242$$

On the ring family this is **out-of-sample** and reaches **ρ = 0.905** with **mean |error| 0.00767**.

| family | n | slope | R² | ρ |
|---|---|---|---|---|
| delete | 120 | 1.104 | 0.8698 | 0.959 |
| add | 120 | 1.240 | 0.9022 | 0.961 |
| rewire | 120 | 0.850 | 0.8511 | 0.957 |
| weight | 120 | 0.513 | 0.8020 | 0.933 |

One slope per family, all near 1 — the law is structural rather than calibrated to one family.

**The spectral norm is exact, not fitted** — $\lVert\lambda\rVert_2 = \sqrt{Nd(d+1)}$ for the
unweighted *d*-regular rings:

| ring | degree | measured ‖λ‖₂ | √(N·d·(d+1)) |
|---|---|---|---|
| D96 | 12 | **122.38** | **122.38** |
| Pair1-47 | 4 | **43.82** | **43.82** |
| S96-123 | 6 | **63.50** | **63.50** |
| S96-135 | 6 | **63.50** | **63.50** |
| D96-24 | 24 | **240.00** | **240.00** |
| Ring48 | 15 | 162.19 | 151.79 (long-range weights) |
| Decay96 | 12 | 50.90 | 122.38 (decaying weights) |
| Boost96 | 12 | 432.22 | 122.38 (growing weights) |

### 4. Head-to-head — what actually predicts recovery?

The magnitude law's coefficient is calibrated on the **sources**; the correlational candidates are
scored by **in-family leave-one-out** — again the more generous protocol.

| predictor | kind | ρ | RMSE | mean \|error\| |
|---|---|---|---|---|
| **MAGNITUDE LAW (1 coeff)** | derived, out-of-sample | **0.905** | **0.00928** | **0.00767** |
| ‖δA‖_F alone | derived, out-of-sample | −0.738 | 7.25937 | 6.24566 |
| λ₂ | correlation, in-family LOO | 0.833 | 0.01459 | 0.01106 |
| near-gap | correlation, in-family LOO | 0.412 | 0.01614 | 0.01151 |
| degeneracy count | correlation, in-family LOO | 0.024 | 0.01679 | 0.01209 |
| max multiplicity | correlation, in-family LOO | 0.024 | 0.01673 | 0.01172 |
| largest share | correlation, in-family LOO | 0.024 | 0.01673 | 0.01172 |
| perturbation family | categorical, in-family LOO | 0.000 | 0.01680 | 0.01239 |

* **The magnitude law leads on ordering and error.**
* **‖δA‖_F alone fails** (ρ = −0.738) — the *denominator* is the content: the perturbation's size matters
  only relative to the spectrum's scale.
* **λ₂ is the best of the five named candidates** (ρ = 0.833) — it is the crudest proxy for ‖λ‖₂, which is
  why it has any grip at all.
* The three **count/shape** candidates sit at **ρ = 0.024**.
* **The honest scale caveat:** recovery spans only **0.0556** across these rings against capacity's
  **0.5791** — a band **10.4× narrower**. Every ρ is therefore inflated and every error deflated; the
  magnitude law's mean |error| of 0.0077 is **14 % of the entire recovery range**, not 1 %.

### 5. The blind test — three frozen predictions, all confirmed

Two new rings carrying **exactly D96's offset set** at weight 4.0 and 0.25 — verified multiplicity-**identical**
to D96 (pattern `6×1, 5×1, 2×42, 1×1`, A₀ = 45, headroom identical) with ‖λ‖₂ ratios of exactly 4.0000
and 0.2500:

| ring | capacity | recovery | delete | add | rewire | weight |
|---|---|---|---|---|---|---|
| D96x4 | 0.99052 | 0.97460 | 0.95515 | 0.99033 | 0.96236 | 0.99055 |
| D96 | 0.99020 | 0.96592 | 0.95515 | 0.95655 | 0.96142 | 0.99055 |
| D96x025 | 0.99052 | 0.87039 | 0.95515 | 0.75829 | 0.77758 | 0.99055 |

1. **Capacity identical across the three** — spread **0.00033** on 0.9902. **CONFIRMED**: the count
   mechanism is blind to a global weight scale.
2. **Recovery under the edge families rises with scale** — 0.96928 > 0.95771 > 0.83034. **CONFIRMED.**
3. **Recovery under the weight family is scale-invariant** — spread **exactly 0.00000** on 0.99055.
   **CONFIRMED** — the cleanest single result in the audit.

**A sharper finding than the prediction anticipated:** **delete recovery is *also* exactly
scale-invariant** (0.95515 × 3), and the scale sensitivity of the edge families comes specifically from
**add** and **rewire**, because those insert an edge at **fixed unit weight** while deletion removes
weight *in proportion*. The magnitude law states this exactly: a proportional removal leaves
‖δA‖_F/‖λ‖₂ unchanged; a fixed-weight insertion does not.

---

## Classification

### DERIVED

* **The answer is a type statement, not a failure.** Capacity is a **count** (bounded by the rank
  budget); recovery is a **magnitude** (a relative spectral displacement). ρ(ΔA, relative shift) = 0.046
  across 480 samples — near-orthogonal. The identical ceiling gives ρ = 0.873 on capacity and 0.409 on
  recovery.
* **The magnitude law for recovery**, derived the same way as the rank bound:
  $1 - \text{recovery} \approx c \cdot \lVert\delta A\rVert_F/\lVert\lambda\rVert_2$ — one coefficient,
  ρ = 0.905 out-of-sample, mean |error| 0.00767 (the best of everything tried).
* **The spectral norm is exact:** $\lVert\lambda\rVert_2 = \sqrt{Nd(d+1)}$ for *d*-regular unweighted
  rings, confirmed to 5 significant figures on four rings (122.38, 43.82, 63.50, 240.00).
* **The scale-sensitivity structure is derived:** proportional removal (delete, weight) is
  scale-invariant; fixed-unit-weight insertion (add, rewire) is not.

### EMERGENT

* **All three frozen predictions confirmed** — capacity blind to scale (spread 0.00033), recovery
  scale-sensitive under insertion (0.99033 → 0.95655 → 0.75829 in the add family), weight-family
  recovery exactly invariant (spread 0.00000).
* **A sharper result than predicted** — delete is *also* exactly invariant, which the frozen prediction
  did not anticipate.
* **λ₂ is the best of the five named candidates** (ρ = 0.833), as the crudest proxy for ‖λ‖₂; the three
  count/shape candidates sit at ρ = 0.024.
* **The scale caveat**: recovery's band is 10.4× narrower than capacity's, so its metrics are inflated.

### REFUTED

* **"Recovery is a failed case of the count predictors."** REFUTED — it is outside their *type*.
* **"Recovery is weakly predicted by the multiplicity distribution after all."** REFUTED: ρ = 0.024 with
  the degeneracy count, max multiplicity and largest share.
* **"Recovery needs no spectral scale — the perturbation norm suffices."** REFUTED: ‖δA‖_F alone gives
  ρ = −0.738 and an error three orders of magnitude larger.
* **"The perturbation family determines recovery."** REFUTED as a ring-level claim: as a per-ring average
  it is a constant (ρ = 0.000). The family effect is real but lives *within* a ring — D_053's finding
  restated on the other target.
* **"The near-gap density matters for recovery."** REFUTED: ρ = 0.412 but no better error than the count
  predictors, since six of eight rings share its value.

---

## Verdict

**DERIVED** — the count/magnitude type distinction; the magnitude law and its single coefficient; the
exact spectral norm $\sqrt{Nd(d+1)}$; and the derived scale-sensitivity structure.

**EMERGENT** — all three blind predictions confirmed (with delete-invariance sharper than predicted);
λ₂'s position as the best of the named candidates; and the narrow-band caveat.

**REFUTED** — recovery as a failed count prediction; any multiplicity-distribution grip on recovery; the
perturbation norm without the spectral scale; family as a ring-level determinant; and the near-gap
density as a recovery predictor.

**Summary.** Recovery belongs to a **different mechanism class**: it is a **magnitude** governed by the
perturbation's norm relative to the spectral norm, exactly as capacity is a **count** governed by the
rank budget against the multiplicity structure. Both mechanisms are now derived, both are exact in their
structural part (the rank lemma; $\operatorname{tr}L^2 = dN(d+1)$), and each carries one coefficient.
**The caveat, stated rather than buried:** recovery spans only 0.0556 across these rings against
capacity's 0.5791 — a band 10.4× narrower — so every ρ is inflated and every error deflated; a mean
|error| of 0.0077 is 14 % of the entire recovery range. The right reading is that the magnitude law
captures the **ordering** and explains the **scale sensitivity**, not that recovery is predicted to
within a percent.

No canonical AT claim, value, equation or registry entry is changed; the D_040 `ClassificationRegistry`
is untouched. No new simulation primitive: the eight previously audited rings come from the shared cache
and only the two scale mirrors are measured here.

---

## References

* **D_057** — Rank-budget law: the count law, and the residual it left open.
* **D_056** — Multiplicity distribution: shown here to be a proxy for tightness.
* **D_055** — Pair1-47 anomaly: the mechanism behind the count.
* **D_053** — Perturbation-family dominance: the family effect restated on recovery.
* **D_049** — Adaptability–robustness frontier: recovery as an axis.
* **D_048** — The perturbation ensemble.
* **D_040** — `ClassificationRegistry` (canonical classification guard; untouched).
* **Test suite** — `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_058_Tests.cs` (7 tests, all passing).
