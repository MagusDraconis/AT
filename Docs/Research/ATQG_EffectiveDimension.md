# AT-QG Phase 4 — Effective Dimension

**Program:** AT-QG (Unification)
**Phase:** 4 — is d=4 fundamental or emergent?
**Status:** COMPLETED — 3/3 xUnit tests pass (15/15 AT-QG)
**Constraint:** no new primitives

---

## 1. Goal

d=4 is not derived (QG2/QG3). Here we test whether AT can be fundamentally higher-dimensional (D>4) while
only an effective d=4 sector is observable, via dimensional reduction, observable submanifolds, information
projection, and causal accessibility. Classify d=4: fundamental or emergent.

---

## 2. Results

### (a) Dimensional reduction: ρ's support is the observable dimension (ATQG40)

If the counting measure ρ varies only along d of the D coordinates (∂ρ = 0 transversely), then the Einstein
tensor is non-trivial only in the observable d×d block — the transverse (D−d) directions are physically empty
(no curvature, no matter).

| D | observable comps | total comps | observable fraction | frozen dirs |
|---|---|---|---|---|
| 4 | 10 | 10 | 1.000 | 0 |
| 5 | 10 | 15 | 0.667 | 1 |
| 6 | 10 | 21 | 0.476 | 2 |
| 7 | 10 | 28 | 0.357 | 3 |

The observable Einstein block = d(d+1)/2 = 10, fixed by d regardless of D. A fundamental D>4 geometry with ρ
supported only on d directions is observationally d-dimensional.

### (b) Metric-origin consistency selects the observable dimension (ATQG41)

Restricting g = ρ^(2/D)η_D to a d-dim submanifold gives √(−g_eff) = ρ^(d/D) ≠ ρ for d≠D. The counting-measure
consistency √(−g)=ρ is dimension-specific: it holds only where the exponent is 2/d, so the observable sector
re-derives its own metric origin in dimension d, decoupled from D.

| D | √(−g_eff) exponent | mismatch \|2/D−2/d\| |
|---|---|---|
| 4 | 1.000 | 0.000 (consistent) |
| 5 | 0.800 | 0.100 |
| 6 | 0.667 | 0.167 |
| 7 | 0.571 | 0.214 |

### (c) Classification (ATQG42)

**EMERGENT** — d=4 is the dimension of the actualization support, not fundamental.

---

## 3. Classification: EMERGENT

- **Dimension-agnostic framework:** nothing fixes the fundamental dimension D, so higher-D is not excluded.
- **Observable dimension = support of ρ:** directions with ∂ρ=0 are empty; an observer sees only d dimensions.
- **Metric origin re-derived per dimension:** √(−g)=ρ forces exponent 2/d in the observable dimension,
  independent of any fundamental D.

Therefore d=4 is **EMERGENT** — the dimension of the observable actualization — not fundamental. This
reformulates the "3+1 dimensionality" question: instead of "why d=4", it becomes "why does actualization vary
along exactly 3 spatial directions" — a property of the ρ-field, not the embedding dimension.

---

## 4. Conclusion

The AT framework is compatible with a fundamentally higher-dimensional geometry whose observable sector is
d=4, provided the counting measure ρ varies only along 4 (3+1) directions — the extra dimensions are then
physically empty and unobservable. d=4 is therefore best understood as an **emergent, observational** dimension
(the support of actualization), not a fundamental one; and the framework neither requires nor excludes D>4.
This closes the dimension arc (QG2/QG3/QG4) with a coherent picture: dimension is supplied at the fundamental
level, but the *observable* dimension is the rank of the actualization, which can be emergent.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG40 `ATQG40_DimensionalReduction` | PASS (observable support = d, transverse empty) |
| ATQG41 `ATQG41_MetricOriginConsistency` | PASS (√(−g)=ρ dimension-specific, selects d) |
| ATQG42 `ATQG42_Classification` | PASS (d=4 EMERGENT) |

Code: `AT.Core/ResearchXH/EffectiveDimension.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase4_EffectiveDimensionTests.cs`.
