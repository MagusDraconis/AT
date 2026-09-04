# Y_NP_043_Result.md — ResearchY-NP_043 Joint State Origin Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_043_Tests.cs`
**Run:** 2026-09-04
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_043"`

---

## Summary

**Question:** Can Joint States be derived from existing canonical objects, or are they
irreducible primitives?

**Verdict: the joint states are IRREDUCIBLE PRIMITIVES (NEW PRIMITIVE)** — not
DERIVED and not EMERGENT from any canonical object. The canonical inventory
(Difference, Actualization, Occupancy, Information, D96 spectrum, Phase) is entirely
single-DOF, classical, or scalar, and reaches Schmidt rank ≤ 1.

## Canonical inventory

| Object | Kind | Entangles? |
|---|---|---|
| Difference (η) | real scalar | no |
| Actualization | diagonal occupancy | no (classical) |
| Occupancy | diagonal counts | no (classical) |
| Information | scalar / MI | no (classical) |
| D96 spectrum | real frequencies | no |
| Phase (θ) | single-DOF amplitude | no (rank 1) |

## Derivation attempts

- **2-body:** phase products / occupancy / interference reach max Schmidt rank 1;
  Bell (rank 2) unreachable → IRREDUCIBLE.
- **3-body:** canonical + 2-body links give biseparable (τ₃=0); GHZ (τ₃=1) and W
  (pairwise C=2/3) unreachable → IRREDUCIBLE.

## Primitive count

| Source | Joint states | Added primitives |
|---|---|---|
| Canonical | 0 | 0 |
| 2-body joint link | Bell | 1 |
| 3-body joint state | GHZ, W | 1 |
| **Total** | | **2** |

## Earliest appearance

The 2-body joint link state (NP_039) is the first rank-2 space; canonical D96 reaches
only rank 1 (correlation only, NP_038).

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_043_CanonicalInventorySingleDofOrClassical` | canonical objects single-DOF/classical/scalar | ✅ |
| `Y_NP_043_TwoBodyJointStateIrreducible` | 2-body derivation fails | ✅ |
| `Y_NP_043_ThreeBodyJointStateIrreducible` | 3-body derivation fails | ✅ |
| `Y_NP_043_PrimitiveCount` | 2 primitives total | ✅ |
| `Y_NP_043_EarliestAppearance` | joint link = first rank-2 | ✅ |
| `Y_NP_043_Classification` | DERIVED/NEW PRIMITIVE/REFUTED | ✅ |
| `Y_NP_043_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| Canonical objects | **DERIVED** (single-DOF/classical/scalar) |
| 2-body joint state | **NEW PRIMITIVE** (irreducible) |
| 3-body joint state | **NEW PRIMITIVE** (irreducible) |
| Joint states DERIVED/EMERGENT from canonical | **REFUTED** |

## Conclusion

Joint states are irreducible primitives; canonical AT reaches only rank 1. The earliest
entanglement-capable state space is the 2-body joint link state. Canonical D96 unchanged.
