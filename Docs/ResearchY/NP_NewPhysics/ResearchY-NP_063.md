# ResearchY-NP_063 — Cosmological Coincidence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_063 (permanent)
**Title:** Cosmological Coincidence Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_063.md`
**Depends on:** ResearchY-NP_055–NP_062 (the dark-energy arc), ResearchY-QG_018 (KL origin
OP1, alternative-measure test), QG_013 (3-family window anchored by ΩΛ), S_001 (ρ as the
information-geometry bridge), AT-QG QG89 (energy = actualization rate), QG234
(ΩΛ = I_occ/ln K)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_063_Tests.cs`

---

## Purpose

NP_061 established the match is a fragile point-correspondence. NP_063 systematizes the
question: **if ΩΛ_AT = 0.6839 is an information observable and ΩΛ_obs = 0.6847 is a
cosmological density fraction, why are they numerically equal?** Program: (1) remove all
dark-energy interpretation; (2) treat ΩΛ_AT as a pure information observable; (3) treat
ΩΛ_obs as a pure cosmological observable; (4) search ALL possible links (causal / descriptor /
scaling law / coincidence / hidden common origin); (5) determine the minimal assumptions to
connect them; (6) identify the first non-derived step. **Success criterion:** explain why two
observables from different domains share nearly the same value. No new primitives; canonical
AT unchanged.

---

## 1. The two observables, stripped

```
ΩΛ_AT  = I_occ/ln K = KL(ρ‖uniform)/ln K = 0.6839   (information — DERIVED from ρ)
ΩΛ_obs = 0.6847                                       (cosmology — MEASURED, Planck)
```

ΩΛ_AT is a function of the count density ρ = [4,4,87]/95. ΩΛ_obs is an empirical property of
the universe's energy budget. They are defined in different domains; nothing in the
information domain *logically* entails the cosmological number.

---

## 2. Search all possible links

| Link | Verdict |
|---|---|
| **A) causal** | **REFUTED.** ΩΛ_AT (a mathematical property) cannot *cause* ΩΛ_obs (a measurement); no causal arrow exists in either direction. |
| **B) descriptor** | **YES — the closest reading.** Both are projections of the SAME underlying state ρ: ΩΛ_AT its information face, ΩΛ_obs its energy face (S_001's "ρ is the information-geometry bridge"). But this presupposes the non-derived bridge. |
| **C) scaling law** | **REFUTED.** No derived formula ΩΛ_obs = f(ΩΛ_AT) exists. The relation is a single-point equality (0.6839 ≈ 0.6847), not a functional law. |
| **D) coincidence** | **PARTIAL.** Principled (no free parameter), but the precision (0.12%) is a fragile point-match (NP_061). Not "numerology", but not robust either. |
| **E) hidden common origin** | **THE RESCUE HYPOTHESIS — unproven.** ρ is the common substrate generating both faces. Valid ONLY if the "energy face" is derived; it is not (QG89 BOUNDARY). |

**Determination:** the link is **B (descriptor), with E (hidden common origin) as an unproven
hypothesis** whose validity hinges entirely on the non-derived QG89 bridge.

---

## 3. The minimal assumption chain

Connecting ΩΛ_AT to ΩΛ_obs requires exactly **four** non-derived assumptions, in chain order:

| # | Assumption | Status | Role |
|---|---|---|---|
| 1 | **KL divergence is the information measure** | EMERGENT (choice; unique match, QG_018 OP1) | turns ρ into I_occ |
| 2 | **N = 96 / K = 3** (the canonical ring) | BOUNDARY (3-family window [4,8); anchored to ΩΛ_obs, QG_013) | fixes ln K and the [4,4,87] occupancy |
| 3 | **energy = actualization rate** | BOUNDARY (QG89 definition) | the information→energy bridge |
| 4 | **dimensionful anchors** v, m_e, ħ, c | BOUNDARY (D_012/D_013, NP_029) | gives the energy reading units |

**The four assumptions are the complete, minimal connective tissue.** Remove any one and the
equality loses its numerical content.

---

## 4. The first non-derived step

Reading the chain from the D96 spectrum left-to-right:

```
D96 spectrum → occupancy [4,4,87] → I_occ = KL(ρ‖uniform) → ln K → ΩΛ_AT = 0.6839
                     DERIVED              ↑ first non-derived       DERIVED
                                          (KL measure, EMERGENT)
```

**The first non-derived step is the KL measure choice (assumption 1).** It is EMERGENT — the
unique measure reproducing the match (Hellinger 0.1917, TV 0.5302, χ² 1.3896 all fail) — but
it is a *choice*, not a derivation (QG_018 OP1: "KL origin" is open). The DEEPEST non-derived
step is assumption 3 (QG89, "energy = actualization rate") — the root of the energy bridge
(NP_058/059).

**Note:** ΩΛ_obs itself is an empirical input (a measurement). No theory "derives" a
measurement; every theory connects its derived quantities to measurements through *some*
identification. The question is only where AT's identification sits — and it sits at QG89 +
anchors + the KL/K choices.

---

## Theorem

