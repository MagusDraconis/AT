# Visibility Function Audit — finite-width recombination

**Goal:** resolve the second-peak deficit by implementing the visibility function
$g(z)$ (finite-width recombination) and projecting the Doppler term through it.
**Result (honest):** $g(z)$ is now computed from the Peebles recombination history
($z_{\rm peak}=1073.1$, $\sigma_\eta=21.3\ \text{Mpc}$), giving a Doppler
visibility damping $D_v(k)=e^{-k^2c_s^2\sigma_\eta^2/2}$. This is a **small
correction** ($D_v^2=0.98,\ 0.87,\ 0.73$ at $\ell=220,\ 537,\ 814$) and does **not**
resolve the second peak: **$D_{\ell_2}/D_{\ell_1}=0.074$ vs Planck 0.44**.
**Scope:** no polarization, no lensing, no full CAMB, no new physics.

---

## 1. Visibility function

$$g(z)=\frac{\sigma_T n_e c}{H(z)(1+z)}\,e^{-\tau(z)},
\qquad n_e=X_e(z)\,n_H(z),$$

with $X_e(z)$ from the Saha + Peebles ODE (hydrogen-only). The visibility
function is peaked where $\tau=1$:

| Quantity | Value |
|---|---|
| $z_{\rm peak}$ | **1073.1** |
| $\sigma_\eta$ (conformal RMS width) | **21.3 Mpc** |
| $c_s(z_*)/c$ | 0.4526 |
| $c_s\,\sigma_\eta$ | 9.64 Mpc |

---

## 2. Doppler visibility damping

The velocity $v_b(\eta)=B\sin(k r_s(\eta))$ is averaged over $g(\eta)$. For a
Gaussian visibility this gives

$$D_v(k)=\exp\!\big(-k^2 c_s^2 \sigma_\eta^2/2\big).$$

| $\ell$ | $k$ [Mpc$^{-1}$] | $D_v$ | $D_v^2$ |
|---|---|---|---|
| 220 | 0.0159 | 0.9884 | 0.9769 |
| 537 | 0.0387 | 0.9327 | 0.8699 |
| 814 | 0.0587 | 0.8521 | 0.7260 |

---

## 3. Peak ratios — before / after visibility

Projection: $D_\ell = S^2 + \tfrac13 D_v(k)^2 v_b^2$ at the density extrema
($|S|$ maxima), $S=\Theta_0+\Phi$.

| Quantity | BEFORE $D_v=1$ | AFTER $D_v<1$ | Planck |
|---|---|---|---|
| $D_{\ell_2}/D_{\ell_1}$ | 0.075 | **0.074** | 0.44 |
| $D_{\ell_3}/D_{\ell_1}$ | 0.624 | **0.624** | 0.68 |

The Doppler visibility damping barely moves the ratios because $v_b \approx 0$
at the density extrema (the velocity is $90^\circ$ out of phase with $S$).

---

## 4. Conclusion

Finite-width recombination is now implemented and quantified, but it is a
**second-order effect** ($\sim1$–$15\%$ in Doppler power at the peaks). It cannot
resolve the second-peak deficit because the second peak is the **Doppler-shifted
rarefaction** at $\ell\approx537$ (not the density extremum at $\ell\approx620$),
where the velocity is non-zero but the density extremum finder never visits.
Resolving it requires the **full Bessel projection** $\int dk\,k^{-1}v_b^2j_\ell'^{\,2}$
with the correct $\ell$-mapping of the Doppler term — the next module, not a
visibility effect. No new physics is needed.

**Sources:** `AT.Core/ResearchDATA/RecombinationAnalyzer.cs`
(`VisibilityWidth`, `ConformalTimeMpc`), `PeakHeightAnalyzer.cs`
(`DopplerVisibilityDamping`, `FindAcousticPeaksVisible`),
`AT.Tests/ResearchDATA/AT_VisibilityAudit.cs`.
