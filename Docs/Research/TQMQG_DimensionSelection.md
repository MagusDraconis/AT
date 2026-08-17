# TQM-QG Phase 3 — Dimension Selection

**Program:** TQM-QG (Unification)
**Phase:** 3 — search for a preferred spacetime dimension (is d=4 special?).
**Status:** COMPLETED — 3/3 xUnit tests pass (12/12 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

QG2 found d≥3 required, d=4 not derived. Here we test whether any native criterion (information density,
curvature efficiency, Einstein-structure richness, graviton degrees of freedom, complexity per degree of
freedom, abundance-law statistics) prefers d=4. Classify d=4: DERIVED / PREFERRED / NOT SPECIAL.

---

## 2. Results

### (a) All native scores are monotonic (TQMQG30)

| d | Einstein richness | graviton | Weyl | a_d | frozen fraction | complexity/d.o.f. |
|---|---|---|---|---|---|---|
| 3 | 10 | 0 | 0 | 0.833 | 0.000 | 10 |
| 4 | 15 | 2 | 10 | 0.750 | 0.667 | 15 |
| 5 | 21 | 5 | 35 | 0.700 | 0.833 | 21 |
| 6 | 28 | 9 | 84 | 0.667 | 0.900 | 28 |
| 7 | 36 | 14 | 168 | 0.643 | 0.933 | 36 |

Every score is **monotonic** (richness/graviton/Weyl/frozen/complexity ↑, a_d ↓): no criterion has a local
extremum at d=4 (or any d≥3). Entropy and abundance statistics are d-independent.

### (b) d=4 is the minimal propagating-gravity dimension (TQMQG31)

Graviton polarizations: d=3 → 0, d=4 → 2, d=5 → 5. d=3 has non-trivial gravity but **no propagating modes**
(static-only); d=4 is the **lowest dimension with propagating graviton modes** (2, the fewest non-zero).

### (c) Classification (TQMQG32)

**NOT SPECIAL natively; PREFERRED only as minimal propagating gravity.**

---

## 3. Classification: NOT SPECIAL (native), PREFERRED (conditional)

- **NOT SPECIAL natively:** every native dimension-score is monotonic in d — no criterion peaks at d=4.
- **The natively-special dimension is d=3:** conformal-complete (Weyl=0, nothing frozen, QG2) and the first
  non-trivial gravity — but with **no propagating modes**.
- **PREFERRED (conditional):** d=4 is the lowest dimension with propagating graviton modes (2 polarizations),
  the minimal dynamical gravity — but this prefers d=4 only under the *imported* requirement that gravity
  propagates (a GR input, not a native TQM consequence: TQM gravity is conformally-flat / scalar-only).

---

## 4. Conclusion

d=4 is **NOT DERIVED** and **NOT SPECIAL** under native criteria — all native quantities scale monotonically
with d, and the natively-distinguished dimension is d=3 (conformal-complete). d=4 is weakly **PREFERRED** only
as the minimal dimension with propagating gravitational waves (2 polarizations), conditional on the imported
"gravity must propagate" requirement. The observed 3+1 dimensionality therefore remains an open, non-derived
input — resolved only by adding a propagation requirement, which TQM's scalar gravity does not itself supply.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG30 `TQMQG30_MonotonicScores` | PASS (all scores monotonic, no d=4 extremum) |
| TQMQG31 `TQMQG31_MinimalPropagatingGravity` | PASS (d=4 minimal propagating dimension) |
| TQMQG32 `TQMQG32_Classification` | PASS (NOT SPECIAL native; PREFERRED conditional) |

Code: `TQM.Core/ResearchXH/DimensionAnalysis.cs` (added `EinsteinRichness`, `FrozenFraction`,
`ComplexityPerDof`); tests `TQM.Tests/ResearchXH/TQMQG_Phase3_DimensionSelectionTests.cs`.
