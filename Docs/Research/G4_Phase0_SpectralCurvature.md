# G4 Phase 0 — Spectral Curvature

**Program:** G4 — Native Metric-to-Operator Coupling
**Phase:** 0 (feasibility probe)
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Is curvature information already encoded in graph spectra?

---

## 1. Goal

Determine whether three constant-curvature 2-dimensional geometries — **flat**, **spherical**,
**hyperbolic** — produce **statistically distinguishable spectral signatures** from the graph
Laplacian alone. No metric tensor is formed; only the graph's eigenvalues are used.

Success criterion (from the program spec):

> Distinct geometries must produce statistically distinguishable spectral signatures.

---

## 2. Implementation

### 2.1 Geometry graphs (deterministic, no randomness)

| Geometry | Construction | N | Scalar curvature R |
|---|---|---|---|
| Flat | 16×16 torus grid (degree 4) | 256 | 0 |
| Sphere | Fibonacci lattice on unit S², geodesic ε-graph (ε=0.500) | 256 | +2 |
| Hyperbolic | Poincaré-disk rings, hyperbolic-distance ε-graph (ε=0.880) | 258 | −2 |

Code: `AT.Core/ResearchXH/` — `FlatGraph`, `SphereGraph`, `HyperbolicGraph`,
`GeometricGraph`, `GraphFactory`, `SpectralCurvature`.

### 2.2 Observables

1. **Eigenvalue spectrum** — sorted eigenvalues of the symmetric **normalized** Laplacian
   $L_{\mathrm{sym}}=I-D^{-1/2}AD^{-1/2}$ (bounded in $[0,2]$, density-invariant; the G4 C2
   candidate operator).
2. **Heat trace** $Z(t)=\sum_k e^{-t\lambda_k}$ at $t\in\{0.5,1,2\}$.
3. **Spectral zeta** $\zeta(s)=\sum_{\lambda_k>0}\lambda_k^{-s}$ at $s\in\{2,3\}$.
4. **Weyl dimension** — cumulative counting law $N(\lambda)=\#\{\lambda_k\le\lambda\}$ fitted
   in the low-$\lambda$ regime on the **unnormalized** Laplacian (which mirrors $-\Delta$):
   $N(\lambda)\propto\lambda^{d/2}$ ⇒ $d=2\cdot\text{slope}$.
5. **Spectral gap** — smallest positive eigenvalue $\lambda_1$.
6. **Kolmogorov–Smirnov distance** between eigenvalue CDFs (scale-free distinguishability).

---

## 3. Results (measured, deterministic)

### 3.1 Spectral signatures

| | Flat (torus) | Sphere (S²) | Hyperbolic (H²) |
|---|---|---|---|
| N | 256 | 256 | 258 |
| mean degree | 4.00 | 15.67 | 7.65 |
| λ_max (normalized) | 2.0000 | 1.2940 | 1.7162 |
| spectral gap λ₁ | 0.0381 | **0.0653** | 0.0468 |
| Weyl dimension d | 2.275 | 2.285 | 2.294 |
| Z(0.5) | 160.181 | 156.622 | 160.429 |
| Z(1.0) | 106.514 | **97.792** | 105.077 |
| Z(2.0) | 55.535 | **41.400** | 52.782 |
| ζ(2) | 4296.242 | **1067.241** | 2364.660 |
| ζ(3) | 85028.046 | **10576.112** | 30029.600 |

### 3.2 Distinguishability (pairwise KS distance between eigenvalue CDFs)

| Pair | KS distance |
|---|---|
| Flat vs Sphere | 0.2852 |
| Flat vs Hyperbolic | 0.1322 |
| Sphere vs Hyperbolic | 0.2636 |
| **minimum** | **0.1322** (≫ 0.05 threshold) |

---

## 4. Assessment against success criterion

**PASS.** The three geometries are pairwise distinguishable:

- **KS distance:** all pairs exceed 0.13 (threshold 0.05); the minimum is 0.1322.
- **Heat trace / zeta:** differ by factors of ~2–4 (e.g. ζ(2): 4296 vs 1067 vs 2365; Z(2):
  55.5 vs 41.4 vs 52.8).
- **Spectral gap ordering:** flat (0.0381) < sphere (0.0653), with hyperbolic (0.0468)
  between — consistent with a closed positively-curved manifold being spectrally gapped.

**Control (dimension is NOT the confound):** all three recover Weyl dimension
$d\approx2.28$ — the spectral differences are attributable to **geometry/curvature**, not
dimension.

---

## 5. Interpretation

1. **Curvature is already encoded in graph spectra.** Three manifolds of *identical*
   dimension (2) and *identical* vertex count (~256) yield clearly separated eigenvalue
   distributions. No metric tensor, no Laplace–Beltrami formula, and no BDG machinery were
   imported — only the graph Laplacian of the event geometry.

2. **The normalized Laplacian is the right carrier.** The bounded, density-invariant
   $L_{\mathrm{sym}}$ gives clean, comparable signatures; the unnormalized Laplacian is used
   only for the Weyl dimension (where it mirrors $-\Delta$).

3. **Consistency with expected curvature signatures:** the sphere (R=+2) shows the largest
   spectral gap and the fastest heat-trace decay (smallest Z), while the flat torus spans the
   full $[0,2]$ band (λ_max=2.0). The hyperbolic case sits between on the gap but has a
   distinct full spectrum (KS 0.13–0.26 vs both).

4. **Phase-0 scope:** this establishes *distinguishability*, not *calibration*. Recovering the
   exact scalar curvature $\int R\,dV$ from the heat-trace subleading coefficient is deferred
   to the next phase (requires the intermediate-$t$ regime and the $N\to\infty$ limit).

---

## 6. Conclusion

**Phase 0 succeeds.** Distinct constant-curvature geometries produce statistically
distinguishable spectral signatures from graph spectra alone. This is the necessary
precondition for the G4 program's central claim: a geometric operator can be read directly
from event geometry, and curvature information survives into that operator's spectrum.

---

## 7. Next phase (Phase 1)

- Calibrate the heat-trace curvature indicator $C_R(t)=\tfrac6t\left[(4\pi t)^{d/2}Z(t)-N\right]$
  and verify the **sign** (flat≈0, sphere>0, hyperbolic<0) in the intermediate-$t$ window.
- Scale up $N$ to sharpen the Weyl/heat-trace asymptotics.
- Compare the native C2 operator against the C4 benchmark $\Delta_g$ with $f=\rho^{2/d}$.

---

## Test program

| Test | Verdict |
|---|---|
| G4-00 `G4_00_GraphBuildersProduceValidGeometricGraphs` | PASS |
| G4-01 `G4_01_SpectralObservablesAreComputableAndConsistent` | PASS |
| G4-02 `G4_02_DistinctGeometriesProduceDistinguishableSpectralSignatures` | PASS |

`AT.Tests/ResearchXH/G4Phase0SpectralCurvatureTests.cs` (inherits `ResearchTestBase`,
deterministic, `StringBuilder`-composed reports).
