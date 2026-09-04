# ResearchY-NP_042 — Multipartite Entanglement Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_042 (permanent)
**Title:** Multipartite Entanglement Audit
**Status:** COMPLETE
**Date:** 2026-09-04
**File:** `NP_NewPhysics/ResearchY-NP_042.md`
**Depends on:** ResearchY-NP_041 (2-body joint link: Bell/CHSH/teleportation DERIVED,
GHZ/W NOT derivable), ResearchY-NP_040 (joint link state = rank-2 2×2 matrix, NEW
STATE OBJECT), ResearchY-NP_039 (joint link state = minimal extension, 1 NEW
PRIMITIVE), ResearchY-NP_038 (entanglement ABSENT), QG_071 (joint link state — NEW
SECTOR), QG_070 (entangling interaction missing), QG_220 (θ = 2πk/N), QG_216
(|ψ| = √ρ), D_036–D_040 (complex state / boundary set), M_001 (measurement reads both
quadratures), R_001 (boundary set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_042_Tests.cs`

---

## Purpose

> What is the minimal extension of the Joint Link State required to obtain GHZ, W,
> and genuine multipartite entanglement?

**Known (NP_041):** the 2-body joint link gives Bell ✓, CHSH ✓, teleportation ✓, but
GHZ ✗, W ✗.

**Program steps:** (1) construct A-B and A-B-C joint-state sectors; (2) test GHZ, W,
graph states, cluster states; (3) compute τ₃, pairwise concurrence, entropy
partitions; (4) determine the minimal ontology — A) 3-body joint state, B) pairwise
links only, C) entangling composition rule; (5) count added primitives.

**Classification:** DERIVED / EMERGENT / NEW PRIMITIVE / REFUTED.

---

## 1. State model (recap)

The 2-body joint link state is a rank-2 complex 2×2 matrix (a two-qubit amplitude) on
a 2-node link. NP_041 showed it closes under two-body phenomenology but cannot reach
genuine tripartite entanglement. NP_042 generalizes: a **3-body joint state** is a
coherent 8-component amplitude over three nodes (a 3-node hyper-edge). The canonical
states are GHZ = (|000⟩+|111⟩)/√2 and W = (|001⟩+|010⟩+|100⟩)/√3.

---

## 2. The three candidate ontologies

| Ontology | Content | Added primitives | Entangles? |
|---|---|---|---|
| **B) pairwise links only** | a network of 2-body Bell pairs | 0 | biseparable — no genuine tripartite state |
| **A) 3-body joint state** | a coherent 3-qubit amplitude | 1 | GHZ AND W |
| **C) entangling composition** | a CZ entangling gate composing links | 1 | GHZ (graph/cluster states), NOT W |

---

## 3. Results (verified)

**B) pairwise links only is REFUTED** (test `Y_NP_042_PairwiseLinksBiseparable`):
Bell_AB ⊗ |0⟩_C has τ₃ = 0, pairwise concurrence C(AB)=1 but C(AC)=C(BC)=0 — a
biseparable state. A network of Bell pairs never reaches genuine tripartite
entanglement.

**A) 3-body joint state is SUFFICIENT** (tests `Y_NP_042_ThreeBodyJointStateGhz`,
`Y_NP_042_ThreeBodyJointStateW`): the 3-body joint state directly hosts GHZ (τ₃ = 1)
and W (τ₃ = 0, but genuinely tripartite with pairwise concurrence C = 2/3).

**C) entangling composition is GHZ-class only** (test `Y_NP_042_ClusterStateGhzClass`):
the CZ-composed 3-qubit cluster/graph state is LU-equivalent to GHZ (τ₃ = 1), but CZ
gates cannot produce W — graph states keep all 8 amplitudes equal-magnitude (1/√8),
whereas W has a zero and 1/√3 entries.

**Entropy partitions** (test `Y_NP_042_EntropyPartitions`): GHZ has every single-qubit
reduction maximally mixed (S = 1 bit); W has S = H(2/3) ≈ 0.918; the biseparable
Bell_AB ⊗ |0⟩_C has S(A)=S(B)=1 but S(C)=0 (C unentangled).

---

## 4. Minimal ontology & added primitive count

| Candidate | Added primitives | GHZ | W |
|---|---|---|---|
| B) pairwise links only | 0 | ✗ | ✗ |
| A) 3-body joint state | **1** | ✓ | ✓ |
| C) entangling composition (CZ) | 1 | ✓ | ✗ |

**The minimal sufficient ontology is A — the 3-body joint state — at 1 added
primitive.** It is the direct n-body generalization of QG71's joint link state (from
a 2-node link to a 3-node hyper-edge), and it is the ONLY candidate that covers BOTH
GHZ and W. C (the entangling gate) is also 1 primitive but generates only the
graph/cluster (GHZ) family; W is a distinct SLOCC class unreachable by CZ composition.

---

## 5. Legacy-lane reconciliation

- **QG71:** the joint link state is a 2-node NEW SECTOR. NP_042 shows that to reach
  genuine multipartite entanglement the primitive must be GENERALIZED from 2-body to
  n-body (3-body) — one further new primitive.
- **QG70:** the "entangling interaction" is missing from θ + S. NP_042 identifies this
  missing interaction with TWO faces: the 3-body joint STATE (A) and the entangling
  GATE (C); both are one added primitive, but A is the state-level minimum.
- **NP_041:** the 2-body link is merely sufficient for Bell pairs — NP_042 supplies
  the next rung: the 3-body joint state.

---

## Theorem

> **Theorem (NP_042).** The minimal extension of the 2-body joint link state that
> obtains GHZ, W, and genuine multipartite entanglement is a single new primitive —
> the 3-body (n-body) joint state, the generalization of QG71's joint link state from
> a 2-node link to a 3-node hyper-edge. Proof: (1) Pairwise links (verified): a
> network of Bell pairs (Bell_AB ⊗ |0⟩_C) is biseparable (τ₃ = 0, C(AC)=C(BC)=0) — 0
> added primitives but insufficient (REFUTED). (2) 3-body joint state (verified):
> hosts GHZ (τ₃ = 1) and W (τ₃ = 0, pairwise C = 2/3) — 1 added primitive, sufficient.
> (3) Entangling composition (verified): the CZ-composed cluster state has τ₃ = 1
> (LU-equivalent to GHZ), but CZ cannot produce W (graph states equal-magnitude, W has
> a zero) — 1 added primitive, GHZ-class only. (4) Entropy (verified): GHZ S = 1 per
> qubit, W S = H(2/3), biseparable S(C) = 0. Hence the first structure capable of the
> full hierarchy (GHZ AND W) is the 3-body joint state (A), added primitive count = 1.
> Classification: Bell/CHSH/teleportation DERIVED; pairwise links REFUTED; 3-body
> joint state NEW PRIMITIVE; entangling composition NEW PRIMITIVE (gate, GHZ-class).
> Canonical D96 unchanged.
>
> *Proof sketch.* (1) pairwise biseparable. (2) 3-body hosts GHZ+W. (3) CZ hosts GHZ
> only. (4) minimal = A, 1 primitive. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Pairwise Bell pairs give GHZ" | Bell_AB⊗|0⟩_C is biseparable (τ₃ = 0) |
| "CZ gates give all multipartite states" | CZ generates graph states (GHZ class); W is not a graph state |
| "W is a graph state" | W has a zero amplitude; graph states are equal-magnitude |
| "The 3-body state costs 3 primitives" | one n-body joint state suffices (1 primitive) |
| "No further primitive is needed" | 2-body links cannot reach genuine tripartite entanglement (NP_041) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| 3-body joint state is minimal | a GHZ or W built from 2-body links alone (biseparable refutation) |
| CZ cannot produce W | a CZ gate sequence turning |+⟩^⊗3 into W |
| 1 primitive suffices | a GHZ/W requiring two independent added primitives |
| W is genuinely tripartite | W expressed as a tensor product of smaller states |

---

## Classification

| Component | Status |
|---|---|
| Bell / CHSH / teleportation (2-body) | **DERIVED** (NP_041 unchanged) |
| Pairwise links only (B) | **REFUTED** as sufficient for GHZ/W (biseparable) |
| 3-body joint state (A) | **NEW PRIMITIVE** (1 added; sufficient for GHZ and W) |
| Entangling composition rule (C, CZ gate) | **NEW PRIMITIVE** (1 added; GHZ/cluster class only) |
| GHZ / W states | hosted by the 3-body joint state (n-body generalization) |

**Conclusion:** the first structure capable of GHZ, W, and genuine multipartite
entanglement is the **3-body (n-body) joint state** — the direct generalization of
QG71's joint link state from a 2-node link to a 3-node hyper-edge — at **1 added
primitive**. Pairwise links are biseparable (REFUTED); the CZ entangling gate reaches
only the GHZ/cluster family (not W). **Canonical D96 unchanged.**

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_042_Tests.cs`
**Run:** 2026-09-04 · **Result:** see `Tests/Results/Y_NP_042_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_042_PairwiseLinksBiseparable` | Bell network biseparable (τ₃=0) | ✅ |
| `Y_NP_042_ThreeBodyJointStateGhz` | 3-body hosts GHZ (τ₃=1) | ✅ |
| `Y_NP_042_ThreeBodyJointStateW` | 3-body hosts W (τ₃=0, C=2/3) | ✅ |
| `Y_NP_042_ClusterStateGhzClass` | CZ cluster = GHZ class, not W | ✅ |
| `Y_NP_042_EntropyPartitions` | S partitions for GHZ/W/biseparable | ✅ |
| `Y_NP_042_CountAddedPrimitives` | B=0, A=1, C=1; minimal=A | ✅ |
| `Y_NP_042_Classification` | DERIVED / REFUTED / NEW PRIMITIVE | ✅ |
| `Y_NP_042_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_042"`

---

## References

- ResearchY-NP_041 (2-body link: Bell/CHSH/teleportation DERIVED, GHZ/W NOT derivable),
  ResearchY-NP_040 (joint link state = rank-2 2×2 matrix), ResearchY-NP_039 (minimal
  extension, 1 NEW PRIMITIVE), ResearchY-NP_038 (entanglement ABSENT), QG_071 (joint
  link state — NEW SECTOR; EntanglingSector.cs), QG_070 (entangling interaction
  missing; EntanglementFromLinks.cs), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ),
  D_036–D_040 (complex state; boundary set), M_001 (measurement reads both
  quadratures), R_001 (boundary set), S_001 (synthesis).
