# ResearchY-NP_075 — Force Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_075 (permanent)
**Title:** Force Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_075.md`
**Depends on:** AT-QG QG161 (gauge origin 1+3+8), QG243 (gauge dynamics = generator action),
QG244 (Lagrangian origin), QG57 (bosons = link excitations), QG162 (couplings), QG89 (Noether
conservation), QG197/222 (metric/gravity), QG44/68 (ψ, gravity), ResearchY-NP_072/073/074
(particle/resonance/quantum-number ontology)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_075_Tests.cs`

---

## Purpose

NP_072–074 established particles = resonance classes and quantum numbers = D96 symmetry
charges. NP_075 completes the ontology: **what is a force?** Program: (1) inventory
electromagnetism, weak, strong, gravity; (2) determine whether a force is particle exchange /
symmetry action / occupancy transfer / resonance transition; (3) trace each interaction to the
earliest AT object; (4) test whether gauge bosons are particles / operators / generators;
(5) state what a photon is. No new primitives; canonical AT unchanged.

---

## 1. Inventory — the four forces and their D96 origin

| Force | D96 generator(s) | Boson | Coupling |
|---|---|---|---|
| **electromagnetism** | U(1) = Z_96 rotation (1 gen) | photon | e = √(4π/137) |
| **weak** | SU(2) = Z2 doublet (3 gens) | W±, Z | g = √(4π·3/Σm) |
| **strong** | SU(3) = 3-family (8 gens) | 8 gluons | g_s = √(4π·8/Σ√m) |
| **gravity** | NOT a gauge generator | (ψ graviton) | the metric g = ρ^(2/d)η |

The three gauge forces are the **12 generator actions** (QG161/243); gravity is the **metric
geometry** (QG197/222), not a gauge force.

---

## 2. What is a force — A/B/C/D?

| Interpretation | Verdict |
|---|---|
| **A) particle exchange** | **PARTIAL.** A gauge boson is exchanged, but it is a LINK excitation (QG57), not a matter particle. |
| **B) symmetry action** | **YES.** The interaction IS the generator's action on the modes (QG243): "an interaction IS the generator's action on the mode". |
| **C) occupancy transfer** | **PARTIAL.** The generator acts on the modes, transferring the link excitation (QG63/65). |
| **D) resonance transition** | **YES.** The vertex is the generator matrix element ⟨f\|T^a\|i⟩ — a transition between D96 modes. |

**Determination: B = D.** A force is the **action of a D96 symmetry generator** (a symmetry
action) that induces a **resonance transition** between modes. It is not a particle exchange in
the matter sense.

---

## 3. Trace each interaction to the earliest object

```
D96 automorphism group (12 link-directions, QG161)
   ├── U(1) rotation generator   → photon   → electromagnetism (charge conservation)
   ├── SU(2) doublet generators  → W±, Z    → weak (isospin conservation)
   └── SU(3) 3-family generators → gluons   → strong (color conservation)

D96 count density ρ + ψ tensor    → metric g = ρ^(2/d)η   → gravity (geometry, NOT gauge)
```

Each gauge force is the **Noether-conserved generator action** (QG89: U(1)→charge, SU(2)→isospin,
SU(3)→color), with the **coupling** the D96-normalized generator strength (QG162), and the
**vertex** the generator matrix element ⟨f|T^a|i⟩ (QG243). Gravity is the geometric face — the
curvature of ρ, completed by the ψ tensor.

---

## 4. Are gauge bosons particles, operators, or generators?

| Reading | Verdict |
|---|---|
| particles | **NO** — a gauge boson is a link excitation, not a matter (deficit) particle |
| operators | **PARTIAL** — the generator acts as an operator on the modes |
| **symmetry generators** | **YES — the correct reading.** A gauge boson IS a D96 symmetry generator (a link excitation, QG57). |

**A gauge boson is a symmetry generator** — the link excitation that carries the generator's
action between modes.

---

## 5. What is a photon?

The **photon is the U(1) = Z_96 rotation generator** — the unique neutral rotation link
excitation (QG161/162). It is the symmetry generator whose action is electromagnetism: it carries
the U(1) rotation (the "photon charge") between modes, with vertex ⟨f|Q|i⟩ (charge matrix element)
and coupling e = √(4π/137). It is **not a matter particle** — it is the generator of the rotation
symmetry of the D96 ring.

