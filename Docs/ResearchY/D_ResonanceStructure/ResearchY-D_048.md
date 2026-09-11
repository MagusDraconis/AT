# ResearchY-D_048 — Latent-Degeneracy Adaptability Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_048 (permanent)
**Title:** Latent-Degeneracy Adaptability Audit
**Status:** COMPLETE
**Date:** 2026-09-10
**File:** `D_ResonanceStructure/ResearchY-D_048.md`
**Depends on:** D_047 (degeneracy-lock), T_015 (spectral robustness)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_048_Tests.cs`

---

## Purpose

**Does latent degeneracy predict adaptability?** *Latent degeneracy* is the fraction of the
spectrum that is degenerate — the structural reservoir a perturbation can unsplit:

$$L \;=\; \frac{N - A_0}{N},$$

where $A_0$ is the number of distinct eigenspaces (attractors) of the graph Laplacian.
The audit measures four response quantities under four perturbation types across five doses,
and correlates each with $\lambda_2$, the spectral entropy $E_0$, the attractor count $A_0$,
and $L$.

**Quantitative only: no AT theoretical assumptions are used anywhere in this audit.**

---

## Method

**Cases** (all $N = 96$):

| case | construction |
|---|---|
| D96 | circulant $C_{96}(\pm1..\pm6)$ |
| D96-3D | $4\times4\times6$ periodic torus (nearest neighbour in 3 directions) |
| random | seeded sparse Erdős–Rényi, $p = 0.3$, seed 42 |
| physical | circulant reconstructed from the symmetric spectrum $\lambda_m = m$ |
| unphysical | circulant reconstructed from the piecewise-constant profile $5/25/60$ |
| complete | $K_{96}$ |

**Perturbations** (deterministic LCG, 3 seeds each; dose = a fraction of the graph's own
edge count, so cases are compared at equal *relative* damage):

1. **edge deletion** — remove $k$ distinct edges;
2. **edge addition** — add $k$ distinct non-edges;
3. **rewiring** — $k$ times: remove a random edge, add a random non-edge;
4. **weight perturbation** — multiply every edge weight by $1 \pm \varepsilon$, $\varepsilon = 2\times$dose.

Doses: $\{0.5\%, 1\%, 2\%, 5\%, 10\%\}$. Perturbations that **disconnect** the graph are
excluded (connectivity guard); in practice **zero** samples were excluded, so no results are
missing.

**Measures**

| # | measure | definition |
|---|---|---|
| 1 | reachable attractor increase | $\Delta A = A(\text{perturbed}) - A_0$ |
| 2 | entropy increase | $\Delta E = E(\text{perturbed}) - E_0$ (nats, basin distribution $p_i = m_i/N$) |
| 3 | recovery | $R = 1 - \lVert\lambda' - \lambda\rVert_2 / \lVert\lambda\rVert_2$ (spectral retention) |
| 4 | structural diversity gain | $\Delta A / (N - A_0)$ — the *fraction of the latent capacity realized* |

**Predictors**: $\lambda_2$ (smallest positive eigenvalue), $E_0$, $A_0$, $L$.
**Adaptive capacity** = mean of measure 4 over all (type, dose, seed) samples.
Statistics: tie-averaged Spearman $\rho$ over the six cases.

---

## 1. Baselines

| case | $A_0$ | edges | $\lambda_2$ | $E_0$ | $L$ | capacity | mean $\Delta A$ | mean $\Delta E$ | recovery |
|---|---|---|---|---|---|---|---|---|---|
| **D96** | 45 | 576 | **0.3864** | 3.7620 | 0.5312 | **0.9902** | 50.50 | 0.7941 | 0.9659 |
| **D96-3D** | 13 | 288 | 1.0000 | 2.2350 | 0.8646 | 0.7207 | 59.82 | 1.7020 | 0.9625 |
| **random** | 96 | 1394 | 17.1888 | 4.5643 | **0.0000** | **0.0000** | 0.00 | 0.0000 | 0.9792 |
| **physical** | 49 | 4512 | 1.0000 | 3.8856 | 0.4896 | 0.8011 | 37.65 | 0.5437 | 0.9558 |
| **unphysical** | 4 | 4560 | 5.0000 | 1.1450 | 0.9583 | 0.7228 | 66.50 | 2.4992 | 0.9443 |
| **complete** | 2 | 4560 | **96.0000** | 0.0579 | **0.9792** | **0.2858** | 26.87 | 1.4601 | 0.9868 |

The two predictors are **near-orthogonal**: $L$ ranks
complete > unphysical > D96-3D > D96 > physical > random, while $\lambda_2$ ranks
D96 < D96-3D = physical < unphysical < random < complete.

---

## 2. Capacity vs dose (structural diversity gain)

| case | 0.5 % | 1.0 % | 2.0 % | 5.0 % | 10.0 % |
|---|---|---|---|---|---|
| D96 | **0.962** | 0.990 | 0.998 | **1.000** | **1.000** |
| D96-3D | 0.363 | 0.567 | 0.738 | 0.935 | **1.000** |
| random | 0.000 | 0.000 | 0.000 | 0.000 | 0.000 |
| physical | 0.878 | 0.878 | 0.750 | 0.750 | 0.750 |
| unphysical | 0.614 | 0.750 | 0.750 | 0.750 | 0.750 |
| complete | 0.149 | 0.207 | 0.319 | 0.377 | 0.378 |

- **D96 saturates immediately** — at the smallest dose it has already realized 96 % of its
  latent capacity.
- **D96-3D and complete are dose-limited** — their capacity keeps rising (or plateaus far
  below 1).
- **physical and unphysical plateau at 0.75** — their reservoir is real but only partly
  reachable by any of the four perturbation types.
- **random is flat zero** at every dose.

## 3. Capacity vs perturbation type

| case | delete | add | rewire | weight |
|---|---|---|---|---|
| D96 | 0.984 | 0.979 | 0.997 | 1.000 |
| D96-3D | 0.573 | 0.583 | 0.726 | 1.000 |
| random | 0.000 | 0.000 | 0.000 | 0.000 |
| physical | 1.000 | 0.204 | 1.000 | 1.000 |
| unphysical | 0.943 | 0.000 | 0.948 | 1.000 |
| complete | 0.633 | 0.000 | 0.000 | 0.511 |

Structural impossibilities appear as **exact zeros**: $K_{96}$ has no non-edge to add, and
rewiring is a no-op (its only non-edge is the edge just removed); the unphysical profile's
spectrum is configured by weights, so adding unit edges does nothing. **Weight perturbation is
the universally strongest lever** (1.000 for every structured case).

---

## 4. Correlations

Spearman $\rho$ (tie-averaged ranks) over the six cases:

| measure | vs $L$ | vs $\lambda_2$ | vs $E_0$ | vs $A_0$ |
|---|---|---|---|---|
| **capacity** | **−0.029** | **−0.841** | +0.029 | +0.029 |
| mean $\Delta A$ | +0.429 | −0.493 | −0.429 | −0.429 |
| mean $\Delta E$ | **+0.829** | 0.000 | −0.829 | −0.829 |
| recovery | +0.029 | +0.464 | −0.029 | −0.029 |

($E_0$ and $A_0$ are monotonically related to $L$ — $L = 1 - A_0/N$ — so their $\rho$ columns
are the sign-flipped copies; they carry no independent information here.)

---

## 5. Hypothesis verdicts

### H1 — *Adaptive capacity scales with latent degeneracy* → **PARTIALLY REFUTED**

- For the **normalized** adaptive capacity: $\rho(\text{capacity}, L) = -0.029$ — **no scaling
  whatever**, despite $L$ spanning 0.00 → 0.98 across the cases.
- For the **raw** gains the scaling is real: $\rho(\Delta A, L) = +0.429$ and, strongly,
  $\rho(\Delta E, L) = +0.829$ — a larger reservoir releases more attractors and much more
  entropy.
- **Interpretation:** $L$ is the *ceiling* ($\Delta A \le N - A_0$ by construction), **not the
  response**. Knowing how much degeneracy a graph has tells you the size of the prize, not how
  much of it a given perturbation collects. Complete ($L = 0.979$) has the largest prize and the
  **lowest** capacity (0.286); D96 ($L = 0.531$) has a moderate prize and the **highest**
  capacity (0.990).

### H2 — *L predicts adaptability better than $\lambda_2$* → **REFUTED**

- $\rho(\text{capacity}, \lambda_2) = -0.841$ versus $\rho(\text{capacity}, L) = -0.029$:
  **the gap wins by a factor ≈ 29 in |ρ|**, and the sign is **inverted** — a *smaller* gap means
  a *more* adaptable graph.
- The same ordering holds for the raw attractor increase ($\rho(\Delta A, \lambda_2) = -0.493$
  vs $+0.429$).
- **Counterexample:** D96's gap is $248\times$ smaller than complete's ($0.3864$ vs $96$) and its
  capacity is $3.5\times$ larger, even though complete holds *more* latent degeneracy.
- **Interpretation:** **near**-degeneracy — a spectrum that is nearly, not exactly, degenerate
  — is what indexes adaptability. A tiny gap means many eigenlevels sit within a perturbation's
  reach; an exact degeneracy count does not capture that.

### H3 — *Random graphs have low latent capacity* → **SUPPORTED**

- $L(\text{random}) = 0$ **exactly** (all 96 eigenvalues distinct) and capacity $= 0$ **exactly**:
  $\Delta A \equiv \Delta E \equiv 0$ for every perturbation type at every dose.
- Every degenerate case adapts ($\text{capacity} > 0.25$), random does not adapt at all.
- Random is nevertheless among the **most** recovered (0.9792) — second only to complete.

### Side finding — recovery and adaptability are anti-correlated

The null graph (least adaptable) is nearly the most stable; the most latent graphs (unphysical
0.9443, complete… 0.9868) and the most adaptable (D96 0.9659) sit on opposite sides of the
recovery ranking from random. Across the six cases $\rho(\text{recovery}, L) = +0.029$ and
$\rho(\text{recovery}, \lambda_2) = +0.464$: **structural reserve buys adaptability at some cost
in stability, and exact integrity buys stability at the cost of all adaptability.**

---

## 6. Bounds and caveats

1. **n = 6 cases.** The Spearman correlations are exact rank statistics over a small sample;
   they are reported as such, not as population estimates. The per-dose and per-type tables
   carry the same information without the reduction to a single number.
2. **Dose measured in edge fractions**, not in operator norm — a deliberate choice so that
   sparse (D96-3D, 288 edges) and dense ($K_{96}$, 4560 edges) graphs are compared at equal
   *relative* damage.
3. **Exact zeros are structural**, not numerical: they mark perturbations that are impossible
   for that graph (add on $K_{96}$, rewire on $K_{96}$ and on the unphysical profile).
4. **Random case sensitivity.** The sparse graph is generated by a seeded PRNG; its $\lambda_2$
   ($\approx 17.2$) varies with the implementation, but its rank position (between 5 and 96) and
   its exact-null behaviour are stable.
5. **Recovery is defined as spectral retention** (measure 3), so it measures stability of the
   spectrum, not a dynamical return to an attractor.

---

## 7. Classification

| Component | Status |
|---|---|
| $L$ bounds the reachable attractor increase, $\Delta A \le N - A_0$ | **DERIVED** (partition of the spectrum) |
| Raw $\Delta A$ and $\Delta E$ scale with $L$ ($\rho = +0.43 / +0.83$) | **DERIVED** (measured rank relation) |
| Smaller $\lambda_2$ ⇒ larger normalized capacity ($\rho = -0.84$) | **DERIVED** (measured rank relation) |
| "Adaptive capacity scales with latent degeneracy" (normalized) | **REFUTED** |
| "L predicts adaptability better than $\lambda_2$" | **REFUTED** |
| "Random graphs have low latent capacity" | **SUPPORTED** |
| Per-case capacity values, dose plateaus (0.75), exact zeros | **EMERGENT** |
| Case selection, dose grid, seed set, connectivity guard | **BOUNDARY** (protocol choices) |

**No AT claim, value, equation, or registry entry is changed.** The
`Y_D_040_Tests.ClassificationRegistry` is untouched.

---

## 8. Relationship to D_047

D_047 showed that a degeneracy is a **lock** whose release is *independent of the perturbation
amplitude*. D_048 shows that the *size* of the lock ($L$) does **not** predict how much of it a
real perturbation collects — the spectral **gap** does, inversely. Together:

- D_047: the release is a **step** in $\varepsilon$ (amplification is divergent).
- D_048: **which graph** releases most is set by $\lambda_2$, not by $L$ — the lock's *proximity*
  matters more than its *size*.

Both are consistent with T_015 (robustness is controlled by degeneracy structure under finite
perturbation) and sharpen it: under *finite* perturbation the controlling variable is the gap,
not the degeneracy count.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_048_Tests.cs`
**Run:** 2026-09-10 · **Result:** 7/7 PASSED

