# ResearchY-NP_094 — Inertia Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_094 (permanent)
**Title:** Inertia Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_094.md`
**Depends on:** ResearchY-NP_072 (particle = resonance class), NP_075 (force = generator action),
NP_091 (network → spacetime), NP_093 (actualization selection = Born rule), NP_081 (energy =
relabeling of count), NP_092 (propagation = tick + null geodesics), AT-QG QG216 (Born rule),
QG220 (phase θ = 2πk/N), QG89 (Noether conservation), QG161/243 (generator action), ResearchY-D_041
(tick / phase advance), D_036 (complex state)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_094_Tests.cs`

---

## Purpose

NP_072 made particles resonance classes; NP_075 made forces symmetry-generator actions; NP_091/093
made space the network geometry, time the tick, and node-selection the Born rule. NP_094 asks the
question those four leave open: **what is inertia?** Program: (1) define motion on the network;
(2) decide whether motion is object transport / resonance propagation / node-activation transfer /
count transport; (3) remove all forces and test whether propagation persists; (4) trace the
electron, photon, and graviton; (5) determine whether momentum is count flow / phase gradient /
resonance persistence / network path persistence; (6) determine why motion continues when no
force acts; (7) compare with Newton I (F = 0 → v = const); (8) identify the AT analogue of
inertia, momentum, and inertial frame. **Success criterion:** explain inertia entirely through
network propagation, actualization, and resonance persistence — no classical mechanics imported.
No new primitives; canonical AT unchanged.

---

## 1. Define motion on the network

From NP_092: **nothing travels.** A particle is a resonance mode (a standing wave); its localized
wave-packet **envelope** advances at the group velocity v_g = dω/dk. Motion is therefore not the
transport of an object from node to node — it is the **coherent propagation of the mode's phase
pattern**: the envelope slides along the network while the underlying nodes and links stay fixed.

```
motion  =  the propagation of a resonance mode's phase pattern
            (the wave-packet envelope advancing at the group velocity v_g = dω/dk)
node/link  =  STATIC (NP_092) — nothing is transported
```

---

## 2. What does motion mean — A / B / C / D?

| Interpretation | Verdict |
|---|---|
| **A) object transport** | **REFUTED.** Nothing travels (NP_092); the nodes and links do not move. |
| **B) resonance propagation** | **YES.** The mode's coherent phase pattern propagates — the envelope advances at v_g (NP_092). |
| **C) node-activation transfer** | **PARTIAL.** The *pattern* of node activations moves, but nothing is transferred: each tick's Born selection (NP_093) is per-tick, not a hand-off of activation. |
| **D) count transport** | **PARTIAL.** Count redistributes by continuity (NP_092/NP_081), but that is conservation, not "motion". |

**Determination: B — motion is resonance propagation.** A moving particle is a propagating
resonance pattern, not a transported object.

---

## 3. Remove all forces — does propagation persist?

A force is a D96 symmetry-generator action that induces a **resonance transition** between modes
(NP_075). Removing all forces = removing all generator actions = no resonance transitions.

| With forces removed | What happens |
|---|---|
| the mode's identity (frequency ω₀, wave number k) | **PERSISTS** — no generator action exists to change it |
| the phase advance θ_t = θ₀ + t·Δθ (D_041) | **CONTINUES** — the tick is independent of forces |
| the envelope's velocity v_g = dω/dk | **CONSTANT** — k is unchanged, so v_g is unchanged |

**YES — propagation persists with no force.** This is the whole point: actualization does not
need a force to keep going. The tick advances (time, NP_091), the Born rule selects nodes
(NP_093), and the phase advances deterministically (D_041) — all without any generator action.
A force is only what *changes* the mode (a resonance transition), not what *sustains* motion.

---

## 4. Trace electron, photon, graviton

| Object | Mode | Motion | Momentum | Inertia |
|---|---|---|---|---|
| **electron** | matter resonance mode (ω₀ = m_e/ℏ, the octave bottom, NP_072) | the envelope at v_g = k/ω | phase gradient k (p = ℏk) | **YES** — persists at constant k, even at rest (k = 0, still oscillating at ω₀) |
| **photon** | the U(1) rotation generator (a link excitation, NP_075) | the null geodesic, n = 1 (NP_092) | wave number k along the null direction | **NO rest inertia** — massless (ω₀ = 0), always at the propagation limit c |
| **graviton** | a ψ ripple (the traceless/Weyl face, NP_080/092) | the metric ripple | the ripple's wave number | **NO rest inertia** — massless (ω₀ = 0) |

**Mass is the rest frequency ω₀.** A matter particle (ω₀ > 0) can persist at rest (k = 0, still
oscillating); a massless generator (ω₀ = 0) cannot rest — it always propagates at n = 1. This is
why the photon and graviton have no inertia in the rest-mass sense, while the electron does.

---

## 5. Momentum — A / B / C / D?

