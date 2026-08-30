# ResearchY-NP_003 — Manipulation Lever Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_003 (permanent)
**Title:** Manipulation Lever Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_003.md`
**Depends on:** ResearchY-D_020–D_045 (derived chain), M_001–M_005 (measurement)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_003_Tests.cs`

---

## Purpose

**Does the theory contain a controllable physical lever?** This audit searches the
derived origin chain — Difference → Actualization → tick → count → magnitude → phase →
complex state → identity → reciprocity → pairing → p=3 → N=96 → Spectrum → {v, m_e} →
Physics — for the earliest quantity that can be modified and propagates changes into
observable physics.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **lever** | a quantity that can be varied and whose variation propagates into observable downstream physics |
| **control parameter** | the value assigned to the lever (what an experimenter sets) |
| **manipulation** | the act of changing the lever's value |
| **observable consequence** | a downstream measurable that changes when the lever changes |

---

## 2. Candidate analysis

| Candidate | Status | Variable? | Observable consequence |
|---|---|---|---|
| **Difference** | BOUNDARY (primitive) | **A) fixed** | none — it defines distinguishability |
| **Actualization rate** | primitive process | **A) fixed** | none — time IS the tick count; there is no separate rate parameter |
| **tick structure** (Δθ = 2πk/N) | DERIVED (D_041) | **A) fixed** per mode | phase advance per tick |
| **phase** θ₀ | DERIVED (D_036/D_039) | **B) LOCALLY VARIABLE** | future trajectory, interference, measurement readout |
| **reciprocity** (two-quadrature basis) | EMERGENT (D_037) | **A) fixed** | the measurement basis structure |
| **pairing** (λ_k = λ_{N−k}) | DERIVED (D_021) | **A) fixed** | complex-state origin |
| **N** | DERIVED (96 unique, D_015/D_019) | **A) fixed** | the whole spectrum; changing N breaks uniqueness |
| **spectrum** | DERIVED from N | **A) fixed** | all frequencies |
| **ω₁** | DERIVED (≈ √91·2π/N) | **A) fixed** | minimum excitation |
| **λ₂** | DERIVED | **A) fixed** | second eigenvalue |
| **anchors {v, m_e}** | BOUNDARY (R_001) | **A) fixed** | the unit scale; irreducible inputs |

**Only ONE candidate is locally variable: the phase θ₀.** Everything else is either a
fixed derived structure or a fixed boundary input.

---

## 3. The phase lever

The phase θ₀ of a complex state is:
- **Locally variable (B)** — a measurement event pins it (M_002, phase-pinning), and
  the pinned phase becomes the initial condition of the deterministic future evolution
  (M_003: θ_t = θ₀ + t·Δθ).
- **Set by manipulation** — preparing/measuring a state fixes its relative phase (the
  readout IS the pinned phase).

**Trace:**
```
phase θ₀
 → pinned by measurement event (M_001/M_002, EMERGENT)
 → becomes the future initial condition (M_003, DERIVED)
 → deterministic phase trajectory θ_t = θ₀ + t·Δθ (D_041)
 → interference with other modes (relative phase)
 → measurement outcomes
```

---

## 4. What does changing the phase modify?

| Observable | Modified by phase lever? | Why |
|---|---|---|
| **time behaviour** | **YES** | the pinned θ₀ sets the initial condition of the future trajectory θ_t = θ₀ + t·Δθ |
| **frequency** | NO | Δθ = 2πk/N is fixed per mode (k, N fixed) |
| **measurement** | **YES** | the readout IS the pinned phase; a different θ₀ → a different outcome trajectory |
| **gravity** | NO | no metric coupling in the derived chain |
| **sector structure** | NO | N, pairing, families are fixed |

**The phase is a genuine lever for time behaviour and measurement — not for frequency,
gravity, or sector structure.**

---

## 5. Smallest manipulable object

**The smallest manipulable object is the phase θ₀ of a single complex state — one real
angular parameter (one degree of freedom).** It is set by one actualization event
(one measurement), and its variation propagates into the future phase trajectory and
the interference/measurement readout.

---

## 6. Global vs local variation

- **Global variation (C): NONE.** No quantity in the derived chain is globally
  variable. N=96 is unique (D_015/D_019); the spectrum, ω₁, λ₂, pairing follow
  deterministically; the anchors {v, m_e} are irreducible boundaries (R_001).
- **Local variation (B): the phase.** The only controllable quantity, and it is local
  (per-state, per-mode).
- **Fixed (A): everything else.** Difference, η, actualization rate, tick structure,
  reciprocity, pairing, N, spectrum, ω₁, λ₂, anchors.

---

## Theorem

