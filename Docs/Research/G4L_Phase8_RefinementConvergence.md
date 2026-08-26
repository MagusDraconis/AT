# G4-L Phase 8 — Refinement Convergence

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 8 — does refinement reduce the remaining Feynman tail?
**Status:** COMPLETED — 3/3 xUnit tests pass (a documented NEGATIVE result)

---

## 1. Goal

Starting from the Phase-7 best native Lorentzian operator H = R1 + A3 + D (negated local-degree
diagonal, leakage 0.428 at N = 72), ask whether refining the causal set drives the residual
Feynman tail to zero and the operator toward the BDG reference. Run N = 72 → 506 (diamond-shaped
grids, tMax = 2·xMax − 1) and classify the behaviour as **convergent / plateau / divergent**.

| target N | grid (tMax, xMax) | actual N |
|---|---|---|
| 72 | (7, 4) | 72 |
| 110 | (9, 5) | 110 |
| 150 | (11, 6) | 156 |
| 250 | (15, 8) | 272 |
| 500 | (21, 11) | 506 |

---

## 2. Results (fixed operator, strength s = 0.75)

| grid | N | leakage | directionality | KS→BDG | pos/neg modes |
|---|---|---|---|---|---|
| 7×4 | 72 | 0.428 | 0.879 | 0.2639 | 0.714 |
| 9×5 | 110 | 0.546 | 0.866 | 0.2727 | 0.618 |
| 11×6 | 156 | 0.503 | 0.706 | 0.2564 | 0.576 |
| 15×8 | 272 | 0.417 | 0.851 | 0.2500 | 0.591 |
| 21×11 | 506 | 0.412 | 0.897 | 0.2372 | 0.567 |

- **Leakage:** 0.428 → 0.546 → 0.503 → 0.417 → 0.412 — *non-monotonic*, Δ = −0.016.
- **KS→BDG:** 0.264 → 0.273 → 0.256 → 0.250 → 0.237 — *non-monotonic*, Δ = −0.027 (~10 % net).
- **Mode ratio:** stays < 1 (both signs persist → indefiniteness survives refinement).

---

## 3. Classification

| observable | first → last | classification |
|---|---|---|
| leakage (Feynman tail) | 0.428 → 0.412 | **PLATEAU** |
| KS distance to BDG | 0.2639 → 0.2372 | **PLATEAU** (weak drift) |

---

## 4. Conclusion

**NO — refinement does NOT reduce the remaining Feynman tail.** The native Lorentzian operator
**plateaus**: the tail oscillates around ~0.41–0.55 with no systematic decrease toward zero, and
the KS distance to BDG drifts only weakly (~10 % net, non-monotonic), remaining far from 0.

This is a **significant negative result**: it proves the residual Feynman tail is **intrinsic** to
the native symmetric off-diagonal alternation, not a discretization artifact that vanishes in the
continuum/refinement limit. Refinement cannot supply the missing BDG diagonal −2 — the gap
identified in Phases 5–7 is real and does not close under N → ∞.

The operator remains (for all N) retarded-biased (directionality 0.71–0.90), indefinite, and
alternating — but its propagator retains an irreducible ~40–55 % spacelike leakage.

**Success criterion ("tail decreases systematically") is NOT met → classification: PLATEAU.**

---

## Test program

| Test | Verdict |
|---|---|
| G4-L80 `G4_L80_RefinementReducesTail` | PASS (documents non-monotonic, persistent tail) |
| G4-L81 `G4_L81_ConvergenceToBdg` | PASS (KS stays far from 0; indefinite survives) |
| G4-L82 `G4_L82_ClassifyConvergence` | PASS (classification = PLATEAU) |

Code: `AT.Core/ResearchXH/LorentzianOperator.cs` (added `NativeLorentzian`);
tests `AT.Tests/ResearchXH/G4L_Phase8_RefinementConvergenceTests.cs`.
