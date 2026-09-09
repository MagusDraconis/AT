# Y_T_009_Result.md — ResearchY-T_009 Fitness-Reach Law Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_009_Tests.cs`
**Run:** 2026-09-09
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_009"`

---

## Summary

**Goal:** Determine whether N_fit (S∞ = min(A, N_fit)) can be expressed analytically.

**Answer:** YES in two limits — μ=0 (crowding threshold) and β=0 (linear Perron equilibrium),
both reproduced exactly; NO universal single-Δw law.

## Analytical laws

| Limit | Closed form | Accuracy |
|---|---|---|
| μ = 0 | S∞ = #{w_k > Z*(β)}, Σ(w_k−Z*) = β·Z* | EXACT (≤1 critical-slowing-down lag) |
| β = 0 | S∞ = #{Perron(M·diag(w)) > ε} | EXACT |
| small μ | N_fit ≈ 1 + log(1/ε)/log(2δ/μ) | ±1–2 |

## Key data

μ=0 threshold (β sweep): D96 1,1,1,3 · D96-3D 2,3,3,8 · random 7,11,17,45 · physical 1,1,2,6 · unphysical 1,1,1,2.

β=0 Perron (μ sweep): D96 3,4,6 · D96-3D 4,5,10 · random 4,7,15 · physical 3,4,7 · unphysical 3,3,3.

Single-Δw refutation (Δw=5, μ=0, β=10): tail-near-top → 5, tail-dropped → 2.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_009_ZeroMutationThreshold` | μ=0 threshold = #{w_k > Z*(β)} (all cases × β) | ✅ |
| `Y_T_009_LinearEquilibrium` | β=0 Perron = #{Perron(M·diag(w)) > ε} (exact) | ✅ |
| `Y_T_009_ReachApproximation` | uniform-gap reach within ±3 of simulation | ✅ |
| `Y_T_009_SingleGapRefuted` | equal Δw ≠ equal N_fit | ✅ |
| `Y_T_009_Classification` | DERIVED / EMERGENT / REFUTED verdicts | ✅ |
| `Y_T_009_Run` | assumptions-first scientific report | ✅ |

## Conclusion

N_fit is exactly solvable at μ=0 (crowding threshold) and β=0 (Perron equilibrium), and has a
DERIVED geometric-tail reach for small μ. The general interior and any single-Δw law are
REFUTED — N_fit depends on the full fitness spectrum.
