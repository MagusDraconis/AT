# ResearchY-NP_044 — Joint State Necessity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_044 (permanent)
**Title:** Joint State Necessity Audit
**Status:** COMPLETE
**Date:** 2026-09-04
**File:** `NP_NewPhysics/ResearchY-NP_044.md`
**Depends on:** ResearchY-NP_043 (joint states irreducible primitives),
ResearchY-NP_042 (3-body joint state), ResearchY-NP_041 (2-body sector), ResearchY-NP_040
(rank-2 matrix), ResearchY-NP_039 (1 NEW PRIMITIVE), ResearchY-NP_038 (entanglement
ABSENT), QG_071 (joint link state — NEW SECTOR), QG_070 (entangling interaction
missing), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ), D_036–D_040 (complex state / boundary
set), M_001 (measurement reads both quadratures), R_001 (boundary set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_044_Tests.cs`

---

## Purpose

> Does any observed phenomenon force the introduction of Joint States, or can all
> currently derived AT results exist without them?

**Goal:** determine whether Joint States are A) necessary physics, B) optional
extension, C) correspondence layer.

**Success criterion:** identify the first empirical result that cannot be reproduced
without the Joint State primitives.

**Classification:** DERIVED / EMERGENT / NEW PRIMITIVE / REFUTED.

---

## 1. Existing derived AT results — no joint state needed

The established AT derivation chain is single-DOF and classical throughout. Verified
(test `Y_NP_044_ExistingResultsWithoutJointStates`):

| Derived result | Form | Joint state needed? |
|---|---|---|
| D96 spectrum | 95 real frequencies ω_k | no |
| A = Σm·#g·occ₂ | 95·44·87 = 363,660 (scalar counts) | no |
| M_Pl = v·A³ | 254.37 · (363,660)³ = 1.2234e19 GeV | no |
| mass ratios | anchor × dimensionless D96 ratio | no |
| couplings, ΩΛ | scalar ratios | no |

The canonical state used throughout is ψ = √ρ e^{iθ} — a rank-1 single-DOF amplitude.
Every derived observable is a scalar or spectral product. **No currently derived AT
result requires a joint state.**

---

## 2. Canonical CHSH bound

Verified (test `Y_NP_044_CanonicalNeverViolatesChsh`): sweeping canonical products
(sector indices × shares) gives maximum CHSH = 2 — canonical D96 never violates the
Bell/CHSH bound (consistent with NP_038).

---

## 3. The first non-reproducible empirical result

Verified (tests `Y_NP_044_BellViolationRequiresJointState`, `Y_NP_044_FirstEmpiricalResult`):
the Bell/CHSH inequality violation S = 2√2 > 2 is reproduced **only** by a rank-2 joint
state (the Bell pair). No canonical object reaches it (NP_038/043). This is the
**first empirical result that cannot be reproduced without the joint-state primitives**
— it is the minimal entanglement phenomenon, and it precedes teleportation and GHZ in
the empirical hierarchy.

---

## 4. What follows Bell

Verified (test `Y_NP_044_TeleportationGhzAlsoRequire`): teleportation (F = 1, needs a
Bell pair) and GHZ (τ₃ = 1, needs a 3-body joint state) also require joint states, but
they come after the Bell violation.

---

## 5. Legacy-lane reconciliation

- **NP_043:** joint states are irreducible primitives — NP_044 shows they are also
  *optional*: nothing in the established chain forces them.
- **QG70/71:** the entangling interaction / joint link state is a NEW SECTOR — NP_044
  shows it is a hosted sector, not a derived necessity.
- **NP_035/036 precedent:** the ω² blackbody DOS was likewise a hosted CORRESPONDENCE
  layer, not an emergent D96 output — the joint-state sector is exactly parallel.

---

## Theorem

> **Theorem (NP_044).** The joint states are an OPTIONAL extension of AT, not a
> necessity: every currently derived AT result exists without them, and the first
> empirical result that forces them is the Bell/CHSH violation (S > 2). Proof:
> (1) Existing results (verified): the canonical state is rank 1 and all derived
> observables (spectrum, A = 95·44·87, M_Pl = v·A³ = 1.2234e19 GeV) are scalar /
> spectral products — no joint state appears. (2) Canonical bound (verified): sweeping
> canonical products gives max CHSH = 2 (no violation). (3) First forced result
> (verified): the Bell pair has CHSH = 2√2 > 2, reproduced only by a rank-2 joint
> state; no canonical object reaches it. (4) Later results (verified): teleportation
> (F = 1) and GHZ (τ₃ = 1) also require joint states, after Bell. Hence the joint
> states are (B) an OPTIONAL extension for the current chain, functioning as (C) a
> CORRESPONDENCE layer hosting observed entanglement, and (A) NECESSARY only once
> entanglement phenomenology is claimed. Canonical D96 unchanged.
>
> *Proof sketch.* (1) derived results joint-free. (2) canonical CHSH ≤ 2. (3) Bell
> forces S > 2. (4) teleportation/GHZ follow. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "The Planck scale needs joint states" | M_Pl = v·A³ is a scalar product of spectral counts |
| "Canonical AT violates Bell" | canonical products give max CHSH = 2 (NP_038) |
| "Bell violation is the only thing joint states add" | teleportation and GHZ/W also require them |
| "Joint states are necessary for current AT" | every derived result is single-DOF / classical |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| Joint states are optional | a currently derived AT result that fails without a rank-2 joint state |
| Bell violation is the first forced result | a canonical (rank-1) state with CHSH > 2 |
| Joint states are a correspondence layer | a derivation of entanglement from canonical D96 (NP_038 refutation) |

---

## Classification

| Component | Status |
|---|---|
| Existing derived AT results (spectrum, masses, Planck, couplings, ΩΛ) | **DERIVED** (single-DOF / classical / scalar) |
| Joint states as necessary physics (A) for current AT | **REFUTED** (no derived result needs them) |
| Joint states as optional extension (B) | **CONFIRMED** (the minimal entanglement-capable extension) |
| Joint states as correspondence layer (C) | **CONFIRMED** (hosts observed entanglement; parallel to ω² DOS, NP_035/036) |
| Bell/CHSH violation S > 2 | **CORRESPONDENCE** (first empirical result forcing a joint state) |

**Conclusion:** the joint states are an **optional extension (B)** of AT that currently
functions as a **correspondence layer (C)** hosting observed entanglement — they are
**not necessary physics (A)** for any already-derived result. The first empirical
result that cannot be reproduced without them is the **Bell/CHSH inequality violation
(S = 2√2 > 2)**. **Canonical D96 unchanged.**

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_044_Tests.cs`
**Run:** 2026-09-04 · **Result:** see `Tests/Results/Y_NP_044_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_044_ExistingResultsWithoutJointStates` | derived results joint-free | ✅ |
| `Y_NP_044_CanonicalNeverViolatesChsh` | canonical CHSH ≤ 2 | ✅ |
| `Y_NP_044_BellViolationRequiresJointState` | Bell CHSH = 2√2 needs rank 2 | ✅ |
| `Y_NP_044_FirstEmpiricalResult` | first forced result = CHSH > 2 | ✅ |
| `Y_NP_044_TeleportationGhzAlsoRequire` | teleportation/GHZ follow Bell | ✅ |
| `Y_NP_044_Classification` | B/C optional/correspondence | ✅ |
| `Y_NP_044_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_044"`

---

## References

- ResearchY-NP_043 (joint states irreducible), NP_042 (3-body), NP_041 (2-body),
  NP_040 (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038 (entanglement ABSENT),
  QG_071 (joint link state — NEW SECTOR; EntanglingSector.cs), QG_070 (entangling
  interaction missing; EntanglementFromLinks.cs), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ),
  D_036–D_040 (complex state; boundary set), M_001 (measurement reads both
  quadratures), R_001 (boundary set), S_001 (synthesis).
