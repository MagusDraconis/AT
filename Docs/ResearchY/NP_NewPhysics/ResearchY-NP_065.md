# ResearchY-NP_065 — Dark Matter Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_065 (permanent)
**Title:** Dark Matter Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_065.md`
**Depends on:** AT-QG QG194 (matter = deficit), QG195 (matter T_μν), QG206 (flat rotation
α=0), QG184 (M ∝ R), QG230 (Λ origin), QG234 (ΩΛ = I_occ/ln K), QG89 (energy =
actualization rate), ResearchY-NP_055–NP_064 (the dark-energy ontology arc), NP_057/060
(the extractable side is matter)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_065_Tests.cs`

---

## Purpose

NP_055–064 established ΩΛ = I_occ/ln K = 0.6839 is a DERIVED information descriptor (the
surplus) whose energy reading is hosted. NP_065 asks the complementary question: **what is
Dark Matter (Ωm) inside Actualization Theory?** Program: (1) inventory every appearance of
Ωm, the matter deficit, and the deficit conservation Σm = 0; (2) determine whether Ωm is
physical matter / occupancy deficit / realized-state fraction / bookkeeping complement;
(3) compare Ωm = H/ln K against the observed Ωm; (4) search for ontology; (5) determine
whether Ωm has stronger physical meaning than ΩΛ. **Success criterion:** identify what Dark
Matter corresponds to inside AT. No new primitives; canonical AT unchanged.

---

## 1. Inventory — Ωm, the matter deficit, and its conservation

| Object | Formula | Value | Source |
|---|---|---|---|
| realized entropy | H = −Σρ ln ρ | 0.3473 nats | QG228 |
| **Ωm** | H/ln K = (ln K − I_occ)/ln K | **0.3161** | QG234 |
| **ΩΛ** (complement) | I_occ/ln K | 0.6839 | QG234 |
| matter deficit | m = ρ̄ − ρ | [0.2912, 0.2912, −0.5825] | QG194 |
| deficit conservation | Σm = Σ(ρ̄ − ρ) = 0 | 0 exactly | QG194 |
| partition | Ωm + ΩΛ = 1 | 1 | QG234 |

**The structural picture (verified):** for ρ = [4,4,87]/95 against the uniform mean
ρ̄ = 1/3, the deficit m = ρ̄ − ρ is **positive in the under-occupied low octaves [4, 4]**
(0.2912 each) and **negative in the over-occupied top octave [87]** (−0.5825), with
Σm = 0 exactly. Matter is the *under-density*; dark energy is the *over-density*.

---

## 2. What is Ωm — A/B/C/D?

| Reading | Verdict |
|---|---|
| **A) physical matter** | **HOSTED energy reading + DERIVED gravitational role.** E_def = m needs QG89 (hosted, like ΩΛ), but the deficit's *gravitational* role is DERIVED (QG195/206/184). |
| **B) occupancy deficit** | **YES — the QG194 identification.** Ωm is read as matter = ρ̄ − ρ (the under-occupancy). |
| **C) realized-state fraction** | **YES — literal.** Ωm = H/ln K *is* the realized-entropy fraction. |
| **D) bookkeeping complement** | **YES — identical to C.** Ωm = 1 − ΩΛ partitions the state-space size. |

**Determination: C = D = B (the realized-entropy fraction IS the bookkeeping complement, read
as the matter deficit); A is the hosted-energy + derived-gravity reading on top.**

---

## 3. Compare Ωm = H/ln K against observation

| Quantity | AT value | Observed (Planck) | Deviation |
|---|---|---|---|
| Ωm | 0.3161 | 0.3153 | **0.26%** |
| (ΩΛ, for reference) | 0.6839 | 0.6847 | 0.12% |

**The matter fraction matches to the same order as ΩΛ (sub-percent).** The pair is DERIVED
jointly from the single partition I_occ + H = ln K.

---

## 4. The ontology — matter = deficit, an effect not a particle

The corpus's dark-matter ontology is fully specified by three derived relations:

