# Y_NP_038_Result.md — ResearchY-NP_038 Entanglement Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_038_Tests.cs`
**Run:** 2026-09-04
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_038"`

---

## Summary

**Question:** Can canonical D96 structures generate true entanglement, or only
correlation?

**Verdict: ONLY correlation — genuine Bell-type entanglement is ABSENT from canonical
D96 (success criterion A).** The two-sector product ψA⊗ψB (independent actualization)
is DERIVED and factorizable (Schmidt rank 1, concurrence 0, CHSH = 2). Shared-event
classical correlation (joint phase pinning) is DERIVED (MI = H(1/3) ≈ 0.918 bits > 0,
diagonal ⇒ separable, CHSH = 2). The single-DOF interference intensity
κ = 2√(ρ_A·ρ_B) is DERIVED as an OBSERVABLE of ONE complex amplitude — not an
entangler. No canonical object has Schmidt rank ≥ 2 or violates CHSH; Bell states
need an entangling gate / joint link state (QG70/71: REQUIRES NEW SECTOR).

## Product state (Section 3)

Canonical products ψA⊗ψB across the sector-index grid and shares {1/3, 1/2, 2/3}
all have Schmidt rank 1, concurrence 0, CHSH = 2 (Bloch rank-1 correlation matrix).
The product state is the only two-sector state the canonical generators build.

## Entanglement witnesses (Section 4)

| Witness | Product | Shared-event mixture | Bell state |
|---|---|---|---|
| Schmidt rank | 1 | — | 2 |
| Concurrence | 0 | 0 | 1 |
| CHSH | 2 | 2 | 2√2 ≈ 2.828 |
| Mutual information | 0 | H(1/3) ≈ 0.918 bits | 1 bit |

MI > 0 with concurrence 0 = classical correlation, NOT entanglement. Only Schmidt
rank ≥ 2 (C > 0, CHSH > 2) is genuine entanglement — which canonical D96 never
reaches.

## The four classes (Section 5)

- Correlation (shared events) — present, **DERIVED** (classical, diagonal-separable).
- Synchronization — equal modes only, **EMERGENT** (trivial co-rotation, a
  product-state classical relation).
- Resonance locking (unequal modes) — **ABSENT / BOUNDARY** (NP_005/006/009/014).
- Genuine entanglement — **REFUTED / ABSENT** as a D96 output.

## Legacy reconciliation (Section 6)

QG70/71 (canonical): shared link phases give classical correlations, not Bell
entanglement; genuine entanglement REQUIRES a NEW SECTOR. QG018: single-DOF scalar
sector ⇒ 1 breathing mode (needs imported tensor sector). ResearchQM-003's DERIVED
claim uses a different primitive base (Q-event + M² non-linearity) and does not
transfer to the D96 chain. The canonical primitive set
{Difference, η, Z2-paired sector, 3 octave families, SU(2) gauge, v, m_e} contains
no entangler.

## Test results

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

## Classification

| Component | Status |
|---|---|
| Two-sector product ψA⊗ψB | **DERIVED** (rank 1, factorizable) |
| Shared-event classical correlation | **DERIVED** (MI > 0, diagonal-separable) |
| Single-DOF interference κ | **DERIVED** as an OBSERVABLE, NOT an entangler |
| Synchronization / resonance locking (unequal modes) | **ABSENT / BOUNDARY** |
| Genuine entanglement from canonical D96 | **REFUTED / ABSENT** |
| Observed entanglement | **CORRESPONDENCE / BOUNDARY** (needs NEW sector) |

## Conclusion

Canonical D96 yields only correlation — genuine Bell-type entanglement is ABSENT
(success criterion A). Two sectors meet only through shared classical events and the
single-DOF interference intensity; neither creates a coherent joint amplitude with
Schmidt rank ≥ 2. Bell states require a joint two-sector preparation (the "joint link
state" QG71 classifies as a NEW SECTOR). No new primitive; canonical AT unchanged.
