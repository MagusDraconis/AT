# ResearchY-D_057 — Rank-Budget Law Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_057 (permanent)
**Title:** Rank-Budget Law Audit
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `D_ResonanceStructure/ResearchY-D_057.md`
**Depends on:** D_055 (the bound), D_056 (the distribution as a predictor), D_053 (the dose convention), D_052, D_048 (the ensemble)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_057_Tests.cs` (6 tests)
**Shared machinery:** `AT.Tests/Shared/AdaptabilityAudit.cs`
**Prediction commit:** `bda8e880` (prediction and lemma verification only — **no simulation**)
**Measurement commit:** the commit following `bda8e880`

---

## Question

**Does $\Delta A \le \sum_i \min(m_i - 1, r)$ directly predict capacity**, replacing the correlations of
D_050–D_056 with a derived law?

Three law forms were frozen in PHASE A, in increasing order of fitted content:

| law | form | parameters |
|---|---|---|
| **L1** | pure ceiling: $C = \operatorname{mean}_d \min(1, \sum_i \min(m_i-1, 2k_d)/(N-A_0))$ | **zero** |
| **L2** | effective-rank: $r_d = \operatorname{round}(c \cdot 2k_d)$ | **one per family** |
| **L3** | tightness-corrected: $C = L1 \cdot \tau$ | **one per family** |

All parameters come from D_048/D_050's six **source** cases only; every ring error is out-of-sample.

---

## Results

### 1. The lemma, verified outside the ensemble

For $m \in \{4, 8, 16\}$ and $r = 1 \dots 6$, 200 random symmetric rank-*r* perturbations supported on
a degenerate eigenspace were diagonalized (Jacobi) and the number of distinct resulting level values
counted:

| m | r | max parts observed | bound r+1 | violations |
|---|---|---|---|---|
| 4 | 1,2,3,4 | 2,3,4,4 | 2,3,4,5 | 0 |
| 8 | 1…6 | 2,3,4,5,6,7 | 2,3,4,5,6,7 | 0 |
| 16 | 1…6 | 2,3,4,5,6,7 | 2,3,4,5,6,7 | 0 |

**0 violations in 3200 draws, and the observed maximum is exactly r + 1 wherever r < m.** The bound is
arithmetic on ranks, independent of the ring family, the ensemble and the doses — so any later failure
is a failure of **tightness** (the bound not being achieved), never of the bound.

### 2. The bound on the real ensemble

| ring family | samples | violations |
|---|---|---|
| all 10 rings × 4 families × 5 doses × 3 seeds | **600** | **0** |

The bound is informative only when *r* bites: it is strictly below the full headroom in 100 % of
delete/add/rewire samples, and **vacuous for weight** (which rescales every edge, so $r = N$ and the
ceiling equals the whole headroom, giving L1 = 1 by construction). The weight family carries no
content for this law — stated up front rather than presented as a success.

### 3. Tightness — and what governs it

Measured / ceiling, both averaged over the four families at the shared dose grid:

| ring | L1 (ceiling) | measured | **tightness** | largest-level share | max m | class |
|---|---|---|---|---|---|---|
| D96 | 1.00000 | 0.99020 | **0.9902** | 6.2 % | 6 | spread |
| S96-123 | 0.98529 | 0.96928 | 0.9837 | 6.2 % | 6 | spread |
| S96-135 | 0.97059 | 0.93693 | 0.9653 | 10.4 % | 10 | spread |
| Ring48 | 1.00000 | 0.99248 | 0.9925 | 6.2 % | 6 | spread |
| Decay96 | 1.00000 | 1.00000 | **1.0000** | 2.1 % | 2 | spread |
| Boost96 | 1.00000 | 0.99524 | 0.9952 | 5.2 % | 5 | spread |
| D96-24 | 1.00000 | 0.97193 | 0.9719 | 12.5 % | 12 | spread |
| **Pair1-47** | 0.63451 | 0.42089 | **0.6633** | 52.1 % | 50 | **DOMINANT** |
| **Half47** | 0.68732 | 0.61479 | **0.8945** | 52.1 % | 50 | **DOMINANT** |
| Triple47 | 1.00000 | 0.99823 | 0.9982 | 2.1 % | 2 | spread |

* **spread rings (8): tightness 0.9653 … 1.0000, mean 0.9871**
* **dominant-level rings (2): tightness 0.6633 … 0.8945, mean 0.7789**
* **ρ(share, tightness) = −0.969**; ρ(max multiplicity, tightness) = −0.969; ρ(L1, tightness) = +0.840

The bound is **nearly achieved** when the multiplicity is spread over many small levels (tens of
doublets all split at once) and **progressively unachieved** as one level concentrates, because the
available rank *r* is then consumed by a single eigenspace. **The law's accuracy is itself a function
of the multiplicity distribution** — which explains D_056: the distribution was predicting the
*tightness*, not the bound.

### 4. The same-share control — resolved by the rank, not the distribution

Pair1-47 and Half47 have **identical multiplicity patterns** (`50×1, 2×22, 1×2`), the same A₀ = 25 and
the same headroom 71, so *every* functional of the multiplicity structure must give them the same
answer — and D_056's predictors indeed cannot separate them. Yet their capacities differ:

| dose | Pair1-47 k/r | ceiling | measured | Half47 k/r | ceiling | measured |
|---|---|---|---|---|---|---|
| 0.5 % | 1 / 2 | 0.3380 | 0.32394 | 1 / 2 | 0.3380 | 0.23005 |
| 1.0 % | 2 / 4 | 0.3662 | 0.33803 | 3 / 6 | 0.3944 | 0.35211 |
| 2.0 % | 4 / 8 | 0.4225 | 0.36620 | 6 / 12 | 0.4789 | 0.39437 |
| 5.0 % | 10 / 20 | 0.5915 | 0.45070 | 14 / 28 | 0.7042 | 0.50704 |
| 10.0 % | 19 / 38 | 0.8451 | 0.57277 | 29 / 58 | **1.0000** | 0.69484 |

Degree 4 (|E| = 192) versus degree 6 (|E| = 288) means the **shared fractional dose grid** hands them
different ranks: at the top dose Pair1-47 gets k = 19 (r = 38 < 49, still trapped) while Half47 gets
k = 29 (r = 58 > 49, able to split completely). **The law is the pair (multiplicity structure, rank)** —
not the structure alone. This also links back to D_053's dose-convention confound.

### 5. Head-to-head

The laws' parameters were calibrated on the six **source** cases, so their errors are genuinely
**out-of-sample**; the correlational predictors are scored by **in-family leave-one-out** — the more
generous protocol. The difference favours the correlations.

| predictor | kind | ρ | RMSE | mean \|error\| |
|---|---|---|---|---|
| **L1 pure ceiling (0 params)** | derived law, out-of-sample | 0.873 | 0.07745 | 0.03918 |
| **L2 effective rank (1/family)** | derived law, out-of-sample | **0.976** | 0.03885 | 0.02287 |
| L3 tightness-corrected (1/family) | derived law, out-of-sample | 0.873 | 0.22181 | 0.21259 |
| max multiplicity | correlation, in-family LOO | −0.878 | 0.02639 | **0.01762** |
| largest share | correlation, in-family LOO | −0.878 | 0.02639 | 0.01762 |
| degeneracy count | correlation, in-family LOO | 0.878 | 0.05699 | 0.04052 |
| λ₂ | correlation, in-family LOO | 0.476 | 0.17706 | 0.12302 |
| near-gap | correlation, in-family LOO | 0.247 | 0.18305 | 0.11922 |

* **L1 uses zero parameters** and matches the *fitted* degeneracy count (ρ 0.873 vs 0.878; error 0.0392
  vs 0.0405) with no parameters and no in-family information.
* **L2's one parameter per family gives the highest ordering in the audit (ρ = 0.976)** and error
  0.02287 — below the degeneracy count's, λ₂'s and near-gap's.
* The max-multiplicity pair has a **slightly smaller error** (0.01762) — but under the more generous
  protocol and at a lower ρ. On ordering the law wins; on raw error under unequal protocols it does not.
* **L3 is the worst of the three** (0.21259): one pooled τ per family is dominated by whatever the
  calibration set happened to contain — the pooling error D_051 and D_056 also exposed.

**The honest limit — per-ring L1 and L2 out-of-sample errors:**

| ring | L1 \|error\| | L2 \|error\| | class |
|---|---|---|---|
| D96 | 0.0098 | 0.0137 | spread |
| S96-123 | 0.0160 | 0.0134 | spread |
| S96-135 | 0.0337 | 0.0046 | spread |
| Ring48 | 0.0075 | 0.0101 | spread |
| Decay96 | **0.0000** | 0.0000 | spread |
| Boost96 | 0.0048 | 0.0044 | spread |
| D96-24 | 0.0281 | 0.0351 | spread |
| **Pair1-47** | **0.2136** | 0.1016 | DOMINANT |

**L1 mean |error|: spread rings 0.0143, dominant-level rings 0.1431 — a factor of 10.** The derived bound
is a near-exact law for spread multiplicity structures and an envelope only for concentrated ones.

### 6. The blind test — both verdicts correct

| ring | design | predicted | observed |
|---|---|---|---|
| **Half47** (±1, ±24, ±47) | level should **survive at λ = 6** | COLLAPSE | λ = **6.000000**, m = 50, capacity **0.61479** ✓ |
| **Triple47** (±1, ±23, ±47) | level should be **destroyed** | healthy | max m = 2, capacity **0.99823** ✓ |

Half47's ±24 term contributes $2(1-\cos(\pi k/2)) = 2$ for odd *k*, so the odd-mode sum is
$2 + (2-2\cos\theta) + (2+2\cos\theta) = 6$ — a **third** dominant-level value, confirming the law
tracks the **level** and not any particular offset. Triple47 uses 23 = N/4 − 1 instead, giving
$6 \mp 2\sin\theta$: no cancellation, no level, a healthy ring. Two rings differing only in that one
offset, and the law called both correctly.

---

## Classification

### DERIVED

* **The lemma** — 3200 synthetic draws, max split count exactly r + 1, zero violations.
* **The bound on the real ensemble** — 600 samples, zero violations.
* **L1 is nearly exact for spread structures with zero parameters** — mean |error| 0.0143 over seven
  rings, including one exact zero (Decay96). For a bound to land that close with no free parameter is
  the strongest form of "derived law" available here.
* **Tightness is ordered by concentration:** ρ(share, tightness) = −0.969, because tens of small levels
  split simultaneously while one huge level consumes all of *r* in a single eigenspace.
* **The same-share control is explained by the rank:** identical patterns and headroom, but degrees 4
  and 6 give different ranks on the shared fractional dose grid — k = 19 (r = 38, trapped) versus
  k = 29 (r = 58, able to split completely).

### EMERGENT

* **The blind design worked, both ways** — Half47 collapsed at λ = 6 with m = 50 (0.61479), Triple47 was
  healthy (0.99823). One offset's difference, opposite verdicts, both correct.
* **L2's single parameter per family is the best ordering in the audit** (ρ = 0.976 out-of-sample),
  halving the worst-case error (dominant-class mean |error| 0.0874 versus L1's 0.1431).
* **Half47's capacity exceeds Pair1-47's despite the identical multiplicity pattern** — an observation no
  distribution-only audit could account for, and that the rank budget does.
* **A residual remains beyond (structure, rank):** at the 0.5 % dose both rings get k = 1, r = 2 and the
  same ceiling 0.3380, yet Half47 measures 0.23005 against Pair1-47's 0.32394.

### REFUTED

* **"The bound directly predicts capacity, period."** REFUTED in the strong form: it is an *upper* bound,
  nearly achieved by spread structures and missed by up to a factor of **10** on concentrated ones. It
  bounds capacity; it does not equal it, and must not be quoted as if it did.
* **"The multiplicity distribution determines capacity."** REFUTED again, now with a **controlled pair**:
  two rings with the same distribution and headroom differ by 0.19390 once their ranks differ. The
  distribution is not a sufficient statistic.
* **"A single pooled tightness correction turns the bound into a law."** REFUTED: L3 is the worst form
  (mean |error| 0.21259).
* **"The weight family supports the law."** REFUTED as empty: r = N makes L1 = 1 by construction.
* **"Rank alone closes the gap."** REFUTED at matched rank — the 0.5 % dose leaves a residual of 0.094.

---

## Verdict

**DERIVED** — the lemma, the bound on the real ensemble, the zero-parameter law's 0.0143 accuracy on
spread structures, the concentration ordering of tightness, and the rank explanation of the same-share
control.

**EMERGENT** — both blind verdicts, L2's leading ordering, Half47's excess over Pair1-47, and the
residual at matched rank.

**REFUTED** — the bound as a direct equality; the distribution as a sufficient statistic; the pooled
tightness correction; the weight family as evidence; and rank as complete.

**Summary.** The rank budget is a genuine **derived law**: its lemma holds in 3200 synthetic draws and
its bound in every real sample, and a **zero-parameter** version already orders the rings (ρ = 0.873)
and lands within 0.0143 on the spread ones. It becomes a usable predictor once **one rank scale per
family** is calibrated (ρ = 0.976) — the best ordering in the D group. But it is **exact only where the
multiplicity is spread**, because tightness is governed by concentration, and it is **not closed by
(structure, rank) alone**: two rings with identical patterns and headroom still differ once their ranks
match. The honest claim is therefore that the derived bound is the correct **first-order** law of
adaptability, with a stated tightness correction whose own dependence remains the open problem.

No canonical AT claim, value, equation or registry entry is changed; the D_040 `ClassificationRegistry`
is untouched. No new simulation primitive: the eight previously audited rings come from the shared cache
and only the two new rings are measured here.

---

## References

* **D_055** — Pair1-47 anomaly: the bound's origin and the dominant-level mechanism.
* **D_056** — Multiplicity distribution: the predictor this audit explains as a proxy for tightness.
* **D_053** — Perturbation-family dominance: the dose convention that makes rank ring-dependent.
* **D_052** — Degeneracy axis; **D_048** — the perturbation ensemble.
* **D_040** — `ClassificationRegistry` (canonical classification guard; untouched).
* **Test suite** — `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_057_Tests.cs` (6 tests, all passing).
