# ResearchY-D_031 — Seed-Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_031 (permanent)
**Title:** Seed-Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_031.md`
**Depends on:** ResearchY-D_020 (selection precondition), D_021 (oscillation symmetry),
D_029 (closure-defect), D_030 (octave-rung)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_031_Tests.cs`

---

## Purpose

**Why does everything begin with a period-3 seed?** D_030 showed the octave ladder
(n = p·2^k) is derived, with the seed period p as the remaining input. This audit asks
whether p=3 itself is derived or the final boundary assumption.

## Accepted (from D_020, D_021, D_029, D_030)

- The octave rung n = p·2^k is DERIVED; the seed period p is the remaining input
  (D_030).
- Complete Z2 pairing (0 unpaired modes) is the observable-sector INPUT (D_020/D_021).
- The seed half-shift requires p | n/2 (QG159/160).

---

## 1. Seed scan: natural octave-rung size

Each seed period p has a natural octave-rung size n = p·2^k in the 3-family window
[60, 120):

| p | natural n | unpaired | complete Z2 | 6|n | converges | defects |
|---|---|---|---|---|---|---|
| 2 | 64 | 1 | **NO** | no | yes | 2 |
| **3** | **96** | **0** | **YES** | **yes** | **yes** | **0** |
| 4 | 64 | 1 | NO | no | yes | 2 |
| 5 | 80 | 1 | NO | no | yes | 2 |
| 6 | 96 | 3 | NO | yes | **NO** | — |

(Verified via the canonical `Period3SeedOrigin` — p=2/4→64, p=5→80 with 1 unpaired;
p=6→96 fails convergence; p=3→96 complete and selected. Classification: INEVITABLE.)

**p=3 is the UNIQUE period whose natural octave-rung size has complete Z2 pairing (0
unpaired) AND converges.**

---

## 2. Does p=3 uniquely minimize structural inconsistency?

**YES.** The defect count at the natural size:

| p | natural n | unpaired | 6|n | 3 families | defects |
|---|---|---|---|---|---|
| 2 | 64 | 1 | no | yes | 2 |
| **3** | **96** | **0** | **yes** | **yes** | **0** |
| 4 | 64 | 1 | no | yes | 2 |
| 5 | 80 | 1 | no | yes | 2 |
| 6 | 96 | 3 | yes | yes | (fails convergence) |

Only p=3 gives a zero-defect, converging natural size.

---

## 3. Is p=3 selected by…?

| Candidate | Verdict |
|---|---|
| A) oscillation | PARTIAL — the oscillation gives the Z2 pairing; the pairing completeness is what selects p=3 |
| B) pairing completeness | **YES** — the 0-unpaired requirement (weak-isospin doublets) selects p=3 uniquely |
| C) closure | PARTIAL — convergence excludes p=6 (density ≤ 1/6), but closure alone doesn't select p=3 |
| D) information structure | NO — the family count is satisfied by several periods |
| E) none | NO |

**p=3 is selected by pairing completeness (B) together with convergence (C-partial).**

---

## 4. Remove p=3: what breaks first?

| Removed | What breaks |
|---|---|
| p=3 → p=2 | the natural size 64 has 1 unpaired mode — incomplete Z2 doublets |
| p=3 → p=4 | same (64, 1 unpaired) |
| p=3 → p=5 | same (80, 1 unpaired) |
| p=3 → p=6 | convergence fails (density 1/6) |

**The pairing completeness breaks first** — any other converging period gives 1 unpaired
mode (incomplete weak-isospin doublets).

---

## 5. The minimal principle generating p=3

The seed period p=3 is the **smallest period whose natural octave-rung size has complete
Z2 pairing and converges**:

```
p=1:  natural n=64, unpaired=1
p=2:  natural n=64, unpaired=1
p=3:  natural n=96, unpaired=0   ← smallest complete
p=4:  natural n=64, unpaired=1
p=5:  natural n=80, unpaired=1
p=6:  natural n=96, but fails convergence
```

**p=3 is the MINIMAL period satisfying the complete-pairing requirement.**

---

## Theorem

