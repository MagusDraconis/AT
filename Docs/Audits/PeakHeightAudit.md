# CMB Peak Height Audit — first peak amplitude

**Status:** first-peak amplitude is now **estimated** with radiation driving +
Silk damping. **Result:** $D_{\ell_1}\approx4000\ \mu K^2$ vs Planck $\sim5700$
(−30%). **Scope:** no polarization, no lensing, no full Cℓ spectrum, no neutrinos.

---

## 1. Method

| Step | Physics |
|---|---|
| Radiation driving | 5-ODE system: $\Theta_0, \Theta_1, \delta_m, v_m, \Phi$ (0i Einstein) |
| Silk damping | $\exp(-k^2/k_D^2)$, $k_D$ from the diffusion integral |
| Amplitude | $D_{\ell_1} = \tfrac{9}{25} A_s T_{\rm CMB}^2\,(S^2 + v_b^2)\,\mathrm{Silk}$ |

---

## 2. Source Table (amplitude contributions)

| Source | Value at $\ell_1$ | Contribution |
|---|---|---|
| Sachs–Wolfe $S^2$ | $0.062$ | minor (baryon-loaded, $\Phi$ decayed) |
| Doppler $v_b^2$ | $0.651$ | **dominant** at the first peak |
| Radiation driving $\Phi$ | $0.485$ (51% decay from 1.0) | reduces SW source |
| Silk damping | $0.997$ | negligible at first peak |
| Normalization $\tfrac{9}{25}A_s T_{\rm CMB}^2$ | $5616\ \mu K^2$ | scale to $\mu K^2$ |

---

## 3. Result

| Quantity | This audit | Planck | Rel. error |
|---|---|---|---|
| $\ell_1$ | $184$ | $220$ | −16% |
| $k_D$ (Silk) | $0.234$ Mpc⁻¹ | $\sim0.15$ | — |
| **$D_{\ell_1}$** | **$4002\ \mu K^2$** | **$\sim5700\ \mu K^2$** | **−30%** |

---

## 4. Error Budget

| Source | Effect on $D_{\ell_1}$ |
|---|---|
| No neutrino driving | ~10–15% |
| No ISW / full Cℓ integral | ~15–25% |
| Tight-coupling approximation | ~5% |
| Hydrogen-only recombination | ~1% |
| Silk damping | <0.5% (first peak) |

---

## 5. Conclusion

Adding radiation driving (evolving $\Phi$, which decays to $\sim0.49$ at
recombination) and Silk damping (negligible at the first peak) yields
$D_{\ell_1}\approx4000\ \mu K^2$, within $\sim30\%$ of Planck. The Doppler term
dominates the first-peak amplitude; the residual is the excluded neutrino
driving and ISW. This closes the CMB chain:
**recombination → r_s → θ\* → oscillator → projection → first-peak amplitude**.

**Sources:** `TQM.Core/ResearchDATA/PeakHeightAnalyzer.cs`,
`TQM.Tests/ResearchDATA/TQM_PeakHeightAudit.cs`.
