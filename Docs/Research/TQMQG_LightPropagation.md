# TQM-QG Phase 21 — Light Propagation

**Program:** TQM-QG (Unification)
**Phase:** 21 — must light follow null geodesics in TQM?
**Status:** COMPLETED — 3/3 xUnit tests pass (66/66 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

QG20 assumed classical null geodesics. Here we test whether light must follow null geodesics of the
conformally-flat metric g = ρ^(2/d)η, and the observable consequences. Classify: NULL-GEODESIC / MODIFIED /
EMERGENT.

---

## 2. Results

### (a) Light speed c, redshift present, bending absent (TQMQG210)

- **Speed**: the effective light speed is c = 1, independent of ρ (null geodesics conformally invariant).
- **Redshift**: g_00 = −ρ^(2/d) varies, so light IS gravitationally redshifted (z = (ρ1/ρ2)^(1/d) − 1 > 0).
- **Bending**: null geodesics of a conformally-flat metric are the straight lines of flat space → ZERO lensing.

### (b) Photon emergence (TQMQG211)

The effective light speed is c for ALL ρ — the temporal field does not refract light (no native refractive index).

### (c) Classification (TQMQG212)

**NULL-GEODESIC (conformally invariant), with a specific prediction.**

---

## 3. Classification: NULL-GEODESIC

Light follows the null geodesics of g = ρ^(2/d)η, which are conformally invariant: light propagates at c and is
NOT bent. But g_00 varies, so light IS redshifted — **redshift WITHOUT lensing**.

---

## 4. Conclusion

TQM predicts **gravitational redshift but NO gravitational lensing**: the conformal factor affects timelike
(matter) geodesics and the clock rate (g_00), but leaves null (light) geodesics straight. This is a specific,
falsifiable prediction that DIFFERS from GR (which predicts both redshift and lensing via the non-conformal Weyl
structure). It also corrects G4-O's "lensing" (which was a potential difference, not a deflection angle). An
"EMERGENT" modification (refractive index from photon–temporal-field coupling) would require a non-conformal
coupling — a new primitive, absent here.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG210 `TQMQG210_SpeedRedshiftNoBending` | PASS (speed c, redshift, no bending) |
| TQMQG211 `TQMQG211_PhotonEmergenceTemporalField` | PASS (speed constant, no refraction) |
| TQMQG212 `TQMQG212_Classification` | PASS (NULL-GEODESIC) |

Code: `TQM.Core/ResearchXH/LightPropagation.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase21_LightPropagationTests.cs`.
