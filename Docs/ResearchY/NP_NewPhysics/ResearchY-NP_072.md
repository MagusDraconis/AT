# ResearchY-NP_072 — Particle Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_072 (permanent)
**Title:** Particle Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_072.md`
**Depends on:** AT-QG QG173 (u-quark mass), QG181 (M_Pl = v·A³), QG209 (lepton hierarchy),
QG210 (family index = octave band), QG150 (mode access), QG178 (electron g-2), QG126
(sector-particle mapping), D_012/D_013 (m_e anchor), D_041 (spectrum), ResearchY-NP_071
(matter = deficit)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_072_Tests.cs`

---

## Purpose

NP_071 established matter = the deficit (a stable self-bound excitation). NP_072 asks the
follow-up: **if matter is a deficit excitation, what are particles — and what is an electron?**
Program: (1) inventory the electron, muon, tau, quarks, proton; (2) determine whether particles
are localized deficits / resonance classes / occupancy configurations / measurement observables;
(3) compare the matter ontology with the particle ontology; (4) determine whether a particle is
fundamental or emergent. No new primitives; canonical AT unchanged.

---

## 1. Inventory — the particles and their D96 content

| Particle | D96 content | Status |
|---|---|---|
| **electron** (m_e = 0.511 MeV) | the lightest lepton, at the **octave bottom** (occ₀ = 4) | **ANCHOR** (BOUNDARY, D_012/D_013) |
| **muon** (m_μ/m_e ≈ 207) | m_e × a D96 ratio (Σm²/√occMom ≈ 207, QG209) | DERIVED |
| **tau** (m_τ/m_e ≈ 3477) | m_e × a higher octave-band ratio | DERIVED |
| **quarks** (u, d, …) | m_e × D96 spectral ratios (QG173) | DERIVED |
| **proton** | a bound composite of quark modes | DERIVED (composite) |

The **electron is the anchor**: its mass fixes the fermion scale, and every other mass is
m_e × a dimensionless D96 ratio. The **families are the octave bands** [4,4,87] (QG210).

---

## 2. What are particles — A/B/C/D?

| Interpretation | Verdict |
|---|---|
| **A) localized deficits** | **NO.** The deficit is matter *as a whole* (the bulk under-occupancy); individual particles are not localized deficit lumps. |
| **B) resonance classes** | **YES.** Particles are the D96 spectrum's **modes/frequencies** — "frequency attractors", "stable fixed points" (the legacy: "particles = frequency attractors"). |
| **C) occupancy configurations** | **YES.** Each particle maps to a **mode-access class** of the occupancy [4,4,87] (QG150: occupation-weighted, isospin-constrained). |
| **D) measurement observables** | **PARTIAL.** Particles exist as modes *before* measurement; measurement realizes them, but they are modes first. |

**Determination: B = C.** A particle is a **resonance class** — an occupancy/mode configuration
of the D96 spectrum — not a localized deficit and not a mere measurement artifact.

---

## 3. Matter ontology vs particle ontology

| Level | Object | Ontology |
|---|---|---|
| **matter** (bulk) | the deficit m = ρ̄ − ρ | the under-occupancy — a stable self-bound excitation |
| **particle** (individual) | a mode/resonance of the D96 spectrum | a frequency attractor — a resonance class in the octave bands |

The two are **complementary**: matter is the *deficit over the modes*; a particle is a *single
mode* (or mode class) of the underlying spectrum. The electron is the lowest fermion mode; the
muon and tau are higher octave-band modes; the quarks are other mode classes.

---

## 4. Fundamental or emergent?

| Aspect | Status |
|---|---|
| the spectrum (95 modes) | **DERIVED** (D_041 — emergent from the D96 ring) |
| the family structure (3 octave bands) | **DERIVED** (QG210) |
| the mass *ratios* (m_μ/m_e, …) | **DERIVED** (QG209/173) |
| the absolute mass scale (m_e) | **BOUNDARY** (anchor, D_012/D_013) |

**A particle is EMERGENT** (a derived mode of the D96 spectrum), but its absolute mass rests on
the **boundary anchor m_e**. The electron is both: an emergent *mode* (the lightest fermion) and
a boundary *anchor* (its mass is not derived).

---

## Theorem