| Interpretation | Verdict |
|---|---|
| **A) count flow** | **NO.** Count is conserved (continuity, NP_081); it is not momentum. |
| **B) phase gradient** | **YES.** Momentum = the phase gradient — the wave number k (the spatial rate of phase change θ = 2πk/N). p = ℏk. |
| **C) resonance persistence** | **NO — that is INERTIA.** Resonance persistence is *why* momentum persists, not momentum itself. |
| **D) network path persistence** | **NO — that is INERTIA (the path analogue).** Not momentum itself. |

**Determination: B — momentum is the phase gradient (the wave number k).** The mode's wave number
is its fixed spectral label; momentum is that label read as a spatial phase gradient.

---

## 6. Why does motion continue when no force acts?

**Because a resonance mode is a stable eigenmode of the spectrum.** The modes are the eigenfunctions
of the D96 Laplacian (D_041) — stable frequency attractors (NP_072). The free actualization (no
generator) does three things, none of which changes the mode:

1. the **tick** advances time (NP_091) — independent of forces;
2. the **Born rule** selects nodes with weight ρ = |ψ|² (NP_093) — it realizes the occupancy, it
   does not change the mode;
3. the **phase** advances deterministically θ_t = θ₀ + t·Δθ (D_041) — it advances the phase, it
   does not change the wave number.

Only a **generator action** (a force, NP_075) mixes modes — a resonance transition that changes k
(and hence v_g). With no generator, k is constant, so v_g = dω/dk is constant. **Motion continues
unchanged because the mode is stable under free actualization — that stability IS inertia.**

---

## 7. Newton I — the AT statement

| Newton I | AT analogue |
|---|---|
| F = 0 → v = const | no generator action → no resonance transition → k constant → v_g = dω/dk constant |
| inertia | resonance persistence: a mode keeps its frequency ω₀ and wave number k across ticks |
| momentum p | the phase gradient k (p = ℏk) |
| force F | the generator action (resonance transition), dp/dt = F |
| inertial frame | the frame with no generator action = the geodesic (free-fall) frame = the light-cone/conformal frame (NP_091) |

Newton's first law is the **network statement that a mode's phase gradient is conserved under free
actualization.** It is not imported: it follows from the eigenmode structure (D_041) + the
Born/deterministic actualization (NP_093) + the generator-action definition of force (NP_075).

---

## 8. The AT analogues

