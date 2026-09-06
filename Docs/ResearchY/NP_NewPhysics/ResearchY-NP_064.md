# ResearchY-NP_064 — Canonical Structure Necessity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_064 (permanent)
**Title:** Canonical Structure Necessity Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_064.md`
**Depends on:** ResearchY-NP_055–NP_063 (the dark-energy arc), AT-QG QG234 (ΩΛ = I_occ/ln K),
D_020/D_031/D_040 (period-3 seed, N=96 octave rung, classification registry), D_028 (span),
D_041 (spectrum), A_003/D_030 (occupancy [4,4,87]), QG_013 (3-family window), NP_037
(removal analysis), S_001/R_001 (boundary set), QG_011 (discrete tick)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_064_Tests.cs`

---

## Purpose

NP_055–063 established ΩΛ = 0.6839 is a derived information observable whose physical
(energetic) import is hosted. NP_064 asks the deepest structural question: **why does the
canonical structure {N = 96, K = 3, occupancy [4,4,87]} exist?** Program: (1) remove N=96;
(2) remove K=3; (3) remove [4,4,87]; (4) measure impact on ΩΛ, families, masses, couplings,
and the information chain; (5) determine derived / selected / boundary / accidental; (6) locate
the deepest reason the canonical structure exists. **Success criterion:** identify the true root
from which ΩΛ = 0.6839 emerges. No new primitives; canonical AT unchanged.

---

## 1. Inventory — the canonical structure and its derivation status

| Structure | Value | Status |
|---|---|---|
| period-3 seed p = 3 | unique complete-pairing period; 6\|N | **DERIVED** (D_031/D_040, BOUNDARY→DERIVED) |
| octave rung | N = 3·2^k (factor-3 × power of 2) | **DERIVED** (given the seed) |
| **N = 96** | 3·2⁵ = 96 | **DERIVED** (given the seed + the window) |
| **3-family window** | span ∈ [4, 8) | **BOUNDARY** (D_028/D_040; anchored to ΩΛ_obs, QG_013) |
| **K = 3** | floor(log₂ span)+1 = 3 at span 6.40 | **VALUE DERIVED** / **WINDOW BOUNDARY** (two-level rule) |
| **occupancy [4, 4, 87]** | octave binning of the N=96 spectrum | **DERIVED** (A_003/D_030) |

**The structure is a chain: a DERIVED seed and rung, a BOUNDARY window, and DERIVED
occupancy.**

---

## 2. The derivation chain — why N=96 exists

The circulant ring C_N(±1..±6) has span s(N) that grows with N. Among the octave rungs
N = 3·2^k (forced by the period-3 seed), the spans are:

| rung k | N | span | families (floor(log₂ span)+1) | in [4,8)? |
|---|---|---|---|---|
| 4 | 48 | 3.2396 | 2 | NO |
| **5** | **96** | **6.4025** | **3** | **YES** |
| 6 | 192 | 12.7791 | 4 | NO |
| 7 | 384 | 25.5369 | 5 | NO |

**The 3-family window [4, 8) uniquely selects the rung k = 5, i.e. N = 96.** The period-3
seed fixes N = 3·2^k; the window fixes k = 5. Together they pin N = 96, which then gives
span 6.40 → K = 3 → occupancy [4, 4, 87] → I_occ = 0.7513 → ΩΛ = 0.6839.

---

## 3. Removal analysis

| Remove | What breaks | What survives |
|---|---|---|
| **N = 96** (→ 48 or 192) | ΩΛ (0.5801/0.7295 vs 0.6839; corpus rung ladder 0.4773/0.8153); family count (2/4 vs 3); mass content (m_μ/m_e = 102.3/416.3 vs 206.77, NP_037 R2) | the spectrum form λ_k = 2−2cos(2πk/N); the information chain (with a *different* ΩΛ) |
| **K = 3** (→ 2 or 4) | ΩΛ (0.5801/0.6263, NP_061); the denominator ln K | I_occ (a fixed count) |
| **[4, 4, 87]** (perturb) | ΩΛ (0.6555/0.8145, NP_061); I_occ | the ring structure; N=96; K=3 |

