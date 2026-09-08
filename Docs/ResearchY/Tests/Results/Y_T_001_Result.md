# Y_T_001_Result.md — ResearchY-T_001 Spectral Blueprint (Inverse Spectral Design)

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_001_Tests.cs`
**Run:** 2026-09-08
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_001"`

---

## Summary

**Goal:** Test the "spectral blueprint" hypothesis — can material properties be
designed by specifying a target spectrum and reconstructing the coupling graph,
instead of the forward path `topology → simulation → observed spectrum`?

**Verdict:** SUPPORTED for circulant (ring) topologies. The inverse problem is
closed-form (inverse DFT), exact, and optimally conditioned (isometry, condition
number 1). Physicality (non-negative coupling) selects **sparse band-structured
spectra**; AT's canonical D96 structure is the sparse, all-physical solution.

## Inverse reconstruction results (N = 96)

| Target | RMS err | Edges | Neg. weights | Min weight | Coupling mass |
|---|---|---|---|---|---|
| D96 (canonical) | 1.5e-13 | 6 | 0 | 0.000 | 12.00 |
| band-gap | 2.4e-13 | 48 | 50 | −1.434 | 44.85 |
| clustered | 3.2e-13 | 47 | 42 | −3.031 | 79.49 |
| octave-spaced | 1.2e-11 | 48 | 47 | −264.611 | 3029.30 |
| max-separated | 2.7e-13 | 24 | 0 | 0.000 | 24.00 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_001_InverseIsExact` | round trip closes for all 5 targets | ✅ |
| `Y_T_001_D96RoundTrip` | recovers connection set {±1..±6} exactly | ✅ |
| `Y_T_001_PhysicalityByTarget` | D96 sparse+physical; band-gap/cluster dense+repulsive | ✅ |
| `Y_T_001_IsometryRobustness` | inverse map is an isometry (condition number 1) | ✅ |
| `Y_T_001_SparsityAccuracyTradeoff` | 6 edges reproduce D96; band-gap needs more | ✅ |
| `Y_T_001_NumericalEigenvalueConsistency` | closed-form DFT == MathNet EVD | ✅ |
| `Y_T_001_Run` | research report | ✅ |

## Classification

| Item | Classification |
|---|---|
| Inverse-map exactness | DERIVED |
| Isometry robustness | DERIVED |
| Physicality selects sparse band spectra | CORRESPONDENCE |
| Hypothesis (target spectrum → low-complexity graph) | SUPPORTED (D96, max-separated) / REFUTED (band-gap, cluster, extreme-octave) |

## Conclusion

Inverse spectral design is a closed-form inverse DFT for circulant graphs, exact
and optimally robust. D96 (3 octave bands, span 6.4) is the sparse all-physical
solution (6 edges, no repulsive coupling); discontinuous or extreme-span spectra
require dense + repulsive coupling. Next step: the general non-circulant
weighted-graph inverse spectral problem (ResearchY-T_002).
