# Y_M_004_Result.md — ResearchY-M_004 Measurement Information Audit

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_004_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_004"`

---

## Summary

**Question:** What is the information-theoretic limit of a measurement event?

**Verdict:** The maximum information content of one actualization event is
**log₂(95) ≈ 6.57 bits** — the size of the distinguishable state space (D_039: 95/95
distinct complex states). A measurement reads both quadratures of one complex mode
(M_001), resolving which of the 95 states is realized.

## Information before vs after

| | Before | After |
|---|---|---|
| state | one of 95 (uncertainty log₂ 95) | outcome realized (uncertainty 0) |
| GAIN | — | log₂(95) = 6.57 bits (uniform) |
| FIXED | — | phase (M_002) + trajectory (M_003) |
| LOST | — | phase freedom (superposition → one trajectory) |

## Prove/refute

**Measurement creates information — YES** (it resolves the state-space uncertainty).

## Key facts

| Quantity | Value |
|---|---|
| distinguishable states | 95/95 (D_039) |
| max info per event | log₂(95) = 6.5699 bits |
| real-only collapse | 48 states (less info) |
| repeated measurement | idempotent — 0 additional info |
| I_occ (Born-weighted, QG228) | 0.7513 nats |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_M_004_InformationGain` | info before log₂95, after 0 → gain log₂95 | ✅ |
| `Y_M_004_RepeatedMeasurement` | idempotent — no additional information | ✅ |
| `Y_M_004_Distinguishability` | 95/95 distinct complex states | ✅ |
| `Y_M_004_ActualizationInformation` | the read resolves the state (max log₂95) | ✅ |
| `Y_M_004_DependencyTrace` | Difference → distinguishability → measurement → information | ✅ |
| `Y_M_004_Run` | Research report | ✅ |

## Conclusion

The information-theoretic limit of one measurement event is **log₂(95) ≈ 6.57 bits** —
the state-space distinguishability. Measurement creates information by resolving which
state is realized; repeated measurements are idempotent (no additional information).
Information is **DERIVED** (from distinguishability, D_039); the measurement event is
**EMERGENT** (M_001). No new primitive; canonical AT unchanged.