**Every removal breaks ΩΛ.** The structure is load-bearing: ΩΛ = 0.6839 is *uniquely* tied to
the specific triple {N=96, K=3, [4,4,87]} (consistent with NP_061's fragile point-match).

---

## 4. Derived / selected / boundary / accidental?

| Structure | Classification |
|---|---|
| period-3 seed | **DERIVED** (D_040) |
| N = 96 (the rung) | **DERIVED** (given the seed + window) |
| 3-family window [4,8) | **BOUNDARY** (anchored to ΩΛ_obs, QG_013) |
| K = 3 (the value) | **DERIVED** (given N=96); window **BOUNDARY** |
| occupancy [4,4,87] | **DERIVED** (given N=96) |
| "selected" / "accidental" | **NO.** Nothing is a dynamical selection or an accident; the structure is the deterministic consequence of the seed + window. |

**The canonical structure is a DERIVED-BOUNDARY hybrid:** the seed (DERIVED) and the window
(BOUNDARY) jointly determine N=96; everything downstream (K=3, [4,4,87], I_occ, ΩΛ) is DERIVED.

---

## 5. The deepest reason — the true root

```
discrete actualization tick                        [BOUNDARY — QG_011, deepest single boundary]
   → circulant spectrum λ_k = 2 − 2cos(2πk/N)      [DERIVED — D_041]
   → period-3 seed p = 3 (complete pairing, 6|N)   [DERIVED — D_031/D_040]
   → octave rung N = 3·2^k                          [DERIVED]
   → 3-family window [4,8)  →  k = 5  →  N = 96     [BOUNDARY window]
   → span 6.4025 → K = 3                            [DERIVED value]
   → occupancy [4, 4, 87]                           [DERIVED — A_003/D_030]
   → I_occ = 0.7513 → ΩΛ = 0.6839                   [DERIVED — QG228/QG234]
```

**The true root from which ΩΛ = 0.6839 emerges is: the period-3 seed (DERIVED) combined with
the 3-family window [4,8) (BOUNDARY), which together pin N = 96.** The period-3 seed is the
deepest *derived* structure (it forces the factor 3 in N); the 3-family window is the deepest
*irreducible* input on the path to ΩΛ (it selects k=5, and is itself anchored to the observed
ΩΛ). Below them sits the discrete tick (QG_011), the theory's deepest single boundary.

---

## Theorem

> **Theorem (NP_064).** The canonical structure {N=96, K=3, [4,4,87]} exists because the
> period-3 seed (DERIVED) forces N = 3·2^k and the 3-family window [4,8) (BOUNDARY) selects
> k = 5, pinning N = 96; the occupancy [4,4,87], I_occ = 0.7513, and ΩΛ = 0.6839 all follow
> DERIVED from N = 96. Proof: (1) Inventory (Section 1): seed DERIVED, rung DERIVED, window
> BOUNDARY, K value DERIVED/window BOUNDARY, occupancy DERIVED. (2) Chain (Section 2,
> verified): among octave rungs 3·2^k, only k=5 (N=96) has span 6.4025 ∈ [4,8) (k=4 → 3.24,
> 2 families; k=6 → 12.78, 4 families). (3) Removal (Section 3): removing N=96, K=3, or
> [4,4,87] breaks ΩΛ (0.4773/0.8153, 0.5801/0.6263, 0.6555/0.8145 respectively). (4)
> Classification (Section 4): a DERIVED-BOUNDARY hybrid, not a selection or accident.
> (5) Deepest root (Section 5): the period-3 seed (DERIVED) × the 3-family window (BOUNDARY) →
> N=96 → ΩΛ; below them, the discrete tick (QG_011). Classification: period-3 seed and N=96
> DERIVED (D_040); the 3-family window BOUNDARY (anchored to ΩΛ_obs); K=3 value DERIVED /
> window BOUNDARY (two-level rule, D_040); occupancy [4,4,87] and ΩΛ DERIVED; dynamical
> selection / accident REFUTED. **Success criterion: ΩΛ = 0.6839 ultimately emerges from the
> period-3 seed (derived) constrained by the 3-family window (boundary) — the two inputs that
> pin N=96, from which everything else follows DERIVED.** No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Inventory the structure. (2) Show the rung+window uniquely pin N=96.
> (3) Remove each element. (4) Classify. (5) Locate the root. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "N=96 is arbitrary" | it is the unique octave rung 3·2^k with span in [4,8) (k=4 → 2 families, k=6 → 4 families) |
| "the structure is accidental" | it is the deterministic consequence of the seed + window, with no free parameter |
| "the window is derived" | QG_013/D_040: the 3-family window [4,8) is BOUNDARY (anchored to observed ΩΛ) |
| "ΩΛ survives removing the structure" | every removal (N, K, occupancy) breaks ΩΛ |
| "N=96 has no derivation" | N=96 = 3·2⁵ is DERIVED (D_040: BOUNDARY→DERIVED) given the seed + window |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| the window uniquely selects N=96 | another octave rung 3·2^k with span in [4,8) (3 families) |
| the period-3 seed is derived | a pairing-complete rung without the factor-3 seed |
| the 3-family window is boundary | a derivation of [4,8) from Difference alone (without anchoring to ΩΛ) |
| ΩΛ is tied to the triple | an ΩΛ = 0.6839 from a different {N, K, occupancy} |

---

## 8. Classification

| Component | Status |
|---|---|
| period-3 seed p = 3 | **DERIVED** (D_040) |
| N = 96 = 3·2⁵ (octave rung) | **DERIVED** (given seed + window) |
| 3-family window [4,8) | **BOUNDARY** (anchored to ΩΛ_obs, QG_013) |
| K = 3 (the value) | **DERIVED** (given N=96); window **BOUNDARY** |
| occupancy [4,4,87] | **DERIVED** (A_003/D_030) |
| I_occ = 0.7513, ΩΛ = 0.6839 | **DERIVED** (QG228/QG234) |
| dynamical selection / accident | **REFUTED** |

**Conclusion.** The canonical structure {N=96, K=3, [4,4,87]} exists as a **DERIVED-BOUNDARY
hybrid**: the period-3 seed (DERIVED) forces N = 3·2^k, and the 3-family window [4,8) (BOUNDARY)
selects k = 5 — together pinning N = 96. From N=96, everything downstream is DERIVED: span
6.40 → K=3 → occupancy [4,4,87] → I_occ = 0.7513 → ΩΛ = 0.6839. **The true root from which
ΩΛ = 0.6839 emerges is the period-3 seed (derived) constrained by the 3-family window
(boundary); below them, the discrete tick is the theory's deepest single boundary.** The
structure is not arbitrary, not selected, and not accidental — it is the deterministic
consequence of one derived seed and one boundary window. No new primitive; canonical AT
unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_064_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_064_Period3SeedAndRung` | N=96 = 3·2⁵; 6\|N | ✅ |
| `Y_NP_064_WindowUniquelySelectsN96` | only rung k=5 has span ∈ [4,8) | ✅ |
| `Y_NP_064_OccupancyAndOmegaL` | [4,4,87] → ΩΛ = 0.6839 | ✅ |
| `Y_NP_064_RemovalImpact` | N/K/occupancy removal breaks ΩΛ | ✅ |
| `Y_NP_064_Classification` | seed/rung DERIVED; window BOUNDARY | ✅ |
| `Y_NP_064_DeepestRoot` | seed (derived) × window (boundary) → N=96 | ✅ |
| `Y_NP_064_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_064"`

---

## References

- ResearchY-NP_055–NP_063 (dark-energy arc), NP_037 (removal analysis).
- AT-QG: QG234 (ΩΛ = I_occ/ln K), QG228 (I_occ), QG011 (discrete tick).
- ResearchY: D_020/D_031/D_040 (period-3 seed, N=96 octave rung, classification registry),
  D_028 (span), D_041 (spectrum), A_003/D_030 (occupancy [4,4,87]), QG_013 (3-family window),
  S_001/R_001 (boundary set).
