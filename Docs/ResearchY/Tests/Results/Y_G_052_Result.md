# Y_G_052 - Result

**Audit:** ResearchY-G_052 - Amplitude Phase Audit
**Verdict:** **DERIVED**
**Tests:** 7/7 PASSED

## Answer

ρ − mean = **A + P** uniquely, with the interface an **identity**: **(phase) = kernel of the contractions (53)**,
**(amplitude) = contractions' row space minus the mean (42 = 43 − 1)**.

## Measurements

| quantity | value |
|---|---|
| amplitude / phase / state dimensions | **42 / 53 / 95** |
| sector overlap | **4.418E-015** |
| reconstruction residual | **2.442E-015** |
| basis-independence residual | **3.349E-012** |
| contraction rank / amplitude+mean | **43 / 43** |
| contraction–phase overlap | **7.111E-014** |
| linear additivity residual | **2.220E-016** |
| cross-term scaling: clock / accel / field | **0.2498 / 0.2503 / 0.2502** (quadratic) |

## Two harness bugs caught by the measurements

1. The alternative spanning set used **30** combinations for a **42**-dimensional subspace, so it could never reproduce
   the projection (**0.172**) - a defect of the *check*, fixed by spanning the full sector and asserting its rank.
2. Additivity was first measured on **maxima** of responses, which is not additive even when every component is
   (residual scaling **0.499**, i.e. spurious first order); it is now measured on the **response vectors**, giving the
   true **0.25** second-order cross term. The step also no longer passes through the normalising perturbation helper,
   whose clamping makes the map nonlinear in the step.
