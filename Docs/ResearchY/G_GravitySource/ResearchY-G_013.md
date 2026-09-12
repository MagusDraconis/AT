# ResearchY-G_013 — Physical Actuator Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_013 (permanent)
**Title:** Physical Actuator Audit — what physical process can realize s = (I − W)ρ* locally?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_013.md`
**Depends on:** ResearchY-G_006/G_007 (the relaxation operator W and its derived form/range), G_008 (the drive
law), G_009 (the clock reading), G_010 (the four targets), G_011 (the drive is imported), G_011b (the
laboratory operator families), G_012 (the incremental feedback is an ACTUATOR, marginal); AT-QG QG194 (count
conservation), QG220 (ψ = √ρ e^{iθ}); `AT.Core/ResearchXH/RhoDynamics.cs`
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_013_Tests.cs` (9/9 PASSED, ~0.4 s)

## Purpose

G_012 proved that the incremental feedback `s = ρ − Wρ` is a genuine actuator — but marginal, and it did not
say what could *build* it. G_013 asks the device question:

> **What physical process can realize `s = (I − W)ρ*` locally?**
> Candidates: feedback controller, oscillator lattice, coupled resonators, active diffusion cancellation,
> pump/loss networks.
> Requirements: **(1) local implementation, (2) finite power, (3) reproduces the exact stencil,
> (4) maintains ρ*.** Measure: drive, stability, power, error.

