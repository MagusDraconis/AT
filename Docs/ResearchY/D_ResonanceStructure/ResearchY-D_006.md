# ResearchY-D_006 — Assignment Constraints Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_006 (permanent)
**Title:** Assignment Constraints Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_006.md`
**Depends on:** ResearchY-D_003 (resonance observables), D_005 (assignment not unique)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_006_Tests.cs`

---

## Purpose

Determine whether **assignment constraints** can reduce the **24 sector permutations**
(D_005) of the four spectral moments onto the four sector roles, and classify each
constraint as **DERIVED, EMERGENT, or BOUNDARY**.

## Accepted (from D_003, D_005)

- Resonance generates the spectral observables; the sector assignment is EMERGENT
  (D_003).
- The moment ordering does not uniquely determine the assignment; 4! = 24 permutations
  exist (D_005).

---

## The Problem

The four spectral moments can be assigned to the four sector roles in 24 ways:

```
moments:   half (64.08), first (95), second (229), octave (1900.25)
sectors:   neutral, full, doublet, octave
permutations: 4! = 24
```

Question: do structural constraints reduce 24 → N?

---

## Test: the five constraints

### 1. Symmetry Constraints

The octave moment occMom is **defined** from the octave occupancies
(occMom = Σocc_j²/occ₀, Ch6). Its definition ties it to the octave/family content:

- occMom is a function of the octave occupancies [4,4,87] — a **definitional** fact.
- Assigning occMom to the "octave" sector is the structurally natural pairing (the
  moment is built from the octave structure).
- **Effect: fixes one pairing → 24 → 6 (3!).**
- **Classification: DERIVED** (definitional — the moment's formula uses the octave
  occupancies).

### 2. Ordering Constraints

The moments are strictly ordered (64.08 < 95 < 229 < 1900.25, DERIVED). But the *sector
labels* (neutral/full/doublet/octave) have **no canonical magnitude ordering** — they are
physical names, not spectral objects (D_005).

- **Effect: no reduction from ordering alone.**
- **Classification: EMERGENT (no constraint — labels are not ordered by the spectrum).**

### 3. Family Constraints

The octave bands ARE the families (D_004: family count = floor(log₂ span)+1 = 3, the
bands [4,4,87] are the families). This reinforces the occMom ↔ octave/family pairing
(same as the symmetry constraint) but adds no further reduction — the families are the
content of the octave sector.

- **Effect: reinforces constraint 1; no additional reduction.**
- **Classification: DERIVED (the family-octave identity is structural).**

### 4. Z2 Constraints

The second moment Σm² = 229 is dominated by the 42 doublets (42×2² = 168, **73%** of
Σm²). The canonical reading (QG157) is the "doublet-occupancy access" — the second
moment is the doublet sector's access count.

- The *doublet dominance* (73%) is a spectral fact (DERIVED structure).
- The *assignment* of Σm² to the doublet sector is the supported correspondence
  (EMERGENT — the name "doublet-occupancy access" reflects the assignment).
- **Effect: fixes the doublet pairing → 6 → 2 (2!).**
- **Classification: DERIVED (dominance) + EMERGENT (the doublet assignment).**

### 5. Calibration Constraints

The first moment Σm = 95 is the **total mode count** (Σ of all multiplicities) — the
"full access" reading is near-tautological (full = all modes). This fixes the full
pairing → 2 → 1. The remaining half-moment → neutral follows by elimination.

- The *total-count* fact is DERIVED (Σm = sum of multiplicities = 95).
- The *final selection* (which assignment matches observation) is the calibration step
  (BOUNDARY — the masses/couplings are matched via anchors, D_003/D_005).
- **Effect: 2 → 1 (full = total count, DERIVED; final match, BOUNDARY).**

---

## Result: 24 → 1

```
24  (all permutations, D_005)
 → 6  (symmetry/definition: occMom built from octave occupancies — DERIVED)
 → 2  (Z2/doublet: Σm² doublet-dominated, 73% — DERIVED dominance + EMERGENT assignment)
 → 1  (ordering/calibration: Σm = total count = full access — DERIVED; final match — BOUNDARY)
```

**The 24 sector permutations reduce to 1** under the constraints. The canonical assignment
(half→neutral, first→full, second→doublet, octave→octave) is the unique survivor.

---

## Theorem

> **Theorem (D_006).** The 24 sector permutations reduce to a unique assignment under
> five constraints.
>
> *Proof sketch.* (1) occMom is defined from the octave occupancies (Ch6), fixing the
> octave pairing: 24 → 6 (Symmetry, DERIVED). (2) Σm² is doublet-dominated (42×2² = 168
> of 229, 73%) and canonically the doublet-occupancy access (QG157), fixing the doublet
> pairing: 6 → 2 (Z2, DERIVED dominance + EMERGENT assignment). (3) Σm = 95 is the total
> mode count — the full access (DERIVED), fixing the full pairing: 2 → 1. (4) The
> half-moment → neutral follows by elimination. (5) The final match to observation is the
> calibration step (BOUNDARY). Hence 24 → 6 → 2 → 1: the assignment is unique under the
> constraints. ∎

---

## Uniqueness Proof

**Claim:** the sector assignment is uniquely determined by the five constraints.

