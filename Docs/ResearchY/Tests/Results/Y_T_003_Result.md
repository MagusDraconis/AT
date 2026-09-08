# Y_T_003_Result.md — ResearchY-T_003 General Inverse Spectral Graph Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_003_Tests.cs`
**Run:** 2026-09-08
**Result:** ✅ 5/5 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_003"`

---

## Summary

**Goal:** Determine whether a target spectrum uniquely determines a general
(non-circulant) graph, and reconstruct graphs from their spectra.

**Verdict:** The spectrum alone does NOT determine a general graph — isospectral
non-isomorphic pairs exist (4 on n=6, none on n=5). Full eigendecomposition
`L = VΛVᵀ` reconstructs exactly. Circulant (ring) graphs are the special rigid case
(T_001/T_002), which is why D96 is a canonical attractor.

## Six graph cases

| Case | Edges | Sparsity | Cond | Spectr. err | Graph sim | Partner |
|---|---|---|---|---|---|---|
| path P6 | 5 | 0.667 | 13.93 | 1.0e-15 | 1.00 | no |
| cycle C6 | 6 | 0.600 | 4.00 | 1.4e-15 | 1.00 | no |
| grid 2×3 | 7 | 0.533 | 5.00 | 8.9e-16 | 1.00 | no |
| random sparse | 9 | 0.400 | 3.95 | 2.0e-15 | 1.00 | no |
| complete K6 | 15 | 0.000 | 1.00 | 5.3e-15 | 1.00 | no |
| D96-derived | 576 | 0.874 | 40.99 | 3.4e-14 | 1.00 | n/a |

## Isospectral pairs (n=6)

4 distinct pairs, 2 degree-signature families: `(2,2,2,2,2,4) ↔ (1,2,2,3,3,3)`
(7 edges) and `(1,3,3,3,3,3) ↔ (2,2,2,3,3,4)` (8 edges).

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_003_KnownSpectra` | path/cycle/complete spectra match closed form | ✅ |
| `Y_T_003_EigenbasisReconstruction` | VΛVᵀ recovers every graph exactly | ✅ |
| `Y_T_003_IsospectralSearch` | isospectral non-isomorphic pairs exist | ✅ |
| `Y_T_003_CirculantRigidity` | cycle/D96 labeled spectrum → unique connection set | ✅ |
| `Y_T_003_Run` | research report | ✅ |

## Classification

| Item | Classification |
|---|---|
| Eigendecomposition reconstructs L exactly | DERIVED |
| Spectrum alone does not determine a graph | DERIVED |
| Circulant graphs spectrally rigid | CORRESPONDENCE |
| Spectrum as complete blueprint | REFUTED (general) / SUPPORTED (circulant) |

## Conclusion

The spectrum is a lossy description of a general graph (isospectral degeneracy), but a
complete blueprint of a circulant graph. D96's circulant geometry is what makes its
spectrum a unique, canonical description.
