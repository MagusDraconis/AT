# AT-F Phase 2 — Matter Attraction

**Program:** AT-F (Foundation)
**Phase:** 2 — can attraction itself be derived?
**Status:** COMPLETED — 3/3 xUnit tests pass (6/6 AT-F)
**Constraint:** no new primitives

---

## 1. Goal

Matter = deficit was uniquely selected (G4-ME5) once attraction was assumed. Here we test whether attraction
itself can be derived, via stability principles, abundance minimization, geodesic convergence, deficit
accumulation dynamics, and entropy production — comparing attractive / repulsive / neutral matter definitions.
Classify: DERIVED / PREFERRED / POSTULATED.

---

## 2. Results

### (a) Geodesic convergence derives the sign of gravity (ATF20)

The Raychaudhuri equation dθ/dτ = −R₀₀ (for a shear/rotation-free congruence) determines focusing. For
g = ρ^(2/d)η, the timelike convergence scalar is

  R₀₀ = (1/d)[(ln ρ)″ + ((d−2)/d)((ln ρ)′)²].

At x=0: **void** (ρ=1−Ae^(−x²), minimum) → R₀₀ = +0.667 (focusing / attraction); **peak** (ρ=1+Ae^(−x²),
maximum) → R₀₀ = −0.222 (divergence / repulsion). The sign of gravity is **derived** from the metric.

### (b) The deficit branch is the stable (clumping) one (ATF21)

∇·a = −(1/d)(ln ρ)″ is negative at the deficit (field converges → matter accumulates, self-focusing = stable)
and positive at the peak (field diverges → matter disperses). Attraction is the branch on which matter forms
stable bound structures.

### (c) Classification (ATF22)

Matter-attraction is **DERIVED** from geodesic convergence + stability.

---

## 3. Classification: DERIVED (conditional on stability of matter)

- **The sign of gravity is DERIVED** from the metric g=ρ^(2/d)η: a = −(1/d)∇lnρ points toward density minima,
  and R₀₀ > 0 (focusing/attraction) exactly at deficits, R₀₀ < 0 (repulsion) at peaks.
- **Matter = deficit is DERIVED from STABILITY**: matter is a stable, self-bound structure (the QM program's
  matter = stabilized wave structures), and only the converging (deficit) branch supports clumping; the peak
  branch disperses.
- **The one input is "matter is stable"** — the defining property of matter from the QM program, not an
  independent gravitational postulate.

This downgrades "matter attracts" (G4-ME5's physical input) from a postulate to a **consequence** of geodesic
convergence + stability.

---

## 4. Conclusion

The three foundation postulates are now all reduced or derived:

| Postulate | Status |
|---|---|
| Conformal flatness (η) | PREFERRED (minimum-information, G4-A1) |
| Indifference (scale-freeness) | PREFERRED (renormalization invariance, AT-F1) |
| **Matter attraction** | **DERIVED (geodesic convergence + stability, AT-F2)** |

The gravity derivation now rests only on the two primitives (causal order, counting measure), the metric
origin √(−g)=ρ (preferred), the two minimum-principle selections (flat η, α=0), and the free parameters (d, G)
— with "matter is stable" imported from the QM program, not the gravity program.

---

## Test program

| Test | Verdict |
|---|---|
| ATF20 `ATF20_GeodesicConvergence` | PASS (R₀₀>0 at deficit, <0 at peak) |
| ATF21 `ATF21_StabilityDeficitAccumulation` | PASS (deficit converges, peak diverges) |
| ATF22 `ATF22_Classification` | PASS (DERIVED) |

Code: `AT.Core/ResearchXH/PhysicalObservables.cs` (added `TimelikeConvergence`,
`AccelerationDivergence`); tests `AT.Tests/ResearchXH/ATF_Phase2_MatterAttractionTests.cs`.
