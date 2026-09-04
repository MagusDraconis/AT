# Y_NP_041_Result.md — ResearchY-NP_041 Joint Link Consequence Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_041_Tests.cs`
**Run:** 2026-09-04
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_041"`

---

## Summary

**Question:** Does the Joint Link State reproduce known quantum-entanglement
phenomenology? Can a rank-2 joint link generate the standard hierarchy of entangled
states?

**Verdict: the joint link state is a COMPLETE TWO-BODY entanglement sector, NOT a
complete multipartite sector — it is MERELY SUFFICIENT FOR BELL PAIRS.** It DERIVEs
Bell pairs, entanglement entropy, CKW monogamy, and Bell-pair teleportation; it does
NOT derive the genuine multipartite states GHZ and W (which need a 3-body joint state
/ entangling gate).

## Phenomenology results

| Phenomenon | Result | Status |
|---|---|---|
| Bell pair | rank 2, C=1, CHSH=2√2, S(ρ_A)=1 bit | **DERIVED** |
| GHZ state | τ₃=1, bipartite reductions separable (C=0) | **REFUTED from 2-body link** |
| W state | τ₃=0, bipartite C=2/3, genuinely tripartite | **REFUTED from 2-body link** |
| Monogamy (CKW) | C²(AB)+C²(AC) ≤ 4·det(ρ_A) | **DERIVED** |
| Entanglement entropy | S(ρ_A)=H(a²); Bell→1 bit | **DERIVED** |
| Teleportation fidelity | F=(2+C)/3; Bell→1 | **DERIVED** |

## Two-body vs multipartite

- GHZ has separable bipartite reductions (C=0) while a Bell pair's reduction is
  maximally entangled → GHZ ≠ Bell ⊗ Bell. It needs a 3-body joint state / entangling
  gate.
- W has entangled bipartite reductions (C=2/3) yet τ₃=0 → genuinely tripartite, not a
  product of two-body links.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_041_BellPair` | Bell: rank 2, C=1, CHSH=2√2, S=1 | ✅ |
| `Y_NP_041_GhzState` | GHZ τ₃=1, reductions separable | ✅ |
| `Y_NP_041_WState` | W τ₃=0, bipartite C=2/3 | ✅ |
| `Y_NP_041_Monogamy` | CKW monogamy holds | ✅ |
| `Y_NP_041_EntanglementEntropy` | S(ρ_A)=H(a²) | ✅ |
| `Y_NP_041_TeleportationFidelity` | F=(2+C)/3, Bell→1 | ✅ |
| `Y_NP_041_Classification` | DERIVED/REFUTED/CORRESPONDENCE | ✅ |
| `Y_NP_041_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| Bell pair | **DERIVED** |
| Entanglement entropy S(ρ_A) | **DERIVED** |
| CKW monogamy | **DERIVED** |
| Teleportation fidelity F=(2+C)/3 | **DERIVED** |
| GHZ state from a single rank-2 link | **REFUTED** |
| W state from a single rank-2 link | **REFUTED** |
| Genuine multipartite entanglement | **CORRESPONDENCE** (3-body extension) |

## Conclusion

The Joint Link State is merely sufficient for Bell pairs: a complete two-body
entanglement sector, not a complete multipartite sector. GHZ/W require a 3-body joint
state or entangling gate. Canonical D96 unchanged.
