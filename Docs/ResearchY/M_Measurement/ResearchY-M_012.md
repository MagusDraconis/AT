# ResearchY-M_012 — Network O(3) Symmetry Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** M — Measurement Origin
**ID:** ResearchY-M_012 (permanent)
**Title:** Network O(3) Symmetry Audit
**Status:** COMPLETE
**Date:** 2026-09-08
**File:** `M_Measurement/ResearchY-M_012.md`
**Depends on:** ResearchY-M_011 (single-ring O(3) audit = NO), NP_088 (network geometry),
NP_089 (approximate O(3)), NP_087 (nuclear structure), QG155 (D96 automorphism group),
QG185/196 (Bekenstein quarter), ResearchY-B_001/B_002/B_003 (π origin), D_041 (D96 spectrum)
**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_012_Tests.cs`

---

## Purpose

**Can effective O(3), spherical symmetry, or shell degeneracies emerge from a NETWORK of
coupled D96 systems, even if a single D96 cannot?** M_011 established that a *single* D96
ring carries Aut = D₉₆ (max irrep dimension 2) — no genuine 3-dimensional representation,
no O(3), no (2l+1) degeneracy tower. M_012 tests ONLY multi-D96 structures: the pair
D96⊗D96, the triad D96⊗D96⊗D96, three orthogonal D96 axes, lattice couplings, and the
resulting tensor-product representations. The central structural question is whether the
*tensor network* — whose site point-group content is the octahedral group O_h (NP_088,
order 48) — supplies the dimension-3 irreducible sectors that the single ring lacks, and
whether that content reproduces any O(3)-hallmark observable (3D representations, 2l+1
degeneracies, spherical-harmonic collective modes, π, shell closures, horizon-area
scaling). **Success criterion:** determine whether a genuine 3-dimensional (p-wave / vector)
irreducible sector exists in the network content, and classify every O(3)-hallmark test as
DERIVED / EMERGENT / CORRESPONDENCE / FIT or REFUTED. No new primitive; canonical AT
unchanged.

---

## 1. Automorphism groups of multi-D96 structures

Let C96 = C96(±1..±6) be the 12-regular D96 attractor ring (Aut = D₉₆, |D₉₆| = 192; M_011).
The D96⊗D96⊗D96 network is the 3D cubic lattice — the Cartesian product C96□C96□C96 of
three copies (NP_088), or equivalently three mutually orthogonal D96 rings whose axis
directions span the three coordinate axes.

| Structure | Symmetry / automorphism content | Order |
|---|---|---|
| single ring C96 | Aut = D₉₆ (dihedral) | 192 |
| two rings C96□C96 | contains D₉₆ × D₉₆ ⋊ Z₂ (axis swap) | ⊇ 192²·2 = 73,728 |
| three rings C96□C96□C96 | contains D₉₆ × D₉₆ × D₉₆ ⋊ S₃ (axis perm.) | ⊇ 192³·6 = 42,467,328 |
| **3 orthogonal axes (site / point group)** | signed permutations of 3 axes = **O_h** (octahedral) | 48 |

Key: the single ring's automorphism group is *planar* (dihedral, irreps of dimension ≤ 2).
Once three rings are coupled on three orthogonal axes the natural axis-arrangement symmetry
is the signed-permutation group of the three coordinates — the **octahedral group O_h**, the
symmetry group of a cube, of order 48, which is a finite subgroup of O(3).

**Irreducible content of O_h.** The character table of O_h has irrep dimensions
{1, 1, 2, 3, 3, 1, 1, 2, 3, 3} (Σ d² = 1+1+4+9+9+1+1+4+9+9 = 48). In particular O_h
possesses **genuine 3-dimensional irreps** (T₁g, T₂g, T₁u, T₂u). This is the decisive
upgrade over the single ring, whose largest irrep is 2-dimensional.

---

## 2. Irreducible representations and the 3D (vector/p-wave) sector

Representations of O_h obtained by restricting the defining 3-dimensional real rotation
representation of O(3) to the octahedral subgroup:

- The defining representation of O_h on ℝ³ (signed permutation matrices) is **irreducible**
  (character inner product ⟨χ,χ⟩ = 1). It is the restriction of the O(3) l = 1 (vector /
  p-wave) representation.
- **2l+1 = 3 triplets are therefore exact O_h sectors.** The p-wave triplet of spherical
  harmonics restricts irreducibly to O_h (T₁u). No cubic field can split it — this is the
  standard crystal-field fact that p-orbitals remain 3-fold degenerate under octahedral
  symmetry.
- Subduction of the O(3) ladder to O_h: l = 0 → A₁g (1); **l = 1 → T₁u (3, stays full)**;
  l = 2 → E_g ⊕ T₂g (2 + 3, splits); l = 3 → A₂u ⊕ T₁u ⊕ T₂u (1 + 3 + 3, splits);
  l = 4 → A₁g ⊕ E_g ⊕ T₁g ⊕ T₂g. Verified by characters: the l-restriction has norm² 1, 1,
  2, 3, 4 for l = 0..4 (norm² = 1 means irreducible).

**Conclusion: the coupled D96 network supplies the genuine 3D irreducible sector that the
single ring lacks — the p-wave / vector (l = 1) triplet, with exact degeneracy 2l+1 = 3.**
This sector exists only because the axes number three (d = 3 geometry); it does NOT exist in
one or two coupled rings.

---

## 3. Search for 3D sectors and 2l+1 degeneracy patterns

### 3D sectors
A "genuine 3D representation" means an irreducible representation of dimension 3. Search:

| Candidate | Dimension | Present in | Verdict |
|---|---|---|---|
| vector/p-sector (defining rep of O_h) | 3 | O_h (site symmetry of the 3-axis network) | **YES — irreducible, exact** |
| 2D sectors (E_g, E_u) | 2 | O_h | YES (from l=2,4,…) |
| sectors of dimension 5, 7 (l=2,3 spherical) | 5, 7 | O_h | **NO — no irrep of dim 5,7 in O_h** |

### 2l+1 degeneracy patterns in the joint (tensor) spectrum
The triad spectrum E = λ_a + λ_b + λ_c (sums of three ring eigenvalues) is invariant under
O_h (permutations of axes + per-axis sign reflection). Actual degeneracy of joint levels was
computed over all 96³ triplets: distinct joint levels number 16,080 and the low-lying
levels carry multiplicities {6, 12, 8, 24, …} — orbit sizes of the octahedral group on
integer wave-vector triples (6 = ±x̂,±ŷ,±ẑ; 12 = face-diagonal pairs; 8 = body diagonals),
NOT the (2l+1) = {1,3,5,7,9,…} ladder of O(3).

| Degeneracy | Source | 2l+1 match? |
|---|---|---|
| 6 (first excited joint level, λ₁ + 0 + 0) | orbit {±e_x, ±e_y, ±e_z} | NO (6 ≠ odd) |
| 3 (p-triplet T₁u) | irreducible O_h sector inside the 6 | **YES — l=1 exact** |
| 12, 8, 24 (higher joint levels) | O_h orbits on k-space lattice | NO |
| 5, 7 (would-be d, f shells) | absent as exact degeneracy | NO (split 2+3, 1+3+3) |

The exact degeneracies of the coupled spectrum are O_h orbit sizes; the only spherical-shell
value they contain is the p-triplet 3 = 2·1+1, realized as an irreducible sector. The full
ladder {1,3,5,7,…} does not appear — d, f, g shells are split by the cubic (octahedral)
field into O_h multiplets.

---

## 4. Spherical harmonics as collective modes

Spherical harmonics Y_lm are the collective angular modes of O(3). In the coupled D96
network, the continuous rotation group is not present — the axis arrangement gives only the
finite octahedral group O_h. Subduction table (verified by characters):

| l | dim 2l+1 | O_h content | Full degeneracy kept? |
|---|---|---|---|
| 0 | 1 | A₁g | YES |
| 1 | 3 | T₁u | **YES — p-wave triplet irreducible under O_h** |
| 2 | 5 | E_g ⊕ T₂g | NO (2+3 split) |
| 3 | 7 | A₂u ⊕ T₁u ⊕ T₂u | NO (1+3+3 split) |
| 4 | 9 | A₁g ⊕ E_g ⊕ T₁g ⊕ T₂g | NO |

So the lowest spherical multiplet beyond the trivial one — the p-wave triplet (l = 1) — IS a
genuine collective sector of the network, exactly degenerate under the emergent octahedral
symmetry. All higher l split. This is precisely why a cubic (octahedral) environment keeps
p-levels triply degenerate but lifts the d-shell degeneracy (5 → 2 + 3) — a textbook
crystal-field statement, now derived from the D96 tensor network's O_h content.

---

## 5. Shell patterns

3D shell closures require the full rotational ladder with (2l+1) degeneracies closed by a
confining (harmonic) potential and/or spin-orbit.

| Closure | What is required | Present in coupled D96 network? |
|---|---|---|
| 2, 8, 20, 40, 70 (3D HO with spin) | l = 0,1,2,3,4 shells all full | NO — only l ≤ 1 stays full under O_h; d, f split |
| 2, 8, 20, 28, 50, 82, 126 (nuclear magic numbers) | full ladder + spin-orbit | NO |
| p-triplet 3 = 2·1+1 | l = 1 sector only | **YES** (exact O_h irrep) |

The network reproduces exactly one step of the shell structure — the s→p closure scale
(degeneracies 1 then 3) — and then the ladder breaks: the would-be d-shell (5) is split
2 + 3 by the octahedral field. Nuclear magic numbers are not reproduced (confirms
NP_087/NP_088/NP_109). The p-triplet closure is real and exact but it is not a shell-closure
tower.

---

## 6. Test of the appearance of π

The coupled-network spectrum is the set of sums λ_a + λ_b + λ_c of ring eigenvalues, each
λ_k = Σ_s 2(1−cos(2πks/96)) an algebraic number. Sums/products of algebraic numbers are
algebraic, so every joint eigenvalue remains ALGEBRAIC; π is transcendental.

- Computed: min |joint level − π| = 0.095, |2π| deviation 0.19, |4π/3| deviation 0.55 —
  no joint level equals π, 2π, or 4π/3. No linear combination found equal to a π-multiple.
- π's ROLE (as the circle constant of the emergent 3D geometry and in asymptotic
  lattice-point counting — e.g. the count of integer triples in a ball ~ (4π/3)R³, the
  Weyl-law coefficient of the 3D DOS) is EMERGENT at large scale, exactly as in M_011/NP_089.
- π's VALUE never appears as an algebraic joint eigenvalue; the transcendental value remains
  a BOUNDARY (B_002, QG291) — network topology does not change this.

**Verdict: π does NOT appear as a value in the coupled-network spectrum — role EMERGENT,
value BOUNDARY/REFUTED, unchanged from M_011.**

---

## 7. Test of horizon-area scaling

The Bekenstein relation S = A/4 in the network setting inherits the single-ring verdict:
S ∝ A is a derived structure (QG185), but the exact coefficient 1/4 requires an imported
2π quantum factor (QG196) that no algebraic (π-free) spectrum can supply. The coupled D96
network, having only octahedral (finite, discrete) symmetry and an algebraic spectrum,
produces no O(3) sphere geometry and hence no continuum 4π/A factors:

| Construction | S/A coefficient | Value |
|---|---|---|
| QG12 boundary counting | ln2/(4π) | 0.0552 |
| deficit first-law | 1/(8π) | 0.0398 |
| Bekenstein 1/4 | 1/4 | 0.25 (needs imported 2π) |
| network (algebraic spectrum) | cannot produce 8π/4π | — |

**Verdict: horizon-area structure S ∝ A EMERGENT (unchanged); the exact Bekenstein quarter
is REFUTED in the network, exactly as in the single ring (M_011/QG196).**

---

## 8. Test of nuclear shell closures

Nuclear magic numbers require the exact 2l+1 ladder plus spin-orbit splitting inside a
rotationally symmetric mean field. In the coupled D96 network:

| Test | Result |
|---|---|
| l = 0, 1 shells with exact (2l+1) degeneracy (1, 3) | **YES — s and p survive octahedral symmetry** |
| l ≥ 2 shells with exact degeneracy (5, 7, 9) | NO — split by O_h into 2+3, 1+3+3, … |
| cumulative closures 2, 8, 20, 28, 50, 82, 126 | NO (only a 1, 3, 6(=1+? counting states) s,p pattern) |
| nuclear magic numbers | **REFUTED** (confirms NP_087/NP_088/NP_109) |

The octahedral network is the correct *crystal-field* arena (s, p unsplit; d, f, g split),
which reproduces atomic-like cubic field splitting but not nuclear (rotational) shell
closures. M_011's nuclear verdict is unchanged: the p-wave sector is the only genuine
spherical triplet, and it is not enough to build magic numbers.

---

## Critical questions

| Question | Answer |
|---|---|
| Can three coupled D96 systems produce a genuine 3D representation? | **YES — EMERGENT.** The 3-axis arrangement's point group O_h has irreducible 3D reps (T₁u etc.), realized as the exact vector/p-sector. Two coupled rings cannot (max dim 2 inherited); three orthogonal axes can. |
| Does O(3) emerge only in the thermodynamic limit? | **PARTIAL.** The finite group O_h is exact at every size; full O(3) appears only as the NP_089 leading-order isotropy of the coarse-grained dispersion (a→0 unattained in the discrete theory). |
| Can spherical harmonics appear as collective modes? | **PARTIAL — YES for l = 0, 1.** The p-wave triplet (l=1, 3-fold) is an exact O_h irrep. l ≥ 2 split; they are not collective modes of the discrete network. |
| Can π arise from network topology rather than from a single spectrum? | **NO as a value** (algebraic joint spectrum); **YES as an asymptotic role** (Weyl lattice-point counting ~ (4π/3)R³, emergent DOS coefficient). |

---

## Determination

| Option | Verdict |
|---|---|
| A) a genuine 3D irreducible sector from coupled D96 | **YES — EMERGENT.** O_h (the point group of 3 orthogonal D96 axes, order 48) has 3D irreps; the p/vector triplet (l = 1) is exact. This is the multi-ring upgrade over M_011's single-ring NO. |
| B) exact O(3) / spherical symmetry | **REFUTED.** O_h is finite (48), a subgroup of O(3), not O(3). Approximate O(3) at large scale remains EMERGENT (NP_089). |
| C) (2l+1) degeneracy ladder / shell closures / magic numbers | **REFUTED beyond l = 1.** The s,p shells (1,3) are exact; d,f,g split (2+3, 1+3+3). |
| D) π value from network topology | **REFUTED as value** (algebraic spectrum); role EMERGENT in the thermodynamic/continuum counting. |
| E) horizon-area quarter S = A/4 | **REFUTED** (unchanged, QG185/QG196); S ∝ A structure EMERGENT. |

**Determination: the coupled D96 NETWORK — but only the three-axis octahedral arrangement —
contains a genuine 3D irreducible sector (the vector/p-wave triplet, l = 1, degeneracy 3),
which the single ring does not. This is the sole exact spherical-harmonic multiplet the
network produces; every higher O(3) hallmark (full 2l+1 ladder, d/f/g degeneracies, shell
closures, magic numbers, π value, exact Bekenstein quarter) is REFUTED. The emergent 3D
content is octahedral (crystal-field-like), not rotational. Consistent with NP_088/NP_089;
M_011's verdict is not reclassified — the single-ring NO stands, and the network upgrade is
limited to the exact p-wave sector.**

---

## Classification

| Component | Status |
|---|---|
| Aut(C96□C96□C96) ⊇ D₉₆³ ⋊ S₃ (order 42,467,328); point group O_h order 48 | **DERIVED** |
| O_h irrep content {1,1,2,3,3,1,1,2,3,3}; 3D irreps exist | **DERIVED** |
| genuine 3D (vector / p-wave, l=1) irreducible sector from 3 orthogonal D96 axes | **EMERGENT** |
| p-triplet degeneracy 3 = 2l+1 (l=1) exact under O_h | **DERIVED** (subduction l=1 → T₁u) |
| exact O(3) / spherical symmetry of the network | **REFUTED** (finite O_h; approximate O(3) EMERGENT per NP_089) |
| 2l+1 ladder, d/f/g degeneracies | **REFUTED** (split 2+3, 1+3+3) |
| shell closures / nuclear magic numbers | **REFUTED** (confirms NP_087/NP_088/NP_109) |
| π value from network topology | **REFUTED** (algebraic joint spectrum); role EMERGENT (Weyl counting) |
| horizon-area structure S ∝ A | **EMERGENT** (unchanged, QG185) |
| exact Bekenstein quarter 1/4 | **REFUTED** (unchanged, QG196) |

**M_012 refines, from the representation side, the NP_088/NP_089 picture: the D96⊗D96⊗D96
network's octahedral point group provides exactly one genuine 3D irreducible sector — the
p-wave/vector triplet — and nothing beyond it. M_011's single-ring NO is confirmed and not
reclassified; no prior audit reclassification; no new primitive; canonical AT unchanged.**

---

## Theorem

> **Theorem (M_012).** A network of three coupled D96 systems on three orthogonal axes
> contains exactly one genuine 3-dimensional irreducible rotational sector — the vector /
> p-wave (l = 1) triplet — and no exact O(3). Proof: (1) The axis point group of the
> 3-ring structure is the octahedral group O_h (order 48), the signed-permutation group on
> three coordinates (Section 1). (2) O_h has irreps of dimension 3 (T₁u etc.; character
> table Σd² = 48 with {1,1,2,3,3,1,1,2,3,3}), so a genuine 3D irrep exists (Section 2). (3)
> The defining vector rep is irreducible; subduction of the O(3) ladder is l=0→1 (full),
> l=1→3 (full, T₁u), l=2→2+3 (split), l=3→1+3+3 (split) — verified by character norms²
> {1,1,2,3,4} (Sections 2, 4). (4) Joint-spectrum degeneracies are O_h orbit sizes
> {6,12,8,…}, not the (2l+1) ladder; only the p-triplet 3 is exact (Section 3). (5) The
> joint spectrum is algebraic (sums of algebraic λ_k), so π never appears as a value
> (Section 6); the exact Bekenstein quarter still requires an imported 2π (Section 7); and
> nuclear magic numbers are absent (Section 8). Therefore the coupled D96 network yields a
> genuine 3D p-wave sector but not O(3) or any higher spherical structure. ∎
>
> *Proof sketch.* Compute the point group (Section 1); inspect O_h irreps (Section 2);
> decompose the low joint spectrum (Section 3); subduce spherical harmonics (Section 4);
> test shells, π, horizon (Sections 5–8). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → attractor ring C96(±1..±6) — Aut = D₉₆ (QG155); spectrum λ_k (D_041)
 → single-ring O(3) audit: NO (M_011)
 → NETWORK D96⊗D96⊗D96 — cubic lattice, point group O_h (NP_088)
    → O_h irrep dims {1,1,2,3,3,…}: genuine 3D irreps      [DERIVED]
    → vector/p-wave triplet (l=1, dim 3): exact sector      [EMERGENT]
    → 2l+1 ladder, d/f/g: split 2+3 / 1+3+3                [REFUTED]
    → spherical-harmonic collective modes: only l=0,1       [PARTIAL]
    → shell closures / magic numbers                        [REFUTED — NP_087/088/109]
    → π value: algebraic spectrum                           [REFUTED; role EMERGENT]
    → exact horizon quarter 1/4                             [REFUTED — QG185/196]
```

