# ResearchY-NP_049 — Entangling Gate Necessity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_049 (permanent)
**Title:** Entangling Gate Necessity Audit
**Status:** COMPLETE
**Date:** 2026-09-05
**File:** `NP_NewPhysics/ResearchY-NP_049.md`
**Depends on:** ResearchY-NP_048 (gate irreducible), NP_047 (gate = creation
primitive), NP_046 (non-separability primitive), NP_045 (CHSH a fact), NP_044
(optional for derived chain), NP_043 (joint state irreducible), NP_042 (3-body),
NP_041 (2-body), NP_040 (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038
(entanglement ABSENT), QG_071 (joint link state — NEW SECTOR), QG_070 (entangling
interaction missing), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ), D_036–D_040 (complex state /
boundary set), M_001 (measurement reads both quadratures), R_001 (boundary set),
S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_049_Tests.cs`

---

## Purpose

> Is the Entangling Gate forced by observed quantum experiments, or could an
> alternative primitive replace it?

**Program steps:** (1) inventory all phenomena currently requiring the gate; (2) remove
the gate; (3) retain joint states only; (4) test Bell, CHSH, teleportation, GHZ, W;
(5) search alternative mechanisms — shared actualization, non-local phase, resonance
coupling, information coupling; (6) count primitive cost.

**Success criterion:** determine whether the Entangling Gate is A) uniquely required,
B) one of several equivalent primitives, C) replaceable.

**Classification:** DERIVED / CORRESPONDENCE / NEW PRIMITIVE / REFUTED.

---

## 1. Phenomena requiring the gate

Every entanglement phenomenon requires **creation** (rank 1 → rank 2), which is the
gate's exclusive job:

| Phenomenon | Body | Requires gate? |
|---|---|---|
| Bell / CHSH | 2-body | yes (prepare the pair) |
| Teleportation | 2-body | yes (Bell resource) |
| GHZ | 3-body | yes (CNOT composition) |
| W | 3-body | yes (3-body entangling operation) |

---

## 2. Remove the gate — joint states are unpreparable

Verified (test `Y_NP_049_RemoveGateRetainJointStates`): removing the gate while
retaining joint states leaves the joint states **statically present** (Bell still has
rank 2) but **unpreparable** — no canonical operation creates rank 2 from a product
(NP_048: max rank 1). The static joint state cannot be reached without the gate.

---

## 3. Alternative mechanisms — all fail

Verified (test `Y_NP_049_AlternativeMechanismsFail`):

| Alternative | Result |
|---|---|
| shared actualization (phase pinning) | rank 1 (classical) |
| non-local / shared phase | rank 1 (classical) — a truly non-local phase (acting on both qubits jointly) IS a controlled-phase = the gate itself |
| resonance coupling | ABSENT (NP_005/006) |
| information coupling | MI > 0 but separable (C = 0) |

The "non-local phase" alternative collapses: either it is a classical shared phase
(rank 1, fails) or a controlled-phase (which **is** the gate). No distinct alternative
primitive exists.

---

## 4. Representative equivalence within the primitive

Verified (test `Y_NP_049_GateRepresentativesEquivalent`): CNOT, CZ, iSWAP, and √SWAP
are all **LU-equivalent** entangling gates — each creates a rank-2 state from a product
input. They are the SAME primitive (the non-local entangling interaction) expressed in
different bases, **not** several independent primitives.

---

## 5. Primitive cost

Verified (test `Y_NP_049_PrimitiveCost`): the gate is **1 primitive**; the alternatives
cost **0** (already canonical) but are **insufficient** (never reach rank 2).

---

## 6. Legacy-lane reconciliation

- **NP_048:** the gate is irreducible — NP_049 shows it is also uniquely required: no
  alternative primitive can replace it.
- **QG70:** the "entangling interaction" is missing — NP_049 shows it is the unique
  missing primitive (not replaceable by any of the existing ones).
- **QG71 / NP_042:** the "entangling composition rule (CZ)" is one representative of
  this single primitive.
- **NP_044:** joint states are optional for the derived chain — NP_049 shows the gate
  is likewise optional for the derived chain but required for observed entanglement.

---

## Theorem

> **Theorem (NP_049).** The entangling gate is uniquely required as a KIND — the
> non-local entangling interaction — and is not replaceable by any alternative
> primitive; it has, however, LU-equivalent representatives (CNOT, CZ, iSWAP, √SWAP)
> that are the same primitive in different bases. Proof: (1) Phenomena (verified):
> Bell, CHSH, teleportation, GHZ, W all require creation (rank 1 → rank 2). (2) Remove
> gate (verified): joint states remain rank 2 statically but are unpreparable — no
> canonical operation creates rank 2. (3) Alternatives (verified): shared actualization
> (rank 1), shared phase (rank 1; a genuinely non-local phase = the controlled-phase =
> the gate), resonance (absent), information (separable) — all fail. (4) Equivalence
> (verified): CNOT, CZ, iSWAP, √SWAP each create rank-2 states from products — the same
> primitive. (5) Cost (verified): gate = 1; alternatives = 0 but insufficient. Hence the
> gate is A) uniquely required (as a kind), with representative freedom within the
> primitive, and C) replaceable is REFUTED. Canonical D96 unchanged.
>
> *Proof sketch.* (1) creation needed. (2) gate-less unpreparable. (3) alternatives
> fail. (4) LU-equivalence. (5) cost 1. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Shared actualization replaces the gate" | rank 1 (classical) |
| "A non-local phase replaces the gate" | a shared phase is rank 1; a controlled-phase IS the gate |
| "Resonance coupling replaces the gate" | ABSENT (NP_005/006) |
| "Information coupling replaces the gate" | MI > 0 but separable |
| "CNOT and CZ are two different primitives" | LU-equivalent — the same primitive in different bases |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| The gate is uniquely required | an alternative (non-gate) operation creating rank 1 → rank 2 |
| CNOT/CZ/iSWAP/√SWAP are equivalent | one of them failing to create entanglement from a product |
| The gate is replaceable | a canonical operation reaching rank 2 (NP_048 refutation) |

---

## Classification

| Component | Status |
|---|---|
| Bell / CHSH / teleportation / GHZ / W requiring creation | **DERIVED** (creation is the gate's job) |
| Entangling gate uniquely required (A) | **CONFIRMED** (as a kind — the non-local interaction) |
| Representative freedom (CNOT ≡ CZ ≡ iSWAP ≡ √SWAP) | **CONFIRMED** (LU-equivalent, same primitive) |
| Gate replaceable by an alternative (C) | **REFUTED** |
| Gate as NEW PRIMITIVE | **CONFIRMED** (NP_047/048) |

**Conclusion:** the Entangling Gate is **uniquely required as a KIND** — the non-local
entangling interaction — and is **not replaceable** by any alternative primitive. The
four alternatives (shared actualization, non-local phase, resonance coupling,
information coupling) are all classical or absent; only the gate reaches rank 2. Within
the primitive there is **representative freedom** (CNOT ≡ CZ ≡ iSWAP ≡ √SWAP under
local unitaries), but these are the same primitive, not alternatives. **Canonical D96
unchanged.**

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_049_Tests.cs`
**Run:** 2026-09-05 · **Result:** see `Tests/Results/Y_NP_049_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_049_PhenomenaRequiringGate` | all phenomena need creation | ✅ |
| `Y_NP_049_RemoveGateRetainJointStates` | static but unpreparable | ✅ |
| `Y_NP_049_AlternativeMechanismsFail` | 4 alternatives fail | ✅ |
| `Y_NP_049_GateRepresentativesEquivalent` | CNOT/CZ/iSWAP/√SWAP LU-equivalent | ✅ |
| `Y_NP_049_PrimitiveCost` | gate=1, alternatives=0 | ✅ |
| `Y_NP_049_Classification` | A confirmed, C refuted | ✅ |
| `Y_NP_049_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_049"`

---

## References

- ResearchY-NP_048 (gate irreducible), NP_047 (gate = creation primitive), NP_046
  (non-separability primitive), NP_045 (CHSH a fact), NP_044 (optional for derived
  chain), NP_043 (joint state irreducible), NP_042 (3-body), NP_041 (2-body), NP_040
  (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038 (entanglement ABSENT), QG_071
  (joint link state — NEW SECTOR; EntanglingSector.cs), QG_070 (entangling interaction
  missing; EntanglementFromLinks.cs), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ),
  D_036–D_040 (complex state; boundary set), M_001 (measurement reads both
  quadratures), R_001 (boundary set), S_001 (synthesis).
