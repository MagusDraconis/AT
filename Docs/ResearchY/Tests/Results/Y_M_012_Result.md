# Y_M_012_Result.md — ResearchY-M_012 Network O(3) Symmetry Audit

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_012_Tests.cs`
**Run:** 2026-09-08
**Result:** ✅ 11/11 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_012"`

---

## Summary

**Question:** Can effective O(3), spherical symmetry, or shell degeneracies emerge from a
NETWORK of coupled D96 systems (single D96 verdict from M_011: NO)?

**Verdict:** **EMERGENT for the genuine 3D (vector/p-wave, l = 1) sector; REFUTED for exact
O(3) and every higher spherical observable.** The three-axis network D96⊗D96⊗D96 (cubic
lattice) has axis point group O_h (order 48; irreps {1,1,2,3,3,1,1,2,3,3}), which contains
genuine 3-dimensional irreps. Spherical-harmonic subduction keeps l = 0 (1) and l = 1 (3,
T₁u) fully degenerate, so a genuine 3D irreducible sector — the p-wave triplet, degeneracy
3 = 2l+1 — EMERGES from three orthogonal coupled rings. But O_h is a finite subgroup of
O(3): the full (2l+1) ladder, d/f/g degeneracies (split 2+3, 1+3+3), shell closures and
magic numbers, π as a value, and the exact Bekenstein quarter remain REFUTED.

## Key verified facts

| Item | Verified result |
|---|---|
| triad automorphism content | C96□C96□C96 ⊇ D96³ ⋊ S₃, order 192³·6 = 42,467,328 |
| axis point group | signed permutations on 3 axes = O_h, order 48 |
| O_h irreps | dims {1,1,2,3,3,1,1,2,3,3}, Σd² = 48; 10 conjugacy classes = 10 irreps; genuine 3D irreps exist |
| vector/p-sector | defining rep of O_h on ℝ³ irreducible (⟨χ,χ⟩ = 1) — genuine 3D irrep |
| spherical subduction | character norms²: l=0 → 1, l=1 → 1 (T₁u, full 3), l=2 → 2 (2+3), l=3 → 3 (1+3+3), l=4 → 4 |
| joint spectrum (λ_a+λ_b+λ_c) | 16,080 distinct levels over 96³ states; first excited level degeneracy 6 = {±x̂,±ŷ,±ẑ}; low degeneracies {6,12,8} = O_h orbit sizes |
| p-wave triplet | 3 = 2l+1 (l=1) is the only exact odd spherical sector; no O_h irrep of dim 5 or 7 |
| appearance of π | joint spectrum algebraic (sums of algebraic λ_k); no joint level = π/2π/4π/3 (min |E−π| = 0.095); role EMERGENT, value BOUNDARY (B_002) |
| horizon-area scaling | S ∝ A structure EMERGENT (QG185); exact quarter 1/4 REFUTED (needs imported 2π, QG196) |
| nuclear shell closures | only s,p (1,3) survive octahedral symmetry; magic numbers NOT reproduced (NP_087/088/109) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_M_012_AutomorphismGroups` | Aut content D96³⋊S₃ (42,467,328); point group O_h order 48 | ✅ |
| `Y_M_012_OhIrreps` | O_h irrep dims {1,1,2,3,3,…}; Σd²=48; 10 classes/irreps | ✅ |
| `Y_M_012_VectorSector3D` | defining vector rep irreducible (genuine 3D sector) | ✅ |
| `Y_M_012_SphericalSubduction` | l=0,1 full; l=2 → 2+3; l=3 → 1+3+3 | ✅ |
| `Y_M_012_JointSpectrum` | 16,080 joint levels; first-excited degeneracy 6 = O_h orbit | ✅ |
| `Y_M_012_PWaveTriplet` | p-triplet (3 = 2l+1, l=1) only exact odd sector; no dim 5/7 | ✅ |
| `Y_M_012_PiAppearance` | π value absent (algebraic joint spectrum) | ✅ |
| `Y_M_012_HorizonArea` | S∝A structure; exact 1/4 REFUTED | ✅ |
| `Y_M_012_NuclearClosures` | magic numbers not reproduced | ✅ |
| `Y_M_012_Classification` | verdict table | ✅ |
| `Y_M_012_Run` | research report | ✅ |

## Conclusion

A network of three coupled D96 rings on orthogonal axes supplies the single genuine 3D
irreducible sector the single ring lacks — the vector / p-wave (l = 1) triplet, an exact
O_h irrep with degeneracy 3 = 2l+1. Everything beyond it fails: exact O(3) is a finite-group
impossibility (O_h ⊂ O(3), order 48); the (2l+1) ladder, d/f/g degeneracies, shell
closures/magic numbers, π as a value, and the Bekenstein quarter are all REFUTED. This
refines NP_088/NP_089 (crystal-field-like, octahedral content) and confirms M_011's
single-ring NO without reclassification. No new primitive; canonical AT unchanged.
