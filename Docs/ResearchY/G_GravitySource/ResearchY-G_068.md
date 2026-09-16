# ResearchY-G_068 - Temporal Prediction Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_068 (permanent)
**Title:** What unique measurable prediction does the surviving AT time sector make that differs from GR?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `G_GravitySource/ResearchY-G_068.md`
**Depends on:** G_019 (the second-order signature), G_020 (the neutron-star redshift error budget), G_035 (the surviving minimal time sector: 25 of 36 components, g₀₀ only), G_049 (the clock pattern is lossless)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_068_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/TemporalPredictionAudit.cs`

## The question

What **unique measurable prediction** does the surviving AT time sector make that **differs from GR**? Output the
**observable**, its **magnitude** and the **current measurement status**. Goal: **return from ρ-structure to experimental
time physics**.

## The answer: **DERIVED - the prediction is exact, unique and undecided**

## The observable

| theory | metric | redshift |
|---|---|---|
| **AT** (the surviving time sector) | `g₀₀ = −e^(2x)` | **`1 + z = e^(−x)`** |
| GR | `g₀₀ = −(1 + 2x)` | **`1 + z = (1 + 2x)^(−1/2)`** |

with `x = −GM/(Rc²)` the **surface potential**. **The observable is the surface redshift of a compact object** - or
equivalently the rate of a clock at its surface against a distant one. Nothing is fitted: both are exact functions of one
variable.

**The constants are recomputed rather than imported**, and the recomputation is cross-checked: the Sun's surface
potential comes out **−2.123047E-6** against G_019's recorded **2.122503E-6**, an agreement of **2.6E-4** relative whose
residual is the **choice of solar mass** - reported rather than rounded away.

## The magnitude: the split is SECOND ORDER

Expanding both exact forms, `z_AT = −x + x²/2 − x³/6` and `z_GR = −x + 3x²/2 − 5x³/2`, so:

**Δz = −x² + (7/3)x³ + …** - leading term **−x²**, next term **(7/3)x³** (which sets the measured relative deviation
**2.333E-6** at `x = −1E-6`).

**So no weak-field measurement can ever see the prediction:**

| case | x | split | precision available | short by |
|---|---|---|---|---|
| **Earth's surface** | −6.961E-10 | **−4.846E-19** | 1E-18 (clock floor) | **2.1×** |
| **the Sun's surface** | −2.123E-06 | **−4.507E-12** | 1E-5 | **2.2E+6×** |
| **Sirius B** (white dwarf) | −2.573E-04 | −6.620E-08 | 0.02 (2 %) | **3.0E+5×** |

And at `x = −1E-9` the split (**−1.000E-18**) is **below one ulp of unity** (2.220E-16): not merely small but
**unrepresentable**. The audit therefore takes the split **from the series** and the redshift through
`AtNumerics.ExpM1`, and **measures the naive route's failure instead of cautioning about it**: the subtraction of two
redshifts returns **−8.224E-17** where the truth is **−1.000E-18** - **inflated by a factor of 82.2** - while the series
route holds to **6E-8 relative** at the same depth.

## The live arena: the surface of a compact object

| object | x | z_AT | z_GR | relative split |
|---|---|---|---|---|
| J0030+0451 (NICER) | −0.152011 | 0.1641730 | 0.1986772 | **−17.367 %** |
| **J0740+6620 (Riley 2021)** | **−0.247002** | **0.2801817** | **0.4058094** | **−30.957 %** |
| J0740+6620 (Miller 2021) | −0.224246 | 0.2513788 | 0.3465550 | −27.464 % |
| generic NICER quality (1.4 M☉, 12 km) | −0.172317 | 0.1880545 | 0.2352593 | −20.065 % |

**The sign is a prediction too, and it is uniform: AT's redshift is always SMALLER.** At the most compact published
object the two theories differ by **nearly a third** - the prediction is falsifiable rather than merely small.

## The measurement status: ALLOWED but UNDECIDED

| quantity | value |
|---|---|
| deciding object | M = 1.4 ± 0.05 M☉, R = 12 ± 1 km |
| x | −0.172317 ± 0.020657 |
| z_AT / z_GR | 0.188055 ± 0.024586 / 0.235259 ± 0.039344 |
| **separation** | **0.047205** |
| combined uncertainty | 0.046394 |
| **significance now** | **1.017 σ** |
| for **3σ** | σ_z ≤ **0.015735** = **8.37 %** of z_AT |
| for **5σ** | σ_z ≤ **0.009441** = **5.02 %** of z_AT |
| current determinations | 20–50 % relative → short by **2.4× to 6.0×** |

**AT is allowed by every published measurement** (G_020: inside 2.5σ everywhere, 0.33σ for the most compact object) **and
preferred by none.**

## And a qualitative difference that needs no precision

**AT's `g₀₀ = −e^(2x)` never vanishes**, so AT has **no clock-stopping surface**, while GR's `1 + 2x` vanishes at
`y = 0.5` and its redshift diverges there. A compact object deep enough to sit near that point **separates the two
theories by inspection rather than by timing**.

## Verdict

**DERIVED.** The prediction is **exact, unique and stated with its magnitude and its deciding experiment**: **the
observable is a compact object's surface redshift**; **the difference is second order in the potential** (`−x²`), which is
why terrestrial, solar and white-dwarf measurements are **2.1× to 2.2E+6× short**; and **the status is
allowed-but-undecided**, with **1.017σ now** and **8.37 % of z_AT needed for 3σ** against current determinations of
20–50 %.
