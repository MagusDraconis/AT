# ResearchY-T_003 — General Inverse Spectral Graph Audit

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_003 (permanent)
**Title:** General Inverse Spectral Graph Audit — does the spectrum determine the graph?
**Status:** COMPLETE
**Date:** 2026-09-08
**File:** `T_SpectralBlueprint/ResearchY-T_003.md`
**Depends on:** ResearchY-T_001 (inverse spectral design), ResearchY-T_002 (physicality)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_003_Tests.cs` (5/5 ✅)
**Core analyzer:** `AT.Core/ResearchT/GeneralInverseSpectrumAnalyzer.cs`

---

## Question

Can a target spectrum reconstruct a physical **non-circulant** weighted graph? Does the
spectrum uniquely determine the graph?

**Scope constraint:** No AT assumption beyond `Laplacian ↔ spectrum`. The graph Laplacian
is a symmetric matrix; its spectrum is the sorted multiset of eigenvalues.

---

## Method

For six graph families — path, cycle, grid, random sparse, complete, D96-derived — we
(1) compute the Laplacian spectrum; (2) reconstruct the Laplacian from its **full
eigendecomposition** `L = VΛVᵀ`; (3) search **exhaustively** over all labeled graphs on
n=5 and n=6 for isospectral non-isomorphic pairs (identical spectrum, different degree
sequence — a degree-sequence difference proves non-isomorphism); (4) contrast with the
circulant case, where the labeled spectrum ↔ coupling map is bijective (T_001/T_002).

Deterministic throughout; no fitted parameters.

---

## Results

### 1. Six graph cases

| Case | Edges | Sparsity | Cond (λ_max/λ₂) | Spectr. err | Graph sim | Isospectral partner |
|---|---|---|---|---|---|---|
| path P6 | 5 | 0.667 | 13.93 | 1.0e-15 | 1.00 | no |
| cycle C6 | 6 | 0.600 | 4.00 | 1.4e-15 | 1.00 | no |
| grid 2×3 | 7 | 0.533 | 5.00 | 8.9e-16 | 1.00 | no |
| random sparse | 9 | 0.400 | 3.95 | 2.0e-15 | 1.00 | no |
| complete K6 | 15 | 0.000 | 1.00 | 5.3e-15 | 1.00 | no |
| D96-derived (n=96) | 576 | 0.874 | 40.99 | 3.4e-14 | 1.00 | (n/a — too large) |

Every case reconstructs **exactly** from its full eigendecomposition (spectral error
~1e-15, graph similarity 1.00): `L = VΛVᵀ` is a linear-algebra identity that loses
nothing.

### 2. Isospectral search

- **n=5:** 0 isospectral pairs — every 5-vertex graph is uniquely determined by its
  Laplacian spectrum.
- **n=6:** 4 distinct isospectral non-isomorphic pairs, grouped in 2 degree-signature
  families:
  - `(2,2,2,2,2,4) ↔ (1,2,2,3,3,3)` — 7 edges (two distinct spectra)
  - `(1,3,3,3,3,3) ↔ (2,2,2,3,3,4)` — 8 edges (two distinct spectra)

Each pair has **identical** Laplacian spectrum but **different** degree sequences, hence
is provably non-isomorphic. **The spectrum alone does not determine a general graph.**

### 3. Circulant rigidity (contrast)

Cycle C6 and D96 (C96(±1..±6)) are circulant: their *labeled* spectrum reconstructs the
connection set uniquely via the inverse DFT (`{±1}` and `{±1..±6}` respectively). For
rings the spectrum **is** a complete blueprint; for general graphs it is lossy.

---

## Key findings

1. **Eigendecomposition suffices (DERIVED).** `L = VΛVᵀ` reconstructs any graph exactly;
   spectrum + eigenbasis loses nothing.

2. **Spectrum alone is lossy for general graphs (DERIVED).** Isospectral non-isomorphic
   pairs exist (n=6): the sorted eigenvalue multiset is not a complete graph invariant.

3. **Spectral rigidity is special (CORRESPONDENCE).** Circulant (ring) graphs are
   spectrally rigid — the labeled spectrum is bijective with the coupling. The six named
   families (path, cycle, grid, random, complete) are each rigid among their size, but
   rigidity is not generic: arbitrary 6-vertex graphs are spectrally degenerate.

---

## Classification

| Item | Classification |
|---|---|
| Eigendecomposition (spectrum + eigenbasis) reconstructs L exactly | DERIVED |
| Spectrum alone does not determine a general graph | DERIVED |
| Circulant graphs are spectrally rigid | CORRESPONDENCE |
| "Spectrum as complete blueprint" | REFUTED for general non-circulant graphs; SUPPORTED for circulant (D96) structure |

---

## Conclusion

A general graph is **not** determined by its spectrum: isospectral non-isomorphic graphs
exist (none on n=5, four on n=6). The spectral blueprint of T_001/T_002 is therefore a
**special** property of circulant (ring) structure, for which the labeled spectrum is
bijective with the coupling. This is precisely why AT's D96 is a canonical attractor: its
circulant geometry makes the spectrum a complete and unique description, whereas a generic
graph's spectrum is an incomplete (degenerate) description. Full reconstruction requires
the eigenbasis in addition to the spectrum.
