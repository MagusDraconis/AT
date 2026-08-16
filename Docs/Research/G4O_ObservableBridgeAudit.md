# G4-O Phase 5 — Audit of the Observable Gravitational Acceleration

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-O)
**Phase:** 5 — is a = −(1/d)∇lnρ the physical acceleration, or an incorrect observable map?
**Status:** COMPLETED — 3/3 xUnit tests pass (18/18 G4-O)
**Constraint:** no imported matter sector, no Einstein equations

---

## 1. Goal

Determine whether the repulsive a = −(1/d)∇lnρ is (A) a real TQM prediction, (B) an incorrect
observable map, or (C) a signal that ρ must be distinguished from observable matter density.

---

## 2. Results

### (a) Full geodesic motion (G4-O50)

A test particle at rest in a Gaussian peak, integrated along its geodesic, moves
**away from the peak** (x: 0.300 → 0.331 over 8 steps) — the acceleration is the genuine geodesic
motion, not an observational artifact.

### (b) Weak-field + curvature consistency (G4-O51)

a = −∇Φ = −(1/d)∇lnρ exactly, and the curvature relation ΔΦ + (1/2)ρR = 0 (d=2) holds. Across
Gaussian / NFW-like / exponential / uniform-sphere profiles, the conformal acceleration is consistent
and profile-independent (repulsive at peaks, zero in uniform regions).

### (c) ρ vs observable matter density (G4-O52)

| map | acceleration at a peak |
|---|---|
| TQM (ρ as conformal factor) | +0.231 (repulsive) |
| Newton (ρ as matter density) | −0.525 (attractive) |

---

## 3. Classification

**A) the repulsive result is a real TQM prediction, AND C) it requires distinguishing ρ from observable
matter density.**

- **Not B (incorrect map)**: a = −(1/d)∇lnρ is the *direct* geodesic equation (Γ^x_00), verified by
  numerical integration — there is no alternative "observable map" involved.
- **A (real)**: the repulsive/expansive field is the genuine geodesics of g = ρ^(2/d)η.
- **C (distinction)**: ρ (the actualization/counting density) acts as the *conformal factor*, not as
  *matter*. Identifying ρ with matter (Newtonian ΔΦ = 4πGρ) would give attraction, which the native
  program does not produce. Newtonian matter attraction would require a **separate matter primitive**.

---

## 4. Conclusion

The observable gravitational acceleration has been **identified correctly**: a = −(1/d)∇lnρ is the
physical geodesic acceleration of test particles in the actualization-density metric g = ρ^(2/d)η. The
repulsive/expansive behavior is a genuine, convention-independent TQM prediction — but it is the
behavior of the actualization density as a **conformal (scale) factor**, not as matter. The program's
"gravity" is therefore an expansive anti-screening effect, and any Newtonian attractive matter sector
would need a density primitive distinct from the conformal factor (which G4-G4 showed does not exist
natively).

---

## Test program

| Test | Verdict |
|---|---|
| G4-O50 `G4_O50_FullGeodesicMotion` | PASS (particle moves away from the peak) |
| G4-O51 `G4_O51_WeakFieldAndCurvatureAcrossProfiles` | PASS (a=−∇Φ; curvature-consistent) |
| G4-O52 `G4_O52_RhoVsMatterAndClassification` | PASS (conformal repulsive vs matter attractive) |

Code: `TQM.Tests/ResearchXH/G4O_Phase5_ObservableBridgeAuditTests.cs`.
