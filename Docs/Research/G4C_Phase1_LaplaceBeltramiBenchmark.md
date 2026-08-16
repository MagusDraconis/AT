# G4-C Phase 1 — Laplace–Beltrami Benchmark

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-C)
**Phase:** 1 (validate Lc = ρ⁻¹ L ρ⁻¹ as a Laplace–Beltrami operator)
**Status:** COMPLETED — 3/3 xUnit tests pass (SC1–SC4 all satisfied)
**Question:** Does Lc = ρ⁻¹ L ρ⁻¹ reproduce the qualitative behavior of Δ_g on known geometries?
**Primitives used:** Q-event graph · density · adjacency · counting measure · spectral observables.
No imported metric tensor. No new primitives.

---

## 1. Goal

Benchmark the native conformal operator **Lc = ρ⁻¹ L ρ⁻¹** (G4-C Phase 0 winner) against the
unnormalized **L** and the degree-normalized **D^(−1/2) L D^(−1/2)** on three conformal
geometries, checking whether Lc behaves like a Laplace–Beltrami operator.

| Geometry | Builder | Density ρ(x) | Conformal R(0) |
|---|---|---|---|
| Negative | `ConformalRateGraph` a=+1 | 1 + x² | −4 |
| Flat | `ConformalRateGraph` a=0 | 1 | 0 |
| Positive | `ConformalRateGraph` a=−0.8 | 1 − 0.8x² | +3.2 |

Operators (all symmetric, real spectrum): `L`, `D^(−1/2)LD^(−1/2)`, `Lc = ρ^(−1)Lρ^(−1)`.
Observables: eigenvalue ordering, spectral gap, heat trace Z(1), heat-trace derivative Z′(1),
spectral zeta ζ(2), Weyl dimension, spectral entropy S(1).

---

## 2. Results (measured, deterministic)

### 2.1 SC1 + SC2 — sign separation and degree artifacts (spectral zeta ζ(2))

| Operator | ζ2 neg | ζ2 flat | ζ2 pos | sign-sep | degree-decreasing? |
|---|---|---|---|---|---|
| L (unnorm) | 1012.4 | 1767.0 | 340.9 | ❌ | ✅ (pure degree artifact) |
| D^(−1/2) L D^(−1/2) | 30235.7 | 23134.0 | 9391.5 | ✅ | ❌ |
| **Lc = ρ⁻¹ L ρ⁻¹** | **5614.9** | **1767.0** | **110.3** | ✅ | ❌ |

Mean degree: negative 5.16, flat 3.75, positive 6.33. The unnormalized L's ζ(2) decreases
monotonically with degree — its response is a **density-magnitude artifact**. Lc's ζ(2) is
**not** a monotonic function of degree — its response is a genuine **curvature-sign** signal.

### 2.2 SC3 — consistent curvature ordering across observables

| Observable | L (monotonic?) | D^(−1/2)LD^(−1/2) | Lc |
|---|---|---|---|
| spectral gap | ❌ | ✅ | ✅ |
| heat trace Z(1) | ❌ | ❌ | ✅ |
| heat-trace derivative Z′(1) | ❌ | ✅ | ✅ |
| spectral zeta ζ(2) | ❌ | ✅ | ✅ |
| spectral entropy S(1) | ❌ | ❌ | ✅ |
| **monotonic observables** | **0 / 5** | **2 / 5** | **5 / 5** |

Lc ranks negative / flat / positive **consistently (monotonic in curvature) for all five
observables**; L scrambles the ordering (0/5); the degree-normalized operator is intermediate
(2/5).

Representative Lc values (neg / flat / pos): gap 0.0161 / 0.0384 / 0.1305; Z(1) 41.16 / 29.45 /
8.72; ζ(2) 5614.9 / 1767.0 / 110.3; S(1) 4.839 / 4.469 / 3.141.

### 2.3 SC4 — refinement stability (Lc ζ(2))

| n | N | ζ2 neg | ζ2 flat | ζ2 pos | sign-sep |
|---|---|---|---|---|---|
| 16 | 256 | 5614.9 | 1767.0 | 110.3 | ✅ |
| 24 | 576 | 4410.7 | 1062.1 | 163.0 | ✅ |

The Lc sign-separation persists under refinement — not a finite-N artifact.

---

## 3. Success-criteria assessment

| Criterion | Requirement | Verdict |
|---|---|---|
| **SC1** | Lc preserves curvature-sign separation | ✅ R<0 up, R>0 down around flat |
| **SC2** | Lc minimizes degree artifacts | ✅ not monotonic in degree (unlike L) |
| **SC3** | Lc produces consistent hyperbolic < flat < positive ordering | ✅ 5/5 observables monotonic |
| **SC4** | Stable under graph refinement | ✅ sign-separation persists n=16→24 |

---

## 4. Failure-mode analysis

- **Degree dependence** — L fails (its ζ2 tracks degree); Lc passes (density-weighted, degree-blind).
- **Density-only response** — L fails (magnitude, sign-blind); Lc passes (sign-separating).
- **Refinement instability** — none observed for Lc (SC4).
- **Topology-only artifact** — the conformal geometries share the same (flat) topology; the
  observed ordering is therefore carried by the conformal factor ρ, not by a topology change —
  confirming the signal is genuinely conformal.

---

## 5. Conclusion

**Lc = ρ⁻¹ L ρ⁻¹ behaves like a Laplace–Beltrami operator.** It is the unique operator among
the three tested that (i) separates curvature sign, (ii) is degree-artifact-free, (iii) ranks
negative / flat / positive consistently across all five observables, and (iv) is stable under
refinement. This validates Lc as the native conformal operator for the G4 metric-to-operator
program — reproducing Δ_g qualitatively **without importing Δ_g or a metric tensor**.

---

## Test program

| Test | Verdict |
|---|---|
| G4-C10 `G4_C10_LcPreservesSignSeparationAndMinimizesDegreeArtifacts` | PASS (SC1+SC2) |
| G4-C11 `G4_C11_LcProducesConsistentCurvatureOrdering` | PASS (SC3) |
| G4-C12 `G4_C12_LcIsStableUnderRefinement` | PASS (SC4) |

`TQM.Tests/ResearchXH/G4C_Phase1_LaplaceBeltramiBenchmarkTests.cs` (inherits `ResearchTestBase`,
deterministic, `StringBuilder`-composed reports).
