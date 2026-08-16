# G4-ME Phase 2 — Long-Range Gravity

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-ME)
**Phase:** 2 — search for the origin of long-range gravity
**Status:** COMPLETED — 3/3 xUnit tests pass (9/9 G4-ME)
**Constraint:** no imported matter sector, no Einstein equations, no new primitives

---

## 1. Goal

The single deficit m = ρ̄−ρ gives the correct attractive sign but the WRONG range (the field
a = −(1/d)∇lnρ = +(1/d)∇m/ρ is localized ∝ ∇m). Question: can LONG-RANGE attraction emerge from
COLLECTIVE deficit structures — networks, multi-scale/nested voids, abundance-law distributions?

---

## 2. Results

### (a) Deficit networks remain localized (G4-ME20)

A collection of localized voids still produces a localized field. A single Gaussian void has
a ∝ r·e^(−r²/σ²), which vanishes by r≈2 while 1/r² = 0.25 there; a network of three void shells has
a(4.0) ≈ 0. **Superposition of exponentially-decaying fields cannot produce a 1/r² tail.**

| r | a_Gauss | 1/r² | a/(1/r²) |
|---|---|---|---|
| 0.5 | −0.119 | 4.00 | −0.030 |
| 1.0 | −0.000055 | 1.00 | −0.0001 |
| 2.0 | ≈ 0 | 0.25 | ≈ 0 |

### (b) Nested self-similar hierarchy → 1/r² (G4-ME21)

A SELF-SIMILAR hierarchy — geometric radii R_k = r₀λ^k, amplitudes A_k = A₀λ^(−k), widths σ_k = σ₀λ^k
(one void per logarithmic octave) — makes the cumulative deficit m(r) ∝ 1/r, whose gradient is 1/r²:

- m(r) log-log slope = **−1.09** (target −1)
- |a(r)| log-log slope = **−2.01** (target −2)
- attractive (a < 0) everywhere

### (c) Abundance-law continuum limit → exact 1/r² (G4-ME22)

The continuum limit of the scale-free abundance law n(R) ∝ 1/R is the smooth power-law deficit
ρ = ρ̄ − m₀/(1 + r/r₀) (deficit m = m₀r₀/(r₀+r) ∝ 1/r). Its field

  a = −(1/d) m′/ρ → −m₀r₀/(dρ̄ r²)

is EXACTLY Newtonian 1/r², and the effective enclosed mass M_eff = −a·r² asymptotes to a constant:

  M_eff → m₀r₀/(dρ̄) = 0.5·0.5/3 = 0.0833

M_eff(12) = 0.0784 (94% of the asymptote), matching the Newtonian point-mass form a = −M_eff/r².

---

## 3. Classification: MECHANISM IDENTIFIED

Long-range (Newtonian-form) gravity emerges from the **conformal 1/r tail** of a **scale-free
(self-similar) deficit hierarchy** — NOT from a single localized void.

- A single/localized deficit: field ∝ ∇m (localized, no long range).
- A scale-free hierarchy (one void per octave, amplitude ∝ 1/R): cumulative deficit m ∝ 1/r,
  whose gradient is the 1/r² field — this is the TQM representation of a point-like source
  ρ ≈ 1 − d·M/r (the conformal factor of a Newtonian point mass).

| Quantity | Localized deficit | Scale-free hierarchy |
|---|---|---|
| deficit m(r) | e^(−r²) (compact) | ∝ 1/r (power-law) |
| field a(r) | ∝ r·e^(−r²) (localized) | ∝ 1/r² (Newtonian) |
| effective M_eff | → 0 | → const (point-mass) |

---

## 4. Conclusion

The origin of long-range gravity is the **scale-free (abundance-law) distribution of actualization
deficits**: a self-similar hierarchy of voids generates a deficit m ∝ 1/r, the conformal tail
ρ ≈ 1 − d·M/r, whose log-gradient is the Newtonian 1/r² field. The effective enclosed mass is then a
constant (point-mass form). This answers the G4-ME Phase 1 open question: Newtonian long-range
attraction is reproduced natively **iff** the deficit structure is scale-free (self-similar), not
localized.

---

## Test program

| Test | Verdict |
|---|---|
| G4-ME20 `G4_ME20_DeficitNetworkBaseline` | PASS (networks stay localized) |
| G4-ME21 `G4_ME21_NestedSelfSimilarEmergence` | PASS (m ∝ 1/r, a ∝ 1/r²) |
| G4-ME22 `G4_ME22_AbundanceLawContinuumLimit` | PASS (M_eff → const, exact 1/r²) |

Code: `TQM.Core/ResearchXH/DeficitCollective.cs` (3D radial TQM/Newtonian acceleration, effective
enclosed mass, power-law/nested/compact/Gaussian deficit profiles, log-log fit);
tests `TQM.Tests/ResearchXH/G4ME_Phase2_LongRangeGravityTests.cs`.
