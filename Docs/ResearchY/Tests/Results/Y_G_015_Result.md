# Y_G_015_Result.md — ResearchY-G_015 Rho To Metric Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_015_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.03 s) — group G total 133/133 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_015"`

## Summary

**Question:** can a laboratory q profile produce any measurable metric effect? (cases: photon occupation,
cavity modes, resonator lattice, oscillator lattice; compute Δτ, ΔΦ, equivalent gravity; use G_010, G_011b,
G_014)
**Answer:** in two channels. The **ANALOGUE** contrast channel (κ = 1, G_014) is **MEASURABLE** for every case;
the **METRIC** channel is **REFUTED** at every laboratory scale (best lab case 3.4611e-26 = 3.46e-8 of the
1e-18 floor) and every metric effect that exists is **ASTROPHYSICAL ONLY**.

## Detail — the clock ladder

| target | `f = Δτ/τ` | `Δln ρ` | `M/r` [kg/m] | `M` inside 1 cm [kg] |
|---|---|---|---|---|
| floor `1e-18` | 1.0e-18 | 3.0e-18 | **1.3466e9** | **1.3466e7** (13 466 t) |
| 1 ns/day | 1.1574e-14 | 3.4722e-14 | 1.5586e13 | 1.5586e11 |
| observed galactic (G_003) | 5.3673e-7 | 1.6102e-6 | **7.2276e20** | 7.2276e18 |
| G_005 band top | 1.6289e-6 | 4.8867e-6 | **2.1935e21** | 2.1935e19 |

## Detail — the four cases (`ΔΦ/c² = G m/(R c²)`, `m = E/c²`)

| case | `E`/`R` | `m` [kg] | `ΔΦ/c²` | `a` [m/s²] | × floor | verdict |
|---|---|---|---|---|---|---|
| photon occupation (1 µm, 5.034116567542709e18 ph) | 1 J / 5 mm | 1.112650e-17 | **1.6525e-42** | 2.9705e-23 | 1.65e-24 | REFUTED |
| cavity modes (SRF, Q = 1e10) | 1 kJ / 6 cm | 1.112650e-14 | **1.3771e-40** | 2.0628e-22 | 1.38e-22 | REFUTED |
| resonator lattice | 1 mJ / 1 cm | 1.112650e-20 | **8.2627e-46** | 7.4262e-27 | 8.26e-28 | REFUTED |
| oscillator lattice (G_011b chain) | 1 nJ / 1 cm | 1.112650e-26 | **8.2627e-52** | 7.4262e-33 | 8.26e-34 | REFUTED |

## Detail — the strongest possible laboratory case (1 m ball, `V = 4.189 m³`)

| `u` [J/m³] | content | `ΔΦ/c²` | × floor | verdict |
|---|---|---|---|---|
| 1e9 | chemical | 3.4611e-35 | 3.46e-17 | REFUTED |
| 1e12 | capacitor | 3.4611e-32 | 3.46e-14 | REFUTED |
| 3e13 | magnetic | 1.0383e-30 | 1.04e-12 | REFUTED |
| 1e18 | **nuclear scale** | **3.4611e-26** | **3.46e-8** | REFUTED |
| 1e34 | neutron-star core | 3.4611e-10 | 3.46e8 | **ASTROPHYSICAL** |

Required densities: `u_floor = 2.889272332033454e25 J/m³` (2.889e7 × nuclear scale);
`u_band = 4.706335701649293e37 J/m³` (4706.3 × a neutron-star core).

## Detail — the measurable channel, priced

| contrast | `Δln q` | `Δτ/τ·86400` [s/day] | × floor | equivalent `M` at 1 cm [kg] |
|---|---|---|---|---|
| counting floor `1.6102e-6` | 1.6102e-6 | **0.046374** | 5.4e11 | 7.2276e18 |
| G_002 witness 4.8:1 | 1.568616 | **45 176.138** | 5.0e17 | 7.0409e24 (1.18 M⊕) |
| 20:1 | 2.995732 | **86 277.089** | 9.6e17 | 1.3447e25 (2.25 M⊕) |

The counting floor is exactly G_014's shot noise (`1/√⟨N⟩ = 1.610200e-6`, `⟨N⟩ = 3.856917553651e11`) and its
0.046374 s/day is exactly G_009's galactic cross-check.

## The critical answer

> **No — in the metric sense.** The strongest laboratory configuration (nuclear-scale density in a metre) is
> `3.4611e-26` against `1e-18`: a factor **2.889e7** short. The floor itself needs 13 466 tonnes within a
> centimetre (`M/r = 1.3466e9 kg/m`), i.e. 2.889e7× nuclear density.
> **Yes — in the analogue sense.** The contrast `Δln q/d` is κ = 1 (G_014): 0.046374 s/day at the counting
> floor, 86 277.089 s/day at 20:1. That is the object the group's laws are testable on.

## Classification and caveats

**No reclassification.** G_004's magnitudes, G_009's clock law, G_011b's no-borrowed-coupling result and
G_014's κ = 1 identification are unchanged inputs. D_040 untouched; no canonical claim, value or equation
changes; no new primitive. Deterministic, no randomness.

* The METRIC verdict is an **upper bound** (nuclear density is deliberately generous): every laboratory
  quantity that could couple to the metric does so through mass-energy (G_011b).
* The ANALOGUE verdict is about *observability* and does not reopen G_011b: a lab can **measure** ρ (G_014) and
  **hold** a pattern (G_012/G_013), but cannot make ρ pull on clocks.
* The two verdicts are one statement seen from the observation side and the source side.

## Open problems (OP1–OP5)

1. A κ = 1 laboratory quantity whose metric coupling is *derived* rather than assumed?
2. Smallest mass at which AT's prediction differs from Newton's `ΔΦ/c²`?
3. A *differential* analogue test separating AT from GR's clock reading at equal energy?
4. Can the G_005 band top (0.1407 s/day) be read astrophysically with an independently known energy density?
5. Any non-mass-energy metric handle not bounded by nuclear density?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_015.md`
* `AT.Tests/Shared/PhysicalUnits.cs`, `AT.Core/ResearchXH/RhoDynamics.cs`
