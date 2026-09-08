# ResearchY-M_013 — Axis-Count Selection Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** M — Measurement Origin
**ID:** ResearchY-M_013 (permanent)
**Title:** Axis-Count Selection Audit
**Status:** COMPLETE
**Date:** 2026-09-08
**File:** `M_Measurement/ResearchY-M_013.md`
**Depends on:** ResearchY-M_011 (single ring O(3) audit), M_012 (network O_h / genuine 3D
sector), NP_036 (3D DOS emergence), NP_037 (role of three), NP_088 (network geometry),
NP_089 (approximate O(3)), AT-QG QG197 (2D→3D bridge; (d−2) Einstein factor), QG2
(d ≥ 3), QG290 (dimension-generic metric), D_041 (D96 spectrum), D_020 (span window)
**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_013_Tests.cs`

---

## Purpose

**Why does Actualization select exactly 3 coupled D96 axes?** The D96⊗D96⊗D96 network is
the arena of the theory's spatial geometry: it carries the 3D Weyl DOS (NP_036), the
cubic point group O_h with a genuine 3D vector/p-wave sector (NP_088, M_012), and it is
the construction through which observed 3D physics is *hosted*. But nothing in NP_036/037
or M_011/012 has asked the sharp structural question: **is the axis count 3 UNIQUE —
selected by the spectral / group / information content of the coupled construction — or
MERELY SUFFICIENT (any count would do, and 3 is the observed value)?** This audit tests
2, 3, 4, and N coupled D96 rings along five axes of comparison — symmetry, irreducible
content, information compression, family structure, spectral efficiency — and asks
whether any of them singles out 3. **Success criterion:** determine whether the exact
axis count 3 is DERIVED (uniquely forced), EMERGENT (a consequence of the coupling), a
CORRESPONDENCE (hosted observed value), or merely sufficient. No new primitive; canonical
AT unchanged.

---

## 1. Symmetry of d coupled D96 rings

Coupling d rings on d orthogonal axes gives the *d-torus* C96□…□C96 whose natural
axis-arrangement (point) group is the group of signed permutations on d coordinates — the
**hyperoctahedral group B_d** (the symmetry of the d-cube), of order 2^d · d!.

| d (axes) | point group | order 2^d·d! | notes |
|---|---|---|---|
| 1 | Z₂ | 2 | single ring reflection |
| 2 | B₂ = D₄ (square) | 8 | planar |
| 3 | B₃ = O_h (cube) | 48 | 3D — the canonical ring |
| 4 | B₄ (hypercube) | 384 | 4D |
| N | B_N | 2^N·N! | ND |

Aut(C96□C96□C96) ⊇ D₉₆³ ⋊ S₃ (order 192³·6 = 42,467,328); its point-group content on the
axes is B₃ = O_h (M_012). The group sequence {2, 8, 48, 384, …} is regular (2^d·d!); no
member is distinguished by order — 48 is not special among {2^d·d!}.

---

## 2. Irreducible content

| d | group | irrep dimensions | max dim | genuine 3D irrep? |
|---|---|---|---|---|
| 1 | Z₂ | {1} | 1 | NO |
| 2 | D₄ | {1,1,1,1,2} (Σd² = 8) | 2 | NO |
| 3 | O_h | {1,1,2,3,3,1,1,2,3,3} (Σd² = 48) | 3 | **YES** (T₁u etc.) |
| 4 | B₄ | includes 4D defining rep; Σd² = 384 | ≥ 4 | contains 3D sub-content but vector sector is 4D |

The defining (coordinate) representation of B_d on ℝ^d is **irreducible for every d ≥ 2**
(verified ⟨χ,χ⟩ = 1 for d = 2,3,4,5). So each d gives an irreducible d-dimensional
"vector" sector; the d = 3 case is the one whose vector sector has dimension 3, matching
the observed p-wave triplet (M_012). The presence of some 3-dimensional representation is
NOT unique to d = 3 as an abstract group statement — but the *coordinate vector sector of
the coupled construction* has dimension exactly d, so the observed 3-component spatial
vector requires d = 3.

**Rotation-generator content (self-duality).** The antisymmetric square of the defining rep
(dim = number of independent rotations = d(d−1)/2):

| d | dim vector sector | dim rotation generators d(d−1)/2 | self-dual? |
|---|---|---|---|
| 2 | 2 | 1 | NO |
| 3 | 3 | **3** | **YES — so(3) ≅ ℝ³** |
| 4 | 4 | 6 | NO |
| 5 | 5 | 10 | NO |
| N | N | N(N−1)/2 | only N = 3 |

**The equation d(d−1)/2 = d has the unique solution d = 3 (d ≥ 1).** Only in three axes do
the rotational degrees of freedom form a vector of the same dimension as the space itself —
the axial-vector/cross-product structure that makes angular momentum a vector and
su(2) ≅ so(3) act on 3-vectors. This is the group-theoretic content behind "why 3D".

---

## 3. Information compression

Coupled d-ring Hilbert space: (96)^d states; per-axis mode content 95 → joint positive
states (95)^d, information d·log₂(95) = d·6.570 bits. Information is strictly additive in
the axis count:

| d | joint states | information (bits) | per-axis cost |
|---|---|---|---|
| 1 | 95 | 6.570 | 6.570 |
| 2 | 9,025 | 13.140 | 6.570 |
| 3 | 857,375 | 19.710 | 6.570 |
| 4 | 81,450,625 | 26.279 | 6.570 |

Information grows **linearly** with d; no axis count is information-optimal, no extremum at
3. Distinct joint spectral levels (ring positive modes): d = 1 → 44, d = 2 → 987, d = 3 →
22,562, d = 4 → 338,413 (computed by convolution of the 44 distinct ring levels); the
distinct/total ratio decreases monotonically (0.46, 0.11, 0.026, 0.0042) — no extremum at
3. **Information content does not select d = 3.**

---

## 4. Family structure

Family count in AT is a *single-ring* quantity: floor(log₂ span(96)) + 1 = 3, from the
span window [4, 8) (BOUNDARY input, D_020; value DERIVED). Coupling axes does not change
the per-axis octave family content [4,4,87]:

| d | family count of each ring | joint family content |
|---|---|---|
| 1, 2, 3, 4, N | 3 (span window, D_020) | unchanged — families are ring-level, additive like copies |

NP_037 already verified the axis/family DECOUPLING: the 3-family ring's tensor ⊗1/⊗2/⊗3
gives DOS p = 1/2/3 while family count stays 3; the octave family 3 is NOT the axis 3
(identity refuted). So the family count 3 cannot be the reason the *axis count* is 3 —
both equal 3 but are independent quantities (NP_037 value-equality ≠ identity). This
audit re-confirms: families select nothing about axes.

---

## 5. Spectral efficiency

Spectral efficiency measures how the coupled spectrum scales with size.

- **DOS exponent**: p = d for the d-fold D96 tensor (Weyl law, NP_035/036 DERIVED). Verified
  by lattice-ball counting: doubling exponents d = 1 → ~1.0, d = 2 → ~2.0, d = 3 → ~2.9,
  d = 4 → ~3.9. The d = 3 case gives the observed blackbody ω³ law — but p = d is an
  identity, so d = 3 is merely the value matching *observed* 3D counting; 2 axes give a
  consistent ω² and 4 axes a consistent ω⁴. DOS does NOT single out 3 except by
  correspondence to observed 3D.
- **Distinct-level compression**: monotone in d (Section 3). No extremum at 3.
- **NP_036 removal test**: D96⊗3 → D96⊗2 loses the ω³ law AND Stefan–Boltzmann π⁴/15; but
  this shows 3 axes are *sufficient* for the observed 3D law, not that Actualization must
  choose exactly 3. 4 axes would give an unobserved ω⁴.

---

## Is 3 unique or merely sufficient?

| Criterion | Does it select exactly 3? | Verdict |
|---|---|---|
| symmetry (point group order 2^d·d!) | NO — 48 not special in {2,8,48,384,…} | not a selector |
| irreducible content | PARTIAL — vector sector dimension = d; observed 3-vector needs d = 3; 3D irrep exists for other d too | d=3 matches observed vector |
| rotation self-duality d(d−1)/2 = d | **YES — unique solution d = 3** | DERIVED (math identity) |
| information compression | NO — additive, linear, no extremum | not a selector |
| family structure | NO — family 3 is ring-level, decoupled (NP_037) | not a selector |
| spectral efficiency / DOS | NO — p = d identity; d = 3 matches observed ω³ only by correspondence | merely sufficient |
| gravity (d−2 factor, QG197) | d ≥ 3 (load-bearing); exact 3 NOT (QG197/NP_037: removing the exact value 3 breaks nothing) | merely sufficient |

**3 is MERELY SUFFICIENT for hosting the observed 3D world** (DOS ω³, blackbody, cubic O_h,
genuine 3D vector sector): every observable 3D hallmark is hosted by the d = 3 tensor
(NP_036/037/088/089, M_012), and d = 4, 5, … would host a consistent but unobserved dD
world. The exact axis count is therefore a HOSTED value, not a derived necessity.

**BUT 3 is UNIQUE in one precise derived sense — rotation self-duality**: the equation
d(d−1)/2 = d, the condition that the rotation generators form a vector of the same
dimension as the space (so(3) ≅ ℝ³; angular momentum a vector; cross product; su(2) ≅
so(3) acting on 3-vectors), has the single solution d = 3. Among all possible coupled-axis
counts this is the one structural identity that singles out 3 independently of observation.

---

## Determination

| Option | Verdict |
|---|---|
| A) Actualization dynamically forces exactly 3 axes | **NO — merely sufficient.** No spectral/information/family/DOS criterion has an extremum at d = 3; any d ≥ 1 gives a consistent dD tensor with p = d. |
| B) 3 is the unique axis count with rotation self-duality (so(3) ≅ ℝ³) | **YES — DERIVED.** d(d−1)/2 = d ⇒ d = 3 (unique for d ≥ 1). |
| C) 3 is the hosted value of the observed spatial dimension | **YES — CORRESPONDENCE** (NP_036/037: exact value 3 hosted; only d ≥ 3 load-bearing via (d−2), QG197). |
| D) the axis-3 is the family-3 | **REFUTED** (NP_037 decoupling re-confirmed). |
| E) information / spectral efficiency selects 3 | **REFUTED** (additive, monotone, no extremum). |
| F) genuine 3D vector sector from 3 coupled rings | **EMERGENT** (M_012, unchanged) — the 3-axis network is where it exists; its dimension 3 is the self-dual value. |

**Determination: the exact axis count 3 is NOT uniquely selected by Actualization dynamics —
it is MERELY SUFFICIENT as a hosted value (any d ≥ 3 hosts a consistent dD world; observed
space is 3D, CORRESPONDENCE). It is UNIQUE in exactly one derived structural sense: d = 3
is the only axis count whose rotation generators form a vector of the same dimension as the
space (d(d−1)/2 = d ⇒ so(3) ≅ ℝ³), which is the group-theoretic condition behind the
observed vectorial nature of rotations. M_012's genuine 3D sector is EMERGENT at that
self-dual value; NP_036/037's verdicts are confirmed, not reclassified. No new primitive;
canonical AT unchanged.**

---

## Classification

| Component | Status |
|---|---|
| point groups B_d = 2^d·d! (orders 2, 8, 48, 384, …) | **DERIVED** |
| defining rep of B_d irreducible, dimension d | **DERIVED** |
| DOS p = d identity | **DERIVED** (NP_035/036, unchanged) |
| rotation self-duality d(d−1)/2 = d ⇒ d = 3 | **DERIVED** (unique solution) |
| axis count 3 as a dynamically forced value | **REFUTED** (no internal selector) |
| information/family/spectral-efficiency extremum at 3 | **REFUTED** (additive/monotone/decoupled) |
| axis-3 = family-3 identification | **REFUTED** (NP_037) |
| exact value 3 as hosted spatial dimension | **CORRESPONDENCE** (NP_036/037, unchanged) |
| genuine 3D vector/p-wave sector of the 3-ring network | **EMERGENT** (M_012, unchanged) |

**M_013 separates "why the coupled construction can host the observed 3D world" (merely
sufficient, CORRESPONDENCE) from "why the hosted dimension is 3" (unique only via the
rotation self-duality identity d(d−1)/2 = d, DERIVED). Actualization itself does not count
axes; observation fixes 3, and self-duality is the one internal reason that count is not
arbitrary. No reclassification of NP_036/037/M_011/M_012; no new primitive; canonical AT
unchanged.**

---

## Theorem

> **Theorem (M_013).** The axis count of the coupled D96 construction is not uniquely
> forced by Actualization dynamics, but the count 3 is uniquely distinguished among all
> counts by rotation self-duality. Proof: (1) For d coupled rings on d orthogonal axes the
> axis point group is B_d of order 2^d·d! (Section 1); the orders {2,8,48,384,…} have no
> distinguished member. (2) The defining vector rep of B_d is irreducible of dimension d
> (Section 2); information is additive, d·log₂95 (Section 3); family count 3 is a
> single-ring span-window quantity independent of d (Section 4, NP_037 decoupling); DOS
> exponent p = d is an identity (Section 5). Therefore no spectral/information/family/DOS
> criterion selects d = 3: the value is merely sufficient to host observed 3D physics
> (CORRESPONDENCE, NP_036/037). (3) The rotation-generator sector has dimension d(d−1)/2;
> d(d−1)/2 = d iff d = 3 (unique integer solution d ≥ 1) — so only in three axes do the
> rotations form a vector of the same dimension as the space (so(3) ≅ ℝ³), the
> self-duality behind vectorial rotations and the observed 3D p-wave sector (M_012).
> Therefore: axis count 3 is merely sufficient (hosted) and uniquely self-dual (DERIVED);
> it is not dynamically forced. ∎
>
> *Proof sketch.* Compute point groups and irrep content for d = 2,3,4,N (Sections 1–2);
> test information, families, DOS (Sections 3–5); solve self-duality (Section 2);
> classify (Determination). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → attractor ring C96(±1..±6) — Aut = D₉₆ (QG155); spectrum (D_041)
 → coupled rings D96⊗d — point group B_d = 2^d·d! (d=3 → O_h)
 → DOS exponent p = d (Weyl identity, NP_035/036)
 → 3D world hosted on d=3 tensor (NP_036/037; only d≥3 load-bearing, QG197)
 → AXIS-COUNT SELECTION AUDIT (M_013)
    → no internal selector of exactly 3 (info/family/DOS)   [REFUTED — merely sufficient]
    → exact 3 hosted by observed 3D space                   [CORRESPONDENCE]
    → rotation self-duality d(d−1)/2 = d ⇒ d = 3            [DERIVED — unique]
    → genuine 3D vector sector at d = 3                     [EMERGENT — M_012, unchanged]
```

