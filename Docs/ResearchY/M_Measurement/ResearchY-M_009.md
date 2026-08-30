# ResearchY-M_009 — Measurement Prediction Discriminator Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** M — Measurement Origin
**ID:** ResearchY-M_009 (permanent)
**Title:** Measurement Prediction Discriminator Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `M_Measurement/ResearchY-M_009.md`
**Depends on:** ResearchY-M_001–M_005, M_008 (the measurement program)
**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_009_Tests.cs`

---

## Purpose

**Do AT-P042 and AT-P043 predict anything beyond standard QM?** M_008 produced two
candidate AT-specific predictions: AT-P042 (post-measurement phase advances per
actualization tick, Δθ = 2πk/N) and AT-P043 (one event reveals at most log₂(95) ≈ 6.57
bits). This audit is a DISCRIMINATOR: it compares each prediction against standard QM,
Copenhagen, decoherence, and information-theoretic QM, and determines which — if any —
is genuinely new (C), which is an equivalent interpretation (B), and which is already
implied (A).

---

## 1. Compare each prediction against QM frameworks

### AT-P042 — discrete tick phase advance (Δθ = 2πk/N per tick)

| Framework | Time parameter | Reachable phase set |
|---|---|---|
| **Standard QM** | continuous (t ∈ ℝ) | continuum of phases |
| **Copenhagen** | continuous | continuum |
| **Decoherence** | continuous | continuum |
| **Info-theoretic QM** | continuous | continuum |
| **AT (D_041/M_003)** | discrete tick COUNT (t ∈ ℤ) | LATTICE {θ₀ + m·2πk/N mod 2π} |

**Discriminator test — continuous vs discrete:** in QM the phase is reachable at ANY
real time; the reachable phase set is the continuum [0, 2π). In AT the phase advances
only per actualization tick, so the reachable phase set is the finite lattice of
cardinality N/gcd(N,k) (e.g. k=16 → 6 distinct phases; k=1 → 96; k=48 → 2). A phase
measurement at a sub-tick time would, in QM, find an intermediate phase; in AT the
phase is pinned to a lattice point until the next tick.

**Observable difference:** the SET of reachable phase values — continuum (QM) vs a
finite lattice (AT). This is a structural, mathematically testable difference.

### AT-P043 — information bound (log₂(95) per event)

| Framework | Per-event info bound |
|---|---|
| **Standard QM** | ≤ log₂(d) for a d-outcome measurement (Shannon bound) |
| **Copenhagen** | same |
| **Decoherence** | same |
| **Info-theoretic QM** | same — the max entropy of a d-outcome distribution is log₂(d) |
| **AT (M_004/D_039)** | log₂(95) with d = 95 (the derived state-space size) |

**Discriminator test — can one event reveal > log₂(95)?** In ANY probabilistic theory
(AT included) a measurement with d = 95 possible outcomes has max Shannon information
log₂(95) (achieved by the uniform distribution). Standard QM imposes exactly the same
limit: the entropy of a 95-outcome distribution is ≤ log₂(95). So AT-P043's bound is
NOT a QM discriminator — every 95-state theory obeys it.

---

## 2. Classification per prediction

| Prediction | vs QM | Class | Evidence |
|---|---|---|---|
| **AT-P042** discrete tick | NOT implied | **C) genuinely new** | QM time is continuous; AT time is a derived discrete count with a finite phase lattice |
| **AT-P043** log₂(95) bound | ALREADY implied | **A) already implied** | the log₂(d) per-event bound is the standard d-outcome entropy bound |

**AT-P042 is C — genuinely new.** The discrete time-parameter (t is a count of
actualization ticks, Δθ = 2πk/N) is absent from standard QM wording, where time is a
continuous parameter.

**AT-P043 is A — already implied by QM.** Its bound is the standard entropy bound on a
d-outcome distribution (Shannon/Holevo-type); QM imposes the same limit. The only
AT-specific content is the VALUE d = 95 (the derived state-space size, D_039), not a
new bound structure.

---

## 3. For AT-P042: continuous phase evolution vs discrete tick

| Observable | Continuous (QM) | Discrete tick (AT) |
|---|---|---|
| reachable phase set | continuum [0, 2π) | lattice, |lattice| = N/gcd(N,k) |
| phase at t = m + ½ tick | intermediate phase | pinned to lattice point |
| time parameter | t ∈ ℝ | tick count t ∈ ℤ |
| step | ω free | Δθ = 2πk/N (DERIVED, D_041) |

**The observable difference is the reachable phase set: a continuum (QM) vs a finite
lattice (AT).** This is a structural difference that is mathematically testable (the
lattice cardinality N/gcd(N,k) is deterministic) and in-principle experimentally
accessible if sub-tick phase resolution were possible. The tick scale itself is not
independently calibrated, so the experimental discriminator is currently in-principle
only.

---

## 4. For AT-P043: can a single event reveal > log₂(95)?

**No — and QM imposes the SAME limit.** The maximum Shannon information of a d-outcome
measurement is log₂(d); for d = 95 that is log₂(95) ≈ 6.57 bits. This holds in every
probabilistic theory (AT, QM, any hidden-variable theory). QM does not relax it:
measuring which of 95 distinguishable states is realized can never convey more than
log₂(95) bits. AT-P043 therefore does NOT discriminate AT from QM — it is a
consistency bound that any 95-state theory satisfies.

---

## 5. Prediction table

| Prediction | QM equivalent? | Unique? | Experimentally testable? |
|---|---|---|---|
| AT-P042 discrete tick | NO (continuous in QM) | **YES** — finite phase lattice absent from QM | in-principle (sub-tick phase resolution) |
| AT-P043 log₂(95) bound | YES (standard d-outcome bound) | NO — only d=95 value is AT-derived | not a discriminator (QM imposes same) |

---

## 6. First uniquely AT prediction

**AT-P042** — the post-measurement discrete tick phase advance (Δθ = 2πk/N) is the
FIRST prediction of the V2.2 measurement program that is absent from standard QM
wording. AT-P043 is downgraded: its bound is QM-standard.

---

## 7. Registry refinement

| ID | M_008 status | M_009 status | Refinement |
|---|---|---|---|
| **AT-P042** | PREDICTION | **PREDICTION** (refined) | discrete tick time-parameter; phase lattice cardinality N/gcd(N,k) — mathematically testable |
| **AT-P043** | PREDICTION | **CORRESPONDENCE** (downgraded) | standard d-outcome entropy bound; AT content = derived value d=95 |

---

## Theorem

> **Theorem (M_009).** Of the two M_008 measurement predictions, EXACTLY ONE is
> genuinely new beyond standard QM. (1) AT-P042 is C — genuinely new: standard QM has a
> continuous time parameter and a continuum of reachable phases, whereas AT derives a
> discrete time-parameter (the actualization tick count) with a FINITE phase lattice
> {θ₀ + m·2πk/N}, of cardinality N/gcd(N,k) ≤ 96 (e.g. k=16 → 6; k=1 → 96; k=48 → 2).
> The lattice is mathematically testable; experimental discrimination requires sub-tick
> phase resolution (in-principle only, tick scale uncalibrated). (2) AT-P043 is A —
> already implied by QM: the per-event information bound log₂(d) is the standard
> d-outcome Shannon entropy bound, which QM imposes exactly as AT does; AT-P043's only
> AT-specific content is the derived state-space value d = 95 (D_039), not a new bound.
> Therefore the FIRST uniquely-AT measurement prediction is AT-P042. Registry
> refinement: AT-P042 remains PREDICTION; AT-P043 is DOWNGRADED to CORRESPONDENCE.
> Falsification: AT-P042 is falsified by a continuous (non-lattice) phase value at
> sub-tick resolution; AT-P043 is NOT a QM discriminator (no single-event experiment
> can distinguish AT from QM on this bound). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Compare each prediction against standard QM / Copenhagen /
> decoherence / information-theoretic QM (Section 1, verified). (2) The reachable phase
> set differs: continuum (QM) vs lattice (AT) — AT-P042 is C (Section 2–3). (3) The
> log₂(d) per-event bound is the standard d-outcome entropy bound, identical in QM —
> AT-P043 is A (Section 2–4). (4) AT-P042 is therefore the first uniquely-AT prediction
> (Section 6); AT-P043 is downgraded (Section 7). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → MEASUREMENT (M_001)
 → PREDICTIONS (M_008)
    → AT-P042 (discrete tick, Δθ = 2πk/N)
    → AT-P043 (info bound log₂ 95)
 → DISCRIMINATOR (M_009)
    → AT-P042: C — genuinely new (lattice vs continuum)
    → AT-P043: A — already implied (standard d-outcome bound)
 → FIRST UNIQUELY AT PREDICTION = AT-P042
```