*Proof.* Among the 24 permutations of {64.08, 95, 229, 1900.25} onto
{neutral, full, doublet, octave}:

1. occMom = 1900.25 must map to the octave sector: the moment is a function of the octave
   occupancies (its definition, Ch6). Any assignment mapping occMom elsewhere contradicts
   its construction. This leaves 3! = 6 permutations.
2. Σm² = 229 must map to the doublet sector: it is the doublet-occupancy access
   (QG157), dominated by the 42 doublets (73%). Any assignment mapping Σm² elsewhere
   contradicts its canonical role. This leaves 2! = 2 permutations.
3. Σm = 95 is the total mode count — the full access (Σ of all multiplicities). Any
   assignment mapping Σm to the neutral sector (and Σ√m to full) would assign the
   neutral access to the total count — a structural mismatch. This leaves 1 permutation:
   (half→neutral, first→full, second→doublet, octave→octave).
4. The final match to observation is the calibration step (BOUNDARY).

Hence exactly one assignment survives the constraints. ∎

---

## Classification Summary

| Constraint | Effect | Classification |
|---|---|---|
| symmetry (occMom defined from octaves) | 24 → 6 | **DERIVED** |
| ordering (no canonical sector ordering) | no reduction | **EMERGENT** (none) |
| family (octave bands = families) | reinforces occMom pairing | **DERIVED** |
| Z2 (Σm² doublet-dominated 73%) | 6 → 2 | DERIVED (dominance) + EMERGENT (assignment) |
| calibration (Σm = total count; final match) | 2 → 1 | DERIVED (total count) + BOUNDARY (match) |

**Result: 24 → 1.**

---

## Dependency Graph

```
D_005 (24 permutations; assignment not unique)
  → D_006: constraints reduce 24 → 1
  ├── symmetry: occMom built from octave occupancies → DERIVED (24→6)
  ├── ordering: no canonical sector ordering → EMERGENT (none)
  ├── family: octave bands = families → DERIVED (reinforces)
  ├── Z2: Σm² doublet-dominated (73%) → DERIVED+EMERGENT (6→2)
  └── calibration: Σm = total count; final match → DERIVED+BOUNDARY (2→1)
```

---

## Research Conclusions

1. **The 24 permutations reduce to 1.** The constraints (symmetry, Z2, ordering) narrow
   the assignment to the unique canonical one.
2. **The structural constraints are DERIVED:** occMom's octave construction and Σm²'s
   doublet dominance (73%) are spectral facts; Σm = 95 is the total mode count.
3. **The assignment of the surviving pairing is EMERGENT (correspondence):** the doublet
   role of Σm² is the supported canonical reading (QG157), not a forced derivation.
4. **The final selection is BOUNDARY:** matching the assignment to observation uses the
   calibration anchors (D_003/D_005).
5. **Unique under constraints, but not "derived" in the strict sense:** the assignment
   is pinned by structural facts plus the canonical sector naming — consistent with
   D_004's "supported, not unique" upgraded to "unique under the constraints."

---

## Open Problems

1. **Doublet-role derivation (D_006 OP1).** Is the doublet reading of Σm² (73% doublet
   share) derivable, or permanently a supported correspondence? (The dominance is
   derived; the sector assignment is the correspondence.)
2. **Neutral-role origin (D_006 OP2).** The half-moment → neutral follows by
   elimination; is there a direct derivation? (Currently: elimination + matching.)
3. **Constraint completeness (D_006 OP3).** Are there additional constraints that would
   make the assignment "derived" (not correspondence)? (Currently: 24 → 1, with the
   surviving assignment EMERGENT.)

---

## Next Steps

- **ResearchY-D_007 (or synthesis):** the assignment-constraint audit (this) completes
   the moment → sector analysis; a synthesis can verify the claim-registry consistency.
- **ResearchY-B_002 follow-up:** the calibration-final-selection (BOUNDARY) connects to
   the anchor/boundary analysis.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_006_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_006_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_006_SymmetryConstraint` | occMom defined from octave occupancies → 24→6 | ✅ |
| `Y_D_006_OrderingConstraint` | no canonical sector ordering → no reduction | ✅ |
| `Y_D_006_FamilyConstraint` | octave bands = families → reinforces occMom pairing | ✅ |
| `Y_D_006_Z2Constraint` | Σm² doublet-dominated (73%) → 6→2 | ✅ |
| `Y_D_006_CalibrationConstraint` | Σm = total count → 2→1; final match BOUNDARY | ✅ |
| `Y_D_006_Run` | Research report | ✅ |

**Conclusion:** the 24 sector permutations reduce to a unique assignment (24 → 6 → 2 → 1)
under the constraints: DERIVED structural facts (occMom's octave construction, Σm²'s
doublet dominance, Σm = total count), an EMERGENT correspondence (the doublet role), and
a BOUNDARY calibration step (final match). No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_006"`

---

## References

- ResearchY-D_003 (resonance observables), D_005 (24 permutations), D_004 (three-layer
  origin).
- ATQG_ClaimClassificationRegistry.md (theorem/correspondence/calibration/fit).
- Monograph V2.0: Ch6 (D96 moments, occMom), Ch11 (SM masses).
- AT-QG: QG157 (effective access, doublet-occupancy), QG210 (family index).
