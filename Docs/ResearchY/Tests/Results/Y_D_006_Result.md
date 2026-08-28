# ResearchY-D_006 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_006_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 6/6 tests (Duration ~24 ms)
**Filter:** `FullyQualifiedName~Y_D_006`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_006_SymmetryConstraint` | occMom defined from octave occupancies → 24→6 (DERIVED) | ✅ |
| `Y_D_006_OrderingConstraint` | moments strictly ordered; no canonical sector ordering → no reduction | ✅ |
| `Y_D_006_FamilyConstraint` | octave bands = families (3) → reinforces occMom pairing | ✅ |
| `Y_D_006_Z2Constraint` | Σm² doublet share 73% (168/229) → 6→2 (DERIVED+EMERGENT) | ✅ |
| `Y_D_006_CalibrationConstraint` | Σm = total count = full access → 2→1 (DERIVED+BOUNDARY) | ✅ |
| `Y_D_006_Run` | Research report | ✅ |

## Constraint Reduction

```
24  (all permutations, D_005)
 → 6  (symmetry: occMom defined from octave occupancies — DERIVED)
 → 2  (Z2: Σm² doublet-dominated 73% — DERIVED dominance + EMERGENT assignment)
 → 1  (calibration: Σm = total mode count = full access — DERIVED; final match — BOUNDARY)
```

## Classification

| Constraint | Effect | Classification |
|---|---|---|
| symmetry | 24 → 6 | **DERIVED** |
| ordering | no reduction | **EMERGENT** (no canonical sector ordering) |
| family | reinforces occMom | **DERIVED** |
| Z2 | 6 → 2 | DERIVED + EMERGENT |
| calibration | 2 → 1 | DERIVED + BOUNDARY |

## Conclusion

**24 → 1.** The unique survivor is the canonical assignment (half→neutral, first→full,
second→doublet, octave→octave). DERIVED structural facts (occMom's octave construction,
Σm²'s doublet dominance, Σm = total count) + EMERGENT correspondence (doublet role) +
BOUNDARY calibration step (final match). The assignment is unique under the constraints.
**No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_006"
```
