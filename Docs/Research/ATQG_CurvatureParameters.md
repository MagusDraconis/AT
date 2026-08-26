# AT-QG Phase 100 — Parameter Origin from Network Curvature

**Program:** AT-QG (Unification)
**Phase:** 100 — can local curvature/deficit patterns determine physical parameters?
**Status:** COMPLETED — 3/3 xUnit tests pass (303/303 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether local curvature or deficit patterns can determine physical parameters. Classify: NO RELATION / PARTIAL RELATION / CURVATURE ORIGIN.

---

## 2. Deficit distributions & triangle defect angles (ATQG1000)

Discrete curvature (deficit angle = 2π − sum of face angles) is real and derived — the same object the G4 program
used to extract curvature from spectra.

---

## 3. Curvature invariants & analogs (ATQG1001)

Curvature is a real geometric observable but DERIVED from the metric (no independent dof), and SM parameters are
INTERNAL — the deficit-angle analogies (mass, mixing) are suggestive, not determinative.

---

## 4. Classification (ATQG1002)

**PARTIAL RELATION.**

- NOT NO RELATION: deficit angles and curvature invariants are real, derived observables;
- NOT CURVATURE ORIGIN: curvature is derived from the metric, and SM parameters are internal — no native mapping;
- PARTIAL RELATION: real derived curvature + suggestive analogy, without value determination.

---

## 5. Conclusion

Network curvature gives a **PARTIAL RELATION** to parameters (derived observable, not curvature origin).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG1000 `ATQG1000_DeficitAndDefect` | PASS (deficit patterns exist) |
| ATQG1001 `ATQG1001_InvariantsAndAnalogs` | PASS (derived, suggestive only) |
| ATQG1002 `ATQG1002_Classification` | PASS (PARTIAL RELATION) |

Code: `AT.Core/ResearchXH/CurvatureParameters.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase100_CurvatureParametersTests.cs`.
