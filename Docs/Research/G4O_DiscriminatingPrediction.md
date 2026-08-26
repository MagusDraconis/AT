# G4-O Phase 1 — Does the ρ-only Einstein Structure Predict an Observable Difference from GR?

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-O)
**Phase:** 1 — compare a curvature-sourced (AT) vs density-sourced (GR) theory
**Status:** COMPLETED — 3/3 xUnit tests pass (6/6 G4-O)
**Constraint:** no imported matter sector, no Einstein equations

---

## 1. Goal

Determine whether the ρ-only Einstein structure — whose native Poisson source is the log-density
curvature (lnρ)″, not the density value ρ — predicts a *qualitative* observable difference from GR.

| theory | Poisson source |
|---|---|
| **GR** | ΔΦ = 4πGρ (density **value**) |
| **AT** | ΔΦ + ((d−2)/2)\|∇Φ\|² = −ρ^(2/d)R/(2(d−1)) (log-density **curvature**) |

---

## 2. Results

### (a) Uniform density (G4-O10) — STRONG DIFFERENCE

| x | a_GR | a_AT |
|---|---|---|
| 0.6 | −0.6 | 0.0 |

GR has a linear field a = −ρ₀x in a uniform density; AT has **zero** field (a = −∇lnρ = 0).

### (b) Shell density (G4-O11) — STRONG DIFFERENCE

| region | a_GR | a_AT |
|---|---|---|
| outside shell (x=0.8) | −0.85 (long-range) | ~1e−4 (≈ 0) |
| inside shell (x=0.2) | −0.2 | ~1e−4 |

GR has the Newtonian **long-range** (1/r²) field outside a mass shell; AT's field is **localized**
at the shell (∝ ∇ρ) and vanishes (exponentially) outside and inside.

### (c) Double-peak (G4-O12) — STRONG DIFFERENCE

| point | S_GR | S_AT |
|---|---|---|
| density minimum (x=0) | +1.0 | **+0.96** |
| density maximum (x=0.4) | +1.0 | **−29.6** |

The AT source is the **sign-changing** log-density curvature (positive at density minima, negative
at maxima); the GR source is the **always-positive** density value.

---

## 3. Classification

| profile | GR vs AT | classification |
|---|---|---|
| uniform density | field present vs absent | **STRONG DIFFERENCE** |
| shell density | long-range vs localized | **STRONG DIFFERENCE** |
| double-peak | positive-definite vs sign-changing source | **STRONG DIFFERENCE** |

---

## 4. Conclusion

**Yes — the ρ-only Einstein structure predicts a STRONG, qualitative, falsifiable difference from GR.**

The decisive prediction is the **absence of a long-range gravitational field in uniform-density and
shell-exterior regions**: AT's field is proportional to ∇ρ (the actualization-density gradient), so it
vanishes wherever the density is uniform — including *outside* a mass shell, where Newtonian GR has its
signature 1/r² field. Equivalently, AT's gravitational source is the sign-changing log-density curvature
(lnρ)″, not the positive-definite density value.

This is a concrete, in-principle-testable discrimination: **AT predicts no Newtonian field in regions of
uniform actualization density, whereas GR predicts the standard field.** No such prediction was found in
the weak-field acceleration/redshift/lensing *forms* (G4-O0, which are GR-like); the difference is in the
*source* and its locality.

---

## Test program

| Test | Verdict |
|---|---|
| G4-O10 `G4_O10_UniformDensityStrongDifference` | PASS (a_GR = −ρ₀x ≠ 0, a_AT = 0) |
| G4-O11 `G4_O11_ShellDensityStrongDifference` | PASS (long-range vs localized field) |
| G4-O12 `G4_O12_DoublePeakAndClassification` | PASS (sign-changing vs positive-definite source) |

Code: `AT.Core/ResearchXH/PhysicalObservables.cs` (added profiles + `GrSource/AtSource/GrAcceleration/AtAcceleration`);
tests `AT.Tests/ResearchXH/G4O_Phase1_DiscriminatingPredictionTests.cs`.
