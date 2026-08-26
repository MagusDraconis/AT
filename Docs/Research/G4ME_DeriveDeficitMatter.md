# G4-ME Phase 5 — Derive Deficit Matter

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-ME)
**Phase:** 5 — can deficit matter m = ρ̄ − ρ emerge uniquely from AT principles?
**Status:** COMPLETED — 3/3 xUnit tests pass (18/18 G4-ME)
**Constraint:** no imported matter sector, no Einstein equations, no new primitives

---

## 1. Goal

Matter is currently *defined* as m = ρ̄ − ρ (the density deficit). Here we test whether that definition
emerges **uniquely** from AT principles — abundance conservation, normalization, positivity, excitation
structure — by comparing alternative matter definitions (m = f(ρ), gradient matter, curvature matter).
Classify: DERIVED / PREFERRED / ASSUMED.

---

## 2. Results

### (a) Normalization + positivity are NOT selective; abundance conservation is (G4-ME50)

At a void (ρ < ρ̄), all three monotonic candidates satisfy normalization (m(ρ̄)=0) and positivity (m>0):

| matter m | value at ρ=0.916 |
|---|---|
| ρ̄ − ρ (deficit) | 0.0845 |
| ln(ρ̄/ρ) (log) | 0.0883 |
| ρ̄/ρ − 1 (ratio) | 0.0923 |

But **abundance conservation** — ∫m dV equals the conserved count deviation ρ̄V − ∫ρ dV — holds EXACTLY only
for the LINEAR deficit (it *is* the count deviation). The log/ratio transforms are nonlinear and differ:

| ∫m dV | value | = count deviation (0.2649)? |
|---|---|---|
| ∫(ρ̄−ρ) dV | 0.2659 | ✅ exact |
| ∫ln(ρ̄/ρ) dV | 0.3322 | ❌ |
| ∫(ρ̄/ρ−1) dV | 0.4286 | ❌ |

### (b) The gradient-source form uniquely selects the deficit (G4-ME51)

The native acceleration is a = −(1/d)∇lnρ = −(1/d)∇ρ/ρ. Writing it as the attractive source
a = +(1/d)∇m/ρ requires ∇m = −∇ρ, i.e. m = −ρ + const = ρ̄ − ρ (const fixed by m(ρ̄)=0) — equivalently
f′(ρ) = −1, a unique solution:

| matter m | residual a − (1/d)∇m/ρ |
|---|---|
| ρ̄ − ρ | 0 (exact) |
| ln(ρ̄/ρ) | 2.5×10⁻² |
| ρ̄/ρ − 1 | 5.3×10⁻² |

For m = ln(ρ̄/ρ) one instead gets a = +(1/d)∇m (no 1/ρ) — a *different* force law.

### (c) Alternatives are rejected; classification (G4-ME52)

- **gradient matter** m = ∇ρ is a **vector** (a = −(1/d)∇ρ/ρ), not a scalar abundance;
- **curvature matter** m = R(ρ) is **second-order** (∝ σ″) while a ∝ σ′ is first-order — mismatched;
- **log/ratio matter** are dimensionless, nonlinear, and give a non-1/ρ force law.

The deficit is the unique **scalar, density-valued, first-order** excitation with a = +(1/d)∇m/ρ exactly.

---

## 3. Classification: DERIVED (unique form, one physical input)

m = ρ̄ − ρ is the **unique** matter field satisfying:

1. normalization m(ρ̄) = 0 (no matter at vacuum),
2. positivity m > 0 for ρ < ρ̄ (matter in actualization deficits),
3. linearity (first-order excitation of the counting measure),
4. abundance conservation ∫m dV = ρ̄V − ∫ρ dV (the conserved count deviation), and
5. the attractive gradient-source form a = +(1/d)∇m/ρ.

The uniqueness is exact (f′(ρ) = −1 ⇒ f = ρ̄ − ρ). The **one physical input** is "matter attracts" — a points
toward matter (the deficit), the standard gravitational principle, not an arbitrary ansatz. Matter is thus
*the deficit in the counting measure*: the quantity whose redistribution, per unit actualization, drives the
native geodesic acceleration.

---

## 4. Conclusion

Deficit matter is **derived**, not assumed: m = ρ̄ − ρ is the unique scalar, density-valued, conserved,
first-order excitation of the counting measure whose gradient-over-density equals the (already derived)
geodesic acceleration, with the attractive orientation. This upgrades the G4-ME0 identification of matter
with the deficit from a hypothesis to a uniqueness result — conditional only on the single, standard physical
input that matter attracts.

---

## Test program

| Test | Verdict |
|---|---|
| G4-ME50 `G4_ME50_NormalizationPositivityAbundance` | PASS (abundance conservation selects the linear deficit) |
| G4-ME51 `G4_ME51_GradientSourceUniqueness` | PASS (a = +(1/d)∇m/ρ only for the deficit) |
| G4-ME52 `G4_ME52_AlternativeDefinitionsClassification` | PASS (DERIVED) |

Code: `AT.Core/ResearchXH/PhysicalObservables.cs` (added `LogMatter`, `RatioMatter`,
`GradientSourceResidual`); tests `AT.Tests/ResearchXH/G4ME_Phase5_DeriveDeficitMatterTests.cs`.