> **Theorem (NP_003).** The theory contains EXACTLY ONE controllable physical lever:
> the phase θ₀ of a complex state. It is locally variable (set by a measurement event
> via phase-pinning, M_002), and its variation propagates into the future phase
> trajectory (θ_t = θ₀ + t·Δθ, M_003/D_041) and into interference/measurement
> outcomes. The lever modifies time behaviour and measurement, but NOT frequency
> (Δθ = 2πk/N is fixed per mode), NOT gravity (no metric coupling), and NOT sector
> structure (N, pairing, families fixed). All other chain quantities are fixed: the
> primitives {Difference, η} and anchors {v, m_e} are BOUNDARY inputs; N, the
> spectrum, ω₁, λ₂, pairing, and the tick structure are DERIVED and unique. There is
> NO globally variable parameter. The smallest manipulable object is the phase of a
> single state — one angular degree of freedom, set by one actualization event.
> Classification: the phase DOF is DERIVED (D_036/D_039); its manipulability (the
> measurement readout that pins it) is EMERGENT (M_001/M_002); every fixed quantity is
> DERIVED or BOUNDARY. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Enumerate the chain and classify each candidate (Section 2).
> (2) Only the phase is locally variable; it propagates via M_003/D_041 (Section 3).
> (3) Its effects are confined to time behaviour and measurement (Section 4, verified:
> θ_t shifts with θ₀, Δθ unchanged; interference I changes with relative phase; sector
> count 3 invariant). (4) No global lever exists (Section 6). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → LEVER (phase θ₀)
 → measurement pins it (M_002)
 → future trajectory θ_t = θ₀ + t·Δθ (M_003/D_041)
 → interference + measurement outcomes
 → Observable physics
```

---

## 7. Leverage ranking

| Rank | Candidate | Type | Leverage |
|---|---|---|---|
| **1** | **phase θ₀** | B (locally variable) | HIGH — sets time behaviour + measurement |
| 2 | actualization rate | A (fixed) | none (time IS the tick count) |
| 3 | N | A (fixed) | none (unique; not controllable) |
| 4 | anchors {v, m_e} | A (fixed) | none (irreducible boundaries) |
| 5 | spectrum / ω₁ / λ₂ | A (fixed) | none (derived uniquely) |

---

## 8. Counterexamples

| Attempted lever | Why it fails |
|---|---|
| "Change N to change physics" | N=96 is unique (D_015/D_019); no construction selects another N |
| "Change the anchors v/m_e" | they are irreducible BOUNDARY inputs (R_001); changing them breaks the theory |
| "Change the tick rate" | time IS the tick count; there is no separate rate parameter to vary |
| "Change frequency by phase" | Δθ = 2πk/N is fixed per mode; the phase shifts the trajectory, not the rate |

---

## Classification

| Component | Status |
|---|---|
| phase DOF (the lever) | **DERIVED** (D_036/D_039) |
| manipulability (measurement pinning) | **EMERGENT** (M_001/M_002) |
| Difference, η, anchors {v, m_e} | **BOUNDARY** (fixed inputs) |
| N, spectrum, ω₁, λ₂, pairing, tick | **DERIVED** (fixed, unique) |
| actualization rate | **fixed** (time = tick count) |

**The theory has exactly one controllable lever — the phase — and it is local. No
global control parameter exists. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Phase as an experimental control (NP_003 OP1).** Whether the phase lever can be
   exploited as a control parameter in an experiment (e.g., interferometric control of
   the future trajectory) — the theory's only manipulable quantity.

---

## Next Steps

- **Registry note:** AT-P042 (discrete tick) is the theory's structural prediction; the
  phase is its only manipulable (control) parameter — a local, not global, lever.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_003_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_003_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_003_LeverCandidates` | classification of all candidates (only phase variable) | ✅ |
| `Y_NP_003_LocalVariation` | phase is locally variable (θ_t shifts with θ₀) | ✅ |
| `Y_NP_003_GlobalVariation` | no global lever (N, anchors fixed) | ✅ |
| `Y_NP_003_ObservableEffects` | phase changes time+measurement, not frequency/sector | ✅ |
| `Y_NP_003_DependencyTrace` | Difference → Actualization → phase → effects | ✅ |
| `Y_NP_003_Run` | research report | ✅ |

**Conclusion:** The theory contains exactly ONE controllable lever — the phase θ₀ of a
complex state, locally variable via measurement (M_002/M_003) and propagating into
time behaviour and measurement outcomes. No global lever exists. No new primitive;
canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_003"`

---

## References

- ResearchY-D_015/D_019 (N=96 uniqueness), D_021 (pairing), D_036/D_039 (complex
  state / state identity), D_037 (reciprocity), D_041 (tick rate), M_001–M_005
  (measurement event → phase-pinning → feedback → information), R_001 (boundaries),
  NP_001/NP_002 (roadmap).
- AT-QG: QG216 (Born rule), QG228 (information).
