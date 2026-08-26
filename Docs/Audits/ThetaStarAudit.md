# Theta* Audit — r_s and θ* from background observables

**Status:** θ* is now **computable** from the existing modules (recombination + FLRW).
**Result:** $100\,\theta_* = 1.0263$ vs Planck $1.04092$ (**−1.4%**).
**Discipline:** background observables only — no perturbation theory, no Cℓ spectrum.

---

## 1. Method

| Step | Quantity | Formula |
|---|---|---|
| 1 | Baryon-to-photon ratio | $R(z) = \dfrac{3\Omega_b}{4\Omega_\gamma}\,(1+z)^{-1}$ |
| 2 | Sound speed | $c_s(z) = \dfrac{c}{\sqrt{3(1+R(z))}}$ |
| 3 | Sound horizon | $r_s = \displaystyle\int_{z_*}^{\infty} \dfrac{c_s(z)}{H(z)}\,dz$ |
| 4 | Comoving distance | $D_M(z) = \displaystyle\int_0^{z} \dfrac{c}{H(z)}\,dz'$ |
| 5 | Angular scale | $\theta_* = \dfrac{r_s}{D_M(z_*)}$ |

> Note: $\theta_* = r_s/D_M$ (comoving ratio) equals
> (physical $r_s$) / (angular-diameter $D_A$), since both are divided by $(1+z_*)$.

---

## 2. Inputs (Planck 2018)

| Quantity | Value |
|---|---|
| $\Omega_b h^2$ | $0.02237$ |
| $\Omega_m h^2$ | $0.1430$ |
| $\Omega_\gamma h^2$ | $2.469\times10^{-5}$ |
| $\Omega_r h^2$ | $4.183\times10^{-5}$ |
| $z_*$ | from Saha + Peebles (recombination module) |

---

## 3. Results vs Planck

| Quantity | This audit | Planck 2018 | Relative error |
|---|---|---|---|
| $z_*$ | $1081.8$ | $1089.9$ | −0.75% |
| $r_s$ | $142.33$ Mpc | $147.09$ Mpc (r_d) | −3.2% |
| $D_M(z_*)$ | $13868$ Mpc | $13770$ Mpc | +0.7% |
| $\theta_*$ | $1.0263\times10^{-2}$ rad | $1.04092\times10^{-2}$ rad | **−1.4%** |
| $100\,\theta_*$ | $1.02630$ | $1.04092$ | −1.4% |
| $c_s(z_*)/c$ | $0.4526$ | $\sim0.45$ | — |

---

## 4. Classification

| Quantity | Status |
|---|---|
| $R(z)$, $c_s(z)$, $r_s$, $D_M$, $\theta_*$ | **Imported** (standard FLRW background) |
| $z_*$ (recombination) | **Imported** — now *implemented* |
| $H(z)$ ($E(z)$) | **Already available** (Pantheon analyzers) |

None of these is **Derived** from AT primitives — θ* is computed entirely from
imported ΛCDM background physics, now wired together in AT.

---

## 5. Residuals

The −1.4% residual arises from:

| Source | Effect |
|---|---|
| Hydrogen-only (no helium) | $r_s$ slightly low (−3%) |
| Power-law $\alpha^{(2)}$ | $z_*$ slightly low |
| $\tau=1$ vs visibility-peak $z_*$ | small |

These are documented approximations, not new physics.

---

## 6. Conclusion

θ* is now a **computed background observable** in AT, obtained entirely from
standard FLRW + recombination, at **−1.4%** of Planck. This closes the
Acoustic-Gap chain: **recombination → $r_s$ → θ***, with **zero free AT
parameters** and **no perturbation theory**.

**Sources:** `AT.Core/ResearchDATA/RecombinationAnalyzer.cs` (ComputeThetaStar),
`AT.Tests/ResearchDATA/AT_ThetaStarAudit.cs`.