| Classical concept | AT analogue | Status |
|---|---|---|
| **inertia** | **resonance persistence** — the stability of a spectral eigenmode under free (generator-free) actualization | DERIVED |
| **momentum** | **the phase gradient** — the wave number k (θ = 2πk/N, p = ℏk) | DERIVED |
| **mass** | **the rest frequency** — ω₀ = m/ℏ (the mode's frequency attractor, anchored by m_e) | DERIVED (anchor m_e BOUNDARY) |
| **force** | **the generator action** — a resonance transition (changes k) | DERIVED (NP_075) |
| **inertial frame** | **the geodesic frame** — the frame with no generator action (the light-cone/conformal structure) | DERIVED (NP_091) |
| **motion** | **resonance propagation** — the mode envelope advancing at v_g | EMERGENT (NP_092) |

---

## Theorem

> **Theorem (NP_094).** Inertia in AT is RESONANCE PERSISTENCE — the stability of a spectral
> eigenmode under free (generator-free) actualization. Motion is NOT object transport (nothing
> travels, NP_092) but RESONANCE PROPAGATION (B): a particle is a resonance mode whose wave-packet
> envelope advances at the group velocity v_g = dω/dk. Momentum is the PHASE GRADIENT (B) — the
> wave number k (θ = 2πk/N, p = ℏk). Mass is the REST FREQUENCY ω₀ = m/ℏ (the mode's frequency
> attractor). A force is a GENERATOR ACTION (a resonance transition, NP_075) that changes k. Free
> actualization — the tick (NP_091) + the Born rule (NP_093) + the deterministic phase advance
> (D_041) — does NOT change k; only a generator action does. Hence Newton I (F = 0 → v = const)
> is the network statement that a mode's phase gradient is conserved under free actualization: no
> generator → no resonance transition → k constant → v_g constant. The electron (a matter mode,
> ω₀ > 0) has inertia and can rest (k = 0, still oscillating); the photon and graviton (massless
> generators, ω₀ = 0) have no rest inertia and always propagate at the limit n = 1. Proof: (1)
> Define motion (Section 1). (2) Motion = B (Section 2, verified). (3) Remove forces (Section 3,
> verified — propagation persists). (4) Trace e/γ/graviton (Section 4). (5) Momentum = B (Section
> 5, verified). (6) Why motion persists (Section 6, verified). (7) Newton I (Section 7, verified).
> (8) Analogues (Section 8). **Success criterion: inertia = resonance persistence, explained
> entirely through network propagation, actualization, and the eigenmode structure — no classical
> mechanics imported.** Classification: inertia DERIVED (eigenmode stability under free
> actualization); momentum DERIVED (the phase gradient k); mass DERIVED (the rest frequency, with
> anchor m_e BOUNDARY); force DERIVED (generator action, NP_075); inertial frame DERIVED (the
> geodesic/conformal frame, NP_091); motion EMERGENT (the envelope propagation, NP_092);
> "motion = object transport" REFUTED. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define motion. (2) Test A–D for motion. (3) Remove forces. (4) Trace the
> three objects. (5) Test A–D for momentum. (6) Explain persistence. (7) Map to Newton I.
> (8) List the analogues. ∎

---

## 9. Counterexamples

| Attempt | Why it fails |
|---|---|
| "motion is object transport" | nothing travels (NP_092); nodes/links are static |
| "a force is needed to sustain motion" | free actualization (tick + Born + phase) keeps motion going; force only *changes* the mode |
| "momentum is count flow" | count is conserved (continuity, NP_081); momentum is the phase gradient k |
| "inertia is an imported law" | it is the eigenmode stability of the spectrum (D_041), not imported |
| "the photon has rest inertia" | the photon is massless (ω₀ = 0); it cannot rest, always at n = 1 |
| "inertia and momentum are the same" | momentum = the phase gradient k; inertia = its persistence under free actualization |

---

## 10. Falsification paths

| Claim | Falsification |
|---|---|
| inertia = resonance persistence | a mode whose wave number changes under free (generator-free) actualization |
| momentum = the phase gradient k | a momentum not expressible as the mode's wave number |
| no force → motion persists | a mode that decays/stops with no generator action |
| mass = the rest frequency ω₀ | a massive particle with zero rest frequency, or a massless one that can rest |
| Newton I is derived | a preferred frame or a generator-free acceleration (k change) |

---

## 11. Classification

| Component | Status |
|---|---|
| inertia (resonance persistence) | **DERIVED** (eigenmode stability under free actualization) |
| momentum (the phase gradient k) | **DERIVED** (θ = 2πk/N, D_041) |
| mass (the rest frequency ω₀) | **DERIVED** (frequency attractor; anchor m_e BOUNDARY, NP_072/082) |
| force (generator action = resonance transition) | **DERIVED** (NP_075) |
| inertial frame (the geodesic/conformal frame) | **DERIVED** (NP_091) |
| motion (resonance propagation) | **EMERGENT** (the envelope at v_g, NP_092) |
| motion = object transport | **REFUTED** |
| inertia as an imported classical law | **REFUTED** |

**Conclusion.** Inertia is **resonance persistence**: a particle is a resonance mode (NP_072), and
its frequency ω₀ (mass) and wave number k (momentum) are its fixed spectral labels. Free
actualization — the tick, the Born-rule selection, and the deterministic phase advance — does not
change k, so the mode's envelope keeps moving at constant v_g = dω/dk. Only a generator action
(a force, NP_075) changes k (a resonance transition). Newton's first law is therefore the network
statement that a mode's phase gradient is conserved under free actualization. Massless generators
(photon, graviton) have no rest inertia (ω₀ = 0), while matter modes (ω₀ > 0) can persist at rest.
Inertia, momentum, and the inertial frame are all DERIVED — nothing is imported from classical
mechanics. No new primitive; canonical AT unchanged.

---

## 12. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_094_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_094_MotionOnNetwork` | motion = resonance propagation, not object transport | ✅ |
| `Y_NP_094_MotionABCD` | B (resonance propagation); A refuted; C/D partial | ✅ |
| `Y_NP_094_RemoveForces` | propagation persists with no force (mode unchanged) | ✅ |
| `Y_NP_094_TraceObjects` | electron inertia, photon/graviton massless | ✅ |
| `Y_NP_094_MomentumABCD` | B (phase gradient k); A no; C/D = inertia | ✅ |
| `Y_NP_094_WhyMotionPersists` | eigenmode stability under free actualization | ✅ |
| `Y_NP_094_NewtonFirstLaw` | F=0 → k const → v_g const | ✅ |
| `Y_NP_094_InertialFrame` | geodesic/conformal frame (no generator) | ✅ |
| `Y_NP_094_Classification` | inertia/momentum/mass DERIVED; object transport REFUTED | ✅ |
| `Y_NP_094_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_094"`

---

## References

- ResearchY-NP_072 (particle = resonance class), NP_075 (force = generator action), NP_081
  (energy = relabeling of count), NP_091 (network → spacetime), NP_092 (propagation), NP_093
  (actualization selection).
- AT-QG: QG216 (Born rule), QG220 (phase θ = 2πk/N), QG89 (Noether conservation), QG161/243
  (generator action), QG57 (bosons = link excitations).
- ResearchY-D_041 (tick / phase advance), D_036 (complex state).
