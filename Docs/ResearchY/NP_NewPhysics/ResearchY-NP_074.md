# ResearchY-NP_074 — Quantum Number Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_074 (permanent)
**Title:** Quantum Number Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_074.md`
**Depends on:** AT-QG QG161 (gauge origin: 1+3+8 generators), QG150 (isospin-constrained mode
access), QG79 (color count = postulate), QG154 (neutrino neutral-charge), QG162 (couplings),
QG173/209 (masses), ResearchY-NP_072/073 (particle/resonance selection)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_074_Tests.cs`

---

## Purpose

NP_072/073 established particles = resonance classes selected by quantum numbers. NP_074 asks
**what quantum numbers themselves are** — charge, isospin, color, hypercharge — as AT objects,
not Standard Model labels. Program: (1) inventory; (2) determine mode labels / occupancy-access
rules / symmetry charges / correspondence objects; (3) trace each to the earliest AT object;
(4) test whether observed quantum numbers follow uniquely; (5) state what an electron's charge
physically represents. No new primitives; canonical AT unchanged.

---

## 1. Inventory — the quantum numbers and their AT origin

| Quantum number | AT origin | Status |
|---|---|---|
| **charge** (U(1)) | the rotation subgroup Z_96 ⊂ D96 is the **photon charge** (QG161) | DERIVED |
| **isospin** (SU(2)) | the Z2 doublet structure: reflection = σ_z (T3), rotation = σ_y, commutator = σ_x (3 generators, algebra closes) | DERIVED |
| **color** (SU(3)) | su(3) = 3²−1 = 8 from the 3 octave families; **color-count 3 = postulate** (QG79) | structure DERIVED, count BOUNDARY |
| **hypercharge** | Y = Q − T3 (the U(1)×SU(2) combination) | DERIVED (combination) |

The gauge symmetry is **1+3+8 = 12 generators** — the 12 link-directions of C_96(±1..±6) ARE the
gauge generators (QG161).

---

## 2. What are quantum numbers — A/B/C/D?

| Interpretation | Verdict |
|---|---|
| **A) mode labels** | **PARTIAL.** Quantum numbers label a particle's band/mode, but that is derivative. |
| **B) occupancy-access rules** | **YES.** QG150: mode access is isospin-constrained (r = 0.955) — the quantum numbers select which part of the spectrum a particle accesses. |
| **C) symmetry charges** | **YES — the deepest.** The quantum numbers are the generators of the D96 automorphism group (U(1) = rotation, SU(2) = doublet, SU(3) = 3-family). |
| **D) correspondence objects** | **PARTIAL.** Only the color-*count* (3) retains a postulate trace (QG79); the rest are derived. |

**Determination: C = B.** Quantum numbers are **symmetry charges** (generators of the D96
automorphism group) that act as **occupancy-access rules** (selecting which part of the spectrum
a particle accesses).

---

## 3. Trace each quantum number to the earliest object

```
D96 automorphism group (C_96(±1..±6), 12 link-directions)
   ├── U(1) = Z_96 rotation    → charge (the photon charge)
   ├── SU(2) = Z2 doublet       → isospin (T3 = reflection σ_z)
   ├── SU(3) = 3-family         → color (8 gluons; count 3 = postulate, QG79)
   └── U(1)_Y                  → hypercharge (Y = Q − T3)
```

**Charge and isospin are DERIVED** (the rotation and doublet subgroups); **color's structure is
derived** (8 gluons) but its **count (3) is a BOUNDARY postulate** (QG79); **hypercharge is a
derived combination** (Y = Q − T3).

---

## 4. Do the observed quantum numbers follow uniquely?

| Quantum number | Unique? |
|---|---|
| charge (U(1)) | **YES** — the Z_96 rotation subgroup is unique |
| isospin (SU(2)) | **YES** — the Z2 doublet structure is unique |
| color structure (su(3), 8) | **YES** — 3²−1 = 8 from the 3 families |
| **color count (3)** | **NO** — a postulate (baryon statistics force 3 colors, not derived, QG79) |

**Charge and isospin follow uniquely; the color count does not** (it is the single
quantum-number boundary).

---

## 5. What is an electron's charge in AT?

The electron's charge **−1** is the electron's **eigenvalue under the U(1) rotation subgroup**
(Z_96 ⊂ D96) — the "photon charge" (QG161). It is a **symmetry charge** (a Noether charge of the
rotation symmetry), which acts as an **occupancy-access rule**: it is why the electron (the
neutral-charge-neighbor neutrino is neutral via QG154) accesses the lowest octave band. The
electron's charge is therefore a **D96 rotation quantum number**, not a SM label.

