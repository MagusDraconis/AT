# ResearchY-NP_043 — Joint State Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_043 (permanent)
**Title:** Joint State Origin Audit
**Status:** COMPLETE
**Date:** 2026-09-04
**File:** `NP_NewPhysics/ResearchY-NP_043.md`
**Depends on:** ResearchY-NP_042 (3-body joint state = minimal multipartite extension),
ResearchY-NP_041 (2-body link: Bell/CHSH/teleportation DERIVED), ResearchY-NP_040
(joint link state = rank-2 2×2 matrix), ResearchY-NP_039 (joint link state = 1 NEW
PRIMITIVE), ResearchY-NP_038 (entanglement ABSENT — only correlation), QG_071 (joint
link state — NEW SECTOR), QG_070 (entangling interaction missing), QG_220 (θ=2πk/N),
QG_216 (|ψ|=√ρ), D_036–D_040 (complex state; boundary set), M_001 (measurement reads
both quadratures), R_001 (boundary set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_043_Tests.cs`

---

## Purpose

> Can Joint States be derived from existing canonical objects, or are they
> irreducible primitives?

**Program steps:** (1) inventory canonical objects — Difference, Actualization,
Occupancy, Information, D96 spectrum, Phase; (2) attempt a derivation of the 2-body
joint state; (3) attempt a derivation of the 3-body joint state; (4) measure the
primitive count; (5) determine — A) derivable, B) emergent, C) irreducible primitive.

**Success criterion:** find the earliest point in the AT chain where
entanglement-capable state space first appears.

**Classification:** DERIVED / EMERGENT / NEW PRIMITIVE / REFUTED.

---

## 1. Canonical inventory (none is entanglement-capable)

| Object | Kind | Entanglement capability |
|---|---|---|
| Difference (η) | real scalar | none — no amplitude |
| Actualization | branching μ=2 → diagonal occupancy | classical (separable) |
| Occupancy | diagonal counts [4,4,87] | classical (separable) |
| Information | I_occ = KL(ρ‖uniform), MI | scalar / classical correlation |
| D96 spectrum | 95 real frequencies ω_k | none — no joint state |
| Phase (θ = 2πk/N) | single-DOF complex amplitude ψ = √ρ e^{iθ} | interference only (rank 1) |

Verified (test `Y_NP_043_CanonicalInventorySingleDofOrClassical`): the phase product
is rank 1 (concurrence 0); the occupancy mixture is separable (concurrence 0) yet has
MI > 0 (classical correlation); Difference and the spectrum are real scalars carrying
no complex amplitude. **No canonical object is a coherent multi-DOF amplitude.**

---

## 2. 2-body joint state — derivation attempt FAILS

Sweeping every canonical two-sector mechanism — phase products, shared occupancy,
single-DOF interference — the maximum reachable Schmidt rank is 1 (test
`Y_NP_043_TwoBodyJointStateIrreducible`). The 2-body joint state (Bell, rank 2) is
**not** in the reachable set. The 2-body joint state is therefore **IRREDUCIBLE**.

---

## 3. 3-body joint state — derivation attempt FAILS

Canonical objects plus 2-body links produce only biseparable states (τ₃ = 0, test
`Y_NP_043_ThreeBodyJointStateIrreducible`). GHZ (τ₃ = 1) and W (genuinely tripartite,
pairwise C = 2/3) are not reachable. The 3-body joint state is **IRREDUCIBLE**
(consistent with NP_042).

---

## 4. Primitive count

| Object | Joint states | Added primitives |
|---|---|---|
| Canonical inventory | 0 | 0 |
| 2-body joint link state | Bell | 1 (NP_039) |
| 3-body joint state | GHZ, W | 1 (NP_042) |
| **Total** | | **2** |

---

## 5. Earliest entanglement-capable state space

The earliest point where entanglement-capable (rank-2) state space appears is the
**2-body joint link state** (NP_039). Canonical D96 reaches only Schmidt rank 1
(correlation only, NP_038) — no canonical object precedes the joint link state with
rank ≥ 2 (test `Y_NP_043_EarliestAppearance`).

---

## 6. Legacy-lane reconciliation

- **QG71:** the joint link state is a NEW SECTOR — NP_043 confirms it is not derivable
  from θ, S, or any canonical object.
- **QG70:** the entangling interaction is missing — NP_043 confirms no canonical
  object supplies it (Difference, Actualization, Occupancy, Information, spectrum,
  Phase are all single-DOF / classical / scalar).
- **NP_038–042:** unchanged — the joint states are NEW primitives, canonical AT
  unchanged.

---

## Theorem

> **Theorem (NP_043).** The joint states (2-body and 3-body) are irreducible
> primitives — they are not DERIVED and not EMERGENT from any canonical object.
> Proof: (1) Inventory (verified): Difference (scalar), Actualization (diagonal),
> Occupancy (diagonal), Information (scalar/classical), D96 spectrum (real), Phase
> (single-DOF) — none is a coherent multi-DOF amplitude. (2) 2-body derivation
> (verified): sweeping phase products, shared occupancy, and interference reaches
> Schmidt rank ≤ 1; the Bell state (rank 2) is unreachable. (3) 3-body derivation
> (verified): canonical objects + 2-body links give biseparable states (τ₃ = 0);
> GHZ (τ₃ = 1) and W (pairwise C = 2/3) are unreachable. (4) Primitive count
> (verified): canonical = 0 joint states; 2-body = 1; 3-body = 1; total = 2.
> (5) Earliest appearance (verified): the 2-body joint link state (NP_039) is the
> first rank-2 space; no canonical object precedes it. Classification: canonical
> objects DERIVED (single-DOF/classical/scalar); 2-body joint state NEW PRIMITIVE;
> 3-body joint state NEW PRIMITIVE; deriving/emerging joint states from canonical AT
> REFUTED. Canonical D96 unchanged.
>
> *Proof sketch.* (1) inventory. (2) 2-body fail. (3) 3-body fail. (4) count. (5)
> earliest. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Phase derives a joint state" | phase is single-DOF → interference, rank 1 |
| "Occupancy derives entanglement" | diagonal → separable (classical) |
| "Information derives entanglement" | MI > 0 is classical correlation, concurrence 0 |
| "The spectrum derives a joint state" | 95 real frequencies carry no amplitude |
| "Joint states emerge from canonical AT" | no canonical object reaches rank ≥ 2 (NP_038) |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| Joint states are irreducible | a canonical derivation (Difference→Phase only) reaching Schmidt rank ≥ 2 |
| 2-body joint state is a primitive | a Bell state built from θ + S alone |
| 3-body joint state is a primitive | a GHZ/W built from canonical objects + 2-body links |
| Earliest appearance = joint link state | a canonical object with rank ≥ 2 preceding NP_039 |

---

## Classification

| Component | Status |
|---|---|
| Difference / Actualization / Occupancy / Information / spectrum / Phase | **DERIVED** (single-DOF, classical, or scalar) |
| 2-body joint state | **NEW PRIMITIVE** (irreducible) |
| 3-body joint state | **NEW PRIMITIVE** (irreducible) |
| Joint states DERIVED from canonical objects | **REFUTED** |
| Joint states EMERGENT from canonical AT | **REFUTED** |

**Conclusion:** the joint states (2-body and 3-body) are **irreducible primitives**
(NEW PRIMITIVE), not derivable and not emergent from any canonical object. The
canonical inventory — Difference, Actualization, Occupancy, Information, D96 spectrum,
Phase — is entirely single-DOF, classical, or scalar, and reaches Schmidt rank ≤ 1.
The earliest entanglement-capable state space is the 2-body joint link state (NP_039),
with 2 added primitives total (2-body + 3-body). **Canonical D96 unchanged.**

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_043_Tests.cs`
**Run:** 2026-09-04 · **Result:** see `Tests/Results/Y_NP_043_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_043_CanonicalInventorySingleDofOrClassical` | canonical objects single-DOF/classical/scalar | ✅ |
| `Y_NP_043_TwoBodyJointStateIrreducible` | 2-body derivation fails (rank ≤ 1) | ✅ |
| `Y_NP_043_ThreeBodyJointStateIrreducible` | 3-body derivation fails (biseparable) | ✅ |
| `Y_NP_043_PrimitiveCount` | 0 canonical + 1 + 1 = 2 primitives | ✅ |
| `Y_NP_043_EarliestAppearance` | joint link state = first rank-2 space | ✅ |
| `Y_NP_043_Classification` | DERIVED / NEW PRIMITIVE / REFUTED | ✅ |
| `Y_NP_043_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_043"`

---

## References

- ResearchY-NP_042 (3-body joint state), NP_041 (2-body link), NP_040 (rank-2 matrix),
  NP_039 (1 NEW PRIMITIVE), NP_038 (entanglement ABSENT), QG_071 (joint link state —
  NEW SECTOR; EntanglingSector.cs), QG_070 (entangling interaction missing;
  EntanglementFromLinks.cs), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ), D_036–D_040 (complex
  state; boundary set), M_001 (measurement reads both quadratures), R_001 (boundary
  set), S_001 (synthesis).
