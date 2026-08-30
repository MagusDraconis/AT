# ResearchY-M_010 — Discrete Phase Lattice Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** M — Measurement Origin
**ID:** ResearchY-M_010 (permanent)
**Title:** Discrete Phase Lattice Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `M_Measurement/ResearchY-M_010.md`
**Depends on:** ResearchY-M_008 (predictions), M_009 (discriminator), D_041 (tick
time-parameter)
**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_010_Tests.cs`

---

## Purpose

**Does AT-P042 produce observable effects that continuous QM cannot reproduce?**
M_009 established that AT-P042 (discrete tick phase advance, θ = θ₀ + m·2πk/N) is the
first uniquely-AT measurement prediction. This audit sharpens that claim: it analyzes
the discrete phase lattice's observable consequences — low k, high k, k = 48, finite
phase cycles, recurrence, phase quantization, interference signatures, finite-state
effects — and asks whether continuous QM already predicts the same observables.

---

## 1. Continuous vs discrete phase evolution

| | Continuous QM | AT-P042 (discrete tick) |
|---|---|---|
| evolution | θ(t) = θ₀ + ωt, t ∈ ℝ | θ_m = θ₀ + m·2πk/N, m ∈ ℤ |
| time | continuous parameter | tick COUNT (discrete) |
| reachable phases | continuum [0, 2π) | lattice, |lattice| = N/gcd(N,k) |
| sample points | any t | integer ticks only |

**The matching continuous model:** set ω = 2πk/(N·τ) where τ is the tick duration.
Then at every integer tick t = m·τ, continuous QM gives θ = θ₀ + m·2πk/N — EXACTLY the
AT-P042 lattice value. All observables measured at actualization (tick) times are
identical.

---

## 2. Analysis by mode

| Mode k | lattice size N/gcd(N,k) | recurrence (ticks) | note |
|---|---|---|---|
| low k (k=1) | 96 | 96 | largest lattice; slowest phase advance 2π/96 per tick |
| mid k (k=16) | 6 | 6 | short cycle; π/3 per tick |
| high k (k=47,49,95) | 96 | 96 | same lattice as k=1 (gcd=1) |
| k = 48 | 2 | 2 | TWO phases only: 0 and π — a binary phase flip |

k = 48 is the sharpest case: the phase alternates between two values per tick. In
continuous QM with ω = 2π·48/(N·τ) = π/τ, the phase at ticks is the same alternation
(0, π, 0, π, …). Identical at sample times.

---

## 3. Search for signatures

### Recurrence
Mode k recurs after N/gcd(N,k) ticks:
- AT: m·2πk/N = 2π·integer ⟺ m·k/N ∈ ℤ ⟺ m = N/gcd(N,k).
- Continuous QM: ωt = 2πm with ω = 2πk/(N·τ) ⟺ t = m·Nτ/k ⟺ t = Nτ/gcd(N,k).
- **Identical recurrence time** Nτ/gcd(N,k). No discriminating signature.

### Phase quantization
AT reachable phases are the lattice {θ₀ + m·2πk/N}. Continuous QM reachable phases at
tick times are the SAME lattice (since ωτ = 2πk/N). The continuum of continuous QM is
never observed because measurements happen at ticks. **No discriminating signature.**

### Interference signatures
Interference of two modes k₁, k₂: |ψ₁+ψ₂|² = ρ₁+ρ₂+2√(ρ₁ρ₂)·cos(θ₁−θ₂). Relative
phase advances 2π(k₁−k₂)/N per tick in BOTH theories (continuous QM: ω₁−ω₂ =
2π(k₁−k₂)/(N·τ)). At every tick the interference pattern is identical. **No
discriminating signature.**

### Finite-state effects
The lattice is finite (≤ 96 states), but at sample times continuous QM visits the
same finite set. Finite-state behavior (e.g., bounded orbit) is reproduced exactly by
the matching continuous model at ticks. **No discriminating signature.**

---

## 4. Does continuous QM already predict the same observables?

**YES — at all tick-sampled times.** For any observable evaluated at an actualization
tick, continuous QM with ω = 2πk/(N·τ) predicts the identical value:

| Observable | Continuous QM | AT-P042 | Verdict |
|---|---|---|---|
| phase at tick m | θ₀ + m·2πk/N | θ₀ + m·2πk/N | identical |
| recurrence period | Nτ/gcd(N,k) | N/gcd(N,k) ticks | identical |
| interference at tick m | cos[2π(k₁−k₂)m/N + Δθ₀] | same | identical |
| phase distribution (finite orbit) | lattice at samples | lattice | identical |

**The ONLY difference is the sub-tick phase:** continuous QM has intermediate phases
θ(t) for t between ticks; AT has no phase between ticks (time IS the tick count).
Discriminating requires a time resolution finer than the actualization tick — a clock
inside the tick. Since the tick is the fundamental clock of the theory, this is
**in-principle-only**: the discriminator asks to measure time that the theory does not
contain.

---

## 5. Observable discriminator table

| Observable | QM reproduces? | Distinguishes AT? | Access |
|---|---|---|---|
| phase at integer ticks | YES (ω = 2πk/Nτ) | NO | any measurement |
| recurrence period | YES | NO | time-resolved measurement |
| interference pattern | YES | NO | two-mode experiment |
| phase quantization (orbit size) | YES | NO | repeated sampling |
| sub-tick phase value | NO (only AT has none) | YES — IN PRINCIPLE | requires sub-tick clock (unavailable) |
| tick-count quantization of time | NO | YES — STRUCTURAL | the time parameter itself |

---

## 6. Determination

| Option | Verdict |
|---|---|
| A) identical to QM at observables | **YES — all tick-sampled observables** |
| B) equivalent interpretation | **YES for experiments** — the matching continuous model reproduces every measurable result |
| C) genuinely new prediction | **YES structurally, NO observably** — the discrete time-parameter is new; its observable content at ticks is QM-reproducible |

**AT-P042 is structurally new but observationally equivalent to continuous QM at all
tick-sampled observables.** The discrete phase lattice has no experimentally
distinguishable consequence at accessible (tick) scales: continuous QM with the
matching rate reproduces phase, recurrence, interference, and finite-state orbits
exactly. The only discriminator — the sub-tick phase — is in-principle-only because
the tick is the theory's fundamental clock.

---

## Theorem

> **Theorem (M_010).** AT-P042's discrete phase evolution θ_m = θ₀ + m·2πk/N produces
> NO observable effect that continuous QM cannot reproduce at any tick-sampled time.
> Proof: with the matching rate ω = 2πk/(N·τ), continuous QM gives θ(m·τ) = θ₀ +
> m·2πk/N — the identical lattice value at every tick. (1) Recurrence: mode k recurs
> after N/gcd(N,k) ticks in both (m·k/N ∈ ℤ ⟺ m = N/gcd(N,k); continuous QM recurs at
> t = Nτ/gcd(N,k)). (2) Interference: the relative phase advance 2π(k₁−k₂)/N per tick
> is identical in both. (3) Phase quantization and finite-state orbits: at tick times
> continuous QM visits exactly the same finite lattice. (4) The ONLY difference is the
> sub-tick phase — continuous QM has intermediate phases, AT has none — and
> discriminating requires a clock finer than the actualization tick, which the theory
> does not contain (in-principle-only). Therefore AT-P042 is structurally new
> (PREDICTION) but observably CORRESPONDENCE: no experiment at tick scale can
> distinguish it from continuous QM. Nothing is FALSIFIED. No new primitive; canonical
> AT unchanged.
>
> *Proof sketch.* (1) Match ω = 2πk/(N·τ) and compare at ticks (Section 1, verified).
> (2) Recurrence identical (Section 3). (3) Interference identical (Section 3). (4)
> Sub-tick phase is the sole in-principle discriminator (Sections 4–5). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → MEASUREMENT (M_001)
 → FEEDBACK (M_003, Δθ = 2πk/N per tick, D_041)
 → PREDICTION AT-P042 (M_008)
 → DISCRIMINATOR (M_009: AT-P042 = genuinely new, structurally)
 → PHASE LATTICE AUDIT (M_010)
    → all tick-sampled observables: CORRESPONDENCE (QM reproduces)
    → sub-tick phase: in-principle discriminator only
    → discrete time-parameter: structurally new (PREDICTION)
```

