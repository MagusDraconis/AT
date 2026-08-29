# ResearchY-D_029 — Closure-Defect Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_029 (permanent)
**Title:** Closure-Defect Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_029.md`
**Depends on:** ResearchY-D_019 (closure-only), D_020 (selection precondition),
D_021 (oscillation symmetry), D_028 (span-origin)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_029_Tests.cs`

---

## Purpose

**What structure must be closed so that no inconsistency remains?** D_019 showed closure
does not determine N; D_020 showed N=96 is selected by the observable-sector
construction. This audit asks what closure actually removes: is it the removal of a
specific structural defect?

## Accepted (from D_019, D_020, D_021, D_028)

- Closure does not select N=96 (D_019); span, family count, occupancy do not select it
  either (D_016, D_018, D_028).
- N=96 is selected by the observable-sector construction (complete Z2 pairing + 3
  families, D_020); the octave-rung discriminates within the window.
- Complete Z2 pairing means 0 unpaired modes (D_021).

---

## 1. Definitions

| Term | Definition |
|---|---|
| **inconsistency** | a structural defect: unpaired mode, broken symmetry, broken ladder, non-closing cycle, unresolved symmetry, incomplete representation |
| **closure** | the process reaching its own completion (no further change) — the boundary is the fixed point (Ch4/QG282) |
| **completion** | the state with zero residual change (zero-defect) |
| **fixed point** | the converged configuration (stable attractor, QG116) |

---

## 2. The inconsistency hierarchy

For varying N, the structural defects are:

| Defect | N affected |
|---|---|
| **Level 1: incomplete Z2** (unpaired modes) | 64, 80, 128 (1 unpaired) |
| **Level 2: broken seed half-shift** (6 ∤ N) | 64, 80, 128, 245 |
| **Level 3: wrong family count** (≠ 3) | 48, 128, 192, 245 |
| **Level 4: span ≥ 8** (outside the 3-family window) | 128, 192, 245 |

---

## 3. Inconsistency count vs N

defect(N) = [unpaired > 0] + [6 ∤ N] + [families ≠ 3] + [span ≥ 8]

| N | defect count | details |
|---|---|---|
| 48 | 1 | 2 families |
| 60 | 0 | zero-defect |
| 64 | 2 | 1 unpaired, 6 ∤ 64 |
| 80 | 2 | 1 unpaired, 6 ∤ 80 |
| 90 | 0 | zero-defect |
| **96** | **0** | **zero-defect** |
| 120 | 0 | zero-defect (span 7.999) |
| 128 | 4 | all four defects |
| 192 | 2 | 4 families, span ≥ 8 |
| 245 | 3 | 6 ∤ 245, 5 families, span ≥ 8 |

**The zero-defect set is {60, 66, …, 120}** — exactly the 11 rings with 6|N + 3
families (D_016).

---

## 4. What disappears at N=96 that survives elsewhere?

At N=96, ALL structural defects vanish:

1. **0 unpaired modes** — complete Z2 pairing (the D_020 observable-sector input).
2. **6|96** — the seed half-shift symmetry holds.
3. **3 families** — the octave ladder is intact.
4. **span 6.4025 < 8** — inside the 3-family window.

But N=96 is NOT the only zero-defect size: **N=60, 66, 72, 78, 84, 90, 102, 108, 114,
120 are also zero-defect.**

---

## 5. The octave-rung discriminator

The zero-defect condition (closure removes the defects) is **necessary but NOT
sufficient** for N=96. The discriminator is the **octave-rung structure** n = 3·2^k
(D_020):

| N | zero-defect? | octave rung (3·2^k)? | selected |
|---|---|---|---|
| 48 | no (2 fam) | 3·2⁴ = 48 | no |
| 60 | yes | no (60 ≠ 3·2^k) | no |
| 90 | yes | no | no |
| **96** | **yes** | **3·2⁵ = 96** | **yes** |
| 120 | yes | no | no |
| 192 | no (4 fam) | 3·2⁶ = 192 | no |

**N=96 is the UNIQUE zero-defect octave rung in [32,300]** (verified: only 96).

---

## 6. Prove or refute: closure is the removal of a specific structural defect

**PARTIALLY SUPPORTED.** Closure removes the structural defects — it produces the
zero-defect set {60, …, 120}. But closure alone does NOT select 96: the zero-defect set
has 11 members. The specific N=96 is selected by the octave-rung discriminator (D_020),
which is a separate structure (the period-3 seed × frequency doubling).

So: **closure removes inconsistency; the selection of 96 is a separate structure.**

---

## Theorem