---

## Falsification Path

1. **"Merely sufficient"** is falsified by any internal quantity with a strict extremum at
   d = 3 (none found: point-group order, info, distinct levels, DOS, families all vary
   monotonically or are d-generic).
2. **"Unique self-duality"** is falsified by an integer d ≥ 2 with d(d−1)/2 = d (none
   exists; equation proven unique at d = 3).
3. **"3 hosted"** is the NP_036/037 status: it would be overturned only by a canonical
   derivation of the exact value 3 from the D96 boundary set alone — already FALSIFIED by
   NP_037 (remove exact 3 → nothing breaks).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_013_Tests.cs`
**Run:** 2026-09-08 · **Result:** see `Tests/Results/Y_M_013_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_M_013_PointGroups` | B_d orders 2^d·d! = 2, 8, 48, 384 | ✅ |
| `Y_M_013_IrreducibleContent` | D₄ max 2; O_h has genuine 3D irrep; defining rep irreducible dim d | ✅ |
| `Y_M_013_RotationSelfDuality` | d(d−1)/2 = d ⇒ unique d = 3 (so(3) ≅ ℝ³) | ✅ |
| `Y_M_013_InformationAdditive` | info = d·log₂95; linear, no extremum at 3 | ✅ |
| `Y_M_013_FamilyDecoupled` | family 3 ring-level, independent of axis count | ✅ |
| `Y_M_013_SpectralEfficiency` | DOS p = d; distinct-level ratio monotone, no extremum | ✅ |
| `Y_M_013_Classification` | verdict table | ✅ |
| `Y_M_013_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_013"`

---

## References

- ResearchY-NP_036 (3D DOS emergence — ω³ hosted on D96⊗3), NP_037 (role of three — axis/
  family decoupling; exact 3 hosted), NP_088 (network geometry O_h), NP_089 (approximate
  O(3)), M_011 (single ring O(3) audit), M_012 (network genuine 3D sector).
- AT-QG: QG197 (2D→3D bridge; (d−2) Einstein factor), QG2 (d ≥ 3), QG290
  (dimension-generic metric).
- D_020 (span window [4,8) BOUNDARY), D_041 (D96 spectrum).