---

## Theorem

> **Theorem (NP_075).** A force in AT is the ACTION of a D96 symmetry generator — a symmetry
> action (B) that induces a resonance transition between modes (D, the vertex ⟨f|T^a|i⟩). The
> gauge bosons are the generators (link excitations, QG57), not matter particles; the photon is
> the U(1) = Z_96 rotation generator; gravity is the metric geometry, not a gauge force. Proof:
> (1) Inventory (Section 1): EM/weak/strong = the 12 generator actions (QG161/243); gravity = the
> metric (QG197/222). (2) Interpretations (Section 2): particle exchange PARTIAL, symmetry action
> and resonance transition YES (B = D). (3) Trace (Section 3): each force = a Noether-conserved
> generator action with a D96-normalized coupling (QG162/89). (4) Gauge bosons (Section 4): they
> are symmetry generators (link excitations), not particles. (5) Photon (Section 5): the U(1)
> rotation generator. Classification: the gauge forces (generator action) DERIVED (QG161/243/244);
> the gauge bosons (generators) DERIVED (QG57/161); the couplings DERIVED (QG162); gravity DERIVED
> (QG197/222) + ψ primitive; "force = matter-particle exchange" REFUTED. **Success criterion: a
> force is a D96 symmetry generator action — the same ontology as particles (resonance classes)
> and quantum numbers (symmetry charges); the photon is the U(1) rotation generator.** No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Test A–D. (3) Trace to D96. (4) Classify bosons.
> (5) State the photon. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "a force is matter-particle exchange" | the boson is a link excitation (generator), not a matter (deficit) particle |
| "a force is a new primitive" | forces are the generator actions of the same D96 symmetry (QG161/243) |
| "gravity is a gauge force" | gravity is the metric geometry (ρ + ψ), not a gauge generator |
| "the photon is a particle" | the photon is the U(1) rotation generator (link excitation) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| a force is a generator action | an interaction not expressible as a D96 generator action |
| the photon is the U(1) rotation generator | a photon not tied to the Z_96 rotation subgroup |
| gauge bosons are generators, not particles | a gauge boson that is a matter (deficit) particle |

---

## 8. Classification

| Component | Status |
|---|---|
| gauge forces (generator action) | **DERIVED** (QG161/243/244) |
| gauge bosons (link excitations / generators) | **DERIVED** (QG57/161) |
| couplings (e, g, g_s) | **DERIVED** (QG162) |
| gravity (the metric) | **DERIVED** (QG197/222) + ψ primitive (QG44) |
| "force = matter-particle exchange" | **REFUTED** |

**Conclusion.** A force in AT is the **action of a D96 symmetry generator** — a symmetry action
that induces a resonance transition between modes (vertex ⟨f|T^a|i⟩). Electromagnetism is the
U(1) rotation generator (the photon), the weak force the SU(2) doublet generators (W/Z), the
strong force the SU(3) 3-family generators (gluons), and gravity the metric geometry (ρ + ψ) —
not a gauge force. The gauge bosons are the **symmetry generators** (link excitations), not
matter particles. Forces therefore share the same ontology as particles (resonance classes) and
quantum numbers (symmetry charges): everything is the D96 structure in action. No new primitive;
canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_075_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_075_ForcesAreGeneratorActions` | EM/weak/strong = 1+3+8 generator actions | ✅ |
| `Y_NP_075_Interpretations` | B=D (symmetry action = resonance transition) | ✅ |
| `Y_NP_075_BosonsAreGenerators` | link excitations, not matter particles | ✅ |
| `Y_NP_075_PhotonIsRotation` | photon = U(1) = Z_96 rotation generator | ✅ |
| `Y_NP_075_GravityIsGeometry` | gravity = metric, not a gauge force | ✅ |
| `Y_NP_075_Classification` | gauge forces DERIVED; particle-exchange REFUTED | ✅ |
| `Y_NP_075_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_075"`

---

## References

- AT-QG: QG161 (gauge origin 1+3+8), QG243 (gauge dynamics = generator action), QG244 (Lagrangian
  origin), QG57 (bosons = link excitations), QG162 (couplings), QG89 (Noether), QG197/222
  (metric/gravity), QG44/68 (ψ, gravity).
- ResearchY-NP_072/073/074 (particle/resonance/quantum-number ontology).
