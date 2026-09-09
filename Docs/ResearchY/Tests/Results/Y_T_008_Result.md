# Y_T_008_Result.md — ResearchY-T_008 Asymptotic Diversity Limit Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_008_Tests.cs`
**Run:** 2026-09-09
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_008"`

---

## Summary

**Goal:** Determine what sets the saturated species count S∞ (from T_007) by sweeping μ, β,
A, fitness variance, and spectral rigidity.

**Scaling law:** **S∞ = min(A, N_fit(μ, β, {w}))** — a mutation–selection balance bounded by
the landscape ceiling A.

## Determinants

| Factor | Result | Classification |
|---|---|---|
| Mutation rate μ | ∂S∞/∂μ ≥ 0 (3→9); μ=0 ⇒ S∞=1 | DERIVED |
| Crowding β | ∂S∞/∂β ≥ 0 (4→7) | DERIVED |
| Landscape size A | ∂S∞/∂A ≈ 0 (S∞ saturates ≈ 4–5 while A 20→188) | DERIVED |
| Fitness variance | informative, not complete (A ceiling + shape matter) | EMERGENT / REFUTED (as sole determinant) |
| Spectral rigidity | not a monotonic determinant (D96-3D more rigid, more survivors) | REFUTED |

## Sweep tables

Mutation (D96, β=1): μ 0.0001→0.5 ⇒ S∞ 3,3,5,6,9.
Crowding (D96, μ=0.01): β 0→10 ⇒ S∞ 4,4,4,5,5,7.
Landscape (circulant): A 20,44,92,188 ⇒ S∞ 5,5,4,4.
Fitness variance: D96-3D 1.311→9 · unphysical 1.083→3 · physical 0.806→5 · D96 0.454→5 · random 0.049→17.
Rigidity: unphysical 0.969→3 · D96-3D 0.875→9 · D96 0.542→5 · physical 0.500→5 · random 0.010→17.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_008_MutationScaling` | ∂S∞/∂μ ≥ 0; μ=0 ⇒ S∞=1; tiny μ re-seeds S₀⁺>1 | ✅ |
| `Y_T_008_CrowdingScaling` | ∂S∞/∂β ≥ 0; bounded | ✅ |
| `Y_T_008_LandscapeSizeScaling` | S∞ ≤ A; S∞ saturates (does not track A) | ✅ |
| `Y_T_008_FitnessVarianceScaling` | random lowest variance, highest S∞; A-ceiling counterexample | ✅ |
| `Y_T_008_RigidityScaling` | rigidity not monotonic (D96-3D vs D96) | ✅ |
| `Y_T_008_ScalingLaw` | S∞ = min(A, N_fit(μ,β,{w})); all monotonicities | ✅ |
| `Y_T_008_Classification` | DERIVED / EMERGENT / REFUTED verdicts | ✅ |
| `Y_T_008_Run` | assumptions-first scientific report | ✅ |

## Conclusion

S∞ = min(A, N_fit(μ, β, {w})). Mutation widens, crowding levels, the landscape is a ceiling
S∞ does not track, and neither variance nor rigidity alone predicts S∞. Monotonicities and
bounds are DERIVED; the functional form is EMERGENT; "A alone", "rigidity alone", and
"variance alone" are REFUTED.
