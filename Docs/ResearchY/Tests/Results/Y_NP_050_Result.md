# Y_NP_050_Result.md — ResearchY-NP_050 Physical Realization Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_050_Tests.cs`
**Run:** 2026-09-05
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_050"`

---

## Summary

**Question:** What physical interaction corresponds to the Entangling Gate?

**Verdict: a coherent two-body (non-local) interaction** — a joint coupling Hamiltonian
H_int = J·σ⊗σ generating U = e^{-i H_int t}. This is CORRESPONDENCE to known physics,
not DERIVED from canonical D96.

## Known entangling interactions → gate language

| Mechanism | Realization |
|---|---|
| Photons (SPDC) | Bell-pair creation |
| Heisenberg exchange | iSWAP / √SWAP |
| Exchange interaction | singlet/triplet |
| Cavity QED (JC) | XX/ZZ |
| Superconducting qubits | XX |

## Local vs non-local Hamiltonian

| Hamiltonian | Effect |
|---|---|
| σ_z ⊗ I (local) | preserves rank 1 |
| σ_z ⊗ σ_z (Ising ZZ) | creates rank 2 |
| σ_x ⊗ σ_x (XX) | creates rank 2 |

## Comparison

- Canonical D96: no two-body coupling (NP_048).
- Joint State: the gate's OUTPUT (NP_040).
- Entangling Gate: the non-local two-body interaction itself.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_050_EntanglingInteractionsInventory` | 5 mechanisms two-body | ✅ |
| `Y_NP_050_LocalHamiltonianPreservesRank` | local preserves rank 1 | ✅ |
| `Y_NP_050_NonLocalHamiltonianCreatesRank2` | Ising/XX create rank 2 | ✅ |
| `Y_NP_050_CommonStructure` | two-body signature | ✅ |
| `Y_NP_050_SingleAbstractInteraction` | one interaction explains all | ✅ |
| `Y_NP_050_CanonicalD96HasNoCoupling` | D96 lacks coupling | ✅ |
| `Y_NP_050_Classification` | CORRESPONDENCE / NEW PRIMITIVE | ✅ |
| `Y_NP_050_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| Known entangling interactions | **CORRESPONDENCE** |
| Physical meaning = two-body interaction | **CORRESPONDENCE** |
| Gate as NEW PRIMITIVE | **CONFIRMED** |
| Gate DERIVED from D96 | **REFUTED** |

## Conclusion

The entangling gate corresponds to a coherent two-body interaction — hosted physics,
not derivable from canonical D96. Canonical D96 unchanged.
