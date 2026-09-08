# ResearchY-M_011 — Effective O(3) Symmetry Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** M — Measurement Origin
**ID:** ResearchY-M_011 (permanent)
**Title:** Effective O(3) Symmetry Audit
**Status:** COMPLETE
**Date:** 2026-09-08
**File:** `M_Measurement/ResearchY-M_011.md`
**Depends on:** ResearchY-NP_087 (nuclear structure), NP_088 (network geometry), NP_089
(rotational symmetry emergence), NP_023 (O(2) mirror search), D_041 (D96 spectrum),
AT-QG QG155 (D96 automorphism group = dihedral), QG161 (gauge origin 1+3+8),
QG185/196 (Bekenstein quarter), ResearchY-B_001/B_002/B_003 (π origin)
**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_011_Tests.cs`

---

## Purpose

**Can an effective O(3) symmetry emerge from D96 actualization dynamics?** NP_087/088/089
established that the D96 ⊗ D96 ⊗ D96 network is a *cubic* lattice whose symmetry is the
finite octahedral group O_h (irreps of dimension 1, 2, 3), NOT rotational O(3); O(3) is only
*approximate* (leading-order isotropy of the free dispersion, broken at O((ka)²) by the cubic
invariant). M_011 audits the *group-theoretic floor* of that verdict: it derives the full
automorphism-group content of the seed ring C96(±1..±6), searches for effective rotational
sectors inside the D96 spectrum, and tests each O(3)-hallmark observable (spherical-harmonic
degeneracies, shell patterns, π, horizon-area scaling, nuclear closures) against the D96
multiplicity structure. **Success criterion:** determine whether an effective O(3) — exact or
approximate — is present in the D96 spectral content, and classify each component. No new
primitive; canonical AT unchanged.

---

## 1. Automorphism groups of C96(±1..±6)

**Definition.** C96(±1..±6) is the 12-regular circulant ring on N = 96 sites whose connection
set is S = {±1, ±2, ±3, ±4, ±5, ±6} (the D96 attractor: degree 12 = 1 + 3 + 8, QG161).
Its eigenvalues are λ_k = Σ_{s=1..6} 2(1 − cos(2πks/96)), k = 0..95 (D_041).

**Graph automorphisms.** Let r: k → k+1 (mod 96) be the rotation (translation on Z₉₆) and
s: k → −k (mod 96) the reflection. Both preserve the adjacency of C96(±1..±6): |Δ| ∈ {1..6}
is invariant under both maps (verified computationally). Hence the dihedral group
D₉₆ = ⟨r, s⟩ with r⁹⁶ = 1, s² = 1, s·r·s = r⁻¹, |D₉₆| = 192, is a subgroup of the graph
automorphism group.

**Multiplier automorphisms.** The remaining candidate automorphisms of a circulant are the
*multiplier* maps k → a·k (mod 96) for a coprime to 96 (a ∈ units(96)). A multiplier is a
graph automorphism iff a·S = S as a set. Computation over all a ∈ units(96) (|units(96)| =
φ(96) = 32) gives **exactly {1, 95}** (i.e. a = ±1): the multiplier subgroup is trivial
(beyond the reflection s already counted). No non-dihedral multiplier automorphism exists.
Therefore **Aut(C96(±1..±6)) = D₉₆**, the full dihedral group of order 192.

| Group | Order | Role |
|---|---|---|
| rotation subgroup ⟨r⟩ = Z₉₆ | 96 | the discrete "rotational" sector of the ring |
| reflection subgroup (all s·rⁱ) | 96 | mirror pairing k ↔ 96−k (Z₂ doublets) |
| full **Aut = D₉₆** = ⟨r, s⟩ | 192 | irrep content: 4 × 1D + 47 × 2D (4·1 + 47·4 = 192) |
| multiplier group stabilizing S | 2 (±1) | no extra affine symmetry of the connection set |
| units(96) acting on mode index | 32 | discrete permutations within gcd classes (NP_023), NOT graph automorphisms |

The irreducible representations of D₉₆ are 1- and 2-dimensional only (47 doublets). No
irrep of dimension 3, 5, 7, … (the O(3) ladder dimensions) exists inside Aut(C96(±1..±6)).

---

## 2. Effective rotational sectors

A "rotational sector" of O(3) would be a multiplet of degenerate modes transforming under a
3-dimensional (or higher) irrep of a rotation-like group, giving degeneracies
2l+1 = 1, 3, 5, 7, … . Search results inside the D96 spectrum:

| Candidate | Found? | Reason |
|---|---|---|
| continuous rotation mixing distinct modes | NO | per-mode phase SO(2) is the ONLY continuous rotation; it never mixes distinct k (NP_023) |
| rotational sector of dimension 3 | NO | max irrep dimension in Aut = D₉₆ is 2; the ring is a planar object |
| O(3) ladder {1,3,5,7,…} | NO | multiplicity content is {2, 5, 6} (below), not a 2l+1 ladder |
| Z₂ mirror doublet (spin-½-like SU(2) carrier) | YES | the 2D irreps of D₉₆ generate the weak-isospin doublets (QG155/QG161) |

The largest rotation-like symmetry of a *single* D96 ring is the finite cyclic group Z₉₆
acting on the 96 sites — a discrete planar rotation, not an O(3). Full 3D rotational content
first requires the tensor product D96⊗D96⊗D96, whose symmetry is the finite octahedral group
O_h (order 48, irreps {1,2,3}), still not O(3) (NP_088).

---

## 3. Degeneracy structure

Computed exactly from λ_k = Σ_s 2(1−cos(2πks/96)), k = 1..95:

- **95 positive modes**, zero mode k=0 excluded (uniform background).
- **44 distinct eigenvalues**; multiplicities sum to 95.
- Multiplicity histogram: **42 doublets (mult 2) + one 5-fold (λ = 12) + one 6-fold (λ = 14)**.
- All 95 modes are mirror-paired: λ_k = λ_{96−k} exactly (0 mismatches).
- The accidental multiplicities are the octave-ladder blocks: λ = 12 on
  {16, 32, 48, 64, 80} (5-fold) and λ = 14 on {8, 24, 40, 56, 72, 88} (6-fold), produced by
  the octave structure (D_030) and giving the canonical [4,4,87] family occupancy.
- Maximal multiplicity = 6; every distinct eigenvalue has mult ≥ 2 (complete pairing, D_035).

The degeneracy content of the D96 spectrum is therefore the discrete set {2, 5, 6} — a planar
(O(2)-type) mirror structure plus octave accidents. It is NOT a (2l+1) ladder.

---

## 4. Comparison to spherical harmonics

Spherical harmonics Y_lm for a given l come with degeneracy 2l+1 = 1, 3, 5, 7, 9, … , the
irrep dimension of O(3).

| D96 multiplicity m | present? | 2l+1 = m? |
|---|---|---|
| 2 (42×, mirror doublets) | YES | NO (2 is not an odd 2l+1) |
| 5 (λ=12 block) | YES | YES (l = 2) — coincidence via octave arithmetic, not O(3) |
| 6 (λ=14 block) | YES | NO (6 is even; not an O(3) irrep dimension) |
| 1, 3, 7, 9, … (l = 0,1,3,4,…) | NO | absent |

The overlap of the D96 multiplicity set {2, 5, 6} with the spherical-harmonic set {1,3,5,7,…}
is exactly {5}. There is no l-ladder, no odd-degeneracy tower, and the 5-fold block is a
periodicity coincidence of the ring (k ∈ {16,32,48,64,80} all share the octave), not a
spherical-harmonic multiplet. **The D96 spectrum does not reproduce the spherical-harmonic
degeneracy structure.**

---

## 5. Shell patterns

Shell closures in 3D physics are cumulative degeneracies:
- 3D harmonic-oscillator shells: (N+1)(N+2)/2 × 2 = 2, 8, 20, 40, 70, 112 (with spin);
- spherical shells with spin-orbit (nuclear magic numbers): 2, 8, 20, 28, 50, 82, 126.

Testing the D96 structure for shell-like closure counts:

| Sequence | Values | Matches D96? |
|---|---|---|
| HO closures (spin) | 2, 8, 20, 40, 70, 112 | NO — D96 cumulative by ordered distinct eigenvalues runs 2,4,6,8,10,… |
| magic numbers | 2, 8, 20, 28, 50, 82, 126 | NO (only cumulative 8 is hit, trivially, by 4 doublets) |
| D96 octave occupancy cumulative | 4, 8, 95 | [4,4,87] — only the value 8 coincides |

The D96 spectral cumulative multiplicities advance in steps of 2 (doublets) then jump by the
octave blocks 5 and 6 — they do not close at 2, 8, 20, 28, 50, 82, 126. **No shell pattern
consistent with 3D shell closures is present** (confirms NP_087/NP_166 for the spectral side).

---

## 6. Test of the appearance of π

The D96 Laplacian is an integer matrix; its spectrum is therefore a set of ALGEBRAIC numbers
(eigenvalues of a matrix with integer entries are algebraic integers — in fact sums of
2(1−cos(2πks/96)) with k,s integers, which are algebraic). π is TRANSCENDENTAL. Therefore:

- **π can never equal any D96 eigenvalue or any polynomial/rational combination of them.**
- Approximate look-alikes exist but are not exact: span/2 = 6.4025…/2 = 3.2013 ≈ π
  (deviation ~1.9%), √10 = 3.1623 ≈ π (0.7%); none is π.
- The ROLE of π (circumference/diameter ratio in the continuous circle, 2π phase advance
  θ = 2πk/N per cycle) EMERGES from the ring geometry (B_001/B_003), but the VALUE π is a
  BOUNDARY: it never appears in the algebraic D96 content (B_002, QG291).

**Verdict: π does NOT appear as a value in the D96 spectrum — only its role can emerge.**
Any claimed appearance of π in rotational-symmetry observables (e.g., a 1/(8π) horizon
coefficient, or 4π solid-angle factors) must be imported, not derived from the algebraic ring.

---

## 7. Test of horizon-area scaling

The Bekenstein relation S = A/4 connects entropy to horizon area. AT's established result
(QG185, strengthened by QG196): the STRUCTURE S ∝ A is fully derived (QG12 boundary counting,
QG184 M ∝ R and T ∝ 1/R), but the exact coefficient 1/4 CANNOT be derived from D96 without
fitting and without importing π:

| Construction | S/A coefficient | Value |
|---|---|---|
| QG12 boundary counting | ln2/(4π) | 0.0552 |
| deficit first-law | 1/(8π) | 0.0398 |
| Bekenstein 1/4 | 1/4 | 0.25 (forces π bits/cell imported) |
| D96 candidate 1/occ₀ | 1/4 = 4/16 | label identity only; wrong units (gives 1/(16π) physical) |

Since the D96 spectrum is algebraic (Section 6), no 8π or 4π factor can be produced by the
ring's own numbers. **Horizon-area scaling S ∝ A is EMERGENT as a structure; the exact
Bekenstein quarter is REFUTED within D96** (it requires the imported 2π quantum factor
T = κ/(2π)). No effective O(3) sphere geometry (4π area, solid angles) exists to supply it.

---

## 8. Test of nuclear shell closures

Nuclear magic numbers [2, 8, 20, 28, 50, 82, 126] follow from 3D (2l+1) spherical-harmonic
shells plus spin-orbit. Tests on the D96 spectrum:

| Test | Result |
|---|---|
| any eigenvalue degeneracy sequence 1,3,5,7,… (2l+1) | NO — mult set {2,5,6} (Section 3) |
| cumulative closures at 2, 8, 20, 28, 50, 82, 126 | NO (Section 5) |
| O(3) group needed for the l-ladder | ABSENT (Aut = D₉₆, max irrep dim 2; cubic network = O_h) |
| exact closures from cubic O_h lattice | REFUTED — NP_087/088/089 |

**Verdict: nuclear shell closures are NOT reproduced by the D96 structure.** This confirms —
rather than reverses — NP_087 (nuclear structure MISSING) and NP_109 (weakest link =
nuclear structure). An exact magic-number derivation from the cubic lattice remains AT's
single most-likely falsifier (NP_108/NP_109), and M_011's group-theoretic audit reinforces
why: the degeneracy machinery of the theory is planar + octahedral, never rotational.

---

## Determination

| Option | Verdict |
|---|---|
| A) exact O(3) rotational symmetry in the D96 spectrum | **NO — REFUTED.** Aut = D₉₆ (finite dihedral, max irrep dim 2); multiplicity set {2,5,6} ≠ {1,3,5,7,…}; no O(3) irrep tower. |
| B) effective O(3) as a *leading-order isotropy* of the emergent network | **YES — EMERGENT.** The free dispersion is isotropic to leading order (NP_089); the exact O(3) limit (a→0) is the unattained boundary. |
| C) spherical-harmonic / nuclear-shell / magic-number content | **REFUTED.** (Sections 4, 5, 8). |
| D) π value or exact horizon quarter S = A/4 from D96 | **REFUTED** (transcendental vs algebraic; coefficient needs imported 2π, Sections 6, 7). |
| E) observed QM-like 3D rotational world as an interpretation of the discrete theory | **CORRESPONDENCE** — the leading-order isotropic sector reproduces QM rotational observables; exact O(3) is not a D96 object. |

**Determination: D96 actualization dynamics do NOT generate an exact effective O(3).** The
symmetry content of the seed ring is the finite dihedral group D₉₆ (planar, max irrep
dimension 2); the 3D network reaches only the finite cubic group O_h (NP_088); O(3) appears
only as the leading-order approximation of the coarse-grained dispersion (EMERGENT, NP_089),
with every exact-O(3) hallmark (2l+1 degeneracies, spherical-harmonic tower, shell patterns,
π value, Bekenstein quarter, nuclear magic numbers) REFUTED.

---

## Classification

| Component | Status |
|---|---|
| Aut(C96(±1..±6)) = D₉₆, |D₉₆| = 192, rotation Z₉₆, reflection, trivial multiplier group | **DERIVED** |
| 2D irreps of D₉₆ → Z₂ doublets (weak-isospin carrier) | **DERIVED** (QG155/QG161) |
| degeneracy structure (95 modes; 44 distinct; {2×42, 5, 6}; mirror exact) | **DERIVED** |
| exact O(3) rotational sectors / (2l+1) ladder in the D96 spectrum | **REFUTED** |
| spherical-harmonic degeneracy correspondence | **REFUTED** (only accidental overlap {5}) |
| shell patterns / magic numbers | **REFUTED** |
| π value in the (algebraic) D96 content | **REFUTED** (role EMERGENT — B_001/B_002) |
| horizon-area structure S ∝ A | **EMERGENT** (derived structure QG185) |
| exact Bekenstein quarter 1/4 | **REFUTED** (needs imported π/2π, QG196) |
| nuclear shell closures | **REFUTED** (confirms NP_087/NP_089/NP_166) |
| leading-order large-scale isotropy (approximate O(3)) | **EMERGENT** (NP_089, unchanged) |

**M_011 confirms, from the automorphism-group side, the NP_087/088/089 verdict: there is no
exact or exact-effective O(3) in D96 content — only the finite planar D₉₆, the finite cubic
O_h, and the leading-order approximate isotropy. No reclassification of any prior audit is
made. No new primitive; canonical AT unchanged.**

---

## Theorem

> **Theorem (M_011).** No exact effective O(3) symmetry emerges from the D96 spectrum. Proof:
> (1) Aut(C96(±1..±6)) = D₉₆ = ⟨r,s⟩, |D₉₆| = 192, with irreps 4 × 1D + 47 × 2D only — no
> irrep of dimension ≥ 3 exists (Section 1, multiplier computation verified: the only
> multiplier stabilizing S = {±1..±6} is a = ±1). (2) The spectrum has multiplicities
> {2, 5, 6} (42 doublets + the octave 5-fold λ=12 + 6-fold λ=14) — not a 2l+1 ladder
> (Section 3). (3) Spherical-harmonic degeneracies require odd dimensions 2l+1; the D96
> overlap is the single accidental {5} (Section 4). (4) Shell closures (2,8,20,40,70 and the
> magic numbers) do not coincide with any D96 cumulative multiplicity (Section 5). (5) The D96
> spectrum is algebraic; π is transcendental, so no π value appears; only its role is
> available (Section 6). (6) The exact horizon coefficient 1/4 cannot be produced by the
> algebraic ring without importing π/2π (Section 7). (7) Nuclear magic numbers are absent
> (Section 8). Therefore D96 actualization dynamics host at most the finite planar rotation
> Z₉₆, the finite cubic O_h, and the leading-order approximate isotropy — never an exact
> O(3). ∎
>
> *Proof sketch.* Compute Aut and its irreps (Sections 1–2); compute the degeneracy histogram
> (Section 3); test each O(3)-hallmark observable (Sections 4–8); classify (Determination). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → Attractor ring C96(±1..±6) — Aut = D₉₆ (QG155)
 → spectrum λ_k (D_041) — degeneracies {2×42, 5, 6}, mirror exact (NP_023)
 → 3D network D96⊗D96⊗D96 — cubic O_h, NOT O(3) (NP_088)
 → leading-order isotropy — approximate O(3) only (NP_089)
 → EFFECTIVE O(3) AUDIT (M_011)
    → no O(3) irrep in Aut (max dim 2)           [REFUTED]
    → no (2l+1) degeneracy ladder                 [REFUTED]
    → no shell / magic-number closures            [REFUTED]
    → π value absent (algebraic spectrum)         [REFUTED; role EMERGENT]
    → exact horizon quarter absent                [REFUTED; structure EMERGENT]
    → nuclear closures absent                     [REFUTED — NP_087/089 confirmed]
```

