# Y_G_011b_Result.md — ResearchY-G_011b Labor Rho Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_011b_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.1 s) — group G total 98/98 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_011b"`

## Summary

**Question:** can a laboratory system (oscillator lattice, resonator network, coupled modes, graph diffusion,
D96 controls) implement a controlled ρ profile?
**Answer:** the ρ **dynamics is PRACTICAL** (a 96-site chain with `d = 0.2` *is* `RhoDynamics.DiffuseStep`);
every **real clock or gravity readout is ASTROPHYSICAL** (`M/r = 1.35e6` t/m for the clock floor); a
**bench-side metric effect is REFUTED** (3.7e-28, 2.7e9× below the clock floor, and exactly Newtonian anyway).

## Detail — the five systems as operators

| system | λ_max | admissible `d` | `0.2 / d_max` | rate spread | verdict |
|--------|-------|----------------|---------------|-------------|---------|
| oscillator lattice (Neumann chain) | 3.998929 | **0.500134** | **0.3999** | **3734.44** | PRACTICAL |
| resonator network (ring) | 4.000000 | 0.500000 | 0.4000 | 934.11 | PRACTICAL |
| coupled modes / D96 controls (chain) | 3.998929 | 0.500134 | 0.3999 | 3734.44 | PRACTICAL |
| graph diffusion (D96 circulant) | 15.837372 | 0.126284 | **1.5837** | 41.00 | REFUTED at `d = 0.2` (unstable) |
| hub / star (control) | 96.000000 | 0.020833 | **9.6000** | 96.00 | REFUTED at `d = 0.2` |

| measure | result |
|---------|--------|
| eigenvector checks | chain `cos(πk(i+½)/N)`: residual **2.7e-14** over k = 1..95; ring `cos(2πkj/N)`: **≤4.9e-15** |
| derived range = CFL | ρ ≥ 0 ⇔ `\|μ_k\| ≤ 1` ⇔ `d ≤ ½`; at `d = 0.2` all 96 eigenvalues ∈ [0.200214, 1]; `d = 0.6` → `1 − 0.6λ_max < −1` |
| rate ladder | `2.141650094e-4` (k = 1) … `0.799785835` (k = 95); `μ_48 = 0.6` exactly |
| required drive | 1 ns/day: **7.436285e-18**/step (k = 1), 2.777034e-14 (k = 95); witness class: **0.48675**/step (49 % of the count), `max\|s\| = 0.01866667` |
| steady state | `ρ = ρ̄(1 + (Δlnρ/2)v_k)`; `Σρ = 1` (1e-12), `min ρ > 0`, log contrast `= Δlnρ·0.9998661`; excursion/ρ̄ ≤ 1.74e-5 ⇒ positivity never binds |
| hold law | `ρ ← Wρ + s` for 20 000 steps reproduces the target **< 1e-12**, at exactly `(1 − μ_1)` of the mode amplitude per step, `Σs = 0` |
| power | 1 mJ in a 1 µs step needs **7.4363e-15 W** for 1 ns/day — the obstacle is mode matching, not power |
| kinematic room | `ln 96 = 4.564348` ⇒ 1.521449 = **131 453 s/day**, at a k = 1 drive of 9.775237e-4/step |

## The gravity ladder

| depth | cost |
|-------|------|
| 1 kg at 1 m | 7.4262e-28 (**1.35e9× below the 1e-18 clock floor**) |
| 1 kg moved 1 m (1 m → 2 m, fixed total energy) | 3.7131e-28 (**2.69e9× below**) |
| 1000 kg at 1 m | 7.4262e-25 |
| Earth surface (self-check) | 6.9613e-10 = `GM⊕/(R⊕c²)`; `M/r = 9.3740e17 kg/m` = `M⊕/R⊕` |
| clock floor 1e-18 | `M/r = 1.3466e9 kg/m` = **1.35 million tonnes per metre** |
| 1 ns/day | `M/r = 1.5586e13 kg/m` |
| 1 ms/day | `M/r = 1.5586e19 kg/m` |
| band top 0.1407 s/day | `M/r = 2.1935e21 kg/m` = **2340 Earths per metre** |

## Verdicts

* **PRACTICAL** — the ρ dynamics, the drive, the steady state and the analogue readout. The derived
  admissibility `0 ≤ d ≤ ½` (G_007, from ρ ≥ 0) **is** the CFL stability bound of the explicit scheme, and
  the canonical `d = 0.2` sits at 0.3999 of it — a genuine cross-check between positivity and stability.
* **ASTROPHYSICAL** — every real clock or gravity readout (1.35e6 t/m … 2.19e21 kg/m).
* **REFUTED** — a bench-side clock or gravity effect from a ρ rearrangement at fixed total energy: the
  strongest honest operation is 3.7131e-28 while AT's own prediction is exactly Newtonian (G_004's 0.99600).

## Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic, no randomness.

* The five systems reduce to two operator families; the canonical `DiffuseStep` is the **ordered chain**, so
  the "graph diffusion" and "D96 controls" rows realise the same operator on different graphs.
* `G` in the mass ladder is the **measured** CODATA value; AT's derived `G` differs by 0.40 %, a common
  factor cancelling in any rate ratio (G_004).
* The kinematic room `ln 96` is a *counting* ceiling (G_005's `MaximumContrast`); nature realises only the
  Poisson band `4.8867e-6`.

## Open problems (OP1–OP4)

1. Can a mode-matched bench source be built to the 1e-18 relative tolerance G_010 requires?
2. Is the `N⁻²` scaling exploitable as a small-device argument?
3. Does the ordered-chain vs ring distinction (0.3999 vs 1.5837 of the stability bound) constrain any
   physical realisation of the occupancy index?
4. Can the ψ-sector (281× coherent-sum change at zero ρ-response) be read out as a non-metric AT observable?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_011b.md`
* `AT.Tests/Shared/RhoActuators.cs`, `AT.Tests/Shared/PhysicalUnits.cs`, `AT.Core/ResearchXH/RhoDynamics.cs`
