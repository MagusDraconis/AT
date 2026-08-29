# Y_D_024_Result.md — ResearchY-D_024 Doublet Compatibility Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_024_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_024"`

---

## Summary

**Question:** Why does SU(2) attach to spectral doublets? Is the doublet shape uniquely
compatible with weak-isospin?

**Verdict:** **The doublet shape is NECESSARY but NOT SUFFICIENT for weak-isospin.** SU(2)
irreps come in every dimension 2j+1 (j = 0, ½, 1, …); the spectral doublet (2D) is
compatible with the fundamental j = 1/2 rep (weak-isospin fermions, T₃ = ±1/2), but the
same 2D space hosts SO(2) and O(2), and the D96 5-fold/6-fold groups are SU(2) carrier
spaces too (j = 2, j = 5/2). The weak-isospin attachment to doublets is the **EMERGENT**
choice of the fundamental rep, not a unique consequence of the doublet shape.

## Compatibility Table

| Spectral multiplet | SU(2) irrep | Weak-isospin compatible? |
|---|---|---|
| singlet (1D, zero mode) | j = 0 | **NO** (trivial, T₃ = 0) |
| **doublet (2D)** | **j = 1/2** | **YES** (fundamental, T₃ = ±1/2) |
| triplet (3D) | j = 1 | NO (adjoint) |
| quadruplet (4D) | j = 3/2 | NO |
| quintuplet (5D) | j = 2 | NO |
| sextuplet (6D) | j = 5/2 | NO |

## Key measured values

| Quantity | Value |
|---|---|
| SU(2) irrep dims | 1, 2, 3, 4, 5, 6 (every integer = 2j+1) |
| D96 spectral multiplicities | 42×2 + 5 + 6 |
| The only weak-isospin fundamental | j = 1/2 (dim 2) |
| 2D space also hosts | SO(2), O(2), SU(2) (D_022/D_023) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_024_SU2Dims` | SU(2) irreps have dim 2j+1 for every half-integer j | ✅ |
| `Y_D_024_D96Multiplicities` | D96: 42×2 + 5 + 6 — all SU(2) dims | ✅ |
| `Y_D_024_DoubletCompatible` | doublet (2D) = fundamental j = 1/2 — compatible | ✅ |
| `Y_D_024_NotUnique` | 2D hosts SO(2)/O(2)/SU(2); 5/6-fold are SU(2) dims — not unique | ✅ |
| `Y_D_024_CompatibilityTable` | singlet/triplet/quadruplet/quintuplet/sextuplet NOT weak-isospin | ✅ |
| `Y_D_024_Verdict` | doublet necessary but not sufficient; attachment EMERGENT | ✅ |
| `Y_D_024_Run` | Research report | ✅ |

## Conclusion

**The doublet shape is necessary but NOT sufficient for weak-isospin.** SU(2) irreps come
in every dimension 2j+1; the spectral doublet (2D) is compatible with the fundamental
j = 1/2 rep (weak-isospin fermions), but the same 2D space hosts SO(2) and O(2), and the
D96 5-fold/6-fold groups are SU(2) carrier spaces too. The weak-isospin attachment to
doublets is the **EMERGENT** choice of the fundamental rep, not a unique consequence of
the doublet shape. No canonical value was changed.
