# Y_G_020_Result.md — ResearchY-G_020 Neutron-Star Redshift Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_020_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.04 s) — group G total 175/175 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_020"`

## Summary

**Question:** can any **currently measured** neutron-star redshift exclude AT's exponential clock law?
(NICER masses and radii, X-ray burst redshifts, published uncertainties; compare AT `g₀₀ = −e^(2x)` with GR
`g₀₀ = −(1+2x)`; compute the σ separation)

**Answer: ALLOWED** — and the reason is **structural**: the binding constraint is the **mass–radius precision,
not the redshift**. With today's M/R the maximum significance achievable with a **perfect** redshift is
**1.334σ**, and `(dz/dx)σ_x` exceeds `Δz/2` for every object, so **no redshift precision whatsoever can reach 3σ,
let alone 5σ**.

## The framework

```
z_AT = e^x − 1 ,   z_GR = (1 − 2x)^(−1/2) − 1 ,   Δz = z_GR − z_AT > 0 ,   x = GM/(Rc²)
S = Δz / √(σ_z² + (dz/dx)²σ_x²) ,   dz/dx|_GR = (1 − 2x)^(−3/2)
```

AT always predicts the **smaller** redshift; the midpoint decides which law is closer.

## Max significance with a PERFECT redshift

| object | M ± δM | R ± δR | x | σ_x/x | `(dz/dx)σ_x` | **max S** |
|---|---|---|---|---|---|---|
| J0030+0451 (Riley 2019) | 1.34 ± 0.16 | 13.02 ± 1.24 km | 0.152011 | 21.46 % | 0.056195 | **0.614** |
| J0030+0451 (Miller 2019) | 1.44 ± 0.15 | 13.02 ± 1.06 km | 0.163355 | 18.56 % | 0.054873 | **0.752** |
| **J0740+6620 (Riley 2021)** | **2.072 ± 0.067** | **12.39 ± 1.30 km** | **0.247002** | **13.73 %** | **0.094193** | **1.334** |
| J0740+6620 (Miller 2021) | 2.08 ± 0.07 | 13.70 ± 2.60 km | 0.224246 | 22.34 % | 0.122334 | **0.778** |
| generic (G_019 object) | 1.4 ± 0.05 | 12.00 ± 1.00 km | 0.172317 | 11.90 % | 0.038665 | **1.221** |

**None reaches even 2σ with a perfect z.** The M/R term exceeds `Δz/2`, `Δz/3` and `Δz/5` for every object.

## The published redshift range — ALLOWED

Representative `z = 0.30` with a 20 % systematic (`σ_z = 0.06`):

| M | R | `z_AT` | `z_GR` | \|0.30 − z_AT\| | \|0.30 − z_GR\| |
|---|---|---|---|---|---|
| 1.4 | 10.0 km | 0.2297 | 0.3058 | 1.17σ | 0.10σ |
| 1.4 | 11.0 km | 0.2068 | 0.2659 | 1.55σ | 0.57σ |
| 1.4 | 12.0 km | 0.1881 | 0.2353 | 1.87σ | 1.08σ |
| 2.072 | 12.39 km | 0.2802 | 0.4058 | **0.33σ** | 1.76σ |

AT is inside 2.5σ everywhere, **0.33σ** for the most compact object. GR is also allowed, so the measurement does
not discriminate at this precision.

## Is AT PREFERRED? — NOT SUPPORTED

`ln LR(AT/GR) = [(z − z_GR)² − (z − z_AT)²]/(2σ_z²)`, `σ_z = 0.05`:

| `z_obs` | R | LR(AT/GR) |
|---|---|---|
| 0.25 | 10.0 km | **1.718** |
| 0.25 | 11.0 km | 0.724 |
| 0.25 | 12.0 km | 0.485 |
| 0.30 | 10.0 / 11.0 / 12.0 km | 0.375 / 0.222 / 0.189 |
| 0.35 | 10.0 / 11.0 / 12.0 km | 0.082 / **0.068** / 0.073 |

Against J0740+6620 (Riley) with `z = 0.30 ± 0.06`: `LR = 4.483` — nominally AT-favoured. But the ratio **flips
sign** across the grid and **only 1 of 9** grid points favours AT. Because `z_AT < z_GR` and observed redshifts
sit mostly **above** `z_AT` for `R ≥ 11 km`, the data lean **toward GR**. The apparent preference is an artefact
of borrowing one object's redshift for another object's `x`.

