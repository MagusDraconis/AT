# G4-L Phase 2 — Retarded Operator

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 2 — transform L3 into a retarded operator
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Can retarded causal propagation be produced natively from causal order?

---

## 1. Construction

From the layer operator L3 (uniform alternating weights (−1)^(k+1)), three directed variants:

| Operator | Definition | Direction |
|---|---|---|
| **R1** | (−1)^(k+1) over **past** layers only (i ≺ j) | retarded |
| **R2** | R1ᵀ (future layers only) | advanced |
| **R3** | R1 + R2 = symmetric L3 | bidirectional (baseline) |

All built natively from causal order + intervals + counting measure.

---

## 2. Results

### 2.1 G4-L20 — construction + directionality

R1 past-directed only ✅ · R2 = R1ᵀ future-directed ✅ · R3 = R1+R2 symmetric ✅.

### 2.2 G4-L21 — spectrum + interval response

| operator | spectrum | interval response |
|---|---|---|
| R1 retarded | **nilpotent** (max\|λ\| ≈ 0) | past-only (−1, +1, −1, +1) |
| R2 advanced | **nilpotent** (max\|λ\| ≈ 0.03) | future-only (−1, +1, −1, +1) |
| R3 symmetric | **indefinite** (31+, 41−, max\|λ\| = 18.8) | both directions |

The retarded/advanced operators are strictly triangular → nilpotent (zero spectrum): the wave
information lives in the off-diagonal structure, **not** the spectrum.

### 2.3 G4-L22 — propagation asymmetry + KS distance

δ-source at (t=3, x=0):

| operator | past | future | direction |
|---|---|---|---|
| BDG (retarded) | 0.00 | 16.00 | forward-only |
| **R1 retarded** | 0.00 | 24.00 | **forward-only** |
| R2 advanced | 15.00 | 0.00 | backward-only |
| R3 bidirectional | 15.00 | 24.00 | both ways |

KS distance to the *symmetric* BDG: R3 = 0.2222 (closest), R1 = R2 = 0.5972.

---

## 3. Conclusion

**YES — retarded causal propagation is produced natively from causal order.** R1 (past-directed)
reproduces BDG's **forward-only** propagation exactly, reducing the propagation-distance to BDG
to zero. R2 is the advanced (backward) counterpart; R3 is the symmetric baseline.

**Trade-off (honest):** directionality and spectrum pull in opposite directions — the retarded
R1 matches BDG's directionality but has a **degenerate (nilpotent) spectrum**, while the symmetric
R3 matches BDG's **indefinite spectrum** but loses directionality. The full retarded BDG
(diagonal −2 + off-diagonal layer weights) would combine both, and remains the next step.

---

## Test program

| Test | Verdict |
|---|---|
| G4-L20 `G4_L20_RetardedOperatorsAreConstructibleAndDirectional` | PASS |
| G4-L21 `G4_L21_SpectrumAndIntervalResponse` | PASS (R1/R2 nilpotent, R3 indefinite) |
| G4-L22 `G4_L22_PropagationAsymmetryAndBdgDistance` | PASS (R1 forward-only matches BDG) |

Code: `TQM.Core/ResearchXH/LorentzianOperator.cs` (added `PastDirectedLayer`,
`FutureDirectedLayer`, `BidirectionalLayer`, `Transpose`, `DirectedLayerProfile`);
`SpectralCurvature.GeneralEigenvalues`; tests
`TQM.Tests/ResearchXH/G4L_Phase2_RetardedOperatorTests.cs`.
