# Cluster Mass Audit

**Status:** Clusters — PARTIAL → **COMPLETE**.
**Scope:** reconstruct the cluster mass profile and compare Newtonian / ΛCDM / TQM
defect models. No new physics; existing TQM assumptions only (X063–X065 defect DM).

---

## Method

1. **Dynamical mass (Coma).** Compute the line-of-sight velocity dispersion
   $\sigma_v$ from the 689-galaxy Coma catalog, then the virial mass
   $M_{\rm vir} = 3\sigma_v^2 R_{\rm vir}/G$ with $R_{\rm vir}=1.4\ \mathrm{Mpc}$.
2. **Baryonic mass (Coma).** Adopt literature values for gas ($1.2\times10^{14}\ M_\odot$)
   and stars ($1.0\times10^{13}\ M_\odot$).
3. **Gas fraction (ACCEPT sample).** Integrate the X-ray electron density
   $n_e(r)$ over radial shells ($\rho_{\rm gas}=\mu m_p n_e$) and divide by the
   hydrostatic enclosed mass $M_{\rm grav}(r)$ for 222 clusters.
4. **Model comparison.** Newtonian (baryons only) vs ΛCDM (baryons + collisionless
   CDM) vs TQM defect model (baryons + topological-defect DM, X063–X065).

---

## Data Sources

| Source | File | Contents |
|---|---|---|
| Coma galaxy catalog | `Data/coma_v3344_ready.csv` | 689 galaxies: ra, dec, z, v_rest |
| ACCEPT profiles | `Data/Coma_Cluster_Chandra_temperature_all_profiles.dat` | $n_e(r)$, $P(r)$, $M_{\rm grav}(r)$, $T_x(r)$ |
| ACCEPT main | `Data/Coma_Cluster_Chandra_temperature_accept_main.tab` | $T_{\rm cl}$, $L_{\rm bol}$, entropy $K_0/K_{100}$ |

*Note: the ACCEPT files contain the full Chandra cluster sample (not Coma alone,
which is too nearby for ACCEPT).*

---

## Results

| Observable | Value |
|---|---|
| $N$ galaxies | 689 |
| $\sigma_v$ (Coma) | $946.7\ \mathrm{km/s}$ |
| $M_{\rm vir}$ (Coma) | $8.75\times10^{14}\ M_\odot$ |
| $M_{\rm baryon}$ (Coma) | $1.30\times10^{14}\ M_\odot$ |
| dynamical / baryon | $6.7\times$ |
| baryon fraction $f_b$ | $0.149$ |
| ACCEPT clusters | 222 |
| $f_{\rm gas}$ mean (median) | $0.152$ ($0.058$) |

**Model comparison:**

| Model | Cluster mass | Verdict |
|---|---|---|
| Newtonian (baryons only) | $\sim1.3\times10^{14}\ M_\odot$ | under-predicts by $6.7\times$ |
| ΛCDM (baryons + CDM) | $\sim8.8\times10^{14}\ M_\odot$ | matches ($f_b\approx0.15$ = cosmic) |
| TQM defect model | $\sim8.8\times10^{14}\ M_\odot$ | matches (defect DM is collisionless) |

**Derived / Fitted / Contingent:**

| Class | Quantities |
|---|---|
| DERIVED | $\sigma_v$, $M_{\rm vir}$ (virial theorem), $f_{\rm gas}$ ($n_e$ integral), hydrostatic equilibrium |
| FITTED | $R_{\rm vir}$, NFW concentration, gas-density profile parameters |
| CONTINGENT | exact $\Omega_{\rm DM}$ (TQM X065: $0.27$ not derivable — initial conditions) |

---

## Limitations

1. **Coma baryon mass is literature input**, not independently measured from the
   provided catalog (which has no gas/star masses).
2. **$R_{\rm vir}=1.4$ Mpc is a fitted/anchor value**; the virial mass scales as $R_{\rm vir}$.
3. **ACCEPT gas fraction** uses $M_{\rm grav}$ from the catalog's own hydrostatic
   deprojection (assumes hydrostatic equilibrium, a systematic shared by all models).
4. **TQM defect model is observationally degenerate with ΛCDM** at the mass-profile
   level — both require $\sim85\%$ dark mass; they differ only in DM identity, not
   in the reconstructed mass.
5. No direct TQM-specific cluster signature is computed here (no new physics).

---

## Encyclopedia Integration Notes

- **Coverage_Report.md** — move `Clusters` from PARTIAL (30%) → **COMPLETE**.
- **TQM_Encyclopedia.md** — Part VIII §8.5 "Clusters": replace TODO with the mass
  audit; source files `TQM.Core/ResearchDATA/ClusterMassAudit.cs` +
  `TQM.Tests/ResearchDATA/TQM_ClusterMassAudit.cs`.
- **Master reference** — cluster-scale result: TQM defect DM ≡ ΛCDM mass profile;
  the dark component is REQUIRED (6.7× discrepancy) and is **CONTINGENT** in
  abundance (X065), consistent with the DERIVED/DRAWN taxonomy.
