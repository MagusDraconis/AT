# G4-C Phase 2 — Curvature Reconstruction

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-C)
**Phase:** 2 (reconstruct curvature from the native conformal operator Lc = ρ⁻¹ L ρ⁻¹)
**Status:** COMPLETED — 3/3 xUnit tests pass (SC1–SC4 all satisfied)
**Question:** Can curvature be inferred from spectral observables of Lc?
**Primitives used:** ρ · L · Lc · spectral observables. No metric tensor, no Laplace–Beltrami import.

---

## 1. Goal

Given the Phase-1 result (Lc separates curvature sign, is refinement-stable, degree-artifact-free,
and Laplace–Beltrami-like), this phase tests whether the **curvature sign and ordering** can be
**reconstructed** from Lc's spectral observables alone.

## 2. Geometries (conformal, ρ = 1 + a·x², via `ConformalRateGraph`)

| Class | a | ρ(x) | known R(0) |
|---|---|---|---|
| Negative / hyperbolic | +1.0 | 1 + x² | −4 |
| Flat | 0.0 | 1 | 0 |
| Positive / sphere | −0.8 | 1 − 0.8x² | +3.2 |

(The density ρ is the conformal factor f = ρ^(2/d); these are the three curvature-sign classes.)

## 3. Reconstruction candidate

For Lc eigenvalues {λ_k}, compute four observables — spectral gap λ₁, heat trace Z(1), spectral
zeta ζ(2), spectral entropy S(1) — and sum their normalized deviations from the flat reference
(each with sign fixed so + ⇒ R > 0):

$$S = \frac{\lambda_1-\lambda_1^f}{\lambda_1^f} + \frac{Z^f-Z}{Z^f} + \frac{\zeta^f-\zeta}{\zeta^f} + \frac{S^f_{\text{ent}}-S_{\text{ent}}}{S^f_{\text{ent}}}.$$

Reconstructed sign = sign(S). Code: `AT.Core/ResearchXH/CurvatureReconstruction.cs`.

## 4. Results (measured, deterministic)

| Geometry | known R | recon score S | recon sign | match |
|---|---|---|---|---|
| Negative | −4 | **−3.240** | −1 | ✅ |
| Flat | 0 | 0.000 | 0 | ✅ |
| Positive | +3.2 | **+4.335** | +1 | ✅ |

Ordering: **−3.240 < 0.000 < +4.335** ⇒ R<0 < R=0 < R>0. ✅

Refinement (n=16 → n=24): signs stay (−1, 0, +1). ✅

Mean degree: negative 5.16, flat 3.75, positive 6.33 — the correct signs across these different
degrees confirm degree-insensitivity.

## 5. Success-criteria assessment

| Criterion | Requirement | Verdict |
|---|---|---|
| **SC1** | Recovered sign(R) | ✅ (−1 / 0 / +1) |
| **SC2** | Recovered ordering R<0 < R=0 < R>0 | ✅ −3.24 < 0 < +4.34 |
| **SC3** | Stable under refinement | ✅ n=16→24 |
| **SC4** | Insensitive to degree variation | ✅ signs correct across degrees 5.16/3.75/6.33 |

## 6. Conclusion

Curvature (sign **and** ordering) is recovered from the spectral observables of the native
conformal operator **Lc = ρ⁻¹ L ρ⁻¹**, using only ρ, L, Lc and spectral observables — no metric
tensor, no Laplace–Beltrami import. This completes the G4-C objective: Lc is a native operator
that both *behaves like* and *allows reconstruction of* the Laplace–Beltrami curvature.

---

## Test program

| Test | Verdict |
|---|---|
| G4-C20 `G4_C20_RecoveredSignMatchesKnownCurvature` | PASS (SC1+SC4) |
| G4-C21 `G4_C21_RecoveredOrderingIsCurvatureConsistent` | PASS (SC2) |
| G4-C22 `G4_C22_ReconstructionStableUnderRefinement` | PASS (SC3) |

`AT.Tests/ResearchXH/G4C_Phase2_CurvatureReconstructionTests.cs` (inherits `ResearchTestBase`,
deterministic, `StringBuilder`-composed reports).
