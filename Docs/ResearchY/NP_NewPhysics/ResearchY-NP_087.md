# ResearchY-NP_087 — Nuclear Structure Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_087 (permanent)
**Title:** Nuclear Structure Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_087.md`
**Depends on:** ResearchY-NP_071 (matter ontology), NP_072 (particle ontology — proton = quark
composite), NP_073 (resonance selection), NP_074 (quantum numbers — isospin doublet), NP_085
(completeness frontier — nuclear structure MISSING), AT-QG QG161/242 (su(3) = 8 generators),
QG75 (force = generator action), QG210 (families), D_041 (D96 spectrum)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_087_Tests.cs`

---

## Purpose

NP_085 flagged nuclear structure as a MISSING domain. NP_087 asks whether that verdict holds under
closer inspection: **can the Actualization Theory explain nuclear structure — binding energies,
magic numbers, shell structure?** Program: (1) inventory proton / neutron / deuteron / helium;
(2) compare the shell model, the liquid-drop model, and the AT resonance ontology; (3) test magic
numbers, nuclear stability, and binding-energy trends; (4) determine whether nuclei are resonance /
deficit / symmetry composites, or correspondence-only. **Success criterion:** identify the AT
ontology of nuclei. No new primitives; canonical AT unchanged.

---

## 1. Inventory — the nucleons and light nuclei

| Object | Content | AT status |
|---|---|---|
| **proton** (uud, 938.27 MeV) | a bound composite of quark modes | **DERIVED** (NP_072 — a resonance-class composite) |
| **neutron** (udd, 939.57 MeV) | the isospin-down partner of the proton | **DERIVED** as a composite; the n−p mass gap (1.29 MeV) is a small isospin-breaking/EM effect — NOT derived |
| p/n isospin doublet | SU(2) Z2 doublet (NP_074) | **MAPPED** (the Z2-paired sector) |
| **deuteron** (np, binding 2.224 MeV) | a bound proton+neutron | **NOT DERIVED** (needs the residual strong force) |
| **helium-4** (2p2n, 28.3 MeV) | the most tightly bound light nucleus | **NOT DERIVED** |

The *nucleons* are mapped (derived quark composites forming an isospin doublet); the *nuclei*
(multi-nucleon bound states) are not.

---

## 2. Shell model / liquid-drop / AT resonance ontology

| Model | What it gives | AT counterpart |
|---|---|---|
| **shell model** | magic numbers 2, 8, 20, 28, 50, 82, 126 from 3D spherical-harmonic shells + spin-orbit | **NONE.** The D96 spectrum is 1D with mirror-pair (O(2)) degeneracies, not (2l+1) spherical harmonics. |
| **liquid-drop model** | bulk binding from surface/volume/Coulomb/symmetry terms | **NONE.** No nuclear force or surface term is derived. |
| **AT resonance ontology** | particles = D96 resonance classes; forces = generator actions | gives the nucleons (as quark composites) and the strong coupling (α_strong = 8/Σ√m as a correspondence), but NOT the nuclear force or shell structure |

**The structural mismatch is decisive:** the D96 ring is one-dimensional — its 95 modes form
47 mirror pairs (O(2) doublets) plus one central mode. Nuclear shells are three-dimensional —
their degeneracies are (2l+1) spherical harmonics, closed by spin-orbit into the magic numbers.
These are *different* degeneracy structures, and the 1D ring does not generate the 3D shell
structure.

---

## 3. Test — magic numbers, stability, binding-energy trends

| Test | Result |
|---|---|
| **magic numbers** [2, 8, 20, 28, 50, 82, 126] | **NOT REPRODUCED.** The harmonic-oscillator closure (2, 8, 20) and spin-orbit closure (28, 50, 82, 126) are 3D spherical-harmonic facts; the D96 octave structure [4,4,87] has no shell closures. |
| **nuclear stability** (the valley of β-stability, N/Z ≈ 1→1.5) | **NOT DERIVED.** No nuclear binding or Coulomb term to produce the stability curve. |
| **binding-energy trends** (per-nucleon rise to Fe-56, ~8.8 MeV, then fall) | **NOT DERIVED.** No liquid-drop volume/surface/Coulomb/asymmetry terms. |
| **deuteron / α-particle binding** | **NOT DERIVED.** The residual strong force (pion exchange between nucleons) is unmapped. |

**The three central nuclear-structure observables — magic numbers, stability, binding-energy
trends — are all absent.** The theory supplies the nucleons (quark composites) and the strong
coupling (a spectral correspondence), but none of the many-body nuclear physics.

---

## 4. A / B / C / D

| Determination | Verdict |
|---|---|
| **A) resonance composites** | **NO.** The *nucleons* are resonance classes (quark composites), but a *nucleus* is a multi-nucleon bound state — it is not itself a D96 mode. |
| **B) deficit composites** | **NO.** The deficit m = ρ̄ − ρ is the bulk under-occupancy (the dark-matter side); it is not the nuclear binding between nucleons. |
| **C) symmetry composites** | **PARTIAL.** The p/n isospin doublet IS a symmetry composite (the Z2 pair), but the *binding* of nucleons into nuclei is not a symmetry fact — it is the residual strong force. |
| **D) correspondence only** | **YES.** The ingredients (nucleons, isospin doublet, quark masses, the strong coupling) are mapped as correspondences; the nuclear structure is not derived. |