---

## Falsification Path

1. **Exact O(3)** is falsified by construction: Aut(C96(±1..±6)) is the finite group D₉₆ with
   no irrep of dimension ≥ 3; no continuum limit exists in the discrete theory (a→0 never
   reached).
2. **The M_011 verdict** would be overturned by: a D96 (or D96⊗D96⊗D96) degeneracy sequence
   equal to a 2l+1 ladder; a shell closure at a magic number from the D96 spectrum; a D96
   combination equal to π; an exact Bekenstein 1/4 without an imported π/2π factor.
3. **Nuclear structure** remains the weakest link (NP_109): an exact magic-number derivation
   from the cubic lattice would falsify the structural core. M_011 shows the group-theoretic
   reason no such derivation can come from Aut = D₉₆ or O_h.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_011_Tests.cs`
**Run:** 2026-09-08 · **Result:** see `Tests/Results/Y_M_011_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_M_011_AutomorphismGroup` | Aut = D₉₆ (order 192; r,s automorphisms; multipliers ±1 only) | ✅ |
| `Y_M_011_RotationalSectors` | no O(3)/2l+1 sector; max irrep dim 2 | ✅ |
| `Y_M_011_DegeneracyStructure` | 95 modes, 44 distinct, {2×42, 5, 6}, mirror exact | ✅ |
| `Y_M_011_SphericalHarmonics` | mult set {2,5,6} ∩ 2l+1 = {5} only | ✅ |
| `Y_M_011_ShellPatterns` | no HO/magic closures | ✅ |
| `Y_M_011_PiAppearance` | π value absent (algebraic spectrum) | ✅ |
| `Y_M_011_HorizonArea` | S∝A structure; exact 1/4 REFUTED | ✅ |
| `Y_M_011_NuclearClosures` | magic numbers not reproduced | ✅ |
| `Y_M_011_Classification` | verdict table | ✅ |
| `Y_M_011_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_011"`

---

## References

- ResearchY-NP_023 (O(2) mirror search — degeneracy structure of D96), NP_087 (nuclear
  structure), NP_088 (cubic network geometry), NP_089 (O(3) approximate), NP_108/NP_109
  (falsification / weakest link), NP_166 (shell capacities 2n² REFUTED).
- ResearchY-B_001/B_002/B_003 (π role/value; 2π origin).
- AT-QG: QG155 (D96 dihedral automorphism group), QG161 (1+3+8 gauge generators = degree of
  C96(±1..±6)), QG185/QG196 (Bekenstein quarter impossibility).
- D_041 (D96 spectrum λ_k), D_035 (complete pairing), D_030 (octave structure).
