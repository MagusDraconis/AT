# ResearchY-M_001 — Measurement Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** M — Measurement Origin
**ID:** ResearchY-M_001 (permanent)
**Title:** Measurement Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `M_Measurement/ResearchY-M_001.md`
**Depends on:** AT-QG QG73 (collapse), QG74 (measurement), ResearchY-D_034
(reciprocity), D_036 (complex-state-origin), D_038 (state-identity), D_039
(state-identity-origin)
**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_001_Tests.cs`

---

## Purpose

**What is a measurement event?** This is the first project of the V2.2 program (NP_002
ranked measurement origin highest). It determines whether measurement is an
actualization event applied to distinguishable states — completing the QM measurement
chain (QG73 collapse + QG74 measurement + D_037 basis + D_039 identity).

## Accepted (from QG73/QG74, D_034–D_039)

- Difference = distinguishability; state identity = the primitive applied (D_039).
- The complex state carries two real DOFs (magnitude, phase) — D_036.
- Observability = complete reconstruction via the {cos, sin} two-quadrature basis
  (D_037): z = a + ib exact; a alone ambiguous.
- The Born rule Σ|ψ|² = 1 is EXACT by construction (QG216).
- QG74 established the unitary (ℂ-linear) general measurement basis; QG73 left the
  collapse binary open.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **measurement** | an actualization event that reads the two-DOF complex state of a distinguishable mode |
| **observation** | the state selection realized as a count/readout event |
| **actualization event** | one tick of Actualization — a count realization (QG216/QG222) |
| **state identity** | each mode is a distinct point of the state space (Difference applied, D_039) |

---

## 2. Unobserved state vs observed state

| | Unobserved state | Observed state |
|---|---|---|
| complex amplitude | ψ = |ψ|·e^{iθ} (both DOFs latent) | read (both quadratures extracted) |
| identity | present (the state is distinct) | ACTUALIZED (a specific outcome realized) |
| probability | |ψ|² = ρ (Born rule, exact) | one outcome selected with weight ρ |
| interference | present (phase-dependent) | collapsed to one outcome |
| Difference | latent (distinguishability potential) | becoming ACTUAL (a specific state realized) |

**The unobserved state carries full identity and interference; the observed state has
its identity ACTUALIZED — one outcome selected with the Born weight.**

---

## 3. What changes during measurement?

**The state's identity transitions from potential to actual.** Before: the complex
amplitude carries both DOFs and all interference; the state is distinguishable but not
selected. After: a count event reads one quadrature pair — one outcome is realized with
probability |ψ|² = ρ. The change is the ACTUALIZATION of a specific distinguishable
state (a Difference becoming actual, D_039).

---

## 4. Is measurement A/B/C/D?

| Option | Verdict |
|---|---|
| A) state selection | **YES** — a measurement selects one state from the distinguishable set |
| B) distinguishability becoming actual | **YES** — the selection IS the primitive Difference becoming actual |
| C) collapse | PARTIAL — "collapse" (QG73) is the binary reading of the same event; the event is the actualization |
| D) none | NO |

**Measurement = state selection (A) realized as distinguishability-becoming-actual (B).**
Collapse (C) is the QG73 label for the same actualization event — not a separate
mechanism.

---

## 5. Remove measurement — what survives?

| Removed | Survives | Gone |
|---|---|---|
| measurement (no readout events) | **state identity** (states remain distinct), **observability** (states distinguishable), **probability** (Born rule exact), **interference** (amplitudes remain) | the ACTUALIZATION of a specific outcome (no selection) |

Removing measurement does not remove the state structure — it removes the realization
of a specific outcome. The complex state, its identity, its probability weights, and
its interference all survive.

---

## 6. Trace: Difference → distinguishability → identity → observability → measurement

```
Difference (primitive)
 → distinguishability (the primitive's content)          [DERIVED — D_039]
 → state identity (primitive applied to the state space) [DERIVED — D_039]
 → observability (complete reconstruction, two-quadrature basis) [DERIVED — D_037/D_038]
 → measurement (an actualization event reads the state)  [EMERGENT — the readout]
 → collapse (the binary reading of the event)            [EMERGENT — QG73 resolved]
```

---

## 7. Minimal condition for a measurement event

**A measurement event requires exactly: an actualization event that reads BOTH
quadratures of one distinguishable complex mode.** The read is the two-DOF extraction
(a = Re, b = Im) — the reconstruction basis (D_037); the distinguishable mode is the
state identity (D_039); the event is an actualization tick (QG216). No further
ingredient is needed: probability (Born rule) and identity (Difference) are already
present in the unmeasured state.

---

## Theorem

> **Theorem (M_001).** A measurement event is an actualization event applied to a
> distinguishable state: it is state selection (A) realized as distinguishability-
> becoming-actual (B). A measurement reads BOTH quadratures of one complex mode — the
> {cos, sin} two-quadrature reconstruction basis (D_037) — extracting the [magnitude,
> phase] pair that constitutes the state's identity (the primitive Difference applied,
> D_039). What changes: the state's identity transitions from potential (in the complex
> amplitude) to actual (a realized outcome with Born weight |ψ|² = ρ, QG216). Collapse
> (C) is the QG73 binary label for the same event, not a separate mechanism. Removing
> measurement leaves state identity, observability, probability, and interference
> intact — only the actualization of a specific outcome is removed. Hence: state
> identity DERIVED (D_039); observability DERIVED (D_037/D_038); Born probability
> DERIVED (QG216); the measurement event EMERGENT (the actualization readout); collapse
> EMERGENT (the event reading). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Identity = Difference applied (D_039). (2) Observability = both
> quadratures read (D_037, verified z = a + ib). (3) The measurement event realizes one
> outcome (Section 3). (4) The structure survives measurement removal (Section 5). (5)
> Hence measurement is the EMERGENT actualization of distinguishability (Section 6–7).
> ∎

---

## Dependency Graph

```
Difference (primitive)
 → Actualization (ticks, QG216)
 → distinguishability (D_039)                 [DERIVED]
 → state identity (D_039)                     [DERIVED]
 → complex state (magnitude + phase, D_036)   [DERIVED]
 → observability (two-quadrature basis, D_037) [DERIVED]
 → probability (Born rule, QG216)             [DERIVED]
 → MEASUREMENT (actualization reads the state) [EMERGENT]
 → collapse (the event's binary reading)      [EMERGENT]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is state identity derived? | **YES** (Difference applied, D_039) |
| Is observability derived? | **YES** (both quadratures, D_037) |
| Is the Born probability derived? | **YES** (QG216, Σ|ψ|²=1) |
| Is measurement a state selection? | **YES** (A) |
| Is it distinguishability becoming actual? | **YES** (B) |
| Is collapse a separate mechanism? | **NO** — it is the event's reading (EMERGENT) |
| What survives measurement removal? | identity, observability, probability, interference |
| Is the measurement event derived or emergent? | **EMERGENT** (the actualization readout) |

---

## Counterexamples

1. **a-alone measurement (one quadrature)**: θ ambiguous — NOT a complete measurement
   (D_037); the state's identity is not actualized.
2. **Real-only mode (singlet)**: only one DOF — cannot carry a complete measurement
   (D_035/D_037); excluded by the observable sector.
3. **Unobserved state**: full identity and interference present, no outcome — the
   measurement event has not occurred.
4. **Observed state**: one outcome with Born weight — the actualization of a
   distinguishable state.

---

## Classification

| Component | Status |
|---|---|
| state identity | **DERIVED** (D_039) |
| observability (two-quadrature basis) | **DERIVED** (D_037/D_038) |
| complex state | **DERIVED** (D_036) |
| Born probability | **DERIVED** (QG216) |
| measurement event | **EMERGENT** (the actualization readout) |
| collapse | **EMERGENT** (the event's binary reading, QG73 resolved) |

**A measurement event is the EMERGENT actualization of a distinguishable state's
identity — state selection realized as a count event. State identity, observability,
and probability are DERIVED; only the readout event is EMERGENT. No new primitive;
canonical AT unchanged.**

---

## Open Problems

1. **Observer role (M_001 OP1).** Whether the "observer" is itself an actualization
   subsystem (a distinguisher within the network) — the next measurement audit.

---

## Next Steps

- **ResearchY-M_002 (or synthesis):** the measurement event is the actualization
  readout; the next audit can derive the measurement-disturbance relations and the
  observer role from the same chain.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_001_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_M_001_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_M_001_StateIdentity` | identity = Difference applied; 95/95 distinct | ✅ |
| `Y_M_001_ActualizationEvent` | measurement is an actualization event (count realization) | ✅ |
| `Y_M_001_MeasurementEvent` | both quadratures read → state selected (A+B) | ✅ |
| `Y_M_001_Observability` | z = a + ib exact; a alone ambiguous | ✅ |
| `Y_M_001_CollapseComparison` | collapse = the event's binary reading, not separate | ✅ |
| `Y_M_001_DependencyTrace` | Difference → identity → observability → measurement | ✅ |
| `Y_M_001_Run` | Research report | ✅ |

**Conclusion:** A measurement event is an actualization event applied to a
distinguishable state — state selection realized as distinguishability-becoming-actual.
It reads both quadratures of one complex mode (the {cos, sin} basis), actualizing the
state's identity with Born weight. State identity, observability, and probability are
DERIVED; the measurement event (and its collapse reading) are EMERGENT. No new
primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_001"`

---

## References

- AT-QG: QG73 (collapse binary — now resolved as the event's reading), QG74 (measurement
  basis), QG216 (Born rule).
- ResearchY-D_034 (reciprocity), D_036 (complex state), D_037 (measurement basis),
  D_038 (state identity), D_039 (identity = Difference).
- Monograph V2.0: Ch9 (quantum mechanics).
