# G4-A Phase 0 — Metric Ansatz Audit

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-A)
**Phase:** 0 — why exactly g = ρ^(2/d)η?
**Status:** COMPLETED — 3/3 xUnit tests pass
**Constraint:** no new primitives

---

## 1. Goal

The entire gravity program rests on the metric ansatz g = ρ^(2/d)η. Here we audit *why* the exponent is
2/d, testing five candidate requirements — scale invariance, volume-element consistency, counting-measure
preservation, conformal covariance, uniqueness — and determine whether the ansatz is UNIQUE, PREFERRED, or
ASSUMED.

---

## 2. Results

### (a) The exponent k = 2/d is UNIQUELY selected by counting-measure preservation (G4-A00)

For g = ρ^k η: det g = ρ^(kd) det η, so √(−g) = ρ^(kd/2). Counting-measure preservation demands the
invariant volume element equal the counting measure, √(−g) = ρ, i.e. ρ^(kd/2) = ρ ⇒ kd/2 = 1 ⇒ **k = 2/d**.

| k | √(−g) at ρ=1.16 | error \|√(−g)−ρ\|/ρ |
|---|---|---|
| 1/d = 0.333 | 1.077 | 7.2×10⁻² |
| **2/d = 0.667** | **1.160** | **0 (exact)** |
| 3/d = 1.000 | 1.249 | 7.7×10⁻² |
| 4/d = 1.333 | 1.346 | 1.6×10⁻¹ |

The equation has a **unique** solution k = 2/d. The exponent is derived, not assumed.

### (b) Scale invariance and conformal covariance are k-independent (G4-A01)

a = −(k/2)∇lnρ is invariant under ρ → cρ for **every** k, because ∇ln(cρ) = ∇lnρ (the constant c drops out
of the gradient). The acceleration coefficient −k/2 means k enters only as a free magnitude: a/k = −(1/2)∇lnρ
is k-independent. These requirements constrain only ∇lnρ (the conformal structure) — they are consistent with
k=2/d but do **not** select it.

### (c) Conformal flatness is an assumption, not a derived consequence (G4-A02)

√(−g)=ρ fixes only the **determinant** (one scalar condition), not the full metric (d(d+1)/2 components). A
ψ-perturbed metric with the *same* determinant — g_00 = −ρ^(2/d)e^{2ψ}, g_11 = ρ^(2/d)e^{−2ψ/(d−1)} (ψ = b·x)
— has √(−g)=ρ identically but a **different** acceleration:

| metric | √(−g) | acceleration a |
|---|---|---|
| conformally flat ρ^(2/d)η | 1.160 (=ρ) | −0.230 |
| ψ-perturbed (non-flat) | 1.160 (=ρ) | −0.760 |

So conformal flatness is a genuine, physically distinct additional assumption.

---

## 3. Classification: PREFERRED

The ansatz g = ρ^(2/d)η is **PREFERRED**, not UNIQUE and not merely ASSUMED:

- **The exponent k = 2/d is UNIQUE** — uniquely forced by the counting-measure requirement √(−g) = ρ.
- **Conformal flatness (η) is ASSUMED** — √(−g)=ρ fixes only det g; a non-flat metric with the same
  volume element is physically distinct.
- **It is PREFERRED because ρ is the only scalar available** (minimality): with no ψ-field primitive in TQM,
  the metric built from ρ alone is the conformal factor times the vacuum representative η.

---

## 4. Conclusion

The metric ansatz is *half derived, half assumed*. The scaling exponent 2/d is uniquely and rigorously
derived from the single natural requirement that the counting measure is the invariant volume element
(√(−g) = ρ). But the conformally-flat form (the flat representative η) is an additional, minimality-based
assumption — not mathematically forced, since the volume element alone leaves d(d+1)/2 − 1 metric functions
free. Within TQM's "ρ is the only scalar" constraint this is the natural (preferred) choice, but it is a
genuine assumption of the framework, not a theorem.

---

## Test program

| Test | Verdict |
|---|---|
| G4-A00 `G4_A00_VolumeElementUniqueness` | PASS (√(−g)=ρ ⇒ k=2/d uniquely) |
| G4-A01 `G4_A01_ScaleInvarianceNonSelective` | PASS (scale invariance k-independent) |
| G4-A02 `G4_A02_ConformalFlatnessAssumption` | PASS (non-flat metric, same √(−g), different a) |

Code: `TQM.Core/ResearchXH/MetricAnsatzAudit.cs`;
tests `TQM.Tests/ResearchXH/G4A_Phase0_MetricAnsatzAuditTests.cs`.