---

## 8. Falsification Path

1. **AT-P042** — falsified if a phase value NOT on the lattice {θ₀ + m·2πk/N} is
   observed (i.e., a continuous, intermediate phase at sub-tick resolution). Also
   falsified if the reachable phase set is a continuum rather than a finite set of
   N/gcd(N,k) values.
2. **AT-P043** — NOT a QM discriminator: observing > log₂(95) bits in one event would
   falsify ANY 95-state theory (including QM), so it cannot distinguish AT. Its value
   is a consistency bound, not a uniqueness test.

---

## 9. Discriminator Report

| Prediction | Framework match | Unique to AT? | Experimental access | Verdict |
|---|---|---|---|---|
| AT-P042 | continuous QM contradicted | **YES** | in-principle (sub-tick) | **C — new** |
| AT-P043 | QM identical | NO (d=95 value only) | non-discriminating | **A — implied** |

**The V2.2 measurement program yields exactly ONE uniquely-AT prediction — AT-P042,
the discrete tick time-parameter. AT-P043 is a QM-standard bound whose only AT content
is the derived value d = 95.**

---

## Classification

| Component | Status |
|---|---|
| AT-P042 discrete tick time-parameter | **PREDICTION** (C — genuinely new) |
| AT-P043 log₂(95) per-event bound | **CORRESPONDENCE** (A — already implied) |
| phase lattice cardinality N/gcd(N,k) | **DERIVED** (from D_041 spectral rate) |
| d = 95 state-space value | **DERIVED** (from D_039) |

