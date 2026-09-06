# ResearchY-NP_083 — Threefold Structure Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_083 (permanent)
**Title:** Threefold Structure Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_083.md`
**Depends on:** ResearchY-NP_037 (the "no universal 3 principle" audit), NP_064 (canonical
structure), NP_073 (resonance selection), NP_074 (quantum number ontology), NP_082 (electron
mass anchor), AT-QG QG79 (color count N=3), QG161/242/248 (1+3+8 gauge), QG210 (family count =
floor(log₂ span)+1), QG210/228/234 (ΩΛ chain), D_020/D_031/D_040 (period-3 seed), D_016/D_028
(span), A_003/D_030 (occupancy), QG_013 (3-family window)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_083_Tests.cs`

---

## Purpose

NP_037 found no *universal* "3 principle" — the many 3s (dimension d=3, DOS ω³, the A³ cube,
valence-3, the triplet rep) are largely coincidental across domains. NP_083 asks the narrower,
sharper question: **do the two remaining boundary-like 3s — the color count 3 and the family
count 3 — share a common origin?** Program: (1) inventory every surviving non-derived 3; (2)
separate derived 3s from boundary 3s; (3) test whether 3 colors and 3 families arise from the
same D96 structure; (4) remove each independently; (5) determine which observables break; (6)
search for a common parent. **Success criterion:** determine whether the remaining threefold
structures are A) independent boundaries, B) a hidden common derivation, or C) a single deeper
boundary. No new primitives; canonical AT unchanged.

---

## 1. Inventory — every surviving non-derived 3

| Appearance | Value | Status | Source |
|---|---|---|---|
| period-3 seed p = 3 | unique complete-pairing period, 6\|N | **DERIVED** | D_031/D_040 |
| octave rung N = 3·2^k | factor-3 × power of 2 | **DERIVED** (given seed) | D_020 |
| **family count 3** | floor(log₂ span)+1 = 3 | **VALUE DERIVED / WINDOW BOUNDARY** | QG210/D_016/D_028 |
| occupancy [4,4,87] | 3 octave bands | **DERIVED** | A_003/D_030 |
| A = 95·44·87 | threefold spectral product | **DERIVED** | QG181 |
| su(3) algebra 8 = 3²−1 | 8 generators from 3 families | **DERIVED** (from the 3 families) | QG161/242 |
| **color count 3** | the SU(3) fundamental dimension N_c | **BOUNDARY** (QG79 identification) | QG79 |

**Two boundary-like 3s remain: the family window [4,8) (for family-3) and the color-count
identification (for color-3).** Everything else that is "3" is either DERIVED or (per NP_037)
coincidental across domains.

---

## 2. Derived 3s vs boundary 3s

| 3 | Derived part | Boundary part |
|---|---|---|
| family 3 | the value floor(log₂ span)+1 = 3 (given N=96) | the window [4,8) that selects N=96 (anchored to ΩΛ_obs) |
| color 3 | the su(3) algebra 8 = 3²−1 (from the 3 family directions) | the identification "3 colors = 3 families" (QG79) |
| period-3 seed | the seed p = 3 itself (complete pairing) | none (D_040 upgraded it BOUNDARY→DERIVED) |

The two surviving boundary-like 3s each have a **derived core** (the value, the algebra) and a
**boundary residue** (the window, the identification).

---

## 3. Do 3 colors and 3 families arise from the SAME D96 structure?

**YES — through the octave/family structure.** The chain is:

```
period-3 seed p = 3 (DERIVED, D_040)
   → N = 3·2^k (factor-3 rung)
   → 3 octave bands [4,4,87]  (DERIVED, A_003/D_030)
   → 3 families = 3 octave bands (QG210: family index = octave band)
   → su(3) algebra = 3²−1 = 8 generators "from the 3 octave families" (QG161/242)
   → color count 3 = the octave-3 identification (QG79)
```