> **Theorem (NP_063).** ΩΛ_AT = 0.6839 and ΩΛ_obs = 0.6847 are numerically equal because AT
> identifies the information partition of the count density ρ with the cosmological energy
> partition — a DESCRIPTOR link (B) via a hypothesized common origin (E, ρ) — and this
> identification rests on exactly four non-derived assumptions: (1) the KL measure (EMERGENT
> choice), (2) N=96/K=3 (BOUNDARY window, anchored to ΩΛ_obs), (3) "energy = actualization
> rate" (QG89, BOUNDARY definition), (4) dimensionful anchors (BOUNDARY). Proof: (1) Strip
> interpretation (Section 1): the two observables live in different domains with no logical
> entailment. (2) Enumerate links (Section 2): causal REFUTED, scaling-law REFUTED, coincidence
> PARTIAL, descriptor YES (presupposing the bridge), hidden-common-origin unproven. (3) Minimal
> assumptions (Section 3): exactly the four listed; remove any and the equality loses content.
> (4) First non-derived step (Section 4, verified): the KL measure (alternatives Hellinger/TV/
> χ² fail); the deepest is QG89. (5) ΩΛ_obs is a measurement — an empirical input, not a
> derivable. Classification: the equality as a causal relation REFUTED; as a scaling law
> REFUTED; as a DESCRIPTOR CORRESPONDENCE via an unproven common origin (ρ) — with the
> non-derived bridge QG89 + anchors + KL/K choices BOUNDARY. **Success criterion: the two
> observables share nearly the same value because AT posits they are two faces of one count
> density ρ — a descriptor link whose enabling assumptions (KL measure, N=96 window, QG89
> bridge, anchors) are all non-derived.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Strip to two numbers. (2) Test A–E. (3) Enumerate the minimal
> assumptions. (4) Locate the first non-derived step. ∎

---

## 5. Counterexamples

| Attempt | Why it fails |
|---|---|
| "ΩΛ_AT causes ΩΛ_obs" | a mathematical property cannot cause a measurement (no causal arrow) |
| "a scaling law connects them" | no derived formula ΩΛ_obs = f(ΩΛ_AT); it is a point equality, not a law |
| "the match needs no assumptions" | the KL measure, N=96 window, QG89 bridge, and anchors are all non-derived |
| "the common origin ρ is proven" | the energy face of ρ requires QG89 (unproven identification) |
| "ΩΛ_obs is derived" | it is a measurement (empirical input), not a theorem |

---

## 6. Falsification paths

| Claim | Falsification |
|---|---|
| the link is a descriptor (not causal/law) | a causal or functional relation between ΩΛ_AT and ΩΛ_obs |
| four non-derived assumptions suffice | a fifth non-derived assumption required to connect them |
| the KL measure is the first non-derived step | a derivation of the KL measure from Difference alone (QG_018 OP1) |
| the common origin ρ is unproven | a derivation of ρ's energy face without QG89/anchors |

---

## 7. Classification

| Component | Status |
|---|---|
| ΩΛ_AT = I_occ/ln K = 0.6839 (information observable) | **DERIVED** (QG234) |
| ΩΛ_obs = 0.6847 (cosmological observable) | **MEASURED** (empirical input) |
| the numerical equality (0.12%) | **CORRESPONDENCE** (descriptor link, B) |
| the KL measure | **EMERGENT** (choice; first non-derived step) |
| N=96 / K=3 window | **BOUNDARY** (anchored to ΩΛ_obs) |
| "energy = actualization rate" (QG89) | **BOUNDARY** (deepest non-derived step) |
| dimensionful anchors (v, m_e, ħ, c) | **BOUNDARY** |
| causal link / scaling law / deep derived relation | **REFUTED** |
| hidden common origin ρ | **hypothesis, unproven** (needs the derived bridge) |

**Conclusion.** ΩΛ_AT = 0.6839 and ΩΛ_obs = 0.6847 share nearly the same value because AT
**posits** they are two faces of one count density ρ — the information face (I_occ/ln K) and
the energy face (the cosmological fraction). This is a **descriptor correspondence** whose
enabling assumptions are exactly four and all non-derived: the KL measure (EMERGENT choice),
the N=96/K=3 window (BOUNDARY, anchored to the observation), "energy = actualization rate"
(QG89, BOUNDARY), and the dimensionful anchors (BOUNDARY). The first non-derived step is the
KL measure; the deepest is QG89. There is no causal link, no scaling law, and no
derived-end-to-end deep relation — the equality is a principled descriptor correspondence
carried by the unproven hypothesis that ρ is the common substrate of information and energy.
No new primitive; canonical AT unchanged.

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_063_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_063_TwoObservables` | ΩΛ_AT = 0.6839 (info), ΩΛ_obs = 0.6847 (cosmo) | ✅ |
| `Y_NP_063_LinkEnumeration` | causal/law REFUTED; descriptor YES; coincidence partial | ✅ |
| `Y_NP_063_MinimalAssumptions` | four non-derived assumptions | ✅ |
| `Y_NP_063_FirstNonDerivedStep` | KL measure EMERGENT (alternatives fail) | ✅ |
| `Y_NP_063_ObsIsMeasurement` | ΩΛ_obs is an empirical input | ✅ |
| `Y_NP_063_Classification` | descriptor correspondence + boundary bridge | ✅ |
| `Y_NP_063_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_063"`

---

## References

- ResearchY-NP_055–NP_062 (dark-energy arc), QG_018 (KL origin OP1, alternative measures),
  QG_013 (3-family window anchored by ΩΛ), S_001 (ρ as the information-geometry bridge).
- AT-QG: QG89 (energy = actualization rate), QG234 (ΩΛ = I_occ/ln K), QG228 (I_occ =
  KL(ρ‖uniform)), QG227 (uniform reference), D_012/D_013 (anchors v, m_e), NP_029 (ħ, c).
