# ResearchY-NP_100 — Bound Structure Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_100 (permanent)
**Title:** Bound Structure Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_100.md`
**Depends on:** ResearchY-NP_071 (matter = deficit excitation), NP_072 (particle = resonance
class), NP_075 (force = generator action), NP_094 (inertia = resonance persistence), NP_098
(localization), NP_099 (classicality), AT-QG QG194 (matter = deficit), QG161/243 (generator
action), QG57 (bosons = link excitations), ResearchY-NP_005 (missing synchronization)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_100_Tests.cs`

---

## Purpose

NP_072 made particles resonance classes; NP_075 made forces generator actions; NP_098/099 made
localization and classicality the wave-packet peak and its decoherence. NP_100 asks the structural
question that follows: **what is a bound structure — and why does it persist?** Program: (1)
define a bound state; (2) decide whether binding is resonance locking / phase synchronization /
persistent generator action / deficit clustering; (3) trace a particle pair, an atom, a molecule;
(4) determine why structures persist across many ticks; (5) compare free / localized / bound
resonances. **Success criterion:** the ontology of stability and binding. No new primitives;
canonical AT unchanged.

---

## 1. Define a bound state

```
bound state  =  a STABLE MUTUAL CONFIGURATION of two or more resonances
                whose relative phase is LOCKED (constant across ticks)
                and whose binding force is a PERSISTENT generator action
```

A bound state is not a new object — it is a **persistent resonance relationship**: the modes stop
drifting and settle into a fixed mutual phase (a stable fixed point of the actualization). Free
modes drift (Δθ grows); bound modes lock (Δθ constant).

---

## 2. A / B / C / D — what is binding?

| Interpretation | Verdict |
|---|---|
| **A) resonance locking** | **YES — the ontology.** Bound modes are phase-locked into a stable mutual configuration. |
| **B) phase synchronization** | **YES — the same object.** The relative phase is fixed (synchronized), not drifting. |
| **C) persistent generator action** | **YES — the binding force.** The binding is the ONGOING exchange of generator actions (link excitations, NP_075), not a single transient transition. |
| **D) deficit clustering** | **YES — the stable configuration.** Matter = the deficit (NP_071), and the deficit is the unique self-bound, clumping branch. |

**Determination: A = B = D (resonance locking = phase synchronization = deficit clustering — three
readings of the same bound object), with C (persistent generator action) as the binding force.**
A bound structure is a deficit clustering held together by persistent generator action, read as
phase-locked synchronized modes.

---

## 3. Trace a particle pair, atom, molecule

| Structure | Composition | Binding (the persistent generator action) |
|---|---|---|
| **particle pair** (e⁻ + p) | electron mode + proton deficit, phase-locked | the persistent U(1) rotation generator (the photon link excitation, NP_075) |
| **atom** (hydrogen) | the pair locked into a stable deficit clustering | the same U(1) action; binding energy 13.6 eV (hosted value, like m_e) |
| **molecule** (H₂) | two atoms, each a deficit clustering, mutually locked | shared generator actions (covalent links = phase-locked modes) |

Each level is a **deficit clustering** of the level below: modes → pair → atom → molecule. The
binding at every level is the persistent generator action (the ongoing exchange of link
excitations) that holds the relative phases locked.

---

## 4. Why do structures persist across many ticks?

**A bound state is a stable fixed point of the actualization.** Its locked relative phase does not
drift, and its persistent generator action is balanced (net-zero over a cycle), so nothing changes
it. This is the bound-state analogue of inertia (NP_094):

```
inertia  = the persistence of a SINGLE mode  (k conserved under free actualization)
binding  = the persistence of a MUTUAL configuration  (the locked phase conserved)
```

Free actualization (the tick, the Born rule, the deterministic phase advance) does not change the
locked configuration — only a strong generator action (enough energy to break the lock) does. So
the structure survives tick after tick.

---

## 5. Free vs localized vs bound resonance

| Resonance | Description | Regime | Persistence |
|---|---|---|---|
| **free** | a single mode, \|ψ\|² uniform over the ring | quantum, delocalized | none — drifts freely |
| **localized** | a wave packet (superposition), peaked \|ψ\|² | quantum, interfering | transient (the packet spreads) |
| **bound** | phase-locked deficit clustering | classical, decohered | **persistent** (the lock is stable) |

The hierarchy free → localized → bound is one of **increasing persistence and stability**: a bound
structure is a resonance (or cluster of resonances) whose mutual phase is locked into a stable
fixed point, so it endures across ticks — the seed of classical matter.

---

## Theorem

