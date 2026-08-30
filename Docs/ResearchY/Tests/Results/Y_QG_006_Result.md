# Y_QG_006_Result.md — ResearchY-QG_006 Count Conservation Origin Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_006_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_006"`

---

## Summary

**Question:** Why must count be conserved?

**Verdict:** Count conservation (Σρ = 1) is **DEFINITIONAL** — built into the counting
measure via the normalizer S — and **NECESSARY**: removing it collapses everything
SIMULTANEOUSLY.

## Definitional

```
ρ_k = μ^k / S,   Σρ_k = Σμ^k/S = 1   (QG194, the normalizer S)
```

Count conservation is built into the definition of ρ as a normalized counting measure.

## Remove count conservation — everything fails together

| Quantity | Without Σρ = 1 |
|---|---|
| geometry | √(−g) = ρ fails (no longer a measure, QG207) |
| information | KL(ρ‖uniform) undefined (QG228) |
| measurement | Born Σ\|ψ\|² = 1 invalid (QG216) |
| black-hole bookkeeping | H_before ≠ H_after (NP_020/021) |

**There is NO "first" — all require the normalized count simultaneously.**

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_006_CountConservation` | Σρ = 1 by construction (normalizer S) | ✅ |
| `Y_QG_006_GeometryRemoval` | geometry fails without Σρ = 1 | ✅ |
| `Y_QG_006_InformationRemoval` | information fails without Σρ = 1 | ✅ |
| `Y_QG_006_MeasurementRemoval` | measurement fails without Σρ = 1 | ✅ |
| `Y_QG_006_BlackHoleBookkeeping` | bookkeeping fails without Σρ = 1 | ✅ |
| `Y_QG_006_Run` | research report | ✅ |

## Conclusion

Count conservation is definitional (built into ρ via the normalizer S, QG194) and
necessary (the foundation of geometry, information, measurement, and black-hole
bookkeeping — all collapse together without it). No new primitive; canonical AT
unchanged.