| Test | Verifies | Result |
|---|---|---|
| `Y_D_048_LatentFractionBaseline` | $L$, $A_0$, $\lambda_2$ baselines; no excluded samples | ✅ |
| `Y_D_048_AdaptiveCapacity` | capacity ordering, dose response, structural zeros | ✅ |
| `Y_D_048_H1_LatentDegeneracy` | H1 — partially refuted | ✅ |
| `Y_D_048_H2_GapBeatsLatent` | H2 — refuted ($\lambda_2$ wins, inverted sign) | ✅ |
| `Y_D_048_H3_RandomNull` | H3 — supported (exact null) | ✅ |
| `Y_D_048_RecoveryAndDiversityGain` | recovery monotone in dose; gain ≤ 1 | ✅ |
| `Y_D_048_Run` | research report | ✅ |

**Conclusion:** latent degeneracy does **not** predict adaptability. $L = (N-A_0)/N$ is the
**ceiling** — raw $\Delta A$ and $\Delta E$ scale with it ($\rho = +0.43$, $+0.83$) — but the
**normalized** adaptive capacity is uncorrelated with it ($\rho = -0.03$) and is instead
predicted by the spectral gap, inverted ($\rho = -0.84$): a *small* $\lambda_2$ means a *more*
adaptable graph. Random graphs have zero latent capacity and are exact nulls ($\Delta A \equiv
\Delta E \equiv 0$). Recovery and adaptability trade off against each other.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_048"`

---

## References

- ResearchY-D_047 (degeneracy-lock amplification — the release law D_048 sizes).
- ResearchY-T_015 (spectral robustness — degeneracy controls robustness under finite perturbation).
- ResearchY-D_028 (span), D_040 (classification registry).
- `SpectralCaseCatalog` / `AttractorDominanceAnalyzer` (the shared spectral case set).
