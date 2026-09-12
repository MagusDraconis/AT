# ResearchY-G_011b — Labor Rho Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_011b (permanent)
**Title:** Labor Rho Audit — can any laboratory system implement a controlled ρ profile?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_011b.md`
**Depends on:** ResearchY-G_003 (the potential channel and the length scale), G_004 (the gravity calibration),
G_005 (the Poisson ceiling), G_006/G_007 (the relaxation operator, its derived form and its derived range
`0 ≤ d ≤ ½`), G_008 (the drive law), G_009 (the clock law), G_010 (the four clock targets), G_011 (no
candidate is an actuator); AT-QG QG181/QG182 (the derived `G`); `AT.Core/ResearchXH/RhoDynamics.cs`
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_011b_Tests.cs` (7/7 PASSED, ~0.1 s)

## Purpose

> Can a laboratory system — oscillator lattice, resonator network, coupled modes, graph diffusion, D96
> controls — implement a controlled ρ profile? Measure per system: **required drive, steady-state ρ, clock
> shift, gravity shift.**

**Answer.** The ρ **dynamics** is textbook-exact and PRACTICAL (a nearest-neighbour chain of coupled
oscillators with `d = 0.2` *is* `RhoDynamics.DiffuseStep`, and the drive is tiny); every **real** clock or
gravity readout is ASTROPHYSICAL (the clock floor needs `M/r = 1.35e6` t/m); and a **bench-side metric
effect** from a ρ rearrangement is REFUTED — the strongest honest operation is `3.7e-28`, 2.7e9× below the
1e-18 clock floor, and in AT the resulting field is *exactly* Newtonian.

## 1. The five systems as operators

| system | operator | λ_max | admissible `d` | `0.2 / d_max` | rate spread |
|--------|----------|-------|----------------|---------------|-------------|
| oscillator lattice | Neumann chain (`DiffuseStep`) | 3.998929 | **0.500134** | **0.3999** | **3734.44** |
| resonator network | nearest-neighbour ring | 4.000000 | 0.500000 | 0.4000 | 934.11 |
| coupled modes / D96 controls | chain, mode-matched drive | 3.998929 | 0.500134 | 0.3999 | 3734.44 |
| graph diffusion | D96 circulant `C₉₆(±1..±6)` | 15.837372 | 0.126284 | **1.5837** | 41.00 |
| hub / star (control) | `{0, 1×(N−2), N}` | 96.000000 | 0.020833 | **9.6000** | 96.00 |

* `cos(πk(i+½)/N)` are exact eigenvectors of `DiffuseStep` (residual **2.7e-14** over `k = 1..95`) and
  `cos(2πkj/N)` are exact eigenvectors of the ring step (**≤ 4.9e-15**): the D96 controls are literally
  buildable.
* **The canonical `d = 0.2` is admissible for the degree-2 lattices only** — 0.3999 of the chain's bound,
  0.4000 of the ring's, but **1.5837×** over the D96 circulant's and **9.6000×** over a hub graph's. The
  occupancy index is an **ordered chain**, not the D96 mutation ring: the two lattices in the corpus are
  different objects with different stability bounds.
* The chain's suppression mechanism is **4× stronger** than the ring's (fundamental twice as smooth) but
  survives in both.

## 2. The derived range IS the CFL bound

