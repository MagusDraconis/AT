# ResearchY-NP_155 — Force Chain Programming Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_155 (permanent)
**Title:** Force Chain Programming Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_155.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_137
(real-world evidence), NP_154 (contact network softening)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_155_Tests.cs`

---

## Purpose

NP_154 established that granite rigidity is contact-network-controlled. NP_155 asks the *programming*
question: **can the force-chain network inside a granite-like material be intentionally
reconfigured?** Program: (1) model granite as grains / contacts / force chains / microcracks;
(2) determine whether excitation can break / create / redirect force chains; (3) compare random vs
directed reconfiguration; (4) search for control variables (static preload, vibration direction,
frequency sweep, spatial phase); (5) determine whether multiple stable contact-network states exist;
(6) evaluate stiffness / strength / damping / fracture resistance per state. **Success criterion:**
determine whether a granite block can be switched between distinct mechanical network states without
changing its chemistry. No new primitives; canonical AT unchanged.

---

## 1. Model granite as grains / contacts / force chains / microcracks

As in NP_154, granite is a jammed granular system:

| Element | Role |
|---|---|
| grains | building blocks |
| contacts | grain junctions |
| **force chains** | the filamentary, load-bearing paths (stress concentrates here) |
| microcracks | compliant defects / damage sites |

Force chains are **emergent, filamentary, and non-uniform** — a small fraction of contacts carries
most of the load.

---

## 2. Break / create / redirect force chains

| Operation | Possible? | Mechanism |
|---|---|---|
| **break** a force chain | yes | exceed a contact's strength (slip or crack) |
| **create** a force chain | yes | increase contact/compaction so new chains form |
| **redirect** force chains | yes | change the loading/shear direction; chains re-align |

Force-chain *reconfiguration* is the standard behavior of granular materials under load — DEM and
photoelastic experiments observe chains forming, breaking, and re-aligning continuously.

---

## 3. Random vs directed reconfiguration

| Mode | Outcome |
|---|---|
| **random disruption** (isotropic vibration) | chains scramble; net drift to a denser/fragile state |
| **directed reconfiguration** (oriented shear/load) | chains align along the principal stress direction — **controllable anisotropy** |

Directed reconfiguration is **established**: force-chain anisotropy follows the loading direction, so
orienting the load *programs* the chain orientation.

---

## 4. Control variables

| Variable | Effect |
|---|---|
| **static preload** | sets chain density / compaction |
| **vibration direction** | orients chains (anisotropy) |
| **frequency sweep** | selects which contacts mobilize (broadband, NP_142) |
| **spatial phase pattern** (phased array) | *emerging* — spatial steering of the acoustic field |

The first three are **standard** granular controls; spatial phase steering is an emerging refinement.

---

## 5. Multiple stable contact-network states

**Yes.** Jammed granular systems have a **family of metastable states** (fragile, shear-jammed,
ultra-stable), selected by loading history; they exhibit **memory and aging**. A packing can be
"trained" into a more stable configuration by cyclic shear, or "reset" by reversing it. So distinct,
stable contact-network states exist and are selectable.

---

## 6. Stiffness / strength / damping / fracture resistance per state

| Property | Varies with chain state? |
|---|---|
| **stiffness** | yes — chain density/anisotropy sets bulk modulus |
| **strength** | yes — load-bearing chain continuity |
| **damping** | yes — loose/contact-rich states dissipate more |
| **fracture resistance** | yes — oriented chains resist aligned loads better |

The contact-network state is a **mechanical-state dial** — different states give different property
combinations, exactly as in NP_148/149 (but in the contact, not dislocation, channel).

---

## Theorem

> **Theorem (NP_155).** The force-chain network inside a granite-like material CAN be intentionally
> reconfigured — break, create, and redirect chains — and multiple stable contact-network states exist;
> but a clean, reversible "switch" of a CONSOLIDATED block is bounded by the reversible–irreversible
> transition. Force-chain reconfiguration, directed anisotropy (chains align with load/shear direction),
> metastable jammed states, and granular memory/aging are all KNOWN PHYSICS (DEM, photoelasticity,
> jamming phase diagrams). Reversible reconfiguration holds only below the damage threshold (sub-damage
> fabric rearrangement); above it, reconfiguration proceeds by microcracking — irreversible, chemistry-
> unchanged but damaging. So a loose granular mass is freely "programmable" between metastable states;
> a cemented granite block can be re-steered only within the reversible regime, and large state changes
> cost irreversible damage. Classification: force-chain reconfiguration KNOWN PHYSICS; directed
> anisotropy KNOWN PHYSICS; metastable states KNOWN PHYSICS; reversible "programming" of consolidated
> granite AT QUESTION (bounded by damage). Proof: (1) Model (Section 1). (2) Break/create/redirect
> (Section 2). (3) Random vs directed (Section 3). (4) Controls (Section 4). (5) States (Section 5).
> (6) Properties (Section 6). **Success criterion: reconfiguration possible; reversible switching of a
> consolidated block is damage-bounded.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Model. (2) Reconfigure. (3) Directed. (4) Controls. (5) States. (6) Properties. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "force chains are fixed" | chains form/break/re-align continuously under load (DEM, photoelastic) |
| "no directed control exists" | chain anisotropy follows the load/shear direction |
| "one stable state only" | granular systems have many metastable jammed states + memory/aging |
| "consolidated granite switches freely" | large reconfiguration = microcracking = irreversible damage |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| force chains reconfigurable (KNOWN) | a granular/rock system whose chains are immobile under all loading |
| reversible switching is damage-bounded | a cemented granite block that reconfigures between large distinct states with no microcracking |

---

## 9. Classification

| Component | Status |
|---|---|
| break / create / redirect force chains | **KNOWN PHYSICS** (granular mechanics) |
| directed anisotropy (load/shear direction) | **KNOWN PHYSICS** |
| multiple metastable contact-network states | **KNOWN PHYSICS** (jamming, memory/aging) |
| reversible "programming" of a consolidated granite block | **AT QUESTION** (bounded by the reversible–irreversible transition) |

**Conclusion.** A granite-like force-chain network **can be intentionally reconfigured** — force
chains break, create, and redirect under loading, multiple metastable contact-network states exist
(jamming, memory/aging), and the network can be **directed** via load/shear orientation (anisotropy).
This is KNOWN PHYSICS. The open question is *reversible* programming of a **consolidated** block:
below the damage threshold reconfiguration is reversible (sub-damage fabric); above it, reconfiguration
proceeds by microcracking — irreversible even though chemistry is unchanged. So a loose granular mass
is freely programmable; a cemented granite block can be re-steered only within the reversible regime,
and large state changes cost damage. No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_155_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_155_Model` | grains / contacts / force chains / microcracks | ✅ |
| `Y_NP_155_Reconfigure` | break / create / redirect chains | ✅ |
| `Y_NP_155_Directed` | directed anisotropy beats random disruption | ✅ |
| `Y_NP_155_Controls` | preload / direction / sweep / phase | ✅ |
| `Y_NP_155_States` | multiple metastable states (jamming, memory) | ✅ |
| `Y_NP_155_Properties` | stiffness/strength/damping/fracture vary with state | ✅ |
| `Y_NP_155_Classification` | reconfiguration KNOWN; reversible consolidated = AT QUESTION | ✅ |
| `Y_NP_155_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_155"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_137 (real-world evidence), NP_154 (contact
  network softening).
- Real-world record: force-chain DEM and photoelastic experiments; shear-induced anisotropy;
  stress-force-fabric relation; granular memory/aging and metastable jammed states (fragile, shear-
  jammed, ultra-stable); the reversible–irreversible phase diagram (PNAS 2019); microcracking in
  cemented granular rocks.
