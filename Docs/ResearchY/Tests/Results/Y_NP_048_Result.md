# Y_NP_048_Result.md — ResearchY-NP_048 Entangling Gate Origin Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_048_Tests.cs`
**Run:** 2026-09-05
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_048"`

---

## Summary

**Question:** Is the entangling gate itself derivable, or is it an irreducible
primitive?

**Verdict: the entangling gate is IRREDUCIBLE — a NEW PRIMITIVE (C).** Every canonical
operation (Difference, Actualization, Occupancy, Information, Phase, D96 Resonance) is
local or classical and reaches Schmidt rank ≤ 1; the gate (CNOT/CZ) is the unique
non-local rank-raising operation and must be imported.

## Canonical operation inventory

| Operation | Kind | Raises rank? |
|---|---|---|
| Difference | scalar | no |
| Actualization | diagonal | no |
| Occupancy | diagonal | no |
| Information | scalar / MI | no |
| Phase | single-DOF | no |
| D96 Resonance | real content | no |

## Construction attempts (all fail)

| Attempt | Result |
|---|---|
| phase coupling | rank 1 |
| resonance locking | ABSENT (NP_005/006) |
| occupancy exchange | separable (C=0) |
| information exchange | separable (C=0) |

Sweep of ALL canonical operations: max Schmidt rank = 1. Only the non-local gate
(CNOT/CZ) raises rank to 2.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_048_CanonicalOperationsLocalOrClassical` | all canonical ops local/classical | ✅ |
| `Y_NP_048_PhaseCouplingRankOne` | phase coupling rank 1 | ✅ |
| `Y_NP_048_ResonanceLockingAbsent` | locking absent | ✅ |
| `Y_NP_048_OccupancyAndInformationExchangeSeparable` | exchange separable | ✅ |
| `Y_NP_048_NoCanonicalOperationReachesRank2` | sweep max rank 1 | ✅ |
| `Y_NP_048_Classification` | gate NEW PRIMITIVE | ✅ |
| `Y_NP_048_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| Canonical operations | **DERIVED** (local/classical) |
| Gate DERIVED (A) | **REFUTED** |
| Gate EMERGENT (B) | **REFUTED** |
| Gate NEW PRIMITIVE (C) | **CONFIRMED** |

## Conclusion

The entangling gate is irreducible — a NEW PRIMITIVE. The entanglement sector needs two
irreducible primitives: the joint state (NP_043) and the entangling gate (NP_048).
Canonical D96 unchanged.
