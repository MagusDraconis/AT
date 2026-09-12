# ResearchY-G_012 — Local Rho Actuator Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_012 (permanent)
**Title:** Local Rho Actuator Audit — can any physically realizable local process act as a ρ source?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_012.md`
**Depends on:** ResearchY-G_002 (the free directions), G_003/G_009 (the Δa_AT and clock conventions), G_005 (the
Poisson band, 4.8867e-6, and the one-cell ceiling ln 96), G_006/G_007 (the relaxation operator, its derived
form and range), G_008 (the drive law: s = (I − W)ρ* holds any target), G_010 (the four clock targets),
G_011 (no independent handle; the drive is imported), G_011b (the laboratory realisation); AT-QG QG194 (count
conservation), QG220 (ψ = √ρ e^{iθ}); `AT.Core/ResearchXH/RhoDynamics.cs`
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_012_Tests.cs` (10/10 PASSED, ~0.5 s)

## Purpose

The previous two audits left a precise vacancy: G_011 found that no physical *quantity* acts on ρ and that the
only handle is an imported source; G_011b showed the ρ *dynamics* is bench-top. G_012 asks the engineering
question directly:

> **Can any physically realizable LOCAL process act as a ρ source?**
> Candidates: active feedback, mode injection, synchronized oscillators, driven D96 lattice,
> non-equilibrium steady states.
> Requirements: **(1) local implementation, (2) finite drive, (3) survives DiffuseStep, (4) no imported
> primitive.** Measure: Δρ, Δτ, drive cost, stability.

**Answer.** **YES — the incremental local feedback `s = ρ − Wρ` is an ACTUATOR**: local (three-point),
count-neutral, finite, built from the state alone, and it **freezes any configuration exactly, including the
gravity-control witness class**. But it is **marginal** — the closed loop is the *identity*, so it is a
perfect **memory** with no restoring force: it can **hold** a configuration, not **create** one. Everything
that would create one is REFUTED (mode injection, synchronization, every restoring gain other than the
smoothest mode), and the lattice/NESS framings are CORRELATED (the medium, not the source).

## 1. The four requirements

| requirement | result |
|---|---|
| **(1) local** | `s = (I − W)ρ*` is a **three-point stencil**: perturbing `ρ_j` moves only `s_{j−1}, s_j, s_{j+1}` (verified). G_008/G_010's "mode matching" was about *knowing* the target, not about the stencil. |
| **(2) finite drive** | `‖s‖₁ = 0.48675` per step for the witness, `max|s| = 0.01866667` (< 1); `3.331453e-10` for the band-top smooth profile. |
| **(3) survives DiffuseStep** | held exactly: 20 000 driven steps reproduce the target to < 1e-12. |
| **(4) no imported primitive** | requires the source to be a function of the local state, `s_i = g(ρ_{i−1}, ρ_i, ρ_{i+1})`. |

## 2. ACTUATOR — the incremental local feedback freezes any configuration

`s = ρ − Wρ` (the negative relaxation increment) makes the closed loop

```
rho <- W rho + (rho - W rho) = rho        (the IDENTITY)
```

| configuration | freeze error after 5000 steps |
|---|---|
| the G_002 witness tilt | 4.336809e-18 |
| the highest mode v₉₅ | < 1e-15 |
| the uniform measure | < 1e-15 |
| the tilt **plus** a 1e-6 perturbation | 8.674e-19 — **retention 100.000 %** |

So it is a **memory, not a stabiliser**: there is no restoring force, an error is preserved rather than
corrected, and the hold is marginal (all 95 closed-loop eigenvalues are exactly 1).

**The three-point linear theorem.** For the count-conserving three-point family `s = β(Wρ − ρ)` the closed-loop
eigenvalues are `c_k = μ_k(1 + β) − β`, and `c_k = 1` for **every** k only at **β = −1**:

| β | c₁ | c₄₈ | c₉₅ | neutral modes |
|---|---|---|---|---|
| **−1** | **1.000000** | **1.000000** | **1.000000** | **95 / 95** |
| −0.5 | 0.999893 | 0.8 | 0.600107 | 0 |
| 0 (plain relaxation) | 0.999786 | 0.6 | 0.200214 | 0 |
| +0.5 | 0.999679 | 0.4 | −0.199679 | 0 |

The freezing feedback is therefore the **unique non-trivial member** of its family, and it freezes all modes
at once.

## 3. The restoring family is limited to the smoothest mode

`s = λ(ρ − ρ̄)` is count-neutral and cellwise; its closed-loop spectrum is `μ_k + λ`, so stability requires

