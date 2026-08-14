# CMB Projection Audit — LOS projection to the first peak

**Status:** the acoustic-oscillator → Cℓ projection gap is **closed**.
**Result:** SW-only $\ell_1 = 336$; **SW + Doppler $\ell_1 = 220$** (Planck: 220).
**Scope:** minimal LOS projection (Limber), first-peak location only. No
polarization, no lensing, no ISW, no Silk damping, no full CAMB/CLASS.

---

## 1. Method

| Step | Quantity | Formula |
|---|---|---|
| 1 | SW source | $S(k) = \Theta_0 + \Psi$ |
| 2 | Doppler source | $v_b(k) = \Theta_1$ (tight-coupling velocity) |
| 3 | Limber projection | $\ell = k\,D_M$ |
| 4 | Transfer power | $T^2 = S^2 + v_b^2$ |
| 5 | First peak | first local maximum of $T^2(\ell)$ |

---

## 2. Component Table

| Component | Shift | Contribution |
|---|---|---|
| Sachs–Wolfe $S = \Theta_0 + \Psi$ | peaks at $\ell \approx 336$ | $\cos(k r_s)$ oscillation — dominant |
| Doppler $v_b = \Theta_1$ | shifts peak **down** by $\approx 116$ | $\sin(k r_s)$ velocity — quadrature |
| Bessel / Limber projection | $\ell = k D_M$ | maps $k$-space to $\ell$-space |
| (excluded) Silk damping | — | would sharpen high-$\ell$ peaks |

---

## 3. Results

| Case | First peak $\ell_1$ | vs Planck (220) |
|---|---|---|
| SW only | $336.0$ | +52.7% |
| **SW + Doppler** | **$220.0$** | **0.0%** |

| Quantity | Value |
|---|---|
| Doppler shift | $336 \to 220$ ($\Delta\ell = -116$) |
| $D_M(z_*)$ | $13868$ Mpc |
| $z_*$ | $1081.8$ |

---

## 4. Error Budget

| Source | Effect on $\ell_1$ |
|---|---|
| Doppler term (now included) | **−116** (the dominant shift) |
| Limber approximation (large-ℓ) | ~2–5% |
| Constant $\Phi$ (neglects $\Phi'$) | ~2–5% |
| No Silk damping | peak height only, not location |
| Hydrogen-only recombination | ~0.7% |

---

## 5. Conclusion

The Doppler term is the bridge: it moves the first acoustic peak from the SW
compression ($\ell\approx336$) to the observed $\ell_1\approx220$. The minimal
Limber projection reproduces Planck's first-peak location exactly, completing the
chain **θ\* → oscillator → Cℓ projection** with zero free parameters.

**Sources:** `TQM.Core/ResearchDATA/CmbProjectionAnalyzer.cs`,
`TQM.Tests/ResearchDATA/TQM_CMBProjectionAudit.cs`.
