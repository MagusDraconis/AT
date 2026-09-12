# ResearchY-G_020 — Neutron-Star Redshift Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_020 (permanent)
**Title:** Neutron-Star Redshift Audit — can any currently measured neutron-star redshift exclude AT's exponential clock law?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_020.md`
**Depends on:** ResearchY-G_019 (the signature is `x²`; the live arena is neutron-star compactness), G_018 (the
provenance ledger), G_009 (the clock law), G_004 (AT ≡ GR at the Earth's surface); AT-QG QG197
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_020_Tests.cs` (7/7 PASSED, ~0.04 s)

## Purpose

G_019 established that the AT-vs-GR discriminator is `x²` and that only neutron stars are a live arena. G_020
puts the question in the strongest available form:

> **Can any currently measured neutron-star redshift exclude AT's exponential clock law?**
> Sources: NICER masses and radii, X-ray burst redshifts, published uncertainties.
> Compare AT (`g₀₀ = −e^(2x)`) with GR (`g₀₀ = −(1 + 2x)`). Compute the σ separation.
> Output: **EXCLUDED / ALLOWED / PREFERRED**.
> **Critical:** what precision on `z` and `M/R` is required for **5σ** discrimination?

**Answer.** **ALLOWED.** AT survives every current measurement — and the reason is *structural*: the binding
constraint is the **mass–radius precision, not the redshift**. With today's M/R, the maximum significance
achievable with a **perfect** redshift is **1.334σ**, and the `(dz/dx)σ_x` term exceeds `Δz/2` for *every* object,
so **no redshift precision whatsoever can reach 3σ, let alone 5σ**.

## 1. The framework

With `x = GM/(Rc²) > 0`:

```
z_AT = e^x − 1 ,     z_GR = (1 − 2x)^(−1/2) − 1 ,     Δz(x) = z_GR − z_AT > 0
S = Δz / √( σ_z² + (dz/dx)² σ_x² ) ,     dz/dx|_GR = (1 − 2x)^(−3/2)
```

**AT always predicts the smaller redshift**; at `x = 0` they coincide and `Δz` grows monotonically. The
midpoint `(z_AT + z_GR)/2` decides which law is *closer* to a given measurement.

| x | `z_AT` | `z_GR` | `Δz` | midpoint | `dz/dx` (GR) |
|---|---|---|---|---|---|
| 0.05 | 0.05127110 | 0.05409255 | 0.00282146 | 0.05268182 | 1.171214 |
| 0.10 | 0.10517092 | 0.11803399 | 0.01286307 | 0.11160245 | 1.397542 |
| 0.152011 | 0.16417320 | 0.19867750 | 0.03450430 | 0.18139337 | 1.722128 |
| 0.172317 | 0.18805439 | 0.23525906 | 0.04720467 | 0.21165672 | 1.884838 |
| 0.224246 | 0.25137882 | 0.34655498 | 0.09517616 | 0.29896690 | 2.441587 |
| 0.247002 | 0.28018167 | 0.40580945 | **0.12562778** | 0.34299556 | 2.778302 |

## 2. The decisive result — the error budget is binding, not the signal

**Maximum significance with a PERFECT redshift** (`σ_z = 0`), using the conservative (larger) branch of each
asymmetric NICER error:

| object | M ± δM | R ± δR | x | σ_x/x | `dz/dx` | `(dz/dx)σ_x` | **max S** |
|---|---|---|---|---|---|---|---|
| J0030+0451 (Riley 2019) | 1.34 ± 0.16 | 13.02 ± 1.24 km | 0.152011 | 21.46 % | 1.7221 | 0.056195 | **0.614** |
| J0030+0451 (Miller 2019) | 1.44 ± 0.15 | 13.02 ± 1.06 km | 0.163355 | 18.56 % | 1.8101 | 0.054873 | **0.752** |
| **J0740+6620 (Riley 2021)** | **2.072 ± 0.067** | **12.39 ± 1.30 km** | **0.247002** | **13.73 %** | **2.7783** | **0.094193** | **1.334** |
| J0740+6620 (Miller 2021) | 2.08 ± 0.07 | 13.70 ± 2.60 km | 0.224246 | 22.34 % | 2.4416 | 0.122334 | **0.778** |
| generic (G_019 object) | 1.4 ± 0.05 | 12.00 ± 1.00 km | 0.172317 | 11.90 % | 1.8848 | 0.038665 | **1.221** |

**No object reaches even 2σ with a perfect redshift.** The M/R term already exceeds `Δz/2`, `Δz/3` and `Δz/5`
for every object — so **the redshift is not the limiting factor**.

## 3. The published redshift range — ALLOWED

A representative burst/atmosphere redshift is `z = 0.30`; with a 20 % systematic, `σ_z = 0.06`:

| M | R | `z_AT` | `z_GR` | \|0.30 − z_AT\| | \|0.30 − z_GR\| |
|---|---|---|---|---|---|
| 1.4 | 10.0 km | 0.2297 | 0.3058 | 1.17σ | 0.10σ |
| 1.4 | 11.0 km | 0.2068 | 0.2659 | 1.55σ | 0.57σ |
| 1.4 | 12.0 km | 0.1881 | 0.2353 | 1.87σ | 1.08σ |
| 2.072 | 12.39 km | 0.2802 | 0.4058 | **0.33σ** | 1.76σ |

AT is inside **2.5σ everywhere** and inside **0.33σ** for the most compact object. With the pessimistic 50 %
systematic it is inside 1σ throughout. GR is also allowed, so at this precision the measurement does **not**
discriminate. **ALLOWED.**

## 4. Is AT *preferred*? — **NOT SUPPORTED**

For a Gaussian measurement, `ln LR(AT/GR) = [(z − z_GR)² − (z − z_AT)²]/(2σ_z²)`. At `σ_z = 0.05`:

| `z_obs` | R | x | `z_AT` | `z_GR` | \|dz\| AT | \|dz\| GR | LR(AT/GR) |
|---|---|---|---|---|---|---|---|
| 0.25 | 10.0 km | 0.2068 | 0.2297 | 0.3058 | 0.0203 | 0.0558 | **1.718** |
| 0.25 | 11.0 km | 0.1880 | 0.2068 | 0.2659 | 0.0432 | 0.0159 | 0.724 |
| 0.25 | 12.0 km | 0.1723 | 0.1881 | 0.2353 | 0.0619 | 0.0147 | 0.485 |
| 0.30 | 10.0 km | 0.2068 | 0.2297 | 0.3058 | 0.0703 | 0.0058 | 0.375 |
| 0.30 | 11.0 km | 0.1880 | 0.2068 | 0.2659 | 0.0932 | 0.0341 | 0.222 |
| 0.30 | 12.0 km | 0.1723 | 0.1881 | 0.2353 | 0.1119 | 0.0647 | 0.189 |
| 0.35 | 10.0 km | 0.2068 | 0.2297 | 0.3058 | 0.1203 | 0.0442 | 0.082 |
| 0.35 | 11.0 km | 0.1880 | 0.2068 | 0.2659 | 0.1432 | 0.0841 | **0.068** |
| 0.35 | 12.0 km | 0.1723 | 0.1881 | 0.2353 | 0.1619 | 0.1147 | 0.073 |

Against **J0740+6620 (Riley)** with `z = 0.30 ± 0.06`, `LR = 4.483` — nominally in AT's favour. **But the ratio
flips sign across the grid** (1.718 down to 0.068), and **only 1 of 9 grid points favours AT**. Because
`z_AT < z_GR` always and the observed redshifts sit mostly **above** `z_AT` for `R ≥ 11 km`, the data lean
**toward GR** over most of the grid. The apparent preference is an artefact of borrowing one object's redshift
for another object's `x` — **the redshift measurements are not made on the NICER objects at all**.

## 5. The critical question — 5σ

Joint requirement: `(Δz/5)² = σ_z² + (dz/dx)²σ_x²`.

| object | perfect `z`: `σ_x/x ≤` | perfect `M/R`: `σ_z ≤` | equal split: `σ_z ≤` | equal split: `σ_x/x ≤` |
|---|---|---|---|---|
| J0030+0451 (Riley 2019) | 2.636 % | 0.006901 (4.20 % of `z_AT`) | 0.004880 (2.97 %) | 1.864 % |
| J0030+0451 (Miller 2019) | 2.790 % | 0.008250 (4.65 %) | 0.005834 (3.29 %) | 1.973 % |
| **J0740+6620 (Riley 2021)** | **3.661 %** | **0.025125 (8.97 % of `z_AT`, 6.19 % of `z_GR`)** | **0.017766 (6.34 %)** | **2.589 %** |
| J0740+6620 (Miller 2021) | 3.477 % | 0.019035 (7.57 %) | 0.013460 (5.35 %) | 2.458 % |
| generic (G_019 object) | 2.907 % | 0.009441 (5.02 %) | 0.006676 (3.55 %) | 2.055 % |

**The best current target (J0740+6620, Riley 2021):**

| route | requirement | today | improvement needed |
|---|---|---|---|
| perfect `z` | `σ_x/x ≤ 3.661 %` | 13.73 % | **3.75×** |
| perfect `M/R` | `σ_z ≤ 0.025125` = 8.97 % of `z_AT` | 20–50 % | 2.2–5.6× |
| **equal split** | **`σ_x/x ≤ 2.589 %` AND `σ_z ≤ 0.017766` = 6.34 % of `z_AT`** | 13.73 %; 20–50 % | **5.30× and 3.2–7.9×** |

For the record, 3σ on the same object: `σ_z ≤ 0.041876` (14.95 % of `z_AT`) or `σ_x/x ≤ 6.102 %`.

**The requirements are JOINT** — the two must be won together, because the error budget is joint.

## 6. The dangerous case and the radius inversion

For a measured `z`, **AT demands a smaller radius than GR** (M = 1.4 M☉):

| z | `R_AT` | `R_GR` | `ΔR/R` |
|---|---|---|---|
| 0.20 | 11.342 km | 13.535 km | −16.20 % |
| 0.25 | 9.267 km | 11.488 km | −19.33 % |
| 0.30 | 7.881 km | 10.129 km | −22.19 % |
| **0.35** | **6.890 km** | **9.164 km** | **−24.81 %** |

**The single dangerous case** is the often-quoted `z = 0.35` of **EXO 0748−676** (Cottam et al. 2002), which was
**not confirmed** in later observations. Taken at face value at M = 1.4, R = 11 km (`x = 0.187982`), the
predictions are `z_AT = 0.206812` and `z_GR = 0.265888` — so `z = 0.35` lies **0.143188 above AT and 0.084112
above GR**: it excludes **BOTH** laws and merely demands a smaller radius. AT-consistency would require
`R = 6.890 km` (**4.110 km below 11 km**); GR requires `R = 9.164 km`.

## 7. Verdicts

| label | content |
|-------|---------|
| **ALLOWED** | **No current neutron-star redshift excludes AT.** AT is inside 2.5σ of a representative `z = 0.30 ± 0.06` for every plausible (M, R) and inside **0.33σ** for the most compact object; inside 1σ throughout with a 50 % systematic. |
| **EXCLUDED** | **Not achieved — and not achievable with current data.** With today's M/R the maximum significance with a *perfect* redshift is **1.334σ**, and `(dz/dx)σ_x` exceeds `Δz/2` for every object: **no `z` precision whatsoever can reach 3σ, let alone 5σ**. |
| **PREFERRED** | **NOT supported.** The nominal AT-favoured ratio (4.483) **flips sign** across the plausible grid (1.718 down to 0.068); only **1 of 9** grid points favours AT; because `z_AT < z_GR` and the observed redshifts sit mostly above `z_AT` for `R ≥ 11 km`, the data lean **toward GR** over most of the grid. |

## 8. Critical answer

> **5σ requires BOTH a better `z` and a better M/R.** For the best current object (J0740+6620, Riley 2021):
> `σ_x/x ≤ 3.661 %` with a perfect redshift, **or** `σ_z ≤ 0.025125 = 8.97 % of z_AT` with a perfect M/R, **or**
> the equal split `σ_x/x ≤ 2.589 %` **and** `σ_z ≤ 0.017766 = 6.34 % of z_AT`. Against today's 13.73 % M/R that is
> **3.75× to 5.30× better in `σ_x/x`, plus 3.2× to 7.9× better in `σ_z`, won jointly.**

**The structural lesson: the deficit is in the error budget, not in the signal.** The signal is `Δz = 0.1256`
for the most compact measured object — large in absolute terms. It is `σ_x` that keeps the test out of reach.

## 9. Classification and caveats

**No reclassification.** G_019's signature, G_018's ledger, G_009's clock law and G_004's calibration are
unchanged inputs. D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic: exact algebra on imported published values.

* Imported values: the NICER M–R posteriors for PSR J0030+0451 (Riley 2019; Miller 2019) and PSR J0740+6620
  (Riley 2021; Miller 2021), used as **central values with the conservative (larger) branch of each asymmetric
  error**; and the literature range `z ≈ 0.2–0.35` for burst/atmosphere fits with **20–50 % systematics**.
* The `z` values are **not measured on the NICER objects** — that is precisely why the PREFERRED verdict fails,
  and it is stated as such rather than hidden.
* The EXO 0748−676 `z = 0.35` is flagged as **unconfirmed**, and the audit shows that even if taken at face
  value it excludes both laws rather than AT alone.
* The `σ_z` values used for the "today" column (20–50 % of `z_AT`) are the literature range for the *method*,
  not a single measurement's error bar; they are used as a range for that reason.

## 10. Open problems (OP1–OP5)

1. Is there a **single** neutron star whose M–R posterior reaches `σ_x/x ≤ 3.7 %`? That alone (with a perfect
   `z`) would give 5σ on J0740+6620's compactness.
2. Can the **radius inversion** (−24.81 % at `z = 0.35`) be pre-registered as a kill criterion for a *named*
   object with a NICER-quality radius?
3. What M–R precision will NICER's extended mission and **STROBE-X / eXTP / Athena** deliver, and does it close
   the 3.75×–5.30× gap?
4. Is there a strong-field observable that constrains `x` **without** a mass measurement (e.g. burst
   oscillations, kilohertz QPOs), avoiding the `σ_M` term?
5. Does the PREFERRED verdict change if the redshift and the M–R are ever measured on the **same** object — and
   in which direction?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_020_Tests.cs` — **7/7 PASSED** (~0.04 s)
**Group total:** G_001–G_020 = **175/175 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_020"`

| label | content |
|-------|---------|
| **ALLOWED** | AT survives every current neutron-star redshift measurement (inside 2.5σ everywhere, 0.33σ for the most compact object) |
| **EXCLUDED** | not achieved, and unreachable at **any** `z` precision on current M/R (max 1.334σ) |
| **PREFERRED** | not supported — LR(AT/GR) flips sign (1.718 → 0.068) and only 1 of 9 grid points favours AT |
| **5σ** | `σ_x/x ≤ 3.661 %` (perfect `z`) **or** `σ_z ≤ 0.025125` (8.97 % of `z_AT`) **or** the equal split `σ_x/x ≤ 2.589 %` **and** `σ_z ≤ 0.017766` (6.34 %) — 3.75–5.30× and 3.2–7.9× better **jointly** |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_019.md`, `ResearchY-G_018.md`, `ResearchY-G_009.md`,
  `ResearchY-G_004.md`
* `Docs/ResearchY/Tests/Results/Y_G_020_Result.md`
* Imported published values: NICER M–R posteriors for PSR J0030+0451 (Riley et al. 2019; Miller et al. 2019) and
  PSR J0740+6620 (Riley et al. 2021; Miller et al. 2021); the burst/atmosphere neutron-star redshift range
  (`z ≈ 0.2–0.35`, 20–50 % systematics); the EXO 0748−676 `z = 0.35` claim (Cottam et al. 2002, unconfirmed).
