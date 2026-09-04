# ResearchY-NP_039 — Minimal Entanglement Sector Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_039 (permanent)
**Title:** Minimal Entanglement Sector Audit
**Status:** COMPLETE
**Date:** 2026-09-04
**File:** `NP_NewPhysics/ResearchY-NP_039.md`
**Depends on:** ResearchY-NP_038 (entanglement ABSENT — only correlation),
QG_070 (θ + S give interference + spinor DOF but no entangling interaction →
REQUIRES NEW SECTOR), QG_071 (minimal additional content = joint link state → NEW
SECTOR), QG_220 (θ = 2πk/N), QG_216 (|ψ| = √ρ), D_036–D_040 (complex state; boundary
set), M_001 (measurement reads both quadratures), R_001 (five-item boundary set),
NP_036 (tensor product is a hosted CONSTRUCTION), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_039_Tests.cs`

---

## Purpose

> What is the minimal extension required to obtain Bell-type entanglement?

**Program steps:** (1) keep canonical D96 unchanged; (2) search minimal additions —
A) complex phase sector, B) tensor-state sector, C) shared occupancy sector,
D) non-local information sector; (3) determine the first modification that allows
Schmidt rank > 1, concurrence > 0, CHSH > 2; (4) measure the added primitive count.

**Goal:** find the smallest entanglement-capable extension of AT.

**Success criterion:** the minimal added primitive count (candidates DERIVED / EMERGENT
/ BOUNDARY / NEW PRIMITIVE / REFUTED).

---

## 1. State model (recap, NP_038)

Canonical single-sector state over {|0⟩, |1⟩}: ψ_S = √ρ₀·e^{iθ₀}|0_S⟩ + √ρ₁·e^{iθ₁}|1_S⟩,
θ_k = 2πk/N, N = 96, shares ρ₀ = 1/3, ρ₁ = 2/3. The joint A×B coefficient matrix is
c_{ij} = a_i·b_j for a product. NP_038 verified: every canonical two-sector mechanism
(product, shared events, single-DOF interference) leaves the joint state at Schmidt
rank 1 (or, for shared events, a diagonal-separable mixture with MI > 0). Entanglement
requires Schmidt rank ≥ 2 (concurrence > 0, CHSH > 2) — never reached canonically.

---

## 2. The four candidate additions (canonical D96 unchanged)

| Candidate | Content | Entangles? | Added primitives |
|---|---|---|---|
| **A) complex phase sector** | single-DOF phase θ (ALREADY canonical, QG220) | no — interference only | 0 |
| **B) tensor-state sector** | the A×B product space | no — product states only (the host) | 0 |
| **C) shared occupancy sector** | shared events / joint phase pinning | no — classical correlation | 0 |
| **D) non-local information sector** | coherent joint amplitude c_ij (rank ≥ 2) | **yes — Bell pair** | **1** |

The witnesses are computed explicitly (test suite):

| Candidate | Schmidt rank | Concurrence | CHSH | MI |
|---|---|---|---|---|
| A (phase) | 1 | 0 | 2 | 0 |
| B (tensor product) | 1 | 0 | 2 | 0 |
| C (shared occupancy, p=1/3) | — (mixed, separable) | 0 | 2 | H(1/3) ≈ 0.918 bits |
| D (joint link state, Bell) | 2 | 1 | 2√2 ≈ 2.828 | 1 bit |

---

## 3. Which candidate is the first to entangle?

**Only D.** A complex phase is a single-degree-of-freedom amplitude (QG71 ATQG710):
it gives interference, never non-separability. The tensor product B is the *host* —
it supplies the 2-qubit space but, filled only with canonical content, holds nothing
but product states (rank 1). Shared occupancy C correlates (MI > 0) but stays
diagonal-separable (NP_038). Only a **coherent joint amplitude** — a non-factorizable
c_{ij} such as the Bell pair (|00⟩+|11⟩)/√2 — has Schmidt rank 2, concurrence 1,
CHSH = 2√2. This is precisely QG71's **joint link state**.

---

## 4. Added primitive count

| Candidate | Primitive status |
|---|---|
| A) complex phase sector | **DERIVED** — θ = 2πk/N is already canonical (QG220); not a new primitive |
| B) tensor-state sector | **DERIVED** — the A×B product is a formal construction (the same hosted-construction status as NP_036's D96⊗3); a host, not a primitive |
| C) shared occupancy sector | **DERIVED** — classical correlation from shared events (NP_038) |
| D) non-local information / joint link state | **NEW PRIMITIVE** — not derivable from θ + S (QG70/71) |

**Added primitive count = 1.** The smallest entanglement-capable extension of AT is
the tensor product B (0 new primitives, the host) plus the joint link state D
(1 new primitive, the content). Total: **one new primitive**.

---

## 5. Legacy-lane reconciliation

- **QG70 (REQUIRES NEW SECTOR):** shared link phases give classical correlations;
  θ provides single-DOF superposition and S provides spinor DOF (the prerequisites),
  but the entangling interaction is missing.
- **QG71 (NEW SECTOR):** the minimal additional link content is a JOINT LINK STATE —
  a joint two-DOF state (e.g. a Bell pair) compatible with the link, but new. Not
  derivable from θ or S.
- **NP_038:** confirmed no canonical object reaches Schmidt rank ≥ 2.

NP_039 does not change these verdicts — it *quantifies* them: the extension is exactly
**one new primitive** (the joint link state), and nothing short of it suffices.

---

## Theorem

> **Theorem (NP_039).** The smallest extension of canonical D96 that produces
> Bell-type entanglement is a single new primitive — the joint link state (a coherent
> two-sector amplitude c_{ij} with Schmidt rank ≥ 2) — hosted on the DERIVED A×B
> tensor product. Proof: (1) A complex phase sector (verified) is a single-DOF
> amplitude: sweeping canonical phase pairs gives Schmidt rank 1, concurrence 0,
> CHSH = 2. (2) The tensor-state sector (verified) supplies the 2-qubit space but,
> filled with canonical content, holds only product states (rank 1) — it is a host,
> not an entangler. (3) The shared occupancy sector (verified) gives MI = H(1/3) > 0
> but concurrence 0 and CHSH = 2 — a diagonal-separable classical correlation.
> (4) The non-local information / joint link state (verified) has Schmidt rank 2,
> concurrence 1, CHSH = 2√2 — the only candidate that entangles. (5) Primitive count
> (verified): A = 0 (θ already canonical), B = 0 (formal construction), C = 0
> (derived classical), D = 1 (joint link state, QG71). Hence the minimal added
> primitive count is 1, attained by D alone. Classification: A DERIVED (REFUTED as
> entangler); B DERIVED (host); C DERIVED (REFUTED as entangler); D NEW PRIMITIVE.
> Canonical D96 unchanged; QG70/71 confirmed.
>
> *Proof sketch.* (1) phase → rank 1. (2) tensor → rank 1 (host). (3) occupancy →
> separable. (4) joint link state → rank 2. (5) count primitives = 1. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Adding more complex phases entangles" | a phase is single-DOF; local phases are local unitaries — rank stays 1 |
| "The tensor product alone gives Bell states" | A×B holds only canonical content = product states (rank 1) |
| "Shared occupancy gives Bell states" | diagonal ⇒ separable; concurrence 0, CHSH = 2 |
| "Entanglement needs zero new primitives" | rank ≥ 2 requires a coherent joint amplitude not derivable from θ + S (QG70/71) |
| "Entanglement needs two or more new primitives" | one joint link state suffices (the Bell pair is already rank 2) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| One new primitive suffices | a rank-2 state requiring a *second* independent primitive beyond the joint link state |
| The joint link state is minimal | a canonical (θ + S only) construction with Schmidt rank ≥ 2 |
| The tensor product is a derived host | a rank-2 state that cannot be expressed in any A×B space |
| Shared occupancy cannot entangle | a diagonal classical mixture with concurrence > 0 or CHSH > 2 |

---

## Classification

| Component | Status |
|---|---|
| A) complex phase sector (θ = 2πk/N) | **DERIVED** (already canonical, QG220); **REFUTED** as an entangler |
| B) tensor-state sector (A×B space) | **DERIVED** (formal construction; the host); not an entangler |
| C) shared occupancy sector (shared events) | **DERIVED** (classical correlation, NP_038); **REFUTED** as an entangler |
| D) non-local information / joint link state | **NEW PRIMITIVE** (1 added; QG71 joint link state) |
| Entanglement from θ + S alone | **REFUTED** (QG70 — REQUIRES NEW SECTOR) |

**Conclusion:** the smallest entanglement-capable extension of AT is **one new
primitive** — the joint link state (a coherent two-sector amplitude, e.g. a Bell
pair) hosted on the DERIVED A×B tensor product. Added primitive count = 1. Nothing
short of it (phase, tensor product, shared occupancy) reaches Schmidt rank ≥ 2.
**Canonical D96 unchanged; QG70/71 confirmed.**

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_039_Tests.cs`
**Run:** 2026-09-04 · **Result:** see `Tests/Results/Y_NP_039_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_039_ComplexPhaseSectorSingleDof` | phase sector → rank 1, C=0, CHSH=2 | ✅ |
| `Y_NP_039_TensorStateSectorProductOnly` | A×B holds only product states | ✅ |
| `Y_NP_039_SharedOccupancyClassical` | shared occupancy MI>0 but separable | ✅ |
| `Y_NP_039_JointLinkStateEntangles` | joint link state rank 2, C=1, CHSH=2√2 | ✅ |
| `Y_NP_039_MinimalExtension` | only D entangles; added count = 1 | ✅ |
| `Y_NP_039_Classification` | DERIVED / NEW PRIMITIVE / REFUTED flags | ✅ |
| `Y_NP_039_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_039"`

---

## References

- ResearchY-NP_038 (entanglement ABSENT — only correlation), QG_070 (θ + S ⇒
  interference + spinor DOF, entangling interaction missing — REQUIRES NEW SECTOR;
  EntanglementFromLinks.cs), QG_071 (joint link state — NEW SECTOR; EntanglingSector.cs),
  QG_220 (θ = 2πk/N), QG_216 (|ψ| = √ρ), D_036–D_040 (complex state; irreducible
  boundary set), M_001 (measurement reads both quadratures), R_001 (five-item
  boundary set), NP_036 (tensor product = hosted CONSTRUCTION), S_001 (synthesis).
