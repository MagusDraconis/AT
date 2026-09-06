# ResearchY-NP_092 — Network Propagation Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_092 (permanent)
**Title:** Network Propagation Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_092.md`
**Depends on:** ResearchY-NP_080 (Difference duality), NP_084 (η framework), NP_091 (network
→ spacetime), AT-QG QG28 (propagation law — null geodesics), QG31 (TRM propagator origin),
QG29 (local tick), QG216 (Born |ψ|² = ρ), QG286 (Difference duality), ResearchY-NP_005 (missing
synchronization), NP_007 (coupling field), NP_075 (force = generator action), NP_081 (energy =
conserved count), NP_089 (cubic dispersion), NP_090 (network ontology)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_092_Tests.cs`

---

## Purpose

NP_090 established the network's *objects* (nodes = distinctions, links = adjacency); NP_091
established how the network becomes *spacetime* (space emergent, time the tick). NP_092 asks the
remaining question: **what actually propagates on the network?** Program: (1) inventory every
propagating object (ρ, ψ, phase, information, particles, forces); (2) decide whether propagation
is A) count transport, B) phase transport, C) deficit transport, or D) geometry transport; (3)
trace the photon, the graviton, and a particle mode through the network; (4) determine what
actually moves vs what only appears to move; (5) identify the network analogue of velocity,
locality, and causality. **Success criterion:** the canonical ontology of propagation. No new
primitives; canonical AT unchanged.

---

## 1. Inventory — every propagating object

| Object | Type | Does it *propagate* on the network? | Source |
|---|---|---|---|
| **ρ (the trace / count)** | scalar density | YES — but as **conservation** (continuity), not free propagation | NP_080/081 |
| **ψ (the traceless / Weyl)** | spin-2 tensor | YES — as a **metric ripple** (the gravitational wave) | NP_080/084 |
| **phase θ = 2πk/N** | per-node lattice label | **NO** — phase does not flow; it is a local label (NP_005) | NP_005/QG216 |
| **information** | the matter deficit | YES — but only as **count redistribution** (bookkeeping) | NP_057/081 |
| **particles** | resonance modes (standing waves) | **APPEARS TO** — the wave-packet *envelope* moves | NP_090 |
| **forces** | D96 generator action | YES — as **link-mediated action**, no field "flies" | NP_075 |

---

## 2. What is native propagation? — the decisive fact (QG28/31)

Actualization produces, in order:

```
Q-events  →  causal order  →  conformal class (the light cone)  →  conformal factor ρ
                                                                   →  metric g = ρ^(2/d)η
```

The causal order determines the **conformal class** — the null directions (the light cone). The
counting measure ρ then supplies only the conformal factor ρ^(2/d), a rescaling that **leaves the
light cone invariant**. Consequently light propagates along the causal-order light cone:

```
null geodesics,  n = 1,  independent of ρ     [QG28 — DERIVED]
```

The tick propagates along the **generation relation**, whose boundary is the light cone: native
index n = 1, effective profile M_eff = n − 1 = 0 (**massless null propagation**, QG31). The
nonzero refractive/mass profile M_eff = e^Φ − 1 is the **non-conformal (ψ) sector** — not native
to the conformal tick network (QG28/31).

---

## 3. A / B / C / D — what kind of transport is propagation?

| Interpretation | Verdict |
|---|---|
| **A) count transport** | **YES (as continuity).** ρ obeys the conservation law ∂_t ρ + ∇·j = 0 — the conserved count (NP_081: energy = conserved count) redistributes, but nothing is created or destroyed. DERIVED. |
| **B) phase transport** | **NO.** θ = 2πk/N is a *local* per-node label; there is no phase flow in the canonical network (NP_005). REFUTED. |
| **C) deficit transport** | **Sub-case of A.** The deficit (matter = maximal − actual occupancy) is derived from count, so "deficit transport" is count transport re-labelled (NP_057/081). |
| **D) geometry transport** | **YES (as ψ ripples).** The traceless face ψ propagates as a gravitational wave — the metric ripple. Native in the conformal sector (the null mode); the *refractive* content (lensing) is the hosted non-conformal extension (QG28/31). |

**Determination: A and D are the two real channels, both DERIVED; B is REFUTED; C reduces to A.**
But the deepest point is that **neither A nor D is a substantial object travelling** — A is a
conservation bookkeeping, D is a ripple in the connectivity's orientation face. The network is
**static** (NP_007: "no propagating field in canonical AT").

---

## 4. What actually moves, and what only appears to move

