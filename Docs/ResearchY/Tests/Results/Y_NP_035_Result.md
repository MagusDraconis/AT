# Y_NP_035_Result.md — ResearchY-NP_035 Density-of-States Origin Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_035_Tests.cs`
**Run:** 2026-09-03
**Result:** ✅ 10/10 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_035"`

---

## Summary

**Question:** Why does the D96 mode set produce g_D96(ω) (1D-chain DOS, N(ω) ∝ ω,
p = 1) instead of the observed g_BB(ω) ∝ ω² (N ∝ ω³, p = 3)? Identify the exact
structural origin of the blackbody DOS mismatch.

**Verdict: DIMENSIONALITY.** The D96 ring is a 1D structure — its spectrum is indexed
by a single integer k ∈ [1, N−1], and its low-frequency dispersion is exactly linear,
ω_k = c·k with c = 2π√91/N. One integer mode index forces the 1D density of states:
N(ω) ∝ ω, p = 1. The blackbody ω² DOS (p = 3) is the density of a 3D mode-index
space — hosted higher-layer geometry, minimally produced by the 3D tensor product of
three D96 rings preserving the ±1..±6 local rule per axis.

## The analytic DOS (Section 1)

λ_k ≈ (2πk/N)²·91 ⇒ ω_k ≈ c·k (ratio 1.000 to 1e-3 at N = 6144, k = 1..4). Equal
frequency spacing ⇒ N(ω) ∝ ω, p = 1. Octave occupancy [4,4,87]: the low octaves hold
4 modes each (constant 1D density per unit ω); the top octave holds 87 because the
band ends at ω_max = 3.98, not because the DOS grows like ω².

## Origin determination (Sections 2–3)

| Candidate | Verdict |
|---|---|
| A) dimensionality | **ORIGIN** — one integer index k ⇒ p = 1 |
| B) topology | refuted — a 1D open chain also has p = 1 |
| C) finite mode count | refuted — p = 1 at N = 96..6144 |
| D) circulant structure | refuted — p = 1 for K = 1..12 |
| E) hosted higher-layer geometry | **YES for ω²** — 3D DOS is hosted content |

## Extensions and minimal construction (Sections 4–5)

- larger N (→6144): p = 1; larger K (→12): p = 1; coupled/longer rings: p = 1.
- tensor products: C_N^⊗2 → p ≈ 2; C_N^⊗3 → p ≈ 3 (adding an independent direction).
- minimal ω³ construction: C_96(±1..±6)^⊗3 (3D tensor of three D96 rings, same local
  rule per axis) → N(ω) ∝ ω³, DOS ∝ ω².

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_035_LowFrequencyDispersion` | ω_k ≈ c·k linear (1D chain limit) | ✅ |
| `Y_NP_035_SingleIndexDosExponent` | octave doubling p = 1 at N = 96..6144 | ✅ |
| `Y_NP_035_ExponentEqualsDimension` | 1D→1, 2D→2, 3D→3 lattice exponents | ✅ |
| `Y_NP_035_CirculantKFamilyIsOneD` | p = 1 for C_N(±1..±K), K = 1..12 | ✅ |
| `Y_NP_035_CoupledRingsRemainOneD` | coupled/longer rings keep p = 1 | ✅ |
| `Y_NP_035_TensorProductRaisesDimension` | C_N^⊗2 → p ≈ 2; C_N^⊗3 → p ≈ 3 | ✅ |
| `Y_NP_035_MinimalW3Construction` | 3D tensor of D96 rings → N ∝ ω³ | ✅ |
| `Y_NP_035_D96TopHeavinessIsFiniteBandEffect` | [4,4,87]: low octaves 4/4, top = cap | ✅ |
| `Y_NP_035_Classification` | A confirmed; B/C/D refuted; E hosted | ✅ |
| `Y_NP_035_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| 1D DOS of the D96 ring (p = 1) | **DERIVED** (single index, linear dispersion) |
| exponent p = number of mode indices | **DERIVED** (1D→1, 2D→2, 3D→3) |
| dimensionality as the origin | **DERIVED** (answer A CONFIRMED) |
| ω² DOS as hosted 3D geometry | **CORRESPONDENCE** (NP_028/034 unchanged) |
| any C_N(±1..±K) ring as blackbody DOS host | **FALSIFIED** (NP_032 unchanged) |
| finite count / topology / circulant as cause | **FALSIFIED** |
| C_N^⊗3 (same local rule) → N ∝ ω³ | **DERIVED** (minimal construction) |

## Conclusion

The D96 mode set produces a 1D DOS because it is a 1D ring with one integer mode
index and linear low-frequency dispersion. The blackbody ω² DOS requires three
independent mode indices and is hosted higher-layer content, minimally realized by the
3D tensor product of three D96 rings preserving the ±1..±6 local rule per axis.
Dimensionality — not topology, count, coupling range, or circulant symmetry — is the
exact structural origin of the blackbody DOS mismatch. No new primitive; canonical AT
unchanged.
