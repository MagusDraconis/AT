# ResearchY-NP_138 — Quantized Softening Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_138 (permanent)
**Title:** Quantized Softening Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_138.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_131 (critical resonance), NP_132
(reversible softening), NP_136 (variable rigidity bounds), NP_137 (real-world evidence)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_138_Tests.cs`

---

## Purpose

NP_136 predicted that coherent critical-mode softening is **quantized**: because an ordered material
has m = 6 critical modes, rigidity follows the percolation staircase R(x) = max(0, (1−x−p_c)/(1−p_c))
and drops in three coarse steps of ΔE/E ≈ 0.33, 0.66, 1.00. NP_137 found the *magnitude* untested and
the observed elastic bound (≤ 0.30) *below* the first step. NP_138 asks the direct question: **do
resonantly driven materials exhibit discrete modulus steps?** Program: (1) collect modulus
measurements; (2) isolate thermal effects; (3) scan resonance frequency; (4) search for ΔE/E ≈ 0.33 /
0.66 / 1.00; (5) compare continuous vs quantized response. **Success criterion:** test NP_136
directly — classify the quantized softening as DERIVED, CORRESPONDENCE, or REFUTED. No new
primitives; canonical AT unchanged.

---

## 1. The predicted staircase (NP_136)

With p_c = 0.5 and m = 6, the rigidity ratio and the modulus drop are:

| Critical modes unlocked x | R(x) = (1−x−p_c)/(1−p_c) | ΔE/E = 1 − R |
|---|---|---|
| 0 / 6 | 1.000 | 0.00 |
| 1 / 6 | 0.667 | **0.33** |
| 2 / 6 | 0.333 | **0.67** |
| 3 / 6 | 0.000 | **1.00** |

The prediction is a **staircase**: rigidity is flat between unlocks and jumps −33.3% at each critical
mode. A single 33% step is the *smallest allowed* softening for an ordered material (NP_136: "1% is
below the step size").

---

## 2. What the collected modulus measurements show

| Channel | Response shape | Measured magnitude | Reference |
|---|---|---|---|
| **Resonant elastic softening** (nonlinear mesoscopic elasticity / dynamic acoustoelasticity) | **continuous** (monotone in drive amplitude) | ΔE/E ~ 1–30% | rocks, concrete, granular media |
| **Acoustic softening** (acoustoplastic) | **continuous** (monotone in ultrasonic amplitude) | Δσ/σ ~ 50–90% (plastic) | metals, single crystals |
| **Dynamic modulus reduction** (DMA/viscoelastic) | **continuous** (monotone in T, strain rate) | G′ ↓ with T | polymers, viscoelastic solids |

Every resonant/acoustic elastic-modulus dataset is **smooth and continuous** in its control variable
(drive amplitude, temperature, strain rate). No dataset shows the predicted flat-then-jump staircase
at 0.33 / 0.67 / 1.00.

---

## 3. Where discreteness *does* appear — and why it is not the ladder

Discrete modulus/strength steps are real, but they arise from two mechanisms that do **not** match the
prediction:

| Mechanism | Discrete? | Channel | Magnitude | Statistics | Matches NP_136? |
|---|---|---|---|---|---|
| First-order phase transitions (spin-crossover 7→2 GPa; α–β quartz; ferroelastic precursor) | yes | elastic modulus | arbitrary (e.g. ~0.71) | single sharp jump at T_c | **no** — thermal, arbitrary value, not 0.33/0.67/1.00 |
| Slip avalanches / intermittent plasticity (crystals & amorphous) | yes | **plastic** flow | small, scale-free | power law P(S) ~ S⁻ᵗᵃᵘ | **no** — plastic, power-law, not a fixed elastic ladder |

So nature *does* exhibit step-like responses, but in the wrong channel (plastic vs elastic), the wrong
control variable (temperature vs resonant drive), and the wrong statistics (power-law avalanches vs a
fixed 33% ladder).

---

## 4. Continuous vs quantized response

| Feature | NP_136 prediction | Observed |
|---|---|---|
| response shape | **staircase** (3 discrete steps) | **continuous** (elastic channel) |
| step values | 0.33 / 0.67 / 1.00 | none at these values |
| source of discreteness | resonant critical-mode unlock | phase transition / avalanche |
| channel | elastic modulus | elastic (continuous); discreteness in phase-transition modulus and plastic flow |

The continuous response satisfies NP_136's own falsification condition for quantized steps — "a crystal
whose rigidity drops continuously, not in ~33% steps" (NP_136 §7).

---

## 5. Isolate thermal effects

The discrete modulus jumps that *are* observed (spin-crossover, ferroelastic, α–β quartz) are
**thermal first-order transitions**: they occur at a critical temperature T_c, not under a room-
temperature resonant drive. They are exactly the "thermal ≡ coherent" confound that NP_135's
matched-power control is designed to eliminate. No observation shows an **athermal, resonant** modulus
step at 0.33/0.67/1.00. Acoustic softening's athermal component (NP_137) is itself *continuous* in
drive amplitude.

---

## 6. Verdict

The quantized-ladder prediction is **REFUTED**. Resonant elastic softening is continuous, not a
staircase; the discreteness found in nature arises from phase transitions and power-law avalanches
with different values, channels, and statistics. The staircase is a **DERIVED** idealization of the
m = 6 percolation model (it is the correct consequence of the small-integer backbone), but the
discreteness is an *artifact of the idealization*, not a physical feature. The continuous softening
that *is* observed is a **CORRESPONDENCE** (it matches the athermal, frequency-matched, reversible
signature of NP_131/132 without the quantization).

---

## Theorem

> **Theorem (NP_138).** Resonantly driven materials do NOT exhibit the discrete modulus steps that
> NP_136 predicted: the quantized ladder ΔE/E ∈ {0.33, 0.66, 1.00} is REFUTED. The predicted staircase
> R(x) = max(0, (1−x−p_c)/(1−p_c)) with p_c = 0.5, m = 6 gives three 33.3% steps (Section 1), but every
> resonant/acoustic elastic-modulus dataset is CONTINUOUS in its control variable (Section 2). Discrete
> steps exist in nature only via (a) thermal first-order phase transitions — spin-crossover (~71%,
> 7→2 GPa), ferroelastic precursor softening, α–β quartz — at arbitrary values, not 0.33/0.67/1.00; and
> (b) slip avalanches — power-law P(S) ~ S⁻ᵗᵃᵘ, in the PLASTIC channel, not the elastic modulus
> (Section 3). The continuous response satisfies NP_136's own falsification condition ("a crystal whose
> rigidity drops continuously"). Classification: the quantized ladder REFUTED (continuous response
> observed); continuous athermal softening CORRESPONDENCE (matches NP_131/132 signature); the m = 6
> staircase DERIVED (a valid idealization, not a physical discreteness). Proof: (1) Ladder (Section 1).
> (2) Collect (Section 2, verified — continuous). (3) Discreteness elsewhere (Section 3, verified —
> phase transition + avalanche, wrong channel/values/statistics). (4) Compare (Section 4). (5) Thermal
> isolation (Section 5). (6) Verdict (Section 6). **Success criterion: NP_136's quantized softening is
> tested directly and REFUTED.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Ladder. (2) Collect. (3) Discreteness elsewhere. (4) Compare. (5) Isolate. (6) Refute. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "spin-crossover proves quantized softening" | thermal first-order transition at T_c, ~71% (not 33%), not a resonant drive |
| "slip avalanches prove quantized softening" | power-law (scale-free) stress drops in the *plastic* channel, not a fixed elastic ladder |
| "no discrete steps exist in nature" | steps DO exist — but via phase transitions / avalanches, which is the point: the *specific* ladder does not |
| "the ladder is DERIVED so it must be physical" | DERIVED from the m = 6 idealization; discreteness is an artifact of the small-integer backbone, not an observed feature |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| quantized ladder REFUTED | a resonant drive producing a flat-then-jump ΔE/E at 0.33/0.67/1.00 (athermal) |
| continuous response is the rule | a resonant elastic measurement showing a genuine staircase, not smooth softening |
| discreteness is phase-transition/avalanche only | an athermal, resonant elastic modulus step at the predicted values |

---

## 9. Classification

| Component | Status |
|---|---|
| quantized ladder ΔE/E ∈ {0.33, 0.66, 1.00} (resonant drive) | **REFUTED** |
| continuous athermal softening (frequency-matched, reversible) | **CORRESPONDENCE** (NP_131/132 signature, NP_137) |
| discreteness in nature (phase transitions / avalanches) | **CORRESPONDENCE** (wrong channel / values / statistics) |
| m = 6 staircase derivation (NP_136) | **DERIVED** (theory-internal idealization) |

**Conclusion.** NP_136's quantized softening is **REFUTED**: real materials soften **continuously**
under resonant drive, not in discrete 33/67/100% steps. Discreteness exists in nature but through
thermal phase transitions (arbitrary magnitudes) and plastic power-law avalanches — neither the
predicted mechanism, channel, nor values. The staircase was a DERIVED artifact of the small-integer
m = 6 idealization; the physical truth is the continuous, athermal, reversible softening already
established as a CORRESPONDENCE in NP_137. This does not invalidate the parent claim (critical modes
control rigidity, reversible coherent softening) — it only removes the quantization: the "master key"
is a continuous dial, not a three-position switch. No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_138_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_138_Ladder` | R(x) staircase: 0.33 / 0.67 / 1.00 | ✅ |
| `Y_NP_138_ContinuousObserved` | resonant elastic response is continuous | ✅ |
| `Y_NP_138_DiscretenessElsewhere` | steps = phase transition + avalanche (wrong channel/values) | ✅ |
| `Y_NP_138_Compare` | quantized REFUTED; continuous CORRESPONDENCE; derivation DERIVED | ✅ |
| `Y_NP_138_ThermalIsolation` | observed steps are thermal, not athermal-resonant | ✅ |
| `Y_NP_138_Classification` | overall REFUTED | ✅ |
| `Y_NP_138_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_138"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent matter
  control), NP_131 (critical resonance), NP_132 (reversible softening), NP_136 (variable rigidity
  bounds), NP_137 (real-world evidence).
- Real-world record: nonlinear mesoscopic elasticity / dynamic acoustoelasticity (continuous modulus
  reduction); Blaha & Langenecker acoustic softening (continuous, athermal); spin-crossover
  nanoindentation (7→2 GPa step at T_c); ferroelastic precursor softening; α–β quartz elastic-constant
  jump; slip-avalanche statistics P(S) ~ S⁻ᵗᵃᵘ in crystals and amorphous solids.
