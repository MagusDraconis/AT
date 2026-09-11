# ResearchY-NP_172 — Retention Null Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_172 (permanent)
**Title:** Retention Null Audit — can two-time-scale retention arise without any AT barrier mechanism?
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `NP_NewPhysics/ResearchY-NP_172.md`
**Depends on:** ResearchY-NP_170 (H2 and the §6 R4 ring-down protocol, §7 statistics, §9 boundaries),
ResearchY-NP_171 (the deterministic lock-lattice simulator and its locked coupling g_c = 1.607),
ResearchY-T_015 (degeneracy-matched control logic), ResearchY-NP_005 (the nonlinearity is an imported
input), QG120 (the one-way barrier whose transferability H2 tests), QG313 (values are domain-specific),
QG316/QG319 (the protocol's statistical rules)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_172_Tests.cs` (5 tests, all passing)
**Simulator:** `AT.Core/Resonance/Kuramoto/RetentionNullSimulator.cs`

---

## Question

NP_170's **H2** claims that a locked configuration **retains state longer than its controls**, with the
pre-registered signature: a two-time-scale ring-down E(t) = a·e^(−t/τ_fast) + b·e^(−t/τ_slow), **R =
τ_slow/τ_fast ≥ 10**, preferred over the single-exponential null by BIC/AIC, while all controls stay
single-exponential. The physical mechanism H2 appeals to is the **AT-120 one-way barrier (QG120)** — which
NP_170 §9 already flags as an *analogy across systems*, not a derivation.

This audit asks the null question, in the form the project's own honesty rules demand:

> **Can a two-time-scale retention signature arise from models that contain NO AT barrier at all?**

If it can, then R4 retention cannot discriminate the lock lattice from ordinary oscillator physics, and H2
must stay labelled an analogy. Five models are charged and ringed down, plus two mechanism probes.

---

## Method

### The models

All five share one deterministic complex-amplitude ring,

```
da_i/dτ = ( −γ_i + i·ω_i )·a_i  +  i·g·( Σ_{j∈N(i)} w_ij·a_j ) / deg_i  −  β·|a_i|²·a_i
```

containing **only** damping (γ = 1/(2Q)), a **reactive** coupling (the factor i — a real coupling would add
to the growth rate and pump the ring instead of locking it) and **amplitude-dependent (saturating) damping**
β|a|²a. There is **no topological charge, no one-way term, no memory and no noise** in any of them. That is
the point: whatever these models produce is generic oscillator physics.

| # | Model | Topology | ω | damping | β |
|---|---|---|---|---|---|
| 1 | **D96 lock lattice** | canonical C₉₆(±1..±6) | D96 spectrum, ω_i = √λ_i | uniform, Q = 100 | 0.05 |
| 2 | **Random lattice** | random 12-regular (LCG seed 20260911) | D96 spectrum | uniform, Q = 100 | 0.05 |
| 3 | **High-Q ring** | canonical ring | D96 spectrum | uniform, Q = 1000 | 0 (linear) |
| 4 | **Weakly nonlinear ring** | canonical ring | D96 spectrum | uniform, Q = 100 | 0.01 |
| 5 | **Strongly nonlinear ring** | canonical ring | D96 spectrum | uniform, Q = 100 | 0.5 |

Reference parameters: γ = 0.005 (Q = 100), coupling g = 1.7 (just above NP_171's predicted g_c = 1.607),
charge amplitude A₀ = 1. Models 1 and 2 are parameter-identical apart from topology, which is what makes
the comparison a topology test.

**Probes** (reported separately, they isolate the second candidate mechanism): P1/P2 are the canonical and
random topologies made **linear** (β = 0) with a deterministic **100× Q contrast**, γ ∈ [0.0005, 0.05]
(log-mean = γ_ref) — realistic resonators do not have identical mode Q's.

### Protocol (NP_170 §6 R4 in spirit)

Charge into the locked configuration (the fundamental k = 1 mode, A₀ = 1), remove the drive, record
E(τ) = Σ_i |a_i|²/2 as a **moving-window envelope** (window 10) so the carrier at ω does not alias the fit.
Fit E = a·e^(−τ/τ₁) (2 parameters, the null) versus E = a·e^(−τ/τ_fast) + b·e^(−τ/τ_slow) (4 parameters),
with the τ's found on a fixed log-spaced grid and the amplitudes solved **linearly** for each pair
(variable projection) — deterministic, no initial guess. BIC/AIC decide, and **the simpler model is always
the null** (NP_170 §7). RK4, dt = 0.05, verified deterministic (bit-identical repeat runs).

### The analytic null, derived before any simulation

For a **coherent** uniform-amplitude charge in a uniform-Q ring the nonlinear term is uniform and the
reactive coupling is a pure frequency shift, so u = A² obeys du/dτ = −2γu − 2βu², with the exact solution

```
E(τ) = u₀·e^(−2γτ) / ( 1 + (β·u₀/γ)·(1 − e^(−2γτ)) ) .
```

Three consequences follow with no simulation at all: **(i)** the late-time rate is **2γ for every β**, so
τ_slow → 1/(2γ) — a slow tail is automatic once any saturation is present; **(ii)** the early decay is
accelerated by βu₀, so the curve *looks* two-time-scale; **(iii)** the shape depends only on the single
dimensionless number **β·u₀/γ** — the topology does not appear in the formula.

---

## Results

### 1. The closed form is correct, and the realisation reproduces it

| Check | Result |
|---|---|
| Closed form versus its own ODE (RK4, du/dτ = −2γu − 2βu²), β = 0 / 0.05 / 0.5 | max relative deviation < 1e−6 |
| Coherent ring simulation versus the closed-form envelope (β = 0.05) | max relative deviation **8.5e−4** |
| D96 lock lattice versus the same envelope | 3.1e−1 |
| Random lattice versus the same envelope | 2.7e−1 |

The coherent case reproduces the formula; the two real lattices deviate by ~30%, and the reason is
structural: their nodes carry **different** natural frequencies (the D96 spectrum), so the charged state is
not a single coherent mode and its nodes dephase. That is a property of the spectrum, not of any barrier, and
it is the first sign that the "locked configuration" is not a protected object in this model.

### 2. The five models

| Model | τ_fast | τ_slow | **R** | ΔBIC (two-exp) | two-exp preferred | E_ret | H2 |
|---|---|---|---|---|---|---|---|
| 1. **D96 lock lattice** | 15.398 | 97.901 | **6.358** | 378.9 | yes | 1.4e−4 | **fail** |
| 2. **Random lattice** | 13.665 | 86.888 | **6.358** | 439.3 | yes | 1.3e−4 | **fail** |
| 3. **High-Q ring** | 979.014 | 1039.210 | 1.061 | 17798.8 | yes * | 2.5e−3 | fail |
| 4. **Weakly nonlinear ring** | 29.683 | 110.311 | 3.716 | 460.3 | yes | 5.8e−4 | fail |
| 5. **Strongly nonlinear ring** | 0.779 | 7.987 | **10.248** | 548.1 | yes | 2.0e−5 | **PASS** |

\* R = 1.061 is the **τ-grid floor** (the smallest ratio the grid can resolve); an R at the floor *is* a
single exponential, and for the linear model that is what the closed form requires. The two-exponential
preference there is a resolution artefact, not retention. The grid floor is reported so that no reader
mistakes 1.06 for a measurement.

**Three results settle the audit:**

1. **The D96 lock lattice and the random lattice give the same R — 6.358, identical to four significant
   digits** (τ_fast 15.4 vs 13.7, τ_slow 97.9 vs 86.9). The AT lattice contributes **nothing** to the
   retention ratio.
2. **The only model that passes H2's R ≥ 10 criterion is the generic strongly nonlinear ring** — no barrier,
   no topological charge, no AT structure. Its two-time-scale behaviour is the saturating-damping law of the
   closed form.
3. **At the reference parameters the AT lattice does not pass H2 at all** (R = 6.36 < 10) — while the
   two-time-scale fit is *preferred* for every model with β > 0, controls included. Both halves of H2's
   discriminating expectation fail.

### 3. What actually controls R

Sweep of β on the canonical lattice (uniform Q):

| β | β·u₀/γ | τ_fast | τ_slow | R | R ≥ 10 |
|---|---|---|---|---|---|
| 0 | 0 | — | — | **1.06** (grid floor) | no |
| 0.005 | 1 | 37.685 | 110.311 | 2.927 | no |
| 0.05 | 10 | 15.398 | 97.901 | 6.358 | no |
| 0.25 | 50 | 0.692 | 9.553 | **13.811** | **PASS** |
| 0.5 | 100 | 0.779 | 7.987 | 10.248 | PASS |

R is a function of **one imported operating parameter** — the saturation-to-damping ratio β·u₀/γ — rising
from ~1 (β = 0, exact single exponential) to > 10 once β·u₀/γ ≳ 50. Beyond that the *measured* R falls back
(10.25 at β = 0.5) because τ_fast ≈ 0.78 drops below the envelope/sampling resolution of the protocol
(window 10, sample every 2): the fitted R becomes a **lower bound** when τ_fast leaves the observation window.
**That limitation applies to NP_170's R4 as specified** — R4 must resolve τ_fast, not only τ_slow.

### 4. The Q-contrast probe: a second mechanism is NOT available

| Probe (linear, β = 0, 100× Q spread) | τ_fast | τ_slow | R | ΔBIC | verdict |
|---|---|---|---|---|---|
| Q-contrast ring | 42.392 | 44.998 | 1.061 | **−10.1** | single exponential preferred |
| Q-contrast random | 57.128 | 60.641 | 1.061 | **−9.6** | single exponential preferred |

A 100× spread in Q produces **no** detectable two-time-scale ring-down: ΔBIC is **negative**, i.e. the
single exponential beats the two-time-scale fit. The mechanism is transparent — the reactive coupling keeps
the energy equipartitioned, so the total energy decays at the energy-weighted **mean** damping (mean
γ ≈ 0.011 → τ ≈ 45) and the slowly damped nodes hold too little of the initial energy to leave a distinct
slow tail. Retention therefore cannot be rescued by appealing to realistic mode-dependent loss either.

---

## Classification

| Item | Classification |
|---|---|
| The closed form E(τ) = u₀e^(−2γτ)/(1 + (βu₀/γ)(1 − e^(−2γτ))); the 2γ late-time rate for every β; τ_slow → 1/(2γ) | **DERIVED** (verified against its ODE to < 1e−6 and against the coherent-ring simulation to 8.5e−4) |
| β = 0 with uniform γ ⇒ single exponential exactly, at any Q | **DERIVED** |
| R is controlled by the single dimensionless ratio β·u₀/γ; topology does not enter | **DERIVED** (closed form) + **CORRESPONDENCE** (measured identically on two topologies) |
| The D96 lattice's retention signature (R = 6.358) being reproduced by a random non-AT lattice (R = 6.358) | **CORRESPONDENCE** — a numerical coincidence of two different systems, i.e. non-discriminating |
| The ring-down of the D96 and random lattices deviating ~30% from the coherent closed form | **CORRESPONDENCE / EMERGENT** — a dephasing consequence of the D96 frequency spread |
| "Two-time-scale retention requires the AT one-way barrier (QG120) / is AT-specific" | **REFUTED** |
| H2's discriminating expectation (only the locked lattice is two-time-scale; controls single-exponential) | **REFUTED** |
| "A realistic Q contrast supplies the retention signature" | **REFUTED** (ΔBIC < 0, R at the grid floor) |
| H2's transferability to a Laplacian ring (NP_170 §9) | **STILL OPEN for hardware, but its signature is now shown to be generic** — the board's own saturation would produce it |
| The R4 protocol's ability to identify the mechanism | **BOUNDARY/limitation** — R must be measured with τ_fast resolved, and scored against a saturation-matched control |

**No canonical AT claim, value, equation or registry entry is changed.** No reclassification of any prior
result (the D_040 ClassificationRegistry is untouched); no new primitive; the energy-storage framing remains
abandoned.

---

## Verdict

1. **No — two-time-scale retention does not require any AT barrier.** The generic strongly nonlinear ring
   reaches **R = 10.248 ≥ 10**, passing H2's pre-registered retention criterion with no barrier, no
   topological charge and no AT structure of any kind. The mechanism is amplitude-dependent (saturating)
   damping: the closed form shows that the late-time rate is 2γ for *every* β while the early decay is
   accelerated by βu₀, so the curve looks two-time-scale as soon as β·u₀/γ ≳ 50.
2. **The AT lattice is not distinguishable by this signature.** D96 and the random lattice give **R = 6.358
   to four significant digits**, and at the reference parameters *neither* passes R ≥ 10. The retention
   signature carries no information about the lock lattice.
3. **A Q contrast is not an alternative explanation either** (ΔBIC < 0; single exponential preferred), so the
   audit's negative is doubly robust: the one mechanism that produces R ≥ 10 is saturation, and it is present
   in every model, AT or not.
4. **Consequence for NP_170 (H2, reason 2 — phase/coherence memory).** NP_171 left R4 retention as *the only*
   remaining discriminating test for the lock lattice. This audit shows R4's signature is **generic**, so a
   positive R4 result on hardware would demonstrate that the board's oscillators saturate — not that the lock
   law, the one-way barrier or the AT structure is at work. To remain informative, R4 must be scored against
   the degeneracy-matched control (T_015) **and** against a **saturation-matched control** (same β·u₀/γ),
   which NP_170 does not currently require, and τ_fast must be resolved by the sampling protocol. The
   AT-120 barrier analogy stays an analogy — and now for a quantified reason.

---

## References

- `NP_NewPhysics/ResearchY-NP_170.md` — H2, §6 R4 ring-down protocol, §7 statistics, §9 boundaries
- `NP_NewPhysics/ResearchY-NP_171.md` — deterministic lock-lattice simulator; g_c = 1.607 used to fix g = 1.7
- `NP_NewPhysics/ResearchY-NP_005.md` — the locking nonlinearity is an imported input
- `T_SpectralBlueprint/ResearchY-T_015.md` — the degeneracy-matched control logic
- QG120 (one-way barrier — the mechanism H2 appeals to), QG313 (values are domain-specific), QG316/QG319
  (protocol statistics)
- Simulator: `AT.Core/Resonance/Kuramoto/RetentionNullSimulator.cs` (reuses the NP_171 lattice builders)
- Test suite: `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_172_Tests.cs`

---

**Status:** COMPLETE (model null audit). Verdict: **DERIVED** closed form; **CORRESPONDENCE** between the AT
lattice and generic rings; **REFUTED** AT-specificity of the retention signature. No canonical AT statement
modified; no reclassification; no new primitive.