---

## 7. Falsification Path

1. **AT-P042** is falsified only by a sub-tick phase measurement finding an
   intermediate (non-lattice) phase value θ ∉ {θ₀ + m·2πk/N} at a time between ticks.
   This requires a clock finer than the actualization tick. It is in-principle
   testable, currently inaccessible.
2. **No tick-sampled experiment** (phase at ticks, recurrence, interference, orbit
   size) can falsify AT-P042 — continuous QM reproduces all of them.

---

## 8. Prediction Strength Assessment

| Dimension | Assessment |
|---|---|
| mathematical testability | HIGH — lattice cardinality N/gcd(N,k), recurrence N/gcd(N,k) deterministic |
| experimental testability | NONE at tick scale — all sampled observables QM-reproducible |
| in-principle testability | sub-tick phase (requires a finer clock than the theory's own) |
| overall strength | STRUCTURAL — the discrete time-parameter is genuinely new, but its observable content is equivalent to continuous QM |

**AT-P042's prediction strength is structural, not experimental.** It is the first
uniquely-AT measurement prediction, but M_010 shows its observable content coincides
with continuous QM at every accessible time. The discrete lattice is a real property
of the theory's time parameter, not a measurable deviation from QM.

---

## Classification

| Component | Status |
|---|---|
| phase at tick times | **CORRESPONDENCE** (QM reproduces exactly) |
| recurrence period | **CORRESPONDENCE** (identical) |
| interference signature | **CORRESPONDENCE** (identical) |
| finite-state orbit | **CORRESPONDENCE** (identical at samples) |
| discrete time-parameter (AT-P042) | **PREDICTION** (structural — in-principle only) |
| sub-tick phase | in-principle discriminator (no FALSIFICATION at tick scale) |

**AT-P042 is structurally new but observably equivalent to continuous QM at every
tick-sampled observable. No prediction is FALSIFIED. No new primitive; canonical AT
unchanged.**

---

## Open Problems

1. **Sub-tick access (M_010 OP1).** Whether any physical observable can probe the
   interval between actualization ticks — the necessary condition for an experimental
   discriminator of AT-P042.

---

## Next Steps

- **Registry note:** AT-P042's strength is structural (in-principle-only discriminator);
  this is the honest experimental status of the theory's first unique measurement
  prediction.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_010_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_M_010_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_M_010_ContinuousPhase` | continuous QM matches AT at every tick | ✅ |
| `Y_M_010_DiscretePhase` | lattice cardinality N/gcd(N,k) | ✅ |
| `Y_M_010_InterferencePattern` | two-mode interference identical at ticks | ✅ |
| `Y_M_010_Recurrence` | recurrence period N/gcd(N,k) identical | ✅ |
| `Y_M_010_PredictionUniqueness` | sub-tick phase is the only in-principle discriminator | ✅ |
| `Y_M_010_Run` | research report | ✅ |

**Conclusion:** AT-P042's discrete phase lattice produces no observable effect beyond
continuous QM at any tick-sampled time — phase, recurrence, interference, and
finite-state orbits are all reproduced exactly by the matching continuous model. The
discrete time-parameter remains a structural PREDICTION; its experimental
discriminator is sub-tick, in-principle-only. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_010"`

---

## References

- ResearchY-M_008 (predictions AT-P042/AT-P043), M_009 (discriminator: AT-P042 = the
  uniquely-AT prediction), M_003 (feedback Δθ = 2πk/N), D_041 (tick time-parameter).
- AT-QG: QG216 (Born rule), QG228 (information).
- V2.0 Prediction Registry (AT-P001…AT-P041); AT-P042 (M_008), refined by M_009
  (PREDICTION), strength assessed by M_010 (structural, in-principle discriminator).
