# ResearchY-D_049 — Adaptability–Robustness Frontier Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_049 (permanent)
**Title:** Adaptability–Robustness Frontier Audit
**Status:** COMPLETE
**Date:** 2026-09-10
**File:** `D_ResonanceStructure/ResearchY-D_049.md`
**Depends on:** D_048 (latent-degeneracy adaptability), D_047 (degeneracy-lock), T_015 (spectral robustness)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_049_Tests.cs`
**Shared machinery:** `AT.Tests/Shared/AdaptabilityAudit.cs` (also serves D_048)

---

## Purpose

**Is there a universal tradeoff  Adaptability × Robustness = const ?** The audit measures the
two axes established in D_048 — **capacity** (adaptability) and **recovery** (robustness) —
together with λ₂ and the degeneracy structure, for six 96-node graphs, and asks three questions:

1. Is the **product** $C \times R$ constant, i.e. is there a conserved quantity?
2. What is the **shape** of the capacity–recovery relation (a fit)?
3. Is the resulting **frontier universal**, i.e. stable across perturbation families?

**Quantitative only: no AT theoretical assumptions are used anywhere in this audit.**

---

## Method

**Cases.** D96 (circulant $C_{96}(\pm1..\pm6)$), **D96³** (the three-dimensional D96 variant —
the $4\times4\times6$ periodic torus, $N=96$, as used in D_048), random (seeded sparse
$p = 0.3$), complete ($K_{96}$), physical (circulant with $\lambda_m = m$), unphysical
(circulant with the piecewise-constant profile $5/25/60$).
*(Note: D_047's "D96³" is the different object $D96\otimes D96\otimes D96$ with $96^3$ modes; here
the label denotes the 3-D variant at $N=96$, keeping all six cases dimensionally comparable.)*

**Measures** (from the shared deterministic ensemble of 4 perturbation types × 5 doses × 3 seeds,
connectivity-guarded; zero samples excluded):

| measure | definition |
|---|---|
| **capacity** | mean $\Delta A/(N-A_0)$ — the fraction of latent capacity realized |
| **recovery** | mean $1 - \lVert\lambda'-\lambda\rVert_2/\lVert\lambda\rVert_2$ — spectral retention |
| **λ₂** | the spectral gap (smallest positive eigenvalue) |
| **degeneracy** | the number of degenerate eigenspaces (multiplicity > 1); plus $L = (N-A_0)/N$ |
| **product** | $C \times R$ — the candidate conserved quantity |
| **damage** | $\Delta = 1 - R$ (positive; used as the fit's abscissa) |

**Fit.** (a) linear OLS $C = a + bR$; (b) power law $C = k\,\Delta^{\beta}$ (free $\beta$);
(c) the **conserved-product law** $C\cdot\Delta = \text{const}$ — that is $\beta = -1$ — forced
through the same data, compared by $R^2$.

---

## 1. The two axes

| case | λ₂ | degen groups | L | **capacity** | **recovery** | **product** | frontier |
|---|---|---|---|---|---|---|---|
| **D96** | 0.3864 | 44 | 0.5312 | **0.9902** | 0.9659 | 0.9564 | **PARETO** |
| D96³ | 1.0000 | 11 | 0.8646 | 0.7207 | 0.9625 | 0.6936 | dominated |
| random | 17.1888 | 0 | 0.0000 | 0.0000 | 0.9792 | **0.0000** | dominated |
| physical | 1.0000 | 47 | 0.4896 | 0.8011 | 0.9558 | 0.7656 | dominated |
| unphysical | 5.0000 | 3 | 0.9583 | 0.7228 | 0.9443 | 0.6826 | dominated |
| **complete** | 96.0000 | 1 | 0.9792 | 0.2858 | **0.9868** | 0.2820 | **PARETO** |

---

## 2. Is the product constant? — **NO**

| statistic | value |
|---|---|
| non-null products | 0.9564, 0.6936, 0.7656, 0.6826, 0.2820 |
| mean / sd / **CV** | 0.6761 / 0.2201 / **0.326** |
| max ÷ min (non-null) | **3.39×** |
| worst deviation from the mean | > 50 % |
| the null case | product = **0.0000 exactly** |

The null graph alone refutes "universal": with capacity 0 its product is 0 while D96's is 0.956 —
an unbounded ratio. Even restricting to the five non-null cases the product spans a factor of
3.39 with a coefficient of variation of 33 %. **No constant is conserved.**

---

## 3. The fit — the tradeoff has the *wrong sign* for a conserved product

| fit | result | $R^2$ |
|---|---|---|
| (a) linear $C = a + bR$ | $C = 16.48 - 16.46\,R$ | **0.475** |
| (b) power law $C = k\Delta^{\beta}$ | $C = 7.98\,\Delta^{+0.737}$ | **0.720** |
| (c) conserved product $C\cdot\Delta$ | $\beta$ forced to $-1$ | **−3.282** |

- The linear slope is **negative** ($-16.46$): across the six cases, higher recovery goes with
  lower capacity.
- The power law of damage is **positive**: capacity **rises** with damage, sub-linearly —
  a doubling of damage buys only $2^{0.737} = 1.666\times$ capacity.
- A conserved product $C \cdot \Delta = \text{const}$ requires the exponent $\beta = -1$.
  Forcing it yields $R^2 = -3.28$ — **worse than predicting the mean** — because the data has the
  *opposite sign*.

So not only is the product not constant; the functional form demanded by conservation is
qualitatively wrong for these graphs.

---

## 4. The frontier — a tradeoff **exists**, but it is a 2-point frontier

Maximizing both axes, the (capacity, recovery) Pareto frontier has **exactly two members**:

| member | role |
|---|---|
| **D96** | maximum adaptability (0.9902); recovery 0.9659 |
| **complete** | maximum robustness (0.9868); capacity 0.2858 |

Every other case — D96³, random, physical, unphysical — is **strictly dominated**: there is a
frontier member better on *both* axes. These four graphs give up adaptability **without buying
any robustness**. The tradeoff is therefore real (two incomparable optima) but extremely thin:
it is a 2-point frontier, not a law.

---

## 5. Universality fails across perturbation families

| family | Pareto frontier |
|---|---|
| edge deletion | **{physical}** — a single case dominates both axes |
| edge addition | {D96, physical, unphysical, complete} |
| rewiring | {D96, physical, complete} |
| weight perturbation | {D96, random, complete} |

The frontier **changes with the perturbation family**, and under **edge deletion it collapses to
one case** — under that family alone there is *no* tradeoff at all, because `physical` is best on
both axes. The aggregate frontier ({D96, complete}) is not reproduced by any single family, and
the degenerate-free null appears on one family frontier (weight) while being strictly dominated
in aggregate.

---

## 6. What shape is the tradeoff? — one quantity, two signs

| axis | vs λ₂ | vs degeneracy count | vs L |
|---|---|---|---|
| **capacity** | **−0.841** | **+0.886** | −0.029 |
| **recovery** | **+0.464** | **−0.543** | +0.029 |

The mechanism is a single structural quantity read two ways. The **degeneracy count** (equivalently,
inversely, the spectral gap — they correlate at $\rho = -0.814$) raises capacity and lowers
recovery: it redistributes one and the same graph between the two axes.

Note the sharp distinction from D_048: the **count** of degenerate eigenspaces predicts capacity
at $\rho = +0.886$, while the degeneracy **fraction** $L = (N-A_0)/N$ predicts it at
$\rho = -0.029$. Counting degeneracies indexes the tradeoff; taking their share does not.

---

## 7. Classification

### DERIVED

1. **The frontier structure.** The (capacity, recovery) Pareto frontier has exactly two members,
   D96 (max adaptability) and complete (max robustness); the other four cases are strictly
   dominated. *A tradeoff exists.*
2. **The mechanism.** One structural quantity governs both axes with opposite signs: the
   degeneracy count ($\rho = +0.886$ capacity, $-0.543$ recovery), equivalently the inverse gap
   ($-0.841$, $+0.464$).
3. **The count-vs-fraction distinction.** The degeneracy count beats the degeneracy fraction $L$
   by a factor ≈ 30 in $|\rho|$.

### EMERGENT

1. **The fitted constants** $\beta = 0.737$, $k = 7.98$ — numbers read off this six-case ensemble,
   with no derivation.
2. **The particular dominated set** and the per-case products.
3. **The frontier instability**: deletion collapses to one case, addition gives four. Nothing
   in the ensemble forbids this; it is an outcome, not a law.

### REFUTED

> **"Adaptability × Robustness = const" as a universal law.**

- The product spans 0 → 0.956 (the null gives exactly 0), and 0.282 → 0.956 among the non-null
  cases: a 3.39× span, CV 0.33.
- The conserved-product law has the **wrong sign**: capacity *rises* with damage (exponent
  $+0.74$), whereas $C \cdot \Delta = \text{const}$ demands $-1$; forcing it gives
  $R^2 = -3.28$ against $+0.72$ for the free power law — worse than the mean.
- The frontier is **not universal**: its membership depends on the perturbation family, and under
  edge deletion it collapses to a single dominant case.

**No AT claim, value, equation, or registry entry is changed.** The
`Y_D_040_Tests.ClassificationRegistry` is untouched.

---

## 8. Relation to the earlier audits

| audit | result | relation |
|---|---|---|
| D_047 | degeneracy splitting releases a fixed entropy for any $\varepsilon$ | *the release law* |
| D_048 | latent degeneracy is a **ceiling**, not a predictor; λ₂ indexes adaptability | *the axes* |
| **D_049** | the two axes form a **2-point** frontier governed by the degeneracy count; no conserved product | *the frontier* |

Collectively: a degeneracy is a lock (D_047); the size of the lock is a ceiling, not a response
(D_048); and the graph that collects the most of its lock trades robustness for it along a frontier
with exactly two optimal members (D_049). **The tradeoff is real but not conserved, and it is not
universal — under a single perturbation family it can vanish.**

---

## 9. Caveats

1. **n = 6 cases** — an exact rank statement over a small ensemble, not a population estimate.
2. **Recovery is spectral retention** (a static measure of spectral damage), not a dynamical return.
3. **The null is excluded from the log-fit** ($\log 0$ undefined); it is included in every other statistic.
4. **Dose is measured in edge fractions**, so sparse and dense graphs are compared at equal
   *relative* damage.
5. **Case-label note.** "D96³" here is the 3-D D96 variant at $N=96$ (the $4\times4\times6$ torus),
   matching D_048; D_047's $96^3$-mode tensor product is a different object.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_049_Tests.cs`
