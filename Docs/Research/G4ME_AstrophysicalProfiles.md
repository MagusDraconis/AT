# G4-ME Phase 3 — Astrophysical Plausibility

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-ME)
**Phase:** 3 — can realistic galaxy-scale mass profiles emerge?
**Status:** COMPLETED — 3/3 xUnit tests pass (12/12 G4-ME)
**Constraint:** no imported matter sector, no Einstein equations, no new primitives

---

## 1. Goal

Phase 2 showed the scale-free deficit hierarchy produces Newton-like 1/r² gravity. Here we test whether
realistic galaxy-scale mass profiles (rotation curves, effective-mass profiles, finite extent) can emerge
from deficit hierarchies, and whether the result is stable.

---

## 2. Results

### (a) Power-law hierarchy → Keplerian (G4-ME30)

The power-law deficit ρ = ρ̄ − m₀/(1+r/r₀) (m ∝ 1/r, from Phase 2) gives a rotation curve

  v²(r) = r·|a| ∝ 1/r  (Keplerian, point-mass)

with effective enclosed mass M_eff = v²r → m₀r₀/(dρ̄) = 0.0833 (constant). v²(3)/v²(9) = 2.5 (a flat
curve would give ≈1).

### (b) Abundance-law (log-deficit) hierarchy → flat rotation curve (G4-ME31)

A constant-deficit-per-octave hierarchy (the "abundance law") generates the LOG deficit
m(r) = m₀·ln(Rmax/r)/ln(Rmax/r₀), whose field a = −m₀/(d·ρ·r·ln(Rmax/r₀)) ∝ −1/r gives

  v²(r) = m₀/(d·ρ·ln(Rmax/r₀)) ≈ const  (FLAT)

| r | m(r) | v² | M_eff = v²r |
|---|---|---|---|
| 3.0 | 0.161 | 0.0530 | 0.159 |
| 5.0 | 0.093 | 0.0490 | 0.245 |
| 7.0 | 0.048 | 0.0467 | 0.327 |
| 9.0 | 0.014 | 0.0451 | 0.406 |

- v²(3)/v²(9) = 1.18 (flat; Keplerian would be ≈3)
- v²(9) = 0.0451 matches the analytic asymptote m₀/(d·ρ̄·ln(Rmax/r₀)) = 0.0445 (1% deviation)
- M_eff ∝ r (0.159 → 0.406 over 3× radius) — the **dark-matter-halo form** M(r) ∝ r
- finite-size cutoff: field vanishes beyond Rmax

### (c) Hierarchical void population + stability (G4-ME32)

The discrete annular hierarchy (constant amplitude per octave, finite K) is the discrete form of the log
deficit: at octave midpoints m_ann = m₀(K−k)/K matches m_log = m₀·ln(Rmax/r)/ln(Rmax/r₀) to ≤ 14% in the
inner octaves, and vanishes beyond Rmax. The flat rotation-curve value is **stable/deterministic**: it
depends only on the total deficit depth m₀ and dynamic range ln(Rmax/r₀), NOT on the microscopic void
spacing λ (numeric v²(9) matches the analytic formula to < 10%).

---

## 3. Classification: PLAUSIBLE (MATCH)

Deficit hierarchies CAN generate realistic long-range gravitational environments:

| Galaxy property | TQM deficit hierarchy |
|---|---|
| point-mass / Keplerian v² ∝ 1/r | power-law deficit m ∝ 1/r |
| flat rotation curve v² ≈ const | log deficit m ∝ ln(Rmax/r) |
| dark-matter-halo M(r) ∝ r | M_eff = v²r ∝ r |
| finite galaxy extent | finite-size cutoff at Rmax |
| stability | depends only on (m₀, Rmax, r₀) |

The flat rotation curve — the classic dark-matter signature — emerges natively from a log-deficit
(constant-deficit-per-octave) hierarchy, with NO dark-matter sector.

---

## 4. Conclusion

Galaxy-scale profiles are reproducible: a **power-law** deficit hierarchy gives the Keplerian point-mass
field, while a **log-deficit** (constant-deficit-per-octave, abundance-law) hierarchy gives the flat
rotation curve and the halo-like effective-mass profile M_eff ∝ r, truncated at a finite radius. The flat
rotation curve is the exact analogue of the observed dark-matter signature, produced entirely from the
counting measure ρ. The result is stable under changes of the microscopic void spacing.

---

## Test program

| Test | Verdict |
|---|---|
| G4-ME30 `G4_ME30_PowerLawKeplerianRotationCurve` | PASS (Keplerian, point-mass M_eff) |
| G4-ME31 `G4_ME31_LogDeficitFlatRotationCurve` | PASS (flat v², halo M_eff ∝ r, cutoff) |
| G4-ME32 `G4_ME32_HierarchicalPopulationStability` | PASS (staircase → log deficit, stable) |

Code: `TQM.Core/ResearchXH/DeficitCollective.cs` (added `RotationCurveProxy`, `NewtonianRotationCurve`,
`LogDeficit`, `AnnularDeficit`); tests `TQM.Tests/ResearchXH/G4ME_Phase3_AstrophysicalProfilesTests.cs`.
