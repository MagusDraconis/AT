# Y_NP_003_Result.md — ResearchY-NP_003 Manipulation Lever Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_003_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_003"`

---

## Summary

**Question:** Does the theory contain a controllable physical lever?

**Verdict:** **EXACTLY ONE — the phase θ₀ of a complex state**, locally variable via
measurement (M_002 phase-pinning), propagating into the future trajectory
(θ_t = θ₀ + t·Δθ, M_003).

## The lever

| Property | Value |
|---|---|
| lever | the phase θ₀ of a complex state |
| type | B — locally variable |
| set by | a measurement event (M_002) |
| propagates | pinned phase → future trajectory → interference/outcomes |
| smallest object | one angular DOF of one state |

## Effects of the lever

| Observable | Modified? |
|---|---|
| time behaviour | **YES** (initial condition of the trajectory) |
| measurement | **YES** (readout = pinned phase) |
| frequency | NO (Δθ = 2πk/N fixed per mode) |
| gravity | NO (no metric coupling) |
| sector structure | NO (N, pairing, families fixed) |

## Fixed quantities

| Class | Quantities |
|---|---|
| BOUNDARY | {Difference, η}, {v, m_e} |
| DERIVED (unique) | N=96, spectrum, ω₁, λ₂, pairing, tick structure |
| global lever | NONE |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_003_LeverCandidates` | classification of all candidates (only phase variable) | ✅ |
| `Y_NP_003_LocalVariation` | phase is locally variable (θ_t shifts with θ₀) | ✅ |
| `Y_NP_003_GlobalVariation` | no global lever (N, anchors fixed) | ✅ |
| `Y_NP_003_ObservableEffects` | phase changes time+measurement, not frequency/sector | ✅ |
| `Y_NP_003_DependencyTrace` | Difference → Actualization → phase → effects | ✅ |
| `Y_NP_003_Run` | research report | ✅ |

## Conclusion

The theory contains exactly ONE controllable lever — the phase θ₀ of a complex state,
locally variable via measurement and propagating into time behaviour and measurement
outcomes. No global lever exists. Classification: phase DOF DERIVED (D_036/D_039);
manipulability EMERGENT (M_001/M_002); fixed quantities DERIVED or BOUNDARY. No new
primitive; canonical AT unchanged.
