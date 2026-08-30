# Y_QG_008_Result.md — ResearchY-QG_008 Finite Distinguishability Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_008_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_008"`

---

## Summary

**Question:** Why must distinguishability be finite?

**Verdict:** Finite distinguishability is a **BOUNDARY** — required for physics (finite
information, well-defined normalization and measure) but not logically implied by
Difference.

## Finite vs infinite

| Property | Finite N (95) | Infinite N (∞) |
|---|---|---|
| normalization | Σρ = 1 well-defined | requires convergence |
| count conservation | clear | via a limit |
| geometry | √(−g) = ρ well-defined | needs a limit measure |
| information | log₂(95) = 6.57 bits | **log₂(N) → ∞ — DIVERGES** |

## First breakdown: INFORMATION

log₂(N) diverges as N → ∞ — the FIRST casualty. Normalization, geometry, and
measurement are second (limit assumptions).

## Determination

| Option | Verdict |
|---|---|
| A) finiteness required | PARTIAL — required for physics |
| B) finiteness emergent | NO |
| **C) finiteness boundary** | **YES** — the value 96 is derived; the finiteness is an input |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_008_FiniteStates` | finite N: normalization/geometry/info well-defined | ✅ |
| `Y_QG_008_InfiniteStates` | infinite N: information diverges | ✅ |
| `Y_QG_008_NormalizationLimit` | normalization needs a convergence assumption | ✅ |
| `Y_QG_008_CountConservation` | count conservation survives via a limit | ✅ |
| `Y_QG_008_GeometryLimit` | geometry needs a limit measure | ✅ |
| `Y_QG_008_InformationLimit` | information breaks first (log₂ N → ∞) | ✅ |
| `Y_QG_008_Run` | research report | ✅ |

## Conclusion

Finite distinguishability is a BOUNDARY — required for physics but not logically
implied by Difference (the value N=96 is derived; the finiteness is an input). With
infinite distinguishability, INFORMATION breaks first (log₂ N diverges); normalization,
geometry, and measurement are second. No new primitive; canonical AT unchanged.
