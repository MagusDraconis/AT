# Y_T_011_Result.md — ResearchY-T_011 Species Ceiling Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_011_Tests.cs`
**Run:** 2026-09-09
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_011"`

---

## Summary

**Goal:** Determine whether the legacy AT-138/139 value (~19 stable species) emerges naturally
from D96.

**Answer:** REFUTED — D96's natural species ceiling is ~5 (survivors and cumulative discovery);
~19 is nearest the RANDOM landscape (17 survivors, 18 discoveries), not D96.

## Population quantities (μ=0.01, β=1)

| case | A | S∞ | cumulative (discovery) |
|---|---|---|---|
| D96 | 44 | 5 | 5 |
| D96-3D | 12 | 9 | 9 |
| random | 95 | 17 | 18 |
| physical | 48 | 5 | 5 |
| unphysical | 3 | 3 | 3 |

## Mutation sweep (D96, β=1)

μ = 0.001, 0.01, 0.1, 0.3, 0.5, 0.8, 0.95 → S∞ = 3, 5, 6, 8, 9, 11, 13. Never 19.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_011_PopulationMeasures` | transient/cumulative bounded between S∞ and A | ✅ |
| `Y_T_011_D96NotNineteen` | D96 survivors ≈5, far from 19; random nearer 19 | ✅ |
| `Y_T_011_DiscoveryTrajectory` | D96 discovers fewer species than random | ✅ |
| `Y_T_011_CeilingSweep` | 19 unreachable for D96 short of the μ→1 uniform limit | ✅ |
| `Y_T_011_Classification` | REFUTED; D96 ceiling DERIVED ≈5; random ≈19 nearest | ✅ |
| `Y_T_011_Run` | assumptions-first scientific report | ✅ |

## Conclusion

~19 species is REFUTED as a D96 outcome; D96 gives ~5 (DERIVED via S∞ = min(A, N_fit)), and the
nearest natural ~19 is the random landscape (17–18). The legacy value traces to the Θ-field
pattern model, not the D96 spectral blueprint.
