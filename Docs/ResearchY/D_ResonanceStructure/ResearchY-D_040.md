# ResearchY-D_040 — Boundary Reclassification Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_040 (permanent)
**Title:** Boundary Reclassification Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_040.md`
**Depends on:** ResearchY-D_020…D_039 (the full D-group chain)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_040_Tests.cs`

---

## Purpose

**Which D_020 boundary assumptions remain after D_021–D_039?** This is the synthesis
audit: re-audit every object tagged BOUNDARY / EMERGENT / DERIVED across the D-chain
and produce the final irreducible boundary set.

## Method

1. Build the dependency graph (D_020 → … → D_039).
2. Inventory every object and its original classification.
3. Re-evaluate each using the later results (D_035/D_036/D_037/D_038/D_039).
4. Produce old → new classification with justification.
5. Determine the final irreducible boundary set.

---

## 1. Dependency Graph (D_020 → … → D_039)

```
D_020 observable-sector construction
 ├── Z2-paired (complex) sector requirement            [BOUNDARY → confirmed]
 │    └── D_021 pairing structure DERIVED
 │    └── D_022 spectral Z2 DERIVED; weak-isospin Z2 BOUNDARY
 │    └── D_023 real algebra DERIVED; SU(2) gauge BOUNDARY
 │    └── D_024 doublet carrier DERIVED; SU(2) dims/fundamental BOUNDARY
 │    └── D_025 real algebra + complexification EMERGENT; compact-form BOUNDARY
 │    └── D_026 su(2) compact-form EMERGENT (observability)
 │    └── D_027 selector DERIVED; {Difference, η} BOUNDARY
 │    └── D_034 reciprocity EMERGENT
 │    └── D_035 complete pairing DERIVED; complex observability EMERGENT
 │    └── D_036 complex state DERIVED; 'complex' REDUCES to D_020
 │    └── D_037 observability EMERGENT; reciprocity EMERGENT
 │    └── D_038 state identity EMERGENT
 │    └── D_039 state identity DERIVED (Difference applied)
 ├── exactly 3 octave families (span ∈ [4,8))          [BOUNDARY → confirmed]
 │    └── D_028 span value DERIVED; 3-family window BOUNDARY (corrected by D_040)
 │    └── D_030 octave rung DERIVED; seed p=3 BOUNDARY
 │    └── D_031 p=3 DERIVED; 6|N DERIVED
 ├── period-3 seed p=3                                 [BOUNDARY → DERIVED]
 ├── N=96                                              [BOUNDARY → DERIVED]
 ├── degree-12 ring                                    [DERIVED — cosmetic]
 └── weak-isospin SU(2) gauge + j=1/2 fundamental      [BOUNDARY → confirmed]
```

---

## 2. Boundary Inventory

Every object tagged BOUNDARY at its originating audit, with its re-evaluation:

