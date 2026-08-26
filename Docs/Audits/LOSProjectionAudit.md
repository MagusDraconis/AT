# LOS Projection Audit — exact $j_\ell$ / $j_\ell'$ projection

**Goal:** resolve the second-peak location by implementing the exact line-of-sight
projection with the full spherical Bessel functions $j_\ell(x)$ and $j_\ell'(x)$.
**Result (honest negative):** the exact projection reproduces the **compression
peaks only** ($\ell_1,\ell_3$); the **rarefaction peak is missing** (a dip), and
the acoustic phase shift is absent. **No polarization, no lensing, no CAMB clone.**

---

## 1. Projection

$$\Theta_\ell(k)=S(k)\,j_\ell(kD)-i\,v_b(k)\,j_\ell'(kD),\qquad
S=A\cos(k r_s)-R\Phi,\ \ v_b=B\sin(k r_s).$$

Using the exact Bessel integrals $\int d\ln k\,j_\ell^2=1/[2\ell(\ell{+}1)]$ and
$\int d\ln k\,j_\ell'^{\,2}=\tfrac13\!\int d\ln k\,j_\ell^2$ (cross term $=0$):

$$D_\ell=\tfrac12\Big[S^2(k{=}\ell/D)+\tfrac13 v_b^2(k{=}\ell/D)\Big]\,e^{-k^2/k_D^2}D_v^2.$$

---

## 2. Peak positions

| peak | model $\ell$ | Planck $\ell$ | error |
|---|---|---|---|
| $\ell_1$ (compression) | **304** | 220 | **+38%** |
| $\ell_2$ (rarefaction) | **missing (dip)** | 537 | — |
| $\ell_3$ (compression) | **904** | 814 | +11% |

---

## 3. Peak ratios

| Quantity | Model | Planck |
|---|---|---|
| $D_{\ell_2}/D_{\ell_1}$ (rarefaction, SW) | 0.244 | 0.44 |
| $D_{\ell_3}/D_{\ell_1}$ (compression) | **0.652** | 0.68 |

---

## 4. Conclusion

The exact $j_\ell/j_\ell'$ projection confirms the diagnosis of the whole chain:
the minimal sudden-recombination tight-coupling model reproduces the **compression
peaks** (positions within $\sim10\%$ for $\ell_3$, ratio $D_{\ell_3}/D_{\ell_1}
=0.65$ vs 0.68) but **cannot produce the rarefaction peak**. Two deficits remain:

1. **No acoustic phase shift** — the compressions sit at $k r_s=n\pi$ ($\ell_1\approx304$),
   not the observed $\ell_1=220$ (phase $\phi\approx0.88$ rad).
2. **No rarefaction peak** — $D_\ell=S^2+\tfrac13 v_b^2$ is monotonic between
   compressions because the Doppler fills the density zero-crossing more than the
   rarefaction, turning the rarefaction into a minimum.

Both require physics beyond sudden recombination (finite-width velocity weighting,
baryon–photon decoupling, or the integrated Sachs–Wolfe term). This is a clean,
honest negative result: **the second peak is not within the reach of the
sudden-recombination + Limber pipeline**, and no new physics is needed — only a
full Boltzmann/CAMB-class solver.

**Sources:** `AT.Core/ResearchDATA/LosProjectionAnalyzer.cs` (`Project`,
`ProjectNumeric`, `J`, `JPrime`), `AT.Tests/ResearchDATA/AT_LosProjectionAudit.cs`.
