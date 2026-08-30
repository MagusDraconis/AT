# Y_NP_019_Result.md — ResearchY-NP_019 Information Cosmology Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_019_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 5/5 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_019"`

---

## Summary

**Question:** Does distinguishability-derived information predict additional
cosmological observables beyond ΩΛ?

**Verdict:** Information predicts EXACTLY the density-fraction pair (ΩΛ, Ωm) and their
ratio — **no additional direct observables**.

## The information cosmology

| Observable | Formula | Value | Status |
|---|---|---|---|
| ΩΛ | I_occ/ln K | 0.6839 | OBSERVED (0.12%) |
| Ωm | (ln K − I_occ)/ln K | 0.3161 | OBSERVED (0.26%) |
| ΩΛ/Ωm | I_occ/(ln K − I_occ) | 2.1636 | derived |

## What is NOT information-derived

| Observable | Value | Source |
|---|---|---|
| n_s | 0.96497 | D96-spectral (QG237), not I_occ |
| ℓ₁ | 220.48 | D96-octave (QG238), not I_occ |
| H₀ | calibration | no relation |
| σ₈, BAO, growth | measured | no relation |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_019_InformationObservable` | ΩΛ = I_occ/ln K | ✅ |
| `Y_NP_019_CosmologyMapping` | which observables depend on I_occ | ✅ |
| `Y_NP_019_AdditionalRelations` | no extra information-derived relations | ✅ |
| `Y_NP_019_PredictionRanking` | ΩΛ/Ωm top; n_s/ℓ₁ correspondence | ✅ |
| `Y_NP_019_Run` | research report | ✅ |

## Conclusion

Distinguishability-derived information predicts EXACTLY the density-fraction pair
(ΩΛ = 0.6839, Ωm = 0.3161) and their ratio (2.1636) — the full information cosmology.
n_s and ℓ₁ are D96-spectral (not I_occ functions); H₀, σ₈, BAO, and growth have no
direct relation. I_occ is a genuine but narrow cosmological variable. No new
primitive; canonical AT unchanged.