| Claim | Verdict | Why |
|---|---|---|
| **the nodes and links** | **DO NOT move.** | They are the difference structure — the static *being* (NP_090). |
| **the tick** | **MOVES.** | The actualization order advances: a Q-event, then its successor along the generation relation. This is the only genuine "movement", one link per tick (the propagation limit, NP_091). FRAMEWORK. |
| **light** | **"MOVES" as the light cone.** | Light propagates along null geodesics (n = 1) — but this is the *boundary of the causal order*, not a thing travelling (QG28). DERIVED. |
| **the photon** | **APPEARS TO MOVE.** | The photon is a resonance mode (a standing wave, NP_090); a localized photon is a wave packet whose *envelope* travels at the group velocity. |
| **the graviton** | **APPEARS TO MOVE.** | A ripple in ψ (the traceless face) — a metric ripple, not a particle flying through space (NP_080). |
| **a particle mode** | **APPEARS TO MOVE.** | A mode is a standing wave; the envelope of a superposition moves, the mode itself does not. |
| **the count ρ** | **REDISTRIBUTES, does not travel.** | Continuity (∂_t ρ + ∇·j = 0) — conservation, not transport. |

**What actually moves = the tick.** Everything else is either the light cone (the causal
boundary), a standing-wave envelope, a metric ripple, or a conserved redistribution — it *appears*
to move without any substantial object travelling.

---

## 5. The network analogue of velocity, locality, causality

| Concept | Network analogue | Value |
|---|---|---|
| **velocity** | the **group velocity** of a wave packet, v_g = dω/dk, from the cubic dispersion ω² = k² − (k_x⁴+k_y⁴+k_z⁴)/12 (NP_089) | v_g → 1 (c) as k → 0; **subluminal at finite k** (verified: v_g = 0.99875 at k=0.1, 0.98871 at k=0.3, 0.95442 at k=0.6) |
| **locality** | the **adjacency**: node i couples only to node j if A_ij ≠ 0 (degree-12 neighbours) | nearest-neighbour only |
| **causality** | the **partial order** (the tick order): nothing changes faster than one tick; acyclic → no closed timelike loops; the light cone = the null geodesic = the causal boundary (NP_091 §6) | the tick is the propagation limit = c |

The maximum propagation speed is **one link per tick** (= c, the imported unit convention, NP_091).
The group velocity from the dispersion is always ≤ 1 in these units, so the network is **causal**:
no excitation outruns the light cone. The cubic correction makes propagation **subluminal** at
finite wavelength and exactly luminal only in the continuum (k → 0).

---

## Theorem

> **Theorem (NP_092).** Propagation in Actualization Theory is NOT the transport of a substance:
> the network is static (nodes and links do not move; NP_007/090), and nothing substantial travels
> from node to node. The ONLY genuine "movement" is the ACTUALIZATION TICK — the advance of the
> causal order along the generation relation (QG29/31), whose boundary is the light cone. Native
> propagation is therefore light along null geodesics (n = 1, independent of ρ — DERIVED, QG28);
> the tick propagates masslessly (M_eff = 0, QG31). Everything else only APPEARS to move: a
> particle is a resonance mode (a standing wave, NP_090) whose localized wave-packet ENVELOPE
> travels at the group velocity v_g = dω/dk (an emergent description); the graviton is a ripple in
> ψ (the traceless/Weyl face, NP_080); a force is link-mediated generator action (NP_075); and the
> count ρ merely redistributes by the continuity equation ∂_t ρ + ∇·j = 0 (conserved count, NP_081).
> Determination: A (count transport, as continuity) and D (geometry transport, as ψ ripples) are
> the two real channels, both DERIVED; B (phase transport) is REFUTED (phase θ = 2πk/N is a local
> label, no phase flow, NP_005); C (deficit transport) reduces to A. Velocity = the group velocity
> (v_g ≤ 1, subluminal at finite k, → c in the continuum); locality = the adjacency (degree-12
> neighbours); causality = the partial order (nothing outruns the tick). **Success criterion: the
> canonical ontology of propagation is — the tick moves (FRAMEWORK), light is the light cone
> (DERIVED), and particles, gravitons, forces, and count only APPEAR to move.** Classification: null
> geodesics DERIVED (QG28); the tick FRAMEWORK (QG29/31); count continuity DERIVED (NP_081); the
> particle wave-packet motion EMERGENT; ψ ripple propagation DERIVED (given the mode structure);
> phase transport REFUTED (NP_005); a propagating field REFUTED (NP_007). No new primitive;
> canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Give the native propagation (QG28/31). (3) Decide A–D.
> (4) Separate moves vs appears-to-move. (5) Define velocity/locality/causality. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "a particle travels from node to node" | a particle is a standing wave (a resonance mode); only the wave-packet envelope moves |
| "phase propagates" | θ = 2πk/N is a local label; there is no phase flow in the canonical network (NP_005) |
| "the count travels" | the count is conserved — it redistributes by continuity, it does not travel |
| "a field flies between particles" | the network is static (NP_007); a force is link-mediated generator action, no field travels |
| "the graviton is a particle flying through space" | the graviton is a ripple in ψ (the traceless face), not a substantial particle (NP_080) |
| "propagation outruns the light cone" | the group velocity v_g ≤ 1 (one link per tick = c); subluminal at finite k |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| native propagation is null geodesics (n = 1) | a native refractive index n ≠ 1 derivable from the conformal tick network alone |
| the tick is massless null (M_eff = 0) | a native nonzero mass/refractive profile from the conformal network |
| phase does not flow | a phase transport mechanism (phase flow) derivable in canonical AT |
| the network is static | a substantial object physically travelling from node to node |
| the group velocity ≤ c | a dispersion with v_g > 1 (superluminal propagation) on the D96 network |

