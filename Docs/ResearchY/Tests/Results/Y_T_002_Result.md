# Y_T_002_Result.md — ResearchY-T_002 Physical Spectrum Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_002_Tests.cs`
**Run:** 2026-09-08
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_002"`

---

## Summary

**Goal:** Determine which target spectra admit sparse positive-weight (physical)
realizations, and map the boundary between physical and unphysical spectra.

**Verdict:** A spectrum is physical iff it is a negative-definite function on Z_N
(non-negative combination of single-edge generators). Physicality is rare (0/400 random
spectra), and the boundary is sharp: D96 is the canonical sparse physical realization;
band-gap, clustered, and extreme-octave spectra are unphysical.

## Canonical targets (N = 96)

| Target | Score P | Class | Edges | Neg. w | Min w |
|---|---|---|---|---|---|
| D96 | 0.958 | PHYSICAL | 6 | 0 | 0.000 |
| band-gap | 0.236 | UNPHYSICAL | 48 | 50 | −1.434 |
| clustered | 0.235 | UNPHYSICAL | 47 | 42 | −3.031 |
| octave-spaced | 0.190 | UNPHYSICAL | 48 | 47 | −264.611 |
| max-separated | 0.500 | MARGINAL | 24 | 0 | 0.000 |

## Boundary map

| Family | Boundary |
|---|---|
| geometric (octave) | r = 1.0110 (span ≈ 1.67) |
| band-gap | g = 0.0000 (D96 has zero margin) |
| clustered | sep = 0.40 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_002_GeneratorBasis` | single-edge generators reconstruct to single edges | ✅ |
| `Y_T_002_D96LikeFamily` | circulant C_N(±1..±K) always physical, K edges | ✅ |
| `Y_T_002_GeometricBoundary` | octave family boundary in (1.0, 1.3) | ✅ |
| `Y_T_002_BandGapBoundary` | band-gap unphysical for any gap (boundary ≈ 0) | ✅ |
| `Y_T_002_ClusteredBoundary` | clustered family boundary in (0, 40) | ✅ |
| `Y_T_002_RandomScan` | <5% of random spectra are physical | ✅ |
| `Y_T_002_PhysicalityScore` | D96 physical; band-gap/clustered/octave unphysical | ✅ |
| `Y_T_002_Run` | research report | ✅ |

## Classification

| Item | Classification |
|---|---|
| Physical spectra = negative-definite cone | DERIVED |
| D96-like family always physical | DERIVED |
| Physicality score maps spectra → materials | CORRESPONDENCE |
| Hypothesis (sparse positive-weight realization) | SUPPORTED / REFUTED (per family) |

## Conclusion

The physical/unphysical boundary is the boundary of the cone of negative-definite
functions on Z_N. D96 is the sparse all-physical realization; band-gap, clustered, and
extreme-octave spectra require repulsive coupling. Physicality is a measure-(near-)zero
property. Next: non-circulant weighted graphs (ResearchY-T_003).
