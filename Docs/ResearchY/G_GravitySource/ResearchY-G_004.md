# ResearchY-G_004 — Gravity Calibration Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_004 (permanent)
**Title:** Gravity Calibration Audit — can a_AT be calibrated to measured gravity?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_004.md`
**Depends on:** ResearchY-G_001 (ρ sources gravity), G_002 (ρ controllable at fixed energy), G_003 (the
SI conversion chain, the ambient calibration Δln ρ = 1.6102e-6 and the suppression requirement 3.746e5);
AT-QG QG21 (redshift), QG080 (`g† = cH₀/2π`), QG103 (Mercury perihelion), QG181/QG182 (G, M_Pl),
QG187 (GPS), QG212 (ψ restores PPN γ = +1), QG194 (Σm = 0); G4-ME21 (scale-free extended deficit),
G4-ME22 (point-like deficit ⇒ M_eff → const), G4-ME3/G4-ME4 (the α = 0 flat curve, SEMI-NATURAL);
`AT_ClusterMassAudit` (Coma: dynamical/baryon 4–10×, f_gas ~ 0.157), X063 (AT modified gravity
insufficient at clusters), X065 (Ω_DM not derived), `Data/derived/A0OverCH_Distribution.csv`
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_004_Tests.cs` (7/7 PASSED, 33 ms)
**Shared machinery:** `AT.Tests/Shared/PhysicalUnits.cs`, `AT.Tests/Shared/DensityField.cs`

---

## Purpose

G_001–G_003 established the source (the counting measure ρ), its controllability at fixed energy, and
the SI conversion of a Δρ. This audit asks whether the resulting `a_AT` can be **calibrated to measured
gravity** at four scales, with the ratio `a_pred/a_obs` computed **without free parameters** — no
quantity fitted to the data it is compared with; every external number is a measured anchor (a mass, a
radius, `H₀`).

---

## 0. What enters (the input inventory)

| Input | Value | Status |
|---|---|---|
| `G_AT` | 6.6476e-11 m³ kg⁻¹ s⁻² | **DERIVED** (QG181/QG182: `1/M_Pl²`, `M_Pl = v(Σm·#g·occ₂)³`) — not fitted |
| `g†` | `c·H₀/(2π)` = 1.04220e-10 m/s² | **DERIVED scale**, zero fitted parameters (QG080) |
| `M_⊕`, `R_⊕`, `GM_☉`, `AU`, `H₀`, `M_500`, `R_500`, `Ω_b/Ω_m` | measured | **anchors** (no fit) |

---

## 1. The four scales

| # | scale | a_obs [m/s²] | a_pred [m/s²] | **a_pred/a_obs** | fitted params | verdict |
|---|---|---|---|---|---|---|
| 1 | Earth surface (`GM_⊕/R_⊕²`) | 9.820250 | 9.780965 | **0.99600** | `M_⊕` only | **CALIBRATED** |
| 2 | Sun–Earth (1 AU) | 5.930084e-3 | 5.906361e-3 | **0.99600** | `GM_☉` only | **CALIBRATED** |
| 3 | Galaxy RAR (`a₀`) | 1.200e-10 | 1.04220e-10 | **0.86850** | **NONE** | **CALIBRATED** |
| 4 | Cluster (Coma) | 6.3650e-11 | 3.2944e-11 | **0.51759** | `f_b = 0.15` | **REFUTED** (modified gravity) |

**Scale 1 and 2.** The residual is *entirely* the derived-`G` offset: `G_AT/G_CODATA = 0.9959996`
(0.40%), the same number QG181 quotes. The prediction is `GM/r²` with the same measured mass, so the
ratio is exactly the coupling ratio — a like-for-like comparison. (The geophysical *effective*
`g = 9.80665 m/s²` is **0.13% below** `GM_⊕/R_⊕²` because of centrifugal and oblateness terms, which is
why the gravitational value is the correct comparator.) Independent checks at the same scales:
GPS time dilation **+38.5 vs +38.6 μs/day** (0.26%, QG187) and the orbital potential term **45.74 vs
45.7 μs/day**; solar-system PPN **γ = β = +1** with ψ (QG212) and the Mercury perihelion
**+42.98 ″/century** (QG103).

**Scale 3 (the parameter-free one).** `g† = cH₀/(2π)` uses one *measured cosmological* anchor (`H₀`) and
**zero fitted parameters** — the RAR's own `a₀` is not an input. Two comparisons:

