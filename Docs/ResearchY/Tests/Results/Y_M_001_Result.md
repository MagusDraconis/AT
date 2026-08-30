# Y_M_001_Result.md — ResearchY-M_001 Measurement Origin Audit

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_001_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_001"`

---

## Summary

**Question:** What is a measurement event?

**Verdict:** A measurement event is an **ACTUALIZATION EVENT applied to a
DISTINGUISHABLE state** — state selection (A) realized as **distinguishability-
becoming-actual** (B). A measurement reads BOTH quadratures of one complex mode (the
{cos, sin} two-quadrature reconstruction basis, D_037): z = a + ib exact; a alone
ambiguous. What changes: the state's identity transitions from **potential** (in the
complex amplitude) to **actual** (a realized outcome with Born weight |ψ|² = ρ, QG216).
Collapse (C) is the QG73 binary reading of the same event, not a separate mechanism.

## Key measured values

| Quantity | Value |
|---|---|
| complex-state identity | 95/95 distinct |
| real-only collapse | 48 (47 pairs + 1 self-conjugate) |
| reconstruction z = a + ib | exact (verified k=16, site 5) |
| single-quadrature ambiguity | same a=1 from (|ψ|=2,θ=π/3) and (|ψ|=1,θ=0) |
| measurement-basis orthogonality | Σ cos·sin = 0 |
| Born rule | Σρ = 1 EXACT (μ=2, J=5) |
| interference | phase-dependent P = 2+2cos Δθ |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_M_001_StateIdentity` | identity = Difference applied; 95/95 distinct | ✅ |
| `Y_M_001_ActualizationEvent` | measurement is an actualization event (count realization) | ✅ |
| `Y_M_001_MeasurementEvent` | both quadratures read → state selected (A+B) | ✅ |
| `Y_M_001_Observability` | z = a + ib exact; a alone ambiguous | ✅ |
| `Y_M_001_CollapseComparison` | collapse = the event's binary reading, not separate | ✅ |
| `Y_M_001_DependencyTrace` | Difference → identity → observability → measurement | ✅ |
| `Y_M_001_Run` | Research report | ✅ |

## Conclusion

A measurement event is an actualization event applied to a distinguishable state —
state selection realized as distinguishability-becoming-actual. It reads both
quadratures of one complex mode (the {cos, sin} basis), actualizing the state's
identity with Born weight. State identity, observability, and probability are
**DERIVED**; the measurement event and its collapse reading are **EMERGENT**. Removing
measurement leaves identity, observability, probability, and interference intact — only
the actualization of a specific outcome is removed. No new primitive; canonical AT
unchanged.
