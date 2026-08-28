# ResearchY-D_005 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_005_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 6/6 tests (Duration ~24 ms)
**Filter:** `FullyQualifiedName~Y_D_005`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_005_MomentOrdering` | ladder strictly ordered 64.08 < 95 < 229 < 1900.25 (DERIVED) | ✅ |
| `Y_D_005_AssignmentUniqueness` | assignment NOT unique (correspondence; labels not spectral) | ✅ |
| `Y_D_005_AlternativeAssignments` | 4! = 24 assignments possible; canonical selected by matching | ✅ |
| `Y_D_005_ElectronSelection` | m_e = 0.511 MeV calibration anchor (BOUNDARY) | ✅ |
| `Y_D_005_FamilyOrdering` | band frequency order DERIVED; family labels EMERGENT | ✅ |
| `Y_D_005_Run` | Research report | ✅ |

## Verdicts

| Question | Answer | Classification |
|---|---|---|
| moment ladder ordering | strict (64.08 < 95 < 229 < 1900.25) | **DERIVED** |
| sector assignment unique? | NO (correspondence) | **EMERGENT** |
| alternative assignments? | YES (24 permutations; selected by matching observation) | **EMERGENT** |
| electron selection derived? | NO (calibration anchor m_e) | **BOUNDARY** |
| family ordering invariant? | band order DERIVED; family labels EMERGENT | **DERIVED + EMERGENT** |

## Conclusion

**Moment ordering does NOT uniquely determine sector assignment.** The ordering is a
derived spectral fact; the assignment is an emergent correspondence (4! = 24 possibilities,
canonical one selected by matching observation); the electron is a boundary (calibration
anchor m_e); family band order is derived while the family labels are conventional.
Negative uniqueness proof: the spectrum cannot distinguish permutations because sector
labels are not spectral objects. **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_005"
```
