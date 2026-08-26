# AT-QG Phase 75 — First Quantitative Prediction

**Program:** AT-QG (Unification)
**Phase:** 75 — what observable curve/spectrum is uniquely predicted?
**Status:** COMPLETED — 3/3 xUnit tests pass (228/228 AT-QG)
**Constraint:** no new primitives added here

---

## 1. Goal

Find a specific quantitative (curve/spectrum) prediction unique to the unified network theory. Classify: UNIQUE /
TESTABLE / FALSIFIABLE.

---

## 2. The predicted profile (ATQG750)

The black-hole core profile is **M_eff(r) = M(1 − e^(−r³/r_c³))** — the Poisson-saturation curve (QG36), with
exponent 3 = the spatial dimension. M_eff(0) = 0 (regular core), M_eff → M asymptotically.

---

## 3. Uniqueness (ATQG751)

- GR: M_eff = M (singular at r=0);
- Hayward: M r³/(r³+2Mℓ²);
- Bardeen: M r³/(r²+r_g²)^(3/2);
- **Network: M(1−e^(−r³/r_c³))** — a DIFFERENT, unique functional form.

---

## 4. Classification (ATQG752)

**UNIQUE** — and TESTABLE and FALSIFIABLE.

- UNIQUE: the specific 1−e^(−r³/r_c³) curve (exponent 3) is absent from GR and Hayward/Bardeen;
- TESTABLE: via black-hole shadow, ISCO, lensing, and GW ringdown;
- FALSIFIABLE: in principle (caveat: the free core scale r_c).

---

## 5. Conclusion

The first quantitative prediction of the unified network theory is the **specific regular-core profile
M(1−e^(−r³/r_c³))** — a unique, testable, falsifiable curve that distinguishes the network from both singular GR
and the Hayward/Bardeen regular-core models.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG750 `ATQG750_PredictedProfile` | PASS (regular core, exponent 3) |
| ATQG751 `ATQG751_Uniqueness` | PASS (differs from GR/Hayward/Bardeen) |
| ATQG752 `ATQG752_Classification` | PASS (UNIQUE) |

Code: `AT.Core/ResearchXH/FirstQuantitativePrediction.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase75_FirstQuantitativePredictionTests.cs`.
