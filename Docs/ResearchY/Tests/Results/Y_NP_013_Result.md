# Y_NP_013_Result.md — ResearchY-NP_013 Unique Spectral Prediction Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_013_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_013"`

---

## Summary

**Question:** What observable follows uniquely from ω₁, span, family count, and O(2)
doublets — and cannot be reproduced by standard QM/SM/GR?

**Verdict:** The strongest D96-specific prediction is the **O(2) EXACT DOUBLET
DEGENERACY** (λ_k = λ_{N−k} for every mode k) — a structural, falsifiable claim
absent from QM, GR, and SM.

## Spectral quantities (N=96)

| Quantity | Value |
|---|---|
| ω₁ | √91·(2π/N) ≈ 0.6244 |
| λ₂ | 2−2cos(4π/96) ≈ 0.0171 |
| span | 6.4025 |
| family count | floor(log₂ span)+1 = 3 |
| O(2) doublets | λ_k = λ_{N−k}, ∀k |
| v structure | 137·ln(span) = 254.37 GeV |

## Exclusion

| Quantity | Implied by QM/GR/SM? |
|---|---|
| ω₁, doublets, span | NO — none fixes these |
| family count, v | NO — SM inputs, does not derive |

## Top-5 ranking (uniqueness × impact × feasibility)

| Rank | Prediction | Score |
|---|---|---|
| 1 | O(2) exact doublet degeneracy | 13 |
| 1 | family count = 3 | 13 |
| 2 | ω₁ = √91·(2π/N) | 12 |
| 3 | v = 137·ln(span) | 11 |
| 4 | span = 6.4025 | 10 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_013_SpectralAnalysis` | the six spectral quantities | ✅ |
| `Y_NP_013_ObservableSearch` | observable consequences | ✅ |
| `Y_NP_013_QMGRSMExclusion` | none implied by QM/GR/SM | ✅ |
| `Y_NP_013_Ranking` | Top-5 ranking (doublets & families top) | ✅ |
| `Y_NP_013_FalsificationPaths` | falsification paths for all five | ✅ |
| `Y_NP_013_Run` | research report | ✅ |

## Conclusion

The strongest falsifiable D96-specific prediction is the O(2) exact doublet
degeneracy; ω₁ = √91·(2π/N), family count = 3, v = 137·ln(span), and span = 6.4025
follow. None is implied by QM/GR/SM. No new primitive; canonical AT unchanged.
