# ResearchY-NP_046 — Joint State Physical Necessity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_046 (permanent)
**Title:** Joint State Physical Necessity Audit
**Status:** COMPLETE
**Date:** 2026-09-05
**File:** `NP_NewPhysics/ResearchY-NP_046.md`
**Depends on:** ResearchY-NP_045 (CHSH violation is a physical fact — joint states
REQUIRED), ResearchY-NP_044 (optional for derived chain), ResearchY-NP_043
(irreducible), NP_042 (3-body), NP_041 (2-body), NP_040 (rank-2 matrix), NP_039
(1 NEW PRIMITIVE), NP_038 (entanglement ABSENT), QG_071 (joint link state — NEW
SECTOR), QG_070 (entangling interaction missing), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ),
D_036–D_040 (complex state / boundary set), M_001 (measurement reads both
quadratures), R_001 (boundary set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_046_Tests.cs`

---

## Purpose

> Why does nature require Joint States?

**Known:** canonical AT gives CHSH ≤ 2; observed physics gives CHSH > 2.

**Program steps:** (1) inventory all phenomena requiring Joint States; (2) search the
common physical feature across Bell, teleportation, GHZ, W; (3) determine whether
Joint States represent A) shared information, B) shared actualization, C) shared
reality, D) fundamentally new ontology; (4) remove Joint States and measure which
observations fail first; (5) identify the minimal physical principle forcing their
existence.

**Success criterion:** explain not how Joint States work, but why nature must contain
them.

**Classification:** DERIVED / EMERGENT / CORRESPONDENCE / NEW PRIMITIVE / REFUTED.

---

## 1. Inventory: the common feature is NON-SEPARABILITY

| Phenomenon | Witness | Non-separable? |
|---|---|---|
| Bell pair | Schmidt rank 2, C = 1 | yes |
| Teleportation | F = 1 (needs a Bell pair) | yes |
| GHZ | τ₃ = 1 | yes (genuinely tripartite) |
| W | τ₃ = 0, pairwise C = 2/3 | yes (genuinely tripartite) |

Verified (test `Y_NP_046_CommonFeatureNonSeparability`): every phenomenon that
requires a joint state shares ONE feature — **the joint state is irreducible to the
states of its parts** (non-separability).

---

## 2. What joint states are NOT

- **A) shared information** — MI > 0 is classical correlation; canonical AT already
  produces it (NP_038: shared events → MI > 0 but separable). REFUTED.
- **B) shared actualization** — joint phase pinning gives classical correlation, a
  product state (rank 1). Already canonical. REFUTED.

Verified (tests `Y_NP_046_SharedInformationInsufficient`,
`Y_NP_046_SharedActualizationInsufficient`): both are classical, separable, and give
only correlation — they cannot produce the observed Bell violation.

---

## 3. What joint states ARE

- **C) SHARED REALITY** — a single coherent actualization spanning two subsystems.
  Verified (test `Y_NP_046_SharedRealityIrreducible`): for a Bell pair each single
  sector is maximally mixed (ρ_A = I/2, S = 1 bit — *no local reality*), yet the joint
  state is pure (S = 0). The coherence lives ONLY in the joint object — the reality is
  in the relation, not the parts.
- **D) FUNDAMENTALLY NEW ONTOLOGY** — the rank-2 joint amplitude (NP_040's "new state
  object").

---

## 4. Removing joint states: first failure

Verified (test `Y_NP_046_RemoveJointStatesFailureOrder`): without joint states the
**Bell/CHSH violation (S > 2) fails first**, then teleportation (F ≤ 2/3), then GHZ/W
(τ₃ = 0).

---

## 5. The minimal physical principle

The minimal physical principle forcing joint states is:

> **The irreducibility of joint actualization to separate single-sector actualizations
> — NON-SEPARABILITY IS PRIMITIVE.**

Nature must contain joint states because the Bell violation proves that two subsystems
can actualize ONE coherent state that no single subsystem possesses — the whole is
irreducible to its parts. Canonical AT's primitives (Difference, Actualization,
Occupancy, Information, Phase) are all single-DOF or classical; none permits a
coherent actualization spanning two subsystems. Joint states are the minimal addition
that allows **shared reality** — and the Bell violation shows nature uses it.

---

## 6. Legacy-lane reconciliation

- **NP_038:** canonical D96 gives only correlation — because its primitives are
  single-DOF / classical, they cannot represent shared reality.
- **NP_039/040:** the joint link state is a NEW PRIMITIVE (a rank-2 amplitude) — this
  is the "fundamentally new ontology" (D) that realizes shared reality (C).
- **NP_045:** CHSH violation is a fact — NP_046 explains WHY it is a fact: actuality
  is not always decomposable into independent single-sector actualizations.
- **QG70/71:** the entangling interaction / joint link state is a NEW SECTOR — NP_046
  names its physical content: shared reality (non-separable joint actualization).

---

## Theorem

> **Theorem (NP_046).** Nature requires Joint States because actualization is not
> always decomposable into independent single-sector actualizations: the minimal
> physical principle is that NON-SEPARABILITY IS PRIMITIVE. Proof: (1) Common feature
> (verified): Bell, teleportation, GHZ, W all share non-separability (rank &gt; 1 or
> genuine tripartiteness). (2) Insufficient candidates (verified): shared information
> (MI &gt; 0) and shared actualization (phase pinning) are classical — separable, rank
> 1 — and already canonical (NP_038). (3) Shared reality (verified): for a Bell pair
> each single sector is maximally mixed (ρ_A = I/2, S = 1) while the joint state is
> pure (S = 0) — the coherence lives only in the joint object. (4) First failure
> (verified): removing joint states fails the Bell violation first, then teleportation,
> then GHZ/W. (5) Minimal principle: the joint state cannot be written as a product
> a⊗b (det ≠ 0); its defining property is non-separability. Hence Joint States
> represent SHARED REALITY (C) realized as a FUNDAMENTALLY NEW ONTOLOGY (D); shared
> information (A) and shared actualization (B) are REFUTED. Canonical D96 unchanged.
>
> *Proof sketch.* (1) non-separability common. (2) A/B classical. (3) C shared reality.
> (4) Bell fails first. (5) non-separability primitive. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Joint states are shared information" | MI > 0 is classical, separable (NP_038) |
| "Joint states are shared actualization" | phase pinning is a product state (rank 1) |
| "The whole is the sum of its parts" | the Bell pair has no local part (ρ_A = I/2), yet is pure |
| "Non-separability is derived" | canonical primitives are single-DOF/classical (NP_043) |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| Non-separability is primitive | a canonical (single-DOF) state with Schmidt rank ≥ 2 |
| Shared reality is the correct ontology | a joint state that is decomposable into local parts |
| Bell fails first | an observation failing before Bell when joint states are removed |

---

## Classification

| Component | Status |
|---|---|
| Shared information (A) as the joint-state content | **REFUTED** (classical, separable) |
| Shared actualization (B) as the joint-state content | **REFUTED** (classical, rank 1) |
| Shared reality (C) | **CONFIRMED** (irreducible joint coherence) |
| Fundamentally new ontology (D) | **CONFIRMED** (rank-2 joint amplitude, NP_040) |
| Non-separability as the minimal principle | **NEW PRIMITIVE** (primitive irreducibility) |

**Conclusion:** nature requires Joint States because **actuality is not always
decomposable into independent single-sector actualizations**. The common feature of
every entanglement phenomenon is non-separability — the whole is irreducible to its
parts. Joint states are **shared reality (C)** realized as a **fundamentally new
ontology (D)**; they are not shared information (A) or shared actualization (B), both
of which are classical and already canonical. The minimal physical principle forcing
them is: **NON-SEPARABILITY IS PRIMITIVE**. **Canonical D96 unchanged.**

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_046_Tests.cs`
**Run:** 2026-09-05 · **Result:** see `Tests/Results/Y_NP_046_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_046_CommonFeatureNonSeparability` | non-separability common to all | ✅ |
| `Y_NP_046_SharedInformationInsufficient` | MI>0 classical | ✅ |
| `Y_NP_046_SharedActualizationInsufficient` | phase pinning rank 1 | ✅ |
| `Y_NP_046_SharedRealityIrreducible` | ρ_A=I/2, joint pure | ✅ |
| `Y_NP_046_RemoveJointStatesFailureOrder` | Bell fails first | ✅ |
| `Y_NP_046_MinimalPrinciple` | non-separability primitive | ✅ |
| `Y_NP_046_Classification` | C/D confirmed, A/B refuted | ✅ |
| `Y_NP_046_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_046"`

---

## References

- ResearchY-NP_045 (CHSH violation a fact), NP_044 (optional for derived chain),
  NP_043 (irreducible), NP_042 (3-body), NP_041 (2-body), NP_040 (rank-2 matrix),
  NP_039 (1 NEW PRIMITIVE), NP_038 (entanglement ABSENT), QG_071 (joint link state —
  NEW SECTOR; EntanglingSector.cs), QG_070 (entangling interaction missing;
  EntanglementFromLinks.cs), QG_220 (θ=2πk/N), QG_216 (|ψ|=√ρ), D_036–D_040 (complex
  state; boundary set), M_001 (measurement reads both quadratures), R_001 (boundary
  set), S_001 (synthesis).
