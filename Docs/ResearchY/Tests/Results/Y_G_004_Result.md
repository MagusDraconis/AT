# Y_G_004_Result.md — ResearchY-G_004 Gravity Calibration Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_004_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (33 ms)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_004"`

---

## Summary

**Question:** Can a_AT be calibrated to measured gravity?

**Answer:** **CALIBRATED** at three of four scales (Earth, Sun–Earth, Galaxy RAR) with no free
parameters; **REFUTED** at the cluster scale for the modified-gravity channel; the RAR interpolating
**form** is **CORRELATED**.

| # | scale | a_obs [m/s²] | a_pred [m/s²] | a_pred/a_obs | fitted params | verdict |
|---|---|---|---|---|---|---|
| 1 | Earth surface | 9.820250 | 9.780965 | **0.99600** | `M_⊕` | **CALIBRATED** |
| 2 | Sun–Earth (1 AU) | 5.930084e-3 | 5.906361e-3 | **0.99600** | `GM_☉` | **CALIBRATED** |
| 3 | Galaxy RAR | 1.200e-10 | 1.04220e-10 = cH₀/2π | **0.86850** | **NONE** | **CALIBRATED** |
| 4 | Cluster (Coma) | 6.3650e-11 | 3.2944e-11 | **0.51759** | `f_b = 0.15` | **REFUTED** (mod-gravity) |

## Detail

- **Earth / Sun–Earth.** The residual is exactly the derived-`G` offset `G_AT/G_CODATA = 0.9959996`
  (0.40%, QG181). `GM_⊕/R_⊕² = 9.820250 m/s²` is the like-for-like comparator; the geophysical
  effective `g = 9.80665` is 0.13% below it (centrifugal + oblateness). Independent checks: GPS
  **+38.5 vs +38.6 μs/day** (0.26%, QG187), orbital potential term **45.74 vs 45.7**, perihelion
  **+42.98 ″/century** (QG103), PPN **γ = β = +1** with ψ (QG212).
- **Galaxy RAR (parameter-free).** `g† = cH₀/(2π) = 1.04220e-10 m/s²`: **0.86850** vs the unweighted
  literature mean 1.1683e-10 (6 determinations, scatter 6.5%, spread 17%), and **0.9226** vs the
  project's combined `a₀/cH₀ = 0.1725` (AT: 1/(2π) = 0.159155). Zero fitted parameters — `a₀` is not
  an input.
- **Cluster (the failure).** Coma `R_500 = 1.48 Mpc`, `M_500 = 1e15 M_☉` → `a_obs = 6.3650e-11 m/s² =
  6.49e-12 g = 0.61 g†`. From baryons only (`a_bar = 0.15 a_obs = 9.5475e-12`): AT/RAR form gives
  3.2944e-11 (**1.93× short**), MOND 3.8857e-11 (1.64× short). Matches the project's X063 /
  `AT_ClusterMassAudit`: "AT modified gravity insufficient at cluster scale"; AT matches clusters only
  through the deficit-as-mass channel (mass inventory, ~85% dark, ratio 4–10×; `Ω_DM` not derived, X065).

## Critical question — why is a 10⁻⁶ g effect not already observed locally?

| step | result |
|---|---|
| (a) 10⁻⁶ g needs, at 15 kpc, | `Δln ρ = 0.15151` — a unit-amplitude whole-lattice reconfiguration |
| (b) the realised field (observed RAR) | `g† = 1.0627e-11 g`, **9.41e4 × below** 10⁻⁶ g |
| (c) the ambient gradient | `d ln ρ/dr = d·g†/c² = 3.4788e-27 m⁻¹` |
| (d) a **uniform** cosmic gradient would give | a constant 1.0627e-11 g everywhere — **~104× above** the ~10⁻¹² m/s² ephemeris bound → **REFUTED** |
| (e) the resolution | a **point-like** deficit gives `M_eff → const`, i.e. exactly `a = −G_AT M/r²` (G4-ME22) with no anomalous term; the flat-curve 1/r regime needs a scale-free **extended** deficit (G4-ME21) absent locally |

**Conclusion:** 10⁻⁶ g is a counterfactual, not a prediction. The realised AT signatures are `G` at
0.40%, the GPS potential at 0.26% and the perihelion at <0.1%; galaxy-wide the AT field is ≤ 1.06e-11 g.
Consistent with all existing gravity tests.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_G_004_Anchors` | input inventory; nothing fitted | ✅ |
| `Y_G_004_EarthScale` | 0.99600; GPS; effective-vs-gravitational `g` | ✅ |
| `Y_G_004_SunEarthScale` | 0.99600; ψ-sector perihelion/PPN | ✅ |
| `Y_G_004_GalaxyRar` | 0.86850 / 0.9226 with zero fitted parameters | ✅ |
| `Y_G_004_ClusterScale` | 1.93× / 1.64× shortfall | ✅ |
| `Y_G_004_LocalBoundCritical` | the five-step critical answer | ✅ |
| `Y_G_004_Run` | research report | ✅ |

## Classification

DERIVED: `G`, `g† = cH₀/(2π)`, the redshift/GPS law, the ψ-sector perihelion/PPN, `M_eff → const` for a
point-like deficit, and hence the four ratios and the local no-anomaly statement. BOUNDARY: the masses,
radii and `H₀`; the RAR interpolating form; the galactic deficit parameters; `Ω_b/Ω_m` and the cluster
dark fraction. EMERGENT: the per-scale residuals. **No reclassification** — the D_040 registry is
untouched and the cluster result restates the project's own X063 finding.
