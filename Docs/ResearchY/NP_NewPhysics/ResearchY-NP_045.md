# ResearchY-NP_045 — CHSH Reality Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_045 (permanent)
**Title:** CHSH Reality Audit
**Status:** COMPLETE
**Date:** 2026-09-04
**File:** `NP_NewPhysics/ResearchY-NP_045.md`
**Depends on:** ResearchY-NP_044 (joint states optional for the derived chain),
ResearchY-NP_043 (joint states irreducible), NP_042 (3-body), NP_041 (2-body),
NP_040 (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038 (entanglement ABSENT),
QG_071 (joint link state — NEW SECTOR), QG_070 (entangling interaction missing),
QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ), D_036–D_040 (complex state / boundary set),
M_001 (measurement reads both quadratures), R_001 (boundary set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_045_Tests.cs`

---

## Purpose

> Must AT accept CHSH violations as fundamental physics?

**Program steps:** (1) inventory every place Bell/CHSH evidence enters physics; (2)
evaluate the evidence classes — Bell 1964 logic, Aspect, Zeilinger, loophole-free
tests; (3) determine — A) CHSH > 2 is a physical fact requiring Joint States, B) CHSH
> 2 is correspondence-only, C) CHSH > 2 can be reproduced without Joint States; (4)
test consistency (canonical AT vs AT + joint-state sector); (5) count observations
explained / missed.

**Classification:** DERIVED / EMERGENT / CORRESPONDENCE / NEW PRIMITIVE / REFUTED.

---

## 1. Evidence inventory

| Evidence class | Year | Content | Status |
|---|---|---|---|
| Bell 1964 | 1964 | local realism ⇒ \|S\| ≤ 2 (theorem) | logical fact |
| CHSH 1969 | 1969 | operational inequality S ≤ 2 | logical fact |
| Aspect | 1982 | first experimental violation S ≈ 2.7 > 2 | observed fact |
| Zeilinger | 1997+ | teleportation + GHZ experiments | observed fact |
| Loophole-free | 2015 | S > 2 with detection+locality closed (Hensen/Giustina/Shalm) | observed fact |

The Bell/CHSH violation is a **robust, loophole-free empirical fact**, confirmed across
four independent evidence classes. The quantum (joint-state) prediction is S = 2√2 > 2.

---

## 2. Canonical AT vs the joint-state sector

Verified (tests `Y_NP_045_CanonicalCannotReproduce`, `Y_NP_045_JointStateReproduces`):

| Sector | CHSH | Teleportation | GHZ |
|---|---|---|---|
| Canonical AT | ≤ 2 (misses the fact) | — | — |
| Joint-state sector | 2√2 (reproduces) | F = 1 | τ₃ = 1 |

Canonical AT reaches at most CHSH = 2; only the rank-2 joint state reaches 2√2.

---

## 3. The three options

- **A) CHSH > 2 is a physical fact requiring Joint States** — CONFIRMED. The
  loophole-free violation is an empirical fact; canonical AT cannot reproduce it.
- **C) CHSH > 2 reproducible without Joint States** — REFUTED. No canonical object
  (single-DOF or classical) reaches S > 2 (NP_038/043).
- **B) correspondence-only** — refined: the joint-state sector ENTERS as a
  correspondence layer (hosted, non-derived), but the fact itself is real.

---

## 4. Consistency

Both sectors are internally consistent (test `Y_NP_045_Consistency`): canonical AT is
self-consistent (CHSH ≤ 2, no violation), and AT + joint-state sector is self-consistent
AND reproduces the observed fact (CHSH = 2√2). Adding the joint-state sector introduces
no contradiction.

---

## 5. Observations explained / missed

Verified (test `Y_NP_045_ObservationsExplainedMissed`):

| Sector | Explained | Missed |
|---|---|---|
| Canonical AT | 0 / 4 | Bell, teleportation, GHZ, W |
| Joint-state sector | 4 / 4 | none |

---

## 6. Legacy-lane reconciliation

- **NP_044:** joint states are optional for the DERIVED chain. NP_045 sharpens this:
  they are REQUIRED for the OBSERVED Bell violation.
- **QG70/71:** the entangling interaction / joint link state is a NEW SECTOR — NP_045
  shows this sector is empirically forced (not an arbitrary add-on).
- **NP_038/043:** canonical AT reaches only rank 1 / CHSH ≤ 2 — the source of the
  mismatch with observation.

---

## Theorem

> **Theorem (NP_045).** AT must accept the CHSH violation as fundamental physics: the
> Bell/CHSH violation (S > 2) is a robust, loophole-free empirical fact that canonical
> AT (CHSH ≤ 2) cannot reproduce, so the joint-state sector is REQUIRED PHYSICS for a
> complete theory of observed entanglement — entering as a CORRESPONDENCE layer
> (hosted, non-derived). Proof: (1) Evidence (verified): Bell 1964 (local realism ⇒
> \|S\| ≤ 2), CHSH 1969 (S ≤ 2), Aspect 1982 (S ≈ 2.7 > 2), Zeilinger (teleportation +
> GHZ), loophole-free 2015 (S > 2 closed) — four independent classes confirm the fact.
> (2) Canonical AT (verified): sweeping canonical products gives max CHSH = 2 — it
> misses the fact. (3) Joint-state sector (verified): the Bell pair gives CHSH = 2√2,
> teleportation F = 1, GHZ τ₃ = 1 — it reproduces the full hierarchy. (4) Option C
> (verified): no canonical object reaches S > 2 — reproducing without joint states is
> REFUTED. (5) Count (verified): canonical AT explains 0/4 observations, the joint-state
> sector 4/4. Hence Joint States = REQUIRED PHYSICS (for observed entanglement), hosted
> as a CORRESPONDENCE layer; refinement of NP_044 (optional for the derived chain,
> required for the observed violation). Canonical D96 unchanged.
>
> *Proof sketch.* (1) evidence robust. (2) canonical ≤ 2. (3) joint-state 2√2. (4) C
> refuted. (5) 4/4 explained. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "CHSH > 2 is correspondence-only (not real)" | it is a loophole-free, repeatedly confirmed empirical fact |
| "Canonical AT reproduces the violation" | canonical max CHSH = 2 (NP_038) |
| "The violation needs no joint state" | no single-DOF/classical object reaches S > 2 |
| "Joint states are purely optional" | a complete theory of observed physics cannot omit the Bell violation |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| CHSH > 2 is a physical fact | a loophole-free experiment consistent with S ≤ 2 |
| Canonical AT cannot reproduce it | a canonical (rank-1) state with CHSH > 2 |
| Joint states are required | an AT sector (without joint states) reproducing S = 2√2 |

---

## Classification

| Component | Status |
|---|---|
| CHSH > 2 as a physical fact (A) | **CONFIRMED** (loophole-free, four evidence classes) |
| Reproducing CHSH > 2 without joint states (C) | **REFUTED** (canonical max CHSH = 2) |
| Joint states as REQUIRED PHYSICS | **CONFIRMED** (for observed entanglement) |
| Joint states as a CORRESPONDENCE layer | **CONFIRMED** (hosted, non-derived) |
| Joint states as OPTIONAL (NP_044) | **REFINED** (optional for derived chain; required for observed Bell violation) |

**Conclusion:** AT must accept CHSH violations as fundamental physics. The loophole-free
Bell/CHSH violation (S = 2√2 > 2) is a confirmed empirical fact that canonical AT
(CHSH ≤ 2) cannot reproduce, so the joint-state sector is **REQUIRED PHYSICS** for a
complete theory of observed entanglement — entering as a **CORRESPONDENCE layer**
(hosted, non-derived). This refines NP_044: the joint states are optional for the
currently-derived chain but required for the observed Bell violation. **Canonical D96
unchanged.**

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_045_Tests.cs`
**Run:** 2026-09-04 · **Result:** see `Tests/Results/Y_NP_045_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_045_EvidenceInventory` | four evidence classes + S=2√2 | ✅ |
| `Y_NP_045_CanonicalCannotReproduce` | canonical CHSH ≤ 2 | ✅ |
| `Y_NP_045_JointStateReproduces` | Bell/teleportation/GHZ reproduced | ✅ |
| `Y_NP_045_OptionCRefuted` | no joint-state-free reproduction | ✅ |
| `Y_NP_045_ObservationsExplainedMissed` | 0/4 vs 4/4 | ✅ |
| `Y_NP_045_Consistency` | both sectors consistent | ✅ |
| `Y_NP_045_Classification` | REQUIRED PHYSICS / CORRESPONDENCE | ✅ |
| `Y_NP_045_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_045"`

---

## References

- ResearchY-NP_044 (optional for derived chain), NP_043 (irreducible), NP_042 (3-body),
  NP_041 (2-body), NP_040 (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038
  (entanglement ABSENT), QG_071 (joint link state — NEW SECTOR; EntanglingSector.cs),
  QG_070 (entangling interaction missing; EntanglementFromLinks.cs), QG_220 (θ=2πk/N),
  QG_216 (|ψ|=√ρ), D_036–D_040 (complex state; boundary set), M_001 (measurement reads
  both quadratures), R_001 (boundary set), S_001 (synthesis). External: Bell 1964,
  CHSH 1969, Aspect 1982, Zeilinger, loophole-free 2015 (Hensen/Giustina/Shalm).
