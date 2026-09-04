# Y_NP_042_Result.md — ResearchY-NP_042 Multipartite Entanglement Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_042_Tests.cs`
**Run:** 2026-09-04
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_042"`

---

## Summary

**Question:** What is the minimal extension of the Joint Link State required to obtain
GHZ, W, and genuine multipartite entanglement?

**Verdict: ONE new primitive — the 3-body (n-body) joint state**, the direct
generalization of QG71's joint link state from a 2-node link to a 3-node hyper-edge.
It is the first structure capable of GHZ, W, and genuine multipartite entanglement.

## Candidate ontologies

| Ontology | Added primitives | GHZ | W |
|---|---|---|---|
| B) pairwise links only | 0 | ✗ (biseparable) | ✗ |
| A) 3-body joint state | **1** | ✓ | ✓ |
| C) entangling composition (CZ) | 1 | ✓ (cluster) | ✗ |

## Results

- **B REFUTED:** Bell_AB ⊗ |0⟩_C has τ₃ = 0, C(AB)=1, C(AC)=C(BC)=0 (biseparable).
- **A SUFFICIENT:** 3-body joint state hosts GHZ (τ₃=1) and W (τ₃=0, pairwise C=2/3).
- **C GHZ-only:** CZ cluster state τ₃=1 (LU-equivalent to GHZ); CZ cannot produce W
  (graph states equal-magnitude, W has a zero).

## Entropy partitions

| State | S(A) | S(B) | S(C) |
|---|---|---|---|
| GHZ | 1 bit | 1 bit | 1 bit |
| W | H(2/3)=0.918 | 0.918 | 0.918 |
| Bell_AB ⊗ \|0⟩_C | 1 | 1 | 0 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_042_PairwiseLinksBiseparable` | Bell network biseparable | ✅ |
| `Y_NP_042_ThreeBodyJointStateGhz` | 3-body hosts GHZ | ✅ |
| `Y_NP_042_ThreeBodyJointStateW` | 3-body hosts W | ✅ |
| `Y_NP_042_ClusterStateGhzClass` | CZ cluster = GHZ class, not W | ✅ |
| `Y_NP_042_EntropyPartitions` | S partitions | ✅ |
| `Y_NP_042_CountAddedPrimitives` | B=0, A=1, C=1; minimal=A | ✅ |
| `Y_NP_042_Classification` | DERIVED/REFUTED/NEW PRIMITIVE | ✅ |
| `Y_NP_042_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| Bell / CHSH / teleportation (2-body) | **DERIVED** |
| Pairwise links only (B) | **REFUTED** (biseparable) |
| 3-body joint state (A) | **NEW PRIMITIVE** (1 added) |
| Entangling composition rule (C, CZ) | **NEW PRIMITIVE** (1 added, GHZ class) |

## Conclusion

The first structure capable of GHZ, W, and genuine multipartite entanglement is the
3-body (n-body) joint state — 1 added primitive, the generalization of the QG71 joint
link state. Canonical D96 unchanged.
