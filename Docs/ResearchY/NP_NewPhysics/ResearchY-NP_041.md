# ResearchY-NP_041 — Joint Link Consequence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_041 (permanent)
**Title:** Joint Link Consequence Audit
**Status:** COMPLETE
**Date:** 2026-09-04
**File:** `NP_NewPhysics/ResearchY-NP_041.md`
**Depends on:** ResearchY-NP_040 (joint link state = rank-2 2×2 matrix, a NEW STATE
OBJECT), ResearchY-NP_039 (joint link state = minimal extension, 1 NEW PRIMITIVE),
ResearchY-NP_038 (entanglement ABSENT — only correlation), QG_071 (joint link state —
NEW SECTOR), QG_070 (entangling interaction missing), QG_220 (θ = 2πk/N), QG_216
(|ψ| = √ρ), D_036–D_040 (complex state / boundary set), M_001 (measurement reads both
quadratures), R_001 (boundary set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_041_Tests.cs`

---

## Purpose

> Does the Joint Link State reproduce known quantum-entanglement phenomenology?

**Program steps:** (1) Bell pair; (2) GHZ state; (3) W state; (4) monogamy relations;
(5) entanglement entropy; (6) teleportation fidelity.

**Determine:** can a rank-2 joint link generate the standard hierarchy of entangled
states?

**Classification:** DERIVED / CORRESPONDENCE / REFUTED.

**Success criterion:** determine whether the Joint Link State is merely sufficient for
Bell pairs, or a complete entanglement sector.

---

## 1. State model (recap)

The joint link state (NP_040) is a normalized rank-2 complex 2×2 matrix — a
**two-qubit** object (one link = two nodes). The canonical representative is the Bell
pair (|00⟩+|11⟩)/√2. NP_041 asks what this object *implies*: does it reproduce the
standard phenomenology of entanglement, or only the two-body level?

---

## 2. The six phenomenology checks

| Phenomenon | Result | Status |
|---|---|---|
| Bell pair | rank 2, C = 1, CHSH = 2√2, S(ρ_A) = 1 bit | **DERIVED** |
| GHZ state (|000⟩+|111⟩)/√2 | 3-tangle τ₃ = 1, bipartite reductions separable (C=0) | **REFUTED from a 2-body link** |
| W state (|001⟩+|010⟩+|100⟩)/√3 | 3-tangle τ₃ = 0, bipartite C = 2/3, genuinely tripartite | **REFUTED from a 2-body link** |
| Monogamy (CKW) | C²(AB)+C²(AC) ≤ 4·det(ρ_A) | **DERIVED** |
| Entanglement entropy | S(ρ_A) = 1 (Bell); H(a²) for a\|00⟩+b\|11⟩ | **DERIVED** |
| Teleportation fidelity | F = (2+C)/3; Bell → F = 1 | **DERIVED** |

---

## 3. Two-body phenomenology is DERIVED

**Bell pair** (test `Y_NP_041_BellPair`): the joint link state *is* the Bell pair —
rank 2, concurrence 1, CHSH = 2√2, and the reduced single-sector density is maximally
mixed (S(ρ_A) = 1 bit).

**Entanglement entropy** (test `Y_NP_041_EntanglementEntropy`): for a general
a|00⟩+b|11⟩ resource, S(ρ_A) = H(a²) = −a²log₂a² − b²log₂b², saturating at 1 bit for
a = b = 1/√2.

**Monogamy** (test `Y_NP_041_Monogamy`): the CKW inequality C²(AB) + C²(AC) ≤
4·det(ρ_A) holds; a two-body joint link entangles each node with at most one partner.

**Teleportation fidelity** (test `Y_NP_041_TeleportationFidelity`): the teleportation
fidelity of a pure resource of concurrence C is F = (2+C)/3, giving F = 1 for a Bell
pair (perfect teleportation) and F = 2/3 in the classical (C = 0) limit.

---

## 4. Genuine multipartite entanglement is NOT DERIVED

The GHZ and W states are **genuinely tripartite** — they are not tensor products of
two-body states (a network of Bell pairs is *biseparable*). Concretely:

- **GHZ** (test `Y_NP_041_GhzState`): τ₃ = 1, but its two-qubit reductions are
  **separable** (concurrence 0, CHSH = 2) — whereas a Bell pair's reduction is
  maximally entangled. A GHZ state therefore cannot be Bell ⊗ Bell: it requires a
  **three-body joint state or an entangling gate** — content beyond the rank-2
  two-body link.
- **W** (test `Y_NP_041_WState`): τ₃ = 0 (a distinct SLOCC class from GHZ), bipartite
  reductions have concurrence 2/3, and the single-qubit reductions are mixed — a
  genuinely tripartite state, likewise not a composition of two-body links.

So the rank-2 joint link generates the **two-body level** of the hierarchy (Bell) but
**not the multipartite levels** (GHZ, W). These are hosted by a 3-body joint state /
entangling gate — a **CORRESPONDENCE**, not a DERIVED consequence of the 2-body link.

---

## 5. Legacy-lane reconciliation

- **QG71:** the joint link state is a NEW SECTOR on a 2-node link. NP_041 shows this
  2-body object closes under two-body phenomenology but does not reach genuine
  multipartite entanglement.
- **QG70:** the entangling *interaction* is missing; a static 2-body joint state gives
  Bell pairs but no entangling *gate* to compose them into GHZ/W.
- **NP_040:** the object is a rank-2 2×2 matrix (a new state object) — NP_041 confirms
  its consequence set is exactly the 2-body entanglement sector.
- **NP_039 / NP_038:** unchanged — the joint link state is 1 NEW PRIMITIVE, and
  canonical D96 alone yields only correlation.

---

## Theorem

> **Theorem (NP_041).** The rank-2 joint link state is a complete TWO-BODY
> entanglement sector but not a complete multipartite entanglement sector: it DERIVEs
> Bell pairs, entanglement entropy, CKW monogamy, and Bell-pair teleportation, but it
> does NOT derive the genuine multipartite states GHZ and W, which require a
> three-body joint state or an entangling gate. Proof: (1) Bell (verified): the joint
> link state has rank 2, C = 1, CHSH = 2√2, S(ρ_A) = 1. (2) Entropy (verified):
> S(ρ_A) = H(a²) for a|00⟩+b|11⟩. (3) Monogamy (verified): C²(AB)+C²(AC) ≤ 4·det(ρ_A)
> holds for GHZ (τ₃ = 1, strict) and W (τ₃ = 0, saturated). (4) Teleportation
> (verified): F = (2+C)/3 → 1 for Bell. (5) Multipartite (verified): GHZ has τ₃ = 1
> with separable bipartite reductions, and W has τ₃ = 0 with entangled bipartite
> reductions (C = 2/3) — both are genuinely tripartite and not tensor products of
> two-body joint link states (a Bell network is biseparable). Hence the two-body level
> is DERIVED, and the multipartite level is CORRESPONDENCE (hosted by a 3-body
> extension). Success criterion: the joint link state is MERELY SUFFICIENT FOR BELL
> PAIRS. Canonical D96 unchanged.
>
> *Proof sketch.* (1)–(4) two-body DERIVED; (5) multipartite not derivable. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "GHZ = Bell ⊗ Bell" | GHZ's bipartite reductions are separable (C=0); a Bell network's are entangled |
| "W is a product of two-body links" | W has entangled bipartite reductions yet τ₃ = 0 — genuinely tripartite |
| "A rank-2 link reaches multipartite entanglement" | the link is 2-qubit; GHZ/W need 3 qubits |
| "Teleportation needs a complete sector" | one Bell pair already gives F = 1 |
| "Monogamy needs a 3-body primitive" | CKW holds as a constraint on 2-body links |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| Two-body phenomenology is derived | a Bell-pair consequence (C, CHSH, S, F) not reproduced by the rank-2 link |
| GHZ/W need a 3-body object | a GHZ or W state expressed as a tensor product of two-body joint link states |
| The link is merely sufficient for Bell pairs | a rank-2 link generating genuine multipartite entanglement without extra content |
| F = (2+C)/3 | a pure resource with concurrence C whose teleportation fidelity deviates |

---

## Classification

| Component | Status |
|---|---|
| Bell pair from the joint link state | **DERIVED** (rank 2, C=1, CHSH=2√2, S=1) |
| Entanglement entropy S(ρ_A) | **DERIVED** (H(a²), Bell → 1 bit) |
| CKW monogamy C²(AB)+C²(AC) ≤ 4·det(ρ_A) | **DERIVED** |
| Teleportation fidelity F = (2+C)/3 | **DERIVED** (Bell → 1) |
| GHZ state from a single rank-2 link | **REFUTED** (needs a 3-body joint state / entangling gate) |
| W state from a single rank-2 link | **REFUTED** (needs a 3-body joint state / entangling gate) |
| Genuine multipartite entanglement as a whole | **CORRESPONDENCE** (hosted by a 3-body extension) |

**Conclusion:** the Joint Link State is **merely sufficient for Bell pairs** — it is a
complete *two-body* entanglement sector (Bell, entropy, monogamy, teleportation all
DERIVED) but **not a complete entanglement sector**: the genuine multipartite states
GHZ and W require a three-body joint state or an entangling gate (CORRESPONDENCE).
**Success criterion: not a complete sector. Canonical D96 unchanged.**

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_041_Tests.cs`
**Run:** 2026-09-04 · **Result:** see `Tests/Results/Y_NP_041_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_041_BellPair` | Bell: rank 2, C=1, CHSH=2√2, S=1 | ✅ |
| `Y_NP_041_GhzState` | GHZ τ₃=1, reductions separable — not 2-body | ✅ |
| `Y_NP_041_WState` | W τ₃=0, bipartite C=2/3 | ✅ |
| `Y_NP_041_Monogamy` | CKW monogamy holds | ✅ |
| `Y_NP_041_EntanglementEntropy` | S(ρ_A)=H(a²) | ✅ |
| `Y_NP_041_TeleportationFidelity` | F=(2+C)/3, Bell→1 | ✅ |
| `Y_NP_041_Classification` | DERIVED / REFUTED / CORRESPONDENCE flags | ✅ |
| `Y_NP_041_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_041"`

---

## References

- ResearchY-NP_040 (joint link state = rank-2 2×2 matrix, NEW STATE OBJECT),
  ResearchY-NP_039 (minimal extension, 1 NEW PRIMITIVE), ResearchY-NP_038
  (entanglement ABSENT), QG_071 (joint link state — NEW SECTOR; EntanglingSector.cs),
  QG_070 (entangling interaction missing; EntanglementFromLinks.cs), QG_220 (θ=2πk/N),
  QG_216 (|ψ|=√ρ), D_036–D_040 (complex state; boundary set), M_001 (measurement reads
  both quadratures), R_001 (boundary set), S_001 (synthesis).
