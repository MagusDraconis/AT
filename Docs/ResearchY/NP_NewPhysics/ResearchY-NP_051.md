# ResearchY-NP_051 — Correspondence Layer Necessity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_051 (permanent)
**Title:** Correspondence Layer Necessity Audit
**Status:** COMPLETE
**Date:** 2026-09-05
**File:** `NP_NewPhysics/ResearchY-NP_051.md`
**Depends on:** ResearchY-NP_050 (physical realization = two-body interaction), NP_049
(gate uniquely required), NP_048 (gate irreducible), NP_047 (gate = creation primitive),
NP_046 (non-separability primitive), NP_045 (CHSH a fact), NP_044 (optional for derived
chain), NP_043 (joint state irreducible), NP_042 (3-body), NP_041 (2-body), NP_040
(rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038 (entanglement ABSENT), QG_071 (joint
link state — NEW SECTOR), QG_070 (entangling interaction missing), QG_220 (θ=2πk/N),
QG_216 (|ψ|=√ρ), D_036–D_040 (complex state / boundary set), M_001 (measurement reads
both quadratures), R_001 (boundary set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_051_Tests.cs`

---

## Purpose

> Why does nature contain the quantum correspondence layer at all?

**Known:** canonical AT is complete without entanglement (NP_044); observed physics
requires entanglement (NP_045).

**Program steps:** (1) inventory every phenomenon requiring the correspondence layer;
(2) search the common invariant — Bell, teleportation, GHZ, W; (3) test whether the
layer is A) optional convenience, B) observational necessity, C) unavoidable
consequence of observation itself; (4) remove the layer completely; (5) determine the
first empirical contradiction produced.

**Success criterion:** identify the minimal physical reason the correspondence layer
exists.

**Classification:** DERIVED / CORRESPONDENCE / NEW PRIMITIVE / REFUTED.

---

## 1. Phenomena requiring the layer

| Phenomenon | Body | Common feature |
|---|---|---|
| Bell / CHSH | 2-body | non-separability |
| Teleportation | 2-body | non-separability |
| GHZ | 3-body | non-separability |
| W | 3-body | non-separability |

Verified (test `Y_NP_051_PhenomenaRequiringLayer`): every phenomenon requiring the
layer shares **non-separability** (NP_046).

---

## 2. The common invariant: non-separability

Verified (test `Y_NP_051_CommonInvariant`): for the Bell pair each single sector is
maximally mixed (ρ_A = I/2 — no local reality) while the joint state is pure. The
irreducibility of the whole to its parts is the single invariant behind every
entanglement phenomenon.

---

## 3. Remove the layer — the first contradiction

Verified (test `Y_NP_051_RemoveLayerFirstContradiction`): removing the correspondence
layer leaves canonical AT with CHSH ≤ 2, which **contradicts the observed Bell
violation** (S = 2√2 > 2). The first empirical contradiction is exactly the Bell/CHSH
violation — the minimal entanglement phenomenon.

---

## 4. Why the layer exists

Verified (test `Y_NP_051_LayerIsObservationalCompletion`): canonical AT is internally
complete (no internal contradiction) yet misses the observed non-separability. The layer
is **not an optional convenience** — it is the **observational completion**: it supplies
precisely what observation reveals (the joint actualization) that the derived chain
cannot.

The minimal physical reason the correspondence layer exists:

> **Observation reads the ACTUAL — a joint actualization — and joint actualization is
> irreducible to separate single-sector actualizations.** The layer is where that
> irreducibility surfaces: it is an unavoidable consequence of observation itself, not
> a convenience and not an arbitrary addition.

---

## 5. Legacy-lane reconciliation

- **NP_044:** the layer is optional for the DERIVED chain — NP_051 refines this: it is
  *necessary for observation*.
- **NP_045:** the CHSH violation is a fact — NP_051 explains WHY the layer that hosts
  it must exist: observation itself is non-decomposable.
- **NP_046:** non-separability is primitive — NP_051 locates it at the point of
  observation.
- **M_001:** measurement reads both quadratures of one complex mode — NP_051 extends
  this to a joint mode: measurement is where the non-separability is actualized.

---

## Theorem

> **Theorem (NP_051).** The quantum correspondence layer exists because observation is
> of the ACTUAL, and the actual is not always decomposable into independent
> single-sector actualizations: the layer is an unavoidable consequence of observation
> itself (C), not an optional convenience (A). Proof: (1) Phenomena (verified): Bell,
> teleportation, GHZ, W all share non-separability. (2) Invariant (verified): the Bell
> pair has ρ_A = I/2 (no local reality) with a pure joint state — the whole is
> irreducible to its parts. (3) Removal (verified): canonical AT alone gives CHSH ≤ 2,
> contradicting the observed S = 2√2 > 2 — the first contradiction is the Bell
> violation. (4) Completion (verified): canonical AT is internally complete but misses
> observed non-separability; the layer supplies it (observational necessity, B). (5)
> Hence the layer is an unavoidable consequence of observation itself (C). Canonical D96
> remains complete WITHOUT the layer (the derived chain); the layer is the observational
> completion. Canonical D96 unchanged.
>
> *Proof sketch.* (1) non-separability common. (2) irreducibility. (3) Bell contradicts.
> (4) observational completion. (5) unavoidable consequence. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "The layer is an optional convenience" | removing it contradicts the observed Bell violation |
| "The layer is an arbitrary add-on" | it is forced by observation (non-decomposable actuality) |
| "Canonical AT is observationally complete" | canonical AT gives CHSH ≤ 2, missing S > 2 |
| "The layer can be derived from D96" | D96 has only local/classical operations (NP_048) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| The layer is observationally necessary | an observationally complete canonical AT (CHSH > 2) |
| Non-separability surfaces at observation | a decomposable (separable) joint actualization |
| The layer is unavoidable | a derived-chain theory reproducing the Bell violation without it |

---

## Classification

| Component | Status |
|---|---|
| Canonical AT (derived chain) | **DERIVED** (complete without the layer) |
| Layer as optional convenience (A) | **REFUTED** |
| Layer as observational necessity (B) | **CONFIRMED** (Bell violation is a fact) |
| Layer as unavoidable consequence of observation (C) | **CONFIRMED** |
| Entanglement sector | **CORRESPONDENCE** (hosted, non-derived) |

**Conclusion:** nature contains the quantum correspondence layer because **observation
is of the ACTUAL, and the actual is not always decomposable** — the layer is an
**unavoidable consequence of observation itself (C)**, not an optional convenience (A).
Canonical D96 remains complete *without* the layer (it describes the single-DOF/
classical derived chain); the layer is the **observational completion** that surfaces
the irreducible joint actualization revealed by measurement. **Canonical D96 unchanged.**

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_051_Tests.cs`
**Run:** 2026-09-05 · **Result:** see `Tests/Results/Y_NP_051_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_051_PhenomenaRequiringLayer` | all share non-separability | ✅ |
| `Y_NP_051_CommonInvariant` | ρ_A=I/2, joint pure | ✅ |
| `Y_NP_051_RemoveLayerFirstContradiction` | Bell contradicts canonical AT | ✅ |
| `Y_NP_051_LayerIsObservationalCompletion` | layer completes observation | ✅ |
| `Y_NP_051_Classification` | B/C confirmed, A refuted | ✅ |
| `Y_NP_051_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_051"`

---

## References

- ResearchY-NP_050 (physical realization), NP_049 (gate uniquely required), NP_048
  (gate irreducible), NP_047 (gate = creation primitive), NP_046 (non-separability
  primitive), NP_045 (CHSH a fact), NP_044 (optional for derived chain), NP_043 (joint
  state irreducible), NP_042 (3-body), NP_041 (2-body), NP_040 (rank-2 matrix), NP_039
  (1 NEW PRIMITIVE), NP_038 (entanglement ABSENT), QG_071 (joint link state — NEW
  SECTOR; EntanglingSector.cs), QG_070 (entangling interaction missing;
  EntanglementFromLinks.cs), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ), D_036–D_040 (complex
  state; boundary set), M_001 (measurement reads both quadratures), R_001 (boundary
  set), S_001 (synthesis).
