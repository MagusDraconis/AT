# Y_T_014_Result.md — ResearchY-T_014 Near-Gap Density Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_014_Tests.cs`
**Run:** 2026-09-09
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_014"`

---

## Summary

**Goal:** Determine what sets the near-gap mode count N_gap(k) = #{λ ≤ k·λ₂}.

**Answer:** N_gap is a DERIVED function of the spectrum — exact counting, with k-growth set by
the Weyl law (tensor-product dimension) and magnitude set by the symmetry/degeneracy class.

## Results (modes | distinct eigenvalues)

| model | λ₂ | k=1.5 | k=2 | k=3 | k=4 | gap mult |
|---|---|---|---|---|---|---|
| D96 | 0.3864 | 2\|1 | 2\|1 | 2\|1 | 4\|2 | 2 |
| D96^3 | 0.3864 | 6\|1 | 18\|2 | 26\|3 | 32\|4 | 6 |
| random | 17.19 | 31\|31 | 71\|71 | 95\|95 | 95\|95 | 1 |
| physical | 1.0 | 2\|1 | 4\|2 | 6\|3 | 8\|4 | 2 |
| unphysical | 5.0 | 32\|1 | 32\|1 | 32\|1 | 32\|1 | 32 |

Dimension scaling: D96 N_gap(4)/N_gap(1) = 2.00 (∝ k^{1/2}); D96^3 = 5.33 (∝ k^{3/2}).

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_014_GapMultiplicity` | N_gap(1) = degeneracy (2/6/1/2/32) | ✅ |
| `Y_T_014_ExactCounting` | N_gap exact, monotone, modes ≥ distinct | ✅ |
| `Y_T_014_DimensionScaling` | 3D grows faster than 1D (Weyl law) | ✅ |
| `Y_T_014_MultiplicityDependence` | unphysical plateau 32; random all-singleton | ✅ |
| `Y_T_014_Classification` | N_gap not universal | ✅ |
| `Y_T_014_Run` | assumptions-first scientific report | ✅ |

## Conclusion

N_gap(k) = Σ_{λ≤kλ₂} m(λ) is DERIVED; k-growth follows the Weyl law (dimension), magnitude the
symmetry class (degeneracy + octahedral orbits). "Universal in k" is REFUTED.
