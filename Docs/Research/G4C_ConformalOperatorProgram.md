# G4-C Phase 0 — Conformal Operator Program

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-C)
**Phase:** 0 (native conformal operators)
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Can a density-weighted graph operator reproduce conformal curvature effects
**without importing Δ_g**?
**Starting point:** f = ρ^(2/d)
**Primitives used:** Q-events · causal order · actualization rate (counting measure) — no new primitives.

---

## 1. Goal

Test the density-weighted operator family and identify which operator is **most sensitive to
curvature sign** and **least sensitive to degree artifacts**.

Operator family (all built as symmetric matrices so their spectrum is real):

| Operator | Matrix | Continuum limit |
|---|---|---|
| Unnormalized L | D − A | ρ·(−Δ_η) |
| Normalized | D^(−1/2) L D^(−1/2) | −Δ_η (degree-normalized) |
| ρ⁻¹ (sym) | ρ^(−1/2) L ρ^(−1/2) | −Δ_η (analytic-density-normalized) |
| ρ⁻² (sym) | ρ^(−1) L ρ^(−1) | ρ⁻¹(−Δ_η) = −Δ_g (conformal) |

Code: `TQM.Core/ResearchXH/ConformalOperator.cs`; density carried by `GeometricGraph.VertexDensity()`
(set by `ConformalRateGraph`).

---

## 2. Method

On three flat geometries with known conformal curvature (ρ(x) = 1 + a·x² ⇒ R(0) = −4a):
**flat** (a=0, R=0), **R<0** (a=+1, R=−4), **R>0** (a=−0.8, R=+3.2), compute for each operator
the heat trace, spectral zeta ζ(2), Weyl dimension, and the **curvature-sign separation** —
whether R<0 and R>0 fall on opposite sides of flat.

---

## 3. Results (measured, deterministic)

| Operator | flat ζ(2) | R<0 ζ(2) | R>0 ζ(2) | separation | sign-separates? |
|---|---|---|---|---|---|
| L (unnorm) | 1767.0 | 1012.4 | 340.9 | 0.38 | ❌ (sign-blind) |
| D^(−1/2) L D^(−1/2) | 23134.0 | 30235.7 | 9391.5 | 0.90 | ✅ |
| ρ^(−1/2) L ρ^(−1/2) | 1767.0 | 2264.5 | 173.2 | 1.18 | ✅ |
| **ρ^(−1) L ρ^(−1)** | 1767.0 | **5614.9** | **110.3** | **3.12** | ✅ (largest) |

(separation = |ζ2(R<0) − ζ2(R>0)| / ζ2(flat).)

All operators recover Weyl dimension d ≈ 1.8–2.1 (the density weighting rescales, but does not
change, the 2-dimensional character).

---

## 4. Findings

1. **The unnormalized Laplacian is sign-blind.** L → ρ·(−Δ_η) is density-weighted, so its
   spectral response is a *magnitude* artifact: both opposite-sign gradients move ζ(2) downward.

2. **The density-normalized operators separate the sign.** `D^(−1/2) L D^(−1/2)`,
   `ρ^(−1/2) L ρ^(−1/2)` and `ρ^(−1) L ρ^(−1)` all put R<0 **above** flat and R>0 **below**
   flat in ζ(2) — they read the conformal-curvature sign.

3. **The conformal operator ρ^(−1) L ρ^(−1) wins.** It has the largest separation (3.12 vs
   0.90 for the degree-normalized operator), and because it uses the **analytic density ρ**
   (not the degree, a noisy proxy), it is the **least degree-artifact-prone**.

4. **No Δ_g was imported.** The conformal operator ρ^(−1)Lρ^(−1) is built purely from the
   adjacency and the counting measure (ρ = actualization rate) — it reproduces conformal
   curvature effects natively, confirming the G4-T chain ρ → f = ρ^(2/d) → R.

---

## 5. Conclusion

**Answer: yes.** A density-weighted graph operator reproduces conformal curvature effects
without importing Δ_g. The winner is

$$\boxed{\;\rho^{-1}\,L\,\rho^{-1}\;\approx\;-\Delta_g\;}$$

— the native conformal operator, most sensitive to curvature sign and least sensitive to
degree artifacts. This fixes the G4-T Phase-1 gap (the plain graph Laplacian was sign-blind)
and supplies the concrete operator for the G4 program's native metric-to-operator coupling.

---

## Test program

| Test | Verdict |
|---|---|
| G4-C-00 `G4_C_00_OperatorsAreValid` | PASS |
| G4-C-01 `G4_C_01_ObservablesPerOperator` | PASS |
| G4-C-02 `G4_C_02_CurvatureSignSeparation` | PASS |

`TQM.Tests/ResearchXH/G4C_ConformalOperatorTests.cs` (inherits `ResearchTestBase`,
deterministic, `StringBuilder`-composed reports).
