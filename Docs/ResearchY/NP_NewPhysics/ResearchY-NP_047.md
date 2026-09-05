# ResearchY-NP_047 — Joint State Dynamics Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_047 (permanent)
**Title:** Joint State Dynamics Audit
**Status:** COMPLETE
**Date:** 2026-09-05
**File:** `NP_NewPhysics/ResearchY-NP_047.md`
**Depends on:** ResearchY-NP_046 (non-separability primitive), ResearchY-NP_045 (CHSH
a fact), NP_044 (optional for derived chain), NP_043 (irreducible), NP_042 (3-body),
NP_041 (2-body), NP_040 (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038
(entanglement ABSENT), QG_071 (joint link state — NEW SECTOR), QG_070 (entangling
interaction missing), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ), D_036–D_040 (complex state /
boundary set), M_001 (measurement reads both quadratures), R_001 (boundary set),
S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_047_Tests.cs`

---

## Purpose

> How are Joint States created, transformed, and destroyed?

**Known:** Joint States exist; non-separability is primitive (NP_046).

**Program steps:** (1) creation rules — Product → Joint; (2) stability rules — Joint
→ Joint; (3) destruction rules — Joint → Product; (4) conservation tests — rank,
concurrence, entropy; (5) multipartite extension — Bell → GHZ, GHZ → W.

**Determine:** A) static ontology only, B) full dynamics exists, C) additional
primitive required.

**Success criterion:** identify the minimal dynamical law for Joint States.

**Classification:** DERIVED / EMERGENT / CORRESPONDENCE / NEW PRIMITIVE / REFUTED.

---

## 1. The three dynamical rules

| Rule | Transition | Mechanism | Primitive |
|---|---|---|---|
| **Creation** | Product → Joint | entangling gate (CNOT/CZ) | **NEW** |
| **Stability** | Joint → Joint | local unitaries U_A⊗U_B | canonical (phase update) |
| **Destruction** | Joint → Product | local measurement (M_001) | canonical |

---

## 2. Creation (Product → Joint)

Verified (tests `Y_NP_047_EntanglingGateCreates`, `Y_NP_047_LocalUnitaryCannotCreate`):

- **CNOT** |+⟩|0⟩ = (|00⟩+|11⟩)/√2 (the Bell pair, rank 2, concurrence 1).
- **CZ** |+⟩|+⟩ = (|00⟩+|01⟩+|10⟩−|11⟩)/2 (rank 2, LU-equivalent to Bell).
- **Local unitaries CANNOT create entanglement**: U_A⊗U_B preserves Schmidt rank, so a
  product state (rank 1) stays a product under any local operation.

Creation requires a **non-local entangling gate** — a NEW primitive beyond the static
joint state.

---

## 3. Stability (Joint → Joint)

Verified (test `Y_NP_047_LocalUnitaryPreservesEntanglement`): a local unitary U_A⊗V_B
preserves Schmidt rank, concurrence, and entanglement entropy — the singular values of
the coefficient matrix are invariant. The canonical per-sector phase update
θ(t+1) = θ(t) + Δθ **is** U_A⊗U_B (NP_038/041), so a joint state is **stable under
canonical local evolution** — no new primitive needed.

---

## 4. Destruction (Joint → Product)

Verified (test `Y_NP_047_MeasurementDestroys`): local measurement (M_001 reads one
quadrature) collapses a joint state to a product state (rank 1, entanglement entropy 0).
Destruction is canonical — measurement is already in AT (M_001).

---

## 5. Conservation tests

Verified (test `Y_NP_047_Conservation`):

| Quantity | Local unitary | Entangling gate | Measurement |
|---|---|---|---|
| Schmidt rank | conserved | increased (1→2) | destroyed (2→1) |
| concurrence | conserved | increased (0→1) | destroyed (→0) |
| entropy | conserved | increased | destroyed (→0) |

Entanglement is **not a conserved quantity** under arbitrary operations — it is
conserved by local unitaries, created by the entangling gate, and destroyed by
measurement.

---

## 6. Multipartite extension

Verified (test `Y_NP_047_MultipartiteExtension`):

- **Bell → GHZ**: entangle a third |0⟩ qubit via CNOT₂₃: (|00⟩+|11⟩)/√2 ⊗ |0⟩ →
  (|000⟩+|111⟩)/√2 = GHZ (τ₃ = 1). Achievable.
- **GHZ → W**: REFUTED. GHZ (τ₃ = 1) and W (τ₃ = 0) are distinct SLOCC classes; no CZ
  composition reaches W (NP_042 — graph states have equal-magnitude amplitudes, W has a
  zero). The minimal gate dynamics covers the GHZ/cluster family only.

---

## 7. Legacy-lane reconciliation

- **NP_046:** non-separability is primitive (static). NP_047 adds the dynamical layer:
  the entangling gate is a second primitive (creation).
- **QG70:** the "entangling interaction" is missing from θ + S — NP_047 identifies it
  concretely as the entangling gate (CNOT/CZ).
- **QG71:** the joint link state is a NEW SECTOR (static) — NP_047 shows dynamics needs
  the gate on top of the state.
- **NP_042:** the "entangling composition rule (CZ)" candidate is exactly the
  creation primitive NP_047 now formalizes.

---

## Theorem

> **Theorem (NP_047).** The minimal dynamical law for joint states is: (1) creation by
> an entangling gate (CNOT/CZ), a NEW primitive; (2) stability by local unitaries
> U_A⊗U_B (canonical — the per-sector phase update); (3) destruction by local
> measurement (canonical — M_001). Proof: (1) Creation (verified): CNOT |+⟩|0⟩ = Bell
> (rank 2) and CZ |+⟩|+⟩ gives a rank-2 cluster state; local unitaries preserve rank 1
> (U_A⊗U_B cannot create entanglement). (2) Stability (verified): a local unitary
> preserves Schmidt rank, concurrence, and entanglement entropy (singular values
> invariant). (3) Destruction (verified): measurement collapses to a product (rank 1,
> S = 0). (4) Conservation (verified): entanglement is conserved by local unitaries,
> created by the gate, destroyed by measurement. (5) Multipartite (verified): Bell →
> GHZ via CNOT + a third |0⟩ qubit; GHZ → W REFUTED (distinct SLOCC class). Hence full
> dynamics exists (not static ontology only) with exactly ONE added primitive — the
> entangling gate. Canonical D96 unchanged.
>
> *Proof sketch.* (1) gate creates. (2) local unitary stabilizes. (3) measurement
> destroys. (4) conservation profile. (5) multipartite reach. ∎

---

## 8. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Local unitaries can create entanglement" | U_A⊗U_B preserves Schmidt rank (product stays product) |
| "Joint states are static (no dynamics)" | creation/stability/destruction are all realized |
| "The gate is not needed" | local unitaries cannot reach rank 2; only the gate does |
| "GHZ → W is achievable" | distinct SLOCC classes; no CZ reaches W (NP_042) |

---

## 9. Falsification paths

| Claim | Falsification |
|---|---|
| The entangling gate is the minimal creation primitive | a local (U_A⊗U_B) operation turning a product into rank 2 |
| Local unitaries conserve entanglement | a U_A⊗U_B changing the Schmidt rank of a joint state |
| GHZ → W is impossible | a LOCC/CZ sequence mapping GHZ to W |

---

## Classification

| Component | Status |
|---|---|
| Creation (Product → Joint) | **NEW PRIMITIVE** (entangling gate, CNOT/CZ) |
| Stability (Joint → Joint) | **DERIVED** (local unitaries; canonical phase update) |
| Destruction (Joint → Product) | **DERIVED** (local measurement, M_001) |
| Full dynamics exists (B) | **CONFIRMED** |
| Static ontology only (A) | **REFUTED** |
| Additional primitive required (C) | **CONFIRMED** (1 — the entangling gate) |

**Conclusion:** Joint States are not static — a full dynamics exists (creation,
stability, destruction). The minimal dynamical law is: **create** by an entangling
gate (CNOT/CZ, 1 NEW PRIMITIVE), **stabilize** by local unitaries (canonical phase
update), **destroy** by local measurement (canonical M_001). Added primitive count for
dynamics = 1. **Canonical D96 unchanged.**

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_047_Tests.cs`
**Run:** 2026-09-05 · **Result:** see `Tests/Results/Y_NP_047_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_047_LocalUnitaryPreservesEntanglement` | rank/C/S conserved | ✅ |
| `Y_NP_047_LocalUnitaryCannotCreate` | local unitaries keep rank 1 | ✅ |
| `Y_NP_047_EntanglingGateCreates` | CNOT/CZ → rank 2 | ✅ |
| `Y_NP_047_MeasurementDestroys` | measurement → rank 1 | ✅ |
| `Y_NP_047_Conservation` | conservation profile | ✅ |
| `Y_NP_047_MultipartiteExtension` | Bell→GHZ; GHZ→W refuted | ✅ |
| `Y_NP_047_Classification` | C confirmed, A refuted | ✅ |
| `Y_NP_047_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_047"`

---

## References

- ResearchY-NP_046 (non-separability primitive), NP_045 (CHSH a fact), NP_044
  (optional for derived chain), NP_043 (irreducible), NP_042 (3-body / CZ gate),
  NP_041 (2-body), NP_040 (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038
  (entanglement ABSENT), QG_071 (joint link state — NEW SECTOR; EntanglingSector.cs),
  QG_070 (entangling interaction missing; EntanglementFromLinks.cs), QG_220 (θ=2πk/N),
  QG_216 (|ψ|=√ρ), D_036–D_040 (complex state; boundary set), M_001 (measurement reads
  both quadratures), R_001 (boundary set), S_001 (synthesis).
