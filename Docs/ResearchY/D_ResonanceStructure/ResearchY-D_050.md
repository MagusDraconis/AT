# ResearchY-D_050 — Spectral Predictability Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_050 (permanent)
**Title:** Spectral Predictability Audit
**Status:** COMPLETE
**Date:** 2026-09-10
**File:** `D_ResonanceStructure/ResearchY-D_050.md`
**Depends on:** D_048 (latent-degeneracy adaptability), D_049 (adaptability–robustness frontier), T_014 (near-gap density), T_015 (spectral robustness)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_050_Tests.cs`
**Shared machinery:** `AT.Tests/Shared/AdaptabilityAudit.cs` (also serves D_048, D_049)

---

## Question

**Can adaptability and robustness be predicted from spectral quantities alone?**

D_048 established *capacity* (adaptability) and D_049 established *recovery* (robustness) as
measurable properties of a graph's edge structure. Both audits left the same question open:
the measures were *computed* from a perturbation ensemble, not *predicted* from anything. If a
handful of spectral numbers already determines them, then "adaptability" is not a dynamic
property at all — it is a spectral restatement, and the perturbation ensemble is expensive
theatre.

This audit tests that directly. It supplies **four spectral inputs and nothing else** —

| input | definition |
|---|---|
| λ₂ | algebraic connectivity (second-smallest Laplacian eigenvalue) |
| degeneracy count | number of Laplacian levels with multiplicity > 1 |
| near-gap density | number of *positive* eigenvalues ≤ 2λ₂ (the T_014 count at $k=2$) |
| distinct count | number of distinct Laplacian eigenvalues |

— and asks whether the two targets, **capacity** and **recovery**, can be reconstructed:

$$ \text{capacity} = F(\lambda_2,\ \text{degeneracy},\ \text{near-gap},\ \text{distinct}),
\qquad
\text{recovery} = G(\lambda_2,\ \text{degeneracy},\ \text{near-gap},\ \text{distinct}) $$

and, if so, **what is the minimal predictor set**?

**Quantitative only: no AT theoretical assumptions are used anywhere in this audit.**

---

## Method

**Cases.** The six D_048/D_049 ensemble members, unchanged: D96 ($C_{96}(\pm1..\pm6)$), D96³
(the $4\times4\times6$ torus), random (seeded sparse $p=0.3$), physical (circulant,
$\lambda_m = m$), unphysical (circulant, piecewise-constant $5/25/60$), complete ($K_{96}$).

**Targets.** Taken *unchanged* from the shared ensemble — same perturbation types, doses, seeds,
connectivity guard, zero-sample exclusion. No re-measurement is performed, so the D_049 tie-out
is a regression test rather than a new experiment:

* **capacity** $= \overline{\Delta A / (N - A_0)}$ — mean normalized edge-activation headroom used;
* **recovery** $= \overline{1 - \lVert\lambda' - \lambda\rVert_2 / \lVert\lambda\rVert_2}$ — mean
  spectral recovery after perturbation.

**Fitting.** Ordinary least squares on standardized-free raw inputs, scored two ways:

* **in-sample** $R^2$ — reported but *not used to decide anything*;
* **leave-one-out RMSE** — one point held out, model refit, error measured on the held-out point.
  With $n = 6$, a four-predictor fit with intercept spends **five parameters on six points**;
  in-sample $R^2$ is therefore meaningless and LOO is the only honest selector.

**Parsimony rule.** The minimal predictor set is the subset with the lowest LOO error, breaking
ties toward fewer parameters. This is a *prediction* criterion, applied before knowing which
subset wins.

**Tests** (5): inputs-and-targets table with D_049 tie-out; collinearity of the four inputs;
one-predictor-at-a-time scan; exhaustive subset scan with LOO; verdict.

---

## Results

### 1. The complete data set ($n = 6$)

| case | capacity | recovery | λ₂ | degeneracy count | near-gap (2λ₂) | distinct |
|---|---|---|---|---|---|---|
| D96 | **0.9902** | 0.9659 | 0.3864 | 44 | 2 | 45 |
| D96³ | 0.7207 | 0.9625 | 1.0000 | 11 | 6 | 13 |
| random | **0.0000** | 0.9792 | 17.1888 | 0 | 71 | 96 |
| physical | 0.8011 | 0.9558 | 1.0000 | 47 | 4 | 49 |
| unphysical | 0.7228 | 0.9443 | 5.0000 | 3 | 32 | 4 |
| complete | 0.2858 | 0.9868 | 96.0000 | 1 | 95 | 2 |

**Tie-out with D_049 (regression test):** D96 capacity 0.9902 (published 0.9902), complete
0.2858 (published 0.2858), random 0.0000 (published 0.0000). Same ensemble, no re-measurement.

Two structural facts are already visible in the raw table, and the fits must respect them:

1. **capacity is not monotone in raw multiplicity.** The random graph has *no* degeneracy and
   exactly zero capacity, but the complete graph — the *largest* multiplicity — reaches only
   0.2858, because capacity is normalized by the headroom $N - A_0$, which the complete graph has
   already spent.
2. **The two targets live on different scales.** recovery spans 0.9443…0.9868 (a 4.5% band) while
   capacity spans 0.0000…0.9902 (the full range). A predictor that works for one need not work
   for the other, and any recovery fit is inherently a fit of small numbers.

### 2. Collinearity — the four inputs are not four independent facts

Spearman ρ between inputs:

| | λ₂ | degeneracy count | near-gap density | distinct count |
|---|---|---|---|---|
| **λ₂** | 1.000 | −0.841 | **0.986** | −0.319 |
| **degeneracy count** | −0.841 | 1.000 | −0.886 | 0.143 |
| **near-gap density** | **0.986** | −0.886 | 1.000 | −0.371 |
| **distinct count** | −0.319 | 0.143 | −0.371 | 1.000 |

* λ₂ vs degeneracy count: ρ = −0.841 — strongly collinear (D_049 measured −0.814 on the same
  ensemble). *A small gap and a large degeneracy count are the same statement.*
* λ₂ vs near-gap density: **ρ = 0.986** — nearly the same variable.
* distinct count spans only 94 levels across the six cases (2 … 96) and correlates with nothing.

**Consequence:** a four-input fit is mostly fitting *one* axis four times over. That is precisely
the regime where extra predictors buy in-sample $R^2$ and nothing else.

### 3. One predictor at a time

| target | input | Spearman ρ | slope | intercept | R² (in) | **LOO RMSE** |
|---|---|---|---|---|---|---|
| capacity | λ₂ | −0.841 | −0.0053 | 0.6935 | 0.295 | 1.7979 |
| capacity | degeneracy count | **+0.886** | +0.0122 | 0.3706 | 0.529 | 0.3318 |
| capacity | **near-gap density** | −0.886 | −0.0081 | 0.8715 | **0.759** | **0.2962** |
| capacity | distinct count | +0.029 | −0.0040 | 0.7250 | 0.152 | 0.6519 |
| recovery | λ₂ | +0.464 | +0.0003 | 0.9597 | 0.536 | 0.0314 |
| recovery | degeneracy count | −0.543 | −0.0002 | 0.9697 | 0.102 | 0.0186 |
| recovery | **near-gap density** | +0.486 | +0.0003 | 0.9558 | **0.525** | **0.0129** |
| recovery | distinct count | −0.029 | +0.0001 | 0.9619 | 0.067 | 0.0220 |

**Best single predictor for both targets: the near-gap density** — but the collinear pair disagrees
between the two criteria, and that disagreement is the informative part:

* **capacity:** the rank correlation is an *exact tie* — |ρ| = 0.886 for both the degeneracy count
  (+0.886) and the near-gap density (−0.886) — while the value fit prefers the near-gap density
  (R² = 0.759 vs 0.529), and LOO too (0.2962 vs 0.3318).
* **recovery:** the rank prefers the **degeneracy count** (|ρ| = 0.543 vs 0.486) while the value
  fit prefers the **near-gap density** (R² = 0.525 vs 0.102), as does LOO (0.0129 vs 0.0186).

Neither member of the pair dominates the other. They are the same axis seen two ways — D_049's
mechanism — so the identity of the "chosen" predictor is a labelling choice among collinear
mirrors, not a discovery. λ₂ is the *worst* of the three equivalent inputs, which is why
"predict from λ₂" is the weakest form of the claim.

### 4. Minimal predictor set — parsimony decided by LOO

Every subset, scored both ways ($n = 6$):

| target | size | subset | R² (in) | **LOO RMSE** | params |
|---|---|---|---|---|---|
| capacity | 1 | **near-gap density** | 0.759 | **0.2962** | 2 |
| capacity | 1 | degeneracy count | 0.529 | 0.3318 | 2 |
| capacity | 1 | distinct count | 0.152 | 0.6519 | 2 |
| capacity | 1 | λ₂ | 0.295 | 1.7979 | 2 |
| capacity | 2 | degeneracy count + near-gap | 0.776 | 0.3164 | 3 |
| capacity | 3 | λ₂ + degeneracy + near-gap | 0.889 | 3.9370 | 4 |
| capacity | 4 | all four | **0.972** | **31.0123** | 5 |
| recovery | 1 | **near-gap density** | 0.525 | **0.0129** | 2 |
| recovery | 1 | degeneracy count | 0.102 | 0.0186 | 2 |
| recovery | 1 | distinct count | 0.067 | 0.0220 | 2 |
| recovery | 1 | λ₂ | 0.536 | 0.0314 | 2 |
| recovery | 2 | degeneracy count + near-gap | 0.623 | 0.0169 | 3 |
| recovery | 3 | degeneracy + near-gap + distinct | 0.646 | 0.2138 | 4 |
| recovery | 4 | all four | 0.964 | 1.4640 | 5 |

$$ \textbf{Minimal set for capacity} = \{\text{near-gap density}\} \qquad
\textbf{Minimal set for recovery} = \{\text{near-gap density}\} $$

**The overfitting demonstration (capacity, all four inputs):** in-sample $R^2 = 0.972$ — by far
the best of any model — with LOO RMSE = **31.01**, versus a single predictor at
$R^2 = 0.759$ and LOO RMSE = **0.296**: the four-input model is **~105× worse** out of sample.
Five parameters on six points. Adding collinear inputs (λ₂, the distinct count) buys in-sample
$R^2$ and nothing that survives LOO.

**Fitted relations (single predictor, the LOO-selected minimal set):**

$$ \text{capacity} = -0.00814\,(\text{near-gap density}) + 0.87153, \qquad R^2 = 0.759 $$
$$ \text{recovery} = +0.000284\,(\text{near-gap density}) + 0.95582, \qquad R^2 = 0.525 $$

### 5. Residuals — the relation ranks, it does not determine

Residuals of the minimal capacity fit (fitted − measured):

| case | measured | fitted | residual |
|---|---|---|---|
| D96 | 0.9902 | 0.8553 | −0.1349 |
| D96³ | 0.7207 | 0.8227 | +0.1020 |
| random | 0.0000 | 0.2939 | **+0.2939** |
| physical | 0.8011 | 0.8390 | +0.0379 |
| unphysical | 0.7228 | 0.6112 | −0.1117 |
| complete | 0.2858 | 0.0986 | −0.1872 |

Largest absolute residual = **0.2939**, i.e. **30% of the full capacity range** — comparable to
the fitted values themselves. The single-predictor law gets the *order* right (D96 highest,
random lowest, complete low) while missing individual values by up to a third of the scale.
Every residual above 0.10 sits at an endpoint of the axis — D96 and the complete graph at the
tail the fit *under*-predicts, random at the head it *over*-predicts by a factor of infinity
(the measured value is exactly zero) — so the linear form breaks precisely where the structural
extremes are, which is where the derived bounds (not the fit) do the work.

---

## Classification

### DERIVED

* **The scales are derived, exactly and in closed form.** $0 \le \text{capacity} \le 1$ because
  $\Delta A \le N - A_0$ = headroom is an identity (D_048); $0 \le \text{recovery} \le 1$ because
  it is a normalized distance. Any predictor must land inside these bounds — a fit leaving them is
  wrong by construction. The fitted relations above respect them on this case set.
* **The random case is a DERIVED null.** Zero degeneracy ⇒ zero capacity, exactly (D_047/D_048).
  This is not a fitted result; it is a structural identity, and it is the reason the capacity fit
  over-predicts random by 0.29.
* **The degeneracy axis is DERIVED.** Capacity is bounded by the multiplicity structure
  ($\Delta E_{\text{lock}} = \tfrac{1}{N}\sum_m m\ln m$, D_047) and recovery by the split
  statistics of exactly those degenerate levels (T_015). A spectral predictor can therefore only
  ever be a *proxy* for a quantity that is already determined combinatorially.

### EMERGENT

* **The fitted forms and their coefficients are emergent ensemble numbers.** capacity *falls*
  with near-gap density (slope −0.0081) while recovery *rises* (slope +0.00030) — one spectral
  axis, two opposite-signed readings (D_049's mechanism). R² = 0.759 and 0.525, with residuals of
  the order of the values.
* **The minimal set is ONE quantity, not four.** The rank correlation and the value fit disagree
  over which member of the collinear pair to prefer (capacity's rank is an exact tie between the
  degeneracy count and the near-gap density at |ρ| = 0.886; recovery's rank prefers the count while
  its value fit prefers the density). "Spectral quantities alone" predict the *direction* and the
  *order* of both targets, not their values.
* **Parsimony is a choice, the coefficients are ensemble-specific.** The predictor was selected by
  LOO before seeing which subset won; its coefficients are numbers of this ensemble and must not
  be presented as AT-derived constants.

### REFUTED

* **"A four-input spectral law determines capacity and recovery."** REFUTED as overfitting: the
  all-input model has the best in-sample $R^2$ (0.972) and the worst LOO error (31.01 vs 0.296),
  on five parameters over six cases — and three of the four inputs are collinear restatements of
  one axis (λ₂ ↔ near-gap density ρ = 0.986; λ₂ ↔ degeneracy count ρ = −0.841).
* **"λ₂, near-gap density and the distinct count are independent predictors."** REFUTED: λ₂ and
  the degeneracy count are strongly collinear, near-gap density adds nothing beyond them (its
  two-input LOO is *worse* than its own singleton — 0.3164 vs 0.2962), and the distinct count is
  nearly constant across the case set and correlates with neither target (|ρ| ≤ 0.03).
* **"A spectral predictor determines adaptability and robustness."** REFUTED in the strong sense:
  the same spectral profile yields different capacity under different perturbation **families**
  (D_049: the frontier collapses to a single case under edge deletion), so the target is not a
  function of the spectrum alone — it is a function of spectrum **and** family. Any predictor that
  is fed the spectrum only is therefore structurally incapable of being exact.

---

## Verdict

**DERIVED** — the bounds ($0 \le \cdot \le 1$), the zero-degeneracy null, and the combinatorial
determination of capacity by the multiplicity structure.

**EMERGENT** — the fitted forms, the rank-vs-value split within the collinear axis, and the
one-predictor minimal set.

**REFUTED** — the four-input spectral law (overfits catastrophically), the independence of the
inputs, and strong spectral determination.

**Answer to the question.** *Can adaptability and robustness be predicted from spectral
quantities alone?* **Partly, and only as an ordering.** The minimal predictor set is **one**
spectral quantity — near-gap density for value, the degeneracy count for rank, collinear on this
case set. It is a useful **ranking** device (which lattice will adapt, which will resist) and a
poor **determination** device: residuals reach 30% of the capacity range, the extremes of the axis
are where the linear form breaks, and D_049 shows the target moves with the perturbation family.

**The honest form of any future claim is:** *capacity and recovery are **ordered** by the
degeneracy axis* — not *they are **determined** by the spectrum*.

No canonical AT claim, value or equation is changed; no reclassification of any prior result (the
D_040 `ClassificationRegistry` is untouched); the D_047/D_048 bounds and the D_049 family
dependence are cited, not altered.

---

## References

* **D_047** — Degeneracy-lock audit: $\Delta E_{\text{lock}} = \tfrac{1}{N}\sum_m m\ln m$.
* **D_048** — Latent-degeneracy adaptability: capacity, and the headroom identity $\Delta A \le N - A_0$.
* **D_049** — Adaptability–robustness frontier: the two axes, the family collapse under edge deletion.
* **T_014** — Near-gap density counting convention (positive eigenvalues within $k\lambda_2$).
* **T_015** — Spectral robustness and split statistics of degenerate levels.
* **D_040** — `ClassificationRegistry` (canonical classification guard; untouched by this audit).
* **Shared machinery** — `AT.Tests/Shared/AdaptabilityAudit.cs`.
* **Test suite** — `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_050_Tests.cs` (5 tests, all passing).