| # | Object | Originating audit | Original | New | Justification |
|---|---|---|---|---|---|
| 1 | complete pairing (0 unpaired) | D_020/D_021/D_032/D_033/D_034 | BOUNDARY | **DERIVED** | D_035: every eigenvalue mult ≥ 2 follows from complex observability (the [magnitude, phase] pair) |
| 2 | self-conjugate multiplet participation | D_035 | (new) | **DERIVED** | D_035: the real-only k=N/2 needs the 5-fold λ=12 group |
| 3 | complex observability (mult ≥ 2) | D_035/D_036 | EMERGENT | **EMERGENT** | the requirement, from the two-DOF sector |
| 4 | reciprocity (every mode complex) | D_034/D_037 | EMERGENT | **EMERGENT** | from complex-state observability |
| 5 | Z2 pairing STRUCTURE | D_021 | DERIVED | **DERIVED** | oscillation necessity + ring reflection |
| 6 | weak-isospin doublet reading | D_021/D_022/D_024 | EMERGENT | **EMERGENT** | the physical correspondence |
| 7 | weak-isospin SU(2) gauge structure | D_022/D_023 | BOUNDARY | **BOUNDARY** | independent gauge input (sector S) |
| 8 | SU(2) rep dimensions 2j+1 | D_024 | BOUNDARY | **BOUNDARY** | group structure |
| 9 | j = 1/2 fundamental choice | D_024 | BOUNDARY | **BOUNDARY** | weak-isospin input |
| 10 | complexification (Fourier i) | D_025 | EMERGENT | **EMERGENT** | representation choice from the phase |
| 11 | su(2) compact-form | D_025 | BOUNDARY | **EMERGENT** | D_026: selected by observability (finite-dim unitary), not a free gauge input |
| 12 | Z2-paired (complex) sector requirement | D_020/D_036–D_039 | BOUNDARY | **BOUNDARY** | the observable-sector input; 'complex' reduces to it (D_036) |
| 13 | exactly 3 octave families | D_020 | BOUNDARY | **BOUNDARY** | span ∈ [4,8) window |
| 14 | period-3 seed p=3 | D_030 | BOUNDARY | **DERIVED** | D_031: unique complete-pairing period |
| 15 | 6\|N | D_030/D_031 | DERIVED | **DERIVED** | seed half-shift |
| 16 | N=96 | D_030 | BOUNDARY | **DERIVED** | D_031/D_020: unique zero-defect octave rung |
| 17 | singleton prohibition | D_033 | BOUNDARY | **DERIVED** | D_035/D_037: a real-only singlet violates complex observability (mult < 2) |
| 18 | state identity | D_038 | EMERGENT | **DERIVED** | D_039: Difference IS distinguishability — the primitive applied |
| 19 | {Difference, η} primitives | D_027/D_039 | BOUNDARY | **BOUNDARY** | the minimal foundation |

---

## 3. Reclassification Summary (old → new)

| Object | Old | New |
|---|---|---|
| complete pairing | BOUNDARY | **DERIVED** |
| singleton prohibition | BOUNDARY | **DERIVED** |
| p=3 seed | BOUNDARY | **DERIVED** |
| N=96 | BOUNDARY | **DERIVED** |
| su(2) compact-form | BOUNDARY | **EMERGENT** |
| state identity | EMERGENT | **DERIVED** |
| Z2-paired sector requirement | BOUNDARY | **BOUNDARY** (confirmed) |
| 3 octave families | BOUNDARY | **BOUNDARY** (confirmed) |
| SU(2) gauge + j=1/2 | BOUNDARY | **BOUNDARY** (confirmed) |
| {Difference, η} | BOUNDARY | **BOUNDARY** (confirmed) |

---

## 4. Focus Objects

| Object | Old | New | Justification |
|---|---|---|---|
| complete pairing | BOUNDARY | **DERIVED** | D_035 (mult ≥ 2 from complex observability) |
| complex observability | EMERGENT | **EMERGENT** | the two-DOF sector requirement |
| reciprocity | EMERGENT | **EMERGENT** | from complex-state observability |
| Z2 structure | DERIVED | **DERIVED** | oscillation + reflection |
| weak-isospin reading | EMERGENT | **EMERGENT** | the correspondence |
| observable-sector construction | BOUNDARY | **BOUNDARY** | = {Z2-paired sector, 3 families} |

---

## 5. Final Irreducible Boundary Set

```
B_final = { {Difference, η}              (the primitives, D_027/D_039)
          , {Z2-paired (complex) sector} (the observable-sector input, D_020)
          , {3 octave families}          (span ∈ [4,8) window, D_020)
          , {SU(2) gauge + j=1/2}        (weak-isospin gauge input, D_022/D_024) }
```

**Everything else in the D-chain is DERIVED or EMERGENT.** No new primitive was added;
canonical AT is unchanged.

---

## Theorem

