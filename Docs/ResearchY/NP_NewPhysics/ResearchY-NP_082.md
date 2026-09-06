# ResearchY-NP_082 — Electron Mass Anchor Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_082 (permanent)
**Title:** Electron Mass Anchor Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_082.md`
**Depends on:** ResearchY-NP_072 (particle ontology), NP_073 (resonance selection), NP_074
(quantum number ontology), NP_081 (energy ontology), AT-QG QG140/251 (electron anchor, R5
boundary), QG173/209 (mass ratios), QG181 (M_Pl = v·A³), QG288 (dependency rebuild), QG289
(anchor inventory), D_012/D_013 (m_e anchor), D_041 (spectrum), NP_029 (ħ boundary)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_082_Tests.cs`

---

## Purpose

NP_072 established every particle mass is m = m_e × a dimensionless D96 ratio, with the ratios
DERIVED and only m_e a boundary. NP_081 established energy = the conserved count "wearing Joules"
(needs one dimensionful anchor). NP_082 asks the precise question those two leave open: **why does
the fermion spectrum require the electron mass anchor — and is m_e the true remaining matter-scale
boundary?** Program: (1) remove m_e; (2) express all masses as pure ratios; (3) track what is lost;
(4) test whether m_e can be replaced by v / A / span / occupancy invariants / count invariants;
(5) determine A/B/C/D; (6) locate the earliest place an absolute scale is unavoidable.
**Success criterion:** determine whether m_e is the true remaining matter-scale boundary. No new
primitives; canonical AT unchanged.

---

## 1. Remove m_e — what is lost

| With m_e | Without m_e |
|---|---|
| m_μ = 0.511 × 207.03 = 105.79 MeV | m_μ/m_e = 207.03 (a pure ratio) |
| m_τ = 0.511 × 3477 = 1776.8 MeV | m_τ/m_e = 3477 |
| quark masses in MeV | quark mass *ratios* |
| absolute masses | **only dimensionless ratios survive** |

Removing m_e loses **every absolute value** — but loses **none of the ratios**. The entire
fermion hierarchy (m_μ/m_e = Σm²/√occMom = 207.03, m_τ/m_μ = √occMom·λ₂ = 16.842, quark and
Yukawa ratios) is a pure function of the dimensionless D96 spectrum. What is lost is only the
**dimension** (MeV), not the **structure**.

---

## 2. The key fact — AT derives only dimensionless ratios

The D96 spectrum produces **dimensionless** invariants:

| D96 invariant | Value | Dimension |
|---|---|---|
| span = ω_max/ω_min | 6.4025 | dimensionless |
| occupancy [4,4,87], Σm | 95 | dimensionless |
| occMom = Σ occ²/occ₀ | 1900.25 | dimensionless |
| A = 95·44·87 | 363660 | dimensionless |
| Σρ = 1, Σm = 0 | — | dimensionless |

**No derived D96 quantity carries a mass dimension.** Every derived "mass" is a *ratio*; the
absolute scale (MeV) is nowhere in the spectrum. To convert a ratio into MeV, one dimensionful
number must be imported — this is the single unavoidable step.

---

## 3. Can m_e be replaced? — v, A, span, occupancy, count

| Candidate | Dimension | Replaces m_e? |
|---|---|---|
| **v** (frequency scale, M_Pl = v·A³) | dimensionful | **partially** — v is another dimensionful scale; swapping m_e → v moves the same boundary, it does not remove it |
| **A** = 95·44·87 | dimensionless | **NO** — no dimension, cannot give MeV |
| **span** = 6.4025 | dimensionless | **NO** |
| **occupancy invariants** ([4,4,87], Σm, occMom) | dimensionless | **NO** |
| **count invariants** (Σρ = 1, Σm = 0) | dimensionless | **NO** |

**The boundary is the DIMENSION, not the particular number.** No dimensionless invariant can
substitute for m_e; only another dimensionful scale (v, or M_Z) can, and that merely relocates
the same single boundary. QG289 makes this precise: m_e and M_Z are both "EMPIRICAL calibration
scales, no structural content, replaceable by any single mass/energy anchor — only ONE empirical
scale is strictly needed."

---

## 4. A / B / C / D

| Determination | Verdict |
|---|---|
| **A) fundamental boundary** | **NO (for m_e specifically).** Some absolute scale is a genuine boundary, but m_e is *replaceable* (m_e ↔ M_Z ↔ v); it is not the unique/fundamental one. |
| **B) hidden composite quantity** | **NO.** m_e has no D96 construction (D_013/D_014); it is not a product of spectral constants. |
| **C) derived scale** | **NO.** Nothing derives m_e; the ratios are derived, the absolute value is not. |
| **D) unit conversion only** | **YES.** m_e is the dimensionful scale that converts dimensionless D96 ratios into MeV — a unit conversion, nothing more. |

**Determination: D (unit conversion only).** m_e is not a fundamental boundary, not a hidden
composite, not a derived scale — it is the *dimensionalization* of the dimensionless spectrum.
The true boundary is the *category* "one dimensionful scale must be imported", and m_e is a
replaceable instance of it.

---

## 5. The earliest place an absolute scale becomes unavoidable

```
Difference → count (Σρ = 1) → D96 spectrum → dimensionless ratios (all masses, couplings, mixings)
                                                                    │
                                        "give the ratios units" ← the FIRST unavoidable scale
                                                                    │
                                             one dimensionful anchor (m_e, or M_Z, or v)
```