---

## Falsification Path

1. **The M_012 upgrade (genuine 3D p-sector) is exact and structural**: it holds wherever
   the axis point group is O_h. It would be falsified by a D96 tensor structure whose axis
   symmetry group has no irreducible dimension-3 representation (e.g., unequal axis weights
   reducing O_h to D₂/D₄ or a single-axis-only coupling).
2. **Exact O(3)** remains falsified by the finite character table of O_h; the full (2l+1)
   ladder and nuclear magic numbers would require an O_h-compatible exact degeneracy 5 or 7,
   which the character table excludes.
3. **π as a value** is excluded by algebraicity of every joint eigenvalue; a joint level
   equal to π would falsify the entire algebraic-spectrum structure.
4. **Horizon quarter** remains excluded by QG196; only an imported continuum 2π could
   supply it.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_012_Tests.cs`
**Run:** 2026-09-08 · **Result:** see `Tests/Results/Y_M_012_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_M_012_AutomorphismGroups` | pair/triad Aut content; point group O_h order 48 | ✅ |
| `Y_M_012_OhIrreps` | O_h irrep dims {1,1,2,3,3,…}; 3D irreps exist | ✅ |
| `Y_M_012_VectorSector3D` | defining vector rep of O_h irreducible (dim 3) | ✅ |
| `Y_M_012_SphericalSubduction` | l=0,1 full; l=2 → 2+3; l=3 → 1+3+3 | ✅ |
| `Y_M_012_JointSpectrum` | joint levels algebraic; low degeneracies = O_h orbits {6,12,8} | ✅ |
| `Y_M_012_PWaveTriplet` | p-triplet (3 = 2l+1, l=1) is the only exact odd sector | ✅ |
| `Y_M_012_PiAppearance` | no π value in algebraic joint spectrum | ✅ |
| `Y_M_012_HorizonArea` | S∝A structure; exact 1/4 REFUTED | ✅ |
| `Y_M_012_NuclearClosures` | magic numbers not reproduced | ✅ |
| `Y_M_012_Classification` | verdict table | ✅ |
| `Y_M_012_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_012"`

---

## References

- ResearchY-M_011 (single-ring effective O(3) audit), NP_087 (nuclear structure), NP_088
  (D96⊗D96⊗D96 network = cubic lattice, O_h), NP_089 (approximate O(3); exact O(3) only in
  the unattained a→0 continuum), NP_109 (weakest link).
- AT-QG: QG155 (D96 automorphism group), QG185/QG196 (Bekenstein quarter impossibility),
  QG291 (π boundary).
- ResearchY-B_001/B_002/B_003 (π role vs value; 2π origin), D_041 (D96 spectrum).
- Standard crystal-field subduction O(3) ↓ O_h (l=0→A₁g, l=1→T₁u, l=2→E_g⊕T₂g, …) used as
  a verified mathematical table, re-derived by character computation here.
