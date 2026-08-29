# Y_D_023_Result.md — ResearchY-D_023 SU(2) Entry Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_023_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_023"`

---

## Summary

**Question:** Where does SU(2) enter? Is SU(2) truly independent or can it emerge from a
deeper spectral structure?

**Verdict:** **SU(2) does NOT emerge from the spectral structure.** The oscillation and
reflection symmetries provide exactly **ONE continuous generator** (J, the SO(2)
rotation of the {cos, sin} eigenspace) plus **one discrete generator** (P, the
reflection) — an O(2)-type structure. SU(2) requires **THREE continuous non-Abelian
generators** (Pauli σₓ, σ_y, σ_z). The real skew-symmetric 2×2 matrices are
**1-dimensional** (only J); the missing generators iσₓ and iσ_z are complex and absent
from the real spectral structure. The D_n 2D irreps generate the Z2 doublets (O(2)-type)
but not SU(2). Removing SU(2) leaves all spectral content intact.
**Verdict: A) SU(2) = independent input (BOUNDARY)**; the doublet is the EMERGENT
attachment surface.

## Key measured values

| Quantity | Value |
|---|---|
| SO(2) generators | 1 continuous (J = [[0,−1],[1,0]]) |
| O(2) generators | 1 continuous + 1 discrete (P = diag(1,−1)) |
| SU(2) generators | 3 continuous non-Abelian (Pauli) |
| Real skew-symmetric 2×2 matrices | **1D** (only J) |
| Spectral continuous generators | **1** (needs 3) |
| D_n 2D irreps | O(2)-type real reps of a discrete group |
| {cos, sin} rotation rep | det-1 SO(2) matrix |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_023_SO2VsSU2` | {cos, sin} is SO(2) (1 generator); SU(2) needs 3 | ✅ |
| `Y_D_023_GeneratorCount` | spectral structure provides 1 continuous + 1 discrete; SU(2) needs 3 | ✅ |
| `Y_D_023_DoubletContent` | D_n 2D irreps are O(2)-type, not SU(2 | ✅ |
| `Y_D_023_RemovalTest` | removing SU(2) leaves spectral content intact | ✅ |
| `Y_D_023_DependencyTrace` | oscillation → Z2 → doublets → ? → SU(2): complexification new | ✅ |
| `Y_D_023_Run` | Research report | ✅ |

## Conclusion

**SU(2) is A) an independent input (BOUNDARY).** The spectral structure provides 1 of the
3 continuous generators; the missing two are complex and outside the real spectrum. The
D_n 2D irreps generate the doublets (O(2)-type) but not the SU(2) gauge algebra. The
doublet is the EMERGENT attachment surface onto which the BOUNDARY SU(2) gauge input
attaches. Removing SU(2) leaves all spectral content (doublets, families, moments,
standing-wave structure) intact. No canonical value was changed.
