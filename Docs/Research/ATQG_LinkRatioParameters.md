# AT-QG Phase 97 — Parameter Ratios from Network Geometry

**Program:** AT-QG (Unification)
**Phase:** 97 — can dimensionless ratios of link lengths determine physical parameters?
**Status:** COMPLETED — 3/3 xUnit tests pass (294/294 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether dimensionless ratios of link lengths can determine physical parameters. Classify: NO RELATION / PARTIAL RELATION / RATIO ORIGIN.

---

## 2. Link-length ratios & triangle geometry (ATQG970)

Physical parameters are dimensionless, and length RATIOS are exactly scale-invariant. Triangle geometry converts
ratios into ANGLES — the natural dimensionless network observable.

---

## 3. Loop geometry, mixing-angle / mass-hierarchy analogs (ATQG971)

CKM/PMNS angles and mass ratios have DIRECT geometric analogs (triangle/loop angles, length ratios). But the
network does not specify WHICH ratio corresponds to WHICH parameter — the values stay free.

---

## 4. Classification (ATQG972)

**PARTIAL RELATION.**

- NOT NO RELATION: length ratios, triangle/loop angles, and mass-ratio analogs are real correspondences;
- NOT RATIO ORIGIN: the network does not determine which ratio gives which value;
- PARTIAL RELATION: the correspondence (angles → angles, ratios → ratios) is direct; the mapping is not derived.

---

## 5. Conclusion

Dimensionless length ratios give a **PARTIAL RELATION** to parameters (direct analog, not ratio origin).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG970 `ATQG970_RatiosAndTriangles` | PASS (ratios → angles) |
| ATQG971 `ATQG971_LoopMixingMass` | PASS (direct analogs, values free) |
| ATQG972 `ATQG972_Classification` | PASS (PARTIAL RELATION) |

Code: `AT.Core/ResearchXH/LinkRatioParameters.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase97_LinkRatioParametersTests.cs`.
