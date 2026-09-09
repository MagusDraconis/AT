# ResearchY-T_012 — Origin of 19 Audit

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_012 (permanent)
**Title:** Origin of 19 — can the historical ~19 species emerge from D96^3 rather than D96?
**Status:** COMPLETE
**Date:** 2026-09-09
**File:** `T_SpectralBlueprint/ResearchY-T_012.md`
**Depends on:** ResearchY-T_007–T_011; NP_037/NP_088 (D96 ⊗ D96 ⊗ D96 tensor product, O_h
symmetry); legacy AT_138/AT_139 (~19 stable species)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_012_Tests.cs` (6/6 ✅)
**Core analyzer:** `AT.Core/ResearchT/BoundedInnovationAnalyzer.cs` (new `RunDistinct`);
`AT.Tests/Shared/SpectralCaseCatalog.cs` (new `D96Cubed`)

---

## Question

T_011 refuted that ~19 emerges from the 1D D96 ring (it gives ~5). T_012 asks the natural
follow-up: **can ~19 emerge from D96^3 — the cubic tensor product D96 ⊗ D96 ⊗ D96 (NP_037/NP_088)
— rather than from D96?**

The cubic lattice has octahedral (O_h) symmetry with irreps of dimension {1, 2, 3}. Its modes
decompose into axis-count **sectors** (1-axis / 2-axis / 3-axis) and permutation-orbit **irrep
classes** (A = {a,a,a}, T = two-equal, G = all-distinct). Measure survivors and cumulative
discovery on the full 3D spectrum and each sub-spectrum; test whether 19 emerges naturally.

## Method

The Cartesian-product spectrum is the Minkowski sum Λ = λ_i + λ_j + λ_k of three 1D D96 spectra,
with multiplicity the product of the 1D multiplicities. Built exactly from the reduced 1D indices
(r ∈ 0..48, multiplicity 1 for r ∈ {0,48}, else 2). Run the replicator–mutator (fitness w = m/λ,
μ = 0.01, β = 1) on the full spectrum and the sector/irrep sub-spectra. Deterministic.

---

## Results

### Landscape inventory

| model | modes | distinct non-zero eigenvalues | S∞ (μ=0.01, β=1) |
|---|---|---|---|
| D96 (1D) | 96 | 44 | **5** |
| D96^3 (cubic ⊗) | 884,736 | 20,811 | **16** |
| random control | 96 | 95 | **17** |
| target | — | — | **19** |

### Sector splitting (axis count)

| sector | distinct eigenvalues | S∞ |
|---|---|---|
| 1-axis (axial) | 46 | 5 |
| 2-axis (planar) | 1,038 | 5 |
| 3-axis (cubic) | 19,727 | **21** |

### Oh irrep (permutation-orbit) classes

| irrep class | distinct eigenvalues | S∞ |
|---|---|---|
| A ({a,a,a}) | 46 | 5 |
| T (two equal) | 2,339 | 6 |
| G (all distinct) | 18,574 | 31 |

### Cumulative discovery (fittest-init, D96^3)

cumulative = 8, survivor = 8.

### Distance from 19 (no tuning)

| | D96 | D96^3 | random |
|---|---|---|---|
| S∞ | 5 | 16 | 17 |
| distance from 19 | 14 | **3** | **2** |

---

## Findings

1. **D96^3 substantially raises diversity toward 19.** The 3D density of states lifts the
   survivor count from D96's 5 to **16** — a DERIVED consequence of the cubic spectrum's far
   denser low-lying structure (20,811 distinct eigenvalues).

2. **But 19 is not hit exactly.** D96^3 gives 16 (distance 3); the cubic 3-axis sector gives 21
   (distance 2); the random control gives 17 (distance 2) — still the nearest natural value.

3. **The match is a CORRESPONDENCE, not a derivation.** 16 is within the legacy ±5 consistency
   tolerance (AT-139's own criterion), so D96^3 *order-of-magnitude* reproduces the historical
   value — but so does random (17), and the value is not pinned.

## Classification

| Item | Classification |
|---|---|
| D96^3 spectrum (20,811 eigenvalues) and S∞ = 16 are deterministic (T_008/T_009) | DERIVED |
| The raise 5 → 16 from the 3D density of states | DERIVED |
| D96^3 ≈ 19 within the legacy ±5 tolerance | CORRESPONDENCE |
| The exact value 16 (or 21 in the cubic sector) | EMERGENT |
| "D96^3 exactly derives 19 without tuning" | REFUTED |

---

## Conclusion

D96^3 does **not** cleanly reproduce the historical ~19 species — but it gets much closer than
the 1D D96 ring. The cubic tensor product raises the survivor count from 5 to **16** (distance 3
from 19), a DERIVED effect of the 3D density of states, and the cubic 3-axis sector alone gives
21 (distance 2). The value 16 is within the legacy ±5 tolerance, so D96^3 **corresponds** to the
historical ~19 at order-of-magnitude, but it does not **derive** 19: the random control (17)
remains the nearest natural value, and the exact integer is EMERGENT, not pinned. The historical
"~19 stable species" therefore remains ambiguous between D96^3 (16), its cubic sector (21), and a
random-like landscape (17) — none reproduces 19 without parameter tuning. No new primitive;
canonical AT unchanged.
