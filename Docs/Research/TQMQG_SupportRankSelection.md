# TQM-QG Phase 9 — Support Rank Selection

> **CORRECTION (QG10):** "conformal efficiency = 1 at d=3" was off by one. Corrected: efficiency = 1/(1+graviton)
> is 1 at **d=2** (forbidden), and among allowed dimensions is maximized at **d=3** (1/3). d=3 (3+1) is the
> single quality-preferred (minimal dynamical) candidate, not d=4. See `TQMQG_InformationDimension.md`.

**Program:** TQM-QG (Unification)
**Phase:** 9 — which support rank d is favored inside higher-dimensional D?
**Status:** COMPLETED — 3/3 xUnit tests pass (30/30 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

Fundamental D may exceed the observable d (QG8: observable dimension = rank of ρ). Here we score efficiency =
useful structure / total complexity across D=5..20, d=3..D, to determine whether a specific support rank is
preferred. Classify: DERIVED / PREFERRED / NOT SELECTED.

---

## 2. Results

### (a) Conformal efficiency is maximized at d=3, independent of D (TQMQG90)

Define **conformal efficiency** = the fraction of the observable metric's d.o.f. NOT frozen by conformal
flatness = 1/(1 + d(d−3)/2):

| d | graviton | conformal efficiency | curvature/d.o.f. |
|---|---|---|---|
| 3 | 0 | **1.000** | 10 |
| 4 | 2 | 0.333 | 15 |
| 5 | 5 | 0.167 | 21 |
| 6 | 9 | 0.100 | 28 |
| … | … | → 0 | → ∞ |

d=3 is the unique dimension with efficiency 1 (nothing frozen); efficiency decreases monotonically for d≥4,
and is independent of the fundamental D.

### (b) Efficiency vs coverage trade-off (TQMQG91)

Two metrics pull in opposite directions:
- **Conformal efficiency** 1/(1+d(d−3)/2) → prefers **d=3** (nothing frozen).
- **Coverage** d(d+1)/(D(D+1)) → prefers **d=D** (no reduction).

There is no single "most efficient" support rank.

### (c) Classification (TQMQG92)

**PREFERRED (d=3 for efficiency, d=4 for minimal dynamics); NOT SELECTED uniquely.**

---

## 3. Classification: PREFERRED, not uniquely SELECTED

- **d=3** is PREFERRED by conformal efficiency — the unique dimension where conformal flatness freezes nothing
  (efficiency 1, Weyl=0), the most efficient observable universe, independent of D.
- **d=4** is PREFERRED as the minimal propagating dimension (2 graviton modes), at efficiency 1/3.
- Efficiency (prefers d=3) and coverage (prefers d=D) **trade off**, so no unique support rank is SELECTED by
  a single criterion.

---

## 4. Conclusion

The observable support rank is **not uniquely selected**: conformal efficiency favors d=3 (most efficient,
nothing frozen), coverage favors d=D (no reduction), and d=4 is the minimal-propagating compromise. The two
quality-preferred candidates — d=3 (conformal-complete) and d=4 (minimal dynamical) — are PREFERRED, not
DERIVED, and the choice between them (and versus d=D) depends on which efficiency metric one prioritizes. This
is consistent with QG5 (support rank is a conserved input) and QG8 (3+1 = the conformal-complete + minimal-
propagating combination).

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG90 `TQMQG90_ConformalEfficiencyLandscape` | PASS (d=3 optimal, monotonic decrease) |
| TQMQG91 `TQMQG91_EfficiencyVsCoverage` | PASS (efficiency vs coverage trade-off) |
| TQMQG92 `TQMQG92_Classification` | PASS (PREFERRED, not uniquely selected) |

Code: `TQM.Core/ResearchXH/EffectiveDimension.cs` (added `ConformalEfficiency`, `CurvaturePerDof`);
tests `TQM.Tests/ResearchXH/TQMQG_Phase9_SupportRankSelectionTests.cs`.
