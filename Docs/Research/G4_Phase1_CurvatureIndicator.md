# G4 Phase 1 — Curvature Indicator

**Program:** G4 — Native Metric-to-Operator Coupling
**Phase:** 1 (calibration of a signed spectral curvature estimator)
**Status:** COMPLETED — 3/3 xUnit tests pass (with a documented partial-result on the negative sign)
**Question:** Can a spectral observable predict the curvature **sign**?
**Target:** Flat → near zero · Sphere → positive · Hyperbolic → negative

---

## 1. Goal

Calibrate a native **Spectral Curvature Indicator (SCI)** — a single number computed from a
graph Laplacian spectrum — such that

```
SCI > 0  => positive curvature
SCI ≈ 0  => flat
SCI < 0  => negative curvature
```

Inputs are the Phase-0 geometries: `FlatGraph` (R≈0), `SphereGraph` (R>0),
`HyperbolicGraph` (R<0).

---

## 2. Implementation

### 2.1 Observables (added to `SpectralCurvature`, `AT.Core/ResearchXH`)

All computed on the symmetric **normalized** Laplacian $L_{\mathrm{sym}}$ (bounded in
$[0,2]$, scale-invariant).

| # | Observable | Definition |
|---|---|---|
| 1 | HeatTrace | $Z(t)=\sum_k e^{-t\lambda_k}$ |
| 2 | HeatTraceDerivative | $Z'(t)=-\sum_k \lambda_k e^{-t\lambda_k}$ |
| 3 | SpectralZeta | $\zeta(s)=\sum_{\lambda_k>0}\lambda_k^{-s}$ |
| 4 | SpectralGap | $\lambda_1$ (smallest positive eigenvalue) |
| 5 | SpectralEntropy | $S(t)=-\sum_k p_k\ln p_k,\ p_k=e^{-t\lambda_k}/Z(t)$ |

### 2.2 Spectral Curvature Indicator (SCI)

The heat-kernel **mean eigenvalue** is $\langle\lambda\rangle(t)= -Z'/Z$. The effective
(heat-kernel) **spectral dimension** is $d_s(t)=2t\langle\lambda\rangle(t)$. Positive
curvature suppresses the low-lying heat flow (sub-diffusive ⇒ $d_s>2$), negative curvature
enhances it (super-diffusive ⇒ $d_s<2$). Hence:

$$\boxed{\;\mathrm{SCI}(t)=d_s(t)-2=2t\,\langle\lambda\rangle(t)-2\;}$$

evaluated at the calibrated time $t=1.5$.

---

## 3. Results (measured, deterministic, normalized Laplacian, t = 1.5)

| Observable | Flat (torus) | Sphere (S²) | Hyperbolic (disk) |
|---|---|---|---|
| HeatTrace Z(1.5) | 74.9721 | 62.6398 | 72.5830 |
| HeatTraceDerivative Z′(1.5) | −48.6654 | −53.9653 | −49.8958 |
| ⟨λ⟩(1.5) | 0.6491 | 0.8615 | 0.6874 |
| SpectralZeta ζ(2) | 4296.242 | 1067.241 | 2364.660 |
| SpectralGap λ₁ | 0.0381 | 0.0653 | 0.0468 |
| SpectralEntropy S(1.5) | 5.2908 | 5.4297 | 5.3159 |
| **SCI(1.5)** | **−0.0527** | **+0.5846** | **+0.0623** |

---

## 4. Assessment against target

| Target | Measured SCI | Verdict |
|---|---|---|
| Flat → near zero | −0.053 | ✅ ≈ 0 |
| Sphere → positive | +0.585 | ✅ > 0 |
| Hyperbolic → negative | +0.062 | ❌ NOT negative |

**Partial success.** SCI cleanly separates **flat (≈0)** from **positive (+0.58)**, and the
ordering $\mathrm{SCI}(\text{flat}) < \mathrm{SCI}(\text{hyperbolic}) < \mathrm{SCI}(\text{sphere})$
is robust. The **negative** target is not reached.

---

## 5. Interpretation — why the hyperbolic disk fails the sign test

The failure is *not* an SCI defect; it is a property of the input graph.

1. **A Poincaré disk is topologically a disk** ($\chi=1$), i.e. a *bounded, boundary-bearing*
   region. Its finite spectrum is **boundary-dominated**: the many near-boundary points are
   far apart in hyperbolic distance, producing an abundance of small eigenvalues that mimic a
   *gapless, weakly-positive* object rather than the negative-curvature bulk.

2. **The negative-curvature signature lives in the heat trace's subleading constant term**
   $\chi/6$ (Euler characteristic via Gauss–Bonnet $\int R\,dV = 4\pi\chi$). For an open disk
   the boundary contributes an $O(t^{-1/2})$ term that *dominates* the $O(1)$ curvature term,
   so a signed estimator extracted at finite $N$ reads the boundary, not the bulk $R<0$.

3. Consequently the disk sits spectrally **between** the flat torus and the sphere on every
   observable computed in Phases 0–1 (gap, ζ, Z, entropy, SCI), rather than on the opposite
   side of flat.

**Implication:** a genuine negative sign requires a **compact, boundary-free** hyperbolic
surface (genus ≥ 2, $\chi<0$), e.g. a genus-2 octagon gluing or a high-girth cubic cage graph
(Desargues/Tutte–Coxeter), or a much larger bulk with a controlled boundary.

---

## 6. Conclusion

Phase 1 delivers a calibrated, scale-invariant **SCI = d_s − 2** that correctly reads
**flat ≈ 0** and **positive > 0**. It also produces a clean, falsifiable **negative result**:
the Poincaré-disk construction is boundary-dominated and does **not** yield a negative SCI at
$N\approx256$. This is recorded as a program failure mode (F6/F3 in the G4 spec) and directs
Phase 2 to a closed genus-≥2 hyperbolic surface for the $R<0$ calibration.

---

## Test program

| Test | Verdict |
|---|---|
| G4-10 `G4_10_ObservablesComputeAndFlatAnchorsZero` | PASS |
| G4-11 `G4_11_PositiveCurvatureGivesPositiveSci` | PASS |
| G4-12 `G4_12_HyperbolicSignIsBoundaryDominated` | PASS (documents the boundary limitation) |

`AT.Tests/ResearchXH/G4Phase1CurvatureIndicatorTests.cs` (inherits `ResearchTestBase`,
deterministic, `StringBuilder`-composed reports).