**The discriminator keeps AT-P042 and downgrades AT-P043. One uniquely-AT measurement
prediction survives. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Sub-tick resolution (M_009 OP1).** Whether the discrete phase lattice of AT-P042
   can be experimentally resolved — the tick scale is not independently calibrated, so
   the discriminator is currently in-principle only.

---

## Next Steps

- **Registry update:** reflect AT-P043's downgrade in the V2.0 prediction registry
  (AT-P001…AT-P041 extended by AT-P042 PREDICTION, AT-P043 CORRESPONDENCE).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_009_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_M_009_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_M_009_PhaseDiscriminator` | lattice vs continuum (N/gcd(N,k) reachable phases) | ✅ |
| `Y_M_009_InformationLimit` | log₂(d) d-outcome bound (standard, d=95) | ✅ |
| `Y_M_009_QMComparison` | QM imposes the same info bound | ✅ |
| `Y_M_009_PredictionUniqueness` | AT-P042 unique; AT-P043 not | ✅ |
| `Y_M_009_FalsificationPath` | falsification paths for both | ✅ |
| `Y_M_009_Run` | research report | ✅ |

**Conclusion:** AT-P042 (discrete tick, Δθ = 2πk/N) is the FIRST uniquely-AT
measurement prediction (C — genuinely new: finite phase lattice vs QM continuum);
AT-P043 (log₂ 95) is A — already implied by QM (the standard d-outcome entropy bound).
No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_009"`

---

## References

- ResearchY-M_003 (feedback: Δθ = 2πk/N), M_004 (information log₂ 95), M_005
  (conservation), M_008 (predictions AT-P042/AT-P043), D_039 (state space 95), D_041
  (tick time-parameter).
- AT-QG: QG216 (Born rule), QG228 (information).
- V2.0 Prediction Registry (AT-P001…AT-P041); M_008 added AT-P042/AT-P043; M_009
  refines them (AT-P042 PREDICTION, AT-P043 CORRESPONDENCE).
