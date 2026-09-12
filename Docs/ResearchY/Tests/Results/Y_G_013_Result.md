# Y_G_013_Result.md — ResearchY-G_013 Physical Actuator Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_013_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 9/9 PASSED (~0.4 s) — group G total 117/117 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_013"`

## Summary

**Question:** what physical process can realize `s = (I − W)ρ*` locally? (feedback controller, oscillator
lattice, coupled resonators, active diffusion cancellation, pump/loss networks; requirements: local, finite
power, exact stencil, maintains ρ*)
**Answer:** the stencil is a **negative Laplacian** (anti-diffusion, `d = 0.2`) and a **balanced
pump-and-drain** (50 %/50 %). **PHYSICAL**: the feedback controller and active diffusion cancellation (an
NIC). **ANALOGUE**: the oscillator lattice (node-wise gain) and a band-limited resonator bank. **REFUTED**:
pump/loss networks. The constraint is **exactness**, not power.

## Detail

| quantity | value |
|---|---|
| stencil | `s_i = −d(ρ_{i−1} − 2ρ_i + ρ_{i+1})` verified to **3.5e-18**; `max\|s\| = 0.01866667`; `‖s‖₁ = 0.48675` |
| balance | injection **50.0000 %** / extraction **50.0000 %** of `‖s‖₁` |
| gain-error law | closed loop `I + ε(I − W)` ⇒ mode k at `1 + ε(1 − μ_k)` (verified per step: ε = 1e-5 → 1.000007998 on v₉₅) |
| hold times | ε = 1e-6: 1.25e6 / 5.0e9 steps · 1e-5: 125 033 / 4.7e8 · 1e-4: 12 503 / 4.7e7 · **1e-3: 1250 / 4.669e6** · 1e-2: 125 / 4.669e5 (fastest / smoothest mode) |
| required gain spread | **3734.437**; a single broadband gain has minimax error **0.3997858349905463** (γ = 0.4) and a **1866.7×** relative error on the smoothest mode |
| band-limited bank | witness share in k ≤ 1 / 4 / 8 / 16 / 24 / 48: 3.240559e-4 / 1.479430e-3 / 4.833883e-3 / **3.626161e-2** / 8.417047e-2 / 2.035517e-1 ⇒ 96.4 % uncompensated at 16 modes |
| delay | roots `{1, μ_k − 1} ∈ [−0.7997858350, 0]` → stable; a 1e-3 perturbation retained (0.9986e-3 … 0.9995e-3 after 2000 steps) — still a memory |
| entropy per step | witness **ΔH = 0.2170247 nats**; band-top smooth profile **0.0** (floor) |
| power | Landauer `k_B T ΔH` = **8.6295e-20 J**/lattice/step ⇒ **8.63e-14 W** at 1 µs; free energy `1.5192e-19 J` ⇒ **1.52e-13 W**; a 1 mW controller is 1e9× above |
| sensor/actuation resolution | `q ≤ 1.0416667e-10` count units for 1 % of ρ̄ over 1e6 steps |
| maintaining (exact) | the controller freezes ρ* to < 1e-15 over 5000 steps (G_012) |

## Verdicts

* **PHYSICAL** — **feedback controller** (sense + compute + actuate; three-point local, exact stencil,
  marginal hold, `|ε| ≤ 1.25e-6` for 1.25e6 steps) and **active diffusion cancellation** (the operator is a
  nearest-neighbour negative conductance: an NIC realizes it element by element, half the elements sourcing
  and half sinking).
* **ANALOGUE** — **oscillator lattice** with a node-wise gain (a flat gain is mode-independent: minimax error
  0.3997858 against a 3734× required spread) and **coupled resonators** (band-limited: 96.4 % of the witness
  uncompensated by a 16-mode bank).
* **REFUTED** — **pump/loss networks**: the required source is balanced, but a pump/loss balance is a
  *scalar* condition whose fixed points are the single Neumann modes (G_012) and whose imbalance grows at the
  fastest mode's rate (factor e in 125 steps at 1 %).

## Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic, no randomness; no stochastic simulation is used.

* The power figures are the *thermodynamic minimum* (Landauer and the free-energy rate); a real controller's
  draw is set by its electronics, which is why the verdict turns on accuracy.
* The PHYSICAL verdicts inherit G_012's **marginal** character: these devices hold a configuration, they do
  not create one.
* The band-limited shares are the witness's own spectral content below the cut — the honest truncation error.

## Open problems (OP1–OP4)

1. Can a self-referencing controller (no externally supplied target) realize the stencil?
2. What loop bandwidth is required once the lattice has a physical step time τ?
3. Is there a *passive* network whose constitutive relation is exactly `s = −d Δρ` (a parametric pump)?
4. How does a real NIC's noise floor compare with the `q ≤ 1.04e-10` actuation-resolution requirement?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_013.md`
* `AT.Tests/Shared/RhoActuators.cs`, `AT.Core/ResearchXH/RhoDynamics.cs`
