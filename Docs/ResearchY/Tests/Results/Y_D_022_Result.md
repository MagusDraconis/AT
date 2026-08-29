# Y_D_022_Result.md — ResearchY-D_022 Weak-Isospin Entry Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_022_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_022"`

---

## Summary

**Question:** Is weak-isospin an emergent reading of oscillation-derived Z2 symmetry?
Where does weak-isospin enter?

**Verdict:** **Weak-isospin is NOT the oscillation-derived Z2.** The oscillation Z2
(phase inversion) and the spectral Z2 (λ_k = λ_{N−k}) are **DERIVED** — they exist at
every ring size (N=32..192) with no gauge sector. The weak-isospin Z2 is the **SU(2)
gauge structure** — an **independent input (BOUNDARY)**. The {cos, sin} spectral doublet
is a 2D real **SO(2)/parity doublet** (det-1 rotations, parity {even, odd}), NOT an SU(2)
rep. Only the doublet **shape** is the **EMERGENT** reading of the spectral pairs.
Classification: weak-isospin **C) independent input**; doublet reading **B) EMERGENT**.

## Key measured values

| Quantity | Value |
|---|---|
| Oscillation Z2 (phase inversion) | universal — holds for any single mode, any N |
| Spectral Z2 (λ_k = λ_{N−k}) | holds for all k at N=32..192 |
| Oscillation Z2 without weak-isospin | **YES** — full doublet structure at all N, no gauge |
| Weak-isospin without spectral Z2 | YES (formally) — SU(2) is a gauge group |
| {cos, sin} rotation rep | 2×2 det-1 matrix = **SO(2)**, not SU(2) |
| Spectral doublet | parity doublet {cos even, sin odd} |
| Weak-isospin doublet | SU(2) fundamental rep (T₃ = ±1/2) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_022_Z2Separation` | oscillation/spectral/weak-isospin Z2 are three distinct objects | ✅ |
| `Y_D_022_NoWeakIsospin` | oscillation-derived Z2 exists at N=32..192 without gauge sector | ✅ |
| `Y_D_022_NoSpectralZ2` | SU(2) doublet writable without spectral degeneracy | ✅ |
| `Y_D_022_NotSU2Rep` | {cos, sin} transforms as SO(2) (det-1), not SU(2) | ✅ |
| `Y_D_022_ParityDoublet` | spectral pair is a parity doublet {even, odd} | ✅ |
| `Y_D_022_Verdict` | weak-isospin = independent input (C); doublet reading EMERGENT (B) | ✅ |
| `Y_D_022_Run` | Research report | ✅ |

## Conclusion

**Weak-isospin is C) an independent input (SU(2) gauge structure), and the weak-isospin
reading of the oscillation-derived doublets is EMERGENT.** The oscillation Z2 and
spectral Z2 are DERIVED (they exist at every ring size); the {cos, sin} spectral doublet
is an SO(2)/parity doublet, not an SU(2) rep. Only the doublet shape is the emergent
reading; the SU(2) gauge algebra is a BOUNDARY input. No canonical value was changed.
