# ResearchY-T_013 — Compression Origin Audit

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_013 (permanent)
**Title:** Compression Origin — why does D96^3 compress 16,079 eigenvalues into 16 survivors?
**Status:** COMPLETE
**Date:** 2026-09-09
**File:** `T_SpectralBlueprint/ResearchY-T_013.md`
**Depends on:** ResearchY-T_007–T_012; NP_037/NP_088 (D96 ⊗ D96 ⊗ D96)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_013_Tests.cs` (5/5 ✅)
**Shared:** `AT.Tests/Shared/SpectralCaseCatalog.cs` (D96CubedBreakdown — fixed doublet folding)

---

## Question

Why does D96^3 compress **16,079** distinct eigenvalues into **16** survivors? Derive the
compression ratio `C = eigenvalues / survivors`.

## Method

Measure, per landscape (D96 / D96^3 / random): fitness distribution (concentration w\*/Σw,
entropy reduction ln A − H), multiplicity hierarchy (max multiplicity), near-gap density
(modes with λ ≤ 2λ₂), and survivor basin volume (survivor multiplicity / total modes). Then
`C = A/S∞`. Deterministic (μ = 0.01, β = 1).

## Results

| model | A | S∞ | C = A/S∞ | conc | entropy reduction | near-gap | max mult | basin |
|---|---|---|---|---|---|---|---|---|
| D96 | 44 | 5 | 8.8 | 0.362 | **0.88** | 2 | 6 | 0.105 |
| D96^3 | 16,079 | 16 | **1004.9** | 0.0008 | 0.16 | 18 | 738 | 0.0025 |
| random | 95 | 17 | 5.6 | 0.017 | 0.03 | 71 | 1 | 0.179 |

---

## The mechanism

1. **A is huge for D96^3 (the 3D sum structure).** The distinct eigenvalues are the 3-way sums
   of the 49 reduced 1D values, giving 16,079 distinct eigenvalues — vs 44 (D96) and 95
   (random).

2. **The survivors are the near-gap modes.** D96^3 has 18 modes within 2λ₂ (octahedral
   degeneracy), and S∞ = 16 — the survivors *are* essentially the near-gap modes; the other
   ~20,000 eigenvalues go extinct under `w = m/λ` selection.

3. **Compression is dominated by A, not fitness peakedness.** The 1D D96 has the *highest*
   entropy reduction (0.88 — the most peaked fitness) yet compresses *least* (C = 8.8);
   D96^3 has a *flatter* fitness (entropy reduction 0.16) but compresses ~114× more (C = 1004.9)
   because its 3D sum structure creates an astronomically larger landscape A.

## Classification

| Item | Classification |
|---|---|
| C = A/S∞ is a deterministic function of the spectrum (A = distinct-sum count; S∞ = reach ≈ near-gap density, T_008/T_009) | DERIVED |
| The value C ≈ 1005 (D96^3), ~8.8 (D96), ~5.6 (random) | EMERGENT |
| "C is set by fitness peakedness (entropy reduction)" | REFUTED — D96 is most peaked yet compresses least |

---

## Conclusion

D96^3 compresses 16,079 → 16 because its 3D sum structure generates a **huge landscape** A
(~20k distinct eigenvalues) while selection `w = m/λ` keeps only the **near-gap modes**
(~16 of the 18 within 2λ₂). The compression ratio `C = A/S∞` is **DERIVED** as a deterministic
function of the spectrum, but its value (1004.9) is **EMERGENT** and dominated by A — *not* by
fitness peakedness, which is **REFUTED** as the driver (D96 is the most peaked yet compresses
least). The octahedral multiplicity hierarchy (max mult 562) concentrates the near-gap modes
into a few eigenvalues, and the survivor basin is a tiny 0.19% of the landscape. No new
primitive; canonical AT unchanged.
**Correction (ResearchY-G_034, 2026-09-13).** The D96³ spectrum's degeneracy was keyed on EXACT double
equality, which split physically degenerate 3-way sums and made A implementation-dependent (convention
20,811 under .NET vs 20,440 under Python for the same IEEE-754 algorithm; one ulp moves it by ~900).
Clustering at a tolerance (the rule D_047 documents and applies) gives **A = 16,079, C = 1004.9,
max multiplicity 738** — the figures now shown above. Every CONCLUSION here is unchanged (C is still a
deterministic function of the spectrum and is still dominated by A, not by fitness peakedness); only the
quoted value moved, from 1300 to 1005, and the "~150×" ratio became ~114×.

