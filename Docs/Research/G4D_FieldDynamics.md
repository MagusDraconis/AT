# G4-D Phase 1 — Field Dynamics (Local Curvature Fields)

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-D)
**Phase:** 1 — from mean density to spatial curvature fields
**Status:** COMPLETED — 3/3 xUnit tests pass (6/6 G4-D)

---

## 1. Goal

Move beyond the Phase-0 mean-field (a single scalar R(0,t) from the mean density) to **local**
curvature fields: a local density ρ(x,t) should generate a **local curvature map** R̂(x,t).

Method (native, no new primitives): the **diagonal heat kernel** of Lc = ρ⁻¹ L ρ⁻¹,

```
K_t(x_i) = Σ_k e^(−t λ_k) φ_k(x_i)² ,   R̂(x_i) = (K_geo(x_i) − K_flat(x_i)) / K_flat(x_i)
```

whose deviation from flat encodes the local scalar curvature via the heat-kernel expansion
K_t(x,x) ≈ (4πt)^(−d/2)(1 + (t/6)R(x) + …). Only ρ, L, and the spectral decomposition enter.

---

## 2. Results (Gaussian-bump density ρ(x) = 1 + ½·e^(−(x/σ)²), σ = 0.5)

### Local curvature map (G4-D10)

| metric | value |
|---|---|
| Pearson(R̂, R_analytic) | **0.9559** |
| R̂(center) sign vs analytic R(0) sign | +1 vs +1 ✅ |
| localization | \|R̂(center)\| = 1.031 ≫ \|R̂(tail)\| = 0.050 |

### Propagation (G4-D11)

A moving bump x₀(t) = −0.6 → +0.6: the peak of R̂(x) tracks x₀(t) with
**Pearson(peak, x₀) = 0.9950** (all 7 frames within ~1.5 grid cells).

### Field vs mean-field + stability (G4-D12)

| metric | value |
|---|---|
| local R̂(center) | +1.031 (sign +1) |
| analytic R(0) | +3.369 (sign +1) |
| global (mean-field) score | **−1.822 (sign −1)** |
| field spatial spread | 1.018 (localizes) |
| refinement n=16 → 20 | Pearson = **0.9963** |

---

## 3. Conclusion

**Yes — native field-level curvature dynamics are achieved.**

- **Local ρ(x) generates local R(x)**: the diagonal heat kernel of Lc reconstructs the local
  curvature map with Pearson 0.956 against the analytic conformal curvature, correctly localized
  at the density bump.
- **Propagation**: the curvature peak tracks a moving density perturbation (Pearson 0.995).
- **Stability**: the map is refinement-stable (Pearson 0.996 from n=16 → 20).
- **Field resolves what the mean field misses**: for the Gaussian bump, the local R̂(center) has the
  **correct** sign (+1, matching analytic R(0) > 0), while the Phase-0 **global score is inverted**
  (−1). The mean-field aggregate (gap + ζ + entropy) is calibrated for the x² profiles and
  misattributes a localized perturbation — exactly the failure mode that motivates field-level
  (local) curvature dynamics.

**No new primitives**: the field is built from ρ and L alone (uniform-grid adjacency with a
density field + the spectral decomposition).

---

## Test program

| Test | Verdict |
|---|---|
| G4-D10 `G4_D10_LocalDensityGeneratesLocalCurvature` | PASS (Pearson 0.956, localized, correct sign) |
| G4-D11 `G4_D11_CurvaturePeakPropagatesWithDensity` | PASS (peak tracks x₀, Pearson 0.995) |
| G4-D12 `G4_D12_FieldIsStableAndResolvesWhatMeanFieldMisses` | PASS (field correct, mean-field inverted, refinement 0.996) |

Code: `TQM.Core/ResearchXH/CurvatureField.cs` (+ `SpectralCurvature.LocalHeatKernel`);
tests `TQM.Tests/ResearchXH/G4D_Phase1_FieldDynamicsTests.cs`.
