# CMB Coverage Audit & Roadmap

**Question:** can TQM produce a *defensible* CMB chapter?
**Answer:** **PARTIAL** — TQM treats the CMB as a *constraint* and makes one
falsifiable *prediction*, but does **not** derive the CMB. No power spectrum, no
sound horizon, no recombination physics, no data fit exist in the repository.

---

## 1. Known (accepted ΛCDM CMB physics)

What TQM explicitly accepts from standard cosmology (not re-derived):

| Fact | Repository evidence |
|---|---|
| Acoustic peaks are photon–baryon oscillations | `DarkMatterAuditAnalyzer.cs` (X063) |
| Peaks depend precisely on $\Omega_b/\Omega_c$ ratio | X063: "CMB acoustic peaks require SPECIFIC $\Omega_b/\Omega_c$ ratio" |
| Planck anchors $\Omega_m h^2 = 0.1430\pm0.0011$ | `CosmologyAudit.cs` (X046b) |
| Recombination at $z\approx1100$ | `TQM_X046b_CosmologyHostileAudit.cs` |
| Peak positions set by $\theta_* = r_s/D_A$ | `ModelDependenceAnalyzer.cs` (QG-081) |
| Sound horizon $r_s$ requires an early-universe + recombination model | QG-081: $r_s$ classified **Inferred** |

---

## 2. Derived (what TQM derives *about* the CMB)

TQM derives **constraints and one prediction**, not the spectrum itself:

| Result | Content | Source |
|---|---|---|
| **CMB ⇒ collisionless DM required** | Correlation-only gravity cannot reproduce the peaks without a non-baryonic, photon-decoupled component — one of 4 cosmological failures | X063, `DarkMatterAuditAnalyzer.cs` |
| **Modified $\Lambda(t)$ ⇒ peak shift** | TQM's evolving $\Lambda$ ($\Lambda\propto H^2$, or $\Lambda=\alpha/\sqrt V$) was larger at recombination → shifts $D_A(z{=}1100)$ | X046b, `CosmologyAudit.cs` |
| **Peak-shift prediction ~0.5–1%** | "Shift in CMB acoustic peak positions ~0.5–1%, partially degenerate with $H_0$" — ranked "subtle, needs precision CMB" | X062, `ObservableDeviationAnalyzer.cs` |
| **$\theta_*$ unchanged under weak time-scale cosmology** | Weak TSC leaves $r_s/D_A$ invariant | QG-080, `TQM_QG080_TimeScaleCosmologyAudit.cs` |

**Status of the derived content:** these are *falsifiable predictions*, not
*derivations*. TQM says "the CMB would shift by X if $\Lambda(t)$ behaves so" —
it does not compute $C_\ell$.

---

## 3. Assumed (imported from ΛCDM, not derived)

TQM's CMB statements **assume** the entire FLRW perturbation framework:

| Assumption | Source |
|---|---|
| FLRW metric, $D_A = \chi/(1+z)$ | QG-081, `ModelDependenceAnalyzer.cs` |
| Linear perturbation theory (photon–baryon fluid) | X063, X046b |
| Recombination physics (Saha/Peebles) | QG-081 ($r_s$ "Inferred") |
| $C_\ell$ statistics / Planck likelihood | X046b (quotes Planck central values) |
| $r_s$, $\theta_*$, $D_A$ as model-dependent inferred quantities | QG-081 (all three flagged Inferred) |

---

## 4. Missing (mathematics absent from the repository)

| Missing item | Evidence of absence |
|---|---|
| CMB power spectrum $C_\ell$ derivation | no `C_l`/`Boltzmann`/`CLASS`-like code anywhere |
| Sound horizon $r_s$ computation from Q-events | $r_s$ only *referenced*, never integrated |
| Recombination model (Saha equation, ionization fraction) | no recombination solver |
| Photon–baryon acoustic oscillator | no perturbation/oscillation solver |
| Planck data + likelihood | no Planck file in `Data/`; no CMB fit |
| Cosmological-parameter inference from CMB | X046b only *quotes* Planck values |

> Note: the legacy `TRM_V2_2.pdf` described a "Runge–Kutta RK4 Fourier-space
> acoustic solver" — but that is **Rejected** legacy material (see
> `TRM_Reconciliation_Audit.md`), not current TQM code.

---

## 5. Required Data (to move PARTIAL → COMPLETE)

| Data | Purpose |
|---|---|
| Planck 2018 $C_\ell$ (TT/TE/EE, low-$\ell$ + high-$\ell$) | fit peak positions/amplitudes |
| Planck best-fit $\{\Omega_b h^2, \Omega_c h^2, n_s, \theta_*,\ \tau\}$ | baseline for the $\Lambda(t)$ shift test |
| BAO $r_d$ (DESI / 6dF / BOSS) | calibrate $r_s$ against the TQM modified background |
| (future) CMB-S4 / Simons Observatory | test the 0.5–1% peak-shift prediction (X062) |

---

## 6. Required Simulations (to derive, not just predict)

| Simulation | Output |
|---|---|
| TQM modified background: integrate $H(z)$ with $\Lambda(t)$ ($\Lambda\propto H^2$ or $\alpha/\sqrt V$) | $D_A(z{=}1100)$, peak-shift magnitude |
| Photon–baryon acoustic oscillator under TQM's $H(z)$ | $r_s$, $C_\ell$ peak positions |
| Recombination (Saha/Peebles) at TQM's $\Lambda(t)$ | ionization history, $z_{\rm rec}$ shift |
| Forecast: CMB-S4 sensitivity to a 0.5–1% $\theta_*$ shift | statistical power of the X062 test |

---

## 7. Classification & Encyclopedia Integration

- **Classification: PARTIAL** (was MISSING at 10%; upgraded to PARTIAL on
  evidence of X063/X046b/X062/QG-081).
- **Coverage_Report.md** — `CMB`: 10% MISSING → **PARTIAL ~45%** (constraint +
  prediction present; derivation absent).
- **TQM_Encyclopedia.md** — Part VIII §8.6 "CMB": content = X063 DM requirement,
  X046b/X062 peak-shift prediction; TODO = power-spectrum derivation, $r_s$
  computation, Planck fit.

**Bottom line:** TQM has a *defensible CMB chapter* only as a **constraint + one
prediction**, not as a derivation. Producing a full CMB chapter requires the
simulations in §6 — none of which exist, and none of which are invented here.
