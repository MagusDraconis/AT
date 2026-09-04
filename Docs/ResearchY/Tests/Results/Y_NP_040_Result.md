# Y_NP_040_Result.md — ResearchY-NP_040 Joint Link Formalization Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_040_Tests.cs`
**Run:** 2026-09-04
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_040"`

---

## Summary

**Question:** What is the minimal mathematical object representing the QG71 Joint
Link State?

**Verdict: a normalized rank-2 complex 2×2 matrix — a coherent two-qubit amplitude —
with minimum ontology ONE NEW STATE OBJECT (NEW PRIMITIVE), hosted on the 2-node
link.** It is the smallest structure satisfying rank > 1, C > 0, CHSH > 2, and is not
reducible to any graph/link type.

## Equivalence (Section 2)

For a normalized pure two-qubit state, the three required properties are ONE
condition: Schmidt rank 2 ⇔ C = 2|det c| > 0 ⇔ CHSH = 2√(1+C²) > 2 ⇔ det c ≠ 0.
Verified by sweeping a|00⟩+b|11⟩ (a=cos α, b=sin α) and a non-diagonal rank-2 example.

## Minimal structure (Section 3)

| Content | Rank |
|---|---|
| one nonzero entry (|00⟩) | 1 (product) |
| two nonzero entries, full-rank (a|00⟩+b|11⟩, a,b≠0) | 2 (entangled) |
| two nonzero entries, non-full-rank (|00⟩+|01⟩) | 1 (product) |

Minimal content = two nonzero amplitudes in a coherent joint superposition; canonical
representative = the Bell pair (|00⟩+|11⟩)/√2.

## Properties (Section 4)

- **Symmetry:** X⊗X invariant; symmetric under A↔B; ρ_A = ρ_B = I/2.
- **Normalization:** Σ|c_ij|² = 1; singular values squared sum to 1.
- **Composition:** per-link primitive — two links compose by tensor product.
- **Locality:** NON-LOCAL — each sector maximally mixed (I/2), joint state pure (C=1).

## Ontology (Section 5)

| Candidate | Entangles? |
|---|---|
| graph edge | no (no amplitude) |
| information link | no (diagonal, rank 1) |
| occupancy link | no (diagonal, rank 1) |
| phase link | no (single-DOF, rank 1) |
| **new state object** | **yes (rank 2)** |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_040_RequiredPropertiesEquivalent` | rank 2 ⇔ C>0 ⇔ CHSH>2 ⇔ det≠0 | ✅ |
| `Y_NP_040_MinimalStructure` | 2 nonzero entries suffice | ✅ |
| `Y_NP_040_Symmetry` | X⊗X, A↔B, ρ_A=ρ_B=I/2 | ✅ |
| `Y_NP_040_Normalization` | Σ\|c_ij\|²=1 | ✅ |
| `Y_NP_040_Composition` | per-link tensor composition | ✅ |
| `Y_NP_040_Locality` | non-local | ✅ |
| `Y_NP_040_Ontology` | new state object | ✅ |
| `Y_NP_040_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| Rank-2 2×2 matrix as minimal object | **DERIVED** (closed-form equivalence) |
| Two-term superposition as minimal content | **DERIVED** (minimality) |
| Joint link state as graph/information/occupancy/phase link | **REFUTED** |
| Joint link state as a new state object | **NEW PRIMITIVE** (NP_039; QG71) |

## Conclusion

The QG71 Joint Link State is a normalized rank-2 complex 2×2 matrix (a coherent
two-qubit amplitude), with minimum ontology ONE NEW STATE OBJECT (NEW PRIMITIVE),
hosted on the 2-node link. Canonical D96 unchanged.
