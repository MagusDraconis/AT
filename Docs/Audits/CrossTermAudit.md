# SW-Doppler Cross Audit — second-peak deficit

**Goal:** resolve the second-peak deficit by implementing the SW-Doppler
interference (cross) term.
**Result (honest negative):** the cross term is **exactly zero** (the monopole
and dipole enter the line-of-sight integral with relative phase $-i$), so it
cannot fill the rarefaction peak. The correct Doppler projection weight is
**$w_D = 1/3$** (the original code used $1$), but this barely moves the ratios
because $v_b \approx 0$ at the density extrema. **$D_{\ell_2}/D_{\ell_1}$ stays
$\sim0.08$ vs Planck 0.44.** The second peak requires the full Bessel projection
+ visibility-function Doppler damping — the next missing module.

**Scope:** no polarization, no lensing, no full Boltzmann hierarchy, no new physics.

---

## 1. The cross term is exactly zero

The line-of-sight temperature transfer function (sudden recombination) is

$$\Theta_\ell(k) = i^{-\ell}\Big[\,S\,j_\ell(kD) - i\,v_b\,j_\ell'(kD)\,\Big],
\qquad S = \Theta_0+\Phi,\ \ v_b = \Theta_1 .$$

The monopole (SW) and dipole (Doppler) carry the relative phase $-i$, so

$$|\Theta_\ell|^2 = S^2 j_\ell^2 + v_b^2\,j_\ell'^{\,2}
   + 2\,S\,v_b\,\mathrm{Re}\!\big[-i\,j_\ell j_\ell'\big]
 = S^2 j_\ell^2 + v_b^2\,j_\ell'^{\,2} .$$

$\mathrm{Re}[-i]=0 \Rightarrow$ **the cross term vanishes identically.** The SW
and Doppler contributions add in quadrature; there is no interference to recover.

---

## 2. Correct Doppler projection weight

Under the line-of-sight projection with measure $d(\ln k)$:

$$w_D = \frac{\int d\ln k\ j_\ell'^{\,2}}{\int d\ln k\ j_\ell^2} = \frac{1}{3}
\qquad \text{(dipole/monopole angular-average ratio).}$$

Verified numerically $\approx 0.333$ for $\ell=150\ldots900$. The original
quadrature $T^2 = S^2 + v_b^2$ implicitly used $w_D=1$, over-weighting the Doppler
term by $3\times$.

---

## 3. Peak ratios — before / after

| Quantity | BEFORE $w_D=1$ | AFTER $w_D=\tfrac13$ | Planck |
|---|---|---|---|
| $D_{\ell_2}/D_{\ell_1}$ | **0.083** | **0.075** | 0.44 |
| $D_{\ell_3}/D_{\ell_1}$ | **0.604** | **0.624** | 0.68 |

$D_{\ell_3}/D_{\ell_1}$ is already robust (density-dominated compression peak,
$\sim0.6$ vs 0.68) and improves slightly with $w_D=1/3$. $D_{\ell_2}/D_{\ell_1}$
is unchanged: at the density extrema the velocity $v_b=\Theta_1\propto\sin(kr_s)$
vanishes, so no Doppler weight can fill the rarefaction there.

---

## 4. Why the second peak is still missing

The second peak is the **Doppler-shifted rarefaction**: it sits at $\ell\approx537$,
between the velocity maximum ($\ell\approx459$) and the rarefaction
($\ell\approx620$), where $v_b$ is non-zero. The Limber quadrature (and the
density-extremum finder) place the peak at the rarefaction, where $S$ and $v_b$
are both small. Filling the shifted peak requires the **full Bessel projection**
($\int dk\,k^{-1}\,v_b^2\,j_\ell'^{\,2}$) plus the **visibility-function Doppler
damping** (the finite recombination width averages out the velocity), which are
the next modules in the CMB chain — out of scope here.

---

## 5. Conclusion

The second-peak deficit is **not** a missing SW-Doppler cross term (that term is
identically zero). It is the **sudden-recombination + Limber** limit of the
minimal pipeline. The correct Doppler weight ($w_D=1/3$) is now implemented and
slightly improves $D_{\ell_3}/D_{\ell_1}$ (0.60 → 0.62 vs Planck 0.68), but the
rarefaction peak requires the full line-of-sight projection. No new physics is
needed — only the next computational module.

**Sources:** `AT.Core/ResearchDATA/PeakHeightAnalyzer.cs` (`CrossTermWeight`,
`DopplerProjectionWeight`, `FindAcousticPeaks`), `AT.Tests/ResearchDATA/AT_CrossTermAudit.cs`.
