# G4-O Phase 4 — Audit of the Physical Interpretation of ρ

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-O)
**Phase:** 4 — what is ρ, and is the repulsive prediction a misidentification?
**Status:** COMPLETED — 3/3 xUnit tests pass (15/15 G4-O)
**Constraint:** no imported matter sector, no Einstein equations

---

## 1. Goal

Determine whether ρ corresponds to matter density, actualization density, event density, conformal
density, or another quantity — and whether the repulsive prediction is genuine physics or a
misidentification of ρ with matter.

---

## 2. Results

### (a) Peak / minimum / vacuum regimes (G4-O40)

| regime | a = −(1/d)∇lnρ |
|---|---|
| density peak (Gaussian) | +0.231 (repulsive / expansive) |
| density minimum (ρ=1+ax²) | −0.124 (toward the minimum) |
| vacuum (uniform ρ) | 0 (no field) |

The native field is the **localized** log-density gradient — it vanishes in vacuum and tracks the
gradient elsewhere, unlike a matter-sourced integral.

### (b) Which density enters the field? (G4-O41)

| interpretation | field | sign at a peak |
|---|---|---|
| raw ρ (matter, a=−∫ρ) | enclosed-mass integral | **−0.525 (attractive)** |
| ln ρ (conformal, a=−∇lnρ) | log-density gradient | **+0.231 (repulsive)** |
| ∇ρ (gradient, a=−∇ρ) | density gradient | +0.751 (toward decreasing density) |

The matter interpretation gives attraction; the conformal/gradient interpretations give repulsion. The
**native** observable is the conformal (ln ρ) gradient.

### (c) What is ρ? (G4-O42)

ρ is the **counting measure** (G4-F): the event/actualization density, which is the **volume element**
(√g = ρ). This forces the conformal factor f = ρ^(2/d) (positive power), hence Φ = (1/d)lnρ and
a = −(1/d)∇lnρ.

---

## 3. Classification

| question | answer |
|---|---|
| ρ = matter density? | ❌ (it is the actualization/event density = counting measure) |
| ρ enters as conformal factor? | ✅ (f = ρ^(2/d), forced by √g = ρ) |
| repulsive prediction genuine? | ✅ **GENUINE** (conformal/scale-factor physics) |
| is it "matter anti-gravity"? | ❌ (it is the expansive effect of the conformal factor, not matter) |

---

## 4. Conclusion

**The repulsive prediction is genuine — but it is the behavior of the actualization density as a
conformal (scale) factor, not as matter.**

ρ is the counting measure (event/actualization density), which the native program identifies as the
volume element √g = ρ, forcing the conformal factor f = ρ^(2/d). A test particle follows geodesics of
g = ρ^(2/d)η and accelerates toward regions of *lower* actualization density (smaller scale) — an
expansive, anti-screening effect, opposite to matter's attractive gravity.

The repulsive prediction is therefore **not a misidentification of ρ with matter**: it is the genuine
gravitational effect of the actualization density acting as the spacetime's conformal factor. A
Newtonian *attractive* matter sector would require a density **distinct** from the conformal factor —
i.e., a separate matter primitive, which the native program deliberately does not import (G4-G4: the
only conserved symmetric tensor is G/κ; there is no independent matter sector).

---

## Test program

| Test | Verdict |
|---|---|
| G4-O40 `G4_O40_PeaksMinimaVacuum` | PASS (peak repulsive, minimum toward-min, vacuum zero) |
| G4-O41 `G4_O41_RawRhoVsLnRhoVsGradRho` | PASS (matter attractive vs conformal repulsive) |
| G4-O42 `G4_O42_Interpretation` | PASS (ρ = counting measure; f = ρ^(2/d) forced) |

Code: `AT.Tests/ResearchXH/G4O_Phase4_RhoInterpretationAuditTests.cs`.
