# G4-L Phase 3 — Retarded-Indefinite Operator

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 3 — construct a native retarded-indefinite Lorentzian operator
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Can a hybrid operator preserve both retarded propagation and Lorentzian spectral structure?

---

## 1. Construction

Three hybrids from the native building blocks (R1 retarded, L3 alternating symmetric, ρ counting density):

| ID | Operator | Definition |
|---|---|---|
| H1 | retarded layer | R1 (past-only) |
| **H2** | retarded alternating-layer | **R1 + L3** |
| H3 | retarded density-weighted | ρ⁻¹ (R1 + L3) ρ⁻¹ |

---

## 2. Results

### 2.1 G4-L30 — propagation asymmetry

| operator | past | future | retarded-ness |
|---|---|---|---|
| R1 | 0.00 | 24.00 | 1.000 |
| L3 | 15.00 | 24.00 | 0.615 |
| **H2** | 15.00 | 48.00 | **0.762** |
| H3 | 0.61 | 1.94 | 0.761 |

H2 is **forward-biased** (more retarded than L3, 0.762 vs 0.615).

### 2.2 G4-L31 — eigenmodes, alternation, KS to BDG

| operator | (n+, n−, n0) | indefinite | KS to BDG |
|---|---|---|---|
| R1 | (0, 0, 72) | no (nilpotent) | 0.5972 |
| **H2** | **(31, 41, 0)** | **yes** | **0.1389** |
| H3 | (31, 41, 0) | yes | 0.5278 |
| L3 | symmetric indefinite | yes | 0.2222 |

- H2 layer profile (k=0 −2.00, k=1 +2.00, k=2 −2.00) **alternates**.
- **H2 is closer to BDG than L3** (KS 0.1389 vs 0.2222): its symmetric part is (3/2)·L3, so its
  spectrum is scaled to match the BDG spectral range better.

### 2.3 G4-L32 — stability under refinement

| grid | N | forward-biased | indefinite | alternating |
|---|---|---|---|---|
| 7×4 | 72 | ✅ | ✅ | ✅ |
| 9×5 | 110 | ✅ | ✅ | ✅ |

---

## 3. Conclusion

**YES — H2 = R1 + L3 is a native retarded-INDEFINITE Lorentzian operator.** It satisfies all four
success criteria simultaneously:

- ✅ **retarded** (forward-biased, retarded-ness 0.762 > L3's 0.615)
- ✅ **alternating** (layer profile −2, +2, −2)
- ✅ **indefinite** (31 positive / 41 negative eigenmodes)
- ✅ **closer to BDG than L3** (KS 0.1389 < 0.2222)

This resolves the Phase-2 direction-vs-spectrum trade-off: combining the retarded operator with
its symmetric alternating counterpart restores the indefinite spectrum *while remaining
retarded-biased*. The density-weighted H3 is also indefinite and retarded-biased but spectrally
farther from BDG (KS 0.5278). H1 (pure retarded) stays nilpotent. The full retarded BDG (with its
binomial coefficients and diagonal) remains the ultimate benchmark.

---

## Test program

| Test | Verdict |
|---|---|
| G4-L30 `G4_L30_HybridIsRetardedBiased` | PASS (H2 retarded-ness 0.762 > L3 0.615) |
| G4-L31 `G4_L31_HybridIsIndefiniteAndCloserToBdg` | PASS (indefinite, KS 0.1389 < 0.2222) |
| G4-L32 `G4_L32_HybridStableUnderRefinement` | PASS (stable at N=72, 110) |

Code: `TQM.Core/ResearchXH/LorentzianOperator.cs` (added `CausalDensity`, `Add`,
`HybridRetardedAlternating`, `HybridRetardedDensityWeighted`); tests
`TQM.Tests/ResearchXH/G4L_Phase3_RetardedIndefiniteOperatorTests.cs`.
