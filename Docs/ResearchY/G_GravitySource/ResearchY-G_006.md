# ResearchY-G_006 — Suppression Mechanism Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_006 (permanent)
**Title:** Suppression Mechanism Audit — what term suppresses large-density rearrangements?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_006.md`
**Depends on:** ResearchY-G_002 (the controllable free room), G_003 (the large theoretical Δg),
G_004 (the observed fields are small), G_005 (the ~34× contraction, mechanism unnamed);
AT-QG QG1 (ρ_{k+1} = μρ_k), QG194 (count conservation); D_047 (the mirror-pairing lock);
G4-ME21 (one void per octave); `AT.Core/ResearchXH/RhoDynamics.cs`
(`DiffuseStep`, `EntropyOf`), `UniversalAttractor.cs`, `NativeMetricDynamics.cs`
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_006_Tests.cs` (9/9 PASSED, ~1 s)
**Shared machinery:** `AT.Tests/Shared/DensityField.cs`, `AT.Tests/Shared/PhysicalUnits.cs`

## Purpose

G_005 measured a **~34× contraction** of the G_002 witness tilt after 200 canonical relaxation steps and
classified the large density rearrangements as SUPPRESSED — but it did not name the suppressing term.
G_006 names it by measuring four quantities (contraction rate, attractor basin, entropy production,
deficit evolution) on three cases (D96, D96³, Random) and answering four candidate mechanisms:

1. exponential decay? 2. power-law decay? 3. entropy driven? 4. actualization driven?

**Answer.** The suppressing term is the **relaxation (coarse-graining) operator**
`RhoDynamics.DiffuseStep` — a **linear low-pass filter** on the eigenspace-occupancy index with the exact
spectrum `μ_k = 1 − 2d(1 − cos(πk/N))`. The decay is **exponential per mode** and exactly computable;
"power law" is an *illusion* of the finite observation window; entropy is **downstream**, not a driver;
and the actualization/branching flow is **arrangement-neutral** and suppresses nothing.

**The 34× is DERIVED**: `1/r(200) = 33.78 = exp(200 × 0.0175991) = exp(3.5198 nats)` for the canonical
D96 witness tilt at `d = 0.2`. It is **not universal** (2.27× at 1 step, 33.78× at 200, 65.13× at 1000),
so the *law* is DERIVED while the *value* is EMERGENT.

## 0. What enters

| Input | Value | Status |
|-------|-------|--------|
| Relaxation operator | `Dx = x + d(x_{i−1} − 2x_i + x_{i+1})`, Neumann ghosts, `d = 0.2` | INPUT (canonical, `DiffuseStep`) |
| Lattice | D96 (96 cells, A₀ = 45); D96³ (884 736 modes, A₀ = 20 812); Random (96 cells, A₀ = 96) | INPUT |
| Witness arrangement | within-multiplet 80/20 tilt (G_002) | DERIVED |
| Suppression measure | std ratio of the arrangement, mean (k = 0) excluded | DEFINITION |
| G_005's measured contraction | 34× at m = 200 | for cross-check |

## 1. The operator and its exact spectrum — exponential, decisively

`D` is the discrete Laplacian with reflecting boundaries. Its eigenvectors are the Neumann cosine modes
`v_i = cos(πk(i+½)/N)`, and **every mode is an exact eigenvector** (verified to < 1e-12 by measuring
`s[i]/v[i]` for one and two steps):

```
mu_k = 1 - 2d(1 - cos(pi k/N)),   d = 0.2,   N = 96
```

| k | μ_k | μ_k²⁰⁰ |
|---|-----|--------|
| 0 | 1.000000000000 | 1 (the mean is an exact invariant) |
| 1 | 0.999785834991 | 9.5807e-1 |
| 5 | 0.994657332834 | 3.4253e-1 |
| 24 | 0.882842712475 | 1.5020e-11 |
| 48 | 0.600000000000 (λ₄₈ = 12 exactly) | 4.2683e-45 |
| 72 | 0.317157287525 | 1.7986e-100 |
| 95 | 0.200214165009 | 1.9905e-140 |

