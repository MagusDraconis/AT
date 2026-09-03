# Y_NP_036_Result.md — ResearchY-NP_036 3D Emergence Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_036_Tests.cs`
**Run:** 2026-09-03
**Result:** ✅ 9/9 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_036"`

---

## Summary

**Question:** Can observed 3D physics emerge from multiple D96 structure sectors? Is
the observed 3D blackbody DOS (N(ω) ∝ ω³, g ∝ ω²) naturally explained as
D96⊗D96⊗D96?

**Verdict: the ω³ DOS IS reproduced by D96⊗D96⊗D96 — as a hosted CORRESPONDENCE, NOT
an EMERGENT consequence of a single D96 sector.** Three independent D96 coordinates
suffice for N(ω)∝ω³ (separable tensor eigenvalues → ω≈c|k|, k∈Z³), and the Weyl count
matches the blackbody/3D-cavity (π/6)R³. But canonical AT is ONE 1D ring, and the
metric ansatz is dimension-generic (d≥3 only). The hidden triple A = 95·44·87 cubed
to M_Pl = v·A³ is a frequency-content triple, not three spatial axes.

## DOS exponents (Section 1)

| Construction | axes | DOS exponent p |
|---|---|---|
| D96 | 1 | p = 1 (octave doubling) |
| D96⊗D96 | 2 | p → 2 (2D Weyl) |
| D96⊗D96⊗D96 | 3 | p → 3 (3D Weyl) |

## Comparison with blackbody/free-field/3D cavity (Section 2)

D96⊗3 DOS g∝ω², cumulative N∝ω³, Weyl coefficient (π/6)R³ — identical to the
blackbody and 3D-cavity spectrum. 2D would be (π/4)R² (wrong law). Stefan-Boltzmann
integral = π⁴/15 = 6.4939 ✓.

## Sufficiency and correspondence (Sections 3–4)

Three independent D96 coordinates are minimally sufficient for N(ω)∝ω³ (separable
eigenvalues Λ=λ_k1+λ_k2+λ_k3, linear per-axis branches, 3D ball count). "One D96 axis
× three" reproduces the 3D Weyl DOS. But canonical AT is ONE ring (p=1 at every N,K);
the metric g=ρ^(2/d)η is dimension-generic (only d≥3 derived, QG290) — three copies
must be hosted. ⇒ CORRESPONDENCE, not EMERGENT.

## Hidden triple-factor structure (Section 5)

A = Σm·#g·occ₂ = 95·44·87 = 363,660 (three spectral counts of the single ring);
M_Pl = v·A³ = 1.2234e19 GeV with cube exponent 3.0000 (QG181/183). A frequency-content
triple — NOT three spatial axes. Three octave families [4,4,87] likewise octave bands
within one coordinate.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_036_SingleRingExponent` | single D96: p=1 | ✅ |
| `Y_NP_036_TensorTwoExponent` | D96⊗2: p→2 | ✅ |
| `Y_NP_036_TensorThreeExponent` | D96⊗3: p→3 | ✅ |
| `Y_NP_036_BlackbodyDosMatch` | 3D (π/6)R³ = cavity; SB = π⁴/15 | ✅ |
| `Y_NP_036_ThreeCoordinatesSufficient` | three axes → ω³ | ✅ |
| `Y_NP_036_ThreeAxesCorrespondToSpace` | one D96 axis × three = 3D DOS | ✅ |
| `Y_NP_036_HiddenTripleFactorStructure` | A = 95·44·87; M_Pl = v·A³ | ✅ |
| `Y_NP_036_Classification` | DERIVED/CORRESPONDENCE/FALSIFIED | ✅ |
| `Y_NP_036_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| DOS exponent p=d of d-fold D96 tensor | **DERIVED** (Weyl count) |
| three independent D96 coordinates → N∝ω³ | **DERIVED** (as a construction) |
| observed 3D DOS as D96⊗3 | **CORRESPONDENCE** (hosted 3D geometry; NP_028/034/035) |
| 3D EMERGING from a single D96 sector | **FALSIFIED** (single ring p=1) |
| spatial dimension d=3 as a canonical output | **FALSIFIED** (metric dimension-generic, d≥3) |
| hidden triple A = 95·44·87 → M_Pl = v·A³ | **DERIVED** (QG181/183) |

## Conclusion

The observed 3D DOS is naturally explained as D96⊗D96⊗D96 only as a hosted 3D
construction (CORRESPONDENCE), not as an emergent product of a single D96 sector.
Three independent D96 coordinates are sufficient for N(ω)∝ω³; the Weyl count matches
the blackbody. No new primitive; canonical AT unchanged.
