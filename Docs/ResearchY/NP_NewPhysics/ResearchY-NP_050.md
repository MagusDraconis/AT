# ResearchY-NP_050 — Physical Realization Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_050 (permanent)
**Title:** Physical Realization Audit
**Status:** COMPLETE
**Date:** 2026-09-05
**File:** `NP_NewPhysics/ResearchY-NP_050.md`
**Depends on:** ResearchY-NP_049 (gate uniquely required), NP_048 (gate irreducible),
NP_047 (gate = creation primitive), NP_046 (non-separability primitive), NP_045 (CHSH
a fact), NP_044 (optional for derived chain), NP_043 (joint state irreducible), NP_042
(3-body), NP_041 (2-body), NP_040 (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038
(entanglement ABSENT), QG_071 (joint link state — NEW SECTOR), QG_070 (entangling
interaction missing), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ), D_036–D_040 (complex state /
boundary set), M_001 (measurement reads both quadratures), R_001 (boundary set),
S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_050_Tests.cs`

---

## Purpose

> What physical interaction corresponds to the Entangling Gate?

**Known:** Joint State = required (NP_043); Entangling Gate = required (NP_048/049).

**Program steps:** (1) inventory known entangling interactions — photons, spin coupling,
exchange interaction, cavity QED, superconducting qubits; (2) abstract them to gate
language; (3) determine the common structure; (4) test whether a single abstract
entangling interaction explains all; (5) compare with Joint State, Entangling Gate,
canonical D96.

**Success criterion:** identify the physical meaning of the entangling gate primitive.

**Classification:** DERIVED / CORRESPONDENCE / NEW PRIMITIVE / REFUTED.

---

## 1. Inventory of known entangling interactions

| Mechanism | Physical process | Gate realized |
|---|---|---|
| Photons (SPDC) | parametric down-conversion | Bell-pair creation (joint two-mode amplitude) |
| Spin coupling (Heisenberg) | J·σ·σ exchange | iSWAP / √SWAP |
| Exchange interaction | identical-fermion antisymmetry | singlet/triplet entangling |
| Cavity QED (Jaynes-Cummings) | atom–field exchange | effective XX/ZZ coupling |
| Superconducting qubits | capacitive/inductive coupling | XX coupling |

Verified (test `Y_NP_050_EntanglingInteractionsInventory`): every mechanism is a
**genuine two-body coupling** — a joint term, not a sum of single-body terms.

---

## 2. Abstracted to gate language

Each mechanism reduces to a **non-local unitary**

U = e^{−i H_int t},   H_int = J·σ⊗σ  (a genuine two-body term).

Verified (tests `Y_NP_050_LocalHamiltonianPreservesRank`,
`Y_NP_050_NonLocalHamiltonianCreatesRank2`):

| Hamiltonian | Type | Effect |
|---|---|---|
| H = σ_z ⊗ I | local (single-body) | preserves rank 1 |
| H = σ_z ⊗ σ_z (Ising ZZ) | non-local (two-body) | creates rank 2 |
| H = σ_x ⊗ σ_x (XX) | non-local (two-body) | creates rank 2 |

A **local** Hamiltonian generates a local unitary (rank-preserving); a **non-local**
one raises rank. This is the precise difference between the canonical phase update
(local) and the entangling gate (non-local).

---

## 3. The common structure

Verified (test `Y_NP_050_CommonStructure`): the common signature of an entangling
interaction is a **non-trivial two-body coupling** — either a cross-block coupling
(|00⟩↔|11⟩, as in XX) or a controlled (joint) phase (as in Ising ZZ). The local gate has
neither. This is the single abstract feature shared by all five mechanisms.

---

## 4. One abstract interaction explains all

Verified (test `Y_NP_050_SingleAbstractInteraction`): a single abstract interaction —
the non-local two-body unitary U = e^{−i H_int t} — explains all five mechanisms; each
is LU-equivalent to the entangling gate (CNOT/CZ/iSWAP, NP_049).

---

## 5. Comparison with the AT chain

| Object | Meaning |
|---|---|
| Canonical D96 | NO two-body coupling (NP_048) — local/classical only |
| Joint State | the OUTPUT of the gate (NP_040) |
| Entangling Gate | the non-local two-body interaction itself |

Verified (test `Y_NP_050_CanonicalD96HasNoCoupling`): canonical D96 has no two-body
coupling term, so the gate is a hosted CORRESPONDENCE — not a D96 derivation.

---

## 6. Legacy-lane reconciliation

- **QG70:** the "entangling interaction" is missing from θ + S — NP_050 names it: a
  coherent two-body (non-local) coupling Hamiltonian.
- **QG71 / NP_042:** the "entangling composition rule" is this interaction.
- **NP_048/049:** the gate is irreducible and uniquely required — NP_050 supplies its
  physical content (the two-body coupling).

---

## Theorem

> **Theorem (NP_050).** The physical meaning of the entangling gate primitive is a
> coherent two-body (non-local) interaction — a joint coupling Hamiltonian
> H_int = J·σ⊗σ generating U = e^{−i H_int t} — which is a CORRESPONDENCE to known
> entangling mechanisms (SPDC, exchange, cavity QED, superconducting qubits), not a
> DERIVED object of canonical D96. Proof: (1) Inventory (verified): all five known
> mechanisms are genuine two-body couplings. (2) Local vs non-local (verified): H =
> σ_z⊗I preserves rank 1, while H = σ_z⊗σ_z and H = σ_x⊗σ_x create rank 2. (3) Common
> structure (verified): the entangling signature is a cross-block coupling or a joint
> phase — absent in the local gate. (4) One abstract interaction (verified): the
> non-local unitary explains all five mechanisms (LU-equivalent). (5) Canonical D96
> (verified): has no two-body coupling (NP_048). Hence the gate = the non-local
> two-body interaction (CORRESPONDENCE / NEW PRIMITIVE in AT). Canonical D96 unchanged.
>
> *Proof sketch.* (1) inventory. (2) local preserves, non-local raises. (3) common
> signature. (4) one interaction. (5) D96 lacks it. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "A local interaction entangles" | H_A⊗I + I⊗H_B generates a local unitary (rank-preserving) |
| "The gate has no physical content" | it is the non-local two-body coupling (SPDC, exchange, …) |
| "Each mechanism is a distinct primitive" | all are LU-equivalent non-local unitaries |
| "Canonical D96 has this interaction" | D96 has only local/classical operations (NP_048) |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| The gate is a two-body interaction | a single-body Hamiltonian raising rank 1 → 2 |
| One abstract interaction suffices | a mechanism not expressible as a non-local unitary |
| Canonical D96 lacks it | a canonical two-body coupling term (NP_048 refutation) |

---

## Classification

| Component | Status |
|---|---|
| Known entangling interactions (SPDC, exchange, cavity QED, qubits) | **CORRESPONDENCE** (hosted physics) |
| Physical meaning = coherent two-body interaction | **CORRESPONDENCE** (maps onto known physics) |
| Entangling gate as NEW PRIMITIVE in AT | **CONFIRMED** (NP_047/048/049) |
| Gate DERIVED from canonical D96 | **REFUTED** (no two-body coupling, NP_048) |

**Conclusion:** the entangling gate corresponds physically to a **coherent two-body
(non-local) interaction** — a joint coupling Hamiltonian H_int = J·σ⊗σ that generates
the non-local unitary U = e^{−i H_int t}. Every known entangling mechanism (photon SPDC,
Heisenberg exchange, exchange interaction, cavity QED, superconducting qubits) is this
same abstract interaction in a different realization, all LU-equivalent to the
entangling gate (CNOT/CZ/iSWAP). It is a **CORRESPONDENCE** to known physics, hosted —
**not derivable** from canonical D96, which contains no two-body coupling term.
**Canonical D96 unchanged.**

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_050_Tests.cs`
**Run:** 2026-09-05 · **Result:** see `Tests/Results/Y_NP_050_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_050_EntanglingInteractionsInventory` | 5 mechanisms two-body | ✅ |
| `Y_NP_050_LocalHamiltonianPreservesRank` | local H preserves rank 1 | ✅ |
| `Y_NP_050_NonLocalHamiltonianCreatesRank2` | Ising/XX create rank 2 | ✅ |
| `Y_NP_050_CommonStructure` | two-body coupling signature | ✅ |
| `Y_NP_050_SingleAbstractInteraction` | one interaction explains all | ✅ |
| `Y_NP_050_CanonicalD96HasNoCoupling` | D96 lacks the coupling | ✅ |
| `Y_NP_050_Classification` | CORRESPONDENCE / NEW PRIMITIVE | ✅ |
| `Y_NP_050_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_050"`

---

## References

- ResearchY-NP_049 (gate uniquely required), NP_048 (gate irreducible), NP_047 (gate =
  creation primitive), NP_046 (non-separability primitive), NP_045 (CHSH a fact),
  NP_044 (optional for derived chain), NP_043 (joint state irreducible), NP_042 (3-body),
  NP_041 (2-body), NP_040 (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038
  (entanglement ABSENT), QG_071 (joint link state — NEW SECTOR; EntanglingSector.cs),
  QG_070 (entangling interaction missing; EntanglementFromLinks.cs), QG_220 (θ=2πk/N),
  QG_216 (|ψ|=√ρ), D_036–D_040 (complex state; boundary set), M_001 (measurement reads
  both quadratures), R_001 (boundary set), S_001 (synthesis).
