# Y_QG_013_Result.md — ResearchY-QG_013 Three-Family Origin Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_013_Tests.cs`
**Run:** 2026-08-31
**Result:** ✅ 6/6 PASSED
**Full suite:** 643/643 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_013"`

---

## Summary

**Question:** Why must the observable sector consist of exactly three families?

**Verdict:** The 3-family WINDOW is a CONFIRMED BOUNDARY — NOT reducible to
distinguishability, count structure, or information density — but it is ANCHORED by
the observed cosmology (ΩΛ = 0.6839).

## Pairing does NOT select 3

All octave rungs 3·2^k are pairing-complete (λ=12 mult 5):

| N | Rung | Families | Pairing |
|---|---|---|---|
| 48 | 3·2⁴ | 2 | ✅ mult 5 |
| 96 | 3·2⁵ | 3 | ✅ mult 5 |
| 192 | 3·2⁶ | 4 | ✅ mult 5 |
| 384 | 3·2⁷ | 5 | ✅ mult 5 |

(N=64/128 fail: λ=12 mult 1.)

## I_occ is monotone — NO extremum at 3

0.524 → 0.630 → 0.7513 → 0.820 → 1.013 (N=48..192). 3 does not minimize, maximize,
or stationarize the information density.

## The observed cosmology selects 3

| N | ΩΛ predicted | vs observed 0.6839 |
|---|---|---|
| 48 | 0.4773 | −30.2% ❌ |
| **96** | **0.6839** | **0.0% ✅** |
| 192 | 0.8153 | +19.2% ❌ |
| 384 | 0.8945 | +30.8% ❌ |

## What first fails at family count ≠ 3

**The observed cosmology** — the predicted dark-energy fraction deviates by 19–31%.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_013_FamilyCount` | floor(log₂ span)+1; span(96) → 3 | ✅ |
| `Y_QG_013_TwoFourFive` | N=48/192/384 are pairing-complete rungs | ✅ |
| `Y_QG_013_InformationDensity` | I_occ monotone in N; no extremum at 3 | ✅ |
| `Y_QG_013_OmegaObservables` | ΩΛ(96) = 0.6839 exact; others deviate 19–31% | ✅ |
| `Y_QG_013_BoundaryReduction` | window not reducible to pairing/info; anchored by ΩΛ | ✅ |
| `Y_QG_013_Run` | research report | ✅ |

## Conclusion

The 3-family window is a CONFIRMED boundary — not reducible to pairing (all octave
rungs pair), count, or information (I_occ is monotone). It is ANCHORED by the observed
cosmology: N=96 is the unique pairing-complete octave rung reproducing ΩΛ = 0.6839,
and 3 families is the span projection of that rung. The family-count VALUE 3 is
DERIVED; the WINDOW is BOUNDARY (D_020/D_040). No new primitive; canonical AT
unchanged.
