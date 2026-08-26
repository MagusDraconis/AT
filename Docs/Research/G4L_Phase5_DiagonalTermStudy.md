# G4-L Phase 5 — Diagonal Self-Term Study

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 5 — the role of the diagonal self-term in retarded Lorentzian operators
**Status:** COMPLETED — 3/3 xUnit tests pass

---

## 1. Goal

H2 = R1 + L3 has a large Feynman tail (leakage ≈ 0.76) because its retarded component R1 is
nilpotent (no diagonal). Test four **native** diagonals (no BDG coefficients) for whether they
suppress the tail while preserving retardation, indefiniteness, and alternation.

| ID | Diagonal | Source |
|---|---|---|
| D1 | constant −1 | uniform self-coupling |
| D2 | −(comparable count) | density over the full causal order |
| D3 | −(past count) | layer count |
| D4 | −(local degree) | link count |

---

## 2. Results (δ-source, N = 72)

| operator | leakage | directionality | indefinite | KS→BDG | alternates |
|---|---|---|---|---|---|
| H2 (baseline) | 0.759 | 0.626 | ✅ | 0.1389 | ✅ |
| D1 constant | 0.759 | 0.663 | ✅ | 0.1250 | ✅ |
| D2 comparable | **0.473** | 0.716 | ❌ | 0.9167 | ✅ |
| D3 past-count | 0.890 | 0.307 | ✅ | 0.5000 | ✅ |
| **D4 degree** | **0.697** | **0.703** | ✅ | 0.3056 | ✅ |

Constant-diagonal strength sweep (s = 0…8): leakage never drops below 0.717; indefiniteness is
preserved for all s. Refinement (N = 72 → 110): leakage-reduction + indefiniteness + alternation
all persist.

---

## 3. Success criteria

| Criterion | D4 (degree) | D2 (comparable) |
|---|---|---|
| reduce leakage | ✅ 0.697 < 0.759 | ✅ 0.473 (best) |
| preserve retardation | ✅ 0.703 > 0.5 | ✅ 0.716 |
| preserve indefiniteness | ✅ | ❌ (over-suppresses) |
| preserve alternation | ✅ | ✅ |

**D4 (local-degree diagonal) satisfies all four criteria** — it reduces leakage, increases
retardation (0.626 → 0.703), and preserves indefiniteness and alternation.

---

## 4. Conclusion

A native diagonal **can** suppress the Feynman tail: the **local-degree diagonal (D4)** is the
successful native self-term — it reduces leakage (0.759 → 0.697) and increases retardation while
preserving indefiniteness and alternation. The density (comparable-count) diagonal (D2) suppresses
leakage most strongly (→ 0.473) but over-suppresses and kills indefiniteness. A constant diagonal
does essentially nothing to leakage.

**Honest caveat:** the reduction is modest (~8 %), and the residual Feynman tail is *intrinsic* to
the symmetric off-diagonal part L3 — a diagonal cannot remove it. Fully suppressing the tail would
require reducing L3's symmetric coupling or the BDG-specific retarded structure (whose diagonal
−2 and binomial coefficients are outside this constraint).

---

## Test program

| Test | Verdict |
|---|---|
| G4-L50 `G4_L50_DiagonalTermReducesLeakage` | PASS (2/4 diagonals reduce leakage) |
| G4-L51 `G4_L51_PreservesRetardationIndefinitenessAlternation` | PASS (D4 satisfies all 4) |
| G4-L52 `G4_L52_StrengthSweepAndRefinement` | PASS (refinement-stable) |

Code: `AT.Core/ResearchXH/LorentzianOperator.cs` (added `ComparableCount`, `PastCount`,
`LocalDegree`, `AddDiagonal`, `GreenResponseMetrics`); tests
`AT.Tests/ResearchXH/G4L_Phase5_DiagonalTermStudyTests.cs`.
