# Recombination Audit — minimal z* solver

**Status:** the Acoustic-Gap "smallest missing module" is now **IMPLEMENTED**.
**Result:** $z_* = 1081.8$ (Planck 2018: $1089.9$, within $0.75\%$).
**Discipline:** standard ΛCDM recombination only; no Cℓ solver; no perturbation theory.

---

## 1. Model (minimal, hydrogen-only)

| Step | Physics | Type |
|---|---|---|
| 1 | Saha equation → $X_e(z_{\rm hi})$ initial condition | algebraic |
| 2 | Peebles ODE → $X_e(z)$ | 1 ODE |
| 3 | Optical depth $\tau(z) = \int \sigma_T n_e\, ds$ | 1 quadrature |
| 4 | $z_*$ where $\tau(z_*) = 1$ | root find |

---

## 2. Equations

**Saha equilibrium ionization (1920):**

$$\frac{X_e^2}{1-X_e} = \frac{1}{n_H}\left(\frac{m_e k_B T}{2\pi\hbar^2}\right)^{3/2} e^{-E_{\rm ion}/k_B T}$$

**Peebles non-equilibrium ODE (1968):**

$$\frac{dX_e}{dz} = \frac{C}{H(1+z)}\ \alpha^{(2)}\left[\, n_H X_e^2 - \left(\frac{m_e k_B T}{2\pi\hbar^2}\right)^{3/2}(1-X_e)\, e^{-E_{\rm ion}/k_B T}\right]$$

**Peebles suppression factor:**

$$C = \frac{1 + K\Lambda_{2s\to1s}\, n_{1s}}{1 + K(\Lambda_{2s\to1s}+\beta^{(2)})\, n_{1s}},\qquad
K = \frac{\lambda_\alpha^3}{8\pi H},\qquad
\beta^{(2)} = \alpha^{(2)}\left(\frac{m_e k_B T}{2\pi\hbar^2}\right)^{3/2} e^{-E_{2s}/k_B T},\qquad
n_{1s} = (1-X_e)\, n_H$$

**Case-B recombination coefficient:** $\alpha^{(2)}(T) = 2.84\times10^{-13}\,(T/10^4\ \mathrm{K})^{-0.7}\ \mathrm{cm^3\,s^{-1}}$

**Optical depth:** $\tau(z) = \int_0^z \dfrac{\sigma_T\, X_e\, n_H\, c}{H(z')(1+z')}\, dz'$

> Note: the bracket's equilibrium is exactly the Saha equation ($E_{\rm ion}=13.6$ eV);
> the $E_{2s}=3.4$ eV exponential appears only in $\beta^{(2)}$ (the Peebles C-factor).
> This is what makes the Saha IC the exact equilibrium (non-stiff ODE).

---

## 3. Required Constants (Planck 2018)

| Constant | Value |
|---|---|
| $T_{\rm CMB,0}$ | $2.7255$ K |
| $H_0$ | $67.36\ \mathrm{km\,s^{-1}Mpc^{-1}}$ |
| $\Omega_b h^2$ | $0.02237$ |
| $\Omega_m h^2$ | $0.1430$ |
| $\Omega_r h^2$ | $4.183\times10^{-5}$ |
| $E_{\rm ion}$ (H 1s) | $13.6057$ eV |
| $E_{2s}$ (H 2s) | $3.3995$ eV |
| $\sigma_T$ (Thomson) | $6.6525\times10^{-29}\ \mathrm{m^2}$ |
| $\Lambda_{2s\to1s}$ | $8.22458\ \mathrm{s^{-1}}$ |
| $\lambda_\alpha$ (Ly-α) | $1215.67$ Å |
| $m_e,\ k_B,\ \hbar,\ m_H,\ c,\ G$ | CODATA |

---

## 4. ODE Count & Implementation Effort

| Item | Count |
|---|---|
| Differential equations | **1** ($dX_e/dz$) |
| Algebraic (Saha IC) | 1 |
| Quadratures ($\tau$) | 1 |
| Free AT parameters | **0** (all imported ΛCDM constants) |

| Effort metric | Value |
|---|---|
| Code | ~130 lines C# |
| Integrator | RK4, 80 000 steps, $\Delta z \approx 0.02$ |
| Runtime | < 0.1 s |
| Accuracy | $z_*$ within $0.75\%$ of Planck |

---

## 5. Classification

| Quantity | Status | Basis |
|---|---|---|
| Saha | **Imported** | equilibrium ionization (Saha 1920) |
| Peebles | **Imported** | non-equilibrium correction (Peebles 1968) |
| $X_e(z)$ | **Imported** | standard recombination (now *implemented*) |
| $z_*$ | **Imported → computable** | from $X_e(z)$ + optical depth; **not** AT-derived |

None of these is **Derived** from AT primitives — recombination is standard
ΛCDM physics. It was **Missing** before this audit and is now **Available**.

---

## 6. Result & Next Step

| Quantity | This solver | Planck 2018 |
|---|---|---|
| $z_*$ | $1081.8$ | $1089.9$ |
| $X_e(z_*)$ | $0.103$ | ~$0.1$ |

- Residual ($0.75\%$) from: hydrogen-only (no helium), power-law $\alpha^{(2)}$,
  and the $\tau=1$ vs visibility-peak definition of $z_*$.
- **Next step toward θ\***: integrate $r_s = \int c_s\, dt$ (sound speed $c_s$ is
  imported; $H(z)$ already available) — the photon–baryon fluid is *not* required.

**Sources:** `AT.Core/ResearchDATA/RecombinationAnalyzer.cs`,
`AT.Tests/ResearchDATA/AT_RecombinationAudit.cs`.
