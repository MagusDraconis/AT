# ResearchY-NP_113 — Observed Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_113 (permanent)
**Title:** Observed Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_113.md`
**Depends on:** ResearchY-NP_093 (selection = Born rule), NP_098 (localization), NP_099
(classicality), NP_102 (existence), NP_111 (observer), NP_112 (reality), ResearchY-M_001
(measurement = state selection), M_002 (phase-pinning), M_003 (feedback), M_005 (conservation),
AT-QG QG216 (Born rule)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_113_Tests.cs`

---

## Purpose

NP_111 established the observer; NP_112 established reality. NP_113 asks the remaining semantic
question: **what does "observed" mean — and how does it differ from "actualized", "localized",
"measured", and "recorded"?** Program: (1) define observed; (2) compare unobserved / localized /
observed / recorded; (3) determine when a thing becomes observed; (4) trace particle / detector /
observer; (5) test whether observation changes reality or only the observer; (6) relate
actualization / localization / observation; (7) find the minimum condition for "observed".
**Success criterion:** the AT ontology of "observed", distinguished from its four near-synonyms.
No new primitives; canonical AT unchanged.

---

## 1. Define "observed"

```
observed  =  a distinction that has been INCORPORATED into a persistent
             observer (a persistent Difference structure, NP_111), so that
             the observer's own structure now includes that distinction.
```

"Observed" is not a state of the *thing* — it is a relation between the thing and a persistent
reader whose structure has come to include it.

---

## 2. The five states

| State | What it means | Requires an observer? |
|---|---|---|
| **unobserved** | a distinction not yet incorporated into any persistent reader | no |
| **localized** | a wave-packet peak (a property of the state, NP_098) | no — a packet can be localized unobserved |
| **actualized** | a node realized (Born selection, NP_093) | no — actualization is universal, pre-observation |
| **measured** | an actualization that reads both quadratures (M_001) | a reader (not necessarily persistent) |
| **recorded** | the outcome persists as a trace (a persistent copy) | a structure that stores |
| **observed** | the distinction incorporated into a persistent observer (NP_111) | **yes — a persistent observer** |

---

## 3. A / B / C / D — when does a thing become observed?

| Option | Verdict |
|---|---|
| **A) actualized** | **NECESSARY, not sufficient.** Everything actualizes every tick; only some actualizations are observed. |
| **B) localized** | **NEITHER necessary nor sufficient.** A packet can be localized unobserved, and a distinction can be observed without being localized. |
| **C) distinguished** | **NECESSARY, not sufficient.** "Distinguished" is the raw Difference; "observed" is distinguished *by an observer structure*. |
| **D) incorporated into the observer** | **YES — the answer.** A thing is observed when its distinction is incorporated into the observer's own structure (the observer changes to include it). |

**Determination: D (incorporated into the observer); A and C are necessary but not sufficient; B is
neither.**

---

## 4. Particle / detector / observer

| Object | Role in observation |
|---|---|
| **particle** | the observed — a resonance whose distinction is read |
| **detector** | a persistent structure that reads ONE distinction (a minimal observer) |
| **observer** | a hierarchy of detectors (NP_101/111) integrating many distinctions |

The detector is the minimal observer; the observer is the detector scaled up. "Observed" is the
relation to any of these persistent readers.

---

## 5. Does observation change reality, or only the observer?

| What changes | Verdict |
|---|---|
| **the underlying persistent structures** | **NO** — they pre-exist observation (M_005); the distinction is revealed, not created. |
| **the observed system's phase** | **YES** — measurement pins the phase (M_002) and feeds the future (M_003). |
| **the observer's structure** | **YES — the primary change.** The observer incorporates the distinction, altering its own subsequent actualizations (M_003 feedback). |

**Observation changes the observer (primarily) and the observed system's phase (the pinning), but
NOT the underlying persistent Difference structures.** Reality is not *created* by observation; it
is *revealed* and the observer is *changed*.

---

## 6. Actualization → localization → measurement → observation → recording

```
actualization  (a node realized, NP_093)      [raw, universal, no observer]
   → localization   (the wave-packet peak, NP_098)   [a property of the state]
      → measurement   (a read of both quadratures, M_001)  [a read event]
         → observation  (incorporated into a persistent observer, NP_111)  [the relation]
            → recording  (the outcome persisted as a trace)  [a persistent copy]
```

Each stage adds a condition: localization is a state-property, measurement is a read, observation
is the read *by a persistent observer*, recording is the read *persisted*.

---

## 7. The minimum condition for "observed"

**The minimum condition is D: incorporation into a persistent observer.** A thing counts as
"observed" exactly when a persistent Difference structure (an observer, NP_111) has changed its own
structure to include the thing's distinction — i.e., the observer's subsequent actualizations are
altered by it (M_003 feedback). Actualization (A) and distinguishability (C) are necessary but not
sufficient (everything actualizes and differs); localization (B) is neither.