**Run:** 2026-09-10 · **Result:** 7/7 PASSED

| Test | Verifies | Result |
|---|---|---|
| `Y_D_049_ProductIsNotConstant` | product span, CV, null = 0 | ✅ |
| `Y_D_049_FitCapacityVsRecovery` | linear + power-law fits; forced $\beta=-1$ collapses | ✅ |
| `Y_D_049_ParetoFrontier` | 2-member frontier; other four strictly dominated | ✅ |
| `Y_D_049_FrontierAcrossPerturbationFamilies` | frontier instability; deletion collapses to one | ✅ |
| `Y_D_049_GapIsTheControlParameter` | one quantity, two signs; count beats fraction | ✅ |
| `Y_D_049_Classification` | DERIVED / EMERGENT / REFUTED outputs | ✅ |
| `Y_D_049_Run` | research report | ✅ |

**Output:**

- **DERIVED** — the (capacity, recovery) Pareto frontier has exactly two members, D96 (max
  adaptability) and complete (max robustness), with the other four strictly dominated; a single
  structural quantity (the degeneracy count / inverse gap) governs both axes with opposite signs.
- **EMERGENT** — the fitted exponent $\beta = 0.74$ and constant $k = 8.0$; the particular
  dominated set; the frontier's instability across perturbation families.
- **REFUTED** — there is **no universal tradeoff** $Adaptability \times Robustness = const$: the
  product spans 0 → 0.956 (CV 0.33, span 3.39× among non-null cases), the conserved-product law has
  the wrong sign and fits at $R^2 = -3.28$ versus $+0.72$ for the free power law, and the frontier
  collapses to a single case under edge deletion.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_049"`

---

## References

- ResearchY-D_047 (degeneracy-lock amplification), D_048 (latent-degeneracy adaptability).
- ResearchY-T_015 (spectral robustness), D_028 (span), D_040 (classification registry).
- `AT.Tests/Shared/AdaptabilityAudit.cs` — the shared deterministic perturbation ensemble.
