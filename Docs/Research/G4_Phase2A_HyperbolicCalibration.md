# G4 Phase 2A — Hyperbolic Calibration

**Program:** G4 — Native Metric-to-Operator Coupling
**Phase:** 2A (negative-curvature calibration)
**Status:** COMPLETED — 3/3 xUnit tests pass (nominal target met, with a documented caveat)
**Question:** Calibrate the SCI for **negative** curvature by replacing the open Poincaré disk
with **compact genus-≥2** hyperbolic surfaces.
**Target:** Sphere SCI > 0 · Flat SCI ≈ 0 · Hyperbolic SCI < 0

---

## 1. Goal

Phase 1 failed to produce SCI < 0 for the Poincaré disk (it is topologically a disk, χ = 1,
and boundary-dominated). This phase replaces it with compact, boundary-free hyperbolic
surfaces and re-tests the SCI.

---

## 2. Implementation

Compact genus-≥2 surfaces are built as generalized Petersen graphs (cubic, high girth,
negative Euler characteristic χ = 2 − 2g):

| Surface | Graph | N | degree | girth | genus g | χ |
|---|---|---|---|---|---|---|
| Desargues | G(10,3) | 20 | 3 | 6 | 2 | −2 |
| Nauru | G(12,5) | 24 | 3 | 6 | 4 | −6 |

Code: `AT.Core/ResearchXH/CompactHyperbolicGraph.cs`.

SCI is the Phase-1 definition (deviation of the heat-kernel spectral dimension from 2):

$$\mathrm{SCI}(t)=2t\,\langle\lambda\rangle(t)-2,\qquad t=1.5.$$

---

## 3. Results (measured, deterministic, normalized Laplacian)

| Geometry | N | gap | Z(1) | ζ(2) | **SCI(1.5)** |
|---|---|---|---|---|---|
| Flat torus | 256 | 0.0381 | 106.51 | 4296.2 | **−0.0527** |
| Sphere S² | 256 | 0.0653 | 97.79 | 1067.2 | **+0.5846** |
| Desargues (genus 2) | 20 | 0.3333 | 8.64 | 51.75 | **−0.2988** |
| Nauru (genus 4) | 24 | 0.3333 | 10.37 | 68.85 | **−0.2977** |

**Target check (nominal):**

| Target | Value | Verdict |
|---|---|---|
| Sphere > 0 | +0.5846 | ✅ |
| Flat ≈ 0 | −0.0527 | ✅ |
| Hyperbolic < 0 | −0.2988 | ✅ |

---

## 4. Critical finding — the SCI is degree-dependent, not curvature-signed

The nominal pass is **not** a genuine curvature calibration. Additional diagnostics show the
Phase-1 SCI $=2t\langle\lambda\rangle-2$ is **degree-dependent**:

| Test | Result |
|---|---|
| Low-degree sphere (deg 3.64) | SCI = **−0.14** (negative, not positive) |
| Dense sphere (deg 15.7) | SCI = +0.58 |
| Dodecahedron G(10,2) (cubic, χ=+2) | SCI ≈ −0.31 |
| Petersen G(5,2) (cubic, χ=0) | SCI ≈ −0.32 |
| Desargues G(10,3) (cubic, χ=−2) | SCI ≈ −0.30 |

Three cubic graphs with χ = +2, 0, −2 all yield **the same** SCI ≈ −0.30. The positive sphere
signal is therefore an artifact of its high degree (15.7), not of its positive curvature.

**Root cause:** the SCI measures the *mean eigenvalue under the heat kernel*, which for an
unweighted graph is set by the degree/spectral shape, not by the metric curvature. Abstract
unweighted cage graphs do **not** carry the embedding's curvature in their Laplacian spectrum —
curvature sign requires a **metric (weighted/ε-) graph** of the surface with intrinsic
distance, whose heat trace has the subleading χ/6 (Euler-characteristic) term.

---

## 5. Conclusion

1. **Nominal target met:** replacing the disk with compact genus-2+ surfaces flips the SCI to
   negative (sphere +0.58, flat −0.05, hyperbolic −0.30), as hypothesized.

2. **But the calibration is not yet genuine:** the SCI's sign is degree-dominated. A
   curvature-signed spectral estimator must be the heat-trace **Euler-characteristic term**
   $\chi/6$, which requires metric ε-graphs of the genus-2 surface (intrinsic hyperbolic
   distance) and the $N\to\infty$ asymptotics — deferred to Phase 2B.

3. **Program consequence:** this phase *corrects* Phase 1's positive-sphere result (degree
   artifact) and pins down the precise requirement for a native, sign-correct curvature
   operator: **density-invariant AND metric-weighted**.

---

## Test program

| Test | Verdict |
|---|---|
| G4-2A-00 `G4_2A_00_CompactHyperbolicGraphsAreValid` | PASS |
| G4-2A-01 `G4_2A_01_HeatTraceAndZetaCompute` | PASS |
| G4-2A-02 `G4_2A_02_SciSignCalibration` | PASS |

`AT.Tests/ResearchXH/G4Phase2AHyperbolicCalibrationTests.cs` (inherits `ResearchTestBase`,
deterministic, `StringBuilder`-composed reports).