```
lambda in [ -(1 + mu_95), 1 - mu_1 ] = [ -1.200214165, 2.141650094e-4 ]
```

A fixed point carrying mode k needs `λ = 1 − μ_k`, and **only k = 1 fits inside the window** (k = 2 would need
8.564307e-4 > 2.141650094e-4, and the k = 48 attempt at λ = 0.4 gives `μ₁ + λ = 1.3998 > 1` — unstable in the
smoothest mode).

| quantity | value |
|---|---|
| k = 1 held at λ = 1 − μ₁ | exact to 1e-15 over 20 000 steps |
| k = 2 decay at the same λ | 6.422657e-4 per step (τ = 1556.99), matching `(μ₂ + λ)^5000 = 0.0402614767` |
| mixture (k = 1 + k = 2) | converges onto its k = 1 part (relative error 2.4e-12) — mode-selective |
| runaway at λ = 2(1 − μ₁) | 1.000214165 per step ⇒ saturation from a Poisson-scale seed in **64 516 steps** |

## 4. What a compact mask can and cannot do

The gain of `(I − W)⁻¹` **decreases** with k (4669.2968 at k = 1 → 1.2503 at k = 95), giving the hard bound

```
HighKShare(rho*)  <=  (g_1/g_48)^2 · D_high/w_1^2  =  2.8666657e-7 · D_high/w_1^2
```

verified for every tested family. Measured profile shares:

| source | high-k share (k ≥ 48) |
|---|---|
| block window w = 2 (the sharpest local source) | 6.630273e-3 |
| block windows w = 4 / 8 / 16 / 32 / 64 | 4.834875e-4 / 1.390521e-5 / 1.106470e-6 / 8.138471e-8 / 6.368861e-9 |
| edge dipole (Neumann boundary) | 0.1888945 |
| fully staggered global source (checkerboard) | 3.281127e-4 |
| **best structured ±1 mask (w = 64)** | **0.6117676** |
| **the G_002/G_003 witness** | **0.7965733** |

To reach the witness share a source needs `D_high/w_1² ≥ 2.7790e6` (half of it: 1.7442e6). Unstructured compact
sources are thousands of times short; a *structured* ±1 mask reaches 77 % of the witness — **but a mask is
prescribed data, not a local process**, which is exactly the "mode injection" candidate.

**Mode injection is REFUTED as a local process.** The drive holding a pure mode is `s = (1 − μ_k)c·v_k`, which
has full support and no compact realisation; for the witness it is **98.3 % high-k** (against the profile's
79.7 %). And the cellwise test fails outright for the witness: `(I − W)ρ*` takes spreads of **1.58e-3 … 9.5e-3**
within groups of *equal* ρ — comparable to the source values themselves (`max|s| = 0.01866667`) — so it is not
a function of `ρ_i` at all. A pure mode, by contrast, is exactly cellwise-linear (deviation 9.3e-15 for
k = 48).

## 5. Synchronization, the lattice and non-equilibrium steady states

* **Synchronized oscillators — REFUTED.** A *locally locked patch* (cells 40–55 share a phase, the rest keep
  the canonical grid) leaves ρ bit-identical: `L1(ρ, |ψ|²) = 2.45e-16`, `max|Δa| < 1e-9`, while the ψ-sector
  coherent sum moves to 0.7537609.
* **Driven D96 lattice — CORRELATED.** The lattice is the **medium**: its undriven attractor is uniform
  (`L1` from the tilt collapses 0.6666667 → 0.0201006 in 200 steps) and its response ratios
  `max|ρ*|/max|s|` run 2.5 (dipole) → 5.0 → 10.0 → 20.0 → 40.0 → 120.0 (checkerboard). The lattice converts a
  source into a profile; it is not the source.
* **Non-equilibrium steady states — CORRELATED.** A NESS is *defined* by `s = (I − W)ρ*`, so its taxonomy **is**
  the source taxonomy: the frozen configuration is exactly the NESS of the incremental feedback, and the
  restoring NESS of a uniform gain exists only for k = 1.

## 6. Readouts (Δρ, Δτ, drive cost, stability)