> **Theorem (D_040).** The D-chain reduces to exactly four irreducible boundary inputs:
> the primitives {Difference, η} (D_027/D_039); the Z2-paired (complex) sector
> requirement (D_020; "observable sector is complex" reduces to it, D_036); exactly 3
> octave families (span ∈ [4,8), D_020); and the weak-isospin SU(2) gauge structure
> with j=1/2 fundamental (D_022/D_024). All previously-BOUNDARY objects downstream are
> reclassified: complete pairing and the singleton prohibition are DERIVED (D_035/D_037);
> p=3 and N=96 are DERIVED (D_031/D_020); the su(2) compact-form is EMERGENT (D_026);
> state identity is DERIVED (D_039). The pairing structure (D_021), the spectral Z2
> (D_022), the real algebra (D_023), the doublet carrier (D_024), the span (D_028), the
> octave rung (D_030), and the selector criteria (D_027) remain DERIVED; reciprocity,
> complex observability, observability, and the weak-isospin reading remain EMERGENT.
> No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory every BOUNDARY/EMERGENT/DERIVED tag (Section 2). (2)
> Reclassify using later results: complete pairing → DERIVED (D_035), p=3/N=96 →
> DERIVED (D_031), singlet → DERIVED (D_035/D_037), su(2) compact-form → EMERGENT
> (D_026), state identity → DERIVED (D_039) — Sections 3–4. (3) The remaining BOUNDARY
> objects are the four irreducible inputs (Section 5); removal of any breaks the
> N=96 selection (D_020) or the gauge attachment (D_022). (4) Dependency graph is
> acyclic (Section 1); no contradictions remain (Section 7). ∎

---

## 6. Consistency Check

| Check | Result |
|---|---|
| Dependency graph acyclic? | **YES** — D_020 roots the tree; each later audit refines, not reverses |
| Every DERIVED object has a derivation path to a BOUNDARY input? | **YES** |
| Every EMERGENT object is a requirement/correspondence, not a new input? | **YES** |
| Old/new classifications consistent with their originating audit + refinement? | **YES** (each reclassification cites the refining audit) |
| Final boundary set minimal (removing any element breaks selection)? | **YES** (D_020: Z2 + 3-family; D_022: gauge) |

---

## 7. Contradiction Report

| Contradiction | Resolution |
|---|---|
| D_021/D_032/D_034: complete pairing BOUNDARY vs D_035: DERIVED | **RESOLVED** — D_035 (mult ≥ 2 from complex observability) supersedes; D_021/D_020 carry refinement notes |
| D_030: p=3 BOUNDARY vs D_031: DERIVED | **RESOLVED** — D_031 (unique complete-pairing period) supersedes |
| D_025: su(2) compact-form BOUNDARY vs D_026: EMERGENT | **RESOLVED** — D_026 (selected by observability) supersedes |
| D_033: singlet prohibition BOUNDARY vs D_035/D_037: DERIVED | **RESOLVED** — complex observability excludes the singlet (mult < 2) |
| D_038: state identity EMERGENT vs D_039: DERIVED | **RESOLVED** — D_039 (Difference applied) supersedes |
| D_035: "observable sector is complex" BOUNDARY vs D_036: REDUCES to D_020 | **RESOLVED** — same input stated from the complex side; no new boundary |

**No open contradictions remain.** The chain is monotone: boundaries only moved
downward (BOUNDARY → DERIVED/EMERGENT) or were confirmed; none moved upward.

---

## 8. Boundary Reduction Map

