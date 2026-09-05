# ResearchY-NP_048 — Entangling Gate Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_048 (permanent)
**Title:** Entangling Gate Origin Audit
**Status:** COMPLETE
**Date:** 2026-09-05
**File:** `NP_NewPhysics/ResearchY-NP_048.md`
**Depends on:** ResearchY-NP_047 (entangling gate = creation primitive),
ResearchY-NP_046 (non-separability primitive), NP_045 (CHSH a fact), NP_044 (optional
for derived chain), NP_043 (joint state irreducible), NP_042 (3-body / CZ gate),
NP_041 (2-body), NP_040 (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038
(entanglement ABSENT), QG_071 (joint link state — NEW SECTOR), QG_070 (entangling
interaction missing), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ), D_036–D_040 (complex state /
boundary set), M_001 (measurement reads both quadratures), R_001 (boundary set),
S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_048_Tests.cs`

---

## Purpose

> Is the entangling gate itself derivable, or is it an irreducible primitive?

**Known:** the Joint State is irreducible (NP_043); the entangling gate creates Joint
States (NP_047).

**Program steps:** (1) inventory all canonical operations — Difference, Actualization,
Occupancy, Information, Phase, D96 Resonance; (2) test whether any canonical operation
can generate rank 1 → rank 2; (3) attempt constructions from phase coupling, resonance
locking, occupancy exchange, information exchange; (4) count required new primitives;
(5) determine — A) gate DERIVED, B) gate EMERGENT, C) gate NEW PRIMITIVE.

**Classification:** DERIVED / EMERGENT / NEW PRIMITIVE / REFUTED.

---

## 1. Canonical operation inventory (all local or classical)

| Operation | Kind | Raises rank? |
|---|---|---|
| Difference (η) | real scalar | no |
| Actualization | diagonal (branching μ=2) | no |
| Occupancy | diagonal counts | no |
| Information | scalar / MI | no |
| Phase (θ) | single-DOF local unitary | no |
| D96 Resonance | real frequency content | no |

Verified (test `Y_NP_048_CanonicalOperationsLocalOrClassical`): the phase product is
rank 1; occupancy and information are diagonal-separable (MI > 0, concurrence 0). Every
canonical operation is **local** (single-DOF) or **classical** (diagonal).

---

## 2. No canonical operation reaches rank 2

Verified (tests `Y_NP_048_PhaseCouplingRankOne`, `Y_NP_048_ResonanceLockingAbsent`,
`Y_NP_048_OccupancyAndInformationExchangeSeparable`,
`Y_NP_048_NoCanonicalOperationReachesRank2`):

| Construction attempt | Result |
|---|---|
| phase coupling (shared phase / joint pinning, NP_004) | rank 1 |
| resonance locking (unequal modes) | ABSENT (NP_005/006/009/014) |
| equal-mode co-rotation | rank 1 (product) |
| occupancy exchange (NP_033) | separable (C = 0) |
| information exchange (MI) | separable (C = 0) |

Sweeping **every** canonical operation gives maximum Schmidt rank **1**. The key
mathematical fact: a local unitary U_A⊗U_B preserves Schmidt rank, and a classical
(diagonal) mixture is separable — so no LOCC (local operations + classical
communication) can raise rank. All canonical operations are LOCC.

---

## 3. The gate is the unique rank-raising operation

Verified (test `Y_NP_048_NoCanonicalOperationReachesRank2`): the entangling gate
(CNOT/CZ) is the **unique** operation that raises Schmidt rank 1 → 2, and it is
**non-local** — it is not expressible as U_A⊗U_B.

---

## 4. Primitive count

| Object | Added primitives |
|---|---|
| Joint state (NP_039/040) | 1 |
| Entangling gate (NP_047) | 1 |
| **Total for the entanglement sector** | **2** |

The gate is a **distinct** primitive from the joint state: the state is the static
non-separable object (NP_040); the gate is the dynamical rank-raising operation
(NP_047).

---

## 5. Legacy-lane reconciliation

- **NP_043:** the joint state is irreducible (no canonical object reaches rank 2).
  NP_048 shows the SAME for the gate: no canonical *operation* raises rank.
- **NP_047:** the gate is the creation primitive — NP_048 confirms it cannot be built
  from canonical operations.
- **QG70:** the "entangling interaction" is missing from θ + S — NP_048 formalizes why:
  θ and S are local/classical, so their compositions stay rank 1.
- **QG71 / NP_042:** the "entangling composition rule (CZ)" candidate is exactly this
  irreducible gate.

---

## Theorem

> **Theorem (NP_048).** The entangling gate is irreducible — it cannot be derived from
> or emergent from any canonical operation. Proof: (1) Inventory (verified): every
> canonical operation (Difference, Actualization, Occupancy, Information, Phase, D96
> Resonance) is local (single-DOF) or classical (diagonal). (2) Local/classical bound
> (verified): a local unitary U_A⊗U_B preserves Schmidt rank and a diagonal mixture is
> separable, so phase coupling (rank 1), occupancy exchange (C = 0), information
> exchange (C = 0), and resonance locking (absent, NP_005/006) all fail to raise rank.
> (3) Exhaustive sweep (verified): no canonical operation reaches Schmidt rank 2.
> (4) The gate (verified): CNOT/CZ is the unique rank-raising operation and is
> non-local. Hence the entangling gate is a NEW PRIMITIVE (C), not DERIVED (A) and not
> EMERGENT (B). Canonical D96 unchanged.
>
> *Proof sketch.* (1) inventory. (2) LOCC bound. (3) sweep rank 1. (4) gate non-local. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Phase coupling builds the gate" | phase is single-DOF; U_A⊗U_B preserves rank 1 |
| "Resonance locking builds the gate" | locking is ABSENT (NP_005/006) |
| "Occupancy exchange builds the gate" | diagonal ⇒ separable (C = 0) |
| "Information exchange builds the gate" | MI > 0 is classical (C = 0) |
| "The gate is a product of local operations" | CNOT/CZ is non-local (not U_A⊗U_B) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| The gate is irreducible | a canonical (local/classical) operation raising Schmidt rank 1 → 2 |
| The gate is non-local | an expression of CNOT/CZ as U_A⊗U_B |
| The gate is a distinct primitive | a derivation of the gate from the joint state alone |

---

## Classification

| Component | Status |
|---|---|
| Canonical operations (local/classical) | **DERIVED** (single-DOF / diagonal) |
| Gate DERIVED from canonical operations (A) | **REFUTED** |
| Gate EMERGENT from canonical operations (B) | **REFUTED** |
| Entangling gate as NEW PRIMITIVE (C) | **CONFIRMED** |

**Conclusion:** the entangling gate is **irreducible** — it is a **NEW PRIMITIVE**,
neither derivable nor emergent from any canonical operation. Every canonical operation
(Difference, Actualization, Occupancy, Information, Phase, D96 Resonance) is local or
classical and reaches Schmidt rank ≤ 1; the gate (CNOT/CZ) is the unique non-local
rank-raising operation and must be imported. This parallels NP_043 (the joint *state*
is irreducible): the entanglement sector needs **two** irreducible primitives — the
joint state and the entangling gate. **Canonical D96 unchanged.**

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_048_Tests.cs`
**Run:** 2026-09-05 · **Result:** see `Tests/Results/Y_NP_048_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_048_CanonicalOperationsLocalOrClassical` | all canonical ops local/classical | ✅ |
| `Y_NP_048_PhaseCouplingRankOne` | phase coupling rank 1 | ✅ |
| `Y_NP_048_ResonanceLockingAbsent` | locking absent | ✅ |
| `Y_NP_048_OccupancyAndInformationExchangeSeparable` | exchange separable | ✅ |
| `Y_NP_048_NoCanonicalOperationReachesRank2` | sweep max rank 1 | ✅ |
| `Y_NP_048_Classification` | gate NEW PRIMITIVE | ✅ |
| `Y_NP_048_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_048"`

---

## References

- ResearchY-NP_047 (entangling gate = creation primitive), NP_046 (non-separability
  primitive), NP_045 (CHSH a fact), NP_044 (optional for derived chain), NP_043 (joint
  state irreducible), NP_042 (3-body / CZ gate), NP_041 (2-body), NP_040 (rank-2
  matrix), NP_039 (1 NEW PRIMITIVE), NP_038 (entanglement ABSENT), QG_071 (joint link
  state — NEW SECTOR; EntanglingSector.cs), QG_070 (entangling interaction missing;
  EntanglementFromLinks.cs), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ), D_036–D_040 (complex
  state; boundary set), M_001 (measurement reads both quadratures), R_001 (boundary
  set), S_001 (synthesis).
