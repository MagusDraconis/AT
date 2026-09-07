# ResearchY-NP_163 — Organizational Path Dependence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_163 (permanent)
**Title:** Organizational Path Dependence Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_163.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_157
(organization vs material), NP_158 (latent organization state), NP_159 (structural training), NP_160
(organizational field), NP_161 (organizational wave), NP_162 (organizational tomography)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_163_Tests.cs`

---

## Purpose

NP_157–162 established that organization is a directly imageable dynamical field with latent states,
training, spatial variation, and propagating fronts. NP_163 asks the *causality* question: **does the
path through organizational state space matter more than the instantaneous excitation?** Program:
(1) define state / path / history / training trajectory; (2) compare identical final loads reached by
different excitation histories; (3) test whether path A → state X and path B → state Y despite
identical end conditions; (4) quantify anisotropy / damping / fracture behavior; (5) search for
unreachable states, hysteresis, training loops. **Success criterion:** determine whether the true
control variable is not vibration, stress, or frequency, but the *trajectory* through organizational
state space. No new primitives; canonical AT unchanged.

---

## 1. Define the four terms

| Term | Definition |
|---|---|
| **state** | an organizational configuration φ |
| **path** | the sequence of intermediate states followed |
| **history** | the record of past excitations that selected the path |
| **training trajectory** | a deliberate path designed to reach a preferred state (NP_159) |

---

## 2. Identical final load, different histories

Granular materials are **athermal and non-ergodic**: the final state is not a function of the final
load alone. Two blocks under the *same* final load but different loading/unloading/vibration sequences
end in **different** organizational states.

---

## 3. Path A → state X, path B → state Y (identical end conditions)

**Yes.** This is the defining signature of granular memory and hysteresis:

```
path A (preload → vibrate → unload)  →  state X (denser, oriented chains)
path B (vibrate → preload → unload)  →  state Y (looser, different fabric)
```

The end conditions are identical; the states differ because the *trajectory* differed. The path is a
genuine control variable.

---

## 4. Quantified differences

| Quantity | Path-dependent? |
|---|---|
| **anisotropy** | yes — orientation depends on the loading sequence |
| **damping** | yes — loose vs trained states dissipate differently |
| **fracture behavior** | yes — weak-path location depends on history (NP_156) |

All three are measurable (NP_162) and differ between path A and path B outcomes.

---

## 5. Unreachable states, hysteresis, training loops

| Feature | Present? |
|---|---|
| **unreachable states** | yes — some configurations are not reachable by a given protocol (non-ergodicity) |
| **hysteresis** | yes — loading/unloading loops do not retrace |
| **training loops** | yes — cyclic shear/vibration "trains" and "rejuvenates" the packing (reversal memory) |

These are the known mechanisms by which the trajectory, not the instantaneous excitation, sets the
state.

---

## Theorem

> **Theorem (NP_163).** The path through organizational state space matters MORE than the instantaneous
> excitation — and this is KNOWN PHYSICS. Granular/rock materials are athermal and non-ergodic: the
> final organizational state is a function of the loading/vibration HISTORY, not the final load. Two
> identical end conditions reached by different paths end in different states (path A → X, path B → Y),
> differing measurably in anisotropy, damping, and fracture behavior (NP_162). Unreachable states,
> hysteresis, and training loops (reversal memory, rejuvenation) are the standard signatures. So the true
> control variable is the TRAJECTORY through organizational state space, not vibration/stress/frequency
> alone. AT's framing of this is an INTERPRETATION of established granular memory/aging/hysteresis
> physics. Classification: path dependence KNOWN PHYSICS; trajectory-as-control KNOWN PHYSICS;
> "organizational path dependence" framing AT INTERPRETATION. Proof: (1) Define (Section 1). (2) Same
> load (Section 2). (3) Path A/B (Section 3). (4) Quantify (Section 4). (5) Hysteresis/unreachable
> (Section 5). **Success criterion: trajectory is the control variable — KNOWN PHYSICS.** No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define. (2) Same load. (3) Path A/B. (4) Quantify. (5) Hysteresis. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "the final load determines the state" | non-ergodicity/hysteresis: identical loads give different states per history |
| "path dependence is new physics" | granular memory/aging/reversal memory are established |
| "all states are reachable" | non-ergodicity excludes some configurations per protocol |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| path dependence (KNOWN) | a granular/rock system whose final state is a pure function of the final load, independent of history |
| trajectory is the control variable | two different paths to identical end conditions producing identical states |

---

## 8. Classification

| Component | Status |
|---|---|
| path dependence (history-dependent final state) | **KNOWN PHYSICS** |
| trajectory as the control variable | **KNOWN PHYSICS** |
| "organizational path dependence" framing | **AT INTERPRETATION** |
| path dependence is a new capability | **REFUTED** |

**Conclusion.** The **trajectory** through organizational state space is the true control variable —
more than vibration, stress, or frequency alone. Granular/rock materials are non-ergodic and
hysteretic: identical end conditions reached by different paths give different states (path A → X,
path B → Y), with unreachable states and training loops. This is KNOWN PHYSICS (granular memory/aging/
hysteresis); AT's "organizational path dependence" is an INTERPRETATION of it. No new primitive;
canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_163_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_163_Define` | state / path / history / trajectory | ✅ |
| `Y_NP_163_SameLoad` | identical final load, different histories | ✅ |
| `Y_NP_163_PathAB` | path A → X, path B → Y | ✅ |
| `Y_NP_163_Quantify` | anisotropy / damping / fracture differ | ✅ |
| `Y_NP_163_Hysteresis` | unreachable states + hysteresis + loops | ✅ |
| `Y_NP_163_Classification` | KNOWN PHYSICS; framing INTERPRETATION | ✅ |
| `Y_NP_163_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_163"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_157 (organization vs material), NP_158
  (latent organization state), NP_159 (structural training), NP_160 (organizational field), NP_161
  (organizational wave), NP_162 (organizational tomography).
- Real-world record: granular memory and aging; hysteresis and non-ergodicity; reversal memory and
  rejuvenation under cyclic shear; the reversible–irreversible phase diagram; DEM/photoelastic history-
  dependence studies.
