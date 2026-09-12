# ResearchY-G_015 — Rho To Metric Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_015 (permanent)
**Title:** Rho To Metric Audit — can a laboratory q profile produce any measurable metric effect?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_015.md`
**Depends on:** ResearchY-G_001 (the source law), G_003 (the ambient calibration and the magnitudes), G_004 (the
calibration and the "why not already seen" answer), G_005 (the Poisson band and its ceiling), G_009 (the clock
law dτ/dt = ρ^(1/d)), G_010 (the drive pricing), G_011b (a laboratory readout carries no metric coupling),
G_014 (the kappa = 1 observable and the counting floor); AT-QG QG220 (ψ = √ρ e^{iθ}), QG15/QG228/QG231 (the
Poisson counting law)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_015_Tests.cs` (7/7 PASSED, ~0.03 s)

## Purpose

G_014 identified what *carries* ρ in a laboratory (the κ = 1 occupation/probability density) and G_011b showed
that carrying it is not the same as sourcing a metric. G_015 asks the closing question of the group:

> **Can a laboratory q profile produce any measurable metric effect?**
> Cases: photon occupation, cavity modes, resonator lattice, oscillator lattice.
> Compute: `Δτ`, `ΔΦ`, equivalent gravity. Use: G_010, G_011b, G_014.

**Answer.** In two channels. The **ANALOGUE** channel — the density *contrast itself*, `Δτ/τ = Δln q/d` — is
**MEASURABLE** for every case, because G_014 showed q is a κ = 1 observable (0.046374 s/day at the counting
floor, 86 277.089 s/day at a 20:1 contrast). The **METRIC** channel — a *real* time dilation, which needs the
lab's q to *be* the actualization density — is **REFUTED** at every laboratory scale: the only laboratory
handle on ρ is mass-energy, and the deepest case (1 kJ in 6 cm) gives `ΔΦ/c² = 1.3771e-40`, 1.38e-22 of the
1e-18 measurement floor. Every metric effect that *does* exist is **ASTROPHYSICAL ONLY**.

## 1. The two channels

The audit only becomes well-posed once G_014's result is used: the map `q = F(ρ)` is unique and it is the
**identity** (`κ = 1`, `L1(|ψ|², ρ) = 2.484991379e-16`). Two readings of "metric effect" then separate cleanly.

* **ANALOGUE (A).** `q` is a positive, normalised, cellwise counting density. Therefore
  `Δτ/τ = Δln q/d` is a *measurable ratio* at any contrast above the counting noise. No mass is needed — the
  clock reading is read off the density itself. This is a statement about **observability**.