The su(3) color structure is **not a separate 3**: it is the same octave-3, read as a gauge
algebra. The 8 generators of su(3) are DERIVED from the 3 family directions. Only the *final*
identification — "the 3 families ARE the 3 colors" — is a BOUNDARY (QG79, "color-3 = the
octave-3 identification", a postulate trace).

---

## 4. Remove each independently

| Remove | What breaks |
|---|---|
| **family 3** (→ 2 or 4 families) | masses (m_μ/m_e = 102.3 / 416.3 vs 206.77), ΩΛ (0.5801/0.7295 vs 0.6839), **and the su(3) structure** (su(2) = 3 or su(4) = 15 generators, not su(3) = 8) |
| **color 3** (→ SU(2) or SU(N)) | baryon structure (the 3-quark antisymmetric color singlet), the strong force — but NOT the masses or ΩΛ |

**The two 3s are coupled one-way:** removing the family-3 breaks the color-3 (the su(3) algebra
is built from the 3 families), but removing the color-3 does NOT break the family-3 (the family
count is an independent spectral fact). This asymmetry confirms a **common parent for the family-3,
with the color-3 as a dependent (but boundary-identified) read of it.**

---

## 5. The common parent

| Candidate | Verdict |
|---|---|
| **period-3 seed p = 3** | **YES — the common parent (DERIVED, D_040).** It forces the factor 3 in N = 3·2^k, from which the 3 octave bands, 3 families, and the su(3) color structure all descend. |
| octave structure (3 bands) | **YES — the intermediary.** The 3 bands ARE the 3 families and generate the su(3) algebra. |
| automorphism group C_96 | **PARTIAL.** C_96 hosts the 12 link-directions (±1..±6) that give 1+3+8 gauge generators, but the *factor 3* traces to the period-3 seed, not to C_96 alone. |
| occupancy geometry [4,4,87] | **PARTIAL.** The 3 bands are the *consequence*, not the *parent* (they descend from N=96). |

**The common parent of color-3 and family-3 is the period-3 seed p = 3 (DERIVED).**

---

## 6. A / B / C

| Determination | Verdict |
|---|---|
| **A) independent boundaries** | **NO.** The two 3s are coupled: the su(3) algebra is derived from the 3 families; removing family-3 breaks color-3. |
| **B) a hidden common derivation** | **YES — the answer.** Both 3s descend from the period-3 seed (DERIVED) via the octave/family structure; the su(3) algebra is a DERIVED read of the 3 families. |
| **C) a single deeper boundary** | **NO.** There is no single "3" boundary: two *distinct* residues remain (the family window [4,8) and the color-count identification QG79). |

**Determination: B — a hidden common derivation**, with the refinement that the common parent is
DERIVED (the period-3 seed), and each 3 retains its own boundary residue (family window; color
identification).

---

## Theorem

> **Theorem (NP_083).** The color count 3 and the family count 3 share a COMMON ORIGIN — the
> period-3 seed p = 3 (DERIVED, D_040), which forces the factor 3 in N = 3·2^k and thus the 3
> octave bands [4,4,87]. The family count 3 IS the octave-band count (floor(log₂ span)+1 = 3,
> value DERIVED, window [4,8) BOUNDARY); the su(3) color structure is the SAME octave-3 read as a
> gauge algebra (8 = 3²−1 generators DERIVED from the 3 family directions, QG161/242); and the
> color count 3 is the octave-3 IDENTIFICATION (QG79, BOUNDARY). Proof: (1) Inventory (Section 1):
> only two boundary-like 3s (family window, color identification). (2) Derived/boundary (Section
> 2): each 3 has a derived core + boundary residue. (3) Same structure (Section 3, verified): the
> su(3) algebra descends from the 3 families (3²−1 = 8). (4) Removal (Section 4, verified): removing
> family-3 breaks color-3 (su(3)→su(2)/su(4)) AND masses/ΩΛ; removing color-3 breaks baryons/strong
> force but not family-3 — a one-way coupling. (5) Parent (Section 5): the period-3 seed. (6)
> Determination (Section 6): B (hidden common derivation), not A (independent), not C (single
> boundary). Classification: period-3 seed DERIVED (D_040); family value DERIVED (QG210), window
> BOUNDARY; su(3) algebra DERIVED (QG161/242); color count BOUNDARY (QG79); "independent boundaries"
> REFUTED; "single deeper boundary" REFUTED. **Success criterion: B — color-3 and family-3 share a
> hidden common derivation (the period-3 seed → octave structure), with two distinct boundary
> residues (the [4,8) window and the QG79 identification).** No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Inventory the 3s. (2) Separate derived from boundary. (3) Trace both to the
> octave structure. (4) Remove each. (5) Name the parent. (6) Decide A/B/C. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "color-3 and family-3 are independent" | the su(3) algebra is derived from the 3 families; removing family-3 breaks color-3 |
| "there is a single '3' boundary" | two distinct residues remain (the [4,8) window and the QG79 identification) |
| "the color count 3 is derived" | QG79: the identification "3 colors = 3 families" is a postulate trace, not a derivation |
| "the family 3 is a bare boundary" | its value (floor(log₂ span)+1 = 3) is DERIVED; only the window is boundary |
| "all 3s in AT share one origin" | NP_037: the cross-domain 3s (d=3, ω³, A³, valence-3) are coincidental — only the D96 spectral 3s share the seed |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| color-3 and family-3 share the period-3 seed | a color-3 that survives changing N (removing the factor-3 seed) |
| the su(3) algebra is derived from the 3 families | an su(3) algebra with 8 generators that does not descend from the octave bands |
| two distinct boundary residues remain | a single derivation producing both the family window and the color identification |
| removing family-3 breaks color-3 | an SU(3) color sector surviving a 2- or 4-family spectrum |

