# Y_NP_012_Result.md — ResearchY-NP_012 Unique Prediction Search

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_012_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_012"`

---

## Summary

**Question:** What observable prediction survives after all QM-equivalent
interpretations are removed?

**Verdict:** The measurement and coupling programs contribute **NO observationally-
testable unique prediction** (all QM-equivalent or negative results). The surviving
uniquely-AT predictions are the **N=96 spectral values**.

## What survives

| Result | Class |
|---|---|
| measurement = event, pinning, feedback, observer | CORRESPONDENCE (QM-equivalent) |
| AT-P043 (log₂ 95 bound) | CORRESPONDENCE (downgraded, M_009) |
| AT-P042 (discrete tick) | PREDICTION — structural only (sub-tick in-principle) |
| coupling network / sync / field / extremum | BOUNDARY (absent — negative results) |
| **ω₁ = √91·(2π/N) = 0.624** | **PREDICTION — uniquely-AT (FIRST)** |
| **families = floor(log₂ span)+1 = 3** | **PREDICTION — uniquely-AT** |
| **O(2) doublet (not SU(2))** | **PREDICTION — uniquely-AT structure** |
| **v = 137·ln(span) = 254.37 GeV** | **PREDICTION — AT-derived structure** |

## Ranking

| Rank | Prediction | Score /20 |
|---|---|---|
| 1 | ω₁ = √91·(2π/N) | 16 |
| 1 | families = 3 | 16 |
| 3 | O(2) doublet | 13 |
| 3 | v = 137·ln(span) | 13 |
| 5 | AT-P042 discrete tick | 11 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_012_PredictionInventory` | enumerate surviving AT-specific results | ✅ |
| `Y_NP_012_QMComparison` | test each as A/B/C | ✅ |
| `Y_NP_012_UniquenessFilter` | filter to C-only survivors | ✅ |
| `Y_NP_012_FalsificationCheck` | each survivor has a falsification path | ✅ |
| `Y_NP_012_Ranking` | 4-axis ranking (ω₁ & families top) | ✅ |
| `Y_NP_012_Run` | research report | ✅ |

## Conclusion

The unique predictions of the theory live in the N=96 SPECTRUM, not the measurement
program. **The first uniquely-AT prediction is the fundamental spectral frequency
ω₁ = √91·(2π/N).** No new primitive; canonical AT unchanged.
