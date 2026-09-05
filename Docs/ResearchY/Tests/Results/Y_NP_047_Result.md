# Y_NP_047_Result.md — ResearchY-NP_047 Joint State Dynamics Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_047_Tests.cs`
**Run:** 2026-09-05
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_047"`

---

## Summary

**Question:** How are Joint States created, transformed, and destroyed?

**Verdict: full dynamics exists with ONE added primitive — the entangling gate.** The
minimal dynamical law is: create by an entangling gate (CNOT/CZ), stabilize by local
unitaries (canonical phase update), destroy by local measurement (canonical M_001).

## The three rules

| Rule | Transition | Mechanism | Primitive |
|---|---|---|---|
| Creation | Product → Joint | entangling gate (CNOT/CZ) | NEW |
| Stability | Joint → Joint | local unitaries U_A⊗U_B | canonical |
| Destruction | Joint → Product | local measurement | canonical |

## Conservation profile

| Quantity | Local unitary | Gate | Measurement |
|---|---|---|---|
| Schmidt rank | conserved | 1→2 | 2→1 |
| concurrence | conserved | 0→1 | →0 |
| entropy | conserved | increased | →0 |

## Multipartite

- Bell → GHZ: achievable (CNOT + third |0⟩ qubit → τ₃=1).
- GHZ → W: REFUTED (distinct SLOCC class; no CZ reaches W).

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_047_LocalUnitaryPreservesEntanglement` | rank/C/S conserved | ✅ |
| `Y_NP_047_LocalUnitaryCannotCreate` | local unitaries keep rank 1 | ✅ |
| `Y_NP_047_EntanglingGateCreates` | CNOT/CZ → rank 2 | ✅ |
| `Y_NP_047_MeasurementDestroys` | measurement → rank 1 | ✅ |
| `Y_NP_047_Conservation` | conservation profile | ✅ |
| `Y_NP_047_MultipartiteExtension` | Bell→GHZ; GHZ→W refuted | ✅ |
| `Y_NP_047_Classification` | C confirmed, A refuted | ✅ |
| `Y_NP_047_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| Creation | **NEW PRIMITIVE** (entangling gate) |
| Stability | **DERIVED** (local unitaries) |
| Destruction | **DERIVED** (measurement) |
| Full dynamics (B) | **CONFIRMED** |
| Static ontology only (A) | **REFUTED** |
| Additional primitive (C) | **CONFIRMED** (1) |

## Conclusion

Joint states have full dynamics: one added primitive (the entangling gate) plus the
already-canonical local unitary and measurement. Canonical D96 unchanged.
