# ResearchY-T_015 — Spectral Robustness Audit

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_015 (permanent)
**Title:** Spectral Robustness — is it controlled by near-gap density ρ or gap size λ₂?
**Status:** COMPLETE
**Date:** 2026-09-09
**File:** `T_SpectralBlueprint/ResearchY-T_015.md`
**Depends on:** ResearchY-T_010/T_014 (near-gap density), T_001–T_004 (spectral rigidity)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_015_Tests.cs` (4/4 ✅)

---

## Question

Is spectral robustness controlled by the **near-gap density** ρ(k) = N_gap(k)/N rather than by
the **gap size** λ₂? Apply edge-removal / edge-addition / weight-noise perturbations and
correlate the resulting λ₂ shift, attractor-count shift, and spectral reordering with λ₂, m(λ₂),
and ρ(k).

## Method

For each of D96 / D96-3D / physical / unphysical / random / complete: compute ρ(k), apply the
three perturbations, measure (1) relative gap shift Δλ₂/λ₂, (2) attractor-count shift ΔA (change
in distinct eigenvalues), (3) normalized spectral L2 shift. Quantitative only, deterministic.

## Results

| model | λ₂ | m(λ₂) | ρ(2) | Δλ₂/λ₂ | ΔA | spectral | degenerate levels |
|---|---|---|---|---|---|---|---|
| D96 | 0.386 | 2 | 0.021 | 0.0077 | **30** | 0.0073 | 44 |
| physical | 1.0 | 2 | 0.021 | 0.0343 | 24 | 0.0376 | 47 |
| D96-3D | 1.0 | 2 | 0.042 | 0.0446 | 8 | 0.0255 | 11 |
| unphysical | 5.0 | 32 | 0.333 | 0.2333 | 2 | 0.0505 | 3 |
| random | 17.19 | 1 | 0.740 | 0.0003 | **0** | 0.0028 | 0 |
| complete | 96.0 | 95 | 0.990 | 0.0403 | **1** | 0.0041 | 1 |

---

## Hypotheses

| H | Claim | Verdict |
|---|---|---|
| H1 | Gap size alone is insufficient | **SUPPORTED** — complete (λ₂=96) is MORE fragile (Δλ₂/λ₂=0.040) than D96 (λ₂=0.386, 0.008), yet random (λ₂=17.2) is MORE robust (0.0003). λ₂ does not rank robustness. |
| H2 | Low ρ(k) predicts robustness | **REFUTED** — D96 has the LOWEST ρ (0.021, isolated gap) yet the LARGEST attractor shift (30); complete has the HIGHEST ρ (0.99) yet ΔA=1. |
| H3 | D96 outperforms random (normalized) | **REFUTED** — random (all-singleton) is MORE robust (spectral 0.0028 < D96 0.0073); D96's rigid circulant symmetry is itself a fragility. |

---

## The mechanism (actual controlling factor)

**Robustness is controlled by the DEGENERACY structure, not ρ or λ₂.** The attractor shift ΔA
counts the degenerate eigenvalue *levels* that split under a symmetry-breaking perturbation:

- **D96** has 44 distinct non-zero eigenvalues, *all* degenerate (doublets + octave degeneracies);
  one edge removal breaks the reflection symmetry and splits ~30 of them (ΔA=30) — fragile.
- **random** has 95 distinct eigenvalues, *all* singletons; nothing to split (ΔA=0) — robust.
- **complete** has one 95-fold-degenerate level; a rank-1 (single-edge) perturbation splits off
  exactly *one* eigenvalue (ΔA=1), not 95 — robust in degeneracy despite the highest ρ.

ΔA tracks the **degenerate-level count** (D96=44, random=0, complete=1), and the gap shift is
confounded by 1/λ₂ (large gap → small relative shift). Neither ρ(k) nor λ₂ predicts robustness.

## Classification

| Item | Classification |
|---|---|
| ΔA = number of degenerate levels that split (deterministic function of the multiplicity structure) | DERIVED |
| The specific shift magnitudes | EMERGENT |
| "Gap size λ₂ predicts robustness" (H1 implication) | REFUTED |
| "Near-gap density ρ predicts robustness" (H2) | REFUTED |
| "D96 outperforms random" (H3) | REFUTED |

---

## Conclusion

Spectral robustness is **not** controlled by the near-gap density ρ(k) nor by the gap size λ₂ —
it is set by the **degeneracy structure**: the number of degenerate (multiplicity>1) eigenvalue
levels that split under a symmetry-breaking perturbation. This is a **DERIVED** function of the
spectrum's multiplicity structure. The specific magnitudes are **EMERGENT**. All three proposed
hypotheses are overturned: H1 is SUPPORTED only as "λ₂ is insufficient"; H2 (ρ predicts
robustness) is **REFUTED**; H3 (D96 outperforms random) is **REFUTED** — D96's rigid circulant
symmetry is itself the source of its fragility (one broken edge splits ~30 doublets), while a
random all-singleton spectrum has nothing to split. No new primitive; canonical AT unchanged.