> **Theorem (NP_072).** A particle in AT is a RESONANCE CLASS — a mode (frequency attractor) of
> the D96 spectrum, organized into octave-band families, whose mass is an anchor × a
> dimensionless D96 ratio. The electron is the lightest fermion mode (the octave bottom, occ₀ =
> 4), and its mass m_e is the boundary anchor; every other particle is a derived ratio of it.
> Proof: (1) Inventory (Section 1): e/μ/τ/quarks all reduce to m_e × D96 ratios; families = octave
> bands. (2) Interpretations (Section 2): localized deficits REFUTED; resonance classes and
> occupancy configurations YES (B = C); measurement observables PARTIAL. (3) Matter vs particle
> (Section 3): matter = the deficit over the modes; a particle = a single mode. (4) Emergence
> (Section 4): modes, families, and ratios DERIVED; m_e BOUNDARY. Classification: the particle
> modes DERIVED (D_041/QG210); the mass ratios DERIVED (QG173/209); the electron mass m_e BOUNDARY
> (anchor); "particle = localized point object" REFUTED; "particle = fundamental" REFUTED.
> **Success criterion: an electron is the lightest fermion mode of the D96 spectrum — a resonance
> class (a stable frequency attractor) at the octave bottom, whose mass is the boundary anchor
> setting the fermion scale.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Test A–D. (3) Contrast matter/particle. (4) Classify
> emergence. ∎

---

## 5. Counterexamples

| Attempt | Why it fails |
|---|---|
| "a particle is a localized deficit" | the deficit is bulk matter; particles are individual modes |
| "a particle is a point object" | particles are frequency attractors / resonance classes, not points |
| "a particle is fundamental" | the modes are derived; only the m_e anchor is boundary |
| "a particle is only a measurement artifact" | the modes exist in the spectrum before measurement |
| "matter and particle are the same object" | matter = the deficit over the modes; a particle = a single mode |

---

## 6. Falsification paths

| Claim | Falsification |
|---|---|
| a particle is a resonance class | a particle not expressible as a D96 mode / mode-access class |
| the electron is the octave-bottom mode | a lighter fermion than the octave-bottom electron |
| m_e is the boundary anchor | a derivation of m_e from the D96 spectrum alone |
| the mass ratios are derived | a fermion mass ratio not expressible as a dimensionless D96 ratio |

---

## 7. Classification

| Component | Status |
|---|---|
| the particle modes (D96 spectrum) | **DERIVED** (D_041) |
| the family structure (octave bands) | **DERIVED** (QG210) |
| the mass ratios (m_μ/m_e, quark masses) | **DERIVED** (QG173/209) |
| the electron mass m_e | **BOUNDARY** (anchor, D_012/D_013) |
| particle = localized point object | **REFUTED** |
| particle = fundamental | **REFUTED** (emergent, anchored) |

**Conclusion.** A particle in AT is a **resonance class** — a mode (a stable frequency attractor)
of the D96 spectrum, organized into octave-band families, with mass = anchor × a dimensionless
D96 ratio. The **electron** is the lightest fermion mode (the octave bottom, occ₀ = 4), and its
mass m_e = 0.511 MeV is the **boundary anchor** that fixes the fermion scale; the muon, tau, and
quarks are derived ratios of it. Particles are **emergent** (derived modes), not fundamental
point objects — matter is the deficit over the modes, and a particle is a single mode. No new
primitive; canonical AT unchanged.

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_072_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_072_ParticlesAreModes` | e/μ/τ/quarks = D96 modes, m_e × ratios | ✅ |
| `Y_NP_072_Interpretations` | B=C (resonance = occupancy); A/D refuted | ✅ |
| `Y_NP_072_MatterVsParticle` | matter = deficit over modes; particle = a mode | ✅ |
| `Y_NP_072_Emergence` | modes DERIVED; m_e BOUNDARY | ✅ |
| `Y_NP_072_Classification` | modes/ratios DERIVED; particle REFUTED | ✅ |
| `Y_NP_072_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_072"`

---

## References

- AT-QG: QG173 (u-quark mass), QG181 (M_Pl = v·A³), QG209 (lepton hierarchy), QG210 (family
  index = octave band), QG150 (mode access), QG178 (electron g-2), QG126 (sector-particle
  mapping), D_041 (spectrum), D_012/D_013 (m_e anchor).
- ResearchY-NP_071 (matter = deficit).