| Object | Content | Status |
|---|---|---|
| **QG194 — matter = deficit** | the actualization deficit IS the energy deficit (m = ρ̄ − ρ), conserved (Σm = 0), with a gradient-source identity ∇m = −∇ρ ⇒ m = ρ̄ − ρ | DERIVED |
| **QG195 — matter T_μν** | the independent matter stress-energy T_μν = (ρ̄ − ρ)v_μv_ν recovered *without* defining matter | DERIVED |
| **QG206 — flat rotation α = 0** | the deficit m ∝ r^(−α) sources gravity; flat rotation (v = const) requires exactly α = 0 (equal deficit per octave) | DERIVED |
| **QG184 — M ∝ R** | the deficit mass follows M ∝ R | DERIVED |

**Dark matter in AT is NOT a particle.** It is the **deficit** — the under-occupancy of the
counting measure — acting as a *gravitational effect*. It reproduces flat rotation curves
(α = 0) and M ∝ R, but carries **no CMB/structure-formation implications** (QG229): it does
not do the job of particle dark matter in structure formation.

---

## 5. Does Ωm have stronger physical meaning than ΩΛ?

**YES — this is the decisive asymmetry of the dark sector:**

| Quantity | What it is | Physical role |
|---|---|---|
| **ΩΛ** (dark energy) | the SURPLUS (information excess, I_occ/ln K) | **a descriptor — no derived dynamical/gravitational role** (NP_060: cannot do work) |
| **Ωm** (dark matter) | the DEFICIT (matter = ρ̄ − ρ, H/ln K) | **a physical effect — the deficit SOURCES gravity** (T_μν = (ρ̄−ρ)v_μv_ν, flat rotation, M∝R) |

The deficit gravitates; the surplus merely describes. Matter (the deficit) is the source term
in the derived metric dynamics, while dark energy (the surplus) is a dimensionless order
parameter with no derived effect.

**With one important caveat:** both share the same hosted energy reading (E_def = m for
matter, I_vac > 0 ⇒ ρ_Λ > 0 for dark energy, both via QG89). Matter is stronger *in the
gravitational direction* — its source role is DERIVED — but not *in the energy direction*,
which is hosted for both. And "dark matter" specifically (non-baryonic, particle) is REFUTED:
AT's deficit is a single matter substance, not split into baryonic and dark, and it has no
structure-formation role.

---

## Theorem