> **Theorem (D_031).** p=3 is DERIVED from pairing completeness + convergence, not a
> final boundary assumption. The complete-Z2-pairing requirement (0 unpaired modes,
> weak-isospin doublets, D_020) applied to the natural octave-rung size n = p·2^k
> selects p=3 uniquely: p=2/4→64 and p=5→80 have 1 unpaired mode (incomplete), p=6→96
> fails convergence (density ≤ 1/6), and p=3→96 has 0 unpaired and converges. p=3 is
> the minimal period satisfying the requirement. The complete-pairing requirement is
> itself the D_020 observable-sector INPUT — so p=3 is DERIVED (from pairing +
> convergence), while the pairing requirement is BOUNDARY.
>
> *Proof sketch.* (1) The natural octave-rung size of period p is n = p·2^k in [60,120):
> p=2/4→64, p=3→96, p=5→80, p=6→96 (Section 1, verified via Period3SeedOrigin). (2)
> Complete Z2 pairing requires 0 unpaired modes: only n=96 has it (64 and 80 have 1;
> p=6's 96 has 3) — Sections 1–2. (3) Convergence excludes p=6 (density 1/6, canonical
> QG160) — Section 1. (4) Hence p=3 is the unique period with complete pairing AND
> convergence (Sections 1–4). (5) The pairing requirement is the D_020 observable-sector
> input. Hence p=3 DERIVED; pairing requirement BOUNDARY. ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → observable-sector construction      [BOUNDARY — D_020: complete Z2 pairing]
 → seed period p                        [DERIVED — the minimal period with complete
                                          pairing at the natural size + convergence]
 → 6|N (seed half-shift p | n/2)        [DERIVED]
 → octave ladder n = p·2^k              [DERIVED — D_030]
 → N=96                                 [DERIVED — the unique zero-defect rung]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is p=3 the unique complete period? | **YES** (only p=3 has 0 unpaired at the natural size) |
| Is p=3 the minimal complete period? | **YES** (p=1, 2 are incomplete; p=3 is the first) |
| Does p=6 break convergence? | **YES** (density 1/6, canonical QG160) |
| Is p=3 selected by pairing completeness? | **YES** (the 0-unpaired requirement) |
| Is p=3 the final boundary? | **NO** — the pairing requirement is the boundary |
| What is the boundary? | the observable-sector construction (D_020) |

---

## Counterexamples

1. **p=2, 4 → n=64**: 1 unpaired mode — incomplete Z2 doublets, despite 3 families.
2. **p=5 → n=80**: 1 unpaired mode — incomplete.
3. **p=6 → n=96**: 3 unpaired modes and fails convergence (density 1/6).
4. **p=1**: natural size 64 with 1 unpaired — not complete.

---

## Classification

| Component | Status |
|---|---|
| complete Z2 pairing requirement (0 unpaired) | **BOUNDARY** (observable-sector input, D_020) |
| convergence requirement | **DERIVED** (density threshold, QG160) |
| seed period p=3 | **DERIVED** (minimal period with complete pairing + convergence) |
| 6|N (seed half-shift) | **DERIVED** (from p=3) |
| N=96 | **DERIVED** (octave rung + zero-defect) |

**p=3 is DERIVED from pairing completeness + convergence; the pairing requirement is the
BOUNDARY input (observable sector, D_020).**

---

## Open Problems

1. **Pairing-completeness origin (D_031 OP1).** The 0-unpaired requirement (weak-isospin
   doublets) is the observable-sector input; whether it is derivable from a deeper
   structure is the D_020 boundary question.
2. **Convergence threshold (D_031 OP2).** Why the density threshold is exactly 1/6
   (p=6 fails) — whether it follows from the K=6 connectivity or is an emergent
   dynamical fact — is open.

---

## Next Steps

- **ResearchY-D_032 (or synthesis):** the seed-origin audit completes the N=96 chain
  (Difference → observable sector → p=3 → octave ladder → N=96). A synthesis can map
  the full seed-to-observables structure.
- **D_030 follow-up:** the "p=3 derived from pairing" verdict sharpens D_030 — the seed
  period is not an independent input but the minimal complete-pairing period.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_031_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_031_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_031_SeedScan` | natural sizes p=2/4→64, p=3→96, p=5→80, p=6→96 | ✅ |
| `Y_D_031_PeriodComparison` | only p=3 has 0 unpaired at the natural size | ✅ |
| `Y_D_031_PairingCompleteness` | p=2/4/5 incomplete (1 unpaired); p=3 complete | ✅ |
| `Y_D_031_DefectCount` | p=3 natural size is the only zero-defect one | ✅ |
| `Y_D_031_DependencyTrace` | Difference → observable sector → p=3 → octave → N=96 | ✅ |
| `Y_D_031_Run` | Research report | ✅ |

**Conclusion:** p=3 is **DERIVED from pairing completeness + convergence**, not a final
boundary assumption. The complete-Z2-pairing requirement (0 unpaired modes, weak-isospin
doublets, D_020) applied to the natural octave-rung size n = p·2^k selects p=3 uniquely:
p=2/4→64 and p=5→80 have 1 unpaired (incomplete), p=6→96 fails convergence (density
1/6), and p=3→96 has 0 unpaired and converges. p=3 is the minimal complete period. The
pairing requirement is itself the D_020 observable-sector input — so p=3 is DERIVED,
while the pairing requirement is BOUNDARY. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_031"`

---

## References

- ResearchY-D_020 (selection precondition), D_021 (oscillation symmetry), D_029
  (closure-defect), D_030 (octave-rung).
- AT-QG: QG159 (D96 selection), QG160 (period-3 seed origin — INEVITABLE).
- Monograph V2.0: Ch3 (actualization), Ch4 (closure), Ch6 (D96 spectrum).
- `AT.Core/ResearchXH/Period3SeedOrigin.cs` (canonical candidate discrimination).
