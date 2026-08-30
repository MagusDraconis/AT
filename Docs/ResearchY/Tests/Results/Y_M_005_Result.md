# Y_M_005_Result.md — ResearchY-M_005 Information Conservation Audit

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_005_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_005"`

---

## Summary

**Question:** Does measurement create information or reveal pre-existing information?

**Verdict:** Measurement **REVEALS pre-existing distinguishability and REDISTRIBUTES
it — it does NOT create information.** The 6.57 bits are pre-existing in the state
space (D_039: 95 distinct states exist before any measurement); the measurement event
reads both quadratures (M_001), resolving WHICH state is realized (reveal), and
converts the phase freedom into a pinned outcome + observer knowledge (redistribute).

## Information balance

| Quantity | Value |
|---|---|
| H_state_space (pre-existing) | log₂ 95 = 6.57 bits (D_039) |
| H_outcome (realized state) | 0 (known) |
| H_observer | log₂ 95 (gained) |
| **TOTAL** | **conserved** (log₂ 95 = 0 + log₂ 95) |
| count conservation | Born rule Σ|ψ|² = 1 EXACT (QG216) |

## Test A/B/C

| Option | Verdict |
|---|---|
| A) create | **NO** (the 95 states pre-exist) |
| B) reveal | **YES** (the event resolves the outcome) |
| C) redistribute | **YES** (phase → outcome + observer) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_M_005_InformationSource` | the 6.57 bits are pre-existing (state space, D_039) | ✅ |
| `Y_M_005_InformationGain` | observer gains log₂ 95 (the reveal) | ✅ |
| `Y_M_005_InformationConservation` | log₂ 95 = outcome + observer (conserved) | ✅ |
| `Y_M_005_ObserverInformation` | the observer's knowledge is the redistribution | ✅ |
| `Y_M_005_PrePostComparison` | 95 states before and after (no creation) | ✅ |
| `Y_M_005_DependencyTrace` | Difference → distinguishability → identity → measurement → info | ✅ |
| `Y_M_005_Run` | Research report | ✅ |

## Conclusion

Information is **CONSERVED through actualization**: the measurement event reveals
pre-existing distinguishability (D_039) and redistributes it (phase freedom → outcome +
observer). It does **NOT** create information. This refines M_004: "measurement creates
information" is the observer's gain; from the conservation view the event reveals +
redistributes. Classification: distinguishability/information DERIVED (D_039,
pre-existing); reveal EMERGENT (M_001); redistribute DERIVED; conservation DERIVED
(count conservation, QG216). No new primitive; canonical AT unchanged.