| | value |
|---|---|
| **Δρ** | 0 to 1e-18 over 5000 steps for the freeze (any configuration, including the witness); `L1 = 0.6666667` from uniform is held unchanged |
| **Δτ/τ = Δlnρ/3** | band top 1.628900e-6 ⇒ **0.140737 s/day**; a 3:1 contrast (Δlnρ = 1.0986) ⇒ **31 640.03 s/day**; 10:1 ⇒ **66 314.45 s/day** (both inside G_005's SUPPRESSED band, below ln 96 = 4.564348) |
| **drive cost** | `0.48675` per step (witness, 49 % of the count) or `3.331453e-10` (band top) — strictly proportional to the configuration, and **independent of the target's tail** since it *is* the relaxation increment |
| **stability** | freeze: the closed loop is the identity, 95/95 modes **neutral** (a memory); restoring: λ ∈ [−1.200214165, 2.141650094e-4] with only k = 1 holdable, k = 2 decaying at 6.422657e-4/step, runaway at 1.000214165/step above threshold |

## 7. Verdicts

| label | content |
|-------|---------|
| **ACTUATOR** | the **incremental local feedback** `s = ρ − Wρ`: three-point local, count-neutral, finite, primitive-free, and it holds **any** profile exactly — including the gravity-control witness class that no compact mask can hold. Marginal (identity closed loop), so it is a **memory**, not a creator. |
| **CORRELATED** | the **driven D96 lattice** (the medium; responses 2.5–120, undriven attractor uniform) and **non-equilibrium steady states** (the general form of a held state; a NESS *is* `s = (I − W)ρ*`). |
| **REFUTED** | **mode injection** (non-local — full support; 98.3 % high-k for the witness; a structured mask is prescribed data, not a process); **synchronized oscillators** (exactly ρ-inert, patch L1 = 2.45e-16); every **restoring gain beyond k = 1** (the k = 2 requirement already breaks stability); and the **local creation** of the witness class. |

## 8. Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; no new primitive (the
feedback is linear in the ρ of the cell and its neighbours, with the canonical coupling `d`).

* **Refinement note (not a reclassification).** G_010 stated that the canonical chain supplies no driver —
  **still true**. G_012 shows an *engineered* local feedback realises one, marginally and only as a memory.
  Likewise G_011 labels the **quantity** (this feedback is a function of ρ, hence CORRELATED there, since it is
  not an independent handle) while G_012 labels the **local generator**; the two audits are complementary.
* The freeze is *not* canonical: it adds a term to the evolution (the self-coupling NP_174 excludes from the
  branching flow). It is an *engineered* actuator, exactly as the candidate list implies.
* The marginal character is physical, not a numerical artefact: the identity closed loop means the actuator
  cannot correct an error, so a real device needs an initialisation process — and that process is the
  restricted (smooth-mode) restoring feedback of §3.
* The `0.6117676` structured-mask result is reported because it is the honest upper end of what a *prescribed*
  compact mask achieves; the audit's REFUTED verdict for mode injection rests on the mask being imported
  information, not on the mask's share being small.

## 9. Open problems (OP1–OP4)

1. Can a **nonlinear** local generator `g(ρ_{i−1}, ρ_i, ρ_{i+1})` hold a multi-mode profile with an
   *asymptotically* stable fixed point (the discrete nonlinear Poisson problem)? The cellwise test already
   excludes the witness for one-point generators.
2. Does the marginal freeze admit a **noise-driven** version (a stochastic feedback whose mean is the identity)
   and what is its lifetime under G_005's Poisson fluctuations?
3. Can the incremental feedback be realised in the G_011b lattice families (resonator network, graph
   diffusion) within a *finite* gain tolerance, given that the window is a single point?
4. Is there any canonical (rather than engineered) term whose closed-loop spectrum is the identity on the
   zero-sum subspace — i.e. does AT itself contain a "memory" channel?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_012_Tests.cs` — **10/10 PASSED** (~0.5 s)
**Group total:** G_001–G_012 = **108/108 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_012"`

| label | content |
|-------|---------|
| **ACTUATOR** | the incremental local feedback `s = ρ − Wρ` — freezes any configuration (witness included) exactly; three-point local, count-neutral, finite, primitive-free; marginal (95/95 modes neutral ⇒ a memory, not a creator) |
| **CORRELATED** | the driven D96 lattice (the medium) and the NESS framing (a NESS *is* `s = (I − W)ρ*`) |
| **REFUTED** | mode injection (full support, prescribed mask), synchronized oscillators (Δρ = 0), every restoring gain beyond k = 1, and local creation of the witness class |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_008.md`, `ResearchY-G_010.md`, `ResearchY-G_011.md`,
  `ResearchY-G_011b.md`, `ResearchY-G_005.md`, `ResearchY-G_006.md`, `ResearchY-G_007.md`
* `Docs/ResearchY/Tests/Results/Y_G_012_Result.md`
* `AT.Tests/Shared/RhoActuators.cs`, `DensityField.cs`, `PhysicalUnits.cs`
* `AT.Core/ResearchXH/RhoDynamics.cs`
