# ResearchY-D_016 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_016_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~45 ms)
**Filter:** `FullyQualifiedName~Y_D_016`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_016_RingScan` | 61 rings with 3 families (N∈[60,120]); N=96 not unique | ✅ |
| `Y_D_016_Mod6` | 6|N not necessary (N=61..65 give 3 families) | ✅ |
| `Y_D_016_SpanScan` | span ∈ [4,8) window; varies smoothly (60→96→120) | ✅ |
| `Y_D_016_ThreeFamilyCondition` | family count=3 ⟺ span∈[4,8) (DERIVED identity) | ✅ |
| `Y_D_016_Counterexamples` | N=64, 90, 120 → 3 families; N=128 → 4 | ✅ |
| `Y_D_016_Classification` | A/B/D SELECTION RULE; C DERIVED | ✅ |
| `Y_D_016_Run` | Research report | ✅ |

## Classification

| Item | Status |
|---|---|
| A) N=96 | **SELECTION RULE** (one of 61 three-family rings) |
| B) divisibility by 6 | **SELECTION RULE** (3-family rings exist without it) |
| C) span ∈ [4,8) | **DERIVED** (the equivalence with family count = 3) |
| D) family count = 3 | **SELECTION RULE** (the 3-family window is a choice) |

## Conclusion

**family count = 3 ⟺ span ∈ [4,8)** is a DERIVED mathematical identity. The 3-family
window, 6|N, and N=96 are **SELECTION RULES** — scanning finds 61 rings with 3 families
(N ∈ [60,120]); N=96 is one of 11 with both 6|N and 3 families, selected by the
additional D96 criteria (D_015), not by the family count alone. **No canonical value was
changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_016"
```
