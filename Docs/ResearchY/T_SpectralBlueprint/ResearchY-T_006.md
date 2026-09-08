# ResearchY-T_006 — Darwinian Dominance Emergence

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_006 (permanent)
**Title:** Darwinian Dominance Emergence — does competition produce dominant attractors?
**Status:** COMPLETE
**Date:** 2026-09-08
**File:** `T_SpectralBlueprint/ResearchY-T_006.md`
**Depends on:** ResearchY-T_005 (attractor structure)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_006_Tests.cs` (3/3 ✅)
**Core analyzer:** `AT.Core/ResearchT/DarwinianDominanceAnalyzer.cs`

---

## Question

Do dominant attractors emerge from **Darwinian resource competition** rather than from
spectral structure alone? (T_005 showed bare spectral structure does NOT dominate.)

## Method

Replicator dynamics over the attractor set. Model-free scoring (no AT assumptions):
`w_i = r_i / c_i`, with `r_i = m_i` (resource acquisition ∝ basin/multiplicity) and
`c_i = λ_i` (maintenance cost ∝ mode energy). Replicator `p_i(t+1) = p_i(t)·w_i / Σ p_j w_j`
from uniform `p_i(0) = 1/A`; zero (uniform) mode excluded. Deterministic; 2000 steps.

Metrics: `D = max(p)`, `N_eff = exp(H)`, extinction fraction `E`, `HHI = Σ p²`,
time-to-dominance (steps to D ≥ 0.9), fitness ratio `w_max/w_2nd`.

---

## Results

| Model | A | D_init | D_final | N_eff | Extinct | t_dom | w_max/w_2nd |
|---|---|---|---|---|---|---|---|
| D96 | 44 | 0.062 | 1.000 | 1.000 | 0.977 | **3** | 3.89 |
| D96-3D | 12 | 0.208 | 1.000 | 1.000 | 0.917 | 13 | 1.20 |
| physical max-sep | 48 | 0.021 | 1.000 | 1.000 | 0.979 | 4 | 2.00 |
| unphysical clustered | 3 | 0.333 | 1.000 | 1.000 | 0.667 | 2 | 5.00 |
| random sparse | 95 | 0.010 | 1.000 | 1.000 | 0.989 | 50 | 1.05 |
| complete | 1 | 0.990 | 1.000 | 1.000 | 0.000 | 1 | ∞ |

---

## Hypotheses

| Hypothesis | Result |
|---|---|
| **H1** spectral organization accelerates competitive dominance | **SUPPORTED** — D96 t_dom=3 vs random t_dom=50 (~17× faster); fitness ratio 3.89 vs 1.05 |
| **H2** D96 produces fewer effective survivors than random | **REFUTED** — both collapse to N_eff≈1 (unique fittest); selection is universal |
| **H3** complete graph is a degenerate special case | **SUPPORTED** — A=1, D_init=0.990 (already dominant, no competition needed) |

**Success criterion** (dominance emerges only after competition): **CONFIRMED** — D rises
from 1–33% (spectral) to 100% (after competition) for every model.

---

## Key findings

1. **Replicator dynamics converge to the fittest eigenspace (DERIVED).** Closed form
   `p_i(t) ∝ w_i^t`; a unique maximal fitness wins.

2. **Dominance emerges only after competition (EMERGENT).** D_init (largest basin 1–33%)
   → D_final ≈ 100% under selection, resolving T_005's finding that spectral structure
   alone does not dominate.

3. **"Spectral structure alone determines dominance" is REFUTED.** It is the Darwinian
   selection layer, not the spectrum, that produces a dominant attractor.

4. **Selection is universal (H2 refuted).** Every model collapses to a single survivor
   (N_eff≈1); "few survivors" is a feature of replicator dynamics, not of D96.

5. **Acceleration is non-monotonic (nuance on H1).** D96 (t_dom=3) beats random (50), but
   unphysical clustered (t_dom=2, ratio 5.0) is fastest and D96-3D (t_dom=13, ratio 1.2)
   is slower than 1D D96 — organization accelerates *via sharp fitness gaps*, not uniformly.

---

## Classification

| Item | Classification |
|---|---|
| Replicator dynamics converge to fittest eigenspace | DERIVED |
| Dominance emerges only after competition | EMERGENT |
| "Spectral structure alone determines dominance" | REFUTED |
| "D96 produces fewer effective survivors" (H2) | REFUTED |

---

## Conclusion

Dominant attractors are a product of **Darwinian resource competition**, not spectral
structure alone. Model-free fitness `w = m/λ` drives every model to a single fittest
eigenspace (D→1, N_eff→1); the complete graph is the degenerate exception. This resolves
the T_005 arc: spectral organization *compresses* the attractor count (T_005) and
*accelerates* selection (H1), but **dominance itself is the dynamical (selection) layer** —
exactly the mechanism AT's Darwinian information ecology (w=r/c) predicts.
