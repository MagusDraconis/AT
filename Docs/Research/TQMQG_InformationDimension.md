# TQM-QG Phase 10 — Information-Theoretic Dimension Selection

**Program:** TQM-QG (Unification)
**Phase:** 10 — how much information can an actualization support of dimension d carry?
**Status:** COMPLETED — 3/3 xUnit tests pass (33/33 TQM-QG)
**Constraint:** no new primitives

---

## ⚠️ Correction to QG2/QG3/QG8/QG9

This phase corrects a systematic index error in the earlier dimension phases. The Weyl and graviton formulas were
written with the spacetime form `D(D+1)(D+2)(D-3)/12` and `D(D-3)/2` but evaluated at the *spatial* index `d`
instead of `D = d+1`. The correct forms are:

- Weyl = (d+1)(d+2)(d+3)(d−2)/12  (0 for d≤2, **10 for d=3**)
- graviton = (d+1)(d−2)/2  (0 for d≤2, **2 for d=3**)

**Consequence:** d=3 (3+1 spacetime) is NOT "conformal-complete" — it has 2 graviton polarizations and 10 Weyl
components (which conformal flatness freezes). The conformal-complete dimension is **d=2 (D=3)**, which is
FORBIDDEN (no gravity). The corrected picture is **cleaner and stronger**: 3+1 is the unique minimal *dynamical*
gravity.

---

## 1. Goal

The observable dimension is the support rank of ρ. Here we measure how much information an actualization of
dimension d can carry, and whether any dimension maximizes information efficiency. Classify: DERIVED /
PREFERRED / NOT SPECIAL.

---

## 2. Results

### (a) Capacity, entropy density, and causal connectivity are monotonic (TQMQG100)

| d | capacity | entropy/d | connectivity | complexity (Weyl) |
|---|---|---|---|---|
| 3 | 10 | 1.059 | 8 | 10 |
| 4 | 15 | 0.866 | 16 | 35 |
| 5 | 21 | 0.738 | 32 | 84 |
| 6 | 28 | 0.645 | 64 | 168 |
| 12 | 91 | 0.379 | 4096 | 5460 |

Information capacity (d+1)(d+2)/2, causal connectivity λ^d, and geometry complexity (Weyl) GROW with d; entropy
density (ln d + ln K)/d DECREASES — all monotonic, no interior maximum.

### (b) Propagation efficiency is dimension-independent (TQMQG101)

Reach ∝ R^d, intensity ∝ R^(−(d−1)); their product is **exactly R** — independent of d. Propagation efficiency
selects nothing. Information efficiency = 1/(1+graviton) is maximized at the smallest allowed dimension d=3.

### (c) Classification (TQMQG102)

**NOT SPECIAL (no interior maximum); d=3 (3+1) PREFERRED as the boundary.**

---

## 3. Classification: NOT SPECIAL natively; d=3 (3+1) PREFERRED

- Information capacity, causal connectivity, and geometry complexity GROW with d; entropy density DECREASES;
  propagation efficiency is dimension-INDEPENDENT — no dimension is an interior maximum.
- Information efficiency 1/(1+graviton) is maximized at the smallest allowed dimension **d=3 (3+1)**; the
  conformal-complete d=2 (efficiency 1) is FORBIDDEN (no gravity).
- Therefore no dimension is DERIVED or an interior SPECIAL; **d=3 (3+1) is PREFERRED** as the boundary — the
  minimal dynamical gravity and the maximal-information-efficiency among allowed dimensions.

---

## 4. Conclusion

No dimension maximizes information efficiency in a non-trivial (interior) way — every information-theoretic
quantity is monotonic, and propagation efficiency is exactly dimension-independent. The corrected dimension arc
(QG2–QG10) therefore converges on a single clean statement: **3+1 spacetime (d=3) is the unique minimal
dynamical gravity** — the first dimension with non-trivial Einstein structure and propagating modes (2 graviton),
and the maximal-information-efficiency among all allowed dimensions. This is PREFERRED, not DERIVED.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG100 `TQMQG100_MonotonicInformationQuantities` | PASS (all monotonic) |
| TQMQG101 `TQMQG101_PropagationEfficiencyDimIndependent` | PASS (propagation d-independent; efficiency peaks at d=3) |
| TQMQG102 `TQMQG102_Classification` | PASS (NOT SPECIAL; d=3 PREFERRED) |

Code: `TQM.Core/ResearchXH/InformationDimension.cs` (+ corrected `DimensionAnalysis.WeylComponents`,
`GravitonPolarizations`); tests `TQM.Tests/ResearchXH/TQMQG_Phase10_InformationDimensionTests.cs`.
