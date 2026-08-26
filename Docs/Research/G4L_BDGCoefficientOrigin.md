# G4-L Phase 11 — Origin of the BDG Coefficients

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 11 — can the BDG binomial coefficients emerge from interval combinatorics?
**Status:** COMPLETED — 3/3 xUnit tests pass (36/36 G4-L)
**Constraint:** no imported coefficients; causal order, intervals, layers, counting measure only
**Classification:** **PARTIAL MATCH**

---

## 1. Goal

Determine whether the BDG d = 2 coefficient pattern — diagonal −2, links (k=0) +4, next layer (k=1)
−2, and 0 beyond — emerges from interval combinatorics (layer occupancy, interval counts, causal
volume, alternating-layer generating functions).

---

## 2. Findings

### (a) Raw statistics do NOT reproduce the coefficients (G4-L110)

| k | layer occupancy O(k) | fraction of pairs | BDG c_k |
|---|---|---|---|
| 0 | 6.000 | 0.089 | +4 |
| 1 | 4.000 | 0.046 | −2 |
| 2 | 6.857 | 0.092 | 0 |
| 3 | 3.905 | 0.050 | 0 |

Occupancy ratio O(1)/O(0) = 0.667 vs BDG ratio c₁/c₀ = −0.50. The raw layer occupancy and interval
counts are lattice-noisy and do **not** yield the BDG ratio — **NO MATCH for naive counting**.

### (b) The BDG stencil IS the binomial second difference (G4-L111)

Indexing by causal layer ℓ (ℓ=0 self, 1 links, 2 next, ≥3 zero), the BDG coefficients satisfy
**exactly**

```
a_ℓ = −2·(−1)^ℓ·C(2,ℓ)   =   {−2, +4, −2, 0, 0, …}
```

i.e. the BDG stencil is −2 × the **second finite difference** {1, −2, 1} over the causal layers. The
generating function is Σ a_ℓ x^ℓ = −2(1−x)² — the binomial transform. The truncation (0 for ℓ>2) is
automatic (C(2,ℓ)=0 for ℓ>2). **MATCH at the level of the binomial structure.**

### (c) Constant-annihilation is stencil-level, not pointwise (G4-L112)

The native combinatorial condition — the diagonal equals minus the sum of off-diagonal coefficients
(Σ a_ℓ = 0) — holds exactly. But applied to a constant field on the *finite* lattice, B·1 ≠ 0
(max|B·1| ≈ 14 on interior events): the layer multiplicities vary, so exact annihilation is an
**averaged (continuum)** property, not a pointwise lattice property.

---

## 3. Classification

| component | origin | verdict |
|---|---|---|
| binomial coefficients (−1)^ℓ·C(2,ℓ) | interval/layer combinatorics (second difference) | **MATCH** (native) |
| truncation at ℓ > d | binomial degree | **MATCH** (native) |
| constant-annihilation (diagonal = −Σ off-diagonal) | combinatorial | **MATCH** (stencil-level) |
| overall scale −2 | continuum normalization to □ | **NO MATCH** (imported) |
| raw layer occupancy / interval counts | lattice statistics | **NO MATCH** (noisy) |

**Overall: PARTIAL MATCH.** The binomial coefficient *structure* (the second-difference over causal
layers, its truncation, and the constant-annihilation condition) emerges from interval combinatorics;
the *overall normalization* −2 does not — it is fixed by matching the continuum d'Alembertian □,
which is outside the pure-counting constraint. This sharpens the G4-L10 result: the native operators
already have the right *shape* (alternating layers); only the global scale −2 separates them from BDG.

---

## Test program

| Test | Verdict |
|---|---|
| G4-L110 `G4_L110_RawStatisticsDoNotReproduceBdg` | PASS (occupancy ratio 0.667 ≠ −0.50) |
| G4-L111 `G4_L111_BdgStencilIsBinomialSecondDifference` | PASS (a_ℓ = −2·(−1)^ℓ·C(2,ℓ) exact) |
| G4-L112 `G4_L112_ConstantAnnihilationAndClassification` | PASS (stencil Σ a_ℓ = 0; B·1 ≠ 0 pointwise) |

Code: `AT.Tests/ResearchXH/G4L_Phase11_BDGCoefficientOriginTests.cs`.