| comparison | ratio |
|---|---|
| vs the unweighted literature mean `a₀ = 1.1683e-10` (6 determinations) | **0.86850** (13.2% low) |
| vs the project's own combined `a₀/cH₀ = 0.1725` (with AT `1/(2π) = 0.159155`) | **0.9226** (7.7% low) |

The six determinations span 1.00–1.21e-10 (scatter 6.5%, full spread 17%), so the parameter-free value
is ~2× the *scatter* below the unweighted mean but only 7.7% below the project's combined value, and
well inside the 17% spread. That is a genuine calibration, quoted with its two-sided honesty.

**Scale 4 (the failure).** Coma at `R_500 = 1.48 Mpc` with `M_500 = 1e15 M_☉` gives
`a_obs = 6.3650e-11 m/s² = 6.49e-12 g = 0.61 g†` — deep in the AT/RAR regime. Building the same law
from the **baryons only** (`a_bar = 0.15 a_obs = 9.5475e-12 m/s²`):

```
AT/RAR form   a_pred = a_bar·sqrt(1 + g†/a_bar)       = 3.2944e-11  ->  1.93x SHORT
MOND form     a_pred = a_bar/(1 - exp(-sqrt(a_bar/a₀))) = 3.8857e-11 ->  1.64x SHORT
```

This is the classical cluster shortfall, and it is the project's own finding (X063 / `AT_ClusterMassAudit`):
**"AT modified gravity (g†/RAR) is INSUFFICIENT at cluster scale."** AT matches clusters only through the
**deficit-as-mass** channel, which is a mass inventory (== ΛCDM, ~85% dark, matching the audited Coma
dynamical/baryon ratio 4–10×) and whose dark fraction is **not derived** (X065; `Ω_DM` BOUNDARY/OPEN).

---

## 2. Verdicts

| verdict | where |
|---|---|
| **CALIBRATED** | Earth surface (0.99600; 0.40%, the derived `G`) · Sun–Earth (0.99600, plus the ψ-sector perihelion and PPN) · Galaxy RAR (0.86850 with **zero** fitted parameters; 0.9226 against the project's combined determination) |
| **CORRELATED** | the RAR **interpolating function** (`g_obs = g_bar√(1 + g†/g_bar)`) is a research model; the AT-native statement is the **α = 0 log deficit** flat curve (G4-ME3/G4-ME4, SEMI-NATURAL — unique scale-invariant deficit, but the marginal α = 0 member) |
| **REFUTED** | the **cluster scale for the modified-gravity channel** (1.93× short with the AT law, 1.64× with MOND) — AT needs the deficit-as-mass channel there, which is not a gravity calibration. Also **REFUTED**: a uniform cosmic AT gradient (see §3d) |

---

## 3. The critical question — why is a 10⁻⁶ g effect not already observed locally?

**Answer: because it is a counterfactual, not a prediction.** Five quantitative steps:

**(a) 10⁻⁶ g is a unit-amplitude counterfactual.** G_003's law `Δa = (c²/d)·Δln ρ/L` requires
`Δln ρ = 0.15151` over a 15 kpc region to produce 10⁻⁶ g. The G_002 witnesses have Δln ρ ≈ 0.15–1.8 — a
whole-lattice reorganisation, not a local one.

**(b) The realised field is fixed by the observed RAR.** `g† = 1.04220e-10 m/s² = 1.0627e-11 g`, which is
**9.41e4 × below 10⁻⁶ g** — exactly G_003's suppression requirement (measured against the G_002
witnesses: ≥ 3.746e5; against this particular target: 9.41e4). Nothing realised is anywhere near 10⁻⁶ g.

**(c) The ambient AT density gradient** implied by the observed field is
`d ln ρ/dr = d·g†/c² = 3.4788e-27 m⁻¹` — a number, not a mechanism.

**(d) A *uniform* cosmic gradient is REFUTED.** If that gradient were cosmic-uniform, AT would predict a
constant `1.0627e-11 g` everywhere, including the solar system, where ephemeris/LLR bounds on anomalous
accelerations are ~10⁻¹² m/s². The ambient value is **~104× above** that bound, so the simplest
realisation is excluded: **the AT gradient must be dominated by local structure, not by a cosmic
background.**

**(e) And it is — the point-mass reduction.** A point-like deficit gives `M_eff → const` and exactly
`a = −G_AT·M/r²` (G4-ME22): Newton with the derived `G` and **no anomalous term**. The flat-curve `1/r`
regime needs a scale-free **extended** deficit (one void per octave, G4-ME21) which neither the Earth
nor the Sun has. So the local AT signatures are exactly three, none at 10⁻⁶ g:

