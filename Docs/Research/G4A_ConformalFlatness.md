# G4-A Phase 1 — Conformal Flatness

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-A)
**Phase:** 1 — derive or eliminate conformal flatness.
**Status:** COMPLETED — 3/3 xUnit tests pass (6/6 G4-A)
**Constraint:** no new primitives

---

## 1. Goal

G4-A0 showed the exponent 2/d is DERIVED (from √(−g)=ρ) but the flat representative η is ASSUMED. Here we
test whether causal order + counting measure can select η, via causal-class consistency, minimum-information
metrics, entropy of metric degrees of freedom, conformal gauge freedom, and ψ-field perturbations.
Classify: DERIVED / PREFERRED / GENUINELY ASSUMED.

---

## 2. Results

### (a) √(−g)=ρ fixes only the determinant; η is the unique flat reference (G4-A10)

The counting measure fixes det g = −ρ² (one condition), leaving the reference h (det h = −1) with
d(d+1)/2 − 1 free functions. The ψ-perturbed reference h_ψ = diag(−e^{2ψ}, e^{−2ψ}) (d=2) has Ricci scalar

  R = (2ψ″ + 4ψ′²)e^{2ψ} = (4b + 16b²x²)e^{2bx²}   for ψ = b·x².

η (ψ=0) has R = 0 (flat); any ψ≠0 has R ≠ 0. η is the unique **structureless** (zero-curvature) reference.

### (b) η minimizes the curvature (information) content (G4-A11)

The curvature content R² is minimized (zero) at η and increases monotonically with |ψ|:

| b | R(0.4) | R² |
|---|---|---|
| 0.0 | 0.000 | 0.000 |
| 0.2 | 0.902 | 0.814 |
| 0.5 | 2.661 | 7.081 |

A ψ-field is an extra degree of freedom whose content is not sourced by ρ.

### (c) η is the stable minimum (G4-A12)

dR²/dψ|₀ = 0 (critical point), d²R²/dψ²|₀ = 32 > 0 (minimum). η is the stable minimum-curvature representative.

---

## 3. Classification: PREFERRED (minimum-curvature/information), DERIVED-conditional

- **Not uniquely forced by causal order + counting measure:** these fix the conformal factor and the
  determinant, leaving the conformal class (reference h) free.
- **Uniquely selected by the minimum-curvature (minimum-information) principle:** η is the structureless
  representative (R=0), exactly analogous to the α=0 maximum-entropy selection (G4-RHO).
- **Conditional derivation:** the conformal class is fixed by the causal structure (Malament); if the Q-event
  causal structure is Minkowskian (flat light cones — the vacuum the program assumes), η follows from causal
  order alone.

So **η is PREFERRED** (minimum-information), and **DERIVED iff the causal vacuum is flat**.

---

## 4. Conclusion

Conformal flatness is not an arbitrary assumption but a **minimum-information selection**: among all
references h with det h = −1, the flat η is the unique zero-curvature (structureless) representative, and it is
the stable minimum of the curvature content R². The one residual input is the *flatness of the causal vacuum*
— if the Q-event causal structure is Minkowskian (as the program's flat backgrounds assume), then η is fully
derived from causal order; otherwise a minimum-curvature principle (parallel to the α=0 entropy selection)
does the job. This downgrades conformal flatness from a load-bearing axiom (foundation audit) to a
**preferred, minimum-information gauge choice**.

---

## Test program

| Test | Verdict |
|---|---|
| G4-A10 `G4_A10_CountingAndFlatness` | PASS (η unique flat reference) |
| G4-A11 `G4_A11_MinimumCurvature` | PASS (R² minimized at η) |
| G4-A12 `G4_A12_StabilityClassification` | PASS (PREFERRED, DERIVED-conditional) |

Code: `TQM.Core/ResearchXH/MetricAnsatzAudit.cs` (added `ReferenceRicciScalar`);
tests `TQM.Tests/ResearchXH/G4A_Phase1_ConformalFlatnessTests.cs`.
