# Y_NP_039_Result.md — ResearchY-NP_039 Minimal Entanglement Sector Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_039_Tests.cs`
**Run:** 2026-09-04
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_039"`

---

## Summary

**Question:** What is the minimal extension required to obtain Bell-type entanglement
(canonical D96 unchanged)?

**Verdict: ONE new primitive — the joint link state (a coherent two-sector amplitude,
e.g. a Bell pair), hosted on the DERIVED A×B tensor product.** Added primitive count
= 1. Of the four candidate additions, only D (non-local information / joint link
state) reaches Schmidt rank > 1, concurrence > 0, CHSH > 2.

## Candidate witnesses

| Candidate | Schmidt rank | Concurrence | CHSH | MI |
|---|---|---|---|---|
| A) complex phase sector | 1 | 0 | 2 | 0 |
| B) tensor-state sector | 1 (product only) | 0 | 2 | 0 |
| C) shared occupancy sector (p=1/3) | — (separable) | 0 | 2 | H(1/3) ≈ 0.918 bits |
| D) non-local information (Bell) | 2 | 1 | 2√2 ≈ 2.828 | 1 bit |

## First entangling modification

Only D. A phase is single-DOF (interference, not non-separability); the tensor
product B is the host but holds only product states; shared occupancy C correlates
yet stays diagonal-separable. The joint link state (Bell pair) is the first candidate
with Schmidt rank 2, concurrence 1, CHSH = 2√2 — QG71's joint link state.

## Added primitive count

| Candidate | Status |
|---|---|
| A) complex phase sector | DERIVED (θ already canonical, QG220) |
| B) tensor-state sector | DERIVED (formal construction, the host) |
| C) shared occupancy sector | DERIVED (classical correlation) |
| D) non-local information / joint link state | NEW PRIMITIVE (1 added) |

**Total added primitives = 1.**

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_039_ComplexPhaseSectorSingleDof` | phase sector → rank 1, C=0, CHSH=2 | ✅ |
| `Y_NP_039_TensorStateSectorProductOnly` | A×B holds only product states | ✅ |
| `Y_NP_039_SharedOccupancyClassical` | shared occupancy MI>0 but separable | ✅ |
| `Y_NP_039_JointLinkStateEntangles` | joint link state rank 2, C=1, CHSH=2√2 | ✅ |
| `Y_NP_039_MinimalExtension` | only D entangles; added count = 1 | ✅ |
| `Y_NP_039_Classification` | DERIVED / NEW PRIMITIVE / REFUTED flags | ✅ |
| `Y_NP_039_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| A) complex phase sector | **DERIVED** (θ); REFUTED as entangler |
| B) tensor-state sector | **DERIVED** (host); not an entangler |
| C) shared occupancy sector | **DERIVED** (classical); REFUTED as entangler |
| D) non-local information / joint link state | **NEW PRIMITIVE** (1 added) |
| Entanglement from θ + S alone | **REFUTED** (QG70) |

## Conclusion

The smallest entanglement-capable extension of AT is one new primitive — the joint
link state (coherent two-sector amplitude) hosted on the DERIVED A×B tensor product.
Nothing short of it (phase, tensor product, shared occupancy) reaches Schmidt rank
≥ 2. Canonical D96 unchanged; QG70/71 confirmed.
