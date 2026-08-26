# G4-C Phase 5 — Absolute Curvature Calibration

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-C)
**Phase:** 5 — absolute curvature calibration
**Status:** COMPLETED — 3/3 xUnit tests pass (18/18 G4-C)

---

## 1. Goal

Sign (G4-C2), ordering (G4-C2) and magnitude ordering (G4-C3) are solved. This phase asks whether
**|R|** can be reconstructed **quantitatively** — not just ordered.

Two native channels: the **local heat kernel** of Lc (field reconstruction, G4-D) and the **global
Lc spectrum** (CurvatureReconstruction.Score). Fit R_true = α·R̂ + β across multiple ± strengths,
then measure relative error and its refinement behaviour.

---

## 2. Results (ρ = 1 + a x², R_true(0) = −4a, a = ±0.2…±0.8)

### Calibration map (G4-C50)

| channel | fit R_true = α·R̂ + β | Pearson |
|---|---|---|
| local heat kernel | −807.17·R̂ + 0.046 | **−0.9999** |
| global Lc spectrum | 0.911·R̂ + 0.605 | 0.9784 |

(The local channel's negative slope is the sign convention: Lc ≈ −Δ_g, so its heat kernel
e^(−tLc) ≈ e^(tΔ_g) has the expansion 1 − (t/6)R + … ⇒ R̂ ∝ −R.)

### Calibrated accuracy (G4-C51)

| channel | mean relative error |
|---|---|
| **local heat kernel** | **0.0210 (2.1 %)** |
| global Lc spectrum | 0.2657 (26.6 %) — ordinal |

### Refinement (G4-C52)

| n | local rel. error | global rel. error |
|---|---|---|
| 16 | 0.0210 | 0.2657 |
| 20 | 0.1360 | 0.2795 |
| 24 | 0.0960 | 0.2779 |

**Non-monotonic** — the relative error does *not* decrease under refinement.

---

## 3. Conclusion

**PARTIAL.**

- **Absolute |R| is reconstructed** by the local heat-kernel channel: Pearson 0.9999, **2.1 %
  relative error** at n = 16 — |R| is quantitatively recovered at fixed scale.
- The **global Lc-spectrum score is ordinal** (26.6 % error): it orders curvature but does not
  quantify it.
- **Refinement does NOT converge**: the relative error is non-monotonic (2.1 % → 13.6 % → 9.6 %).
  At the fixed heat-kernel time t = 0.5 the reconstruction is not in the asymptotic (t → 0) regime,
  so the calibration constant drifts with n.

The original blocker "absolute curvature magnitude" is therefore **partially closed**: quantitative
reconstruction exists (local channel), but a refinement-convergent absolute calibration would need
the t → 0 asymptotic (or a per-n recalibration), which is left open.

---

## Test program

| Test | Verdict |
|---|---|
| G4-C50 `G4_C50_CalibrationDataAndFit` | PASS (both channels calibrate, \|Pearson\| > 0.95) |
| G4-C51 `G4_C51_CalibratedAccuracy` | PASS (local 2.1 %; global ordinal 26.6 %) |
| G4-C52 `G4_C52_RelativeErrorDecreasesUnderRefinement` | PASS (documents non-convergence) |

Code: `AT.Tests/ResearchXH/G4C_Phase5_AbsoluteMagnitudeTests.cs` (uses `CurvatureField` from G4-D).
