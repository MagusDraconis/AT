# CMB Spectrum Gap Audit — from θ* to the first acoustic peak

**Goal:** minimal dependency path from the computed θ* to a first-peak power
spectrum. **Scope:** dependency analysis only — no new physics, no solver.

**Legend:** **Available** = already in AT code; **Imported** = standard ΛCDM
formula (no new physics, not yet wired); **Missing** = requires a new (standard)
solver/derivation.

---

## 1. Component Classification

| Component | Status | Basis |
|---|---|---|
| Acoustic oscillator ($\Theta_0,\Theta_1$) | **Missing** | coupled photon–baryon perturbation ODEs; no solver in AT |
| Baryon loading $R(z)=3\rho_b/4\rho_\gamma$ | **Available** | already in `SoundSpeed` ($c_s$ uses $R$) |
| Radiation driving ($\Phi$) | **Missing** | gravitational potential from radiation perturbations |
| Silk damping ($k_D$) | **Imported** | diffusion integral over $R(z)$ + $n_e(z)$ — both available |
| Transfer functions ($\Theta(k,z_*)$) | **Missing** | acoustic solution at recombination (output of the oscillator) |
| $C_\ell$ generation | **Imported** | Sachs–Wolfe + Doppler line-of-sight integral |

---

## 2. Minimal Path (θ* → first peak)

| # | Step | Status | Needs |
|---|---|---|---|
| 1 | Peak position $\ell_1 \approx \pi/\theta_*$ | **Available** | θ* (computed) |
| 2 | Baryon loading $R(z)$ | **Available** | $\Omega_b,\Omega_\gamma$ (already used) |
| 3 | Tight-coupling oscillator $\Theta_0(k,\tau)$ | **Missing** | 2nd-order driven ODE |
| 4 | Radiation driving $\Phi(\tau)$ | **Missing** | Einstein equation (adiabatic IC) |
| 5 | Silk damping $e^{-k^2/k_D^2}$ | **Imported** | $R(z)$, $X_e(z)$ (available) |
| 6 | $C_\ell$ projection (SW + Doppler) | **Imported** | $\Theta(k,z_*)$ from step 3–5 |

---

## 3. Smallest Missing Module

| Item | Value |
|---|---|
| **Smallest missing module** | **Tight-coupling oscillator** ($\Theta_0$ driven by $\Phi$) |
| Form | 1 second-order ODE (≡ 2 first-order), plus 1 for $\Phi$ |
| Why minimal | Silk damping, $R(z)$, and the $C_\ell$ projection are already standard/imported |
| After adding | $\Theta(k,z_*) \Rightarrow$ first-peak $C_\ell$ via SW + Doppler |

---

## 4. ODE / Module Count

| Component | Count |
|---|---|
| Differential equations (new) | 2 (oscillator) + 1 ($\Phi$) |
| Available (reused) | θ*, $R$, $c_s$, $X_e$, $H(z)$ |
| Imported (plug-in formulas) | Silk $k_D$, SW + Doppler $C_\ell$ |
| Free AT parameters | 0 |

---

## 5. Dependency Summary

| From | To | Status |
|---|---|---|
| θ* | $\ell_1$ | Available |
| $R(z)$ | oscillator equilibrium | Available |
| $X_e(z),R(z)$ | Silk $k_D$ | Imported (computable) |
| oscillator + $\Phi$ | $\Theta(k,z_*)$ | **Missing** |
| $\Theta(k,z_*)$ | $C_\ell$ (first peak) | Imported formula |

---

## 6. Bottom Line

| Verdict | Result |
|---|---|
| Smallest gap | tight-coupling acoustic oscillator (+ $\Phi$) |
| Everything else | Available (θ*, R, c_s, X_e) or Imported (Silk, SW, Doppler) |
| Post-gap | first acoustic peak $C_\ell$ computable, no free parameters |

The first peak **position** is already computable ($\ell_1 \approx \pi/\theta_*$).
The first peak **amplitude/shape** requires only the tight-coupling oscillator +
radiation driving — a 2–3 ODE module, after which the Silk-damping factor and the
Sachs–Wolfe + Doppler projection complete the spectrum from imported formulas.
