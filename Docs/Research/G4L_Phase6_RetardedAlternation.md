# G4-L Phase 6 — Retarded Alternation

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 6 — reduce the Feynman tail at its source
**Status:** COMPLETED — 3/3 xUnit tests pass

---

## 1. Goal

H2 = R1 + L3 leaks because its symmetric part L3 = R1 + R2 couples equally to past and future.
Test four partially-retarded alternating operators that down-weight the FUTURE (symmetric)
contribution, preserving the indefinite spectrum while reducing leakage.

| ID | Operator | Construction |
|---|---|---|
| A1 | lower-triangular alternation | R1 (past only) |
| A2 | causally weighted alternation | R1 + ½·R2 |
| A3 | retarded interval alternation | past full (−1)^(k+1), future (−1)^(k+1)/(k+1) |
| A4 | hybrid alternating-retarded | R1 + ½·L3 |

---

## 2. Results (δ-source, N = 72)

| operator | leakage | directionality | (n+, n−) | indefinite | KS→BDG | alternates |
|---|---|---|---|---|---|---|
| H2 (baseline) | 0.759 | 0.626 | (31, 41) | ✅ | 0.1389 | ✅ |
| A1 lower-tri | 0.569 | 1.000 | (38, 34)* | (nilpotent) | 0.5972 | ❌‡ |
| A2 causal-wtd | 0.759 | 0.626 | (31, 41) | ✅ | 0.2917 | ✅ |
| **A3 interval-wtd** | **0.669** | **0.720** | (31, 41) | ✅ | 0.3472 | ✅ |
| A4 hybrid | 0.750 | 0.736 | (31, 41) | ✅ | 0.2639 | ✅ |

\* A1's (38, 34) split is a numerical artifact — R1 is strictly triangular (nilpotent, true
spectrum all 0). ‡ its alternation lives in the lower triangle, which the symmetric layer-profile
sampling does not see.

Refinement (A3): leak 0.669 (N=72) → 0.589 (N=110), indefinite + alternating preserved.

---

## 3. Success criteria

| Criterion | A3 (interval-weighted) |
|---|---|
| leakage < H2 | ✅ 0.669 < 0.759 |
| preserve indefiniteness | ✅ (31+, 41−) |
| preserve alternation | ✅ |
| refinement stability | ✅ (N=72 → 110) |

---

## 4. Conclusion

**YES.** Down-weighting the future layer contribution — the **interval-weighted alternation (A3)**,
with past layers at full weight and future layers decaying as 1/(k+1) — reduces the Feynman tail
(0.759 → 0.669) while preserving the indefinite spectrum (31+, 41−) and layer alternation, and is
refinement-stable. A1 (pure lower-triangular) reduces leakage most (0.569) but is genuinely
nilpotent; A2 (uniform future down-weight) has no effect; A4 (hybrid) is marginal.

**Honest caveat:** the reduction is ~12 %, not a full suppression — the residual tail is the
irreducible symmetric remnant of the alternating operator. Full causality would require the BDG
diagonal (−2) and a purely retarded structure, both outside this constraint.

---

## Test program

| Test | Verdict |
|---|---|
| G4-L60 `G4_L60_PartialRetardationReducesLeakage` | PASS (3/4 reduce leakage) |
| G4-L61 `G4_L61_PreservesIndefinitenessAlternation` | PASS (A3/A4 satisfy all) |
| G4-L62 `G4_L62_StableUnderRefinement` | PASS (A3 stable at N=72, 110) |

Code: `AT.Core/ResearchXH/LorentzianOperator.cs` (added `Scale`, `IntervalWeightedAlternation`);
tests `AT.Tests/ResearchXH/G4L_Phase6_RetardedAlternationTests.cs`.
