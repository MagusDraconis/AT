# Y_D_021_Result.md — ResearchY-D_021 Oscillation Symmetry Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_021_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_021"`

---

## Summary

**Question:** Is complete Z2 pairing a consequence of oscillation symmetry (the ±
structure of standing waves) rather than weak-isospin?

**Verdict:** **The Z2 PAIRING is DERIVED from oscillation.** The pair
{cos(2πkn/N), sin(2πkn/N)} at each frequency ω_k is the two-quadrature structure of a
single real oscillation — both are eigenfunctions of L with the SAME λ_k, forced by the
spectral symmetry λ_k = λ_{N−k} (ring reflection). It is NOT a weak-isospin-only input;
the weak-isospin doublet reading is **EMERGENT** (D_014). Standing-wave completeness (a
complete Fourier basis) survives removal of Z2 pairing — completeness is a basis
property, pairing is a degeneracy property. Only the *completeness* of pairing (0
unpaired) is a **BOUNDARY** N-arithmetic selection (D_020).

## Key measured values

| Quantity | Value |
|---|---|
| +A ↔ −A | same mode, π phase offset — no pairing |
| cos(ωt) ↔ −cos(ωt) | half-period shift — no pairing |
| cos(2π(N−k)n/N) vs cos(2πkn/N) | **identical** (even) |
| sin(2π(N−k)n/N) vs sin(2πkn/N) | **−sin** (odd) — the mirror pair generator |
| λ_k = λ_{N−k} | **exact for all k** (e.g. λ₁=λ₉₅=0.3864) |
| L·cos = λ·cos, L·sin = λ·sin | **both eigenfunctions at the same λ_k** (k=1,3,47) |
| Standing-wave basis size | N=64→64, N=96→96, N=128→128 (complete, deg or not) |
| unpaired(64), unpaired(128) | 1, 1 (incomplete — N-arithmetic) |
| unpaired(96), unpaired(192) | 0, 0 (complete) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_021_OscillationSymmetry` | +A↔−A, cos↔−cos are per-mode phase gauges; k↔N−k pairs | ✅ |
| `Y_D_021_MirrorMode` | cos(N−k)=cos(k), sin(N−k)=−sin(k); λ_k=λ_{N−k} exact | ✅ |
| `Y_D_021_QuadraturePair` | cos and sin both eigenfunctions of L at same λ_k | ✅ |
| `Y_D_021_PairingDerived` | pairing is DERIVED (oscillation+spectral), not weak-isospin-only | ✅ |
| `Y_D_021_CompletenessSurvives` | standing-wave basis complete for all N (deg or not) | ✅ |
| `Y_D_021_CompletenessArithmetic` | 0-unpaired tracks N arithmetic, not oscillation | ✅ |
| `Y_D_021_Run` | Research report | ✅ |

## Conclusion

**Z2 pairing is the two-quadrature (cos/sin) structure of a single real oscillation** —
DERIVED from oscillation necessity and the ring's spectral symmetry λ_k = λ_{N−k}. The
weak-isospin doublet reading is EMERGENT. Standing-wave completeness survives removal of
Z2 pairing. Only the completeness of pairing (0 unpaired) is a BOUNDARY N-arithmetic
selection (D_020). No canonical value was changed.