---

## Theorem

> **Theorem (NP_074).** Quantum numbers in AT are the GENERATORS of the D96 automorphism group
> — symmetry charges (C) that act as occupancy-access rules (B). Charge = the U(1) = Z_96 rotation
> subgroup (the photon charge); isospin = the SU(2) = Z2 doublet structure; color = su(3) = 3²−1
> = 8 from the 3 families (structure DERIVED, count 3 = a BOUNDARY postulate, QG79); hypercharge
> = the derived U(1)×SU(2) combination (Y = Q − T3). Proof: (1) Inventory (Section 1): charge and
> isospin DERIVED, color-structure DERIVED, color-count BOUNDARY. (2) Interpretations (Section 2):
> quantum numbers = symmetry charges (C) = occupancy-access rules (B); mode labels and
> correspondence objects PARTIAL. (3) Trace (Section 3): 1+3+8 = 12 generators = the 12 link-
> directions of C_96(±1..±6). (4) Uniqueness (Section 4): charge/isospin/color-structure unique;
> color-count NOT. (5) Electron charge (Section 5): the U(1) rotation eigenvalue. Classification:
> charge (U(1)) DERIVED (QG161); isospin (SU(2)) DERIVED (QG161); color structure DERIVED, color
> count BOUNDARY (QG79); hypercharge DERIVED; quantum numbers as SM labels alone REFUTED (they
> are AT symmetry charges). **Success criterion: a quantum number is a symmetry charge of the D96
> automorphism group — an occupancy-access rule, not a Standard Model label.** No new primitive;
> canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Test A–D. (3) Trace each to D96. (4) Test uniqueness.
> (5) State the electron charge. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "quantum numbers are just SM labels" | they are D96 symmetry charges (generators of the automorphism group) |
| "quantum numbers are mode labels only" | they are deeper: symmetry charges that act as access rules |
| "the color count is derived" | QG79: the 3-color count is a postulate (baryon statistics), a boundary |
| "hypercharge is a new primitive" | Y = Q − T3, a derived combination of U(1) and SU(2) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| charge = the U(1) rotation subgroup | a charge not expressible as a D96 rotation eigenvalue |
| isospin = the Z2 doublet structure | an isospin not from the doublet-restricted su(2) |
| the color count is a boundary | a derivation of the 3-color count from the 3-family structure alone |

---

## 8. Classification

| Component | Status |
|---|---|
| charge (U(1) = Z_96 rotation) | **DERIVED** (QG161) |
| isospin (SU(2) = Z2 doublet) | **DERIVED** (QG161) |
| color structure (su(3) = 8) | **DERIVED** (QG161) |
| color count (3) | **BOUNDARY** (QG79 postulate) |
| hypercharge (Y = Q − T3) | **DERIVED** (combination) |
| quantum numbers as SM labels only | **REFUTED** |

**Conclusion.** Quantum numbers in AT are **symmetry charges** — the generators of the D96
automorphism group (1+3+8 = 12), which act as **occupancy-access rules** selecting which part of
the spectrum a particle accesses. Charge (U(1) = Z_96 rotation), isospin (SU(2) = Z2 doublet),
color structure (su(3) = 8), and hypercharge (Y = Q − T3) are all DERIVED; only the color **count**
(3) is a BOUNDARY postulate (QG79). The electron's charge is its **U(1) rotation eigenvalue** — a
D96 symmetry charge, not a Standard Model label. No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_074_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_074_Generators` | 1+3+8 = 12 gauge generators | ✅ |
| `Y_NP_074_ChargeIsospin` | U(1)=rotation, SU(2)=doublet (derived) | ✅ |
| `Y_NP_074_ColorBoundary` | su(3)=8 derived; count 3 postulate | ✅ |
| `Y_NP_074_Interpretations` | C=B (symmetry charge = access rule) | ✅ |
| `Y_NP_074_ElectronCharge` | charge = U(1) rotation eigenvalue | ✅ |
| `Y_NP_074_Classification` | charge/isospin DERIVED; color-count BOUNDARY | ✅ |
| `Y_NP_074_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_074"`

---

## References

- AT-QG: QG161 (gauge origin), QG150 (isospin mode access), QG79 (color count postulate), QG154
  (neutrino neutral-charge), QG162 (couplings), QG173/209 (masses).
- ResearchY-NP_072/073 (particle/resonance selection).
