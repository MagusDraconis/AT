# ResearchY-T_005 — Attractor Dominance Audit

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_005 (permanent)
**Title:** Attractor Dominance Audit — do few attractors dominate the state space?
**Status:** COMPLETE
**Date:** 2026-09-08
**File:** `T_SpectralBlueprint/ResearchY-T_005.md`
**Depends on:** ResearchY-T_002 (physicality), T_003/T_004 (isospectrality/rigidity)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_005_Tests.cs` (4/4 ✅)
**Core analyzer:** `AT.Core/ResearchT/AttractorDominanceAnalyzer.cs`

---

## Question

Do a small number of attractors dominate the state space, or is dominance an artifact
of the model definition? Does spectral organization compress the accessible state space
into few dominant attractors?

**Model:** attractors = distinct eigenspaces of the Laplacian (damped mode-locking);
basin of an eigenspace = its multiplicity fraction (the exact measure over uniform
initial states), confirmed by lock-to-max trajectory sampling. Deterministic, N=96.

---

## Results — dominance across models (N=96, sorted by largest basin)

| Model | A (attractors) | D (largest basin) | R (D/2nd) | E_norm | DI | Classification |
|---|---|---|---|---|---|---|
| complete | 2 | 0.990 | 95.00 | 0.013 | 1.98 | ATTRACTOR DOMINATED |
| unphysical clustered | 4 | 0.333 | 1.00 | 0.251 | 1.33 | NOT DOMINATED |
| D96-3D (4×4×6 torus) | 13 | 0.208 | 1.00 | 0.490 | 2.71 | NOT DOMINATED |
| **D96** | 45 | 0.062 | 1.20 | 0.824 | 2.81 | NOT DOMINATED |
| physical max-sep | 49 | 0.021 | 1.00 | 0.851 | 1.02 | NOT DOMINATED |
| unphysical band-gap | 49 | 0.021 | 1.00 | 0.851 | 1.02 | NOT DOMINATED |
| unphysical octave | 49 | 0.021 | 1.00 | 0.851 | 1.02 | NOT DOMINATED |
| random-sparse | 96 | 0.010 | 1.00 | 1.000 | 1.00 | NOT DOMINATED |

D96-3D = the 3D analogue (4×4×6 periodic torus), 96 vertices.

---

## Critical answers

1. **Does D96 produce fewer attractors than random?** YES — A(D96)=45 vs A(random)=96.
2. **Does one attractor dominate?** NO for D96 (largest basin 6.2%); only the degenerate
   complete graph dominates (D=0.99).
3. **Does dominance increase with spectral organization?** YES, weakly — D: complete
   0.99 > D96 0.062 > random 0.010; and D96-3D (A=13) compresses *more* than 1D D96 (A=45).
4. **Are physical spectra more dominant than unphysical?** NOT uniformly — unphysical
   clustered has A=4 (fewer, larger basins) than physical D96 (A=45); and physical
   max-separated is *identical* (A=49) to unphysical band-gap/octave, since the basin
   structure is set by eigenvalue degeneracy (symmetry), not physical realizability.

---

## Key findings

1. **Attractor structure = distinct eigenspaces (DERIVED).** Basins are multiplicity
   fractions; lock-to-max trajectory sampling *concentrates* them (D96 largest sampled
   basin ≈ 0.22) but still below a majority.

2. **Spectral organization compresses the attractor COUNT (EMERGENT).** D96 (A=45) and
   D96-3D (A=13) have far fewer distinct attractors than a random graph (A=96) — but the
   compression is **degeneracy**, not dominance.

3. **"Few attractors dominate" is REFUTED for D96.** Its largest basin is 6% (the
   doublet/octave structure keeps basins even). Only maximal degeneracy (complete graph)
   yields D>0.5.

4. **Dominance is partly a model artifact (CONFIRMED).** "Fewest attractors" even favors
   unphysical clustered spectra (A=4); physicality (T_002) does not distinguish
   max-separated from band-gap (both A=49).

---

## Classification

| Item | Classification |
|---|---|
| Attractor structure = distinct eigenspaces (multiplicity basins) | DERIVED |
| Spectral organization compresses attractor count | EMERGENT |
| "A small number of attractors DOMINATE the state space" | REFUTED (for D96) |
| Dominance is partly an artifact of the model definition | CONFIRMED |

---

## Conclusion

Spectral organization (D96) **compresses** the attractor count (45, and 13 in 3D, vs ~96
random), but does **not** by itself produce a single dominant attractor — D96's largest
basin is ~6%, because its octave/doublet degeneracy spreads the state space evenly. Strong
dominance (D>0.5) requires massive degeneracy (complete graph), and "fewest attractors"
can even favor unphysical clustered spectra. AT's "few dominant attractors" is therefore
**not a bare spectral fact**: it reflects the Darwinian selection (resource-competition)
layer — a mechanism beyond the eigenmode structure — exactly as the question suspected.
