# ResearchY-NP_160 — Organizational Field Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_160 (permanent)
**Title:** Organizational Field Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_160.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_148 (property
programming), NP_154 (contact network softening), NP_157 (organization vs material), NP_158 (latent
organization state), NP_159 (structural training)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_160_Tests.cs`

---

## Purpose

NP_157–159 established that properties are readouts of organization, that granite possesses latent
states, and that it can be structurally trained. NP_160 asks the *field* question: **can a granite-like
block support spatially varying organizational states — a programmable internal organizational field,
rather than a single global state?** Program: (1) divide a 1 m block into regions; (2) determine
whether different training histories can be written into different regions; (3) test whether state A can
coexist with state B; (4) measure local stiffness / damping / fracture response; (5) determine whether
organization behaves like a field variable; (6) search for limits (state diffusion, relaxation,
coupling). **Success criterion:** determine whether a material can contain a programmable internal
organizational field rather than a single global state. No new primitives; canonical AT unchanged.

---

## 1. Divide the block into regions

A 1 m granite block can be partitioned into sub-volumes, each with its own local contact-network state
(packing fraction, contact density, fabric orientation, crack population). The question is whether
these regional states can be set **independently** and held **simultaneously**.

---

## 2. Different training histories in different regions

**Yes.** Localized loading, compaction, or peening writes a *different* organizational state into the
treated region than the untreated one:

| Technique | Regional write |
|---|---|
| localized preload/compaction | denser, oriented chains in that region |
| surface peening (NP_148) | compressed nanograin layer vs untouched bulk |
| oriented vibration (NP_156) | directed anisotropy in the excited volume |

The standard example is the **peening depth profile**: a continuous gradient from a compressed,
hardened surface to an untouched interior — a spatially varying organizational state written by a
surface-localized protocol.

---

## 3. State A coexisting with state B

**Yes — this is a gradient / functionally graded structure.** A hard, compressed surface state coexists
with a soft, uncompressed interior state, with a continuous transition. Shear bands are another case: a
localized, high-strain organizational state embedded in an otherwise-intact surrounding state. So
spatially distinct organizational states coexist within one block.

---

## 4. Local stiffness / damping / fracture response

| Quantity | Spatial variation? |
|---|---|
| **local stiffness** | yes (surface hardened, interior soft) |
| **local damping** | yes (loose vs dense regions) |
| **local fracture response** | yes (weak-path / hardened-layer location, NP_156) |

These are measured by local indentation and ultrasonic mapping — the field is observable.

---

## 5. Does organization behave like a field variable?

**Yes.** The fabric (packing fraction, contact density, anisotropy) is a **field φ(x)** — the standard
continuum description of granular/rock fabric (fabric tensor field). The organizational state varies
continuously in space; "organization as a field" is the known continuum-thermomechanical view, not a
new object.

---

## 6. Limits: diffusion, relaxation, coupling

| Limit | Effect |
|---|---|
| **state diffusion** | sharp gradients relax (annealing/aging blurs boundaries) |
| **relaxation** | metastable regional states decay toward the global minimum over time |
| **coupling** | adjacent regions interact through stress redistribution (a strong region unloads a weak one) |

These bound how *sharp* and *persistent* a programmed field can be; they are the known
gradient-stability constraints.

---

## Theorem

> **Theorem (NP_160).** A granite-like block CAN support a spatially varying organizational state — a
> programmable internal organizational field — and this is KNOWN PHYSICS. Different training histories
> can be written into different regions (localized compaction, peening depth profiles, oriented
> vibration), and distinct organizational states (hard compressed surface vs soft interior; shear bands)
> coexist within one block, with spatially varying local stiffness, damping, and fracture response. The
> organizational state is a fabric FIELD φ(x) — the standard continuum fabric-tensor description of
> granular/rock — bounded by state diffusion, relaxation, and inter-region coupling. AT's "organizational
> field" is an INTERPRETATION of this known gradient/fabric-field physics. Classification: spatial field
> KNOWN PHYSICS; "organizational field" framing AT INTERPRETATION; fine-grained reversible field
> programming in consolidated rock AT QUESTION (damage-bounded, NP_155). Proof: (1) Divide (Section 1).
> (2) Regional writes (Section 2). (3) Coexistence (Section 3). (4) Measure (Section 4). (5) Field
> (Section 5). (6) Limits (Section 6). **Success criterion: programmable internal field — KNOWN PHYSICS
> (gradient/fabric field).** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Divide. (2) Regional. (3) Coexist. (4) Measure. (5) Field. (6) Limits. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "a block has one global state" | peening depth profiles and shear bands show spatially distinct states |
| "the organizational field is new" | fabric tensor / gradient / functionally graded structures are established |
| "fields are indefinitely sharp" | diffusion/relaxation/coupling bound gradient persistence |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| spatial organizational field (KNOWN) | a material whose local stiffness/damping are spatially uniform under all regional protocols |
| regional states coexist | a material that relaxes any gradient instantaneously to a single global state |

---

## 9. Classification

| Component | Status |
|---|---|
| spatially varying organizational state (field) | **KNOWN PHYSICS** (gradient / fabric field) |
| regional coexistence (state A + state B) | **KNOWN PHYSICS** (peening profiles, shear bands) |
| "organizational field" framing | **AT INTERPRETATION** |
| fine-grained reversible field programming in consolidated rock | **AT QUESTION** (damage-bounded, NP_155) |

**Conclusion.** A granite-like block **can contain a programmable internal organizational field** —
spatially varying organizational states written by regional training histories (peening depth profiles,
oriented vibration, localized compaction), with coexisting distinct states and spatially varying
stiffness/damping/fracture response. This is KNOWN PHYSICS (gradient/fabric-field continuum mechanics);
AT's "organizational field" is an INTERPRETATION of it, and the fine-grained *reversible* programming of
such a field in consolidated rock remains an AT QUESTION (damage-bounded, NP_155). No new primitive;
canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_160_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_160_Divide` | block → regions | ✅ |
| `Y_NP_160_RegionalWrite` | different histories per region | ✅ |
| `Y_NP_160_Coexist` | state A + state B coexist | ✅ |
| `Y_NP_160_Measure` | local stiffness/damping/fracture | ✅ |
| `Y_NP_160_Field` | organization = field variable φ(x) | ✅ |
| `Y_NP_160_Limits` | diffusion / relaxation / coupling | ✅ |
| `Y_NP_160_Classification` | KNOWN PHYSICS; framing INTERPRETATION | ✅ |
| `Y_NP_160_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_160"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_148 (property programming), NP_154 (contact
  network softening), NP_157 (organization vs material), NP_158 (latent organization state), NP_159
  (structural training).
- Real-world record: peening/UNSM depth profiles (surface→bulk gradient); functionally graded materials;
  shear-band localization; fabric-tensor continuum description of granular/rock; local indentation and
  ultrasonic mapping.
