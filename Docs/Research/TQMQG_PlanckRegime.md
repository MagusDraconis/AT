# TQM-QG Phase 14 — Planck-Regime Audit

**Program:** TQM-QG (Unification)
**Phase:** 14 — does actualization imply a natural minimum length or maximum density?
**Status:** COMPLETED — 3/3 xUnit tests pass (45/45 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

Test whether actualization implies a natural minimum length or maximum density, via maximal event density,
minimum spacing, branching saturation, curvature divergence, and entropy bounds.

---

## 2. Results

### (a) Curvature diverges at ρ=0 (TQMQG140)

For the profile ρ = 1 − x² (vanishing at x=1, d=3), the scalar curvature R ∝ ρ^(−2/3) diverges as ρ → 0:

| x | ρ | R(ρ) | ρ^(−2/3) |
|---|---|---|---|
| 0.0 | 1.000 | −2.667 | 1.00 |
| 0.9 | 0.190 | −8.3 | 3.02 |
| 0.99 | 0.0199 | −36.5 | 13.6 |
| 0.999 | 0.002 | −170 | 63.0 |

The metric √(−g)=ρ degenerates at ρ=0 — a **native lower bound ρ > 0**, the "maximum deficit" (a horizon).

### (b) Branching saturation + minimum cell (TQMQG141)

- Critical μ=1 is the **maximum sustained branching** (μ^50 = 1); supercritical μ=1.1 diverges (μ^50 ≈ 117),
  subcritical μ=0.9 dies (μ^50 ≈ 0.005).
- The minimum cell size ℓ = ρ_max^(−1/d) is set by the **free** maximum density ρ_max — there is no native ℓ.

### (c) Classification (TQMQG142)

**PARTIAL — native bounds, but no native minimum length.**

---

## 3. Classification: PARTIAL

- **Native lower bound ρ > 0**: the metric degenerates and curvature diverges at ρ=0 — a native "maximum
  deficit" (horizon), not a minimum length.
- **Native branching bound μ=1**: critical branching is the maximum sustained actualization rate.
- **No native minimum length ℓ**: ℓ = ρ_max^(−1/d) is set by a free maximum density; the physical Planck length
  ℓ = √(Għ/c³) involves G (native as deficit mass, QG6) and ħ (a free parameter) — so the cutoff scale is not
  derived.

---

## 4. Conclusion

TQM has **native bounds** (ρ > 0 from metric degeneracy/curvature divergence; μ=1 from critical branching) but
**no native minimum length or maximum density**: the Planck-scale cutoff is a free scale, set by ħ (and G).
This is consistent with the LabBook open problem "numerical values of ℓ, τ, ħ — empirical, not derived". The
Planck regime is therefore the one place where TQM requires an external scale, just as the full black-hole
thermodynamics (QG13) requires the external ħ.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG140 `TQMQG140_CurvatureDivergence` | PASS (curvature diverges at ρ=0) |
| TQMQG141 `TQMQG141_BranchingSaturationAndMinimumCell` | PASS (μ=1 max sustained; ℓ free) |
| TQMQG142 `TQMQG142_Classification` | PASS (PARTIAL) |

Code: `TQM.Core/ResearchXH/PlanckRegime.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase14_PlanckRegimeTests.cs`.
