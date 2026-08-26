# G4-L Phase 7 — Native Diagonal Self-Term

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 7 — derive a native diagonal self-term
**Status:** COMPLETED — 3/3 xUnit tests pass

---

## 1. Goal

Starting from H0 = R1 + A3 (the retarded interval operator), ask whether a diagonal self-term
derived from **causal structure alone** can push the Feynman tail below 0.50 while preserving
retardation, indefiniteness, and layer alternation.

| ID | Diagonal | Native definition |
|---|---|---|
| D1 | local degree | −(past + future Hasse links) |
| D2 | interval count | −Σ_comparable 1/(k+1) (near-layer weighted) |
| D3 | comparable count | −# comparable events |
| D4 | layer occupancy | −# events on the same time slice |
| D5 | causal volume | −Σ_comparable (k+1) (far-layer weighted) |

All negated (BDG-like sign), applied as a per-vertex self-term to H0.

---

## 2. Results (δ-source, N = 72)

Baseline: **H0 = R1 + A3 → leakage 0.548** (already down from H2's 0.759).

**Natural (coefficient-1) forms**

| diagonal | leakage | direction | indefinite | alternates |
|---|---|---|---|---|
| D0 −degree/2 (BDG-balanced) | 0.734 | — | — | — |
| D1 degree | 0.598 | 0.815 | ✅ | ✅ |
| D2 interval | 0.503 | 0.835 | ✅ | ✅ |
| D3 comparable | 0.322 | 0.877 | ❌ (over-suppressed) | ✅ |
| D4 occupancy | **0.488** | 0.844 | ✅ | ✅ |
| D5 volume | 0.073 | 0.884 | ❌ (over-suppressed) | ✅ |

**Strength sweep** (normalized max |diag| = s; lowest leakage preserving indefiniteness+alternation)

| diagonal | best s | leakage | < 0.50 |
|---|---|---|---|
| **D1 degree** | 0.75 | **0.428** | ✅ |
| D2 interval | 1.00 | 0.442 | ✅ |
| D3 comparable | 0.50 | 0.481 | ✅ |
| D4 occupancy | 0.50 | 0.460 | ✅ |
| D5 volume | 2.00 | 0.440 | ✅ |

**5/5 native diagonals reach leakage < 0.50** with structure preserved.

**Best (D1 degree, s = 0.75):** leakage **0.428**, directionality 0.879 (retarded),
indefinite ✅, alternating ✅, KS→BDG 0.2639. Refinement: 0.428 (N=72) → 0.443 (N=110).

---

## 3. Success criteria

| Criterion | Result |
|---|---|
| leakage < 0.50 | ✅ **0.428** |
| retarded | ✅ directionality 0.879 |
| indefinite | ✅ |
| alternating | ✅ |

---

## 4. Conclusion

**YES.** A native diagonal self-term emerges from causal structure alone: the **negated local link
degree** (the discrete analogue of the BDG self-term — the diagonal that balances the off-diagonal
link coupling) pushes the Feynman tail to **0.428 < 0.50** while preserving retardation,
indefiniteness, and alternation, and survives refinement.

Three sharp findings:

1. **The degree diagonal is the right *form*, but not BDG's *value*.** Importing BDG's own
   diagonal −2 (equivalently −degree/2, since grid-interior degree = 4) **overshoots** (leakage
   0.734, *worse* than baseline). The native operator's coupling is ±1 (not BDG's +4/−2), so the
   correct self-term is smaller — the optimum sits at ~0.75 of the degree scale. The diagonal is
   native, but its strength must be *calibrated to the native coupling*, not copied from BDG.
2. **Over-suppression destroys indefiniteness.** Comparable-count (0.322) and causal-volume
   (0.073) at full strength push leakage far below 0.50 but kill the positive part of the spectrum.
   The diagonal must not dominate the off-diagonal alternation.
3. **The emergence is not unique.** All five causal-structure diagonals (degree, interval,
   comparable, occupancy, volume) reach < 0.50 at an appropriate strength — a family of native
   self-terms, with degree the best.

**Bottom line:** with H = R1 + A3 + D, the Feynman tail is now suppressed to ~43 % (from H2's
~76 %). The diagonal is derived natively; only its *strength* is a calibration, and it is no longer
the BDG −2.

---

## Test program

| Test | Verdict |
|---|---|
| G4-L70 `G4_L70_NativeDiagonalReducesLeakage` | PASS (5/5 diagonals reach < 0.50) |
| G4-L71 `G4_L71_BestDiagonalPreservesStructure` | PASS (D1: leak 0.428, dir 0.879, indefinite, alternating) |
| G4-L72 `G4_L72_RefinementStability` | PASS (N=72 → 0.428, N=110 → 0.443) |

Code: `AT.Core/ResearchXH/LorentzianOperator.cs` (added `IntervalCount`, `LayerOccupancy`,
`CausalVolume`, `RetardedInterval`); tests `AT.Tests/ResearchXH/G4L_Phase7_NativeDiagonalTests.cs`.
