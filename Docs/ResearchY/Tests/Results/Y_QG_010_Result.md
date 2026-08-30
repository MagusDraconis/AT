# Y_QG_010_Result.md — ResearchY-QG_010 Observable Finiteness Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_010_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 5/5 PASSED
**Full suite:** 621/621 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_010"`

---

## Summary

**Question:** Why is the observable state space finite if infinite distinguishability
is consistent?

**Verdict:** Observability requires finite distinguishability. A measurement event is a
FINITE act (M_001) with finite information capacity log₂(N_obs) (M_004) — an infinite
observable state space would require infinite information per event.

## Finite vs infinite observable space

| Property | Observable finite N (95) | Observable infinite N (∞) |
|---|---|---|
| state identity | 95 distinct, fully resolvable (D_039) | infinite in principle; finite in observation |
| measurement | resolves 1 of 95 (M_001) | must resolve 1 of ∞ — impossible |
| information gain | log₂(95) = 6.57 bits/event (M_004) | **log₂(N) → ∞ — DIVERGES** |
| distinguishability | 95/95 realized | bounded by event capacity |

## The selecting property: finite event information capacity

```
finite measurement event (M_001)
 → finite information capacity log₂(N_obs) (M_004)
 → finite outcome alphabet
 → FINITE OBSERVABLE STATE SPACE
```

All four finite links present: finite observer (M_006), finite measurement (M_001),
finite resolution (M_002/M_004), finite bookkeeping (M_005/NP_021).

## Prove/refute: observability requires finite distinguishability

**PROVEN** — the measurement event is a finite act (M_001) with finite information
capacity (M_004); resolving 1 of an infinite alphabet would require log₂(N) → ∞ bits
per event; a finite act cannot carry infinite information.

## Resolution of QG_009 OP1

**YES — observability (not Difference) pins the finite observable state space.** The
observable projection of any state space is finite.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_010_FiniteObservability` | finite N: identity/measurement/info/distinguishability all work | ✅ |
| `Y_QG_010_InfiniteObservability` | infinite N: per-event info diverges; unobservable | ✅ |
| `Y_QG_010_InformationCapacity` | log₂(N_obs) per event; finite ⟹ N_obs finite | ✅ |
| `Y_QG_010_MeasurementResolution` | finite event resolves a finite outcome set | ✅ |
| `Y_QG_010_Run` | research report | ✅ |

## Conclusion

Observability requires finite distinguishability. The finite measurement event (M_001)
has finite information capacity log₂(N_obs) (M_004), so it can only resolve a finite
outcome alphabet — the observable state space is finite. This resolves QG_009 OP1:
observability (not Difference) pins the finite observable state space. No new
primitive; canonical AT unchanged.