* **METRIC (M).** A *physical* time dilation requires the laboratory's q to be the actualization density, i.e.
  to carry the metric coupling. The only laboratory handle on that coupling is **mass-energy** (G_011b: the
  coupling is not borrowed by a readout), so

  ```
  m = E/c² ,        ΔΦ/c² = G m / (R c²) ,        a = G m / R² .
  ```

  AT predicts exactly the Newtonian field for it (G_011b, G_004's 0.99600 self-checks), so this is the
  strongest possible laboratory effect and an *upper bound* on what a lab can do.

**Measurement floor.** `f_floor = 1e-18` — the best optical-clock fractional resolution. The clock law (G_009)
turns it into a density requirement `Δln ρ = d·f = 3.0e-18` and, via G_011b, into a mass requirement
`M/r = f c²/G`.

## 2. The clock ladder — what each target costs

| target | `f = Δτ/τ` | `Δln ρ` | `M/r` [kg/m] | `M` inside **1 cm** [kg] |
|---|---|---|---|---|
| measurement floor `1e-18` | 1.0e-18 | 3.0e-18 | **1.3466e9** | **1.3466e7** (13 466 t) |
| 1 ns/day | 1.1574e-14 | 3.4722e-14 | 1.5586e13 | 1.5586e11 |
| observed galactic (G_003) | 5.3673e-7 | 1.6102e-6 | **7.2276e20** | 7.2276e18 |
| G_005 band top (1 %) | 1.6289e-6 | 4.8867e-6 | **2.1935e21** | 2.1935e19 |

The floor alone is **13 466 tonnes inside a centimetre**; the observed galactic field needs
`M/r = 7.23e20 kg/m`. The self-check reproduces G_004: `M/r = GM/G / R = 9.3740e17 kg/m` for the Earth's
`6.9613e-10`.

## 3. The four cases — the metric effect of stored energy

Every laboratory readout that could touch the metric is an energy argument. Put the energy E in a region of
size R, take `m = E/c²`:

| case | `E` | `R` | `m = E/c²` [kg] | `ΔΦ/c²` | `a` [m/s²] | **× floor** | verdict |
|---|---|---|---|---|---|---|---|
| photon occupation (1 µm, 5.034116567542709e18 ph) | 1 J | 5 mm | 1.112650e-17 | **1.6525e-42** | 2.9705e-23 | 1.65e-24 | REFUTED |
| cavity modes (SRF, Q = 1e10) | 1 kJ | 6 cm | 1.112650e-14 | **1.3771e-40** | 2.0628e-22 | 1.38e-22 | REFUTED |
| resonator lattice | 1 mJ | 1 cm | 1.112650e-20 | **8.2627e-46** | 7.4262e-27 | 8.26e-28 | REFUTED |
| oscillator lattice (G_011b chain) | 1 nJ | 1 cm | 1.112650e-26 | **8.2627e-52** | 7.4262e-33 | 8.26e-34 | REFUTED |

The ordering is by stored energy over size: cavity modes > photon occupation > resonator lattice > oscillator
lattice. The *best* laboratory case is still **twenty-two orders** below the measurement floor
(`ΔΦ/c² = 1.3771e-40`); the equivalent Newtonian accelerations are ~1e-22 m/s², i.e. `~1e-23 g`.

## 4. The strongest possible laboratory case

No device beats an **energy-density** argument, so take the best densities known and hold them in a 1 m ball
(`V = 4.189 m³`, `R = 1 m`):

| energy density `u` [J/m³] | content | `ΔΦ/c²` | × floor | verdict |
|---|---|---|---|---|
| 1e9 | chemical | 3.4611e-35 | 3.46e-17 | REFUTED |
| 1e12 | capacitor | 3.4611e-32 | 3.46e-14 | REFUTED |
| 3e13 | magnetic | 1.0383e-30 | 1.04e-12 | REFUTED |
| 1e18 | **nuclear scale** | **3.4611e-26** | **3.46e-8** | REFUTED |
| 1e34 | neutron-star core | 3.4611e-10 | 3.46e8 | **ASTROPHYSICAL** |

The strongest thing a laboratory could ever hold in a metre — nuclear-scale energy density — is
**3.46e-8 of the floor**, still **2.889e7× short**. What the floor itself needs:

```
u_floor = f_floor c⁴/(G · (4/3)πR)  =  2.889272332033454e25 J/m³   = 2.889e7 × nuclear scale
u_band  = (band/D) c⁴/(G · (4/3)πR) = 4.706335701649293e37 J/m³   = 4706.3 × a neutron-star core
```

Both are compact-object densities. A neutron-star core (1e34 J/m³) *would* finally be measurable
(3.4611e-10 > 1e-18) — which is exactly the ASTROPHYSICAL-ONLY band, not a laboratory one.

## 5. The measurable channel, priced

The ANALOGUE channel is real and large, and its price in equivalent mass is the whole point:

| contrast | `Δln q` | `Δτ/τ · 86400` [s/day] | × floor | equivalent `M` at 1 cm [kg] |
|---|---|---|---|---|
| counting floor `1.6102e-6` | 1.6102e-6 | **0.046374** | 5.4e11 | 7.2276e18 |
| G_002 witness 4.8:1 | 1.568616 | **45 176.138** | 5.0e17 | 7.0409e24 (1.18 M⊕) |
| 20:1 | 2.995732 | **86 277.089** | 9.6e17 | 1.3447e25 (2.25 M⊕) |

The counting floor is *exactly* G_014's shot noise: `1/√⟨N⟩ = 1.610200e-6` with `⟨N⟩ = 3.856917553651e11`, and
its 0.046374 s/day is *exactly* G_009's galactic cross-check. **So the density is readable (MEASURABLE) while
the same reading, taken as a metric effect, would require 1.18 to 2.25 Earth masses at a centimetre.**

## 6. The critical answer

> **Does any physically realizable q produce a clock shift above the measurement floor?**

**No — in the metric sense.** The strongest laboratory configuration (nuclear-scale density in a metre) is
`3.4611e-26` against `1e-18`: a factor **2.889e7** short, and even that requires matter at nuclear density
sustained at bench scale. The floor itself is not laboratory physics: `M/r = 1.3466e9 kg/m` is 13 466 tonnes
within a centimetre, an energy density 2.889e7× the nuclear scale.

**Yes — in the analogue sense.** The contrast `Δln q/d` is a κ = 1 observable (G_014), so it is measurable at
0.046374 s/day at the counting floor and 86 277.089 s/day at 20:1. That is the object the group's laws are
testable on.

**Why the metric effects that exist are astrophysical.** The observed galactic field costs 0.0464 s/day and
needs `M/r = 7.2276e20 kg/m`; the G_005 band top costs 0.1407 s/day and needs `2.1935e21 kg/m` — nine orders
above the floor's requirement. Those are the only metric effects AT exhibits, and G_004's answer stands: no
anomaly is missed because every configuration that could produce one is compact-object density.

## 7. Verdicts

| label | content |
|-------|---------|
| **MEASURABLE** | the **analogue contrast channel**, all four cases: q is κ = 1 (G_014), so `Δτ/τ = Δln q/d` is directly readable — 0.046374 s/day at the counting floor `1.6102e-6`, 45 176.138 s/day at G_002's 4.8:1 witness, 86 277.089 s/day at 20:1. |
| **ASTROPHYSICAL ONLY** | **every metric effect that exists**: the observed galactic field (0.0464 s/day, `M/r = 7.2276e20 kg/m`) through the G_005 band top (0.1407 s/day, `2.1935e21 kg/m`); a neutron-star core (1e34 J/m³) would finally exceed the floor, at 3.4611e-10. |
| **REFUTED** | **a laboratory q profile producing a metric effect**: the deepest case (1 kJ, 6 cm) is 1.3771e-40 = 1.38e-22 of the floor; the strongest lab-scale case (1e18 J/m³ in a metre) is 3.4611e-26 = 3.46e-8 of it — 2.889e7× short. The floor requires 13 466 t within a centimetre. |

## 8. Classification and caveats

**No reclassification.** G_004's magnitudes, G_009's clock law, G_011b's no-borrowed-coupling result and
G_014's κ = 1 identification are unchanged and used as inputs. D_040 untouched; no canonical claim, value or
equation changes; no new primitive. Deterministic: exact algebra, no randomness.

* The METRIC verdict is an **upper bound**, not a survey: every laboratory quantity that could couple to the
  metric does so through mass-energy, and mass-energy is bounded above at bench scale by the nuclear density.
  Using nuclear-scale density is deliberately generous.
* The ANALOGUE verdict is about *observability*: it does not reopen G_011b. A lab can **measure** ρ (G_014) and
  **hold** a pattern (G_012/G_013); it cannot make ρ pull on clocks.
* The two verdicts are not in tension: "the contrast is measurable" and "the contrast is not a metric source"
  are the same statement viewed from the observation side and the source side.
* The floor is taken as `1e-18`; a `1e-21` clock would change the ladder by three orders and nothing about the
  verdicts (the lab case is eight to twenty-two orders short).

## 9. Open problems (OP1–OP5)

1. Is there a laboratory quantity with κ = 1 whose coupling to the metric is *derived* rather than assumed —
   i.e. can the identification premise itself be measured (G_014 OP4)?
2. What is the smallest mass at which AT's prediction *differs* from Newton's `ΔΦ/c²`? (G_011b showed they agree
   to the achievable precision for laboratory mass-energy; the divergence, if any, must be at compact-object
   scale.)
3. Does the analogue channel have a *differential* test — a comparison of `Δln q/d` between two platforms at
   equal energy that would separate AT from GR's clock reading?
4. Can the G_005 band top (0.1407 s/day) be read astrophysically in a system where the energy density is
   independently known, closing the ladder from below?
5. Is there any *non*-mass-energy coupling (G_011's ρ-inert phase channel) that nonetheless sources the metric,
   i.e. a second metric handle that is not bounded by nuclear density?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_015_Tests.cs` — **7/7 PASSED** (~0.03 s)
**Group total:** G_001–G_015 = **133/133 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_015"`

| label | content |
|-------|---------|
| **MEASURABLE** | the analogue contrast channel (κ = 1; 0.046374 → 86 277.089 s/day) |
| **ASTROPHYSICAL ONLY** | every metric effect that exists (galactic 0.0464 s/day → band top 0.1407 s/day) |
| **REFUTED** | a laboratory q profile producing a metric effect (best lab case 3.4611e-26, 3.46e-8 of the floor) |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_004.md`, `ResearchY-G_009.md`, `ResearchY-G_010.md`,
  `ResearchY-G_011b.md`, `ResearchY-G_014.md`
* `Docs/ResearchY/Tests/Results/Y_G_015_Result.md`
* `AT.Tests/Shared/PhysicalUnits.cs`; `AT.Core/ResearchXH/RhoDynamics.cs`
