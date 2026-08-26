# AT-QG Phase 15 — Spacetime Fluctuations

**Program:** AT-QG (Unification)
**Phase:** 15 — do event-count fluctuations generate metric fluctuations?
**Status:** COMPLETED — 3/3 xUnit tests pass (48/48 AT-QG)
**Constraint:** no new primitives

---

## 1. Goal

Test whether Poisson fluctuations in the event count (δρ) propagate to metric (δg) and curvature (δR)
fluctuations, and whether they are graviton-like (tensor) or scalar (conformal).

---

## 2. Results

### (a) Poisson variance (ATQG150)

The counting measure has Poisson statistics (Var N = N), so δρ/ρ = 1/√N:

| N | δρ/ρ = 1/√N |
|---|---|
| 10 | 0.316 |
| 100 | 0.100 |
| 1000 | 0.032 |
| 10000 | 0.010 |

The relative fluctuation is suppressed as 1/√N — spacetime-foam scaling (large regions fluctuate less).

### (b) Propagation to metric and curvature (ATQG151)

δg/g = (2/d)·δρ/ρ (from g = ρ^(2/d)η), δR/R ≈ δρ/ρ — the metric and curvature inherit the Poisson fluctuation,
with correlation length set by the cell size ℓ (Poisson events are uncorrelated beyond one cell).

### (c) Scalar vs graviton (ATQG152)

The metric fluctuation δg_μν = (2/d)(δρ/ρ)g_μν is **proportional to the metric** — pure trace, with the
traceless (graviton) part **identically zero**.

---

## 3. Classification: PARTIAL — scalar fluctuations, not graviton-like

- **Fluctuations EMERGE statistically**: Poisson δρ/ρ = 1/√N propagates to δg and δR, with the correct
  spacetime-foam scaling.
- **But they are SCALAR (conformal)**, not graviton-like: δg_μν = (2/d)(δρ/ρ)g_μν is pure trace, so the
  transverse-traceless graviton modes do NOT fluctuate — they are frozen by conformal flatness (Weyl = 0, QG10).
- Graviton-like (tensor) fluctuations would require relaxing conformal flatness (admitting a dynamical
  Weyl/ψ-field), which AT does not provide.

---

## 4. Conclusion

Event-count fluctuations generate **scalar (conformal) metric/curvature fluctuations** with the correct Poisson
1/√N (spacetime-foam) scaling, but **not graviton-like (tensor) fluctuations** — the graviton modes are frozen
by conformal flatness. This is consistent with the whole dimension arc: AT's gravity is a scalar (conformal)
theory; the tensor (graviton) sector, which would carry the fluctuating gravitational waves, is exactly the
degree of freedom AT freezes out.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG150 `ATQG150_PoissonVariance` | PASS (δρ/ρ = 1/√N) |
| ATQG151 `ATQG151_MetricCurvaturePropagation` | PASS (δg, δR inherit 1/√N) |
| ATQG152 `ATQG152_Classification` | PASS (PARTIAL: scalar, not graviton) |

Code: `AT.Core/ResearchXH/SpacetimeFluctuations.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase15_SpacetimeFluctuationsTests.cs`.
