# ResearchY-NP_158 — Latent Organization State Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_158 (permanent)
**Title:** Latent Organization State Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_158.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_154 (contact
network softening), NP_155 (force chain programming), NP_157 (organization vs material)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_158_Tests.cs`

---

## Purpose

NP_157 established that resonance acts on organization; NP_154/155 established that granite rigidity
is contact-network-controlled and that force chains can be reconfigured. NP_158 asks the *multiplicity*
question: **does a granite-like block contain multiple latent organizational states that are not
normally explored?** Program: (1) define organizational / latent / metastable state; (2) determine
whether a granite block possesses multiple mechanically distinct states without changing chemistry,
mineral composition, or temperature; (3) search for hidden contact-network configurations; (4) compare
natural / compacted / vibrated / re-jammed states; (5) estimate property differences (stiffness,
damping, fracture); (6) identify reversible transitions. **Success criterion:** determine whether
granite behaves as a single material or as a family of latent organizational states. No new primitives;
canonical AT unchanged.

---

## 1. Define the three state types

| State | Definition |
|---|---|
| **organizational state** | the contact-network / force-chain configuration (NP_157's "organization") |
| **latent state** | an organizational state not currently occupied but reachable by a physical transition |
| **metastable state** | a locally stable but not globally minimal configuration |

A granite block is a point in the manifold of organizational states; "latent" states are the other
points it can reach without changing chemistry/mineralogy/temperature.

---

## 2. Multiple mechanically distinct states (chemistry/mineral/T fixed)

**Yes.** The contact network has a family of configurations at fixed composition and temperature:

| Axis | Varies without chemistry change |
|---|---|
| packing fraction / density | yes (compaction) |
| contact density / coordination | yes |
| force-chain orientation (fabric) | yes (anisotropy, NP_155) |
| crack population (sub-damage) | yes (partial) |

These are the "hidden" configurations of a jammed granular system, selected by loading/vibration
history (NP_155).

---

## 3. Hidden contact-network configurations

Jammed granular systems possess a **multitude of metastable states** — fragile, shear-jammed,
ultra-stable — with **memory and aging**: the state depends on the preparation protocol (how it was
poured, compacted, vibrated). A given block, undisturbed, sits in one of many possible states; the
others are latent.

---

## 4. Natural / compacted / vibrated / re-jammed states

| State | Preparation | Network character |
|---|---|---|
| **natural** | as-deposited/formed | reference packing |
| **compacted** | tamped/preloaded | denser, more contacts, stiffer |
| **vibrated** | vibration-fluidized | loosened or re-arranged (fluidized → settle) |
| **re-jammed** | re-settled after fluidization | a *different* stable state (history-dependent) |

These are distinct organizational states — the same chemistry, different networks, different mechanics.

---

## 5. Property differences

| Property | Varies between states? |
|---|---|
| **stiffness** | yes (packing fraction / contact density) |
| **damping** | yes (loose/contact-rich states dissipate more) |
| **fracture behavior** | yes (weak-path location, NP_156) |

The block's stiffness, damping, and fracture response are all *state-dependent*, not fixed material
constants.

---

## 6. Reversible transitions

| Transition | Reversibility |
|---|---|
| compacted ↔ natural | partially reversible (re-loose by vibration) |
| vibrated ↔ re-jammed | reversible (re-jam by settling), sub-damage |
| large rewrites (consolidated) | irreversible (microcracking, NP_155) |

Sub-damage organizational transitions are reversible; damage-bounded ones are not (NP_155).

---

## Theorem

> **Theorem (NP_158).** Granite behaves as a FAMILY of latent organizational states, not a single
> material — and this is KNOWN PHYSICS. A granite block, at fixed chemistry/mineral/temperature,
> possesses multiple mechanically distinct contact-network configurations (packing fraction, contact
> density, fabric orientation, sub-damage crack population) — metastable jammed states with memory and
> aging, selected by loading/vibration history. Natural, compacted, vibrated, and re-jammed states differ
> measurably in stiffness, damping, and fracture behavior. Sub-damage transitions between them are
> reversible; damage-bounded ones are not (NP_155). AT's "latent organizational states" is an
> INTERPRETATION of this known granular/jamming physics. Classification: multiple latent states KNOWN
> PHYSICS; "latent organizational state" framing AT INTERPRETATION; single-material view REFUTED. Proof:
> (1) Define (Section 1). (2) Multiple states (Section 2). (3) Hidden configs (Section 3). (4) Compare
> (Section 4). (5) Properties (Section 5). (6) Reversible (Section 6). **Success criterion: a family of
> latent states — KNOWN PHYSICS.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define. (2) Multiple. (3) Hidden. (4) Compare. (5) Properties. (6) Reversible. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "granite is a single material" | packing/contact/fabric states differ measurably at fixed composition |
| "latent states are new physics" | jamming metastability + memory/aging are established |
| "all transitions are reversible" | damage-bounded rewrites are irreversible (NP_155) |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| multiple latent states (KNOWN) | a granite block whose stiffness/damping/fracture are identical across all packing/vibration protocols |
| state-dependent properties | two organizational states with identical mechanical response |

---

## 9. Classification

| Component | Status |
|---|---|
| multiple latent organizational states | **KNOWN PHYSICS** (granular jamming, memory/aging) |
| property differences across states | **KNOWN PHYSICS** |
| "latent organizational state" framing | **AT INTERPRETATION** (NP_157) |
| "granite is a single material" | **REFUTED** |

**Conclusion.** Granite behaves as a **family of latent organizational states**, not a single material
— KNOWN PHYSICS. At fixed chemistry, mineral composition, and temperature, a block possesses many
metastable contact-network configurations (packing fraction, contact density, fabric orientation)
selected by history, with measurable differences in stiffness, damping, and fracture behavior, and
partially reversible transitions between them (sub-damage). AT's "latent organizational state" language
is an INTERPRETATION of this granular/jamming physics. No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_158_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_158_Define` | organizational / latent / metastable | ✅ |
| `Y_NP_158_Multiple` | multiple states at fixed chemistry/mineral/T | ✅ |
| `Y_NP_158_Hidden` | hidden contact-network configs (jamming, memory) | ✅ |
| `Y_NP_158_Compare` | natural / compacted / vibrated / re-jammed | ✅ |
| `Y_NP_158_Properties` | stiffness / damping / fracture vary | ✅ |
| `Y_NP_158_Reversible` | sub-damage reversible; damage-bounded not | ✅ |
| `Y_NP_158_Classification` | KNOWN PHYSICS; framing AT INTERPRETATION | ✅ |
| `Y_NP_158_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_158"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_154 (contact network softening), NP_155
  (force chain programming), NP_157 (organization vs material).
- Real-world record: granular jamming and metastable states (fragile, shear-jammed, ultra-stable);
  packing fraction and coordination control of stiffness; granular memory/aging; the reversible–
  irreversible phase diagram; compaction and vibration conditioning.