## The critical question — 5σ

| object | perfect `z`: `σ_x/x ≤` | perfect `M/R`: `σ_z ≤` | equal split |
|---|---|---|---|
| J0030+0451 (Riley 2019) | 2.636 % | 0.006901 (4.20 % of `z_AT`) | 0.004880 (2.97 %) + 1.864 % |
| J0030+0451 (Miller 2019) | 2.790 % | 0.008250 (4.65 %) | 0.005834 (3.29 %) + 1.973 % |
| **J0740+6620 (Riley 2021)** | **3.661 %** | **0.025125 (8.97 % of `z_AT`, 6.19 % of `z_GR`)** | **0.017766 (6.34 %) + 2.589 %** |
| J0740+6620 (Miller 2021) | 3.477 % | 0.019035 (7.57 %) | 0.013460 (5.35 %) + 2.458 % |
| generic (G_019 object) | 2.907 % | 0.009441 (5.02 %) | 0.006676 (3.55 %) + 2.055 % |

**Best current target: 3.75× better `σ_x/x` (perfect z) or 5.30× better (equal split), plus 3.2–7.9× better
`σ_z` — won jointly.** For the record, 3σ: `σ_z ≤ 0.041876` (14.95 % of `z_AT`) or `σ_x/x ≤ 6.102 %`.

## The dangerous case and the radius inversion

Measured `z` demands a smaller radius under AT (M = 1.4 M☉): z = 0.20 → 11.342 vs 13.535 km (−16.20 %);
0.25 → 9.267 vs 11.488 (−19.33 %); 0.30 → 7.881 vs 10.129 (−22.19 %); **0.35 → 6.890 vs 9.164 km (−24.81 %)**.

**The single dangerous case** is the unconfirmed `z = 0.35` of EXO 0748−676 (Cottam et al. 2002). At M = 1.4,
R = 11 km (`x = 0.187982`): `z_AT = 0.206812`, `z_GR = 0.265888` — so 0.35 lies 0.143188 above AT and 0.084112
above GR: it excludes **BOTH** laws. AT-consistency needs `R = 6.890 km` (4.110 km below 11 km); GR needs
9.164 km.

## Verdicts

| label | content |
|-------|---------|
| **ALLOWED** | no current neutron-star redshift excludes AT (inside 2.5σ everywhere, 0.33σ for the most compact) |
| **EXCLUDED** | not achieved, and **unreachable at any `z` precision** on current M/R (max 1.334σ) |
| **PREFERRED** | **not supported** — LR flips sign (1.718 → 0.068), only 1 of 9 grid points favours AT |
| **5σ** | `σ_x/x ≤ 3.661 %` OR `σ_z ≤ 0.025125` (8.97 % of `z_AT`) OR joint `σ_x/x ≤ 2.589 %` + `σ_z ≤ 0.017766` (6.34 %) |

**Structural lesson: the deficit is in the error budget, not in the signal** (`Δz = 0.1256` for the most compact
object is large in absolute terms; it is `σ_x` that keeps the test out of reach).

## Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic: exact algebra on imported published values.

* NICER M–R posteriors used as central values with the **conservative (larger) branch** of each asymmetric error.
* The `z` values are **not** measured on the NICER objects — stated openly, and it is why PREFERRED fails.
* EXO 0748−676 `z = 0.35` flagged **unconfirmed**; even taken at face value it excludes *both* laws.
* The 20–50 % `σ_z` figures are the literature **method** range, not one measurement's error bar.

## Open problems (OP1–OP5)

1. Any single neutron star with `σ_x/x ≤ 3.7 %`? That alone (perfect z) would give 5σ.
2. Pre-register the radius inversion (−24.81 % at z = 0.35) as a kill criterion for a named object?
3. What will NICER-extended, STROBE-X, eXTP, Athena deliver — does it close the 3.75–5.30× gap?
4. A strong-field constaint on `x` **without** a mass measurement (burst oscillations, kHz QPOs)?
5. Does PREFERRED change if `z` and M–R are ever measured on the **same** object, and in which direction?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_020.md`
* Imported: NICER M–R posteriors (PSR J0030+0451: Riley 2019, Miller 2019; PSR J0740+6620: Riley 2021,
  Miller 2021); the burst/atmosphere NS redshift range (z ≈ 0.2–0.35, 20–50 % systematics); the EXO 0748−676
  z = 0.35 claim (Cottam et al. 2002, unconfirmed).