The slowest mode has a **1/e time of 4669 steps** and survives 200 steps at 0.958067; the fastest is
annihilated. The rate spread over 200 steps is **2.08 × 10⁻¹⁴⁰** — a 10¹⁴⁰ separation. One step
multiplies any mode by a *constant*, which is the definition of geometric decay: **no power law can do
this**, because a power law has no eigen-rate.

## 2. The exact closed form — and what the 34× actually is

The arrangement `ρ` is a superposition of modes, so

```
r(m) = sqrt( Σ_{k≥1} w_k² μ_k^{2m} / Σ_{k≥1} w_k² ),   w = DCT-II(ρ),  k = 0 dropped
```

**The closed form reproduces direct iteration to < 1e-9 at every horizon tested** (m = 1 … 50 000). The
suppression factor `1/r(m)` for the canonical D96 witness tilt:

| m | r(m) | 1/r(m) | rate −ln r/m |
|---|------|--------|--------------|
| 1 | 4.41463783e-1 | 2.27 | 8.17659e-1 |
| 10 | 1.62587073e-1 | 6.15 | 1.81654e-1 |
| 50 | 5.95435080e-2 | 16.79 | 5.64210e-2 |
| 100 | 3.97390419e-2 | 25.16 | 3.22542e-2 |
| **200** | **2.96049384e-2** | **33.78** | **1.75991e-2** |
| 500 | 2.04988751e-2 | 48.78 | 7.77477e-3 |
| 1000 | 1.53549663e-2 | 65.13 | 4.17632e-3 |
| 5000 | 6.16918965e-3 | 162.10 | 1.01764e-3 |
| 50 000 | 4.02047942e-7 | 2 487 266 | 2.94534e-4 |

**THE DERIVATION OF 34×:** `1/r(200) = 33.78 = exp(200 × 1.75991e-2) = exp(3.5198 nats)`. Nothing else
enters: it is the mixture rate over the G_005 horizon times the horizon. The arrangement's spectrum is
*broad* (weights spread over k = 1…95), so no single mode describes the 200-step window — which is
exactly why the number is not a constant (see §3).

## 3. Exponential or power law? The illusion, and its resolution

**(a) The illusion.** Over the accessible window m = 1…200:

| fit | form | R² |
|-----|------|----|
| exponential (2 params) | `ln r = −2.2141 − 0.00807870 m` | 0.7579 |
| power law (2 params) | `ln r = −0.6039 − 0.561161 ln m` | **0.9945** |

A power law fits the **aggregate better** — a naive fit would misname the mechanism. The window rate
(−8.0787e-3) is **37.7×** the true asymptotic rate.

**(b) The resolution.** Beyond the mixing horizon the envelope is **exactly a single exponential**:

```
tail fit over m = 5000..50000:  slope = -2.1418813131e-4
                                ln mu_1 = -2.1418794605e-4
                                difference = 1.85e-10      R^2 = 1.0000000000
```

A power law has no eigennumber to converge to; its log-slope keeps falling like `(ln m)/m`.

**(c) The rate floor.** The instantaneous rate falls monotonically
1.7599e-2 (m = 200) → 4.1505e-4 (m = 20 000) → 2.941e-4 (m = 50 000) → **|ln μ₁| = 2.1419e-4**:
it **converges to a geometric floor** instead of decaying to zero. The surviving mode is `k = 1`, the
slowest one the witness arrangement still contains (relative weight 1.7e-4).

## 4. Entropy production — a consequence, not a driver

* **Linearity.** `|D(ax + by) − a·Dx − b·Dy| = 3.47e-18` (machine zero). The operator is **exactly
  linear**, so it *cannot* depend on the entropy — a nonlinear functional of ρ. "Entropy driven" is
  refuted structurally, not merely numerically.