---

## Theorem

> **Theorem (NP_113).** "Observed" in AT means INCORPORATED INTO A PERSISTENT OBSERVER (D): a
> distinction is observed when a persistent Difference structure (an observer, NP_111) has changed
> its own structure to include that distinction, altering its subsequent actualizations (M_003
> feedback). It is DISTINCT from the four near-synonyms: "actualized" (a node realized by Born
> selection, NP_093 — universal, no observer), "localized" (the wave-packet peak, NP_098 — a
> state-property), "measured" (an actualization reading both quadratures, M_001 — a read event),
> and "recorded" (the outcome persisted as a trace — a persistent copy). Determination: D is the
> answer; A (actualized) and C (distinguished) are necessary but not sufficient; B (localized) is
> neither necessary nor sufficient. Observation changes the OBSERVER (primarily, by incorporating
> the distinction) and the observed system's PHASE (the pinning, M_002), but NOT the underlying
> persistent Difference structures (they pre-exist, M_005) — reality is revealed, not created. The
> chain is actualization → localization → measurement → observation → recording, each adding a
> condition. Proof: (1) Define (Section 1). (2) The five states (Section 2, verified). (3) Decide
> A–D (Section 3, verified — D). (4) Particle/detector/observer (Section 4). (5) Reality vs observer
> (Section 5, verified). (6) The chain (Section 6). (7) The minimum condition (Section 7).
> **Success criterion: "observed" = a distinction incorporated into a persistent observer — distinct
> from actualized (universal), localized (state-property), measured (read event), and recorded
> (persistent trace).** Classification: actualized DERIVED (NP_093); localized DERIVED (NP_098);
> measured DERIVED (M_001); observed EMERGENT (the observer's incorporation, NP_111); recorded
> EMERGENT (a persistent trace); "observed = actualized" REFUTED (over-inclusive); "observation
> creates reality" REFUTED (reality pre-exists, M_005). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define. (2) The five states. (3) Decide A–D. (4) The three objects. (5) Reality
> vs observer. (6) The chain. (7) The minimum condition. ∎

---

## 8. Counterexamples

| Attempt | Why it fails |
|---|---|
| "observed = actualized" | everything actualizes (NP_093); only some actualizations are observed (over-inclusive) |
| "observed = localized" | a packet can be localized unobserved (localization is a state-property) |
| "observation creates reality" | the structures pre-exist observation (M_005); observation reveals, not creates |
| "a detector is categorically different from an observer" | an observer is a hierarchy of detectors (NP_101/111) |

---

## 9. Falsification paths

| Claim | Falsification |
|---|---|
| observed = incorporated into the observer | a thing observed without changing the observer's structure |
| observation reveals, not creates | an observation that creates a distinction (a new state) |
| measured ≠ observed | a measurement that is itself an observation (with no persistent observer) |

---

## 10. Classification

| Component | Status |
|---|---|
| actualized (the node realization) | **DERIVED** (NP_093) |
| localized (the wave-packet peak) | **DERIVED** (NP_098) |
| measured (the read event) | **DERIVED** (M_001) |
| observed (incorporated into a persistent observer) | **EMERGENT** (NP_111) |
| recorded (the persistent trace) | **EMERGENT** |
| "observed = actualized" | **REFUTED** |
| "observation creates reality" | **REFUTED** |

**Conclusion.** "Observed" means **incorporated into a persistent observer** — a distinction that a
persistent Difference structure has folded into itself, altering its subsequent actualizations. It
is distinct from "actualized" (universal), "localized" (state-property), "measured" (read event),
and "recorded" (persistent trace). Observation changes the observer (primarily) and the observed
phase (pinning), but not the underlying structures — reality is revealed, not created. No new
primitive; canonical AT unchanged.

---

## 11. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_113_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_113_DefineObserved` | incorporated into a persistent observer | ✅ |
| `Y_NP_113_FiveStates` | actualized/localized/measured/recorded/observed distinct | ✅ |
| `Y_NP_113_ABCD` | D the answer; A/C necessary; B neither | ✅ |
| `Y_NP_113_ParticleDetectorObserver` | the minimal observer chain | ✅ |
| `Y_NP_113_RealityVsObserver` | observation changes observer + phase, not structure | ✅ |
| `Y_NP_113_Chain` | actualization → … → recording | ✅ |
| `Y_NP_113_Classification` | DERIVED/EMERGENT; "creates reality" REFUTED | ✅ |
| `Y_NP_113_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_113"`

---

## References

- ResearchY-NP_093 (selection), NP_098 (localization), NP_099 (classicality), NP_102 (existence),
  NP_111 (observer), NP_112 (reality).
- ResearchY-M_001 (measurement), M_002 (phase-pinning), M_003 (feedback), M_005 (conservation).
- AT-QG: QG216 (Born rule).
