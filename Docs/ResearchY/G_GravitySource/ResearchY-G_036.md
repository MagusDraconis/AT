# ResearchY-G_036 — Temporal Core Test Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_036 (permanent)
**Title:** Temporal Core Test Audit — can the surviving temporal core be tested without any spatial metric?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_036.md`
**Depends on:** G_035 (the temporal core is independent), G_020 (the neutron-star redshift budget), G_019 (the discriminatory signature; only neutron stars are a live arena), G_009 (the clock law), G_028 (the source law IS the clock law), G_032 (conformal flatness is an assumed primitive), G_027 (verdict discipline)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_036_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/TemporalCoreTestAudit.cs`

## The answer: BOUNDARY — and the reason is not the signal

G_035 proved the temporal core is **independent**. G_036 asks whether it is **testable**. The answer is a sharp
BOUNDARY, and the obstacle is not the size of the effect. It is *compactness*.

| question | answer |
|---|---|
| Is the temporal observable pure (g₀₀ only)? | **YES** — `z = 1/√(−g₀₀) − 1` needs no spatial metric |
| Is the signal large where it matters? | **YES** — `Δz = 0.125628` at J0740+6620, **45 %** of `z_AT` |
| Can any current experiment isolate the core? | **NO** — the precision-limiting compactness comes from light bending |
| Does a *pure* route exist? | **YES** — and it is about a factor of **3** short |

## The observable is pure, and the split is second order

| law | g₀₀ | redshift |
|---|---|---|
| **AT** | `−e^(−2x)` | `z = e^x − 1` |
| **GR** | `−(1 − 2x)` | `z = 1/√(1 − 2x) − 1` |

Series: AT `x + x²/2 + x³/6`, GR `x + 3x²/2 + 5x³/2`. They **agree at O(x)** and first differ at **O(x²)**:
`Δz = x² + (7/3)x³ + (13/3)x⁴ + …`. The observables are computed by functions whose signature contains **no B**
— the same arity argument G_035 used — so nothing about the spatial sector enters the *measurement*.

Both figures reproduce G_020's published values exactly: `Δz = 0.125628` (G_020: 0.1256) at `x = 0.247002`, and
at `x = 0.187982` the audit returns `z_AT = 0.206812`, `z_GR = 0.265888` — G_020's numbers to the digit.

## The arena is neutron stars only

Reaching `|Δz| > σ_z` requires a compactness above a threshold that **climbs with precision**:

| σ_z | needs x ≥ |
|---|---|
| 1e−3 | **0.030494** |
| 0.01 | **0.089439** |
| 0.017766 (G_020's equal split) | **0.115094** |
| 0.025125 | **0.133350** |

| arena | x | x² = Δz | pure? |
|---|---|---|---|
| terrestrial tower (Pound–Rebka class) | 2.45e−15 | 6.00e−30 | yes |
| optical lattice clocks (1 cm) | 1.1e−18 | 1.21e−36 | yes |
| solar surface | 2.12e−6 | 4.49e−12 | yes |
| binary pulsar (Einstein delay) | 1.0e−6 | 1.00e−12 | yes |
| white dwarf surface | 1.0e−4 | 1.00e−8 | yes |
| **neutron star J0740+6620** | **0.247002** | **1.256e−1** | yes — but see below |

The weak-field arenas would need `σ_z` between **1e−30 and 1e−12** — nine to twelve orders of magnitude below
anything measurable. This is the arithmetic behind G_019's finding that only neutron stars are a live arena.

## The weak-field split is not merely unmeasurable — it is unrepresentable

Each law passes through an intermediate ≈ 1, so each carries ~1 ulp = **2.2204e−16** of absolute error, while the
true split at the Pound–Rebka compactness is **6.00e−30** — fourteen orders below it. Measured:

| quantity at x = 2.45e−15 | value |
|---|---|
| true split (series) | **+6.0025e−30** |
| direct subtraction `z_GR − z_AT` | **−7.5093e−18** — wrong magnitude *and* **wrong sign** |

And at `x = 1.1e−18` (optical-clock compactness) `z_GR` is annihilated outright: `1 − 2x` rounds to `1.0` and the
GR redshift returns **exactly 0**. In the weak field the split can be *stated* but not *computed by subtraction*,
which is why the audit uses the series below `x = 1e−3`.

*(Note: `Math.Exp(x) − 1` behaves differently across runtimes here — it returns exactly 0.0 in Python and
2.4425e−15 in .NET for the same input. `AtNumerics.ExpM1` is used throughout so the audit does not depend on
which libm is underneath.)*

## The bottleneck is the compactness, not the redshift

G_020's budget, re-derived: `σ_x/x = **13.73 %**` achieved against `**3.661 %**` needed for 5σ — still **3.75×**
better, with a maximum attainable significance today of **1.334σ**. The signal itself is not the problem:
`Δz = 0.125628` is **44.84 %** of `z_AT`.

**And for a neutron star the compactness is itself obtained from light bending.** Pulse-profile modelling fits
the thermal emission from hot spots including light bending — the spatial sector that G_032 showed to be an
assumed primitive and G_035 showed the temporal core does not need. So:

> **The observable is pure, but the number required to read it is not.**

This is the audit's central finding, and it is why the verdict is BOUNDARY rather than TESTABLE.

## A pure route exists — and is about three times short

The apparent radius from a thermal flux and a parallax distance,

  `R_∞ = R / √(1 − 2x)`

is a **g₀₀ effect** — photon energy and arrival rate, not bending — so `{z, R_∞}` solves `{M, R}` with **no light
bending at all**. Its systematics (distance, atmosphere models) currently keep `σ_x/x` above the ≈3.7 % the test
needs. The pure route is real, and roughly a factor of three short.

## The first experiment

**Pound & Rebka (1960)** — the gravitational redshift of ⁵⁷Fe γ-rays over a 22.5 m tower, `x = 2.45e−15`. It is
**pure g₀₀** with no light bending anywhere in the measurement, and it is the first experiment to probe the
temporal core at all. But it probes only the **first-order** term — the one AT shares with GR, with
`x² = 6.0e−30`.

> **No experiment has yet probed the core's distinctive content.** The frontier is a compact-object `{z, R_∞}`
> measurement at `σ_x/x ≲ 3.7 %`.

## Output

**BOUNDARY** — testable in principle with a pure observable, not yet isolated in practice. The verdict is
**computed** (G_027): BOUNDARY because the observable is pure, a discriminating regime exists, and a pure route to
that regime exists — while the precision currently supplied is not pure.

## Result summary

- The temporal core is **independent** (G_035) *and* **cleanly testable in principle** (G_036) — its observable
  needs only g₀₀.
- The discriminating split is **O(x²)**, so the arena is **neutron stars only**; the weak field is nine to twelve
  orders short, and its split is not even representable in double precision.
- The live obstacle is **compactness precision**, not signal: `σ_x/x = 13.73 %` against 3.661 % needed.
- The compactness currently used to *read* the pure observable comes from **light bending** — so the core's only
  discriminating test is interpretively entangled with the spatial sector.
- A **pure** route (`{z, R_∞}` from thermal flux plus parallax distance) exists and is ~3× short.
- **Pound & Rebka (1960)** was the first probe of the core, and it is pure — but reaches only the term AT shares
  with GR.

## Classification and caveats

**No reclassification.** G_035's minimal time sector, G_020's redshift budget, G_019's signature, G_009's clock
law and G_004's calibration are unchanged inputs. D_040 untouched; no canonical claim, value or equation changes;
no new primitive. Deterministic: exact algebra on imported published values.