The three conditions coincide: **ρ ≥ 0** (a convex combination, G_007's derivation), **|μ_k| ≤ 1**
(stability of the explicit step) and **`d ≤ ½`** are the *same* inequality. At `d = 0.2` all 96 eigenvalues
lie in `[0.200214, 1]`; just above the bound the fastest mode leaves `[−1, 1]` (`d = 0.6` → `−1.399`).

| quantity | value |
|----------|-------|
| `λ_max` (chain) | 3.998929 → `d_max = 0.500134` |
| `d = 0.2` | **0.3999 of the bound** |
| rate ladder `d·λ_k` | **2.141650094e-4** (k = 1) … **0.799785835** (k = 95) |
| `μ_48` | 0.600000000 exactly |

This is a genuine cross-check: the **positivity** of the counting measure and the **numerical stability** of
the explicit diffusion scheme are the same constraint.

## 3. Required drive, steady state, clock and gravity per target

| target | Δlnρ | drive/step (k = 1) | drive/step (k = 95) | × band | well | **M/r needed** |
|--------|------|--------------------|---------------------|--------|------|----------------|
| 1 ns/day | 3.472222e-14 | **7.436285e-18** | 2.777034e-14 | 7.11e-9 | 32.25 m/s | 1.5586e13 kg/m |
| 1 µs/day | 3.472222e-11 | 7.436285e-15 | 2.777034e-11 | 7.11e-6 | 1019.91 m/s | 1.5586e16 kg/m |
| 1 ms/day | 3.472222e-8 | 7.436285e-12 | 2.777034e-8 | 7.11e-3 | 32.25 km/s | 1.5586e19 kg/m |
| 1 s/day | 3.472222e-5 | 7.436285e-9 | 2.777034e-5 | **7.105** | 1019.91 km/s | 1.5586e22 kg/m |
| band top (G_005) | 4.8867e-6 | 1.046560e-9 | 3.908313e-6 | 1 | 382.62 km/s | 2.1935e21 kg/m |
| clock floor | 3.0e-18 | 6.425e-22 | 2.400e-18 | 6.14e-13 | 0.2998 mm/s | 1.3466e9 kg/m |
| kinematic room (ln 96) | 4.564348 | 9.775237e-4 | — | 9.34e5 | — | — |

* **Steady state.** `ρ_i = ρ̄(1 + (Δlnρ/2)v_k(i))`, `ρ̄ = 1/96`: `Σρ = 1` to 1e-12, `min ρ > 0`, and the log
  contrast is `2 atanh((Δlnρ/2)|v_1|max) = Δlnρ·0.9998661`. **Positivity never binds** (the largest
  excursion is `1.74e-5` of `ρ̄`).
* **The drive holds it exactly**: `ρ ← Wρ + s` for 20 000 steps reproduces the target to **< 1e-12**, at
  exactly `(1 − μ_1) = 2.141650094e-4` of the mode amplitude per step, with `Σs = 0`.
* For the **G_002 witness class** the price is the G_008 number: `‖s‖₁ = 0.48675` per step (49 % of the
  count), `max|s| = 0.01866667`.
* Power is trivial: `1 mJ` held in a `1 µs` step needs **7.4363e-15 W** for a 1 ns/day excursion. The
  obstacle is mode matching and the missing metric coupling, never power.

## 4. The gravity shift: what buys each depth

`ΔΦ/c² = GM/(rc²)` ⇒ `M/r = f c²/G` (measured `G`). Checked against AT's own Earth calibration:
`GM⊕/(R⊕c²) = 6.9613e-10` ⇒ `M/r = 9.3740e17 kg/m`, exactly `M⊕/R⊕`.

| depth | what it costs |
|-------|---------------|
| 1 kg at 1 m | 7.4262e-28 — **1.35e9× below the clock floor** |
| 1 kg moved 1 m (from 1 m to 2 m) | 3.7131e-28 — **2.69e9× below the clock floor** |
| 1000 kg at 1 m | 7.4262e-25 |
| 1e-18 (clock floor) | `M/r = 1.3466e9 kg/m` = **1.35 million tonnes per metre** |
| 1 ns/day | `M/r = 1.5586e13 kg/m` |
| 1 ms/day | `M/r = 1.5586e19 kg/m` |
| band top (0.1407 s/day) | `M/r = 2.1935e21 kg/m` = **2340 Earths per metre** |

## 5. There is no metric coupling to borrow

* The AT prediction for **any** real mass-energy arrangement *is* the Newtonian one: `GM⊕/R⊕² = 9.820250`
  with the derived `G` at 0.99600, and AT ≡ GR to double precision at the Earth's surface (G_004/G_009). So
  a laboratory ρ rearrangement produces **no new effect at any magnitude**.
* The **analogue** readout is trivially measurable — the fractional occupancy shift `Δlnρ/3 = 1.157407e-14`
  for a 1 ns/day target is a voltage/amplitude ratio, 10× above 1e-15 metrology — but it is a *configuration
  of the device*, not the actualization density of spacetime.
* The only laboratory handle that *does* change the actualization density is moving mass-energy, and then
  the effect is Newtonian at **1e-27**: the gravity channel of a bench device is REFUTED while its
  ρ-dynamics is PRACTICAL.

## 6. Verdicts

| label | content |
|-------|---------|
| **PRACTICAL** | the ρ dynamics, the drive, the steady state and the analogue readout: a 96-site chain with `d = 0.2` implements `DiffuseStep` exactly (residual 2.7e-14), the derived `d ≤ ½` *is* the CFL bound, the drive is `7.44e-18` per step (1 ns/day) or `0.48675` (the witness), positivity never binds, and the profile is held exactly. |
| **ASTROPHYSICAL** | every real clock or gravity readout: the 1e-18 floor needs `M/r = 1.35e6` t/m, 1 ns/day `1.5586e13`, 1 ms/day `1.5586e19`, the G_005 band top `2.1935e21 kg/m` (2340 Earths/m). |
| **REFUTED** | a bench-side clock or gravity effect from a ρ rearrangement at fixed total energy: the strongest honest operation is 3.7131e-28 (2.69e9× below the clock floor) and the AT field is exactly Newtonian. |

## 7. Classification and caveats

**No reclassification.** D_040 untouched. No canonical claim, value or equation changes; no new primitive:
the audit only maps existing structures onto laboratory operator families. Deterministic throughout.

* The five systems reduce to **two operator families** (degree-2 lattice vs hub/6-neighbour circulant); the
  physics (`DiffuseStep`) is the **chain**, so the "graph diffusion" and "D96 controls" rows are the same
  operator realised on different graphs.
* `G` is the **measured** CODATA value in the mass ladder (real bench masses); AT's derived `G` differs by
  0.40 %, a common factor that cancels in any rate ratio (G_004) and changes no verdict.
* The kinematic room is the one-cell counting ceiling `ln 96 = 4.564348` (G_005's `MaximumContrast`) — it is
  a *counting* limit, not an energy limit; what nature actually realises is the Poisson band `4.8867e-6`.

## 8. Open problems (OP1–OP4)

1. Can a **mode-matched** bench-source be built to the tolerance G_010 measured (1e-18 relative), and what
   does that require of the actuator's spectral purity?
2. Is the **`N⁻²`** scaling (G_010) exploitable as a *small-device* argument for a larger effective lattice?
3. Does the **ordered-chain vs ring** distinction (0.3999 vs 1.5837 of the stability bound) constrain any
   physical realisation of the AT occupancy index?
4. Could the **ψ-sector** (phase, 281× coherent-sum change at exactly zero ρ-response) be read out in a
   laboratory as a *non-metric* AT observable?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_011b_Tests.cs` — **7/7 PASSED** (~0.1 s)
**Group total:** G_001–G_011b = **98/98 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_011b"`

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_003.md`, `ResearchY-G_004.md`, `ResearchY-G_005.md`,
  `ResearchY-G_007.md`, `ResearchY-G_008.md`, `ResearchY-G_009.md`, `ResearchY-G_010.md`, `ResearchY-G_011.md`
* `Docs/ResearchY/Tests/Results/Y_G_011b_Result.md`
* `AT.Tests/Shared/RhoActuators.cs`, `AT.Tests/Shared/PhysicalUnits.cs`, `AT.Core/ResearchXH/RhoDynamics.cs`