> **Theorem (NP_100).** A bound structure in AT is a DEFICIT CLUSTERING held together by
> PERSISTENT GENERATOR ACTION, read as PHASE-LOCKED (synchronized) resonance modes — A = B = D,
> with C the binding force. Matter is the deficit (NP_071), the unique self-bound, clumping branch;
> a force is a generator action (a resonance transition, NP_075). A bound state is the case where the
> generator action is PERSISTENT (an ongoing exchange of link excitations, e.g. the photon = the U(1)
> rotation generator) rather than a single transient transition: it locks the relative phase of two
> modes to a stable fixed point. The structure persists across ticks because the lock is stable under
> free actualization — the locked phase does not drift, exactly as a single mode's k does not change
> under inertia (NP_094). Bound states therefore cascade: modes → pair (e⁻+p) → atom (hydrogen,
> 13.6 eV) → molecule (H₂), each a deficit clustering of the level below. Classification: the binding
> ontology (resonance locking = phase synchronization = deficit clustering) DERIVED (NP_071/075/094);
> the bound structure as a persistent macroscopic configuration EMERGENT; the binding energies
> (13.6 eV etc.) BOUNDARY (imported values, the m_e-anchor pattern); "binding as a new primitive"
> REFUTED. Proof: (1) Define (Section 1). (2) Test A–D (Section 2, verified — A = B = D, C the
> force). (3) Trace pair/atom/molecule (Section 3). (4) Persistence (Section 4, verified — the lock
> is a stable fixed point). (5) Free/localized/bound (Section 5). **Success criterion: stability and
> binding = resonance locking (phase synchronization) realized as deficit clustering, held by
> persistent generator action — DERIVED, with the bound structure EMERGENT.** No new primitive;
> canonical AT unchanged.
>
> *Proof sketch.* (1) Define. (2) Test A–D. (3) Trace structures. (4) Explain persistence.
> (5) Compare the three resonances. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "binding is a new primitive" | binding is deficit clustering + persistent generator action, both derived |
| "binding is a single transient transition" | a bound state is a PERSISTENT (ongoing) generator action, not a one-off transition |
| "bound modes drift" | bound modes are phase-locked (Δθ constant); only free modes drift |
| "a bound state is a single node" | a bound state is a mutual configuration of resonances, not one node |
| "binding energies are derived" | the specific values (13.6 eV) are imported/hosted, like the m_e anchor |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| binding = resonance locking | a bound state with a drifting (unlocked) relative phase |
| binding = deficit clustering | a bound structure not expressible as a deficit (under-occupancy) configuration |
| the lock is stable under free actualization | a bound state that decays with no generator action |
| binding is derived | a binding mechanism requiring a new primitive beyond Difference/η |

---

## 8. Classification

| Component | Status |
|---|---|
| binding ontology (resonance locking = phase synchronization = deficit clustering) | **DERIVED** (NP_071/075/094) |
| the persistent generator action (the binding force) | **DERIVED** (NP_075) |
| the bound structure (atom / molecule) as a persistent configuration | **EMERGENT** |
| the binding energies (13.6 eV etc.) | **BOUNDARY** (imported values, the m_e-anchor pattern) |
| binding as a new primitive | **REFUTED** |

**Conclusion.** A bound structure is a **deficit clustering** — matter's stable, self-bound
configuration (NP_071) — held together by **persistent generator action** (the ongoing exchange of
link excitations, NP_075), read as **phase-locked (synchronized) resonance modes** (A = B = D, with
C the force). It persists across ticks because the lock is a stable fixed point of the actualization:
the relative phase does not drift, exactly as a single mode's k is conserved under inertia (NP_094).
Structures cascade — pair → atom → molecule — each a deficit clustering of the level below. Binding
is DERIVED; the bound structure is EMERGENT; the binding energies are BOUNDARY (imported). No new
primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_100_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_100_DefineBoundState` | a stable mutual configuration (locked relative phase) | ✅ |
| `Y_NP_100_ABCD` | A = B = D; C the binding force | ✅ |
| `Y_NP_100_TraceStructures` | pair → atom → molecule = deficit clusterings | ✅ |
| `Y_NP_100_PersistenceAcrossTicks` | the lock is a stable fixed point | ✅ |
| `Y_NP_100_PhaseLocking` | bound = Δθ constant; free = Δθ drifts | ✅ |
| `Y_NP_100_FreeLocalizedBound` | increasing persistence / stability | ✅ |
| `Y_NP_100_Classification` | binding DERIVED; structure EMERGENT; energy BOUNDARY | ✅ |
| `Y_NP_100_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_100"`

---

## References

- ResearchY-NP_071 (matter = deficit excitation), NP_072 (particle = resonance class), NP_075
  (force = generator action), NP_094 (inertia), NP_098 (localization), NP_099 (classicality),
  NP_005 (missing synchronization).
- AT-QG: QG194 (matter = deficit), QG161/243 (generator action), QG57 (bosons = link
  excitations).
- ResearchY-D_041 (tick / phase advance).