| local signature | value | precision reached |
|---|---|---|
| the value of `G` | 0.99600 × Newton | 0.40% |
| the GPS potential/time dilation | +38.5 vs +38.6 μs/day | 0.26% |
| the Mercury perihelion | +42.98 ″/century | < 0.1% |

**Conclusion.** A 10⁻⁶ g effect would require a galactic-scale unit-amplitude reconfiguration of the
counting measure, which (i) no known process realises (G_002 OP2) and (ii) must be suppressed by
≥ 3.7e5 if it were (G_003). The observable AT field is the *suppressed* one: ≤ 1.06e-11 g galaxy-wide and
exactly Newtonian locally — consistent with every existing gravity test.

---

## 4. Classification and caveats

| Component | Status |
|---|---|
| `G` (QG181/182), `g† = cH₀/(2π)` (QG080), the redshift/GPS law (QG21/QG187), the ψ-sector perihelion and PPN (QG103/QG212), `M_eff → const` for a point-like deficit (G4-ME22), the four ratios and the local no-anomaly statement | **DERIVED** |
| the masses and radii (`M_⊕`, `GM_☉`, `M_500`, `R_500`) and `H₀`; the RAR interpolating form; the galactic deficit-profile parameters; `Ω_b/Ω_m = 0.15` and the cluster dark fraction (X065) | **BOUNDARY** |
| the specific per-scale residuals | **EMERGENT** |

**No reclassification.** The D_040 `ClassificationRegistry` is untouched; G_001–G_003's verdicts are
used, not modified. The cluster result is **not new** — it restates the project's own X063 finding in
calibration-ratio form.

---

## 5. Falsification paths

| Claim | Falsification |
|---|---|
| `G_AT` is the measured coupling | a gravity measurement disagreeing with `G = 6.6476e-11` beyond 0.4% |
| `g† = cH₀/(2π)` is the RAR scale | an `a₀` determination inconsistent with 1.042e-10 at the combined precision |
| the local prediction is Newtonian | a detected anomalous solar-system acceleration above ~10⁻¹² m/s² |
| the cluster channel is refuted for modified gravity | a baryonic-only AT/RAR prediction matching cluster masses |
| no 10⁻⁶ g local effect | a local anomalous acceleration at the 10⁻⁶ g level |

---

## 6. Open problems

1. **G-004 OP1 — close the 0.40% `G` gap.** `G_AT` is 0.40% below CODATA; whether that is a real
   (derivable) correction or numerology is undecided (QG183 declares the exponent cubic robust, not exact).
2. **G-004 OP2 — the 7.7–13% RAR offset.** Is `g† = cH₀/(2π)` exactly right, or is the true AT scale
   (e.g. with a different ΩΛ-weighted factor) slightly higher? The combined determination prefers 1.13e-10.
3. **G-004 OP3 — clusters.** The modified-gravity channel fails by ~2×; the deficit-as-mass channel
   works but needs the non-derived dark fraction. Whether AT can derive the required cluster dark mass
   from the deficit picture without importing ΛCDM's Ω_DM is open (X065).
4. **G-004 OP4 — the local/global boundary.** What exactly suppresses the cosmic AT gradient inside a
   gravitationally bound system (the point-mass reduction is stated, not derived as a boundary condition)?

---

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_004_Tests.cs` — **7/7 PASSED** (33 ms)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_004"`

| Test | Verifies | Result |
|---|---|---|
| `Y_G_004_Anchors` | what enters; nothing fitted | PASS |
| `Y_G_004_EarthScale` | 0.99600; GPS 0.26%; effective-vs-gravitational `g` | PASS |
| `Y_G_004_SunEarthScale` | 0.99600; perihelion and PPN via ψ | PASS |
| `Y_G_004_GalaxyRar` | 0.86850 (literature) and 0.9226 (combined), zero fitted parameters | PASS |
| `Y_G_004_ClusterScale` | 1.93× / 1.64× shortfall; deficit-as-mass channel | PASS |
| `Y_G_004_LocalBoundCritical` | the five-step answer to the critical question | PASS |
| `Y_G_004_Run` | research report | PASS |

---

## References

- ResearchY-G_001, G_002, G_003; D_047, D_048.
- AT-QG: QG21, QG080, QG103, QG181, QG182, QG187, QG194, QG212; G4-ME3, G4-ME4, G4-ME21, G4-ME22.
- `AT_ClusterMassAudit` (Coma), X063 (correlation dark matter audit), X065, `Data/derived/A0OverCH_Distribution.csv`,
  `Docs/RAR_TimeInterpretation.md`.