```
D_020  observable-sector construction {Z2-paired sector, 3 families}   [BOUNDARY]
  ↓ D_021 pairing structure DERIVED
  ↓ D_022 spectral Z2 DERIVED; weak-isospin Z2 BOUNDARY (gauge)
  ↓ D_023 real algebra DERIVED; SU(2) gauge BOUNDARY
  ↓ D_024 doublet carrier DERIVED; SU(2) dims/j=1/2 BOUNDARY
  ↓ D_025 complexification EMERGENT; compact-form BOUNDARY
  ↓ D_026 su(2) compact-form EMERGENT   [BOUNDARY REMOVED]
  ↓ D_027 selector DERIVED; {Difference, η} BOUNDARY
  ↓ D_028 span value DERIVED; 3-family window BOUNDARY   [window corrected by D_040]
  ↓ D_029 zero-defect set EMERGENT
  ↓ D_030 octave rung DERIVED; seed p=3 BOUNDARY
  ↓ D_031 p=3 DERIVED; 6|N DERIVED   [BOUNDARY REMOVED]
  ↓ D_032 pairing completeness BOUNDARY
  ↓ D_033 singlet prohibition BOUNDARY
  ↓ D_034 reciprocity EMERGENT
  ↓ D_035 complete pairing DERIVED; complex observability EMERGENT   [BOUNDARY REMOVED]
  ↓ D_036 complex state DERIVED; 'complex' REDUCES to D_020
  ↓ D_037 observability EMERGENT; reciprocity EMERGENT   [singlet BOUNDARY REMOVED]
  ↓ D_038 state identity EMERGENT
  ↓ D_039 state identity DERIVED (Difference applied)   [EMERGENT REMOVED]
  ⇒ D_040: B_final = {Difference, η} ∪ {Z2-paired sector} ∪ {3 families} ∪ {SU(2) gauge + j=1/2}
```

---

## Classification

| Component | Status |
|---|---|
| {Difference, η} | **BOUNDARY** (the primitives) |
| Z2-paired (complex) sector requirement | **BOUNDARY** (observable-sector input) |
| exactly 3 octave families | **BOUNDARY** (span ∈ [4,8) window) |
| SU(2) gauge + j=1/2 fundamental | **BOUNDARY** (weak-isospin input) |
| pairing structure, spectral Z2, real algebra, doublet carrier | **DERIVED** |
| complete pairing, singleton prohibition, p=3, 6\|N, N=96 | **DERIVED** |
| state identity, magnitude, phase, complex state, span, octave rung, selector | **DERIVED** |
| su(2) compact-form, complexification, reciprocity, observability, weak-isospin reading | **EMERGENT** |

**The final irreducible boundary set has exactly four elements. Everything else in the
D-chain is DERIVED or EMERGENT. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Observable-sector origin (D_040 OP1).** Why the observable sector is the Z2-paired
   (complex), 3-family sector — the boundary (D_020) itself — remains open. D_036–D_039
   pushed all intermediate claims to DERIVED; the sector choice itself is the residue.
2. **Gauge-sector origin (D_040 OP2).** The weak-isospin SU(2) gauge structure and the
   j=1/2 fundamental remain independent inputs (D_022–D_024); their origin is outside
   the D-chain.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_040_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_040_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_040_BoundaryInventory` | the four boundary elements + the reclassified set | ✅ |
| `Y_D_040_Reclassification` | old → new for complete pairing, p=3, N=96, singlet, su(2), state identity | ✅ |
| `Y_D_040_DependencyConsistency` | the D-chain DAG is acyclic; every DERIVED object has a path | ✅ |
| `Y_D_040_ContradictionCheck` | the six contradictions are all resolved | ✅ |
| `Y_D_040_ClassificationRegistry` | GUARD: canonical final classifications locked; two-level 3-family rule; no dual tagging | ✅ |
| `Y_D_040_IrreducibleBoundary` | removing any boundary element breaks selection | ✅ |
| `Y_D_040_Run` | Research report | ✅ |

**Conclusion:** The D-chain reduces to exactly **four irreducible boundary inputs**:
{Difference, η} (the primitives), {Z2-paired (complex) sector}, {3 octave families},
and {SU(2) gauge + j=1/2}. Complete pairing, the singleton prohibition, p=3, 6\|N, and
N=96 are all DERIVED; the su(2) compact-form and state identity moved to EMERGENT/DERIVED
respectively; reciprocity, complex observability, observability, and the weak-isospin
reading remain EMERGENT. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_040"`

---

## References

- ResearchY-D_020…D_039 (the full D-group chain).
- AT-QG: QG153/155 (Z2 doublets), QG159 (D96 selection), QG160 (period-3), QG216/218/220
  (amplitude/phase/complex), QG138 (3 families).
- Monograph V2.0: Ch6 (D96 spectrum), Ch9 (quantum mechanics).
