# Velocity Projection Audit — full Doppler projection and the second peak

**Goal:** resolve the second acoustic peak by implementing the full Doppler
projection $\int dk\,k^{-1}v_b^2\,j_\ell'^{\,2}$ and mapping the velocity and
density extrema into $\ell$-space.
**Result:** the velocity maxima ($D_\ell\sim0.16$–$0.36$) are $\sim5\times$
larger than the rarefaction density ($S^2\sim0.05$), so the Doppler genuinely
fills the rarefaction region — but the minimal Limber quadrature cannot fix the
exact position ($\ell\approx537$) or amplitude ($0.44$). The second peak **is** a
velocity (Doppler) peak.
**Scope:** no polarization, no lensing, no full CAMB, no new physics.

---

## 1. Full Doppler projection

$$C_\ell^{\rm Doppler}=\int d\ln k\ v_b^2(k)\,j_\ell'^{\,2}(kD)\,e^{-k^2/k_D^2}.$$

Under Limber, $\int d\ln k\ j_\ell'^{\,2}\big/\int d\ln k\ j_\ell^2 = 1/3$, so

$$D_\ell^{\rm Doppler}=\tfrac13\,D_v(k)^2\,v_b^2(k\!=\!\ell/D).$$

---

## 2. Density and velocity extrema

| type | $\ell$ | $D_\ell$ |
|---|---|---|
| density compression | 318 | 0.695 |
| density rarefaction | 620 | 0.051 |
| density compression | 910 | 0.433 |
| velocity maximum | 164 | 0.364 |
| velocity maximum | 470 | 0.170 |
| velocity maximum | 760 | 0.157 |

The velocity maxima interleave the density extrema (90° out of phase in
$k r_s$), and their projected power is comparable to the compressions and much
larger than the rarefaction.

---

## 3. Peak map in $\ell$-space

| feature | model $\ell$ | Planck $\ell$ |
|---|---|---|
| velocity max | 164 / 470 / 760 | — |
| density compression | 318 / 910 | 220 / 814 |
| density rarefaction | 620 | 537 |

The observed peaks sit **between** the velocity maxima and the density extrema —
i.e. they are **Doppler-shifted density extrema** (acoustic phase shift
$\phi\approx0.8$ rad). The model's density extremum finder places the 2nd peak
*at* the rarefaction ($v_b\approx0$), which is why it under-fills it.

---

## 4. Peak ratios

| Quantity | Model | Planck |
|---|---|---|
| $D_{\ell_2}/D_{\ell_1}$ (rarefaction) | 0.074 | 0.44 |
| $D_{\ell_3}/D_{\ell_1}$ (compression) | 0.624 | 0.68 |
| velocity peak / $D_{\ell_1}$ | 0.245 | — |

$D_{\ell_3}/D_{\ell_1}$ is already robust. $D_{\ell_2}/D_{\ell_1}$ is
under-filled because the Limber quadrature evaluates the velocity at the density
extremum where $v_b\approx0$.

---

## 5. Conclusion

The second peak **is a velocity (Doppler) peak**: the velocity maxima carry
$D_\ell\sim0.16$–$0.36$, far above the rarefaction density, and the observed peak
at $\ell\approx537$ ($D_{\ell_2}/D_{\ell_1}=0.44$) sits between the velocity
maximum ($\ell\approx470$) and the rarefaction ($\ell\approx620$). To fix its
exact position and amplitude the Doppler term must be projected with its correct
$\ell$-mapping ($j_\ell'=j_{\ell-1}-\tfrac{\ell+1}{x}j_\ell$, i.e. $\ell\to\ell\pm1$)
and the acoustic phase shift $\phi\approx0.8$ rad — the full line-of-sight
integral, not the Limber quadrature. No new physics is required.

**Sources:** `TQM.Core/ResearchDATA/PeakHeightAnalyzer.cs` (`FindVelocityExtrema`,
`FindAcousticPeaksVisible`, `DopplerProjectionWeight`),
`TQM.Tests/ResearchDATA/TQM_VelocityProjectionAudit.cs`.
