# Y_QG_017_Result.md — ResearchY-QG_017 Distinguishability Cosmology Extension Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_017_Tests.cs`
**Run:** 2026-08-31
**Result:** ✅ 6/6 PASSED
**Full suite:** 673/673 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_017"`

> **ID note.** This audit was submitted as QG_015/QG_016 (both taken); registered as
> **QG_017** per the permanent-ID rule.

---

## Summary

**Question:** If ΩΛ comes from distinguishability, what else must follow?

**Verdict:** ΩΛ is NOT an isolated success — it is the first member of a **FINITE
distinguishability cosmology family**: ΩΛ, Ωm, ratio, q₀, z_acc. No full
distinguishability cosmology.

## The finite family

| Observable | Formula | Value |
|---|---|---|
| ΩΛ | I_occ/ln K | **0.6839** (observed 0.12%) |
| Ωm | (ln K − I_occ)/ln K | **0.3161** (observed 0.26%) |
| ΩΛ/Ωm | I_occ/(ln K − I_occ) | **2.1636** |
| q₀ | Ωm/2 − ΩΛ | **−0.5258** |
| z_acc | (2ΩΛ/Ωm)^(1/3) − 1 | **0.6295** |

## Closure relations (the family is CLOSED)

- entropy identity: **I_occ + H = ln K** (0.7513 + 0.3473 = 1.0986)
- completeness: **ΩΛ + Ωm = 1**; Ωm = H/ln K
- q₀ and z_acc are deterministic closures of the pair (hosted FRW form)

## No full distinguishability cosmology

H₀ (anchor), σ₈ (A_s), BAO (sound horizon Ωb/Ωr), structure growth (A_s, n_s), weak
lensing (σ₈), horizon size (H₀), clustering (σ₈, P(k)) — **none** depends only on
{I_occ, ln K, ρ}.

## Determination

| Option | Verdict |
|---|---|
| A) ΩΛ uniquely privileged | PARTIAL |
| **B) finite cosmology family** | **YES** |
| C) full distinguishability cosmology | NO |

**Strongest next prediction beyond ΩΛ: q₀ = −0.5258 and z_acc = 0.6295.**

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_017_InformationObservable` | ΩΛ = I_occ/ln K; Ωm = complement | ✅ |
| `Y_QG_017_ClosureRelations` | entropy identity; fraction completeness; the closed family | ✅ |
| `Y_QG_017_SecondaryObservable` | q₀, z_acc closures of the pair | ✅ |
| `Y_QG_017_PredictionRanking` | ΩΛ top; q₀/z_acc next | ✅ |
| `Y_QG_017_FalsificationCheck` | the family is falsifiable; full cosmology refuted | ✅ |
| `Y_QG_017_Run` | research report | ✅ |

## Conclusion

ΩΛ is not an isolated success — it is the first member of a FINITE distinguishability
cosmology family (ΩΛ, Ωm, ratio 2.1636, q₀ = −0.5258, z_acc = 0.6295). No full
distinguishability cosmology: H₀/σ₈/BAO/growth/lensing/horizon/clustering need
non-information inputs. The strongest next prediction beyond ΩΛ is the q₀/z_acc
closure. No new primitive; canonical AT unchanged.