* **Downstream identity.** `H + (N/2)·E → ln N` as the Dirichlet energy `E → 0`:

| m | H(ρ) | ln 96 − H | E | N·E/2 | H + N·E/2 |
|---|------|-----------|---|-------|-----------|
| 0 | 4.29178299 | 2.7257e-1 | 6.3822e-3 | 3.0634e-1 | 4.59812674 |
| 10 | 4.55614642 | 8.2018e-3 | 1.6871e-4 | 8.0981e-3 | 4.56424448 |
| 50 | 4.56326089 | 1.0873e-3 | 2.2628e-5 | 1.0861e-3 | 4.56434701 |
| 200 | 4.56407873 | 2.6946e-4 | 5.5937e-6 | 2.6850e-4 | **4.56434722** |

`ln 96 = 4.56434819`: the identity residual at m = 200 is **9.7e-7**. The entropy rise is the
second-order expansion of the energy decay — **entropy is slaved to the contraction**, and the entropy
produced is a function of the energy removed (ratio → 1 as the quadratic correction dies).

## 5. Is it the actualization (branching) flow?

| probe | result |
|-------|--------|
| Branching continuity `ρ_{k+1} = μρ_k` (same μ per cell) | True for any μ |
| Scale invariance `a(λρ) = a(ρ)` | `|Δa| = 5.56e-11` at λ = 10⁶ |
| Density/metric static at criticality (μ = 1) | True / True |
| Attractor basin fraction (any size) | 1.0, and `UniversalAcrossSize` = True |
| Field amplitude `max|a|` after 200 **relaxation** steps | 0.6032 → 1.8746e-3 (322× removal) |

The branching flow multiplies **every** cell by the same μ: it scales the density and never moves
occupancy, so it contracts nothing (G_005 §5). The attractor basin is lattice- *and* size-independent, so
it cannot discriminate the witnesses either. **The relaxation operator does all of the work.**

## 6. The three cases: D96, D96³, Random

| case | modes | A₀ | free room | witness | factor at m = 200 |
|------|-------|-----|-----------|---------|-------------------|
| D96 | 96 | 45 | 51 | within-multiplet 80/20 tilt | **33.78** |
| D96³ | 884 736 | 20 812 | 863 924 | within-multiplet 80/20 tilt | **7.33** |
| Random | 96 | 96 | **0** | **EMPTY** (no degeneracy) | n/a |
| Random* | 96 | 96 | 0 | cell-scale alternation (extremal) | **55.13** |

\* The degeneracy-free control has every multiplicity equal to 1, so `Σ(m_i − 1) = 0`: the G_002
within-multiplet witness class is **empty** there, and the alternation is the extremal arrangement it
admits — annihilated fastest.

The cube's witness is only 4.6× weaker than D96's because its tilt lives inside blocks that are
individually shorter-lived (multiplicities up to 562 push the within-block content to higher k), while
most of its 20 812 eigenspaces are unaffected.

**Structural finding.** `μ_k` depends on **N only**: D96 and Random (both N = 96) have the *identical*
operator; the cube (N = 884 736) has `μ₁ = 0.99999999999748` (`|ln μ₁| < 1e-11`) — a slow mode that
essentially never decays. **The mechanism is arrangement-selective (a low-pass filter on the occupancy
index), not lattice-selective: no property of the D96 spectrum is doing the suppressing.**

## 7. Verdict

| claim | verdict |
|-------|---------|
| Each Neumann mode decays geometrically (exact eigenbasis) | **DERIVED** |
| Closed form `r(m) = sqrt(Σ w_k²μ_k^{2m}/Σ w_k²)` (iteration < 1e-9) | **DERIVED** |
| The 34× `= 1/r(200) = exp(200·rate(200))` | **DERIVED** (as a closed form) |
| The 34× as a *universal* constant | **EMERGENT** (2.27× at m = 1; 33.78× at 200; 65.13× at 1000) |
| Power-law decay as the law | **REFUTED** (tail slope = `ln μ₁` to 1.9e-10) |
| Power-law *appearance* over m = 1…200 | **EMERGENT** (R² 0.9945 > 0.7579) |
| Entropy-driven suppression | **REFUTED** (linear operator; H slaved to E) |
| Actualization/branching-driven suppression | **REFUTED** (arrangement-neutral) |
| Relaxation (coarse-graining) as the suppressing term | **DERIVED** |

