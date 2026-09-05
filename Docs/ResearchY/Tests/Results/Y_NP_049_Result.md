# Y_NP_049_Result.md — ResearchY-NP_049 Entangling Gate Necessity Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_049_Tests.cs`
**Run:** 2026-09-05
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_049"`

---

## Summary

**Question:** Is the Entangling Gate forced by observed quantum experiments, or could an
alternative primitive replace it?

**Verdict: UNIQUELY REQUIRED AS A KIND (A) — the non-local entangling interaction — and
not replaceable.** It has LU-equivalent representatives (CNOT ≡ CZ ≡ iSWAP ≡ √SWAP), but
these are the same primitive in different bases, not alternatives.

## Phenomena requiring the gate

Bell, CHSH, teleportation (2-body); GHZ, W (3-body) — all require creation (rank 1 →
rank 2).

## Remove gate — joint states unpreparable

Joint states remain statically rank 2 but are UNPREPARABLE: no canonical operation
creates rank 2 from a product.

## Alternative mechanisms (all fail)

| Alternative | Result |
|---|---|
| shared actualization | rank 1 |
| non-local/shared phase | rank 1 (controlled-phase = the gate) |
| resonance coupling | ABSENT |
| information coupling | MI>0 but separable |

## Representative equivalence

CNOT, CZ, iSWAP, √SWAP each create rank-2 states from products — LU-equivalent, same
primitive.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_049_PhenomenaRequiringGate` | all need creation | ✅ |
| `Y_NP_049_RemoveGateRetainJointStates` | static but unpreparable | ✅ |
| `Y_NP_049_AlternativeMechanismsFail` | 4 alternatives fail | ✅ |
| `Y_NP_049_GateRepresentativesEquivalent` | LU-equivalent gates | ✅ |
| `Y_NP_049_PrimitiveCost` | gate=1, alternatives=0 | ✅ |
| `Y_NP_049_Classification` | A confirmed, C refuted | ✅ |
| `Y_NP_049_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| Gate uniquely required (A) | **CONFIRMED** (as a kind) |
| Representative freedom | **CONFIRMED** (LU-equivalent) |
| Gate replaceable (C) | **REFUTED** |
| Gate as NEW PRIMITIVE | **CONFIRMED** |

## Conclusion

The entangling gate is uniquely required as a kind — the non-local entangling
interaction — with representative freedom (CNOT ≡ CZ ≡ iSWAP ≡ √SWAP) but no replaceable
alternative. Canonical D96 unchanged.
