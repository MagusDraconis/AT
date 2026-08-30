# Y_NP_021_Result.md — ResearchY-NP_021 Information Horizon Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_021_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 5/5 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_021"`

---

## Summary

**Question:** If information is conserved, where is it stored across a horizon?

**Verdict:** **HORIZON BOOKKEEPING** — storage + redistribution + encoding. State-space
expansion is refuted (the state space is fixed at 95, D_039).

## The mechanism

| Mechanism | Works? |
|---|---|
| storage (states retain distinguishability) | **YES** — D_039 |
| redistribution (radiation re-encodes) | **YES** — M_005 |
| encoding (hidden/accessible partition) | **YES** |
| state-space expansion | **NO** — state space fixed at 95 |

## The information balance

```
log₂(95) = 6.57 bits = H_hidden + H_observer
```

Conserved through actualization (M_005). Before the horizon the information lives in
the 95 states; after, it is partitioned (hidden/accessible) — the total is unchanged.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_021_PreHorizon` | information in the 95 states before | ✅ |
| `Y_NP_021_PostHorizon` | H = H_hidden + H_observer after | ✅ |
| `Y_NP_021_InformationStorage` | storage in distinct states (D_039) | ✅ |
| `Y_NP_021_InformationRedistribution` | redistribution into the external system | ✅ |
| `Y_NP_021_Run` | research report | ✅ |

## Conclusion

Information conservation across a horizon is implemented by horizon bookkeeping —
storage (in the distinct states), redistribution (into the external radiation), and
encoding (the hidden/accessible partition) — with the state space fixed at 95.
State-space expansion is refuted. No new primitive; canonical AT unchanged.
