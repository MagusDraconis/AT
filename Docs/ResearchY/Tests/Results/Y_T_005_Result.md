# Y_T_005_Result.md — ResearchY-T_005 Attractor Dominance Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_005_Tests.cs`
**Run:** 2026-09-08
**Result:** ✅ 4/4 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_005"`

---

## Summary

**Goal:** Determine whether a small number of attractors dominate the state space, or
whether dominance is a model artifact; test whether spectral organization compresses the
state space into few dominant attractors.

**Verdict:** Spectral organization (D96) compresses the attractor *count* (45, and 13 in
3D, vs ~96 random) but does NOT produce a single dominant attractor (largest basin ~6%).
Dominance is partly a model artifact — "fewest attractors" even favors unphysical
clustered spectra.

## Dominance ranking (N=96)

| Model | A | D | R | E_norm | DI | Classification |
|---|---|---|---|---|---|---|
| complete | 2 | 0.990 | 95.00 | 0.013 | 1.98 | ATTRACTOR DOMINATED |
| unphysical clustered | 4 | 0.333 | 1.00 | 0.251 | 1.33 | NOT DOMINATED |
| D96-3D | 13 | 0.208 | 1.00 | 0.490 | 2.71 | NOT DOMINATED |
| D96 | 45 | 0.062 | 1.20 | 0.824 | 2.81 | NOT DOMINATED |
| physical max-sep | 49 | 0.021 | 1.00 | 0.851 | 1.02 | NOT DOMINATED |
| unphysical band-gap | 49 | 0.021 | 1.00 | 0.851 | 1.02 | NOT DOMINATED |
| unphysical octave | 49 | 0.021 | 1.00 | 0.851 | 1.02 | NOT DOMINATED |
| random-sparse | 96 | 0.010 | 1.00 | 1.000 | 1.00 | NOT DOMINATED |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_005_DominanceRanking` | complete dominates; D96 A=45 < random; D96 not dominated | ✅ |
| `Y_T_005_SamplingConsistency` | lock-to-max sampling concentrates (0.22) but no majority | ✅ |
| `Y_T_005_Robustness` | edge flip breaks circulant degeneracy (A non-decreasing) | ✅ |
| `Y_T_005_Run` | research report | ✅ |

## Classification

| Item | Classification |
|---|---|
| Attractor structure = distinct eigenspaces | DERIVED |
| Spectral organization compresses attractor count | EMERGENT |
| "Few attractors dominate the state space" | REFUTED (for D96) |
| Dominance is a model artifact | CONFIRMED |

## Conclusion

D96 compresses the attractor count (45, 13 in 3D) but has no single dominant attractor
(~6% largest basin). Dominance requires massive degeneracy (complete graph); physicality
does not track "fewest attractors" (unphysical clustered A=4). AT's "few dominant
attractors" reflects the Darwinian selection layer, not bare spectral structure.