---

## 9. Classification

| Component | Status |
|---|---|
| period-3 seed p = 3 | **DERIVED** (D_040) — the common parent |
| family count 3 (value) | **DERIVED** (QG210, floor(log₂ span)+1) |
| family window [4,8) | **BOUNDARY** (anchored to ΩΛ_obs) |
| su(3) algebra 8 = 3²−1 | **DERIVED** (QG161/242, from the 3 families) |
| color count 3 (N_c = 3) | **BOUNDARY** (QG79 identification) |
| "independent boundaries" | **REFUTED** |
| "single deeper boundary" | **REFUTED** |

**Conclusion.** The color count 3 and the family count 3 are **not independent boundaries and not
a single deeper boundary** — they are a **hidden common derivation** (B) rooted in the period-3
seed p = 3 (DERIVED). The factor 3 in N = 3·2^k gives the 3 octave bands, which are both the 3
families (value DERIVED, window BOUNDARY) and the source of the su(3) color algebra (8 = 3²−1,
DERIVED). Only two distinct boundary residues remain — the family window [4,8) and the color-count
identification (QG79). This refines NP_037: within the D96 spectral domain the 3s share one seed
(unlike the coincidental cross-domain 3s); but "3" is still not a single principle — it is one
DERIVED seed plus two boundary identifications. No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_00083_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_083_Inventory` | only two boundary-like 3s (family, color) | ✅ |
| `Y_NP_083_DerivedVsBoundary` | value/algebra DERIVED; window/identification BOUNDARY | ✅ |
| `Y_NP_083_CommonStructure` | su(3) 8 = 3²−1 from 3 families | ✅ |
| `Y_NP_083_RemoveFamilyBreaksColor` | family-3 removal → su(2)/su(4), masses/ΩΛ break | ✅ |
| `Y_NP_083_RemoveColorKeepsFamily` | color-3 removal → baryons break, family-3 survives | ✅ |
| `Y_NP_083_CommonParent` | period-3 seed (DERIVED) is the parent | ✅ |
| `Y_NP_083_ABC` | B (common derivation); A, C refuted | ✅ |
| `Y_NP_083_Classification` | seed/algebra DERIVED; window/identification BOUNDARY | ✅ |
| `Y_NP_083_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_083"`

---

## References

- ResearchY-NP_037 (no universal "3 principle"), NP_064 (canonical structure), NP_073 (resonance
  selection), NP_074 (quantum numbers), NP_082 (electron mass anchor).
- AT-QG: QG79 (color count N=3), QG161/242/248 (1+3+8 gauge), QG210 (family count), QG228/234
  (ΩΛ chain), QG_013 (3-family window).
- ResearchY: D_020/D_031/D_040 (period-3 seed), D_016/D_028 (span), A_003/D_030 (occupancy).
