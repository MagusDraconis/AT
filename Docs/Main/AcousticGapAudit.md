# Acoustic Gap Audit

**Goal:** minimal path from current TQM to a *computable* θ*.
**Scope:** θ* = r_s / D_A only (no C_l derivation; no acoustic-model invention).

---

## 1. Dependencies for θ*

| # | Dependency | Definition | Needed by |
|---|---|---|---|
| 1 | Background $H(z)$ | FLRW expansion (TQM: $w(z)$ or $\Lambda(t)$) | $r_s$, $D_A$ |
| 2 | $\rho_\gamma$, $T_{\rm CMB}$ | radiation density (2.7255 K) | $c_s$ |
| 3 | $\Omega_b h^2$ | baryon density (Planck) | $c_s$, recombination |
| 4 | Sound speed $c_s^2 = c^2/[3(1+R)]$, $R = 3\rho_b/4\rho_\gamma$ | baryon-to-photon ratio | $r_s$ |
| 5 | Recombination $z_*$ | photon decoupling redshift | $r_s$, $D_A$ |
| 6 | $r_s = \int_0^{t_*} c_s\,dt$ | sound horizon | θ* |
| 7 | $D_A = \int_0^{z_*} dz/H(z) \cdot (1+z)^{-1}$ | angular diameter distance | θ* |

---

## 2. Component Classification

| Component | Status | Difficulty | Dependency |
|---|---|---|---|
| $H(z)$ | **Already Available** | Low | `EofZ()` in `PantheonRealityCheckAnalyzer.cs` / `PantheonDetectabilityAnalyzer.cs` |
| $D_A$ | **Already Available** | Low | same integrator ($D_A = D_L/(1+z)^2$) |
| $c_s$ (sound speed) | **Imported** | Low | standard $R = 3\Omega_b/4\Omega_\gamma$; no derivation needed |
| **recombination ($z_*$)** | **Missing** | **Medium** | Saha / Peebles ionization; $\Omega_b h^2$, $T_{\rm CMB}$, $H(z)$ |
| photon–baryon fluid | **Missing** | High | Boltzmann perturbation system |
| acoustic oscillator | **Missing** | High | photon–baryon fluid |

---

## 3. Smallest Missing Module

| Item | Value |
|---|---|
| **Smallest missing module** | **recombination redshift $z_*$** (Saha/Peebles ionization ODE) |
| Why | θ* needs only $r_s$ and $D_A$ — both background integrals; neither needs the perturbation/oscillator system |
| Already have | $H(z)$, $D_A$, $c_s$ (imported), $\Omega_b h^2$, $T_{\rm CMB}$ |
| After adding | $z_* \Rightarrow r_s \Rightarrow \theta_* = r_s/D_A$ is fully computable |

---

## 4. Minimal Path

| Step | Action | Status |
|---|---|---|
| 1 | Integrate $H(z)$ for $D_A(z_*)$ | Already Available |
| 2 | Form $c_s(z)$ from $R = 3\Omega_b/4\Omega_\gamma$ | Imported |
| 3 | Solve Saha/Peebles for $z_*$ | **Missing → add** |
| 4 | Integrate $r_s = \int c_s\,dt$ | composable after step 3 |
| 5 | θ* = $r_s/D_A$ | computable |

**Conclusion:** the photon–baryon fluid and acoustic oscillator are **not on the
critical path** to θ*. The single missing module is the **recombination redshift**
(Saha equation) — a self-contained, ~medium-difficulty ODE that needs no new physics.
