# Y_M_014_Result.md — ResearchY-M_014 Automatic Ring-Size Rank Scan Audit

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_014_Tests.cs`
**Run:** 2026-09-08
**Result:** ✅ 503/503 PASSED (497 exhaustive N-cases + 6 facts)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_014"`

---

## Summary

**Question:** Which ring size does the D96 attractor occupy when every N in [16,512] is
evaluated AUTOMATICALLY, with NO manual candidate selection?

**Verdict:** **N = 96 is the unique Score-4 maximizer over ALL 497 rings in [16,512]** under
the explicit canonical score Score(N) = [0 unpaired] + [3 families ∧ span < 8] + [6|N] +
[N = 3·2^k]. Prior selection audits (D_029 window [32,300], D_030 octave rungs, D_031
natural sizes) used hand-picked candidates; M_014 removes all preselection and finds 96
by exhaustive ranking alone. Score-3 = seed-3 rungs at wrong family count (24, 48, 192,
384) + the D_029 zero-defect rings {60..120}\{96}. Drop analysis: removing the 3-family
window admits the 5 rungs; removing the octave rung admits the 11 zero-defect rings;
removing pairing or 6-divisibility leaves 96 unique.

## Rank table (Score ≥ 3 candidates)

| N | Score | A B C D | Families | #Eigen | Multiplicity pattern |
|---|---|---|---|---|---|
| 96 | **4** | 1 1 1 1 | 3 | 44 | {2×42, 5×1, 6×1} |
| 24 | 3 | 1 0 1 1 | 1 | 8 | {2×6, 5×1, 6×1} |
| 48 | 3 | 1 0 1 1 | 2 | 20 | {2×18, 5×1, 6×1} |
| 60 | 3 | 1 1 1 0 | 3 | 26 | {2×24, 5×1, 6×1} |
| 66 | 3 | 1 1 1 0 | 3 | 31 | {2×30, 5×1} |
| 72 | 3 | 1 1 1 0 | 3 | 32 | {2×30, 5×1, 6×1} |
| 78 | 3 | 1 1 1 0 | 3 | 32 | {2×30, 5×1, 12×1} |
| 84 | 3 | 1 1 1 0 | 3 | 32 | {2×30, 11×1, 12×1} |
| 90 | 3 | 1 1 1 0 | 3 | 43 | {2×42, 5×1} |
| 102 | 3 | 1 1 1 0 | 3 | 49 | {2×48, 5×1} |
| 108 | 3 | 1 1 1 0 | 3 | 50 | {2×48, 5×1, 6×1} |
| 114 | 3 | 1 1 1 0 | 3 | 55 | {2×54, 5×1} |
| 120 | 3 | 1 1 1 0 | 3 | 56 | {2×54, 5×1, 6×1} |
| 192 | 3 | 1 0 1 1 | 4 | 92 | {2×90, 5×1, 6×1} |
| 384 | 3 | 1 0 1 1 | 5 | 188 | {2×186, 5×1, 6×1} |

Score distribution over all 497 rings: {4:1, 3:14, 2:101, 1:255, 0:126}.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_M_014_ExhaustiveRange` (theory ×497) | every N = 16..512 is scored (no manual selection) | ✅ |
| `Y_M_014_UniqueTop` | N = 96 unique Score-4 maximizer | ✅ |
| `Y_M_014_RankTable` | score distribution {4:1, 3:14, 2:101, 1:255, 0:126} | ✅ |
| `Y_M_014_DropAnalysis` | drop B → 5 rungs; drop D → 11 zero-defect; drop A/C → 96 unique | ✅ |
| `Y_M_014_CanonicalRow` | N=96: score 4; 44 eigenvalues; {2×42,5×1,6×1}; span 6.403 | ✅ |
| `Y_M_014_Score3Sets` | score-3 = rungs at wrong family + zero-defect \ {96} | ✅ |
| `Y_M_014_Run` | research report | ✅ |

## Conclusion

The canonical D96 selection is reproducible by an exhaustive automated scan over every
integer N ∈ [16,512]: N = 96 is the unique global maximizer of the four-criterion
composite score, with no manual candidate selection anywhere in the procedure. This
confirms D_029 (zero-defect set), D_030 (octave rung discriminator) and D_031 (period-3
seed → 96) and removes the concern that those results depended on hand-picked windows or
ladders. No reclassification; no new primitive; canonical AT unchanged.
