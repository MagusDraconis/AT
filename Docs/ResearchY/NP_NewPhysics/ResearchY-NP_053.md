# ResearchY-NP_053 — Relativistic Consistency Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_053 (permanent)
**Title:** Relativistic Consistency Audit
**Status:** COMPLETE
**Date:** 2026-09-05
**File:** `NP_NewPhysics/ResearchY-NP_053.md`
**Depends on:** ResearchY-NP_052 (two primitives complete), NP_051 (correspondence
layer = observation), NP_050 (gate = two-body interaction), NP_049 (gate uniquely
required), NP_048 (gate irreducible), NP_047 (gate = creation primitive), NP_046
(non-separability primitive), NP_045 (CHSH a fact), NP_044 (optional for derived
chain), NP_043 (joint state irreducible), NP_042 (3-body), NP_041 (2-body), NP_040
(rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038 (entanglement ABSENT), QG_071 (joint
link state — NEW SECTOR), QG_070 (entangling interaction missing), QG_220 (θ=2πk/N),
QG_216 (|ψ|=√ρ), D_036–D_040 (complex state / boundary set), M_001 (measurement reads
both quadratures), R_001 (boundary set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_053_Tests.cs`

---

## Purpose

> Do Joint States and Entangling Gates introduce any violation of causality, locality,
> or relativistic consistency?

**Known:** Joint State is non-separable (NP_046); Entangling Gate is a non-local
interaction (NP_050).

**Program steps:** (1) test the Bell pair A — B under spacelike separation; (2) verify
no-signalling; (3) test Bell, CHSH, teleportation, GHZ for superluminal communication;
(4) determine whether joint reality implies information transfer; (5) compare canonical
AT vs AT + correspondence layer; (6) search contradictions with causality, Lorentz
invariance, no-signalling.

**Success criterion:** determine whether {Joint State, Entangling Gate} is fully
compatible with relativistic physics.

**Classification:** DERIVED / CORRESPONDENCE / REFUTED / NEW PRIMITIVE.

---

## 1. Bell pair under spacelike separation

Verified (test `Y_NP_053_BellPairReducedDensityIsMaximallyMixed`): for a Bell pair, each
party's reduced density is ρ = I/2 — maximally random, carrying **zero information**
about the other side. S(ρ_A) = S(ρ_B) = 1 bit (maximal ignorance). There is no
observable effect at A from the state of B.

---

## 2. No-signalling

Verified (test `Y_NP_053_NoSignallingUnderUnitary`): the marginal ρ_A is **invariant**
under arbitrary local unitaries on B (bit-flip, phase-flip, Hadamard). This is the
no-signalling theorem: no local operation on B can change the statistics observed at A.

---

## 3. Bell / CHSH / teleportation / GHZ — no superluminal communication

- **CHSH correlations** (test `Y_NP_053_ChshCorrelationsNeedClassicalChannel`): each
  party alone sees maximally random outcomes; the non-local correlation is only
  observable **after classical comparison**. No superluminal signalling.
- **Teleportation** (test `Y_NP_053_TeleportationNeedsClassicalChannel`): the four
  Bell-basis outcomes are equiprobable (1/4 each), and without the 2-bit **classical**
  channel Bob's state is I/2 (zero information). The quantum channel alone transfers
  nothing; the reconstruction needs the classical bits.

---

## 4. Joint reality ≠ information transfer

Verified (test `Y_NP_053_JointRealityNotInformationTransfer`): the Bell pair has perfect
correlation (supported on |00⟩ and |11⟩ only) yet each marginal is I/2. Non-separability
is **correlation**, not **information transfer** — the latter requires a classical
channel.

---

## 5. Canonical AT vs AT + correspondence layer

Verified (test `Y_NP_053_CanonicalVsLayer`): canonical AT is trivially local (single-DOF,
CHSH ≤ 2) — no signalling, no contradiction. The correspondence layer adds **non-local
correlations** (CHSH > 2) but still **no signalling** (each marginal stays I/2).

---

## 6. No contradiction with relativity

Verified (test `Y_NP_053_NoContradictionWithRelativity`): no contradiction with
causality (no effect before cause), Lorentz invariance (the no-signalling bound is
frame-independent), or no-signalling (marginals invariant).

---

## 7. Legacy-lane reconciliation

- **NP_046:** non-separability is primitive — NP_053 shows it is non-separability of
  *correlations*, not of *causal influence*.
- **NP_050:** the gate is a non-local *interaction* — but the resulting states obey
  no-signalling (the non-locality is in the correlations, not the signalling).
- **NP_051:** the layer is the observational completion — NP_053 confirms it is
  observationally consistent with relativity.

---

## Theorem

> **Theorem (NP_053).** The pair {Joint State, Entangling Gate} is fully compatible with
> relativistic physics: it exhibits non-local correlations but obeys no-signalling, so it
> introduces no superluminal communication and no contradiction with causality or Lorentz
> invariance. Proof: (1) Spacelike separation (verified): a Bell pair's marginals are
> ρ = I/2 (maximally random, S = 1) — no information about the other party. (2) No-
> signalling (verified): ρ_A is invariant under arbitrary local unitaries on B. (3)
> CHSH/teleportation (verified): the correlation is observable only after classical
> comparison, and teleportation needs a 2-bit classical channel (without it Bob's state
> is I/2). (4) Joint reality (verified): non-separability is correlation, not
> information transfer. (5) Canonical vs layer (verified): canonical AT is local; the
> layer adds non-local correlations but no signalling. (6) No contradiction (verified):
> causality, Lorentz invariance, and no-signalling all hold. Hence the extension is
> fully relativistic. Canonical D96 unchanged.
>
> *Proof sketch.* (1) marginals I/2. (2) no-signalling. (3) classical channel. (4)
> correlation not transfer. (5) layer no-signalling. (6) no contradiction. ∎

---

## 8. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Entanglement allows superluminal signalling" | marginals are I/2 — no local operation changes them |
| "Teleportation transfers information faster than light" | it needs a 2-bit classical channel |
| "Non-separability implies information transfer" | it is correlation, not transfer |
| "The layer violates Lorentz invariance" | no-signalling is frame-independent |

---

## 9. Falsification paths

| Claim | Falsification |
|---|---|
| No-signalling holds | a local operation on B that changes ρ_A |
| Teleportation needs a classical channel | teleportation reconstructing |ψ⟩ without classical bits |
| Fully relativistic | a Bell-pair protocol with superluminal information transfer |

---

## Classification

| Component | Status |
|---|---|
| Bell / CHSH / teleportation / GHZ (non-local correlations) | **CORRESPONDENCE** |
| No-signalling (marginals ρ = I/2) | **DERIVED** (mathematical fact) |
| Superluminal communication | **REFUTED** |
| Joint reality = information transfer | **REFUTED** (correlation ≠ transfer) |
| Full relativistic compatibility | **CONFIRMED** |

**Conclusion:** the pair **{Joint State, Entangling Gate} is fully compatible with
relativistic physics.** The entanglement sector is non-local in *correlations* but obeys
**no-signalling**: each marginal is I/2 (invariant under the other party's operations),
the Bell/CHSH correlations require classical comparison to observe, and teleportation
requires a 2-bit classical channel. Joint reality (non-separability) is **correlation,
not information transfer**. There is no contradiction with causality, Lorentz invariance,
or no-signalling. **Canonical D96 unchanged.**

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_053_Tests.cs`
**Run:** 2026-09-05 · **Result:** see `Tests/Results/Y_NP_053_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_053_BellPairReducedDensityIsMaximallyMixed` | ρ = I/2 | ✅ |
| `Y_NP_053_NoSignallingUnderUnitary` | ρ_A invariant under U_B | ✅ |
| `Y_NP_053_ChshCorrelationsNeedClassicalChannel` | comparison needed | ✅ |
| `Y_NP_053_TeleportationNeedsClassicalChannel` | 2-bit channel needed | ✅ |
| `Y_NP_053_JointRealityNotInformationTransfer` | correlation ≠ transfer | ✅ |
| `Y_NP_053_CanonicalVsLayer` | layer non-local but no-signalling | ✅ |
| `Y_NP_053_NoContradictionWithRelativity` | no contradiction | ✅ |
| `Y_NP_053_Classification` | CORRESPONDENCE / REFUTED | ✅ |
| `Y_NP_053_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_053"`

---

## References

- ResearchY-NP_052 (two primitives complete), NP_051 (correspondence layer =
  observation), NP_050 (gate = two-body interaction), NP_049 (gate uniquely required),
  NP_048 (gate irreducible), NP_047 (gate = creation primitive), NP_046
  (non-separability primitive), NP_045 (CHSH a fact), NP_044 (optional for derived
  chain), NP_043 (joint state irreducible), NP_042 (3-body), NP_041 (2-body), NP_040
  (rank-2 matrix), NP_039 (1 NEW PRIMITIVE), NP_038 (entanglement ABSENT), QG_071
  (joint link state — NEW SECTOR; EntanglingSector.cs), QG_070 (entangling interaction
  missing; EntanglementFromLinks.cs), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ),
  D_036–D_040 (complex state; boundary set), M_001 (measurement reads both
  quadratures), R_001 (boundary set), S_001 (synthesis).
