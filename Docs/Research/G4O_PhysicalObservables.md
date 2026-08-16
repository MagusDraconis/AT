# G4-O Phase 0 — Physical Observables of the ρ-only Einstein Structure

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-O)
**Phase:** 0 — what measurable consequences follow from Q-events → ρ → G_μν?
**Status:** COMPLETED — 3/3 xUnit tests pass
**Constraint:** no imported matter sector, no Einstein equations; ρ, ∂ρ, ∂²ρ, G_μν only

---

## 1. Goal

Identify the physical observables implied by the ρ-only Einstein structure, measure their sign /
scaling / dimension dependence, and classify each as KNOWN GR-LIKE / TQM-SPECIFIC / UNDECIDED.

---

## 2. Observables and classification

| observable | native form | classification |
|---|---|---|
| curvature–density relation | R = −(1/ρ)(ln ρ)″ (d=2); algebraic, no PDE | **TQM-SPECIFIC** |
| native Poisson equation | ΔΦ + ((d−2)/2)\|∇Φ\|² = −ρ^(2/d)R/(2(d−1)) | **TQM-SPECIFIC** |
| effective potential | Φ = (1/d) ln ρ | TQM-SPECIFIC form |
| geodesic acceleration | a = −∇Φ | **KNOWN GR-LIKE** |
| gravitational redshift | Δν/ν = −ΔΦ | **KNOWN GR-LIKE** |
| lensing deflection | ∝ ΔΦ | **KNOWN GR-LIKE** |
| expansion | H = ρ̇/ρ (0 for static ρ) | **KNOWN GR-LIKE** |
| dimension dependence | Φ, a ∝ 1/d | **TQM-SPECIFIC** |

---

## 3. Results

### (a) Curvature–density + native Poisson (G4-O00)

R = −(lnρ)″/ρ holds exactly (d=2); the native Poisson relation
ΔΦ + ((d−2)/2)|∇Φ|² = −ρ^(2/d)R/(2(d−1)) holds (d=3, residual < 1e−12). The curvature is
**algebraically** fixed by ρ, and the Poisson source is the **curvature** (ρ″ structure), not the
density value (unlike GR's ΔΦ = 4πGρ). → **TQM-SPECIFIC**.

### (b) Acceleration + redshift (G4-O01)

a = −∇Φ holds (GR weak-field form), redshift = −ΔΦ (standard gravitational redshift), with
Φ_eff = (1/d)lnρ. → **KNOWN GR-LIKE** (form), with the TQM-specific potential.

### (c) Lensing + expansion + dimension (G4-O02)

Lensing deflection ∝ ΔΦ (non-zero off-axis), expansion H = ρ̇/ρ = 0 (static), and the potential/
acceleration scale as 1/d. → **KNOWN GR-LIKE** (lensing/expansion), **TQM-SPECIFIC** (1/d scaling).

---

## 4. Conclusion

The ρ-only Einstein structure yields a **mixed** observable spectrum:

- **KNOWN GR-LIKE**: the weak-field phenomenology — acceleration = −∇Φ, gravitational redshift = −ΔΦ,
  lensing ∝ ΔΦ, expansion H = ρ̇/ρ — all follow with the effective potential Φ = (1/d)lnρ. TQM
  reproduces the *form* of Newtonian/GR weak-field gravity.
- **TQM-SPECIFIC**: (i) the curvature is an *algebraic* (not PDE-solved) function of ρ; (ii) the
  Poisson source is the **curvature** (ρ″), not the density value; (iii) the potential and all
  observables carry the conformal-weight factor **1/d**.

The decisive TQM-specific prediction is the **curvature-sourced Poisson equation**: the gravitational
source is the second-derivative (curvature) content of the actualization density, not the density's
value — an observable difference from generic GR (ΔΦ = 4πGρ) that is testable in principle.

---

## Test program

| Test | Verdict |
|---|---|
| G4-O00 `G4_O00_CurvatureDensityAndPoisson` | PASS (R=−(lnρ)″/ρ; native Poisson residual < 1e−12) |
| G4-O01 `G4_O01_AccelerationAndRedshift` | PASS (a=−∇Φ; redshift=−ΔΦ) |
| G4-O02 `G4_O02_LensingExpansionAndDimension` | PASS (lensing ∝ ΔΦ; H=0; 1/d scaling) |

Code: `TQM.Core/ResearchXH/PhysicalObservables.cs`;
tests `TQM.Tests/ResearchXH/G4O_Phase0_PhysicalObservablesTests.cs`.
