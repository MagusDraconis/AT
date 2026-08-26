# G4-T Phase 1 — Conformal Actualization

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-T)
**Phase:** 1 (conformal geometry from actualization-rate gradients)
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Do actualization-rate gradients generate **effective conformal geometry**?
**Chain:** ρ(x) → f(x) = ρ^(2/d) → conformally-flat metric g = f·η → curvature R = −(2/f)(ln ρ)″
**Primitives used:** Q-events · causal order · actualization rate (counting measure) — no new primitives.

---

## 1. Goal

Test whether a spatial gradient in the actualization rate (counting measure) ρ(x) — through the
native conformal factor f = ρ^(2/d) — produces a **conformal metric** with non-zero curvature,
and whether that conformal curvature is visible in the graph spectrum. Compare **rate-induced**
signals against **true** curvature (sphere / hyperbolic).

---

## 2. Implementation

`AT.Core/ResearchXH/ConformalRateGraph.cs` builds a flat square [−1,1]² with density
ρ(x) = 1 + a·x² (deterministic inverse-CDF placement), connected by a Euclidean ε-threshold
graph. The induced conformally-flat metric g = f·η (f = ρ in d=2) has analytic scalar curvature

$$R(x)=-\frac{2}{f}(\ln\rho)''=-\frac{4a(1-a x^2)}{(1+a x^2)^3},\qquad R(0)=-4a.$$

So a > 0 ⇒ R < 0 (negative), a < 0 ⇒ R > 0 (positive).

---

## 3. Results (measured, deterministic)

### 3.1 Unnormalized (density-weighted) Laplacian — L → ρ·Δ

| Geometry | R(0) | gap | ζ(2) | KS→flat |
|---|---|---|---|---|
| Flat (uniform) | 0 | 0.0384 | 1767.0 | 0.016 |
| Rate ρ=1+x² | **−4** | 0.0416 | 1012.4 | 0.254 |
| Rate ρ=1−0.8x² | **+3.2** | 0.0878 | 340.9 | 0.422 |
| Sphere (true +) | +2 | 1.0307 | 4.36 | 0.938 |
| Hyperbolic (true −) | −2 | 0.2174 | 143.0 | 0.391 |

### 3.2 Normalized (density-invariant) Laplacian — L_sym → Δ

| Geometry | gap | ζ(2) | KS→flat |
|---|---|---|---|
| Flat | 0.0106 | 23134.0 | 0.008 |
| Rate ρ=1+x² | 0.0070 | 30235.8 | 0.113 |
| Rate ρ=1−0.8x² | 0.0151 | 9391.5 | 0.152 |
| Sphere | 0.0653 | 1067.2 | 0.293 |
| Hyperbolic | 0.0468 | 2364.7 | 0.109 |

---

## 4. Findings

1. **Rate gradients DO define conformal geometry.** ρ(x) → f = ρ^(2/d) yields a conformally-flat
   metric with genuine curvature R(0) = −4a — an analytic, sign-definite result.

2. **But the graph Laplacian does not read the conformal sign.** Both R<0 (ρ=1+x²) and R>0
   (ρ=1−0.8x²) shift the *unnormalized* ζ(2) **downward** (1767 → 1012 and → 341) and increase
   the gap. The response is **magnitude-dominated** by the density gradient, not aligned with
   the conformal-curvature sign — the two opposite-sign profiles move the spectrum the *same*
   direction.

3. **The normalized Laplacian removes the density** (KS→flat falls from 0.254/0.422 to
   0.113/0.152), recovering flatness — density-invariance confirmed.

4. **True curvature is a different signal.** The sphere/hyperbolic ε-graphs (metric graphs of
   genuinely curved manifolds) show KS→flat ≈ 0.94/0.39 in the unnormalized operator — an order
   of magnitude beyond the rate-induced shifts. The conformal geometry ρ→f is real, but the
   plain graph Laplacian (normalized or not) is **not** the operator that reads it: that is the
   conformal Laplace–Beltrami Δ_g = ρ⁻¹Δ_η = L/ρ².

---

## 5. Conclusion

Actualization-rate gradients generate effective conformal geometry (a conformal factor and
hence curvature R = −(2/f)(ln ρ)″), but the **graph Laplacian's spectral response is
density-magnitude-dominated and sign-blind**; the density-invariant normalized Laplacian
suppresses it. Reading the conformal curvature *sign* requires the conformal operator
Δ_g = L/ρ² (weighted by the counting measure squared), not the plain graph Laplacian. This
sharpens the G4 program's central requirement: the native metric-operator must be
**density-invariant for the metric, but density-weighted for the conformal factor** — two
distinct roles that the unnormalized and normalized Laplacians each capture only partially.

---

## Test program

| Test | Verdict |
|---|---|
| G4-T1-00 `G4_T1_00_ConformalCurvatureAndBuilderValid` | PASS |
| G4-T1-01 `G4_T1_01_ObservablesForRateGradientsAndCurvature` | PASS |
| G4-T1-02 `G4_T1_02_RateInducedResponseIsMagnitudeDominated` | PASS |

`AT.Tests/ResearchXH/G4T_Phase1_ConformalActualizationTests.cs` (inherits `ResearchTestBase`,
deterministic, `StringBuilder`-composed reports).
