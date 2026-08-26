# G4-O Phase 3 — Attempt to Falsify the Discriminating Prediction

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-O)
**Phase:** 3 — is the repulsive/localized prediction physical or an artifact?
**Status:** COMPLETED — 3/3 xUnit tests pass (12/12 G4-O)
**Constraint:** no imported matter sector, no Einstein equations

---

## 1. Goal

Falsify the discriminating prediction by checking whether the native acceleration is an artifact of
sign convention, metric signature, weak-field mapping, local-only curvature extraction, or the G→Φ
reduction. The test computes the geodesic acceleration **directly** from the metric (a = −Γ^x_00 =
+(1/2)g^xx ∂_x g_00) and compares it against the potential-based a = −∇Φ.

---

## 2. Results

### (a) Sign convention (G4-O30)

| case | a = −Γ | sign |
|---|---|---|
| Newtonian (Φ=−GM/r) | −0.100 = −GM/r² | **attractive** (inward) |
| AT ρ=1+ax² (density **minimum** at origin) | −0.123 | toward the minimum |
| AT Gaussian (density **peak** at origin) | +0.231 | **repulsive** (outward) |

The SAME a=−Γ formula gives Newtonian attraction and AT repulsion-around-peaks — the sign is
**fixed** by g_00 = −ρ^(2/d), not a free convention.

### (b) Signature + weak-field (G4-O31)

The exact weak-field potential Φ = (ρ^(2/d)−1)/2 and the linearized σ = (1/d)lnρ give the **same**
acceleration sign; both are positive where ρ > 1 (opposite Newton's Φ = −GM/r < 0 near mass).

### (c) Direct geodesic + gauge (G4-O32)

a_geodesic = −Γ^x_00 equals a_Φ = −∇Φ exactly (max|diff| < 1e−9); the Poisson relation
ΔΦ + (1/2)ρR = 0 (d=2) ties the geodesic potential to the Einstein tensor; g_00 = −1.053 ≠ −1 confirms
the conformal factor is the **physical** metric, not a removable gauge.

---

## 3. Classification: **ROBUST**

The native acceleration is the **genuine geodesic acceleration** of the metric g = ρ^(2/d)η:

a = −Γ^x_00 = −(1/d)∇ln ρ,

consistent with the G→Φ reduction and the Newtonian sign convention. It is **not** an artifact of sign,
signature, weak-field linearization, or gauge choice. Its physical content — the field points toward
density **minima** (repulsive around peaks, "attractive" toward minima), opposite to Newtonian gravity
(pointing toward mass/density maxima) — is therefore a genuine, convention-independent prediction.

**Important correction** (surfaced by this falsification attempt): the ρ = 1+ax² profile used throughout
G4-G has a density *minimum* at the origin, so its native field points *inward* (toward the minimum).
The "repulsive" prediction applies to density **peaks** (Gaussian/NFW/shell), where a = −∇lnρ > 0. Both
are the same physical statement: **AT gravity points toward density minima, not toward mass.**

---

## Test program

| Test | Verdict |
|---|---|
| G4-O30 `G4_O30_SignConventionIsFixed` | PASS (Newton attractive; AT toward minima / repulsive at peaks) |
| G4-O31 `G4_O31_SignatureAndWeakField` | PASS (sign invariant under weak-field + signature) |
| G4-O32 `G4_O32_DirectGeodesicAndGaugeInvariance` | PASS (geodesic = −∇Φ; Poisson consistent; physical gauge) |

Code: `AT.Core/ResearchXH/PhysicalObservables.cs` (added `MetricG00/Ginv`, `GeodesicAcceleration`, `WeakFieldPotential`);
tests `AT.Tests/ResearchXH/G4O_Phase3_FalsificationAttemptTests.cs`.
