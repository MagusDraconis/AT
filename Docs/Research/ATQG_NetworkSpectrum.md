# AT-QG Phase 104 — Compute Network Spectrum

**Program:** AT-QG (Unification)
**Phase:** 104 — for a concrete causal network, what are the eigenvalues of the native network operator?
**Status:** COMPLETED — 3/3 xUnit tests pass (315/315 AT-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational audit of the native operator spectrum)

---

## 1. Goal

Compute the actual eigenvalues of the native network operators of a CONCRETE causal network and compare the
resulting spectrum against the known SM mass hierarchies. Classify: NO MATCH / PARTIAL MATCH / NUMERICAL
CORRESPONDENCE.

Concrete network: the deterministic 1+1D Minkowski causal-set grid (t ∈ [0,6], x ∈ [−6,6], 91 events,
Hasse-link edges) — the native (V,E) used throughout the G4 program.

---

## 2. Adjacency & graph Laplacian spectra (ATQG1040)

- **Adjacency A**: real spectrum, bipartite-symmetric (λ_min = −5.45, λ_max = +5.45, spectral radius ≤ max
  degree 6) — the Hasse graph is bipartite, so its adjacency spectrum is symmetric about 0.
- **Graph Laplacian L = D − A**: positive semi-definite, exactly one zero mode (connected), spectral gap
  λ_2 = 0.099, λ_max = 11.30. The native network operators genuinely possess computable spectra.

---

## 3. Actualization operator, stable modes, spectral ratios (ATQG1041)

- **Actualization operator Lc = ρ⁻¹Lρ⁻¹** (ρ = past-degree + future-degree, the causal counting measure,
  QG89): real, PSD, one zero mode (same connectivity as L), λ_max = 0.58.
- **Stable-mode frequencies ω = √λ**: 90 positive real modes, ω_1 = 0.315 … ω_max = 3.36, monotonically
  increasing — genuine normal-mode eigenfrequencies of the network dynamics ẍ = −L x.
- **Spectral ratios** (successive ω_k+1/ω_k): discrete, scale-free fingerprint ~1.0–2.0; spectral-hierarchy
  span ω_max/ω_min = 10.7 (more than a decade) — the spectrum IS hierarchical.

---

## 4. SM hierarchies vs network spectra (ATQG1042)

SM scale-free mass ratios (PDG 2022):
- Charged leptons: m_e/m_μ = 4.836e-3, m_μ/m_τ = 5.946e-2, m_e/m_τ = 2.876e-4; Koide Q = 2/3 (known hidden
  structure).
- Quarks (successive): d/u = 2.16, s/d = 20.0, c/s = 13.6, b/c = 3.29, t/b = 41.3.

Network spectra (computed):
- Best match vs leptons: relative error 15.8 (factor ~16 away).
- Best match vs quarks: relative error 8.6% (nearest but not < 1%).
- Numerical correspondence (< 1%): leptons NO, quarks NO.

---

## 5. Classification (ATQG1042)

**PARTIAL MATCH.**

- NOT NO MATCH: the network genuinely possesses discrete hierarchical spectra (span > 10, stable modes,
  spectral gaps) — a structural analogy to the SM hierarchy exists.
- NOT NUMERICAL CORRESPONDENCE: no specific network spectral ratio equals a SM mass ratio (m_e/m_μ ≈ 4.8e-3,
  m_μ/m_τ ≈ 5.9e-2) within 1% — the concrete un-tuned network does not reproduce the SM numbers.
- PARTIAL MATCH: hierarchical discrete spectrum + quantization (structural analogy), without numerical value
  determination — consistent with QG94/95 (spectra exist, mapping speculative).

---

## 6. Conclusion

The native network operators (adjacency, graph Laplacian, actualization operator ρ⁻¹Lρ⁻¹) of a concrete causal
network have genuine, computable, hierarchical spectra with stable-mode frequencies and spectral ratios. This
confirms — computationally, on a concrete network — the earlier QG94/95 conclusion: the spectrum is real and
quantization-like, but the mapping to specific SM mass ratios is **PARTIAL MATCH** (structural analogy), not a
numerical correspondence.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG1040 `ATQG1040_AdjacencyAndLaplacianSpectra` | PASS (real adjacency + PSD Laplacian spectra, gap 0.099) |
| ATQG1041 `ATQG1041_ActualizationOperatorModesAndRatios` | PASS (PSD Lc, 90 stable modes, span 10.7) |
| ATQG1042 `ATQG1042_SMHierarchyComparisonAndClassification` | PASS (PARTIAL MATCH) |

Code: `AT.Core/ResearchXH/NetworkSpectrum.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase104_NetworkSpectrumTests.cs`.