## 8. What this closes, and what it opens

**Closes G_005's open mechanism.** The G_002 free directions are *within-multiplet* rearrangements, i.e.
**high-k content** in the occupancy index, so they are precisely the modes the coarse-graining filter
removes fastest. The smooth, scale-free deficit that produces the observed galactic field (G4-ME21's
"one void per octave", effectively the k = 1 mode) sits on the **slowest** mode and survives:
`μ₁²⁰⁰ = 0.958`. **Suppression and observability are the two ends of the same filter** — which is why
G_004 finds the observed field calibrated while G_003's witnesses never appear.

**Classification.** DERIVED: the relaxation-as-mechanism statement, the exact spectrum, the closed form,
the asymptotic single-exponential tail, the linearity and the downstream entropy identity, the
arrangement-vs-lattice selectivity. EMERGENT: the specific 34× and the apparent power-law exponent.
REFUTED: power-law decay as the law, entropy-driven suppression, branching-driven suppression.
**No reclassification** — the D_040 registry is untouched; G_005's verdicts (ACCESSIBLE / SUPPRESSED /
FORBIDDEN) are unchanged and are here *explained*, not revised.

**Open (OP1–OP4).**
1. **Why `d = 0.2`?** The damping is canonical (`UniversalAttractor.DefaultDamping`) but not derived
   here; the *law* holds for any `d ∈ (0, ½)`, the *value* of the 34× scales with `d`.
2. **Could a shorter-horizon suppression be realised dynamically?** At m = 1 the factor is only 2.27;
   the 34× is a 200-step figure.
3. **The smoothness mapping.** §8 assumes spatial smoothness ≈ occupancy-index smoothness (the deficit
   is a scale-free, one-void-per-octave configuration). The exact map is not exhibited.
4. **The cube's weaker suppression (7.33×)** suggests multiplicity structure sets the filter's passband;
   whether that is observable in the D96³ program is untested.

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_006_Tests.cs` — **9/9 PASSED** (~1 s)
**Group total:** G_001–G_006 = **49/49 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_006"`

| verdict | result |
|---------|--------|
| DERIVED | the relaxation operator is the suppressing term; geometric per-mode decay with `μ_k = 1 − 2d(1 − cos(πk/N))`; the closed form (iteration < 1e-9); the 34× as `exp(200 × 0.0175991)`; the asymptotic exponential tail (slope = `ln μ₁`, 1.9e-10); linearity (3.5e-18); entropy slaved to the Dirichlet energy (residual 9.7e-7) |
| EMERGENT | the value 33.78 (horizon- and arrangement-specific: 2.27 → 33.78 → 65.13 across m = 1, 200, 1000) and the apparent power law (`α = 0.5612`, R² = 0.9945 over 1…200) |
| REFUTED | power-law decay as the law; entropy-driven suppression; actualization/branching-driven suppression |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_005.md` (the 34× measurement; §5 dynamical accessibility)
* `Docs/ResearchY/G_GravitySource/ResearchY-G_002.md` (the free room = within-multiplet directions)
* `Docs/ResearchY/Tests/Results/Y_G_006_Result.md`
* `AT.Core/ResearchXH/RhoDynamics.cs` (`DiffuseStep`, `EntropyOf`), `UniversalAttractor.cs`,
  `NativeMetricDynamics.cs`
* `Docs/Research/G4ME_LongRangeGravity.md` (one void per octave — the surviving slow mode)
