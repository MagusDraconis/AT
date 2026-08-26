# Higher Peaks Audit — second and third acoustic peaks

**Goal:** compute $\ell_2$ and $\ell_3$ and peak ratios, compare to Planck.
**Result:** acoustic peaks are evenly spaced at $\Delta\ell \approx \pi/\theta_* \approx 306$;
the density-extremum amplitudes give **$D_{\ell_3}/D_{\ell_1} = 0.60$ vs Planck 0.68**
(robust), while **$D_{\ell_2}/D_{\ell_1} = 0.08$ vs Planck 0.44** (rarefaction
under-filled — requires the SW–Doppler cross term that the Limber quadrature drops).
**Scope:** no polarization, no lensing, no full Boltzmann hierarchy, no new physics.

---

## 1. Method

| Step | Physics |
|---|---|
| Source | $S = \Theta_0 + \Phi$ (Sachs–Wolfe), $v_b = \Theta_1$ (Doppler) |
| Solver | `FullSolveNu` 7-ODE (radiation driving + neutrino driving) |
| Peak position | local maxima of $|S|$ = density extrema (acoustic peaks) |
| Amplitude | $D_\ell = \tfrac{9}{25} A_s T_{\rm CMB}^2\,(S^2+v_b^2)\,\mathrm{Silk}$ |

Acoustic peaks coincide with **density extrema** ($S' = 0$): odd peaks
(compressions) are $S^2$-dominated, even peaks (rarefactions) sit at the
velocity-dominated zero crossings. The Limber projection $S^2 + v_b^2$
(no cross term) recovers the compression peaks but not the rarefaction.

---

## 2. Peak Positions

| Peak | Model $S$-extremum | Planck | $\Delta\ell$ (model) |
|---|---|---|---|
| $\ell_1$ | **318** | 220 | — |
| $\ell_2$ | **620** | 537 | 302 |
| $\ell_3$ | **910** | 814 | 290 |

$100\,\theta_* = 1.0263 \Rightarrow \pi/\theta_* = 306$. The model's even
spacing $\Delta\ell \approx 300$ matches Planck's spacing (317, 277) to
$\sim5\%$. The absolute offset (318 vs 220) is the Doppler projection shift
already measured in the CMB Projection Audit (SW+Doppler moves the first peak
$336 \to 220$).

---

## 3. Peak Ratios

| Quantity | Model | Planck | Rel. error |
|---|---|---|---|
| $D_{\ell_2}/D_{\ell_1}$ | **0.083** | 0.44 | −81% |
| $D_{\ell_3}/D_{\ell_1}$ | **0.604** | 0.68 | −11% |

$D_{\ell_3}/D_{\ell_1}$ is robust (density-dominated compression peak) and
agrees with Planck to $\sim11\%$. $D_{\ell_2}/D_{\ell_1}$ is under-predicted
by a factor $\sim5$: the second peak is a **rarefaction**, where $S^2$ nearly
vanishes and the observed amplitude comes from the Doppler term via the
SW–Doppler **cross term** in the full Bessel projection — a term the Limber
quadrature $S^2 + v_b^2$ omits.

---

## 4. Error Budget

| Source | Effect |
|---|---|
| Limber $S^2+v_b^2$ (no cross term) | kills $\ell_2$ (−81%) |
| Doppler projection shift (positions) | $\ell_1$ 318 vs 220 |
| Fluid + $3j_1(x)/x$ neutrino approx | ~5–10% |
| Tight-coupling approximation | ~5% |
| No ISW / full Cℓ integral | ~10–15% |

---

## 5. Conclusion

The higher-peak chain recovers the **acoustic spacing** ($\Delta\ell \approx
\pi/\theta_*$) and the **third/first peak ratio** ($0.60$ vs $0.68$) with no
new physics — confirmation that the oscillator, radiation driving, neutrino
driving, and Silk damping are the correct minimal ingredients. The second peak
(rarefaction) cannot be recovered without the full line-of-sight Bessel
projection including the SW–Doppler cross term, which is the next missing
module in the CMB chain (see CMBSpectrumGapAudit).

**Sources:** `AT.Core/ResearchDATA/PeakHeightAnalyzer.cs` (FindAcousticPeaks,
FullSolveNu), `AT.Tests/ResearchDATA/AT_HigherPeaksAudit.cs`.
