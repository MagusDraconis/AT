# ResearchY-NP_038 — Entanglement Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_038 (permanent)
**Title:** Entanglement Audit
**Status:** COMPLETE
**Date:** 2026-09-04
**File:** `NP_NewPhysics/ResearchY-NP_038.md`
**Depends on:** ResearchY-NP_037 (The Role of Three — rung-ladder decoupling),
NP_036 (3D emergence CORRESPONDENCE), NP_014 (synchronization optional),
NP_011/010 (phase-flow layer not a physical field), NP_009/008 (actualization
extremizes nothing), NP_007 (coupling network static), NP_006 (locking term κ
derivable, mechanism non-canonical), NP_005 (unequal-mode locking ABSENT/BOUNDARY),
NP_004 (two-system coupling = interference + shared events only), NP_003 (phase the
only lever), QG_070/071 (entanglement REQUIRES NEW SECTOR), QG_018 (single-DOF
scalar ⇒ 1 breathing mode, needs imported tensor sector), QG_216/220 (|ψ|=√ρ,
θ=2πk/N), D_036–D_040 (complex state, irreducible boundary set), M_001 (measurement
reads both quadratures), R_001 (five-item boundary set), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_038_Tests.cs`

---

## Purpose

> Can canonical D96 structures generate true entanglement, or only correlation?

**Program steps:** (1) define two independent D96 sectors A, B; (2) construct the
A×B state space; (3) search for states NOT writable as ψA⊗ψB; (4) test Bell-type
factorization, separability, mutual information, Schmidt rank; (5) distinguish
correlation / synchronization / resonance locking / genuine entanglement; (6)
determine whether any canonical object produces non-factorizable states.

**Success criterion:** A) absent, B) emergent, C) correspondence only, D) fundamental
consequence of D96.

---

## 1. State model

Each sector S ∈ {A, B} is the 2-mode reduction used by the NP interference program.
The canonical single-sector state over the basis {|0⟩, |1⟩} is

ψ_S = √ρ₀·e^{iθ₀}|0_S⟩ + √ρ₁·e^{iθ₁}|1_S⟩,   θ_k = 2πk/N,   N = 96,

with the default two-step branching shares ρ₀ = 1/3, ρ₁ = 2/3. The joint A×B space
has coefficient matrix c_{ij} = ⟨ij|ψ⟩ = a_i·b_j for a product state, where a, b are
the 2-component sector amplitudes. All objects below are 2×2 complex matrices with
entries in the computational product basis {|00⟩, |01⟩, |10⟩, |11⟩}. The complex
amplitude itself is canonical: |ψ| = √ρ (QG216), θ = 2πk/N (QG220); measurement reads
both quadratures {cos, sin} of one complex mode (M_001/D_037).

---

## 2. Inventory of canonical two-sector mechanisms

| Mechanism | Canonical object | Status | Ref |
|---|---|---|---|
| Independent actualization | product ψA⊗ψB (per-sector local rule) | factorizable, rank 1 | D_036 |
| Shared actualization event / joint phase pinning | common-origin classical correlation | classical, diagonal-separable | M_002 / NP_004 |
| Interference intensity κ = 2√(ρ_A·ρ_B) | intensity of ONE complex amplitude | observable, NOT an entangler | NP_006/007 |
| Synchronization / resonance locking of unequal modes | (no locking force) | ABSENT / BOUNDARY | NP_005/006/009/014 |
| Measurement | local quadrature read (one sector) | local, no cross-sector coupling | M_001 |

Two sectors meet only through (a) shared classical events (phase pinning) and (b)
the interference intensity (single-DOF). Neither creates a coherent joint amplitude
c_{ij} with Schmidt rank ≥ 2.

---

## 3. Factorization & separability results

Verified (test `Y_NP_038_CanonicalProductSeparable`): for every canonical product
ψA⊗ψB (grid of sector indices, default shares 1/3, 2/3), the coefficient matrix has
**Schmidt rank 1**, **concurrence 0**, and **CHSH = 2** (no violation). The product
state is the only two-sector state the canonical generators can build.

---

## 4. Entanglement witnesses

| Witness | Product ψA⊗ψB | Shared-event mixture | Bell state |
|---|---|---|---|
| Schmidt rank | 1 | — (mixed) | 2 |
| Concurrence C | 0 | 0 | 1 |
| CHSH | 2 | 2 | 2√2 ≈ 2.828 |
| Mutual information | 0 | H(1/3) ≈ 0.918 bits | 1 bit (S(A)=S(B)=1) |

The decisive distinction: **MI > 0 with concurrence 0** is *classical correlation*,
not entanglement. Only a state with **Schmidt rank ≥ 2** (equivalently concurrence
> 0, CHSH > 2) is genuinely entangled. The canonical two-sector mechanisms produce
MI > 0 (shared events) but never Schmidt rank ≥ 2.

---

## 5. The four classes distinguished

| Class | Presence in canonical D96 | Status |
|---|---|---|
| Correlation (shared events, phase pinning) | present | **DERIVED** (classical, diagonal-separable) |
| Synchronization | equal modes only (trivial co-rotation) | **EMERGENT** (equal modes); a product-state classical relation |
| Resonance locking (unequal modes) | absent | **ABSENT / BOUNDARY** (NP_005/006/009/014 unchanged) |
| Genuine entanglement (Schmidt rank ≥ 2, CHSH > 2) | absent | **REFUTED / ABSENT** as a D96 output |

---

## 6. Legacy-lane reconciliation

- **QG070/071 (canonical verdict):** shared link phases give classical correlations,
  NOT Bell entanglement; genuine entanglement REQUIRES a NEW SECTOR (a joint link
  state / entangling interaction), which is not in {θ, S} nor derivable from it.
  Code: `AT.Core/ResearchXH/EntanglementFromLinks.cs`, `EntanglingSector.cs`.
- **QG018 (legacy):** the scalar sector has 1 breathing mode; observed GW needs 2
  tensor TT modes → PARTIAL MATCH. Precedent: a canonical single-DOF sector cannot
  reproduce multi-DOF phenomena without an imported tensor sector.
- **ResearchQM-003 (legacy alternate lane):** claimed tensor product + entanglement
  DERIVED from *Q-event individuation + shared causal ancestry + M² non-linearity* —
  a DIFFERENT primitive base than canonical D96 `{Difference, η}`. This is NOT
  reproduced in the D96 chain, and is not in the current `Docs/NewChat_Start.md`.
  Code-only lane: `AT.Core/ResearchQM/TensorEntanglementAnalyzer.cs`,
  `AT.Tests/ResearchQM/AT_QM003_TensorProductEntanglementAudit.cs`.

The canonical primitive set (R_001 / D_040) — {Difference, η} ∪ {Z2-paired sector}
∪ {3 octave families} ∪ {SU(2) gauge + j=1/2} ∪ {v, m_e} — contains **no**
entangling interaction, no joint link state, no M² non-linearity (verified by test
`Y_NP_038_ResearchQMLegacyDifferentBase`).

---

## Theorem

> **Theorem (NP_038).** Canonical D96 structures generate correlation but not
> entanglement. Proof: (1) Product states (Section 3, verified): every canonical
> two-sector state ψA⊗ψB has Schmidt rank 1, concurrence 0, CHSH = 2 — no
> non-factorizable state is produced by independent actualization. (2) Shared events
> (Section 4, verified): the classical mixture p|00⟩⟨00|+(1−p)|11⟩⟨11| has MI =
> H(p) > 0 but concurrence 0 and CHSH = 2 — it is a diagonal, PPT-separable classical
> correlation, not entanglement. (3) Interference (verified): I = ρ0+ρ1+2√(ρ0ρ1)cosΔθ
> is the intensity of ONE complex amplitude (single-sector coherence); it is an
> observable, not an entangler — tensoring it with another sector stays rank 1.
> (4) Bell requirement (verified): a Bell state (|00⟩+|11⟩)/√2 has Schmidt rank 2,
> concurrence 1, CHSH = 2√2; sweeping the canonical product grid (sector indices ×
> shares) never reaches rank 2 — a Bell state needs an entangling gate / joint
> coherent preparation. (5) Canonical generators (verified): per-sector actualization
> (product), local phase update θ(t+1)=θ(t)+Δθ (local unitary U_A⊗U_B), local
> quadrature measurement (M_001), and the interference observable (NP_008/009) all
> preserve Schmidt rank 1 and concurrence 0 under evolution. (6) Primitive base
> (verified): the canonical primitive set contains no entangling object; QG070/71's
> "joint link state" is a NEW SECTOR, and ResearchQM-003's DERIVED claim uses a
> different primitive base that does not transfer. Classification: two-sector product
> DERIVED; shared-event classical correlation DERIVED; single-DOF interference DERIVED
> as an observable (not an entangler); synchronization of unequal modes ABSENT/
> BOUNDARY; genuine entanglement from canonical D96 REFUTED/ABSENT; observed
> entanglement CORRESPONDENCE/BOUNDARY (needs a NEW entangling sector). Success
> criterion A — ABSENT. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) product rank-1. (2) shared events classical. (3) interference
> single-DOF. (4) Bell needs gate. (5) generators preserve rank. (6) primitive base
> clean. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "A single canonical product can violate CHSH" | every product has CHSH = 2 (Bloch rank-1 correlation matrix) |
| "Shared events create Bell states" | the mixture is diagonal ⇒ PPT ⇒ separable; concurrence 0, CHSH = 2 |
| "Interference κ is an entangler" | κ is the intensity of ONE complex amplitude; tensoring stays rank 1 |
| "M² / ResearchQM primitives are in the D96 chain" | the canonical set {Difference, η, Z2, octave families, SU(2), v, m_e} has no M²/entangler |
| "Entanglement emerges from one sector" | one sector is a single 2-mode amplitude; no joint c_{ij} exists |
| "Synchronization locks unequal modes into entanglement" | locking is ABSENT/BOUNDARY (NP_005/006); equal-mode co-rotation is classical |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| Canonical D96 is non-factorizable | a canonical product with Schmidt rank ≥ 2 or CHSH > 2 |
| Shared events produce Bell states | a diagonal classical mixture with concurrence > 0 or CHSH > 2 |
| κ entangles | an interference observable that raises the joint Schmidt rank above 1 |
| Entanglement emergent from D96 | a derivation of a joint amplitude c_{ij} (rank ≥ 2) from {Difference, η} alone |
| QG70/71 "REQUIRES NEW SECTOR" is wrong | a Bell-type state generated without an entangling gate / joint link state |

---

## Classification

| Component | Status |
|---|---|
| Two-sector product state ψA⊗ψB (independent actualization) | **DERIVED** (Schmidt rank 1, factorizable, concurrence 0, CHSH ≤ 2) |
| Classical (common-origin) correlation via shared events / joint phase pinning | **DERIVED** (MI > 0, diagonal ⇒ separable, CHSH = 2) |
| Single-DOF interference κ = 2√(ρ_A·ρ_B), I = ρ_A+ρ_B+2√(ρ_Aρ_B)cosΔθ | **DERIVED** as an OBSERVABLE, NOT an entangler (single-sector coherence) |
| Synchronization / resonance locking of unequal modes | **ABSENT / BOUNDARY** (unchanged NP_005/006/009/014) |
| Genuine (non-factorizable, Bell-type) entanglement from canonical D96 | **REFUTED / ABSENT** (no canonical object has Schmidt rank ≥ 2 or violates CHSH) |
| Observed / desired entanglement | **CORRESPONDENCE / BOUNDARY** (requires a NEW entangling sector; QG70/71 unchanged) |

**Conclusion:** canonical D96 yields only correlation — genuine Bell-type entanglement
is ABSENT (success criterion **A**). Two sectors meet only through shared classical
events and the single-DOF interference intensity; neither creates a coherent joint
amplitude c_{ij} with Schmidt rank ≥ 2. Bell states require a joint two-sector
preparation — the "joint link state" that QG71 classifies as a NEW SECTOR. **No new
primitive; canonical AT unchanged.**

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_038_Tests.cs`
**Run:** 2026-09-04 · **Result:** see `Tests/Results/Y_NP_038_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_038_CanonicalProductSeparable` | product ψA⊗ψB rank 1, C=0, CHSH≤2 | ✅ |
| `Y_NP_038_SharedEventCorrelationSeparable` | shared-event mixture MI>0 but separable | ✅ |
| `Y_NP_038_InterferenceSingleDofNotEntangler` | I = single-DOF intensity, not an entangler | ✅ |
| `Y_NP_038_BellNeedsEntanglingGate` | Bell rank 2 / C=1 / CHSH=2√2; products never rank 2 | ✅ |
| `Y_NP_038_NoEntanglingGateInCanonicalSet` | canonical generators preserve rank 1 / C=0 | ✅ |
| `Y_NP_038_ResearchQMLegacyDifferentBase` | no entangler in canonical primitive set | ✅ |
| `Y_NP_038_Classification` | DERIVED / ABSENT / CORRESPONDENCE flags | ✅ |
| `Y_NP_038_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_038"`

---

## References

- ResearchY-NP_037 (rung-ladder decoupling), NP_036 (3D CORRESPONDENCE), NP_014
  (synchronization optional), NP_011/010 (phase-flow layer not a physical field),
  NP_009/008 (actualization extremizes nothing), NP_007 (static coupling network),
  NP_006 (locking term κ derivable, mechanism non-canonical), NP_005 (unequal-mode
  locking ABSENT/BOUNDARY), NP_004 (two-system coupling = interference + shared
  events), NP_003 (phase the only lever), QG_070/071 (entanglement REQUIRES NEW
  SECTOR; EntanglementFromLinks.cs / EntanglingSector.cs), QG_018 (scalar 1 breathing
  mode), QG_216/220 (|ψ|=√ρ, θ=2πk/N), D_036–D_040 (complex state; irreducible
  boundary set), M_001 (measurement reads both quadratures), R_001 (five-item
  boundary set), S_001 (synthesis). Legacy alternate lane: ResearchQM-003
  (TensorEntanglementAnalyzer.cs / AT_QM003_TensorProductEntanglementAudit.cs).
