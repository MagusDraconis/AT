# Y_QG_012_Result.md — ResearchY-QG_012 Distinguishability Cosmology Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_012_Tests.cs`
**Run:** 2026-08-31
**Result:** ✅ 6/6 PASSED
**Full suite:** 637/637 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_012"`

---

## Summary

**Question:** Is ΩΛ uniquely privileged, or does distinguishability generate additional
cosmological observables?

**Verdict:** ΩΛ is privileged but NOT unique. Distinguishability generates a **FINITE
family** of cosmological observables — the density-fraction pair and its deterministic
closure — while H₀/σ₈/BAO/growth/lensing/clustering are not information functions.

## The finite family

| Observable | Formula | Value |
|---|---|---|
| ΩΛ | I_occ/ln K | **0.6839** (observed 0.12%) |
| Ωm | 1 − ΩΛ | **0.3161** (observed 0.26%) |
| ΩΛ/Ωm ratio | I_occ/(ln K − I_occ) | **2.1636** |
| q₀ (deceleration) | Ωm/2 − ΩΛ | **−0.5258** |
| z_acc (turnaround) | (2ΩΛ/Ωm)^(1/3) − 1 | **0.6295** |

The entropy identity I_occ + H = ln K (H = 0.3473 nats) gives Ωm = H/ln K — the pair
partitions the state-space size.

## Not information functions

| Observable | Why not |
|---|---|
| H₀ | dimensionful — needs an anchor |
| σ₈ | needs the primordial amplitude A_s |
| BAO scale | needs the sound horizon (Ωb, Ωr) |
| structure growth | needs A_s, n_s, growth index |
| weak lensing / horizon / clustering | inherit σ₈, H₀, P(k) |

## Determination

| Option | Verdict |
|---|---|
| A) ΩΛ uniquely privileged | PARTIAL — the primary fraction, but not alone |
| **B) finite family of information observables** | **YES** — pair + closures |
| C) full information cosmology | NO — refuted |

**q₀/z_acc form is CORRESPONDENCE** (hosted FRW kinematics); **their values are
DERIVED** from the information fractions (two-level rule).

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_012_InformationObservable` | ΩΛ = I_occ/ln K; Ωm = complement | ✅ |
| `Y_QG_012_CosmologyMapping` | which observables are information functions | ✅ |
| `Y_QG_012_SecondaryObservable` | q₀, z_acc closures of the pair | ✅ |
| `Y_QG_012_PredictionRanking` | ΩΛ top; q₀/z_acc next | ✅ |
| `Y_QG_012_FalsificationCheck` | the family is falsifiable; full cosmology refuted | ✅ |
| `Y_QG_012_Run` | research report | ✅ |

## Conclusion

ΩΛ is privileged but not unique. Distinguishability generates a FINITE family of
cosmological observables — the density-fraction pair (ΩΛ = 0.6839, Ωm = 0.3161) and its
deterministic closure (ratio 2.1636, q₀ = −0.526, z_acc = 0.630) — while H₀, σ₈, BAO,
growth, lensing, and clustering are not information functions (BOUNDARY/calibration).
No new primitive; canonical AT unchanged.
