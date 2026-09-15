# ResearchY-G_060 - Phase Evolution Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_060 (permanent)
**Title:** Can the difference-generated phase push be promoted to an actual update rule?
**Status:** COMPLETE
**Date:** 2026-09-15
**File:** `G_GravitySource/ResearchY-G_060.md`
**Depends on:** G_059 (the difference IS the source; the canonical state has no phase content; the difference's multiplier is exact), G_057 (the running process is phase-static), G_052 (the interface identity), G_050 (the phase sector is spanned by the sine modes)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_060_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/PhaseEvolutionAudit.cs`

## The question

Can the difference-generated phase push be **promoted to an actual update rule**? Test `ρ(t+1) = ρ(t) + εDρ` and other
AT-native updates. Measure the **phase evolution rank**, the **amplitude evolution rank**, **stability** and **fixed
points**. Critical: **does any AT-native update generate non-trivial phase dynamics?**

## The answer: **DERIVED - the promotion exists, and it is the norm-preserving form; the form the question names is the one that fails**

> **Every candidate is circulant, so one complex multiplier per channel is the whole update and nothing is estimated. The
> question's own step is STABLE, and that is exactly why it fails: its symmetric part is negative semi-definite, so it is
> DISSIPATIVE, and its attractor is the phase-free state. The phase survives only where `|μ| = 1`, and exactly one form
> has it.**

## 1. The multipliers (ε = 1E-3)

| update | kind | \|μ\| min … max |
|---|---|---|
| forward difference | the question's own test | 0.998000000 … 0.999997861 |
| backward difference | anti-dissipative | 1.000002143 … 1.002000000 |
| centred (skew) difference | a bare rotation generator | 1.000000000 … 1.000000500 |
| exact flow `exp(εD)` | the difference's own flow | 0.998001999 … 0.999997859 |
| **unitary (Cayley of the skew part)** | **norm-preserving** | **1.000000000 … 1.000000000** |
| positivity-clipped difference | AT-native: ρ is a density | 0.998000000 … 0.999997861 |
| actualization (CONTROL) | the running rule | 1.000000000 … 1.000000000 |

## 2. The question's own update is stable, and that is the problem

`ρ + εDρ` has `|μ|² = 1 − 2εx(1 − ε)` with `x = 1 − cos δ_c`, so it **contracts** for `0 < ε < 1`: the difference's
symmetric part is **negative semi-definite**, which makes this a **dissipative** step and not the skew step its name
suggests. Its attractor is the kernel of `D` - the **constant**.

| measurement (20 000 steps, ε = 1E-3) | value |
|---|---|
| deviation norm, start → end | **1.005E+000 → 2.424E-001** |
| the attractor | the **phase-free** uniform state |

**The closure with G_059:** the stable difference flow is attracted **exactly** to the state with no phase content,
which is why the canonical state has none.

**A claim is withdrawn.** A first draft asserted a plain decay of the phase content. Measured, the phase content
**oscillates** on the way down while the envelope falls - `0.065` at 100 steps, **`0.303` at 1000** - so the audit
reports the **deviation norm** as the envelope and the phase content as the oscillation.

## 3. The ranks: 42 of 53, by two independent routes

| update | reachable phase | by Krylov | reachable amplitude | unreachable |
|---|---|---|---|---|
| every difference form | **42** | **42** | **42** | **11** |
| actualization (CONTROL) | 0 | 0 | 42 | 53 |

**The eleven unreachable directions:** the **doubly-hidden channels 14, 19, 24, 32 and 40** (both quadratures - 10
directions, channels carrying no visible quadrature at all, so a phase-free state has nothing there to act on) plus the
**alternating mode** (channel 48 - a channel with no partner quadrature, which every circulant merely rescales).

## 4. Stability, with the simplex budget

| update | ε = 1E-3 | ε = 1E-1 | off-simplex after |
|---|---|---|---|
| forward difference | 42 decay, radius 0.999998 | 42 decay | > 4000 |
| backward difference | 42 grow, radius 1.002000 | 42 grow | **796 / 9** |
| centred (skew) difference | 42 grow, radius 1.000001 | 42 grow | > 4000 / **399** |
| exact flow `exp(εD)` | 42 decay | 42 decay | > 4000 |
| **unitary** | **42 sustain, radius exactly 1.000000** | **42 sustain** | **> 4000** |

The unitary form **conserves the deviation norm exactly** (1.005011E+000 at every horizon tested), conserves the sum
exactly, and keeps every cell positive over **20 000** steps at every ε tested.

## 5. Fixed points

| update | exactly-fixed dimension |
|---|---|
| forward / backward / `exp(εD)` / clipped | **1** (the constant) |
| centred (skew) / unitary | **2** (the constant **and the alternating mode**) |
| actualization (CONTROL) | **96** (it moves nothing) |

The centred difference **annihilates the alternating mode** - `(S − S⁻¹)e₄₈ = 0` - so one of the eleven directions the
phase cannot reach is in that form's **kernel**.

## 6. Defects recorded rather than hidden

- **The AT-native positivity guard never fires.** A clipped variant is carried because ρ is a density; measured, its
  orbit is numerically identical to the unclipped step (agreement to **5E-15**, minimum cell **7.086E-001** over 4000
  steps). More than one defect lived here: dividing by the clipped mean exploded when the clip removed most of the
  state, and it annihilated a zero-mean deviation outright - a **probe** defect, not a finding about AT. The guard now
  restores the mass by a uniform **shift**, which is bounded and reduces to the unclipped step wherever the clip is
  inactive.
- **The difference-of-orbits construction carries a measured leak of 2.329E-008** into the eleven unreachable
  directions - identical for the linear and the clipped update, which is how the audit knows the leak is its own
  arithmetic. The reachability measurement therefore uses the **linear** response, which has no such floor.
- **The rank scan must live in the space it measures.** Counting in the 96-dimensional state space produced a rank of
  **63** and then **54** inside a **53**-dimensional subspace; the count is now taken in the phase sector's own
  coordinate space, where the bound is structural.

## Verdict

**DERIVED.** The promotion exists: the **norm-preserving** form (the exact flow of the centred difference) sustains all
**42** reachable phase directions with `|μ| = 1` **exactly**, conserving the norm and the sum while keeping every cell
positive over 20 000 steps. The form the question names is the one that fails - `ρ + εDρ` is **stable because it is
dissipative**, and it drives the state to the phase-free attractor. And **11 of the 53 phase directions are
unreachable from any phase-free state by any update of this family**.
