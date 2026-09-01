# Y_NP_026_Result.md — ResearchY-NP_026 Protected Block Universality Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_026_Tests.cs`
**Run:** 2026-09-01
**Result:** ✅ 8/8 PASSED
**Full suite:** 706/706 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_026"`

---

## Summary

**Question:** Is R_K = √(K/(K+1)) a theorem of all circulant-ring spectra, or only
of the canonical nearest-neighbour class C_N(±1..±K)?

**Verdict:** R_K is a theorem of the **CANONICAL NEAREST-NEIGHBOUR CIRCULANT class**
(determination B) — NOT of all circulants, NOT of general graphs, NOT an
approximation.

## The topology scans

| Topology | Result |
|---|---|
| canonical ±{1..±K} | ✅ √(K/(K+1)) exact (blocks at λ=2K, 2K+2) |
| alternative generator sets (odd, powers-of-2) | ❌ no partner block / no blocks |
| weighted links (linear/exp/random) | ❌ blocks destroyed |
| random perturbations (±5%) | ❌ blocks destroyed |
| missing links | ❌ ratio changed/destroyed |
| non-circulant graphs (path, complete, random) | ❌ no protected ratio |

## The analytic origin

```
λ_{N/4} = 2·Σ(1−cos(πs/2)) = 2K+2   (K=6: 14)
λ_{N/6} = 2·Σ(1−cos(πs/3)) = 2K     (K=6: 12)
ratio √(12/14) = √(6/7) = √(K/(K+1))
```

**Requirements:** consecutive uniform generator set (all weights = 1); N divisible
by 4 and 6.

## Determination: B) circulant-only theorem

- A) exact theorem of all circulants — **NO**
- **B) circulant-only theorem (canonical nearest-neighbour class) — YES**
- C) approximation — **NO** (exact within the class)

## Distinct protection classes

- **Mirror pairs** (NP_023): protected against reflection-preserving perturbations.
- **Block ratio** (NP_026): weight-FRAGILE (destroyed by any weight variation or
  missing link).

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_026_CirculantTheorem` | canonical class gives √(K/(K+1)) exactly | ✅ |
| `Y_NP_026_AlternativeGenerators` | odd/powers-of-2 sets fail | ✅ |
| `Y_NP_026_WeightedLinks` | weights destroy the blocks | ✅ |
| `Y_NP_026_RandomPerturbation` | perturbations destroy the blocks | ✅ |
| `Y_NP_026_MissingLinks` | missing links change/destroy the ratio | ✅ |
| `Y_NP_026_NonCirculant` | path/complete/random graphs fail | ✅ |
| `Y_NP_026_OriginDetermination` | B — canonical nearest-neighbour class | ✅ |
| `Y_NP_026_Run` | research report | ✅ |

## Conclusion

R_K = √(K/(K+1)) is a theorem of the canonical nearest-neighbour circulant class
C_N(±1..±K) with uniform weights — the true mathematical origin is the consecutive
uniform generator set with the special modes k=N/4 and k=N/6. It is NOT a theorem of
all circulants, NOT of general graphs, and NOT an approximation. No new primitive;
canonical AT unchanged.
