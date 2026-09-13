# Y_G_036 Result — Temporal Core Test Audit

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_036_Tests.cs`
**Status:** 6/6 PASSED
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_036"`
**Group total:** G_001–G_036 = **283/283 PASSED**

## Verdict

**BOUNDARY** — testable in principle with a pure observable, not yet isolated in practice. The missing ingredient is
**compactness precision, not signal**. Computed (G_027): the observable is pure, a discriminating regime exists, and
a pure route to that regime exists — while the precision currently supplied is not pure.

## The observable is pure, and the split is second order

| law | g₀₀ | redshift |
|---|---|---|
| **AT** | `−e^(−2x)` | `z = e^x − 1` |
| **GR** | `−(1 − 2x)` | `z = 1/√(1 − 2x) − 1` |

Series: AT `x + x²/2 + x³/6` against GR `x + 3x²/2 + 5x³/2`. They agree at **O(x)** and first differ at **O(x²)**:
`Δz = x² + (7/3)x³ + (13/3)x⁴`.

**The observables take no spatial argument** — the arity argument of G_035. Verified in the suite:
`ClockRateAT`, `ClockRateGR`, `RedshiftAT`, `RedshiftGR`, `Discriminator` all accept only `x`, and
`AllObservablesArePure()` is asserted.

### G_020's published figures, reproduced exactly

| quantity | this audit | G_020 |
|---|---|---|
| `Δz` at x = 0.247002 | **0.125628** | 0.1256 |
| `z_AT` at x = 0.187982 | **0.206812** | 0.206812 |
| `z_GR` at x = 0.187982 | **0.265888** | 0.265888 |

## The arena is neutron stars only

| σ_z | needs x ≥ |
|---|---|
| 1e−3 | **0.030494** |
| 0.01 | **0.089439** |
| 0.017766 (G_020's equal split) | **0.115094** |
| 0.025125 | **0.133350** |

| arena | x | Δz = x² | pure? |
|---|---|---|---|
| terrestrial tower (Pound–Rebka class) | 2.45e−15 | 6.00e−30 | yes |
| optical lattice clocks (1 cm) | 1.1e−18 | 1.21e−36 | yes |
| solar surface | 2.12e−6 | 4.49e−12 | yes |
| binary pulsar (Einstein delay) | 1.0e−6 | 1.00e−12 | yes |
| white dwarf surface | 1.0e−4 | 1.00e−8 | yes |
| **neutron star J0740+6620** | **0.247002** | **1.256e−1** | yes (compactness imported) |

## The weak-field split is unrepresentable, not merely unmeasurable

Each law passes through an intermediate ≈ 1 and carries ~1 ulp = **2.220446049250313e−16** of absolute error, while
the true split at x = 2.45e−15 is **6.0025e−30** — fourteen orders below it.

| quantity at x = 2.45e−15 | value |
|---|---|
| true split (series) | **+6.0025e−30** |
| direct subtraction `z_GR − z_AT` | **−7.5093e−18** (wrong magnitude *and* **wrong sign**) |

At `x = 1.1e−18` the GR redshift is **annihilated outright** — `1 − 2x` rounds to `1.0` and `z_GR` returns exactly
`0.0`. The suite asserts both facts plus `Math.Exp(1e−18) − 1 == 0.0` for the naive form, with
`AtNumerics.ExpM1` surviving at full precision.

> *Cross-runtime note:* `Math.Exp(x) − 1` returns exactly `0.0` in Python and `2.4425e−15` in .NET for the same
> input — a 100 % disagreement. `AtNumerics.ExpM1` is used so the audit does not depend on the underlying libm.

## The bottleneck is the compactness, not the redshift

| quantity | value |
|---|---|
| `σ_x/x` needed for 5σ | **3.661 %** (G_020) |
| `σ_x/x` achieved today | **13.73 %** (G_020) |
| improvement still required | **3.75×** |
| best significance today | **1.334σ** |
| the signal at J0740+6620 | **Δz = 0.125628 = 44.84 % of z_AT** |

**And for a neutron star the compactness is itself obtained from light bending** — pulse-profile modelling fits the
thermal emission from hot spots *including* bending. The observable is pure; the number required to read it is not.
The registry records exactly one candidate as impure outright and exactly one as pure-but-compactness-imported.

## A pure route exists and is ~3× short

`R_∞ = R/√(1 − 2x)` from a thermal flux plus a parallax distance is a **g₀₀ effect** (photon energy and arrival
rate, **not** bending), so `{z, R_∞}` solves `{M, R}` with no light bending at all. Distance and atmosphere
systematics keep `σ_x/x` above the ≈3.7 % needed. The suite verifies the relation inverts exactly:
`TrueRadiusFrom(ApparentRadius(r, x), z_GR(x)) == r`.

## The first experiment

**Pound & Rebka (1960)** — gravitational redshift of ⁵⁷Fe γ-rays over a **22.5 m** tower, `x = 2.45e−15`. **Pure
g₀₀**, no light bending anywhere in the measurement, and the first probe of the temporal core at all — but it
reaches only the **first-order** term, the one AT shares with GR (`x² = 6.0e−30`).

> **No experiment has yet probed the core's distinctive content.** The frontier is a compact-object `{z, R_∞}`
> measurement at `σ_x/x ≲ 3.7 %`.

## Classification and caveats

**No reclassification.** G_035's minimal time sector, G_020's redshift budget, G_019's signature, G_009's clock law
and G_004's calibration are unchanged inputs. D_040 untouched; no canonical claim, value or equation changes; no
new primitive. Deterministic: exact algebra on imported published values.

**Registry:** G_036 added to the live G_035 registry as `Spatial` → **BOUNDARY**, triaged `ScanDetectsIt: false`
because the suite's metric vocabulary lives entirely inside report string literals (0/0/0 after stripping — the
audit asks which quantities the *measurement* needs, not which metric symbols the code spells). Registry counts
become **25 / 9 / 3 of 37**; the boundary index is still **23**, and no prior classification changed.
