# Y_G_012_Result.md — ResearchY-G_012 Local Rho Actuator Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_012_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 10/10 PASSED (~0.5 s) — group G total 108/108 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_012"`

## Summary

**Question:** can any physically realizable local process act as a ρ source? (active feedback, mode injection,
synchronized oscillators, driven D96 lattice, non-equilibrium steady states; requirements: local, finite
drive, survives DiffuseStep, no imported primitive)
**Answer:** **YES — the incremental local feedback `s = ρ − Wρ` is an ACTUATOR**: local (three-point),
count-neutral, finite, primitive-free, and it freezes **any** configuration exactly (witness class included).
It is **marginal** (closed loop = identity ⇒ a perfect memory with no restoring force), so it can *hold* a
configuration but not *create* one. Creation is REFUTED; the lattice/NESS framings are CORRELATED.

## Detail

| requirement | result |
|---|---|
| (1) local | `s = (I − W)ρ*` is a **three-point stencil** (perturbing `ρ_j` moves only `s_{j−1}, s_j, s_{j+1}`) |
| (2) finite drive | `‖s‖₁ = 0.48675` (witness), `max\|s\| = 0.01866667`; `3.331453e-10` (band top) |
| (3) survives DiffuseStep | 20 000 driven steps reproduce the target to < 1e-12 |
| (4) no imported primitive | `s_i = g(ρ_{i−1}, ρ_i, ρ_{i+1})` — built from the state alone |

| measure | result |
|---|---|
| **Δρ (freeze)** | witness held to **4.336809e-18** over 5000 steps; a 1e-6 perturbation retained **100.000 %** (8.674e-19) |
| **Δτ/τ** | band top 0.140737 s/day; 3:1 contrast 31 640.03 s/day; 10:1 66 314.45 s/day (both inside the SUPPRESSED band < ln 96) |
| **drive cost** | `0.48675`/step (witness) or `3.331453e-10` (band top) = the relaxation increment, strictly proportional |
| **stability (freeze)** | the closed loop is the **identity**: 95/95 modes neutral — a memory, no restoring force |
| **stability (restoring)** | λ ∈ [−1.200214165, **2.141650094e-4**]; only k = 1 holdable; k = 2 decays 6.422657e-4/step (τ = 1556.99); k = 48 at λ = 0.4 ⇒ μ₁ + λ = **1.3998** unstable |
| **runaway** | λ = 2(1 − μ₁): 1.000214165/step ⇒ saturation from a Poisson seed in **64 516 steps** |
| **three-point theorem** | for `s = β(Wρ − ρ)`, `c_k = μ_k(1 + β) − β` is 1 for every k **only at β = −1** (95/95 neutral; β = 0: 0/95) |
| **gain bound** | `HighKShare ≤ 2.8666657e-7 · D_high/w_1²`; the witness needs `D_high/w_1² ≥ 2.7790e6` |
| **compact masks** | block w = 2: 6.630273e-3 … w = 64: 6.368861e-9; edge dipole 0.1888945; staggered global 3.281127e-4 |
| **best structured mask** | **0.6117676** (77 % of the witness) — a *prescribed* mask, i.e. imported information |
| **cellwise test** | witness `(I − W)ρ*` spread within equal-ρ groups 1.58e-3 … **9.5e-3** vs `max\|s\| = 0.01866667` ⇒ not a function of `ρ_i`; a pure mode is exactly linear (9.3e-15) |
| **synchronization (local patch)** | `L1(ρ, \|ψ\|²) = 2.45e-16`, `max\|Δa\| < 1e-9`, coherent sum 0.7537609 |
| **medium (lattice)** | undriven attractor uniform (`L1` 0.6666667 → 0.0201006 in 200 steps); responses 2.5 → 5.0 → 10.0 → 20.0 → 40.0 → **120.0** |

## Verdicts

* **ACTUATOR** — the incremental local feedback `s = ρ − Wρ`: local, count-neutral, finite, primitive-free,
  freezes any configuration exactly (witness included). Marginal (identity closed loop) ⇒ a **memory**.
* **CORRELATED** — the driven D96 lattice (the medium) and the NESS framing (a NESS *is* `s = (I − W)ρ*`).
* **REFUTED** — mode injection (full support; a prescribed mask is imported), synchronized oscillators
  (exactly ρ-inert), every restoring gain beyond k = 1, and the local *creation* of the witness class.

## Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic, no randomness.

* **Refinement note (not a reclassification).** G_010's "the canonical chain supplies no driver" stands; G_012
  shows an *engineered* local feedback realises one, marginally and only as a memory. G_011 labels the
  **quantity** (a function of ρ ⇒ CORRELATED there), G_012 the **local generator** — complementary.
* The freeze is *engineered*, not canonical: it adds the self-coupling that NP_174 excludes from the branching
  flow.
* The marginal character is physical: no restoring force means a real device needs an initialisation process —
  the restricted (smooth-mode) restoring feedback.

## Open problems (OP1–OP4)

1. Can a nonlinear local generator hold a multi-mode profile with an asymptotically stable fixed point?
2. Does the marginal freeze admit a noise-driven version, and what is its lifetime under Poisson fluctuations?
3. Can the incremental feedback be realised in the G_011b lattice families within a finite gain tolerance?
4. Is there any canonical term whose closed-loop spectrum is the identity on the zero-sum subspace?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_012.md`
* `AT.Tests/Shared/RhoActuators.cs`, `AT.Core/ResearchXH/RhoDynamics.cs`