> **Theorem (NP_065).** Dark Matter inside AT is the matter deficit m = ρ̄ − ρ — an EFFECT,
> not a particle — whose fraction is Ωm = H/ln K = 0.3161 (the realized-entropy fraction, the
> bookkeeping complement of ΩΛ). It has STRONGER physical meaning than dark energy: the
> deficit SOURCES gravity (a DERIVED role — T_μν = (ρ̄−ρ)v_μv_ν, flat rotation α=0, M∝R),
> whereas the surplus (ΩΛ) is a descriptor with no derived effect. Proof: (1) Inventory
> (Section 1, verified): ρ=[4,4,87]/95 gives H=0.3473, Ωm=H/ln K=0.3161, ΩΛ=0.6839, and the
> deficit m=ρ̄−ρ=[0.2912,0.2912,−0.5825] with Σm=0 exactly (positive in the under-occupied low
> octaves, negative in the over-occupied top octave). (2) Readings (Section 2): Ωm is literally
> the realized-state fraction (C) and the bookkeeping complement (D), read as the matter deficit
> (B); physical matter (A) is the hosted-energy + derived-gravity reading. (3) Observation
> (Section 3, verified): Ωm=0.3161 matches Ωm_obs=0.3153 to 0.26% (same order as ΩΛ's 0.12%).
> (4) Ontology (Section 4): matter=deficit (QG194, conserved, gradient-source), T_μν=(ρ̄−ρ)v_μv_ν
> (QG195), flat rotation α=0 (QG206), M∝R (QG184) — all DERIVED, an effect not a particle, no
> CMB/structure role (QG229). (5) Asymmetry (Section 5): the deficit gravitates (DERIVED), the
> surplus describes (no derived effect) — so Ωm has stronger physical meaning than ΩΛ, though
> both share the hosted QG89 energy reading. Classification: Ωm = H/ln K DERIVED (QG234);
> matter = deficit and Σm = 0 DERIVED (QG194); the gravitational role (T_μν, α=0, M∝R) DERIVED
> (QG195/206/184); the energy reading (E_def = m) CORRESPONDENCE (hosted, QG89); dark matter as
> a particle REFUTED (an effect, no structure role); the baryonic/dark split BOUNDARY (not
> derived). **Success criterion: Dark Matter = the matter deficit (m = ρ̄ − ρ), the gravitating
> under-occupancy of the counting measure — an effect with a derived gravitational role, and
> thereby stronger than the descriptive dark-energy surplus.** No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Inventory Ωm/deficit/Σm. (2) Test A–D. (3) Compare to observation.
> (4) Trace the matter-deficit ontology. (5) Establish the deficit/surplus asymmetry. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Ωm is a particle (dark matter)" | the deficit is an EFFECT (QG194/229), not a particle; it carries no CMB/structure role |
| "Ωm has no physical meaning" | the deficit sources gravity (T_μν, flat rotation α=0, M∝R) — a DERIVED physical role |
| "Ωm and ΩΛ are symmetric" | the deficit gravitates (derived); the surplus only describes (NP_060) — an asymmetry |
| "Ωm's energy is derived" | E_def = m needs QG89 (hosted), exactly like ΩΛ's energy |
| "AT splits baryonic vs dark matter" | the deficit is a single matter substance; the baryonic/dark split is BOUNDARY (not derived) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| Ωm = H/ln K (the realized-entropy fraction) | a measured Ωm deviating from H/ln K beyond 0.26% |
| matter = deficit (m = ρ̄ − ρ, Σm = 0) | a deficit source that violates Σm = 0 |
| the deficit sources gravity (DERIVED) | a metric whose source is not the deficit ∇m |
| dark matter is an effect, not a particle | a structure-formation signature requiring a particle (e.g. a detection of a deficit particle) |
| the deficit/surplus asymmetry | a derived dynamical role for ΩΛ (the surplus) |

---

## 8. Classification

| Component | Status |
|---|---|
| Ωm = H/ln K = 0.3161 (the value, realized-entropy fraction) | **DERIVED** (QG234) |
| matter = deficit (m = ρ̄ − ρ, Σm = 0) | **DERIVED** (QG194) |
| gravitational role (T_μν = (ρ̄−ρ)v_μv_ν, α=0, M∝R) | **DERIVED** (QG195/206/184) |
| the energy reading (E_def = m) | **CORRESPONDENCE** (hosted, QG89) |
| dark matter as a particle | **REFUTED** (an effect; no structure role, QG229) |
| the baryonic/dark split | **BOUNDARY** (not derived) |

**Conclusion.** Dark Matter inside AT is the **matter deficit m = ρ̄ − ρ** — the under-occupancy
of the counting measure — an **effect, not a particle**, whose fraction Ωm = H/ln K = 0.3161 is
the realized-entropy fraction (the bookkeeping complement of ΩΛ), matching the observed Ωm to
0.26%. It has **stronger physical meaning than dark energy**: the deficit *sources gravity* (a
DERIVED role — T_μν = (ρ̄−ρ)v_μv_ν, flat rotation α=0, M∝R), while the surplus ΩΛ is a
descriptor with no derived effect. The caveat: the deficit's *energy* reading is hosted (QG89,
like ΩΛ), and "dark matter" as a *particle* (non-baryonic, with a structure-formation role) is
REFUTED — AT's deficit is a single matter substance, not a baryonic/dark split. No new
primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_065_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_065_OmegaMRealizedEntropyFraction` | Ωm = H/ln K = 0.3161; matches 0.3153 (0.26%) | ✅ |
| `Y_NP_065_MatterDeficit` | m = ρ̄ − ρ = [0.2912, 0.2912, −0.5825] | ✅ |
| `Y_NP_065_DeficitConservation` | Σm = Σ(ρ̄ − ρ) = 0 exactly | ✅ |
| `Y_NP_065_Readings` | C = D = B; A is hosted-energy + derived-gravity | ✅ |
| `Y_NP_065_Asymmetry` | deficit gravitates (derived); surplus describes | ✅ |
| `Y_NP_065_Classification` | deficit DERIVED; particle REFUTED; energy hosted | ✅ |
| `Y_NP_065_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_065"`

---

## References

- AT-QG: QG194 (matter = deficit, Σm = 0), QG195 (matter T_μν), QG206 (flat rotation α=0),
  QG184 (M ∝ R), QG234 (Ωm = H/ln K), QG228 (H, I_occ), QG89 (energy = actualization rate),
  QG229 (dark matter as an effect).
- ResearchY: NP_055–NP_064 (dark-energy arc), NP_057 (Ωm = the deficit/matter side),
  NP_060 (the extractable side is matter).