---

## 8. Classification

| Component | Status |
|---|---|
| light along null geodesics (n = 1) | **DERIVED** (QG28) |
| the tick (causal-order advance, massless null) | **FRAMEWORK** (QG29/31) |
| count continuity ∂_t ρ + ∇·j = 0 | **DERIVED** (NP_081, conserved count) |
| particle wave-packet motion (envelope, v_g) | **EMERGENT** (mode superposition) |
| ψ ripple propagation (the gravitational wave) | **DERIVED** (given the mode structure; refractive content hosted, QG28/31) |
| force as link-mediated generator action | **DERIVED** (NP_075) |
| phase transport | **REFUTED** (NP_005 — no phase flow) |
| a substantial propagating field | **REFUTED** (NP_007 — the network is static) |

**Conclusion.** Propagation in AT is not transport. The network — distinctions joined by links —
is **static**: nothing substantial travels from node to node (NP_007/090). The only genuine
movement is the **actualization tick**, the advance of the causal order (FRAMEWORK), and its
native propagation law is **light along null geodesics** (n = 1, DERIVED, QG28). Particles,
gravitons, forces, and count all only *appear* to move: a particle is a standing wave whose
wave-packet envelope moves at the group velocity (EMERGENT); the graviton is a ripple in ψ
(DERIVED); a force is link-mediated generator action (DERIVED); and count redistributes by
continuity (DERIVED). Velocity is the group velocity (≤ c, subluminal at finite wavelength);
locality is the adjacency; causality is the partial order. No new primitive; canonical AT
unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_092_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_092_Inventory` | six objects with their propagation status | ✅ |
| `Y_NP_092_NativeIsNullGeodesic` | light n = 1, DERIVED (QG28) | ✅ |
| `Y_NP_092_TickAlongCausalOrder` | tick along generation relation, M_eff = 0 (QG31) | ✅ |
| `Y_NP_092_NetworkIsStatic` | no propagating field (NP_007) | ✅ |
| `Y_NP_092_PhaseDoesNotFlow` | θ local label, no phase transport (NP_005) | ✅ |
| `Y_NP_092_CountConservation` | ∂_t ρ + ∇·j = 0, conserved count (NP_081) | ✅ |
| `Y_NP_092_ParticlesStandingWaves` | particle = mode; envelope moves at v_g | ✅ |
| `Y_NP_092_GroupVelocity` | v_g ≤ 1, → c at k→0 (verified) | ✅ |
| `Y_NP_092_VelocityLocalityCausality` | velocity = v_g, locality = adjacency, causality = order | ✅ |
| `Y_NP_092_ABCD` | A + D real (DERIVED), B refuted, C = A | ✅ |
| `Y_NP_092_Classification` | null DERIVED; tick FRAMEWORK; phase/field REFUTED | ✅ |
| `Y_NP_092_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_092"`

---

## References

- ResearchY-NP_080 (Difference duality), NP_084 (η framework), NP_091 (network → spacetime),
  NP_005 (missing synchronization), NP_007 (coupling field), NP_075 (force ontology), NP_081
  (energy ontology), NP_089 (cubic dispersion), NP_090 (network ontology).
- AT-QG: QG28 (propagation law — null geodesics), QG31 (TRM propagator origin), QG29 (local
  tick), QG216 (Born |ψ|² = ρ), QG286 (Difference duality).