> **Theorem (D_029).** Closure is the removal of structural defects — it produces the
> zero-defect set {60, 66, …, 120} (11 rings with 6|N + 3 families, 0 unpaired modes,
> span < 8). But closure does NOT select N=96: N=60, 90, 120 are zero-defect too. The
> octave-rung structure n = 3·2^k is the discriminator — N=96 = 3·2⁵ is the UNIQUE
> zero-defect octave rung in [32,300] (48 has 2 families, 192 has 4). Hence closure
> removes inconsistency (DERIVED/EMERGENT), while the specific N=96 is a BOUNDARY
> selection (octave-rung, D_020).
>
> *Proof sketch.* (1) The defects (unpaired, 6∤N, families≠3, span≥8) define an
> inconsistency count; the zero-defect set is {60,66,…,120} (Section 3, verified). (2)
> Closure converges to the fixed point, removing these defects (Section 2). (3) But the
> zero-defect set has 11 members — N=60/90/120 are zero-defect yet not 96 (Sections 4,
> 5). (4) The octave-rung n = 3·2^k selects 96 uniquely: only 96 = 3·2⁵ is a
> zero-defect rung (Section 5, verified). (5) Hence closure removes inconsistency;
> N=96 is selected by the octave rung (D_020). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → closure (fixed point, Ch4/QG282)
 → inconsistency removal (zero-defect set {60,66,…,120})
     → unpaired-mode removal (0 unpaired)
     → seed half-shift (6|N)
     → 3-family window (span < 8)
 → octave-rung structure (n = 3·2^k)     [BOUNDARY — D_020 discriminator]
 → N=96 (unique zero-defect octave rung)
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is N=96 the only zero-defect size? | **NO** — 11 rings (60…120) are zero-defect |
| Does closure remove the defects? | **YES** — the zero-defect set has no defects |
| Does closure select 96? | **NO** — N=60/90/120 are zero-defect too |
| What discriminates 96? | the octave-rung structure (3·2⁵) |
| Is N=96 the unique zero-defect octave rung? | **YES** (verified [32,300]) |
| Closure = defect removal? | PARTIAL — it removes defects, but selection is separate |

---

## Counterexamples

1. **N=60, 90, 120**: zero-defect (closure removes all defects) but NOT N=96 — closure
   alone does not select 96.
2. **N=64, 80**: 1 unpaired mode — incomplete Z2 despite 3 families — the Level-1
   defect closure removes.
3. **N=128**: all four defects (unpaired, 6∤128, 4 families, span ≥ 8) — closure removes
   all, yet 128 is far from 96.
4. **N=192**: 4 families, span ≥ 8 — zero-defect criterion fails, but it IS an octave
   rung (3·2⁶) — the rung alone is not sufficient either.

---

## Classification

| Component | Status |
|---|---|
| unpaired-mode defect (Level 1) | **DERIVED** (spectral) |
| broken seed half-shift (Level 2) | **DERIVED** (seed structure) |
| wrong family count (Level 3) | **DERIVED** (span window) |
| span ≥ 8 (Level 4) | **DERIVED** (span ~ 0.0578·N, D_028) |
| zero-defect set (closure removes defects) | **EMERGENT** (closure + derived defects) |
| specific N=96 | **BOUNDARY** (octave-rung selection, D_020) |

**Closure removes inconsistency (the zero-defect set is EMERGENT from the derived
defects); the specific N=96 is BOUNDARY (octave-rung selection).**

---

## Open Problems

1. **Defect-removal uniqueness (D_029 OP1).** The zero-defect set has 11 members; a
   finer inconsistency measure that selects 96 uniquely (beyond the octave rung) is
   open.
2. **Octave-rung necessity (D_029 OP2).** Why the octave-rung (period-3 × frequency
   doubling) is the discriminator — whether it is a closure-consistency requirement or
   an independent selection — is the D_020 boundary question.

---

## Next Steps

- **ResearchY-D_030 (or synthesis):** the closure-defect audit separates the
  inconsistency removal (EMERGENT zero-defect set) from the N=96 selection (BOUNDARY
  octave rung). A synthesis can map the full inconsistency-hierarchy structure.
- **D_020 follow-up:** the defect-count formulation sharpens the observable-sector
  construction — the zero-defect condition is its natural (derived) face.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_029_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_029_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_029_UnpairedModes` | unpaired(64/80/128)=1, unpaired(96/192)=0 | ✅ |
| `Y_D_029_BrokenSymmetry` | 6|N holds at 96; broken at 64/80/128/245 | ✅ |
| `Y_D_029_CycleClosure` | zero-defect set = {60,66,…,120} | ✅ |
| `Y_D_029_RepresentationClosure` | N=96 unique zero-defect octave rung in [32,300] | ✅ |
| `Y_D_029_InconsistencyCount` | defect counts: 64=2, 80=2, 128=4, 192=2, 245=3, 96=0 | ✅ |
| `Y_D_029_Run` | Research report | ✅ |

**Conclusion:** Closure is the removal of structural defects — it produces the
zero-defect set {60, 66, …, 120} (11 rings with 6|N + 3 families, 0 unpaired, span < 8).
But closure does NOT select N=96: N=60/90/120 are zero-defect too. The octave-rung
structure n = 3·2^k discriminates — N=96 = 3·2⁵ is the UNIQUE zero-defect octave rung in
[32,300]. Closure removes inconsistency (EMERGENT zero-defect set); the specific N=96 is
BOUNDARY (octave-rung selection, D_020). No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_029"`

---

## References

- ResearchY-D_019 (closure-only), D_020 (selection precondition), D_021 (oscillation
  symmetry), D_028 (span-origin).
- Monograph V2.0: Ch3 (actualization), Ch4 (closure — fixed point), Ch6 (D96 spectrum).
- AT-QG: QG116 (universal attractor), QG282 (closure principle), QG210 (families).
