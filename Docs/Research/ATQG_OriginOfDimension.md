# AT-QG Phase 2 — Origin of Spacetime Dimension

> **CORRECTION (QG10):** the Weyl/graviton formulas here used the spatial index `d` in the spacetime form.
> Corrected: Weyl = (d+1)(d+2)(d+3)(d−2)/12 and graviton = (d+1)(d−2)/2. So Weyl/graviton vanish for **d≤2**
> (not d≤3), and d=3 (3+1) has 2 graviton + 10 Weyl. The conformal-complete dimension is d=2 (forbidden), not d=3.
> See `ATQG_InformationDimension.md`.

**Program:** AT-QG (Unification)
**Phase:** 2 — can the preferred dimension emerge from actualization statistics?
**Status:** COMPLETED — 3/3 xUnit tests pass (9/9 AT-QG)
**Constraint:** no new primitives

---

## 1. Goal

The gravity chain is derived once d is supplied. Here we test whether any dimension is preferred or uniquely
selected by actualization statistics, entropy, Einstein-structure consistency, conformal-flatness cost, or
branching criticality. Classify: DERIVED / PREFERRED / SUPPLIED.

---

## 2. Results

### (a) Einstein non-triviality requires d ≥ 3 (ATQG20)

The Einstein x-component G_11 = ((d−1)(d−2)/2)(σ′)² vanishes for d=1 (no radial curvature term) and d=2
(degenerate), and is non-zero for d≥3. Gravity (non-trivial Einstein structure) exists only for **d ≥ 3**.

| d | G_11 coeff | G_ii coeff | trace coeff |
|---|---|---|---|
| 1 | 0 | −1 | +0.5 |
| 2 | 0 | 0 | 0 |
| 3 | 1 | 1 | −0.5 |
| 4 | 3 | 2 | −1 |

### (b) Conformal flatness is automatic for d≤3, restrictive for d≥4 (ATQG21)

The Weyl tensor has d(d+1)(d+2)(d−3)/12 independent components — **zero for d≤3**, non-zero for d≥4. The
propagating graviton has d(d−3)/2 polarizations — 0 for d≤3, **2 at d=4**.

| d | Weyl comps | graviton pols |
|---|---|---|
| 2 | 0 | 0 |
| 3 | 0 | 0 |
| 4 | 10 | 2 |
| 5 | 35 | 5 |

In d≤3 conformal flatness is **free** (freezes nothing out); in d≥4 it discards the graviton (2 polarizations
at d=4). The conformal weight a_d = (d+2)/(2d) and metric exponent 2/d are **monotonic** — no special d.

### (c) Classification (ATQG22)

**SUPPLIED** (with a derived lower bound d ≥ 3).

---

## 3. Classification: SUPPLIED (d ≥ 3 derived; no unique selection)

- **No actualization statistic selects d**: the entropy H = ln K is d-independent (allocation over octaves,
  not dimensions); the flat-rotation value v²=|s|/d, conformal weight a_d, and exponent 2/d are all monotonic
  in d — no special value.
- **One DERIVED constraint**: d ≥ 3 is required for non-trivial Einstein structure (gravity).
- **d=3 is the conformal-complete dimension**: the Weyl tensor vanishes identically, so the AT
  conformally-flat gravity freezes out *nothing* — it is complete. d≥4 requires the (assumed) conformal
  flatness to discard the graviton.

Therefore d is **SUPPLIED**, not derived. Among d≥3 the program is dimension-generic, with d=3 the
assumption-free (conformal-complete) case and d=4 the first where gravitational waves (2 polarizations) are
frozen out by conformal flatness.

---

## 4. Conclusion

The spacetime dimension is not derivable from the actualization/gravity program: entropy, branching, and all
dimension-dependent quantities are monotonic or d-independent. The program derives only the **lower bound
d ≥ 3** (needed for gravity), and identifies **d=3 as the conformal-complete dimension** (Weyl≡0, no frozen
degrees of freedom). The observed d=3+1 (or any d≥4) would require an additional principle beyond conformal
scalar gravity — consistent with the LabBook open problem "3+1 dimensionality — not derived".

---

## Test program

| Test | Verdict |
|---|---|
| ATQG20 `ATQG20_EinsteinRequiresDAtLeast3` | PASS (d≥3 for non-trivial gravity) |
| ATQG21 `ATQG21_ConformalFlatnessCost` | PASS (Weyl free d≤3, restrictive d≥4) |
| ATQG22 `ATQG22_Classification` | PASS (SUPPLIED, d≥3 derived) |

Code: `AT.Core/ResearchXH/DimensionAnalysis.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase2_OriginOfDimensionTests.cs`.
