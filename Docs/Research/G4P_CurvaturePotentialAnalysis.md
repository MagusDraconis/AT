# G4-P Phase 1 — Curvature Potential Analysis

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-P)
**Phase:** 1 — isolate the native curvature potential
**Status:** COMPLETED — 3/3 xUnit tests pass

---

## 1. Goal

The Phase-0 analytic result is Lc = ρ⁻¹ L ρ⁻¹ = −c Δ_g + c·V, with V = Δρ/ρ² (d = 2). Split this
into its three terms and measure which one produces the observed curvature reconstruction:

1. **Δ_g only** (Laplace–Beltrami, Lc with the potential subtracted)
2. **V = Δρ/ρ² only** (the native zeroth-order potential, diagonal)
3. **Δ_g + V** (full Lc)

---

## 2. Results (ρ = 1 + a x², R(0) = −4a)

### Sign recovery

| term | score neg (R<0) | score pos (R>0) | orientation |
|---|---|---|---|
| Δ_g only | −24.43 | +49.50 | **correct** (−, +) |
| V = Δρ/ρ² only | +0.67 | −1202.74 | **inverted** (+, −) |
| Δ_g + V (Lc) | −3.24 | +4.34 | **correct** (−, +) |

### Magnitude ordering (9 strengths)

| term | monotonic | note |
|---|---|---|
| Δ_g only | ✗ (near-flat hiccup) | −43.7 → +49.5, one inversion at a=0.2 |
| V = Δρ/ρ² only | ✗ (decreasing) | 0.63 → −1202, **diverges** as a → −1 |
| Δ_g + V (Lc) | ✅ | −4.76 → +4.34 strictly increasing |

### Refinement (n = 16 → 24)

| term | sign (n=16) | sign (n=24) | score scale |
|---|---|---|---|
| Δ_g only | (−, +) ✅ | (−, +) ✅ | 49.5 → 39.2 |
| V = Δρ/ρ² only | (+, −) ✗ | (+, −) ✗ | 1202 → 15343 (diverging) |
| Δ_g + V (Lc) | (−, +) ✅ | (−, +) ✅ | 4.3 → 4.5 |

---

## 3. Classification

| term | classification | reason |
|---|---|---|
| **Δ_g (Laplace–Beltrami)** | **DOMINANT** | recovers the correct sign (−/+) alone, refinement-stable |
| **V = Δρ/ρ²** | **SECONDARY** | carries curvature but **inverted** (V ∝ Δρ ∝ −R), and diverges as the metric degenerates (a → −1) |
| Δ_g + V (Lc) | correct operator | Δ_g dominates the sign; V is a subdominant, sign-flipped correction |

---

## 4. Conclusion

**The curvature reconstruction is driven by Δ_g (the Laplace–Beltrami), not the potential V.**

This **corrects the Phase-0 attribution**: V = Δρ/ρ² is proportional to −R (inverted), not R, so
it cannot be the source of the correct sign. The genuine Laplace–Beltrami Δ_g = ρ⁻¹Δ_η carries the
correct sign — its heat trace / spectral observables encode ∫R_g (the Weyl coefficient), which is
what G4-C's sign separation detected.

The decomposition is therefore best read as

```
Lc  =  −Δ_g  +  V,   with Δ_g the DOMINANT (correctly-oriented) part and V a SECONDARY
                     (inverted, degenerating) correction.
```

**No new primitives**: Δ_g and V are both built from ρ and L alone. The potential V is a native
by-product, not the driver — its most useful role is its divergence near metric degeneracy (a → −1),
which flags the breakdown of the conformal chart.

---

## Test program

| Test | Verdict |
|---|---|
| G4-P10 `G4_P10_SignRecoveryPerTerm` | PASS (Δ_g correct, V inverted, Lc correct) |
| G4-P11 `G4_P11_MagnitudeOrderingPerTerm` | PASS (Lc monotonic; Δ_g near-monotonic; V diverging) |
| G4-P12 `G4_P12_RefinementStabilityAndClassification` | PASS (Δ_g DOMINANT, V SECONDARY/inverted) |

Code: `TQM.Core/ResearchXH/CurvaturePotential.cs` (+ `CurvatureReconstruction.ScoreRobust`);
tests `TQM.Tests/ResearchXH/G4P_Phase1_CurvaturePotentialAnalysisTests.cs`.
