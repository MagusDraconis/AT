# ResearchY-NP_040 — Joint Link Formalization Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_040 (permanent)
**Title:** Joint Link Formalization Audit
**Status:** COMPLETE
**Date:** 2026-09-04
**File:** `NP_NewPhysics/ResearchY-NP_040.md`
**Depends on:** ResearchY-NP_039 (joint link state = minimal extension, 1 NEW
PRIMITIVE), ResearchY-NP_038 (entanglement ABSENT — only correlation), QG_071
(joint link state — NEW SECTOR), QG_070 (θ + S ⇒ interference + spinor DOF, entangling
interaction missing), QG_220 (θ = 2πk/N), QG_216 (|ψ| = √ρ), D_036–D_040 (complex
state / boundary set), M_001 (measurement reads both quadratures), R_001 (boundary
set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_040_Tests.cs`

---

## Purpose

> What is the minimal mathematical object representing the Joint Link State?

**Program steps:** (1) define required properties — Schmidt rank > 1, concurrence
C > 0, CHSH > 2; (2) search the smallest structure satisfying all; (3) test
symmetry, normalization, composition, locality; (4) determine whether the object is
a graph edge, an information link, an occupancy link, a phase link, or a new state
object.

**Goal:** formalize the QG71 Joint Link State with the minimum possible ontology.

**Classification:** DERIVED / EMERGENT / NEW PRIMITIVE / REFUTED.

---

## 1. State model (recap)

Canonical single-sector state over {|0⟩, |1⟩}: ψ_S = √ρ₀·e^{iθ₀}|0_S⟩ + √ρ₁·e^{iθ₁}|1_S⟩,
θ_k = 2πk/N, N = 96. A joint two-qubit state is the 2×2 complex coefficient matrix
c_{ij} = ⟨ij|ψ⟩. NP_039 established the joint link state (a rank-2 c, e.g. the Bell
pair) as the minimal entangling extension — one NEW PRIMITIVE. NP_040 now asks: what
*is* that object, minimally?

---

## 2. The three required properties are one condition

For a **normalized pure two-qubit state** the three requirements are equivalent
(verified, test `Y_NP_040_RequiredPropertiesEquivalent`):

| Property | Equivalent form |
|---|---|
| Schmidt rank > 1 | det c ≠ 0 (full rank) |
| concurrence C > 0 | C = 2\|det c\| > 0 |
| CHSH > 2 | CHSH = 2√(1+C²) > 2 |

Sweeping the diagonal family a|00⟩ + b|11⟩ (a = cos α, b = sin α) confirms all three
flip together, exactly at det c ≠ 0. A non-diagonal rank-2 example obeys the same
equivalence. So **one condition, three readings** — the required properties are not
three independent demands.

---

## 3. The minimal structure

The smallest object satisfying all three properties is a **rank-2 complex 2×2 matrix**
(verified, test `Y_NP_040_MinimalStructure`):

| Content | Rank | Verdict |
|---|---|---|
| one nonzero entry (e.g. \|00⟩) | 1 | product, not entangled |
| two nonzero entries, full-rank arrangement (a\|00⟩+b\|11⟩, a,b≠0) | 2 | entangled |
| two nonzero entries, non-full-rank (\|00⟩+\|01⟩) | 1 | product |

The minimal content is **two nonzero amplitudes in a coherent joint superposition**.
The canonical symmetric representative is the Bell pair (|00⟩+|11⟩)/√2 — a maximally
entangled, rank-2 2×2 matrix.

---

## 4. The four properties

**Symmetry** (test `Y_NP_040_Symmetry`): the Bell state is invariant under the
per-sector bit flip X⊗X, symmetric under swapping the two sectors (c^T = c), and has
equal reduced densities ρ_A = ρ_B = I/2 — the joint link state lives on the symmetric
2-node link.

**Normalization** (test `Y_NP_040_Normalization`): Σ|c_ij|² = 1, and the squared
singular values sum to 1 (probability conservation).

**Composition** (test `Y_NP_040_Composition`): the object is a **per-link primitive** —
two disjoint links compose by tensor product (each carries its own rank-2 state), and
the combined 4-qubit amplitude stays normalized. The ontology scales as one object
per link.

**Locality** (test `Y_NP_040_Locality`): the joint link state is **non-local** — each
single sector is maximally mixed (ρ_A = ρ_B = I/2, zero local information), yet the
joint state is pure and maximally entangled (C = 1). Entanglement is a global property
of the link, not a property of either node.

---

## 5. Ontology: what kind of object is it?

| Candidate | Carries | Entangles? | Verdict |
|---|---|---|---|
| graph edge | binary adjacency (no amplitude) | no | NOT the object — it has no state |
| information link | classical bits (diagonal) | no | rank 1, separable |
| occupancy link | shared occupancy (diagonal) | no | rank 1, separable |
| phase link | single-DOF phase θ | no | rank 1 (interference only) |
| **new state object** | coherent joint two-qubit amplitude c_ij | **yes** | **the joint link state** |

Verified (test `Y_NP_040_Ontology`): a graph edge carries no amplitude; the
information/occupancy link (diagonal mixture) has concurrence 0; the phase link
(product of canonical sectors) has Schmidt rank 1; only the rank-2 joint amplitude
(the Bell pair) entangles.

**The minimal ontology is a NEW STATE OBJECT** — a normalized rank-2 complex 2×2
matrix (a coherent two-qubit amplitude) hosted on the 2-node link. It is *not*
reducible to any of the four link types: each of those carries only classical or
single-DOF content.

---

## 6. Legacy-lane reconciliation

- **QG71:** the joint link state is "COMPATIBLE with the link, but new" — NEW SECTOR.
  NP_040 supplies the precise formalization: the object is a rank-2 2×2 matrix (a
  coherent joint amplitude), the link being its natural home.
- **QG70:** the entangling interaction is missing from θ + S — NP_040 confirms the
  object is not a phase link (single-DOF) and not reducible to θ.
- **NP_038:** entanglement ABSENT from canonical D96 — NP_040 confirms no canonical
  link type reaches rank 2.
- **NP_039:** the joint link state is the minimal extension (1 new primitive) —
  NP_040 pins its ontology as a new state object.

---

## Theorem

> **Theorem (NP_040).** The minimal mathematical object representing the QG71 joint
> link state is a normalized rank-2 complex 2×2 matrix — a coherent two-qubit
> amplitude c_{ij} with det c ≠ 0 — and its ontology is a NEW STATE OBJECT, not a
> graph edge, information link, occupancy link, or phase link. Proof: (1) Equivalence
> (verified): for a normalized pure two-qubit state, Schmidt rank 2 ⇔ C = 2|det c| > 0
> ⇔ CHSH = 2√(1+C²) > 2 ⇔ det c ≠ 0 — the three required properties are one
> condition. (2) Minimality (verified): one nonzero entry is a product (rank 1); two
> nonzero entries in a full-rank arrangement (a|00⟩+b|11⟩) already give rank 2 — the
> minimum content is a two-term coherent superposition. (3) Properties (verified):
> symmetric (X⊗X invariant, A↔B symmetric, ρ_A = ρ_B = I/2); normalized
> (Σ|c_ij|² = 1); per-link composition (tensor product); non-local (reduced densities
> mixed, joint state pure). (4) Ontology (verified): a graph edge has no amplitude; an
> information/occupancy link is diagonal (concurrence 0); a phase link is single-DOF
> (rank 1); only a rank-2 joint amplitude entangles. Hence the object is a NEW STATE
> OBJECT (NEW PRIMITIVE, per NP_039), hosted on the 2-node link. Canonical D96
> unchanged.
>
> *Proof sketch.* (1) equivalence. (2) minimality. (3) properties. (4) ontology. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "The joint link state is a graph edge" | an edge carries no amplitude; it cannot hold a coherent state |
| "It is an information link" | classical information is diagonal ⇒ separable (concurrence 0) |
| "It is an occupancy link" | shared occupancy is a diagonal classical mixture (rank 1) |
| "It is a phase link" | a single-DOF phase gives interference, rank 1 |
| "The three properties are independent" | for pure 2-qubit they are all ⇔ det c ≠ 0 |
| "It needs more than 2 nonzero amplitudes" | a|00⟩+b|11⟩ (2 terms) already has rank 2 |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| Rank 2 ⇔ C>0 ⇔ CHSH>2 | a normalized pure 2-qubit state with rank 2 but CHSH = 2 (or C = 0) |
| Two nonzero amplitudes suffice | a full-rank 2×2 with fewer than 2 nonzero entries |
| It is not reducible to a link type | a graph/phase/occupancy/information link reaching Schmidt rank 2 |
| It is a new state object | a derivation of a rank-2 joint amplitude from θ + S alone (QG70 refutation) |

---

## Classification

| Component | Status |
|---|---|
| Rank-2 2×2 matrix (det c ≠ 0) as the minimal object | **DERIVED** (closed-form equivalence, verified) |
| Two-term coherent superposition as minimal content | **DERIVED** (minimality test, verified) |
| Joint link state as graph/information/occupancy/phase link | **REFUTED** (each is rank 1 / no amplitude) |
| Joint link state as a new state object | **NEW PRIMITIVE** (per NP_039; QG71 NEW SECTOR) |
| Canonical D96 producing the joint link state | **REFUTED** (NP_038 — only correlation) |

**Conclusion:** the QG71 Joint Link State is formally a **normalized rank-2 complex
2×2 matrix** — a coherent two-qubit amplitude — with minimum ontology **one NEW STATE
OBJECT** (NEW PRIMITIVE), hosted on the 2-node link. It is the smallest structure
satisfying rank > 1, C > 0, CHSH > 2, and is not reducible to any graph/link type.
**Canonical D96 unchanged.**

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_040_Tests.cs`
**Run:** 2026-09-04 · **Result:** see `Tests/Results/Y_NP_040_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_040_RequiredPropertiesEquivalent` | rank 2 ⇔ C>0 ⇔ CHSH>2 ⇔ det≠0 | ✅ |
| `Y_NP_040_MinimalStructure` | 2 nonzero entries suffice; 1 is product | ✅ |
| `Y_NP_040_Symmetry` | X⊗X, A↔B, ρ_A=ρ_B=I/2 | ✅ |
| `Y_NP_040_Normalization` | Σ\|c_ij\|²=1, singular values sum 1 | ✅ |
| `Y_NP_040_Composition` | per-link tensor composition | ✅ |
| `Y_NP_040_Locality` | non-local (mixed reduced, pure joint) | ✅ |
| `Y_NP_040_Ontology` | new state object, not a link type | ✅ |
| `Y_NP_040_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_040"`

---

## References

- ResearchY-NP_039 (joint link state = minimal extension, 1 NEW PRIMITIVE),
  ResearchY-NP_038 (entanglement ABSENT), QG_071 (joint link state — NEW SECTOR;
  EntanglingSector.cs), QG_070 (entangling interaction missing; EntanglementFromLinks.cs),
  QG_220 (θ = 2πk/N), QG_216 (|ψ| = √ρ), D_036–D_040 (complex state; irreducible
  boundary set), M_001 (measurement reads both quadratures), R_001 (five-item
  boundary set), S_001 (synthesis).