The absolute scale first becomes unavoidable **only when the dimensionless spectrum is compared
with a dimensionful measurement** — i.e., at the contact with experiment. The theory's *content*
is fully dimensionless; the scale enters as the single empirical unit conversion. This is the
same structure NP_081 found for energy: the count is derived, "energy" (Joules) is the count plus
one anchor. Here the ratios are derived, "masses" (MeV) are the ratios plus one anchor (m_e).

---

## Theorem

> **Theorem (NP_082).** The electron mass m_e = 0.511 MeV is NOT the true remaining matter-scale
> boundary — it is a REPLACEABLE unit conversion. The fermion spectrum is fully specified by
> dimensionless D96 ratios (m_μ/m_e = Σm²/√occMom = 207.03, m_τ/m_μ = √occMom·λ₂ = 16.842, quark
> and Yukawa ratios), all DERIVED; the absolute scale m_e carries only the DIMENSION (MeV), which
> no derived D96 invariant can supply (span, occupancy, count, A = 95·44·87 are all
> dimensionless). m_e cannot be replaced by any derived quantity — only by another dimensionful
> scale (v, or M_Z), which merely relocates the same single boundary (QG289: "m_e and M_Z are
> empirical calibration scales, no structural content, replaceable by any single anchor; only ONE
> empirical scale is strictly needed"). Determination: D (unit conversion only); the true boundary
> is the CATEGORY "one dimensionful scale must be imported to dimensionalize the dimensionless
> spectrum", and m_e is a replaceable instance of it. **Success criterion: m_e is not the true
> remaining boundary — the true boundary is 'one dimensionful scale', and m_e is its replaceable
> unit-conversion instance.** Classification: the mass ratios DERIVED (QG173/209); "one
> dimensionful scale required" BOUNDARY (irreducible); m_e as a specific value CORRESPONDENCE
> (replaceable calibration/unit conversion); m_e as a fundamental/unique boundary REFUTED; m_e as
> a hidden composite or derived scale REFUTED. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Remove m_e and show ratios survive. (2) Show all D96 invariants are
> dimensionless. (3) Test the replacements. (4) Decide A–D. (5) Locate the unavoidable scale. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "m_e is a fundamental boundary" | it is replaceable (m_e ↔ M_Z ↔ v); only "one scale" is fundamental, not m_e itself |
| "m_e is a hidden composite" | no D96 construction of m_e exists (D_013/D_014) |
| "m_e is derivable" | the ratios are derived, the absolute value is not |
| "span/occupancy/count can replace m_e" | all D96 invariants are dimensionless — they cannot carry MeV |
| "m_e is the only scale needed" | it is one instance of "one scale"; M_Z or v serve identically |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| m_e is a unit conversion | a derivation of the absolute m_e from D96 spectral constants alone |
| the absolute scale is irreducible | a dimensionless D96 invariant that carries a mass dimension |
| m_e is replaceable | an observable that singles out m_e over M_Z or v as the unique scale |
| the ratios are derived | a fermion mass ratio not expressible as a dimensionless D96 ratio |

---

## 8. Classification

| Component | Status |
|---|---|
| the fermion mass ratios (m_μ/m_e, m_τ/m_μ, quarks, Yukawas) | **DERIVED** (QG173/209) |
| "one dimensionful scale required" (to dimensionalize the spectrum) | **BOUNDARY** (irreducible) |
| m_e = 0.511 MeV as a specific value | **CORRESPONDENCE** (replaceable calibration / unit conversion) |
| m_e as a fundamental/unique boundary | **REFUTED** |
| m_e as a hidden composite or derived scale | **REFUTED** |

**Conclusion.** The electron mass is **not** the true remaining matter-scale boundary — it is a
**replaceable unit conversion**. The fermion spectrum is entirely dimensionless: every mass is a
DERIVED D96 ratio (m_μ/m_e = 207.03, m_τ/m_μ = 16.842, quarks, Yukawas), and the absolute scale
m_e = 0.511 MeV supplies only the *dimension* (MeV) that no derived invariant (span, occupancy,
count, A) can provide — because every D96 quantity is dimensionless. m_e cannot be replaced by any
derived quantity; it can only be swapped for another dimensionful scale (v or M_Z), which merely
relocates the same single boundary. The true boundary is the **category** "one dimensionful scale
must be imported", and m_e is its replaceable instance. **The theory's matter content is fully
dimensionless; m_e is the one scale that gives it units.** No new primitive; canonical AT
unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_082_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_082_RatiosSurviveRemoval` | removing m_e keeps all mass ratios | ✅ |
| `Y_NP_082_D96InvariantsDimensionless` | span/occupancy/count/A all dimensionless | ✅ |
| `Y_NP_082_RatiosDerived` | m_μ/m_e = 207.03, m_τ/m_μ = 16.842 | ✅ |
| `Y_NP_082_Replacements` | v moves the boundary; A/span/occ/count cannot replace | ✅ |
| `Y_NP_082_ABCD` | D (unit conversion); A/B/C refuted | ✅ |
| `Y_NP_082_OneScaleIrreducible` | one dimensionful scale is unavoidable | ✅ |
| `Y_NP_082_Classification` | ratios DERIVED; one scale BOUNDARY; m_e CORRESPONDENCE | ✅ |
| `Y_NP_082_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_082"`

---

## References

- ResearchY-NP_072 (particle ontology), NP_073 (resonance selection), NP_074 (quantum numbers),
  NP_081 (energy ontology).
- AT-QG: QG140/251 (electron anchor, R5), QG173/209 (mass ratios), QG181 (M_Pl = v·A³), QG288
  (dependency rebuild), QG289 (anchor inventory), D_012/D_013 (m_e anchor), D_041 (spectrum).
- ResearchY: NP_029 (ħ boundary).