**Answer.** The stencil is a **negative Laplacian** — anti-diffusion of strength `d = 0.2` on the
nearest-neighbour chain, and a **balanced pump-and-drain** (exactly 50 %/50 %). Two candidates are
**PHYSICAL** (the feedback controller and active diffusion cancellation via a negative conductance), two are
**ANALOGUE** (the oscillator lattice's node-wise gain and a band-limited resonator bank), and pump/loss
networks are **REFUTED**. The binding constraint is not power (10 orders of margin) but **exactness**:
`|ε| ≤ 1.25e-6` of the mode rates for a 1.25e6-step hold of the witness.

## 1. The stencil in physical form

```
s_i = -d (rho_{i-1} - 2 rho_i + rho_{i+1})        (verified to 3.5e-18)
```

| quantity | value |
|---|---|
| peak drive | `max|s| = 0.01866667` (< 1) |
| total drive | `‖s‖₁ = 0.48675` per step (the witness) |
| balance | **injection 50.0000 % / extraction 50.0000 %** — a symmetrical pump-and-drain |
| locality | strictly three-point; count-neutral (`Σs = 0`) |
| operator | a **negative conductance** of canonical value `d = 0.2` per coupling |

## 2. Requirements per candidate

| candidate | (1) local | (2) power | (3) exact stencil | (4) maintains ρ* | **verdict** |
|---|---|---|---|---|---|
| **feedback controller** | yes (3-point) | `8.6e-14 W` | **yes** (exact) | yes, marginal (`\|ε\| ≤ 1.25e-6` for 1.25e6 steps) | **PHYSICAL** |
| **active diffusion cancellation** | yes (nearest neighbour) | `8.6e-14 W` | **yes** (element by element, an NIC) | yes, marginal (component tolerance) | **PHYSICAL** |
| **oscillator lattice** (node-wise gain) | yes | `8.6e-14 W` | no — mode-independent rate | no — minimax modal error **0.3997858** | **ANALOGUE** |
| **coupled resonators** (band-limited) | yes | `8.6e-14 W` | only over the retained band | no — a 16-mode bank leaves **96.4 %** uncompensated | **ANALOGUE** |
| **pump/loss networks** | yes | `8.6e-14 W` | no — a *scalar* balance | no — fixed points are single modes (G_012) | **REFUTED** |

## 3. The error budget — the binding constraint

With a residual loop gain `(1 + ε)` the closed loop is `I + ε(I − W)`, whose eigenvalues are
`1 + ε(1 − μ_k)`: mode k grows or decays at exactly that rate (verified step by step: ε = 1e-5 gives
1.000007998 per step on `v₉₅`, and the n-step law `(1 + ε(1 − μ_k))ⁿ` holds).

| ε | fastest mode (0.7997858350) | smoothest mode (2.141650094e-4) |
|---|---|---|
| 1e-6 | 1.250e6 steps | 5.0e9 steps |
| 1e-5 | 125 033 steps | 4.7e8 steps |
| 1e-4 | 12 503 steps | 4.7e7 steps |
| 1e-3 | **1 250 steps** | **4.669e6 steps** |
| 1e-2 | 125 steps | 4.669e5 steps |

The required per-mode gain spans **3734.437**, so a single broadband gain has a minimax error of
**0.3997858349905463** (at γ = 0.4) — a factor **1866.7** wrong on the smoothest mode. **A 0.1 % tolerance
holds the smooth class for 4.67e6 steps but the fine-grained witness class for only 1250 steps.**

**Sensor/actuation resolution.** A bounded per-step actuation error `q` gives a worst-case drift `≤ N q`
(random-walk in practice), so 1 % of `ρ̄` over 1e6 steps needs `q ≤ 1.0416667e-10` count units — about
17 bits of the 1/96 scale, comfortably within analogue or digital control.

**Delay.** A one-step control delay gives the roots `{1, μ_k − 1} ∈ [−0.7997858350, 0]`: **stable**, with an
alternating transient. A 1e-3 perturbation is *retained* (0.9986e-3 … 0.9995e-3 after 2000 steps) — the
delayed loop is still a marginal **memory**, not a stabiliser.

## 4. The power budget — not binding

Holding ρ* against the relaxation must remove the entropy the diffusion produces:

| quantity | witness | band-top smooth profile |
|---|---|---|
| entropy produced per step | **ΔH = 0.2170247 nats** | **0.0** (at the double-precision floor) |
| Landauer `k_B T ΔH` per lattice per step | 8.6295e-20 J | ≲ 1e-31 J |
| free energy `k_B T Σ s_i ln(ρ_i/ρ̄)` per step | 1.5192e-19 J | 6.39e-16 J |
| power at a 1 µs step | **8.63e-14 W** (Landauer) … **1.52e-13 W** | ≈ 0 |

Ten orders below the ~1 mW quiescent draw of any electronic controller, and the *smooth* class is
**thermodynamically free** (H is stationary in the k = 1 direction). **Power is not the constraint;
exactness is.**

## 5. Verdicts

| label | content |
|-------|---------|
| **PHYSICAL** | the **feedback controller** (sense ρ and its neighbours, compute (I − W)ρ, actuate: three-point local, exact, `\|ε\| ≤ 1.25e-6` for a 1.25e6-step hold) and **active diffusion cancellation** (the operator *is* a nearest-neighbour negative conductance, so an NIC realizes it element by element, half the elements sourcing and half sinking). Both are marginal memories (G_012), and a one-step delay is tolerable. |
| **ANALOGUE** | the **oscillator lattice** with a node-wise gain (a flat gain has a mode-independent rate, so the residual error is `\|γ − (1 − μ_k)\|` with minimax 0.3997858 against a 3734× required spread) and the **coupled-resonator bank** (band-limited: a 16-mode bank leaves 96.4 % of the witness's structure uncompensated; k ≤ 8: 0.483 %, k ≤ 24: 8.42 %, k ≤ 48: 20.36 %). |
| **REFUTED** | **pump/loss networks** as an *exact* actuator: the required source is indeed balanced, but a pump/loss balance is a **scalar** condition whose fixed points are the single Neumann modes (G_012's restoring-family theorem) — it maintains no arbitrary ρ*, and an imbalance grows at the fastest mode's rate (a factor e in **125 steps** at 1 %). |

## 6. Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic: exact algebra, no randomness (the Landauer and free-energy rates are evaluated on the exact
deterministic profiles; no stochastic simulation is used anywhere).

* The `8.6e-14 W` column is the *thermodynamic minimum* (Landauer) at a 1 µs step; a real controller's power
  is set by its electronics, which is why the verdict turns on accuracy rather than energy.
* The band-limited share figures are the witness's own spectral content below the cut — the honest
  truncation error of a finite-mode device.
* The PHYISICAL verdicts inherit G_012's **marginal** character: these devices *hold* a configuration; they
  do not create one.

## 7. Open problems (OP1–OP4)

1. Can a **self-referencing** controller (using ρ alone, no externally supplied target) realize the stencil,
   or is the target always imported (G_011)?
2. What loop bandwidth does the one-step-delay result require once the lattice has a physical step time τ?
3. Is there a **passive** (non-powered) network whose constitutive relation is exactly `s = −d Δρ` — i.e. is
   the negative conductance an *effective* property realizable by a parametric pump?
4. How does a real NIC's noise floor (Johnson noise amplified by the negative resistance) compare with the
   `q ≤ 1.04e-10` actuation-resolution requirement?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_013_Tests.cs` — **9/9 PASSED** (~0.4 s)
**Group total:** G_001–G_013 = **117/117 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_013"`

| label | content |
|-------|---------|
| **PHYSICAL** | feedback controller (exact, `\|ε\| ≤ 1.25e-6`) and active diffusion cancellation (the negative conductance/NIC) |
| **ANALOGUE** | oscillator lattice (node-wise gain, minimax error 0.3997858) and coupled resonators (band-limited, 96.4 % uncompensated at 16 modes) |
| **REFUTED** | pump/loss networks (a scalar balance maintains no arbitrary ρ*) |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_012.md`, `ResearchY-G_011.md`, `ResearchY-G_011b.md`,
  `ResearchY-G_008.md`, `ResearchY-G_007.md`, `ResearchY-G_005.md`
* `Docs/ResearchY/Tests/Results/Y_G_013_Result.md`
* `AT.Tests/Shared/RhoActuators.cs`, `DensityField.cs`, `PhysicalUnits.cs`; `AT.Core/ResearchXH/RhoDynamics.cs`