**Determination: D — nuclei are correspondence-only.** The nucleons are derived quark composites,
and the strong coupling is a spectral correspondence, but nuclear structure is MISSING.

---

## Theorem

> **Theorem (NP_087).** Nuclei in AT are CORRESPONDENCE-ONLY: the nucleons (proton, neutron) are
> derived quark composites forming an isospin doublet, and the strong coupling is a spectral
> correspondence (α_strong = 8/Σ√m), but the nuclear STRUCTURE — binding energies, magic numbers
> [2, 8, 20, 28, 50, 82, 126], and shell structure — is NOT derived. Proof: (1) Inventory
> (Section 1): nucleons DERIVED, deuteron/helium NOT. (2) Comparison (Section 2, verified): the
> D96 spectrum is 1D with 47 mirror-pair (O(2)) degeneracies, while nuclear shells are 3D
> (2l+1) spherical harmonics + spin-orbit — a genuine structural mismatch. (3) Tests (Section 3):
> magic numbers, stability, and binding-energy trends are all absent. (4) Determination
> (Section 4): A (resonance composite) and B (deficit composite) REFUTED; C (symmetry composite)
> PARTIAL (isospin doublet only); D (correspondence only) YES. Classification: the nucleons and
> isospin DERIVED (NP_072/074); the strong coupling CORRESPONDENCE (NP_085); nuclear structure
> MISSING (binding, magic numbers, shells). **Success criterion: the AT ontology of nuclei is
> correspondence-only — the nucleons are mapped, the nuclei are not — and nuclear structure
> remains a MISSING domain that does not follow from the 1D D96 ring.** No new primitive; canonical
> AT unchanged.
>
> *Proof sketch.* (1) Inventory the nucleons and light nuclei. (2) Compare the three models and
> expose the 1D-vs-3D mismatch. (3) Test magic numbers / stability / binding. (4) Decide A–D. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "nuclei are resonance composites" | the nucleons are resonance classes, but a nucleus is a multi-nucleon bound state, not a D96 mode |
| "nuclei are deficit composites" | the deficit is bulk/dark matter, not the nuclear force between nucleons |
| "magic numbers follow from D96" | D96 has 1D mirror-pair (O(2)) degeneracies, not 3D (2l+1) spherical-harmonic shells |
| "the nuclear force is derived" | the strong coupling is a spectral correspondence, not a derivation of pion exchange between nucleons |
| "nuclear structure is a small correction" | binding energies, magic numbers, and the stability curve are the *core* of nuclear physics, not a minor detail |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| nuclear structure is MISSING | a magic-number or binding-energy trend derived from the D96 spectrum |
| the D96 ring does not generate 3D shells | a 1D mirror-pair spectrum whose closure numbers match the 3D magic numbers |
| nuclei are correspondence-only | a nucleus (deuteron, helium) derived as a D96 resonance or deficit composite |
| the strong coupling is not a derivation | a nuclear-force derivation from the su(3) generator action alone |

---

## 8. Classification

| Component | Status |
|---|---|
| the nucleons (proton/neutron) as quark composites | **DERIVED** (NP_072) |
| the p/n isospin doublet (Z2 pair) | **DERIVED** (NP_074) |
| the strong coupling α_strong = 8/Σ√m | **CORRESPONDENCE** (spectral ratio) |
| nuclear binding energies | **MISSING** |
| magic numbers [2, 8, 20, 28, 50, 82, 126] | **MISSING** |
| nuclear shell structure | **MISSING** |

**Conclusion.** Actualization Theory explains the *nucleons* — the proton and neutron are derived
quark composites forming an isospin doublet, and the strong coupling is a spectral correspondence —
but it does **not** explain *nuclear structure*: the binding energies, the magic numbers
[2, 8, 20, 28, 50, 82, 126], and the shell structure are all absent. The reason is structural: the
D96 ring is one-dimensional with mirror-pair (O(2)) degeneracies, while nuclear shells are
three-dimensional with (2l+1) spherical-harmonic degeneracies closed by spin-orbit. **Nuclei are
correspondence-only (D): the ingredients are mapped, the structure is missing.** This confirms
NP_085's frontier verdict, with a sharper diagnosis: nuclear structure is the clearest large
physics domain that does *not* follow from the D96 ontology. No new primitive; canonical AT
unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_087_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_087_Inventory` | nucleons DERIVED; deuteron/helium NOT | ✅ |
| `Y_NP_087_StructuralMismatch` | D96 1D mirror pairs vs 3D spherical harmonics | ✅ |
| `Y_NP_087_MagicNumbers` | [2,8,20,28,50,82,126] not reproduced | ✅ |
| `Y_NP_087_BindingTrends` | no volume/surface/Coulomb/asymmetry terms | ✅ |
| `Y_NP_087_ABCD` | D (correspondence only); A/B refuted, C partial | ✅ |
| `Y_NP_087_Classification` | nucleons/isospin DERIVED; strong coupling CORRESPONDENCE; structure MISSING | ✅ |
| `Y_NP_087_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_087"`

---

## References

- ResearchY-NP_071 (matter ontology), NP_072 (particle ontology), NP_073 (resonance selection),
  NP_074 (quantum numbers), NP_085 (completeness frontier).
- AT-QG: QG161/242 (su(3) = 8 generators), QG75 (force = generator action), QG210 (families),
  D_041 (D96 spectrum).
