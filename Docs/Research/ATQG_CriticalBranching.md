# AT-QG Phase 7 — Critical Branching

**Program:** AT-QG (Unification)
**Phase:** 7 — why must actualization be critical (μ=1)?
**Status:** COMPLETED — 3/3 xUnit tests pass (24/24 AT-QG)
**Constraint:** no new primitives

---

## 1. Goal

The chain is Q-events → critical branching (μ=1) → α=0 → ρ → gravity. Here we test WHY actualization must be
critical, via branching stability, extinction probability, runaway growth, entropy production, and
renormalization fixed points. Classify: DERIVED / PREFERRED / POSTULATED.

---

## 2. Results

### (a) μ=1 is the unique marginal point (ATQG70)

| μ | extinction q | μ^100 | total population (100 gen) |
|---|---|---|---|
| 0.5 | 1.000 | 8×10⁻³¹ | 2.0 (finite) |
| 0.8 | 1.000 | 2×10⁻¹⁰ | 5.0 (finite) |
| **1.0** | **1.000** | **1.0 (marginal)** | **100 (linear)** |
| 1.1 | 0.176 | 1.4×10⁴ | 1.4×10⁵ (exponential) |
| 1.5 | 0.417 | 4×10¹⁷ | runaway |

Subcritical (μ<1) dies out (q=1, finite total); supercritical (μ>1) runs away exponentially (q<1, diverging);
only μ=1 is non-vanishing and non-exploding.

### (b) Three criteria coincide at μ=1 (ATQG71)

- **Marginal stability**: μ=1 is the boundary between extinction and runaway.
- **Scale-freeness**: μ=1 ⟺ α=0 ⟺ L = 1/|ln μ| = ∞ (renormalization-invariant).
- **Maximum entropy**: μ=1 ⟺ α=0 ⟺ H(α) maximum (uniform per-octave allocation).

### (c) Classification (ATQG72)

**DERIVED (unique), conditional on scale-freeness / renormalization invariance.**

---

## 3. Classification: DERIVED (unique)

- μ=1 is the **unique marginal point**: subcritical dies out, supercritical runs away — only μ=1 is
  non-vanishing and non-exploding.
- μ=1 is the **unique scale-free point** (L=∞, renormalization-invariant) and the **maximum-entropy** point
  (α=0) — three independent criteria coincide.
- Therefore criticality is **DERIVED**: uniquely selected by stability (non-extinction, non-runaway) +
  scale-freeness (renormalization invariance, AT-F1) + maximum entropy (G4-RHO1).
- The single conditioning input is **scale-freeness** (renormalization invariance), which AT-F1 reduced to
  the statement that the primitives carry no intrinsic scale.

---

## 4. Conclusion

Criticality is **derived, not postulated**: μ=1 is the unique branching point that is simultaneously
marginal-stable (neither extinct nor runaway), scale-free (renormalization-invariant), and maximum-entropy.
This closes the chain **Q-events → critical branching → α=0 → ρ → gravity** with criticality itself derived —
leaving scale-freeness (renormalization invariance) as the single, already-reduced conditioning input.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG70 `ATQG70_ExtinctionVsRunaway` | PASS (μ=1 unique marginal point) |
| ATQG71 `ATQG71_ThreeCriteriaCoincide` | PASS (scale-free = marginal = max-entropy at μ=1) |
| ATQG72 `ATQG72_Classification` | PASS (DERIVED) |

Code: `AT.Core/ResearchXH/QEventBranching.cs` (added `ExtinctionProbability`, `TotalExpectedPopulation`);
tests `AT.Tests/ResearchXH/ATQG_Phase7_CriticalBranchingTests.cs`.
